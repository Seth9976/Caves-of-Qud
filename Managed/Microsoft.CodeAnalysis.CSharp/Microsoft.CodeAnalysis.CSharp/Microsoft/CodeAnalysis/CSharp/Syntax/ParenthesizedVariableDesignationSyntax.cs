using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F7 RID: 759
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParenthesizedVariableDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x06004237 RID: 16951 RVA: 0x00110A3C File Offset: 0x0010EC3C
		internal ParenthesizedVariableDesignationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x00110A47 File Offset: 0x0010EC47
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedVariableDesignationSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x00110A68 File Offset: 0x0010EC68
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<VariableDesignationSyntax> Variables
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.variables, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<VariableDesignationSyntax>);
				}
				return new SeparatedSyntaxList<VariableDesignationSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x00110A9D File Offset: 0x0010EC9D
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedVariableDesignationSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00110AC3 File Offset: 0x0010ECC3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.variables, 1);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00110AD8 File Offset: 0x0010ECD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.variables;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x00110AE6 File Offset: 0x0010ECE6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedVariableDesignation(this);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x00110AEF File Offset: 0x0010ECEF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedVariableDesignation(this);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00110AF8 File Offset: 0x0010ECF8
		public ParenthesizedVariableDesignationSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(variables != this.Variables) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax = SyntaxFactory.ParenthesizedVariableDesignation(openParenToken, variables, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parenthesizedVariableDesignationSyntax;
			}
			return parenthesizedVariableDesignationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x00110B51 File Offset: 0x0010ED51
		public ParenthesizedVariableDesignationSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Variables, this.CloseParenToken);
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00110B66 File Offset: 0x0010ED66
		public ParenthesizedVariableDesignationSyntax WithVariables([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDesignationSyntax> variables)
		{
			return this.Update(this.OpenParenToken, variables, this.CloseParenToken);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00110B7B File Offset: 0x0010ED7B
		public ParenthesizedVariableDesignationSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Variables, closeParenToken);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00110B90 File Offset: 0x0010ED90
		public ParenthesizedVariableDesignationSyntax AddVariables(params VariableDesignationSyntax[] items)
		{
			return this.WithVariables(this.Variables.AddRange(items));
		}

		// Token: 0x040014C4 RID: 5316
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode variables;
	}
}
