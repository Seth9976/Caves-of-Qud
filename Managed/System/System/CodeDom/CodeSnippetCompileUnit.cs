using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x0200032C RID: 812
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class. </summary>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00060C8F File Offset: 0x0005EE8F
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent. </param>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00060C97 File Offset: 0x0005EE97
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00060CA6 File Offset: 0x0005EEA6
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00060CB7 File Offset: 0x0005EEB7
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

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00060CC0 File Offset: 0x0005EEC0
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x00060CC8 File Offset: 0x0005EEC8
		public CodeLinePragma LinePragma { get; set; }

		// Token: 0x04000DF8 RID: 3576
		private string _value;
	}
}
