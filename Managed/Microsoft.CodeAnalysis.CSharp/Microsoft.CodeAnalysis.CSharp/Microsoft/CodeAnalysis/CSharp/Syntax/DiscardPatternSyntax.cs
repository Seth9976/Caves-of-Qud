using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DE RID: 734
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DiscardPatternSyntax : PatternSyntax
	{
		// Token: 0x06004145 RID: 16709 RVA: 0x0010F2FE File Offset: 0x0010D4FE
		internal DiscardPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x0010F309 File Offset: 0x0010D509
		public SyntaxToken UnderscoreToken
		{
			get
			{
				return new SyntaxToken(this, ((DiscardPatternSyntax)base.Green).underscoreToken, base.Position, 0);
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0010F328 File Offset: 0x0010D528
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0010F32B File Offset: 0x0010D52B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0010F32E File Offset: 0x0010D52E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDiscardPattern(this);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x0010F337 File Offset: 0x0010D537
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDiscardPattern(this);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0010F340 File Offset: 0x0010D540
		public DiscardPatternSyntax Update(SyntaxToken underscoreToken)
		{
			if (!(underscoreToken != this.UnderscoreToken))
			{
				return this;
			}
			DiscardPatternSyntax discardPatternSyntax = SyntaxFactory.DiscardPattern(underscoreToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return discardPatternSyntax;
			}
			return discardPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x0010F37B File Offset: 0x0010D57B
		public DiscardPatternSyntax WithUnderscoreToken(SyntaxToken underscoreToken)
		{
			return this.Update(underscoreToken);
		}
	}
}
