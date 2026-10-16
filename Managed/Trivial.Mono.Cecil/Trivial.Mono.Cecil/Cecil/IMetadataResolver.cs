using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000055 RID: 85
	public interface IMetadataResolver
	{
		// Token: 0x06000284 RID: 644
		TypeDefinition Resolve(TypeReference type);

		// Token: 0x06000285 RID: 645
		FieldDefinition Resolve(FieldReference field);

		// Token: 0x06000286 RID: 646
		MethodDefinition Resolve(MethodReference method);
	}
}
