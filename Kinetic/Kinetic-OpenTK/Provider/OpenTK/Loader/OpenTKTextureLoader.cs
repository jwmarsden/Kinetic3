using System;
using System.Drawing;
using System.Drawing.Imaging;

using Kinetic.Resource;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Kinetic.Provide
{
	public class OpenTKTextureLoader<A>: TextureLoader<A>
		where A: Texture
	{
		public OpenTKTextureLoader (A texture): base(texture)
		{
		}
		
		public override void LoadIntoSystemMemory() {
			if(!_texture.InSystemMemory) {
				Console.WriteLine(string.Format("Resource \"{0}\" Not Found in System Memory", _texture.Name));
				TextureSource source = _texture.TextureSource;
				if(source == null) {
					throw new Exception(string.Format("No texture source found for Texture {1} with ID {0}", _texture.ID, _texture.Name));
				}
				if(source.HasBitmap()) {
					_texture.Bitmap = source.GetBitmap();	
				} else {
					// TODO: Load the default bitmap.	
				}
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
		
		public override void ReleaseFromSystemMemory() {
			
		}
		
		public override void ReleaseFromVideoMemory() {
			
		}
		
		public override void UpdateSystemMemory() {
			if(!_texture.InSystemMemory) {
				LoadIntoSystemMemory();
			} else {
				// TODO: Release and upload again?	
			}
		}
		
		public override void UpdateVideoMemory() {
			if(_texture.InSystemMemory && ! _texture.InVideoMemory) {
				LoadIntoVideoMemory();
			} else {
				// TODO: Release and upload again?	
			}
		}
	}
}

