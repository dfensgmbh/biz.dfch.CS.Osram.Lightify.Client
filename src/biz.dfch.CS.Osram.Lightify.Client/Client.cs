﻿/**
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
using System.Diagnostics.Contracts;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public class Client
    {
        private Uri BaseUri { get; set; }

        internal SessionResponse SessionResponse { get; set; }

        public Client()
        {
            // N/A
        }

        public Client(Uri baseUri)
        {
            Contract.Requires(null != baseUri);
            Contract.Requires(baseUri.IsAbsoluteUri);

            this.BaseUri = baseUri;
        }

        public string GetToken(string userName, string password, string serialNumber)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(userName));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            var result = GetToken(BaseUri, userName, password, serialNumber);
            return result;
        }

        public string GetToken(Uri baseUri, string userName, string password, string serialNumber)
        {
            Contract.Requires(null != baseUri);
            Contract.Requires(baseUri.IsAbsoluteUri);
            Contract.Requires(!string.IsNullOrWhiteSpace(userName));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            this.BaseUri = baseUri;

            var sut = new SessionRequest
            {
                Username = userName,
                Password = password,
                SerialNumber = serialNumber
            };
            var body = sut.SerializeObject();
            var client = new RestCallExecutor();
            var requestUri = new Uri(baseUri, Constants.ApiSuffixes.SESSION);
            var result = client.Invoke(HttpMethod.Post, requestUri.AbsoluteUri, null, body);
            Contract.Assert(!string.IsNullOrWhiteSpace(result));

            var response = BaseDto.DeserializeObject<SessionResponse>(result);

            SessionResponse = response;

            return response.SecurityToken;
        }


    }
}
