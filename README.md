Protected-TodoAzureAuth
=======================

This repository demonstrates protecting the Xamarin.Forms sample [TodoAzureAuth](https://developer.xamarin.com/samples/xamarin-forms/WebServices/TodoAzureAuth/) with Root Checks.
When injected into a Xamarin.Android app, Root Checks detect and respond to rooted Android devices.
[Dotfuscator Community Edition (CE)](https://docs.microsoft.com/en-us/visualstudio/ide/dotfuscator/), a .NET protection tool that is included with Visual Studio 2017, can inject Root Checks into your Xamarin.Android app automatically.

To set up and run this sample yourself, see [the Setting Up the Sample section](#setup).

About this Repository
---------------------

This Repository has several branches that illustrate various ways to apply Root Checks to the sample.
For a full list of branches, see this README on the `master` branch.

About the Original Sample
-------------------------

The original TodoAzureAuth sample demonstrates a to-do list application where the data is stored, accessed, and authenticated from an Azure Mobile App instance.

The app functionality is:

* View a list of tasks.
* Add a new item to the list of tasks.
* Set a task's status to 'completed'.

In all cases the tasks are stored in an Azure Mobile App instance.

For more information about this sample see the Xamarin documentation on [Consuming an Azure Mobile App](http://developer.xamarin.com/guides/cross-platform/xamarin-forms/web-services/consuming/azure/) and [Authenticating Users with Azure Mobile Apps](http://developer.xamarin.com/guides/cross-platform/xamarin-forms/web-services/authentication/azure/).

Changes from the Original Sample
--------------------------------

The primary difference between the original TodoAzureAuth and this repository is that [*PreEmptive Protection - Dotfuscator* Community Edition (CE)](https://docs.microsoft.com/en-us/visualstudio/ide/dotfuscator/) has been integrated into the build of `TodoAzure.Droid`, per [the instructions on the Xamarin Blog](https://blog.xamarin.com/protecting-xamarin-apps-dotfuscator/).
Thus, both assemblies for the Android app - `TodoAzure.dll` and `TodoAzure.Droid.dll` - are protected.

Dotfuscator injects a Root Check into the Android app, which operates as follows:

* When the user of the app selects the "Login" button on the initial Login Page, the Root Check detects whether or not the app is running on a device that has been rooted.

Dotfuscator also protects the Android app with renaming obfuscation.
The following changes have been made to the renaming configuration, compared to the default configuration supplied by the build integration:

* Both input assemblies have Library Mode turned off, to allow greater renaming of code elements.
* The `TodoAzure.LoginPage.LastAuthStatus` property has been excluded from renaming, as the name of this property is implicitly used by this property's setter's call to `OnPropertyChanged()`.
* `TodoAzure.TodoItem` and its data properties have been excluded from renaming, as these are used via reflection to access the Azure Mobile App instance's table.

This repository also differs from TodoAzureAuth in the following ways:

* The app is configured to use an Azure Mobile App instance owned by the repository's author. See [the Default Azure Mobile App section](#azure-default).
    * For instructions on how to set up your own instance, see [the Setting up an Azure Mobile App section](#azure).
* Visual Studio App Center Analytics has been integrated into `TodoAzure.Droid`. The following custom events are reported:
    * *Rooted Device Login Attempt*: When the user attempts to log in on a rooted Android device. As of this commit, however, nothing triggers this. Includes a custom property, *Login Status*, indicating the result of the login attempt.
    * *Add Item*: When the user adds an item to the shared to-do list. 
    * *Complete Item*: When the user completes an item on the shared to-do list.
* Authentication errors are now handled by the Login Page's code.
* The result of the last authentication attempt is now shown on the Login Page.
* The Windows Phone 8.1 project (`TodoAzure.WinPhone81`) has been removed, as Visual Studio 2017 does not support this project type.
* The build configurations have been updated such that the `TodoAzure` portable class library (PCL) and one platform-specific project are built for each platform:
    * `TodoAzure.Droid`: AnyCPU
    * `TodoAzure.iOS`: iPhone, iPhoneSimulator
    * `TodoAzure.UWP`: ARM, x64, x86
* Release builds of `TodoAzure.Droid` now support running on x86-based emulators.

<a name="setup"></a>
Setting up the Sample
---------------------

To run this sample application, perform the following steps:

1. Install [the latest version of Dotfuscator CE for Visual Studio 2017](https://www.preemptive.com/products/dotfuscator/downloads).
2. [Register Dotfuscator CE](https://www.preemptive.com/dotfuscator/ce/docs/help/gui_getstarted.html#pctoc-registering-dotfuscator-ce).
3. Open the Android project file, `Droid/TodoAzure.Droid.csproj`, in a text editor and modify the contents of the  `<DotfuscatorXamarinCliPath>` tag to be the absolute path of the Dotfuscator CE command-line interface.
    * This path can be obtained by [opening Dotfuscator CE from Visual Studio](https://www.preemptive.com/dotfuscator/ce/docs/help/gui_getstarted.html#pctoc-starting-dotfuscator-ce), then [opening a Dotfuscator Command Prompt](https://www.preemptive.com/dotfuscator/ce/docs/help/intro_cli.html#pctoc-dotfuscator-command-prompt) and running `where dotfuscatorCLI`.
4. Open `TodoAzure.sln` in Visual Studio 2017.
5. The sample is configured by default to use an Azure Mobile App instance owned by the repository's author. Please review [the Default Azure Mobile App section](#azure-default). If you want to use your own instance instead, follow the instructions in [the Setting up an Azure Mobile App section](#azure).
6. Set up an App Center project by following the [Create your app in the App Center Portal to obtain the App Secret](https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin#2-create-your-app-in-the-app-center-portal-to-obtain-the-app-secret) section of the App Center documentation's *Get Started with Xamarin* page.
    * Select "Android" as your OS.
    * Select "Xamarin" as your Platform.
    * In the new project's *Settings* section, note the **App Secret** in the top-right corner.
7. In the TodoAzure (Portable) project, in the `Constants.cs` file, update the `AppCenterSecretAndroid` constant to the App Secret you noted in step 6.
8. Set your [MSBuild project build output verbosity](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-view-save-and-configure-build-log-files#to-change-the-amount-of-information-included-in-the-build-log) to Normal or a more verbose setting.
9. Build the `TodoAzure.Droid` Xamarin.Android project for the *AnyCPU* platform in the *Release* configuration.
10. Verify that Dotfuscator ran by checking Visual Studio's Output pane. Look for the line `Running Dotfuscator with config file 'DotfuscatorConfig.xml'...`, which indicates where the Dotfuscator output begins.
    * A line such as `[Build Output]    RootCheck: TodoAzure.TodoList::AddItem` indicates where Dotfuscator injected Root Check code. In this case, a Root Check was injected into the `AddItem` method of the `TodoAzure.TodoList` type; note that the names shown are the pre-obfuscated names.
    * You may see the warning `Injecting Android-specific Root Check functionality into non-Android-specific assembly TodoAzure.dll`. This warning can be ignored, because in this case Dotfuscator is processing a copy of `TodoAzure.dll` specifically for use with the Android project, so injecting Android-specific functionality into that copy is not an issue.
11. Deploy this Xamarin.Android app to a device or emulator.

<a name="azure-default"></a>
Default Azure Mobile App
------------------------

An Azure Mobile App instance is required to run TodoAzureAuth.
Protected-TodoAzureAuth is already configured to use such an instance, `https://jsewell-quickstart.azurewebsites.net`, which is owned by the repository's author.
Note the following when using this instance:

* Each user can only view and modify to-do items that user created.
* The author and anyone else who manages the instance can view and modify all of the to-do items. They cannot view user credentials, however, as that process is handled by Google and Microsoft Azure automatically.
* The author makes no uptime guarantees.

You can [set up your own instance and configure Protected-TodoAzureAuth to use it instead](#azure).

<a name="azure"></a>
Setting up an Azure Mobile App
------------------------------

You can create your own Azure Mobile App instance and have Protected-TodoAzureAuth use that instance, rather than [the default instance](#azure-default). To do so:

1. Create a mobile back end by following the [Create a new Mobile Apps back end](
https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started#create-a-new-mobile-apps-back-end
) section of the Azure documentation's *Create a Xamarin.Forms app* page.
2. Configure your instance by following the [Configure the server project](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started#configure-the-server-project)  section of the Azure documentation's *Create a Xamarin.Forms app* page.
    * Select "Xamarin.Forms" as your client platform.
    * Select "Node.js" as your backend language.
3. In your Azure portal, select in the App Services in the navigation menu, choose your Azure Mobile App instance, and view the "Overview" page. Note the value of the "URL" field. This is the **Azure Mobile App instance URL**.
4. Register your Azure Mobile App instance as an app in Google authentication by following the Azure documentation's [How to configure your App Service application to use Google login](https://docs.microsoft.com/en-us/azure/app-service/app-service-mobile-how-to-configure-google-authentication) page.
    * Select "Log in with Google" as the "Action to take when request is not authenticated".
5. Create a redirect URL by following the [Add your app to the Allowed External Redirect URLs](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-users#redirecturl) section of the Azure documentation's *Add authentication to your Xamarin Forms app* page.
    * Note the scheme of this URL (everything before the `://easyauth.callback`). This is the **authentication redirect scheme**.
6. Require authentication by following the *Node.js back end (via the Azure portal)* instructions in the [Restrict permissions to authenticated users](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-users#restrict-permissions-to-authenticated-users) section of the Azure documentation's *Add authentication to your Xamarin Forms app* page.
7. (Optional) Prevent users from seeing and modifying each others' to-do items by doing the following:
    * In your Azure portal, select in the App Services in the navigation menu, choose your Azure Mobile App instance, and view the *Mobile* section's "Easy tables" page.
    * Select the `todoitem` table.
    * Click *Edit script*.
    * In the `todoitem.js` script that appears, uncomment the `table.read`, `table.insert`, `table.update`, and `table.delete` calls. Save the script.
8. Reconfigure the Protected-TodoAzureAuth client code to use your instance and redirect scheme. In the TodoAzure (Portable) project, in the `Constants.cs` file:
    * Update the `AzureMobileAppInstanceURL` constant to the Azure Mobile App instance URL you noted in step 3.
    * Update the `AuthenticationRedirectScheme` constant to the authentication redirect scheme you noted in step 5.
9. Reconfigure the Protected-TodoAzureAuth permissions to use your redirect scheme. Open each of the following files in a text editor and replace `jsewellquickstart` with the authentication redirect scheme you noted in step 5:
    * `Droid/Properties/AndroidManifest.xml`
    * `iOS/Info.plist`
    * `UWP/Package.appxmanifest`

About Dotfuscator
-----------------

[PreEmptive Protection - Dotfuscator](https://www.preemptive.com/products/dotfuscator/overview) is a .NET obfuscation and application hardening solution from PreEmptive Solutions.
Dotfuscator protects your .NET and Xamarin applications from reverse engineering, piracy, and data theft.

This sample uses [Dotfuscator Community Edition](https://docs.microsoft.com/en-us/visualstudio/ide/dotfuscator/), which is included with Visual Studio and provides basic obfuscation and Runtime Check features.
[Dotfuscator Professional Edition](https://www.preemptive.com/products/dotfuscator/overview) offers advanced protection features and ongoing support suitable for commercial projects.
You can [compare editions](https://www.preemptive.com/products/dotfuscator/compare-editions) and [get a free trial of Professional Edition](https://www.preemptive.com/landing/eval-request) through [the PreEmptive Solutions website](https://www.preemptive.com/).

Authors
-------

Original TodoAzureAuth Sample: David Britch  
Protected-TodoAzureAuth: Joe Sewell

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