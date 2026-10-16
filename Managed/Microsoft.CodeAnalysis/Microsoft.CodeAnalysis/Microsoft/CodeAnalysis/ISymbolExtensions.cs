using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000250 RID: 592
	[NullableContext(1)]
	[Nullable(0)]
	public static class ISymbolExtensions
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x000455B0 File Offset: 0x000437B0
		[return: Nullable(2)]
		public static IMethodSymbol GetConstructedReducedFrom(this IMethodSymbol method)
		{
			if (method.MethodKind != MethodKind.ReducedExtension)
			{
				return null;
			}
			IMethodSymbol reducedFrom = method.ReducedFrom;
			if (!reducedFrom.IsGenericMethod)
			{
				return reducedFrom;
			}
			ITypeSymbol[] array = new ITypeSymbol[reducedFrom.TypeParameters.Length];
			int i = 0;
			int length = method.TypeParameters.Length;
			while (i < length)
			{
				ITypeSymbol typeSymbol = method.TypeArguments[i];
				ITypeParameterSymbol typeParameterSymbol = method.TypeParameters[i];
				if (typeSymbol.Equals(typeParameterSymbol))
				{
					typeSymbol = typeParameterSymbol.ReducedFrom;
				}
				array[typeParameterSymbol.ReducedFrom.Ordinal] = typeSymbol;
				i++;
			}
			int j = 0;
			int length2 = reducedFrom.TypeParameters.Length;
			while (j < length2)
			{
				ITypeSymbol typeInferredDuringReduction = method.GetTypeInferredDuringReduction(reducedFrom.TypeParameters[j]);
				if (typeInferredDuringReduction != null)
				{
					array[j] = typeInferredDuringReduction;
				}
				j++;
			}
			return reducedFrom.Construct(array);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0004569C File Offset: 0x0004389C
		internal static bool IsDefaultTupleElement(this IFieldSymbol field)
		{
			return field == field.CorrespondingTupleField;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000456A7 File Offset: 0x000438A7
		internal static bool IsTupleElement(this IFieldSymbol field)
		{
			return field.CorrespondingTupleField != null;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000456B2 File Offset: 0x000438B2
		[return: Nullable(2)]
		internal static string ProvidedTupleElementNameOrNull(this IFieldSymbol field)
		{
			if (!field.IsTupleElement() || field.IsImplicitlyDeclared)
			{
				return null;
			}
			return field.Name;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000456CC File Offset: 0x000438CC
		[return: Nullable(2)]
		internal static INamespaceSymbol GetNestedNamespace(this INamespaceSymbol container, string name)
		{
			foreach (INamespaceOrTypeSymbol namespaceOrTypeSymbol in container.GetMembers(name))
			{
				if (namespaceOrTypeSymbol.Kind == SymbolKind.Namespace)
				{
					return (INamespaceSymbol)namespaceOrTypeSymbol;
				}
			}
			return null;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0004572C File Offset: 0x0004392C
		internal static bool IsNetModule(this IAssemblySymbol assembly)
		{
			ISourceAssemblySymbol sourceAssemblySymbol = assembly as ISourceAssemblySymbol;
			return sourceAssemblySymbol != null && sourceAssemblySymbol.Compilation.Options.OutputKind.IsNetModule();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0004575C File Offset: 0x0004395C
		internal static bool IsInSource(this ISymbol symbol)
		{
			ImmutableArray<Location>.Enumerator enumerator = symbol.Locations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsInSource)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00045794 File Offset: 0x00043994
		[NullableContext(0)]
		internal static IVTConclusion PerformIVTCheck(this AssemblyIdentity assemblyGrantingAccessIdentity, ImmutableArray<byte> assemblyWantingAccessKey, ImmutableArray<byte> grantedToPublicKey)
		{
			bool isStrongName = assemblyGrantingAccessIdentity.IsStrongName;
			bool flag = !grantedToPublicKey.IsDefaultOrEmpty;
			bool flag2 = !assemblyWantingAccessKey.IsDefaultOrEmpty;
			bool flag3 = flag && flag2 && ByteSequenceComparer.Equals(grantedToPublicKey, assemblyWantingAccessKey);
			if (flag && !flag3)
			{
				return IVTConclusion.PublicKeyDoesntMatch;
			}
			if (!isStrongName && flag2)
			{
				return IVTConclusion.OneSignedOneNot;
			}
			return IVTConclusion.Match;
		}
	}
}
