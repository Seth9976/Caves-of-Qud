using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000331 RID: 817
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseCrefParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x060045CF RID: 17871 RVA: 0x0011755D File Offset: 0x0011575D
		internal BaseCrefParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060045D0 RID: 17872
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<CrefParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x00117568 File Offset: 0x00115768
		public BaseCrefParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters)
		{
			return this.WithParametersCore(parameters);
		}

		// Token: 0x060045D2 RID: 17874
		internal abstract BaseCrefParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters);

		// Token: 0x060045D3 RID: 17875 RVA: 0x00117571 File Offset: 0x00115771
		public BaseCrefParameterListSyntax AddParameters(params CrefParameterSyntax[] items)
		{
			return this.AddParametersCore(items);
		}

		// Token: 0x060045D4 RID: 17876
		internal abstract BaseCrefParameterListSyntax AddParametersCore(params CrefParameterSyntax[] items);
	}
}
