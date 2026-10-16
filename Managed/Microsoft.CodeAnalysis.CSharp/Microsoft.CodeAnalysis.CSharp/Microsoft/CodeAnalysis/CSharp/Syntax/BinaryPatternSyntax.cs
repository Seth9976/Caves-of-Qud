using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E9 RID: 745
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BinaryPatternSyntax : PatternSyntax
	{
		// Token: 0x060041BB RID: 16827 RVA: 0x0010FEAB File Offset: 0x0010E0AB
		internal BinaryPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x0010FEB6 File Offset: 0x0010E0B6
		public PatternSyntax Left
		{
			get
			{
				return base.GetRedAtZero<PatternSyntax>(ref this.left);
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x0010FEC4 File Offset: 0x0010E0C4
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((BinaryPatternSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x0010FEEA File Offset: 0x0010E0EA
		public PatternSyntax Right
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.right, 2);
			}
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x0010FEFC File Offset: 0x0010E0FC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			PatternSyntax patternSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					patternSyntax = null;
				}
				else
				{
					patternSyntax = base.GetRed<PatternSyntax>(ref this.right, 2);
				}
			}
			else
			{
				patternSyntax = base.GetRedAtZero<PatternSyntax>(ref this.left);
			}
			return patternSyntax;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x0010FF34 File Offset: 0x0010E134
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			PatternSyntax patternSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					patternSyntax = null;
				}
				else
				{
					patternSyntax = this.right;
				}
			}
			else
			{
				patternSyntax = this.left;
			}
			return patternSyntax;
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x0010FF5F File Offset: 0x0010E15F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBinaryPattern(this);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x0010FF68 File Offset: 0x0010E168
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBinaryPattern(this);
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x0010FF74 File Offset: 0x0010E174
		public BinaryPatternSyntax Update(PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
		{
			if (left == this.Left && !(operatorToken != this.OperatorToken) && right == this.Right)
			{
				return this;
			}
			BinaryPatternSyntax binaryPatternSyntax = SyntaxFactory.BinaryPattern(base.Kind(), left, operatorToken, right);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return binaryPatternSyntax;
			}
			return binaryPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x0010FFC9 File Offset: 0x0010E1C9
		public BinaryPatternSyntax WithLeft(PatternSyntax left)
		{
			return this.Update(left, this.OperatorToken, this.Right);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x0010FFDE File Offset: 0x0010E1DE
		public BinaryPatternSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Left, operatorToken, this.Right);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x0010FFF3 File Offset: 0x0010E1F3
		public BinaryPatternSyntax WithRight(PatternSyntax right)
		{
			return this.Update(this.Left, this.OperatorToken, right);
		}

		// Token: 0x040014B8 RID: 5304
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax left;

		// Token: 0x040014B9 RID: 5305
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax right;
	}
}
