using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000482 RID: 1154
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CSharpSymbolChanges : SymbolChanges
	{
		// Token: 0x06006070 RID: 24688 RVA: 0x00174CE9 File Offset: 0x00172EE9
		public CSharpSymbolChanges(DefinitionMap definitionMap, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol)
			: base(definitionMap, edits, isAddedSymbol)
		{
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x00174CF4 File Offset: 0x00172EF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override ISymbolInternal GetISymbolInternalOrNull(ISymbol symbol)
		{
			Symbol symbol2 = symbol as Symbol;
			if (symbol2 == null)
			{
				return null;
			}
			return symbol2.UnderlyingSymbol;
		}
	}
}
