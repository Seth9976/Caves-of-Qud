using System;

namespace Mono.Cecil
{
	// Token: 0x0200022A RID: 554
	internal sealed class CustomMarshalInfo : MarshalInfo
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002981A File Offset: 0x00027A1A
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00029822 File Offset: 0x00027A22
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002982B File Offset: 0x00027A2B
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00029833 File Offset: 0x00027A33
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002983C File Offset: 0x00027A3C
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00029844 File Offset: 0x00027A44
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002984D File Offset: 0x00027A4D
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00029855 File Offset: 0x00027A55
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

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002985E File Offset: 0x00027A5E
		public CustomMarshalInfo()
			: base(NativeType.CustomMarshaler)
		{
		}

		// Token: 0x04000360 RID: 864
		internal Guid guid;

		// Token: 0x04000361 RID: 865
		internal string unmanaged_type;

		// Token: 0x04000362 RID: 866
		internal TypeReference managed_type;

		// Token: 0x04000363 RID: 867
		internal string cookie;
	}
}
