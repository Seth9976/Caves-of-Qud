using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032B RID: 811
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class QualifiedCrefSyntax : CrefSyntax
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x00116E3F File Offset: 0x0011503F
		internal QualifiedCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x00116E4A File Offset: 0x0011504A
		public TypeSyntax Container
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.container);
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x00116E58 File Offset: 0x00115058
		public SyntaxToken DotToken
		{
			get
			{
				return new SyntaxToken(this, ((QualifiedCrefSyntax)base.Green).dotToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00116E7E File Offset: 0x0011507E
		public MemberCrefSyntax Member
		{
			get
			{
				return base.GetRed<MemberCrefSyntax>(ref this.member, 2);
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00116E90 File Offset: 0x00115090
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
					syntaxNode = base.GetRed<MemberCrefSyntax>(ref this.member, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.container);
			}
			return syntaxNode;
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x00116EC8 File Offset: 0x001150C8
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
					syntaxNode = this.member;
				}
			}
			else
			{
				syntaxNode = this.container;
			}
			return syntaxNode;
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00116EF3 File Offset: 0x001150F3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQualifiedCref(this);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00116EFC File Offset: 0x001150FC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQualifiedCref(this);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x00116F08 File Offset: 0x00115108
		public QualifiedCrefSyntax Update(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
		{
			if (container == this.Container && !(dotToken != this.DotToken) && member == this.Member)
			{
				return this;
			}
			QualifiedCrefSyntax qualifiedCrefSyntax = SyntaxFactory.QualifiedCref(container, dotToken, member);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return qualifiedCrefSyntax;
			}
			return qualifiedCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00116F57 File Offset: 0x00115157
		public QualifiedCrefSyntax WithContainer(TypeSyntax container)
		{
			return this.Update(container, this.DotToken, this.Member);
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x00116F6C File Offset: 0x0011516C
		public QualifiedCrefSyntax WithDotToken(SyntaxToken dotToken)
		{
			return this.Update(this.Container, dotToken, this.Member);
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x00116F81 File Offset: 0x00115181
		public QualifiedCrefSyntax WithMember(MemberCrefSyntax member)
		{
			return this.Update(this.Container, this.DotToken, member);
		}

		// Token: 0x04001511 RID: 5393
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax container;

		// Token: 0x04001512 RID: 5394
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private MemberCrefSyntax member;
	}
}
