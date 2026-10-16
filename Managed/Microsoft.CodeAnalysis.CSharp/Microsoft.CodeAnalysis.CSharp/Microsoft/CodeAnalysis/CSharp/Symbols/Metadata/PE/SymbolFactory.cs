using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005F2 RID: 1522
	internal sealed class SymbolFactory : SymbolFactory<PEModuleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>
	{
		// Token: 0x06007FDB RID: 32731 RVA: 0x001CC769 File Offset: 0x001CA969
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetMDArrayTypeSymbol(PEModuleSymbol moduleSymbol, int rank, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol elementType, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> customModifiers, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			if (elementType is UnsupportedMetadataTypeSymbol)
			{
				return elementType;
			}
			return ArrayTypeSymbol.CreateMDArray(moduleSymbol.ContainingAssembly, SymbolFactory.CreateType(elementType, customModifiers), rank, sizes, lowerBounds);
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x001CC78D File Offset: 0x001CA98D
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetSpecialType(PEModuleSymbol moduleSymbol, SpecialType specialType)
		{
			return moduleSymbol.ContainingAssembly.GetSpecialType(specialType);
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x001CC79B File Offset: 0x001CA99B
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetSystemTypeSymbol(PEModuleSymbol moduleSymbol)
		{
			return moduleSymbol.SystemTypeSymbol;
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x001CC7A3 File Offset: 0x001CA9A3
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol MakePointerTypeSymbol(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> customModifiers)
		{
			if (type is UnsupportedMetadataTypeSymbol)
			{
				return type;
			}
			return new PointerTypeSymbol(SymbolFactory.CreateType(type, customModifiers));
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x001CC7BB File Offset: 0x001CA9BB
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol MakeFunctionPointerTypeSymbol(CallingConvention callingConvention, ImmutableArray<ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> retAndParamTypes)
		{
			return FunctionPointerTypeSymbol.CreateFromMetadata(callingConvention, retAndParamTypes);
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x001CC7C4 File Offset: 0x001CA9C4
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetEnumUnderlyingType(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			return type.GetEnumUnderlyingType();
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x001CC7CC File Offset: 0x001CA9CC
		internal override PrimitiveTypeCode GetPrimitiveTypeCode(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			return type.PrimitiveTypeCode;
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x001CC7D4 File Offset: 0x001CA9D4
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetSZArrayTypeSymbol(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol elementType, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> customModifiers)
		{
			if (elementType is UnsupportedMetadataTypeSymbol)
			{
				return elementType;
			}
			return ArrayTypeSymbol.CreateSZArray(moduleSymbol.ContainingAssembly, SymbolFactory.CreateType(elementType, customModifiers));
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x001CC7F2 File Offset: 0x001CA9F2
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetUnsupportedMetadataTypeSymbol(PEModuleSymbol moduleSymbol, BadImageFormatException exception)
		{
			return new UnsupportedMetadataTypeSymbol(exception);
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x001CC7FC File Offset: 0x001CA9FC
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol SubstituteTypeParameters(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol genericTypeDef, ImmutableArray<KeyValuePair<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>>>> arguments, ImmutableArray<bool> refersToNoPiaLocalType)
		{
			if (genericTypeDef is UnsupportedMetadataTypeSymbol)
			{
				return genericTypeDef;
			}
			foreach (KeyValuePair<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>>> keyValuePair in arguments)
			{
				if (keyValuePair.Key.Kind == SymbolKind.ErrorType && keyValuePair.Key is UnsupportedMetadataTypeSymbol)
				{
					return new UnsupportedMetadataTypeSymbol(null);
				}
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)genericTypeDef;
			ImmutableArray<AssemblySymbol> linkedReferencedAssemblies = moduleSymbol.ContainingAssembly.GetLinkedReferencedAssemblies();
			bool flag = false;
			if (!linkedReferencedAssemblies.IsDefaultOrEmpty || moduleSymbol.Module.ContainsNoPiaLocalTypes())
			{
				NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol;
				int num = refersToNoPiaLocalType.Length - 1;
				while (namedTypeSymbol2.IsInterface)
				{
					num -= namedTypeSymbol2.Arity;
					namedTypeSymbol2 = namedTypeSymbol2.ContainingType;
					if (namedTypeSymbol2 == null)
					{
						break;
					}
				}
				for (int i = num; i >= 0; i--)
				{
					if (refersToNoPiaLocalType[i] || (!linkedReferencedAssemblies.IsDefaultOrEmpty && MetadataDecoder.IsOrClosedOverATypeFromAssemblies(arguments[i].Key, linkedReferencedAssemblies)))
					{
						flag = true;
						break;
					}
				}
			}
			ImmutableArray<TypeParameterSymbol> allTypeParameters = namedTypeSymbol.GetAllTypeParameters();
			if (allTypeParameters.Length != arguments.Length)
			{
				return new UnsupportedMetadataTypeSymbol(null);
			}
			NamedTypeSymbol namedTypeSymbol3 = new TypeMap(allTypeParameters, arguments.SelectAsArray<KeyValuePair<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>>>, TypeWithAnnotations>((KeyValuePair<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>>> arg) => SymbolFactory.CreateType(arg.Key, arg.Value)), false).SubstituteNamedType(namedTypeSymbol);
			if (flag)
			{
				namedTypeSymbol3 = new NoPiaIllegalGenericInstantiationSymbol(moduleSymbol, namedTypeSymbol3);
			}
			return namedTypeSymbol3;
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x001CC954 File Offset: 0x001CAB54
		internal override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol MakeUnboundIfGeneric(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null || !namedTypeSymbol.IsGenericType)
			{
				return type;
			}
			return namedTypeSymbol.AsUnboundGenericType();
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x001CC97B File Offset: 0x001CAB7B
		private static TypeWithAnnotations CreateType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> customModifiers)
		{
			return TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, CSharpCustomModifier.Convert(customModifiers));
		}

		// Token: 0x04001E28 RID: 7720
		internal static readonly SymbolFactory Instance = new SymbolFactory();
	}
}
