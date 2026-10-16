using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x0200032E RID: 814
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x060019CD RID: 6605 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent. </param>
		// Token: 0x060019CE RID: 6606 RVA: 0x00060CFA File Offset: 0x0005EEFA
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00060D09 File Offset: 0x0005EF09
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00060D1A File Offset: 0x0005EF1A
		public string Value
		{
			get
			{
				return this._value ?? string.Empty;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000DFB RID: 3579
		private string _value;
	}
}
