using System;

namespace Steamworks
{
	// Token: 0x02000186 RID: 390
	public static class SteamMusic
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0000AEFD File Offset: 0x000090FD
		public static bool BIsEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsEnabled(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000AF0E File Offset: 0x0000910E
		public static bool BIsPlaying()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsPlaying(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000AF1F File Offset: 0x0000911F
		public static AudioPlayback_Status GetPlaybackStatus()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetPlaybackStatus(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000AF30 File Offset: 0x00009130
		public static void Play()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Play(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000AF41 File Offset: 0x00009141
		public static void Pause()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Pause(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000AF52 File Offset: 0x00009152
		public static void PlayPrevious()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayPrevious(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000AF63 File Offset: 0x00009163
		public static void PlayNext()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayNext(CSteamAPIContext.GetSteamMusic());
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000AF74 File Offset: 0x00009174
		public static void SetVolume(float flVolume)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_SetVolume(CSteamAPIContext.GetSteamMusic(), flVolume);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000AF86 File Offset: 0x00009186
		public static float GetVolume()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetVolume(CSteamAPIContext.GetSteamMusic());
		}
	}
}
