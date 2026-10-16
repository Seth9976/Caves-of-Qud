using System;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x02000317 RID: 791
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class.</summary>
		// Token: 0x06001910 RID: 6416 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeLabeledStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label. </param>
		// Token: 0x06001911 RID: 6417 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		public CodeLabeledStatement(string label)
		{
			this._label = label;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name and statement.</summary>
		/// <param name="label">The name of the label. </param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to associate with the label. </param>
		// Token: 0x06001912 RID: 6418 RVA: 0x0005FDB3 File Offset: 0x0005DFB3
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this._label = label;
			this.Statement = statement;
		}

		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005FDC9 File Offset: 0x0005DFC9
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0005FDDA File Offset: 0x0005DFDA
		public string Label
		{
			get
			{
				return this._label ?? string.Empty;
			}
			set
			{
				this._label = value;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005FDE3 File Offset: 0x0005DFE3
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0005FDEB File Offset: 0x0005DFEB
		public CodeStatement Statement { get; set; }

		// Token: 0x04000DB6 RID: 3510
		private string _label;
	}
}
