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
using System.Drawing;
using Kinetic.Math;

namespace Kinetic.Common
{
	public class ColorUtil
	{
		public static Color Random ()
		{
			Color color = Color.FromArgb (QuickMath.RandomInt (0, 255), QuickMath.RandomInt (0, 255), QuickMath.RandomInt (0, 255));
			return color;
		}

		public static int ColorToRgba32 (Color c)
		{
			return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
		}
	}
}

