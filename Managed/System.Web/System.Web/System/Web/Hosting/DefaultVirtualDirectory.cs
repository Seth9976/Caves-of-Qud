using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Web.Hosting
{
	// Token: 0x02000548 RID: 1352
	internal sealed class DefaultVirtualDirectory : VirtualDirectory
	{
		// Token: 0x06003A80 RID: 14976 RVA: 0x0009BC00 File Offset: 0x00099E00
		internal DefaultVirtualDirectory(string virtualPath)
			: base(virtualPath)
		{
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0009BC0C File Offset: 0x00099E0C
		private void Init()
		{
			if (this.phys_dir == null)
			{
				string virtualPath = base.VirtualPath;
				string text = HostingEnvironment.MapPath(virtualPath);
				if (File.Exists(text))
				{
					this.virtual_dir = VirtualPathUtility.GetDirectory(virtualPath);
					this.phys_dir = HostingEnvironment.MapPath(this.virtual_dir);
					return;
				}
				this.virtual_dir = VirtualPathUtility.AppendTrailingSlash(virtualPath);
				this.phys_dir = text;
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0009BC68 File Offset: 0x00099E68
		private List<VirtualFileBase> AddDirectories(List<VirtualFileBase> list, string dir)
		{
			if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
			{
				return list;
			}
			foreach (string text in Directory.GetDirectories(this.phys_dir))
			{
				list.Add(new DefaultVirtualDirectory(VirtualPathUtility.Combine(this.virtual_dir, Path.GetFileName(text))));
			}
			return list;
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0009BCC4 File Offset: 0x00099EC4
		private List<VirtualFileBase> AddFiles(List<VirtualFileBase> list, string dir)
		{
			if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
			{
				return list;
			}
			foreach (string text in Directory.GetFiles(this.phys_dir))
			{
				list.Add(new DefaultVirtualFile(VirtualPathUtility.Combine(this.virtual_dir, Path.GetFileName(text))));
			}
			return list;
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x0009BD20 File Offset: 0x00099F20
		public override IEnumerable Children
		{
			get
			{
				this.Init();
				List<VirtualFileBase> list = new List<VirtualFileBase>();
				this.AddDirectories(list, this.phys_dir);
				return this.AddFiles(list, this.phys_dir);
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x0009BD54 File Offset: 0x00099F54
		public override IEnumerable Directories
		{
			get
			{
				this.Init();
				return this.AddDirectories(new List<VirtualFileBase>(), this.phys_dir);
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x0009BD6D File Offset: 0x00099F6D
		public override IEnumerable Files
		{
			get
			{
				this.Init();
				return this.AddFiles(new List<VirtualFileBase>(), this.phys_dir);
			}
		}

		// Token: 0x04001FD9 RID: 8153
		private string phys_dir;

		// Token: 0x04001FDA RID: 8154
		private string virtual_dir;
	}
}
