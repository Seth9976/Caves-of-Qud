using System;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000027 RID: 39
	public static class Utilities
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002558 File Offset: 0x00000758
		public static Platform GetCurrentPlatform()
		{
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.IPhonePlayer)
			{
				if (platform == RuntimePlatform.OSXPlayer)
				{
					return Platform.MacOS;
				}
				if (platform != RuntimePlatform.WindowsPlayer)
				{
					if (platform == RuntimePlatform.IPhonePlayer)
					{
						return Platform.iOS;
					}
				}
			}
			else
			{
				if (platform == RuntimePlatform.Android)
				{
					return Platform.Android;
				}
				if (platform == RuntimePlatform.PS4)
				{
					return Platform.PS4;
				}
				switch (platform)
				{
				case RuntimePlatform.Switch:
					return Platform.Switch;
				case RuntimePlatform.GameCoreXboxSeries:
					return Platform.GameCoreXboxSeries;
				case RuntimePlatform.GameCoreXboxOne:
					return Platform.GameCoreXboxOne;
				case RuntimePlatform.PS5:
					return Platform.PS5;
				}
			}
			return Platform.Windows;
		}

		// Token: 0x04000053 RID: 83
		public const string DELAY_SHUTDOWN_OFF_MAIN_THREAD_DEBUG_MSG = "Platform IO:: Calls which write to SaveData must be called from the main Unity thread (aka Qud UI thread). This is because you must notify the console on its main thread to not shutdown during a save operation or risk corruption. If necessary game-code can circumvent this restriction by: 1) using DelayShutdownScope on the main thread 2) make the I/O calls on any thread 3) then disposing DelayShutdownScope.";
	}
}
