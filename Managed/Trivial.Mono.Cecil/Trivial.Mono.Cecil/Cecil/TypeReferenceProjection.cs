using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C4 RID: 196
	internal sealed class TypeReferenceProjection
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00019BE9 File Offset: 0x00017DE9
		public TypeReferenceProjection(TypeReference type, TypeReferenceTreatment treatment)
		{
			this.Name = type.Name;
			this.Namespace = type.Namespace;
			this.Scope = type.Scope;
			this.Treatment = treatment;
		}

		// Token: 0x040002EF RID: 751
		public readonly string Name;

		// Token: 0x040002F0 RID: 752
		public readonly string Namespace;

		// Token: 0x040002F1 RID: 753
		public readonly IMetadataScope Scope;

		// Token: 0x040002F2 RID: 754
		public readonly TypeReferenceTreatment Treatment;
	}
}
