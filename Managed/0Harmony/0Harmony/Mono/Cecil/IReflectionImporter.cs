using System;
using System.Reflection;

namespace Mono.Cecil
{
	// Token: 0x02000221 RID: 545
	public interface IReflectionImporter
	{
		// Token: 0x06000BB6 RID: 2998
		AssemblyNameReference ImportReference(AssemblyName reference);

		// Token: 0x06000BB7 RID: 2999
		TypeReference ImportReference(Type type, IGenericParameterProvider context);

		// Token: 0x06000BB8 RID: 3000
		FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context);

		// Token: 0x06000BB9 RID: 3001
		MethodReference ImportReference(MethodBase method, IGenericParameterProvider context);
	}
}
