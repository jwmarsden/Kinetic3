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
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Kinetic.Scene;
using Kinetic.IO;
using Kinetic.Render;
using Kinetic.Resource;

namespace Kinetic.Base
{
	public abstract class BaseApplication
	{
		protected Provider _provider;
		protected ResourceManager _resourceManager;
		
		public int _mainDisplay = 0;
		public int _mainRenderer = 0;
		
		/// <summary>
		/// Container to hold the display objects. Must be same cardinality as renderers.
		/// </summary>
		protected Display[] _displays;
		
		/// <summary>
		/// Constianer to hold the renderer objects. Must be same cardinality as displays.
		/// </summary>
		protected Renderer[] _renderers;

		/// <summary>
		/// Constianer to hold extra handler objects.
		/// </summary>
		//protected Handler[] handlers;
		
		/// <summary>
		/// Scene Holder container.
		/// </summary>
		//protected SceneHolder sceneHolder;
		
		public BaseApplication ()
		{
			_provider = new Kinetic.Provide.OpenTKProvider();
			_resourceManager = _provider.CreateResourceManager();
			_displays = null;
			_renderers = null;
			//handlers = null;
			//sceneHolder = null;
		}
		
		public Display[] Displays {
			get { return _displays; }
		}
		
		public Display MainDisplay {
			get { return _displays[_mainDisplay]; }
		}
		
		public Renderer[] Renderers {
			get { return _renderers; }	
		}
		
		public Renderer MainRenderer {
			get { return _renderers[_mainRenderer]; }	
		}
		
		public Provider Provider {
			get { return _provider; }
		}
		
		public ResourceManager ResourceManager {
			get { return _resourceManager; }	
		}
		
		public abstract void Initialize();
		
		public abstract void Update(long time);
		
		public virtual void ApplicationRender() {
		}
		
		public void start() {
			CreateDisplays();
			
			CreateRenderers();
			
			Console.WriteLine("Initialise Application.");
			Initialize();
			
			Console.WriteLine("Display Window.");
			_displays[_mainDisplay].ShowWindow();
			
			Console.WriteLine("Initialise Application Loop.");
			Stopwatch timer = new Stopwatch();
			Stopwatch loopWatch = new Stopwatch();
			Stopwatch updateWatch = new Stopwatch();
			Stopwatch renderWatch = new Stopwatch();
			Stopwatch sleepWatch = new Stopwatch();
			
			float sleepTime = 25;
			float loopPeriod = 25;
			float loopErrorCorrect = 0;
			
			long timeBase = timer.ElapsedTicks;
			long time = timer.ElapsedTicks - timeBase;
			bool running = true;

			Console.WriteLine("Run Application Loop.");
			while(running) {
				loopWatch.Reset();
				updateWatch.Reset();
				renderWatch.Reset();
				
				timer.Stop();
				time = timer.ElapsedTicks - timeBase;
				timer.Start();
				/**
				 * Begin Render Loop
				 */
				loopWatch.Start();
				{
					/**
					* Process Display Events
					**/
					updateWatch.Start();
					{

						/**
						 * Update Scene
						 **/					
						//sceneHolder.Update(time);
						/**
						* Update Application
						**/
						Update(time);
						/**
						 * Update Handlers
						 **/
						//foreach (Handler handler in handlers) {
						//	handler.Update(time);	
						//}
					}
					updateWatch.Stop();
					
					int d = 0;
					foreach (Display display in _displays) {
						display.ProcessEvents();
						if(display.Closing && d==_mainDisplay) {
							Console.WriteLine("Close of Main Display Detected.");
							running = false;
							break;
						}
						renderWatch.Start();
						{
							/**
							 * Handle Render
							 **/
							display.BeforeRender();
							_renderers[_mainRenderer].ClearBuffers();
							/*
							foreach (Renderer renderer in renderers) {
								
								if(renderer != null) {
									renderer.RenderScene(sceneHolder);
								}
							}
							*/
							
							ApplicationRender();
							
							display.AfterRender();
						}
						renderWatch.Stop();
						d++;
					}	
				}
				if(!running) {
					break;
				}
				/**
				 * End Render Loop
				 */
		
				sleepTime = loopPeriod - updateWatch.ElapsedMilliseconds - renderWatch.ElapsedMilliseconds + loopErrorCorrect;
				if(sleepTime < 1)
				{
					sleepTime = 1;
				}
					
				sleepWatch.Reset();
				sleepWatch.Start();
				{
					Thread.Sleep((int) sleepTime);
				}
				sleepWatch.Stop();
				
				loopWatch.Stop();
				
				//Console.WriteLine("UpdateWatch:" + updateWatch.ElapsedMilliseconds + " RenderWatch:" + renderWatch.ElapsedMilliseconds + " SleepWatch:" + sleepWatch.ElapsedMilliseconds);
				
				loopErrorCorrect = loopPeriod - loopWatch.ElapsedMilliseconds;
			}
			Console.WriteLine("Application Exit.");
		}
		
		public void CreateDisplays() {
			Console.WriteLine("Creating Display.");
			_displays = new Display[1];
			_displays[_mainDisplay] = _provider.CreateDisplay();
			Console.WriteLine("Constructing Main Display Window.");
			_displays[_mainDisplay].Width = 800;
			_displays[_mainDisplay].Height = 600;
			_displays[_mainDisplay].CreateWindow();
			Console.WriteLine("Main Display Window Created.");
		}	
		
		public virtual void CreateRenderers() {
			Console.WriteLine("Creating Main Renderer.");
			_renderers = new Renderer[1];
			Renderer renderer = _provider.CreateRenderer();
			renderer._x = 0;
			renderer._y = 0;
			renderer._width = MainDisplay.Width;
			renderer._height = MainDisplay.Height;
			renderer._catalog = new Catalog();
			_renderers[_mainRenderer] = renderer;

			
			//renderers[mainRenderer].Initialize();
		}
	}
}

