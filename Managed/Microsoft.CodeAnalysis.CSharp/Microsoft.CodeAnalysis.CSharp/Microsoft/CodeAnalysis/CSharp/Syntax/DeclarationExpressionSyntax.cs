using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BD RID: 701
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DeclarationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003FBC RID: 16316 RVA: 0x0010C6DB File Offset: 0x0010A8DB
		internal DeclarationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x0010C6E6 File Offset: 0x0010A8E6
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0010C6F4 File Offset: 0x0010A8F4
		public VariableDesignationSyntax Designation
		{
			get
			{
				return base.GetRed<VariableDesignationSyntax>(ref this.designation, 1);
			}
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x0010C704 File Offset: 0x0010A904
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<VariableDesignationSyntax>(ref this.designation, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
			return syntaxNode;
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0010C73C File Offset: 0x0010A93C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.designation;
				}
			}
			else
			{
				syntaxNode = this.type;
			}
			return syntaxNode;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0010C767 File Offset: 0x0010A967
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDeclarationExpression(this);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x0010C770 File Offset: 0x0010A970
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDeclarationExpression(this);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0010C77C File Offset: 0x0010A97C
		public DeclarationExpressionSyntax Update(TypeSyntax type, VariableDesignationSyntax designation)
		{
			if (type == this.Type && designation == this.Designation)
			{
				return this;
			}
			DeclarationExpressionSyntax declarationExpressionSyntax = SyntaxFactory.DeclarationExpression(type, designation);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return declarationExpressionSyntax;
			}
			return declarationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x0010C7BC File Offset: 0x0010A9BC
		public DeclarationExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(type, this.Designation);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x0010C7CB File Offset: 0x0010A9CB
		public DeclarationExpressionSyntax WithDesignation(VariableDesignationSyntax designation)
		{
			return this.Update(this.Type, designation);
		}

		// Token: 0x0400147C RID: 5244
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x0400147D RID: 5245
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDesignationSyntax designation;
	}
}
