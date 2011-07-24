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
	public abstract class Buffer {
		
		/// <summary>
		/// Capacity for the buffer
		/// </summary>
		protected int capacity;
		
		/// <summary>
		/// The element that is the maximum to be read
		/// </summary>
		protected int limit;
		
		/// <summary>
		/// The position of the next element to be read or written
		/// </summary>
		protected int position;
		
		/// <summary>
		/// The mark for the buffer
		/// </summary>
		protected int mark;
		
		protected bool readOnly;
		
		public Buffer(): this(10) {
			
		}
		
		public Buffer(int capacity) {
			this.capacity = capacity;
			limit = capacity;
			position = 0;
			mark = 0;
			readOnly = false;
		}
		
		public int Capacity {
			get { return capacity; }	
		}
		
		public int Limit {
			get { return limit; }
			set { 
				if(value <= capacity) {
					limit = value; 	
				} else {
					throw new Exception("Limit must be <= capacity");
				}
			}
		}
		
		public int Position {
			get { return position; }	
			set {
				if(value <= capacity && value <= limit) {
					position = value; 	
				} else {
					throw new Exception("Position must be <= capacity & limit");
				}
			}
		}
		
		public int Mark {
			get { return mark; }	
			set { 
				if(value <= capacity && value <= limit && value <= position) {
					mark = value; 	
				} else {
					throw new Exception("Position must be <= capacity & limit & position");
				} 
			}
		}
		
		public Buffer Clear() {
			limit = capacity;
			position = 0;
			return this;
		}
		
		public void CreateMark() {
			mark = position;	
		}
		
		public Buffer Flip() {
			limit = position;
			position = 0;
			return this;
		}
		
		public Buffer Flop() {
			position = limit;
			limit = capacity;
			return this;
		}
		
		public bool HasRemaining() {
			return position < limit;
		}
	
		public bool HasRemaining(int blockSize) {
			return (position+blockSize) < limit;
		}
		
		public Buffer Reset() {
			position = mark;
			return this;
		}
		
		public int Remaining() {
			return limit-position;
		}
		
		public Buffer Rewind() {
			mark = 0;
			position = 0;
			return this;
		}
		
		public override string ToString ()
		{
			return string.Format ("[Buffer: Capacity={0}, Limit={1}, Position={2}, Mark={3}]", Capacity, Limit, Position, Mark);
		}
	}
	
}

