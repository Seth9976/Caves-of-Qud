using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FE1 RID: 4065
	public enum ActivePartStatus
	{
		// Token: 0x040042FE RID: 17150
		Operational,
		// Token: 0x040042FF RID: 17151
		NeedsSubject,
		// Token: 0x04004300 RID: 17152
		EMP,
		// Token: 0x04004301 RID: 17153
		Broken,
		// Token: 0x04004302 RID: 17154
		Rusted,
		// Token: 0x04004303 RID: 17155
		Booting,
		// Token: 0x04004304 RID: 17156
		NotHanging,
		// Token: 0x04004305 RID: 17157
		SwitchedOff,
		// Token: 0x04004306 RID: 17158
		RealityStabilized,
		// Token: 0x04004307 RID: 17159
		LimbIncompatible,
		// Token: 0x04004308 RID: 17160
		PrimarySystemOffline,
		// Token: 0x04004309 RID: 17161
		ProcessInputMissing,
		// Token: 0x0400430A RID: 17162
		ProcessInputInvalid,
		// Token: 0x0400430B RID: 17163
		Unpowered,
		// Token: 0x0400430C RID: 17164
		LocallyDefinedFailure
	}
}
