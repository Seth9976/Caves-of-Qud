using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E0 RID: 736
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class VarPatternSyntax : PatternSyntax
	{
		// Token: 0x06004157 RID: 16727 RVA: 0x0010F482 File Offset: 0x0010D682
		internal VarPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0010F48D File Offset: 0x0010D68D
		public SyntaxToken VarKeyword
		{
			get
			{
				return new SyntaxToken(this, ((VarPatternSyntax)base.Green).varKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x0010F4AC File Offset: 0x0010D6AC
		public VariableDesignationSyntax Designation
		{
			get
			{
				return base.GetRed<VariableDesignationSyntax>(ref this.designation, 1);
			}
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x0010F4BB File Offset: 0x0010D6BB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<VariableDesignationSyntax>(ref this.designation, 1);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0010F4D0 File Offset: 0x0010D6D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.designation;
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x0010F4DE File Offset: 0x0010D6DE
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVarPattern(this);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0010F4E7 File Offset: 0x0010D6E7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVarPattern(this);
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x0010F4F0 File Offset: 0x0010D6F0
		public VarPatternSyntax Update(SyntaxToken varKeyword, VariableDesignationSyntax designation)
		{
			if (!(varKeyword != this.VarKeyword) && designation == this.Designation)
			{
				return this;
			}
			VarPatternSyntax varPatternSyntax = SyntaxFactory.VarPattern(varKeyword, designation);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return varPatternSyntax;
			}
			return varPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0010F535 File Offset: 0x0010D735
		public VarPatternSyntax WithVarKeyword(SyntaxToken varKeyword)
		{
			return this.Update(varKeyword, this.Designation);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0010F544 File Offset: 0x0010D744
		public VarPatternSyntax WithDesignation(VariableDesignationSyntax designation)
		{
			return this.Update(this.VarKeyword, designation);
		}

		// Token: 0x040014AB RID: 5291
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private VariableDesignationSyntax designation;
	}
}
