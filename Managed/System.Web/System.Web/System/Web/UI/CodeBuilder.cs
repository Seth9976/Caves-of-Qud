using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001A9 RID: 425
	internal abstract class CodeBuilder : ControlBuilder
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0002B5FF File Offset: 0x000297FF
		public CodeBuilder(string code, bool isAssign, ILocation location)
		{
			this.code = code;
			this.isAssign = isAssign;
			base.Line = location.BeginLine;
			base.FileName = location.Filename;
			base.Location = location;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00003BF6 File Offset: 0x00001DF6
		internal override object CreateInstance()
		{
			return null;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0002B634 File Offset: 0x00029834
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x0002B63C File Offset: 0x0002983C
		internal string Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0002B645 File Offset: 0x00029845
		internal bool IsAssign
		{
			get
			{
				return this.isAssign;
			}
		}

		// Token: 0x04001386 RID: 4998
		private string code;

		// Token: 0x04001387 RID: 4999
		private bool isAssign;
	}
}
