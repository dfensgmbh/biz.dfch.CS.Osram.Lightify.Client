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

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    public class TestConstants
    {
        public static readonly Uri OSRAM_LIGHTIFY_BASE_URI = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");

        public const string USER_ID = "arbitrary.user@example.com";
        public const string SECURITY_TOKEN = "ValidSecurityToken";
        public const string USERNAME = "ArbitraryUser";
        public const string PASSWORD = "ArbitraryPassword";
        public const string SERIAL_NUMBER = "OSR42424242";
        public const string GATEWAY_VERSION = "1.1.3.22-1.2.0.68";
    }
}
