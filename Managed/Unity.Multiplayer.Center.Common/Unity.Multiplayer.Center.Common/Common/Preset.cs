using System;
using UnityEngine;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x0200000D RID: 13
	[InspectorOrder(InspectorSort.ByName, InspectorSortDirection.Ascending)]
	[Serializable]
	public enum Preset
	{
		// Token: 0x04000027 RID: 39
		[InspectorName("-")]
		None,
		// Token: 0x04000028 RID: 40
		[InspectorName("Adventure")]
		Adventure,
		// Token: 0x04000029 RID: 41
		[InspectorName("Shooter, Battle Royale, Battle Arena")]
		Shooter,
		// Token: 0x0400002A RID: 42
		[InspectorName("Racing")]
		Racing,
		// Token: 0x0400002B RID: 43
		[InspectorName("Card Battle, Turn-based, Tabletop")]
		TurnBased,
		// Token: 0x0400002C RID: 44
		[InspectorName("Simulation")]
		Simulation,
		// Token: 0x0400002D RID: 45
		[InspectorName("Strategy")]
		Strategy,
		// Token: 0x0400002E RID: 46
		[InspectorName("Sports")]
		Sports,
		// Token: 0x0400002F RID: 47
		[InspectorName("Role-Playing, MMO")]
		RolePlaying,
		// Token: 0x04000030 RID: 48
		[InspectorName("Async, Idle, Hyper Casual, Puzzle")]
		Async,
		// Token: 0x04000031 RID: 49
		[InspectorName("Fighting")]
		Fighting,
		// Token: 0x04000032 RID: 50
		[InspectorName("Arcade, Platformer, Sandbox")]
		Sandbox
	}
}
