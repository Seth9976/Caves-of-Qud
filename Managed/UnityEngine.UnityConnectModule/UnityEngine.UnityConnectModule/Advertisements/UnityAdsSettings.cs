using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Advertisements
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityConnect/UnityAds/UnityAdsSettings.h")]
	internal static class UnityAdsSettings
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20
		// (set) Token: 0x06000015 RID: 21
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool enabled
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021E8 File Offset: 0x000003E8
		[Obsolete("warning No longer supported and will always return true")]
		public static bool IsPlatformEnabled(RuntimePlatform platform)
		{
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021FB File Offset: 0x000003FB
		[Obsolete("warning No longer supported and will do nothing")]
		public static void SetPlatformEnabled(RuntimePlatform platform, bool value)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24
		// (set) Token: 0x06000019 RID: 25
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool initializeOnStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26
		// (set) Token: 0x0600001B RID: 27
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool testMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002200 File Offset: 0x00000400
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static string GetGameId(RuntimePlatform platform)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				UnityAdsSettings.GetGameId_Injected(platform, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002230 File Offset: 0x00000430
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public unsafe static void SetGameId(RuntimePlatform platform, string gameId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(gameId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = gameId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UnityAdsSettings.SetGameId_Injected(platform, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGameId_Injected(RuntimePlatform platform, out ManagedSpanWrapper ret);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGameId_Injected(RuntimePlatform platform, ref ManagedSpanWrapper gameId);
	}
}
