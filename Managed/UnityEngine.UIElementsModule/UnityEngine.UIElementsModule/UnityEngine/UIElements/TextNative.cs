using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002FB RID: 763
	[NativeHeader("Modules/UIElements/Core/Native/TextNative.bindings.h")]
	internal static class TextNative
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x00055B2C File Offset: 0x00053D2C
		public static Vector2 GetCursorPosition(TextNativeSettings settings, Rect rect, int cursorIndex)
		{
			bool flag = settings.font == null;
			Vector2 vector;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				vector = Vector2.zero;
			}
			else
			{
				vector = TextNative.DoGetCursorPosition(settings, rect, cursorIndex);
			}
			return vector;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00055B6C File Offset: 0x00053D6C
		public static float ComputeTextWidth(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float num;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				num = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					num = 0f;
				}
				else
				{
					num = TextNative.DoComputeTextWidth(settings);
				}
			}
			return num;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00055BC0 File Offset: 0x00053DC0
		public static float ComputeTextHeight(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float num;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				num = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					num = 0f;
				}
				else
				{
					num = TextNative.DoComputeTextHeight(settings);
				}
			}
			return num;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00055C14 File Offset: 0x00053E14
		public static NativeArray<TextVertex> GetVertices(TextNativeSettings settings)
		{
			int num = 0;
			TextNative.GetVertices(settings, IntPtr.Zero, UnsafeUtility.SizeOf<TextVertex>(), ref num);
			NativeArray<TextVertex> nativeArray = new NativeArray<TextVertex>(num, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			bool flag = num > 0;
			if (flag)
			{
				TextNative.GetVertices(settings, (IntPtr)nativeArray.GetUnsafePtr<TextVertex>(), UnsafeUtility.SizeOf<TextVertex>(), ref num);
				Debug.Assert(num == nativeArray.Length);
			}
			return nativeArray;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00055C7C File Offset: 0x00053E7C
		public static Vector2 GetOffset(TextNativeSettings settings, Rect screenRect)
		{
			bool flag = settings.font == null;
			Vector2 vector;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				vector = new Vector2(0f, 0f);
			}
			else
			{
				settings.text = settings.text ?? "";
				vector = TextNative.DoGetOffset(settings, screenRect);
			}
			return vector;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00055CDC File Offset: 0x00053EDC
		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00055D3C File Offset: 0x00053F3C
		[FreeFunction(Name = "TextNative::ComputeTextWidth")]
		private static float DoComputeTextWidth(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextWidth_Injected(ref settings);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00055D50 File Offset: 0x00053F50
		[FreeFunction(Name = "TextNative::ComputeTextHeight")]
		private static float DoComputeTextHeight(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextHeight_Injected(ref settings);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00055D64 File Offset: 0x00053F64
		[FreeFunction(Name = "TextNative::GetCursorPosition")]
		private static Vector2 DoGetCursorPosition(TextNativeSettings settings, Rect rect, int cursorPosition)
		{
			Vector2 vector;
			TextNative.DoGetCursorPosition_Injected(ref settings, ref rect, cursorPosition, out vector);
			return vector;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00055D80 File Offset: 0x00053F80
		[FreeFunction(Name = "TextNative::GetVertices")]
		private static void GetVertices(TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount)
		{
			TextNative.GetVertices_Injected(ref settings, buffer, vertexSize, ref vertexCount);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00055D98 File Offset: 0x00053F98
		[FreeFunction(Name = "TextNative::GetOffset")]
		private static Vector2 DoGetOffset(TextNativeSettings settings, Rect rect)
		{
			Vector2 vector;
			TextNative.DoGetOffset_Injected(ref settings, ref rect, out vector);
			return vector;
		}

		// Token: 0x0600159D RID: 5533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextWidth_Injected([In] ref TextNativeSettings settings);

		// Token: 0x0600159E RID: 5534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextHeight_Injected([In] ref TextNativeSettings settings);

		// Token: 0x0600159F RID: 5535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetCursorPosition_Injected([In] ref TextNativeSettings settings, [In] ref Rect rect, int cursorPosition, out Vector2 ret);

		// Token: 0x060015A0 RID: 5536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVertices_Injected([In] ref TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount);

		// Token: 0x060015A1 RID: 5537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetOffset_Injected([In] ref TextNativeSettings settings, [In] ref Rect rect, out Vector2 ret);
	}
}
