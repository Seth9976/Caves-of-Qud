using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DF RID: 735
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DeclarationPatternSyntax : PatternSyntax
	{
		// Token: 0x0600414D RID: 16717 RVA: 0x0010F384 File Offset: 0x0010D584
		internal DeclarationPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x0010F38F File Offset: 0x0010D58F
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0010F39D File Offset: 0x0010D59D
		public VariableDesignationSyntax Designation
		{
			get
			{
				return base.GetRed<VariableDesignationSyntax>(ref this.designation, 1);
			}
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0010F3AC File Offset: 0x0010D5AC
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

		// Token: 0x06004151 RID: 16721 RVA: 0x0010F3E4 File Offset: 0x0010D5E4
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

		// Token: 0x06004152 RID: 16722 RVA: 0x0010F40F File Offset: 0x0010D60F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x0010F418 File Offset: 0x0010D618
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x0010F424 File Offset: 0x0010D624
		public DeclarationPatternSyntax Update(TypeSyntax type, VariableDesignationSyntax designation)
		{
			if (type == this.Type && designation == this.Designation)
			{
				return this;
			}
			DeclarationPatternSyntax declarationPatternSyntax = SyntaxFactory.DeclarationPattern(type, designation);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return declarationPatternSyntax;
			}
			return declarationPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x0010F464 File Offset: 0x0010D664
		public DeclarationPatternSyntax WithType(TypeSyntax type)
		{
			return this.Update(type, this.Designation);
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x0010F473 File Offset: 0x0010D673
		public DeclarationPatternSyntax WithDesignation(VariableDesignationSyntax designation)
		{
			return this.Update(this.Type, designation);
		}

		// Token: 0x040014A9 RID: 5289
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040014AA RID: 5290
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDesignationSyntax designation;
	}
}
