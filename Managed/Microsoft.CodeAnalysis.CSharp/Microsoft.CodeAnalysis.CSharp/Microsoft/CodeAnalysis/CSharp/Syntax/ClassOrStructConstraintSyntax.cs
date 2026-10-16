using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024D RID: 589
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ClassOrStructConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x0600384D RID: 14413 RVA: 0x000FA7A2 File Offset: 0x000F89A2
		public ClassOrStructConstraintSyntax Update(SyntaxToken classOrStructKeyword)
		{
			return this.Update(classOrStructKeyword, this.QuestionToken);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000FA7B1 File Offset: 0x000F89B1
		internal ClassOrStructConstraintSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000FA7BC File Offset: 0x000F89BC
		public SyntaxToken ClassOrStructKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ClassOrStructConstraintSyntax)base.Green).classOrStructKeyword, base.Position, 0);
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000FA7DC File Offset: 0x000F89DC
		public SyntaxToken QuestionToken
		{
			get
			{
				SyntaxToken questionToken = ((ClassOrStructConstraintSyntax)base.Green).questionToken;
				if (questionToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, questionToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000FA81C File Offset: 0x000F8A1C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000FA81F File Offset: 0x000F8A1F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000FA822 File Offset: 0x000F8A22
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitClassOrStructConstraint(this);
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000FA82B File Offset: 0x000F8A2B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitClassOrStructConstraint(this);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000FA834 File Offset: 0x000F8A34
		public ClassOrStructConstraintSyntax Update(SyntaxToken classOrStructKeyword, SyntaxToken questionToken)
		{
			if (!(classOrStructKeyword != this.ClassOrStructKeyword) && !(questionToken != this.QuestionToken))
			{
				return this;
			}
			ClassOrStructConstraintSyntax classOrStructConstraintSyntax = SyntaxFactory.ClassOrStructConstraint(base.Kind(), classOrStructKeyword, questionToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return classOrStructConstraintSyntax;
			}
			return classOrStructConstraintSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000FA884 File Offset: 0x000F8A84
		public ClassOrStructConstraintSyntax WithClassOrStructKeyword(SyntaxToken classOrStructKeyword)
		{
			return this.Update(classOrStructKeyword, this.QuestionToken);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000FA893 File Offset: 0x000F8A93
		public ClassOrStructConstraintSyntax WithQuestionToken(SyntaxToken questionToken)
		{
			return this.Update(this.ClassOrStructKeyword, questionToken);
		}
	}
}
