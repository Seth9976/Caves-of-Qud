using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025B RID: 603
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EnumMemberDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x0600395B RID: 14683 RVA: 0x000FD54A File Offset: 0x000FB74A
		public EnumMemberDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
		{
			return this.Update(attributeLists, this.Modifiers, identifier, equalsValue);
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000FD55B File Offset: 0x000FB75B
		internal EnumMemberDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000FD566 File Offset: 0x000FB766
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x000FD57C File Offset: 0x000FB77C
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

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000FD5B8 File Offset: 0x000FB7B8
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((EnumMemberDeclarationSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000FD5DE File Offset: 0x000FB7DE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EqualsValueClauseSyntax EqualsValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<EqualsValueClauseSyntax>(ref this.equalsValue, 3);
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000FD5F0 File Offset: 0x000FB7F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<EqualsValueClauseSyntax>(ref this.equalsValue, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000FD628 File Offset: 0x000FB828
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.equalsValue;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000FD653 File Offset: 0x000FB853
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEnumMemberDeclaration(this);
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000FD65C File Offset: 0x000FB85C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEnumMemberDeclaration(this);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000FD668 File Offset: 0x000FB868
		public EnumMemberDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax equalsValue)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(identifier != this.Identifier) && equalsValue == this.EqualsValue)
			{
				return this;
			}
			EnumMemberDeclarationSyntax enumMemberDeclarationSyntax = SyntaxFactory.EnumMemberDeclaration(attributeLists, modifiers, identifier, equalsValue);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return enumMemberDeclarationSyntax;
			}
			return enumMemberDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000FD6CD File Offset: 0x000FB8CD
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000FD6D6 File Offset: 0x000FB8D6
		public new EnumMemberDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Identifier, this.EqualsValue);
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000FD6F1 File Offset: 0x000FB8F1
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000FD6FA File Offset: 0x000FB8FA
		public new EnumMemberDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Identifier, this.EqualsValue);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000FD715 File Offset: 0x000FB915
		public EnumMemberDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, identifier, this.EqualsValue);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000FD730 File Offset: 0x000FB930
		public EnumMemberDeclarationSyntax WithEqualsValue([global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax equalsValue)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, equalsValue);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000FD74B File Offset: 0x000FB94B
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000FD754 File Offset: 0x000FB954
		public new EnumMemberDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000FD776 File Offset: 0x000FB976
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000FD780 File Offset: 0x000FB980
		public new EnumMemberDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x040013BD RID: 5053
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013BE RID: 5054
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private EqualsValueClauseSyntax equalsValue;
	}
}
