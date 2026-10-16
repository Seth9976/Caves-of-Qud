using System;
using System.IO;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010E RID: 270
	public class FileOrDirectoryExistsConstraint : Constraint
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00013F7E File Offset: 0x0001217E
		public FileOrDirectoryExistsConstraint IgnoreDirectories
		{
			get
			{
				this._ignoreDirectories = true;
				return this;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00013F88 File Offset: 0x00012188
		public FileOrDirectoryExistsConstraint IgnoreFiles
		{
			get
			{
				this._ignoreFiles = true;
				return this;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00011D90 File Offset: 0x0000FF90
		public FileOrDirectoryExistsConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00013F92 File Offset: 0x00012192
		public FileOrDirectoryExistsConstraint(bool ignoreDirectories)
			: base(new object[0])
		{
			this._ignoreDirectories = ignoreDirectories;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00013FA7 File Offset: 0x000121A7
		public override string Description
		{
			get
			{
				if (this._ignoreDirectories)
				{
					return "file exists";
				}
				if (this._ignoreFiles)
				{
					return "directory exists";
				}
				return "file or directory exists";
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00013FCC File Offset: 0x000121CC
		public override ConstraintResult ApplyTo(object actual)
		{
			if (actual == null)
			{
				throw new ArgumentNullException("actual", "The actual value must be a non-null string" + this.ErrorSubstring);
			}
			if (actual is string)
			{
				return this.CheckString<object>(actual);
			}
			FileInfo fileInfo = actual as FileInfo;
			if (!this._ignoreFiles && fileInfo != null)
			{
				return new ConstraintResult(this, actual, fileInfo.Exists);
			}
			DirectoryInfo directoryInfo = actual as DirectoryInfo;
			if (!this._ignoreDirectories && directoryInfo != null)
			{
				return new ConstraintResult(this, actual, directoryInfo.Exists);
			}
			throw new ArgumentException("The actual value must be a string" + this.ErrorSubstring, "actual");
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00014064 File Offset: 0x00012264
		private ConstraintResult CheckString<TActual>(TActual actual)
		{
			string text = actual as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("The actual value cannot be an empty string", "actual");
			}
			FileInfo fileInfo = new FileInfo(text);
			if (this._ignoreDirectories && !this._ignoreFiles)
			{
				return new ConstraintResult(this, actual, fileInfo.Exists);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (this._ignoreFiles && !this._ignoreDirectories)
			{
				return new ConstraintResult(this, actual, directoryInfo.Exists);
			}
			return new ConstraintResult(this, actual, fileInfo.Exists || directoryInfo.Exists);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00014106 File Offset: 0x00012306
		private string ErrorSubstring
		{
			get
			{
				if (this._ignoreDirectories)
				{
					return " or FileInfo";
				}
				if (this._ignoreFiles)
				{
					return " or DirectoryInfo";
				}
				return ", FileInfo or DirectoryInfo";
			}
		}

		// Token: 0x040001C5 RID: 453
		private bool _ignoreDirectories;

		// Token: 0x040001C6 RID: 454
		private bool _ignoreFiles;
	}
}
