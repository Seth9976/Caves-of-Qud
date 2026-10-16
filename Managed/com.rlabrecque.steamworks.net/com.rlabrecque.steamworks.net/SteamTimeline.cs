using System;

namespace Steamworks
{
	// Token: 0x02000190 RID: 400
	public static class SteamTimeline
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public static void SetTimelineStateDescription(string pchDescription, float flTimeDelta)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				NativeMethods.ISteamTimeline_SetTimelineStateDescription(CSteamAPIContext.GetSteamTimeline(), utf8StringHandle, flTimeDelta);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public static void ClearTimelineStateDescription(float flTimeDelta)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamTimeline_ClearTimelineStateDescription(CSteamAPIContext.GetSteamTimeline(), flTimeDelta);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0000CA24 File Offset: 0x0000AC24
		public static void AddTimelineEvent(string pchIcon, string pchTitle, string pchDescription, uint unPriority, float flStartOffsetSeconds, float flDurationSeconds, ETimelineEventClipPriority ePossibleClip)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchIcon))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchTitle))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchDescription))
					{
						NativeMethods.ISteamTimeline_AddTimelineEvent(CSteamAPIContext.GetSteamTimeline(), utf8StringHandle, utf8StringHandle2, utf8StringHandle3, unPriority, flStartOffsetSeconds, flDurationSeconds, ePossibleClip);
					}
				}
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public static void SetTimelineGameMode(ETimelineGameMode eMode)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamTimeline_SetTimelineGameMode(CSteamAPIContext.GetSteamTimeline(), eMode);
		}
	}
}
