using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000035 RID: 53
	internal sealed class FieldDefinitionProjection
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00005B2C File Offset: 0x00003D2C
		public FieldDefinitionProjection(FieldDefinition field, FieldDefinitionTreatment treatment)
		{
			this.Attributes = field.Attributes;
			this.Treatment = treatment;
		}

		// Token: 0x0400007D RID: 125
		public readonly FieldAttributes Attributes;

		// Token: 0x0400007E RID: 126
		public readonly FieldDefinitionTreatment Treatment;
	}
}
