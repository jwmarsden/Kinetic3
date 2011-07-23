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

namespace Kinetic
{
	public class Frustum
	{	
		public const int NEAR_TOP_LEFT = 0;
		public const int NEAR_TOP_RIGHT = 1;
		public const int NEAR_BOTTOM_RIGHT = 2;
		public const int NEAR_BOTTOM_LEFT = 3;
		
		public const int FAR_TOP_LEFT = 4;
		public const int FAR_TOP_RIGHT = 5;
		public const int FAR_BOTTOM_RIGHT = 6;
		public const int FAR_BOTTOM_LEFT = 7;
		
		public const int NEAR = 0;
		public const int FAR = 1;
		public const int TOP = 2;
		public const int BOTTOM = 3;
		public const int LEFT = 4;
		public const int RIGHT = 5;
		
		public Vector3f position;
		
		public float farPlaneHeight;
		public float farPlaneWidth;
		public float nearPlaneHeight;
		public float nearPlaneWidth;
		
		public Vector3f farPlaneCenter;
		public Vector3f nearPlaneCenter;
		
		public Vector3f[] points;
		public Plane[] planes;
		
		public Frustum ()
		{
			position = new Vector3f(0,0,0);
			farPlaneHeight = 0;
			farPlaneWidth = 0;
			nearPlaneHeight = 0;
			nearPlaneWidth = 0;
			farPlaneCenter = new Vector3f(0,0,0);
			nearPlaneCenter = new Vector3f(0,0,0);
			points = new Vector3f[10];
			for (int i=0;i<10;i++)
			{
				points[i] = new Vector3f(0,0,0);	
			}
			planes = new Plane[6];
			for (int i=0;i<6;i++)
			{
				planes[i] = new Plane();	
			}
		}
		
		public Vector3f Postion {
			get { return position; }
			set { position = value; }
		}
		
		public Vector3f NearPlaneCenter {
			get { return nearPlaneCenter; }
			set { nearPlaneCenter = value; }
		}
	
		public Vector3f FarPlaneCenter {
			get { return farPlaneCenter; }
			set { farPlaneCenter = value; }
		}
		
		public Vector3f NearTopLeft {
			get { return points[NEAR_TOP_LEFT]; }
			set { points[NEAR_TOP_LEFT] = value; }
		}
		
		public Vector3f NearTopRight {
			get { return points[NEAR_TOP_RIGHT]; }
			set { points[NEAR_TOP_RIGHT] = value; }
		}
		
		public Vector3f NearBottomRight {
			get { return points[NEAR_BOTTOM_RIGHT]; }
			set { points[NEAR_BOTTOM_RIGHT] = value; }
		}
		
		public Vector3f NearBottomLeft {
			get { return points[NEAR_BOTTOM_LEFT]; }
			set { points[NEAR_BOTTOM_LEFT] = value; }
		}
		
		public Vector3f FarTopLeft {
			get { return points[FAR_TOP_LEFT]; }
			set { points[FAR_TOP_LEFT] = value; }
		}
		
		public Vector3f FarTopRight {
			get { return points[FAR_TOP_RIGHT]; }
			set { points[FAR_TOP_RIGHT] = value; }
		}
		
		public Vector3f FarBottomRight {
			get { return points[FAR_BOTTOM_RIGHT]; }
			set { points[FAR_BOTTOM_RIGHT] = value; }
		}
		
		public Vector3f FarBottomLeft {
			get { return points[FAR_BOTTOM_LEFT]; }
			set { points[FAR_BOTTOM_LEFT] = value; }
		}
		
		public Vector3f[] Points {
			get { return points; }
		}
		
		public Plane Near {
			get { return planes[NEAR]; }
			set { planes[NEAR] = value; }
		}
		
		public Plane Far {
			get { return planes[FAR]; }
			set { planes[FAR] = value; }
		}
		
		public Plane Top {
			get { return planes[TOP]; }
			set { planes[TOP] = value; }
		}
		
		public Plane Bottom {
			get { return planes[BOTTOM]; }
			set { planes[BOTTOM] = value; }
		}
		
		public Plane Left {
			get { return planes[LEFT]; }
			set { planes[LEFT] = value; }
		}
		
		public Plane Right {
			get { return planes[RIGHT]; }
			set { planes[RIGHT] = value; }
		}
		
		public Plane[] Planes {
			get { return planes; }
		}
		
		public override string ToString ()
		{
			return string.Format ("[Frustum: Postion={0} FarPlaneCenter={1} NearPlaneCenter={2}]", Postion, FarPlaneCenter, NearPlaneCenter);
		}
	}
}

