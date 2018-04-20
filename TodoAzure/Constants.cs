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

namespace TodoAzure
{
    public static class Constants
    {
        // The Azure Mobile App instance URL
        public static string AzureMobileAppInstanceURL = @"https://jsewell-quickstart.azurewebsites.net";

        // The authentication redirect scheme
        // NOTE: If you update this, be sure to also update project specific settings:
        //  * TodoAzure.Droid:  AndroidManifest.xml
        //  * TodoAzure.iOS:    Info.plist
        //  * TodoAzure.UWP:    Package.appxmanifest
        public static string AuthenticationRedirectScheme = "jsewellquickstart";
        
        // The app secret for a Visual Studio App Center project for Xamarin Android
        public static string AppCenterSecretAndroid = "YOUR_APP_SECRET_HERE";
    }
}

