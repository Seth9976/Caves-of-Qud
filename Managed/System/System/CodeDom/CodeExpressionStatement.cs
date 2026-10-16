using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x02000312 RID: 786
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x060018F4 RID: 6388 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement. </param>
		// Token: 0x060018F5 RID: 6389 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0005FC4B File Offset: 0x0005DE4B
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x0005FC53 File Offset: 0x0005DE53
		public CodeExpression Expression { get; set; }
	}
}
