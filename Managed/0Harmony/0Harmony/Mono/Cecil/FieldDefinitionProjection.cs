using System;

namespace Mono.Cecil
{
	// Token: 0x02000282 RID: 642
	internal sealed class FieldDefinitionProjection
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003132D File Offset: 0x0002F52D
		public FieldDefinitionProjection(FieldDefinition field, FieldDefinitionTreatment treatment)
		{
			this.Attributes = field.Attributes;
			this.Treatment = treatment;
		}

		// Token: 0x040005BC RID: 1468
		public readonly FieldAttributes Attributes;

		// Token: 0x040005BD RID: 1469
		public readonly FieldDefinitionTreatment Treatment;
	}
}
