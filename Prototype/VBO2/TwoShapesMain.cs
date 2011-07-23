using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VBO2
{
	/// <summary>
	/// Struct to hold our VBO data.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VBO {
		public int vertexBufferID;
		public int normalBufferID;
		public int indiciesBufferID;
		public int textureBufferID;
		public int elementCount;
	}
	
	class TwoShapesMain : GameWindow
	{	
		// Variables for the VBO handles
		VBO cube = new VBO();
		VBO pyramid = new VBO();
		
		// Variables for the textures
		int cubeTextureID = 0;
		int pyramidTextureID = 0;
		
		// Storage for the simple rotation angle to make it a bit more interesting
		double rotationAngle = 0;
		double randomSeed1 = 0;
		double randomSeed2 = 0;

		protected override void OnLoad (EventArgs e)
		{
			Title = "Two Shapes!";
			GL.ClearColor (Color.LightGray);
			
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
			
			// Vbo Data
			VBOData cubeData = new CubeVBOData();
			VBOData pyramidData = new PyramidVBOData();
			
			// Load the textures we are going to use
            LoadTexture("texture.jpg", out cubeTextureID);
			LoadTexture("pyramid.jpg", out pyramidTextureID);
			
			// Create the VBOs
			CreateVBO(ref cubeData, ref cube);
			CreateVBO(ref pyramidData, ref pyramid);
			
			// Set some random numbers for rendering different stuff in the background
			Random random = new Random();
			randomSeed1 = random.NextDouble();
			randomSeed2 = random.NextDouble();
		}
		
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
			rotationAngle += System.Math.PI / 4;
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
			OpenTK.Matrix4 perspective = OpenTK.Matrix4.CreatePerspectiveFieldOfView ((float)(System.Math.PI / 4f), (float)Width / Height, 1f, 600f);
			GL.LoadMatrix (ref perspective);
			
			// Draw the Cube
			GL.MatrixMode (MatrixMode.Modelview);
			GL.LoadIdentity ();
			GL.Translate (-2f, 0, -10f);
			GL.Rotate (rotationAngle, Vector3d.UnitY);
			
			DrawVBO(ref cube, cubeTextureID);
			
			// Draw the Pyramid
			GL.MatrixMode (MatrixMode.Modelview);
			GL.LoadIdentity ();
			GL.Translate (2f, 0, -10f);
			GL.Rotate (-rotationAngle, Vector3d.UnitY);
		
			DrawVBO(ref pyramid, pyramidTextureID);
			
			// Make a bit of a background of the shapes
			for (float i = -16; i <= 16; i=i+4) {
				for (float j = -8; j <= 8; j=j+4) {
					for (float z = -32; z >= -38; z = z-4) {
						GL.MatrixMode (MatrixMode.Modelview);
						GL.LoadIdentity ();
						GL.Translate (i, j, z);
						
						// Just a mess of x y and z to make it interesting.
						int psudoRand1 = (int) ((i/3f+j/3f)*z/3f/(i+1f) * randomSeed1 / randomSeed2+1) % 3;
						
						if(Math.Abs(psudoRand1) == 0) {
							GL.Rotate (-rotationAngle, Vector3d.UnitX);
						} else if(Math.Abs(psudoRand1) == 1) {
							GL.Rotate (rotationAngle, 6, 2, 1);
						} else if(Math.Abs(psudoRand1) == 2) {
							GL.Rotate (-rotationAngle, 3, 4, 5);
						}
						
						int psudoRand2 = (int) ((i/5f+j/psudoRand1+1f)*z*i*1337f/(i+2f) * randomSeed1 / randomSeed2+1) % 2;
						if(Math.Abs(psudoRand2) == 0) {
							DrawVBO(ref cube, cubeTextureID);
						} else if(Math.Abs(psudoRand2) == 1) {
							DrawVBO(ref pyramid, pyramidTextureID);
						}
					}
				}
			}
			
			
			SwapBuffers ();
		}
		
		public void CreateVBO(ref VBOData vboData, ref VBO vbo) {
			int bufferSize;
			
			// Normal Array Buffer
			if (vboData.NormalData != null) {
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.normalBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.normalBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(vboData.NormalData.Length * sizeof(float)), vboData.NormalData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.NormalData.Length * sizeof(float)!= bufferSize)
					throw new ApplicationException ("Normal array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// TexCoord Array Buffer
            if (vboData.TextureData != null)
            {
                // Generate Array Buffer Id
                GL.GenBuffers(1, out vbo.textureBufferID);

                // Bind current context to Array Buffer ID
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.textureBufferID);

                // Send data to buffer
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vboData.TextureData.Length * sizeof(float)), vboData.TextureData, BufferUsageHint.StaticDraw);

                // Validate that the buffer is the correct size
                GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
                if (vboData.TextureData.Length * sizeof(float) != bufferSize)
                    throw new ApplicationException("TexCoord array not uploaded correctly");

                // Clear the buffer Binding
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }

			// Vertex Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.vertexBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.vertexBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(vboData.VertexData.Length * sizeof(float)), vboData.VertexData, BufferUsageHint.DynamicDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.VertexData.Length * sizeof(float) != bufferSize)
					throw new ApplicationException ("Vertex array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}
			
			// Element Array Buffer
			{
				// Generate Array Buffer Id
				GL.GenBuffers (1, out vbo.indiciesBufferID);
				
				// Bind current context to Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, vbo.indiciesBufferID);
				
				// Send data to buffer
				GL.BufferData (BufferTarget.ElementArrayBuffer, (IntPtr)(vboData.IndicesData.Length * sizeof(int)), vboData.IndicesData, BufferUsageHint.StaticDraw);
				
				// Validate that the buffer is the correct size
				GL.GetBufferParameter (BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
				if (vboData.IndicesData.Length * sizeof(int) != bufferSize)
					throw new ApplicationException ("Element array not uploaded correctly");
				
				// Clear the buffer Binding
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, 0);
			}
			
			vbo.elementCount = vboData.IndicesData.Length;
		}
		
		public void DrawVBO (ref VBO vbo, int textureID) {
			if(vbo.vertexBufferID == 0)
				return;
			if(vbo.indiciesBufferID == 0)
				return;
			
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, textureID);
			
			// Texture Data Buffer Binding
			if(vbo.textureBufferID != 0)
            {
                // Bind to the Array Buffer ID
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.textureBufferID);

                // Set the Pointer to the current bound array describing how the data ia stored
                GL.TexCoordPointer(2, TexCoordPointerType.Float, sizeof(float)*2, IntPtr.Zero);

                // Enable the client state so it will use this array buffer pointer
				GL.EnableClientState(ArrayCap.TextureCoordArray);
            }
			
			// Vertex Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo.vertexBufferID);
				
				// Set the Pointer to the current bound array describing how the data ia stored
				GL.VertexPointer (3, VertexPointerType.Float, sizeof(float)*3, IntPtr.Zero);
				
				// Enable the client state so it will use this array buffer pointer
				GL.EnableClientState (ArrayCap.VertexArray);
			}
			
			// Element Array Buffer
			{
				// Bind to the Array Buffer ID
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, vbo.indiciesBufferID);
				
				// Draw the elements in the element array buffer
				// Draws up items in the Color, Vertex, TexCoordinate, and Normal Buffers using indices in the ElementArrayBuffer
				GL.DrawElements (BeginMode.Triangles, cube.elementCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
			}
			GL.Disable(EnableCap.Texture2D);
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
		
		public static void LoadTexture(string path, out int textureID) {
			GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            Bitmap bitmap = new Bitmap(path);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
            bitmap.UnlockBits(data);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public static void Main (string[] args)
		{
			using (TwoShapesMain p = new TwoShapesMain ()) {
				p.Run (60);
			}
		}
		
		
	}
	
	[Serializable]
	public class VBOData {
		public float[] VertexData;
		public float[] NormalData;
		public float[] TextureData;
		public uint[] IndicesData;
		
		public VBOData() {
			VertexData = null;
			NormalData = null;
			TextureData = null;
			IndicesData = null;
		}
	}
	
	public class CubeVBOData : VBOData {
			public CubeVBOData() {
				init();
			}
			
			public void init() {
				// Vertex Data
				VertexData = new float[] { 
					// Front face
					-1.0f, -1.0f, 1.0f, 
					1.0f, -1.0f, 1.0f, 
					1.0f, 1.0f, 1.0f, 
					-1.0f, 1.0f, 1.0f,
					// Right face
					1.0f, -1.0f, 1.0f, 
					1.0f, -1.0f, -1.0f, 
					1.0f, 1.0f, -1.0f, 
					1.0f, 1.0f, 1.0f,
					// Back face
					1.0f, -1.0f, -1.0f, 
					-1.0f, -1.0f, -1.0f, 
					-1.0f, 1.0f, -1.0f, 
					1.0f, 1.0f, -1.0f,
					// Left face
					-1.0f, -1.0f, -1.0f, 
					-1.0f, -1.0f, 1.0f, 
					-1.0f, 1.0f, 1.0f, 
					-1.0f, 1.0f, -1.0f,
					// Top Face	
					-1.0f, 1.0f, 1.0f, 
					1.0f, 1.0f, 1.0f,
					1.0f, 1.0f, -1.0f, 
					-1.0f, 1.0f, -1.0f,
					// Bottom Face
					1.0f, -1.0f, 1.0f, 
					-1.0f, -1.0f, 1.0f,
					-1.0f, -1.0f, -1.0f, 
					1.0f, -1.0f, -1.0f
				};
			
				// Normal Data for the Cube Verticies
				NormalData = new float[] {
					// Front face
					 0f, 0f, 1f, 
					 0f, 0f, 1f,
					 0f, 0f, 1f,
					 0f, 0f, 1f, 
					// Right face
					 1f, 0f, 0f, 
					 1f, 0f, 0f, 
					 1f, 0f, 0f, 
					 1f, 0f, 0f,
					// Back face
					 0f, 0f, -1f, 
					 0f, 0f, -1f, 
					 0f, 0f, -1f,  
					 0f, 0f, -1f, 
					// Left face
					 -1f, 0f, 0f,  
					 -1f, 0f, 0f, 
					 -1f, 0f, 0f,  
					 -1f, 0f, 0f,
					// Top Face	
					 0f, 1f, 0f,  
					 0f, 1f, 0f, 
					 0f, 1f, 0f,  
					 0f, 1f, 0f,
					// Bottom Face
					 0f, -1f, 0f,  
					 0f, -1f, 0f, 
					 0f, -1f, 0f,  
					 0f, -1f, 0f
				};
			
				// Texture Data for the Cube Verticies 
				TextureData = new float[] {
					// Font Face
	                0, 1,
	                1, 1,
	                1, 0,
	                0, 0,
					// Right Face
					0, 1,
	                1, 1,
	                1, 0,
	                0, 0,
					// Back Face
	                0, 1,
	                1, 1,
	                1, 0,
	                0, 0,
					// Left Face
					0, 1,
	                1, 1,
	                1, 0,
	                0, 0,
					// Top Face	
					0, 1,
	                1, 1,
	                1, 0,
	                0, 0,
					// Bottom Face
					0, 1,
	                1, 1,
	                1, 0,
	                0, 0
				};
					
				// Element Indices for the Cube
				IndicesData = new uint[] { 
					// Font face
					0, 1, 2, 2, 3, 0, 
					// Right face
					7, 6, 5, 5, 4, 7, 
					// Back face
					11, 10, 9, 9, 8, 11,
					// Left face
					15, 14, 13, 13, 12, 15, 
					// Top Face	
					19, 18, 17, 17, 16, 19,
					// Bottom Face
					23, 22, 21, 21, 20, 23,
				};
			}
		}
	
	public class PyramidVBOData : VBOData {
			public PyramidVBOData() {
				init();
			}
			
			public void init() {
				// Vertex Data
				VertexData = new float[] { 
					// Front face
					-1.0f, -1.0f, 1.0f, 
					1.0f, -1.0f, 1.0f, 
					0f, 1.0f, 0f, 
				
					// Right face
					1.0f, -1.0f, 1.0f, 
					1.0f, -1.0f, -1.0f, 
					0f, 1.0f, 0f, 
		
					// Back face
					1.0f, -1.0f, -1.0f, 
					-1.0f, -1.0f, -1.0f, 
					0f, 1.0f, 0f, 
				
					// Left face
					-1.0f, -1.0f, -1.0f, 
					-1.0f, -1.0f, 1.0f, 
					0f, 1.0f, 0f, 
					
					// Bottom Face
					1.0f, -1.0f, 1.0f, 
					-1.0f, -1.0f, 1.0f,
					-1.0f, -1.0f, -1.0f, 
					1.0f, -1.0f, -1.0f
				};
			
				// Normal Data for the Pyramid Verticies
				NormalData = new float[] {
					// Front face
					 0f, 0f, 1f, 
					 0f, 0f, 1f,
					 0f, 0f, 1f, 
					// Right face
					 1f, 0f, 0f, 
					 1f, 0f, 0f, 
					 1f, 0f, 0f,
					// Back face
					 0f, 0f, -1f, 
					 0f, 0f, -1f, 
					 0f, 0f, -1f, 
					// Left face
					 -1f, 0f, 0f,  
					 -1f, 0f, 0f, 
					 -1f, 0f, 0f, 
					// Bottom Face
					 0f, -1f, 0f,  
					 0f, -1f, 0f, 
					 0f, -1f, 0f,  
					 0f, -1f, 0f
				};
			
				// Texture Data for the Pyramid Verticies 
				TextureData = new float[] {
					// Font Face
	                0, 1,
	                0.5f, 1,
	                0.25f, 0,
					// Right Face
	                0, 1,
	                0.5f, 1,
	                0.25f, 0,
					// Back Face
	                0, 1,
	                0.5f, 1,
	                0.25f, 0, 
					// Left Face
	                0, 1,
	                0.5f, 1,
	                0.25f, 0,
					// Bottom Face
					0.5f, 1f,
	                1f, 1f,
	                1f, 0f,
					0.5f, 0f
				};
					
				// Element Indices for the Pyramid
				IndicesData = new uint[] { 
					// Front face
					0, 1, 2, 
					// Right face
					3, 4, 5, 
					// Back face
					6, 7, 8, 
					// Left face
					9, 10, 11, 
					// Bottom Face
					15, 14, 13, 13, 12, 15,
				};
			}
		}
}