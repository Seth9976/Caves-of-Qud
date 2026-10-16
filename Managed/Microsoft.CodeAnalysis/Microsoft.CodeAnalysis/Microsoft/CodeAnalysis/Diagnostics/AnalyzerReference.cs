using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030C RID: 780
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AnalyzerReference
	{
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002246 RID: 8774
		[Nullable(2)]
		public abstract string FullPath
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0006D23B File Offset: 0x0006B43B
		public virtual string Display
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002248 RID: 8776
		public abstract object Id { get; }

		// Token: 0x06002249 RID: 8777
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<DiagnosticAnalyzer> GetAnalyzersForAllLanguages();

		// Token: 0x0600224A RID: 8778
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<DiagnosticAnalyzer> GetAnalyzers(string language);

		// Token: 0x0600224B RID: 8779 RVA: 0x0006D242 File Offset: 0x0006B442
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual ImmutableArray<ISourceGenerator> GetGeneratorsForAllLanguages()
		{
			return ImmutableArray<ISourceGenerator>.Empty;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0006D249 File Offset: 0x0006B449
		[Obsolete("Use GetGenerators(string language) or GetGeneratorsForAllLanguages()")]
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual ImmutableArray<ISourceGenerator> GetGenerators()
		{
			return ImmutableArray<ISourceGenerator>.Empty;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0006D250 File Offset: 0x0006B450
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual ImmutableArray<ISourceGenerator> GetGenerators(string language)
		{
			return ImmutableArray<ISourceGenerator>.Empty;
		}
	}
}
