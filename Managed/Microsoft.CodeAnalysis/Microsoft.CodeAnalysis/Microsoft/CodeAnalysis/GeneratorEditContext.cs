using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E0 RID: 480
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct GeneratorEditContext
	{
		// Token: 0x060013DF RID: 5087 RVA: 0x0003E07C File Offset: 0x0003C27C
		internal GeneratorEditContext(AdditionalSourcesCollection sources, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.AdditionalSources = sources;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003E08C File Offset: 0x0003C28C
		public CancellationToken CancellationToken { get; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0003E094 File Offset: 0x0003C294
		public AdditionalSourcesCollection AdditionalSources { get; }
	}
}
