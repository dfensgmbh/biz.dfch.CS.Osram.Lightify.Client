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
    public partial class Client
    {
        public Device GetDevice(long id)
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Requires(0 < id);

            var requestUri = string.Format("device?idx={0}", id);

            try
            {
                var json = Invoke(HttpMethod.Get, requestUri, null, null);
                var result = BaseDto.DeserializeObject<Device>(json);
                
                Contract.Assert(id == result.DeviceId);
                
                return result;
            }
            catch (Exception ex)
            {
                return default(Device);
            }
        }

        public Device GetDevice(string name)
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Requires(!string.IsNullOrWhiteSpace(name));

            return GetDevice(name, false);
        }

        public Device GetDevice(string name, bool ignoreCase)
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Requires(!string.IsNullOrWhiteSpace(name));

            var devices = GetDevices();

            var comparisonType = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            var devicesMatchingName = devices.Where(e => e.Name.Equals(name, comparisonType));
            // ReSharper disable once PossibleMultipleEnumeration
            Contract.Assert(1 >= devicesMatchingName.Count());

            // ReSharper disable once PossibleMultipleEnumeration
            return devicesMatchingName.FirstOrDefault();
        }
    }
}
