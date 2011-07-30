using System;

namespace Kinetic.Resource
{
	public class TextureLoader<A>: Loader<A>
		where A: Texture
	{
		A _texture;
		
		public TextureLoader (A texture)
		{
			_texture = texture;
		}
		
		public A Texture {
			get { return _texture; }
			set { _texture = value; }
		}
		
	}
}

