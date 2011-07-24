#region License
//  Copyright 2010-2011 J.W.Marsden
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
#endregion

using System;

using Kinetic.IO;
using Kinetic.Render;
using Kinetic.Resource;

namespace Kinetic
{
	public enum ProviderSelection {
		OpenTK = 0
	}

	public sealed class Provider
	{
		static readonly Provider instance = new Provider();
		
		ProviderSelection providerSelection;
		
		//ResourceManager resourceManager = null;
		
		static Provider() {
		}
		
		Provider () {
			providerSelection = ProviderSelection.OpenTK;
		}
		
		public static Provider Instance {
			get { return instance; }	
		}
		
		public ProviderSelection ProviderSelection {
			get { return providerSelection; }
			set { providerSelection = value; }
		}
		
		public Display CreateDisplay ()
		{
			switch(providerSelection) {
			case ProviderSelection.OpenTK:
				return new OpenTKDisplay();
			}
			return null;
		}
		/*
		public Renderer CreateRenderer (Display display)
		{
			switch(providerSelection) {
			case ProviderSelection.OpenTK:
				return new OpenTKRenderer(display);
			}
			return null;
		}
		
		public ResourceManager CreateResourceManager() {
			switch(providerSelection) {
			case ProviderSelection.OpenTK:
				resourceManager = new OpenTKResourceManager();
				return resourceManager;
			}
			return null;
		}
		
		public ResourceManager GetResourceManager() {
			if(resourceManager == null) {
				resourceManager = CreateResourceManager();
			} 
			return resourceManager;
		}
		*/
	}
}

