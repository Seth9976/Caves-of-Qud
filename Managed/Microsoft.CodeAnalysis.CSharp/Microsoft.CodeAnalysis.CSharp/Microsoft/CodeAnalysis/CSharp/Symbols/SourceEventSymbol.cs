using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000535 RID: 1333
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SourceEventSymbol : EventSymbol, IAttributeTargetSymbol
	{
		// Token: 0x06006FC3 RID: 28611 RVA: 0x0019A878 File Offset: 0x00198A78
		internal SourceEventSymbol(SourceMemberContainerTypeSymbol containingType, CSharpSyntaxNode syntax, SyntaxTokenList modifiers, bool isFieldLike, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax interfaceSpecifierSyntaxOpt, SyntaxToken nameTokenSyntax, BindingDiagnosticBag diagnostics)
		{
			this._location = nameTokenSyntax.GetLocation();
			this.containingType = containingType;
			this._syntaxRef = syntax.GetReference();
			bool flag = interfaceSpecifierSyntaxOpt != null;
			bool flag2;
			this._modifiers = this.MakeModifiers(modifiers, flag, isFieldLike, this._location, diagnostics, out flag2);
			this.CheckAccessibility(this._location, diagnostics, flag);
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x0019A8DA File Offset: 0x00198ADA
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x0019A8DD File Offset: 0x00198ADD
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this._state.HasComplete(part);
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x0019A8EB File Offset: 0x00198AEB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			this._state.DefaultForceComplete(this, cancellationToken);
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06006FC7 RID: 28615
		public abstract override string Name { get; }

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06006FC8 RID: 28616
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract override MethodSymbol AddMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06006FC9 RID: 28617
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract override MethodSymbol RemoveMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x06006FCA RID: 28618
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06006FCB RID: 28619
		public abstract override TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06006FCC RID: 28620 RVA: 0x0019A8FA File Offset: 0x00198AFA
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this.containingType;
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x06006FCD RID: 28621 RVA: 0x0019A902 File Offset: 0x00198B02
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this.containingType;
			}
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x0019A90A File Offset: 0x00198B0A
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return new LexicalSortKey(this._location, this.DeclaringCompilation);
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x06006FCF RID: 28623 RVA: 0x0019A91D File Offset: 0x00198B1D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray.Create<Location>(this._location);
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x0019A92A File Offset: 0x00198B2A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this._syntaxRef);
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06006FD1 RID: 28625 RVA: 0x0019A938 File Offset: 0x00198B38
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this.containingType.AnyMemberHasAttributes)
				{
					CSharpSyntaxNode csharpSyntaxNode = this.CSharpSyntaxNode;
					if (csharpSyntaxNode != null)
					{
						SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
						if (syntaxKind == SyntaxKind.VariableDeclarator)
						{
							return ((EventFieldDeclarationSyntax)csharpSyntaxNode.Parent.Parent).AttributeLists;
						}
						if (syntaxKind == SyntaxKind.EventDeclaration)
						{
							return ((EventDeclarationSyntax)csharpSyntaxNode).AttributeLists;
						}
						throw ExceptionUtilities.UnexpectedValue(csharpSyntaxNode.Kind());
					}
				}
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06006FD2 RID: 28626 RVA: 0x0019A9AF File Offset: 0x00198BAF
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06006FD3 RID: 28627 RVA: 0x0019A9B2 File Offset: 0x00198BB2
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Event;
			}
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06006FD4 RID: 28628 RVA: 0x0019A9B6 File Offset: 0x00198BB6
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				return this.AllowedAttributeLocations;
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06006FD5 RID: 28629
		protected abstract AttributeLocation AllowedAttributeLocations { get; }

		// Token: 0x06006FD6 RID: 28630 RVA: 0x0019A9C0 File Offset: 0x00198BC0
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			if ((this._lazyCustomAttributesBag == null || !this._lazyCustomAttributesBag.IsSealed) && base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.AttributeDeclarationSyntaxList), ref this._lazyCustomAttributesBag, AttributeLocation.None, false, null, null))
			{
				this.DeclaringCompilation.SymbolDeclaredEvent(this);
				this._state.NotePartComplete(CompletionPart.Attributes);
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x0019AA1E File Offset: 0x00198C1E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x0019AA2C File Offset: 0x00198C2C
		protected CommonEventWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (CommonEventWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x0019AA60 File Offset: 0x00198C60
		internal CommonEventEarlyWellKnownAttributeData GetEarlyDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (CommonEventEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x0019AA94 File Offset: 0x00198C94
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override CSharpAttributeData EarlyDecodeWellKnownAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })] ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			CSharpAttributeData csharpAttributeData;
			ObsoleteAttributeData obsoleteAttributeData;
			if (Symbol.EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref arguments, out csharpAttributeData, out obsoleteAttributeData))
			{
				if (obsoleteAttributeData != null)
				{
					arguments.GetOrCreateData<CommonEventEarlyWellKnownAttributeData>().ObsoleteAttributeData = obsoleteAttributeData;
				}
				return csharpAttributeData;
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06006FDB RID: 28635 RVA: 0x0019AAC8 File Offset: 0x00198CC8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (!this.containingType.AnyMemberHasAttributes)
				{
					return null;
				}
				CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
				if (lazyCustomAttributesBag == null || !lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
				{
					return ObsoleteAttributeData.Uninitialized;
				}
				CommonEventEarlyWellKnownAttributeData commonEventEarlyWellKnownAttributeData = (CommonEventEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
				if (commonEventEarlyWellKnownAttributeData == null)
				{
					return null;
				}
				return commonEventEarlyWellKnownAttributeData.ObsoleteAttributeData;
			}
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x0019AB14 File Offset: 0x00198D14
		internal sealed override void DecodeWellKnownAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (attribute.IsTargetAttribute(this, AttributeDescription.SpecialNameAttribute))
			{
				arguments.GetOrCreateData<CommonEventWellKnownAttributeData>().HasSpecialNameAttribute = true;
				return;
			}
			if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute))
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.ExcludeFromCodeCoverageAttribute))
				{
					arguments.GetOrCreateData<CommonEventWellKnownAttributeData>().HasExcludeFromCodeCoverageAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.SkipLocalsInitAttribute))
				{
					CSharpAttributeData.DecodeSkipLocalsInitAttribute<CommonEventWellKnownAttributeData>(this.DeclaringCompilation, ref arguments);
				}
			}
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x0019AB94 File Offset: 0x00198D94
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations typeWithAnnotations = this.TypeWithAnnotations;
			if (typeWithAnnotations.Type.ContainsDynamic())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(typeWithAnnotations.Type, typeWithAnnotations.CustomModifiers.Length, RefKind.None));
			}
			if (typeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, typeWithAnnotations.Type));
			}
			if (typeWithAnnotations.Type.ContainsTupleNames())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, this.DeclaringCompilation.SynthesizeTupleNamesAttribute(typeWithAnnotations.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.containingType.GetNullableContextValue(), typeWithAnnotations));
			}
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06006FDE RID: 28638 RVA: 0x0019AC52 File Offset: 0x00198E52
		internal sealed override bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				CommonEventWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasExcludeFromCodeCoverageAttribute;
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06006FDF RID: 28639 RVA: 0x0019AC68 File Offset: 0x00198E68
		internal sealed override bool HasSpecialName
		{
			get
			{
				CommonEventWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSpecialNameAttribute;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06006FE0 RID: 28640 RVA: 0x0019AC87 File Offset: 0x00198E87
		public bool HasSkipLocalsInitAttribute
		{
			get
			{
				CommonEventWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSkipLocalsInitAttribute;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06006FE1 RID: 28641 RVA: 0x0019AC9A File Offset: 0x00198E9A
		public sealed override bool IsAbstract
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Abstract) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x0019ACA7 File Offset: 0x00198EA7
		public sealed override bool IsExtern
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Extern) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06006FE3 RID: 28643 RVA: 0x0019ACB8 File Offset: 0x00198EB8
		public sealed override bool IsStatic
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x0019ACC5 File Offset: 0x00198EC5
		public sealed override bool IsOverride
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Override) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x0019ACD6 File Offset: 0x00198ED6
		public sealed override bool IsSealed
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Sealed) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x0019ACE3 File Offset: 0x00198EE3
		public sealed override bool IsVirtual
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Virtual) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06006FE7 RID: 28647 RVA: 0x0019ACF4 File Offset: 0x00198EF4
		internal bool IsReadOnly
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x0019AD05 File Offset: 0x00198F05
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this._modifiers);
			}
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06006FE9 RID: 28649 RVA: 0x0019AD12 File Offset: 0x00198F12
		internal sealed override bool MustCallMethodsDirectly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x0019AD15 File Offset: 0x00198F15
		internal SyntaxReference SyntaxReference
		{
			get
			{
				return this._syntaxRef;
			}
		}

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06006FEB RID: 28651 RVA: 0x0019AD20 File Offset: 0x00198F20
		internal CSharpSyntaxNode CSharpSyntaxNode
		{
			get
			{
				return (CSharpSyntaxNode)this._syntaxRef.GetSyntax(default(CancellationToken));
			}
		}

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x0019AD46 File Offset: 0x00198F46
		internal SyntaxTree SyntaxTree
		{
			get
			{
				return this._syntaxRef.SyntaxTree;
			}
		}

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06006FED RID: 28653 RVA: 0x0019AD53 File Offset: 0x00198F53
		internal bool IsNew
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.New) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x0019AD60 File Offset: 0x00198F60
		internal DeclarationModifiers Modifiers
		{
			get
			{
				return this._modifiers;
			}
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x0019AD68 File Offset: 0x00198F68
		private void CheckAccessibility(Location location, BindingDiagnosticBag diagnostics, bool isExplicitInterfaceImplementation)
		{
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this._modifiers, this, isExplicitInterfaceImplementation);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(new CSDiagnostic(csdiagnosticInfo, location, false));
			}
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x0019AD94 File Offset: 0x00198F94
		private DeclarationModifiers MakeModifiers(SyntaxTokenList modifiers, bool explicitInterfaceImplementation, bool isFieldLike, Location location, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			bool isInterface = this.ContainingType.IsInterface;
			DeclarationModifiers declarationModifiers = ((isInterface && !explicitInterfaceImplementation) ? DeclarationModifiers.Public : DeclarationModifiers.Private);
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.None;
			DeclarationModifiers declarationModifiers3 = DeclarationModifiers.Unsafe;
			if (!explicitInterfaceImplementation)
			{
				declarationModifiers3 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.New | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Virtual;
				if (!isInterface)
				{
					declarationModifiers3 |= DeclarationModifiers.Override;
				}
				else
				{
					declarationModifiers = DeclarationModifiers.None;
					declarationModifiers3 |= DeclarationModifiers.Extern;
					declarationModifiers2 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Extern | DeclarationModifiers.Virtual;
				}
			}
			else if (isInterface)
			{
				declarationModifiers3 |= DeclarationModifiers.Abstract;
			}
			if (this.ContainingType.IsStructType())
			{
				declarationModifiers3 |= DeclarationModifiers.ReadOnly;
			}
			if (!isInterface)
			{
				declarationModifiers3 |= DeclarationModifiers.Extern;
			}
			DeclarationModifiers declarationModifiers4 = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, declarationModifiers, declarationModifiers3, location, diagnostics, out modifierErrors);
			this.CheckUnsafeModifier(declarationModifiers4, diagnostics);
			ModifierUtils.ReportDefaultInterfaceImplementationModifiers(!isFieldLike, declarationModifiers4, declarationModifiers2, location, diagnostics);
			if (isInterface)
			{
				declarationModifiers4 = ModifierUtils.AdjustModifiersForAnInterfaceMember(declarationModifiers4, !isFieldLike, explicitInterfaceImplementation);
			}
			return declarationModifiers4;
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x0019AE58 File Offset: 0x00199058
		protected void CheckModifiersAndType(BindingDiagnosticBag diagnostics)
		{
			Location location = this.Locations[0];
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			bool flag = this.ContainingType.IsInterface && this.IsExplicitInterfaceImplementation;
			if (this.DeclaredAccessibility == Accessibility.Private && (this.IsVirtual || (this.IsAbstract && !flag) || this.IsOverride))
			{
				diagnostics.Add(ErrorCode.ERR_VirtualPrivate, location, new object[] { this });
			}
			else if (this.IsStatic && (this.IsOverride || this.IsVirtual || this.IsAbstract))
			{
				diagnostics.Add(ErrorCode.ERR_StaticNotVirtual, location, new object[] { this });
			}
			else if (this.IsReadOnly && this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_StaticMemberCantBeReadOnly, location, new object[] { this });
			}
			else if (this.IsReadOnly && base.HasAssociatedField)
			{
				diagnostics.Add(ErrorCode.ERR_FieldLikeEventCantBeReadOnly, location, new object[] { this });
			}
			else if (this.IsOverride && (this.IsNew || this.IsVirtual))
			{
				diagnostics.Add(ErrorCode.ERR_OverrideNotNew, location, new object[] { this });
			}
			else if (this.IsSealed && !this.IsOverride && (!flag || !this.IsAbstract))
			{
				diagnostics.Add(ErrorCode.ERR_SealedNonOverride, location, new object[] { this });
			}
			else if (this.IsAbstract && this.ContainingType.TypeKind == TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_BadMemberFlag, location, new object[] { SyntaxFacts.GetText(SyntaxKind.AbstractKeyword) });
			}
			else if (this.IsVirtual && this.ContainingType.TypeKind == TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_BadMemberFlag, location, new object[] { SyntaxFacts.GetText(SyntaxKind.VirtualKeyword) });
			}
			else if (this.IsAbstract && this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractAndExtern, location, new object[] { this });
			}
			else if (this.IsAbstract && this.IsSealed && !flag)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractAndSealed, location, new object[] { this });
			}
			else if (this.IsAbstract && this.IsVirtual)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractNotVirtual, location, new object[]
				{
					this.Kind.Localize(),
					this
				});
			}
			else if (this.ContainingType.IsSealed && this.DeclaredAccessibility.HasProtected() && !this.IsOverride)
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), location, new object[] { this });
			}
			else if (this.ContainingType.IsStatic && !this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_InstanceMemberInStaticClass, location, new object[] { this.Name });
			}
			else if (!base.Type.IsVoidType())
			{
				if (!this.IsNoMoreVisibleThan(base.Type, ref compoundUseSiteInfo))
				{
					EventDeclarationSyntax eventDeclarationSyntax = this.CSharpSyntaxNode as EventDeclarationSyntax;
					if (((eventDeclarationSyntax != null) ? eventDeclarationSyntax.ExplicitInterfaceSpecifier : null) == null)
					{
						diagnostics.Add(ErrorCode.ERR_BadVisEventType, location, new object[] { this, base.Type });
						goto IL_040E;
					}
				}
				if (!base.Type.IsDelegateType() && !base.Type.IsErrorType())
				{
					diagnostics.Add(ErrorCode.ERR_EventNotDelegate, location, new object[] { this });
				}
				else if (this.IsAbstract && !this.ContainingType.IsAbstract && (this.ContainingType.TypeKind == TypeKind.Class || this.ContainingType.TypeKind == TypeKind.Submission))
				{
					diagnostics.Add(ErrorCode.ERR_AbstractInConcreteClass, location, new object[] { this, this.ContainingType });
				}
				else if (this.IsVirtual && this.ContainingType.IsSealed)
				{
					diagnostics.Add(ErrorCode.ERR_NewVirtualInSealed, location, new object[] { this, this.ContainingType });
				}
			}
			IL_040E:
			diagnostics.Add(location, compoundUseSiteInfo);
		}

		// Token: 0x06006FF2 RID: 28658 RVA: 0x0019B27C File Offset: 0x0019947C
		public override string GetDocumentationCommentXml([global::System.Runtime.CompilerServices.Nullable(2)] CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this._lazyExpandedDocComment : ref this._lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this, expandIncludes, ref ptr);
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x0019B2A4 File Offset: 0x001994A4
		protected static void CopyEventCustomModifiers(EventSymbol eventWithCustomModifiers, ref TypeWithAnnotations type, AssemblySymbol containingAssembly)
		{
			TypeSymbol type2 = eventWithCustomModifiers.Type;
			if (type.Type.Equals(type2, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				type = type.WithTypeAndModifiers(CustomModifierUtils.CopyTypeCustomModifiers(type2, type.Type, containingAssembly), eventWithCustomModifiers.TypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x0019B2EF File Offset: 0x001994EF
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

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06006FF5 RID: 28661 RVA: 0x0019B312 File Offset: 0x00199512
		public sealed override bool IsWindowsRuntimeEvent
		{
			get
			{
				if (!this._lazyIsWindowsRuntimeEvent.HasValue())
				{
					this._lazyIsWindowsRuntimeEvent = this.ComputeIsWindowsRuntimeEvent().ToThreeState();
				}
				return this._lazyIsWindowsRuntimeEvent.Value();
			}
		}

		// Token: 0x06006FF6 RID: 28662 RVA: 0x0019B340 File Offset: 0x00199540
		private bool ComputeIsWindowsRuntimeEvent()
		{
			ImmutableArray<EventSymbol> explicitInterfaceImplementations = this.ExplicitInterfaceImplementations;
			if (!explicitInterfaceImplementations.IsEmpty)
			{
				return explicitInterfaceImplementations[0].IsWindowsRuntimeEvent;
			}
			if (this.containingType.IsInterfaceType())
			{
				return this.IsCompilationOutputWinMdObj();
			}
			EventSymbol overriddenEvent = base.OverriddenEvent;
			if (overriddenEvent != null)
			{
				return overriddenEvent.IsWindowsRuntimeEvent;
			}
			bool flag = false;
			foreach (NamedTypeSymbol namedTypeSymbol in this.containingType.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.Keys)
			{
				foreach (Symbol symbol in namedTypeSymbol.GetMembers(this.Name))
				{
					if (symbol.Kind == SymbolKind.Event && symbol.IsImplementableInterfaceMember() && this == this.containingType.FindImplementationForInterfaceMemberInNonInterface(symbol, true))
					{
						flag = true;
						if (((EventSymbol)symbol).IsWindowsRuntimeEvent)
						{
							return true;
						}
					}
				}
			}
			return !flag && this.IsCompilationOutputWinMdObj();
		}

		// Token: 0x06006FF7 RID: 28663 RVA: 0x0019B450 File Offset: 0x00199650
		internal static string GetAccessorName(string eventName, bool isAdder)
		{
			return (isAdder ? "add_" : "remove_") + eventName;
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x0019B467 File Offset: 0x00199667
		protected TypeWithAnnotations BindEventType(Binder binder, TypeSyntax typeSyntax, BindingDiagnosticBag diagnostics)
		{
			binder = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks | BinderFlags.SuppressUnsafeDiagnostics, this);
			return binder.BindType(typeSyntax, diagnostics, null, false);
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x0019B484 File Offset: 0x00199684
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location location = this.Locations[0];
			this.CheckModifiersAndType(diagnostics);
			base.Type.CheckAllConstraints(declaringCompilation, conversions, location, diagnostics);
			if (base.Type.ContainsNativeInteger())
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, location, true);
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && this.TypeWithAnnotations.NeedsNullableAttribute())
			{
				declaringCompilation.EnsureNullableAttributeExists(diagnostics, location, true);
			}
		}

		// Token: 0x04001B62 RID: 7010
		private readonly Location _location;

		// Token: 0x04001B63 RID: 7011
		private readonly SyntaxReference _syntaxRef;

		// Token: 0x04001B64 RID: 7012
		private readonly DeclarationModifiers _modifiers;

		// Token: 0x04001B65 RID: 7013
		internal readonly SourceMemberContainerTypeSymbol containingType;

		// Token: 0x04001B66 RID: 7014
		private SymbolCompletionState _state;

		// Token: 0x04001B67 RID: 7015
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001B68 RID: 7016
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private string _lazyDocComment;

		// Token: 0x04001B69 RID: 7017
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private string _lazyExpandedDocComment;

		// Token: 0x04001B6A RID: 7018
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;

		// Token: 0x04001B6B RID: 7019
		private ThreeState _lazyIsWindowsRuntimeEvent;
	}
}
