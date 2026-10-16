using System;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x0200035C RID: 860
	internal enum CustomDebugInfoKind : byte
	{
		// Token: 0x04000FCC RID: 4044
		UsingGroups,
		// Token: 0x04000FCD RID: 4045
		ForwardMethodInfo,
		// Token: 0x04000FCE RID: 4046
		ForwardModuleInfo,
		// Token: 0x04000FCF RID: 4047
		StateMachineHoistedLocalScopes,
		// Token: 0x04000FD0 RID: 4048
		StateMachineTypeName,
		// Token: 0x04000FD1 RID: 4049
		DynamicLocals,
		// Token: 0x04000FD2 RID: 4050
		EditAndContinueLocalSlotMap,
		// Token: 0x04000FD3 RID: 4051
		EditAndContinueLambdaMap,
		// Token: 0x04000FD4 RID: 4052
		TupleElementNames
	}
}
