using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F6 RID: 758
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DiscardDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x0600422F RID: 16943 RVA: 0x001109B4 File Offset: 0x0010EBB4
		internal DiscardDesignationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x001109BF File Offset: 0x0010EBBF
		public SyntaxToken UnderscoreToken
		{
			get
			{
				return new SyntaxToken(this, ((DiscardDesignationSyntax)base.Green).underscoreToken, base.Position, 0);
			}
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x001109DE File Offset: 0x0010EBDE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x001109E1 File Offset: 0x0010EBE1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x001109E4 File Offset: 0x0010EBE4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDiscardDesignation(this);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x001109ED File Offset: 0x0010EBED
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDiscardDesignation(this);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x001109F8 File Offset: 0x0010EBF8
		public DiscardDesignationSyntax Update(SyntaxToken underscoreToken)
		{
			if (!(underscoreToken != this.UnderscoreToken))
			{
				return this;
			}
			DiscardDesignationSyntax discardDesignationSyntax = SyntaxFactory.DiscardDesignation(underscoreToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return discardDesignationSyntax;
			}
			return discardDesignationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x00110A33 File Offset: 0x0010EC33
		public DiscardDesignationSyntax WithUnderscoreToken(SyntaxToken underscoreToken)
		{
			return this.Update(underscoreToken);
		}
	}
}
