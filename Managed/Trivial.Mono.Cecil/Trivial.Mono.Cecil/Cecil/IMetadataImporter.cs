using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000053 RID: 83
	public interface IMetadataImporter
	{
		// Token: 0x06000280 RID: 640
		TypeReference ImportReference(TypeReference type, IGenericParameterProvider context);

		// Token: 0x06000281 RID: 641
		FieldReference ImportReference(FieldReference field, IGenericParameterProvider context);

		// Token: 0x06000282 RID: 642
		MethodReference ImportReference(MethodReference method, IGenericParameterProvider context);
	}
}
