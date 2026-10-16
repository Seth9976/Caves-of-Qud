using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000559 RID: 1369
	internal abstract class SourceUserDefinedOperatorSymbolBase : SourceMemberMethodSymbol
	{
		// Token: 0x060073FA RID: 29690 RVA: 0x001B1474 File Offset: 0x001AF674
		protected SourceUserDefinedOperatorSymbolBase(MethodKind methodKind, string name, SourceMemberContainerTypeSymbol containingType, Location location, CSharpSyntaxNode syntax, DeclarationModifiers declarationModifiers, bool hasBody, bool isExpressionBodied, bool isIterator, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax.GetReference(), location, isIterator)
		{
			this._name = name;
			this._isExpressionBodied = isExpressionBodied;
			this.CheckUnsafeModifier(declarationModifiers, diagnostics);
			base.MakeFlags(methodKind, declarationModifiers, false, false, isNullableAnalysisEnabled, false);
			if (this.ContainingType.IsInterface && (methodKind == MethodKind.Conversion || name == "op_Equality" || name == "op_Inequality"))
			{
				return;
			}
			if (this.ContainingType.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorInStaticClass, location, new object[] { this });
				return;
			}
			if (this.DeclaredAccessibility != Accessibility.Public || !this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorsMustBeStatic, this.Locations[0], new object[] { this });
			}
			if (hasBody && this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_ExternHasBody, location, new object[] { this });
			}
			else if (!hasBody && !this.IsExtern && !this.IsAbstract && !base.IsPartial)
			{
				diagnostics.Add(ErrorCode.ERR_ConcreteMissingBody, location, new object[] { this });
			}
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.DeclarationModifiers, this, false);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(csdiagnosticInfo, location);
			}
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x001B15BC File Offset: 0x001AF7BC
		protected static DeclarationModifiers MakeDeclarationModifiers(BaseMethodDeclarationSyntax syntax, Location location, BindingDiagnosticBag diagnostics)
		{
			DeclarationModifiers declarationModifiers = DeclarationModifiers.Private;
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.Static | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Extern | DeclarationModifiers.Unsafe;
			bool flag;
			return ModifierUtils.MakeAndCheckNontypeMemberModifiers(syntax.Modifiers, declarationModifiers, declarationModifiers2, location, diagnostics, out flag);
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x060073FC RID: 29692
		protected abstract Location ReturnTypeLocation { get; }

		// Token: 0x060073FD RID: 29693 RVA: 0x001B15E8 File Offset: 0x001AF7E8
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters" })]
		protected ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindReturnType(BaseMethodDeclarationSyntax declarationSyntax, TypeSyntax returnTypeSyntax, BindingDiagnosticBag diagnostics)
		{
			Binder binder = this.DeclaringCompilation.GetBinderFactory(declarationSyntax.SyntaxTree, false).GetBinder(returnTypeSyntax, declarationSyntax, this).WithAdditionalFlags(BinderFlags.SuppressConstraintChecks);
			SyntaxToken syntaxToken;
			ImmutableArray<ParameterSymbol> immutableArray = ParameterHelpers.MakeParameters(binder, this, declarationSyntax.ParameterList, out syntaxToken, diagnostics, true, false, false);
			if (syntaxToken.Kind() == SyntaxKind.ArgListKeyword)
			{
				diagnostics.Add(ErrorCode.ERR_IllegalVarArgs, new SourceLocation(in syntaxToken));
			}
			TypeWithAnnotations typeWithAnnotations = binder.BindType(returnTypeSyntax, diagnostics, null, false);
			if (typeWithAnnotations.IsRestrictedType(true))
			{
				diagnostics.Add(ErrorCode.ERR_MethodReturnCantBeRefAny, returnTypeSyntax.Location, new object[] { typeWithAnnotations.Type });
			}
			if (typeWithAnnotations.Type.IsStatic)
			{
				diagnostics.Add(ErrorFacts.GetStaticClassReturnCode(false), returnTypeSyntax.Location, new object[] { typeWithAnnotations.Type });
			}
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>>(typeWithAnnotations, immutableArray);
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x001B16B8 File Offset: 0x001AF8B8
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> valueTuple = this.MakeParametersAndBindReturnType(diagnostics);
			this._lazyReturnType = valueTuple.Item1;
			this._lazyParameters = valueTuple.Item2;
			base.SetReturnsVoid(this._lazyReturnType.IsVoidType());
			if ((this.ContainingType.IsInterfaceType() && (this.MethodKind == MethodKind.Conversion || this.Name == "op_Equality" || this.Name == "op_Inequality")) || this.ContainingType.IsStatic)
			{
				return;
			}
			base.CheckEffectiveAccessibility(this._lazyReturnType, this._lazyParameters, diagnostics);
			this.CheckValueParameters(diagnostics);
			this.CheckOperatorSignatures(diagnostics);
		}

		// Token: 0x060073FF RID: 29695
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters" })]
		protected abstract ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics);

		// Token: 0x06007400 RID: 29696 RVA: 0x001B1760 File Offset: 0x001AF960
		private void CheckValueParameters(BindingDiagnosticBag diagnostics)
		{
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				if (parameterSymbol.RefKind != RefKind.None && parameterSymbol.RefKind != RefKind.In)
				{
					diagnostics.Add(ErrorCode.ERR_IllegalRefParam, this.Locations[0]);
					return;
				}
			}
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x001B17BC File Offset: 0x001AF9BC
		private void CheckOperatorSignatures(BindingDiagnosticBag diagnostics)
		{
			if (!SourceUserDefinedOperatorSymbolBase.DoesOperatorHaveCorrectArity(this.Name, this.ParameterCount))
			{
				return;
			}
			string name = this.Name;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1850069070U)
			{
				if (num <= 1034931220U)
				{
					if (num != 90588446U)
					{
						if (num != 215197780U)
						{
							if (num != 1034931220U)
							{
								goto IL_01BD;
							}
							if (!(name == "op_Increment"))
							{
								goto IL_01BD;
							}
							goto IL_01AD;
						}
						else if (!(name == "op_Implicit"))
						{
							goto IL_01BD;
						}
					}
					else
					{
						if (!(name == "op_OnesComplement"))
						{
							goto IL_01BD;
						}
						goto IL_019D;
					}
				}
				else if (num != 1548478473U)
				{
					if (num != 1587019679U)
					{
						if (num != 1850069070U)
						{
							goto IL_01BD;
						}
						if (!(name == "op_False"))
						{
							goto IL_01BD;
						}
						goto IL_01A5;
					}
					else if (!(name == "op_Explicit"))
					{
						goto IL_01BD;
					}
				}
				else
				{
					if (!(name == "op_RightShift"))
					{
						goto IL_01BD;
					}
					goto IL_01B5;
				}
				this.CheckUserDefinedConversionSignature(diagnostics);
				return;
			}
			if (num <= 2574677899U)
			{
				if (num != 2242295702U)
				{
					if (num != 2536726348U)
					{
						if (num != 2574677899U)
						{
							goto IL_01BD;
						}
						if (!(name == "op_LogicalNot"))
						{
							goto IL_01BD;
						}
					}
					else
					{
						if (!(name == "op_Decrement"))
						{
							goto IL_01BD;
						}
						goto IL_01AD;
					}
				}
				else
				{
					if (!(name == "op_LeftShift"))
					{
						goto IL_01BD;
					}
					goto IL_01B5;
				}
			}
			else if (num != 3075696130U)
			{
				if (num != 3568900899U)
				{
					if (num != 3716665893U)
					{
						goto IL_01BD;
					}
					if (!(name == "op_UnaryNegation"))
					{
						goto IL_01BD;
					}
				}
				else
				{
					if (!(name == "op_True"))
					{
						goto IL_01BD;
					}
					goto IL_01A5;
				}
			}
			else if (!(name == "op_UnaryPlus"))
			{
				goto IL_01BD;
			}
			IL_019D:
			this.CheckUnarySignature(diagnostics);
			return;
			IL_01A5:
			this.CheckTrueFalseSignature(diagnostics);
			return;
			IL_01AD:
			this.CheckIncrementDecrementSignature(diagnostics);
			return;
			IL_01B5:
			this.CheckShiftSignature(diagnostics);
			return;
			IL_01BD:
			this.CheckBinarySignature(diagnostics);
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x001B1990 File Offset: 0x001AFB90
		private static bool DoesOperatorHaveCorrectArity(string name, int parameterCount)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1850069070U)
			{
				if (num <= 215197780U)
				{
					if (num != 90588446U)
					{
						if (num != 215197780U)
						{
							goto IL_012E;
						}
						if (!(name == "op_Implicit"))
						{
							goto IL_012E;
						}
					}
					else if (!(name == "op_OnesComplement"))
					{
						goto IL_012E;
					}
				}
				else if (num != 1034931220U)
				{
					if (num != 1587019679U)
					{
						if (num != 1850069070U)
						{
							goto IL_012E;
						}
						if (!(name == "op_False"))
						{
							goto IL_012E;
						}
					}
					else if (!(name == "op_Explicit"))
					{
						goto IL_012E;
					}
				}
				else if (!(name == "op_Increment"))
				{
					goto IL_012E;
				}
			}
			else if (num <= 2574677899U)
			{
				if (num != 2536726348U)
				{
					if (num != 2574677899U)
					{
						goto IL_012E;
					}
					if (!(name == "op_LogicalNot"))
					{
						goto IL_012E;
					}
				}
				else if (!(name == "op_Decrement"))
				{
					goto IL_012E;
				}
			}
			else if (num != 3075696130U)
			{
				if (num != 3568900899U)
				{
					if (num != 3716665893U)
					{
						goto IL_012E;
					}
					if (!(name == "op_UnaryNegation"))
					{
						goto IL_012E;
					}
				}
				else if (!(name == "op_True"))
				{
					goto IL_012E;
				}
			}
			else if (!(name == "op_UnaryPlus"))
			{
				goto IL_012E;
			}
			return parameterCount == 1;
			IL_012E:
			return parameterCount == 2;
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x001B1AD0 File Offset: 0x001AFCD0
		private void CheckUserDefinedConversionSignature(BindingDiagnosticBag diagnostics)
		{
			if (this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorCantReturnVoid, this.Locations[0]);
			}
			TypeSymbol parameterType = base.GetParameterType(0);
			TypeSymbol returnType = base.ReturnType;
			TypeSymbol typeSymbol = parameterType.StrippedType();
			TypeSymbol typeSymbol2 = returnType.StrippedType();
			if (typeSymbol.IsInterfaceType() || typeSymbol2.IsInterfaceType())
			{
				diagnostics.Add(ErrorCode.ERR_ConversionWithInterface, this.Locations[0], new object[] { this });
				return;
			}
			if (!this.MatchesContainingType(typeSymbol) && !this.MatchesContainingType(typeSymbol2) && !this.MatchesContainingType(parameterType) && !this.MatchesContainingType(returnType))
			{
				diagnostics.Add(ErrorCode.ERR_ConversionNotInvolvingContainedType, this.Locations[0]);
				return;
			}
			if ((this.ContainingType.SpecialType == SpecialType.System_Nullable_T) ? parameterType.Equals(returnType, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes) : typeSymbol.Equals(typeSymbol2, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				diagnostics.Add(ErrorCode.ERR_IdentityConversion, this.Locations[0]);
				return;
			}
			if (parameterType.IsDynamic() || returnType.IsDynamic())
			{
				diagnostics.Add(ErrorCode.ERR_BadDynamicConversion, this.Locations[0], new object[] { this });
				return;
			}
			TypeSymbol typeSymbol3;
			TypeSymbol typeSymbol4;
			if (this.MatchesContainingType(typeSymbol))
			{
				typeSymbol3 = parameterType;
				typeSymbol4 = returnType;
			}
			else
			{
				typeSymbol3 = returnType;
				typeSymbol4 = parameterType;
			}
			if (typeSymbol4.IsClassType())
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
				if (typeSymbol3.IsDerivedFrom(typeSymbol4, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes, ref compoundUseSiteInfo))
				{
					diagnostics.Add(ErrorCode.ERR_ConversionWithBase, this.Locations[0], new object[] { this });
				}
				else if (typeSymbol4.IsDerivedFrom(typeSymbol3, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes, ref compoundUseSiteInfo))
				{
					diagnostics.Add(ErrorCode.ERR_ConversionWithDerived, this.Locations[0], new object[] { this });
				}
				diagnostics.Add(this.Locations[0], compoundUseSiteInfo);
			}
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x001B1CC8 File Offset: 0x001AFEC8
		private void CheckUnarySignature(BindingDiagnosticBag diagnostics)
		{
			if (!this.MatchesContainingType(base.GetParameterType(0).StrippedType()))
			{
				diagnostics.Add(ErrorCode.ERR_BadUnaryOperatorSignature, this.Locations[0]);
			}
			if (this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorCantReturnVoid, this.Locations[0]);
			}
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x001B1D28 File Offset: 0x001AFF28
		private void CheckTrueFalseSignature(BindingDiagnosticBag diagnostics)
		{
			if (base.ReturnType.SpecialType != SpecialType.System_Boolean)
			{
				diagnostics.Add(ErrorCode.ERR_OpTFRetType, this.Locations[0]);
			}
			if (!this.MatchesContainingType(base.GetParameterType(0).StrippedType()))
			{
				diagnostics.Add(ErrorCode.ERR_BadUnaryOperatorSignature, this.Locations[0]);
			}
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x001B1D90 File Offset: 0x001AFF90
		private void CheckIncrementDecrementSignature(BindingDiagnosticBag diagnostics)
		{
			TypeSymbol parameterType = base.GetParameterType(0);
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.MatchesContainingType(parameterType.StrippedType()))
			{
				diagnostics.Add(ErrorCode.ERR_BadIncDecSignature, this.Locations[0]);
			}
			else if (!base.ReturnType.EffectiveTypeNoUseSiteDiagnostics.IsEqualToOrDerivedFrom(parameterType, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes, ref compoundUseSiteInfo))
			{
				diagnostics.Add(ErrorCode.ERR_BadIncDecRetType, this.Locations[0]);
			}
			diagnostics.Add(this.Locations[0], compoundUseSiteInfo);
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x001B1E27 File Offset: 0x001B0027
		private bool MatchesContainingType(TypeSymbol type)
		{
			return type.Equals(this.ContainingType, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x001B1E38 File Offset: 0x001B0038
		private void CheckShiftSignature(BindingDiagnosticBag diagnostics)
		{
			if (!this.MatchesContainingType(base.GetParameterType(0).StrippedType()) || base.GetParameterType(1).StrippedType().SpecialType != SpecialType.System_Int32)
			{
				diagnostics.Add(ErrorCode.ERR_BadShiftOperatorSignature, this.Locations[0]);
			}
			if (this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorCantReturnVoid, this.Locations[0]);
			}
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x001B1EAC File Offset: 0x001B00AC
		private void CheckBinarySignature(BindingDiagnosticBag diagnostics)
		{
			if (!this.MatchesContainingType(base.GetParameterType(0).StrippedType()) && !this.MatchesContainingType(base.GetParameterType(1).StrippedType()))
			{
				diagnostics.Add(ErrorCode.ERR_BadBinaryOperatorSignature, this.Locations[0]);
			}
			if (this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_OperatorCantReturnVoid, this.Locations[0]);
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x0600740A RID: 29706 RVA: 0x001B1F1F File Offset: 0x001B011F
		public sealed override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x0600740B RID: 29707 RVA: 0x001B1F27 File Offset: 0x001B0127
		public sealed override bool ReturnsVoid
		{
			get
			{
				base.LazyMethodChecks();
				return base.ReturnsVoid;
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x0600740C RID: 29708 RVA: 0x001B1F35 File Offset: 0x001B0135
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x0600740D RID: 29709 RVA: 0x001B1F38 File Offset: 0x001B0138
		public sealed override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x0600740E RID: 29710 RVA: 0x001B1F3B File Offset: 0x001B013B
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x0600740F RID: 29711 RVA: 0x001B1F43 File Offset: 0x001B0143
		internal sealed override int ParameterCount
		{
			get
			{
				if (!this._lazyParameters.IsDefault)
				{
					return this._lazyParameters.Length;
				}
				return this.GetParameterCountFromSyntax();
			}
		}

		// Token: 0x06007410 RID: 29712
		protected abstract int GetParameterCountFromSyntax();

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06007411 RID: 29713 RVA: 0x001B1F64 File Offset: 0x001B0164
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x06007412 RID: 29714 RVA: 0x001B1F72 File Offset: 0x001B0172
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06007413 RID: 29715 RVA: 0x001B1F79 File Offset: 0x001B0179
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x001B1F80 File Offset: 0x001B0180
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06007415 RID: 29717 RVA: 0x001B1F87 File Offset: 0x001B0187
		public sealed override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x06007416 RID: 29718 RVA: 0x001B1F8A File Offset: 0x001B018A
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x06007417 RID: 29719 RVA: 0x001B1F98 File Offset: 0x001B0198
		internal sealed override bool IsExpressionBodied
		{
			get
			{
				return this._isExpressionBodied;
			}
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x001B1FA0 File Offset: 0x001B01A0
		internal sealed override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			base.ReturnType.CheckAllConstraints(declaringCompilation, conversions, this.Locations[0], diagnostics);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				parameterSymbol.Type.CheckAllConstraints(declaringCompilation, conversions, parameterSymbol.Locations[0], diagnostics);
			}
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (base.ReturnType.ContainsNativeInteger())
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, this.ReturnTypeLocation, true);
			}
			ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && this.ReturnTypeWithAnnotations.NeedsNullableAttribute())
			{
				declaringCompilation.EnsureNullableAttributeExists(diagnostics, this.ReturnTypeLocation, true);
			}
			ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, this.Parameters, diagnostics, true);
		}

		// Token: 0x04001C1F RID: 7199
		private const TypeCompareKind ComparisonForUserDefinedOperators = TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes;

		// Token: 0x04001C20 RID: 7200
		private readonly string _name;

		// Token: 0x04001C21 RID: 7201
		private readonly bool _isExpressionBodied;

		// Token: 0x04001C22 RID: 7202
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001C23 RID: 7203
		private TypeWithAnnotations _lazyReturnType;
	}
}
