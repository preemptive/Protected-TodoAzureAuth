Protected-TodoAzureAuth
=======================

This is a modified version of the Xamarin.Forms sample [TodoAzureAuth](https://developer.xamarin.com/samples/xamarin-forms/WebServices/TodoAzureAuth/).

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

This sample differs from the original TodoAzureAuth sample in the following ways:

* The Windows Phone 8.1 project (`TodoAzure.WinPhone81`) has been removed, as Visual Studio 2017 does not support this project type.
* The build configurations have been updated such that the `TodoAzure` portable class library (PCL) and one platform-specific project are built for each platform:
    * `TodoAzure.Droid`: AnyCPU
    * `TodoAzure.iOS`: iPhone, iPhoneSimulator
    * `TodoAzure.UWP`: ARM, x64, x86

Setting up an Azure Mobile App
----------------------------------

In order to run this sample application an Azure Mobile App must first be created. When creating an Azure Mobile App instance please ensure that the service uses a Node.js backend.

For information about how to Create an Azure Mobile App that can be consumed by Xamarin.Forms, see [Create a Xamarin.Forms app](https://azure.microsoft.com/en-gb/documentation/articles/app-service-mobile-xamarin-forms-get-started/) on the Azure website.

For information about how to configure the Azure Mobile App instance to manage the authentication process, see [Add authentication to your Xamarin.Forms app](https://azure.microsoft.com/en-gb/documentation/articles/app-service-mobile-xamarin-forms-get-started-users/) on the Azure website.

Please ensure that the Azure Mobile App authentication options specifies a URL scheme.

Sample Setup
----------------

In order to run this sample application the following steps must be carried out:

1. Update Constants.cs, in the PCL project, to include the URL of the Azure Mobile App, and the URL scheme for the Xamarin.Forms app.
1. In the iOS project, update Info.plist to include the URL scheme.
1. In the Android project, update AndroidManifest.xml to include the URL scheme.
1. In the UWP project, update Package.appxmanifest to include the URL scheme.

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