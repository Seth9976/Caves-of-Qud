using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000318 RID: 792
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConstructorConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06004468 RID: 17512 RVA: 0x00114D61 File Offset: 0x00112F61
		internal ConstructorConstraintSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x00114D6C File Offset: 0x00112F6C
		public SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorConstraintSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x00114D8B File Offset: 0x00112F8B
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorConstraintSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x00114DB1 File Offset: 0x00112FB1
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorConstraintSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x00114DD7 File Offset: 0x00112FD7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x00114DDA File Offset: 0x00112FDA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x00114DDD File Offset: 0x00112FDD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorConstraint(this);
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x00114DE6 File Offset: 0x00112FE6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorConstraint(this);
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x00114DF0 File Offset: 0x00112FF0
		public ConstructorConstraintSyntax Update(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
		{
			if (!(newKeyword != this.NewKeyword) && !(openParenToken != this.OpenParenToken) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ConstructorConstraintSyntax constructorConstraintSyntax = SyntaxFactory.ConstructorConstraint(newKeyword, openParenToken, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return constructorConstraintSyntax;
			}
			return constructorConstraintSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x00114E49 File Offset: 0x00113049
		public ConstructorConstraintSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.OpenParenToken, this.CloseParenToken);
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00114E5E File Offset: 0x0011305E
		public ConstructorConstraintSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.NewKeyword, openParenToken, this.CloseParenToken);
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00114E73 File Offset: 0x00113073
		public ConstructorConstraintSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.NewKeyword, this.OpenParenToken, closeParenToken);
		}
	}
}
