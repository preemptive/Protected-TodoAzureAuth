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
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace TodoAzure
{
	public class TodoItem
	{
		string id;
		string name;
		bool done;

		[JsonProperty (PropertyName = "id")]
		public string Id {
			get { return id; }
			set { id = value; }
		}

		[JsonProperty (PropertyName = "text")]
		public string Name {
			get { return name; }
			set { name = value; }
		}

		[JsonProperty (PropertyName = "complete")]
		public bool Done {
			get { return done; }
			set { done = value; }
		}

		[Version]
		public string Version { get; set; }
	}
}

