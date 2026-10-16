using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000298 RID: 664
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NullableTypeSyntax : TypeSyntax
	{
		// Token: 0x06003E2F RID: 15919 RVA: 0x00109E78 File Offset: 0x00108078
		internal NullableTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x00109E83 File Offset: 0x00108083
		public TypeSyntax ElementType
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.elementType);
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x00109E91 File Offset: 0x00108091
		public SyntaxToken QuestionToken
		{
			get
			{
				return new SyntaxToken(this, ((NullableTypeSyntax)base.Green).questionToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x00109EB7 File Offset: 0x001080B7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.elementType);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00109ECA File Offset: 0x001080CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.elementType;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00109ED7 File Offset: 0x001080D7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNullableType(this);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x00109EE0 File Offset: 0x001080E0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNullableType(this);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00109EEC File Offset: 0x001080EC
		public NullableTypeSyntax Update(TypeSyntax elementType, SyntaxToken questionToken)
		{
			if (elementType == this.ElementType && !(questionToken != this.QuestionToken))
			{
				return this;
			}
			NullableTypeSyntax nullableTypeSyntax = SyntaxFactory.NullableType(elementType, questionToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return nullableTypeSyntax;
			}
			return nullableTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00109F31 File Offset: 0x00108131
		public NullableTypeSyntax WithElementType(TypeSyntax elementType)
		{
			return this.Update(elementType, this.QuestionToken);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00109F40 File Offset: 0x00108140
		public NullableTypeSyntax WithQuestionToken(SyntaxToken questionToken)
		{
			return this.Update(this.ElementType, questionToken);
		}

		// Token: 0x04001455 RID: 5205
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax elementType;
	}
}
