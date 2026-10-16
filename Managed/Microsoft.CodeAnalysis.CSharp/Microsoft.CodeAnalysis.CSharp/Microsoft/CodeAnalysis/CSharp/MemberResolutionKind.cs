using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000065 RID: 101
	internal enum MemberResolutionKind : byte
	{
		// Token: 0x04000424 RID: 1060
		None,
		// Token: 0x04000425 RID: 1061
		ApplicableInNormalForm,
		// Token: 0x04000426 RID: 1062
		ApplicableInExpandedForm,
		// Token: 0x04000427 RID: 1063
		InaccessibleTypeArgument,
		// Token: 0x04000428 RID: 1064
		NoCorrespondingParameter,
		// Token: 0x04000429 RID: 1065
		NoCorrespondingNamedParameter,
		// Token: 0x0400042A RID: 1066
		DuplicateNamedArgument,
		// Token: 0x0400042B RID: 1067
		RequiredParameterMissing,
		// Token: 0x0400042C RID: 1068
		NameUsedForPositional,
		// Token: 0x0400042D RID: 1069
		BadNonTrailingNamedArgument,
		// Token: 0x0400042E RID: 1070
		UseSiteError,
		// Token: 0x0400042F RID: 1071
		UnsupportedMetadata,
		// Token: 0x04000430 RID: 1072
		BadArgumentConversion,
		// Token: 0x04000431 RID: 1073
		TypeInferenceFailed,
		// Token: 0x04000432 RID: 1074
		TypeInferenceExtensionInstanceArgument,
		// Token: 0x04000433 RID: 1075
		ConstructedParameterFailedConstraintCheck,
		// Token: 0x04000434 RID: 1076
		ConstraintFailure,
		// Token: 0x04000435 RID: 1077
		StaticInstanceMismatch,
		// Token: 0x04000436 RID: 1078
		WrongCallingConvention,
		// Token: 0x04000437 RID: 1079
		WrongRefKind,
		// Token: 0x04000438 RID: 1080
		WrongReturnType,
		// Token: 0x04000439 RID: 1081
		LessDerived,
		// Token: 0x0400043A RID: 1082
		Worse,
		// Token: 0x0400043B RID: 1083
		Worst
	}
}
