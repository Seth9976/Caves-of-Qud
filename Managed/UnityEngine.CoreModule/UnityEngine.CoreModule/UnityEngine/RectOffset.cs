using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	[UsedByNativeCode]
	[NativeHeader("Modules/IMGUI/GUIStyle.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RectOffset : IFormattable
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x00013AA5 File Offset: 0x00011CA5
		public RectOffset()
		{
			this.m_Ptr = RectOffset.InternalCreate();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00013ABA File Offset: 0x00011CBA
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal RectOffset(object sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00013AD4 File Offset: 0x00011CD4
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Destroy();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00013B14 File Offset: 0x00011D14
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.m_Ptr = RectOffset.InternalCreate();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00013B4C File Offset: 0x00011D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00013B68 File Offset: 0x00011D68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00013B84 File Offset: 0x00011D84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left.ToString(format, formatProvider),
				this.right.ToString(format, formatProvider),
				this.top.ToString(format, formatProvider),
				this.bottom.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00013C08 File Offset: 0x00011E08
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				RectOffset.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000A80 RID: 2688
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate();

		// Token: 0x06000A81 RID: 2689
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00013C44 File Offset: 0x00011E44
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00013C68 File Offset: 0x00011E68
		[NativeProperty("left", false, TargetType.Field)]
		public int left
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_left_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_left_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00013C8C File Offset: 0x00011E8C
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00013CB0 File Offset: 0x00011EB0
		[NativeProperty("right", false, TargetType.Field)]
		public int right
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_right_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_right_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00013CD4 File Offset: 0x00011ED4
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00013CF8 File Offset: 0x00011EF8
		[NativeProperty("top", false, TargetType.Field)]
		public int top
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_top_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_top_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00013D1C File Offset: 0x00011F1C
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00013D40 File Offset: 0x00011F40
		[NativeProperty("bottom", false, TargetType.Field)]
		public int bottom
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_bottom_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_bottom_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00013D64 File Offset: 0x00011F64
		public int horizontal
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_horizontal_Injected(intPtr);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00013D88 File Offset: 0x00011F88
		public int vertical
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_vertical_Injected(intPtr);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00013DAC File Offset: 0x00011FAC
		public Rect Add(Rect rect)
		{
			IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rect rect2;
			RectOffset.Add_Injected(intPtr, ref rect, out rect2);
			return rect2;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public Rect Remove(Rect rect)
		{
			IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rect rect2;
			RectOffset.Remove_Injected(intPtr, ref rect, out rect2);
			return rect2;
		}

		// Token: 0x06000A8E RID: 2702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_left_Injected(IntPtr _unity_self);

		// Token: 0x06000A8F RID: 2703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_left_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000A90 RID: 2704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_right_Injected(IntPtr _unity_self);

		// Token: 0x06000A91 RID: 2705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_right_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000A92 RID: 2706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_top_Injected(IntPtr _unity_self);

		// Token: 0x06000A93 RID: 2707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_top_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000A94 RID: 2708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_bottom_Injected(IntPtr _unity_self);

		// Token: 0x06000A95 RID: 2709
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bottom_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000A96 RID: 2710
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_horizontal_Injected(IntPtr _unity_self);

		// Token: 0x06000A97 RID: 2711
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertical_Injected(IntPtr _unity_self);

		// Token: 0x06000A98 RID: 2712
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add_Injected(IntPtr _unity_self, [In] ref Rect rect, out Rect ret);

		// Token: 0x06000A99 RID: 2713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Remove_Injected(IntPtr _unity_self, [In] ref Rect rect, out Rect ret);

		// Token: 0x0400047C RID: 1148
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400047D RID: 1149
		private readonly object m_SourceStyle;

		// Token: 0x02000155 RID: 341
		internal static class BindingsMarshaller
		{
			// Token: 0x06000A9A RID: 2714 RVA: 0x00013DFB File Offset: 0x00011FFB
			public static IntPtr ConvertToNative(RectOffset rectOffset)
			{
				return rectOffset.m_Ptr;
			}
		}
	}
}
