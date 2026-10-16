using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000166 RID: 358
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AdditionalText
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E75 RID: 3701
		public abstract string Path { get; }

		// Token: 0x06000E76 RID: 3702
		[NullableContext(2)]
		public abstract SourceText GetText(CancellationToken cancellationToken = default(CancellationToken));
	}
}
