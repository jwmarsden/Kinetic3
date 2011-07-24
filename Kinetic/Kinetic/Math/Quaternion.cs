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
	public class Quaternion
	{
		float w, x, y, z;

		public Quaternion ()
		{
			this.w = 0f;
			this.x = 0f;
			this.y = 0f;
			this.z = 0f;
		}

		public Quaternion (float w, float x, float y, float z)
		{
			this.w = w;
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public Quaternion (Vector3f axis, float angle) {
			axis.Normalize();
			this.w = QuickMath.Cos(angle);
			float sinAngle = (float) QuickMath.Sin(angle);
			this.x = axis.X * sinAngle;
			this.y = axis.Y * sinAngle;
			this.z = axis.Z * sinAngle;
		}

		public float W {
			get { return this.w; }
			set { this.w = value; }
		}

		public float X {
			get { return this.x; }
			set { this.x = value; }
		}

		public float Y {
			get { return this.y; }
			set { this.y = value; }
		}

		public float Z {
			get { return this.z; }
			set { this.z = value; }
		}
		
		public Quaternion Add (Quaternion b) {
			x = x + b.x;
			y = y + b.y;
			z = z + b.z;
			w = w + b.w;
			return this;
		}
		
		public Quaternion MultiplyScalar (float v) {
			x = v * x;
			y = v * y;
			z = v * z;
			w = v * w;
			return this;
		}
		
		public float Magnitude () {
			return QuickMath.Sqrt(w*w + x*x + y*y + z*z);
		}
		
		public Quaternion Normalize () {
			float magnitude = QuickMath.Sqrt(w*w + x*x + y*y + z*z);
			float recip = 1f/magnitude;
			x = x * recip;
			y = y * recip;
			z = z * recip;
			w = w * recip;
			return this;
		}
		
		public float Dot (Quaternion b) {
			return x * b.x + y * b.y + z * b.z + w * b.w;	
		}
		
		public Matrix3f RotationMatrix3f() {
			float xx = x * x;
			float xy = x * y;
    			float xz = x * z;
			float xw = x * w;
			float yy = y * y;
			float yz = y * z;
			float yw = y * w;
			float zz = z * z;
			float zw = z * w;
			
			Matrix3f rotation = new Matrix3f(
				1 - 2 * ( yy + zz )	, 2 * ( xy + zw )	, 2 * ( xz - yw ) , 
			        2 * ( xy - zw )		, 1 - 2 * ( xx + zz )	, 2 * ( yz + xw ) , 
			        2 * ( xz + yw ) + yz	, 2 * ( yz - xw )	, 1 - 2 * ( xx + yy )	                      
			);
			
			return rotation;
		}
		
		public Matrix4f RotationMatrix4f() {
			float xx = x * x;
			float xy = x * y;
    			float xz = x * z;
			float xw = x * w;
			float yy = y * y;
			float yz = y * z;
			float yw = y * w;
			float zz = z * z;
			float zw = z * w;
			
			Matrix4f rotation = new Matrix4f(
				1 - 2 * ( yy + zz )	, 2 * ( xy + zw )	, 2 * ( xz - yw )	, 0,
			        2 * ( xy - zw )		, 1 - 2 * ( xx + zz )	, 2 * ( yz + xw )	, 0,
			        2 * ( xz + yw ) + yz	, 2 * ( yz - xw )	, 1 - 2 * ( xx + yy )	, 0,
			        0			, 0			, 0			, 1                         
			);
			return rotation;
		}
		
		public override string ToString ()
		{
			return string.Format ("| {0}, {1}, {2}, {3} |", W, X, Y, Z);
		}
	}
}

