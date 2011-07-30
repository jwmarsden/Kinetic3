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
using System.Drawing;
using System.Drawing.Imaging;

namespace Kinetic.Resource
{
	public class Texture: Asset
	{
		public string _path;
		public Bitmap _textureBitmap;
		
		public Texture ()
		{
			_path = null;
			_textureBitmap = null;
		}
		
		public string Path {
			get { return _path; }
			set { _path = value; }
		}
		
		public Bitmap Bitmap {
			get { return _textureBitmap; }
			set { _textureBitmap = value; }
		}
	}
}

