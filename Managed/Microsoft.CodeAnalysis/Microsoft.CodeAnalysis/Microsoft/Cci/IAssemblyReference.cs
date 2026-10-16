using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Microsoft.Cci
{
	// Token: 0x020000E6 RID: 230
	internal interface IAssemblyReference : IModuleReference, IUnitReference, IReference, INamedEntity
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000758 RID: 1880
		[Nullable(1)]
		AssemblyIdentity Identity
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000759 RID: 1881
		[Nullable(2)]
		Version AssemblyVersionPattern
		{
			[NullableContext(2)]
			get;
		}
	}
}
