using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Modules/IMGUI/GUIDebugger.bindings.h")]
	internal class GUIDebugger
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000890C File Offset: 0x00006B0C
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style)
		{
			GUIDebugger.LogLayoutEntry_Injected(ref rect, left, right, top, bottom, (style == null) ? ((IntPtr)0) : GUIStyle.BindingsMarshaller.ConvertToNative(style));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008938 File Offset: 0x00006B38
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutGroupEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical)
		{
			GUIDebugger.LogLayoutGroupEntry_Injected(ref rect, left, right, top, bottom, (style == null) ? ((IntPtr)0) : GUIStyle.BindingsMarshaller.ConvertToNative(style), isVertical);
		}

		// Token: 0x060001DA RID: 474
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[NativeMethod("LogEndGroup")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogLayoutEndGroup();

		// Token: 0x060001DB RID: 475 RVA: 0x00008964 File Offset: 0x00006B64
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		public unsafe static void LogBeginProperty(string targetTypeAssemblyQualifiedName, string path, Rect position)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(targetTypeAssemblyQualifiedName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = targetTypeAssemblyQualifiedName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = path.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				GUIDebugger.LogBeginProperty_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref position);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x060001DC RID: 476
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogEndProperty();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001DD RID: 477
		[NativeConditional("UNITY_EDITOR")]
		public static extern bool active
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutEntry_Injected([In] ref Rect rect, int left, int right, int top, int bottom, IntPtr style);

		// Token: 0x060001E0 RID: 480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutGroupEntry_Injected([In] ref Rect rect, int left, int right, int top, int bottom, IntPtr style, bool isVertical);

		// Token: 0x060001E1 RID: 481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogBeginProperty_Injected(ref ManagedSpanWrapper targetTypeAssemblyQualifiedName, ref ManagedSpanWrapper path, [In] ref Rect position);
	}
}
