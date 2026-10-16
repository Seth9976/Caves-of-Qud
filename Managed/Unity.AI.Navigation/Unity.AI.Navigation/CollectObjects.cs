using System;
using UnityEngine;

namespace Unity.AI.Navigation
{
	// Token: 0x02000007 RID: 7
	public enum CollectObjects
	{
		// Token: 0x0400002C RID: 44
		[InspectorName("All Game Objects")]
		All,
		// Token: 0x0400002D RID: 45
		[InspectorName("Volume")]
		Volume,
		// Token: 0x0400002E RID: 46
		[InspectorName("Current Object Hierarchy")]
		Children,
		// Token: 0x0400002F RID: 47
		[InspectorName("NavMeshModifier Component Only")]
		MarkedWithModifier
	}
}
