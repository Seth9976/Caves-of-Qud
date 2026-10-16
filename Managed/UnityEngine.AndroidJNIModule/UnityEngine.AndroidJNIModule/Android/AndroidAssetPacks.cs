using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Modules/AndroidJNI/Public/AndroidAssetPacksBindingsHelpers.h")]
	[StaticAccessor("AndroidAssetPacksBindingsHelpers", StaticAccessorType.DoubleColon)]
	public static class AndroidAssetPacks
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000AA08 File Offset: 0x00008C08
		public static bool coreUnityAssetPacksDownloaded
		{
			get
			{
				return AndroidAssetPacks.CoreUnityAssetPacksDownloaded();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000AA20 File Offset: 0x00008C20
		internal static string textureCompressionsPackName
		{
			get
			{
				return AndroidAssetPacks.GetTextureCompressionsPackName();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000AA38 File Offset: 0x00008C38
		internal static string dataPackName
		{
			get
			{
				return AndroidAssetPacks.GetDataPackName();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AA50 File Offset: 0x00008C50
		internal static string streamingAssetsPackName
		{
			get
			{
				return AndroidAssetPacks.GetStreamingAssetsPackName();
			}
		}

		// Token: 0x0600028C RID: 652
		[NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CoreUnityAssetPacksDownloaded();

		// Token: 0x0600028D RID: 653 RVA: 0x0000AA68 File Offset: 0x00008C68
		public static string[] GetCoreUnityAssetPackNames()
		{
			return new string[0];
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void GetAssetPackStateAsync(string[] assetPackNames, Action<ulong, AndroidAssetPackState[]> callback)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AA80 File Offset: 0x00008C80
		public static GetAssetPackStateAsyncOperation GetAssetPackStateAsync(string[] assetPackNames)
		{
			return null;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void DownloadAssetPackAsync(string[] assetPackNames, Action<AndroidAssetPackInfo> callback)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AA94 File Offset: 0x00008C94
		public static DownloadAssetPackAsyncOperation DownloadAssetPackAsync(string[] assetPackNames)
		{
			return null;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void RequestToUseMobileDataAsync(Action<AndroidAssetPackUseMobileDataRequestResult> callback)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public static RequestToUseMobileDataAsyncOperation RequestToUseMobileDataAsync()
		{
			return null;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AABC File Offset: 0x00008CBC
		public static string GetAssetPackPath(string assetPackName)
		{
			return "";
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void CancelAssetPackDownload(string[] assetPackNames)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void RemoveAssetPack(string assetPackName)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		private static string GetTextureCompressionsPackName()
		{
			return "UnityTextureCompressionsAssetPack";
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000AAEC File Offset: 0x00008CEC
		private static string GetDataPackName()
		{
			return "UnityDataAssetPack";
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AB04 File Offset: 0x00008D04
		private static string GetStreamingAssetsPackName()
		{
			return "UnityStreamingAssetsPack";
		}
	}
}
