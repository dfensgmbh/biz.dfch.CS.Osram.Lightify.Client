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
        public OperationResponse TurnLightGroupOn(Group group)
        {
            return TurnLightGroupOn(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group id on
        /// </summary>

        public OperationResponse TurnLightGroupOn(long id)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }

        /// <summary>
        /// Turns the lights of the specified group id off
        /// </summary>
        public OperationResponse TurnLightGroupOff(Group group)
        {
            return TurnLightGroupOff(group.GroupId);
        }

        /// <summary>
        /// Turns the lights of the specified group off
        /// </summary>
        public OperationResponse TurnLightGroupOff(long id)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }

        public OperationResponse SetLevelGroup(Group group, float level)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, group.GroupId},
                {Constants.QueryParameter.LEVEL, level}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }

        public OperationResponse SetLevelGroup(long groupId, float level)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.LEVEL, level}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }

        public OperationResponse SetLevelGroup(Group group, float level, long time)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, group.GroupId},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }

        public OperationResponse SetLevelGroup(long groupId, float level, long time)
        {
            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, groupId},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            return Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUPSET, queryParams, null, null);
        }
    }
}
