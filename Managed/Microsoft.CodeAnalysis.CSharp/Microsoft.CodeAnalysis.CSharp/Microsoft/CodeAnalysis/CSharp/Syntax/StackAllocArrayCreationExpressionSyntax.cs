using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027E RID: 638
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class StackAllocArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003CAA RID: 15530 RVA: 0x00105C06 File Offset: 0x00103E06
		public StackAllocArrayCreationExpressionSyntax Update(SyntaxToken stackAllocKeyword, TypeSyntax type)
		{
			return this.Update(this.StackAllocKeyword, type, this.Initializer);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00105C1B File Offset: 0x00103E1B
		internal StackAllocArrayCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00105C26 File Offset: 0x00103E26
		public SyntaxToken StackAllocKeyword
		{
			get
			{
				return new SyntaxToken(this, ((StackAllocArrayCreationExpressionSyntax)base.Green).stackAllocKeyword, base.Position, 0);
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x00105C45 File Offset: 0x00103E45
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x00105C54 File Offset: 0x00103E54
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00105C64 File Offset: 0x00103E64
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
				syntaxNode = base.GetRed<TypeSyntax>(ref this.type, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00105CA0 File Offset: 0x00103EA0
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

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00105CCC File Offset: 0x00103ECC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x00105CD5 File Offset: 0x00103ED5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00105CE0 File Offset: 0x00103EE0
		public StackAllocArrayCreationExpressionSyntax Update(SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			if (!(stackAllocKeyword != this.StackAllocKeyword) && type == this.Type && initializer == this.Initializer)
			{
				return this;
			}
			StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax = SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return stackAllocArrayCreationExpressionSyntax;
			}
			return stackAllocArrayCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x00105D2F File Offset: 0x00103F2F
		public StackAllocArrayCreationExpressionSyntax WithStackAllocKeyword(SyntaxToken stackAllocKeyword)
		{
			return this.Update(stackAllocKeyword, this.Type, this.Initializer);
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x00105D44 File Offset: 0x00103F44
		public StackAllocArrayCreationExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.StackAllocKeyword, type, this.Initializer);
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x00105D59 File Offset: 0x00103F59
		public StackAllocArrayCreationExpressionSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.Update(this.StackAllocKeyword, this.Type, initializer);
		}

		// Token: 0x04001422 RID: 5154
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001423 RID: 5155
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
