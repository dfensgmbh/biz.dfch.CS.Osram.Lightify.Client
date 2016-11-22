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
 using biz.dfch.CS.Osram.Lightify.Client.Model;
 using biz.dfch.CS.Web.Utilities.Rest;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public partial class Client
    {
        /// <summary>
        /// Turns the lights of the specified group on
        /// </summary>
        public bool TurnLightGroupOn(Group group)
        {
            return TurnLightGroupOn(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group on
        /// </summary>
        public bool TurnLightGroupOn(long id)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
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
            return TurnLightGroupOff(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group off
        /// </summary>
        public bool TurnLightGroupOff(long id)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetGroupLevel(Group group, float level)
        {
            return SetGroupLevel(group.GroupId, level);
        }

        public bool SetGroupLevel(long groupId, float level)
        {
            Contract.Requires(1.000 > level && 0 < level);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.LEVEL, level}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetGroupLevel(Group group, float level, long time)
        {
            return SetGroupLevel(group.GroupId, level, time);
        }

        public bool SetGroupLevel(long groupId, float level, long time)
        {
            Contract.Requires(1.000 > level && 0 < level);
            Contract.Requires(1000 > time && 0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetGroupSaturation(long groupId, float saturation)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(0 < saturation && 1.000 > saturation);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.SATURATION, saturation}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetGroupSaturation(long groupId, float saturation, long time)
        {
            Contract.Requires(0 < groupId);
            Contract.Requires(0 < saturation && 1.000 > saturation);
            Contract.Requires(1000 > time && 0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.SATURATION, saturation}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetGroupSaturation(Group group, float saturation)
        {
            return SetGroupSaturation(group.GroupId, saturation);
        }

        public bool SetGroupSaturation(Group group, float saturation, long time)
        {
            return SetGroupSaturation(group.GroupId, saturation, time);
        }
    }
}
