using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x0200061C RID: 1564
	internal class ParserStack
	{
		// Token: 0x06004311 RID: 17169 RVA: 0x000B1107 File Offset: 0x000AF307
		public ParserStack()
		{
			this.files = new Hashtable();
			this.parsers = new Stack();
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000B1128 File Offset: 0x000AF328
		public bool Push(AspParser parser)
		{
			if (this.files.Contains(parser.Filename))
			{
				return false;
			}
			this.files[parser.Filename] = true;
			this.parsers.Push(parser);
			this.current = parser;
			return true;
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000B1178 File Offset: 0x000AF378
		public AspParser Pop()
		{
			if (this.parsers.Count == 0)
			{
				return null;
			}
			this.files.Remove(this.current.Filename);
			AspParser aspParser = (AspParser)this.parsers.Pop();
			if (this.parsers.Count > 0)
			{
				this.current = (AspParser)this.parsers.Peek();
				return aspParser;
			}
			this.current = null;
			return aspParser;
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x000B11E6 File Offset: 0x000AF3E6
		public int Count
		{
			get
			{
				return this.parsers.Count;
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x000B11F3 File Offset: 0x000AF3F3
		public AspParser Parser
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000B11FB File Offset: 0x000AF3FB
		public string Filename
		{
			get
			{
				return this.current.Filename;
			}
		}

		// Token: 0x040023F4 RID: 9204
		private Hashtable files;

		// Token: 0x040023F5 RID: 9205
		private Stack parsers;

		// Token: 0x040023F6 RID: 9206
		private AspParser current;
	}
}
