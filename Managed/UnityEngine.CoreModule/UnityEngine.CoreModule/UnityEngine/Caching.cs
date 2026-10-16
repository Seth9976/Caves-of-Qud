using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	[NativeHeader("Runtime/Misc/CachingManager.h")]
	[StaticAccessor("GetCachingManager()", StaticAccessorType.Dot)]
	public sealed class Caching
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000653 RID: 1619
		// (set) Token: 0x06000654 RID: 1620
		public static extern bool compressionEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000655 RID: 1621
		public static extern bool ready
		{
			[NativeName("GetIsReady")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000656 RID: 1622
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ClearCache();

		// Token: 0x06000657 RID: 1623 RVA: 0x0000B710 File Offset: 0x00009910
		public static bool ClearCache(int expiration)
		{
			return Caching.ClearCache_Int(expiration);
		}

		// Token: 0x06000658 RID: 1624
		[NativeName("ClearCache")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ClearCache_Int(int expiration);

		// Token: 0x06000659 RID: 1625 RVA: 0x0000B728 File Offset: 0x00009928
		public static bool ClearCachedVersion(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersionInternal(assetBundleName, hash);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000B758 File Offset: 0x00009958
		[NativeName("ClearCachedVersion")]
		internal unsafe static bool ClearCachedVersionInternal(string assetBundleName, Hash128 hash)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Caching.ClearCachedVersionInternal_Injected(ref managedSpanWrapper, ref hash);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public static bool ClearOtherCachedVersions(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, hash, true);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public static bool ClearAllCachedVersions(string assetBundleName)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, default(Hash128), false);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000B818 File Offset: 0x00009A18
		internal unsafe static bool ClearCachedVersions(string assetBundleName, Hash128 hash, bool keepInputVersion)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Caching.ClearCachedVersions_Injected(ref managedSpanWrapper, ref hash, keepInputVersion);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000B874 File Offset: 0x00009A74
		internal unsafe static Hash128[] GetCachedVersions(string assetBundleName)
		{
			Hash128[] array2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Caching.GetCachedVersions_Injected(ref managedSpanWrapper, out blittableArrayWrapper);
			}
			finally
			{
				char* ptr = null;
				BlittableArrayWrapper blittableArrayWrapper;
				Hash128[] array;
				blittableArrayWrapper.Unmarshal<Hash128>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public static void GetCachedVersions(string assetBundleName, List<Hash128> outCachedVersions)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			bool flag2 = outCachedVersions == null;
			if (flag2)
			{
				throw new ArgumentNullException("Input outCachedVersions cannot be null.");
			}
			outCachedVersions.AddRange(Caching.GetCachedVersions(assetBundleName));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000B920 File Offset: 0x00009B20
		[Obsolete("Please use IsVersionCached with Hash128 instead.")]
		public static bool IsVersionCached(string url, int version)
		{
			return Caching.IsVersionCached(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000B944 File Offset: 0x00009B44
		public static bool IsVersionCached(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.IsVersionCached(url, "", hash);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000B978 File Offset: 0x00009B78
		public static bool IsVersionCached(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.IsVersionCached("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		[NativeName("IsCached")]
		internal unsafe static bool IsVersionCached(string url, string assetBundleName, Hash128 hash)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = assetBundleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = Caching.IsVersionCached_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref hash);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000BA4C File Offset: 0x00009C4C
		[Obsolete("Please use MarkAsUsed with Hash128 instead.")]
		public static bool MarkAsUsed(string url, int version)
		{
			return Caching.MarkAsUsed(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000BA70 File Offset: 0x00009C70
		public static bool MarkAsUsed(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.MarkAsUsed(url, "", hash);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public static bool MarkAsUsed(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.MarkAsUsed("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000BAEC File Offset: 0x00009CEC
		internal unsafe static bool MarkAsUsed(string url, string assetBundleName, Hash128 hash)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = assetBundleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = Caching.MarkAsUsed_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref hash);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000BB78 File Offset: 0x00009D78
		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		public static int GetVersionFromCache(string url)
		{
			return -1;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000BB8C File Offset: 0x00009D8C
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceUsed
		{
			get
			{
				return (int)Caching.spaceOccupied;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600066A RID: 1642
		[Obsolete("This property is only used for the current cache, use Cache.spaceOccupied to get used bytes per cache.")]
		public static extern long spaceOccupied
		{
			[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[NativeName("GetCachingDiskSpaceUsed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceAvailable
		{
			get
			{
				return (int)Caching.spaceFree;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600066C RID: 1644
		[Obsolete("This property is only used for the current cache, use Cache.spaceFree to get unused bytes per cache.")]
		public static extern long spaceFree
		{
			[NativeName("GetCachingDiskSpaceFree")]
			[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600066D RID: 1645
		// (set) Token: 0x0600066E RID: 1646
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		[Obsolete("This property is only used for the current cache, use Cache.maximumAvailableStorageSpace to access the maximum available storage space per cache.")]
		public static extern long maximumAvailableDiskSpace
		{
			[NativeName("GetMaximumDiskSpaceAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetMaximumDiskSpaceAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600066F RID: 1647
		// (set) Token: 0x06000670 RID: 1648
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		[Obsolete("This property is only used for the current cache, use Cache.expirationDelay to access the expiration delay per cache.")]
		public static extern int expirationDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static Cache AddCache(string cachePath)
		{
			bool flag = string.IsNullOrEmpty(cachePath);
			if (flag)
			{
				throw new ArgumentNullException("Cache path cannot be null or empty.");
			}
			bool flag2 = false;
			bool flag3 = cachePath.Replace('\\', '/').StartsWith(Application.streamingAssetsPath);
			if (flag3)
			{
				flag2 = true;
			}
			else
			{
				bool flag4 = !Directory.Exists(cachePath);
				if (flag4)
				{
					throw new ArgumentException("Cache path '" + cachePath + "' doesn't exist.");
				}
				bool flag5 = (File.GetAttributes(cachePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
				if (flag5)
				{
					flag2 = true;
				}
			}
			bool valid = Caching.GetCacheByPath(cachePath).valid;
			if (valid)
			{
				throw new InvalidOperationException("Cache with path '" + cachePath + "' has already been added.");
			}
			return Caching.AddCache(cachePath, flag2);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000BC70 File Offset: 0x00009E70
		[NativeName("AddCachePath")]
		internal unsafe static Cache AddCache(string cachePath, bool isReadonly)
		{
			Cache cache2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(cachePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = cachePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Cache cache;
				Caching.AddCache_Injected(ref managedSpanWrapper, isReadonly, out cache);
			}
			finally
			{
				char* ptr = null;
				Cache cache;
				cache2 = cache;
			}
			return cache2;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000BCCC File Offset: 0x00009ECC
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_GetCacheHandleAt")]
		[NativeThrows]
		public static Cache GetCacheAt(int cacheIndex)
		{
			Cache cache;
			Caching.GetCacheAt_Injected(cacheIndex, out cache);
			return cache;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_GetCacheHandleByPath")]
		[NativeThrows]
		public unsafe static Cache GetCacheByPath(string cachePath)
		{
			Cache cache2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(cachePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = cachePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Cache cache;
				Caching.GetCacheByPath_Injected(ref managedSpanWrapper, out cache);
			}
			finally
			{
				char* ptr = null;
				Cache cache;
				cache2 = cache;
			}
			return cache2;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000BD40 File Offset: 0x00009F40
		public static void GetAllCachePaths(List<string> cachePaths)
		{
			cachePaths.Clear();
			for (int i = 0; i < Caching.cacheCount; i++)
			{
				cachePaths.Add(Caching.GetCacheAt(i).path);
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000BD80 File Offset: 0x00009F80
		[NativeName("Caching_RemoveCacheByHandle")]
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static bool RemoveCache(Cache cache)
		{
			return Caching.RemoveCache_Injected(ref cache);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000BD94 File Offset: 0x00009F94
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_MoveCacheBeforeByHandle")]
		public static void MoveCacheBefore(Cache src, Cache dst)
		{
			Caching.MoveCacheBefore_Injected(ref src, ref dst);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000BDAC File Offset: 0x00009FAC
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_MoveCacheAfterByHandle")]
		public static void MoveCacheAfter(Cache src, Cache dst)
		{
			Caching.MoveCacheAfter_Injected(ref src, ref dst);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000679 RID: 1657
		public static extern int cacheCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache defaultCache
		{
			[NativeName("Caching_GetDefaultCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_defaultCache_Injected(out cache);
				return cache;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000BDDC File Offset: 0x00009FDC
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache currentCacheForWriting
		{
			[NativeName("Caching_GetCurrentCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_currentCacheForWriting_Injected(out cache);
				return cache;
			}
			[NativeThrows]
			[NativeName("Caching_SetCurrentCacheByHandle")]
			set
			{
				Caching.set_currentCacheForWriting_Injected(ref value);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000BE08 File Offset: 0x0000A008
		[Obsolete("This function is obsolete. Please use ClearCache.  (UnityUpgradable) -> ClearCache()")]
		public static bool CleanCache()
		{
			return Caching.ClearCache();
		}

		// Token: 0x0600067F RID: 1663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersionInternal_Injected(ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash);

		// Token: 0x06000680 RID: 1664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersions_Injected(ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash, bool keepInputVersion);

		// Token: 0x06000681 RID: 1665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCachedVersions_Injected(ref ManagedSpanWrapper assetBundleName, out BlittableArrayWrapper ret);

		// Token: 0x06000682 RID: 1666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsVersionCached_Injected(ref ManagedSpanWrapper url, ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash);

		// Token: 0x06000683 RID: 1667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool MarkAsUsed_Injected(ref ManagedSpanWrapper url, ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash);

		// Token: 0x06000684 RID: 1668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCache_Injected(ref ManagedSpanWrapper cachePath, bool isReadonly, out Cache ret);

		// Token: 0x06000685 RID: 1669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheAt_Injected(int cacheIndex, out Cache ret);

		// Token: 0x06000686 RID: 1670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheByPath_Injected(ref ManagedSpanWrapper cachePath, out Cache ret);

		// Token: 0x06000687 RID: 1671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveCache_Injected([In] ref Cache cache);

		// Token: 0x06000688 RID: 1672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheBefore_Injected([In] ref Cache src, [In] ref Cache dst);

		// Token: 0x06000689 RID: 1673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheAfter_Injected([In] ref Cache src, [In] ref Cache dst);

		// Token: 0x0600068A RID: 1674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultCache_Injected(out Cache ret);

		// Token: 0x0600068B RID: 1675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentCacheForWriting_Injected(out Cache ret);

		// Token: 0x0600068C RID: 1676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_currentCacheForWriting_Injected([In] ref Cache value);
	}
}
