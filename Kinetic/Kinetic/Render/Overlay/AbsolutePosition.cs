using System;

namespace Kinetic.Render.Overlay
{
	public class AbsolutePosition: OverlayPosition
	{
		public int _xPos;
		public int _yPos;
		public int _width;
		public int _height;
		
		public AbsolutePosition (int xPos, int yPos, int width, int height)
		{
			_xPos = xPos;
			_yPos = yPos;
			_width = width;
			_height = height;
		}
		
		public int XPos {
			get { return _xPos; }
			set { _xPos = value; }
		}
		
		public int YPos {
			get { return _yPos; }
			set { _yPos = value; }
		}
		
		public int Width {
			get { return _width; }
			set { _width = value; }
		}
		
		public int Height {
			get { return _height; }
			set { _height = value; }
		}
		
	}
}

