using System;
using LaundryBear.PlatformServices;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014EC RID: 5356
	public class MainMenuOptionData : FrameworkDataElement
	{
		// Token: 0x0400528E RID: 21134
		public bool Enabled = true;

		// Token: 0x0400528F RID: 21135
		public string Text;

		// Token: 0x04005290 RID: 21136
		public string Command;

		// Token: 0x04005291 RID: 21137
		public Platform AllowedPlatforms = (Platform)(-1);

		// Token: 0x04005292 RID: 21138
		public MainMenuOptionData.AlertMode Alert;

		// Token: 0x04005293 RID: 21139
		public KeyCode Shortcut;

		// Token: 0x02001DFC RID: 7676
		public enum AlertMode
		{
			// Token: 0x04007532 RID: 30002
			None,
			// Token: 0x04007533 RID: 30003
			ModStatus
		}
	}
}
