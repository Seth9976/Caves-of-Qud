using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000325 RID: 805
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004549 RID: 17737 RVA: 0x00116886 File Offset: 0x00114A86
		internal BaseParameterSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600454A RID: 17738
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00116891 File Offset: 0x00114A91
		public BaseParameterSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x0600454C RID: 17740
		internal abstract BaseParameterSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists);

		// Token: 0x0600454D RID: 17741 RVA: 0x0011689A File Offset: 0x00114A9A
		public BaseParameterSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.AddAttributeListsCore(items);
		}

		// Token: 0x0600454E RID: 17742
		internal abstract BaseParameterSyntax AddAttributeListsCore(params AttributeListSyntax[] items);

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x0600454F RID: 17743
		public abstract SyntaxTokenList Modifiers { get; }

		// Token: 0x06004550 RID: 17744 RVA: 0x001168A3 File Offset: 0x00114AA3
		public BaseParameterSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.WithModifiersCore(modifiers);
		}

		// Token: 0x06004551 RID: 17745
		internal abstract BaseParameterSyntax WithModifiersCore(SyntaxTokenList modifiers);

		// Token: 0x06004552 RID: 17746 RVA: 0x001168AC File Offset: 0x00114AAC
		public BaseParameterSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.AddModifiersCore(items);
		}

		// Token: 0x06004553 RID: 17747
		internal abstract BaseParameterSyntax AddModifiersCore(params SyntaxToken[] items);

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004554 RID: 17748
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x001168B5 File Offset: 0x00114AB5
		public BaseParameterSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.WithTypeCore(type);
		}

		// Token: 0x06004556 RID: 17750
		internal abstract BaseParameterSyntax WithTypeCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type);
	}
}
