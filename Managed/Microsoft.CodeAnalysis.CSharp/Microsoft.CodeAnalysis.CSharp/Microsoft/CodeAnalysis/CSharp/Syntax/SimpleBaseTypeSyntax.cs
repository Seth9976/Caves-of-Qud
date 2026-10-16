using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000314 RID: 788
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SimpleBaseTypeSyntax : BaseTypeSyntax
	{
		// Token: 0x06004443 RID: 17475 RVA: 0x0011497A File Offset: 0x00112B7A
		internal SimpleBaseTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x00114985 File Offset: 0x00112B85
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x00114993 File Offset: 0x00112B93
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.type);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x001149A6 File Offset: 0x00112BA6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x001149B3 File Offset: 0x00112BB3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSimpleBaseType(this);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x001149BC File Offset: 0x00112BBC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSimpleBaseType(this);
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x001149C8 File Offset: 0x00112BC8
		public SimpleBaseTypeSyntax Update(TypeSyntax type)
		{
			if (type == this.Type)
			{
				return this;
			}
			SimpleBaseTypeSyntax simpleBaseTypeSyntax = SyntaxFactory.SimpleBaseType(type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return simpleBaseTypeSyntax;
			}
			return simpleBaseTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x001149FE File Offset: 0x00112BFE
		internal override BaseTypeSyntax WithTypeCore(TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x00114A07 File Offset: 0x00112C07
		public new SimpleBaseTypeSyntax WithType(TypeSyntax type)
		{
			return this.Update(type);
		}

		// Token: 0x040014F2 RID: 5362
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
