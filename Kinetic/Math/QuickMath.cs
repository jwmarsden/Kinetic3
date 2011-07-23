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

namespace Kinetic.Math
{
	public class QuickMath
	{
		protected static Random random = null;
		
		/// <summary>
		/// Constant value for PI
		/// </summary>
		public const float PI = 3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647093844609550582231725359408128481117450284102701938521105559644622948954930382f;
		public const float TWO_PI = 2 * QuickMath.PI;
		public const float HALF_PI = QuickMath.PI / 2;
		
		public const float EPSILON = 1E-6f;
		
		public static float Pow(float n, float p) {
			return (float) System.Math.Pow(n, p);
		}
		
		public static float Sqrt(float n) {
			return (float) System.Math.Sqrt(n);
		}
		
		public static float Abs(float n) {
			return (float) System.Math.Abs(n);
		}
		
		public static float Cos(float n) {
			return (float) System.Math.Cos(n);
		}
		
		public static double Sin(double n) {
			return System.Math.Sin(n);
		}
		
		public static float Tan(float n) {
			return (float) System.Math.Tan(n);
		}
		
		public static double Random() {
			if(random == null) {
				random = new Random();
			}
			return random.NextDouble();
		}
		
		public static int RandomInt(int low, int high) {
			int band = high-low;
			int randomValue = (int) (Random() * (double) band);
			return randomValue + low;
		}
	}
}

