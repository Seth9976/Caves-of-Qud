using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000225 RID: 549
	[NativeHeader("Runtime/Export/Networking/Ping.bindings.h")]
	public sealed class Ping
	{
		// Token: 0x06001DFE RID: 7678 RVA: 0x00039AD9 File Offset: 0x00037CD9
		public Ping(string address)
		{
			this.m_Ptr = Ping.Internal_Create(address);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00039AF0 File Offset: 0x00037CF0
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00039B20 File Offset: 0x00037D20
		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (!flag)
			{
				Ping.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06001E01 RID: 7681
		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06001E02 RID: 7682 RVA: 0x00039B5C File Offset: 0x00037D5C
		[FreeFunction("CreatePing")]
		private unsafe static IntPtr Internal_Create(string address)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(address, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = address.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = Ping.Internal_Create_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00039BB4 File Offset: 0x00037DB4
		public bool isDone
		{
			get
			{
				bool flag = this.m_Ptr == IntPtr.Zero;
				return !flag && this.Internal_IsDone();
			}
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00039BE4 File Offset: 0x00037DE4
		[NativeName("GetIsDone")]
		private bool Internal_IsDone()
		{
			IntPtr intPtr = Ping.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Ping.Internal_IsDone_Injected(intPtr);
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00039C08 File Offset: 0x00037E08
		public int time
		{
			get
			{
				IntPtr intPtr = Ping.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Ping.get_time_Injected(intPtr);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00039C2C File Offset: 0x00037E2C
		public string ip
		{
			[NativeName("GetIP")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Ping.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Ping.get_ip_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x06001E07 RID: 7687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create_Injected(ref ManagedSpanWrapper address);

		// Token: 0x06001E08 RID: 7688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsDone_Injected(IntPtr _unity_self);

		// Token: 0x06001E09 RID: 7689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_time_Injected(IntPtr _unity_self);

		// Token: 0x06001E0A RID: 7690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ip_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x040008F4 RID: 2292
		internal IntPtr m_Ptr;

		// Token: 0x02000226 RID: 550
		internal static class BindingsMarshaller
		{
			// Token: 0x06001E0B RID: 7691 RVA: 0x00039C6C File Offset: 0x00037E6C
			public static IntPtr ConvertToNative(Ping ping)
			{
				return ping.m_Ptr;
			}
		}
	}
}
