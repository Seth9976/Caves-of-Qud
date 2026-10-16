using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000308 RID: 776
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AttributeArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004322 RID: 17186 RVA: 0x001120FE File Offset: 0x001102FE
		internal AttributeArgumentListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x00112109 File Offset: 0x00110309
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((AttributeArgumentListSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x00112128 File Offset: 0x00110328
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AttributeArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.arguments, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<AttributeArgumentSyntax>);
				}
				return new SeparatedSyntaxList<AttributeArgumentSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x0011215D File Offset: 0x0011035D
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((AttributeArgumentListSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x00112183 File Offset: 0x00110383
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.arguments, 1);
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x00112198 File Offset: 0x00110398
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.arguments;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x001121A6 File Offset: 0x001103A6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeArgumentList(this);
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x001121AF File Offset: 0x001103AF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeArgumentList(this);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x001121B8 File Offset: 0x001103B8
		public AttributeArgumentListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(arguments != this.Arguments) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax = SyntaxFactory.AttributeArgumentList(openParenToken, arguments, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return attributeArgumentListSyntax;
			}
			return attributeArgumentListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x00112211 File Offset: 0x00110411
		public AttributeArgumentListSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Arguments, this.CloseParenToken);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x00112226 File Offset: 0x00110426
		public AttributeArgumentListSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeArgumentSyntax> arguments)
		{
			return this.Update(this.OpenParenToken, arguments, this.CloseParenToken);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0011223B File Offset: 0x0011043B
		public AttributeArgumentListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Arguments, closeParenToken);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x00112250 File Offset: 0x00110450
		public AttributeArgumentListSyntax AddArguments(params AttributeArgumentSyntax[] items)
		{
			return this.WithArguments(this.Arguments.AddRange(items));
		}

		// Token: 0x040014D8 RID: 5336
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arguments;
	}
}
