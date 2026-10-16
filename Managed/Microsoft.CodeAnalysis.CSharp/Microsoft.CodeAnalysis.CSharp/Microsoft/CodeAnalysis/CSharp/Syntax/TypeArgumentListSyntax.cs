using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200028F RID: 655
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003DC9 RID: 15817 RVA: 0x0010940E File Offset: 0x0010760E
		internal TypeArgumentListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x00109419 File Offset: 0x00107619
		public SyntaxToken LessThanToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeArgumentListSyntax)base.Green).lessThanToken, base.Position, 0);
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x00109438 File Offset: 0x00107638
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.arguments, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<TypeSyntax>);
				}
				return new SeparatedSyntaxList<TypeSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x0010946D File Offset: 0x0010766D
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeArgumentListSyntax)base.Green).greaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00109493 File Offset: 0x00107693
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.arguments, 1);
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x001094A8 File Offset: 0x001076A8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.arguments;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x001094B6 File Offset: 0x001076B6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeArgumentList(this);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x001094BF File Offset: 0x001076BF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeArgumentList(this);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x001094C8 File Offset: 0x001076C8
		public TypeArgumentListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
		{
			if (!(lessThanToken != this.LessThanToken) && !(arguments != this.Arguments) && !(greaterThanToken != this.GreaterThanToken))
			{
				return this;
			}
			TypeArgumentListSyntax typeArgumentListSyntax = SyntaxFactory.TypeArgumentList(lessThanToken, arguments, greaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeArgumentListSyntax;
			}
			return typeArgumentListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00109521 File Offset: 0x00107721
		public TypeArgumentListSyntax WithLessThanToken(SyntaxToken lessThanToken)
		{
			return this.Update(lessThanToken, this.Arguments, this.GreaterThanToken);
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00109536 File Offset: 0x00107736
		public TypeArgumentListSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeSyntax> arguments)
		{
			return this.Update(this.LessThanToken, arguments, this.GreaterThanToken);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0010954B File Offset: 0x0010774B
		public TypeArgumentListSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
		{
			return this.Update(this.LessThanToken, this.Arguments, greaterThanToken);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x00109560 File Offset: 0x00107760
		public TypeArgumentListSyntax AddArguments(params TypeSyntax[] items)
		{
			return this.WithArguments(this.Arguments.AddRange(items));
		}

		// Token: 0x0400144C RID: 5196
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arguments;
	}
}
