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

namespace Kinetic.Resource
{
	public class Asset
	{
		public int _id;
		public int _handle;
		public string _name;
		public bool _inSystemMemory;
		public bool _inVideoMemory;
		
		public Asset ()
		{
			_id = -1;
			_handle = -1;
			_name = null;
			_inSystemMemory = false;
			_inVideoMemory = false;
		}
		
		public int ID {
			get { return _id; }
			set { _id = value; }
		}
		
		public int Handle {
			get { return _handle; }
			set { _handle = value; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public bool InSystemMemory {
			get { return _inSystemMemory; }	
			set { _inSystemMemory = value; }
		}
		
		public bool InVideoMemory {
			get { return _inVideoMemory; }	
			set { _inVideoMemory = value; }
		}
	}
}

