using System;
using System.Collections;
using System.Collections.Generic;

namespace Kinetic.Render.Overlay
{
	public class OverlayHolder
	{
		public bool _dirty;
		public OverlayItem _background;
		public List<OverlayItem> _items;
		
		public OverlayHolder ()
		{
			_background = null;
			_items = new List<OverlayItem>();
			_dirty = false;
		}
		
		
	}
}

