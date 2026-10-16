using System;
using System.IO;
using AiUnity.Common.Patterns;
using UnityEngine;

namespace AiUnity.Common.IO
{
	// Token: 0x020002DB RID: 731
	public abstract class UnityFileInfo<T> : Singleton<T> where T : new()
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x000808BD File Offset: 0x0007EABD
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x000808C5 File Offset: 0x0007EAC5
		public FileInfo FileInfo { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000808CE File Offset: 0x0007EACE
		public string NameWithoutExtension
		{
			get
			{
				return Path.GetFileNameWithoutExtension(this.FileInfo.Name);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x000808E0 File Offset: 0x0007EAE0
		public string RelativeName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.FileInfo.FullName))
				{
					return this.FileInfo.FullName.Substring(Application.dataPath.Length - 6);
				}
				return null;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00080912 File Offset: 0x0007EB12
		public string RelativeNameWithoutExtension
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(this.RelativeName), this.NameWithoutExtension);
			}
		}
	}
}
