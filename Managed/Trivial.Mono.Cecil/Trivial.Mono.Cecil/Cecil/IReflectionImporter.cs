using System;
using System.Reflection;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000061 RID: 97
	public interface IReflectionImporter
	{
		// Token: 0x060002C9 RID: 713
		TypeReference ImportReference(Type type, IGenericParameterProvider context);

		// Token: 0x060002CA RID: 714
		FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context);

		// Token: 0x060002CB RID: 715
		MethodReference ImportReference(MethodBase method, IGenericParameterProvider context);
	}
}
