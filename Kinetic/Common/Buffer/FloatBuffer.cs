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
	public class FloatBuffer: Buffer
	{
		protected float[] buffer;
		
		public FloatBuffer (int capacity) : base(capacity) {
			buffer = new float[capacity];
		}
		
		public FloatBuffer (float[] contents) : base(contents.Length) {
			buffer = contents;
		}
		
		public float[] Array() {
			if(limit == capacity) {
				return (float[]) buffer.Clone();
			} else {
				float[] floatArray = new float[limit];
				for(int i=0;i<limit;i++) {
					floatArray[i] = buffer[i];
				}
				return floatArray;
			}
		}
		
		public float @Get() {
			return buffer[position++];
		}
		
		public float @Get(int index) {
			if(!(index <= limit-1)) {
				throw new Exception("Index must be less than limit");	
			}
			return buffer[index];
		}
		
		
		public void @Get(ref float[] readBuffer) {
			if(!(position+readBuffer.Length <= limit)) {
				throw new Exception(string.Format("Not enought elements to read. ({0}+{1} <= {2})", position, readBuffer.Length, limit));	
			}
			for(int i=0;i<readBuffer.Length;i++) {
				readBuffer[i] = buffer[position++];
			}
		}
		
		
		public virtual void @Put(float floatValue) {
			if(position >= limit) {
				throw new Exception("One to many elements. Sad face.");
			}
			buffer[position++] = floatValue;
		}
		
		public virtual void @Put(int index, float floatValue) {
			if(!(index <= capacity-1)) {
				throw new Exception("Index must be less than capacity");	
			}
			if(index >= limit) {
				limit = index+1;	
			}
			if(index > position) {
				position = index+1;	
			}
			buffer[index] = floatValue;
		}
		
		public virtual void @Put(float[] floatValues) {
			if((position + floatValues.Length) > capacity) {
				throw new Exception("Buffer does not have the capacity for the addition.");
			}
			foreach (float floatValue in floatValues) {
				Put(floatValue);
			}
		}
		
		public static FloatBuffer Allocate (int capacity) {
			return new FloatBuffer(capacity);
		}
		
		public void DumpContents() {
			Console.Write("[Buffer [ ");
			for (int i=0;i<Limit;i++) {
				Console.Write(string.Format("{0}f ", Get(i)));
			}
			Console.WriteLine("]]");
		}
		
	}
}

