using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000066 RID: 102
	internal struct MemberResolutionResult<TMember> where TMember : Symbol
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0004975A File Offset: 0x0004795A
		internal MemberResolutionResult(TMember member, TMember leastOverriddenMember, MemberAnalysisResult result)
		{
			this._member = member;
			this._leastOverriddenMember = leastOverriddenMember;
			this._result = result;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00049771 File Offset: 0x00047971
		internal bool IsNull
		{
			get
			{
				return this._member == null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00049781 File Offset: 0x00047981
		internal bool IsNotNull
		{
			get
			{
				return this._member != null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00049791 File Offset: 0x00047991
		public TMember Member
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00049799 File Offset: 0x00047999
		internal TMember LeastOverriddenMember
		{
			get
			{
				return this._leastOverriddenMember;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000497A1 File Offset: 0x000479A1
		public MemberResolutionKind Resolution
		{
			get
			{
				return this.Result.Kind;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000497B0 File Offset: 0x000479B0
		public bool IsValid
		{
			get
			{
				return this.Result.IsValid;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000497CC File Offset: 0x000479CC
		public bool IsApplicable
		{
			get
			{
				return this.Result.IsApplicable;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000497E7 File Offset: 0x000479E7
		internal MemberResolutionResult<TMember> Worse()
		{
			return new MemberResolutionResult<TMember>(this.Member, this.LeastOverriddenMember, MemberAnalysisResult.Worse());
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000497FF File Offset: 0x000479FF
		internal MemberResolutionResult<TMember> Worst()
		{
			return new MemberResolutionResult<TMember>(this.Member, this.LeastOverriddenMember, MemberAnalysisResult.Worst());
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00049818 File Offset: 0x00047A18
		internal bool HasUseSiteDiagnosticToReport
		{
			get
			{
				return this._result.HasUseSiteDiagnosticToReportFor(this._member);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0004983E File Offset: 0x00047A3E
		internal MemberAnalysisResult Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00049846 File Offset: 0x00047A46
		public override bool Equals(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0004984D File Offset: 0x00047A4D
		public override int GetHashCode()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400043C RID: 1084
		private readonly TMember _member;

		// Token: 0x0400043D RID: 1085
		private readonly TMember _leastOverriddenMember;

		// Token: 0x0400043E RID: 1086
		private readonly MemberAnalysisResult _result;
	}
}
