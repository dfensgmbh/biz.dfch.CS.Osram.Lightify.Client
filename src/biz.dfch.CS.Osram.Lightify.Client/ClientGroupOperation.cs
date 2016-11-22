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
﻿using System.Text.RegularExpressions;
﻿using System.Threading.Tasks;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
﻿using Group = biz.dfch.CS.Osram.Lightify.Client.Model.Group;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public partial class Client
    {
        /// <summary>
        /// Turns the lights of the specified group on
        /// </summary>
        public bool TurnLightGroupOn(Group group)
        {
            Contract.Requires(null != group);

            return TurnLightGroupOn(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group on
        /// </summary>
        public bool TurnLightGroupOn(long groupId)
        {
            Contract.Requires(0 < groupId);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        /// <summary>
        /// Turns the lights of the specified group off
        /// </summary>
        public bool TurnLightGroupOff(Group group)
        {
            Contract.Requires(null != group);

            return TurnLightGroupOff(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group off
        /// </summary>
        public bool TurnLightGroupOff(long groupId)
        {
            Contract.Requires(0 < groupId);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.ON_OFF, 0}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetGroupLevel(Group group, float level)
        {
            Contract.Requires(null != group);

            return SetGroupLevel(group.GroupId, level);
        }

        public bool SetGroupLevel(long groupId, float level)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(1.000 >= level && 0 <= level);

            return SetGroupLevel(groupId, level, 0);
        }

        public bool SetGroupLevel(Group group, float level, long time)
        {
            Contract.Requires(null != group);

            return SetGroupLevel(group.GroupId, level, time);
        }

        public bool SetGroupLevel(long groupId, float level, long time)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(1.000 >= level && 0 <= level);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetGroupSaturation(Group group, float saturation)
        {
            Contract.Requires(null != group);

            return SetGroupSaturation(group.GroupId, saturation);
        }

        public bool SetGroupSaturation(long groupId, float saturation)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(0 <= saturation && 1.000 >= saturation);

            return SetGroupSaturation(groupId, saturation, 0);
        }

        public bool SetGroupSaturation(Group group, float saturation, long time)
        {
            Contract.Requires(null != group);

            return SetGroupSaturation(group.GroupId, saturation, time);
        }

        public bool SetGroupSaturation(long groupId, float saturation, long time)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(0 <= saturation && 1.000 >= saturation);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.SATURATION, saturation}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetGroupCTemp(Group group, long ctemp)
        {
            Contract.Requires(null != group);

            return SetGroupCTemp(group.GroupId, ctemp);
        }

        public bool SetGroupCTemp(long groupId, long ctemp)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(1000 <= ctemp && 8000 >= ctemp);

            return SetGroupCTemp(groupId, ctemp, 0);
        }

        public bool SetGroupCTemp(Group group, long ctemp, long time)
        {
            Contract.Requires(null != group);

            return SetGroupCTemp(group.GroupId, ctemp, time);
        }

        public bool SetGroupCTemp(long groupId, long ctemp, long time)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(1000 <= ctemp && 8000 >= ctemp);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.CTEMP, ctemp},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetGroupColor(Group group, string hexColorCode)
        {
            return SetGroupColor(group.GroupId, hexColorCode);
        }

        public bool SetGroupColor(long groupId, string hexColorCode)
        {
            return SetGroupColor(groupId, hexColorCode, 0);
        }

        public bool SetGroupColor(Group group, string hexColorCode, long time)
        {
            Contract.Requires(null != group);

            return SetGroupColor(group.GroupId, hexColorCode, time);
        }

        public bool SetGroupColor(long groupId, string hexColorCode, long time)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(Regex.IsMatch(hexColorCode, "^[A-Fa-f0-9]{6}$"));
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.COLOR, hexColorCode},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }
    }
}
