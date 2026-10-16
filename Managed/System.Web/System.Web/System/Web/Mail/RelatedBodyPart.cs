using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000F9 RID: 249
	public class RelatedBodyPart
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x00023405 File Offset: 0x00021605
		public RelatedBodyPart(string id, string fileName)
		{
			this.id = id;
			if (this.FileExists(fileName))
			{
				this.fileName = fileName;
				return;
			}
			throw new HttpException(500, "Invalid related body part");
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00023434 File Offset: 0x00021634
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0002343C File Offset: 0x0002163C
		public string Name
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00023445 File Offset: 0x00021645
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0002344D File Offset: 0x0002164D
		public string Path
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00023458 File Offset: 0x00021658
		private bool FileExists(string fileName)
		{
			bool flag;
			try
			{
				File.OpenRead(fileName).Close();
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001148 RID: 4424
		private string id;

		// Token: 0x04001149 RID: 4425
		private string fileName;
	}
}
