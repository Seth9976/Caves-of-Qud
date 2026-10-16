using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000322 RID: 802
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AccessorListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004527 RID: 17703 RVA: 0x0011658B File Offset: 0x0011478B
		internal AccessorListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x00116596 File Offset: 0x00114796
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((AccessorListSyntax)base.Green).openBraceToken, base.Position, 0);
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x001165B5 File Offset: 0x001147B5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AccessorDeclarationSyntax> Accessors
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AccessorDeclarationSyntax>(base.GetRed(ref this.accessors, 1));
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x001165C9 File Offset: 0x001147C9
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((AccessorListSyntax)base.Green).closeBraceToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x001165EF File Offset: 0x001147EF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.accessors, 1);
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00116604 File Offset: 0x00114804
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.accessors;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x00116612 File Offset: 0x00114812
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAccessorList(this);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0011661B File Offset: 0x0011481B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAccessorList(this);
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x00116624 File Offset: 0x00114824
		public AccessorListSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
		{
			if (!(openBraceToken != this.OpenBraceToken) && !(accessors != this.Accessors) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			AccessorListSyntax accessorListSyntax = SyntaxFactory.AccessorList(openBraceToken, accessors, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return accessorListSyntax;
			}
			return accessorListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x0011667D File Offset: 0x0011487D
		public AccessorListSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(openBraceToken, this.Accessors, this.CloseBraceToken);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x00116692 File Offset: 0x00114892
		public AccessorListSyntax WithAccessors([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AccessorDeclarationSyntax> accessors)
		{
			return this.Update(this.OpenBraceToken, accessors, this.CloseBraceToken);
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x001166A7 File Offset: 0x001148A7
		public AccessorListSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.OpenBraceToken, this.Accessors, closeBraceToken);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001166BC File Offset: 0x001148BC
		public AccessorListSyntax AddAccessors(params AccessorDeclarationSyntax[] items)
		{
			return this.WithAccessors(this.Accessors.AddRange(items));
		}

		// Token: 0x04001509 RID: 5385
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode accessors;
	}
}
