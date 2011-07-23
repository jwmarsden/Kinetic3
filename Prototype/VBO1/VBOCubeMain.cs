using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VBO1
{
	/// <summary>
	/// Struct to hold the Vector Data. You can use the standard Vector3 struct
	/// that comes with OpenTK. 
	/// 
	/// If your app supports other graphics libraries then it may be appropriate to
	/// have your own vector storage.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2f : IEquatable<Vector2f>
	{
		public float x, y;

		public static readonly int ByteSize = Marshal.SizeOf (new Vector2f ());

		public Vector2f (float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public bool Equals (Vector2f other)
		{
			return x == other.x && y == other.y;
		}
	}
	
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3f : IEquatable<Vector3f>
	{
		public float x, y, z;

		public static readonly int ByteSize = Marshal.SizeOf (new Vector3f ());

		public Vector3f (float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public bool Equals (Vector3f other)
		{
			return x == other.x && y == other.y && z == other.z;
		}
	}

	class VBOCubeMain : GameWindow
	{
		// Variables for the cube data
		Vector3f[] cubeVertexData = null;
		Vector3f[] cubeNormalData = null;
		int[] colorData = null;
		uint[] indicesVboData = null;
		
		// Variables for the graphics card handles
		int colorBufferID;
		int normalBufferID;
		int vertexBufferID;
		int indiciesBufferID;
		
		// Count of tri elements to be drawn
		int elementCount = 0;
		
		// Storage for the simple rotation angle to make it a bit more interesting
		double rotationAngle = 0;

		protected override void OnLoad (EventArgs e)
		{
			Title = "VBO Cube";
			GL.ClearColor (Color.DarkBlue);
			
			// Vertex Data
			cubeVertexData = new Vector3f[] { 
				new Vector3f (-1.0f, -1.0f, 1.0f), 
				new Vector3f (1.0f, -1.0f, 1.0f), 
				new Vector3f (1.0f, 1.0f, 1.0f), 
				new Vector3f (-1.0f, 1.0f, 1.0f), 
				new Vector3f (-1.0f, -1.0f, -1.0f), 
				new Vector3f (1.0f, -1.0f, -1.0f), 
				new Vector3f (1.0f, 1.0f, -1.0f), 
				new Vector3f (-1.0f, 1.0f, -1.0f) 
			};
			
			// Normal Data for the Cube Verticies
			cubeNormalData = new Vector3f[] { 
				new Vector3f (-1.0f, -1.0f, 1.0f), 
				new Vector3f (1.0f, -1.0f, 1.0f), 
				new Vector3f (1.0f, 1.0f, 1.0f), 
				new Vector3f (-1.0f, 1.0f, 1.0f), 
				new Vector3f (-1.0f, -1.0f, -1.0f), 
				new Vector3f (1.0f, -1.0f, -1.0f), 
				new Vector3f (1.0f, 1.0f, -1.0f), 
				new Vector3f (-1.0f, 1.0f, -1.0f) 
			};
			
			// Color Data for the Cube Verticies
			colorData = new int[] { 
				ColorToRgba32 (Color.Cyan), 
				ColorToRgba32 (Color.Cyan), 
				ColorToRgba32 (Color.DarkCyan), 
				ColorToRgba32 (Color.DarkCyan), 
				ColorToRgba32 (Color.Cyan), 
				ColorToRgba32 (Color.Cyan), 
				ColorToRgba32 (Color.DarkCyan), 
				ColorToRgba32 (Color.DarkCyan) 
			};
			
			// Element Indices for the Cube
			indicesVboData = new uint[] { 
				0, 1, 2, 2, 3, 0, 
				3, 2, 6, 6, 7, 3, 
				7, 6, 5, 5, 4, 7, 
				4, 0, 3, 3, 7, 4, 
				0, 1, 5, 5, 4, 0,
				1, 5, 6, 6, 2, 1 
			};
			
			int bufferSize;
			
			// Color Array Buffer
			if (colorData != null) {
				// Generate Array Buffer Id
				GL.GenBuffers (1, out colorBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, colorBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(colorData.Length * sizeof(int)), colorData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				
				if (colorData.Length * sizeof(int) != bufferSize)
					throw new ApplicationException ("Vertex array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// Normal Array Buffer
			if (cubeNormalData != null) {
				// Generate Array Buffer Id
				GL.GenBuffers (1, out normalBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, normalBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(cubeNormalData.Length * Vector3f.ByteSize), cubeNormalData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (cubeNormalData.Length * Vector3f.ByteSize != bufferSize)
					throw new ApplicationException ("Normal array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// Vertex Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vertexBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vertexBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(cubeVertexData.Length * Vector3f.ByteSize), cubeVertexData, BufferUsageHint.DynamicDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (cubeVertexData.Length * Vector3f.ByteSize != bufferSize)
					throw new ApplicationException ("Vertex array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// Element Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out indiciesBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, indiciesBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ElementArrayBuffer, (IntPtr)(indicesVboData.Length * sizeof(int)), indicesVboData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (indicesVboData.Length * sizeof(int) != bufferSize)
					throw new ApplicationException ("Element array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, 0);
			}
			
			elementCount = indicesVboData.Length;
		}
		
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
			rotationAngle += System.Math.PI / 16;
		}
		
		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			
			// Clear the buffers
			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			
			// Enable depth testing so our cube draws correctly
			GL.Enable (EnableCap.DepthTest);
			
			// Set the viewport
			GL.Viewport (0, 0, Width, Height);
			
			// Load a perspective matrix view
			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadIdentity ();
			OpenTK.Matrix4 perspective = OpenTK.Matrix4.CreatePerspectiveFieldOfView ((float)(System.Math.PI / 4f), (float)Width / Height, 1f, 200f);
			GL.LoadMatrix (ref perspective);
			
			// Translate a little into the z-axis so we can see the cube
			GL.Translate (0, 0, -5f);
			
			// Rotate by the current angle
			GL.Rotate (rotationAngle, Vector3d.UnitY);
			
			if (vertexBufferID == 0)
				return;
			if (indiciesBufferID == 0)
				return;
			
			// Color Array Buffer (Colors not used when lighting is enabled)
			if (colorBufferID != 0) {
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, colorBufferID);
				
				// Set the Pointer to the current bound array describing how the data ia stored
				GL.ColorPointer (4, ColorPointerType.UnsignedByte, sizeof(int), IntPtr.Zero);
				
				// Enable the client state so it will use this array buffer pointer
				GL.EnableClientState (ArrayCap.ColorArray);
			}
			
			// Vertex Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vertexBufferID);
				
				// Set the Pointer to the current bound array describing how the data ia stored
				GL.VertexPointer (3, VertexPointerType.Float, Vector3f.ByteSize, IntPtr.Zero);
				
				// Enable the client state so it will use this array buffer pointer
				GL.EnableClientState (ArrayCap.VertexArray);
			}
			
			// Element Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, indiciesBufferID);
				
				// Draw the elements in the element array buffer
				// Draws up items in the Color, Vertex, TexCoordinate, and Normal Buffers using indices in the ElementArrayBuffer
				GL.DrawElements (BeginMode.Triangles, elementCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
			}

			SwapBuffers ();
		}
		
		/// <summary>
		/// Converts a Color instance into an int representation
		/// </summary>
		/// <param name="c">
		/// A <see cref="Color"/> instance to be converted
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public static int ColorToRgba32 (Color c)
		{
			return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
		}

		public static void Main (string[] args)
		{
			using (VBOCubeMain p = new VBOCubeMain ()) {
				p.Run (60);
			}
		}
	}
}