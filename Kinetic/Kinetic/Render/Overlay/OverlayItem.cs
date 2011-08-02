using System;
using System.Drawing;

namespace Kinetic.Render.Overlay
{
	public class OverlayItem
	{
		public Bitmap _bitmap;
		public int _zOrder;
		public OverlayPosition _position;
		
		public OverlayItem (Bitmap bitmap) {
			_bitmap = bitmap;
			_zOrder = 1;
		}
		
		public Bitmap Bitmap {
			get { return _bitmap; }
			set { _bitmap = value; }
		}
		
		public int ZOrder {
			get { return _zOrder; }
			set { _zOrder = value; }
		}
		
		public OverlayPosition Position {
			get { return _position; }
			set { _position = value; }
		}
	}
}

