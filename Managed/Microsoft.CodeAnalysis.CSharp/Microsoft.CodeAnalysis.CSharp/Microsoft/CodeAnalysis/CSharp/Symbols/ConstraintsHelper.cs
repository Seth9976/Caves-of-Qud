using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D5 RID: 1237
	internal static class ConstraintsHelper
	{
		// Token: 0x060068B7 RID: 26807 RVA: 0x00188BC8 File Offset: 0x00186DC8
		public static TypeParameterBounds ResolveBounds(this TypeParameterSymbol typeParameter, AssemblySymbol corLibrary, ConsList<TypeParameterSymbol> inProgress, ImmutableArray<TypeWithAnnotations> constraintTypes, bool inherited, CSharpCompilation currentCompilation, BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			TypeParameterBounds typeParameterBounds = typeParameter.ResolveBounds(corLibrary, inProgress, constraintTypes, inherited, currentCompilation, instance, ref arrayBuilder, new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, currentCompilation.Assembly));
			if (arrayBuilder != null)
			{
				instance.AddRange(arrayBuilder);
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance)
			{
				diagnostics.Add(typeParameterDiagnosticInfo.UseSiteInfo, typeParameterDiagnosticInfo.TypeParameter.Locations[0]);
			}
			instance.Free();
			return typeParameterBounds;
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x00188C50 File Offset: 0x00186E50
		public static TypeParameterBounds ResolveBounds(this TypeParameterSymbol typeParameter, AssemblySymbol corLibrary, ConsList<TypeParameterSymbol> inProgress, ImmutableArray<TypeWithAnnotations> constraintTypes, bool inherited, CSharpCompilation currentCompilation, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder, CompoundUseSiteInfo<AssemblySymbol> template)
		{
			NamedTypeSymbol namedTypeSymbol = corLibrary.GetSpecialType(typeParameter.HasValueTypeConstraint ? SpecialType.System_ValueType : SpecialType.System_Object);
			TypeSymbol typeSymbol = namedTypeSymbol;
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (constraintTypes.Length == 0)
			{
				immutableArray = ImmutableArray<NamedTypeSymbol>.Empty;
			}
			else
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
				ArrayBuilder<NamedTypeSymbol> instance2 = ArrayBuilder<NamedTypeSymbol>.GetInstance();
				TypeConversions typeConversions = new TypeConversions(corLibrary, false);
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(template);
				foreach (TypeWithAnnotations typeWithAnnotations in constraintTypes)
				{
					NamedTypeSymbol namedTypeSymbol2;
					TypeSymbol typeSymbol2;
					switch (typeWithAnnotations.TypeKind)
					{
					case TypeKind.Array:
						namedTypeSymbol2 = corLibrary.GetSpecialType(SpecialType.System_Array);
						typeSymbol2 = typeWithAnnotations.Type;
						break;
					case TypeKind.Class:
					case TypeKind.Delegate:
					case TypeKind.Interface:
						if (typeWithAnnotations.Type.IsInterfaceType())
						{
							ConstraintsHelper.AddInterface(instance2, (NamedTypeSymbol)typeWithAnnotations.Type);
							instance.Add(typeWithAnnotations);
							continue;
						}
						namedTypeSymbol2 = (NamedTypeSymbol)typeWithAnnotations.Type;
						typeSymbol2 = typeWithAnnotations.Type;
						break;
					case TypeKind.Dynamic:
					case TypeKind.Module:
					case TypeKind.Submission:
						goto IL_02C3;
					case TypeKind.Enum:
						namedTypeSymbol2 = corLibrary.GetSpecialType(SpecialType.System_Enum);
						typeSymbol2 = typeWithAnnotations.Type;
						break;
					case TypeKind.Error:
						namedTypeSymbol2 = (NamedTypeSymbol)typeWithAnnotations.Type;
						typeSymbol2 = typeWithAnnotations.Type;
						break;
					case TypeKind.Pointer:
					case TypeKind.FunctionPointer:
						continue;
					case TypeKind.Struct:
						if (typeWithAnnotations.IsNullableType())
						{
							TypeSymbol nullableUnderlyingType = typeWithAnnotations.Type.GetNullableUnderlyingType();
							if (nullableUnderlyingType.TypeKind == TypeKind.TypeParameter)
							{
								TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)nullableUnderlyingType;
								if (typeParameterSymbol.ContainingSymbol == typeParameter.ContainingSymbol && inProgress.ContainsReference(typeParameterSymbol))
								{
									diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameterSymbol, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_CircularConstraint, new object[] { typeParameterSymbol, typeParameter }))));
									continue;
								}
							}
						}
						namedTypeSymbol2 = corLibrary.GetSpecialType(SpecialType.System_ValueType);
						typeSymbol2 = typeWithAnnotations.Type;
						break;
					case TypeKind.TypeParameter:
					{
						TypeParameterSymbol typeParameterSymbol2 = (TypeParameterSymbol)typeWithAnnotations.Type;
						ConsList<TypeParameterSymbol> consList;
						if (typeParameterSymbol2.ContainingSymbol == typeParameter.ContainingSymbol)
						{
							if (inProgress.ContainsReference(typeParameterSymbol2))
							{
								diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameterSymbol2, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_CircularConstraint, new object[] { typeParameterSymbol2, typeParameter }))));
								continue;
							}
							consList = inProgress;
						}
						else
						{
							consList = ConsList<TypeParameterSymbol>.Empty;
						}
						namedTypeSymbol2 = typeParameterSymbol2.GetEffectiveBaseClass(consList);
						typeSymbol2 = typeParameterSymbol2.GetDeducedBaseType(consList);
						ConstraintsHelper.AddInterfaces(instance2, typeParameterSymbol2.GetInterfaces(consList));
						if (!inherited && currentCompilation != null && typeParameterSymbol2.IsFromCompilation(currentCompilation))
						{
							ErrorCode errorCode;
							if (typeParameterSymbol2.HasUnmanagedTypeConstraint)
							{
								errorCode = ErrorCode.ERR_ConWithUnmanagedCon;
							}
							else
							{
								if (!typeParameterSymbol2.HasValueTypeConstraint)
								{
									break;
								}
								errorCode = ErrorCode.ERR_ConWithValCon;
							}
							diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(errorCode, new object[] { typeParameter, typeParameterSymbol2 }))));
							continue;
						}
						break;
					}
					default:
						goto IL_02C3;
					}
					instance.Add(typeWithAnnotations);
					if (typeSymbol.IsErrorType() || typeSymbol2.IsErrorType() || ConstraintsHelper.IsEncompassedBy(typeConversions, typeSymbol, typeSymbol2, ref compoundUseSiteInfo))
					{
						continue;
					}
					if (!ConstraintsHelper.IsEncompassedBy(typeConversions, typeSymbol2, typeSymbol, ref compoundUseSiteInfo))
					{
						diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BaseConstraintConflict, new object[] { typeParameter, typeSymbol2, typeSymbol }))));
						continue;
					}
					typeSymbol = typeSymbol2;
					namedTypeSymbol = namedTypeSymbol2;
					continue;
					IL_02C3:
					throw ExceptionUtilities.UnexpectedValue(typeWithAnnotations.TypeKind);
				}
				ConstraintsHelper.AppendUseSiteDiagnostics(compoundUseSiteInfo, typeParameter, ref useSiteDiagnosticsBuilder);
				constraintTypes = instance.ToImmutableAndFree();
				immutableArray = instance2.ToImmutableAndFree();
			}
			if (constraintTypes.Length == 0 && typeSymbol.SpecialType == SpecialType.System_Object)
			{
				return null;
			}
			TypeParameterBounds typeParameterBounds = new TypeParameterBounds(constraintTypes, immutableArray, namedTypeSymbol, typeSymbol);
			if (inherited)
			{
				ConstraintsHelper.CheckOverrideConstraints(typeParameter, typeParameterBounds, diagnosticsBuilder);
			}
			return typeParameterBounds;
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x00188FF4 File Offset: 0x001871F4
		internal static ImmutableArray<ImmutableArray<TypeWithAnnotations>> MakeTypeParameterConstraintTypes(this MethodSymbol containingSymbol, Binder binder, ImmutableArray<TypeParameterSymbol> typeParameters, TypeParameterListSyntax typeParameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BindingDiagnosticBag diagnostics)
		{
			if (typeParameters.Length == 0 || constraintClauses.Count == 0)
			{
				return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
			}
			binder = binder.WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.GenericConstraintsClause);
			ImmutableArray<TypeParameterConstraintClause> immutableArray = binder.BindTypeParameterConstraintClauses(containingSymbol, typeParameters, typeParameterList, constraintClauses, diagnostics, false, false);
			if (immutableArray.All<TypeParameterConstraintClause>((TypeParameterConstraintClause clause) => clause.ConstraintTypes.IsEmpty))
			{
				return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
			}
			return immutableArray.SelectAsArray<TypeParameterConstraintClause, ImmutableArray<TypeWithAnnotations>>((TypeParameterConstraintClause clause) => clause.ConstraintTypes);
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00189088 File Offset: 0x00187288
		internal static ImmutableArray<TypeParameterConstraintKind> MakeTypeParameterConstraintKinds(this MethodSymbol containingSymbol, Binder binder, ImmutableArray<TypeParameterSymbol> typeParameters, TypeParameterListSyntax typeParameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			if (typeParameters.Length == 0)
			{
				return ImmutableArray<TypeParameterConstraintKind>.Empty;
			}
			ImmutableArray<TypeParameterConstraintClause> immutableArray;
			if (constraintClauses.Count == 0)
			{
				immutableArray = binder.GetDefaultTypeParameterConstraintClauses(typeParameterList);
			}
			else
			{
				binder = binder.WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.GenericConstraintsClause | BinderFlags.SuppressTypeArgumentBinding);
				immutableArray = binder.BindTypeParameterConstraintClauses(containingSymbol, typeParameters, typeParameterList, constraintClauses, BindingDiagnosticBag.Discarded, true, false);
				immutableArray = ConstraintsHelper.AdjustConstraintKindsBasedOnConstraintTypes(containingSymbol, typeParameters, immutableArray);
			}
			if (immutableArray.All<TypeParameterConstraintClause>((TypeParameterConstraintClause clause) => clause.Constraints == TypeParameterConstraintKind.None))
			{
				return ImmutableArray<TypeParameterConstraintKind>.Empty;
			}
			return immutableArray.SelectAsArray<TypeParameterConstraintClause, TypeParameterConstraintKind>((TypeParameterConstraintClause clause) => clause.Constraints);
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00189134 File Offset: 0x00187334
		internal static ImmutableArray<TypeParameterConstraintClause> AdjustConstraintKindsBasedOnConstraintTypes(Symbol container, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeParameterConstraintClause> constraintClauses)
		{
			int length = typeParameters.Length;
			SmallDictionary<TypeParameterSymbol, bool> smallDictionary = TypeParameterConstraintClause.BuildIsValueTypeMap(container, typeParameters, constraintClauses);
			SmallDictionary<TypeParameterSymbol, bool> smallDictionary2 = TypeParameterConstraintClause.BuildIsReferenceTypeFromConstraintTypesMap(container, typeParameters, constraintClauses);
			ArrayBuilder<TypeParameterConstraintClause> arrayBuilder = null;
			for (int i = 0; i < length; i++)
			{
				TypeParameterConstraintClause typeParameterConstraintClause = constraintClauses[i];
				TypeParameterSymbol typeParameterSymbol = typeParameters[i];
				TypeParameterConstraintKind typeParameterConstraintKind = typeParameterConstraintClause.Constraints;
				if ((typeParameterConstraintKind & TypeParameterConstraintKind.AllValueTypeKinds) == TypeParameterConstraintKind.None && smallDictionary[typeParameterSymbol])
				{
					typeParameterConstraintKind |= TypeParameterConstraintKind.ValueTypeFromConstraintTypes;
				}
				if (smallDictionary2[typeParameterSymbol])
				{
					typeParameterConstraintKind |= TypeParameterConstraintKind.ReferenceTypeFromConstraintTypes;
				}
				if (typeParameterConstraintClause.Constraints != typeParameterConstraintKind)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<TypeParameterConstraintClause>.GetInstance(constraintClauses.Length);
						arrayBuilder.AddRange(constraintClauses);
					}
					arrayBuilder[i] = TypeParameterConstraintClause.Create(typeParameterConstraintKind, typeParameterConstraintClause.ConstraintTypes);
				}
			}
			if (arrayBuilder != null)
			{
				constraintClauses = arrayBuilder.ToImmutableAndFree();
			}
			return constraintClauses;
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x00189208 File Offset: 0x00187408
		private static void CheckOverrideConstraints(TypeParameterSymbol typeParameter, TypeParameterBounds bounds, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder)
		{
			TypeSymbol deducedBaseType = bounds.DeducedBaseType;
			ImmutableArray<TypeWithAnnotations> constraintTypes = bounds.ConstraintTypes;
			if (ConstraintsHelper.IsValueType(typeParameter, constraintTypes) && ConstraintsHelper.IsReferenceType(typeParameter, constraintTypes))
			{
				diagnosticsBuilder.Add(ConstraintsHelper.GenerateConflictingConstraintsError(typeParameter, deducedBaseType, deducedBaseType.IsValueType));
				return;
			}
			if (deducedBaseType.IsNullableType() && (typeParameter.HasValueTypeConstraint || typeParameter.HasReferenceTypeConstraint))
			{
				diagnosticsBuilder.Add(ConstraintsHelper.GenerateConflictingConstraintsError(typeParameter, deducedBaseType, typeParameter.HasReferenceTypeConstraint));
			}
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x00189274 File Offset: 0x00187474
		public static void CheckAllConstraints(this TypeSymbol type, CSharpCompilation compilation, ConversionsBase conversions, Location location, BindingDiagnosticBag diagnostics)
		{
			bool flag = compilation.IsFeatureEnabled(MessageID.IDS_FeatureNullableReferenceTypes);
			type.CheckAllConstraints(new ConstraintsHelper.CheckConstraintsArgsBoxed(compilation, conversions, flag, location, diagnostics));
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x001892A0 File Offset: 0x001874A0
		public static bool CheckAllConstraints(this TypeSymbol type, CSharpCompilation compilation, ConversionsBase conversions)
		{
			BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(DiagnosticBag.GetInstance());
			type.CheckAllConstraints(new ConstraintsHelper.CheckConstraintsArgsBoxed(compilation, conversions, false, NoLocation.Singleton, bindingDiagnosticBag));
			bool flag = !bindingDiagnosticBag.HasAnyErrors();
			bindingDiagnosticBag.Free();
			return flag;
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x001892DB File Offset: 0x001874DB
		[MethodImpl(256)]
		public static void CheckAllConstraints(this TypeSymbol type, ConstraintsHelper.CheckConstraintsArgsBoxed args)
		{
			type.VisitType(ConstraintsHelper.s_checkConstraintsSingleTypeFunc, args, false);
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x001892EC File Offset: 0x001874EC
		private static bool CheckConstraintsSingleType(TypeSymbol type, in ConstraintsHelper.CheckConstraintsArgs args)
		{
			if (type.Kind == SymbolKind.NamedType)
			{
				((NamedTypeSymbol)type).CheckConstraints(in args);
			}
			else if (type.Kind == SymbolKind.PointerType)
			{
				Binder.CheckManagedAddr(args.CurrentCompilation, ((PointerTypeSymbol)type).PointedAtType, args.Location, args.Diagnostics);
			}
			return false;
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x00189340 File Offset: 0x00187540
		public static void CheckConstraints(this NamedTypeSymbol tuple, in ConstraintsHelper.CheckConstraintsArgs args, SyntaxNode typeSyntax, ImmutableArray<Location> elementLocations, BindingDiagnosticBag nullabilityDiagnosticsOpt)
		{
			ConstraintsHelper.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.typeSyntax = typeSyntax;
			CS$<>8__locals1.elementLocations = elementLocations;
			if (!ConstraintsHelper.RequiresChecking(tuple))
			{
				return;
			}
			if (CS$<>8__locals1.typeSyntax.HasErrors)
			{
				return;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> instance2 = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<NamedTypeSymbol> instance3 = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			NamedTypeSymbol.GetUnderlyingTypeChain(tuple, instance3);
			CS$<>8__locals1.offset = 0;
			foreach (NamedTypeSymbol namedTypeSymbol in instance3)
			{
				ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
				ConstraintsHelper.CheckTypeConstraints(namedTypeSymbol, in args, instance, (nullabilityDiagnosticsOpt == null) ? null : instance2, ref arrayBuilder);
				if (arrayBuilder != null)
				{
					instance.AddRange(arrayBuilder);
				}
				ConstraintsHelper.<CheckConstraints>g__populateDiagnosticsAndClear|13_0(instance, args.Diagnostics, ref CS$<>8__locals1);
				ConstraintsHelper.<CheckConstraints>g__populateDiagnosticsAndClear|13_0(instance2, nullabilityDiagnosticsOpt, ref CS$<>8__locals1);
				CS$<>8__locals1.offset += 7;
			}
			instance3.Free();
			instance.Free();
			instance2.Free();
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0018940C File Offset: 0x0018760C
		public static bool CheckConstraintsForNamedType(this NamedTypeSymbol type, in ConstraintsHelper.CheckConstraintsArgs args, SyntaxNode typeSyntax, SeparatedSyntaxList<TypeSyntax> typeArgumentsSyntax, ConsList<TypeSymbol> basesBeingResolved)
		{
			if (!ConstraintsHelper.RequiresChecking(type))
			{
				return true;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			bool flag = !typeSyntax.HasErrors && ConstraintsHelper.CheckTypeConstraints(type, in args, instance, args.IncludeNullability ? instance : null, ref arrayBuilder);
			if (arrayBuilder != null)
			{
				instance.AddRange(arrayBuilder);
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance)
			{
				int ordinal = typeParameterDiagnosticInfo.TypeParameter.Ordinal;
				Location location = ((ordinal < typeArgumentsSyntax.Count) ? typeArgumentsSyntax[ordinal].Location : args.Location);
				args.Diagnostics.Add(typeParameterDiagnosticInfo.UseSiteInfo, location);
			}
			instance.Free();
			if (ConstraintsHelper.HasDuplicateInterfaces(type, basesBeingResolved))
			{
				flag = false;
				args.Diagnostics.Add(ErrorCode.ERR_BogusType, args.Location, new object[] { type });
			}
			return flag;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x001894EC File Offset: 0x001876EC
		public static bool CheckConstraints(this NamedTypeSymbol type, in ConstraintsHelper.CheckConstraintsArgs args)
		{
			if (!ConstraintsHelper.RequiresChecking(type))
			{
				return true;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			bool flag = ConstraintsHelper.CheckTypeConstraints(type, in args, instance, args.IncludeNullability ? instance : null, ref arrayBuilder);
			if (arrayBuilder != null)
			{
				instance.AddRange(arrayBuilder);
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance)
			{
				args.Diagnostics.Add(typeParameterDiagnosticInfo.UseSiteInfo, args.Location);
			}
			instance.Free();
			if ((args.CurrentCompilation == null || !type.IsFromCompilation(args.CurrentCompilation)) && ConstraintsHelper.HasDuplicateInterfaces(type, null))
			{
				flag = false;
				args.Diagnostics.Add(ErrorCode.ERR_BogusType, args.Location, new object[] { type });
			}
			return flag;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x001895AC File Offset: 0x001877AC
		private static bool HasDuplicateInterfaces(NamedTypeSymbol type, ConsList<TypeSymbol> basesBeingResolved)
		{
			ImmutableArray<NamedTypeSymbol> immutableArray = type.OriginalDefinition.InterfacesNoUseSiteDiagnostics(basesBeingResolved);
			int length = immutableArray.Length;
			if (length > 1)
			{
				if (length != 2)
				{
					PooledHashSet<object> instance = PooledHashSet<object>.GetInstance();
					foreach (NamedTypeSymbol namedTypeSymbol in immutableArray)
					{
						if (!instance.Add(namedTypeSymbol.OriginalDefinition))
						{
							instance.Free();
							goto IL_0080;
						}
					}
					instance.Free();
					return false;
				}
				if (immutableArray[0].OriginalDefinition != immutableArray[1].OriginalDefinition)
				{
					return false;
				}
				IL_0080:
				return type.InterfacesNoUseSiteDiagnostics(basesBeingResolved).HasDuplicates<NamedTypeSymbol>(SymbolEqualityComparer.IgnoringDynamicTupleNamesAndNullability);
			}
			return false;
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x0018964C File Offset: 0x0018784C
		public static bool CheckConstraints(this MethodSymbol method, in ConstraintsHelper.CheckConstraintsArgs args)
		{
			if (!ConstraintsHelper.RequiresChecking(method))
			{
				return true;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			bool flag = ConstraintsHelper.CheckMethodConstraints(method, in args, instance, null, ref arrayBuilder, default(BitVector));
			if (arrayBuilder != null)
			{
				instance.AddRange(arrayBuilder);
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance)
			{
				args.Diagnostics.Add(typeParameterDiagnosticInfo.UseSiteInfo, args.Location);
			}
			instance.Free();
			return flag;
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x001896C8 File Offset: 0x001878C8
		[MethodImpl(256)]
		private static bool CheckTypeConstraints(NamedTypeSymbol type, in ConstraintsHelper.CheckConstraintsArgs args, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder)
		{
			return type.CheckConstraints(in args, type.TypeSubstitution, type.OriginalDefinition.TypeParameters, type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics, diagnosticsBuilder, nullabilityDiagnosticsBuilderOpt, ref useSiteDiagnosticsBuilder, default(BitVector), null);
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x00189704 File Offset: 0x00187904
		public static bool CheckMethodConstraints(MethodSymbol method, in ConstraintsHelper.CheckConstraintsArgs args, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder, BitVector skipParameters = default(BitVector))
		{
			return method.CheckConstraints(in args, method.TypeSubstitution, method.OriginalDefinition.TypeParameters, method.TypeArgumentsWithAnnotations, diagnosticsBuilder, nullabilityDiagnosticsBuilderOpt, ref useSiteDiagnosticsBuilder, skipParameters, null);
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x00189738 File Offset: 0x00187938
		public static bool CheckConstraints(this Symbol containingSymbol, in ConstraintsHelper.CheckConstraintsArgs args, TypeMap substitution, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeWithAnnotations> typeArguments, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder, BitVector skipParameters = default(BitVector), HashSet<TypeParameterSymbol> ignoreTypeConstraintsDependentOnTypeParametersOpt = null)
		{
			int length = typeParameters.Length;
			bool flag = true;
			for (int i = 0; i < length; i++)
			{
				if (!skipParameters[i] && !ConstraintsHelper.CheckConstraints(containingSymbol, in args, substitution, typeParameters[i], typeArguments[i], diagnosticsBuilder, nullabilityDiagnosticsBuilderOpt, ref useSiteDiagnosticsBuilder, ignoreTypeConstraintsDependentOnTypeParametersOpt))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x0018978C File Offset: 0x0018798C
		[MethodImpl(8)]
		private static bool CheckBasicConstraints(Symbol containingSymbol, in ConstraintsHelper.CheckConstraintsArgs args, TypeParameterSymbol typeParameter, TypeWithAnnotations typeArgument, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder)
		{
			if (typeArgument.Type.IsPointerOrFunctionPointer() || typeArgument.IsRestrictedType(false) || typeArgument.IsVoidType())
			{
				diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BadTypeArgument, new object[] { typeArgument.Type }))));
				return false;
			}
			if (typeArgument.IsStatic)
			{
				diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_GenericArgIsStaticClass, new object[] { typeArgument.Type }))));
				return false;
			}
			if (typeParameter.HasReferenceTypeConstraint && !typeArgument.Type.IsReferenceType)
			{
				diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_RefConstraintNotSatisfied, new object[]
				{
					containingSymbol.ConstructedFrom(),
					typeParameter,
					typeArgument.Type
				}))));
				return false;
			}
			ConstraintsHelper.CheckNullability(containingSymbol, typeParameter, typeArgument, nullabilityDiagnosticsBuilderOpt);
			if (typeParameter.HasUnmanagedTypeConstraint)
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(args.Template);
				ManagedKind managedKind = typeArgument.Type.GetManagedKind(ref compoundUseSiteInfo);
				ConstraintsHelper.AppendUseSiteDiagnostics(compoundUseSiteInfo, typeParameter, ref useSiteDiagnosticsBuilder);
				if (managedKind == ManagedKind.Managed || !typeArgument.Type.IsNonNullableValueType())
				{
					diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_UnmanagedConstraintNotSatisfied, new object[]
					{
						containingSymbol.ConstructedFrom(),
						typeParameter,
						typeArgument.Type
					}))));
					return false;
				}
				if (managedKind == ManagedKind.UnmanagedWithGenerics && args.CurrentCompilation != null)
				{
					CSDiagnosticInfo featureAvailabilityDiagnosticInfo = MessageID.IDS_FeatureUnmanagedConstructedTypes.GetFeatureAvailabilityDiagnosticInfo(args.CurrentCompilation);
					if (featureAvailabilityDiagnosticInfo != null)
					{
						diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(featureAvailabilityDiagnosticInfo)));
						return false;
					}
				}
			}
			if (typeParameter.HasValueTypeConstraint && !typeArgument.Type.IsNonNullableValueType())
			{
				diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_ValConstraintNotSatisfied, new object[]
				{
					containingSymbol.ConstructedFrom(),
					typeParameter,
					typeArgument.Type
				}))));
				return false;
			}
			return true;
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x00189980 File Offset: 0x00187B80
		private static bool CheckConstraints(Symbol containingSymbol, in ConstraintsHelper.CheckConstraintsArgs args, TypeMap substitution, TypeParameterSymbol typeParameter, TypeWithAnnotations typeArgument, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder, HashSet<TypeParameterSymbol> ignoreTypeConstraintsDependentOnTypeParametersOpt)
		{
			if (typeArgument.Type.IsErrorType())
			{
				return true;
			}
			if (!ConstraintsHelper.CheckBasicConstraints(containingSymbol, in args, typeParameter, typeArgument, diagnosticsBuilder, nullabilityDiagnosticsBuilderOpt, ref useSiteDiagnosticsBuilder))
			{
				return false;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(args.Template);
			ImmutableArray<TypeWithAnnotations> immutableArray = typeParameter.ConstraintTypesWithDefinitionUseSiteDiagnostics(ref compoundUseSiteInfo);
			substitution.SubstituteConstraintTypesDistinctWithoutModifiers(typeParameter, immutableArray, instance, ignoreTypeConstraintsDependentOnTypeParametersOpt);
			bool flag = false;
			foreach (TypeWithAnnotations typeWithAnnotations in instance)
			{
				ConstraintsHelper.CheckConstraintType(containingSymbol, in args, typeParameter, typeArgument, diagnosticsBuilder, nullabilityDiagnosticsBuilderOpt, ref compoundUseSiteInfo, typeWithAnnotations, ref flag);
			}
			instance.Free();
			if (ConstraintsHelper.AppendUseSiteDiagnostics(compoundUseSiteInfo, typeParameter, ref useSiteDiagnosticsBuilder))
			{
				flag = true;
			}
			if (typeParameter.HasConstructorConstraint && !ConstraintsHelper.SatisfiesConstructorConstraint(typeArgument.Type))
			{
				diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_NewConstraintNotSatisfied, new object[]
				{
					containingSymbol.ConstructedFrom(),
					typeParameter,
					typeArgument.Type
				}))));
				return false;
			}
			return !flag;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x00189A74 File Offset: 0x00187C74
		[MethodImpl(8)]
		private static void CheckNullability(Symbol containingSymbol, TypeParameterSymbol typeParameter, TypeWithAnnotations typeArgument, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt)
		{
			if (nullabilityDiagnosticsBuilderOpt != null)
			{
				if (typeParameter.HasNotNullConstraint && typeArgument.GetValueNullableAnnotation().IsAnnotated() && !typeArgument.Type.IsNonNullableValueType())
				{
					nullabilityDiagnosticsBuilderOpt.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.WRN_NullabilityMismatchInTypeParameterNotNullConstraint, new object[]
					{
						containingSymbol.ConstructedFrom(),
						typeParameter,
						typeArgument
					}))));
				}
				if (typeParameter.HasReferenceTypeConstraint)
				{
					bool? referenceTypeConstraintIsNullable = typeParameter.ReferenceTypeConstraintIsNullable;
					bool flag = false;
					if (((referenceTypeConstraintIsNullable.GetValueOrDefault() == flag) & (referenceTypeConstraintIsNullable != null)) && typeArgument.GetValueNullableAnnotation().IsAnnotated())
					{
						nullabilityDiagnosticsBuilderOpt.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint, new object[]
						{
							containingSymbol.ConstructedFrom(),
							typeParameter,
							typeArgument
						}))));
					}
				}
			}
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x00189B4C File Offset: 0x00187D4C
		[MethodImpl(8)]
		private static void CheckConstraintType(Symbol containingSymbol, in ConstraintsHelper.CheckConstraintsArgs args, TypeParameterSymbol typeParameter, TypeWithAnnotations typeArgument, ArrayBuilder<TypeParameterDiagnosticInfo> diagnosticsBuilder, ArrayBuilder<TypeParameterDiagnosticInfo> nullabilityDiagnosticsBuilderOpt, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, TypeWithAnnotations constraintType, ref bool hasError)
		{
			if (ConstraintsHelper.SatisfiesConstraintType(args.Conversions.WithNullability(false), typeArgument, constraintType, ref useSiteInfo))
			{
				if (nullabilityDiagnosticsBuilderOpt != null && (!ConstraintsHelper.SatisfiesConstraintType(args.Conversions.WithNullability(true), typeArgument, constraintType, ref useSiteInfo) || !ConstraintsHelper.<CheckConstraintType>g__constraintTypeAllows|24_1(in constraintType, ConstraintsHelper.<CheckConstraintType>g__getTypeArgumentState|24_0(in typeArgument))))
				{
					nullabilityDiagnosticsBuilderOpt.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.WRN_NullabilityMismatchInTypeParameterConstraint, new object[]
					{
						containingSymbol.ConstructedFrom(),
						constraintType,
						typeParameter,
						typeArgument
					}))));
				}
				return;
			}
			ErrorCode errorCode;
			if (typeArgument.Type.IsReferenceType)
			{
				errorCode = ErrorCode.ERR_GenericConstraintNotSatisfiedRefType;
			}
			else if (typeArgument.IsNullableType())
			{
				errorCode = (constraintType.Type.IsInterfaceType() ? ErrorCode.ERR_GenericConstraintNotSatisfiedNullableInterface : ErrorCode.ERR_GenericConstraintNotSatisfiedNullableEnum);
			}
			else if (typeArgument.TypeKind == TypeKind.TypeParameter)
			{
				errorCode = ErrorCode.ERR_GenericConstraintNotSatisfiedTyVar;
			}
			else
			{
				errorCode = ErrorCode.ERR_GenericConstraintNotSatisfiedValType;
			}
			SymbolDistinguisher symbolDistinguisher = new SymbolDistinguisher(args.CurrentCompilation, constraintType.Type, typeArgument.Type);
			diagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(errorCode, new object[]
			{
				containingSymbol.ConstructedFrom(),
				symbolDistinguisher.First,
				typeParameter,
				symbolDistinguisher.Second
			}))));
			hasError = true;
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x00189C90 File Offset: 0x00187E90
		private static bool AppendUseSiteDiagnostics(CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, TypeParameterSymbol typeParameter, ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder)
		{
			if ((!useSiteInfo.AccumulatesDiagnostics || !useSiteInfo.HasErrors) && useSiteInfo.AccumulatesDependencies && !useSiteInfo.Dependencies.IsNullOrEmpty<AssemblySymbol>())
			{
				ConstraintsHelper.<AppendUseSiteDiagnostics>g__ensureUseSiteDiagnosticsBuilder|25_0(ref useSiteDiagnosticsBuilder).Add(new TypeParameterDiagnosticInfo(typeParameter, (useSiteInfo.Dependencies.Count == 1) ? new UseSiteInfo<AssemblySymbol>(Enumerable.Single<AssemblySymbol>(useSiteInfo.Dependencies)) : new UseSiteInfo<AssemblySymbol>(useSiteInfo.Dependencies.ToImmutableHashSet<AssemblySymbol>())));
			}
			if (!useSiteInfo.AccumulatesDiagnostics)
			{
				return false;
			}
			IReadOnlyCollection<DiagnosticInfo> diagnostics = useSiteInfo.Diagnostics;
			if (diagnostics.IsNullOrEmpty<DiagnosticInfo>())
			{
				return false;
			}
			ConstraintsHelper.<AppendUseSiteDiagnostics>g__ensureUseSiteDiagnosticsBuilder|25_0(ref useSiteDiagnosticsBuilder);
			bool flag = false;
			foreach (DiagnosticInfo diagnosticInfo in diagnostics)
			{
				if (diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					flag = true;
				}
				useSiteDiagnosticsBuilder.Add(new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(diagnosticInfo)));
			}
			return flag;
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x00189D84 File Offset: 0x00187F84
		private static bool SatisfiesConstraintType(ConversionsBase conversions, TypeWithAnnotations typeArgument, TypeWithAnnotations constraintType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (constraintType.Type.IsErrorType())
			{
				return false;
			}
			if (conversions.HasIdentityOrImplicitReferenceConversion(typeArgument.Type, constraintType.Type, ref useSiteInfo))
			{
				return true;
			}
			if (typeArgument.Type.IsValueType && conversions.HasBoxingConversion(typeArgument.Type.IsNullableType() ? ((NamedTypeSymbol)typeArgument.Type).ConstructedFrom : typeArgument.Type, constraintType.Type, ref useSiteInfo))
			{
				return true;
			}
			if (typeArgument.TypeKind == TypeKind.TypeParameter)
			{
				TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)typeArgument.Type;
				if (conversions.HasImplicitTypeParameterConversion(typeParameterSymbol, constraintType.Type, ref useSiteInfo))
				{
					return true;
				}
				foreach (TypeWithAnnotations typeWithAnnotations in typeParameterSymbol.ConstraintTypesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
				{
					if (ConstraintsHelper.SatisfiesConstraintType(conversions, typeWithAnnotations, constraintType, ref useSiteInfo))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x00189E5E File Offset: 0x0018805E
		private static bool IsReferenceType(TypeParameterSymbol typeParameter, ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			return typeParameter.HasReferenceTypeConstraint || TypeParameterSymbol.CalculateIsReferenceTypeFromConstraintTypes(constraintTypes);
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x00189E70 File Offset: 0x00188070
		private static bool IsValueType(TypeParameterSymbol typeParameter, ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			return typeParameter.HasValueTypeConstraint || TypeParameterSymbol.CalculateIsValueTypeFromConstraintTypes(constraintTypes);
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x00189E82 File Offset: 0x00188082
		private static TypeParameterDiagnosticInfo GenerateConflictingConstraintsError(TypeParameterSymbol typeParameter, TypeSymbol deducedBase, bool classConflict)
		{
			return new TypeParameterDiagnosticInfo(typeParameter, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BaseConstraintConflict, new object[]
			{
				typeParameter,
				deducedBase,
				classConflict ? "class" : "struct"
			})));
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x00189EBC File Offset: 0x001880BC
		private static void AddInterfaces(ArrayBuilder<NamedTypeSymbol> builder, ImmutableArray<NamedTypeSymbol> interfaces)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in interfaces)
			{
				ConstraintsHelper.AddInterface(builder, namedTypeSymbol);
			}
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x00189EEB File Offset: 0x001880EB
		private static void AddInterface(ArrayBuilder<NamedTypeSymbol> builder, NamedTypeSymbol @interface)
		{
			if (!builder.Contains(@interface))
			{
				builder.Add(@interface);
			}
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x00189F00 File Offset: 0x00188100
		[MethodImpl(8)]
		private static bool SatisfiesConstructorConstraint(TypeSymbol typeArgument)
		{
			switch (typeArgument.TypeKind)
			{
			case TypeKind.Class:
				return ConstraintsHelper.HasPublicParameterlessConstructor((NamedTypeSymbol)typeArgument) && !typeArgument.IsAbstract;
			case TypeKind.Dynamic:
			case TypeKind.Enum:
			case TypeKind.Struct:
				return true;
			case TypeKind.TypeParameter:
			{
				TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)typeArgument;
				return typeParameterSymbol.HasConstructorConstraint || typeParameterSymbol.IsValueType;
			}
			case TypeKind.Submission:
				throw ExceptionUtilities.UnexpectedValue(typeArgument.TypeKind);
			}
			return false;
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x00189F90 File Offset: 0x00188190
		private static bool HasPublicParameterlessConstructor(NamedTypeSymbol type)
		{
			foreach (MethodSymbol methodSymbol in type.InstanceConstructors)
			{
				if (methodSymbol.ParameterCount == 0)
				{
					return methodSymbol.DeclaredAccessibility == Accessibility.Public;
				}
			}
			return false;
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x00189FD2 File Offset: 0x001881D2
		private static bool IsEncompassedBy(ConversionsBase conversions, TypeSymbol a, TypeSymbol b, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return conversions.HasIdentityOrImplicitReferenceConversion(a, b, ref useSiteInfo) || conversions.HasBoxingConversion(a, b, ref useSiteInfo);
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x00189FEC File Offset: 0x001881EC
		private static bool IsValidEncompassedByArgument(TypeSymbol type)
		{
			TypeKind typeKind = type.TypeKind;
			return typeKind - TypeKind.Array <= 2 || typeKind == TypeKind.Enum || typeKind == TypeKind.Struct;
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x0018A012 File Offset: 0x00188212
		public static bool RequiresChecking(NamedTypeSymbol type)
		{
			return type.Arity != 0 && type.OriginalDefinition != type;
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x0018A02A File Offset: 0x0018822A
		public static bool RequiresChecking(MethodSymbol method)
		{
			return method.IsGenericMethod && method.OriginalDefinition != method;
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x0018A042 File Offset: 0x00188242
		[Conditional("DEBUG")]
		private static void CheckEffectiveAndDeducedBaseTypes(ConversionsBase conversions, TypeSymbol effectiveBase, TypeSymbol deducedBase)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x0018A04C File Offset: 0x0018824C
		internal static TypeWithAnnotations ConstraintWithMostSignificantNullability(TypeWithAnnotations type1, TypeWithAnnotations type2)
		{
			switch (type2.NullableAnnotation)
			{
			case NullableAnnotation.NotAnnotated:
				return type2;
			case NullableAnnotation.Oblivious:
				if (type1.NullableAnnotation.IsNotAnnotated())
				{
					return type1;
				}
				return type2;
			case NullableAnnotation.Annotated:
				return type1;
			default:
				throw ExceptionUtilities.UnexpectedValue(type2.NullableAnnotation);
			}
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0018A099 File Offset: 0x00188299
		internal static bool IsObjectConstraint(TypeWithAnnotations type, ref TypeWithAnnotations bestObjectConstraint)
		{
			if (type.SpecialType == SpecialType.System_Object)
			{
				if (type.NullableAnnotation != NullableAnnotation.Annotated)
				{
					if (!bestObjectConstraint.HasType)
					{
						bestObjectConstraint = type;
					}
					else
					{
						bestObjectConstraint = ConstraintsHelper.ConstraintWithMostSignificantNullability(bestObjectConstraint, type);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x0018A0D4 File Offset: 0x001882D4
		internal static bool IsObjectConstraintSignificant(bool? isNotNullable, TypeWithAnnotations objectConstraint)
		{
			if (isNotNullable != null)
			{
				if (isNotNullable.GetValueOrDefault())
				{
					return false;
				}
			}
			else if (objectConstraint.NullableAnnotation.IsOblivious())
			{
				return false;
			}
			return true;
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x0018A114 File Offset: 0x00188314
		[CompilerGenerated]
		internal static void <CheckConstraints>g__populateDiagnosticsAndClear|13_0(ArrayBuilder<TypeParameterDiagnosticInfo> builder, BindingDiagnosticBag bag, ref ConstraintsHelper.<>c__DisplayClass13_0 A_2)
		{
			if (bag == null)
			{
				builder.Clear();
				return;
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in builder)
			{
				int ordinal = typeParameterDiagnosticInfo.TypeParameter.Ordinal;
				Location location = ((ordinal == 7) ? A_2.typeSyntax.Location : A_2.elementLocations[ordinal + A_2.offset]);
				bag.Add(typeParameterDiagnosticInfo.UseSiteInfo, location);
			}
			builder.Clear();
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x0018A18C File Offset: 0x0018838C
		[CompilerGenerated]
		internal static NullableFlowState <CheckConstraintType>g__getTypeArgumentState|24_0(in TypeWithAnnotations typeWithAnnotations)
		{
			TypeSymbol type = typeWithAnnotations.Type;
			if (type == null)
			{
				return NullableFlowState.NotNull;
			}
			if (type.IsValueType)
			{
				if (!type.IsNullableTypeOrTypeParameter())
				{
					return NullableFlowState.NotNull;
				}
				return NullableFlowState.MaybeNull;
			}
			else
			{
				NullableAnnotation nullableAnnotation = typeWithAnnotations.NullableAnnotation;
				if (nullableAnnotation == NullableAnnotation.Oblivious)
				{
					return NullableFlowState.NotNull;
				}
				if (nullableAnnotation != NullableAnnotation.Annotated)
				{
					TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
					if (typeParameterSymbol != null)
					{
						bool? isNotNullable = typeParameterSymbol.IsNotNullable;
						bool flag = true;
						if (!((isNotNullable.GetValueOrDefault() == flag) & (isNotNullable != null)))
						{
							NullableFlowState? nullableFlowState = default(NullableFlowState?);
							foreach (TypeWithAnnotations typeWithAnnotations2 in typeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics)
							{
								NullableFlowState nullableFlowState2 = ConstraintsHelper.<CheckConstraintType>g__getTypeArgumentState|24_0(in typeWithAnnotations2);
								if (nullableFlowState == null)
								{
									nullableFlowState = new NullableFlowState?(nullableFlowState2);
								}
								else
								{
									nullableFlowState = new NullableFlowState?(nullableFlowState.Value.Meet(nullableFlowState2));
								}
							}
							NullableFlowState? nullableFlowState3 = nullableFlowState;
							if (nullableFlowState3 == null)
							{
								return NullableFlowState.MaybeNull;
							}
							return nullableFlowState3.GetValueOrDefault();
						}
					}
					return NullableFlowState.NotNull;
				}
				if (!type.IsTypeParameterDisallowingAnnotationInCSharp8())
				{
					return NullableFlowState.MaybeNull;
				}
				return NullableFlowState.MaybeDefault;
			}
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0018A278 File Offset: 0x00188478
		[CompilerGenerated]
		internal static bool <CheckConstraintType>g__constraintTypeAllows|24_1(in TypeWithAnnotations typeWithAnnotations, NullableFlowState state)
		{
			if (state == NullableFlowState.NotNull)
			{
				return true;
			}
			TypeSymbol type = typeWithAnnotations.Type;
			if (type == null || type.IsValueType)
			{
				return true;
			}
			NullableAnnotation nullableAnnotation = typeWithAnnotations.NullableAnnotation;
			if (nullableAnnotation - NullableAnnotation.Oblivious <= 1)
			{
				return true;
			}
			TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
			if (typeParameterSymbol != null)
			{
				bool? isNotNullable = typeParameterSymbol.IsNotNullable;
				bool flag = true;
				if (!((isNotNullable.GetValueOrDefault() == flag) & (isNotNullable != null)))
				{
					foreach (TypeWithAnnotations typeWithAnnotations2 in typeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics)
					{
						if (!ConstraintsHelper.<CheckConstraintType>g__constraintTypeAllows|24_1(in typeWithAnnotations2, state))
						{
							return false;
						}
					}
					return state == NullableFlowState.MaybeNull;
				}
			}
			return false;
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x0018A310 File Offset: 0x00188510
		[CompilerGenerated]
		internal static ArrayBuilder<TypeParameterDiagnosticInfo> <AppendUseSiteDiagnostics>g__ensureUseSiteDiagnosticsBuilder|25_0(ref ArrayBuilder<TypeParameterDiagnosticInfo> useSiteDiagnosticsBuilder)
		{
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder;
			if ((arrayBuilder = useSiteDiagnosticsBuilder) == null)
			{
				ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder2;
				useSiteDiagnosticsBuilder = (arrayBuilder2 = new ArrayBuilder<TypeParameterDiagnosticInfo>());
				arrayBuilder = arrayBuilder2;
			}
			return arrayBuilder;
		}

		// Token: 0x04001A38 RID: 6712
		private static readonly Func<TypeSymbol, ConstraintsHelper.CheckConstraintsArgsBoxed, bool, bool> s_checkConstraintsSingleTypeFunc = (TypeSymbol type, ConstraintsHelper.CheckConstraintsArgsBoxed arg, bool unused) => ConstraintsHelper.CheckConstraintsSingleType(type, in arg.Args);

		// Token: 0x020008F1 RID: 2289
		internal readonly struct CheckConstraintsArgs
		{
			// Token: 0x06008F18 RID: 36632 RVA: 0x001F889E File Offset: 0x001F6A9E
			public CheckConstraintsArgs(CSharpCompilation currentCompilation, ConversionsBase conversions, Location location, BindingDiagnosticBag diagnostics)
			{
				this = new ConstraintsHelper.CheckConstraintsArgs(currentCompilation, conversions, currentCompilation.IsFeatureEnabled(MessageID.IDS_FeatureNullableReferenceTypes), location, diagnostics);
			}

			// Token: 0x06008F19 RID: 36633 RVA: 0x001F88B6 File Offset: 0x001F6AB6
			public CheckConstraintsArgs(CSharpCompilation currentCompilation, ConversionsBase conversions, bool includeNullability, Location location, BindingDiagnosticBag diagnostics)
			{
				this = new ConstraintsHelper.CheckConstraintsArgs(currentCompilation, conversions, includeNullability, location, diagnostics, new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, currentCompilation.Assembly));
			}

			// Token: 0x06008F1A RID: 36634 RVA: 0x001F88D2 File Offset: 0x001F6AD2
			public CheckConstraintsArgs(CSharpCompilation currentCompilation, ConversionsBase conversions, bool includeNullability, Location location, BindingDiagnosticBag diagnostics, CompoundUseSiteInfo<AssemblySymbol> template)
			{
				this.CurrentCompilation = currentCompilation;
				this.Conversions = conversions;
				this.IncludeNullability = includeNullability;
				this.Location = location;
				this.Diagnostics = diagnostics;
				this.Template = template;
			}

			// Token: 0x0400270A RID: 9994
			public readonly CSharpCompilation CurrentCompilation;

			// Token: 0x0400270B RID: 9995
			public readonly ConversionsBase Conversions;

			// Token: 0x0400270C RID: 9996
			public readonly bool IncludeNullability;

			// Token: 0x0400270D RID: 9997
			public readonly Location Location;

			// Token: 0x0400270E RID: 9998
			public readonly BindingDiagnosticBag Diagnostics;

			// Token: 0x0400270F RID: 9999
			public readonly CompoundUseSiteInfo<AssemblySymbol> Template;
		}

		// Token: 0x020008F2 RID: 2290
		internal sealed class CheckConstraintsArgsBoxed
		{
			// Token: 0x06008F1B RID: 36635 RVA: 0x001F8901 File Offset: 0x001F6B01
			[MethodImpl(8)]
			public CheckConstraintsArgsBoxed(CSharpCompilation currentCompilation, ConversionsBase conversions, Location location, BindingDiagnosticBag diagnostics)
			{
				this.Args = new ConstraintsHelper.CheckConstraintsArgs(currentCompilation, conversions, location, diagnostics);
			}

			// Token: 0x06008F1C RID: 36636 RVA: 0x001F8919 File Offset: 0x001F6B19
			[MethodImpl(8)]
			public CheckConstraintsArgsBoxed(CSharpCompilation currentCompilation, ConversionsBase conversions, bool includeNullability, Location location, BindingDiagnosticBag diagnostics)
			{
				this.Args = new ConstraintsHelper.CheckConstraintsArgs(currentCompilation, conversions, includeNullability, location, diagnostics);
			}

			// Token: 0x04002710 RID: 10000
			public readonly ConstraintsHelper.CheckConstraintsArgs Args;
		}
	}
}
