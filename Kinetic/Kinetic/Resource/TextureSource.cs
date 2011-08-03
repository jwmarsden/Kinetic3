using System;
using System.Drawing;

namespace Kinetic.Resource
{
	public interface TextureSource
	{
		bool HasNewBitmap();
		
		bool HasBitmap();
		
		Bitmap GetNewBitmap();
		
		Bitmap GetBitmap();
	}
}

