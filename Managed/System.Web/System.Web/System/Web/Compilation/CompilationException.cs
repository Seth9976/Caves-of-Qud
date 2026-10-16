using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x02000646 RID: 1606
	[Serializable]
	internal class CompilationException : HtmlizedException
	{
		// Token: 0x06004516 RID: 17686 RVA: 0x000BB12C File Offset: 0x000B932C
		private CompilationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("filename");
			this.errors = info.GetValue("errors", typeof(CompilerErrorCollection)) as CompilerErrorCollection;
			this.results = info.GetValue("results", typeof(CompilerResults)) as CompilerResults;
			this.fileText = info.GetString("fileText");
			this.errmsg = info.GetString("errmsg");
			this.errorLines = info.GetValue("errorLines", typeof(int[])) as int[];
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000BB1D4 File Offset: 0x000B93D4
		public CompilationException(string filename, CompilerErrorCollection errors, string fileText)
		{
			this.filename = filename;
			this.errors = errors;
			this.fileText = fileText;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000BB1F1 File Offset: 0x000B93F1
		public CompilationException(string filename, CompilerResults results, string fileText)
			: this(filename, (results != null) ? results.Errors : null, fileText)
		{
			this.results = results;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000BB210 File Offset: 0x000B9410
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			base.GetObjectData(info, ctx);
			info.AddValue("filename", this.filename);
			info.AddValue("errors", this.errors);
			info.AddValue("results", this.results);
			info.AddValue("fileText", this.fileText);
			info.AddValue("errmsg", this.errmsg);
			info.AddValue("errorLines", this.errorLines);
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x000BB28B File Offset: 0x000B948B
		public override string Message
		{
			get
			{
				return this.ErrorMessage;
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000BB293 File Offset: 0x000B9493
		public override string SourceFile
		{
			get
			{
				if (this.errors == null || this.errors.Count == 0)
				{
					return this.filename;
				}
				return this.errors[0].FileName;
			}
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x000BB2C2 File Offset: 0x000B94C2
		public override string FileName
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x000BB2CA File Offset: 0x000B94CA
		public override string Title
		{
			get
			{
				return "Compilation Error";
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x000BB2D1 File Offset: 0x000B94D1
		public override string Description
		{
			get
			{
				return "Error compiling a resource required to service this request. Review your source file and modify it to fix this error.";
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x000BB2D8 File Offset: 0x000B94D8
		public override string ErrorMessage
		{
			get
			{
				if (this.errmsg == null && this.errors != null)
				{
					CompilerError compilerError = null;
					foreach (object obj in this.errors)
					{
						CompilerError compilerError2 = (CompilerError)obj;
						if (!compilerError2.IsWarning)
						{
							compilerError = compilerError2;
							break;
						}
					}
					if (compilerError != null)
					{
						this.errmsg = compilerError.ToString();
						int num = this.errmsg.IndexOf(" : error ");
						if (num > -1)
						{
							this.errmsg = this.errmsg.Substring(num + 9);
						}
					}
					else
					{
						this.errmsg = string.Empty;
					}
				}
				return this.errmsg;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x000BB3A0 File Offset: 0x000B95A0
		public override string FileText
		{
			get
			{
				return this.fileText;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x000BB3A8 File Offset: 0x000B95A8
		public override int[] ErrorLines
		{
			get
			{
				if (this.errorLines == null && this.errors != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in this.errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (!compilerError.IsWarning && compilerError.Line != 0 && !arrayList.Contains(compilerError.Line))
						{
							arrayList.Add(compilerError.Line);
						}
					}
					this.errorLines = (int[])arrayList.ToArray(typeof(int));
					Array.Sort<int>(this.errorLines);
				}
				return this.errorLines;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool ErrorLinesPaired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x000BB478 File Offset: 0x000B9678
		public StringCollection CompilerOutput
		{
			get
			{
				if (this.results == null)
				{
					return null;
				}
				return this.results.Output;
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x000BB48F File Offset: 0x000B968F
		public CompilerResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x040024D4 RID: 9428
		private string filename;

		// Token: 0x040024D5 RID: 9429
		private CompilerErrorCollection errors;

		// Token: 0x040024D6 RID: 9430
		private CompilerResults results;

		// Token: 0x040024D7 RID: 9431
		private string fileText;

		// Token: 0x040024D8 RID: 9432
		private string errmsg;

		// Token: 0x040024D9 RID: 9433
		private int[] errorLines;
	}
}
