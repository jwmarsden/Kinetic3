using System;

namespace Kinetic.Resource
{
	public abstract class ResourceManager
	{
		public ResourceManager ()
		{
		}

		public abstract TextureLoader<Texture> CreateTextureLoader(Texture Texture);
		
		public abstract Texture ImportTexture(Catalog Catalog, string Name, string Path);
		
		/*
		public abstract ModelGroup ImportModel(Catalog catalog, string name, string path);
		
		public abstract VertexProgram ImportVertexProgram(Catalog catalog, string name, string path);
		
		public abstract FragmentProgram ImportFragmentProgram(Catalog catalog, string name, string path);
		*/
	}
}

