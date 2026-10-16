using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050E RID: 1294
	internal static class PEPropertyOrEventHelpers
	{
		// Token: 0x06006CB9 RID: 27833 RVA: 0x00191C0A File Offset: 0x0018FE0A
		internal static ISet<PropertySymbol> GetPropertiesForExplicitlyImplementedAccessor(MethodSymbol accessor)
		{
			return PEPropertyOrEventHelpers.GetSymbolsForExplicitlyImplementedAccessor<PropertySymbol>(accessor);
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x00191C12 File Offset: 0x0018FE12
		internal static ISet<EventSymbol> GetEventsForExplicitlyImplementedAccessor(MethodSymbol accessor)
		{
			return PEPropertyOrEventHelpers.GetSymbolsForExplicitlyImplementedAccessor<EventSymbol>(accessor);
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x00191C1C File Offset: 0x0018FE1C
		private static ISet<T> GetSymbolsForExplicitlyImplementedAccessor<T>(MethodSymbol accessor) where T : Symbol
		{
			if (accessor == null)
			{
				return SpecializedCollections.EmptySet<T>();
			}
			ImmutableArray<MethodSymbol> explicitInterfaceImplementations = accessor.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.Length == 0)
			{
				return SpecializedCollections.EmptySet<T>();
			}
			HashSet<T> hashSet = new HashSet<T>();
			foreach (MethodSymbol methodSymbol in explicitInterfaceImplementations)
			{
				T t = methodSymbol.AssociatedSymbol as T;
				if (t != null)
				{
					hashSet.Add(t);
				}
			}
			return hashSet;
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x00191C89 File Offset: 0x0018FE89
		internal static Accessibility GetDeclaredAccessibilityFromAccessors(MethodSymbol accessor1, MethodSymbol accessor2)
		{
			if (accessor1 == null)
			{
				if (accessor2 != null)
				{
					return accessor2.DeclaredAccessibility;
				}
				return Accessibility.NotApplicable;
			}
			else
			{
				if (accessor2 == null)
				{
					return accessor1.DeclaredAccessibility;
				}
				return PEPropertyOrEventHelpers.GetDeclaredAccessibilityFromAccessors(accessor1.DeclaredAccessibility, accessor2.DeclaredAccessibility);
			}
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x00191CB8 File Offset: 0x0018FEB8
		internal static Accessibility GetDeclaredAccessibilityFromAccessors(Accessibility accessibility1, Accessibility accessibility2)
		{
			int num = (int)((accessibility1 > accessibility2) ? accessibility2 : accessibility1);
			Accessibility accessibility3 = ((accessibility1 > accessibility2) ? accessibility1 : accessibility2);
			if (num != 3 || accessibility3 != Accessibility.Internal)
			{
				return accessibility3;
			}
			return Accessibility.ProtectedOrInternal;
		}
	}
}
