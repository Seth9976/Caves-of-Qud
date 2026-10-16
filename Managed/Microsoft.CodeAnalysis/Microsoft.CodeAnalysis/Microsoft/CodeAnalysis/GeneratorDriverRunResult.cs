using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001EF RID: 495
	public class GeneratorDriverRunResult
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0003F206 File Offset: 0x0003D406
		internal GeneratorDriverRunResult(ImmutableArray<GeneratorRunResult> results)
		{
			this.Results = results;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003F215 File Offset: 0x0003D415
		public ImmutableArray<GeneratorRunResult> Results { get; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003F220 File Offset: 0x0003D420
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyDiagnostics.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<Diagnostic>(ref this._lazyDiagnostics, Enumerable.SelectMany<GeneratorRunResult, Diagnostic>(this.Results, (GeneratorRunResult r) => r.Diagnostics).ToImmutableArray<Diagnostic>());
				}
				return this._lazyDiagnostics;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0003F280 File Offset: 0x0003D480
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<SyntaxTree> GeneratedTrees
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyGeneratedTrees.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<SyntaxTree>(ref this._lazyGeneratedTrees, Enumerable.SelectMany<GeneratorRunResult, SyntaxTree>(this.Results, (GeneratorRunResult r) => r.GeneratedSources.Select<GeneratedSourceResult, SyntaxTree>((GeneratedSourceResult g) => g.SyntaxTree)).ToImmutableArray<SyntaxTree>());
				}
				return this._lazyGeneratedTrees;
			}
		}

		// Token: 0x04000627 RID: 1575
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> _lazyDiagnostics;

		// Token: 0x04000628 RID: 1576
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SyntaxTree> _lazyGeneratedTrees;
	}
}
