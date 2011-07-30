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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Kinetic.Provide
{
	public class OpenTKGameWindow : NativeWindow, IDisposable
	{
		IGraphicsContext glContext;

		VSyncMode vsync;

		#region --- Contructors ---

		#region public GameWindow()

		/// <summary>Constructs a new GameWindow with sensible default attributes.</summary>
		public OpenTKGameWindow () : this(640, 480, GraphicsMode.Default, "OpenTK Game Window", 0, DisplayDevice.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		public OpenTKGameWindow (int width, int height) : this(width, height, GraphicsMode.Default, "OpenTK Game Window", 0, DisplayDevice.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode) : this(width, height, mode, "OpenTK Game Window", 0, DisplayDevice.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode, string title)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		/// <param name="title">The title of the GameWindow.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode, string title) : this(width, height, mode, title, 0, DisplayDevice.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		/// <param name="title">The title of the GameWindow.</param>
		/// <param name="options">GameWindow options regarding window appearance and behavior.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode, string title, GameWindowFlags options) : this(width, height, mode, title, options, DisplayDevice.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		/// <param name="title">The title of the GameWindow.</param>
		/// <param name="options">GameWindow options regarding window appearance and behavior.</param>
		/// <param name="device">The OpenTK.Graphics.DisplayDevice to construct the GameWindow in.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : this(width, height, mode, title, options, device, 1, 0, GraphicsContextFlags.Default)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device, int major, int minor, GraphicsContextFlags flags)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		/// <param name="title">The title of the GameWindow.</param>
		/// <param name="options">GameWindow options regarding window appearance and behavior.</param>
		/// <param name="device">The OpenTK.Graphics.DisplayDevice to construct the GameWindow in.</param>
		/// <param name="major">The major version for the OpenGL GraphicsContext.</param>
		/// <param name="minor">The minor version for the OpenGL GraphicsContext.</param>
		/// <param name="flags">The GraphicsContextFlags version for the OpenGL GraphicsContext.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device, int major, int minor, GraphicsContextFlags flags) : this(width, height, mode, title, options, device, major, minor, flags, null)
		{
		}

		#endregion

		#region public GameWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device, int major, int minor, GraphicsContextFlags flags, IGraphicsContext sharedContext)

		/// <summary>Constructs a new GameWindow with the specified attributes.</summary>
		/// <param name="width">The width of the GameWindow in pixels.</param>
		/// <param name="height">The height of the GameWindow in pixels.</param>
		/// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GameWindow.</param>
		/// <param name="title">The title of the GameWindow.</param>
		/// <param name="options">GameWindow options regarding window appearance and behavior.</param>
		/// <param name="device">The OpenTK.Graphics.DisplayDevice to construct the GameWindow in.</param>
		/// <param name="major">The major version for the OpenGL GraphicsContext.</param>
		/// <param name="minor">The minor version for the OpenGL GraphicsContext.</param>
		/// <param name="flags">The GraphicsContextFlags version for the OpenGL GraphicsContext.</param>
		/// <param name="sharedContext">An IGraphicsContext to share resources with.</param>
		public OpenTKGameWindow (int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device, int major, int minor, GraphicsContextFlags flags, IGraphicsContext sharedContext) : base(width, height, title, options, mode == null ? GraphicsMode.Default : mode, device == null ? DisplayDevice.Default : device)
		{
			try {
				glContext = new GraphicsContext (mode == null ? GraphicsMode.Default : mode, WindowInfo, major, minor, flags);
				glContext.MakeCurrent (WindowInfo);
				(glContext as IGraphicsContextInternal).LoadAll ();
				
				VSync = VSyncMode.On;
				
				//glWindow.WindowInfoChanged += delegate(object sender, EventArgs e) { OnWindowInfoChangedInternal(e); };
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
				base.Dispose ();
				throw;
			}
		}
		#endregion
		#endregion

		/// <summary>
		/// Disposes of the GameWindow, releasing all resources consumed by it.
		/// </summary>
		public override void Dispose ()
		{
			try {
				Dispose(true);
			} finally {
				try {
					if (glContext != null) {
						glContext.Dispose ();
						glContext = null;
					}
				} finally {
					base.Dispose ();
				}
			}
			GC.SuppressFinalize (this);
		}

		#region SwapBuffers

		/// <summary>
		/// Swaps the front and back buffer, presenting the rendered scene to the user.
		/// </summary>
		public void SwapBuffers ()
		{
			EnsureUndisposed ();
			this.Context.SwapBuffers ();
		}

		#endregion

		#region Properties

		#region Context

		/// <summary>
		/// Returns the opengl IGraphicsContext associated with the current GameWindow.
		/// </summary>
		public IGraphicsContext Context {
			get {
				EnsureUndisposed ();
				return glContext;
			}
		}

		#endregion

		#region Joysticks

		/// <summary>
		/// Gets a readonly IList containing all available OpenTK.Input.JoystickDevices.
		/// </summary>
		public IList<JoystickDevice> Joysticks {
			get { return InputDriver.Joysticks; }
		}

		#endregion

		#region Keyboard

		/// <summary>
		/// Gets the primary Keyboard device, or null if no Keyboard exists.
		/// </summary>
		public KeyboardDevice Keyboard {
			get { return InputDriver.Keyboard.Count > 0 ? InputDriver.Keyboard[0] : null; }
		}

		#endregion

		#region Mouse

		/// <summary>
		/// Gets the primary Mouse device, or null if no Mouse exists.
		/// </summary>
		public MouseDevice Mouse {
			get { return InputDriver.Mouse.Count > 0 ? InputDriver.Mouse[0] : null; }
		}

		#endregion

		#endregion


		#region VSync

		/// <summary>
		/// Gets or sets the VSyncMode.
		/// </summary>
		public VSyncMode VSync {
			get {
				EnsureUndisposed ();
				GraphicsContext.Assert ();
				return vsync;
			}
			set {
				EnsureUndisposed ();
				GraphicsContext.Assert ();
				Context.VSync = (vsync = value) != VSyncMode.Off;
			}
		}

		#endregion

		#region WindowState

		/// <summary>
		/// Gets or states the state of the NativeWindow.
		/// </summary>
		public override WindowState WindowState {
			get { return base.WindowState; }
			set {
				base.WindowState = value;
				//Debug.Print("Updating Context after setting WindowState to {0}", value);
				
				if (Context != null)
					Context.Update (WindowInfo);
			}
		}
		#endregion


		#region Dispose

		/// <summary>
		/// Override to add custom cleanup logic.
		/// </summary>
		/// <param name="manual">True, if this method was called by the application; false if this was called by the finalizer thread.</param>
		protected virtual void Dispose (bool manual)
		{
		}

		#endregion
		
		#region OnResize

	        /// <summary>
	        /// Called when this window is resized.
	        /// </summary>
	        /// <param name="e">Not used.</param>
	        /// <remarks>
	        /// You will typically wish to update your viewport whenever
	        /// the window is resized. See the
	        /// <see cref="OpenTK.Graphics.OpenGL.GL.Viewport(int, int, int, int)"/> method.
	        /// </remarks>
	        protected override void OnResize(EventArgs e)
	        {
	            base.OnResize(e);
	            glContext.Update(base.WindowInfo);
	        }
	
	        #endregion

		
	}
	
	#region public enum VSyncMode

	/// <summary>
	/// Enumerates available VSync modes.
	/// </summary>
	public enum VSyncMode
	{
		/// <summary>
		/// Vsync disabled.
		/// </summary>
		Off = 0,
		/// <summary>
		/// VSync enabled.
		/// </summary>
		On,
		/// <summary>
		/// VSync enabled, unless framerate falls below one half of target framerate.
		/// If no target framerate is specified, this behaves exactly like <see cref="VSyncMode.On"/>.
		/// </summary>
		Adaptive
	}

	#endregion
}