using System;

namespace XRL.Core
{
	// Token: 0x02000633 RID: 1587
	public static class Globals
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x00157AB0 File Offset: 0x00155CB0
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x00157AC0 File Offset: 0x00155CC0
		public static bool EnableMetrics
		{
			get
			{
				return !Globals.ForceMetricsOff && Globals._EnableMetrics;
			}
			set
			{
				Globals._EnableMetrics = value;
			}
		}

		// Token: 0x04002091 RID: 8337
		public static RenderModeType RenderMode = RenderModeType.Tiles;

		// Token: 0x04002092 RID: 8338
		public static bool ForceMetricsOff = false;

		// Token: 0x04002093 RID: 8339
		public static bool _EnableMetrics = true;

		// Token: 0x04002094 RID: 8340
		public static bool EnableSound = false;

		// Token: 0x04002095 RID: 8341
		public static bool EnableMusic = false;

		// Token: 0x04002096 RID: 8342
		public static bool EnableAmbient = false;

		// Token: 0x04002097 RID: 8343
		public static float AmbientVolume = 0f;

		// Token: 0x04002098 RID: 8344
		public static float InterfaceVolume = 0f;

		// Token: 0x04002099 RID: 8345
		public static float CombatVolume = 0f;

		// Token: 0x0400209A RID: 8346
		public static int HPWarningThreshold = 40;
	}
}
