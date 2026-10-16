using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000320 RID: 800
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConstructorInitializerSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004510 RID: 17680 RVA: 0x00116355 File Offset: 0x00114555
		internal ConstructorInitializerSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x00116360 File Offset: 0x00114560
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorInitializerSyntax)base.Green).colonToken, base.Position, 0);
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x0011637F File Offset: 0x0011457F
		public SyntaxToken ThisOrBaseKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorInitializerSyntax)base.Green).thisOrBaseKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x001163A5 File Offset: 0x001145A5
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 2);
			}
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x001163B4 File Offset: 0x001145B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 2);
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x001163C9 File Offset: 0x001145C9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.argumentList;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x001163D7 File Offset: 0x001145D7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorInitializer(this);
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x001163E0 File Offset: 0x001145E0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorInitializer(this);
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x001163EC File Offset: 0x001145EC
		public ConstructorInitializerSyntax Update(SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
		{
			if (!(colonToken != this.ColonToken) && !(thisOrBaseKeyword != this.ThisOrBaseKeyword) && argumentList == this.ArgumentList)
			{
				return this;
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = SyntaxFactory.ConstructorInitializer(base.Kind(), colonToken, thisOrBaseKeyword, argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return constructorInitializerSyntax;
			}
			return constructorInitializerSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00116446 File Offset: 0x00114646
		public ConstructorInitializerSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(colonToken, this.ThisOrBaseKeyword, this.ArgumentList);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0011645B File Offset: 0x0011465B
		public ConstructorInitializerSyntax WithThisOrBaseKeyword(SyntaxToken thisOrBaseKeyword)
		{
			return this.Update(this.ColonToken, thisOrBaseKeyword, this.ArgumentList);
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00116470 File Offset: 0x00114670
		public ConstructorInitializerSyntax WithArgumentList(ArgumentListSyntax argumentList)
		{
			return this.Update(this.ColonToken, this.ThisOrBaseKeyword, argumentList);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00116488 File Offset: 0x00114688
		public ConstructorInitializerSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001507 RID: 5383
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArgumentListSyntax argumentList;
	}
}
