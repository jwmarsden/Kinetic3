using System;
using Kinetic;
using Kinetic.IO;
using Kinetic.Render;
using Kinetic.Resource;

namespace Kinetic.Provide
{
	public class OpenTKProvider: Provider
	{
		public OpenTKProvider ()
		{
		}
		
		public override Display CreateDisplay () {
			return new OpenTKDisplay();
		}
		
		public override ResourceManager CreateResourceManager() {
			return new OpenTKResourceManager();
		}
	}
}

