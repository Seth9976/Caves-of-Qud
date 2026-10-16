using System;

namespace Mono.Cecil
{
	// Token: 0x02000281 RID: 641
	internal sealed class MethodDefinitionProjection
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x000312FA File Offset: 0x0002F4FA
		public MethodDefinitionProjection(MethodDefinition method, MethodDefinitionTreatment treatment)
		{
			this.Attributes = method.Attributes;
			this.ImplAttributes = method.ImplAttributes;
			this.Name = method.Name;
			this.Treatment = treatment;
		}

		// Token: 0x040005B8 RID: 1464
		public readonly MethodAttributes Attributes;

		// Token: 0x040005B9 RID: 1465
		public readonly MethodImplAttributes ImplAttributes;

		// Token: 0x040005BA RID: 1466
		public readonly string Name;

		// Token: 0x040005BB RID: 1467
		public readonly MethodDefinitionTreatment Treatment;
	}
}
