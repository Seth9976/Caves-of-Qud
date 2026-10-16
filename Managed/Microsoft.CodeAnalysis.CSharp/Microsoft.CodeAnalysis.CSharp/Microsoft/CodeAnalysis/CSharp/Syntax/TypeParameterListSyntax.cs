using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030B RID: 779
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x001124B3 File Offset: 0x001106B3
		internal TypeParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x001124BE File Offset: 0x001106BE
		public SyntaxToken LessThanToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeParameterListSyntax)base.Green).lessThanToken, base.Position, 0);
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x001124E0 File Offset: 0x001106E0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.parameters, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<TypeParameterSyntax>);
				}
				return new SeparatedSyntaxList<TypeParameterSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x00112515 File Offset: 0x00110715
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeParameterListSyntax)base.Green).greaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x0011253B File Offset: 0x0011073B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x00112550 File Offset: 0x00110750
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x0011255E File Offset: 0x0011075E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameterList(this);
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x00112567 File Offset: 0x00110767
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameterList(this);
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x00112570 File Offset: 0x00110770
		public TypeParameterListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (!(lessThanToken != this.LessThanToken) && !(parameters != this.Parameters) && !(greaterThanToken != this.GreaterThanToken))
			{
				return this;
			}
			TypeParameterListSyntax typeParameterListSyntax = SyntaxFactory.TypeParameterList(lessThanToken, parameters, greaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeParameterListSyntax;
			}
			return typeParameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x001125C9 File Offset: 0x001107C9
		public TypeParameterListSyntax WithLessThanToken(SyntaxToken lessThanToken)
		{
			return this.Update(lessThanToken, this.Parameters, this.GreaterThanToken);
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x001125DE File Offset: 0x001107DE
		public TypeParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterSyntax> parameters)
		{
			return this.Update(this.LessThanToken, parameters, this.GreaterThanToken);
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x001125F3 File Offset: 0x001107F3
		public TypeParameterListSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
		{
			return this.Update(this.LessThanToken, this.Parameters, greaterThanToken);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x00112608 File Offset: 0x00110808
		public TypeParameterListSyntax AddParameters(params TypeParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x040014DD RID: 5341
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
