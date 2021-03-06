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

namespace Kinetic.Render
{
	[FlagsAttribute]
	public enum CullMode
	{
		VIEW=1,
		ALWAYS=2,
		NEVER=3,
		INHERIT=4
	}
	
	[FlagsAttribute]
	public enum State {
		CURRENT,
		UPDATE_LOCAL_BOUND,
		UPDATE_WORLD_BOUND,
		UPDATE_WORLD_TRANSFORM
	}
}

