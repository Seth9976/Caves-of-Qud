using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200048A RID: 1162
	[NullableContext(1)]
	internal interface IPEDeltaAssemblyBuilder
	{
		// Token: 0x06002DBD RID: 11709
		void OnCreatedIndices(DiagnosticBag diagnostics);

		// Token: 0x06002DBE RID: 11710
		IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> GetAnonymousTypeMap();
	}
}
