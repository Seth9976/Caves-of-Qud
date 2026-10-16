using System;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x0200031E RID: 798
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x0600194E RID: 6478 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value. </param>
		// Token: 0x0600194F RID: 6479 RVA: 0x000603B6 File Offset: 0x0005E5B6
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or null if the statement is part of a subroutine.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000603C5 File Offset: 0x0005E5C5
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x000603CD File Offset: 0x0005E5CD
		public CodeExpression Expression { get; set; }
	}
}
