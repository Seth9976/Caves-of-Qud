using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F7 RID: 247
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IBoundLambdaOrFunction
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060012D1 RID: 4817
		MethodSymbol Symbol { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060012D2 RID: 4818
		SyntaxNode Syntax { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012D3 RID: 4819
		[global::System.Runtime.CompilerServices.Nullable(2)]
		BoundBlock Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060012D4 RID: 4820
		bool WasCompilerGenerated { get; }
	}
}
