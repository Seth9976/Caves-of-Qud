using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x0200065C RID: 1628
	[Serializable]
	internal class ParseException : HtmlizedException
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x000BDFCC File Offset: 0x000BC1CC
		private ParseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000BDFD6 File Offset: 0x000BC1D6
		public ParseException(ILocation location, string message)
			: this(location, message, null)
		{
			location = new Location(location);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000BDFE9 File Offset: 0x000BC1E9
		public ParseException(ILocation location, string message, Exception inner)
			: base(message, inner)
		{
			this.location = location;
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x000BDFFA File Offset: 0x000BC1FA
		public override string Title
		{
			get
			{
				return "Parser Error";
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000BE001 File Offset: 0x000BC201
		public override string Description
		{
			get
			{
				return "Error parsing a resource required to service this request. Review your source file and modify it to fix this error.";
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000BE008 File Offset: 0x000BC208
		public override string ErrorMessage
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000BE010 File Offset: 0x000BC210
		public override string SourceFile
		{
			get
			{
				return this.FileName;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000BE018 File Offset: 0x000BC218
		public override string FileName
		{
			get
			{
				if (this.location == null)
				{
					return null;
				}
				return this.location.Filename;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000BE030 File Offset: 0x000BC230
		public override string FileText
		{
			get
			{
				if (this.fileText != null)
				{
					return this.fileText;
				}
				string text = ((this.location != null) ? this.location.FileText : null);
				if (text != null && text.Length > 0)
				{
					return text;
				}
				if (this.FileName == null)
				{
					return null;
				}
				using (TextReader textReader = new StreamReader(this.FileName))
				{
					this.fileText = textReader.ReadToEnd();
				}
				return this.fileText;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x000BE0B8 File Offset: 0x000BC2B8
		public override int[] ErrorLines
		{
			get
			{
				if (this.location == null)
				{
					return null;
				}
				return new int[]
				{
					this.location.BeginLine,
					this.location.EndLine
				};
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool ErrorLinesPaired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x000BE0E6 File Offset: 0x000BC2E6
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			base.GetObjectData(info, ctx);
		}

		// Token: 0x04002504 RID: 9476
		private ILocation location;

		// Token: 0x04002505 RID: 9477
		private string fileText;
	}
}
