using System;

namespace System.CodeDom
{
	/// <summary>Represents a try block with any number of catch clauses and, optionally, a finally block.</summary>
	// Token: 0x02000334 RID: 820
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class.</summary>
		// Token: 0x060019ED RID: 6637 RVA: 0x00060E7C File Offset: 0x0005F07C
		public CodeTryCatchFinallyStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try and catch clauses.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		// Token: 0x060019EE RID: 6638 RVA: 0x00060EA8 File Offset: 0x0005F0A8
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try, catch clauses, and finally statements.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		/// <param name="finallyStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the finally statements to use. </param>
		// Token: 0x060019EF RID: 6639 RVA: 0x00060EF4 File Offset: 0x0005F0F4
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00060F4C File Offset: 0x0005F14C
		public CodeStatementCollection TryStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00060F54 File Offset: 0x0005F154
		public CodeCatchClauseCollection CatchClauses { get; } = new CodeCatchClauseCollection();

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00060F5C File Offset: 0x0005F15C
		public CodeStatementCollection FinallyStatements { get; } = new CodeStatementCollection();
	}
}
