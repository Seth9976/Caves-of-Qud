using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000299 RID: 665
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TupleTypeSyntax : TypeSyntax
	{
		// Token: 0x06003E39 RID: 15929 RVA: 0x00109F4F File Offset: 0x0010814F
		internal TupleTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x00109F5A File Offset: 0x0010815A
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TupleTypeSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x00109F7C File Offset: 0x0010817C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TupleElementSyntax> Elements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.elements, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<TupleElementSyntax>);
				}
				return new SeparatedSyntaxList<TupleElementSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x00109FB1 File Offset: 0x001081B1
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TupleTypeSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00109FD7 File Offset: 0x001081D7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.elements, 1);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00109FEC File Offset: 0x001081EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.elements;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00109FFA File Offset: 0x001081FA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleType(this);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0010A003 File Offset: 0x00108203
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleType(this);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0010A00C File Offset: 0x0010820C
		public TupleTypeSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(elements != this.Elements) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			TupleTypeSyntax tupleTypeSyntax = SyntaxFactory.TupleType(openParenToken, elements, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return tupleTypeSyntax;
			}
			return tupleTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0010A065 File Offset: 0x00108265
		public TupleTypeSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Elements, this.CloseParenToken);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0010A07A File Offset: 0x0010827A
		public TupleTypeSyntax WithElements([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TupleElementSyntax> elements)
		{
			return this.Update(this.OpenParenToken, elements, this.CloseParenToken);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0010A08F File Offset: 0x0010828F
		public TupleTypeSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Elements, closeParenToken);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0010A0A4 File Offset: 0x001082A4
		public TupleTypeSyntax AddElements(params TupleElementSyntax[] items)
		{
			return this.WithElements(this.Elements.AddRange(items));
		}

		// Token: 0x04001456 RID: 5206
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode elements;
	}
}
