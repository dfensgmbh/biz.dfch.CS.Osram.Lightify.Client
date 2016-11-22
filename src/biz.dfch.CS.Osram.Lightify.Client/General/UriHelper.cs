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
 
﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.dfch.CS.Osram.Lightify.Client.General
{
    public static class UriHelper
    {
        public const string QUERY_SEPARATOR = "&";

        public static string CreateQueryString(IDictionary<string, object> queryParameters)
        {
            Contract.Requires(null != queryParameters);
            Contract.Requires(queryParameters.Count > 0);

            var queryString = string.Empty;
            var separator = string.Empty;

            foreach (var parameter in queryParameters)
            {
                Contract.Assert(null != parameter.Key);
                Contract.Assert(null != parameter.Value);

                if (parameter.Value is bool)
                {
                    queryString += string.Format("{0}{1}={2}", separator, parameter.Key, parameter.Value.ToString().ToLower());
                }
                else
                {
                    queryString += string.Format("{0}{1}={2}", separator, parameter.Key, parameter.Value);
                }

                separator = QUERY_SEPARATOR;
            }

            return queryString;
        }
    }
}
