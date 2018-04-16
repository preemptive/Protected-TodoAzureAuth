/*
   Copyright 2018 Xamarin Inc.

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

using System;
using System.Threading.Tasks;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace TodoAzure.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
    {
        MobileServiceUser user;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            App.Init(this);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Google, Constants.URLScheme);
                    if (user != null)
                    {
                        var authAlert = UIAlertController.Create("Authentication", "You are now logged in " + user.UserId, UIAlertControllerStyle.Alert);
                        authAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
                        UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(authAlert, true, null);
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                var authAlert = UIAlertController.Create("Authentication failed", ex.Message, UIAlertControllerStyle.Alert);
                authAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(authAlert, true, null);
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                    {
                        NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                    }
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();

                    var logoutAlert = UIAlertController.Create("Authentication", "You are now logged out " + user.UserId, UIAlertControllerStyle.Alert);
                    logoutAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
                    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(logoutAlert, true, null);
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                var logoutAlert = UIAlertController.Create("Logout failed", ex.Message, UIAlertControllerStyle.Alert);
                logoutAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(logoutAlert, true, null);
            }
            return success;
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return TodoItemManager.DefaultManager.CurrentClient.ResumeWithURL(url);
        }
    }
}
