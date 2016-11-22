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

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public class Constants
    {
        public static class ApiOperation
        {
            public const string GATEWAY = "gateway";
            public const string GROUPS = "groups";
            public const string SESSION = "session";
            public const string VERSION = "version";
            public const string DEVICE = "device";
            public const string DEVICES = "devices";
        }

        public static class Messages
        {
            public const string CLIENT_NOT_LOGGED_IN = "Perform getToken before using the client.";
        }

        public class HttpHeaders
        {
            public const string AUTHORIZATION = "Authorization";
        }
    }
}
