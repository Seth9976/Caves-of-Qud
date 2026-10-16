using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000549 RID: 1353
	internal abstract class SourceOrdinaryMethodSymbolBase : SourceMemberMethodSymbol
	{
		// Token: 0x0600728D RID: 29325 RVA: 0x001AC434 File Offset: 0x001AA634
		protected SourceOrdinaryMethodSymbolBase(NamedTypeSymbol containingType, string name, Location location, CSharpSyntaxNode syntax, MethodKind methodKind, bool isIterator, bool isExtensionMethod, bool isPartial, bool hasBody, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax.GetReference(), location, isIterator)
		{
			this._name = name;
			ValueTuple<DeclarationModifiers, bool> valueTuple = this.MakeModifiers(methodKind, isPartial, hasBody, location, diagnostics);
			DeclarationModifiers item = valueTuple.Item1;
			this.HasExplicitAccessModifier = valueTuple.Item2;
			bool flag = methodKind == MethodKind.ExplicitInterfaceImplementation;
			base.MakeFlags(methodKind, item, false, isExtensionMethod, isNullableAnalysisEnabled, flag);
			this._typeParameters = this.MakeTypeParameters(syntax, diagnostics);
			base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, location, hasBody, diagnostics);
			if (hasBody)
			{
				base.CheckModifiersForBody(location, diagnostics);
			}
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.DeclarationModifiers, this, methodKind == MethodKind.ExplicitInterfaceImplementation);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(csdiagnosticInfo, location);
			}
		}

		// Token: 0x0600728E RID: 29326
		protected abstract ImmutableArray<TypeParameterSymbol> MakeTypeParameters(CSharpSyntaxNode node, BindingDiagnosticBag diagnostics);

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x0600728F RID: 29327 RVA: 0x001AC4D9 File Offset: 0x001AA6D9
		public override bool ReturnsVoid
		{
			get
			{
				base.LazyMethodChecks();
				return base.ReturnsVoid;
			}
		}

		// Token: 0x06007290 RID: 29328 RVA: 0x001AC4E8 File Offset: 0x001AA6E8
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> valueTuple = this.MakeParametersAndBindReturnType(diagnostics);
			this._lazyReturnType = valueTuple.Item1;
			this._lazyParameters = valueTuple.Item2;
			bool item = valueTuple.Item3;
			ImmutableArray<TypeParameterConstraintClause> item2 = valueTuple.Item4;
			base.SetReturnsVoid(this._lazyReturnType.IsVoidType());
			base.CheckEffectiveAccessibility(this._lazyReturnType, this._lazyParameters, diagnostics);
			Location location = this.locations[0];
			if (this.Name == "Finalize" && this.ParameterCount == 0 && this.Arity == 0 && this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.WRN_FinalizeMethod, location);
			}
			this.ExtensionMethodChecks(diagnostics);
			if (base.IsPartial)
			{
				if (this.MethodKind == MethodKind.ExplicitInterfaceImplementation)
				{
					diagnostics.Add(ErrorCode.ERR_PartialMethodNotExplicit, location);
				}
				if (!this.ContainingType.IsPartial())
				{
					diagnostics.Add(ErrorCode.ERR_PartialMethodOnlyInPartialClass, location);
				}
			}
			if (!base.IsPartial)
			{
				this.LazyAsyncMethodChecks(CancellationToken.None);
			}
			this._lazyRefCustomModifiers = ImmutableArray<CustomModifier>.Empty;
			MethodSymbol methodSymbol = null;
			if (this.MethodKind != MethodKind.ExplicitInterfaceImplementation)
			{
				this._lazyExplicitInterfaceImplementations = ImmutableArray<MethodSymbol>.Empty;
				if (this.IsOverride)
				{
					methodSymbol = base.OverriddenMethod;
					if (methodSymbol != null)
					{
						CustomModifierUtils.CopyMethodCustomModifiers(methodSymbol, this, out this._lazyReturnType, out this._lazyRefCustomModifiers, out this._lazyParameters, true);
					}
				}
				else if (this.RefKind == RefKind.In)
				{
					NamedTypeSymbol wellKnownType = Binder.GetWellKnownType(this.DeclaringCompilation, WellKnownType.System_Runtime_InteropServices_InAttribute, diagnostics, this.ReturnTypeLocation);
					this._lazyRefCustomModifiers = ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(wellKnownType));
				}
			}
			else if (this.ExplicitInterfaceType != null)
			{
				methodSymbol = this.FindExplicitlyImplementedMethod(diagnostics);
				if (methodSymbol != null)
				{
					this._lazyExplicitInterfaceImplementations = ImmutableArray.Create<MethodSymbol>(methodSymbol);
					CustomModifierUtils.CopyMethodCustomModifiers(methodSymbol, this, out this._lazyReturnType, out this._lazyRefCustomModifiers, out this._lazyParameters, false);
					this.FindExplicitlyImplementedMemberVerification(methodSymbol, diagnostics);
					TypeSymbol.CheckNullableReferenceTypeMismatchOnImplementingMember(this.ContainingType, this, methodSymbol, true, diagnostics);
				}
				else
				{
					this._lazyExplicitInterfaceImplementations = ImmutableArray<MethodSymbol>.Empty;
				}
			}
			if (!item2.IsDefault && methodSymbol != null)
			{
				int i = 0;
				while (i < item2.Length)
				{
					TypeParameterSymbol typeParameterSymbol = this._typeParameters[i];
					TypeParameterConstraintKind typeParameterConstraintKind = item2[i].Constraints & (TypeParameterConstraintKind.ReferenceType | TypeParameterConstraintKind.ValueType | TypeParameterConstraintKind.Default);
					ErrorCode errorCode;
					if (typeParameterConstraintKind != TypeParameterConstraintKind.ReferenceType)
					{
						if (typeParameterConstraintKind != TypeParameterConstraintKind.ValueType)
						{
							if (typeParameterConstraintKind == TypeParameterConstraintKind.Default)
							{
								if (typeParameterSymbol.IsReferenceType || typeParameterSymbol.IsValueType)
								{
									errorCode = ErrorCode.ERR_OverrideDefaultConstraintNotSatisfied;
									goto IL_026D;
								}
							}
						}
						else if (!typeParameterSymbol.IsNonNullableValueType())
						{
							errorCode = ErrorCode.ERR_OverrideValConstraintNotSatisfied;
							goto IL_026D;
						}
					}
					else if (!typeParameterSymbol.IsReferenceType)
					{
						errorCode = ErrorCode.ERR_OverrideRefConstraintNotSatisfied;
						goto IL_026D;
					}
					IL_02AE:
					i++;
					continue;
					IL_026D:
					diagnostics.Add(errorCode, typeParameterSymbol.Locations[0], new object[]
					{
						this,
						typeParameterSymbol,
						methodSymbol.TypeParameters[i],
						methodSymbol
					});
					goto IL_02AE;
				}
			}
			this.CheckModifiers(this.MethodKind == MethodKind.ExplicitInterfaceImplementation, item, this.HasAnyBody, location, diagnostics);
		}

		// Token: 0x06007291 RID: 29329
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		protected abstract ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics);

		// Token: 0x06007292 RID: 29330
		protected abstract void ExtensionMethodChecks(BindingDiagnosticBag diagnostics);

		// Token: 0x06007293 RID: 29331
		protected abstract MethodSymbol FindExplicitlyImplementedMethod(BindingDiagnosticBag diagnostics);

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06007294 RID: 29332
		protected abstract Location ReturnTypeLocation { get; }

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06007295 RID: 29333
		protected abstract TypeSymbol ExplicitInterfaceType { get; }

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06007296 RID: 29334
		protected abstract bool HasAnyBody { get; }

		// Token: 0x06007297 RID: 29335 RVA: 0x001AC7D0 File Offset: 0x001AA9D0
		protected sealed override void LazyAsyncMethodChecks(CancellationToken cancellationToken)
		{
			if (!this.IsAsync)
			{
				this.CompleteAsyncMethodChecks(null, cancellationToken);
				return;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			base.AsyncMethodChecks(instance);
			this.CompleteAsyncMethodChecks(instance, cancellationToken);
			instance.Free();
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x001AC80C File Offset: 0x001AAA0C
		private void CompleteAsyncMethodChecks(BindingDiagnosticBag diagnosticsOpt, CancellationToken cancellationToken)
		{
			if (this.state.NotePartComplete(CompletionPart.Members))
			{
				if (diagnosticsOpt != null)
				{
					this.AddDeclarationDiagnostics(diagnosticsOpt);
				}
				this.CompleteAsyncMethodChecksBetweenStartAndFinish();
				this.state.NotePartComplete(CompletionPart.TypeMembers);
				return;
			}
			this.state.SpinWaitComplete(CompletionPart.TypeMembers, cancellationToken);
		}

		// Token: 0x06007299 RID: 29337
		protected abstract void CompleteAsyncMethodChecksBetweenStartAndFinish();

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x0600729A RID: 29338 RVA: 0x001AC85E File Offset: 0x001AAA5E
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x0600729B RID: 29339 RVA: 0x001AC866 File Offset: 0x001AAA66
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x0600729C RID: 29340 RVA: 0x001AC86E File Offset: 0x001AAA6E
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

		// Token: 0x0600729D RID: 29341
		protected abstract int GetParameterCountFromSyntax();

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x0600729E RID: 29342 RVA: 0x001AC88F File Offset: 0x001AAA8F
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x0600729F RID: 29343 RVA: 0x001AC89D File Offset: 0x001AAA9D
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x060072A0 RID: 29344
		public abstract override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x060072A1 RID: 29345 RVA: 0x001AC8AB File Offset: 0x001AAAAB
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.MethodKind == MethodKind.ExplicitInterfaceImplementation;
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x060072A2 RID: 29346 RVA: 0x001AC8B6 File Offset: 0x001AAAB6
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x060072A3 RID: 29347 RVA: 0x001AC8C4 File Offset: 0x001AAAC4
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyRefCustomModifiers;
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x060072A4 RID: 29348 RVA: 0x001AC8D2 File Offset: 0x001AAAD2
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x060072A5 RID: 29349
		protected abstract override SourceMemberMethodSymbol BoundAttributesSource { get; }

		// Token: 0x060072A6 RID: 29350
		internal abstract override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations();

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x060072A7 RID: 29351 RVA: 0x001AC8DA File Offset: 0x001AAADA
		internal bool HasExplicitAccessModifier { get; }

		// Token: 0x060072A8 RID: 29352 RVA: 0x001AC8E4 File Offset: 0x001AAAE4
		[return: TupleElementNames(new string[] { "mods", "hasExplicitAccessMod" })]
		private ValueTuple<DeclarationModifiers, bool> MakeModifiers(MethodKind methodKind, bool isPartial, bool hasBody, Location location, BindingDiagnosticBag diagnostics)
		{
			bool isInterface = this.ContainingType.IsInterface;
			bool flag = methodKind == MethodKind.ExplicitInterfaceImplementation;
			DeclarationModifiers declarationModifiers = ((isInterface && isPartial && !flag) ? DeclarationModifiers.Public : DeclarationModifiers.Private);
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.Partial | DeclarationModifiers.Unsafe;
			DeclarationModifiers declarationModifiers3 = DeclarationModifiers.None;
			if (!flag)
			{
				declarationModifiers2 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.New | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Virtual;
				if (!isInterface)
				{
					declarationModifiers2 |= DeclarationModifiers.Override;
				}
				else
				{
					declarationModifiers = DeclarationModifiers.None;
					declarationModifiers3 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Extern | DeclarationModifiers.Partial | DeclarationModifiers.Virtual | DeclarationModifiers.Async;
				}
			}
			else if (isInterface)
			{
				declarationModifiers2 |= DeclarationModifiers.Abstract;
			}
			declarationModifiers2 |= DeclarationModifiers.Extern | DeclarationModifiers.Async;
			if (this.ContainingType.IsStructType())
			{
				declarationModifiers2 |= DeclarationModifiers.ReadOnly;
			}
			DeclarationModifiers declarationModifiers4 = this.MakeDeclarationModifiers(declarationModifiers2, diagnostics);
			bool flag2;
			if ((declarationModifiers4 & DeclarationModifiers.AccessibilityMask) == DeclarationModifiers.None)
			{
				flag2 = false;
				declarationModifiers4 |= declarationModifiers;
			}
			else
			{
				flag2 = true;
			}
			this.CheckUnsafeModifier(declarationModifiers4, diagnostics);
			ModifierUtils.ReportDefaultInterfaceImplementationModifiers(hasBody, declarationModifiers4, declarationModifiers3, location, diagnostics);
			declarationModifiers4 = SourceOrdinaryMethodSymbolBase.AddImpliedModifiers(declarationModifiers4, isInterface, methodKind, hasBody);
			return new ValueTuple<DeclarationModifiers, bool>(declarationModifiers4, flag2);
		}

		// Token: 0x060072A9 RID: 29353
		protected abstract DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics);

		// Token: 0x060072AA RID: 29354 RVA: 0x001AC9B7 File Offset: 0x001AABB7
		private static DeclarationModifiers AddImpliedModifiers(DeclarationModifiers mods, bool containingTypeIsInterface, MethodKind methodKind, bool hasBody)
		{
			if (containingTypeIsInterface)
			{
				mods = ModifierUtils.AdjustModifiersForAnInterfaceMember(mods, hasBody, methodKind == MethodKind.ExplicitInterfaceImplementation);
			}
			else if (methodKind == MethodKind.ExplicitInterfaceImplementation)
			{
				mods = (mods & ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected)) | DeclarationModifiers.Private;
			}
			return mods;
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x060072AB RID: 29355 RVA: 0x001AC9DF File Offset: 0x001AABDF
		internal bool HasExtendedPartialModifier
		{
			get
			{
				return (this.DeclarationModifiers & (DeclarationModifiers.Sealed | DeclarationModifiers.New | DeclarationModifiers.Extern | DeclarationModifiers.Virtual | DeclarationModifiers.Override)) > DeclarationModifiers.None;
			}
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x001AC9F0 File Offset: 0x001AABF0
		private void CheckModifiers(bool isExplicitInterfaceImplementation, bool isVararg, bool hasBody, Location location, BindingDiagnosticBag diagnostics)
		{
			bool flag = isExplicitInterfaceImplementation && this.ContainingType.IsInterface;
			if (base.IsPartial && this.HasExplicitAccessModifier)
			{
				Binder.CheckFeatureAvailability(this.SyntaxNode, MessageID.IDS_FeatureExtendedPartialMethods, diagnostics, location);
			}
			if (base.IsPartial && this.IsAbstract)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodInvalidModifier, location);
				return;
			}
			if (base.IsPartial && !this.HasExplicitAccessModifier && !this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodWithNonVoidReturnMustHaveAccessMods, location, new object[] { this });
				return;
			}
			if (base.IsPartial && !this.HasExplicitAccessModifier && this.HasExtendedPartialModifier)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodWithExtendedModMustHaveAccessMods, location, new object[] { this });
				return;
			}
			if (base.IsPartial && !this.HasExplicitAccessModifier)
			{
				if (this.Parameters.Any<ParameterSymbol>((ParameterSymbol p) => p.RefKind == RefKind.Out))
				{
					diagnostics.Add(ErrorCode.ERR_PartialMethodWithOutParamMustHaveAccessMods, location, new object[] { this });
					return;
				}
			}
			if (this.DeclaredAccessibility == Accessibility.Private && (this.IsVirtual || (this.IsAbstract && !flag) || this.IsOverride))
			{
				diagnostics.Add(ErrorCode.ERR_VirtualPrivate, location, new object[] { this });
				return;
			}
			if (this.IsStatic && (this.IsOverride || this.IsVirtual || this.IsAbstract))
			{
				diagnostics.Add(ErrorCode.ERR_StaticNotVirtual, location, new object[] { this });
				return;
			}
			if (this.IsOverride && (base.IsNew || this.IsVirtual))
			{
				diagnostics.Add(ErrorCode.ERR_OverrideNotNew, location, new object[] { this });
				return;
			}
			if (this.IsSealed && !this.IsOverride && (!flag || !this.IsAbstract))
			{
				diagnostics.Add(ErrorCode.ERR_SealedNonOverride, location, new object[] { this });
				return;
			}
			if (this.IsSealed && this.ContainingType.TypeKind == TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_BadMemberFlag, location, new object[] { SyntaxFacts.GetText(SyntaxKind.SealedKeyword) });
				return;
			}
			if (this._lazyReturnType.IsStatic)
			{
				diagnostics.Add(ErrorFacts.GetStaticClassReturnCode(this.ContainingType.IsInterfaceType()), location, new object[] { this._lazyReturnType.Type });
				return;
			}
			if (this.IsAbstract && this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractAndExtern, location, new object[] { this });
				return;
			}
			if (this.IsAbstract && this.IsSealed && !flag)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractAndSealed, location, new object[] { this });
				return;
			}
			if (this.IsAbstract && this.IsVirtual)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractNotVirtual, location, new object[]
				{
					this.Kind.Localize(),
					this
				});
				return;
			}
			if (this.IsAbstract && this.ContainingType.TypeKind == TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_BadMemberFlag, location, new object[] { SyntaxFacts.GetText(SyntaxKind.AbstractKeyword) });
				return;
			}
			if (this.IsVirtual && this.ContainingType.TypeKind == TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_BadMemberFlag, location, new object[] { SyntaxFacts.GetText(SyntaxKind.VirtualKeyword) });
				return;
			}
			if (this.IsStatic && this.IsDeclaredReadOnly)
			{
				diagnostics.Add(ErrorCode.ERR_StaticMemberCantBeReadOnly, location, new object[] { this });
				return;
			}
			if (this.IsAbstract && !this.ContainingType.IsAbstract && (this.ContainingType.TypeKind == TypeKind.Class || this.ContainingType.TypeKind == TypeKind.Submission))
			{
				diagnostics.Add(ErrorCode.ERR_AbstractInConcreteClass, location, new object[] { this, this.ContainingType });
				return;
			}
			if (this.IsVirtual && this.ContainingType.IsSealed)
			{
				diagnostics.Add(ErrorCode.ERR_NewVirtualInSealed, location, new object[] { this, this.ContainingType });
				return;
			}
			if (!hasBody && this.IsAsync)
			{
				diagnostics.Add(ErrorCode.ERR_BadAsyncLacksBody, location);
				return;
			}
			if (!hasBody && !this.IsExtern && !this.IsAbstract && !base.IsPartial && !this.IsExpressionBodied)
			{
				diagnostics.Add(ErrorCode.ERR_ConcreteMissingBody, location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsSealed && this.DeclaredAccessibility.HasProtected() && !this.IsOverride)
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsStatic && !this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_InstanceMemberInStaticClass, location, new object[] { this.Name });
				return;
			}
			if (isVararg && (this.IsGenericMethod || this.ContainingType.IsGenericType || (this._lazyParameters.Length > 0 && this._lazyParameters[this._lazyParameters.Length - 1].IsParams)))
			{
				diagnostics.Add(ErrorCode.ERR_BadVarargs, location);
				return;
			}
			if (isVararg && this.IsAsync)
			{
				diagnostics.Add(ErrorCode.ERR_VarargsAsync, location);
			}
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x001ACF50 File Offset: 0x001AB150
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.IsExtensionMethod)
			{
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_ExtensionAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x001ACF98 File Offset: 0x001AB198
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
			Location returnTypeLocation = this.ReturnTypeLocation;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			this.CheckConstraintsForExplicitInterfaceType(conversions, diagnostics);
			base.ReturnType.CheckAllConstraints(declaringCompilation, conversions, this.Locations[0], diagnostics);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				parameterSymbol.Type.CheckAllConstraints(declaringCompilation, conversions, parameterSymbol.Locations[0], diagnostics);
			}
			this.PartialMethodChecks(diagnostics);
			if (this.RefKind == RefKind.In)
			{
				declaringCompilation.EnsureIsReadOnlyAttributeExists(diagnostics, returnTypeLocation, true);
			}
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (base.ReturnType.ContainsNativeInteger())
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, returnTypeLocation, true);
			}
			ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && this.ReturnTypeWithAnnotations.NeedsNullableAttribute())
			{
				declaringCompilation.EnsureNullableAttributeExists(diagnostics, returnTypeLocation, true);
			}
			ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, this.Parameters, diagnostics, true);
		}

		// Token: 0x060072AF RID: 29359
		protected abstract void CheckConstraintsForExplicitInterfaceType(ConversionsBase conversions, BindingDiagnosticBag diagnostics);

		// Token: 0x060072B0 RID: 29360
		protected abstract void PartialMethodChecks(BindingDiagnosticBag diagnostics);

		// Token: 0x04001BE2 RID: 7138
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001BE3 RID: 7139
		private readonly string _name;

		// Token: 0x04001BE4 RID: 7140
		private ImmutableArray<MethodSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001BE5 RID: 7141
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001BE6 RID: 7142
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001BE7 RID: 7143
		private TypeWithAnnotations _lazyReturnType;

		// Token: 0x04001BE9 RID: 7145
		private const DeclarationModifiers PartialMethodExtendedModifierMask = DeclarationModifiers.Sealed | DeclarationModifiers.New | DeclarationModifiers.Extern | DeclarationModifiers.Virtual | DeclarationModifiers.Override;
	}
}
