using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200054E RID: 1358
	internal abstract class SourcePropertySymbolBase : PropertySymbol, IAttributeTargetSymbol
	{
		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x06007319 RID: 29465 RVA: 0x001AEB52 File Offset: 0x001ACD52
		public Location Location { get; }

		// Token: 0x0600731A RID: 29466 RVA: 0x001AEB5C File Offset: 0x001ACD5C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected SourcePropertySymbolBase(SourceMemberContainerTypeSymbol containingType, CSharpSyntaxNode syntax, bool hasGetAccessor, bool hasSetAccessor, bool isExplicitInterfaceImplementation, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol explicitInterfaceType, [global::System.Runtime.CompilerServices.Nullable(2)] string aliasQualifierOpt, DeclarationModifiers modifiers, bool hasInitializer, bool isAutoProperty, bool isExpressionBodied, bool isInitOnly, RefKind refKind, string memberName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> indexerNameAttributeLists, Location location, BindingDiagnosticBag diagnostics)
		{
			this._syntaxRef = syntax.GetReference();
			this.Location = location;
			this._containingType = containingType;
			this._refKind = refKind;
			this._modifiers = modifiers;
			this._explicitInterfaceType = explicitInterfaceType;
			if (isExplicitInterfaceImplementation)
			{
				this._propertyFlags |= SourcePropertySymbolBase.Flags.IsExplicitInterfaceImplementation;
			}
			else
			{
				this._lazyExplicitInterfaceImplementations = ImmutableArray<PropertySymbol>.Empty;
			}
			bool isIndexer = this.IsIndexer;
			isAutoProperty = isAutoProperty && (!containingType.IsInterface || this.IsStatic) && !this.IsAbstract && !this.IsExtern && !isIndexer;
			if (isAutoProperty)
			{
				this._propertyFlags |= SourcePropertySymbolBase.Flags.IsAutoProperty;
			}
			if (hasInitializer)
			{
				this._propertyFlags |= SourcePropertySymbolBase.Flags.HasInitializer;
			}
			if (isExpressionBodied)
			{
				this._propertyFlags |= SourcePropertySymbolBase.Flags.IsExpressionBodied;
			}
			if (isIndexer)
			{
				if (indexerNameAttributeLists.Count == 0 || isExplicitInterfaceImplementation)
				{
					this._lazySourceName = memberName;
				}
				this._name = ExplicitInterfaceHelpers.GetMemberName("this[]", this._explicitInterfaceType, aliasQualifierOpt);
			}
			else
			{
				this._lazySourceName = memberName;
				this._name = memberName;
			}
			if ((isAutoProperty && hasGetAccessor) || hasInitializer)
			{
				string text = GeneratedNames.MakeBackingFieldName(this._name);
				this.BackingField = new SynthesizedBackingFieldSymbol(this, text, (hasGetAccessor && !hasSetAccessor) || isInitOnly, this.IsStatic, hasInitializer);
			}
			if (hasGetAccessor)
			{
				this._getMethod = this.CreateGetAccessorSymbol(isAutoProperty, diagnostics);
			}
			if (hasSetAccessor)
			{
				this._setMethod = this.CreateSetAccessorSymbol(isAutoProperty, diagnostics);
			}
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x001AECD0 File Offset: 0x001ACED0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private void EnsureSignatureGuarded(BindingDiagnosticBag diagnostics)
		{
			PropertySymbol propertySymbol = null;
			this._lazyRefCustomModifiers = ImmutableArray<CustomModifier>.Empty;
			ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> valueTuple = this.MakeParametersAndBindType(diagnostics);
			TypeWithAnnotations typeWithAnnotations = valueTuple.Item1;
			this._lazyParameters = valueTuple.Item2;
			this._lazyType = new TypeWithAnnotations.Boxed(typeWithAnnotations);
			bool isExplicitInterfaceImplementation = this.IsExplicitInterfaceImplementation;
			if (isExplicitInterfaceImplementation || this.IsOverride)
			{
				bool flag = false;
				PropertySymbol propertySymbol2;
				if (!isExplicitInterfaceImplementation)
				{
					flag = true;
					propertySymbol2 = base.OverriddenProperty;
				}
				else
				{
					CSharpSyntaxNode csharpSyntaxNode = this.CSharpSyntaxNode;
					string text = (this.IsIndexer ? "this[]" : ((PropertyDeclarationSyntax)csharpSyntaxNode).Identifier.ValueText);
					propertySymbol = this.FindExplicitlyImplementedProperty(this._explicitInterfaceType, text, this.GetExplicitInterfaceSpecifier(), diagnostics);
					this.FindExplicitlyImplementedMemberVerification(propertySymbol, diagnostics);
					propertySymbol2 = propertySymbol;
				}
				if (propertySymbol2 != null)
				{
					this._lazyRefCustomModifiers = ((this._refKind != RefKind.None) ? propertySymbol2.RefCustomModifiers : ImmutableArray<CustomModifier>.Empty);
					TypeWithAnnotations typeWithAnnotations2 = propertySymbol2.TypeWithAnnotations;
					if (typeWithAnnotations.Type.Equals(typeWithAnnotations2.Type, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
					{
						typeWithAnnotations = typeWithAnnotations.WithTypeAndModifiers(CustomModifierUtils.CopyTypeCustomModifiers(typeWithAnnotations2.Type, typeWithAnnotations.Type, this.ContainingAssembly), typeWithAnnotations2.CustomModifiers);
						this._lazyType = new TypeWithAnnotations.Boxed(typeWithAnnotations);
					}
					this._lazyParameters = CustomModifierUtils.CopyParameterCustomModifiers(propertySymbol2.Parameters, this._lazyParameters, flag);
				}
			}
			else if (this._refKind == RefKind.In)
			{
				NamedTypeSymbol wellKnownType = Binder.GetWellKnownType(this.DeclaringCompilation, WellKnownType.System_Runtime_InteropServices_InAttribute, diagnostics, this.TypeLocation);
				this._lazyRefCustomModifiers = ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(wellKnownType));
			}
			this._lazyExplicitInterfaceImplementations = ((propertySymbol == null) ? ImmutableArray<PropertySymbol>.Empty : ImmutableArray.Create<PropertySymbol>(propertySymbol));
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x0600731C RID: 29468
		[global::System.Runtime.CompilerServices.Nullable(1)]
		protected abstract Location TypeLocation
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x0600731D RID: 29469 RVA: 0x001AEE65 File Offset: 0x001AD065
		internal sealed override ImmutableArray<string> NotNullMembers
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullMembers;
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x0600731E RID: 29470 RVA: 0x001AEE7C File Offset: 0x001AD07C
		internal sealed override ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullWhenTrueMembers;
			}
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x0600731F RID: 29471 RVA: 0x001AEE93 File Offset: 0x001AD093
		internal sealed override ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullWhenFalseMembers;
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x06007320 RID: 29472 RVA: 0x001AEEAA File Offset: 0x001AD0AA
		internal bool IsExpressionBodied
		{
			get
			{
				return (this._propertyFlags & SourcePropertySymbolBase.Flags.IsExpressionBodied) > (SourcePropertySymbolBase.Flags)0;
			}
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x001AEEB7 File Offset: 0x001AD0B7
		private void CheckInitializer(bool isAutoProperty, bool isInterface, bool isStatic, Location location, BindingDiagnosticBag diagnostics)
		{
			if (isInterface && !isStatic)
			{
				diagnostics.Add(ErrorCode.ERR_InstancePropertyInitializerInInterface, location, new object[] { this });
				return;
			}
			if (!isAutoProperty)
			{
				diagnostics.Add(ErrorCode.ERR_InitializerOnNonAutoProperty, location, new object[] { this });
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x06007322 RID: 29474 RVA: 0x001AEEF5 File Offset: 0x001AD0F5
		public sealed override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x06007323 RID: 29475 RVA: 0x001AEEFD File Offset: 0x001AD0FD
		public sealed override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				this.EnsureSignature();
				return this._lazyType.Value;
			}
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x001AEF10 File Offset: 0x001AD110
		private void EnsureSignature()
		{
			if (!this._state.HasComplete(CompletionPart.FinishBaseType))
			{
				SyntaxReference syntaxRef = this._syntaxRef;
				lock (syntaxRef)
				{
					if (this._state.NotePartComplete(CompletionPart.StartBaseType))
					{
						BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
						try
						{
							this.EnsureSignatureGuarded(instance);
							this.AddDeclarationDiagnostics(instance);
						}
						finally
						{
							this._state.NotePartComplete(CompletionPart.FinishBaseType);
							instance.Free();
						}
					}
				}
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x06007325 RID: 29477 RVA: 0x001AEFA0 File Offset: 0x001AD1A0
		internal bool HasPointerType
		{
			get
			{
				if (this._lazyType != null)
				{
					return this._lazyType.Value.DefaultType.IsPointerOrFunctionPointer();
				}
				return this.HasPointerTypeSyntactically;
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x06007326 RID: 29478
		protected abstract bool HasPointerTypeSyntactically { get; }

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06007327 RID: 29479 RVA: 0x001AEFC6 File Offset: 0x001AD1C6
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06007328 RID: 29480 RVA: 0x001AEFD0 File Offset: 0x001AD1D0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal string SourceName
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				if (this._lazySourceName == null)
				{
					SyntaxList<AttributeListSyntax> attributeLists = ((IndexerDeclarationSyntax)this.CSharpSyntaxNode).AttributeLists;
					string text = null;
					CustomAttributesBag<CSharpAttributeData> customAttributesBag = null;
					base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(attributeLists), ref customAttributesBag, AttributeLocation.None, true, null, null);
					if (customAttributesBag != null)
					{
						PropertyEarlyWellKnownAttributeData propertyEarlyWellKnownAttributeData = (PropertyEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
						if (propertyEarlyWellKnownAttributeData != null)
						{
							text = propertyEarlyWellKnownAttributeData.IndexerName;
						}
					}
					text = text ?? "Item";
					InterlockedOperations.Initialize<string>(ref this._lazySourceName, text);
				}
				return this._lazySourceName;
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06007329 RID: 29481 RVA: 0x001AF044 File Offset: 0x001AD244
		public override string MetadataName
		{
			get
			{
				return this.SourceName.Replace(" ", "");
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x0600732A RID: 29482 RVA: 0x001AF05B File Offset: 0x001AD25B
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x0600732B RID: 29483 RVA: 0x001AF063 File Offset: 0x001AD263
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x0600732C RID: 29484 RVA: 0x001AF06B File Offset: 0x001AD26B
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return new LexicalSortKey(this.Location, this.DeclaringCompilation);
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x001AF07E File Offset: 0x001AD27E
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray.Create<Location>(this.Location);
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x0600732E RID: 29486 RVA: 0x001AF08B File Offset: 0x001AD28B
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this._syntaxRef);
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x001AF098 File Offset: 0x001AD298
		public override bool IsAbstract
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Abstract) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x06007330 RID: 29488 RVA: 0x001AF0A5 File Offset: 0x001AD2A5
		public override bool IsExtern
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Extern) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x001AF0B6 File Offset: 0x001AD2B6
		public override bool IsStatic
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06007332 RID: 29490 RVA: 0x001AF0C3 File Offset: 0x001AD2C3
		internal bool IsFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06007333 RID: 29491 RVA: 0x001AF0C6 File Offset: 0x001AD2C6
		public override bool IsIndexer
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Indexer) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x06007334 RID: 29492 RVA: 0x001AF0D7 File Offset: 0x001AD2D7
		public override bool IsOverride
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Override) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x06007335 RID: 29493 RVA: 0x001AF0E8 File Offset: 0x001AD2E8
		public override bool IsSealed
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Sealed) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06007336 RID: 29494 RVA: 0x001AF0F5 File Offset: 0x001AD2F5
		public override bool IsVirtual
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Virtual) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06007337 RID: 29495 RVA: 0x001AF106 File Offset: 0x001AD306
		internal bool IsNew
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.New) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06007338 RID: 29496 RVA: 0x001AF113 File Offset: 0x001AD313
		internal bool HasReadOnlyModifier
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x06007339 RID: 29497
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract SourcePropertyAccessorSymbol CreateGetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics);

		// Token: 0x0600733A RID: 29498
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract SourcePropertyAccessorSymbol CreateSetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics);

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x001AF124 File Offset: 0x001AD324
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override MethodSymbol GetMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._getMethod;
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x0600733C RID: 29500 RVA: 0x001AF12C File Offset: 0x001AD32C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override MethodSymbol SetMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._setMethod;
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x0600733D RID: 29501 RVA: 0x001AF134 File Offset: 0x001AD334
		internal override CallingConvention CallingConvention
		{
			get
			{
				if (!this.IsStatic)
				{
					return CallingConvention.HasThis;
				}
				return CallingConvention.Default;
			}
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x001AF142 File Offset: 0x001AD342
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				this.EnsureSignature();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x0600733F RID: 29503 RVA: 0x001AF150 File Offset: 0x001AD350
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return (this._propertyFlags & SourcePropertySymbolBase.Flags.IsExplicitInterfaceImplementation) > (SourcePropertySymbolBase.Flags)0;
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06007340 RID: 29504 RVA: 0x001AF15D File Offset: 0x001AD35D
		public sealed override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if (this.IsExplicitInterfaceImplementation)
				{
					this.EnsureSignature();
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06007341 RID: 29505 RVA: 0x001AF173 File Offset: 0x001AD373
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				this.EnsureSignature();
				return this._lazyRefCustomModifiers;
			}
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06007342 RID: 29506 RVA: 0x001AF181 File Offset: 0x001AD381
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this._modifiers);
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06007343 RID: 29507 RVA: 0x001AF18E File Offset: 0x001AD38E
		public bool HasSkipLocalsInitAttribute
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSkipLocalsInitAttribute;
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06007344 RID: 29508 RVA: 0x001AF1A1 File Offset: 0x001AD3A1
		internal bool IsAutoPropertyWithGetAccessor
		{
			get
			{
				return this.IsAutoProperty && this._getMethod != null;
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06007345 RID: 29509 RVA: 0x001AF1B6 File Offset: 0x001AD3B6
		protected bool IsAutoProperty
		{
			get
			{
				return (this._propertyFlags & SourcePropertySymbolBase.Flags.IsAutoProperty) > (SourcePropertySymbolBase.Flags)0;
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06007346 RID: 29510 RVA: 0x001AF1C3 File Offset: 0x001AD3C3
		internal SynthesizedBackingFieldSymbol BackingField { get; }

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x001AF1CB File Offset: 0x001AD3CB
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06007348 RID: 29512 RVA: 0x001AF1CE File Offset: 0x001AD3CE
		internal SyntaxReference SyntaxReference
		{
			get
			{
				return this._syntaxRef;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x001AF1D8 File Offset: 0x001AD3D8
		internal CSharpSyntaxNode CSharpSyntaxNode
		{
			get
			{
				return (CSharpSyntaxNode)this._syntaxRef.GetSyntax(default(CancellationToken));
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x0600734A RID: 29514 RVA: 0x001AF1FE File Offset: 0x001AD3FE
		internal SyntaxTree SyntaxTree
		{
			get
			{
				return this._syntaxRef.SyntaxTree;
			}
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x001AF20C File Offset: 0x001AD40C
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			bool isExplicitInterfaceImplementation = this.IsExplicitInterfaceImplementation;
			this.CheckAccessibility(this.Location, diagnostics, isExplicitInterfaceImplementation);
			this.CheckModifiers(isExplicitInterfaceImplementation, this.Location, this.IsIndexer, diagnostics);
			if ((this._propertyFlags & SourcePropertySymbolBase.Flags.HasInitializer) > (SourcePropertySymbolBase.Flags)0)
			{
				this.CheckInitializer(this.IsAutoProperty, this.ContainingType.IsInterface, this.IsStatic, this.Location, diagnostics);
			}
			if (this.IsAutoPropertyWithGetAccessor)
			{
				if (!this.IsStatic)
				{
					MethodSymbol setMethod = this.SetMethod;
					if (setMethod != null && !setMethod.IsInitOnly)
					{
						if (this.ContainingType.IsReadOnly)
						{
							diagnostics.Add(ErrorCode.ERR_AutoPropsInRoStruct, this.Location);
						}
						else if (this.HasReadOnlyModifier)
						{
							diagnostics.Add(ErrorCode.ERR_AutoPropertyWithSetterCantBeReadOnly, this.Location, new object[] { this });
						}
					}
				}
				Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(this.DeclaringCompilation, WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, diagnostics, this.Location, null);
				if (this.RefKind != RefKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_AutoPropertyCannotBeRefReturning, this.Location, new object[] { this });
				}
				if (this.SetMethod == null && !base.IsReadOnly)
				{
					diagnostics.Add(ErrorCode.ERR_AutoPropertyMustOverrideSet, this.Location, new object[] { this });
				}
			}
			if (!this.IsExpressionBodied)
			{
				bool flag = this.GetMethod != null;
				bool flag2 = this.SetMethod != null;
				if (flag && flag2)
				{
					if (this._refKind != RefKind.None)
					{
						diagnostics.Add(ErrorCode.ERR_RefPropertyCannotHaveSetAccessor, this._setMethod.Locations[0], new object[] { this._setMethod });
					}
					else if (this._getMethod.LocalAccessibility != Accessibility.NotApplicable && this._setMethod.LocalAccessibility != Accessibility.NotApplicable)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicatePropertyAccessMods, this.Location, new object[] { this });
					}
					else if (this._getMethod.LocalDeclaredReadOnly && this._setMethod.LocalDeclaredReadOnly)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicatePropertyReadOnlyMods, this.Location, new object[] { this });
					}
					else if (this.IsAbstract)
					{
						SourcePropertySymbolBase.CheckAbstractPropertyAccessorNotPrivate(this._getMethod, diagnostics);
						SourcePropertySymbolBase.CheckAbstractPropertyAccessorNotPrivate(this._setMethod, diagnostics);
					}
				}
				else
				{
					if (!flag && !flag2)
					{
						diagnostics.Add(ErrorCode.ERR_PropertyWithNoAccessors, this.Location, new object[] { this });
					}
					else if (this.RefKind != RefKind.None)
					{
						if (!flag)
						{
							diagnostics.Add(ErrorCode.ERR_RefPropertyMustHaveGetAccessor, this.Location, new object[] { this });
						}
					}
					else if (!flag && this.IsAutoProperty)
					{
						diagnostics.Add(ErrorCode.ERR_AutoPropertyMustHaveGetAccessor, this._setMethod.Locations[0], new object[] { this._setMethod });
					}
					if (!this.IsOverride)
					{
						SourcePropertyAccessorSymbol sourcePropertyAccessorSymbol = this._getMethod ?? this._setMethod;
						if (sourcePropertyAccessorSymbol != null)
						{
							if (sourcePropertyAccessorSymbol.LocalAccessibility != Accessibility.NotApplicable)
							{
								diagnostics.Add(ErrorCode.ERR_AccessModMissingAccessor, this.Location, new object[] { this });
							}
							if (sourcePropertyAccessorSymbol.LocalDeclaredReadOnly)
							{
								diagnostics.Add(ErrorCode.ERR_ReadOnlyModMissingAccessor, this.Location, new object[] { this });
							}
						}
					}
				}
				this.CheckAccessibilityMoreRestrictive(this._getMethod, diagnostics);
				this.CheckAccessibilityMoreRestrictive(this._setMethod, diagnostics);
			}
			PropertySymbol propertySymbol = this.ExplicitInterfaceImplementations.FirstOrDefault<PropertySymbol>();
			if (propertySymbol != null)
			{
				this.CheckExplicitImplementationAccessor(this.GetMethod, propertySymbol.GetMethod, propertySymbol, diagnostics);
				this.CheckExplicitImplementationAccessor(this.SetMethod, propertySymbol.SetMethod, propertySymbol, diagnostics);
			}
			Location typeLocation = this.TypeLocation;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (this._explicitInterfaceType != null)
			{
				ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = this.GetExplicitInterfaceSpecifier();
				this._explicitInterfaceType.CheckAllConstraints(declaringCompilation, conversions, new SourceLocation(explicitInterfaceSpecifier.Name), diagnostics);
				if (propertySymbol != null)
				{
					TypeSymbol.CheckNullableReferenceTypeMismatchOnImplementingMember(this.ContainingType, this, propertySymbol, true, diagnostics);
				}
			}
			if (this._refKind == RefKind.In)
			{
				declaringCompilation.EnsureIsReadOnlyAttributeExists(diagnostics, typeLocation, true);
			}
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (base.Type.ContainsNativeInteger())
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, typeLocation, true);
			}
			ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && this.TypeWithAnnotations.NeedsNullableAttribute())
			{
				declaringCompilation.EnsureNullableAttributeExists(diagnostics, typeLocation, true);
			}
			ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, this.Parameters, diagnostics, true);
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x001AF650 File Offset: 0x001AD850
		private void CheckAccessibility(Location location, BindingDiagnosticBag diagnostics, bool isExplicitInterfaceImplementation)
		{
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this._modifiers, this, isExplicitInterfaceImplementation);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(new CSDiagnostic(csdiagnosticInfo, location, false));
			}
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x001AF67C File Offset: 0x001AD87C
		private void CheckModifiers(bool isExplicitInterfaceImplementation, Location location, bool isIndexer, BindingDiagnosticBag diagnostics)
		{
			bool flag = isExplicitInterfaceImplementation && this.ContainingType.IsInterface;
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
			if (this.IsStatic && this.HasReadOnlyModifier)
			{
				diagnostics.Add(ErrorCode.ERR_StaticMemberCantBeReadOnly, location, new object[] { this });
				return;
			}
			if (this.IsOverride && (this.IsNew || this.IsVirtual))
			{
				diagnostics.Add(ErrorCode.ERR_OverrideNotNew, location, new object[] { this });
				return;
			}
			if (this.IsSealed && !this.IsOverride && (!this.IsAbstract || !flag))
			{
				diagnostics.Add(ErrorCode.ERR_SealedNonOverride, location, new object[] { this });
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
			if (this.ContainingType.IsSealed && this.DeclaredAccessibility.HasProtected() && !this.IsOverride)
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsStatic && !this.IsStatic)
			{
				ErrorCode errorCode = (isIndexer ? ErrorCode.ERR_IndexerInStaticClass : ErrorCode.ERR_InstanceMemberInStaticClass);
				diagnostics.Add(errorCode, location, new object[] { this });
			}
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x001AF910 File Offset: 0x001ADB10
		private void CheckAccessibilityMoreRestrictive(SourcePropertyAccessorSymbol accessor, BindingDiagnosticBag diagnostics)
		{
			if (accessor != null && !SourcePropertySymbolBase.IsAccessibilityMoreRestrictive(this.DeclaredAccessibility, accessor.LocalAccessibility))
			{
				diagnostics.Add(ErrorCode.ERR_InvalidPropertyAccessMod, accessor.Locations[0], new object[] { accessor, this });
			}
		}

		// Token: 0x0600734F RID: 29519 RVA: 0x001AF95C File Offset: 0x001ADB5C
		private static bool IsAccessibilityMoreRestrictive(Accessibility property, Accessibility accessor)
		{
			return accessor == Accessibility.NotApplicable || (accessor < property && (accessor != Accessibility.Protected || property != Accessibility.Internal));
		}

		// Token: 0x06007350 RID: 29520 RVA: 0x001AF978 File Offset: 0x001ADB78
		private static void CheckAbstractPropertyAccessorNotPrivate(SourcePropertyAccessorSymbol accessor, BindingDiagnosticBag diagnostics)
		{
			if (accessor.LocalAccessibility == Accessibility.Private)
			{
				diagnostics.Add(ErrorCode.ERR_PrivateAbstractAccessor, accessor.Locations[0], new object[] { accessor });
			}
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x001AF9B4 File Offset: 0x001ADBB4
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this._lazyExpandedDocComment : ref this._lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this, expandIncludes, ref ptr);
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x001AF9DC File Offset: 0x001ADBDC
		private void CheckExplicitImplementationAccessor(MethodSymbol thisAccessor, MethodSymbol otherAccessor, PropertySymbol explicitlyImplementedProperty, BindingDiagnosticBag diagnostics)
		{
			bool flag = thisAccessor != null;
			bool flag2 = otherAccessor.IsImplementable();
			if (flag2 && !flag)
			{
				diagnostics.Add(ErrorCode.ERR_ExplicitPropertyMissingAccessor, this.Location, new object[] { this, otherAccessor });
				return;
			}
			if (!flag2 && flag)
			{
				diagnostics.Add(ErrorCode.ERR_ExplicitPropertyAddingAccessor, thisAccessor.Locations[0], new object[] { thisAccessor, explicitlyImplementedProperty });
				return;
			}
			if (TypeSymbol.HaveInitOnlyMismatch(thisAccessor, otherAccessor))
			{
				diagnostics.Add(ErrorCode.ERR_ExplicitPropertyMismatchInitOnly, thisAccessor.Locations[0], new object[] { thisAccessor, otherAccessor });
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x06007353 RID: 29523 RVA: 0x001AFA83 File Offset: 0x001ADC83
		internal override OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				if (this._lazyOverriddenOrHiddenMembers == null)
				{
					Interlocked.CompareExchange<OverriddenOrHiddenMembersResult>(ref this._lazyOverriddenOrHiddenMembers, this.MakeOverriddenOrHiddenMembers(), null);
				}
				return this._lazyOverriddenOrHiddenMembers;
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06007354 RID: 29524 RVA: 0x001AFAA8 File Offset: 0x001ADCA8
		internal SynthesizedSealedPropertyAccessor SynthesizedSealedAccessorOpt
		{
			get
			{
				bool flag = this.GetMethod != null;
				bool flag2 = this.SetMethod != null;
				if (!this.IsSealed || (flag && flag2))
				{
					return null;
				}
				if (this._lazySynthesizedSealedAccessor == null)
				{
					Interlocked.CompareExchange<SynthesizedSealedPropertyAccessor>(ref this._lazySynthesizedSealedAccessor, this.MakeSynthesizedSealedAccessor(), null);
				}
				return this._lazySynthesizedSealedAccessor;
			}
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x001AFAFC File Offset: 0x001ADCFC
		private SynthesizedSealedPropertyAccessor MakeSynthesizedSealedAccessor()
		{
			if (this.GetMethod != null)
			{
				MethodSymbol ownOrInheritedSetMethod = this.GetOwnOrInheritedSetMethod();
				if (ownOrInheritedSetMethod != null)
				{
					return new SynthesizedSealedPropertyAccessor(this, ownOrInheritedSetMethod);
				}
				return null;
			}
			else
			{
				if (this.SetMethod == null)
				{
					return null;
				}
				MethodSymbol ownOrInheritedGetMethod = this.GetOwnOrInheritedGetMethod();
				if (ownOrInheritedGetMethod != null)
				{
					return new SynthesizedSealedPropertyAccessor(this, ownOrInheritedGetMethod);
				}
				return null;
			}
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06007356 RID: 29526
		public abstract SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList { get; }

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06007357 RID: 29527
		public abstract IAttributeTargetSymbol AttributesOwner { get; }

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06007358 RID: 29528 RVA: 0x001AFB42 File Offset: 0x001ADD42
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this.AttributesOwner;
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06007359 RID: 29529 RVA: 0x001AFB4A File Offset: 0x001ADD4A
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Property;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x0600735A RID: 29530 RVA: 0x001AFB4E File Offset: 0x001ADD4E
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				if (!this.IsAutoPropertyWithGetAccessor)
				{
					return AttributeLocation.Property;
				}
				return AttributeLocation.Field | AttributeLocation.Property;
			}
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x001AFB60 File Offset: 0x001ADD60
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsSealed)
			{
				return lazyCustomAttributesBag;
			}
			SynthesizedBackingFieldSymbol backingField = this.BackingField;
			if (backingField != null)
			{
				backingField.GetAttributes();
			}
			if (base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.AttributeDeclarationSyntaxList), ref this._lazyCustomAttributesBag, AttributeLocation.None, false, null, null))
			{
				this._state.NotePartComplete(CompletionPart.Attributes);
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x0600735C RID: 29532 RVA: 0x001AFBC3 File Offset: 0x001ADDC3
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x0600735D RID: 29533 RVA: 0x001AFBD0 File Offset: 0x001ADDD0
		private PropertyWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (PropertyWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x001AFC04 File Offset: 0x001ADE04
		internal PropertyEarlyWellKnownAttributeData GetEarlyDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (PropertyEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x001AFC38 File Offset: 0x001ADE38
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations typeWithAnnotations = this.TypeWithAnnotations;
			if (typeWithAnnotations.Type.ContainsDynamic())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(typeWithAnnotations.Type, typeWithAnnotations.CustomModifiers.Length + this.RefCustomModifiers.Length, this._refKind));
			}
			if (typeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, typeWithAnnotations.Type));
			}
			if (typeWithAnnotations.Type.ContainsTupleNames())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(typeWithAnnotations.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.ContainingType.GetNullableContextValue(), typeWithAnnotations));
			}
			if (base.ReturnsByRefReadonly)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06007360 RID: 29536 RVA: 0x001AFD1A File Offset: 0x001ADF1A
		internal sealed override bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasExcludeFromCodeCoverageAttribute;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06007361 RID: 29537 RVA: 0x001AFD30 File Offset: 0x001ADF30
		internal override bool HasSpecialName
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSpecialNameAttribute;
			}
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x001AFD50 File Offset: 0x001ADF50
		internal override CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute;
			ObsoleteAttributeData obsoleteAttributeData;
			if (Symbol.EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref arguments, out attribute, out obsoleteAttributeData))
			{
				if (obsoleteAttributeData != null)
				{
					arguments.GetOrCreateData<PropertyEarlyWellKnownAttributeData>().ObsoleteAttributeData = obsoleteAttributeData;
				}
				return attribute;
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.IndexerNameAttribute))
			{
				bool flag;
				attribute = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
				if (!attribute.HasErrors)
				{
					string text = attribute.CommonConstructorArguments[0].DecodeValue<string>(SpecialType.System_String);
					if (text != null)
					{
						arguments.GetOrCreateData<PropertyEarlyWellKnownAttributeData>().IndexerName = text;
					}
					if (!flag)
					{
						return attribute;
					}
				}
				return null;
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06007363 RID: 29539 RVA: 0x001AFDF0 File Offset: 0x001ADFF0
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!this._containingType.AnyMemberHasAttributes)
				{
					return null;
				}
				CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
				if (lazyCustomAttributesBag == null || !lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
				{
					return ObsoleteAttributeData.Uninitialized;
				}
				PropertyEarlyWellKnownAttributeData propertyEarlyWellKnownAttributeData = (PropertyEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
				if (propertyEarlyWellKnownAttributeData == null)
				{
					return null;
				}
				return propertyEarlyWellKnownAttributeData.ObsoleteAttributeData;
			}
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x001AFE3C File Offset: 0x001AE03C
		internal override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			CSharpAttributeData attribute = arguments.Attribute;
			if (attribute.IsTargetAttribute(this, AttributeDescription.IndexerNameAttribute))
			{
				this.ValidateIndexerNameAttribute(attribute, arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SpecialNameAttribute))
			{
				arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasSpecialNameAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ExcludeFromCodeCoverageAttribute))
			{
				arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasExcludeFromCodeCoverageAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SkipLocalsInitAttribute))
			{
				CSharpAttributeData.DecodeSkipLocalsInitAttribute<PropertyWellKnownAttributeData>(this.DeclaringCompilation, ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DynamicAttribute))
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ExplicitDynamicAttr, arguments.AttributeSyntaxOpt.Location);
				return;
			}
			if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.DynamicAttribute | Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute))
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.DisallowNullAttribute))
				{
					arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasDisallowNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AllowNullAttribute))
				{
					arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasAllowNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.MaybeNullAttribute))
				{
					arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasMaybeNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullAttribute))
				{
					arguments.GetOrCreateData<PropertyWellKnownAttributeData>().HasNotNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.MemberNotNullAttribute))
				{
					MessageID.IDS_FeatureMemberNotNull.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
					CSharpAttributeData.DecodeMemberNotNullAttribute<PropertyWellKnownAttributeData>(this.ContainingType, ref arguments);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.MemberNotNullWhenAttribute))
				{
					MessageID.IDS_FeatureMemberNotNull.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
					CSharpAttributeData.DecodeMemberNotNullWhenAttribute<PropertyWellKnownAttributeData>(this.ContainingType, ref arguments);
				}
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06007365 RID: 29541 RVA: 0x001AFFC8 File Offset: 0x001AE1C8
		internal bool HasDisallowNull
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasDisallowNullAttribute;
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06007366 RID: 29542 RVA: 0x001AFFE8 File Offset: 0x001AE1E8
		internal bool HasAllowNull
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasAllowNullAttribute;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06007367 RID: 29543 RVA: 0x001B0008 File Offset: 0x001AE208
		internal bool HasMaybeNull
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasMaybeNullAttribute;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06007368 RID: 29544 RVA: 0x001B0028 File Offset: 0x001AE228
		internal bool HasNotNull
		{
			get
			{
				PropertyWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasNotNullAttribute;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06007369 RID: 29545 RVA: 0x001B0047 File Offset: 0x001AE247
		internal SourceAttributeData DisallowNullAttributeIfExists
		{
			get
			{
				return this.FindAttribute(AttributeDescription.DisallowNullAttribute);
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x0600736A RID: 29546 RVA: 0x001B0054 File Offset: 0x001AE254
		internal SourceAttributeData AllowNullAttributeIfExists
		{
			get
			{
				return this.FindAttribute(AttributeDescription.AllowNullAttribute);
			}
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x0600736B RID: 29547 RVA: 0x001B0061 File Offset: 0x001AE261
		internal SourceAttributeData MaybeNullAttributeIfExists
		{
			get
			{
				return this.FindAttribute(AttributeDescription.MaybeNullAttribute);
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x0600736C RID: 29548 RVA: 0x001B006E File Offset: 0x001AE26E
		internal SourceAttributeData NotNullAttributeIfExists
		{
			get
			{
				return this.FindAttribute(AttributeDescription.NotNullAttribute);
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x0600736D RID: 29549 RVA: 0x001B007B File Offset: 0x001AE27B
		internal ImmutableArray<SourceAttributeData> MemberNotNullAttributeIfExists
		{
			get
			{
				return this.FindAttributes(AttributeDescription.MemberNotNullAttribute);
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x0600736E RID: 29550 RVA: 0x001B0088 File Offset: 0x001AE288
		internal ImmutableArray<SourceAttributeData> MemberNotNullWhenAttributeIfExists
		{
			get
			{
				return this.FindAttributes(AttributeDescription.MemberNotNullWhenAttribute);
			}
		}

		// Token: 0x0600736F RID: 29551 RVA: 0x001B0098 File Offset: 0x001AE298
		private SourceAttributeData FindAttribute(AttributeDescription attributeDescription)
		{
			return (SourceAttributeData)this.GetAttributes().First<CSharpAttributeData>((CSharpAttributeData a) => a.IsTargetAttribute(this, attributeDescription));
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x001B00D8 File Offset: 0x001AE2D8
		private ImmutableArray<SourceAttributeData> FindAttributes(AttributeDescription attributeDescription)
		{
			return Enumerable.Cast<SourceAttributeData>(from a in this.GetAttributes()
				where a.IsTargetAttribute(this, attributeDescription)
				select a).ToImmutableArray<SourceAttributeData>();
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x001B011A File Offset: 0x001AE31A
		internal override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x001B012C File Offset: 0x001AE32C
		private void ValidateIndexerNameAttribute(CSharpAttributeData attribute, AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			if (!this.IsIndexer || this.IsExplicitInterfaceImplementation)
			{
				diagnostics.Add(ErrorCode.ERR_BadIndexerNameAttr, node.Name.Location, new object[] { node.GetErrorDisplayName() });
				return;
			}
			string text = attribute.CommonConstructorArguments[0].DecodeValue<string>(SpecialType.System_String);
			if (text == null || !SyntaxFacts.IsValidIdentifier(text))
			{
				diagnostics.Add(ErrorCode.ERR_BadArgumentToAttribute, node.ArgumentList.Arguments[0].Location, new object[] { node.GetErrorDisplayName() });
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06007373 RID: 29555 RVA: 0x001B01C9 File Offset: 0x001AE3C9
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x001B01CC File Offset: 0x001AE3CC
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this._state.HasComplete(part);
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x001B01DC File Offset: 0x001AE3DC
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this._state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.FinishBaseType)
				{
					if (nextIncompletePart <= CompletionPart.Attributes)
					{
						if (nextIncompletePart == CompletionPart.None)
						{
							break;
						}
						if (nextIncompletePart != CompletionPart.Attributes)
						{
							goto IL_01DE;
						}
						this.GetAttributes();
					}
					else
					{
						if (nextIncompletePart != CompletionPart.StartBaseType && nextIncompletePart != CompletionPart.FinishBaseType)
						{
							goto IL_01DE;
						}
						this.EnsureSignature();
					}
				}
				else if (nextIncompletePart <= CompletionPart.FinishInterfaces)
				{
					if (nextIncompletePart != CompletionPart.StartInterfaces && nextIncompletePart != CompletionPart.FinishInterfaces)
					{
						goto IL_01DE;
					}
					if (this._state.NotePartComplete(CompletionPart.StartInterfaces))
					{
						if (this.Parameters.Length > 0)
						{
							BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
							TypeConversions typeConversions = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
							foreach (ParameterSymbol parameterSymbol in this.Parameters)
							{
								parameterSymbol.ForceComplete(locationOpt, cancellationToken);
								parameterSymbol.Type.CheckAllConstraints(this.DeclaringCompilation, typeConversions, parameterSymbol.Locations[0], instance);
							}
							this.AddDeclarationDiagnostics(instance);
							instance.Free();
						}
						this.DeclaringCompilation.SymbolDeclaredEvent(this);
						this._state.NotePartComplete(CompletionPart.FinishInterfaces);
					}
					else
					{
						this._state.SpinWaitComplete(CompletionPart.FinishInterfaces, cancellationToken);
					}
				}
				else
				{
					if (nextIncompletePart != CompletionPart.EnumUnderlyingType && nextIncompletePart != CompletionPart.TypeArguments)
					{
						goto IL_01DE;
					}
					if (this._state.NotePartComplete(CompletionPart.EnumUnderlyingType))
					{
						BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
						TypeConversions typeConversions2 = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
						base.Type.CheckAllConstraints(this.DeclaringCompilation, typeConversions2, this.Location, instance2);
						this.ValidatePropertyType(instance2);
						this.AddDeclarationDiagnostics(instance2);
						this._state.NotePartComplete(CompletionPart.TypeArguments);
						instance2.Free();
					}
					else
					{
						this._state.SpinWaitComplete(CompletionPart.TypeArguments, cancellationToken);
					}
				}
				IL_01EF:
				this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_01DE:
				this._state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.TypeParameters | CompletionPart.Members | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
				goto IL_01EF;
			}
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x001B03EC File Offset: 0x001AE5EC
		protected virtual void ValidatePropertyType(BindingDiagnosticBag diagnostics)
		{
			TypeSymbol type = base.Type;
			if (type.IsRestrictedType(true))
			{
				diagnostics.Add(ErrorCode.ERR_FieldCantBeRefAny, this.TypeLocation, new object[] { type });
				return;
			}
			if (this.IsAutoPropertyWithGetAccessor && type.IsRefLikeType && (this.IsStatic || !this.ContainingType.IsRefLikeType))
			{
				diagnostics.Add(ErrorCode.ERR_FieldAutoPropCantBeByRefLike, this.TypeLocation, new object[] { type });
			}
		}

		// Token: 0x06007377 RID: 29559
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: TupleElementNames(new string[] { "Type", "Parameters" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected abstract ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindType(BindingDiagnosticBag diagnostics);

		// Token: 0x06007378 RID: 29560 RVA: 0x001B0467 File Offset: 0x001AE667
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected static ExplicitInterfaceSpecifierSyntax GetExplicitInterfaceSpecifier(SyntaxNode syntax)
		{
			BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = syntax as BasePropertyDeclarationSyntax;
			if (basePropertyDeclarationSyntax == null)
			{
				return null;
			}
			return basePropertyDeclarationSyntax.ExplicitInterfaceSpecifier;
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x001B047A File Offset: 0x001AE67A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ExplicitInterfaceSpecifierSyntax GetExplicitInterfaceSpecifier()
		{
			return SourcePropertySymbolBase.GetExplicitInterfaceSpecifier(this.CSharpSyntaxNode);
		}

		// Token: 0x04001BFA RID: 7162
		protected const string DefaultIndexerName = "Item";

		// Token: 0x04001BFB RID: 7163
		private readonly SourceMemberContainerTypeSymbol _containingType;

		// Token: 0x04001BFC RID: 7164
		private readonly string _name;

		// Token: 0x04001BFD RID: 7165
		private readonly SyntaxReference _syntaxRef;

		// Token: 0x04001BFE RID: 7166
		protected readonly DeclarationModifiers _modifiers;

		// Token: 0x04001BFF RID: 7167
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001C00 RID: 7168
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly SourcePropertyAccessorSymbol _getMethod;

		// Token: 0x04001C01 RID: 7169
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly SourcePropertyAccessorSymbol _setMethod;

		// Token: 0x04001C02 RID: 7170
		private readonly TypeSymbol _explicitInterfaceType;

		// Token: 0x04001C03 RID: 7171
		private ImmutableArray<PropertySymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001C04 RID: 7172
		private readonly SourcePropertySymbolBase.Flags _propertyFlags;

		// Token: 0x04001C05 RID: 7173
		private readonly RefKind _refKind;

		// Token: 0x04001C06 RID: 7174
		private SymbolCompletionState _state;

		// Token: 0x04001C07 RID: 7175
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001C08 RID: 7176
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001C09 RID: 7177
		private string _lazySourceName;

		// Token: 0x04001C0A RID: 7178
		private string _lazyDocComment;

		// Token: 0x04001C0B RID: 7179
		private string _lazyExpandedDocComment;

		// Token: 0x04001C0C RID: 7180
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;

		// Token: 0x04001C0D RID: 7181
		private SynthesizedSealedPropertyAccessor _lazySynthesizedSealedAccessor;

		// Token: 0x04001C0E RID: 7182
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x0200093E RID: 2366
		[Flags]
		private enum Flags : byte
		{
			// Token: 0x0400281D RID: 10269
			IsExpressionBodied = 1,
			// Token: 0x0400281E RID: 10270
			IsAutoProperty = 2,
			// Token: 0x0400281F RID: 10271
			IsExplicitInterfaceImplementation = 4,
			// Token: 0x04002820 RID: 10272
			HasInitializer = 8
		}
	}
}
