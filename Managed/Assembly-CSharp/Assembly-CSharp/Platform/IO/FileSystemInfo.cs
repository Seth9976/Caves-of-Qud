using System;

namespace Platform.IO
{
	// Token: 0x0200033A RID: 826
	public abstract class FileSystemInfo
	{
		// Token: 0x06001F19 RID: 7961 RVA: 0x00098716 File Offset: 0x00096916
		protected FileSystemInfo(string fullPath)
		{
			this.rawPath = fullPath;
			this.Refresh();
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0009872B File Offset: 0x0009692B
		public FileAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x00098733 File Offset: 0x00096933
		public bool Exists
		{
			get
			{
				return this.exists;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0009873B File Offset: 0x0009693B
		public string Extension
		{
			get
			{
				return Path.GetExtension(this.FullName);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00098748 File Offset: 0x00096948
		public string FullName
		{
			get
			{
				return this.rawPath;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00098750 File Offset: 0x00096950
		public string Name
		{
			get
			{
				return Path.GetFileName(this.FullName);
			}
		}

		// Token: 0x06001F1F RID: 7967
		public abstract void Delete();

		// Token: 0x06001F20 RID: 7968 RVA: 0x0009875D File Offset: 0x0009695D
		public virtual void Refresh()
		{
			this.fullName = this.rawPath;
			this.name = Path.GetFileName(this.FullName);
			this.extension = Path.GetExtension(this.FullName);
			this.attributes = FileAttributes.None;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00098794 File Offset: 0x00096994
		public override string ToString()
		{
			return string.Format("{0} {1} [{2}]", base.GetType().Name, this.FullName, this.attributes);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000987BC File Offset: 0x000969BC
		public bool HasAttribute(FileAttributes attribute)
		{
			return this.attributes.IsSubset(attribute);
		}

		// Token: 0x04001003 RID: 4099
		public const int SAVEDATA_MINIMUM_SPACE_BEFORE_EXPANSION = 4194304;

		// Token: 0x04001004 RID: 4100
		protected string rawPath;

		// Token: 0x04001005 RID: 4101
		protected FileAttributes attributes;

		// Token: 0x04001006 RID: 4102
		protected bool exists;

		// Token: 0x04001007 RID: 4103
		protected string extension;

		// Token: 0x04001008 RID: 4104
		protected string fullName;

		// Token: 0x04001009 RID: 4105
		protected string name;
	}
}
