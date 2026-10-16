using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	[NativeHeader("Runtime/Export/Bootstrap/BootConfig.bindings.h")]
	internal class BootConfigData
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0000B1FB File Offset: 0x000093FB
		public void AddKey(string key)
		{
			this.Append(key, null);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000B208 File Offset: 0x00009408
		public string Get(string key)
		{
			return this.GetValue(key, 0);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000B224 File Offset: 0x00009424
		public string Get(string key, int index)
		{
			return this.GetValue(key, index);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000B240 File Offset: 0x00009440
		public unsafe void Append(string key, string value)
		{
			try
			{
				IntPtr intPtr = BootConfigData.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				BootConfigData.Append_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public unsafe void Set(string key, string value)
		{
			try
			{
				IntPtr intPtr = BootConfigData.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				BootConfigData.Set_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000B370 File Offset: 0x00009570
		private unsafe string GetValue(string key, int index)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = BootConfigData.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				BootConfigData.GetValue_Injected(intPtr, ref managedSpanWrapper, index, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[RequiredByNativeCode]
		private static BootConfigData WrapBootConfigData(IntPtr nativeHandle)
		{
			return new BootConfigData(nativeHandle);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000B3FC File Offset: 0x000095FC
		private BootConfigData(IntPtr nativeHandle)
		{
			bool flag = nativeHandle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("native handle can not be null");
			}
			this.m_Ptr = nativeHandle;
		}

		// Token: 0x06000629 RID: 1577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Append_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, ref ManagedSpanWrapper value);

		// Token: 0x0600062A RID: 1578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Set_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, ref ManagedSpanWrapper value);

		// Token: 0x0600062B RID: 1579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetValue_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, int index, out ManagedSpanWrapper ret);

		// Token: 0x040003B8 RID: 952
		private IntPtr m_Ptr;

		// Token: 0x02000117 RID: 279
		internal static class BindingsMarshaller
		{
			// Token: 0x0600062C RID: 1580 RVA: 0x0000B432 File Offset: 0x00009632
			public static IntPtr ConvertToNative(BootConfigData bootConfig)
			{
				return bootConfig.m_Ptr;
			}
		}
	}
}
