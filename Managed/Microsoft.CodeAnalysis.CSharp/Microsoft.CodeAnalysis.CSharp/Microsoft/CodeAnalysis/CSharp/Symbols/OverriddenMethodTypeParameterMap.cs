using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000554 RID: 1364
	internal sealed class OverriddenMethodTypeParameterMap : OverriddenMethodTypeParameterMapBase
	{
		// Token: 0x060073D6 RID: 29654 RVA: 0x001B0F7F File Offset: 0x001AF17F
		public OverriddenMethodTypeParameterMap(SourceOrdinaryMethodSymbol overridingMethod)
			: base(overridingMethod)
		{
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x001B0F88 File Offset: 0x001AF188
		protected override MethodSymbol GetOverriddenMethod(SourceOrdinaryMethodSymbol overridingMethod)
		{
			MethodSymbol methodSymbol = overridingMethod;
			do
			{
				methodSymbol = methodSymbol.OverriddenMethod;
			}
			while (methodSymbol != null && methodSymbol.IsOverride);
			return methodSymbol;
		}
	}
}
