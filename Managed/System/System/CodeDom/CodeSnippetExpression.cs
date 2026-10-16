using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal expression.</summary>
	// Token: 0x0200032D RID: 813
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class.</summary>
		// Token: 0x060019C9 RID: 6601 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeSnippetExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class using the specified literal expression.</summary>
		/// <param name="value">The literal expression to represent. </param>
		// Token: 0x060019CA RID: 6602 RVA: 0x00060CD1 File Offset: 0x0005EED1
		public CodeSnippetExpression(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal string of code.</summary>
		/// <returns>The literal string.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00060CE0 File Offset: 0x0005EEE0
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x00060CF1 File Offset: 0x0005EEF1
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

		// Token: 0x04000DFA RID: 3578
		private string _value;
	}
}
