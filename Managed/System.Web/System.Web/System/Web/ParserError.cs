using System;

namespace System.Web
{
	/// <summary>Represents a parser error or warning. This class cannot be inherited. </summary>
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public sealed class ParserError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class.</summary>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00002050 File Offset: 0x00000250
		public ParserError()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class by using the specified error text, virtual path, and source line number.</summary>
		/// <param name="errorText">The error message text.</param>
		/// <param name="virtualPath">The virtual path of the file being parsed when the error occurred.</param>
		/// <param name="line">The line number of the error source.</param>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001C38A File Offset: 0x0001A58A
		public ParserError(string errorText, string virtualPath, int line)
		{
			this._errorText = errorText;
			this._virtualPath = virtualPath;
			this._line = line;
		}

		/// <summary>Gets or sets a string that represents the error for the <see cref="T:System.Web.ParserError" /> object.</summary>
		/// <returns>A string containing the error message.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001C3A7 File Offset: 0x0001A5A7
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0001C3AF File Offset: 0x0001A5AF
		public string ErrorText
		{
			get
			{
				return this._errorText;
			}
			set
			{
				this._errorText = value;
			}
		}

		/// <summary>Gets or set the virtual path of the file that was being parsed when the error occurred.</summary>
		/// <returns>A string that specifies the virtual path of the file that contains the parser error.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		/// <summary>Gets or sets the line number of the source at which the error occurs.</summary>
		/// <returns>The source line number where the parser encountered the error.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001C3C9 File Offset: 0x0001A5C9
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0001C3D1 File Offset: 0x0001A5D1
		public int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x04001071 RID: 4209
		private string _errorText;

		// Token: 0x04001072 RID: 4210
		private string _virtualPath;

		// Token: 0x04001073 RID: 4211
		private int _line;
	}
}
