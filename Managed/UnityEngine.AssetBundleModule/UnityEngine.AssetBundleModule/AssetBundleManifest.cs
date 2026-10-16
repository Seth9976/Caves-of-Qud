using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleManifest.h")]
	public class AssetBundleManifest : Object
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundleManifest()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E20 File Offset: 0x00001020
		[NativeMethod("GetAllAssetBundles")]
		public string[] GetAllAssetBundles()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundleManifest>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AssetBundleManifest.GetAllAssetBundles_Injected(intPtr);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E44 File Offset: 0x00001044
		[NativeMethod("GetAllAssetBundlesWithVariant")]
		public string[] GetAllAssetBundlesWithVariant()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundleManifest>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AssetBundleManifest.GetAllAssetBundlesWithVariant_Injected(intPtr);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E68 File Offset: 0x00001068
		[NativeMethod("GetAssetBundleHash")]
		public unsafe Hash128 GetAssetBundleHash(string assetBundleName)
		{
			Hash128 hash2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundleManifest>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Hash128 hash;
				AssetBundleManifest.GetAssetBundleHash_Injected(intPtr, ref managedSpanWrapper, out hash);
			}
			finally
			{
				char* ptr = null;
				Hash128 hash;
				hash2 = hash;
			}
			return hash2;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002ED4 File Offset: 0x000010D4
		[NativeMethod("GetDirectDependencies")]
		public unsafe string[] GetDirectDependencies(string assetBundleName)
		{
			string[] directDependencies_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundleManifest>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				directDependencies_Injected = AssetBundleManifest.GetDirectDependencies_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return directDependencies_Injected;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F3C File Offset: 0x0000113C
		[NativeMethod("GetAllDependencies")]
		public unsafe string[] GetAllDependencies(string assetBundleName)
		{
			string[] allDependencies_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundleManifest>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				allDependencies_Injected = AssetBundleManifest.GetAllDependencies_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return allDependencies_Injected;
		}

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetAllAssetBundles_Injected(IntPtr _unity_self);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetAllAssetBundlesWithVariant_Injected(IntPtr _unity_self);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAssetBundleHash_Injected(IntPtr _unity_self, ref ManagedSpanWrapper assetBundleName, out Hash128 ret);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetDirectDependencies_Injected(IntPtr _unity_self, ref ManagedSpanWrapper assetBundleName);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetAllDependencies_Injected(IntPtr _unity_self, ref ManagedSpanWrapper assetBundleName);
	}
}
