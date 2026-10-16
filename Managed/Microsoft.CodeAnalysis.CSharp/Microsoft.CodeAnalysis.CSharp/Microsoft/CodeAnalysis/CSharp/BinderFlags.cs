using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000010 RID: 16
	[Flags]
	internal enum BinderFlags : uint
	{
		// Token: 0x04000023 RID: 35
		None = 0U,
		// Token: 0x04000024 RID: 36
		SuppressConstraintChecks = 1U,
		// Token: 0x04000025 RID: 37
		SuppressObsoleteChecks = 2U,
		// Token: 0x04000026 RID: 38
		ConstructorInitializer = 4U,
		// Token: 0x04000027 RID: 39
		FieldInitializer = 8U,
		// Token: 0x04000028 RID: 40
		ObjectInitializerMember = 16U,
		// Token: 0x04000029 RID: 41
		CollectionInitializerAddMethod = 32U,
		// Token: 0x0400002A RID: 42
		AttributeArgument = 64U,
		// Token: 0x0400002B RID: 43
		GenericConstraintsClause = 128U,
		// Token: 0x0400002C RID: 44
		Cref = 256U,
		// Token: 0x0400002D RID: 45
		CrefParameterOrReturnType = 512U,
		// Token: 0x0400002E RID: 46
		UnsafeRegion = 1024U,
		// Token: 0x0400002F RID: 47
		SuppressUnsafeDiagnostics = 2048U,
		// Token: 0x04000030 RID: 48
		SemanticModel = 4096U,
		// Token: 0x04000031 RID: 49
		EarlyAttributeBinding = 8192U,
		// Token: 0x04000032 RID: 50
		CheckedRegion = 16384U,
		// Token: 0x04000033 RID: 51
		UncheckedRegion = 32768U,
		// Token: 0x04000034 RID: 52
		InLockBody = 65536U,
		// Token: 0x04000035 RID: 53
		InCatchBlock = 131072U,
		// Token: 0x04000036 RID: 54
		InFinallyBlock = 262144U,
		// Token: 0x04000037 RID: 55
		InTryBlockOfTryCatch = 524288U,
		// Token: 0x04000038 RID: 56
		InCatchFilter = 1048576U,
		// Token: 0x04000039 RID: 57
		InNestedFinallyBlock = 2097152U,
		// Token: 0x0400003A RID: 58
		IgnoreAccessibility = 4194304U,
		// Token: 0x0400003B RID: 59
		ParameterDefaultValue = 8388608U,
		// Token: 0x0400003C RID: 60
		AllowManagedAddressOf = 16777216U,
		// Token: 0x0400003D RID: 61
		AllowAwaitInUnsafeContext = 33554432U,
		// Token: 0x0400003E RID: 62
		IgnoreCorLibraryDuplicatedTypes = 67108864U,
		// Token: 0x0400003F RID: 63
		InContextualAttributeBinder = 134217728U,
		// Token: 0x04000040 RID: 64
		InEEMethodBinder = 268435456U,
		// Token: 0x04000041 RID: 65
		SuppressTypeArgumentBinding = 536870912U,
		// Token: 0x04000042 RID: 66
		AllClearedAtExecutableCodeBoundary = 4128768U
	}
}
