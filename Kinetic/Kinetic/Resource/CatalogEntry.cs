using System;
using System.Collections;
using System.Collections.Generic;

namespace Kinetic.Resource
{
	public class CatalogEntry<A, L> 
		where A : Asset 
		where L : Loader<A>
	{
		readonly string _name;
		readonly int _id;
		readonly A _asset;
		readonly L _loader;
		
		public CatalogEntry (A asset, L loader)
		{
			_name = asset.Name;
			_id = asset.ID;
			_asset = asset;
			_loader = loader;
		}
		
		public A Asset {
			get { return _asset; }
		}
		
		public L Loader {
			get { return _loader; }
		}
	}
}

