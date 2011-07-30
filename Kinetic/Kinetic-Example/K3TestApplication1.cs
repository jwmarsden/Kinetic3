
using System;
using System.Drawing;

using Kinetic.Base;
using Kinetic.IO;
using Kinetic.Common;
using Kinetic.Math;
using Kinetic.Scene;
using Kinetic.Resource;
using Kinetic.Render;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace KineticExample
{
	public class K3TestApplication1: BaseApplication
	{
		Texture kineticMainImageTexture = null;
		
		public K3TestApplication1 ()
		{
		}
		
		public override void Initialize() 
		{	
			Display display = _displays[0];
			display.SetTitle("Kinetic K3");
			string[] extensions = display.SupportedExtensions();
			Console.Write("Supported Extensions: ");
			foreach (string extension in extensions) 
			{
				Console.Write(string.Format("{0}", extension));	
			}
			Console.Write("\r\n");
			
			kineticMainImageTexture = ResourceManager.ImportTexture(MainRenderer.Catalog, "KineticBanner", "KineticBanner.jpg");
		}
		
		public override void Update(long time) {	
		}
		
		public override void ApplicationRender() {
			MainRenderer.DrawTexture(kineticMainImageTexture);
		}
		
		public static void Main (string[] args) 
		{
			Console.WriteLine("Running Application.");	
			K3TestApplication1 k3App1 = new K3TestApplication1();
			k3App1.start();
		}
	}
}

