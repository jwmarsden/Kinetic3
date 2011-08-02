using System;
using System.IO;
using Kinetic.Resource;

namespace Kinetic.Provide
{
	public class OpenTKResourceManager: ResourceManager
	{
		public OpenTKResourceManager ()
		{
		}
		
		public override Texture ImportTexture(Catalog Catalog, string Name, string Path) {
			Console.WriteLine(string.Format("ImportTexture({0},\"{1}\",\"{2}\")", Catalog, Name, Path));
			
			if(!File.Exists(Path)) {
				throw new Exception(string.Format("There is no image for \"{0}\" found at the specified path \"{1}\"", Name, Path));
			}
			
			Texture texture = new Texture();
			texture.ID = 1;
			texture.Name = Name;
			texture.Path = Path;
			
			TextureLoader<Texture> textureLoader = new OpenTKDiskTextureLoader<Texture>(texture);
			
			Catalog.RegisterTexture(ref texture, ref textureLoader);
		
			return texture;
		}
		
	}
}

