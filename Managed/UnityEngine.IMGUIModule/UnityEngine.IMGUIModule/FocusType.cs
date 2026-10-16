using System;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public enum FocusType
	{
		// Token: 0x0400007E RID: 126
		[Obsolete("FocusType.Native now behaves the same as FocusType.Passive in all OS cases. (UnityUpgradable) -> Passive", false)]
		Native,
		// Token: 0x0400007F RID: 127
		Keyboard,
		// Token: 0x04000080 RID: 128
		Passive
	}
}
