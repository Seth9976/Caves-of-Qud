using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	[NativeHeader("Modules/IMGUI/GUIClip.h")]
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal sealed class GUIClip
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000184 RID: 388
		internal static extern bool enabled
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000080F4 File Offset: 0x000062F4
		internal static Rect visibleRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
			get
			{
				Rect rect;
				GUIClip.get_visibleRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000810C File Offset: 0x0000630C
		internal static Rect topmostRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopMostPhysicalRect")]
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				Rect rect;
				GUIClip.get_topmostRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008124 File Offset: 0x00006324
		internal static void Internal_Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push_Injected(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x06000188 RID: 392
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Pop();

		// Token: 0x06000189 RID: 393
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetCount();

		// Token: 0x0600018A RID: 394 RVA: 0x00008140 File Offset: 0x00006340
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopRect")]
		internal static Rect GetTopRect()
		{
			Rect rect;
			GUIClip.GetTopRect_Injected(out rect);
			return rect;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008158 File Offset: 0x00006358
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Vector2 Unclip_Vector2(Vector2 pos)
		{
			Vector2 vector;
			GUIClip.Unclip_Vector2_Injected(ref pos, out vector);
			return vector;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008170 File Offset: 0x00006370
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Rect Unclip_Rect(Rect rect)
		{
			Rect rect2;
			GUIClip.Unclip_Rect_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008188 File Offset: 0x00006388
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Vector2 Clip_Vector2(Vector2 absolutePos)
		{
			Vector2 vector;
			GUIClip.Clip_Vector2_Injected(ref absolutePos, out vector);
			return vector;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000081A0 File Offset: 0x000063A0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Rect Internal_Clip_Rect(Rect absoluteRect)
		{
			Rect rect;
			GUIClip.Internal_Clip_Rect_Injected(ref absoluteRect, out rect);
			return rect;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000081B8 File Offset: 0x000063B8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Vector2 UnclipToWindow_Vector2(Vector2 pos)
		{
			Vector2 vector;
			GUIClip.UnclipToWindow_Vector2_Injected(ref pos, out vector);
			return vector;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000081D0 File Offset: 0x000063D0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Rect UnclipToWindow_Rect(Rect rect)
		{
			Rect rect2;
			GUIClip.UnclipToWindow_Rect_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000081E8 File Offset: 0x000063E8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Vector2 ClipToWindow_Vector2(Vector2 absolutePos)
		{
			Vector2 vector;
			GUIClip.ClipToWindow_Vector2_Injected(ref absolutePos, out vector);
			return vector;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008200 File Offset: 0x00006400
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Rect ClipToWindow_Rect(Rect absoluteRect)
		{
			Rect rect;
			GUIClip.ClipToWindow_Rect_Injected(ref absoluteRect, out rect);
			return rect;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008218 File Offset: 0x00006418
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetAbsoluteMousePosition")]
		private static Vector2 Internal_GetAbsoluteMousePosition()
		{
			Vector2 vector;
			GUIClip.Internal_GetAbsoluteMousePosition_Injected(out vector);
			return vector;
		}

		// Token: 0x06000194 RID: 404
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reapply();

		// Token: 0x06000195 RID: 405 RVA: 0x00008230 File Offset: 0x00006430
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 matrix4x;
			GUIClip.GetMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008248 File Offset: 0x00006448
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.SetMatrix_Injected(ref m);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000825C File Offset: 0x0000645C
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetParentTransform")]
		internal static Matrix4x4 GetParentMatrix()
		{
			Matrix4x4 matrix4x;
			GUIClip.GetParentMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008271 File Offset: 0x00006471
		internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip(objectTransform, objectTransform, clipRect);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008280 File Offset: 0x00006480
		internal static void Internal_PushParentClip(Matrix4x4 renderTransform, Matrix4x4 inputTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip_Injected(ref renderTransform, ref inputTransform, ref clipRect);
		}

		// Token: 0x0600019A RID: 410
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopParentClip();

		// Token: 0x0600019B RID: 411 RVA: 0x00008298 File Offset: 0x00006498
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push(screenRect, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000082A5 File Offset: 0x000064A5
		internal static void Pop()
		{
			GUIClip.Internal_Pop();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000082B0 File Offset: 0x000064B0
		public static Vector2 Unclip(Vector2 pos)
		{
			return GUIClip.Unclip_Vector2(pos);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000082C8 File Offset: 0x000064C8
		public static Rect Unclip(Rect rect)
		{
			return GUIClip.Unclip_Rect(rect);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000082E0 File Offset: 0x000064E0
		public static Vector2 Clip(Vector2 absolutePos)
		{
			return GUIClip.Clip_Vector2(absolutePos);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000082F8 File Offset: 0x000064F8
		public static Rect Clip(Rect absoluteRect)
		{
			return GUIClip.Internal_Clip_Rect(absoluteRect);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008310 File Offset: 0x00006510
		public static Vector2 UnclipToWindow(Vector2 pos)
		{
			return GUIClip.UnclipToWindow_Vector2(pos);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008328 File Offset: 0x00006528
		public static Rect UnclipToWindow(Rect rect)
		{
			return GUIClip.UnclipToWindow_Rect(rect);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008340 File Offset: 0x00006540
		public static Vector2 ClipToWindow(Vector2 absolutePos)
		{
			return GUIClip.ClipToWindow_Vector2(absolutePos);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008358 File Offset: 0x00006558
		public static Rect ClipToWindow(Rect absoluteRect)
		{
			return GUIClip.ClipToWindow_Rect(absoluteRect);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008370 File Offset: 0x00006570
		public static Vector2 GetAbsoluteMousePosition()
		{
			return GUIClip.Internal_GetAbsoluteMousePosition();
		}

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_visibleRect_Injected(out Rect ret);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_topmostRect_Injected(out Rect ret);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Push_Injected([In] ref Rect screenRect, [In] ref Vector2 scrollOffset, [In] ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTopRect_Injected(out Rect ret);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Vector2_Injected([In] ref Vector2 pos, out Vector2 ret);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Rect_Injected([In] ref Rect rect, out Rect ret);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clip_Vector2_Injected([In] ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Clip_Rect_Injected([In] ref Rect absoluteRect, out Rect ret);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Vector2_Injected([In] ref Vector2 pos, out Vector2 ret);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Rect_Injected([In] ref Rect rect, out Rect ret);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Vector2_Injected([In] ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Rect_Injected([In] ref Rect absoluteRect, out Rect ret);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAbsoluteMousePosition_Injected(out Vector2 ret);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected([In] ref Matrix4x4 m);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParentMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_PushParentClip_Injected([In] ref Matrix4x4 renderTransform, [In] ref Matrix4x4 inputTransform, [In] ref Rect clipRect);

		// Token: 0x02000014 RID: 20
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
		internal struct ParentClipScope : IDisposable
		{
			// Token: 0x060001B8 RID: 440 RVA: 0x00008387 File Offset: 0x00006587
			public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
			{
				this.m_Disposed = false;
				GUIClip.Internal_PushParentClip(objectTransform, clipRect);
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000839C File Offset: 0x0000659C
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUIClip.Internal_PopParentClip();
				}
			}

			// Token: 0x0400006E RID: 110
			private bool m_Disposed;
		}
	}
}
