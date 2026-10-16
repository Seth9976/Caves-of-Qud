using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002EC RID: 748
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterpolatedStringTextSyntax : InterpolatedStringContentSyntax
	{
		// Token: 0x060041D2 RID: 16850 RVA: 0x001100E6 File Offset: 0x0010E2E6
		internal InterpolatedStringTextSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x001100F1 File Offset: 0x0010E2F1
		public SyntaxToken TextToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolatedStringTextSyntax)base.Green).textToken, base.Position, 0);
			}
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x00110110 File Offset: 0x0010E310
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x00110113 File Offset: 0x0010E313
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x00110116 File Offset: 0x0010E316
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolatedStringText(this);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x0011011F File Offset: 0x0010E31F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolatedStringText(this);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00110128 File Offset: 0x0010E328
		public InterpolatedStringTextSyntax Update(SyntaxToken textToken)
		{
			if (!(textToken != this.TextToken))
			{
				return this;
			}
			InterpolatedStringTextSyntax interpolatedStringTextSyntax = SyntaxFactory.InterpolatedStringText(textToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interpolatedStringTextSyntax;
			}
			return interpolatedStringTextSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x00110163 File Offset: 0x0010E363
		public InterpolatedStringTextSyntax WithTextToken(SyntaxToken textToken)
		{
			return this.Update(textToken);
		}
	}
}
