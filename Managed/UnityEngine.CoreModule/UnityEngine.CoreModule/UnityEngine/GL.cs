using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200016D RID: 365
	[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/GfxDevice.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Camera/CameraUtil.h")]
	public sealed class GL
	{
		// Token: 0x06000CF8 RID: 3320
		[NativeName("ImmediateVertex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00018ABC File Offset: 0x00016CBC
		public static void Vertex(Vector3 v)
		{
			GL.Vertex3(v.x, v.y, v.z);
		}

		// Token: 0x06000CFA RID: 3322
		[NativeName("ImmediateVertices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Vertices(Vector3* v, Vector3* coords, Vector4* colors, int length);

		// Token: 0x06000CFB RID: 3323
		[NativeName("ImmediateTexCoordAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x06000CFC RID: 3324 RVA: 0x00018AD7 File Offset: 0x00016CD7
		public static void TexCoord(Vector3 v)
		{
			GL.TexCoord3(v.x, v.y, v.z);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00018AF2 File Offset: 0x00016CF2
		public static void TexCoord2(float x, float y)
		{
			GL.TexCoord3(x, y, 0f);
		}

		// Token: 0x06000CFE RID: 3326
		[NativeName("ImmediateTexCoord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		// Token: 0x06000CFF RID: 3327 RVA: 0x00018B02 File Offset: 0x00016D02
		public static void MultiTexCoord(int unit, Vector3 v)
		{
			GL.MultiTexCoord3(unit, v.x, v.y, v.z);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00018B1E File Offset: 0x00016D1E
		public static void MultiTexCoord2(int unit, float x, float y)
		{
			GL.MultiTexCoord3(unit, x, y, 0f);
		}

		// Token: 0x06000D01 RID: 3329
		[NativeName("ImmediateColor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ImmediateColor(float r, float g, float b, float a);

		// Token: 0x06000D02 RID: 3330 RVA: 0x00018B2F File Offset: 0x00016D2F
		public static void Color(Color c)
		{
			GL.ImmediateColor(c.r, c.g, c.b, c.a);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000D03 RID: 3331
		// (set) Token: 0x06000D04 RID: 3332
		public static extern bool wireframe
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000D05 RID: 3333
		// (set) Token: 0x06000D06 RID: 3334
		public static extern bool sRGBWrite
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000D07 RID: 3335
		// (set) Token: 0x06000D08 RID: 3336
		[NativeProperty("UserBackfaceMode")]
		public static extern bool invertCulling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000D09 RID: 3337
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Flush();

		// Token: 0x06000D0A RID: 3338
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RenderTargetBarrier();

		// Token: 0x06000D0B RID: 3339 RVA: 0x00018B50 File Offset: 0x00016D50
		private static Matrix4x4 GetWorldViewMatrix()
		{
			Matrix4x4 matrix4x;
			GL.GetWorldViewMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00018B68 File Offset: 0x00016D68
		private static void SetViewMatrix(Matrix4x4 m)
		{
			GL.SetViewMatrix_Injected(ref m);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00018B7C File Offset: 0x00016D7C
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00018B93 File Offset: 0x00016D93
		public static Matrix4x4 modelview
		{
			get
			{
				return GL.GetWorldViewMatrix();
			}
			set
			{
				GL.SetViewMatrix(value);
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00018BA0 File Offset: 0x00016DA0
		[NativeName("SetWorldMatrix")]
		public static void MultMatrix(Matrix4x4 m)
		{
			GL.MultMatrix_Injected(ref m);
		}

		// Token: 0x06000D10 RID: 3344
		[Obsolete("IssuePluginEvent(eventID) is deprecated. Use IssuePluginEvent(callback, eventID) instead.", false)]
		[NativeName("InsertCustomMarker")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IssuePluginEvent(int eventID);

		// Token: 0x06000D11 RID: 3345
		[NativeName("SetUserBackfaceMode")]
		[Obsolete("SetRevertBackfacing(revertBackFaces) is deprecated. Use invertCulling property instead. (UnityUpgradable) -> invertCulling", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		// Token: 0x06000D12 RID: 3346
		[FreeFunction("GLPushMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		// Token: 0x06000D13 RID: 3347
		[FreeFunction("GLPopMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		// Token: 0x06000D14 RID: 3348
		[FreeFunction("GLLoadIdentityScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadIdentity();

		// Token: 0x06000D15 RID: 3349
		[FreeFunction("GLLoadOrthoScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		// Token: 0x06000D16 RID: 3350
		[FreeFunction("GLLoadPixelMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadPixelMatrix();

		// Token: 0x06000D17 RID: 3351 RVA: 0x00018BB4 File Offset: 0x00016DB4
		[FreeFunction("GLLoadProjectionMatrixScript")]
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.LoadProjectionMatrix_Injected(ref mat);
		}

		// Token: 0x06000D18 RID: 3352
		[FreeFunction("GLInvalidateState")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InvalidateState();

		// Token: 0x06000D19 RID: 3353 RVA: 0x00018BC8 File Offset: 0x00016DC8
		[FreeFunction("GLGetGPUProjectionMatrix")]
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			Matrix4x4 matrix4x;
			GL.GetGPUProjectionMatrix_Injected(ref proj, renderIntoTexture, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000D1A RID: 3354
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLLoadPixelMatrixScript(float left, float right, float bottom, float top);

		// Token: 0x06000D1B RID: 3355 RVA: 0x00018BE0 File Offset: 0x00016DE0
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.GLLoadPixelMatrixScript(left, right, bottom, top);
		}

		// Token: 0x06000D1C RID: 3356
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLIssuePluginEvent(IntPtr callback, int eventID);

		// Token: 0x06000D1D RID: 3357 RVA: 0x00018BF0 File Offset: 0x00016DF0
		public static void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.", "callback");
			}
			GL.GLIssuePluginEvent(callback, eventID);
		}

		// Token: 0x06000D1E RID: 3358
		[FreeFunction("GLBegin", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		// Token: 0x06000D1F RID: 3359
		[FreeFunction("GLEnd")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		// Token: 0x06000D20 RID: 3360 RVA: 0x00018C28 File Offset: 0x00016E28
		[FreeFunction]
		private static void GLClear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.GLClear_Injected(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00018C3F File Offset: 0x00016E3F
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00018C4C File Offset: 0x00016E4C
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, 1f);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00018C60 File Offset: 0x00016E60
		[FreeFunction("SetGLViewport")]
		public static void Viewport(Rect pixelRect)
		{
			GL.Viewport_Injected(ref pixelRect);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00018C74 File Offset: 0x00016E74
		[FreeFunction("ClearWithSkybox")]
		public static void ClearWithSkybox(bool clearDepth, Camera camera)
		{
			GL.ClearWithSkybox_Injected(clearDepth, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x06000D26 RID: 3366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldViewMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000D27 RID: 3367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewMatrix_Injected([In] ref Matrix4x4 m);

		// Token: 0x06000D28 RID: 3368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MultMatrix_Injected([In] ref Matrix4x4 m);

		// Token: 0x06000D29 RID: 3369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadProjectionMatrix_Injected([In] ref Matrix4x4 mat);

		// Token: 0x06000D2A RID: 3370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUProjectionMatrix_Injected([In] ref Matrix4x4 proj, bool renderIntoTexture, out Matrix4x4 ret);

		// Token: 0x06000D2B RID: 3371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLClear_Injected(bool clearDepth, bool clearColor, [In] ref Color backgroundColor, float depth);

		// Token: 0x06000D2C RID: 3372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Viewport_Injected([In] ref Rect pixelRect);

		// Token: 0x06000D2D RID: 3373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearWithSkybox_Injected(bool clearDepth, IntPtr camera);

		// Token: 0x040004AD RID: 1197
		public const int TRIANGLES = 4;

		// Token: 0x040004AE RID: 1198
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040004AF RID: 1199
		public const int QUADS = 7;

		// Token: 0x040004B0 RID: 1200
		public const int LINES = 1;

		// Token: 0x040004B1 RID: 1201
		public const int LINE_STRIP = 2;
	}
}
