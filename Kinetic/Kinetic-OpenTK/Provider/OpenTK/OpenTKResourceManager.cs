using System;
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
			// Check that File Exists
			
			// Create Texture Instance
			
			// Create Texture Loader Instance
			
			// Register with Catalog
			
			// Return Texture
			
			return null;
		}
	}
}

