using System;

namespace System
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002C2A File Offset: 0x00000E2A
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C32 File Offset: 0x00000E32
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002C41 File Offset: 0x00000E41
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000D43 RID: 3395
		private string comment;
	}
}
