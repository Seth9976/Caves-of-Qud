using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C7 RID: 711
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004041 RID: 16449 RVA: 0x0010D466 File Offset: 0x0010B666
		internal ArrayCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x0010D471 File Offset: 0x0010B671
		public SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ArrayCreationExpressionSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x0010D490 File Offset: 0x0010B690
		public ArrayTypeSyntax Type
		{
			get
			{
				return base.GetRed<ArrayTypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x0010D49F File Offset: 0x0010B69F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
			}
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0010D4B0 File Offset: 0x0010B6B0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRed<ArrayTypeSyntax>(ref this.type, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0010D4EC File Offset: 0x0010B6EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.initializer;
				}
			}
			else
			{
				syntaxNode = this.type;
			}
			return syntaxNode;
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x0010D518 File Offset: 0x0010B718
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayCreationExpression(this);
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0010D521 File Offset: 0x0010B721
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayCreationExpression(this);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0010D52C File Offset: 0x0010B72C
		public ArrayCreationExpressionSyntax Update(SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			if (!(newKeyword != this.NewKeyword) && type == this.Type && initializer == this.Initializer)
			{
				return this;
			}
			ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression(newKeyword, type, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return arrayCreationExpressionSyntax;
			}
			return arrayCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0010D57B File Offset: 0x0010B77B
		public ArrayCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.Type, this.Initializer);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0010D590 File Offset: 0x0010B790
		public ArrayCreationExpressionSyntax WithType(ArrayTypeSyntax type)
		{
			return this.Update(this.NewKeyword, type, this.Initializer);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0010D5A5 File Offset: 0x0010B7A5
		public ArrayCreationExpressionSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.Update(this.NewKeyword, this.Type, initializer);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0010D5BC File Offset: 0x0010B7BC
		public ArrayCreationExpressionSyntax AddTypeRankSpecifiers(params ArrayRankSpecifierSyntax[] items)
		{
			return this.WithType(this.Type.WithRankSpecifiers(this.Type.RankSpecifiers.AddRange(items)));
		}

		// Token: 0x0400148C RID: 5260
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrayTypeSyntax type;

		// Token: 0x0400148D RID: 5261
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
