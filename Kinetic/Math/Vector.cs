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
	public enum Axis {
		X = 0,
		Y = 1,
		Z = 2
	}

	public class Vector2f 
	{
		public float x;
		public float y;
	
		public static readonly Vector2f ZERO = new Vector2f (0f, 0f);
		public static readonly Vector2f X_UNIT_BASE = new Vector2f (1f, 0f);
		public static readonly Vector2f Y_UNIT_BASE = new Vector2f (0f, 1f);
		
		public Vector2f (float x, float y)
		{
			this.x = x;
			this.y = y;                              
		}
		
		public Vector2f (Vector2f v)
		{
			this.x = v.X;
			this.y = v.Y;
		}

		public float X {
			get { return this.x; }
			set { this.x = value; }
		}

		public float Y {
			get { return this.y; }
			set { this.y = value; }
		}
		
		public float[] toArray() {
			return new float[] {x,y};	
		}
		
		public static Vector2f Zero ()
		{
			return ZERO;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector2f))
				return false;
			
			return this.Equals((Vector2f)obj);
		}
		
		public bool Equals(Vector2f other)
		{
			return x == other.x && y == other.y;
		}
		
		public override int GetHashCode ()
		{
			int hash = 9090;
			hash = hash + x.GetHashCode ();
			hash *= 17;
			hash = hash + y.GetHashCode ();
			hash *= 17;
			return hash;
		}
		
		public object Clone ()
		{
			return this.MemberwiseClone();
		}
		
		public Vector2f CloneVector2f ()
		{
			return (Vector2f) this.MemberwiseClone();
		}
		
		public override string ToString ()
		{
			return string.Format ("[Vector2f: ({0},{1})]", x, y);
		}
		
	}
	
	public class Vector3f : IEquatable<Vector3f>, ICloneable
	{
		public float x;
		public float y;
		public float z;
		
		public static readonly Vector3f ZERO = new Vector3f (0f, 0f, 0f);
		public static readonly Vector3f X_UNIT = new Vector3f (1f, 0f, 0f);
		public static readonly Vector3f Y_UNIT = new Vector3f (0f, 1f, 0f);
		public static readonly Vector3f Z_UNIT = new Vector3f (0f, 0f, 1f);
		
		public Vector3f ()
		{
			this.x = 0;
			this.y = 0;
			this.z = 0;
		}
		
		public Vector3f (float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public Vector3f (Vector3f v)
		{
			this.x = v.X;
			this.y = v.Y;
			this.z = v.Z;
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
		
		public float @get (int i)
		{
			if (i == 0) {
				return x;
			} else if (i == 1) {
				return y;
			} else if (i == 2) {
				return z;
			} else {
				throw new Exception ("Index out of bounds. There are only 3 elements in this vector.");
			}
		}

		public void @set (int i, float v)
		{
			if (i == 0) {
				x = v;
			} else if (i == 1) {
				y = v;
			} else if (i == 2) {
				z = v;
			} else {
				throw new Exception ("Index out of bounds. There are only 3 elements in this vector.");
			}
		}
		
		public void @set(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public void setUnit(Axis unit) {
			if (unit == Axis.X) {
				set(1,0,0);
			} else if (unit == Axis.Y) {
				set(0,1,0);
			} else if (unit == Axis.Z) {
				set(0,0,1);
			}
		}
		
		public Vector3f Reset() {
			set(0f,0f,0f);
			return this;
		}

		public void CopyFrom(ref Vector3f b) {
			x = b.x;
			y = b.y;
			z = b.z;
		}
		
		public void CopyTo(ref Vector3f b) {
			b.x = x;
			b.y = y;
			b.z = z;
		}
		
		public void CopyFrom(Vector3f b) {
			x = b.x;
			y = b.y;
			z = b.z;
		}
		
		public void CopyTo(Vector3f b) {
			b.x = x;
			b.y = y;
			b.z = z;
		}
		
		public Vector3f Negative ()
		{
			x *= -1;
			y *= -1;
			z *= -1;
			return this;
		}

		public Vector3f Negative (Vector3f c)
		{
			c.x = x * -1;
			c.y = y * -1;
			c.z = z * -1;
			return c;
		}
		
		public float MagnitudeSquared ()
		{
			float squaredSum = X * X + Y * Y + Z * Z;
			return squaredSum;
		}

		public float LengthSquared ()
		{
			return MagnitudeSquared ();
		}

		public float Magnitude ()
		{
			return QuickMath.Sqrt (MagnitudeSquared ());
		}

		public float Length ()
		{
			return Magnitude ();
		}

		public Vector3f Normalize () 
		{
			float squaredSum = x * x + y * y + z * z;
			if(squaredSum == 0 || squaredSum < QuickMath.EPSILON) 
			{
				x = y = z = 0;
				return this;
			}
			float lengthRecprocal = 1f/QuickMath.Sqrt(squaredSum); 
			x *= lengthRecprocal;
			y *= lengthRecprocal;
			z *= lengthRecprocal;
			return this;
		}
		
		public Vector3f Normalise () 
		{
			return Normalize ();
		}
		
		public Vector3f Add (ref Vector3f b)
		{
			this.x += b.x;
			this.y += b.y;
			this.z += b.z;
			return this;
		}
		
		public Vector3f Add (ref Vector3f b, ref Vector3f c)
		{
			c.x = x + b.x;
			c.y = y + b.y;
			c.z = z + b.z;
			return c;
		}
		
		public Vector3f Subtract (ref Vector3f b)
		{
			x -= b.X;
			y -= b.Y;
			z -= b.Z;
			return this;
		}
		
		public Vector3f Subtract (ref Vector3f b, ref Vector3f c)
		{
			c.x = x - b.x;
			c.y = y - b.y;
			c.z = z - b.z;
			return c;
		}

		public float Dot (ref Vector3f b)
		{
			return X * b.X + Y * b.Y + Z * b.Z;
		}

		public Vector3f Cross (ref Vector3f b)
		{
			float xCross, yCross, zCross;
			xCross = y * b.Z - z * b.Y;
			yCross = z * b.X - x * b.Z;
			zCross = x * b.Y - y * b.X;
			x = xCross;
			y = yCross;
			z = zCross;
			return this;
		}
		
		public Vector3f Cross (ref Vector3f b, ref Vector3f c)
		{
			float xCross, yCross, zCross;
			xCross = y * b.Z - z * b.Y;
			yCross = z * b.X - x * b.Z;
			zCross = x * b.Y - y * b.X;
			c.x = xCross;
			c.y = yCross;
			c.z = zCross;
			return c;
		}

		public Vector3f AddScalar (float v)
		{
			x += v;
			y += v;
			z += v;
			return this;
		}
		
		public Vector3f AddScalar (float v, ref Vector3f c)
		{
			c.x = x + v;
			c.y = y + v;
			c.z = z + v;
			return c;
		}
		
		public Vector3f SubtractScalar (float v)
		{
			x -= v;
			y -= v;
			z -= v;
			return this;
		}
		
		public Vector3f SubtractScalar (float v, ref Vector3f c)
		{
			c.x = x - v;
			c.y = y - v;
			c.z = z - v;
			return c;
		}

		public Vector3f MultiplyScalar (float v)
		{
			x *= v;
			y *= v;
			z *= v;
			return this;
		}
		
		public Vector3f MultiplyScalar (float v, ref Vector3f c)
		{
			c.x = x * v;
			c.y = y * v;
			c.z = z * v;
			return c;
		}
		
		public Vector3f DivideScalar (float v)
		{
			if (v == 0) {
				throw new DivideByZeroException ("Cannot divide by a scalar equal to zero");
			}
			float scalarReciprocal = 1f/v;
			x *= scalarReciprocal;
			y *= scalarReciprocal;
			z *= scalarReciprocal;
			return this;
		}
		
		public Vector3f DivideScalar (float v, ref Vector3f c)
		{
			if (v == 0) {
				throw new DivideByZeroException ("Cannot divide by a scalar equal to zero");
			}
			float scalarReciprocal = 1f/v;
			c.x = x * scalarReciprocal;
			c.y = y * scalarReciprocal;
			c.z = z * scalarReciprocal;
			return c;
		}

		public Vector3f Transform(Matrix4f transform) {
			float tX = x*transform.m00 + y*transform.m10 + z*transform.m20 + 1.0f * transform.m30;
			float tY = x*transform.m01 + y*transform.m11 + z*transform.m21 + 1.0f * transform.m31;
			float tZ = x*transform.m02 + y*transform.m12 + z*transform.m22 + 1.0f * transform.m32; 
			x = tX;
			y = tY;
			z = tZ;
			return this;
		}
		
		public Vector3f Transform(Matrix4f transform, ref Vector3f transformedVector) {
			float tX = x*transform.m00 + y*transform.m10 + z*transform.m20 + 1.0f * transform.m30;
			float tY = x*transform.m01 + y*transform.m11 + z*transform.m21 + 1.0f * transform.m31;
			float tZ = x*transform.m02 + y*transform.m12 + z*transform.m22 + 1.0f * transform.m32; 
			transformedVector.x = tX;
			transformedVector.y = tY;
			transformedVector.z = tZ;
			return transformedVector;
		}
		
		public float[] toArray() {
			return new float[] {x,y,z};	
		}
		
		public static Vector3f Zero ()
		{
			return ZERO;
		}

		public static Vector3f operator + (Vector3f u, Vector3f v)
		{
			return u.Add (ref v);
		}

		public static Vector3f operator - (Vector3f u, Vector3f v)
		{
			return u.Subtract (ref v);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector3f))
				return false;
			
			return this.Equals((Vector3f)obj);
		}
		
		public bool Equals(Vector3f other)
		{
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode ()
		{
			int hash = 8080;
			hash = hash + x.GetHashCode ();
			hash *= 17;
			hash = hash + y.GetHashCode ();
			hash *= 17;
			hash = hash + z.GetHashCode ();
			hash *= 17;
			return hash;
		}
		
		public object Clone ()
		{
			return this.MemberwiseClone();
		}
		
		public Vector3f CloneVector3f ()
		{
			return (Vector3f) this.MemberwiseClone();
		}
		
		public override string ToString ()
		{
			return string.Format ("[Vector3f: ({0},{1},{2})]", x, y, z);
		}
	}
}

