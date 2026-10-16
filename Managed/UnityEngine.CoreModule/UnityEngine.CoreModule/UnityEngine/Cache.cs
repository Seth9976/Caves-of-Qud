using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000119 RID: 281
	[NativeHeader("Runtime/Misc/Cache.h")]
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	public struct Cache : IEquatable<Cache>
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000B494 File Offset: 0x00009694
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000B4AC File Offset: 0x000096AC
		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000B510 File Offset: 0x00009710
		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000B53C File Offset: 0x0000973C
		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000B560 File Offset: 0x00009760
		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		// Token: 0x06000639 RID: 1593
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsValid(int handle);

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000B580 File Offset: 0x00009780
		public bool ready
		{
			get
			{
				return Cache.Cache_IsReady(this.m_Handle);
			}
		}

		// Token: 0x0600063B RID: 1595
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReady(int handle);

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public bool readOnly
		{
			get
			{
				return Cache.Cache_IsReadonly(this.m_Handle);
			}
		}

		// Token: 0x0600063D RID: 1597
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReadonly(int handle);

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000B5E0 File Offset: 0x000097E0
		[NativeThrows]
		internal static string Cache_GetPath(int handle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Cache.Cache_GetPath_Injected(handle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000B610 File Offset: 0x00009810
		public int index
		{
			get
			{
				return Cache.Cache_GetIndex(this.m_Handle);
			}
		}

		// Token: 0x06000641 RID: 1601
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetIndex(int handle);

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000B630 File Offset: 0x00009830
		public long spaceFree
		{
			get
			{
				return Cache.Cache_GetSpaceFree(this.m_Handle);
			}
		}

		// Token: 0x06000643 RID: 1603
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetSpaceFree(int handle);

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000B650 File Offset: 0x00009850
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000B66D File Offset: 0x0000986D
		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache.Cache_GetMaximumDiskSpaceAvailable(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		// Token: 0x06000646 RID: 1606
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		// Token: 0x06000647 RID: 1607
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000B680 File Offset: 0x00009880
		public long spaceOccupied
		{
			get
			{
				return Cache.Cache_GetCachingDiskSpaceUsed(this.m_Handle);
			}
		}

		// Token: 0x06000649 RID: 1609
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0000B6BD File Offset: 0x000098BD
		public int expirationDelay
		{
			get
			{
				return Cache.Cache_GetExpirationDelay(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		// Token: 0x0600064C RID: 1612
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetExpirationDelay(int handle);

		// Token: 0x0600064D RID: 1613
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		// Token: 0x0600064E RID: 1614 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public bool ClearCache()
		{
			return Cache.Cache_ClearCache(this.m_Handle);
		}

		// Token: 0x0600064F RID: 1615
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache(int handle);

		// Token: 0x06000650 RID: 1616 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public bool ClearCache(int expiration)
		{
			return Cache.Cache_ClearCache_Expiration(this.m_Handle, expiration);
		}

		// Token: 0x06000651 RID: 1617
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);

		// Token: 0x06000652 RID: 1618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cache_GetPath_Injected(int handle, out ManagedSpanWrapper ret);

		// Token: 0x040003BB RID: 955
		private int m_Handle;
	}
}
