using System;

namespace Mono.Cecil
{
	// Token: 0x0200021F RID: 543
	public interface IMetadataImporter
	{
		// Token: 0x06000BB1 RID: 2993
		AssemblyNameReference ImportReference(AssemblyNameReference reference);

		// Token: 0x06000BB2 RID: 2994
		TypeReference ImportReference(TypeReference type, IGenericParameterProvider context);

		// Token: 0x06000BB3 RID: 2995
		FieldReference ImportReference(FieldReference field, IGenericParameterProvider context);

		// Token: 0x06000BB4 RID: 2996
		MethodReference ImportReference(MethodReference method, IGenericParameterProvider context);
	}
}
