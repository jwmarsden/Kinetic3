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
using System.Collections;
using System.Collections.Generic;

using Kinetic.Render;

namespace Kinetic.Scene
{
	public class Node: Spatial
	{
		/// <summary>
		/// Holds the related children of the spatial node.
		/// </summary>
		List<Spatial> children;
		
		public Node () {
			children = new List<Spatial>();
		}
		
		public List<Spatial> Children {
			get { return children; }	
		}
		
		public void AttachChild(Spatial child) {
			if(!children.Contains(child)) {
				children.Add(child);
				child.Parent = this;
			}
		}
		
		public void DetachChild(Spatial child) {
			if(children.Contains(child)) {
				children.Remove(child);
				child.Parent = null;
			}
		}
	}
}

