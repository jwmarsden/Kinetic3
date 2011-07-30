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
//
// Parts of this file are Copyright the Open Toolkit Library
// ---------------------------------------------------------------------------
// The Open Toolkit Library License
//
// Copyright (c) 2006 - 2009 the Open Toolkit library.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Drawing;

using Kinetic.IO;
using Kinetic.Common;
using Kinetic.Math;
using Kinetic.Scene;
using Kinetic.Resource;
using Kinetic.Render;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Kinetic.Provide
{
	public class OpenTKRenderer : Renderer
	{
		protected OpenTKCamera _camera;
		
		protected bool _nullScene;

		public OpenTKRenderer ()
		{
			_camera = null;
			_nullScene = false;
		}
		
		public override string GetRendererType() {
			return "OpenTK";
		}
		
		public override Camera CreateCamera(int width, int height) {
			_camera = new OpenTKCamera(width, height);
			return _camera;
		}
		
		public override void SetCamera(Camera Camera) {
			if(Camera is OpenTKCamera) {
				_camera = (OpenTKCamera) Camera;
			} else {
				throw new Exception("Cannot attach a non OpenTKCamera to an OpenTKRenderer.");
			}
		}
		
		public override Camera GetCamera() {
			return _camera;
		}
		
		/// <summary>
		/// Buffer management
		/// </summary>
		public override void ClearBackBuffer() {
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}
		
		public override void ClearZBuffer() {
			GL.Clear(ClearBufferMask.DepthBufferBit);
		}
		
		public override void ClearStencilBuffer() {
			GL.Clear(ClearBufferMask.StencilBufferBit);
		}
		
		public override void ClearBuffers() {
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
		}
		
		public override void LoadTexture(Texture Texture) {
			if(!Texture.InSystemMemory) {
				Loader<Texture> textureLoader = Catalog.FindTextureLoader(Texture.Name);
				textureLoader.LoadIntoSystemMemory();
				textureLoader.LoadIntoVideoMemory();
			}
			if(!Texture.InVideoMemory) {
				Loader<Texture> textureLoader = Catalog.FindTextureLoader(Texture.Name);
				textureLoader.LoadIntoVideoMemory();
			}
		}
		
		public override void EnableTexture(Texture Texture) {
			if(!Texture.InVideoMemory) {
				LoadTexture(Texture);
			}
			GL.BindTexture(TextureTarget.Texture2D, Texture.Handle);
		}
		
		public override void DisableTexture() {
			GL.BindTexture(TextureTarget.Texture2D, -1);
		}
		
		
		public override void DrawTexture(Texture Texture) {
			
			
			GL.PushMatrix();
			GL.LoadIdentity();
			
			Matrix4 ortho_projection = Matrix4.CreateOrthographicOffCenter(0, 100, 100, 0, -1, 1);
			GL.MatrixMode(MatrixMode.Projection);
			
			GL.PushMatrix();
			GL.LoadMatrix(ref ortho_projection);
			
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
			EnableTexture(Texture);
			GL.Enable(EnableCap.Texture2D);
			
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
			GL.TexCoord2(1, 0); GL.Vertex2(100, 0);
			GL.TexCoord2(1, 1); GL.Vertex2(100, 100);
			GL.TexCoord2(0, 1); GL.Vertex2(0, 100);
			GL.End();
			GL.PopMatrix();
			
			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
			DisableTexture();
			GL.MatrixMode(MatrixMode.Modelview);
			GL.PopMatrix();
			
		}
	}
	
	
}

