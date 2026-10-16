using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030A RID: 778
	[NullableContext(1)]
	[Nullable(0)]
	public class AnalyzerOptions
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0006D04C File Offset: 0x0006B24C
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<AdditionalText> AdditionalFiles
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0006D054 File Offset: 0x0006B254
		public AnalyzerConfigOptionsProvider AnalyzerConfigOptionsProvider { get; }

		// Token: 0x0600223D RID: 8765 RVA: 0x0006D05C File Offset: 0x0006B25C
		public AnalyzerOptions([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles, AnalyzerConfigOptionsProvider optionsProvider)
		{
			if (optionsProvider == null)
			{
				throw new ArgumentNullException("optionsProvider");
			}
			this.AdditionalFiles = additionalFiles.NullToEmpty<AdditionalText>();
			this.AnalyzerConfigOptionsProvider = optionsProvider;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0006D085 File Offset: 0x0006B285
		public AnalyzerOptions([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
			: this(additionalFiles, CompilerAnalyzerConfigOptionsProvider.Empty)
		{
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0006D093 File Offset: 0x0006B293
		public AnalyzerOptions WithAdditionalFiles([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
		{
			if (this.AdditionalFiles == additionalFiles)
			{
				return this;
			}
			return new AnalyzerOptions(additionalFiles);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			AnalyzerOptions analyzerOptions = obj as AnalyzerOptions;
			return analyzerOptions != null && (this.AdditionalFiles == analyzerOptions.AdditionalFiles || this.AdditionalFiles.SequenceEqual<AdditionalText, AdditionalText>(analyzerOptions.AdditionalFiles, new Func<AdditionalText, AdditionalText, bool>(object.ReferenceEquals)));
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0006D0FD File Offset: 0x0006B2FD
		public override int GetHashCode()
		{
			return Hash.CombineValues<AdditionalText>(this.AdditionalFiles, int.MaxValue);
		}

		// Token: 0x04000EB5 RID: 3765
		internal static readonly AnalyzerOptions Empty = new AnalyzerOptions(ImmutableArray<AdditionalText>.Empty);
	}
}
