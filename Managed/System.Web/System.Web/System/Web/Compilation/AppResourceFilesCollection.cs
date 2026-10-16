using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200060D RID: 1549
	internal class AppResourceFilesCollection
	{
		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x000AE6E6 File Offset: 0x000AC8E6
		public string SourceDir
		{
			get
			{
				return this.sourceDir;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000AE6EE File Offset: 0x000AC8EE
		public bool HasFiles
		{
			get
			{
				return !string.IsNullOrEmpty(this.sourceDir) && this.files.Count > 0;
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x000AE70D File Offset: 0x000AC90D
		public List<AppResourceFileInfo> Files
		{
			get
			{
				return this.files;
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000AE718 File Offset: 0x000AC918
		public AppResourceFilesCollection(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.isGlobal = true;
			this.files = new List<AppResourceFileInfo>();
			string text = Path.Combine(HttpRuntime.AppDomainAppPath, "App_GlobalResources");
			if (Directory.Exists(text))
			{
				this.sourceDir = text;
			}
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x000AE76C File Offset: 0x000AC96C
		public AppResourceFilesCollection(string parserDir)
		{
			if (string.IsNullOrEmpty(parserDir))
			{
				throw new ArgumentException("parserDir cannot be empty");
			}
			this.isGlobal = true;
			this.files = new List<AppResourceFileInfo>();
			string text = Path.Combine(parserDir, "App_LocalResources");
			if (Directory.Exists(text))
			{
				this.sourceDir = text;
				HttpApplicationFactory.WatchLocationForRestart(this.sourceDir, "*");
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
		public void Collect()
		{
			if (string.IsNullOrEmpty(this.sourceDir))
			{
				return;
			}
			FileInfo[] array = new DirectoryInfo(this.sourceDir).GetFiles();
			if (array.Length == 0)
			{
				return;
			}
			foreach (FileInfo fileInfo in array)
			{
				string extension = fileInfo.Extension;
				AppResourceFileKind appResourceFileKind;
				if (this.Acceptable(extension, out appResourceFileKind))
				{
					AppResourceFileInfo appResourceFileInfo = new AppResourceFileInfo(fileInfo, appResourceFileKind);
					this.files.Add(appResourceFileInfo);
				}
			}
			if (this.isGlobal && this.files.Count == 0)
			{
				return;
			}
			AppResourcesLengthComparer<AppResourceFileInfo> appResourcesLengthComparer = new AppResourcesLengthComparer<AppResourceFileInfo>();
			this.files.Sort(appResourcesLengthComparer);
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000AE870 File Offset: 0x000ACA70
		private bool Acceptable(string extension, out AppResourceFileKind kind)
		{
			string text = extension.ToLower(Helpers.InvariantCulture);
			if (text == ".resx")
			{
				kind = AppResourceFileKind.ResX;
				return true;
			}
			if (!(text == ".resource"))
			{
				kind = AppResourceFileKind.NotResource;
				return false;
			}
			kind = AppResourceFileKind.Resource;
			return true;
		}

		// Token: 0x040023CA RID: 9162
		private List<AppResourceFileInfo> files;

		// Token: 0x040023CB RID: 9163
		private bool isGlobal;

		// Token: 0x040023CC RID: 9164
		private string sourceDir;
	}
}
