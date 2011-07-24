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
	public class DynamicFloatBuffer: FloatBuffer
	{
		protected int growthFactor;
		protected int growthCount;
		
		public DynamicFloatBuffer (int capacity) : base(capacity) {
			growthFactor = 30;
			growthCount = 0;
		}
		
		public DynamicFloatBuffer (float[] contents) : base(contents.Length) {
			growthFactor = 30;
			growthCount = 0;
		}
		
		public override void @Put(float floatValue) {
			if(position >= limit) {
				ResizeInternalBuffer();
			}
			buffer[position++] = floatValue;
		}
		
		public override void @Put(int index, float floatValue) {
			if(!(index <= capacity-1)) {
				ResizeInternalBuffer();	
			}
			if(index >= limit) {
				limit = index+1;	
			}
			if(index > position) {
				position = index+1;	
			}
			buffer[index] = floatValue;
		}
		
		public override void @Put(float[] floatValues) {
			if((position + floatValues.Length) > capacity) {
				ResizeInternalBuffer();
			}
			foreach (float floatValue in floatValues) {
				Put(floatValue);
			}
		}
		
		protected virtual void ResizeInternalBuffer() {
			growthCount = growthCount + 1;
			int newCapacity = capacity + (growthFactor * growthCount);
			float[] tmpBuffer = new float[newCapacity];
			for(int i=0;i<position;i++) {
				tmpBuffer[i] = buffer[i];
			}
			capacity = newCapacity;
			limit = newCapacity;
			buffer = tmpBuffer;
		}
	}
}

