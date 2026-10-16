using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000290 RID: 656
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PredefinedTypeSyntax : TypeSyntax
	{
		// Token: 0x06003DD6 RID: 15830 RVA: 0x00109582 File Offset: 0x00107782
		internal PredefinedTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x0010958D File Offset: 0x0010778D
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((PredefinedTypeSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x001095AC File Offset: 0x001077AC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x001095AF File Offset: 0x001077AF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x001095B2 File Offset: 0x001077B2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPredefinedType(this);
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x001095BB File Offset: 0x001077BB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPredefinedType(this);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x001095C4 File Offset: 0x001077C4
		public PredefinedTypeSyntax Update(SyntaxToken keyword)
		{
			if (!(keyword != this.Keyword))
			{
				return this;
			}
			PredefinedTypeSyntax predefinedTypeSyntax = SyntaxFactory.PredefinedType(keyword);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return predefinedTypeSyntax;
			}
			return predefinedTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x001095FF File Offset: 0x001077FF
		public PredefinedTypeSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword);
		}
	}
}
