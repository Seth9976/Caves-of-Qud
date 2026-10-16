using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000064 RID: 100
	internal struct MemberAnalysisResult
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00049090 File Offset: 0x00047290
		private MemberAnalysisResult(MemberResolutionKind kind, ImmutableArray<int> badArgumentsOpt = default(ImmutableArray<int>), ImmutableArray<int> argsToParamsOpt = default(ImmutableArray<int>), ImmutableArray<Conversion> conversionsOpt = default(ImmutableArray<Conversion>), int missingParameter = -1, bool hasAnyRefOmittedArgument = false, ImmutableArray<TypeParameterDiagnosticInfo> constraintFailureDiagnosticsOpt = default(ImmutableArray<TypeParameterDiagnosticInfo>))
		{
			this.Kind = kind;
			this.BadArgumentsOpt = badArgumentsOpt;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.ConversionsOpt = conversionsOpt;
			this.BadParameter = missingParameter;
			this.HasAnyRefOmittedArgument = hasAnyRefOmittedArgument;
			this.ConstraintFailureDiagnostics = constraintFailureDiagnosticsOpt.NullToEmpty<TypeParameterDiagnosticInfo>();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000490CC File Offset: 0x000472CC
		public override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000490D3 File Offset: 0x000472D3
		public override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000490DC File Offset: 0x000472DC
		public Conversion ConversionForArg(int arg)
		{
			if (this.ConversionsOpt.IsDefault)
			{
				return Conversion.Identity;
			}
			return this.ConversionsOpt[arg];
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00049110 File Offset: 0x00047310
		public int ParameterFromArgument(int arg)
		{
			if (this.ArgsToParamsOpt.IsDefault)
			{
				return arg;
			}
			return this.ArgsToParamsOpt[arg];
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00049140 File Offset: 0x00047340
		public bool IsApplicable
		{
			get
			{
				MemberResolutionKind kind = this.Kind;
				return kind - MemberResolutionKind.ApplicableInNormalForm <= 1 || kind - MemberResolutionKind.Worse <= 1;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00049164 File Offset: 0x00047364
		public bool IsValid
		{
			get
			{
				MemberResolutionKind kind = this.Kind;
				return kind - MemberResolutionKind.ApplicableInNormalForm <= 1;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00049181 File Offset: 0x00047381
		internal bool HasUseSiteDiagnosticToReportFor(Symbol symbol)
		{
			return !MemberAnalysisResult.SuppressUseSiteDiagnosticsForKind(this.Kind) && symbol != null && symbol.GetUseSiteInfo().DiagnosticInfo != null;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000491A3 File Offset: 0x000473A3
		private static bool SuppressUseSiteDiagnosticsForKind(MemberResolutionKind kind)
		{
			if (kind - MemberResolutionKind.NoCorrespondingParameter > 4)
			{
				if (kind == MemberResolutionKind.UnsupportedMetadata)
				{
					return true;
				}
				if (kind != MemberResolutionKind.LessDerived)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000491BC File Offset: 0x000473BC
		public static MemberAnalysisResult ArgumentParameterMismatch(ArgumentAnalysisResult argAnalysis)
		{
			switch (argAnalysis.Kind)
			{
			case ArgumentAnalysisResultKind.NoCorrespondingParameter:
				return MemberAnalysisResult.NoCorrespondingParameter(argAnalysis.ArgumentPosition);
			case ArgumentAnalysisResultKind.NoCorrespondingNamedParameter:
				return MemberAnalysisResult.NoCorrespondingNamedParameter(argAnalysis.ArgumentPosition);
			case ArgumentAnalysisResultKind.DuplicateNamedArgument:
				return MemberAnalysisResult.DuplicateNamedArgument(argAnalysis.ArgumentPosition);
			case ArgumentAnalysisResultKind.RequiredParameterMissing:
				return MemberAnalysisResult.RequiredParameterMissing(argAnalysis.ParameterPosition);
			case ArgumentAnalysisResultKind.NameUsedForPositional:
				return MemberAnalysisResult.NameUsedForPositional(argAnalysis.ArgumentPosition);
			case ArgumentAnalysisResultKind.BadNonTrailingNamedArgument:
				return MemberAnalysisResult.BadNonTrailingNamedArgument(argAnalysis.ArgumentPosition);
			default:
				throw ExceptionUtilities.UnexpectedValue(argAnalysis.Kind);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004924C File Offset: 0x0004744C
		public static MemberAnalysisResult NameUsedForPositional(int argumentPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.NameUsedForPositional, ImmutableArray.Create<int>(argumentPosition), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00049284 File Offset: 0x00047484
		public static MemberAnalysisResult BadNonTrailingNamedArgument(int argumentPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.BadNonTrailingNamedArgument, ImmutableArray.Create<int>(argumentPosition), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000492BC File Offset: 0x000474BC
		public static MemberAnalysisResult NoCorrespondingParameter(int argumentPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.NoCorrespondingParameter, ImmutableArray.Create<int>(argumentPosition), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000492F4 File Offset: 0x000474F4
		public static MemberAnalysisResult NoCorrespondingNamedParameter(int argumentPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.NoCorrespondingNamedParameter, ImmutableArray.Create<int>(argumentPosition), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0004932C File Offset: 0x0004752C
		public static MemberAnalysisResult DuplicateNamedArgument(int argumentPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.DuplicateNamedArgument, ImmutableArray.Create<int>(argumentPosition), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00049364 File Offset: 0x00047564
		public static MemberAnalysisResult RequiredParameterMissing(int parameterPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.RequiredParameterMissing, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), parameterPosition, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000493A0 File Offset: 0x000475A0
		public static MemberAnalysisResult UseSiteError()
		{
			return new MemberAnalysisResult(MemberResolutionKind.UseSiteError, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000493DC File Offset: 0x000475DC
		public static MemberAnalysisResult UnsupportedMetadata()
		{
			return new MemberAnalysisResult(MemberResolutionKind.UnsupportedMetadata, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00049418 File Offset: 0x00047618
		public static MemberAnalysisResult BadArgumentConversions(ImmutableArray<int> argsToParamsOpt, ImmutableArray<int> badArguments, ImmutableArray<Conversion> conversions)
		{
			return new MemberAnalysisResult(MemberResolutionKind.BadArgumentConversion, badArguments, argsToParamsOpt, conversions, -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0004943C File Offset: 0x0004763C
		public static MemberAnalysisResult InaccessibleTypeArgument()
		{
			return new MemberAnalysisResult(MemberResolutionKind.InaccessibleTypeArgument, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00049478 File Offset: 0x00047678
		public static MemberAnalysisResult TypeInferenceFailed()
		{
			return new MemberAnalysisResult(MemberResolutionKind.TypeInferenceFailed, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000494B4 File Offset: 0x000476B4
		public static MemberAnalysisResult TypeInferenceExtensionInstanceArgumentFailed()
		{
			return new MemberAnalysisResult(MemberResolutionKind.TypeInferenceExtensionInstanceArgument, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000494F0 File Offset: 0x000476F0
		public static MemberAnalysisResult StaticInstanceMismatch()
		{
			return new MemberAnalysisResult(MemberResolutionKind.StaticInstanceMismatch, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0004952C File Offset: 0x0004772C
		public static MemberAnalysisResult ConstructedParameterFailedConstraintsCheck(int parameterPosition)
		{
			return new MemberAnalysisResult(MemberResolutionKind.ConstructedParameterFailedConstraintCheck, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), parameterPosition, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00049568 File Offset: 0x00047768
		public static MemberAnalysisResult WrongRefKind()
		{
			return new MemberAnalysisResult(MemberResolutionKind.WrongRefKind, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000495A4 File Offset: 0x000477A4
		public static MemberAnalysisResult WrongReturnType()
		{
			return new MemberAnalysisResult(MemberResolutionKind.WrongReturnType, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000495E0 File Offset: 0x000477E0
		public static MemberAnalysisResult LessDerived()
		{
			return new MemberAnalysisResult(MemberResolutionKind.LessDerived, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0004961C File Offset: 0x0004781C
		public static MemberAnalysisResult NormalForm(ImmutableArray<int> argsToParamsOpt, ImmutableArray<Conversion> conversions, bool hasAnyRefOmittedArgument)
		{
			return new MemberAnalysisResult(MemberResolutionKind.ApplicableInNormalForm, default(ImmutableArray<int>), argsToParamsOpt, conversions, -1, hasAnyRefOmittedArgument, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00049648 File Offset: 0x00047848
		public static MemberAnalysisResult ExpandedForm(ImmutableArray<int> argsToParamsOpt, ImmutableArray<Conversion> conversions, bool hasAnyRefOmittedArgument)
		{
			return new MemberAnalysisResult(MemberResolutionKind.ApplicableInExpandedForm, default(ImmutableArray<int>), argsToParamsOpt, conversions, -1, hasAnyRefOmittedArgument, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00049674 File Offset: 0x00047874
		public static MemberAnalysisResult Worse()
		{
			return new MemberAnalysisResult(MemberResolutionKind.Worse, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000496B0 File Offset: 0x000478B0
		public static MemberAnalysisResult Worst()
		{
			return new MemberAnalysisResult(MemberResolutionKind.Worst, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000496EC File Offset: 0x000478EC
		internal static MemberAnalysisResult ConstraintFailure(ImmutableArray<TypeParameterDiagnosticInfo> constraintFailureDiagnostics)
		{
			return new MemberAnalysisResult(MemberResolutionKind.ConstraintFailure, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, constraintFailureDiagnostics);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00049720 File Offset: 0x00047920
		internal static MemberAnalysisResult WrongCallingConvention()
		{
			return new MemberAnalysisResult(MemberResolutionKind.WrongCallingConvention, default(ImmutableArray<int>), default(ImmutableArray<int>), default(ImmutableArray<Conversion>), -1, false, default(ImmutableArray<TypeParameterDiagnosticInfo>));
		}

		// Token: 0x0400041C RID: 1052
		public readonly ImmutableArray<Conversion> ConversionsOpt;

		// Token: 0x0400041D RID: 1053
		public readonly ImmutableArray<int> BadArgumentsOpt;

		// Token: 0x0400041E RID: 1054
		public readonly ImmutableArray<int> ArgsToParamsOpt;

		// Token: 0x0400041F RID: 1055
		public readonly ImmutableArray<TypeParameterDiagnosticInfo> ConstraintFailureDiagnostics;

		// Token: 0x04000420 RID: 1056
		public readonly int BadParameter;

		// Token: 0x04000421 RID: 1057
		public readonly MemberResolutionKind Kind;

		// Token: 0x04000422 RID: 1058
		public readonly bool HasAnyRefOmittedArgument;
	}
}
