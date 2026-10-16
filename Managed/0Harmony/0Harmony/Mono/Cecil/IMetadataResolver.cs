using System;

namespace Mono.Cecil
{
	// Token: 0x02000231 RID: 561
	public interface IMetadataResolver
	{
		// Token: 0x06000C31 RID: 3121
		TypeDefinition Resolve(TypeReference type);

		// Token: 0x06000C32 RID: 3122
		FieldDefinition Resolve(FieldReference field);

		// Token: 0x06000C33 RID: 3123
		MethodDefinition Resolve(MethodReference method);
	}
}
