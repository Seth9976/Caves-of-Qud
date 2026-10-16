using System;
using System.Globalization;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x0200034C RID: 844
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06001BB2 RID: 7090 RVA: 0x00065FE0 File Offset: 0x000641E0
		public CompilerError()
			: this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error. </param>
		/// <param name="line">The line of the source of the error. </param>
		/// <param name="column">The column of the source of the error. </param>
		/// <param name="errorNumber">The error number of the error. </param>
		/// <param name="errorText">The error message text. </param>
		// Token: 0x06001BB3 RID: 7091 RVA: 0x00065FF9 File Offset: 0x000641F9
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.Line = line;
			this.Column = column;
			this.ErrorNumber = errorNumber;
			this.ErrorText = errorText;
			this.FileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00066026 File Offset: 0x00064226
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0006602E File Offset: 0x0006422E
		public int Line { get; set; }

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00066037 File Offset: 0x00064237
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0006603F File Offset: 0x0006423F
		public int Column { get; set; }

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00066048 File Offset: 0x00064248
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x00066050 File Offset: 0x00064250
		public string ErrorNumber { get; set; }

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00066059 File Offset: 0x00064259
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00066061 File Offset: 0x00064261
		public string ErrorText { get; set; }

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>true if the error is a warning; otherwise, false.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0006606A File Offset: 0x0006426A
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00066072 File Offset: 0x00064272
		public bool IsWarning { get; set; }

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0006607B File Offset: 0x0006427B
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x00066083 File Offset: 0x00064283
		public string FileName { get; set; }

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06001BC0 RID: 7104 RVA: 0x0006608C File Offset: 0x0006428C
		public override string ToString()
		{
			if (this.FileName.Length <= 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", this.WarningString, this.ErrorNumber, this.ErrorText);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[] { this.FileName, this.Line, this.Column, this.WarningString, this.ErrorNumber, this.ErrorText });
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0006611E File Offset: 0x0006431E
		private string WarningString
		{
			get
			{
				if (!this.IsWarning)
				{
					return "error";
				}
				return "warning";
			}
		}
	}
}
