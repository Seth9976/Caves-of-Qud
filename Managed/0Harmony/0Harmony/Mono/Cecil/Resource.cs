using System;

namespace Mono.Cecil
{
	// Token: 0x0200025F RID: 607
	public abstract class Resource
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002E0A1 File Offset: 0x0002C2A1
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0002E0A9 File Offset: 0x0002C2A9
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0002E0B2 File Offset: 0x0002C2B2
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0002E0BA File Offset: 0x0002C2BA
		public ManifestResourceAttributes Attributes
		{
			get
			{
				return (ManifestResourceAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000EBE RID: 3774
		public abstract ResourceType ResourceType { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0002E0C3 File Offset: 0x0002C2C3
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0002E0D2 File Offset: 0x0002C2D2
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x0002E0F7 File Offset: 0x0002C2F7
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002E10D File Offset: 0x0002C30D
		internal Resource(string name, ManifestResourceAttributes attributes)
		{
			this.name = name;
			this.attributes = (uint)attributes;
		}

		// Token: 0x040004C4 RID: 1220
		private string name;

		// Token: 0x040004C5 RID: 1221
		private uint attributes;
	}
}
