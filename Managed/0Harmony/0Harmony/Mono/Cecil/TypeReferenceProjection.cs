using System;

namespace Mono.Cecil
{
	// Token: 0x02000280 RID: 640
	internal sealed class TypeReferenceProjection
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x000312C7 File Offset: 0x0002F4C7
		public TypeReferenceProjection(TypeReference type, TypeReferenceTreatment treatment)
		{
			this.Name = type.Name;
			this.Namespace = type.Namespace;
			this.Scope = type.Scope;
			this.Treatment = treatment;
		}

		// Token: 0x040005B4 RID: 1460
		public readonly string Name;

		// Token: 0x040005B5 RID: 1461
		public readonly string Namespace;

		// Token: 0x040005B6 RID: 1462
		public readonly IMetadataScope Scope;

		// Token: 0x040005B7 RID: 1463
		public readonly TypeReferenceTreatment Treatment;
	}
}
