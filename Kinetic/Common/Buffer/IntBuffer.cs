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

namespace Kinetic.Common
{
	public class IntBuffer : Buffer
	{
		protected int[] buffer;

		public IntBuffer (int size) : base(size)
		{
			buffer = new int[size];
		}

		public IntBuffer (uint[] contents) : base(contents.Length)
		{
			buffer = new int[contents.Length];
			for (int i = 0; i < contents.Length; i++) {
				buffer[i] = (int)contents[i];
			}
		}


		public IntBuffer (int[] contents) : base(contents.Length)
		{
			buffer = contents;
		}


		public int[] Array ()
		{
			if (limit == capacity) {
				return (int[])buffer.Clone ();
			} else {
				int[] intArray = new int[limit];
				for (int i = 0; i < limit; i++) {
					intArray[i] = buffer[i];
				}
				return intArray;
			}
		}

		public int Get ()
		{
			return buffer[position++];
		}

		public int Get (int index)
		{
			if (!(index <= limit - 1)) {
				throw new Exception ("Index must be less than limit");
			}
			return buffer[index];
		}


		public void Get (ref int[] readBuffer)
		{
			if (!(position + readBuffer.Length <= limit)) {
				throw new Exception ("Not enought elements to read.");
			}
			for (int i = 0; i < readBuffer.Length; i++) {
				readBuffer[i] = buffer[position++];
			}
		}

		public virtual void Put (int intValue)
		{
			if (position >= limit) {
				throw new Exception ("One to many elements. Sad face.");
			}
			buffer[position++] = intValue;
		}

		public virtual void Put (int index, int intValue)
		{
			if (!(index <= capacity - 1)) {
				throw new Exception ("Index must be less than capacity");
			}
			if (index >= limit) {
				limit = index + 1;
			}
			if (index > position) {
				position = index + 1;
			}
			buffer[index] = intValue;
		}

		public virtual void Put (int[] intValues)
		{
			if ((position + intValues.Length) > capacity) {
				throw new Exception ("Buffer does not have the capacity for the addition.");
			}
			foreach (int intValue in intValues) {
				Put (intValue);
			}
		}

		public void DumpContents ()
		{
			Console.Write ("Buffer [ ");
			for (int i = 0; i < Limit; i++) {
				Console.Write(string.Format("{0} ", Get(i)));
			}
			Console.Write ("]");
		}
	}
}

