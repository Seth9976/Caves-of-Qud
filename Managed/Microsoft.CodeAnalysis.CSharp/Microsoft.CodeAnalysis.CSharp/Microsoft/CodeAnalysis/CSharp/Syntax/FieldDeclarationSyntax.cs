using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031B RID: 795
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FieldDeclarationSyntax : BaseFieldDeclarationSyntax
	{
		// Token: 0x06004484 RID: 17540 RVA: 0x00114F9C File Offset: 0x0011319C
		internal FieldDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x00114FA7 File Offset: 0x001131A7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x00114FBC File Offset: 0x001131BC
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

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x00114FF8 File Offset: 0x001131F8
		public override VariableDeclarationSyntax Declaration
		{
			get
			{
				return base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 2);
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x00115007 File Offset: 0x00113207
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((FieldDeclarationSyntax)base.Green).semicolonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x00115030 File Offset: 0x00113230
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
					syntaxNode = base.GetRed<VariableDeclarationSyntax>(ref this.declaration, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x00115068 File Offset: 0x00113268
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
					syntaxNode = this.declaration;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00115093 File Offset: 0x00113293
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFieldDeclaration(this);
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x0011509C File Offset: 0x0011329C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFieldDeclaration(this);
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x001150A8 File Offset: 0x001132A8
		public FieldDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && declaration == this.Declaration && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			FieldDeclarationSyntax fieldDeclarationSyntax = SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return fieldDeclarationSyntax;
			}
			return fieldDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x0011510D File Offset: 0x0011330D
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x00115116 File Offset: 0x00113316
		public new FieldDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x00115131 File Offset: 0x00113331
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x0011513A File Offset: 0x0011333A
		public new FieldDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Declaration, this.SemicolonToken);
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x00115155 File Offset: 0x00113355
		internal override BaseFieldDeclarationSyntax WithDeclarationCore(VariableDeclarationSyntax declaration)
		{
			return this.WithDeclaration(declaration);
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x0011515E File Offset: 0x0011335E
		public new FieldDeclarationSyntax WithDeclaration(VariableDeclarationSyntax declaration)
		{
			return this.Update(this.AttributeLists, this.Modifiers, declaration, this.SemicolonToken);
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x00115179 File Offset: 0x00113379
		internal override BaseFieldDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x00115182 File Offset: 0x00113382
		public new FieldDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Declaration, semicolonToken);
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x0011519D File Offset: 0x0011339D
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x001151A8 File Offset: 0x001133A8
		public new FieldDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x001151CA File Offset: 0x001133CA
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x001151D4 File Offset: 0x001133D4
		public new FieldDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x001151F6 File Offset: 0x001133F6
		internal override BaseFieldDeclarationSyntax AddDeclarationVariablesCore(params VariableDeclaratorSyntax[] items)
		{
			return this.AddDeclarationVariables(items);
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x00115200 File Offset: 0x00113400
		public new FieldDeclarationSyntax AddDeclarationVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.WithDeclaration(this.Declaration.WithVariables(this.Declaration.Variables.AddRange(items)));
		}

		// Token: 0x040014F8 RID: 5368
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014F9 RID: 5369
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDeclarationSyntax declaration;
	}
}
