using System;
using System.IO;

namespace System.Web.Compilation
{
	// Token: 0x0200060A RID: 1546
	internal class AppResourceFileInfo
	{
		// Token: 0x060042A1 RID: 17057 RVA: 0x000AE5BC File Offset: 0x000AC7BC
		public AppResourceFileInfo(FileInfo info, AppResourceFileKind kind)
		{
			this.Embeddable = kind == AppResourceFileKind.Resource || kind == AppResourceFileKind.Binary;
			this.Compilable = kind == AppResourceFileKind.ResX;
			this.Info = info;
			this.Kind = kind;
			this.Seen = false;
		}

		// Token: 0x040023C0 RID: 9152
		public readonly bool Embeddable;

		// Token: 0x040023C1 RID: 9153
		public readonly bool Compilable;

		// Token: 0x040023C2 RID: 9154
		public readonly FileInfo Info;

		// Token: 0x040023C3 RID: 9155
		public readonly AppResourceFileKind Kind;

		// Token: 0x040023C4 RID: 9156
		public bool Seen;
	}
}
