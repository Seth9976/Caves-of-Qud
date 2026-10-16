using System;

namespace Mono.Cecil
{
	// Token: 0x02000283 RID: 643
	internal sealed class CustomAttributeValueProjection
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00031348 File Offset: 0x0002F548
		public CustomAttributeValueProjection(AttributeTargets targets, CustomAttributeValueTreatment treatment)
		{
			this.Targets = targets;
			this.Treatment = treatment;
		}

		// Token: 0x040005BE RID: 1470
		public readonly AttributeTargets Targets;

		// Token: 0x040005BF RID: 1471
		public readonly CustomAttributeValueTreatment Treatment;
	}
}
