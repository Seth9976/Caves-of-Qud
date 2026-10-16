using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.Cci
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	internal interface IReference
	{
		// Token: 0x06000341 RID: 833
		IEnumerable<ICustomAttribute> GetAttributes(EmitContext context);

		// Token: 0x06000342 RID: 834
		void Dispatch(MetadataVisitor visitor);

		// Token: 0x06000343 RID: 835
		[NullableContext(2)]
		IDefinition AsDefinition(EmitContext context);

		// Token: 0x06000344 RID: 836
		[NullableContext(2)]
		ISymbolInternal GetInternalSymbol();
	}
}
