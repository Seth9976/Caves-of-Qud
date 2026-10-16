using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000023 RID: 35
	internal sealed class CustomAttributeValueProjection
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000484A File Offset: 0x00002A4A
		public CustomAttributeValueProjection(AttributeTargets targets, CustomAttributeValueTreatment treatment)
		{
			this.Targets = targets;
			this.Treatment = treatment;
		}

		// Token: 0x0400003F RID: 63
		public readonly AttributeTargets Targets;

		// Token: 0x04000040 RID: 64
		public readonly CustomAttributeValueTreatment Treatment;
	}
}
