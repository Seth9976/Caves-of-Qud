using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000044 RID: 68
	internal static class AccessCheck
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0003CDD4 File Offset: 0x0003AFD4
		public static bool IsSymbolAccessible(Symbol symbol, AssemblySymbol within, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag;
			return AccessCheck.IsSymbolAccessibleCore(symbol, within, null, out flag, within.DeclaringCompilation, ref useSiteInfo, null);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0003CDF4 File Offset: 0x0003AFF4
		public static bool IsSymbolAccessible(Symbol symbol, NamedTypeSymbol within, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, TypeSymbol throughTypeOpt = null)
		{
			bool flag;
			return AccessCheck.IsSymbolAccessibleCore(symbol, within, throughTypeOpt, out flag, within.DeclaringCompilation, ref useSiteInfo, null);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0003CE13 File Offset: 0x0003B013
		public static bool IsSymbolAccessible(Symbol symbol, NamedTypeSymbol within, TypeSymbol throughTypeOpt, out bool failedThroughTypeCheck, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			return AccessCheck.IsSymbolAccessibleCore(symbol, within, throughTypeOpt, out failedThroughTypeCheck, within.DeclaringCompilation, ref useSiteInfo, basesBeingResolved);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0003CE28 File Offset: 0x0003B028
		internal static bool IsEffectivelyPublicOrInternal(Symbol symbol, out bool isInternal)
		{
			SymbolKind kind = symbol.Kind;
			switch (kind)
			{
			case SymbolKind.Event:
			case SymbolKind.Field:
			case SymbolKind.Method:
			case SymbolKind.NamedType:
				goto IL_0050;
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				break;
			default:
				if (kind == SymbolKind.Property)
				{
					goto IL_0050;
				}
				if (kind == SymbolKind.TypeParameter)
				{
					symbol = symbol.ContainingSymbol;
					goto IL_0050;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			IL_0050:
			isInternal = false;
			for (;;)
			{
				switch (symbol.DeclaredAccessibility)
				{
				case Accessibility.Private:
					return false;
				case Accessibility.ProtectedAndInternal:
				case Accessibility.Internal:
					isInternal = true;
					goto IL_0094;
				case Accessibility.Protected:
				case Accessibility.ProtectedOrInternal:
				case Accessibility.Public:
					goto IL_0094;
				}
				break;
				IL_0094:
				symbol = symbol.ContainingType;
				if (symbol == null)
				{
					return true;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(symbol.DeclaredAccessibility);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		private static bool IsSymbolAccessibleCore(Symbol symbol, Symbol within, TypeSymbol throughTypeOpt, out bool failedThroughTypeCheck, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			failedThroughTypeCheck = false;
			switch (symbol.Kind)
			{
			case SymbolKind.Alias:
				return AccessCheck.IsSymbolAccessibleCore(((AliasSymbol)symbol).Target, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case SymbolKind.ArrayType:
				return AccessCheck.IsSymbolAccessibleCore(((ArrayTypeSymbol)symbol).ElementType, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case SymbolKind.Assembly:
			case SymbolKind.DynamicType:
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
			case SymbolKind.Namespace:
			case SymbolKind.Parameter:
			case SymbolKind.RangeVariable:
			case SymbolKind.TypeParameter:
				break;
			case SymbolKind.ErrorType:
				return true;
			case SymbolKind.Event:
			case SymbolKind.Field:
			case SymbolKind.Property:
				goto IL_0161;
			case SymbolKind.Method:
				if (((MethodSymbol)symbol).MethodKind != MethodKind.LocalFunction)
				{
					goto IL_0161;
				}
				break;
			case SymbolKind.NamedType:
				return AccessCheck.IsNamedTypeAccessible((NamedTypeSymbol)symbol, within, ref useSiteInfo, basesBeingResolved);
			case SymbolKind.PointerType:
				return AccessCheck.IsSymbolAccessibleCore(((PointerTypeSymbol)symbol).PointedAtType, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case SymbolKind.Preprocessing:
				goto IL_0186;
			case SymbolKind.Discard:
				return AccessCheck.IsSymbolAccessibleCore(((DiscardSymbol)symbol).TypeWithAnnotations.Type, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case SymbolKind.FunctionPointerType:
			{
				FunctionPointerTypeSymbol functionPointerTypeSymbol = (FunctionPointerTypeSymbol)symbol;
				if (!AccessCheck.IsSymbolAccessibleCore(functionPointerTypeSymbol.Signature.ReturnType, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved))
				{
					return false;
				}
				ImmutableArray<ParameterSymbol>.Enumerator enumerator = functionPointerTypeSymbol.Signature.Parameters.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!AccessCheck.IsSymbolAccessibleCore(enumerator.Current.Type, within, null, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved))
					{
						return false;
					}
				}
				return true;
			}
			default:
				goto IL_0186;
			}
			return true;
			IL_0161:
			if (!symbol.RequiresInstanceReceiver())
			{
				throughTypeOpt = null;
			}
			return AccessCheck.IsMemberAccessible(symbol.ContainingType, symbol.DeclaredAccessibility, within, throughTypeOpt, out failedThroughTypeCheck, compilation, ref useSiteInfo, null);
			IL_0186:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0003D07C File Offset: 0x0003B27C
		private static bool IsNamedTypeAccessible(NamedTypeSymbol type, Symbol within, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			CSharpCompilation declaringCompilation = within.DeclaringCompilation;
			if (!type.IsDefinition)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in type.TypeArgumentsWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
				{
					bool flag;
					if (typeWithAnnotations.Type.Kind != SymbolKind.TypeParameter && !AccessCheck.IsSymbolAccessibleCore(typeWithAnnotations.Type, within, null, out flag, declaringCompilation, ref useSiteInfo, basesBeingResolved))
					{
						return false;
					}
				}
			}
			NamedTypeSymbol containingType = type.ContainingType;
			if (containingType != null)
			{
				bool flag;
				return AccessCheck.IsMemberAccessible(containingType, type.DeclaredAccessibility, within, null, out flag, declaringCompilation, ref useSiteInfo, basesBeingResolved);
			}
			return AccessCheck.IsNonNestedTypeAccessible(type.ContainingAssembly, type.DeclaredAccessibility, within);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0003D114 File Offset: 0x0003B314
		private static bool IsNonNestedTypeAccessible(AssemblySymbol assembly, Accessibility declaredAccessibility, Symbol within)
		{
			switch (declaredAccessibility)
			{
			case Accessibility.NotApplicable:
			case Accessibility.Public:
				return true;
			case Accessibility.Private:
			case Accessibility.ProtectedAndInternal:
			case Accessibility.Protected:
				return false;
			case Accessibility.Internal:
			case Accessibility.ProtectedOrInternal:
			{
				NamedTypeSymbol namedTypeSymbol = within as NamedTypeSymbol;
				AssemblySymbol assemblySymbol = ((namedTypeSymbol != null) ? namedTypeSymbol.ContainingAssembly : ((AssemblySymbol)within));
				return assemblySymbol == assembly || assemblySymbol.HasInternalAccessTo(assembly);
			}
			default:
				throw ExceptionUtilities.UnexpectedValue(declaredAccessibility);
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0003D17B File Offset: 0x0003B37B
		private static bool IsMemberAccessible(NamedTypeSymbol containingType, Accessibility declaredAccessibility, Symbol within, TypeSymbol throughTypeOpt, out bool failedThroughTypeCheck, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			failedThroughTypeCheck = false;
			return containingType == within || (AccessCheck.IsNamedTypeAccessible(containingType, within, ref useSiteInfo, basesBeingResolved) && (declaredAccessibility == Accessibility.Public || AccessCheck.IsNonPublicMemberAccessible(containingType, declaredAccessibility, within, throughTypeOpt, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved)));
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
		private static bool IsNonPublicMemberAccessible(NamedTypeSymbol containingType, Accessibility declaredAccessibility, Symbol within, TypeSymbol throughTypeOpt, out bool failedThroughTypeCheck, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			failedThroughTypeCheck = false;
			NamedTypeSymbol originalDefinition = containingType.OriginalDefinition;
			NamedTypeSymbol namedTypeSymbol = within as NamedTypeSymbol;
			AssemblySymbol assemblySymbol = ((namedTypeSymbol != null) ? namedTypeSymbol.ContainingAssembly : ((AssemblySymbol)within));
			switch (declaredAccessibility)
			{
			case Accessibility.NotApplicable:
				return true;
			case Accessibility.Private:
				return containingType.TypeKind == TypeKind.Submission || (namedTypeSymbol != null && AccessCheck.IsPrivateSymbolAccessible(namedTypeSymbol, originalDefinition));
			case Accessibility.ProtectedAndInternal:
				return assemblySymbol.HasInternalAccessTo(containingType.ContainingAssembly) && AccessCheck.IsProtectedSymbolAccessible(namedTypeSymbol, throughTypeOpt, originalDefinition, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case Accessibility.Protected:
				return AccessCheck.IsProtectedSymbolAccessible(namedTypeSymbol, throughTypeOpt, originalDefinition, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			case Accessibility.Internal:
				return assemblySymbol.HasInternalAccessTo(containingType.ContainingAssembly);
			case Accessibility.ProtectedOrInternal:
				return assemblySymbol.HasInternalAccessTo(containingType.ContainingAssembly) || AccessCheck.IsProtectedSymbolAccessible(namedTypeSymbol, throughTypeOpt, originalDefinition, out failedThroughTypeCheck, compilation, ref useSiteInfo, basesBeingResolved);
			default:
				throw ExceptionUtilities.UnexpectedValue(declaredAccessibility);
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0003D288 File Offset: 0x0003B488
		private static bool IsProtectedSymbolAccessible(NamedTypeSymbol withinType, TypeSymbol throughTypeOpt, NamedTypeSymbol originalContainingType, out bool failedThroughTypeCheck, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			failedThroughTypeCheck = false;
			if (originalContainingType.TypeKind == TypeKind.Submission)
			{
				return true;
			}
			if (withinType == null)
			{
				return false;
			}
			if (AccessCheck.IsNestedWithinOriginalContainingType(withinType, originalContainingType))
			{
				return true;
			}
			NamedTypeSymbol namedTypeSymbol = withinType.OriginalDefinition;
			TypeSymbol typeSymbol = ((throughTypeOpt == null) ? null : throughTypeOpt.OriginalDefinition);
			while (namedTypeSymbol != null)
			{
				if (namedTypeSymbol.InheritsFromOrImplementsIgnoringConstruction(originalContainingType, compilation, ref useSiteInfo, basesBeingResolved))
				{
					if (typeSymbol == null || typeSymbol.InheritsFromOrImplementsIgnoringConstruction(namedTypeSymbol, compilation, ref useSiteInfo, null))
					{
						return true;
					}
					failedThroughTypeCheck = true;
				}
				namedTypeSymbol = namedTypeSymbol.ContainingType;
			}
			return false;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		private static bool IsPrivateSymbolAccessible(Symbol within, NamedTypeSymbol originalContainingType)
		{
			NamedTypeSymbol namedTypeSymbol = within as NamedTypeSymbol;
			return namedTypeSymbol != null && AccessCheck.IsNestedWithinOriginalContainingType(namedTypeSymbol, originalContainingType);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0003D31C File Offset: 0x0003B51C
		private static bool IsNestedWithinOriginalContainingType(NamedTypeSymbol withinType, NamedTypeSymbol originalContainingType)
		{
			for (NamedTypeSymbol namedTypeSymbol = withinType.OriginalDefinition; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
			{
				if (namedTypeSymbol == originalContainingType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0003D344 File Offset: 0x0003B544
		private static bool InheritsFromOrImplementsIgnoringConstruction(this TypeSymbol type, NamedTypeSymbol baseType, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved = null)
		{
			PooledHashSet<NamedTypeSymbol> pooledHashSet = null;
			ArrayBuilder<NamedTypeSymbol> arrayBuilder = null;
			bool isInterface = baseType.IsInterface;
			if (isInterface)
			{
				pooledHashSet = PooledHashSet<NamedTypeSymbol>.GetInstance();
				arrayBuilder = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			}
			PooledHashSet<NamedTypeSymbol> pooledHashSet2 = null;
			TypeSymbol typeSymbol = type;
			bool flag = false;
			while (typeSymbol != null)
			{
				if (isInterface == typeSymbol.IsInterfaceType() && typeSymbol == baseType)
				{
					flag = true;
					break;
				}
				if (isInterface)
				{
					AccessCheck.<InheritsFromOrImplementsIgnoringConstruction>g__getBaseInterfaces|12_0(typeSymbol, arrayBuilder, pooledHashSet, basesBeingResolved);
				}
				TypeSymbol nextBaseTypeNoUseSiteDiagnostics = typeSymbol.GetNextBaseTypeNoUseSiteDiagnostics(basesBeingResolved, compilation, ref pooledHashSet2);
				if (nextBaseTypeNoUseSiteDiagnostics == null)
				{
					typeSymbol = null;
				}
				else
				{
					typeSymbol = nextBaseTypeNoUseSiteDiagnostics.OriginalDefinition;
					typeSymbol.AddUseSiteInfo(ref useSiteInfo, true);
				}
			}
			if (pooledHashSet2 != null)
			{
				pooledHashSet2.Free();
			}
			if (!flag && isInterface)
			{
				while (arrayBuilder.Count != 0)
				{
					NamedTypeSymbol namedTypeSymbol = arrayBuilder.Pop<NamedTypeSymbol>();
					if (namedTypeSymbol.IsInterface)
					{
						if (namedTypeSymbol == baseType)
						{
							flag = true;
							break;
						}
						AccessCheck.<InheritsFromOrImplementsIgnoringConstruction>g__getBaseInterfaces|12_0(namedTypeSymbol, arrayBuilder, pooledHashSet, basesBeingResolved);
					}
				}
				if (!flag)
				{
					foreach (NamedTypeSymbol namedTypeSymbol2 in pooledHashSet)
					{
						namedTypeSymbol2.AddUseSiteInfo(ref useSiteInfo, true);
					}
				}
			}
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			return flag;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0003D460 File Offset: 0x0003B660
		internal static bool HasInternalAccessTo(this AssemblySymbol fromAssembly, AssemblySymbol toAssembly)
		{
			return object.Equals(fromAssembly, toAssembly) || fromAssembly.AreInternalsVisibleToThisAssembly(toAssembly) || (fromAssembly.IsInteractive && toAssembly.IsInteractive);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0003D48B File Offset: 0x0003B68B
		internal static ErrorCode GetProtectedMemberInSealedTypeError(NamedTypeSymbol containingType)
		{
			if (containingType.TypeKind != TypeKind.Struct)
			{
				return ErrorCode.WRN_ProtectedInSealed;
			}
			return ErrorCode.ERR_ProtectedInStruct;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		[CompilerGenerated]
		internal static void <InheritsFromOrImplementsIgnoringConstruction>g__getBaseInterfaces|12_0(TypeSymbol derived, ArrayBuilder<NamedTypeSymbol> baseInterfaces, PooledHashSet<NamedTypeSymbol> interfacesLookedAt, ConsList<TypeSymbol> basesBeingResolved)
		{
			if (basesBeingResolved != null && basesBeingResolved.ContainsReference(derived))
			{
				return;
			}
			TypeParameterSymbol typeParameterSymbol = derived as TypeParameterSymbol;
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (typeParameterSymbol == null)
			{
				NamedTypeSymbol namedTypeSymbol = derived as NamedTypeSymbol;
				if (namedTypeSymbol == null)
				{
					immutableArray = derived.InterfacesNoUseSiteDiagnostics(basesBeingResolved);
				}
				else
				{
					immutableArray = namedTypeSymbol.GetDeclaredInterfaces(basesBeingResolved);
				}
			}
			else
			{
				immutableArray = typeParameterSymbol.AllEffectiveInterfacesNoUseSiteDiagnostics;
			}
			foreach (NamedTypeSymbol namedTypeSymbol2 in immutableArray)
			{
				NamedTypeSymbol originalDefinition = namedTypeSymbol2.OriginalDefinition;
				if (interfacesLookedAt.Add(originalDefinition))
				{
					baseInterfaces.Add(originalDefinition);
				}
			}
		}
	}
}
