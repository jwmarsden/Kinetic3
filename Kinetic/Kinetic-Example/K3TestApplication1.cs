
using System;
using System.Drawing;

using Kinetic.Base;
using Kinetic.IO;
using Kinetic.Common;
using Kinetic.Math;
using Kinetic.Scene;
using Kinetic.Resource;
using Kinetic.Render;
using Kinetic.Render.Overlay;

namespace KineticExample
{
	public class K3TestApplication1: BaseApplication
	{
		
		OverlayHolder _overlayHolder;
		
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

			_overlayHolder = new OverlayHolder(ResourceManager, MainRenderer.Catalog, MainRenderer.Width, MainRenderer.Height);
			
			ResourceManager.ImportTexture(MainRenderer.Catalog, "KineticBanner", "Resources/KineticBanner.jpg");
		}
		
		public override void Update(long time) {
		}
		
		public override void ApplicationRender() {
			Texture overlayTexture = _overlayHolder.GetOverlay();
			if(overlayTexture != null) {
				overlayTexture.Bitmap.Save("blah.3.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
			}
			//MainRenderer.Draw(_overlayHolder.GetOverlay(),0,0,800,600);
		}
		
		public static void Main (string[] args) 
		{
			Console.WriteLine("Running Application.");	
			K3TestApplication1 k3App1 = new K3TestApplication1();
			k3App1.start();
		}
	}
}

