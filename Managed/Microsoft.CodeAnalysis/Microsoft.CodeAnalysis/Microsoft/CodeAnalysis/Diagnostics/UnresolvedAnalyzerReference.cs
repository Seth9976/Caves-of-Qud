using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000351 RID: 849
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnresolvedAnalyzerReference : AnalyzerReference
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x00071C18 File Offset: 0x0006FE18
		public UnresolvedAnalyzerReference(string unresolvedPath)
		{
			if (unresolvedPath == null)
			{
				throw new ArgumentNullException("unresolvedPath");
			}
			this._unresolvedPath = unresolvedPath;
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00071C35 File Offset: 0x0006FE35
		public override string Display
		{
			get
			{
				return CodeAnalysisResources.Unresolved + this.FullPath;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x00071C47 File Offset: 0x0006FE47
		public override string FullPath
		{
			get
			{
				return this._unresolvedPath;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x00071C4F File Offset: 0x0006FE4F
		public override object Id
		{
			get
			{
				return this._unresolvedPath;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00071C57 File Offset: 0x0006FE57
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzersForAllLanguages()
		{
			return ImmutableArray<DiagnosticAnalyzer>.Empty;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00071C5E File Offset: 0x0006FE5E
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzers(string language)
		{
			return ImmutableArray<DiagnosticAnalyzer>.Empty;
		}

		// Token: 0x04000F90 RID: 3984
		private readonly string _unresolvedPath;
	}
}
