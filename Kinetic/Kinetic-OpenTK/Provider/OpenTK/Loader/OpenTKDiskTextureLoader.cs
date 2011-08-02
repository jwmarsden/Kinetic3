using System;
using System.Drawing;
using System.Drawing.Imaging;

using Kinetic.Resource;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Kinetic.Provide
{
	public class OpenTKDiskTextureLoader<A>: TextureLoader<A>
		where A: Texture
	{
		public OpenTKDiskTextureLoader (A texture): base(texture)
		{
		}
		
		public override void LoadIntoSystemMemory() {
			if(!_texture.InSystemMemory) {
				Console.WriteLine(string.Format("Resource \"{0}\" Not Found in System Memory", _texture.Name));
				_texture.Bitmap = new Bitmap(_texture.Path);
				_texture.InSystemMemory = true;
				Console.WriteLine(string.Format("Resource \"{0}\" -> System Memory", _texture.Name));
			}
			
		}

		public override void LoadIntoVideoMemory() {
			if(_texture.InSystemMemory && ! _texture.InVideoMemory) {
				Console.WriteLine(string.Format("Resource \"{0}\" Not Found in Video Memory", _texture.Name));
				GL.GenTextures(1, out _texture._handle);
				GL.BindTexture(TextureTarget.Texture2D, _texture._handle);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
				BitmapData data = _texture.Bitmap.LockBits(new Rectangle(0, 0, _texture.Bitmap.Width, _texture.Bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
				_texture.Bitmap.UnlockBits(data);
				_texture.InVideoMemory = true;
				Console.WriteLine(string.Format("Resource \"{0}\" -> Video Memory ({1})", _texture.Name, _texture.Handle));
			}
		}
	}
}

