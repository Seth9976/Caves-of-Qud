using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006B RID: 107
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class Sockaddr
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000B5BF File Offset: 0x000097BF
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000B5C7 File Offset: 0x000097C7
		public UnixAddressFamily sa_family
		{
			get
			{
				return this._sa_family;
			}
			set
			{
				this._sa_family = value;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000B5D0 File Offset: 0x000097D0
		public Sockaddr()
		{
			this.type = SockaddrType.Sockaddr;
			this.sa_family = UnixAddressFamily.AF_UNSPEC;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000B5E6 File Offset: 0x000097E6
		internal Sockaddr(SockaddrType type, UnixAddressFamily sa_family)
		{
			this.type = type;
			this.sa_family = sa_family;
		}

		// Token: 0x0600045B RID: 1115
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Sockaddr_GetNativeSize", SetLastError = true)]
		private unsafe static extern int GetNativeSize(_SockaddrHeader* address, out long size);

		// Token: 0x0600045C RID: 1116 RVA: 0x0000B5FC File Offset: 0x000097FC
		internal unsafe long GetNativeSize()
		{
			long num;
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(this).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] array;
				byte* ptr3;
				if ((array = Sockaddr.GetDynamicData(this)) == null || array.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &array[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(this, ptr3, false);
				if (Sockaddr.GetNativeSize(Sockaddr.GetNative(&sockaddrDynamic, ptr2), out num) != 0)
				{
					throw new ArgumentException("Failed to get size of native struct", "this");
				}
				array = null;
			}
			return num;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000B66B File Offset: 0x0000986B
		internal static Sockaddr GetAddress(Sockaddr address)
		{
			if (address == null)
			{
				return Sockaddr.nullSockaddr;
			}
			return address;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000B678 File Offset: 0x00009878
		internal unsafe static _SockaddrHeader* GetNative(_SockaddrDynamic* dyn, SockaddrType* addr)
		{
			if (dyn->data != null)
			{
				return (_SockaddrHeader*)dyn;
			}
			fixed (SockaddrType* ptr = &Sockaddr.nullSockaddr.type)
			{
				SockaddrType* ptr2 = ptr;
				if (addr == ptr2)
				{
					return null;
				}
			}
			return (_SockaddrHeader*)addr;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000B6AA File Offset: 0x000098AA
		internal static byte[] GetDynamicData(Sockaddr addr)
		{
			if (addr == null)
			{
				return null;
			}
			return addr.DynamicData();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000B6B7 File Offset: 0x000098B7
		internal virtual byte[] DynamicData()
		{
			return null;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000B6BA File Offset: 0x000098BA
		internal virtual long GetDynamicLength()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000B6C1 File Offset: 0x000098C1
		internal virtual void SetDynamicLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B6C8 File Offset: 0x000098C8
		public SockaddrStorage ToSockaddrStorage()
		{
			SockaddrStorage sockaddrStorage = new SockaddrStorage((int)this.GetNativeSize());
			sockaddrStorage.SetTo(this);
			return sockaddrStorage;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public static Sockaddr FromSockaddrStorage(SockaddrStorage storage)
		{
			Sockaddr sockaddr = new Sockaddr();
			storage.CopyTo(sockaddr);
			return sockaddr;
		}

		// Token: 0x0400048D RID: 1165
		internal SockaddrType type;

		// Token: 0x0400048E RID: 1166
		internal UnixAddressFamily _sa_family;

		// Token: 0x0400048F RID: 1167
		private static Sockaddr nullSockaddr = new Sockaddr();
	}
}
