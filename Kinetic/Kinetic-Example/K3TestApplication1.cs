
using System;

using Kinetic.Base;
using Kinetic.IO;

namespace KineticExample
{
	public class K3TestApplication1: BaseApplication
	{
		public K3TestApplication1 ()
		{
		}
		
		public override void Initialize() 
		{	
			Display display = displays[0];
			display.SetTitle("Kinetic K3");
			string[] extensions = display.SupportedExtensions();
			Console.Write("Supported Extensions: ");
			foreach (string extension in extensions) 
			{
				Console.Write(string.Format("{0}", extension));	
			}
			Console.Write("\r\n");
			
			
			//Renderer renderer = renderers[0];
			//renderer.SetBackgroundColor(Color.DarkGray);
			/*
			handlers = new Handler[1];
			firstPersonHandler = new FirstPersonHandler(display, renderer);
			firstPersonHandler.UpDown = true;
			handlers[0] = firstPersonHandler;
			*/

			
			
		}
		
		public override void Update(long time) {
		}
		
		public static void Main (string[] args) 
		{
			Console.WriteLine("Running Application.");	
			K3TestApplication1 k3App1 = new K3TestApplication1();
			k3App1.start();
		}
	}
}

