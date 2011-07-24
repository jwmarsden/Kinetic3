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

namespace Kinetic.Base
{
	public abstract class BaseApplication
	{
		protected Provider provider;
		
		public int mainDisplay = 0;
		public int mainRenderer = 0;
		
		/// <summary>
		/// Container to hold the display objects. Must be same cardinality as renderers.
		/// </summary>
		protected Display[] displays;
		
		/// <summary>
		/// Constianer to hold the renderer objects. Must be same cardinality as displays.
		/// </summary>
		protected Renderer[] renderers;

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
			provider = Provider.Instance;
			displays = null;
			renderers = null;
			//handlers = null;
			//sceneHolder = null;
		}
		
		public Display[] Displays
		{
			get { return displays; }
		}
		
		public abstract void Initialize();
		
		public abstract void Update(long time);
		
		public void start() {
			CreateDisplays();
			
			CreateRenderers();
			
			Console.WriteLine("Initialise Application.");
			Initialize();
			
			Console.WriteLine("Display Window.");
			displays[mainDisplay].ShowWindow();
			
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
					foreach (Display display in displays) {
						display.ProcessEvents();
						if(display.Closing && d==mainDisplay) {
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
							/*
							renderers[mainRenderer].Clear();
							foreach (Renderer renderer in renderers) {
								
								if(renderer != null) {
									renderer.RenderScene(sceneHolder);
								}
							}
							*/
	
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
			displays = new Display[1];
			displays[mainDisplay] = provider.CreateDisplay();
			Console.WriteLine("Constructing Main Display Window.");
			displays[mainDisplay].Width = 800;
			displays[mainDisplay].Height = 600;
			displays[mainDisplay].CreateWindow();
		}	
		
		public virtual void CreateRenderers() {
			Console.WriteLine("Creating Main Renderer.");
			renderers = new Renderer[1];
			//renderers[mainRenderer] = provider.CreateRenderer(displays[mainDisplay]);
			//renderers[mainRenderer].Initialize();
		}
	}
}

