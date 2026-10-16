using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000C8 RID: 200
	public class SFVersion
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000E8CF File Offset: 0x0000CACF
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000E8D7 File Offset: 0x0000CAD7
		public short Major
		{
			get
			{
				return this.major;
			}
			set
			{
				this.major = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		public short Minor
		{
			get
			{
				return this.minor;
			}
			set
			{
				this.minor = value;
			}
		}

		// Token: 0x04000531 RID: 1329
		private short major;

		// Token: 0x04000532 RID: 1330
		private short minor;
	}
}
