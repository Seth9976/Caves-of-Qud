using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000009 RID: 9
	public interface ICodeSecurityValidator
	{
		// Token: 0x0600004E RID: 78
		bool IsAssemblyReferenceAllowed(string assemblyReference);

		// Token: 0x0600004F RID: 79
		bool IsNamespaceReferenceAllowed(string namespaceName);

		// Token: 0x06000050 RID: 80
		bool IsTypeReferenceAllowed(TypeReference reference);

		// Token: 0x06000051 RID: 81
		bool IsMemberReferenceAllowed(MemberReference reference);
	}
}
