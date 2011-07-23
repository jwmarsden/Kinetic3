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
using Kinetic.Render;

namespace Kinetic.IO
{
	public delegate void LoadEventHandler(object sender, EventArgs e);
	
	public abstract class Display
	{
		protected string title;
		protected int width;
		protected int height;
		
		protected InputRegister inputRegister = null;	
		protected bool held;
		protected bool externalMouseReadWhileHeld;
		protected bool closing;
		protected bool customMouseInput;
		
		public Display ()
		{	
			title = "Kinetic Window";
			width = 640;
			height = 480;
			closing = false;
			held = false;
			externalMouseReadWhileHeld = false;
		}
		
		public int Width
		{
			get { return width; }
			set { width = value; }
		}
		
		public int Height
		{
			get { return height; }
			set { height = value; }
		}
		
		public string Title
		{
			get { return title; }
		}
		
		public bool Held {
			get { return held; }	
		}
		
		public bool Closing {
			get { return closing; }	
		}
		
		public InputRegister InputRegister
		{
			get { return inputRegister; }
			set { inputRegister = value; }
		}
		
		public bool CustomMouseInput {
			get { return customMouseInput; }
			set { customMouseInput = value; }
		}
		
		public event LoadEventHandler Load;
		
		protected virtual void OnLoad(EventArgs e) {
			if (Load != null) {
				Load(this, e);
			}
		}
		
		public abstract string[] SupportedExtensions();
		
		public abstract void SetTitle(string title);
		
		public abstract void CreateWindow();
		
		public abstract void Update();
		
		public abstract void ShowWindow();
		
		public abstract void HideWindow();
		
		public abstract void HoldInput();
		
		public abstract void HoldInput(bool externalMouseRead);
		
		public abstract void ReleaseInput();
		
		public abstract void ProcessEvents();
		
		public abstract MouseMoveDeltaEventArgs MouseMoveDeltaRead();
		
		public abstract void BeforeRender();
		
		public abstract void AfterRender();
	}
}

