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
 
﻿using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public partial class Client
    {
        /// <summary>
        /// Turns the specified device on
        /// </summary>
        public bool TurnDeviceOn(Device device)
        {
            Contract.Requires(null != device);

            return TurnDeviceOn(device.DeviceId);
        }

        /// <summary>
        /// Turns the specified device on
        /// </summary>
        public bool TurnDeviceOn(long id)
        {
            Contract.Requires(0 < id);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        /// <summary>
        /// Turns the specified device off
        /// </summary>
        public bool TurnDeviceOff(Device device)
        {
            Contract.Requires(null != device);

            return TurnDeviceOff(device.DeviceId);
        }

        /// <summary>
        /// Turns the specified device off
        /// </summary>
        public bool TurnDeviceOff(long id)
        {
            Contract.Requires(0 < id);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.ON_OFF, 1}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetDeviceLevel(Device device, float level)
        {
            Contract.Requires(null != device);

            return SetDeviceLevel(device.DeviceId, level);
        }

        public bool SetDeviceLevel(long id, float level)
        {
            Contract.Requires(0 < id);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.LEVEL, level}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetDeviceLevel(Device device, float level, long time)
        {
            Contract.Requires(null != device);

            return SetDeviceLevel(device.DeviceId, level, time);
        }

        public bool SetDeviceLevel(long id, float level, long time)
        {
            Contract.Requires(0 < id);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.GROUP_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }
    }
}
