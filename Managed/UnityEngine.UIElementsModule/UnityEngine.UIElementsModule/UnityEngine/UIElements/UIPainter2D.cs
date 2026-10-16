using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EF RID: 751
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIPainter2D.bindings.h")]
	internal static class UIPainter2D
	{
		// Token: 0x06001554 RID: 5460
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(bool computeBBox = false);

		// Token: 0x06001555 RID: 5461
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(IntPtr handle);

		// Token: 0x06001556 RID: 5462
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Reset(IntPtr handle);

		// Token: 0x06001557 RID: 5463
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLineWidth(IntPtr handle);

		// Token: 0x06001558 RID: 5464
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLineWidth(IntPtr handle, float value);

		// Token: 0x06001559 RID: 5465 RVA: 0x000557D4 File Offset: 0x000539D4
		public static Color GetStrokeColor(IntPtr handle)
		{
			Color color;
			UIPainter2D.GetStrokeColor_Injected(handle, out color);
			return color;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000557EC File Offset: 0x000539EC
		public static void SetStrokeColor(IntPtr handle, Color value)
		{
			UIPainter2D.SetStrokeColor_Injected(handle, ref value);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00055804 File Offset: 0x00053A04
		[NativeName("GetStrokeGradientCopy")]
		public static Gradient GetStrokeGradient(IntPtr handle)
		{
			IntPtr strokeGradient_Injected = UIPainter2D.GetStrokeGradient_Injected(handle);
			return (strokeGradient_Injected == 0) ? null : Gradient.BindingsMarshaller.ConvertToManaged(strokeGradient_Injected);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00055828 File Offset: 0x00053A28
		public static void SetStrokeGradient(IntPtr handle, Gradient gradient)
		{
			UIPainter2D.SetStrokeGradient_Injected(handle, (gradient == null) ? ((IntPtr)0) : Gradient.BindingsMarshaller.ConvertToNative(gradient));
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005584C File Offset: 0x00053A4C
		public static Color GetFillColor(IntPtr handle)
		{
			Color color;
			UIPainter2D.GetFillColor_Injected(handle, out color);
			return color;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00055864 File Offset: 0x00053A64
		public static void SetFillColor(IntPtr handle, Color value)
		{
			UIPainter2D.SetFillColor_Injected(handle, ref value);
		}

		// Token: 0x0600155F RID: 5471
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern LineJoin GetLineJoin(IntPtr handle);

		// Token: 0x06001560 RID: 5472
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLineJoin(IntPtr handle, LineJoin value);

		// Token: 0x06001561 RID: 5473
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern LineCap GetLineCap(IntPtr handle);

		// Token: 0x06001562 RID: 5474
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLineCap(IntPtr handle, LineCap value);

		// Token: 0x06001563 RID: 5475
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMiterLimit(IntPtr handle);

		// Token: 0x06001564 RID: 5476
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMiterLimit(IntPtr handle, float value);

		// Token: 0x06001565 RID: 5477
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginPath(IntPtr handle);

		// Token: 0x06001566 RID: 5478 RVA: 0x0005587C File Offset: 0x00053A7C
		public static void MoveTo(IntPtr handle, Vector2 pos)
		{
			UIPainter2D.MoveTo_Injected(handle, ref pos);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00055894 File Offset: 0x00053A94
		public static void LineTo(IntPtr handle, Vector2 pos)
		{
			UIPainter2D.LineTo_Injected(handle, ref pos);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000558AC File Offset: 0x00053AAC
		public static void ArcTo(IntPtr handle, Vector2 p1, Vector2 p2, float radius)
		{
			UIPainter2D.ArcTo_Injected(handle, ref p1, ref p2, radius);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000558C4 File Offset: 0x00053AC4
		public static void Arc(IntPtr handle, Vector2 center, float radius, float startAngleRads, float endAngleRads, ArcDirection direction)
		{
			UIPainter2D.Arc_Injected(handle, ref center, radius, startAngleRads, endAngleRads, direction);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000558E0 File Offset: 0x00053AE0
		public static void BezierCurveTo(IntPtr handle, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			UIPainter2D.BezierCurveTo_Injected(handle, ref p1, ref p2, ref p3);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000558FC File Offset: 0x00053AFC
		public static void QuadraticCurveTo(IntPtr handle, Vector2 p1, Vector2 p2)
		{
			UIPainter2D.QuadraticCurveTo_Injected(handle, ref p1, ref p2);
		}

		// Token: 0x0600156C RID: 5484
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClosePath(IntPtr handle);

		// Token: 0x0600156D RID: 5485 RVA: 0x00055914 File Offset: 0x00053B14
		public static Rect GetBBox(IntPtr handle)
		{
			Rect rect;
			UIPainter2D.GetBBox_Injected(handle, out rect);
			return rect;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005592C File Offset: 0x00053B2C
		public static MeshWriteDataInterface Stroke(IntPtr handle)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			UIPainter2D.Stroke_Injected(handle, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00055944 File Offset: 0x00053B44
		public static MeshWriteDataInterface Fill(IntPtr handle, FillRule fillRule)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			UIPainter2D.Fill_Injected(handle, fillRule, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x06001570 RID: 5488
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int TakeStrokeSnapshot(IntPtr handle);

		// Token: 0x06001571 RID: 5489
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int TakeFillSnapshot(IntPtr handle, FillRule fillRule);

		// Token: 0x06001572 RID: 5490
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearSnapshots(IntPtr handle);

		// Token: 0x06001573 RID: 5491 RVA: 0x0005595C File Offset: 0x00053B5C
		[ThreadSafe]
		public static MeshWriteDataInterface ExecuteSnapshotFromJob(IntPtr painterHandle, int i)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			UIPainter2D.ExecuteSnapshotFromJob_Injected(painterHandle, i, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x06001574 RID: 5492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStrokeColor_Injected(IntPtr handle, out Color ret);

		// Token: 0x06001575 RID: 5493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStrokeColor_Injected(IntPtr handle, [In] ref Color value);

		// Token: 0x06001576 RID: 5494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStrokeGradient_Injected(IntPtr handle);

		// Token: 0x06001577 RID: 5495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStrokeGradient_Injected(IntPtr handle, IntPtr gradient);

		// Token: 0x06001578 RID: 5496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFillColor_Injected(IntPtr handle, out Color ret);

		// Token: 0x06001579 RID: 5497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFillColor_Injected(IntPtr handle, [In] ref Color value);

		// Token: 0x0600157A RID: 5498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveTo_Injected(IntPtr handle, [In] ref Vector2 pos);

		// Token: 0x0600157B RID: 5499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LineTo_Injected(IntPtr handle, [In] ref Vector2 pos);

		// Token: 0x0600157C RID: 5500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ArcTo_Injected(IntPtr handle, [In] ref Vector2 p1, [In] ref Vector2 p2, float radius);

		// Token: 0x0600157D RID: 5501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Arc_Injected(IntPtr handle, [In] ref Vector2 center, float radius, float startAngleRads, float endAngleRads, ArcDirection direction);

		// Token: 0x0600157E RID: 5502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BezierCurveTo_Injected(IntPtr handle, [In] ref Vector2 p1, [In] ref Vector2 p2, [In] ref Vector2 p3);

		// Token: 0x0600157F RID: 5503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void QuadraticCurveTo_Injected(IntPtr handle, [In] ref Vector2 p1, [In] ref Vector2 p2);

		// Token: 0x06001580 RID: 5504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBBox_Injected(IntPtr handle, out Rect ret);

		// Token: 0x06001581 RID: 5505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stroke_Injected(IntPtr handle, out MeshWriteDataInterface ret);

		// Token: 0x06001582 RID: 5506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Fill_Injected(IntPtr handle, FillRule fillRule, out MeshWriteDataInterface ret);

		// Token: 0x06001583 RID: 5507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteSnapshotFromJob_Injected(IntPtr painterHandle, int i, out MeshWriteDataInterface ret);
	}
}
