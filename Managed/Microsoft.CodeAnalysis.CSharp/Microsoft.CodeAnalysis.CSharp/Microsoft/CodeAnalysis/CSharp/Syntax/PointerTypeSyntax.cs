using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000292 RID: 658
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PointerTypeSyntax : TypeSyntax
	{
		// Token: 0x06003DE9 RID: 15849 RVA: 0x00109736 File Offset: 0x00107936
		internal PointerTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x00109741 File Offset: 0x00107941
		public TypeSyntax ElementType
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.elementType);
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x0010974F File Offset: 0x0010794F
		public SyntaxToken AsteriskToken
		{
			get
			{
				return new SyntaxToken(this, ((PointerTypeSyntax)base.Green).asteriskToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00109775 File Offset: 0x00107975
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.elementType);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00109788 File Offset: 0x00107988
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.elementType;
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00109795 File Offset: 0x00107995
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPointerType(this);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0010979E File Offset: 0x0010799E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPointerType(this);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001097A8 File Offset: 0x001079A8
		public PointerTypeSyntax Update(TypeSyntax elementType, SyntaxToken asteriskToken)
		{
			if (elementType == this.ElementType && !(asteriskToken != this.AsteriskToken))
			{
				return this;
			}
			PointerTypeSyntax pointerTypeSyntax = SyntaxFactory.PointerType(elementType, asteriskToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return pointerTypeSyntax;
			}
			return pointerTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x001097ED File Offset: 0x001079ED
		public PointerTypeSyntax WithElementType(TypeSyntax elementType)
		{
			return this.Update(elementType, this.AsteriskToken);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x001097FC File Offset: 0x001079FC
		public PointerTypeSyntax WithAsteriskToken(SyntaxToken asteriskToken)
		{
			return this.Update(this.ElementType, asteriskToken);
		}

		// Token: 0x0400144F RID: 5199
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax elementType;
	}
}
