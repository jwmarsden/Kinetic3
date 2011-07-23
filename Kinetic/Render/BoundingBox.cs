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
using Kinetic.Math;
using Kinetic.Common;

namespace Kinetic.Render
{
	public class BoundingBox: Bound {
		public Vector3f min;
		public Vector3f max;
		
		public BoundingBox() {
			min = new Vector3f(0,0,0);
			max = new Vector3f(0,0,0);
		}
		
		public void CopyTo(ref BoundingBox other) {
			min.CopyTo(other.min);
			max.CopyTo(other.max);
		}
		
		public bool Valid() {
			return (min.x != 0 || max.x != 0) || (min.y != 0 || max.y != 0) || (min.z != 0 || max.z != 0);
		}
		
		public override string ToString ()
		{
			return string.Format ("[BB: Min={0} Max={1}]", min, max);
		}
		
		public FloatBuffer getVerticies() {
			FloatBuffer buffer = new FloatBuffer(72);
			
			// 0 min X min Y min Z
			// 1 max X min Y min Z
			// 2 max X max Y min Z
			// 3 min X max Y min Z
			
			// 01
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
			
			// 12
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			
			// 23
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			
			// 30 
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
						
			// 4 min X min Y max Z
			// 5 max X min Y max Z
			// 6 max X max Y max Z
			// 7 min X max Y max Z
			
			// 45
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(max.z);
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(max.z);	
			
			// 56
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(max.z);
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			
			// 67
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			
			// 74
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(max.z);
					
			// 04
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
			buffer.Put(min.x);
			buffer.Put(min.y);
			buffer.Put(max.z);
			
			// 15
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(min.z);
			buffer.Put(max.x);
			buffer.Put(min.y);
			buffer.Put(max.z);
			
			// 26
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			buffer.Put(max.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			
			// 37
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(min.z);
			buffer.Put(min.x);
			buffer.Put(max.y);
			buffer.Put(max.z);
			
			buffer.Reset();
			return buffer;
		}
	}
}

