using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000323 RID: 803
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004534 RID: 17716 RVA: 0x001166DE File Offset: 0x001148DE
		internal BaseParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06004535 RID: 17717
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<ParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001166E9 File Offset: 0x001148E9
		public BaseParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters)
		{
			return this.WithParametersCore(parameters);
		}

		// Token: 0x06004537 RID: 17719
		internal abstract BaseParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters);

		// Token: 0x06004538 RID: 17720 RVA: 0x001166F2 File Offset: 0x001148F2
		public BaseParameterListSyntax AddParameters(params ParameterSyntax[] items)
		{
			return this.AddParametersCore(items);
		}

		// Token: 0x06004539 RID: 17721
		internal abstract BaseParameterListSyntax AddParametersCore(params ParameterSyntax[] items);
	}
}
