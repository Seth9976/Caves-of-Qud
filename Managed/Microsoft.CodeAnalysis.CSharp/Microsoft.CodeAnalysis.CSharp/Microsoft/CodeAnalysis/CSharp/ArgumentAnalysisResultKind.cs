using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000062 RID: 98
	internal enum ArgumentAnalysisResultKind : byte
	{
		// Token: 0x04000411 RID: 1041
		Normal,
		// Token: 0x04000412 RID: 1042
		Expanded,
		// Token: 0x04000413 RID: 1043
		NoCorrespondingParameter,
		// Token: 0x04000414 RID: 1044
		FirstInvalid = 2,
		// Token: 0x04000415 RID: 1045
		NoCorrespondingNamedParameter,
		// Token: 0x04000416 RID: 1046
		DuplicateNamedArgument,
		// Token: 0x04000417 RID: 1047
		RequiredParameterMissing,
		// Token: 0x04000418 RID: 1048
		NameUsedForPositional,
		// Token: 0x04000419 RID: 1049
		BadNonTrailingNamedArgument
	}
}
