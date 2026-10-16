using System;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	public enum SimulationStage : ushort
	{
		// Token: 0x040000B8 RID: 184
		None,
		// Token: 0x040000B9 RID: 185
		PrepareSimulation,
		// Token: 0x040000BA RID: 186
		RunSimulation,
		// Token: 0x040000BB RID: 187
		PublishSimulationResults = 4,
		// Token: 0x040000BC RID: 188
		All = 7
	}
}
