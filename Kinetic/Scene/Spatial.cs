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
using Kinetic.Render;

namespace Kinetic.Scene
{
	/// <summary>
	/// Spatial is the base class of anything that has a spatial location within Kinetic.
	/// All classes that are placeable in a scene are children of Spatial.
	/// </summary>
	public class Spatial
	{
		/// <summary>
		/// Name for the spatial. Used for debugging and finding the object.
		/// </summary>
		string name;

		/// <summary>
		/// Link to the parent Spatial. 
		/// If the object is in a scene it has a parent unless it is the root node.
		/// </summary>
		Spatial parent;

		/// <summary>
		/// Storage for the local transformation. This is writable by the application.
		/// </summary>
		Transformation localTransformation;
		
		/// <summary>
		/// Internal Storage for the world transformation. Applications can read this but it is managed by Kinetic.
		/// </summary>
		Transformation worldTransformation;

		/// <summary>
		/// Current State of the spatial. This is used to track if the transforms require updating.
		/// </summary>
		State spatialState;
		
		/// <summary>
		/// Cull Behavior.
		/// </summary>
		CullMode cullMode;

		public Spatial ()
		{
			// TODO: This should have a unique number suffic across all of the same types.
			name = this.GetType ().Name;
			
			parent = null;
			
			localTransformation = new Transformation ();
			worldTransformation = new Transformation ();
			
			spatialState = State.UPDATE_LOCAL_BOUND;
			cullMode = CullMode.INHERIT;
		}

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public Spatial Parent {
			get { return parent; }
			set { parent = value; }
		}

		public Transformation Transformation {
			get {
				CheckUpdateState ();
				return localTransformation;
			}
			set {
				localTransformation = value;
				spatialState = State.UPDATE_LOCAL_BOUND;
			}
		}

		public Vector3f Translation {
			get {
				CheckUpdateState ();
				return localTransformation.Translation;
			}
			set {
				localTransformation.Translation = value;
				spatialState = State.UPDATE_WORLD_BOUND | State.UPDATE_WORLD_TRANSFORM;
			}
		}

		public Vector3f Scale {
			get {
				CheckUpdateState ();
				return localTransformation.Scale;
			}
			set {
				localTransformation.Scale = value;
				spatialState = State.UPDATE_LOCAL_BOUND;
			}
		}

		public Matrix3f Rotation {
			get {
				CheckUpdateState ();
				return localTransformation.Rotation;
			}
			set {
				localTransformation.Rotation = value;
				spatialState = State.UPDATE_LOCAL_BOUND;
			}
		}
		
		public Transformation WorldTransformation {
			get {
				CheckUpdateState ();
				return worldTransformation;
			}
		}

		public Vector3f WorldTranslation {
			get {
				CheckUpdateState ();
				return worldTransformation.Translation;
			}
		}

		public Vector3f WorldScale {
			get {
				CheckUpdateState ();
				return worldTransformation.Scale;
			}
		}

		public Matrix3f WorldRotation {
			get {
				CheckUpdateState ();
				return worldTransformation.Rotation;
			}
		}
		
		public CullMode CullMode {
			get { return cullMode; }
			set { cullMode = value; }
		}

		public State State {
			get { return spatialState; }
		}

		public void AddSpatialState (State state) {
			if (parent != null) {
				parent.AddSpatialState (state);
			}
			spatialState |= state;
		}
		
		public virtual void CheckUpdateState() 
		{
			if(spatialState == State.CURRENT) {
				return;
			}
			if((spatialState & State.UPDATE_LOCAL_BOUND) == State.UPDATE_LOCAL_BOUND) {
				//UpdateLocalBound();
				//UpdateWorldTransform();
				//UpdateWorldBound();	
			} else if((spatialState & State.UPDATE_WORLD_TRANSFORM) == State.UPDATE_WORLD_TRANSFORM) {
				//UpdateWorldTransform();
				//UpdateWorldBound();
			} else if((spatialState & State.UPDATE_WORLD_BOUND) == State.UPDATE_WORLD_BOUND) {
				//UpdateWorldBound();
			}
			spatialState = State.CURRENT;
		}
	}
}

