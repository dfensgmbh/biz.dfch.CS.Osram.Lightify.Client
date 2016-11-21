/**
 * Copyright 2016 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public class Login
    {
        private Uri Uri { get; set; }

        internal SessionResponse SessionResponse { get; set; }

        public Login()
        {
            // N/A
        }

        public Login(Uri uri)
        {
            Contract.Requires(null != uri);
            Contract.Requires(uri.IsAbsoluteUri);

            this.Uri = uri;
        }

        public string GetToken(string userName, string password, string serialNumber)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(userName));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            var result = GetToken(Uri, userName, password, serialNumber);
            return result;
        }

        public string GetToken(Uri uri, string userName, string password, string serialNumber)
        {
            Contract.Requires(null != uri);
            Contract.Requires(uri.IsAbsoluteUri);
            Contract.Requires(!string.IsNullOrWhiteSpace(userName));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            var sut = new SessionRequest
            {
                Username = userName,
                Password = password,
                SerialNumber = serialNumber
            };
            var body = sut.SerializeObject();
            var client = new RestCallExecutor();
            var result = client.Invoke(HttpMethod.Post, uri.AbsoluteUri, null, body);
            Contract.Assert(!string.IsNullOrWhiteSpace(result));

            var response = BaseDto.DeserializeObject<SessionResponse>(result);

            SessionResponse = response;

            return response.SecurityToken;
        }
    }
}

