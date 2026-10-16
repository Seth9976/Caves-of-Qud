using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000063 RID: 99
	internal readonly struct CallingConventionInfo
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x00049080 File Offset: 0x00047280
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public CallingConventionInfo(CallingConvention callKind, ImmutableHashSet<CustomModifier> unmanagedCallingConventionTypes)
		{
			this.CallKind = callKind;
			this.UnmanagedCallingConventionTypes = unmanagedCallingConventionTypes;
		}

		// Token: 0x0400041A RID: 1050
		internal readonly CallingConvention CallKind;

		// Token: 0x0400041B RID: 1051
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		internal readonly ImmutableHashSet<CustomModifier> UnmanagedCallingConventionTypes;
	}
}
