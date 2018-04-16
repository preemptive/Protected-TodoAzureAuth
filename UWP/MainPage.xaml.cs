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

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TodoAzure.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : IAuthenticate
    {
        MobileServiceUser user;

        public MainPage()
        {
            this.InitializeComponent();
            TodoAzure.App.Init(this);
            this.LoadApplication(new TodoAzure.App());
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;

            try
            {
                if (user == null)
                {
                    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(MobileServiceAuthenticationProvider.Google, Constants.AuthenticationRedirectScheme);
                    if (user != null)
                    {
                        var dialog = new MessageDialog(string.Format("You are now logged in - {0}", user.UserId), "Authentication");
                        await dialog.ShowAsync();
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message, "Authentication Failed");
                await dialog.ShowAsync();
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
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();
                    var dialog = new MessageDialog(string.Format("You are now logged out - {0}", user.UserId), "Logout");
                    await dialog.ShowAsync();
                }

                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message, "Logout failed");
                await dialog.ShowAsync();
            }
            return success;
        }
    }
}
