using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000597 RID: 1431
	internal sealed class SynthesizedLambdaCacheFieldSymbol : SynthesizedFieldSymbolBase, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x060077D6 RID: 30678 RVA: 0x001B8720 File Offset: 0x001B6920
		public SynthesizedLambdaCacheFieldSymbol(NamedTypeSymbol containingType, TypeSymbol type, string name, MethodSymbol topLevelMethod, bool isReadOnly, bool isStatic)
			: base(containingType, name, true, isReadOnly, isStatic)
		{
			this._type = TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this._topLevelMethod = topLevelMethod;
		}

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x060077D7 RID: 30679 RVA: 0x001B8758 File Offset: 0x001B6958
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x060077D8 RID: 30680 RVA: 0x001B875B File Offset: 0x001B695B
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this._topLevelMethod;
			}
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x060077D9 RID: 30681 RVA: 0x001B8763 File Offset: 0x001B6963
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060077DA RID: 30682 RVA: 0x001B8766 File Offset: 0x001B6966
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._type;
		}

		// Token: 0x04001CE6 RID: 7398
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001CE7 RID: 7399
		private readonly MethodSymbol _topLevelMethod;
	}
}
