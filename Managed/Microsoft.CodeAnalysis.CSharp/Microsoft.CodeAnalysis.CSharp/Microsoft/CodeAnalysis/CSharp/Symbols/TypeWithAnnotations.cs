using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AA RID: 1450
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal readonly struct TypeWithAnnotations : IFormattable
	{
		// Token: 0x060079B3 RID: 31155 RVA: 0x001BDA7C File Offset: 0x001BBC7C
		private TypeWithAnnotations(TypeSymbol defaultType, NullableAnnotation nullableAnnotation, TypeWithAnnotations.Extensions extensions)
		{
			this.DefaultType = defaultType;
			this.NullableAnnotation = nullableAnnotation;
			this._extensions = extensions;
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x001BDA93 File Offset: 0x001BBC93
		public override string ToString()
		{
			return this.Type.ToString();
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x001BDAA0 File Offset: 0x001BBCA0
		internal static TypeWithAnnotations Create(bool isNullableEnabled, TypeSymbol typeSymbol, bool isAnnotated = false)
		{
			if (typeSymbol == null)
			{
				return default(TypeWithAnnotations);
			}
			return TypeWithAnnotations.Create(typeSymbol, isAnnotated ? NullableAnnotation.Annotated : (isNullableEnabled ? NullableAnnotation.NotAnnotated : NullableAnnotation.Oblivious), default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x001BDAD8 File Offset: 0x001BBCD8
		internal static TypeWithAnnotations Create(TypeSymbol typeSymbol, NullableAnnotation nullableAnnotation = NullableAnnotation.Oblivious, ImmutableArray<CustomModifier> customModifiers = default(ImmutableArray<CustomModifier>))
		{
			if (typeSymbol == null && nullableAnnotation == NullableAnnotation.NotAnnotated)
			{
				return default(TypeWithAnnotations);
			}
			if (nullableAnnotation <= NullableAnnotation.Oblivious && typeSymbol != null && typeSymbol.IsNullableType())
			{
				nullableAnnotation = NullableAnnotation.Annotated;
			}
			return TypeWithAnnotations.CreateNonLazyType(typeSymbol, nullableAnnotation, customModifiers.NullToEmpty<CustomModifier>());
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x001BDB14 File Offset: 0x001BBD14
		internal TypeWithAnnotations AsAnnotated()
		{
			if (this.NullableAnnotation.IsAnnotated() || (this.Type.IsValueType && this.Type.IsNullableType()))
			{
				return this;
			}
			return TypeWithAnnotations.Create(this.Type, NullableAnnotation.Annotated, this.CustomModifiers);
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x001BDB64 File Offset: 0x001BBD64
		internal TypeWithAnnotations AsNotAnnotated()
		{
			if (this.NullableAnnotation.IsNotAnnotated() || (this.Type.IsValueType && !this.Type.IsNullableType()))
			{
				return this;
			}
			return TypeWithAnnotations.Create(this.Type, NullableAnnotation.NotAnnotated, this.CustomModifiers);
		}

		// Token: 0x060079B9 RID: 31161 RVA: 0x001BDBB4 File Offset: 0x001BBDB4
		internal NullableAnnotation GetValueNullableAnnotation()
		{
			if (this.NullableAnnotation.IsAnnotated())
			{
				return this.NullableAnnotation;
			}
			TypeSymbol type = this.Type;
			if (type != null && type.IsPossiblyNullableReferenceTypeTypeParameter())
			{
				return NullableAnnotation.Annotated;
			}
			if (this.Type.IsNullableTypeOrTypeParameter())
			{
				return NullableAnnotation.Annotated;
			}
			return this.NullableAnnotation;
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x060079BA RID: 31162 RVA: 0x001BDC00 File Offset: 0x001BBE00
		internal bool CanBeAssignedNull
		{
			get
			{
				NullableAnnotation nullableAnnotation = this.NullableAnnotation;
				if (nullableAnnotation == NullableAnnotation.NotAnnotated)
				{
					return this.Type.IsNullableTypeOrTypeParameter();
				}
				if (nullableAnnotation - NullableAnnotation.Oblivious <= 1)
				{
					return true;
				}
				throw ExceptionUtilities.UnexpectedValue(this.NullableAnnotation);
			}
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x001BDC3B File Offset: 0x001BBE3B
		private static TypeWithAnnotations CreateNonLazyType(TypeSymbol typeSymbol, NullableAnnotation nullableAnnotation, ImmutableArray<CustomModifier> customModifiers)
		{
			return new TypeWithAnnotations(typeSymbol, nullableAnnotation, TypeWithAnnotations.Extensions.Create(customModifiers));
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x001BDC4A File Offset: 0x001BBE4A
		private static TypeWithAnnotations CreateLazyNullableType(CSharpCompilation compilation, TypeWithAnnotations underlying)
		{
			return new TypeWithAnnotations(underlying.DefaultType, NullableAnnotation.Annotated, TypeWithAnnotations.Extensions.CreateLazy(compilation, underlying));
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x001BDC5F File Offset: 0x001BBE5F
		internal bool IsDefault
		{
			get
			{
				return this.DefaultType == null && this.NullableAnnotation == NullableAnnotation.NotAnnotated && (this._extensions == null || this._extensions == TypeWithAnnotations.Extensions.Default);
			}
		}

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x060079BE RID: 31166 RVA: 0x001BDC8A File Offset: 0x001BBE8A
		internal bool HasType
		{
			get
			{
				return this.DefaultType != null;
			}
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x001BDC98 File Offset: 0x001BBE98
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public TypeWithAnnotations SetIsAnnotated(CSharpCompilation compilation)
		{
			TypeWithAnnotations.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.compilation = compilation;
			CS$<>8__locals1.typeSymbol = this.Type;
			if (CS$<>8__locals1.typeSymbol.TypeKind != TypeKind.TypeParameter)
			{
				if (!CS$<>8__locals1.typeSymbol.IsValueType && !CS$<>8__locals1.typeSymbol.IsErrorType())
				{
					return TypeWithAnnotations.CreateNonLazyType(CS$<>8__locals1.typeSymbol, NullableAnnotation.Annotated, this.CustomModifiers);
				}
				return TypeWithAnnotations.<SetIsAnnotated>g__makeNullableT|21_0(ref CS$<>8__locals1);
			}
			else
			{
				if (((TypeParameterSymbol)CS$<>8__locals1.typeSymbol).TypeParameterKind == TypeParameterKind.Cref)
				{
					return TypeWithAnnotations.<SetIsAnnotated>g__makeNullableT|21_0(ref CS$<>8__locals1);
				}
				return TypeWithAnnotations.CreateLazyNullableType(CS$<>8__locals1.compilation, this);
			}
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x001BDD2A File Offset: 0x001BBF2A
		public void TryForceResolve(bool asValueType)
		{
			this._extensions.TryForceResolve(asValueType);
		}

		// Token: 0x060079C1 RID: 31169 RVA: 0x001BDD38 File Offset: 0x001BBF38
		private TypeWithAnnotations AsNullableReferenceType()
		{
			return this._extensions.AsNullableReferenceType(this);
		}

		// Token: 0x060079C2 RID: 31170 RVA: 0x001BDD4B File Offset: 0x001BBF4B
		public TypeWithAnnotations AsNotNullableReferenceType()
		{
			return this._extensions.AsNotNullableReferenceType(this);
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x001BDD60 File Offset: 0x001BBF60
		internal TypeWithAnnotations MergeEquivalentTypes(TypeWithAnnotations other, VarianceKind variance)
		{
			TypeSymbol type = other.Type;
			NullableAnnotation nullableAnnotation = this.NullableAnnotation.MergeNullableAnnotation(other.NullableAnnotation, variance);
			return TypeWithAnnotations.Create(this.Type.MergeEquivalentTypes(type, variance), nullableAnnotation, this.CustomModifiers);
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x001BDDA1 File Offset: 0x001BBFA1
		public TypeWithAnnotations WithModifiers(ImmutableArray<CustomModifier> customModifiers)
		{
			return this._extensions.WithModifiers(this, customModifiers);
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x060079C5 RID: 31173 RVA: 0x001BDDB5 File Offset: 0x001BBFB5
		public bool IsResolved
		{
			get
			{
				TypeWithAnnotations.Extensions extensions = this._extensions;
				return extensions == null || extensions.IsResolved;
			}
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x060079C6 RID: 31174 RVA: 0x001BDDC8 File Offset: 0x001BBFC8
		public TypeSymbol Type
		{
			get
			{
				TypeWithAnnotations.Extensions extensions = this._extensions;
				if (extensions == null)
				{
					return null;
				}
				return extensions.GetResolvedType(this.DefaultType);
			}
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x001BDDE1 File Offset: 0x001BBFE1
		public TypeSymbol NullableUnderlyingTypeOrSelf
		{
			get
			{
				return this._extensions.GetNullableUnderlyingTypeOrSelf(this.DefaultType);
			}
		}

		// Token: 0x060079C8 RID: 31176 RVA: 0x001BDDF4 File Offset: 0x001BBFF4
		public bool IsNullableType()
		{
			return this.Type.IsNullableType();
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x001BDE01 File Offset: 0x001BC001
		public ImmutableArray<CustomModifier> CustomModifiers
		{
			get
			{
				return this._extensions.CustomModifiers;
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x060079CA RID: 31178 RVA: 0x001BDE0E File Offset: 0x001BC00E
		public TypeKind TypeKind
		{
			get
			{
				return this.Type.TypeKind;
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x001BDE1B File Offset: 0x001BC01B
		public SpecialType SpecialType
		{
			get
			{
				return this._extensions.GetSpecialType(this.DefaultType);
			}
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x060079CC RID: 31180 RVA: 0x001BDE2E File Offset: 0x001BC02E
		public PrimitiveTypeCode PrimitiveTypeCode
		{
			get
			{
				return this.Type.PrimitiveTypeCode;
			}
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x001BDE3B File Offset: 0x001BC03B
		public bool IsVoidType()
		{
			return this._extensions.IsVoid(this.DefaultType);
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x001BDE4E File Offset: 0x001BC04E
		public bool IsSZArray()
		{
			return this._extensions.IsSZArray(this.DefaultType);
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x060079CF RID: 31183 RVA: 0x001BDE61 File Offset: 0x001BC061
		public bool IsStatic
		{
			get
			{
				return this._extensions.IsStatic(this.DefaultType);
			}
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x001BDE74 File Offset: 0x001BC074
		public bool IsRestrictedType(bool ignoreSpanLikeTypes = false)
		{
			return this._extensions.IsRestrictedType(this.DefaultType, ignoreSpanLikeTypes);
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x001BDE88 File Offset: 0x001BC088
		public string ToDisplayString(SymbolDisplayFormat format = null)
		{
			if (this.IsResolved || this.IsSafeToResolve())
			{
				string text = ((!this.HasType) ? "<null>" : this.Type.ToDisplayString(format));
				if (format != null)
				{
					if (this.NullableAnnotation.IsAnnotated() && format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier) && (!this.HasType || (!this.IsNullableType() && !this.Type.IsValueType)))
					{
						return text + "?";
					}
					if (this.NullableAnnotation.IsNotAnnotated() && format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier) && (!this.HasType || (!this.Type.IsValueType && !this.Type.IsTypeParameterDisallowingAnnotationInCSharp8())))
					{
						return text + "!";
					}
				}
				return text;
			}
			if (this.NullableAnnotation.IsAnnotated() && format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier))
			{
				return this.DefaultType.ToDisplayString(format) + "?";
			}
			return this.DefaultType.ToDisplayString(format);
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x001BDF9C File Offset: 0x001BC19C
		private bool IsSafeToResolve()
		{
			TypeParameterSymbol typeParameterSymbol = this.DefaultType as TypeParameterSymbol;
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = ((typeParameterSymbol != null) ? typeParameterSymbol.DeclaringMethod : null) as SourceOrdinaryMethodSymbol;
			return sourceOrdinaryMethodSymbol == null || sourceOrdinaryMethodSymbol.HasComplete(CompletionPart.StartMemberChecks) || (!sourceOrdinaryMethodSymbol.IsOverride && !sourceOrdinaryMethodSymbol.IsExplicitInterfaceImplementation);
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x001BDFEB File Offset: 0x001BC1EB
		internal string GetDebuggerDisplay()
		{
			if (this.HasType)
			{
				return this.ToDisplayString(TypeWithAnnotations.DebuggerDisplayFormat);
			}
			return "<null>";
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x001BE006 File Offset: 0x001BC206
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x001BE014 File Offset: 0x001BC214
		public bool Equals(TypeWithAnnotations other, TypeCompareKind comparison)
		{
			if (this.IsSameAs(other))
			{
				return true;
			}
			if (!this.HasType)
			{
				if (other.HasType)
				{
					return false;
				}
			}
			else if (!other.HasType || !this.TypeSymbolEquals(other, comparison))
			{
				return false;
			}
			if ((comparison & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) == TypeCompareKind.ConsiderEverything && !this.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(other.CustomModifiers, null))
			{
				return false;
			}
			NullableAnnotation nullableAnnotation = this.NullableAnnotation;
			NullableAnnotation nullableAnnotation2 = other.NullableAnnotation;
			if ((comparison & TypeCompareKind.IgnoreNullableModifiersForReferenceTypes) == TypeCompareKind.ConsiderEverything && nullableAnnotation2 != nullableAnnotation && ((comparison & TypeCompareKind.ObliviousNullableModifierMatchesAny) == TypeCompareKind.ConsiderEverything || (!nullableAnnotation.IsOblivious() && !nullableAnnotation2.IsOblivious())))
			{
				if (!this.HasType)
				{
					return false;
				}
				TypeSymbol type = this.Type;
				if (!type.IsValueType || type.IsNullableType())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x001BE0C7 File Offset: 0x001BC2C7
		internal bool TypeSymbolEquals(TypeWithAnnotations other, TypeCompareKind comparison)
		{
			return this._extensions.TypeSymbolEquals(this, other, comparison);
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x001BE0DC File Offset: 0x001BC2DC
		public bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return this.Type.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, this.CustomModifiers, owner, ref checkedTypes);
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x001BE0FE File Offset: 0x001BC2FE
		public bool IsAtLeastAsVisibleAs(Symbol sym, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.NullableUnderlyingTypeOrSelf.IsAtLeastAsVisibleAs(sym, ref useSiteInfo);
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x001BE10D File Offset: 0x001BC30D
		public TypeWithAnnotations SubstituteType(AbstractTypeMap typeMap)
		{
			return this._extensions.SubstituteType(this, typeMap);
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x001BE124 File Offset: 0x001BC324
		internal TypeWithAnnotations SubstituteTypeCore(AbstractTypeMap typeMap)
		{
			ImmutableArray<CustomModifier> immutableArray = typeMap.SubstituteCustomModifiers(this.CustomModifiers);
			TypeSymbol type = this.Type;
			TypeWithAnnotations typeWithAnnotations = typeMap.SubstituteType(type);
			if (!type.IsTypeParameter())
			{
				if (type.Equals(typeWithAnnotations.Type, TypeCompareKind.ConsiderEverything) && immutableArray == this.CustomModifiers)
				{
					return this;
				}
				if ((this.NullableAnnotation.IsOblivious() || (type.IsNullableType() && this.NullableAnnotation.IsAnnotated())) && immutableArray.IsEmpty)
				{
					return typeWithAnnotations;
				}
				return TypeWithAnnotations.Create(typeWithAnnotations.Type, this.NullableAnnotation, immutableArray);
			}
			else
			{
				if (typeWithAnnotations.Is((TypeParameterSymbol)type) && immutableArray == this.CustomModifiers)
				{
					return this;
				}
				if (this.Is((TypeParameterSymbol)type) && typeWithAnnotations.NullableAnnotation != NullableAnnotation.Ignored)
				{
					return typeWithAnnotations;
				}
				if (typeWithAnnotations.Type is PlaceholderTypeArgumentSymbol)
				{
					return typeWithAnnotations;
				}
				NullableAnnotation nullableAnnotation;
				if (this.NullableAnnotation.IsAnnotated() || typeWithAnnotations.NullableAnnotation.IsAnnotated())
				{
					nullableAnnotation = NullableAnnotation.Annotated;
				}
				else if (typeWithAnnotations.NullableAnnotation == NullableAnnotation.Ignored)
				{
					nullableAnnotation = this.NullableAnnotation;
				}
				else if (this.NullableAnnotation != NullableAnnotation.Oblivious)
				{
					if (!type.IsTypeParameterDisallowingAnnotationInCSharp8())
					{
						nullableAnnotation = this.NullableAnnotation;
					}
					else
					{
						nullableAnnotation = typeWithAnnotations.NullableAnnotation;
					}
				}
				else if (typeWithAnnotations.NullableAnnotation != NullableAnnotation.Oblivious)
				{
					nullableAnnotation = typeWithAnnotations.NullableAnnotation;
				}
				else
				{
					nullableAnnotation = this.NullableAnnotation;
				}
				return TypeWithAnnotations.CreateNonLazyType(typeWithAnnotations.Type, nullableAnnotation, immutableArray.Concat<CustomModifier>(typeWithAnnotations.CustomModifiers));
			}
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x001BE28E File Offset: 0x001BC48E
		public void ReportDiagnosticsIfObsolete(Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			this._extensions.ReportDiagnosticsIfObsolete(this, binder, syntax, diagnostics);
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x001BE2A4 File Offset: 0x001BC4A4
		private bool TypeSymbolEqualsCore(TypeWithAnnotations other, TypeCompareKind comparison)
		{
			return this.Type.Equals(other.Type, comparison);
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x001BE2B9 File Offset: 0x001BC4B9
		private void ReportDiagnosticsIfObsoleteCore(Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			binder.ReportDiagnosticsIfObsolete(diagnostics, this.Type, syntax, false);
		}

		// Token: 0x060079DE RID: 31198 RVA: 0x001BE2CF File Offset: 0x001BC4CF
		public TypeSymbol AsTypeSymbolOnly()
		{
			return this._extensions.AsTypeSymbolOnly(this.DefaultType);
		}

		// Token: 0x060079DF RID: 31199 RVA: 0x001BE2E4 File Offset: 0x001BC4E4
		public bool Is(TypeParameterSymbol other)
		{
			return this.NullableAnnotation.IsOblivious() && this.DefaultType == other && this.CustomModifiers.IsEmpty;
		}

		// Token: 0x060079E0 RID: 31200 RVA: 0x001BE317 File Offset: 0x001BC517
		public TypeWithAnnotations WithTypeAndModifiers(TypeSymbol typeSymbol, ImmutableArray<CustomModifier> customModifiers)
		{
			return this._extensions.WithTypeAndModifiers(this, typeSymbol, customModifiers);
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x001BE32C File Offset: 0x001BC52C
		public TypeWithAnnotations WithType(TypeSymbol typeSymbol)
		{
			return this._extensions.WithTypeAndModifiers(this, typeSymbol, this.CustomModifiers);
		}

		// Token: 0x060079E2 RID: 31202 RVA: 0x001BE346 File Offset: 0x001BC546
		public bool NeedsNullableAttribute()
		{
			return TypeWithAnnotations.NeedsNullableAttribute(this, null);
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x001BE354 File Offset: 0x001BC554
		public static bool NeedsNullableAttribute(TypeWithAnnotations typeWithAnnotationsOpt, TypeSymbol typeOpt)
		{
			return typeWithAnnotationsOpt.VisitType(typeOpt, (TypeWithAnnotations t, object a, bool b) => t.NullableAnnotation != NullableAnnotation.Oblivious && !t.Type.IsErrorType() && !t.Type.IsValueType, null, null, false, false) != null;
		}

		// Token: 0x060079E4 RID: 31204 RVA: 0x001BE384 File Offset: 0x001BC584
		private static bool IsNonGenericValueType(TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			if (namedTypeSymbol.IsGenericType)
			{
				return type.IsNullableType();
			}
			return type.IsValueType;
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x001BE3B2 File Offset: 0x001BC5B2
		public void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			TypeWithAnnotations.AddNullableTransforms(this, transforms);
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x001BE3C0 File Offset: 0x001BC5C0
		private static void AddNullableTransforms(TypeWithAnnotations typeWithAnnotations, ArrayBuilder<byte> transforms)
		{
			TypeSymbol type;
			for (;;)
			{
				type = typeWithAnnotations.Type;
				if (!TypeWithAnnotations.IsNonGenericValueType(type))
				{
					NullableAnnotation nullableAnnotation = typeWithAnnotations.NullableAnnotation;
					byte b;
					if (nullableAnnotation.IsOblivious() || type.IsValueType)
					{
						b = 0;
					}
					else if (nullableAnnotation.IsAnnotated())
					{
						b = 2;
					}
					else
					{
						b = 1;
					}
					transforms.Add(b);
				}
				if (type.TypeKind != TypeKind.Array)
				{
					break;
				}
				typeWithAnnotations = ((ArrayTypeSymbol)type).ElementTypeWithAnnotations;
			}
			type.AddNullableTransforms(transforms);
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x001BE42C File Offset: 0x001BC62C
		public bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, out TypeWithAnnotations result)
		{
			result = this;
			TypeSymbol type = this.Type;
			byte b;
			if (TypeWithAnnotations.IsNonGenericValueType(type))
			{
				b = 0;
			}
			else if (transforms.IsDefault)
			{
				b = defaultTransformFlag;
			}
			else
			{
				if (position >= transforms.Length)
				{
					return false;
				}
				int num = position;
				position = num + 1;
				b = transforms[num];
			}
			TypeSymbol typeSymbol;
			if (!type.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeSymbol))
			{
				return false;
			}
			if (type != typeSymbol)
			{
				result = result.WithTypeAndModifiers(typeSymbol, result.CustomModifiers);
			}
			switch (b)
			{
			case 0:
				if (result.NullableAnnotation != NullableAnnotation.Oblivious && (!result.NullableAnnotation.IsAnnotated() || !type.IsNullableType()))
				{
					result = TypeWithAnnotations.CreateNonLazyType(typeSymbol, NullableAnnotation.Oblivious, result.CustomModifiers);
				}
				break;
			case 1:
				result = result.AsNotNullableReferenceType();
				break;
			case 2:
				result = result.AsNullableReferenceType();
				break;
			default:
				result = this;
				return false;
			}
			return true;
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x001BE52C File Offset: 0x001BC72C
		public TypeWithAnnotations WithTopLevelNonNullability()
		{
			TypeSymbol type = this.Type;
			if (this.NullableAnnotation.IsNotAnnotated() || (type.IsValueType && !type.IsNullableType()))
			{
				return this;
			}
			return TypeWithAnnotations.CreateNonLazyType(type, NullableAnnotation.NotAnnotated, this.CustomModifiers);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x001BE574 File Offset: 0x001BC774
		public TypeWithAnnotations SetUnknownNullabilityForReferenceTypes()
		{
			TypeSymbol type = this.Type;
			TypeSymbol typeSymbol = type.SetUnknownNullabilityForReferenceTypes();
			if (this.NullableAnnotation != NullableAnnotation.Oblivious && !type.IsValueType)
			{
				return TypeWithAnnotations.CreateNonLazyType(typeSymbol, NullableAnnotation.Oblivious, this.CustomModifiers);
			}
			if (typeSymbol != type)
			{
				return this.WithTypeAndModifiers(typeSymbol, this.CustomModifiers);
			}
			return this;
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x001BE5C8 File Offset: 0x001BC7C8
		[Obsolete("Unsupported", true)]
		public override bool Equals(object other)
		{
			if (other is TypeWithAnnotations)
			{
				TypeWithAnnotations typeWithAnnotations = (TypeWithAnnotations)other;
				return this.Equals(typeWithAnnotations, TypeCompareKind.ConsiderEverything);
			}
			return false;
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x001BE5EE File Offset: 0x001BC7EE
		[Obsolete("Unsupported", true)]
		public override int GetHashCode()
		{
			if (!this.HasType)
			{
				return 0;
			}
			return this.Type.GetHashCode();
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x001BE608 File Offset: 0x001BC808
		public static bool operator ==(TypeWithAnnotations? x, TypeWithAnnotations? y)
		{
			return x != null == (y != null) && (x == null || x.GetValueOrDefault().IsSameAs(y.GetValueOrDefault()));
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x001BE648 File Offset: 0x001BC848
		public static bool operator !=(TypeWithAnnotations? x, TypeWithAnnotations? y)
		{
			return !(x == y);
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x001BE654 File Offset: 0x001BC854
		internal bool IsSameAs(TypeWithAnnotations other)
		{
			return this.DefaultType == other.DefaultType && this.NullableAnnotation == other.NullableAnnotation && this._extensions == other._extensions;
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x001BE682 File Offset: 0x001BC882
		internal TypeWithState ToTypeWithState()
		{
			return TypeWithState.Create(this.Type, TypeWithAnnotations.<ToTypeWithState>g__getFlowState|78_0(this.Type, this.NullableAnnotation));
		}

		// Token: 0x060079F1 RID: 31217 RVA: 0x001BE6DC File Offset: 0x001BC8DC
		[CompilerGenerated]
		internal static TypeWithAnnotations <SetIsAnnotated>g__makeNullableT|21_0(ref TypeWithAnnotations.<>c__DisplayClass21_0 A_0)
		{
			return TypeWithAnnotations.Create(A_0.compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(ImmutableArray.Create<TypeSymbol>(A_0.typeSymbol)), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x001BE718 File Offset: 0x001BC918
		[CompilerGenerated]
		internal static NullableFlowState <ToTypeWithState>g__getFlowState|78_0(TypeSymbol type, NullableAnnotation annotation)
		{
			if (type == null)
			{
				if (!annotation.IsAnnotated())
				{
					return NullableFlowState.NotNull;
				}
				return NullableFlowState.MaybeDefault;
			}
			else
			{
				NullableFlowState nullableFlowState;
				if (type.IsPossiblyNullableReferenceTypeTypeParameter())
				{
					if (annotation != NullableAnnotation.NotAnnotated)
					{
						if (annotation == NullableAnnotation.Annotated)
						{
							nullableFlowState = NullableFlowState.MaybeDefault;
						}
						else
						{
							nullableFlowState = NullableFlowState.NotNull;
						}
					}
					else
					{
						nullableFlowState = NullableFlowState.MaybeNull;
					}
					return nullableFlowState;
				}
				if (type.IsTypeParameterDisallowingAnnotationInCSharp8())
				{
					if (annotation == NullableAnnotation.Annotated)
					{
						nullableFlowState = NullableFlowState.MaybeDefault;
					}
					else
					{
						nullableFlowState = NullableFlowState.NotNull;
					}
					return nullableFlowState;
				}
				if (type.IsNullableTypeOrTypeParameter())
				{
					return NullableFlowState.MaybeNull;
				}
				if (annotation == NullableAnnotation.Annotated)
				{
					nullableFlowState = NullableFlowState.MaybeNull;
				}
				else
				{
					nullableFlowState = NullableFlowState.NotNull;
				}
				return nullableFlowState;
			}
		}

		// Token: 0x04001D1F RID: 7455
		internal readonly TypeSymbol DefaultType;

		// Token: 0x04001D20 RID: 7456
		private readonly TypeWithAnnotations.Extensions _extensions;

		// Token: 0x04001D21 RID: 7457
		public readonly NullableAnnotation NullableAnnotation;

		// Token: 0x04001D22 RID: 7458
		private static readonly SymbolDisplayFormat DebuggerDisplayFormat = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.None, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

		// Token: 0x04001D23 RID: 7459
		internal static readonly SymbolDisplayFormat TestDisplayFormat = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters, SymbolDisplayMemberOptions.None, SymbolDisplayDelegateStyle.NameOnly, SymbolDisplayExtensionMethodStyle.Default, SymbolDisplayParameterOptions.None, SymbolDisplayPropertyStyle.NameOnly, SymbolDisplayLocalOptions.None, SymbolDisplayKindOptions.None, SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier);

		// Token: 0x02000960 RID: 2400
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal sealed class Boxed
		{
			// Token: 0x06009112 RID: 37138 RVA: 0x001FCF45 File Offset: 0x001FB145
			internal Boxed(TypeWithAnnotations value)
			{
				this.Value = value;
			}

			// Token: 0x06009113 RID: 37139 RVA: 0x001FCF54 File Offset: 0x001FB154
			internal string GetDebuggerDisplay()
			{
				return this.Value.GetDebuggerDisplay();
			}

			// Token: 0x04002870 RID: 10352
			internal static readonly TypeWithAnnotations.Boxed Sentinel = new TypeWithAnnotations.Boxed(default(TypeWithAnnotations));

			// Token: 0x04002871 RID: 10353
			internal readonly TypeWithAnnotations Value;
		}

		// Token: 0x02000961 RID: 2401
		internal sealed class EqualsComparer : EqualityComparer<TypeWithAnnotations>
		{
			// Token: 0x06009115 RID: 37141 RVA: 0x001FCF84 File Offset: 0x001FB184
			public EqualsComparer(TypeCompareKind compareKind)
			{
				this._compareKind = compareKind;
			}

			// Token: 0x06009116 RID: 37142 RVA: 0x001FCF93 File Offset: 0x001FB193
			public override int GetHashCode(TypeWithAnnotations obj)
			{
				if (!obj.HasType)
				{
					return 0;
				}
				return obj.Type.GetHashCode();
			}

			// Token: 0x06009117 RID: 37143 RVA: 0x001FCFAC File Offset: 0x001FB1AC
			public override bool Equals(TypeWithAnnotations x, TypeWithAnnotations y)
			{
				if (!x.HasType)
				{
					return !y.HasType;
				}
				return x.Equals(y, this._compareKind);
			}

			// Token: 0x04002872 RID: 10354
			internal static readonly TypeWithAnnotations.EqualsComparer ConsiderEverythingComparer = new TypeWithAnnotations.EqualsComparer(TypeCompareKind.ConsiderEverything);

			// Token: 0x04002873 RID: 10355
			internal static readonly TypeWithAnnotations.EqualsComparer IgnoreNullableModifiersForReferenceTypesComparer = new TypeWithAnnotations.EqualsComparer(TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

			// Token: 0x04002874 RID: 10356
			private readonly TypeCompareKind _compareKind;
		}

		// Token: 0x02000962 RID: 2402
		private abstract class Extensions
		{
			// Token: 0x06009119 RID: 37145 RVA: 0x001FCFE8 File Offset: 0x001FB1E8
			internal static TypeWithAnnotations.Extensions Create(ImmutableArray<CustomModifier> customModifiers)
			{
				if (customModifiers.IsEmpty)
				{
					return TypeWithAnnotations.Extensions.Default;
				}
				return new TypeWithAnnotations.NonLazyType(customModifiers);
			}

			// Token: 0x0600911A RID: 37146 RVA: 0x001FCFFF File Offset: 0x001FB1FF
			internal static TypeWithAnnotations.Extensions CreateLazy(CSharpCompilation compilation, TypeWithAnnotations underlying)
			{
				return new TypeWithAnnotations.LazyNullableTypeParameter(compilation, underlying);
			}

			// Token: 0x170028E2 RID: 10466
			// (get) Token: 0x0600911B RID: 37147
			internal abstract bool IsResolved { get; }

			// Token: 0x0600911C RID: 37148
			internal abstract TypeSymbol GetResolvedType(TypeSymbol defaultType);

			// Token: 0x170028E3 RID: 10467
			// (get) Token: 0x0600911D RID: 37149
			internal abstract ImmutableArray<CustomModifier> CustomModifiers { get; }

			// Token: 0x0600911E RID: 37150
			internal abstract TypeWithAnnotations AsNullableReferenceType(TypeWithAnnotations type);

			// Token: 0x0600911F RID: 37151
			internal abstract TypeWithAnnotations AsNotNullableReferenceType(TypeWithAnnotations type);

			// Token: 0x06009120 RID: 37152
			internal abstract TypeWithAnnotations WithModifiers(TypeWithAnnotations type, ImmutableArray<CustomModifier> customModifiers);

			// Token: 0x06009121 RID: 37153
			internal abstract TypeSymbol GetNullableUnderlyingTypeOrSelf(TypeSymbol typeSymbol);

			// Token: 0x06009122 RID: 37154
			internal abstract TypeSymbol AsTypeSymbolOnly(TypeSymbol typeSymbol);

			// Token: 0x06009123 RID: 37155
			internal abstract SpecialType GetSpecialType(TypeSymbol typeSymbol);

			// Token: 0x06009124 RID: 37156
			internal abstract bool IsRestrictedType(TypeSymbol typeSymbol, bool ignoreSpanLikeTypes);

			// Token: 0x06009125 RID: 37157
			internal abstract bool IsStatic(TypeSymbol typeSymbol);

			// Token: 0x06009126 RID: 37158
			internal abstract bool IsVoid(TypeSymbol typeSymbol);

			// Token: 0x06009127 RID: 37159
			internal abstract bool IsSZArray(TypeSymbol typeSymbol);

			// Token: 0x06009128 RID: 37160
			internal abstract TypeWithAnnotations WithTypeAndModifiers(TypeWithAnnotations type, TypeSymbol typeSymbol, ImmutableArray<CustomModifier> customModifiers);

			// Token: 0x06009129 RID: 37161
			internal abstract bool TypeSymbolEquals(TypeWithAnnotations type, TypeWithAnnotations other, TypeCompareKind comparison);

			// Token: 0x0600912A RID: 37162
			internal abstract TypeWithAnnotations SubstituteType(TypeWithAnnotations type, AbstractTypeMap typeMap);

			// Token: 0x0600912B RID: 37163
			internal abstract void ReportDiagnosticsIfObsolete(TypeWithAnnotations type, Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics);

			// Token: 0x0600912C RID: 37164
			internal abstract void TryForceResolve(bool asValueType);

			// Token: 0x04002875 RID: 10357
			internal static readonly TypeWithAnnotations.Extensions Default = new TypeWithAnnotations.NonLazyType(ImmutableArray<CustomModifier>.Empty);
		}

		// Token: 0x02000963 RID: 2403
		private sealed class NonLazyType : TypeWithAnnotations.Extensions
		{
			// Token: 0x0600912F RID: 37167 RVA: 0x001FD021 File Offset: 0x001FB221
			public NonLazyType(ImmutableArray<CustomModifier> customModifiers)
			{
				this._customModifiers = customModifiers;
			}

			// Token: 0x170028E4 RID: 10468
			// (get) Token: 0x06009130 RID: 37168 RVA: 0x001FD030 File Offset: 0x001FB230
			internal override bool IsResolved
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06009131 RID: 37169 RVA: 0x001FD033 File Offset: 0x001FB233
			internal override TypeSymbol GetResolvedType(TypeSymbol defaultType)
			{
				return defaultType;
			}

			// Token: 0x170028E5 RID: 10469
			// (get) Token: 0x06009132 RID: 37170 RVA: 0x001FD036 File Offset: 0x001FB236
			internal override ImmutableArray<CustomModifier> CustomModifiers
			{
				get
				{
					return this._customModifiers;
				}
			}

			// Token: 0x06009133 RID: 37171 RVA: 0x001FD03E File Offset: 0x001FB23E
			internal override SpecialType GetSpecialType(TypeSymbol typeSymbol)
			{
				return typeSymbol.SpecialType;
			}

			// Token: 0x06009134 RID: 37172 RVA: 0x001FD046 File Offset: 0x001FB246
			internal override bool IsRestrictedType(TypeSymbol typeSymbol, bool ignoreSpanLikeTypes)
			{
				return typeSymbol.IsRestrictedType(ignoreSpanLikeTypes);
			}

			// Token: 0x06009135 RID: 37173 RVA: 0x001FD04F File Offset: 0x001FB24F
			internal override bool IsStatic(TypeSymbol typeSymbol)
			{
				return typeSymbol.IsStatic;
			}

			// Token: 0x06009136 RID: 37174 RVA: 0x001FD057 File Offset: 0x001FB257
			internal override bool IsVoid(TypeSymbol typeSymbol)
			{
				return typeSymbol.IsVoidType();
			}

			// Token: 0x06009137 RID: 37175 RVA: 0x001FD05F File Offset: 0x001FB25F
			internal override bool IsSZArray(TypeSymbol typeSymbol)
			{
				return typeSymbol.IsSZArray();
			}

			// Token: 0x06009138 RID: 37176 RVA: 0x001FD067 File Offset: 0x001FB267
			internal override TypeSymbol GetNullableUnderlyingTypeOrSelf(TypeSymbol typeSymbol)
			{
				return typeSymbol.StrippedType();
			}

			// Token: 0x06009139 RID: 37177 RVA: 0x001FD06F File Offset: 0x001FB26F
			internal override TypeWithAnnotations WithModifiers(TypeWithAnnotations type, ImmutableArray<CustomModifier> customModifiers)
			{
				return TypeWithAnnotations.CreateNonLazyType(type.DefaultType, type.NullableAnnotation, customModifiers);
			}

			// Token: 0x0600913A RID: 37178 RVA: 0x001FD083 File Offset: 0x001FB283
			internal override TypeSymbol AsTypeSymbolOnly(TypeSymbol typeSymbol)
			{
				return typeSymbol;
			}

			// Token: 0x0600913B RID: 37179 RVA: 0x001FD086 File Offset: 0x001FB286
			internal override TypeWithAnnotations WithTypeAndModifiers(TypeWithAnnotations type, TypeSymbol typeSymbol, ImmutableArray<CustomModifier> customModifiers)
			{
				return TypeWithAnnotations.CreateNonLazyType(typeSymbol, type.NullableAnnotation, customModifiers);
			}

			// Token: 0x0600913C RID: 37180 RVA: 0x001FD095 File Offset: 0x001FB295
			internal override TypeWithAnnotations AsNullableReferenceType(TypeWithAnnotations type)
			{
				return TypeWithAnnotations.CreateNonLazyType(type.DefaultType, NullableAnnotation.Annotated, this._customModifiers);
			}

			// Token: 0x0600913D RID: 37181 RVA: 0x001FD0A9 File Offset: 0x001FB2A9
			internal override TypeWithAnnotations AsNotNullableReferenceType(TypeWithAnnotations type)
			{
				TypeSymbol defaultType = type.DefaultType;
				return TypeWithAnnotations.CreateNonLazyType(defaultType, defaultType.IsNullableType() ? type.NullableAnnotation : NullableAnnotation.NotAnnotated, this._customModifiers);
			}

			// Token: 0x0600913E RID: 37182 RVA: 0x001FD0CD File Offset: 0x001FB2CD
			internal override bool TypeSymbolEquals(TypeWithAnnotations type, TypeWithAnnotations other, TypeCompareKind comparison)
			{
				return type.TypeSymbolEqualsCore(other, comparison);
			}

			// Token: 0x0600913F RID: 37183 RVA: 0x001FD0D8 File Offset: 0x001FB2D8
			internal override TypeWithAnnotations SubstituteType(TypeWithAnnotations type, AbstractTypeMap typeMap)
			{
				return type.SubstituteTypeCore(typeMap);
			}

			// Token: 0x06009140 RID: 37184 RVA: 0x001FD0E2 File Offset: 0x001FB2E2
			internal override void ReportDiagnosticsIfObsolete(TypeWithAnnotations type, Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
			{
				type.ReportDiagnosticsIfObsoleteCore(binder, syntax, diagnostics);
			}

			// Token: 0x06009141 RID: 37185 RVA: 0x001FD0EF File Offset: 0x001FB2EF
			internal override void TryForceResolve(bool asValueType)
			{
			}

			// Token: 0x04002876 RID: 10358
			private readonly ImmutableArray<CustomModifier> _customModifiers;
		}

		// Token: 0x02000964 RID: 2404
		private sealed class LazyNullableTypeParameter : TypeWithAnnotations.Extensions
		{
			// Token: 0x06009142 RID: 37186 RVA: 0x001FD0F1 File Offset: 0x001FB2F1
			public LazyNullableTypeParameter(CSharpCompilation compilation, TypeWithAnnotations underlying)
			{
				this._compilation = compilation;
				this._underlying = underlying;
			}

			// Token: 0x06009143 RID: 37187 RVA: 0x001FD107 File Offset: 0x001FB307
			internal override bool IsVoid(TypeSymbol typeSymbol)
			{
				return false;
			}

			// Token: 0x06009144 RID: 37188 RVA: 0x001FD10A File Offset: 0x001FB30A
			internal override bool IsSZArray(TypeSymbol typeSymbol)
			{
				return false;
			}

			// Token: 0x06009145 RID: 37189 RVA: 0x001FD10D File Offset: 0x001FB30D
			internal override bool IsStatic(TypeSymbol typeSymbol)
			{
				return false;
			}

			// Token: 0x06009146 RID: 37190 RVA: 0x001FD110 File Offset: 0x001FB310
			private TypeSymbol GetResolvedType()
			{
				if (this._resolved == null)
				{
					this.TryForceResolve(this._underlying.Type.IsValueType);
				}
				return this._resolved;
			}

			// Token: 0x06009147 RID: 37191 RVA: 0x001FD136 File Offset: 0x001FB336
			internal override TypeSymbol GetNullableUnderlyingTypeOrSelf(TypeSymbol typeSymbol)
			{
				return this._underlying.Type;
			}

			// Token: 0x06009148 RID: 37192 RVA: 0x001FD144 File Offset: 0x001FB344
			internal override SpecialType GetSpecialType(TypeSymbol typeSymbol)
			{
				SpecialType specialType = this._underlying.SpecialType;
				if (!specialType.IsValueType())
				{
					return specialType;
				}
				return SpecialType.None;
			}

			// Token: 0x06009149 RID: 37193 RVA: 0x001FD168 File Offset: 0x001FB368
			internal override bool IsRestrictedType(TypeSymbol typeSymbol, bool ignoreSpanLikeTypes)
			{
				return this._underlying.IsRestrictedType(ignoreSpanLikeTypes);
			}

			// Token: 0x0600914A RID: 37194 RVA: 0x001FD176 File Offset: 0x001FB376
			internal override TypeSymbol AsTypeSymbolOnly(TypeSymbol typeSymbol)
			{
				return this.GetResolvedType();
			}

			// Token: 0x170028E6 RID: 10470
			// (get) Token: 0x0600914B RID: 37195 RVA: 0x001FD17E File Offset: 0x001FB37E
			internal override bool IsResolved
			{
				get
				{
					return this._resolved != null;
				}
			}

			// Token: 0x0600914C RID: 37196 RVA: 0x001FD189 File Offset: 0x001FB389
			internal override TypeSymbol GetResolvedType(TypeSymbol defaultType)
			{
				return this.GetResolvedType();
			}

			// Token: 0x170028E7 RID: 10471
			// (get) Token: 0x0600914D RID: 37197 RVA: 0x001FD191 File Offset: 0x001FB391
			internal override ImmutableArray<CustomModifier> CustomModifiers
			{
				get
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
			}

			// Token: 0x0600914E RID: 37198 RVA: 0x001FD198 File Offset: 0x001FB398
			internal override TypeWithAnnotations WithModifiers(TypeWithAnnotations type, ImmutableArray<CustomModifier> customModifiers)
			{
				if (customModifiers.IsEmpty)
				{
					return type;
				}
				TypeSymbol resolvedType = this.GetResolvedType();
				if (resolvedType.IsNullableType())
				{
					return TypeWithAnnotations.Create(resolvedType, type.NullableAnnotation, customModifiers);
				}
				return TypeWithAnnotations.CreateNonLazyType(resolvedType, type.NullableAnnotation, customModifiers);
			}

			// Token: 0x0600914F RID: 37199 RVA: 0x001FD1DA File Offset: 0x001FB3DA
			internal override TypeWithAnnotations WithTypeAndModifiers(TypeWithAnnotations type, TypeSymbol typeSymbol, ImmutableArray<CustomModifier> customModifiers)
			{
				if (typeSymbol.IsNullableType())
				{
					return TypeWithAnnotations.Create(typeSymbol, type.NullableAnnotation, customModifiers);
				}
				return TypeWithAnnotations.CreateNonLazyType(typeSymbol, type.NullableAnnotation, customModifiers);
			}

			// Token: 0x06009150 RID: 37200 RVA: 0x001FD1FF File Offset: 0x001FB3FF
			internal override TypeWithAnnotations AsNullableReferenceType(TypeWithAnnotations type)
			{
				return type;
			}

			// Token: 0x06009151 RID: 37201 RVA: 0x001FD202 File Offset: 0x001FB402
			internal override TypeWithAnnotations AsNotNullableReferenceType(TypeWithAnnotations type)
			{
				if (!this._underlying.Type.IsValueType)
				{
					return this._underlying;
				}
				return type;
			}

			// Token: 0x06009152 RID: 37202 RVA: 0x001FD220 File Offset: 0x001FB420
			internal override TypeWithAnnotations SubstituteType(TypeWithAnnotations type, AbstractTypeMap typeMap)
			{
				if (this._resolved != null)
				{
					return type.SubstituteTypeCore(typeMap);
				}
				TypeWithAnnotations typeWithAnnotations = this._underlying.SubstituteTypeCore(typeMap);
				if (typeWithAnnotations.IsSameAs(this._underlying))
				{
					return type;
				}
				if (typeWithAnnotations.Type.Equals(this._underlying.Type, TypeCompareKind.ConsiderEverything) && typeWithAnnotations.CustomModifiers.IsEmpty)
				{
					return TypeWithAnnotations.CreateLazyNullableType(this._compilation, typeWithAnnotations);
				}
				return type.SubstituteTypeCore(typeMap);
			}

			// Token: 0x06009153 RID: 37203 RVA: 0x001FD29C File Offset: 0x001FB49C
			internal override void ReportDiagnosticsIfObsolete(TypeWithAnnotations type, Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
			{
				if (this._resolved != null)
				{
					type.ReportDiagnosticsIfObsoleteCore(binder, syntax, diagnostics);
					return;
				}
				diagnostics.Add(new LazyObsoleteDiagnosticInfo(type, binder.ContainingMemberOrLambda, binder.Flags), syntax.GetLocation());
			}

			// Token: 0x06009154 RID: 37204 RVA: 0x001FD2D8 File Offset: 0x001FB4D8
			internal override bool TypeSymbolEquals(TypeWithAnnotations type, TypeWithAnnotations other, TypeCompareKind comparison)
			{
				TypeWithAnnotations.LazyNullableTypeParameter lazyNullableTypeParameter = other._extensions as TypeWithAnnotations.LazyNullableTypeParameter;
				if (lazyNullableTypeParameter != null)
				{
					return this._underlying.TypeSymbolEquals(lazyNullableTypeParameter._underlying, comparison);
				}
				return type.TypeSymbolEqualsCore(other, comparison);
			}

			// Token: 0x06009155 RID: 37205 RVA: 0x001FD310 File Offset: 0x001FB510
			internal override void TryForceResolve(bool asValueType)
			{
				TypeSymbol typeSymbol = (asValueType ? this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(ImmutableArray.Create<TypeWithAnnotations>(this._underlying)) : this._underlying.Type);
				Interlocked.CompareExchange<TypeSymbol>(ref this._resolved, typeSymbol, null);
			}

			// Token: 0x04002877 RID: 10359
			private readonly CSharpCompilation _compilation;

			// Token: 0x04002878 RID: 10360
			private readonly TypeWithAnnotations _underlying;

			// Token: 0x04002879 RID: 10361
			private TypeSymbol _resolved;
		}
	}
}
