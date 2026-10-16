using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007D RID: 125
	internal sealed class MethodDefinitionProjection
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0001065C File Offset: 0x0000E85C
		public MethodDefinitionProjection(MethodDefinition method, MethodDefinitionTreatment treatment)
		{
			this.Attributes = method.Attributes;
			this.ImplAttributes = method.ImplAttributes;
			this.Name = method.Name;
			this.Treatment = treatment;
		}

		// Token: 0x0400016B RID: 363
		public readonly MethodAttributes Attributes;

		// Token: 0x0400016C RID: 364
		public readonly MethodImplAttributes ImplAttributes;

		// Token: 0x0400016D RID: 365
		public readonly string Name;

		// Token: 0x0400016E RID: 366
		public readonly MethodDefinitionTreatment Treatment;
	}
}
