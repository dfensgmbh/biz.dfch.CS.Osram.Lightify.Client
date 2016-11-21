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
        internal ICollection<GroupResponse> Groups { get; set; }

        public List<Group> GetGroups()
        {
            var result = Invoke(HttpMethod.Get, Constants.ApiSuffixes.GROUPS, null, null);
            Contract.Assert(!string.IsNullOrWhiteSpace(result));

            var response = BaseDto.DeserializeObject<ICollection<GroupResponse>>(result);
            var groups = new List<Group>();
            foreach (var group in response)
            {
                var test = group.GroupId;
            }
            return default(List<Group>);
        }
    }
}
