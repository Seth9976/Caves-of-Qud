using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000555 RID: 1365
	internal sealed class ExplicitInterfaceMethodTypeParameterMap : OverriddenMethodTypeParameterMapBase
	{
		// Token: 0x060073D8 RID: 29656 RVA: 0x001B0FAA File Offset: 0x001AF1AA
		public ExplicitInterfaceMethodTypeParameterMap(SourceOrdinaryMethodSymbol implementationMethod)
			: base(implementationMethod)
		{
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x001B0FB4 File Offset: 0x001AF1B4
		protected override MethodSymbol GetOverriddenMethod(SourceOrdinaryMethodSymbol overridingMethod)
		{
			ImmutableArray<MethodSymbol> explicitInterfaceImplementations = overridingMethod.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.Length <= 0)
			{
				return null;
			}
			return explicitInterfaceImplementations[0];
		}
	}
}
