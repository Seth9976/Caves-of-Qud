using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200021F RID: 543
	internal static class NullableAnnotationExtensions
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x000DA56C File Offset: 0x000D876C
		public static bool IsAnnotated(this NullableAnnotation annotation)
		{
			return annotation == NullableAnnotation.Annotated;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000DA572 File Offset: 0x000D8772
		public static bool IsNotAnnotated(this NullableAnnotation annotation)
		{
			return annotation == NullableAnnotation.NotAnnotated;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000DA578 File Offset: 0x000D8778
		public static bool IsOblivious(this NullableAnnotation annotation)
		{
			return annotation == NullableAnnotation.Oblivious;
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000DA57E File Offset: 0x000D877E
		public static NullableAnnotation Join(this NullableAnnotation a, NullableAnnotation b)
		{
			if (a >= b)
			{
				return a;
			}
			return b;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000DA587 File Offset: 0x000D8787
		public static NullableAnnotation Meet(this NullableAnnotation a, NullableAnnotation b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000DA590 File Offset: 0x000D8790
		public static NullableAnnotation EnsureCompatible(this NullableAnnotation a, NullableAnnotation b)
		{
			NullableAnnotation nullableAnnotation;
			if (a != NullableAnnotation.Oblivious)
			{
				if (b != NullableAnnotation.Oblivious)
				{
					nullableAnnotation = ((a < b) ? a : b);
				}
				else
				{
					nullableAnnotation = a;
				}
			}
			else
			{
				nullableAnnotation = b;
			}
			return nullableAnnotation;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000DA5BC File Offset: 0x000D87BC
		public static NullableAnnotation MergeNullableAnnotation(this NullableAnnotation a, NullableAnnotation b, VarianceKind variance)
		{
			NullableAnnotation nullableAnnotation;
			switch (variance)
			{
			case VarianceKind.None:
				nullableAnnotation = a.EnsureCompatible(b);
				break;
			case VarianceKind.Out:
				nullableAnnotation = a.Join(b);
				break;
			case VarianceKind.In:
				nullableAnnotation = a.Meet(b);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(variance);
			}
			return nullableAnnotation;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000DA608 File Offset: 0x000D8808
		internal static NullabilityInfo ToNullabilityInfo(this NullableAnnotation annotation, TypeSymbol type)
		{
			if (annotation == NullableAnnotation.None)
			{
				return default(NullabilityInfo);
			}
			return annotation.ToInternalAnnotation().ToNullabilityInfo(type);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000DA630 File Offset: 0x000D8830
		internal static NullabilityInfo ToNullabilityInfo(this NullableAnnotation annotation, TypeSymbol type)
		{
			NullableFlowState state = TypeWithAnnotations.Create(type, annotation, default(ImmutableArray<CustomModifier>)).ToTypeWithState().State;
			return new NullabilityInfo(NullableAnnotationExtensions.ToPublicAnnotation(type, annotation), state.ToPublicFlowState());
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000DA66D File Offset: 0x000D886D
		internal static ITypeSymbol GetPublicSymbol(this TypeWithAnnotations type)
		{
			TypeSymbol type2 = type.Type;
			if (type2 == null)
			{
				return null;
			}
			return type2.GetITypeSymbol(type.ToPublicAnnotation());
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000DA687 File Offset: 0x000D8887
		internal static ImmutableArray<ITypeSymbol> GetPublicSymbols(this ImmutableArray<TypeWithAnnotations> types)
		{
			return types.SelectAsArray<TypeWithAnnotations, ITypeSymbol>((TypeWithAnnotations t) => t.GetPublicSymbol());
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000DA6AE File Offset: 0x000D88AE
		internal static NullableAnnotation ToPublicAnnotation(this TypeWithAnnotations type)
		{
			return NullableAnnotationExtensions.ToPublicAnnotation(type.Type, type.NullableAnnotation);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000DA6C2 File Offset: 0x000D88C2
		internal static ImmutableArray<NullableAnnotation> ToPublicAnnotations(this ImmutableArray<TypeWithAnnotations> types)
		{
			return types.SelectAsArray<TypeWithAnnotations, NullableAnnotation>((TypeWithAnnotations t) => t.ToPublicAnnotation());
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000DA6EC File Offset: 0x000D88EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static NullableAnnotation ToPublicAnnotation(TypeSymbol type, NullableAnnotation annotation)
		{
			NullableAnnotation nullableAnnotation;
			switch (annotation)
			{
			case NullableAnnotation.NotAnnotated:
				nullableAnnotation = NullableAnnotation.NotAnnotated;
				break;
			case NullableAnnotation.Oblivious:
				if (type != null && type.IsValueType)
				{
					nullableAnnotation = NullableAnnotation.NotAnnotated;
				}
				else
				{
					nullableAnnotation = NullableAnnotation.None;
				}
				break;
			case NullableAnnotation.Annotated:
				nullableAnnotation = NullableAnnotation.Annotated;
				break;
			case NullableAnnotation.Ignored:
				nullableAnnotation = NullableAnnotation.None;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(annotation);
			}
			return nullableAnnotation;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000DA740 File Offset: 0x000D8940
		internal static NullableAnnotation ToInternalAnnotation(this NullableAnnotation annotation)
		{
			NullableAnnotation nullableAnnotation;
			switch (annotation)
			{
			case NullableAnnotation.None:
				nullableAnnotation = NullableAnnotation.Oblivious;
				break;
			case NullableAnnotation.NotAnnotated:
				nullableAnnotation = NullableAnnotation.NotAnnotated;
				break;
			case NullableAnnotation.Annotated:
				nullableAnnotation = NullableAnnotation.Annotated;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(annotation);
			}
			return nullableAnnotation;
		}

		// Token: 0x04001138 RID: 4408
		public const byte NotAnnotatedAttributeValue = 1;

		// Token: 0x04001139 RID: 4409
		public const byte AnnotatedAttributeValue = 2;

		// Token: 0x0400113A RID: 4410
		public const byte ObliviousAttributeValue = 0;
	}
}
