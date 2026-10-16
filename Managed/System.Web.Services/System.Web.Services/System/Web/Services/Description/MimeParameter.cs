using System;
using System.CodeDom;

namespace System.Web.Services.Description
{
	// Token: 0x020000D7 RID: 215
	internal class MimeParameter
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00018CC5 File Offset: 0x00016EC5
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00018CDB File Offset: 0x00016EDB
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00018CE4 File Offset: 0x00016EE4
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00018CFA File Offset: 0x00016EFA
		internal string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00018D03 File Offset: 0x00016F03
		internal CodeAttributeDeclarationCollection Attributes
		{
			get
			{
				if (this.attrs == null)
				{
					this.attrs = new CodeAttributeDeclarationCollection();
				}
				return this.attrs;
			}
		}

		// Token: 0x04000395 RID: 917
		private string name;

		// Token: 0x04000396 RID: 918
		private string typeName;

		// Token: 0x04000397 RID: 919
		private CodeAttributeDeclarationCollection attrs;
	}
}
