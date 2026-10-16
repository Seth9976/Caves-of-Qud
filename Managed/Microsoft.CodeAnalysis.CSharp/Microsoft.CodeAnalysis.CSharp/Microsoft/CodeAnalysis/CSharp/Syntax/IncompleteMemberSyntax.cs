using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000327 RID: 807
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IncompleteMemberSyntax : MemberDeclarationSyntax
	{
		// Token: 0x0600456A RID: 17770 RVA: 0x00116ABA File Offset: 0x00114CBA
		internal IncompleteMemberSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x00116AC5 File Offset: 0x00114CC5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x00116ADC File Offset: 0x00114CDC
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x00116B18 File Offset: 0x00114D18
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00116B28 File Offset: 0x00114D28
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<TypeSyntax>(ref this.type, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00116B60 File Offset: 0x00114D60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.type;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x00116B8B File Offset: 0x00114D8B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIncompleteMember(this);
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00116B94 File Offset: 0x00114D94
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIncompleteMember(this);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x00116BA0 File Offset: 0x00114DA0
		public IncompleteMemberSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && type == this.Type)
			{
				return this;
			}
			IncompleteMemberSyntax incompleteMemberSyntax = SyntaxFactory.IncompleteMember(attributeLists, modifiers, type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return incompleteMemberSyntax;
			}
			return incompleteMemberSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00116BF4 File Offset: 0x00114DF4
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00116BFD File Offset: 0x00114DFD
		public new IncompleteMemberSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Type);
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00116C12 File Offset: 0x00114E12
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00116C1B File Offset: 0x00114E1B
		public new IncompleteMemberSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Type);
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00116C30 File Offset: 0x00114E30
		public IncompleteMemberSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, type);
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x00116C45 File Offset: 0x00114E45
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00116C50 File Offset: 0x00114E50
		public new IncompleteMemberSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00116C72 File Offset: 0x00114E72
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x00116C7C File Offset: 0x00114E7C
		public new IncompleteMemberSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x0400150D RID: 5389
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400150E RID: 5390
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
