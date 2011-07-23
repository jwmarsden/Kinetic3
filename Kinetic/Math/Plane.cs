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

namespace Kinetic.Math
{
	public class Plane
	{
		public Vector3f point;
		public Vector3f normal;
		
		protected Vector3f ab;
		protected Vector3f ac;
		
		public Plane () 
		{
			ab = new Vector3f();
			ac = new Vector3f();
			point = new Vector3f();
			normal = new Vector3f(0,0,1);	
		}
		
		public Plane (Vector3f p, Vector3f n) 
		{
			point = p;
			normal = n;
			ab = new Vector3f();
			ac = new Vector3f();
		}
		
		public Plane (Vector3f a, Vector3f b, Vector3f c)
		{
			ab = b.Subtract(ref a);
			ac = c.Subtract(ref a);
			point = new Vector3f(a.x,a.y,a.z);
			normal = ab.Cross(ref ac).Normalize();
		}
		
		public void @Set(Vector3f p, Vector3f n) 
		{
			this.point = p;
			this.normal = n;
		}
		
		public void @Set(Vector3f a, Vector3f b, Vector3f c) 
		{
			b.Subtract(ref a, ref ab);
			c.Subtract(ref a, ref ac);
			point.@set(a.x,a.y,a.z);
			ab.Cross(ref ac, ref normal).Normalize();
		}
		
		public override string ToString ()
		{
			return string.Format ("[Plane {0} {1}]", point, normal);
		}
	}
}

