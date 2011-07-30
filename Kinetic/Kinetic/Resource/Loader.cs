using System;

namespace Kinetic.Resource
{
	public abstract class Loader<T> 
		where T : Asset
	{
		public Loader ()
		{
		}
		
		/*
		public abstract void LoadIntoSystemMemory();
		
		public abstract void LoadIntoVideoMemory();
		 */
		}
}

