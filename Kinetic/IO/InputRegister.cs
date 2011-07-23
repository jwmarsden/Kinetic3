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
using System.Collections;
using System.Collections.Generic;

namespace Kinetic.IO
{	
	//Keyboard Events
	public class ResizeArgs : EventArgs
	{
		protected Display display;
		protected int width;
		protected int height;

		public ResizeArgs (Display display, int width, int height)
		{
			this.display = display;
			this.width = width;
			this.height = height;
		}

		public Display Display {
			get { return display; }
			set { display = value; }
		}
		
		public int Width {
			get { return width; }
			set { width = value; }
		}
		
		public int Height {
			get { return height; }
			set { height = value; }
		}
		
		public override string ToString ()
		{
			return string.Format ("[ResizeArgs: Display={0}, Width={1}, Height={2}]", Display, Width, Height);
		}
	}
	
	/// <summary>
	/// Keboard Event Arguments.
	/// </summary>
	public class KeyEventArgs : EventArgs
	{
		protected Key key;

		public KeyEventArgs (Key key)
		{
			this.key = key;
		}

		public Key Key {
			get { return Key; }
		}
	}

	//public delegate void KeyDownEventHandler (object sender, KeyEventArgs e);

	//public delegate void KeyUpEventHandler (object sender, KeyEventArgs e);

	//public delegate void KeyPressedEventHandler (object sender, KeyEventArgs e);

	/*
	 * Mouse Events
	 */
	
	/// <summary>
	/// Mouse Move Normal Event Arguments
	/// </summary>
	public class MouseMoveEventArgs : EventArgs
	{
		protected int x;
		protected int y;
		protected int xDelta;
		protected int yDelta;

		public MouseMoveEventArgs (int x, int y, int xDelta, int yDelta)
		{
			this.x = x;
			this.y = y;
			this.xDelta = xDelta;
			this.yDelta = yDelta;
		}

		public int X {
			get { return x; }
		}
		
		public int Y {
			get { return y; }
		}
		
		public int XDelta {
			get { return xDelta; }
		}
		
		public int YDelta {
			get { return yDelta; }
		}
		
		public override string ToString ()
		{
			return string.Format ("[MouseMoveEventArgs: X={0}, Y={1}, XDelta={2}, YDelta={3}]", X, Y, XDelta, YDelta);
		}
	}
	
	/// <summary>
	/// Mouse Move Delta Event Arguments (used when the display is held).
	/// </summary>
	public class MouseMoveDeltaEventArgs : EventArgs
	{
		protected int xDelta;
		protected int yDelta;

		public MouseMoveDeltaEventArgs (int xDelta, int yDelta)
		{
			this.xDelta = xDelta;
			this.yDelta = yDelta;
		}

		public int XDelta {
			get { return xDelta; }
		}
		
		public int YDelta {
			get { return yDelta; }
		}
		
		public override string ToString ()
		{
			return string.Format ("[MouseMoveDeltaEventArgs: XDelta={0}, YDelta={1}]", XDelta, YDelta);
		}
	}
	
	//public delegate void MouseMoveEventHandler (object sender, MouseMoveEventArgs e);
	
	
	
	//public delegate void MouseMoveDeltaEventHandler (object sender, MouseMoveDeltaEventArgs e);

	/// <summary>
	/// Mouse click event arguments
	/// </summary>
	public class MouseClickEventArgs : EventArgs
	{
		protected int x;
		protected int y;
		
		public MouseClickEventArgs(int x, int y) 
		{
			this.x = x;
			this.y = y;
		}
		
		public int X {
			get { return x; }
		}
		
		public int Y {
			get { return y; }
		}
	}
	/*
	public delegate void MouseLeftDownEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseLeftUpEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseLeftClickEventHandler (object sender, MouseClickEventArgs e);
		
	public delegate void MouseMiddleDownEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseMiddleUpEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseMiddleClickEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseRightDownEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseRightUpEventHandler (object sender, MouseClickEventArgs e);
	
	public delegate void MouseRightClickEventHandler (object sender, MouseClickEventArgs e);
	 */
	
	/// <summary>
	/// Mouse Scroll Wheel Event
	/// </summary>
	
	
	//public delegate void MouseScrollWheelEventHandler (object sender, EventArgs e);

	public class InputRegister
	{

		bool[] register = new bool[130];
		bool[] mouse = new bool[3];
		bool collectMouseMove;
		float xMoveHolder;
		float yMoveHolder;
		
		public InputRegister ()
		{
			for (int i = 0; i < 130; i++) {
				register[i] = false;
			}
			
			mouse[0] = false;
			mouse[1] = false;
			mouse[2] = false;
			
			collectMouseMove = false;
			xMoveHolder = 0;
			yMoveHolder = 0;
		}
		
		public bool CollectMouseMoves {
			get { return collectMouseMove; }
			set { 
				collectMouseMove = value; 
				if(collectMouseMove == false) {
					xMoveHolder = 0;
					yMoveHolder = 0;
				}
			}
		}

		public event EventHandler<ResizeArgs> ResizeDisplay;
		protected virtual void OnResizeDisplay (ResizeArgs e)
		{
			if (ResizeDisplay != null) {
				ResizeDisplay (this, e);
			}
		}
		
		// Key Down Event
		public event EventHandler<KeyEventArgs> KeyDown;
		protected virtual void OnKeyDown (KeyEventArgs e)
		{
			if (KeyDown != null) {
				KeyDown (this, e);
			}
		}

		// Key Up Event
		public event EventHandler<KeyEventArgs> KeyUp;
		protected virtual void OnKeyUp (KeyEventArgs e)
		{
			if (KeyUp != null) {
				KeyUp (this, e);
			}
		}

		// Key Pressed Event
		public event EventHandler<KeyEventArgs> KeyPressed;
		protected virtual void OnKeyPressed (KeyEventArgs e)
		{
			if (KeyPressed != null) {
				KeyPressed (this, e);
			}
		}

		// Mouse Move Event
		public event EventHandler<MouseMoveEventArgs> MouseMove;
		protected virtual void OnMouseMove (MouseMoveEventArgs e)
		{
			if (MouseMove != null) {
				MouseMove (this, e);
			}
		}	
		
		public event EventHandler<MouseMoveDeltaEventArgs> MouseMoveDelta;
		protected virtual void OnMouseMoveDelta (MouseMoveDeltaEventArgs e)
		{
			if (MouseMoveDelta != null) {
				MouseMoveDelta (this, e);
			}
		}	
		
		// Mouse Left Click
		public event EventHandler<MouseClickEventArgs> MouseLeftDown;
		protected virtual void OnMouseLeftDown (MouseClickEventArgs e)
		{
			if (MouseLeftDown != null) {
				MouseLeftDown (this, e);
			}
		}
		
		public event EventHandler<MouseClickEventArgs> MouseLeftUp;
		protected virtual void OnMouseLeftUp (MouseClickEventArgs e)
		{
			if (MouseLeftUp != null) {
				MouseLeftUp (this, e);
			}
		}
		
		public event EventHandler<MouseClickEventArgs> MouseLeftClick;
		protected virtual void OnMouseLeftClick (MouseClickEventArgs e)
		{
			if (MouseLeftClick != null) {
				MouseLeftClick (this, e);
			}
		}	
		
		// Mouse Right Click
		public event EventHandler<MouseClickEventArgs> MouseRightDown;
		protected virtual void OnMouseRightDown (MouseClickEventArgs e)
		{
			if (MouseRightDown != null) {
				MouseRightDown (this, e);
			}
		}
		
		public event EventHandler<MouseClickEventArgs> MouseRightUp;
		protected virtual void OnMouseRightUp (MouseClickEventArgs e)
		{
			if (MouseRightUp != null) {
				MouseRightUp (this, e);
			}
		}
		
		public event EventHandler<MouseClickEventArgs> MouseRightClick;
		protected virtual void OnMouseRightClick (MouseClickEventArgs e)
		{
			if (MouseRightClick != null) {
				MouseRightClick (this, e);
			}
		}
		
		// Mouse Middle Click
		public event EventHandler<MouseClickEventArgs> MouseMiddleDown;
		protected virtual void OnMouseMiddleDown (MouseClickEventArgs e)
		{
			if (MouseMiddleDown != null) {
				MouseMiddleDown (this, e);
			}
		}
		
		public event EventHandler<MouseClickEventArgs> MouseMiddleUp;
		protected virtual void OnMouseMiddleUp (MouseClickEventArgs e)
		{
			if (MouseMiddleUp != null) {
				MouseMiddleUp (this, e);
			}
		}
		
		
		public event EventHandler<MouseClickEventArgs> MouseMiddleClick;
		protected virtual void OnMouseMiddleClick (MouseClickEventArgs e)
		{
			if (MouseMiddleClick != null) {
				MouseMiddleClick (this, e);
			}
		}
		
		// Mouse Scroll Wheel


		
		
		/****************************
		 * Public Callable methods
		 ***************************/
		
		public void ResizeInput(Display display, int width, int height) 
		{
			OnResizeDisplay(new ResizeArgs(display, width, height));
		}
		
		public void KeyDownInput (Kinetic.IO.Key key)
		{
			int keyPressed = (int)key;
			OnKeyDown (new KeyEventArgs (key));
			register[keyPressed] = true;
		}

		public void KeyUpInput (Kinetic.IO.Key key)
		{
			int keyPressed = (int)key;
			OnKeyUp (new KeyEventArgs (key));
			if (register[keyPressed]) {
				register[keyPressed] = false;
				OnKeyPressed (new KeyEventArgs (key));
			}
		}

		public void MouseMoveInput (int x, int y, int xDelta, int yDelta)
		{
			OnMouseMove (new MouseMoveEventArgs (x, y, xDelta, yDelta));
		}

		public void MouseMoveDeltaInput (int xDelta, int yDelta) 
		{
			if(collectMouseMove) {
				xMoveHolder += xDelta;
				yMoveHolder += yDelta;
			}
			OnMouseMoveDelta (new MouseMoveDeltaEventArgs(xDelta, yDelta));
		}
		
		public void MouseButtonDownInput(Kinetic.IO.MouseButton button, int x, int y) {
			int mouseButton = (int)button;
			mouse[mouseButton] = true;
			
			if(button == MouseButton.Left) {
				OnMouseLeftDown(new MouseClickEventArgs(x,y));	
			} else if(button == MouseButton.Middle) {
				OnMouseMiddleDown(new MouseClickEventArgs(x,y));	
			} else if(button == MouseButton.Right) {
				OnMouseRightDown(new MouseClickEventArgs(x,y));	
			}
			
			
		}
		
		public void MouseButtonUpInput(Kinetic.IO.MouseButton button, int x, int y) {
			int mouseButton = (int)button;
			if(mouse[mouseButton]) {
				mouse[mouseButton] = false;
				MouseClickEventArgs mouseClickEventArgs = new MouseClickEventArgs(x,y);
				if(button == MouseButton.Left) {
					OnMouseLeftUp(mouseClickEventArgs);	
					OnMouseLeftClick(mouseClickEventArgs);
				} else if(button == MouseButton.Middle) {
					OnMouseMiddleUp(mouseClickEventArgs);	
					OnMouseMiddleClick(mouseClickEventArgs);
				} else if(button == MouseButton.Right) {
					OnMouseRightUp(mouseClickEventArgs);	
					OnMouseRightClick(mouseClickEventArgs);
				}
			}
		}
		
		
		public bool KeyIsPressed (Kinetic.IO.Key key) {
			int keyPressed = (int)key;
			return register[keyPressed];
		}
		
		public bool MouseButtonIsPressed(Kinetic.IO.MouseButton button) {
			int mouseButton = (int)button;
			return mouse[mouseButton];
		}
		
		public MouseMoveDeltaEventArgs MouseMoveDeltaRead() {
			MouseMoveDeltaEventArgs mouseMoveDetla = new MouseMoveDeltaEventArgs((int) xMoveHolder, (int) yMoveHolder);
			xMoveHolder = 0;
			yMoveHolder = 0;
			return mouseMoveDetla;
		}
	}
}

