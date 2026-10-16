using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000151 RID: 337
	internal abstract class SemanticModelProvider
	{
		// Token: 0x06000DA3 RID: 3491
		[NullableContext(1)]
		public abstract SemanticModel GetSemanticModel(SyntaxTree tree, Compilation compilation, bool ignoreAccessibility = false);
	}
}
