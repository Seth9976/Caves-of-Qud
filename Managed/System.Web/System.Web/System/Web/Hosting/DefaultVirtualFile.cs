using System;
using System.IO;

namespace System.Web.Hosting
{
	// Token: 0x02000549 RID: 1353
	internal class DefaultVirtualFile : VirtualFile
	{
		// Token: 0x06003A87 RID: 14983 RVA: 0x0009BD86 File Offset: 0x00099F86
		internal DefaultVirtualFile(string virtualPath)
			: base(virtualPath)
		{
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x0009BD8F File Offset: 0x00099F8F
		public override Stream Open()
		{
			return File.OpenRead(HostingEnvironment.MapPath(base.VirtualPath));
		}
	}
}
