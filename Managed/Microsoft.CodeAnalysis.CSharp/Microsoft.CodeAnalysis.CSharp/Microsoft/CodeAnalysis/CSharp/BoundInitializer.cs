using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015F RID: 351
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundInitializer : BoundNode
	{
		// Token: 0x06001D97 RID: 7575 RVA: 0x0009F8C0 File Offset: 0x0009DAC0
		protected BoundInitializer(BoundKind kind, SyntaxNode syntax, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0009F8CB File Offset: 0x0009DACB
		protected BoundInitializer(BoundKind kind, SyntaxNode syntax)
			: base(kind, syntax)
		{
		}
	}
}
