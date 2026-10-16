using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityAction.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class AccessibilityAction : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AccessibilityAction()
		{
			this.m_Ptr = AccessibilityAction.Internal_Create(this);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000266
		public AccessibilityAction(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002078 File Offset: 0x00000278
		~AccessibilityAction()
		{
			this.Dispose(false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AC File Offset: 0x000002AC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				AccessibilityAction.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create([Unmarshalled] AccessibilityAction self);

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		public int id
		{
			get
			{
				IntPtr intPtr = AccessibilityAction.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AccessibilityAction.get_id_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AccessibilityAction.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AccessibilityAction.set_id_Injected(intPtr, value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002184 File Offset: 0x00000384
		public unsafe string label
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = AccessibilityAction.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					AccessibilityAction.get_label_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = AccessibilityAction.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					AccessibilityAction.set_label_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021E8 File Offset: 0x000003E8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021F0 File Offset: 0x000003F0
		public Func<bool> activated { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021FC File Offset: 0x000003FC
		[RequiredByNativeCode]
		private bool Internal_InvokeActivated()
		{
			return this.activated != null && this.activated();
		}

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_id_Injected(IntPtr _unity_self);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_id_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_label_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_label_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x04000001 RID: 1
		private IntPtr m_Ptr;

		// Token: 0x02000003 RID: 3
		internal static class BindingsMarshaller
		{
			// Token: 0x06000013 RID: 19 RVA: 0x00002224 File Offset: 0x00000424
			public static IntPtr ConvertToNative(AccessibilityAction obj)
			{
				return obj.m_Ptr;
			}

			// Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
			public static AccessibilityAction ConvertToManaged(IntPtr ptr)
			{
				return new AccessibilityAction(ptr);
			}
		}
	}
}
