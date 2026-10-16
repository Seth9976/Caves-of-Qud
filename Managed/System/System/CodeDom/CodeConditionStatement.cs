using System;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an if statement.</summary>
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x060018AF RID: 6319 RVA: 0x0005F865 File Offset: 0x0005DA65
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		// Token: 0x060018B0 RID: 6320 RVA: 0x0005F883 File Offset: 0x0005DA83
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is false. </param>
		// Token: 0x060018B1 RID: 6321 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate true or false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate true or false.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005F8F1 File Offset: 0x0005DAF1
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0005F8F9 File Offset: 0x0005DAF9
		public CodeExpression Condition { get; set; }

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to true.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to true.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0005F902 File Offset: 0x0005DB02
		public CodeStatementCollection TrueStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to false.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0005F90A File Offset: 0x0005DB0A
		public CodeStatementCollection FalseStatements { get; } = new CodeStatementCollection();
	}
}
