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
		
		public CatalogEntry (A Asset, L Loader)
		{
			_name = Asset.Name;
			_id = Asset.ID;
			_asset = Asset;
			_loader = Loader;
		}
		
		public A Asset {
			get { return _asset; }
		}
		
		public L Loader {
			get { return _loader; }
		}
	}
}

