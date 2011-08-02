using System;
using System.Drawing;

namespace Graphics1
{
	public class Graphics1: IDisposable
	{
		public Graphics1 ()
		{
		}
		
		public void Run() {
			Console.WriteLine("Running Graphics Processor.");
			Bitmap bitmap1 = new Bitmap(600,400);
			using (Graphics graphics = Graphics.FromImage(bitmap1))
			{
				graphics.Clear(Color.Transparent);
				String drawString = "This is not art!";
				Font drawFont = new Font("Serif", 10);
				SolidBrush drawBrush = new SolidBrush(Color.Red);
				PointF drawPoint = new PointF(20F, 20F);
				graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
			}
			
			Bitmap bitmap2 = new Bitmap(600,400);
			using (Graphics graphics = Graphics.FromImage(bitmap2))
			{
				graphics.Clear(Color.Transparent);
				String drawString = "This is not art!";
				Font drawFont = new Font("Serif", 10);
				SolidBrush drawBrush = new SolidBrush(Color.Blue);
				PointF drawPoint = new PointF(100F, 50F);
				graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
				
				//graphics.DrawImage(bitmap1, new PointF(0,0));
			}
			
			Bitmap bitmap3 = new Bitmap("screenshot.png");
			using (Graphics graphics = Graphics.FromImage(bitmap3))
			{
				String drawString = "Image 3!";
				Font drawFont = new Font("Serif", 10);
				SolidBrush drawBrush = new SolidBrush(Color.Yellow);
				PointF drawPoint = new PointF(400F, 350F);
				graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
				
				graphics.DrawImage(bitmap1, new PointF(0,0));
				graphics.DrawImage(bitmap2, new PointF(0,0));
			}
			
			bitmap3.Save("tmp1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
		}
		
		public void Dispose() {
			
		}
		
		public static void Main(string[] args) {
			using (Graphics1 p = new Graphics1 ()) {
				p.Run();
			}
		}
		
		
	}
}

