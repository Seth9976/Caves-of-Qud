using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DF RID: 479
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GeneratorPostInitializationContext
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0003E040 File Offset: 0x0003C240
		internal GeneratorPostInitializationContext(AdditionalSourcesCollection additionalSources, CancellationToken cancellationToken)
		{
			this._additionalSources = additionalSources;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0003E050 File Offset: 0x0003C250
		public CancellationToken CancellationToken { get; }

		// Token: 0x060013DD RID: 5085 RVA: 0x0003E058 File Offset: 0x0003C258
		public void AddSource(string hintName, string source)
		{
			this.AddSource(hintName, SourceText.From(source, Encoding.UTF8, SourceHashAlgorithm.Sha1));
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003E06D File Offset: 0x0003C26D
		public void AddSource(string hintName, SourceText sourceText)
		{
			this._additionalSources.Add(hintName, sourceText);
		}

		// Token: 0x0400060D RID: 1549
		private readonly AdditionalSourcesCollection _additionalSources;
	}
}
