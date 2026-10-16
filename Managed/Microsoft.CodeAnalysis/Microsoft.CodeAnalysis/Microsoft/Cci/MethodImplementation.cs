using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000DB RID: 219
	[NullableContext(1)]
	[Nullable(0)]
	internal struct MethodImplementation
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00018039 File Offset: 0x00016239
		public MethodImplementation(IMethodDefinition ImplementingMethod, IMethodReference ImplementedMethod)
		{
			this.ImplementingMethod = ImplementingMethod;
			this.ImplementedMethod = ImplementedMethod;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00018049 File Offset: 0x00016249
		public ITypeDefinition ContainingType
		{
			get
			{
				return this.ImplementingMethod.ContainingTypeDefinition;
			}
		}

		// Token: 0x0400021A RID: 538
		public readonly IMethodDefinition ImplementingMethod;

		// Token: 0x0400021B RID: 539
		public readonly IMethodReference ImplementedMethod;
	}
}
