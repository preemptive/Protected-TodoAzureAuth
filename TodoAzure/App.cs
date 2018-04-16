/*
   Copyright 2018 PreEmptive Solutions, LLC
   Portions Copyright 2018 Xamarin Inc.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

﻿using System;
using Xamarin.Forms;

namespace TodoAzure
{
    public class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }
        private static Action ExitAction { get; set; }

        public static void Exit()
        {
            ExitAction();
        }

        public App()
        {
            MainPage = new LoginPage();
        }

        public static void Init(IAuthenticate authenticator, Action exitAction)
        {
            Authenticator = authenticator;
            ExitAction = exitAction;
        }

        private const string DisabledPropertyKey = "AppStatus";
        public static void DisableIfCompromised(bool wasCompromised)
        {
            if (!wasCompromised) { return; }
            Current.Properties[DisabledPropertyKey] = new Random().Next();
            SavePropertiesNow();
        }
        public static bool IsDisabled => Current.Properties.ContainsKey(DisabledPropertyKey);

        private static void SavePropertiesNow()
        {
            Current.SavePropertiesAsync().Wait(2000);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

