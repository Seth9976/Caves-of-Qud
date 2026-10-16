using System;

namespace System.CodeDom
{
	/// <summary>Represents a specific location within a specific file.</summary>
	// Token: 0x02000318 RID: 792
	[Serializable]
	public class CodeLinePragma
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class. </summary>
		// Token: 0x06001917 RID: 6423 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeLinePragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		/// <param name="fileName">The file name of the associated file. </param>
		/// <param name="lineNumber">The line number to store a reference to. </param>
		// Token: 0x06001918 RID: 6424 RVA: 0x0005FDF4 File Offset: 0x0005DFF4
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		/// <summary>Gets or sets the name of the associated file.</summary>
		/// <returns>The file name of the associated file.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005FE0A File Offset: 0x0005E00A
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x0005FE1B File Offset: 0x0005E01B
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets the line number of the associated reference.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0005FE24 File Offset: 0x0005E024
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public int LineNumber { get; set; }

		// Token: 0x04000DB8 RID: 3512
		private string _fileName;
	}
}
