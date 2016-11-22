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
﻿using System.Text.RegularExpressions;
﻿using System.Threading.Tasks;

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
            Contract.Requires(0.000 <= level && 1.000 >= level);

            return SetDeviceLevel(device.DeviceId, level);
        }

        public bool SetDeviceLevel(long id, float level)
        {
            Contract.Requires(0 < id);
            Contract.Requires(0.000 <= level && 1.000 >= level);

            return SetDeviceLevel(id, level, 0);
        }

        public bool SetDeviceLevel(Device device, float level, long time)
        {
            Contract.Requires(null != device);
            Contract.Requires(0.000 <= level && 1.000 >= level);
            Contract.Requires(0 <= time);

            return SetDeviceLevel(device.DeviceId, level, time);
        }

        public bool SetDeviceLevel(long id, float level, long time)
        {
            Contract.Requires(0 < id);
            Contract.Requires(0.000 <= level && 1.000 >= level);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.LEVEL, level},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return 0 == result.ReturnCode;
        }

        public bool SetDeviceSaturation(Device device, float saturation)
        {
            Contract.Requires(null != device);

            return SetDeviceSaturation(device.DeviceId, saturation);
        }

        public bool SetDeviceSaturation(long id, float saturation)
        {
            Contract.Requires(0 < id);
            Contract.Requires(0 <= saturation && 1.000 >= saturation);

            return SetDeviceSaturation(id, saturation, 0);
        }

        public bool SetDeviceSaturation(Device device, float saturation, long time)
        {
            Contract.Requires(null != device);

            return SetDeviceSaturation(device.DeviceId, saturation, time);
        }

        public bool SetDeviceSaturation(long id, float saturation, long time)
        {
            Contract.Requires(0 < id);
            Contract.Requires(0 <= saturation && 1.000 >= saturation);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.SATURATION, saturation}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetDeviceCTemp(Device device, long ctemp)
        {
            Contract.Requires(null != device);

            return SetDeviceCTemp(device.DeviceId, ctemp);
        }

        public bool SetDeviceCTemp(long id, long ctemp)
        {
            Contract.Requires(0 < id);
            Contract.Requires(1000 <= ctemp && 8000 >= ctemp);

            return SetDeviceCTemp(id, ctemp, 0);
        }

        public bool SetDeviceCTemp(Device device, long ctemp, long time)
        {
            Contract.Requires(null != device);

            return SetDeviceCTemp(device.DeviceId, ctemp, time);
        }

        public bool SetDeviceCTemp(long id, long ctemp, long time)
        {
            Contract.Requires(0 < id);
            Contract.Requires(1000 <= ctemp && 8000 >= ctemp);
            Contract.Requires(0 <= time);

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.CTEMP, ctemp},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }

        public bool SetDeviceColor(Device device, string hexColorCode)
        {
            return SetDeviceColor(device.DeviceId, hexColorCode);
        }

        public bool SetDeviceColor(long id, string hexColorCode)
        {
            return SetDeviceColor(id, hexColorCode, 0);
        }

        public bool SetDeviceColor(Device device, string hexColorCode, long time)
        {
            return SetDeviceColor(device.DeviceId, hexColorCode, time);
        }

        public bool SetDeviceColor(long id, string hexColorCode, long time)
        {
            Contract.Requires(0 < id);
            Contract.Requires(Regex.IsMatch(hexColorCode, "^[A-Fa-f0-9]{6}$"));

            var queryParams = new Dictionary<string, object>
            {
                {Constants.QueryParameter.IDX, id},
                {Constants.QueryParameter.COLOR, hexColorCode},
                {Constants.QueryParameter.TIME, time}
            };

            var result = Invoke<OperationResponse>(HttpMethod.Get, Constants.ApiOperation.DEVICE_SET, queryParams, null, null);
            return result.ReturnCode == 0;
        }
    }
}
