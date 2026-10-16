using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000061 RID: 97
	internal struct ArgumentAnalysisResult
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00048F54 File Offset: 0x00047154
		public int ParameterFromArgument(int arg)
		{
			if (this.ArgsToParamsOpt.IsDefault)
			{
				return arg;
			}
			return this.ArgsToParamsOpt[arg];
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00048F82 File Offset: 0x00047182
		private ArgumentAnalysisResult(ArgumentAnalysisResultKind kind, int argumentPosition, int parameterPosition, ImmutableArray<int> argsToParamsOpt)
		{
			this.Kind = kind;
			this.ArgumentPosition = argumentPosition;
			this.ParameterPosition = parameterPosition;
			this.ArgsToParamsOpt = argsToParamsOpt;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00048FA1 File Offset: 0x000471A1
		public bool IsValid
		{
			get
			{
				return this.Kind < ArgumentAnalysisResultKind.NoCorrespondingParameter;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00048FAC File Offset: 0x000471AC
		public static ArgumentAnalysisResult NameUsedForPositional(int argumentPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.NameUsedForPositional, argumentPosition, 0, default(ImmutableArray<int>));
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00048FCC File Offset: 0x000471CC
		public static ArgumentAnalysisResult NoCorrespondingParameter(int argumentPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.NoCorrespondingParameter, argumentPosition, 0, default(ImmutableArray<int>));
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00048FEC File Offset: 0x000471EC
		public static ArgumentAnalysisResult NoCorrespondingNamedParameter(int argumentPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.NoCorrespondingNamedParameter, argumentPosition, 0, default(ImmutableArray<int>));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0004900C File Offset: 0x0004720C
		public static ArgumentAnalysisResult DuplicateNamedArgument(int argumentPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.DuplicateNamedArgument, argumentPosition, 0, default(ImmutableArray<int>));
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0004902C File Offset: 0x0004722C
		public static ArgumentAnalysisResult RequiredParameterMissing(int parameterPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.RequiredParameterMissing, 0, parameterPosition, default(ImmutableArray<int>));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0004904C File Offset: 0x0004724C
		public static ArgumentAnalysisResult BadNonTrailingNamedArgument(int argumentPosition)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.BadNonTrailingNamedArgument, argumentPosition, 0, default(ImmutableArray<int>));
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0004906A File Offset: 0x0004726A
		public static ArgumentAnalysisResult NormalForm(ImmutableArray<int> argsToParamsOpt)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.Normal, 0, 0, argsToParamsOpt);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00049075 File Offset: 0x00047275
		public static ArgumentAnalysisResult ExpandedForm(ImmutableArray<int> argsToParamsOpt)
		{
			return new ArgumentAnalysisResult(ArgumentAnalysisResultKind.Expanded, 0, 0, argsToParamsOpt);
		}

		// Token: 0x0400040C RID: 1036
		public readonly ImmutableArray<int> ArgsToParamsOpt;

		// Token: 0x0400040D RID: 1037
		public readonly int ArgumentPosition;

		// Token: 0x0400040E RID: 1038
		public readonly int ParameterPosition;

		// Token: 0x0400040F RID: 1039
		public readonly ArgumentAnalysisResultKind Kind;
	}
}
