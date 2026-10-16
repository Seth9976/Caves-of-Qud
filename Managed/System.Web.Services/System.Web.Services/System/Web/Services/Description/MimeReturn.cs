using System;
using System.CodeDom;

namespace System.Web.Services.Description
{
	// Token: 0x020000DA RID: 218
	internal class MimeReturn
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00018D53 File Offset: 0x00016F53
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00018D69 File Offset: 0x00016F69
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00018D72 File Offset: 0x00016F72
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00018D7A File Offset: 0x00016F7A
		internal Type ReaderType
		{
			get
			{
				return this.readerType;
			}
			set
			{
				this.readerType = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00018D83 File Offset: 0x00016F83
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

		// Token: 0x0400039A RID: 922
		private string typeName;

		// Token: 0x0400039B RID: 923
		private Type readerType;

		// Token: 0x0400039C RID: 924
		private CodeAttributeDeclarationCollection attrs;
	}
}
