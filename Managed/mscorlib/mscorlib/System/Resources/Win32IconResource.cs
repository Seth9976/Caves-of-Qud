using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000876 RID: 2166
	internal class Win32IconResource : Win32Resource
	{
		// Token: 0x06004821 RID: 18465 RVA: 0x000ED1BA File Offset: 0x000EB3BA
		public Win32IconResource(int id, int language, ICONDIRENTRY icon)
			: base(Win32ResourceType.RT_ICON, id, language)
		{
			this.icon = icon;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x000ED1CC File Offset: 0x000EB3CC
		public ICONDIRENTRY Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x000ED1D4 File Offset: 0x000EB3D4
		public override void WriteTo(Stream s)
		{
			s.Write(this.icon.image, 0, this.icon.image.Length);
		}

		// Token: 0x04002E2E RID: 11822
		private ICONDIRENTRY icon;
	}
}
