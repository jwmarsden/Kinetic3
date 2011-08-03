using System;

namespace Kinetic.Resource
{
	public abstract class Loader<T> 
		where T : Asset
	{
		public Loader ()
		{
		}
		
		public abstract void LoadIntoSystemMemory();

		public abstract void LoadIntoVideoMemory();
		
		public abstract void ReleaseFromSystemMemory();
		
		public abstract void ReleaseFromVideoMemory();
		
		public abstract void UpdateSystemMemory();
		
		public abstract void UpdateVideoMemory();
	}
}

