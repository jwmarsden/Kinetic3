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

using Kinetic.Resource;
using Kinetic.Scene;

namespace Kinetic.Render
{
	public abstract class Renderer
	{
		public int _x;
		public int _y;
		public int _width;
		public int _height;

		public Color _background;
		
		public Catalog _catalog;
		
		public Renderer ()
		{
			_x = -1;
			_y = -1;
			_width = -1;
			_height = -1;
			_background = Color.Gray;
			_catalog = null;
		}
		
		public Catalog Catalog {
			get { return _catalog; }
			set { _catalog = value; }
		}
		
		public int X {
			get { return _x; }
		}
		
		public int Y {
			get { return _y; }
		}
		
		public int Width {
			get { return _width; }
		}
		
		public int Height {
			get { return _height; }
		}

		public int GetRenderWidth() {
			return _width - _x;
		}
		
		public int GetRenderHeight() {
			return _height - _y;	
		}
		
		public abstract string GetRendererType();
		
		public abstract Camera CreateCamera(int width, int height);
		
		public abstract void SetCamera(Camera Camera);
		
		public abstract Camera GetCamera();
		
		/*
		/// <summary>
		/// Events
		/// </summary>
		
		/*
		 * OnFrustumChange - Camera Moves
		 * OnFrameChange - Window Event
		 * OnViewportChange - Viewport settings change on Camera
		 */
		
		
		/// <summary>
		/// Global State Management
		/// </summary>
		/*
		public abstract void SetColor(Color Color);
		
		public abstract Color GetColor();
		*/
		
		// Shitloads of stuff
		
		
		/// <summary>
		/// Buffer management
		/// </summary>
	
		public abstract void ClearBackBuffer();
		
		public abstract void ClearZBuffer();
		
		public abstract void ClearStencilBuffer();
		
		public virtual void ClearBuffers() {
			ClearBackBuffer();
			ClearZBuffer();
			ClearStencilBuffer();
		}
		/*
		public abstract void ClearBackBuffer(int XPos, int YPos, int Width, int Height);
		
		public abstract void ClearZBuffer(int XPos, int YPos, int Width, int Height);
		
		public abstract void ClearStencilBuffer(int XPos, int YPos, int Width, int Height);
		
		public virtual void ClearBuffers(int XPos, int YPos, int Width, int Height) {
			ClearBackBuffer(XPos, YPos, Width, Height);
			ClearZBuffer(XPos, YPos, Width, Height);
			ClearStencilBuffer(XPos, YPos, Width, Height);
		}
		*/
		/// <summary>
		/// Scene management
		/// </summary>
		
		/*
		public virtual void BeginScene() {}
		
		public virtual void EndScene() {}
		
		public abstract void DrawScene(VisibleSet VisibleSet);
		
		public abstract void Draw(Geometry Geometry);
		
		
		*/
			
		// public abstract void ApplyEffect(ShaderEffect ShaderEffect, bool PrimaryEffect);
		
		
		/// <summary>
		/// Text and 2D
		/// </summary>
		// SelectFont
		public abstract void Draw(Texture Texture);
		
		public abstract void Draw(Bitmap Bitmap);
		
		public abstract void Draw(Texture Texture, int XPos, int YPos);
		
		public abstract void Draw(Bitmap Bitmap, int XPos, int YPos);
		
		public abstract void Draw(Texture Texture, int XPos, int YPos, int Width, int Height);
		
		public abstract void Draw(Bitmap Bitmap, int XPos, int YPos, int Width, int Height);
		
		public abstract void Draw(int XPos, int YPos, Color color, string text);
		
		/// <summary>
		/// Resource Management
		/// </summary>
		/**
		 * Load From Catalog
		 **/
		// Load Texture
		public abstract void LoadTexture(Texture Texture); 
		
		// Unload Texture
				
		
		
		// Load Model
		
		// Unload Model
		
		// LoadFont
		
		// UnloadFont
		
		// Load Float Buffer
		
		// Unload Float Buffer
		
		// Load Int Buffer
		
		// Unload Int Buffer
		
		// Load Vertex Shader
		
		// Unload Vertex Shader
		
		// Load Fragment Shader
		
		// Unload Fragment Shader
		
		/**
		 * Enable/Disable Resources
		 **/
		
		// Enable Texture
		public abstract void EnableTexture(Texture Texture);
		
		// Disable Texture
		public abstract void DisableTexture();
		
		// Enable Vertex Buffer
		
		// Disable Vertex Buffer
		
		// Enable Indicies Buffer
		
		// Disable Indicies Buffer
		
		// Enable Vertex Shader
		
		// Disable Vertex Shader
		
		// Enable Fragment Shader
		
		// Disable Fragment Shader
		
		/**
		 * Set Shader Constants
		 **/
		
		// Shitloads of stuff
	}
}

