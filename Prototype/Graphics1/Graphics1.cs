using System;

namespace Graphics1
{
	public class Graphics1
	{
		public Graphics1 ()
		{
		}
		
		public void Run() {
			Console.WriteLine("Running Graphics Processor.");
			
		
		}
		
		public static void Main(string[] args) {
			using (Graphics1 p = new Graphics1 ()) {
				p.Run();
			}
		}
	}
}

