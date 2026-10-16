using System;

namespace System.Globalization
{
	// Token: 0x0200097E RID: 2430
	internal static class GlobalizationMode
	{
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060055A8 RID: 21928 RVA: 0x00121285 File Offset: 0x0011F485
		internal static bool Invariant { get; } = GlobalizationMode.GetGlobalizationInvariantMode();

		// Token: 0x060055A9 RID: 21929 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		private static bool GetGlobalizationInvariantMode()
		{
			return false;
		}

		// Token: 0x04003553 RID: 13651
		private const string c_InvariantModeConfigSwitch = "System.Globalization.Invariant";
	}
}
