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

namespace Kinetic.Render
{
	public abstract class Camera
	{
		protected float width;

		protected float height;

		/// <summary>The Position of the Camera.</summary>
		protected Vector3f position;
		/// <summary>The Right Unit Vector for the Camera.</summary>
		protected Vector3f right;
		/// <summary>The Up Unit Vector for the Camera.</summary>
		protected Vector3f up;
		/// <summary>The direction that the camera points in (Z Unit).</summary>
		protected Vector3f direction;

		protected float fieldOfView;

		protected float nearPlaneDistance;

		protected float farPlaneDistance;
		
		protected Frustum frustum;

		public Camera (float width, float height)
		{
			this.width = width;
			this.height = height;
			
			position = new Vector3f (0, 0, 0);
			right = Vector3f.X_UNIT;
			up = Vector3f.Y_UNIT;
			direction = Vector3f.Z_UNIT;
			
			fieldOfView = QuickMath.PI / 4f;
			nearPlaneDistance = 0.1f;
			farPlaneDistance = 200f;
			frustum = new Frustum();
		}

		public Vector3f Position {
			get { return position; }
		}

		public Vector3f Direction {
			get { return direction; }
		}

		public Vector3f Right {
			get { return right; }
		}

		public Vector3f Up {
			get { return up; }
		}
		
		public float FieldOfView {
			get { return fieldOfView; }	
		}
		
		public float AspectRatio {
			get { return width/height; }	
		}
		
		public float NearPlaneDistance {
			get { return nearPlaneDistance; }	
		}
		
		public float FarPlaneDistance {
			get { return farPlaneDistance; }	
		}
		
		public Frustum Frustum {
			get { return frustum; }	
		}
		
		public void Resize (int width, int height)
		{
			this.width = width;
			this.height = height;
		}
		
		public void Update (Vector3f translation, Matrix3f rotation)
		{
			position = translation;
			Matrix4f rotationMatrix = new Matrix4f(rotation);
			right.setUnit(Axis.X);
			right.Transform (rotationMatrix);
			up.setUnit(Axis.Y);
			up.Transform (rotationMatrix);
			direction.setUnit(Axis.Z);
			direction.Transform (rotationMatrix);
			UpdateCameraFrustum ();
		}
		
		public void Update (Vector3f translation, Quaternion rotation)
		{
			position = translation;
			Matrix4f rotationMatrix = rotation.RotationMatrix4f ();
			right.setUnit(Axis.X);
			right.Transform (rotationMatrix);
			up.setUnit(Axis.Y);
			up.Transform (rotationMatrix);
			direction.setUnit(Axis.Z);
			direction.Transform (rotationMatrix);
			UpdateCameraFrustum ();
		}
		
		public void UpdateCameraFrustum ()
		{
			float doubleHalfTanFOV = 2f * QuickMath.Tan (fieldOfView / 2f);
			float aspectRatio = width / height;
			
			float heightNearPlane = doubleHalfTanFOV * nearPlaneDistance;
			float widthNearPlane = heightNearPlane * aspectRatio;
			float heightFarPlane = doubleHalfTanFOV * farPlaneDistance;
			float widthFarPlane = heightFarPlane * aspectRatio;
			
			frustum.Postion = Position;
			
			// Find the Far Plane Center Point
			Vector3f farPlaneCenter = frustum.FarPlaneCenter.Reset();
			direction.MultiplyScalar (farPlaneDistance, ref farPlaneCenter).Add (ref position);
			
			// Find the Far Plane Verticies
			Vector3f upHalfHeightFar = new Vector3f(0,0,0);
			up.MultiplyScalar(heightFarPlane/2f, ref upHalfHeightFar);
			Vector3f rightHalfWidthFar = new Vector3f(0,0,0);
			right.MultiplyScalar(widthFarPlane/2f, ref rightHalfWidthFar);
			
			Vector3f farTopLeft = frustum.FarTopLeft.Reset();
			Vector3f farTopRight = frustum.FarTopRight.Reset();
			Vector3f farBottomLeft = frustum.FarBottomLeft.Reset();
			Vector3f farBottomRight = frustum.FarBottomRight.Reset();

			farPlaneCenter.Add(ref upHalfHeightFar, ref farTopLeft).Subtract(ref rightHalfWidthFar);
			farPlaneCenter.Add(ref upHalfHeightFar, ref farTopRight).Add(ref rightHalfWidthFar);
			farPlaneCenter.Subtract(ref upHalfHeightFar, ref farBottomLeft).Subtract(ref rightHalfWidthFar);
			farPlaneCenter.Subtract(ref upHalfHeightFar, ref farBottomRight).Add(ref rightHalfWidthFar);
			
			// Find the Near Plane Center Point
			Vector3f nearPlaneCenter = frustum.NearPlaneCenter.Reset();
			direction.MultiplyScalar (nearPlaneDistance, ref nearPlaneCenter).Add (ref position);
			
			// Find the Near Plane Verticies
			Vector3f upHalfHeightNear = new Vector3f(0,0,0);
			up.MultiplyScalar(heightNearPlane/2f, ref upHalfHeightNear);
			Vector3f rightHalfWidthNear = new Vector3f(0,0,0);
			right.MultiplyScalar(widthNearPlane/2f, ref rightHalfWidthNear);
			
			Vector3f nearTopLeft = frustum.NearTopLeft.Reset();
			Vector3f nearTopRight = frustum.NearTopRight.Reset();
			Vector3f nearBottomLeft = frustum.NearBottomLeft.Reset();
			Vector3f nearBottomRight = frustum.NearBottomRight.Reset();
			
			nearPlaneCenter.Add(ref upHalfHeightNear, ref nearTopLeft).Subtract(ref rightHalfWidthNear);
			nearPlaneCenter.Add(ref upHalfHeightNear, ref nearTopRight).Add(ref rightHalfWidthNear);
			nearPlaneCenter.Subtract(ref upHalfHeightNear, ref nearBottomLeft).Subtract(ref rightHalfWidthNear);
			nearPlaneCenter.Subtract(ref upHalfHeightNear, ref nearBottomRight).Add(ref rightHalfWidthNear);
			
			// Set the planes
			frustum.Near.Set(nearBottomLeft, nearBottomRight, nearTopLeft);
			frustum.Far.Set(farBottomRight, farBottomLeft, farTopRight);
			frustum.Top.Set(farTopRight, farTopLeft, nearTopRight);
			frustum.Bottom.Set(farBottomLeft, farBottomRight, nearBottomLeft);
			frustum.Left.Set(farBottomLeft, farTopLeft, nearBottomLeft);
			frustum.Right.Set(nearBottomRight, farBottomRight, nearTopRight);
			
			Console.WriteLine(frustum);
		}
	}
}

