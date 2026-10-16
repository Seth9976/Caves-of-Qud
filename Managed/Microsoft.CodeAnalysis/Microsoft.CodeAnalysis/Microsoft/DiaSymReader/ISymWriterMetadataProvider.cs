using System;
using System.Reflection;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000EE RID: 238
	internal interface ISymWriterMetadataProvider
	{
		// Token: 0x060007DB RID: 2011
		bool TryGetTypeDefinitionInfo(int typeDefinitionToken, out string namespaceName, out string typeName, out TypeAttributes attributes);

		// Token: 0x060007DC RID: 2012
		bool TryGetEnclosingType(int nestedTypeToken, out int enclosingTypeToken);

		// Token: 0x060007DD RID: 2013
		bool TryGetMethodInfo(int methodDefinitionToken, out string methodName, out int declaringTypeToken);
	}
}
