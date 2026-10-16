using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000553 RID: 1363
	internal abstract class OverriddenMethodTypeParameterMapBase
	{
		// Token: 0x060073D0 RID: 29648 RVA: 0x001B0EB5 File Offset: 0x001AF0B5
		protected OverriddenMethodTypeParameterMapBase(SourceOrdinaryMethodSymbol overridingMethod)
		{
			this._overridingMethod = overridingMethod;
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x060073D1 RID: 29649 RVA: 0x001B0ECF File Offset: 0x001AF0CF
		public SourceOrdinaryMethodSymbol OverridingMethod
		{
			get
			{
				return this._overridingMethod;
			}
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x001B0ED8 File Offset: 0x001AF0D8
		public TypeParameterSymbol GetOverriddenTypeParameter(int ordinal)
		{
			MethodSymbol overriddenMethod = this.OverriddenMethod;
			if (overriddenMethod == null)
			{
				return null;
			}
			return overriddenMethod.TypeParameters[ordinal];
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x060073D3 RID: 29651 RVA: 0x001B0F00 File Offset: 0x001AF100
		public TypeMap TypeMap
		{
			get
			{
				if (this._lazyTypeMap == null)
				{
					MethodSymbol overriddenMethod = this.OverriddenMethod;
					if (overriddenMethod != null)
					{
						ImmutableArray<TypeParameterSymbol> typeParameters = overriddenMethod.TypeParameters;
						ImmutableArray<TypeParameterSymbol> typeParameters2 = this._overridingMethod.TypeParameters;
						TypeMap typeMap = new TypeMap(typeParameters, typeParameters2, true);
						Interlocked.CompareExchange<TypeMap>(ref this._lazyTypeMap, typeMap, null);
					}
				}
				return this._lazyTypeMap;
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x060073D4 RID: 29652 RVA: 0x001B0F4D File Offset: 0x001AF14D
		private MethodSymbol OverriddenMethod
		{
			get
			{
				if (this._lazyOverriddenMethod == ErrorMethodSymbol.UnknownMethod)
				{
					Interlocked.CompareExchange<MethodSymbol>(ref this._lazyOverriddenMethod, this.GetOverriddenMethod(this._overridingMethod), ErrorMethodSymbol.UnknownMethod);
				}
				return this._lazyOverriddenMethod;
			}
		}

		// Token: 0x060073D5 RID: 29653
		protected abstract MethodSymbol GetOverriddenMethod(SourceOrdinaryMethodSymbol overridingMethod);

		// Token: 0x04001C1B RID: 7195
		private readonly SourceOrdinaryMethodSymbol _overridingMethod;

		// Token: 0x04001C1C RID: 7196
		private TypeMap _lazyTypeMap;

		// Token: 0x04001C1D RID: 7197
		private MethodSymbol _lazyOverriddenMethod = ErrorMethodSymbol.UnknownMethod;
	}
}
