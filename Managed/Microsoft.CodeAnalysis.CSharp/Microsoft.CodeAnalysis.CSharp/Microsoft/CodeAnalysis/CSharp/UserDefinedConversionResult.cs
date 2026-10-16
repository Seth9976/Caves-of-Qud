using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000053 RID: 83
	internal struct UserDefinedConversionResult
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x000424D6 File Offset: 0x000406D6
		public static UserDefinedConversionResult NoApplicableOperators(ImmutableArray<UserDefinedConversionAnalysis> results)
		{
			return new UserDefinedConversionResult(UserDefinedConversionResultKind.NoApplicableOperators, results, -1);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000424E0 File Offset: 0x000406E0
		public static UserDefinedConversionResult NoBestSourceType(ImmutableArray<UserDefinedConversionAnalysis> results)
		{
			return new UserDefinedConversionResult(UserDefinedConversionResultKind.NoBestSourceType, results, -1);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000424EA File Offset: 0x000406EA
		public static UserDefinedConversionResult NoBestTargetType(ImmutableArray<UserDefinedConversionAnalysis> results)
		{
			return new UserDefinedConversionResult(UserDefinedConversionResultKind.NoBestTargetType, results, -1);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000424F4 File Offset: 0x000406F4
		public static UserDefinedConversionResult Ambiguous(ImmutableArray<UserDefinedConversionAnalysis> results)
		{
			return new UserDefinedConversionResult(UserDefinedConversionResultKind.Ambiguous, results, -1);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000424FE File Offset: 0x000406FE
		public static UserDefinedConversionResult Valid(ImmutableArray<UserDefinedConversionAnalysis> results, int best)
		{
			return new UserDefinedConversionResult(UserDefinedConversionResultKind.Valid, results, best);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00042508 File Offset: 0x00040708
		private UserDefinedConversionResult(UserDefinedConversionResultKind kind, ImmutableArray<UserDefinedConversionAnalysis> results, int best)
		{
			this.Kind = kind;
			this.Results = results;
			this.Best = best;
		}

		// Token: 0x04000134 RID: 308
		public readonly ImmutableArray<UserDefinedConversionAnalysis> Results;

		// Token: 0x04000135 RID: 309
		public readonly int Best;

		// Token: 0x04000136 RID: 310
		public readonly UserDefinedConversionResultKind Kind;
	}
}
