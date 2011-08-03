using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using Kinetic.Resource;

namespace Kinetic.Render.Overlay
{
	public class OverlayHolder: TextureSource
	{
		public ResourceManager _resourceManager;
		public Catalog _catalog;
		public int _width;
		public int _height;
		
		public bool _dirty;
		public Texture _overlayTexture;
		public Bitmap _overlay;
		public OverlayItem _background;
		public List<OverlayItem> _items;
		
		public bool _newBitmap;
		
		public OverlayHolder (ResourceManager ResourceManager, Catalog Catalog) {
			_resourceManager = ResourceManager;
			_catalog = Catalog;
			_width = 0;
			_height = 0;
			_background = null;
			_items = new List<OverlayItem>();
			_dirty = false;
			
			_overlay = null;
			_overlayTexture = null;
			
			_newBitmap = false;
		}
		
		public OverlayHolder (ResourceManager ResourceManager, Catalog Catalog, int Width, int Height)
		{
			_resourceManager = ResourceManager;
			_catalog = Catalog;
			_width = Width;
			_height = Height;
			_background = null;
			_items = new List<OverlayItem>();
			_dirty = false;
			
			_overlay = null;
			_overlayTexture = null;
			
			_newBitmap = false;
		}
		
		public void Resize(int Width, int Height) {
			_width = Width;
			_height = Height;
			_dirty = true;
		}
		
		public void Initialize() {
			_overlayTexture = new Texture();
			_overlayTexture.Name = "$overlay";
			_overlayTexture.TextureSource = this;
			
			TextureLoader<Texture> loader = _resourceManager.CreateTextureLoader(_overlayTexture);
			_catalog.RegisterTexture(ref _overlayTexture, ref loader);
			
			_dirty = true;
		}
		
		public bool HasOverlay() {
			return (_background != null || _items.Count != 0);
		}
		
		public Bitmap GetOverlayImage() {
			if(_overlay == null || _dirty == true) {
				_overlay = GenerateOverlayImage();
				_dirty = false;
				_newBitmap = true;
			}
			return _overlay;
		}
		
		public Texture GetOverlay() {
			if(_overlay == null || _dirty == true || _newBitmap == true) {
				GetOverlayImage();
				_catalog.FindTextureLoader("$overlay").UpdateVideoMemory();
			}
			return _overlayTexture;
		}
		
		public Bitmap GenerateOverlayImage() {
			if(_height == 0 || _width == 0) {
				return null;
			}
			Bitmap bitmap = new Bitmap(_width, _height);
			Console.WriteLine(string.Format("Generate Overlay ({0}x{1}).", _width, _height));
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				if (_background != null) {
					graphics.DrawImage(_background.Bitmap, 0, 0, _width, _height);
					Console.WriteLine("Added Background");
				}
				foreach (OverlayItem item in _items) {
					if(item.Position == null) {
						graphics.DrawImage(item.Bitmap, 0, 0, item.Bitmap.Width, item.Bitmap.Height);
					}
					if(item.Position is AbsolutePosition) {
						AbsolutePosition position = (AbsolutePosition) item.Position;
						graphics.DrawImage(item.Bitmap, position.XPos, position.YPos, position.Width, position.Height);
					}
				}
			}
			return bitmap;
		}

		public bool HasNewBitmap() {
			return HasOverlay() && (_dirty || _newBitmap);	
		}
		
		public bool HasBitmap() {
			return HasOverlay();
		}
		
		public Bitmap GetNewBitmap() {
			return GetOverlayImage();
		}
		
		public Bitmap GetBitmap() {
			return GetOverlayImage();
		}
	}
}

