using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031C RID: 796
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EventFieldDeclarationSyntax : BaseFieldDeclarationSyntax
	{
		// Token: 0x0600449C RID: 17564 RVA: 0x00115232 File Offset: 0x00113432
		internal EventFieldDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x0011523D File Offset: 0x0011343D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x00115254 File Offset: 0x00113454
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

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x00115290 File Offset: 0x00113490
		public SyntaxToken EventKeyword
		{
			get
			{
				return new SyntaxToken(this, ((EventFieldDeclarationSyntax)base.Green).eventKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x001152B6 File Offset: 0x001134B6
		public override VariableDeclarationSyntax Declaration
		{
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x001152C5 File Offset: 0x001134C5
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((EventFieldDeclarationSyntax)base.Green).semicolonToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x001152EC File Offset: 0x001134EC
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
					syntaxNode = base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x00115324 File Offset: 0x00113524
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
					syntaxNode = this.declaration;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x0011534F File Offset: 0x0011354F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEventFieldDeclaration(this);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x00115358 File Offset: 0x00113558
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEventFieldDeclaration(this);
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00115364 File Offset: 0x00113564
		public EventFieldDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(eventKeyword != this.EventKeyword) && declaration == this.Declaration && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			EventFieldDeclarationSyntax eventFieldDeclarationSyntax = SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, eventKeyword, declaration, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return eventFieldDeclarationSyntax;
			}
			return eventFieldDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x001153DA File Offset: 0x001135DA
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x001153E3 File Offset: 0x001135E3
		public new EventFieldDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.EventKeyword, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x00115404 File Offset: 0x00113604
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x0011540D File Offset: 0x0011360D
		public new EventFieldDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.EventKeyword, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0011542E File Offset: 0x0011362E
		public EventFieldDeclarationSyntax WithEventKeyword(SyntaxToken eventKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, eventKeyword, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x0011544F File Offset: 0x0011364F
		internal override BaseFieldDeclarationSyntax WithDeclarationCore(VariableDeclarationSyntax declaration)
		{
			return this.WithDeclaration(declaration);
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x00115458 File Offset: 0x00113658
		public new EventFieldDeclarationSyntax WithDeclaration(VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, declaration, this.SemicolonToken);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x00115479 File Offset: 0x00113679
		internal override BaseFieldDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x00115482 File Offset: 0x00113682
		public new EventFieldDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.EventKeyword, this.Declaration, semicolonToken);
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x001154A3 File Offset: 0x001136A3
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x001154AC File Offset: 0x001136AC
		public new EventFieldDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x001154CE File Offset: 0x001136CE
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x001154D8 File Offset: 0x001136D8
		public new EventFieldDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x001154FA File Offset: 0x001136FA
		internal override BaseFieldDeclarationSyntax AddDeclarationVariablesCore(params VariableDeclaratorSyntax[] items)
		{
			return this.AddDeclarationVariables(items);
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x00115504 File Offset: 0x00113704
		public new EventFieldDeclarationSyntax AddDeclarationVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.WithDeclaration(this.Declaration.WithVariables(this.Declaration.Variables.AddRange(items)));
		}

		// Token: 0x040014FA RID: 5370
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014FB RID: 5371
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;
	}
}
