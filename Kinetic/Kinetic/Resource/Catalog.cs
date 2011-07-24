 #region License
 //  Copyright 2010-2011 J.W.Marsden
 // 
 //    Licensed under the Apache License, Version 2.0 (the "License");
 //    you may not use this file except in compliance with the License.
 //    You may obtain a copy of the License at
 // 
 //        http://www.apache.org/licenses/LICENSE-2.0
 // 
 //    Unless required by applicable law or agreed to in writing, software
 //    distributed under the License is distributed on an "AS IS" BASIS,
 //    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 //    See the License for the specific language governing permissions and
 //    limitations under the License.
 #endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace Kinetic.Resource
{
	/// <summary>
	/// Global storage for vertex buffers, indicies buffers, textures, 
	/// vertex programs and fragment programs.
	/// 
	/// Each of the sub types are stored in an instance of Catalog and bound
	/// statically to this object. Only one catalog can be active for any
	/// given type at a time.
	/// </summary>
	public class Catalog<CatalogType>
	{
		/// <summary>
		/// Storage for the items in the catalog.
		/// </summary>
		Dictionary<string, CatalogType> catalogItems;
		
		static Catalog<VertexBuffer> vertexBufferCatalog;
		static Catalog<IndiciesBuffer> indiciesBufferCatalog;
		static Catalog<Texture> textureCatalog;
		static Catalog<VertexProgram> vertexProgramCatalog;
		static Catalog<FragmentProgram> fragmentProgramCatalog;
		
		public Catalog ()
		{
			catalogItems = new Dictionary<string, CatalogType>();
			
			vertexBufferCatalog = null;
			indiciesBufferCatalog = null;
			textureCatalog = null;
			vertexProgramCatalog = null;
			fragmentProgramCatalog = null;
		}
		
		public int Count {
			get { return catalogItems.Count; }	
		}
		
		public CatalogType this[string name] {
			get { return catalogItems[name]; }
			set { catalogItems[name] = value; }
		}
		
		static public Catalog<VertexBuffer> VertexBuffers {
			get { return vertexBufferCatalog; }
			set { vertexBufferCatalog = value; }
		}
		
		static public Catalog<IndiciesBuffer> IndiciesBuffers {
			get { return indiciesBufferCatalog; }
			set { indiciesBufferCatalog = value; }
		}
		
		static public Catalog<Texture> Textures {
			get { return textureCatalog; }
			set { textureCatalog = value; }
		}

		static public Catalog<VertexProgram> VertexPrograms {
			get { return vertexProgramCatalog; }
			set { vertexProgramCatalog = value; }
		}
		
		static public Catalog<FragmentProgram> FragmentPrograms {
			get { return fragmentProgramCatalog; }
			set { fragmentProgramCatalog = value; }
		}
	}
}

