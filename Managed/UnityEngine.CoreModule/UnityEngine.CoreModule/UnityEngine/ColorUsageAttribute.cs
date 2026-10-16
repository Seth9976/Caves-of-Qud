using System;

namespace UnityEngine
{
	// Token: 0x02000239 RID: 569
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x06001E6A RID: 7786 RVA: 0x0003A8EC File Offset: 0x00038AEC
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0003A944 File Offset: 0x00038B44
		public ColorUsageAttribute(bool showAlpha, bool hdr)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		[Obsolete("Brightness and exposure parameters are no longer used for anything. Use ColorUsageAttribute(bool showAlpha, bool hdr)")]
		public ColorUsageAttribute(bool showAlpha, bool hdr, float minBrightness, float maxBrightness, float minExposureValue, float maxExposureValue)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
			this.minBrightness = minBrightness;
			this.maxBrightness = maxBrightness;
			this.minExposureValue = minExposureValue;
			this.maxExposureValue = maxExposureValue;
		}

		// Token: 0x0400090A RID: 2314
		public readonly bool showAlpha = true;

		// Token: 0x0400090B RID: 2315
		public readonly bool hdr = false;

		// Token: 0x0400090C RID: 2316
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minBrightness = 0f;

		// Token: 0x0400090D RID: 2317
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxBrightness = 8f;

		// Token: 0x0400090E RID: 2318
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minExposureValue = 0.125f;

		// Token: 0x0400090F RID: 2319
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxExposureValue = 3f;
	}
}
