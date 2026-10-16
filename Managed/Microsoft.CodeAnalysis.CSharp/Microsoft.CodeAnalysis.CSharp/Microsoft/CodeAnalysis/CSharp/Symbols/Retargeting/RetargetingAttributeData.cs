using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D2 RID: 1490
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RetargetingAttributeData : SourceAttributeData
	{
		// Token: 0x06007CA5 RID: 31909 RVA: 0x001C1950 File Offset: 0x001BFB50
		internal RetargetingAttributeData(SyntaxReference applicationNode, NamedTypeSymbol attributeClass, MethodSymbol attributeConstructor, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypedConstant> constructorArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> constructorArgumentsSourceIndices, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments, bool hasErrors, bool isConditionallyOmitted)
			: base(applicationNode, attributeClass, attributeConstructor, constructorArguments, constructorArgumentsSourceIndices, namedArguments, hasErrors, isConditionallyOmitted)
		{
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x001C1970 File Offset: 0x001BFB70
		internal override TypeSymbol GetSystemType(Symbol targetSymbol)
		{
			RetargetingAssemblySymbol retargetingAssemblySymbol = (RetargetingAssemblySymbol)((targetSymbol.Kind == SymbolKind.Assembly) ? targetSymbol : targetSymbol.ContainingAssembly);
			TypeSymbol wellKnownType = retargetingAssemblySymbol.UnderlyingAssembly.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Type);
			return ((RetargetingModuleSymbol)retargetingAssemblySymbol.Modules[0]).RetargetingTranslator.Retarget(wellKnownType, RetargetOptions.RetargetPrimitiveTypesByTypeCode);
		}
	}
}
