using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A4 RID: 1444
	internal sealed class TypeMap : AbstractTypeParameterMap
	{
		// Token: 0x060078B5 RID: 30901 RVA: 0x001B98C8 File Offset: 0x001B7AC8
		internal static ImmutableArray<TypeWithAnnotations> TypeParametersAsTypeSymbolsWithAnnotations(ImmutableArray<TypeParameterSymbol> typeParameters)
		{
			return typeParameters.SelectAsArray<TypeParameterSymbol, TypeWithAnnotations>((TypeParameterSymbol tp) => TypeWithAnnotations.Create(tp, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x001B98EF File Offset: 0x001B7AEF
		internal static ImmutableArray<TypeWithAnnotations> TypeParametersAsTypeSymbolsWithIgnoredAnnotations(ImmutableArray<TypeParameterSymbol> typeParameters)
		{
			return typeParameters.SelectAsArray<TypeParameterSymbol, TypeWithAnnotations>((TypeParameterSymbol tp) => TypeWithAnnotations.Create(tp, NullableAnnotation.Ignored, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x001B9918 File Offset: 0x001B7B18
		internal static ImmutableArray<TypeSymbol> AsTypeSymbols(ImmutableArray<TypeWithAnnotations> typesOpt)
		{
			if (!typesOpt.IsDefault)
			{
				return typesOpt.SelectAsArray<TypeWithAnnotations, TypeSymbol>(TypeMap.AsTypeSymbol);
			}
			return default(ImmutableArray<TypeSymbol>);
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x001B9943 File Offset: 0x001B7B43
		internal TypeMap(ImmutableArray<TypeParameterSymbol> from, ImmutableArray<TypeWithAnnotations> to, bool allowAlpha = false)
			: base(TypeMap.ConstructMapping(from, to))
		{
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x001B9952 File Offset: 0x001B7B52
		internal TypeMap(ImmutableArray<TypeParameterSymbol> from, ImmutableArray<TypeParameterSymbol> to, bool allowAlpha = false)
			: this(from, TypeMap.TypeParametersAsTypeSymbolsWithAnnotations(to), allowAlpha)
		{
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x001B9962 File Offset: 0x001B7B62
		private TypeMap(SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> mapping)
			: base(new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>(mapping, ReferenceEqualityComparer.Instance))
		{
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x001B9978 File Offset: 0x001B7B78
		private static SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> ForType(NamedTypeSymbol containingType)
		{
			SubstitutedNamedTypeSymbol substitutedNamedTypeSymbol = containingType as SubstitutedNamedTypeSymbol;
			if (substitutedNamedTypeSymbol == null)
			{
				return new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>(ReferenceEqualityComparer.Instance);
			}
			return new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>(substitutedNamedTypeSymbol.TypeSubstitution.Mapping, ReferenceEqualityComparer.Instance);
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x001B99B0 File Offset: 0x001B7BB0
		internal TypeMap(NamedTypeSymbol containingType, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeWithAnnotations> typeArguments)
			: base(TypeMap.ForType(containingType))
		{
			for (int i = 0; i < typeParameters.Length; i++)
			{
				TypeParameterSymbol typeParameterSymbol = typeParameters[i];
				TypeWithAnnotations typeWithAnnotations = typeArguments[i];
				if (!typeWithAnnotations.Is(typeParameterSymbol))
				{
					this.Mapping.Add(typeParameterSymbol, typeWithAnnotations);
				}
			}
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x001B9A04 File Offset: 0x001B7C04
		private TypeMap()
			: base(TypeMap.s_emptyDictionary)
		{
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x060078BE RID: 30910 RVA: 0x001B9A11 File Offset: 0x001B7C11
		public static TypeMap Empty
		{
			get
			{
				return TypeMap.s_emptyTypeMap;
			}
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x001B9A18 File Offset: 0x001B7C18
		private TypeMap WithAlphaRename(ImmutableArray<TypeParameterSymbol> oldTypeParameters, Symbol newOwner, out ImmutableArray<TypeParameterSymbol> newTypeParameters)
		{
			if (oldTypeParameters.Length == 0)
			{
				newTypeParameters = ImmutableArray<TypeParameterSymbol>.Empty;
				return this;
			}
			TypeMap typeMap = new TypeMap(this.Mapping);
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			bool flag = oldTypeParameters[0].ContainingSymbol.OriginalDefinition != newOwner.OriginalDefinition;
			int num = 0;
			foreach (TypeParameterSymbol typeParameterSymbol in oldTypeParameters)
			{
				SubstitutedTypeParameterSymbol substitutedTypeParameterSymbol = (flag ? new SynthesizedSubstitutedTypeParameterSymbol(newOwner, typeMap, typeParameterSymbol, num) : new SubstitutedTypeParameterSymbol(newOwner, typeMap, typeParameterSymbol, num));
				typeMap.Mapping.Add(typeParameterSymbol, TypeWithAnnotations.Create(substitutedTypeParameterSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				instance.Add(substitutedTypeParameterSymbol);
				num++;
			}
			newTypeParameters = instance.ToImmutableAndFree();
			return typeMap;
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x001B9ADD File Offset: 0x001B7CDD
		internal TypeMap WithAlphaRename(NamedTypeSymbol oldOwner, NamedTypeSymbol newOwner, out ImmutableArray<TypeParameterSymbol> newTypeParameters)
		{
			return this.WithAlphaRename(oldOwner.OriginalDefinition.TypeParameters, newOwner, out newTypeParameters);
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x001B9AF2 File Offset: 0x001B7CF2
		internal TypeMap WithAlphaRename(MethodSymbol oldOwner, Symbol newOwner, out ImmutableArray<TypeParameterSymbol> newTypeParameters)
		{
			return this.WithAlphaRename(oldOwner.OriginalDefinition.TypeParameters, newOwner, out newTypeParameters);
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x001B9B08 File Offset: 0x001B7D08
		internal TypeMap WithConcatAlphaRename(MethodSymbol oldOwner, Symbol newOwner, out ImmutableArray<TypeParameterSymbol> newTypeParameters, out ImmutableArray<TypeParameterSymbol> oldTypeParameters, MethodSymbol stopAt = null)
		{
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			while (oldOwner != null && oldOwner != stopAt)
			{
				ImmutableArray<TypeParameterSymbol> typeParameters = oldOwner.OriginalDefinition.TypeParameters;
				for (int i = typeParameters.Length - 1; i >= 0; i--)
				{
					instance.Add(typeParameters[i]);
				}
				oldOwner = oldOwner.ContainingSymbol.OriginalDefinition as MethodSymbol;
			}
			instance.ReverseContents();
			oldTypeParameters = instance.ToImmutableAndFree();
			return this.WithAlphaRename(oldTypeParameters, newOwner, out newTypeParameters);
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x001B9B94 File Offset: 0x001B7D94
		private static SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> ConstructMapping(ImmutableArray<TypeParameterSymbol> from, ImmutableArray<TypeWithAnnotations> to)
		{
			SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> smallDictionary = new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>(ReferenceEqualityComparer.Instance);
			for (int i = 0; i < from.Length; i++)
			{
				TypeParameterSymbol typeParameterSymbol = from[i];
				TypeWithAnnotations typeWithAnnotations = to[i];
				if (!typeWithAnnotations.Is(typeParameterSymbol))
				{
					smallDictionary.Add(typeParameterSymbol, typeWithAnnotations);
				}
			}
			return smallDictionary;
		}

		// Token: 0x04001D0D RID: 7437
		public static readonly Func<TypeWithAnnotations, TypeSymbol> AsTypeSymbol = (TypeWithAnnotations t) => t.Type;

		// Token: 0x04001D0E RID: 7438
		private static readonly SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> s_emptyDictionary = new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>(ReferenceEqualityComparer.Instance);

		// Token: 0x04001D0F RID: 7439
		private static readonly TypeMap s_emptyTypeMap = new TypeMap();
	}
}
