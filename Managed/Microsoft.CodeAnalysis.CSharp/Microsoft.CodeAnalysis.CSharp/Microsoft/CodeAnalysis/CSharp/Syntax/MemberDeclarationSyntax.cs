using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000306 RID: 774
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class MemberDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004308 RID: 17160 RVA: 0x00111EC6 File Offset: 0x001100C6
		internal MemberDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06004309 RID: 17161
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x00111ED1 File Offset: 0x001100D1
		public MemberDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x0600430B RID: 17163
		internal abstract MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists);

		// Token: 0x0600430C RID: 17164 RVA: 0x00111EDA File Offset: 0x001100DA
		public MemberDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddAttributeListsCore(items);
		}

		// Token: 0x0600430D RID: 17165
		internal abstract MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items);

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600430E RID: 17166
		public abstract SyntaxTokenList Modifiers { get; }

		// Token: 0x0600430F RID: 17167 RVA: 0x00111EE3 File Offset: 0x001100E3
		public MemberDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.WithModifiersCore(modifiers);
		}

		// Token: 0x06004310 RID: 17168
		internal abstract MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers);

		// Token: 0x06004311 RID: 17169 RVA: 0x00111EEC File Offset: 0x001100EC
		public MemberDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.AddModifiersCore(items);
		}

		// Token: 0x06004312 RID: 17170
		internal abstract MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items);
	}
}
