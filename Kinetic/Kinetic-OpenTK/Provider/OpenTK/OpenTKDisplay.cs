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
using System.Threading;
using System.Windows;

using Kinetic.IO;
using Kinetic.Math;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Kinetic.Provide
{
	public class OpenTKDisplay : Display
	{
		OpenTKGameWindow window;
		bool check;
		int xStore = 0;
		int yStore = 0;
		
		public OpenTKDisplay ()
		{
			window = null;
			check = false;
		}

		public override string[] SupportedExtensions ()
		{
			return GL.GetString (StringName.Extensions).Split (' ');
		}

		public override void SetTitle (string title)
		{
			this.title = title;
			if (window != null) {
				window.Title = title;
			}
		}
		
		public override void CreateWindow ()
		{
			//Console.WriteLine(string.Format("Create Display Window ({0}x{1})", Width, Height));
			if (window != null) {
				throw new Exception ("Window Already Created.");
			}
			window = new OpenTKGameWindow (Width, Height);
			//Console.WriteLine("Create Display Window - Instance Created");
			window.Title = Title;
			window.Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs> (OnKeyDownHandler);
			window.Keyboard.KeyUp += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs> (OnKeyUpHandler);
			//window.Closing += new EventHandler<System.ComponentModel.>(OnClosingHandler);
			window.Closed += new EventHandler<EventArgs> (OnClosedHandler);
			
			window.Resize += new EventHandler<EventArgs> (OnResizeHandler);
			
			window.Mouse.Move += new EventHandler<OpenTK.Input.MouseMoveEventArgs> (OnMouseMoveHandler);
			window.Mouse.ButtonDown += new EventHandler<OpenTK.Input.MouseButtonEventArgs> (OnMouseButtonDownHandler);
			window.Mouse.ButtonUp += new EventHandler<OpenTK.Input.MouseButtonEventArgs> (OnMouseButtonUpHandler);
			//Console.WriteLine("Create Display Window - Events Registered");
			
			inputRegister = new InputRegister ();
		}

		public override void Update ()
		{
		}

		public override void ShowWindow ()
		{
			window.Visible = true;
		}

		public override void HideWindow ()
		{
			window.Visible = false;
		}

		public override void ProcessEvents ()
		{
			if (closing || !window.Exists) {
				return;
			}
			window.ProcessEvents ();
			/*if (held && !externalMouseReadWhileHeld && !closing) {
				Point windowLocation = window.Location;
				Point center = new Point (windowLocation.X + (window.Width / 2), windowLocation.Y + (window.Height / 2));
				Point cursorLocation = System.Windows.Forms.Cursor.Position;
				
				
				int xDelta = cursorLocation.X - center.X;
				int yDelta = cursorLocation.Y - center.Y;
				if (!(xDelta == 0 && yDelta == 0)) {
					inputRegister.MouseMoveDeltaInput (xDelta, yDelta);
					//System.Windows.Forms.Cursor.Position = center;
					OpenTK.Input.Mouse.SetPosition(center.X, center.Y);
				}
				
				
			}*/
		}

		public override MouseMoveDeltaEventArgs MouseMoveDeltaRead ()
		{
			if (closing || !window.Exists) {
				return new MouseMoveDeltaEventArgs(0,0);
			}
			Point windowLocation = window.Location;
			Point center = new Point (windowLocation.X + (window.Width / 2), windowLocation.Y + (window.Height / 2));
			Point cursorLocation = System.Windows.Forms.Cursor.Position;
			
			int xDelta = cursorLocation.X - xStore;
			int yDelta = cursorLocation.Y - yStore;
			
			if (!(xDelta == 0 && yDelta == 0)) {
				inputRegister.MouseMoveDeltaInput (xDelta, yDelta);
				if(cursorLocation.X >= windowLocation.X + Width - 25 || cursorLocation.X <= windowLocation.X + 50 ||
				   cursorLocation.Y >= windowLocation.Y + Height - 25 || cursorLocation.Y <= windowLocation.Y + 25) {
					xStore = center.X;
					yStore = center.Y;
					Point newLocation = new Point(xStore,yStore);
					OpenTK.Input.Mouse.SetPosition(newLocation.X, newLocation.Y);
				} else {
					xStore = cursorLocation.X;
					yStore = cursorLocation.Y;
				}
			}
			return new MouseMoveDeltaEventArgs(xDelta, yDelta);
		}

		public override void BeforeRender ()
		{
			if (closing || !window.Exists) {
				return;
			}
			window.Context.MakeCurrent (window.WindowInfo);
			if (check) {
				window.Title = Title;
				//GL.ClearColor (BackgroundColor);
				check = false;
			}
		}

		public override void AfterRender ()
		{
			if (closing || !window.Exists) {
				return;
			}
			window.SwapBuffers ();
			window.Context.MakeCurrent (null);
		}
		
		public override void HoldInput ()
		{
			if(customMouseInput) {
				HoldInput(false);
			} else {
				//window.CursorVisible = false;
			}
			held = true;
		}
		
		public override void HoldInput (bool externalMouseRead)
		{
			if(customMouseInput) {
				Point windowLocation = window.Location;
				Point center = new Point (windowLocation.X + (window.Width / 2), windowLocation.Y + (window.Height / 2));
				System.Windows.Forms.Cursor.Current = null;
				xStore = center.X;
				yStore = center.Y;
				System.Windows.Forms.Cursor.Position = center;
				externalMouseReadWhileHeld = externalMouseRead;
			} else {
				//window.CursorVisible = false;
			}
			held = true;	
		}

		public override void ReleaseInput ()
		{
			if(customMouseInput) {
				externalMouseReadWhileHeld = false;
			} else {
				//window.CursorVisible = true;
			}
			held = false;
		}


		//protected void OnClosingHandler(object sender, EventArgs e) {
		//	closing = true;
		//}

		protected void OnClosedHandler (object sender, EventArgs e)
		{
			closing = true;
		}

		protected void OnResizeHandler (object sender, EventArgs e)
		{
			height = window.Height;
			width = window.Width;
			InputRegister.ResizeInput (this, width, height);
		}

		protected void OnKeyDownHandler (object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			Kinetic.IO.Key key = (Kinetic.IO.Key)e.Key;
			InputRegister.KeyDownInput (key);
		}

		protected void OnKeyUpHandler (object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			Kinetic.IO.Key key = (Kinetic.IO.Key)e.Key;
			InputRegister.KeyUpInput (key);
			
		}

		protected void OnMouseMoveHandler (object sender, OpenTK.Input.MouseMoveEventArgs e)
		{
			//Console.WriteLine(string.Format("X:{0} Y:{1} XD:{2} YD:{3}", e.X, e.Y, e.XDelta, e.YDelta));
			int xDelta = e.XDelta;
			int yDelta = e.YDelta;
			inputRegister.MouseMoveDeltaInput (xDelta, yDelta);
			inputRegister.MouseMoveInput (e.X, e.Y, xDelta, yDelta);
		}

		protected void OnMouseButtonDownHandler (object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			if (e.Button == OpenTK.Input.MouseButton.Left) {
				InputRegister.MouseButtonDownInput (Kinetic.IO.MouseButton.Left, e.X, e.Y);
			} else if (e.Button == OpenTK.Input.MouseButton.Middle) {
				InputRegister.MouseButtonDownInput (Kinetic.IO.MouseButton.Middle, e.X, e.Y);
			} else if (e.Button == OpenTK.Input.MouseButton.Right) {
				InputRegister.MouseButtonDownInput (Kinetic.IO.MouseButton.Right, e.X, e.Y);
			}
		}

		protected void OnMouseButtonUpHandler (object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			if (e.Button == OpenTK.Input.MouseButton.Left) {
				InputRegister.MouseButtonUpInput (Kinetic.IO.MouseButton.Left, e.X, e.Y);
			} else if (e.Button == OpenTK.Input.MouseButton.Middle) {
				InputRegister.MouseButtonUpInput (Kinetic.IO.MouseButton.Middle, e.X, e.Y);
			} else if (e.Button == OpenTK.Input.MouseButton.Right) {
				InputRegister.MouseButtonUpInput (Kinetic.IO.MouseButton.Right, e.X, e.Y);
			}
		}
		
	}
}


