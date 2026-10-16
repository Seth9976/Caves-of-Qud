using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000AC RID: 172
	public abstract class Resource
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00015B8D File Offset: 0x00013D8D
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x00015B95 File Offset: 0x00013D95
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00015B9E File Offset: 0x00013D9E
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00015BA6 File Offset: 0x00013DA6
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000727 RID: 1831
		public abstract ResourceType ResourceType { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00015BAF File Offset: 0x00013DAF
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00015BBE File Offset: 0x00013DBE
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00015BD4 File Offset: 0x00013DD4
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00015BE3 File Offset: 0x00013DE3
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

		// Token: 0x0600072C RID: 1836 RVA: 0x00015BF9 File Offset: 0x00013DF9
		internal Resource(string name, ManifestResourceAttributes attributes)
		{
			this.name = name;
			this.attributes = (uint)attributes;
		}

		// Token: 0x04000254 RID: 596
		private string name;

		// Token: 0x04000255 RID: 597
		private uint attributes;
	}
}
