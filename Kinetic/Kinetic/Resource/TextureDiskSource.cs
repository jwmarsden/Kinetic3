using System;
using System.Drawing;
using System.IO;

namespace Kinetic.Resource
{
	public class TextureDiskSource: TextureSource
	{
		protected string _path;
		
		public TextureDiskSource (string path)
		{
			_path = path;
		}
		
		public string Path {
			get { return _path; }
			set { _path = value; }
		}
		
		public bool HasNewBitmap() {
			return false;	
		}
		
		public bool HasBitmap() {
			return true;	
		}
		
		public Bitmap GetNewBitmap() {
			return GetBitmap();	
		}
		
		public Bitmap GetBitmap() {
			if(_path == null || _path.Trim().Equals("")) {
				throw new Exception("Path cannot be null or empty.");
			}
			if(!File.Exists(_path)) {
				throw new Exception(string.Format("No file found at the path {0}", _path));
			}
			Bitmap bitmap = new Bitmap(_path);
			return bitmap;
		}
	}
}

