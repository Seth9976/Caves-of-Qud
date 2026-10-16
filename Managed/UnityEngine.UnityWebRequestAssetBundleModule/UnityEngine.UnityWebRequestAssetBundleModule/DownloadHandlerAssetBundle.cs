using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityWebRequestAssetBundle/Public/DownloadHandlerAssetBundle.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAssetBundle : DownloadHandler
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021E4 File Offset: 0x000003E4
		private unsafe static IntPtr Create([Unmarshalled] DownloadHandlerAssetBundle obj, string url, uint crc)
		{
			IntPtr intPtr;
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
				intPtr = DownloadHandlerAssetBundle.Create_Injected(obj, ref managedSpanWrapper, crc);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000223C File Offset: 0x0000043C
		private unsafe static IntPtr CreateCached([Unmarshalled] DownloadHandlerAssetBundle obj, string url, string name, Hash128 hash, uint crc)
		{
			IntPtr intPtr;
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
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = name.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				intPtr = DownloadHandlerAssetBundle.CreateCached_Injected(obj, ref managedSpanWrapper, ref managedSpanWrapper2, ref hash, crc);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return intPtr;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022CC File Offset: 0x000004CC
		private void InternalCreateAssetBundle(string url, uint crc)
		{
			this.m_Ptr = DownloadHandlerAssetBundle.Create(this, url, crc);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022DD File Offset: 0x000004DD
		private void InternalCreateAssetBundleCached(string url, string name, Hash128 hash, uint crc)
		{
			this.m_Ptr = DownloadHandlerAssetBundle.CreateCached(this, url, name, hash, crc);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022F1 File Offset: 0x000004F1
		public DownloadHandlerAssetBundle(string url, uint crc)
		{
			this.InternalCreateAssetBundle(url, crc);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002304 File Offset: 0x00000504
		public DownloadHandlerAssetBundle(string url, uint version, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, "", new Hash128(0U, 0U, 0U, version), crc);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002325 File Offset: 0x00000525
		public DownloadHandlerAssetBundle(string url, Hash128 hash, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, "", hash, crc);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000233E File Offset: 0x0000053E
		public DownloadHandlerAssetBundle(string url, string name, Hash128 hash, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, name, hash, crc);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002354 File Offset: 0x00000554
		public DownloadHandlerAssetBundle(string url, CachedAssetBundle cachedBundle, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, cachedBundle.name, cachedBundle.hash, crc);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002375 File Offset: 0x00000575
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported for asset bundles");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002382 File Offset: 0x00000582
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for asset bundles");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002390 File Offset: 0x00000590
		public AssetBundle assetBundle
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAssetBundle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AssetBundle>(DownloadHandlerAssetBundle.get_assetBundle_Injected(intPtr));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023DC File Offset: 0x000005DC
		public bool autoLoadAssetBundle
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAssetBundle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DownloadHandlerAssetBundle.get_autoLoadAssetBundle_Injected(intPtr);
			}
			[NativeThrows]
			set
			{
				IntPtr intPtr = DownloadHandlerAssetBundle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DownloadHandlerAssetBundle.set_autoLoadAssetBundle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002400 File Offset: 0x00000600
		public bool isDownloadComplete
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAssetBundle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DownloadHandlerAssetBundle.get_isDownloadComplete_Injected(intPtr);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002424 File Offset: 0x00000624
		public static AssetBundle GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAssetBundle>(www).assetBundle;
		}

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerAssetBundle obj, ref ManagedSpanWrapper url, uint crc);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateCached_Injected(DownloadHandlerAssetBundle obj, ref ManagedSpanWrapper url, ref ManagedSpanWrapper name, [In] ref Hash128 hash, uint crc);

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_assetBundle_Injected(IntPtr _unity_self);

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoLoadAssetBundle_Injected(IntPtr _unity_self);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoLoadAssetBundle_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isDownloadComplete_Injected(IntPtr _unity_self);

		// Token: 0x02000004 RID: 4
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00002441 File Offset: 0x00000641
			public static IntPtr ConvertToNative(DownloadHandlerAssetBundle handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
