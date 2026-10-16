using System;

namespace XRL.World
{
	// Token: 0x0200098E RID: 2446
	[Serializable]
	public enum LightLevel : byte
	{
		// Token: 0x04003028 RID: 12328
		Blackout,
		// Token: 0x04003029 RID: 12329
		None,
		// Token: 0x0400302A RID: 12330
		Darkvision = 10,
		// Token: 0x0400302B RID: 12331
		Dimvision = 15,
		// Token: 0x0400302C RID: 12332
		Safelight = 30,
		// Token: 0x0400302D RID: 12333
		Light = 200,
		// Token: 0x0400302E RID: 12334
		Interpolight = 210,
		// Token: 0x0400302F RID: 12335
		Radar = 228,
		// Token: 0x04003030 RID: 12336
		LitRadar = 232,
		// Token: 0x04003031 RID: 12337
		Omniscient = 255
	}
}
