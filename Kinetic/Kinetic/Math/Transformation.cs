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
using Kinetic;

namespace Kinetic.Math
{
	public class Transformation
	{
		Vector3f translation;
		Vector3f scale;
		Matrix3f rotation;
		Matrix4f transformation;

		public Transformation ()
		{
			translation = new Vector3f(0,0,0);
			scale = new Vector3f(1,1,1);
			rotation = Matrix3f.Identity();
			transformation = Matrix4f.Identity();
		}
		
		public Vector3f Translation
		{
			get { return translation; }
			set { translation = value; }
		}
		
		public Vector3f Scale
		{
			get { return scale; }
			set { scale = value; }
		}
		
		public Matrix3f Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}
		
		public Matrix4f Combine() {
			transformation = Matrix4f.Identity();

			transformation.m00 = scale.x * rotation.m00;
			transformation.m01 = scale.y * rotation.m01;
			transformation.m02 = scale.z * rotation.m02;
			
			transformation.m10 = scale.x * rotation.m10;
			transformation.m11 = scale.y * rotation.m11;
			transformation.m12 = scale.z * rotation.m12;
			
			transformation.m20 = scale.x * rotation.m20;
			transformation.m21 = scale.y * rotation.m21;
			transformation.m22 = scale.z * rotation.m22;
			
			transformation.m30 = translation.x;
			transformation.m31 = translation.y;
			transformation.m32 = translation.z;
			
			return transformation;
		}
		
		public Transformation Concat(ref Transformation additionalTransform) {
			Transformation transformation = new Transformation();
			
			translation.Add(ref additionalTransform.translation, ref transformation.translation);
			
			transformation.scale.X = scale.X * additionalTransform.scale.X;
			transformation.scale.Y = scale.Y * additionalTransform.scale.Y;
			transformation.scale.Z = scale.Z * additionalTransform.scale.Z;
			
			rotation.Multiply(ref additionalTransform.rotation, ref transformation.rotation);
			
			return transformation;
		}
		
		public Transformation Concat(ref Transformation additionalTransform, ref Transformation transformation) {
			
			translation.Add(ref additionalTransform.translation, ref transformation.translation);
			
			transformation.scale.X = scale.X * additionalTransform.scale.X;
			transformation.scale.Y = scale.Y * additionalTransform.scale.Y;
			transformation.scale.Z = scale.Z * additionalTransform.scale.Z;
			
			rotation.Multiply(ref additionalTransform.rotation, ref transformation.rotation);
			
			return transformation;
		}
		
		public Transformation CopyTo(ref Transformation transformation)
		{
			transformation.translation.CopyTo(ref translation);
			transformation.rotation.CopyTo(ref rotation);
			transformation.scale.CopyTo(ref scale);
			return transformation;
		}
		
		public Transformation CopyFrom(ref Transformation transformation)
		{
			translation.CopyTo(ref transformation.translation);
			rotation.CopyTo(ref transformation.rotation);
			scale.CopyTo(ref transformation.scale);
			return transformation;
		}
		
		public static Matrix3f CreateXRotationMatrix3f(float angle) {
			Matrix3f rotation = Matrix3f.Identity();
			rotation.m11 = QuickMath.Cos(angle);
			rotation.m22 = rotation.m11;
			rotation.m12 = (float) QuickMath.Sin(angle);
			rotation.m21 = -rotation.m12;
			return rotation;	
		}
		
		public static Matrix3f CreateYRotationMatrix3f(float angle) {
			Matrix3f rotation = Matrix3f.Identity();
			rotation.m00 = QuickMath.Cos(angle);
			rotation.m22 = rotation.m00;
			rotation.m20 = (float) QuickMath.Sin(angle);
			rotation.m02 = -rotation.m20;
			return rotation;
		}
		
		public static Matrix3f CreateZRotationMatrix3f(float angle) {
			Matrix3f rotation = Matrix3f.Identity();
			rotation.m00 = QuickMath.Cos(angle);
			rotation.m11 = rotation.m00;
			rotation.m01 = (float) QuickMath.Sin(angle);
			rotation.m10 = -rotation.m01;
			return rotation;
		}
		
		public static Matrix4f CreateXRotationMatrix4f(float angle) {
			Matrix4f rotation = Matrix4f.Identity();
			rotation.m11 = QuickMath.Cos(angle);
			rotation.m22 = rotation.m11;
			rotation.m12 = (float) QuickMath.Sin(angle);
			rotation.m21 = -rotation.m12;
			return rotation;	
		}
		
		public static Matrix4f CreateYRotationMatrix4f(float angle) {
			Matrix4f rotation = Matrix4f.Identity();
			rotation.m00 = QuickMath.Cos(angle);
			rotation.m22 = rotation.m00;
			rotation.m20 = (float) QuickMath.Sin(angle);
			rotation.m02 = -rotation.m20;
			return rotation;
		}
		
		public static Matrix4f CreateZRotationMatrix4f(float angle) {
			Matrix4f rotation = Matrix4f.Identity();
			rotation.m00 = QuickMath.Cos(angle);
			rotation.m11 = rotation.m00;
			rotation.m01 = (float) QuickMath.Sin(angle);
			rotation.m10 = -rotation.m01;
			return rotation;
		}
	}
}

