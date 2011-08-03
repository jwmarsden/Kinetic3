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
	public class Catalog
	{
		List<Texture> _textureList;
		Dictionary<string, CatalogEntry<Texture, TextureLoader<Texture>>> _textureCatalog;
		//Dictionary<string, CatalogEntry<VertexBuffer, VertexBufferLoader>> _vertexBufferCatalog;
		
		public Catalog ()
		{
			_textureList = new List<Texture>();	
			_textureCatalog = new Dictionary<string, CatalogEntry<Texture, TextureLoader<Texture>>>();
		}
		
		public Texture RegisterTexture(ref Texture texture, ref TextureLoader<Texture> textureLoader) {
			if(_textureList.Contains(texture)) {
				throw new Exception(string.Format("Texture {0} already registered.", texture));
			}
			if(_textureCatalog.ContainsKey(texture.Name)) {
				throw new Exception(string.Format("A Texture with the Name {0} already exists in this catalog. Cannot insert {1}.", texture.Name, texture));
			}
			_textureList.Add(texture);
			_textureCatalog.Add(
				texture.Name, 
			    new CatalogEntry<Texture, TextureLoader<Texture>>(texture, textureLoader)
			);
			return texture;
		}
		
		public Texture FindTexture(int id) {
			Texture texture = _textureList[id];
			return texture;
		}
		
		public Texture FindTexture(string name) {
			CatalogEntry<Texture, TextureLoader<Texture>> textureCatalogEntry = _textureCatalog[name];
			if(textureCatalogEntry != null) {
				return textureCatalogEntry.Asset;
			} else {
				return null;
			}
		}
		
		public TextureLoader<Texture> FindTextureLoader(Texture Texture) {
			CatalogEntry<Texture, TextureLoader<Texture>> textureCatalogEntry = _textureCatalog[Texture.Name];
			if(textureCatalogEntry != null) {
				return textureCatalogEntry.Loader;
			} else {
				return null;
			}
		}
		
		public TextureLoader<Texture> FindTextureLoader(string name) {
			CatalogEntry<Texture, TextureLoader<Texture>> textureCatalogEntry = _textureCatalog[name];
			if(textureCatalogEntry != null) {
				return textureCatalogEntry.Loader;
			} else {
				return null;
			}
		}
	}
}