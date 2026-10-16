using System;

namespace System.CodeDom
{
	/// <summary>Represents a for statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x02000316 RID: 790
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class.</summary>
		// Token: 0x06001907 RID: 6407 RVA: 0x0005FD21 File Offset: 0x0005DF21
		public CodeIterationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class using the specified parameters.</summary>
		/// <param name="initStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the loop initialization statement. </param>
		/// <param name="testExpression">A <see cref="T:System.CodeDom.CodeExpression" /> containing the expression to test for exit condition. </param>
		/// <param name="incrementStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the per-cycle increment statement. </param>
		/// <param name="statements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements within the loop. </param>
		// Token: 0x06001908 RID: 6408 RVA: 0x0005FD34 File Offset: 0x0005DF34
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.InitStatement = initStatement;
			this.TestExpression = testExpression;
			this.IncrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0005FD69 File Offset: 0x0005DF69
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0005FD71 File Offset: 0x0005DF71
		public CodeStatement InitStatement { get; set; }

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0005FD7A File Offset: 0x0005DF7A
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0005FD82 File Offset: 0x0005DF82
		public CodeExpression TestExpression { get; set; }

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0005FD8B File Offset: 0x0005DF8B
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0005FD93 File Offset: 0x0005DF93
		public CodeStatement IncrementStatement { get; set; }

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0005FD9C File Offset: 0x0005DF9C
		public CodeStatementCollection Statements { get; } = new CodeStatementCollection();
	}
}
