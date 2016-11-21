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

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public partial class Client
    {
        public ICollection<Device> GetDevices()
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Ensures(null != Contract.Result<ICollection<Device>>());

            return default(ICollection<Device>);
        }

        public ICollection<Device> GetDevices(Group group)
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Requires(null != group);
            Contract.Ensures(null != Contract.Result<ICollection<Device>>());

            return default(ICollection<Device>);
        }

        public ICollection<Device> GetDevices(long groupId)
        {
            Contract.Requires(null != SessionResponse && !string.IsNullOrWhiteSpace(SessionResponse.SecurityToken), Constants.Messages.CLIENT_NOT_LOGGED_IN);
            Contract.Requires(0 < groupId);
            Contract.Ensures(null != Contract.Result<ICollection<Device>>());

            return default(ICollection<Device>);
        }
    }
}
