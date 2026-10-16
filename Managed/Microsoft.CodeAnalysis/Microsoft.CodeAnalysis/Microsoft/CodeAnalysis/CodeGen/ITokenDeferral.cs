using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C8 RID: 1224
	[NullableContext(1)]
	internal interface ITokenDeferral
	{
		// Token: 0x06003143 RID: 12611
		uint GetFakeStringTokenForIL(string value);

		// Token: 0x06003144 RID: 12612
		uint GetFakeSymbolTokenForIL(IReference value, SyntaxNode syntaxNode, DiagnosticBag diagnostics);

		// Token: 0x06003145 RID: 12613
		uint GetFakeSymbolTokenForIL(ISignature value, SyntaxNode syntaxNode, DiagnosticBag diagnostics);

		// Token: 0x06003146 RID: 12614
		uint GetSourceDocumentIndexForIL(DebugSourceDocument document);

		// Token: 0x06003147 RID: 12615
		IFieldReference GetFieldForData([Nullable(0)] ImmutableArray<byte> data, SyntaxNode syntaxNode, DiagnosticBag diagnostics);

		// Token: 0x06003148 RID: 12616
		IMethodReference GetInitArrayHelper();

		// Token: 0x06003149 RID: 12617
		string GetStringFromToken(uint token);

		// Token: 0x0600314A RID: 12618
		object GetReferenceFromToken(uint token);

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600314B RID: 12619
		ArrayMethods ArrayMethods { get; }
	}
}
