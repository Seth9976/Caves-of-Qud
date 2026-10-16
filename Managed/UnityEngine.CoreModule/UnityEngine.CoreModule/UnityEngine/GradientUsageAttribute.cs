using System;

namespace UnityEngine
{
	// Token: 0x0200023A RID: 570
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class GradientUsageAttribute : PropertyAttribute
	{
		// Token: 0x06001E6D RID: 7789 RVA: 0x0003AA20 File Offset: 0x00038C20
		public GradientUsageAttribute(bool hdr)
		{
			this.hdr = hdr;
			this.colorSpace = ColorSpace.Gamma;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0003AA46 File Offset: 0x00038C46
		public GradientUsageAttribute(bool hdr, ColorSpace colorSpace)
		{
			this.hdr = hdr;
			this.colorSpace = colorSpace;
		}

		// Token: 0x04000910 RID: 2320
		public readonly bool hdr = false;

		// Token: 0x04000911 RID: 2321
		public readonly ColorSpace colorSpace = ColorSpace.Gamma;
	}
}
