using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000277 RID: 631
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class QualifiedNameSyntax : NameSyntax
	{
		// Token: 0x06003C0D RID: 15373 RVA: 0x0010459D File Offset: 0x0010279D
		internal override SimpleNameSyntax GetUnqualifiedName()
		{
			return this.Right;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x001045A5 File Offset: 0x001027A5
		internal override string ErrorDisplayName()
		{
			return this.Left.ErrorDisplayName() + "." + this.Right.ErrorDisplayName();
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x001045C7 File Offset: 0x001027C7
		internal QualifiedNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06003C10 RID: 15376 RVA: 0x001045D2 File Offset: 0x001027D2
		public NameSyntax Left
		{
			get
			{
				return base.GetRedAtZero<NameSyntax>(ref this.left);
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x001045E0 File Offset: 0x001027E0
		public SyntaxToken DotToken
		{
			get
			{
				return new SyntaxToken(this, ((QualifiedNameSyntax)base.Green).dotToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x00104606 File Offset: 0x00102806
		public SimpleNameSyntax Right
		{
			get
			{
				return base.GetRed<SimpleNameSyntax>(ref this.right, 2);
			}
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00104618 File Offset: 0x00102818
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			NameSyntax nameSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					nameSyntax = null;
				}
				else
				{
					nameSyntax = base.GetRed<SimpleNameSyntax>(ref this.right, 2);
				}
			}
			else
			{
				nameSyntax = base.GetRedAtZero<NameSyntax>(ref this.left);
			}
			return nameSyntax;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00104650 File Offset: 0x00102850
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			NameSyntax nameSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					nameSyntax = null;
				}
				else
				{
					nameSyntax = this.right;
				}
			}
			else
			{
				nameSyntax = this.left;
			}
			return nameSyntax;
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0010467B File Offset: 0x0010287B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQualifiedName(this);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00104684 File Offset: 0x00102884
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQualifiedName(this);
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00104690 File Offset: 0x00102890
		public QualifiedNameSyntax Update(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
		{
			if (left == this.Left && !(dotToken != this.DotToken) && right == this.Right)
			{
				return this;
			}
			QualifiedNameSyntax qualifiedNameSyntax = SyntaxFactory.QualifiedName(left, dotToken, right);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return qualifiedNameSyntax;
			}
			return qualifiedNameSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x001046DF File Offset: 0x001028DF
		public QualifiedNameSyntax WithLeft(NameSyntax left)
		{
			return this.Update(left, this.DotToken, this.Right);
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x001046F4 File Offset: 0x001028F4
		public QualifiedNameSyntax WithDotToken(SyntaxToken dotToken)
		{
			return this.Update(this.Left, dotToken, this.Right);
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00104709 File Offset: 0x00102909
		public QualifiedNameSyntax WithRight(SimpleNameSyntax right)
		{
			return this.Update(this.Left, this.DotToken, right);
		}

		// Token: 0x04001413 RID: 5139
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameSyntax left;

		// Token: 0x04001414 RID: 5140
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SimpleNameSyntax right;
	}
}
