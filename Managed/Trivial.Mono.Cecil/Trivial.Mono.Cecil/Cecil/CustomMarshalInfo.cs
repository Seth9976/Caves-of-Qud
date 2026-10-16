using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000026 RID: 38
	public sealed class CustomMarshalInfo : MarshalInfo
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000048CD File Offset: 0x00002ACD
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000048D5 File Offset: 0x00002AD5
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000048DE File Offset: 0x00002ADE
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000048E6 File Offset: 0x00002AE6
		public string UnmanagedType
		{
			get
			{
				return this.unmanaged_type;
			}
			set
			{
				this.unmanaged_type = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000048EF File Offset: 0x00002AEF
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000048F7 File Offset: 0x00002AF7
		public TypeReference ManagedType
		{
			get
			{
				return this.managed_type;
			}
			set
			{
				this.managed_type = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004900 File Offset: 0x00002B00
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004908 File Offset: 0x00002B08
		public string Cookie
		{
			get
			{
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004911 File Offset: 0x00002B11
		public CustomMarshalInfo()
			: base(NativeType.CustomMarshaler)
		{
		}

		// Token: 0x04000047 RID: 71
		internal Guid guid;

		// Token: 0x04000048 RID: 72
		internal string unmanaged_type;

		// Token: 0x04000049 RID: 73
		internal TypeReference managed_type;

		// Token: 0x0400004A RID: 74
		internal string cookie;
	}
}
