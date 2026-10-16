using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000333 RID: 819
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class.</summary>
		// Token: 0x060019E9 RID: 6633 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeThrowExceptionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class with the specified exception type instance.</summary>
		/// <param name="toThrow">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the exception to throw. </param>
		// Token: 0x060019EA RID: 6634 RVA: 0x00060E5C File Offset: 0x0005F05C
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.ToThrow = toThrow;
		}

		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00060E6B File Offset: 0x0005F06B
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x00060E73 File Offset: 0x0005F073
		public CodeExpression ToThrow { get; set; }
	}
}
