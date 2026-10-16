using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F1 RID: 753
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class VariableDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x00110569 File Offset: 0x0010E769
		internal VariableDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00110574 File Offset: 0x0010E774
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x00110584 File Offset: 0x0010E784
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<VariableDeclaratorSyntax> Variables
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.variables, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<VariableDeclaratorSyntax>);
				}
				return new SeparatedSyntaxList<VariableDeclaratorSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x001105BC File Offset: 0x0010E7BC
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
					syntaxNode = base.GetRed(ref this.variables, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
			return syntaxNode;
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x001105F4 File Offset: 0x0010E7F4
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
					syntaxNode = this.variables;
				}
			}
			else
			{
				syntaxNode = this.type;
			}
			return syntaxNode;
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x0011061F File Offset: 0x0010E81F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVariableDeclaration(this);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x00110628 File Offset: 0x0010E828
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVariableDeclaration(this);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x00110634 File Offset: 0x0010E834
		public VariableDeclarationSyntax Update(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
		{
			if (type == this.Type && !(variables != this.Variables))
			{
				return this;
			}
			VariableDeclarationSyntax variableDeclarationSyntax = SyntaxFactory.VariableDeclaration(type, variables);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return variableDeclarationSyntax;
			}
			return variableDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x00110679 File Offset: 0x0010E879
		public VariableDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(type, this.Variables);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x00110688 File Offset: 0x0010E888
		public VariableDeclarationSyntax WithVariables([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
		{
			return this.Update(this.Type, variables);
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x00110698 File Offset: 0x0010E898
		public VariableDeclarationSyntax AddVariables(params VariableDeclaratorSyntax[] items)
		{
			return this.WithVariables(this.Variables.AddRange(items));
		}

		// Token: 0x040014BF RID: 5311
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040014C0 RID: 5312
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode variables;
	}
}
