using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x020002FC RID: 764
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06001861 RID: 6241 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator. </param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator. </param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator. </param>
		// Token: 0x06001862 RID: 6242 RVA: 0x0005F3C4 File Offset: 0x0005D5C4
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0005F3E1 File Offset: 0x0005D5E1
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x0005F3E9 File Offset: 0x0005D5E9
		public CodeExpression Right { get; set; }

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005F3F2 File Offset: 0x0005D5F2
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x0005F3FA File Offset: 0x0005D5FA
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005F403 File Offset: 0x0005D603
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x0005F40B File Offset: 0x0005D60B
		public CodeBinaryOperatorType Operator { get; set; }
	}
}
