using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F0 RID: 752
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class StatementSyntax : CSharpSyntaxNode
	{
		// Token: 0x060041FE RID: 16894 RVA: 0x0011054C File Offset: 0x0010E74C
		internal StatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x060041FF RID: 16895
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00110557 File Offset: 0x0010E757
		public StatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06004201 RID: 16897
		internal abstract StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists);

		// Token: 0x06004202 RID: 16898 RVA: 0x00110560 File Offset: 0x0010E760
		public StatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddAttributeListsCore(items);
		}

		// Token: 0x06004203 RID: 16899
		internal abstract StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items);
	}
}
