using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000E8 RID: 232
	internal interface IModuleReference : IUnitReference, IReference, INamedEntity
	{
		// Token: 0x0600075F RID: 1887
		[NullableContext(1)]
		IAssemblyReference GetContainingAssembly(EmitContext context);
	}
}
