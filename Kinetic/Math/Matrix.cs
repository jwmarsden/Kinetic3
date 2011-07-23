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
	[Serializable]
	public class Matrix3f
	{
		public float m00, m01, m02, m10, m11, m12, m20, m21, m22;

		public Matrix3f ()
		{
			this.m00 = 0f;
			this.m01 = 0f;
			this.m02 = 0f;
			this.m10 = 0f;
			this.m11 = 0f;
			this.m12 = 0f;
			this.m20 = 0f;
			this.m21 = 0f;
			this.m22 = 0f;
		}

		public Matrix3f (float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
		}

		public Matrix3f (float[][] data)
		{
			// TODO: Check the dimensions of data JWM
			m00 = data[0][0];
			m01 = data[0][1];
			m02 = data[0][2];
			m10 = data[1][0];
			m11 = data[1][1];
			m12 = data[1][2];
			m20 = data[2][0];
			m21 = data[2][1];
			m22 = data[2][2];
		}
			
		public float this[int index]
		{
			get { 
				int i = index/3;
				int j = index%3;
				if(!(i<3) || !(j<3)) {
					throw new Exception("Index out of bounds");
				}
				return get(i,j);
			}
			set { 
				int i = index/3;
				int j = index%3;
				if(!(i<3) || !(j<3)) {
					throw new Exception("Index out of bounds");
				}
				set(i,j,value); 
			}
		}

		public float @get (int i, int j)
		{
			if (i == 0 && j == 0) {
				return m00;
			} else if (i == 0 && j == 1) {
				return m01;
			} else if (i == 0 && j == 2) {
				return m02;
			} else if (i == 1 && j == 0) {
				return m10;
			} else if (i == 1 && j == 1) {
				return m11;
			} else if (i == 1 && j == 2) {
				return m12;
			} else if (i == 2 && j == 0) {
				return m20;
			} else if (i == 2 && j == 1) {
				return m21;
			} else if (i == 2 && j == 2) {
				return m22;
			} else {
				throw new Exception ("Out of Matrix Dimension Exception (i and j must be greater then 0 and less than 2)");
			}
		}

		public void @set (int i, int j, float v)
		{
			if (i == 0 && j == 0) {
				m00 = v;
			} else if (i == 0 && j == 1) {
				m01 = v;
			} else if (i == 0 && j == 2) {
				m02 = v;
			} else if (i == 1 && j == 0) {
				m10 = v;
			} else if (i == 1 && j == 1) {
				m11 = v;
			} else if (i == 1 && j == 2) {
				m12 = v;
			} else if (i == 2 && j == 0) {
				m20 = v;
			} else if (i == 2 && j == 1) {
				m21 = v;
			} else if (i == 2 && j == 2) {
				m22 = v;
			} else {
				throw new Exception ("Out of Matrix Dimension Exception (i and j must be greater then 0 and less than 2)");
			}
		}

		public Matrix3f Add (ref Matrix3f b)
		{
			m00 += b.m00;
			m01 += b.m01;
			m02 += b.m02;
			m10 += b.m10;
			m11 += b.m11;
			m12 += b.m12;
			m20 += b.m20;
			m21 += b.m21;
			m22 += b.m22;
			return this;
		}

		public Matrix3f Subtract (ref Matrix3f b)
		{
			m00 -= b.m00;
			m01 -= b.m01;
			m02 -= b.m02;
			m10 -= b.m10;
			m11 -= b.m11;
			m12 -= b.m12;
			m20 -= b.m20;
			m21 -= b.m21;
			m22 -= b.m22;
			return this;
		}

		public Matrix3f Multiply (ref Matrix3f b)
		{
			float c00 = m00 * b.m00 + m10 * b.m01 + m20 * b.m02;
			float c01 = m01 * b.m00 + m11 * b.m01 + m21 * b.m02;
			float c02 = m02 * b.m00 + m12 * b.m01 + m22 * b.m02;
			
			float c10 = m00 * b.m10 + m10 * b.m11 + m20 * b.m12;
			float c11 = m01 * b.m10 + m11 * b.m11 + m21 * b.m12;
			float c12 = m02 * b.m10 + m12 * b.m11 + m22 * b.m12;
			
			float c20 = m00 * b.m20 + m10 * b.m21 + m20 * b.m22;
			float c21 = m01 * b.m20 + m11 * b.m21 + m21 * b.m22;
			float c22 = m02 * b.m20 + m12 * b.m21 + m22 * b.m22;
			
			m00 = c00;
			m01 = c01;
			m02 = c02;
			m10 = c10;
			m11 = c11;
			m12 = c12;
			m20 = c20;
			m21 = c21;
			m22 = c22;
			
			return this;
		}

		public Matrix3f Multiply (ref Matrix3f b, ref Matrix3f c)
		{
			c.m00 = m00 * b.m00 + m10 * b.m01 + m20 * b.m02;
			c.m01 = m01 * b.m00 + m11 * b.m01 + m21 * b.m02;
			c.m02 = m02 * b.m00 + m12 * b.m01 + m22 * b.m02;
			
			c.m10 = m00 * b.m10 + m10 * b.m11 + m20 * b.m12;
			c.m11 = m01 * b.m10 + m11 * b.m11 + m21 * b.m12;
			c.m12 = m02 * b.m10 + m12 * b.m11 + m22 * b.m12;
			
			c.m20 = m00 * b.m20 + m10 * b.m21 + m20 * b.m22;
			c.m21 = m01 * b.m20 + m11 * b.m21 + m21 * b.m22;
			c.m22 = m02 * b.m20 + m12 * b.m21 + m22 * b.m22;
			
			return c;
		}

		public Matrix3f AddScalar (float v)
		{
			m00 += v;
			m01 += v;
			m02 += v;
			m10 += v;
			m11 += v;
			m12 += v;
			m20 += v;
			m21 += v;
			m22 += v;
			return this;
		}

		public Matrix3f SubtractScalar (float v)
		{
			m00 -= v;
			m01 -= v;
			m02 -= v;
			m10 -= v;
			m11 -= v;
			m12 -= v;
			m20 -= v;
			m21 -= v;
			m22 -= v;
			return this;
		}

		public Matrix3f MultiplyScalar (float v)
		{
			m00 *= v;
			m01 *= v;
			m02 *= v;
			m10 *= v;
			m11 *= v;
			m12 *= v;
			m20 *= v;
			m21 *= v;
			m22 *= v;
			return this;
		}

		public Matrix3f DivideScalar (float v)
		{
			if (v == 0) {
				throw new DivideByZeroException ("Cannot divide by a scalar equal to zero");
			}
			float scalarReciprocal = 1f / v;
			m00 *= scalarReciprocal;
			m01 *= scalarReciprocal;
			m02 *= scalarReciprocal;
			m10 *= scalarReciprocal;
			m11 *= scalarReciprocal;
			m12 *= scalarReciprocal;
			m20 *= scalarReciprocal;
			m21 *= scalarReciprocal;
			m22 *= scalarReciprocal;
			return this;
		}

		public Matrix3f Transpose ()
		{
			// [00,01,02]
			// [10,11,12]
			// [20,21,22]
			
			float c01 = m10;
			float c02 = m20;
			float c12 = m21;
			
			m10 = m01;
			m20 = m02;
			m21 = m12;
			
			m01 = c01;
			m02 = c02;
			m12 = c12;
			return this;
		}

		public float Determinant ()
		{
			return m00 * m11 * m22 + m01 * m12 * m20 + m02 * m10 * m21 - m00 * m12 * m21 - m01 * m10 * m22 - m02 * m11 * m20;
		}
		
		public Matrix3f CopyTo (ref Matrix3f matrix) 
		{
			matrix.m00 = m00;
			matrix.m01 = m01;
			matrix.m02 = m02;
			matrix.m10 = m10;
			matrix.m11 = m11;
			matrix.m12 = m12;
			matrix.m20 = m20;
			matrix.m21 = m21;
			matrix.m22 = m22;
			return matrix;
		}
		
		public Matrix3f CopyFrom (ref Matrix3f matrix) 
		{
			m00 = matrix.m00;
			m01 = matrix.m01;
			m02 = matrix.m02;
			m10 = matrix.m10;
			m11 = matrix.m11;
			m12 = matrix.m12;
			m20 = matrix.m20;
			m21 = matrix.m21;
			m22 = matrix.m22;
			return matrix;
		}
		
		public Quaternion Quaternion () {
			float trace = 1 + m00 + m11 + m22;
			float s=0, x=0, y=0, z=0, w=0;
			if(trace != 0 && QuickMath.Abs(trace) >= 0.000001) {
				s = QuickMath.Sqrt(trace) * 2f;
				x = (m12 - m21) / s;
				y = (m20 - m02) / s;
				z = (m01 - m10) / s;
				w = 0.25f * s;
			} else if (m00 > m11 && m00 > m22) {
				s = QuickMath.Sqrt(1.0f + m00 - m11 - m22) * 2f;
				x = 0.25f * s;
				y = (m01 + m10) / s;
				z = (m20 + m02) / s;
				w = (m12 - m21) / s;
			} else if (m11 > m22) {
				s = QuickMath.Sqrt(1.0f + m11 - m00 - m22) * 2f;
				x = (m01 + m10) / s;
				y = 0.25f * s;
				z = (m12 + m21) / s;
				w = (m20 - m02) / s;
			} else {
				s = QuickMath.Sqrt(1.0f + m22 - m00 - m11) * 2f;
				x = (m20 + m02) / s;
				y = (m12 + m21) / s;
				z = 0.25f * s;
				w = (m01 - m10) / s;
			}
			return new Quaternion(w, x, y, z);
		}

		public static Matrix3f Identity ()
		{
			Matrix3f i = new Matrix3f (1, 0, 0, 0, 1, 0, 0, 0, 1);
			return i;
		}

		public override bool Equals (System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null) {
				return false;
			}
			
			// If parameter cannot be cast to Matrix3f return false.
			Matrix3f b = obj as Matrix3f;
			if ((System.Object)b == null) {
				return false;
			}
			
			// Return true if the fields match:
			return Equals (b);
		}

		public bool Equals (Matrix3f b)
		{
			// If parameter is null return false:
			if ((object)b == null) {
				return false;
			}
			
			// Return true if the fields match:
			return m00 == b.m00 && m11 == b.m11 && m22 == b.m22 
				&& m01 == b.m01 && m02 == b.m02 
				&& m10 == b.m10 && m12 == b.m12 
				&& m20 == b.m20 && m21 == b.m21;
		}

		public override int GetHashCode ()
		{
			int hash = 1337;
			hash = hash + m00.GetHashCode ();
			hash *= 17;
			hash = hash + m01.GetHashCode ();
			hash *= 17;
			hash = hash + m02.GetHashCode ();
			hash *= 17;
			hash = hash + m10.GetHashCode ();
			hash *= 17;
			hash = hash + m11.GetHashCode ();
			hash *= 17;
			hash = hash + m12.GetHashCode ();
			hash *= 17;
			hash = hash + m20.GetHashCode ();
			hash *= 17;
			hash = hash + m21.GetHashCode ();
			hash *= 17;
			hash = hash + m22.GetHashCode ();
			hash *= 17;
			return hash;
		}
		
		public bool Equals (Matrix4f b, float epsilon)
		{
			// If parameter is null return false:
			if ((object)b == null) {
				return false;
			}
			
			bool equal = true;
			if(!(QuickMath.Abs(m00-b.m00) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m01-b.m01) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m02-b.m02) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m10-b.m10) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m11-b.m11) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m12-b.m12) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m20-b.m20) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m21-b.m21) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m22-b.m22) <= epsilon)) {
				equal = false;
			}
			return equal;
		}
		
		public override string ToString ()
		{
			string rowFormat = "[{0},{1},{2}]\r\n";
			string output = "\r\n";
			output += string.Format(rowFormat, m00, m01, m02);
			output += string.Format(rowFormat, m10, m11, m12);
			output += string.Format(rowFormat, m20, m21, m22);
			return output;
		}
	}

	[Serializable]
	public class Matrix4f
	{
		public float m00, m01, m02, m03, m10, m11, m12, m13, m20, m21,
		m22, m23, m30, m31, m32, m33;

		public Matrix4f ()
		{
			this.m00 = 0f;
			this.m01 = 0f;
			this.m02 = 0f;
			this.m03 = 0f;
			this.m10 = 0f;
			this.m11 = 0f;
			this.m12 = 0f;
			this.m13 = 0f;
			this.m20 = 0f;
			this.m21 = 0f;
			this.m22 = 0f;
			this.m23 = 0f;
			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 0f;
			
		}

		public Matrix4f (float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21,
		float m22, float m23, float m30, float m31, float m32, float m33)
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;
			this.m03 = m03;
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;
			this.m13 = m13;
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
			this.m23 = m23;
			this.m30 = m30;
			this.m31 = m31;
			this.m32 = m32;
			this.m33 = m33;
		}

		public Matrix4f (float[][] data)
		{
			// TODO: Check the dimensions of data JWM
			m00 = data[0][0];
			m01 = data[0][1];
			m02 = data[0][2];
			m03 = data[0][3];
			m10 = data[1][0];
			m11 = data[1][1];
			m12 = data[1][2];
			m13 = data[1][3];
			m20 = data[2][0];
			m21 = data[2][1];
			m22 = data[2][2];
			m23 = data[2][3];
			m30 = data[3][0];
			m31 = data[3][1];
			m32 = data[3][2];
			m33 = data[3][3];
		}
		
		public Matrix4f (Matrix3f matrix)
		{
			// TODO: Check the dimensions of data JWM
			m00 = matrix.@get(0,0);
			m01 = matrix.@get(0,1);
			m02 = matrix.@get(0,2);
			m03 = 0;
			m10 = matrix.@get(1,0);
			m11 = matrix.@get(1,1);
			m12 = matrix.@get(1,2);
			m13 = 0;
			m20 = matrix.@get(2,0);
			m21 = matrix.@get(2,1);
			m22 = matrix.@get(2,2);
			m23 = 0;
			m30 = 0;
			m31 = 0;
			m32 = 0;
			m33 = 0;
		}
		
		public float this[int index]
		{
			get { 
				int i = index/4;
				int j = index%4;
				if(!(i<4) || !(j<4)) {
					throw new Exception("Index out of bounds");
				}
				return get(i,j);
			}
			set { 
				int i = index/4;
				int j = index%4;
				if(!(i<4) || !(j<4)) {
					throw new Exception("Index out of bounds");
				}
				set(i,j,value); 
			}
		}

		public float @get (int i, int j)
		{
			if (i == 0 && j == 0) {
				return m00;
			} else if (i == 0 && j == 1) {
				return m01;
			} else if (i == 0 && j == 2) {
				return m02;
			} else if (i == 0 && j == 3) {
				return m03;
			} else if (i == 1 && j == 0) {
				return m10;
			} else if (i == 1 && j == 1) {
				return m11;
			} else if (i == 1 && j == 2) {
				return m12;
			} else if (i == 1 && j == 3) {
				return m13;
			} else if (i == 2 && j == 0) {
				return m20;
			} else if (i == 2 && j == 1) {
				return m21;
			} else if (i == 2 && j == 2) {
				return m22;
			} else if (i == 2 && j == 3) {
				return m23;
			} else if (i == 3 && j == 0) {
				return m30;
			} else if (i == 3 && j == 1) {
				return m31;
			} else if (i == 3 && j == 2) {
				return m32;
			} else if (i == 3 && j == 3) {
				return m33;
			} else {
				throw new Exception ("Out of Matrix Dimension Exception (i and j must be greater then 0 and less than 3)");
			}
		}

		public void @set (int i, int j, float v)
		{
			if (i == 0 && j == 0) {
				m00 = v;
			} else if (i == 0 && j == 1) {
				m01 = v;
			} else if (i == 0 && j == 2) {
				m02 = v;
			} else if (i == 0 && j == 3) {
				m03 = v;
			} else if (i == 1 && j == 0) {
				m10 = v;
			} else if (i == 1 && j == 1) {
				m11 = v;
			} else if (i == 1 && j == 2) {
				m12 = v;
			} else if (i == 1 && j == 3) {
				m13 = v;
			} else if (i == 2 && j == 0) {
				m20 = v;
			} else if (i == 2 && j == 1) {
				m21 = v;
			} else if (i == 2 && j == 2) {
				m22 = v;
			} else if (i == 2 && j == 3) {
				m23 = v;
			} else if (i == 3 && j == 0) {
				m30 = v;
			} else if (i == 3 && j == 1) {
				m31 = v;
			} else if (i == 3 && j == 2) {
				m32 = v;
			} else if (i == 3 && j == 3) {
				m33 = v;
			} else {
				throw new Exception ("Out of Matrix Dimension Exception (i and j must be greater then 0 and less than 3)");
			}
		}
		
		public Matrix4f @set (Matrix3f matrix)
		{
			// TODO: Check the dimensions of data JWM
			m00 = matrix.@get(0,0);
			m01 = matrix.@get(0,1);
			m02 = matrix.@get(0,2);
			m03 = 0;
			m10 = matrix.@get(1,0);
			m11 = matrix.@get(1,1);
			m12 = matrix.@get(1,2);
			m13 = 0;
			m20 = matrix.@get(2,0);
			m21 = matrix.@get(2,1);
			m22 = matrix.@get(2,2);
			m23 = 0;
			m30 = 0;
			m31 = 0;
			m32 = 0;
			m33 = 0;
			return this;
		}

		public Matrix4f Add (Matrix4f b)
		{
			m00 += b.m00;
			m01 += b.m01;
			m02 += b.m02;
			m03 += b.m03;
			m10 += b.m10;
			m11 += b.m11;
			m12 += b.m12;
			m13 += b.m13;
			m20 += b.m20;
			m21 += b.m21;
			m22 += b.m22;
			m23 += b.m23;
			m30 += b.m30;
			m31 += b.m31;
			m32 += b.m32;
			m33 += b.m33;
			return this;
		}

		public Matrix4f Subtract (Matrix4f b)
		{
			m00 -= b.m00;
			m01 -= b.m01;
			m02 -= b.m02;
			m03 -= b.m03;
			m10 -= b.m10;
			m11 -= b.m11;
			m12 -= b.m12;
			m13 -= b.m13;
			m20 -= b.m20;
			m21 -= b.m21;
			m22 -= b.m22;
			m23 -= b.m23;
			m30 -= b.m30;
			m31 -= b.m31;
			m32 -= b.m32;
			m33 -= b.m33;
			return this;
		}

		public Matrix4f Multiply (ref Matrix4f b)
		{
			float c00 = m00 * b.m00 + m01 * b.m10 + m02 * b.m20 + m03 * b.m30;
			float c01 = m00 * b.m01 + m01 * b.m11 + m02 * b.m21 + m03 * b.m31;
			float c02 = m00 * b.m02 + m01 * b.m12 + m02 * b.m22 + m03 * b.m32;
			float c03 = m00 * b.m03 + m01 * b.m13 + m02 * b.m23 + m03 * b.m33;
			
			float c10 = m10 * b.m00 + m11 * b.m10 + m12 * b.m20 + m13 * b.m30;
			float c11 = m10 * b.m01 + m11 * b.m11 + m12 * b.m21 + m13 * b.m31;
			float c12 = m10 * b.m02 + m11 * b.m12 + m12 * b.m22 + m13 * b.m32;
			float c13 = m10 * b.m03 + m11 * b.m13 + m12 * b.m23 + m13 * b.m33;
			
			float c20 = m20 * b.m00 + m21 * b.m10 + m22 * b.m20 + m23 * b.m30;
			float c21 = m20 * b.m01 + m21 * b.m11 + m22 * b.m21 + m23 * b.m31;
			float c22 = m20 * b.m02 + m21 * b.m12 + m22 * b.m22 + m23 * b.m32;
			float c23 = m20 * b.m03 + m21 * b.m13 + m22 * b.m23 + m23 * b.m33;
			
			float c30 = m30 * b.m00 + m31 * b.m10 + m32 * b.m20 + m33 * b.m30;
			float c31 = m30 * b.m01 + m31 * b.m11 + m32 * b.m21 + m33 * b.m31;
			float c32 = m30 * b.m02 + m31 * b.m12 + m32 * b.m22 + m33 * b.m32;
			float c33 = m30 * b.m03 + m31 * b.m13 + m32 * b.m23 + m33 * b.m33;
			
			m00 = c00;
			m01 = c01;
			m02 = c02;
			m03 = c03;
			m10 = c10;
			m11 = c11;
			m12 = c12;
			m13 = c13;
			m20 = c20;
			m21 = c21;
			m22 = c22;
			m23 = c23;
			m30 = c30;
			m31 = c31;
			m32 = c32;
			m33 = c33;
			
			return this;
		}

		public Matrix4f Multiply (ref Matrix4f b, ref Matrix4f c)
		{
			c.m00 = m00 * b.m00 + m01 * b.m10 + m02 * b.m20 + m03 * b.m30;
			c.m01 = m00 * b.m01 + m01 * b.m11 + m02 * b.m21 + m03 * b.m31;
			c.m02 = m00 * b.m02 + m01 * b.m12 + m02 * b.m22 + m03 * b.m32;
			c.m03 = m00 * b.m03 + m01 * b.m13 + m02 * b.m23 + m03 * b.m33;
			
			c.m10 = m10 * b.m00 + m11 * b.m10 + m12 * b.m20 + m13 * b.m30;
			c.m11 = m10 * b.m01 + m11 * b.m11 + m12 * b.m21 + m13 * b.m31;
			c.m12 = m10 * b.m02 + m11 * b.m12 + m12 * b.m22 + m13 * b.m32;
			c.m13 = m10 * b.m03 + m11 * b.m13 + m12 * b.m23 + m13 * b.m33;
			
			c.m20 = m20 * b.m00 + m21 * b.m10 + m22 * b.m20 + m23 * b.m30;
			c.m21 = m20 * b.m01 + m21 * b.m11 + m22 * b.m21 + m23 * b.m31;
			c.m22 = m20 * b.m02 + m21 * b.m12 + m22 * b.m22 + m23 * b.m32;
			c.m23 = m20 * b.m03 + m21 * b.m13 + m22 * b.m23 + m23 * b.m33;
			
			c.m30 = m30 * b.m00 + m31 * b.m10 + m32 * b.m20 + m33 * b.m30;
			c.m31 = m30 * b.m01 + m31 * b.m11 + m32 * b.m21 + m33 * b.m31;
			c.m32 = m30 * b.m02 + m31 * b.m12 + m32 * b.m22 + m33 * b.m32;
			c.m33 = m30 * b.m03 + m31 * b.m13 + m32 * b.m23 + m33 * b.m33;
			return c;
		}
		
		public Matrix4f Transpose ()
		{
			// [00,01,02,03]
			// [10,11,12,13]
			// [20,21,22,23]
			// [30,31,32,33]
			
			float c01 = m10;
			float c02 = m20;
			float c03 = m30;
			
			float c12 = m21;
			float c13 = m31;
			float c23 = m32;
			
			m10 = m01;
			m20 = m02;
			m21 = m12;
			m30 = m03;
			m31 = m13;
			m32 = m23;
			
			m01 = c01;
			m02 = c02;
			m03 = c03;
			m12 = c12;
			m13 = c13;
			m23 = c23;
			return this;
		}
		
		public Matrix4f CopyTo (ref Matrix4f matrix) 
		{
			matrix.m00 = m00;
			matrix.m01 = m01;
			matrix.m02 = m02;
			matrix.m03 = m03;
			matrix.m10 = m10;
			matrix.m11 = m11;
			matrix.m12 = m12;
			matrix.m13 = m13;
			matrix.m20 = m20;
			matrix.m21 = m21;
			matrix.m22 = m22;
			matrix.m23 = m23;
			matrix.m30 = m30;
			matrix.m31 = m31;
			matrix.m32 = m32;
			matrix.m33 = m33;
			return matrix;
		}
		
		public Matrix4f CopyFrom (ref Matrix4f matrix) 
		{
			m00 = matrix.m00;
			m01 = matrix.m01;
			m02 = matrix.m02;
			m03 = matrix.m03;
			m10 = matrix.m10;
			m11 = matrix.m11;
			m12 = matrix.m12;
			m13 = matrix.m13;
			m20 = matrix.m20;
			m21 = matrix.m21;
			m22 = matrix.m22;
			m23 = matrix.m23;
			m30 = matrix.m30;
			m31 = matrix.m31;
			m32 = matrix.m32;
			m33 = matrix.m33;
			return matrix;
		}
		
		public Quaternion Quaternion () {
			float trace = 1 + m00 + m11 + m22;
			float s=0, x=0, y=0, z=0, w=0;
			if(trace != 0 && QuickMath.Abs(trace) >= 0.000001) {
				s = QuickMath.Sqrt(trace) * 2f;
				x = (m12 - m21) / s;
				y = (m20 - m02) / s;
				z = (m01 - m10) / s;
				w = 0.25f * s;
			} else if (m00 > m11 && m00 > m22) {
				s = QuickMath.Sqrt(1.0f + m00 - m11 - m22) * 2f;
				x = 0.25f * s;
				y = (m01 + m10) / s;
				z = (m20 + m02) / s;
				w = (m12 - m21) / s;
			} else if (m11 > m22) {
				s = QuickMath.Sqrt(1.0f + m11 - m00 - m22) * 2f;
				x = (m01 + m10) / s;
				y = 0.25f * s;
				z = (m12 + m21) / s;
				w = (m20 - m02) / s;
			} else {
				s = QuickMath.Sqrt(1.0f + m22 - m00 - m11) * 2f;
				x = (m20 + m02) / s;
				y = (m12 + m21) / s;
				z = 0.25f * s;
				w = (m01 - m10) / s;
			}
			return new Quaternion(w, x, y, z);
		}
		
		public bool Equals (Matrix4f b)
		{
			// If parameter is null return false:
			if ((object)b == null) {
				return false;
			}
			
			// Return true if the fields match:
			bool equal = m00 == b.m00 && m11 == b.m11 && m22 == b.m22 && m33 == b.m33 
				&& m01 == b.m01 && m02 == b.m02 && m03 == b.m03 
				&& m10 == b.m10 && m12 == b.m12 && m13 == b.m13 
				&& m20 == b.m20 && m21 == b.m21 && m23 == b.m23 
				&& m30 == b.m30 && m31 == b.m31 && m32 == b.m32;
			return equal;
		}

		public bool Equals (Matrix4f b, float epsilon)
		{
			// If parameter is null return false:
			if ((object)b == null) {
				return false;
			}
			
			bool equal = true;
			if(!(QuickMath.Abs(m00-b.m00) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m01-b.m01) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m02-b.m02) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m03-b.m03) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m10-b.m10) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m11-b.m11) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m12-b.m12) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m13-b.m13) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m20-b.m20) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m21-b.m21) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m22-b.m22) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m23-b.m23) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m30-b.m30) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m31-b.m31) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m32-b.m32) <= epsilon)) {
				equal = false;
			} else if(!(QuickMath.Abs(m33-b.m33) <= epsilon)) {
				equal = false;
			} 
			return equal;
		}

		public override int GetHashCode ()
		{
			int hash = 7331;
			hash = hash + m00.GetHashCode ();
			hash *= 17;
			hash = hash + m01.GetHashCode ();
			hash *= 17;
			hash = hash + m02.GetHashCode ();
			hash *= 17;
			hash = hash + m03.GetHashCode ();
			hash *= 17;
			hash = hash + m10.GetHashCode ();
			hash *= 17;
			hash = hash + m11.GetHashCode ();
			hash *= 17;
			hash = hash + m12.GetHashCode ();
			hash *= 17;
			hash = hash + m13.GetHashCode ();
			hash *= 17;
			hash = hash + m20.GetHashCode ();
			hash *= 17;
			hash = hash + m21.GetHashCode ();
			hash *= 17;
			hash = hash + m22.GetHashCode ();
			hash *= 17;
			hash = hash + m23.GetHashCode ();
			hash *= 17;
			hash = hash + m30.GetHashCode ();
			hash *= 17;
			hash = hash + m31.GetHashCode ();
			hash *= 17;
			hash = hash + m32.GetHashCode ();
			hash *= 17;
			hash = hash + m33.GetHashCode ();
			hash *= 17;
			return hash;
		}
		
		public override string ToString ()
		{
			string rowFormat = "[{0},{1},{2},{3}]\r\n";
			string output = "\r\n";
			output += string.Format(rowFormat, m00, m01, m02, m03);
			output += string.Format(rowFormat, m10, m11, m12, m13);
			output += string.Format(rowFormat, m20, m21, m22, m23);
			output += string.Format(rowFormat, m30, m31, m32, m33);
			return output;
		}
		
		public static Matrix4f Identity() {
			return new Matrix4f(1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1);
		}
	}
}

