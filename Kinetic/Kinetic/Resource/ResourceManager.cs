using System;

namespace Kinetic.Resource
{
	public abstract class ResourceManager
	{
		public ResourceManager ()
		{
		}

		public abstract Texture ImportTexture(Catalog catalog, string name, string path);
		
		/*
		public abstract ModelGroup LoadModel(Catalog catalog, string name, string path);
		
		public abstract VertexProgram LoadVertexProgram(Catalog catalog, string name, string path);
		
		public abstract FragmentProgram LoadFragmentProgram(Catalog catalog, string name, string path);
		*/
	}
}

