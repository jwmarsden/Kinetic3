
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
		Texture _kineticBannerTexture = null;
		
		public K3TestApplication1 ()
		{
		}
		
		public override void Initialize() 
		{	
			MainDisplay.SetTitle("Kinetic K3");
			string[] extensions = MainDisplay.SupportedExtensions();
			Console.Write("Supported Extensions: ");
			foreach (string extension in extensions) 
			{
				Console.Write(string.Format("{0}", extension));	
			}
			Console.Write("\r\n");
			_kineticBannerTexture = ResourceManager.ImportTexture(MainRenderer.Catalog, "KineticBanner", "Resources/KineticBanner.jpg");
		}
		
		public override void Update(long time) {	
		}
		
		public override void ApplicationRender() {
			MainRenderer.Draw(_kineticBannerTexture,50,50,120,100);
			MainRenderer.Draw(0, 0, Color.Red, "This is not Art!");
		}
		
		public static void Main (string[] args) 
		{
			Console.WriteLine("Running Application.");	
			K3TestApplication1 k3App1 = new K3TestApplication1();
			k3App1.start();
		}
	}
}

