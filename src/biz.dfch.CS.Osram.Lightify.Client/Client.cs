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
using System.Diagnostics.Contracts;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.General;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public partial class Client
    {
        private Uri BaseUri { get; set; }

        internal UserInformation UserInformation { get; set; }

        public Client(Uri baseUri)
        {
            Contract.Requires(null != baseUri);
            Contract.Requires(baseUri.IsAbsoluteUri);

            this.BaseUri = baseUri;
        }

        public string GetToken(string username, string password, string serialNumber)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(username));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            var result = GetToken(BaseUri, username, password, serialNumber);
            return result;
        }

        public string GetToken(Uri baseUri, string username, string password, string serialNumber)
        {
            Contract.Requires(null != baseUri);
            Contract.Requires(baseUri.IsAbsoluteUri);
            Contract.Requires(!string.IsNullOrWhiteSpace(username));
            Contract.Requires(!string.IsNullOrWhiteSpace(password));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialNumber));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            this.BaseUri = baseUri;

            var sut = new SessionRequest
            {
                Username = username,
                Password = password,
                SerialNumber = serialNumber
            };

            var body = sut.SerializeObject();
            var client = new RestCallExecutor();
            var requestUri = new Uri(baseUri, Constants.ApiOperation.SESSION);
            var result = client.Invoke(HttpMethod.Post, requestUri.AbsoluteUri, null, body);
            Contract.Assert(!string.IsNullOrWhiteSpace(result));

            var response = BaseDto.DeserializeObject<SessionResponse>(result);

            UserInformation = new UserInformation()
            {
                UserId = response.UserId,
                Username = username,
                Password = password,
                SerialNumber = serialNumber,
                SecurityToken = response.SecurityToken
            };

            return response.SecurityToken;
        }

        public T Invoke<T>(HttpMethod httpMethod, string requestUriSuffix, Dictionary<string, object> queryParams, 
            Dictionary<string, string> headers, string body)
            where T : BaseDto
        {
            var result = Invoke(httpMethod, requestUriSuffix, queryParams, headers, body);
            return BaseDto.DeserializeObject<T>(result);
        }

        public string Invoke(HttpMethod httpMethod, string requestUriSuffix, Dictionary<string, object> queryParams, 
            Dictionary<string, string> headers, string body)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(requestUriSuffix));

            if (null != queryParams)
            {
                var queryString = UriHelper.CreateQueryString(queryParams);
                requestUriSuffix = string.Format("{0}?{1}", requestUriSuffix, queryString);
            }

            return Invoke(httpMethod, requestUriSuffix, headers, body);
        }

        public T Invoke<T>(HttpMethod httpMethod, string requestUriSuffix, Dictionary<string, string> headers, string body) 
            where T : BaseDto
        {
            var result = Invoke(httpMethod, requestUriSuffix, headers, body);
            return BaseDto.DeserializeObject<T>(result);
        }

        public string Invoke(HttpMethod httpMethod, string requestUriSuffix, Dictionary<string, string> headers, string body)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(requestUriSuffix));
            Contract.Requires(null == headers || (null != headers && !headers.ContainsKey(Constants.HttpHeaders.AUTHORIZATION)));
            Contract.Requires(null != UserInformation, Constants.Messages.CLIENT_NOT_LOGGED_IN);
            
            if (null == headers)
            {
                headers = new Dictionary<string, string>();
            }

            headers.Add(Constants.HttpHeaders.AUTHORIZATION, UserInformation.SecurityToken);

            var restCallExecutor = new RestCallExecutor();
            var requestUri = new Uri(BaseUri, requestUriSuffix);

            string result;
            try
            {
                result = restCallExecutor.Invoke(httpMethod, requestUri.AbsoluteUri, headers, body ?? "");
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                var sessionResponse = GetToken(UserInformation.Username, UserInformation.Password, UserInformation.SerialNumber);
                Contract.Assert(null != sessionResponse);
                result = restCallExecutor.Invoke(httpMethod, requestUri.AbsoluteUri, headers, body ?? "");
            }

            Contract.Assert(!string.IsNullOrWhiteSpace(result));

            return result;
        }
    }
}

