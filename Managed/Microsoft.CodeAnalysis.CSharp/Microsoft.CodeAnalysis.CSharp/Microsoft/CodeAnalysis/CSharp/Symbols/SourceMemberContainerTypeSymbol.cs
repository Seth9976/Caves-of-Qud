using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053D RID: 1341
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SourceMemberContainerTypeSymbol : NamedTypeSymbol
	{
		// Token: 0x06007066 RID: 28774 RVA: 0x0019C644 File Offset: 0x0019A844
		internal SourceMemberContainerTypeSymbol(NamespaceOrTypeSymbol containingSymbol, MergedTypeDeclaration declaration, BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this._containingSymbol = containingSymbol;
			this.declaration = declaration;
			TypeKind typeKind = declaration.Kind.ToTypeKind();
			DeclarationModifiers declarationModifiers = this.MakeModifiers(typeKind, diagnostics);
			foreach (SingleTypeDeclaration singleTypeDeclaration in declaration.Declarations)
			{
				diagnostics.AddRange<Diagnostic>(singleTypeDeclaration.Diagnostics);
			}
			int num = (int)(declarationModifiers & DeclarationModifiers.AccessibilityMask);
			if ((num & (num - 1)) != 0)
			{
				if ((declarationModifiers & DeclarationModifiers.Partial) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_PartialModifierConflict, this.Locations[0], new object[] { this });
				}
				num &= ~(num - 1);
				declarationModifiers &= ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected);
				declarationModifiers |= (DeclarationModifiers)num;
			}
			this._declModifiers = declarationModifiers;
			SpecialType specialType = ((num == 16) ? this.MakeSpecialType() : SpecialType.None);
			this._flags = new SourceMemberContainerTypeSymbol.Flags(specialType, typeKind);
			NamedTypeSymbol containingType = this.ContainingType;
			if (containingType != null && containingType.IsSealed && this.DeclaredAccessibility.HasProtected())
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), this.Locations[0], new object[] { this });
			}
			this.state.NotePartComplete(CompletionPart.TypeArguments);
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x0019C798 File Offset: 0x0019A998
		private SpecialType MakeSpecialType()
		{
			if (this.ContainingSymbol.Kind == SymbolKind.Namespace && this.ContainingSymbol.ContainingAssembly.KeepLookingForDeclaredSpecialTypes)
			{
				return SpecialTypes.GetTypeFromMetadataName(MetadataHelpers.BuildQualifiedName(this.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), this.MetadataName));
			}
			return SpecialType.None;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x0019C7E8 File Offset: 0x0019A9E8
		private DeclarationModifiers MakeModifiers(TypeKind typeKind, BindingDiagnosticBag diagnostics)
		{
			Symbol containingSymbol = this.ContainingSymbol;
			DeclarationModifiers declarationModifiers = DeclarationModifiers.AccessibilityMask;
			DeclarationModifiers declarationModifiers2;
			if (containingSymbol.Kind == SymbolKind.Namespace)
			{
				declarationModifiers2 = DeclarationModifiers.Internal;
			}
			else
			{
				declarationModifiers |= DeclarationModifiers.New;
				if (((NamedTypeSymbol)containingSymbol).IsInterface)
				{
					declarationModifiers2 = DeclarationModifiers.Public;
				}
				else
				{
					declarationModifiers2 = DeclarationModifiers.Private;
				}
			}
			if (typeKind <= TypeKind.Delegate)
			{
				if (typeKind != TypeKind.Class)
				{
					if (typeKind != TypeKind.Delegate)
					{
						goto IL_0096;
					}
					declarationModifiers |= DeclarationModifiers.Unsafe;
					goto IL_0096;
				}
			}
			else
			{
				if (typeKind == TypeKind.Interface)
				{
					declarationModifiers |= DeclarationModifiers.Partial | DeclarationModifiers.Unsafe;
					goto IL_0096;
				}
				if (typeKind != TypeKind.Struct)
				{
					if (typeKind != TypeKind.Submission)
					{
						goto IL_0096;
					}
				}
				else
				{
					declarationModifiers |= DeclarationModifiers.ReadOnly | DeclarationModifiers.Partial | DeclarationModifiers.Unsafe;
					if (!this.IsRecordStruct)
					{
						declarationModifiers |= DeclarationModifiers.Ref;
						goto IL_0096;
					}
					goto IL_0096;
				}
			}
			declarationModifiers |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.Partial | DeclarationModifiers.Unsafe;
			if (!this.IsRecord)
			{
				declarationModifiers |= DeclarationModifiers.Static;
			}
			IL_0096:
			bool flag;
			DeclarationModifiers declarationModifiers3 = this.MakeAndCheckTypeModifiers(declarationModifiers2, declarationModifiers, diagnostics, out flag);
			this.CheckUnsafeModifier(declarationModifiers3, diagnostics);
			if (!flag && (declarationModifiers3 & DeclarationModifiers.Abstract) != DeclarationModifiers.None && (declarationModifiers3 & (DeclarationModifiers.Sealed | DeclarationModifiers.Static)) != DeclarationModifiers.None)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractSealedStatic, this.Locations[0], new object[] { this });
			}
			if (!flag && (declarationModifiers3 & (DeclarationModifiers.Sealed | DeclarationModifiers.Static)) == (DeclarationModifiers.Sealed | DeclarationModifiers.Static))
			{
				diagnostics.Add(ErrorCode.ERR_SealedStaticClass, this.Locations[0], new object[] { this });
			}
			switch (typeKind)
			{
			case TypeKind.Delegate:
				return declarationModifiers3 | DeclarationModifiers.Sealed;
			case TypeKind.Dynamic:
			case TypeKind.Error:
				return declarationModifiers3;
			case TypeKind.Enum:
				break;
			case TypeKind.Interface:
				return declarationModifiers3 | DeclarationModifiers.Abstract;
			default:
				if (typeKind != TypeKind.Struct)
				{
					return declarationModifiers3;
				}
				break;
			}
			declarationModifiers3 |= DeclarationModifiers.Sealed;
			return declarationModifiers3;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x0019C944 File Offset: 0x0019AB44
		private DeclarationModifiers MakeAndCheckTypeModifiers(DeclarationModifiers defaultAccess, DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			modifierErrors = false;
			DeclarationModifiers declarationModifiers = DeclarationModifiers.Unset;
			int length = this.declaration.Declarations.Length;
			bool flag = false;
			for (int i = 0; i < length; i++)
			{
				DeclarationModifiers declarationModifiers2 = this.declaration.Declarations[i].Modifiers;
				if (length > 1 && (declarationModifiers2 & DeclarationModifiers.Partial) == DeclarationModifiers.None)
				{
					flag = true;
				}
				if (!modifierErrors)
				{
					declarationModifiers2 = ModifierUtils.CheckModifiers(declarationModifiers2, allowedModifiers, this.declaration.Declarations[i].NameLocation, diagnostics, default(SyntaxTokenList?), out modifierErrors);
					if (!modifierErrors)
					{
						CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(declarationModifiers2, this, false);
						if (csdiagnosticInfo != null)
						{
							diagnostics.Add(csdiagnosticInfo, this.Locations[0]);
							modifierErrors = true;
						}
					}
				}
				if (declarationModifiers == DeclarationModifiers.Unset)
				{
					declarationModifiers = declarationModifiers2;
				}
				else
				{
					declarationModifiers |= declarationModifiers2;
				}
			}
			if ((declarationModifiers & DeclarationModifiers.AccessibilityMask) == DeclarationModifiers.None)
			{
				declarationModifiers |= defaultAccess;
			}
			if (flag)
			{
				if ((declarationModifiers & DeclarationModifiers.Partial) == DeclarationModifiers.None)
				{
					SymbolKind kind = this.ContainingSymbol.Kind;
					if (kind != SymbolKind.NamedType)
					{
						if (kind == SymbolKind.Namespace)
						{
							for (int j = 1; j < length; j++)
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateNameInNS, this.declaration.Declarations[j].NameLocation, new object[] { this.Name, this.ContainingSymbol });
								modifierErrors = true;
							}
						}
					}
					else
					{
						for (int k = 1; k < length; k++)
						{
							if (this.ContainingType.Locations.Length == 1 || this.ContainingType.IsPartial())
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, this.declaration.Declarations[k].NameLocation, new object[] { this.ContainingSymbol, this.Name });
							}
							modifierErrors = true;
						}
					}
				}
				else
				{
					for (int l = 0; l < length; l++)
					{
						SingleTypeDeclaration singleTypeDeclaration = this.declaration.Declarations[l];
						if ((singleTypeDeclaration.Modifiers & DeclarationModifiers.Partial) == DeclarationModifiers.None)
						{
							diagnostics.Add(ErrorCode.ERR_MissingPartial, singleTypeDeclaration.NameLocation, new object[] { this.Name });
							modifierErrors = true;
						}
					}
				}
			}
			if (this.Name == SyntaxFacts.GetText(SyntaxKind.RecordKeyword))
			{
				foreach (SyntaxReference syntaxReference in this.SyntaxReferences)
				{
					SyntaxNode syntax = syntaxReference.GetSyntax(default(CancellationToken));
					BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = syntax as BaseTypeDeclarationSyntax;
					SyntaxToken? syntaxToken;
					if (baseTypeDeclarationSyntax == null)
					{
						DelegateDeclarationSyntax delegateDeclarationSyntax = syntax as DelegateDeclarationSyntax;
						if (delegateDeclarationSyntax == null)
						{
							syntaxToken = default(SyntaxToken?);
						}
						else
						{
							syntaxToken = new SyntaxToken?(delegateDeclarationSyntax.Identifier);
						}
					}
					else
					{
						syntaxToken = new SyntaxToken?(baseTypeDeclarationSyntax.Identifier);
					}
					SyntaxToken? syntaxToken2 = syntaxToken;
					SourceMemberContainerTypeSymbol.ReportTypeNamedRecord((syntaxToken2 != null) ? syntaxToken2.GetValueOrDefault().Text : null, this.DeclaringCompilation, diagnostics.DiagnosticBag, ((syntaxToken2 != null) ? syntaxToken2.GetValueOrDefault().GetLocation() : null) ?? Location.None);
				}
			}
			return declarationModifiers;
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x0019CC7E File Offset: 0x0019AE7E
		internal static void ReportTypeNamedRecord([global::System.Runtime.CompilerServices.Nullable(2)] string name, CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticBag diagnostics, Location location)
		{
			if (diagnostics != null && name == SyntaxFacts.GetText(SyntaxKind.RecordKeyword) && compilation.LanguageVersion >= MessageID.IDS_FeatureRecords.RequiredVersion())
			{
				diagnostics.Add(ErrorCode.WRN_RecordNamedDisallowed, location, new object[] { name });
			}
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x0600706B RID: 28779 RVA: 0x0019CCBE File Offset: 0x0019AEBE
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x0019CCC1 File Offset: 0x0019AEC1
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this.state.HasComplete(part);
		}

		// Token: 0x0600706D RID: 28781
		protected abstract void CheckBase(BindingDiagnosticBag diagnostics);

		// Token: 0x0600706E RID: 28782
		protected abstract void CheckInterfaces(BindingDiagnosticBag diagnostics);

		// Token: 0x0600706F RID: 28783 RVA: 0x0019CCD0 File Offset: 0x0019AED0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this.state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.EnumUnderlyingType)
				{
					if (nextIncompletePart <= CompletionPart.StartBaseType)
					{
						if (nextIncompletePart == CompletionPart.None)
						{
							return;
						}
						if (nextIncompletePart == CompletionPart.Attributes)
						{
							this.GetAttributes();
							goto IL_030D;
						}
						if (nextIncompletePart != CompletionPart.StartBaseType)
						{
							goto IL_02FF;
						}
					}
					else
					{
						if (nextIncompletePart <= CompletionPart.StartInterfaces)
						{
							if (nextIncompletePart == CompletionPart.FinishBaseType)
							{
								goto IL_00FF;
							}
							if (nextIncompletePart != CompletionPart.StartInterfaces)
							{
								goto IL_02FF;
							}
						}
						else if (nextIncompletePart != CompletionPart.FinishInterfaces)
						{
							if (nextIncompletePart != CompletionPart.EnumUnderlyingType)
							{
								goto IL_02FF;
							}
							NamedTypeSymbol enumUnderlyingType = this.EnumUnderlyingType;
							goto IL_030D;
						}
						if (this.state.NotePartComplete(CompletionPart.StartInterfaces))
						{
							BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
							this.CheckInterfaces(instance);
							this.AddDeclarationDiagnostics(instance);
							this.state.NotePartComplete(CompletionPart.FinishInterfaces);
							instance.Free();
							goto IL_030D;
						}
						goto IL_030D;
					}
					IL_00FF:
					if (this.state.NotePartComplete(CompletionPart.StartBaseType))
					{
						BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
						this.CheckBase(instance2);
						this.AddDeclarationDiagnostics(instance2);
						this.state.NotePartComplete(CompletionPart.FinishBaseType);
						instance2.Free();
					}
				}
				else if (nextIncompletePart <= CompletionPart.TypeMembers)
				{
					if (nextIncompletePart <= CompletionPart.TypeParameters)
					{
						if (nextIncompletePart != CompletionPart.TypeArguments)
						{
							if (nextIncompletePart != CompletionPart.TypeParameters)
							{
								goto IL_02FF;
							}
							foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
							{
								typeParameterSymbol.ForceComplete(locationOpt, cancellationToken);
							}
							this.state.NotePartComplete(CompletionPart.TypeParameters);
						}
						else
						{
							ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
						}
					}
					else if (nextIncompletePart != CompletionPart.Members)
					{
						if (nextIncompletePart != CompletionPart.TypeMembers)
						{
							goto IL_02FF;
						}
						this.GetTypeMembersUnordered();
					}
					else
					{
						this.GetMembersByName();
					}
				}
				else
				{
					if (nextIncompletePart <= CompletionPart.StartMemberChecks)
					{
						if (nextIncompletePart == CompletionPart.SynthesizedExplicitImplementations)
						{
							this.GetSynthesizedExplicitImplementations(cancellationToken);
							goto IL_030D;
						}
						if (nextIncompletePart != CompletionPart.StartMemberChecks)
						{
							goto IL_02FF;
						}
					}
					else if (nextIncompletePart != CompletionPart.FinishMemberChecks)
					{
						if (nextIncompletePart != CompletionPart.MembersCompleted)
						{
							goto IL_02FF;
						}
						ImmutableArray<Symbol> membersUnordered = this.GetMembersUnordered();
						bool flag = true;
						if (locationOpt == null)
						{
							foreach (Symbol symbol in membersUnordered)
							{
								cancellationToken.ThrowIfCancellationRequested();
								symbol.ForceComplete(locationOpt, cancellationToken);
							}
						}
						else
						{
							foreach (Symbol symbol2 in membersUnordered)
							{
								Symbol.ForceCompleteMemberByLocation(locationOpt, symbol2, cancellationToken);
								flag = flag && symbol2.HasComplete(CompletionPart.All);
							}
						}
						if (!flag)
						{
							break;
						}
						this.EnsureFieldDefinitionsNoted();
						this.state.NotePartComplete(CompletionPart.MembersCompleted);
						goto IL_030D;
					}
					if (this.state.NotePartComplete(CompletionPart.StartMemberChecks))
					{
						BindingDiagnosticBag instance3 = BindingDiagnosticBag.GetInstance();
						this.AfterMembersChecks(instance3);
						this.AddDeclarationDiagnostics(instance3);
						this.DeclaringCompilation.SymbolDeclaredEvent(this);
						this.state.NotePartComplete(CompletionPart.FinishMemberChecks);
						instance3.Free();
					}
				}
				IL_030D:
				this.state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_02FF:
				this.state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type);
				goto IL_030D;
			}
			CompletionPart completionPart = CompletionPart.NamedTypeSymbolWithLocationAll;
			this.state.SpinWaitComplete(completionPart, cancellationToken);
		}

		// Token: 0x06007070 RID: 28784 RVA: 0x0019CFFB File Offset: 0x0019B1FB
		internal void EnsureFieldDefinitionsNoted()
		{
			if (this._flags.FieldDefinitionsNoted)
			{
				return;
			}
			this.NoteFieldDefinitions();
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x0019D014 File Offset: 0x0019B214
		private void NoteFieldDefinitions()
		{
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this.GetMembersAndInitializers();
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers2 = membersAndInitializers;
			lock (membersAndInitializers2)
			{
				if (!this._flags.FieldDefinitionsNoted)
				{
					SourceAssemblySymbol sourceAssemblySymbol = (SourceAssemblySymbol)this.ContainingAssembly;
					Accessibility accessibility = this.EffectiveAccessibility();
					ImmutableArray<Symbol>.Enumerator enumerator = membersAndInitializers.NonTypeMembers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						FieldSymbol fieldSymbol;
						if (enumerator.Current.IsFieldOrFieldLikeEvent(out fieldSymbol) && !fieldSymbol.IsConst && !fieldSymbol.IsFixedSizeBuffer)
						{
							Accessibility declaredAccessibility = fieldSymbol.DeclaredAccessibility;
							if (declaredAccessibility == Accessibility.Private)
							{
								sourceAssemblySymbol.NoteFieldDefinition(fieldSymbol, false, true);
							}
							else if (accessibility == Accessibility.Private)
							{
								sourceAssemblySymbol.NoteFieldDefinition(fieldSymbol, false, false);
							}
							else if (declaredAccessibility == Accessibility.Internal || accessibility == Accessibility.Internal)
							{
								sourceAssemblySymbol.NoteFieldDefinition(fieldSymbol, true, false);
							}
						}
					}
					this._flags.SetFieldDefinitionsNoted();
				}
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06007072 RID: 28786 RVA: 0x0019D0FC File Offset: 0x0019B2FC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override NamedTypeSymbol ContainingType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._containingSymbol as NamedTypeSymbol;
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06007073 RID: 28787 RVA: 0x0019D109 File Offset: 0x0019B309
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06007074 RID: 28788 RVA: 0x0019D111 File Offset: 0x0019B311
		public override SpecialType SpecialType
		{
			get
			{
				return this._flags.SpecialType;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06007075 RID: 28789 RVA: 0x0019D11E File Offset: 0x0019B31E
		public override TypeKind TypeKind
		{
			get
			{
				return this._flags.TypeKind;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06007076 RID: 28790 RVA: 0x0019D12B File Offset: 0x0019B32B
		internal MergedTypeDeclaration MergedDeclaration
		{
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06007077 RID: 28791 RVA: 0x0019D133 File Offset: 0x0019B333
		internal sealed override bool IsInterface
		{
			get
			{
				return this.TypeKind == TypeKind.Interface;
			}
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x0019D140 File Offset: 0x0019B340
		internal override ManagedKind GetManagedKind([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ManagedKind managedKind = this._flags.ManagedKind;
			if (managedKind == ManagedKind.Unknown)
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(this.ContainingAssembly);
				managedKind = base.GetManagedKind(ref compoundUseSiteInfo);
				IReadOnlyCollection<DiagnosticInfo> diagnostics = compoundUseSiteInfo.Diagnostics;
				ImmutableInterlocked.InterlockedInitialize<DiagnosticInfo>(ref this._managedKindUseSiteDiagnostics, (diagnostics != null) ? diagnostics.ToImmutableArray<DiagnosticInfo>() : ImmutableArray<DiagnosticInfo>.Empty);
				IReadOnlyCollection<AssemblySymbol> dependencies = compoundUseSiteInfo.Dependencies;
				ImmutableInterlocked.InterlockedInitialize<AssemblySymbol>(ref this._managedKindUseSiteDependencies, (dependencies != null) ? dependencies.ToImmutableArray<AssemblySymbol>() : ImmutableArray<AssemblySymbol>.Empty);
				this._flags.SetManagedKind(managedKind);
			}
			if (useSiteInfo.AccumulatesDiagnostics)
			{
				ImmutableArray<DiagnosticInfo> immutableArray = this._managedKindUseSiteDiagnostics;
				immutableArray = ImmutableInterlocked.InterlockedCompareExchange<DiagnosticInfo>(ref this._managedKindUseSiteDiagnostics, immutableArray, immutableArray);
				useSiteInfo.AddDiagnostics(immutableArray);
			}
			if (useSiteInfo.AccumulatesDependencies)
			{
				ImmutableArray<AssemblySymbol> immutableArray2 = this._managedKindUseSiteDependencies;
				immutableArray2 = ImmutableInterlocked.InterlockedCompareExchange<AssemblySymbol>(ref this._managedKindUseSiteDependencies, immutableArray2, immutableArray2);
				useSiteInfo.AddDependencies(immutableArray2);
			}
			return managedKind;
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x06007079 RID: 28793 RVA: 0x0019D20D File Offset: 0x0019B40D
		public override bool IsStatic
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.Static);
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x0600707A RID: 28794 RVA: 0x0019D216 File Offset: 0x0019B416
		public sealed override bool IsRefLikeType
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.Ref);
			}
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x0600707B RID: 28795 RVA: 0x0019D223 File Offset: 0x0019B423
		public override bool IsReadOnly
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.ReadOnly);
			}
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x0600707C RID: 28796 RVA: 0x0019D230 File Offset: 0x0019B430
		public override bool IsSealed
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.Sealed);
			}
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x0600707D RID: 28797 RVA: 0x0019D239 File Offset: 0x0019B439
		public override bool IsAbstract
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.Abstract);
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x0600707E RID: 28798 RVA: 0x0019D242 File Offset: 0x0019B442
		internal bool IsPartial
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.Partial);
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x0600707F RID: 28799 RVA: 0x0019D24F File Offset: 0x0019B44F
		internal bool IsNew
		{
			get
			{
				return this.HasFlag(DeclarationModifiers.New);
			}
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x0019D258 File Offset: 0x0019B458
		[MethodImpl(256)]
		private bool HasFlag(DeclarationModifiers flag)
		{
			return (this._declModifiers & flag) > DeclarationModifiers.None;
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06007081 RID: 28801 RVA: 0x0019D265 File Offset: 0x0019B465
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this._declModifiers);
			}
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x0019D274 File Offset: 0x0019B474
		private Accessibility EffectiveAccessibility()
		{
			Accessibility accessibility = this.DeclaredAccessibility;
			if (accessibility == Accessibility.Private)
			{
				return Accessibility.Private;
			}
			for (Symbol symbol = this.ContainingType; symbol != null; symbol = symbol.ContainingType)
			{
				Accessibility declaredAccessibility = symbol.DeclaredAccessibility;
				if (declaredAccessibility == Accessibility.Private)
				{
					return Accessibility.Private;
				}
				if (declaredAccessibility == Accessibility.Internal)
				{
					accessibility = Accessibility.Internal;
				}
			}
			return accessibility;
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06007083 RID: 28803 RVA: 0x0019D2B8 File Offset: 0x0019B4B8
		public override bool IsScriptClass
		{
			get
			{
				DeclarationKind kind = this.declaration.Declarations[0].Kind;
				return kind == DeclarationKind.Script || kind == DeclarationKind.Submission;
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06007084 RID: 28804 RVA: 0x0019D2EC File Offset: 0x0019B4EC
		public override bool IsImplicitClass
		{
			get
			{
				return this.declaration.Declarations[0].Kind == DeclarationKind.ImplicitClass;
			}
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x06007085 RID: 28805 RVA: 0x0019D318 File Offset: 0x0019B518
		internal override bool IsRecord
		{
			get
			{
				return this.declaration.Declarations[0].Kind == DeclarationKind.Record;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06007086 RID: 28806 RVA: 0x0019D344 File Offset: 0x0019B544
		internal override bool IsRecordStruct
		{
			get
			{
				return this.declaration.Declarations[0].Kind == DeclarationKind.RecordStruct;
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x06007087 RID: 28807 RVA: 0x0019D36E File Offset: 0x0019B56E
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this.IsImplicitClass || this.IsScriptClass;
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x06007088 RID: 28808 RVA: 0x0019D380 File Offset: 0x0019B580
		public override int Arity
		{
			get
			{
				return this.declaration.Arity;
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x06007089 RID: 28809 RVA: 0x0019D38D File Offset: 0x0019B58D
		public override string Name
		{
			get
			{
				return this.declaration.Name;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x0600708A RID: 28810 RVA: 0x0019D39A File Offset: 0x0019B59A
		internal override bool MangleName
		{
			get
			{
				return this.Arity > 0;
			}
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x0019D3A5 File Offset: 0x0019B5A5
		internal override LexicalSortKey GetLexicalSortKey()
		{
			if (!this._lazyLexicalSortKey.IsInitialized)
			{
				this._lazyLexicalSortKey.SetFrom(this.declaration.GetLexicalSortKey(this.DeclaringCompilation));
			}
			return this._lazyLexicalSortKey;
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x0600708C RID: 28812 RVA: 0x0019D3D6 File Offset: 0x0019B5D6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.declaration.NameLocations.Cast<SourceLocation, Location>();
			}
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x0600708D RID: 28813 RVA: 0x0019D3E8 File Offset: 0x0019B5E8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<SyntaxReference> SyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.declaration.SyntaxReferences;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x0600708E RID: 28814 RVA: 0x0019D3F5 File Offset: 0x0019B5F5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.SyntaxReferences;
			}
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x0019D400 File Offset: 0x0019B600
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken)
		{
			ImmutableArray<SingleTypeDeclaration> declarations = this.declaration.Declarations;
			if (this.IsImplicitlyDeclared && declarations.IsEmpty)
			{
				return this.ContainingSymbol.IsDefinedInSourceTree(tree, definedWithinSpan, cancellationToken);
			}
			foreach (SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration in declarations)
			{
				cancellationToken.ThrowIfCancellationRequested();
				SyntaxReference syntaxReference = singleNamespaceOrTypeDeclaration.SyntaxReference;
				if (syntaxReference.SyntaxTree == tree && (definedWithinSpan == null || syntaxReference.Span.IntersectsWith(definedWithinSpan.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06007090 RID: 28816 RVA: 0x0019D48C File Offset: 0x0019B68C
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> StaticInitializers
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this.GetMembersAndInitializers().StaticInitializers;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x06007091 RID: 28817 RVA: 0x0019D499 File Offset: 0x0019B699
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> InstanceInitializers
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this.GetMembersAndInitializers().InstanceInitializers;
			}
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x0019D4A8 File Offset: 0x0019B6A8
		internal int CalculateSyntaxOffsetInSynthesizedConstructor(int position, SyntaxTree tree, bool isStatic)
		{
			if (this.IsScriptClass && !isStatic)
			{
				int num = 0;
				foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
				{
					SyntaxReference syntaxReference = singleTypeDeclaration.SyntaxReference;
					if (tree == syntaxReference.SyntaxTree)
					{
						return num + position;
					}
					num += syntaxReference.Span.Length;
				}
				throw ExceptionUtilities.Unreachable;
			}
			int num2;
			if (this.TryCalculateSyntaxOffsetOfPositionInInitializer(position, tree, isStatic, 0, out num2))
			{
				return num2;
			}
			if (this.declaration.Declarations.Length >= 1 && position == this.declaration.Declarations[0].Location.SourceSpan.Start)
			{
				return 0;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x0019D570 File Offset: 0x0019B770
		internal bool TryCalculateSyntaxOffsetOfPositionInInitializer(int position, SyntaxTree tree, bool isStatic, int ctorInitializerLength, out int syntaxOffset)
		{
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this.GetMembersAndInitializers();
			ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> immutableArray = (isStatic ? membersAndInitializers.StaticInitializers : membersAndInitializers.InstanceInitializers);
			FieldOrPropertyInitializer fieldOrPropertyInitializer;
			int num;
			if (!SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__findInitializer|95_0(immutableArray, position, tree, out fieldOrPropertyInitializer, out num))
			{
				syntaxOffset = 0;
				return false;
			}
			int num2 = SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getInitializersLength|95_3(immutableArray);
			int num3 = position - fieldOrPropertyInitializer.Syntax.Span.Start;
			int num4 = num2 + ctorInitializerLength - (num + num3);
			syntaxOffset = -num4;
			return true;
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x0019D5DC File Offset: 0x0019B7DC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static int IndexOfInitializerContainingPosition(ImmutableArray<FieldOrPropertyInitializer> initializers, int position)
		{
			int num = initializers.BinarySearch(position, (FieldOrPropertyInitializer initializer, int pos) => initializer.Syntax.Span.Start.CompareTo(pos));
			if (num >= 0)
			{
				return num;
			}
			int num2 = ~num - 1;
			if (num2 >= 0 && initializers[num2].Syntax.Span.Contains(position))
			{
				return num2;
			}
			return -1;
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x06007095 RID: 28821 RVA: 0x0019D640 File Offset: 0x0019B840
		public override IEnumerable<string> MemberNames
		{
			get
			{
				if (!this.IsTupleType && !this.IsRecord && !this.IsRecordStruct)
				{
					return this.declaration.MemberNames;
				}
				return from m in this.GetMembers()
					select m.Name;
			}
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x0019D69D File Offset: 0x0019B89D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.GetTypeMembersDictionary().Flatten<string, NamedTypeSymbol>(null);
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x0019D6AB File Offset: 0x0019B8AB
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return this.GetTypeMembersDictionary().Flatten<string, NamedTypeSymbol>(LexicalOrderSymbolComparer.Instance);
		}

		// Token: 0x06007098 RID: 28824 RVA: 0x0019D6C0 File Offset: 0x0019B8C0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (this.GetTypeMembersDictionary().TryGetValue(name, ref immutableArray))
			{
				return immutableArray;
			}
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007099 RID: 28825 RVA: 0x0019D6E4 File Offset: 0x0019B8E4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.GetTypeMembers(name).WhereAsArray((NamedTypeSymbol t, int arity) => t.Arity == arity, arity);
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x0019D714 File Offset: 0x0019B914
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<NamedTypeSymbol>> GetTypeMembersDictionary()
		{
			if (this._lazyTypeMembers == null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<NamedTypeSymbol>>>(ref this._lazyTypeMembers, this.MakeTypeMembers(instance), null) == null)
				{
					this.AddDeclarationDiagnostics(instance);
					this.state.NotePartComplete(CompletionPart.TypeMembers);
				}
				instance.Free();
			}
			return this._lazyTypeMembers;
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x0019D768 File Offset: 0x0019B968
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<NamedTypeSymbol>> MakeTypeMembers(BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			Dictionary<ValueTuple<string, int>, SourceNamedTypeSymbol> dictionary = new Dictionary<ValueTuple<string, int>, SourceNamedTypeSymbol>();
			Dictionary<string, ImmutableArray<NamedTypeSymbol>> dictionary3;
			try
			{
				foreach (MergedTypeDeclaration mergedTypeDeclaration in this.declaration.Children)
				{
					SourceNamedTypeSymbol sourceNamedTypeSymbol = new SourceNamedTypeSymbol(this, mergedTypeDeclaration, diagnostics, null);
					this.CheckMemberNameDistinctFromType(sourceNamedTypeSymbol, diagnostics);
					ValueTuple<string, int> valueTuple = new ValueTuple<string, int>(sourceNamedTypeSymbol.Name, sourceNamedTypeSymbol.Arity);
					SourceNamedTypeSymbol sourceNamedTypeSymbol2;
					if (dictionary.TryGetValue(valueTuple, ref sourceNamedTypeSymbol2))
					{
						if (this.Locations.Length == 1 || this.IsPartial)
						{
							if (sourceNamedTypeSymbol.IsPartial && sourceNamedTypeSymbol2.IsPartial)
							{
								diagnostics.Add(ErrorCode.ERR_PartialTypeKindConflict, sourceNamedTypeSymbol.Locations[0], new object[] { sourceNamedTypeSymbol });
							}
							else
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, sourceNamedTypeSymbol.Locations[0], new object[] { this, sourceNamedTypeSymbol.Name });
							}
						}
					}
					else
					{
						dictionary.Add(valueTuple, sourceNamedTypeSymbol);
					}
					instance.Add(sourceNamedTypeSymbol);
				}
				if (this.IsInterface)
				{
					foreach (NamedTypeSymbol namedTypeSymbol in instance)
					{
						Binder.CheckFeatureAvailability(namedTypeSymbol.DeclaringSyntaxReferences[0].GetSyntax(default(CancellationToken)), MessageID.IDS_DefaultInterfaceImplementation, diagnostics, namedTypeSymbol.Locations[0]);
					}
				}
				Dictionary<string, ImmutableArray<NamedTypeSymbol>> dictionary2;
				if (instance.Count <= 0)
				{
					dictionary2 = SourceMemberContainerTypeSymbol.s_emptyTypeMembers;
				}
				else
				{
					dictionary2 = instance.ToDictionary<string>(([global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol s) => s.Name, StringOrdinalComparer.Instance);
				}
				dictionary3 = dictionary2;
			}
			finally
			{
				instance.Free();
			}
			return dictionary3;
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x0019D944 File Offset: 0x0019BB44
		private void CheckMemberNameDistinctFromType(Symbol member, BindingDiagnosticBag diagnostics)
		{
			TypeKind typeKind = this.TypeKind;
			if (typeKind != TypeKind.Class)
			{
				if (typeKind != TypeKind.Interface)
				{
					if (typeKind != TypeKind.Struct)
					{
						return;
					}
				}
				else if (!member.IsStatic)
				{
					return;
				}
			}
			if (member.Name == this.Name)
			{
				diagnostics.Add(ErrorCode.ERR_MemberNameSameAsType, member.Locations[0], new object[] { this.Name });
				return;
			}
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x0019D9AC File Offset: 0x0019BBAC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetMembersUnordered()
		{
			ImmutableArray<Symbol> immutableArray = this._lazyMembersFlattened;
			if (immutableArray.IsDefault)
			{
				immutableArray = this.GetMembersByName().Flatten<string, Symbol>(null);
				ImmutableInterlocked.InterlockedInitialize<Symbol>(ref this._lazyMembersFlattened, immutableArray);
				immutableArray = this._lazyMembersFlattened;
			}
			return immutableArray.ConditionallyDeOrder<Symbol>();
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x0019D9F0 File Offset: 0x0019BBF0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			if (this._flags.FlattenedMembersIsSorted)
			{
				return this._lazyMembersFlattened;
			}
			ImmutableArray<Symbol> immutableArray = this.GetMembersUnordered();
			if (immutableArray.Length > 1)
			{
				immutableArray = immutableArray.Sort(LexicalOrderSymbolComparer.Instance);
				ImmutableInterlocked.InterlockedExchange<Symbol>(ref this._lazyMembersFlattened, immutableArray);
			}
			this._flags.SetFlattenedMembersIsSorted();
			return immutableArray;
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x0019DA48 File Offset: 0x0019BC48
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<Symbol> GetMembers(string name)
		{
			ImmutableArray<Symbol> immutableArray;
			if (this.GetMembersByName().TryGetValue(name, ref immutableArray))
			{
				return immutableArray;
			}
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x0019DA6C File Offset: 0x0019BC6C
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return this.IsRecord;
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x0019DA74 File Offset: 0x0019BC74
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetSimpleNonTypeMembers(string name)
		{
			if (this._lazyMembersDictionary == null && !this.declaration.MemberNames.Contains(name))
			{
				DeclarationKind kind = this.declaration.Kind;
				if (kind != DeclarationKind.Record && kind != DeclarationKind.RecordStruct)
				{
					return ImmutableArray<Symbol>.Empty;
				}
			}
			return this.GetMembers(name);
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x0019DABF File Offset: 0x0019BCBF
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			if (this.TypeKind == TypeKind.Enum)
			{
				FieldSymbol enumValueField = ((SourceNamedTypeSymbol)this).EnumValueField;
				yield return enumValueField;
			}
			foreach (Symbol symbol in this.GetMembers())
			{
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.Event)
				{
					if (kind == SymbolKind.Field && !(symbol is TupleErrorFieldSymbol))
					{
						yield return (FieldSymbol)symbol;
					}
				}
				else
				{
					FieldSymbol associatedField = ((EventSymbol)symbol).AssociatedField;
					if (associatedField != null)
					{
						yield return associatedField;
					}
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x060070A3 RID: 28835 RVA: 0x0019DACF File Offset: 0x0019BCCF
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.GetEarlyAttributeDecodingMembersDictionary().Flatten<string, Symbol>(null);
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x0019DAE0 File Offset: 0x0019BCE0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			ImmutableArray<Symbol> immutableArray;
			if (!this.GetEarlyAttributeDecodingMembersDictionary().TryGetValue(name, ref immutableArray))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x0019DB04 File Offset: 0x0019BD04
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<Symbol>> GetEarlyAttributeDecodingMembersDictionary()
		{
			if (this._lazyEarlyAttributeDecodingMembersDictionary == null)
			{
				Dictionary<string, ImmutableArray<Symbol>> dictionary = Volatile.Read<Dictionary<string, ImmutableArray<Symbol>>>(ref this._lazyMembersDictionary);
				if (dictionary != null)
				{
					return dictionary;
				}
				SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this.GetMembersAndInitializers();
				Dictionary<string, ImmutableArray<Symbol>> dictionary2;
				if (!membersAndInitializers.HaveIndexers)
				{
					dictionary2 = membersAndInitializers.NonTypeMembers.ToDictionary<string, Symbol>((Symbol s) => s.Name, null);
				}
				else
				{
					dictionary2 = membersAndInitializers.NonTypeMembers.WhereAsArray<Symbol>(delegate(Symbol s)
					{
						if (s.IsIndexer())
						{
							return false;
						}
						if (s.IsAccessor())
						{
							Symbol associatedSymbol = ((MethodSymbol)s).AssociatedSymbol;
							return associatedSymbol == null || !associatedSymbol.IsIndexer();
						}
						return true;
					}).ToDictionary<string, Symbol>((Symbol s) => s.Name, null);
				}
				SourceMemberContainerTypeSymbol.AddNestedTypesToDictionary(dictionary2, this.GetTypeMembersDictionary());
				Interlocked.CompareExchange<Dictionary<string, ImmutableArray<Symbol>>>(ref this._lazyEarlyAttributeDecodingMembersDictionary, dictionary2, null);
			}
			return this._lazyEarlyAttributeDecodingMembersDictionary;
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x0019DBDC File Offset: 0x0019BDDC
		protected SourceMemberContainerTypeSymbol.MembersAndInitializers GetMembersAndInitializers()
		{
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this._lazyMembersAndInitializers;
			if (membersAndInitializers != null)
			{
				return membersAndInitializers;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			membersAndInitializers = this.BuildMembersAndInitializers(instance);
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers2 = Interlocked.CompareExchange<SourceMemberContainerTypeSymbol.MembersAndInitializers>(ref this._lazyMembersAndInitializers, membersAndInitializers, null);
			if (membersAndInitializers2 != null)
			{
				instance.Free();
				return membersAndInitializers2;
			}
			this.AddDeclarationDiagnostics(instance);
			instance.Free();
			this._lazyDeclaredMembersAndInitializers = null;
			return membersAndInitializers;
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x0019DC34 File Offset: 0x0019BE34
		[Conditional("DEBUG")]
		internal void AssertMemberExposure(Symbol member, bool forDiagnostics = false)
		{
			if (member is FieldSymbol && forDiagnostics && this.IsTupleType)
			{
				return;
			}
			if (member is NamedTypeSymbol)
			{
				return;
			}
			if (member is TypeParameterSymbol || member is SynthesizedMethodBaseSymbol)
			{
				return;
			}
			FieldSymbol fieldSymbol = member as FieldSymbol;
			if (fieldSymbol != null)
			{
				EventSymbol eventSymbol = fieldSymbol.AssociatedSymbol as EventSymbol;
				if (eventSymbol != null)
				{
					member = eventSymbol;
				}
			}
			SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers = Volatile.Read<SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers>(ref this._lazyDeclaredMembersAndInitializers);
			if (declaredMembersAndInitializers == null || (!declaredMembersAndInitializers.NonTypeMembers.Contains<Symbol>((Symbol m) => m == member) && declaredMembersAndInitializers.RecordPrimaryConstructor != member))
			{
				SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = Volatile.Read<SourceMemberContainerTypeSymbol.MembersAndInitializers>(ref this._lazyMembersAndInitializers);
				if (membersAndInitializers != null)
				{
					membersAndInitializers.NonTypeMembers.Contains<Symbol>((Symbol m) => m == member);
				}
			}
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x0019DD20 File Offset: 0x0019BF20
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		protected Dictionary<string, ImmutableArray<Symbol>> GetMembersByName()
		{
			if (this.state.HasComplete(CompletionPart.Members))
			{
				return this._lazyMembersDictionary;
			}
			return this.GetMembersByNameSlow();
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x0019DD44 File Offset: 0x0019BF44
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<Symbol>> GetMembersByNameSlow()
		{
			if (this._lazyMembersDictionary == null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				Dictionary<string, ImmutableArray<Symbol>> dictionary = this.MakeAllMembers(instance);
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<Symbol>>>(ref this._lazyMembersDictionary, dictionary, null) == null)
				{
					this.AddDeclarationDiagnostics(instance);
					this.state.NotePartComplete(CompletionPart.Members);
				}
				instance.Free();
			}
			this.state.SpinWaitComplete(CompletionPart.Members, default(CancellationToken));
			return this._lazyMembersDictionary;
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x0019DDB3 File Offset: 0x0019BFB3
		internal override IEnumerable<Symbol> GetInstanceFieldsAndEvents()
		{
			return this.GetMembersAndInitializers().NonTypeMembers.Where<Symbol>(NamedTypeSymbol.IsInstanceFieldOrEvent);
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x0019DDCC File Offset: 0x0019BFCC
		protected void AfterMembersChecks(BindingDiagnosticBag diagnostics)
		{
			if (this.IsInterface)
			{
				SourceMemberContainerTypeSymbol.CheckInterfaceMembers(this.GetMembersAndInitializers().NonTypeMembers, diagnostics);
			}
			this.CheckMemberNamesDistinctFromType(diagnostics);
			this.CheckMemberNameConflicts(diagnostics);
			this.CheckRecordMemberNames(diagnostics);
			this.CheckSpecialMemberErrors(diagnostics);
			this.CheckTypeParameterNameConflicts(diagnostics);
			this.CheckAccessorNameConflicts(diagnostics);
			bool knownCircularStruct = this.KnownCircularStruct;
			this.CheckSequentialOnPartialType(diagnostics);
			this.CheckForProtectedInStaticClass(diagnostics);
			this.CheckForUnmatchedOperators(diagnostics);
			Location location = this.Locations[0];
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (this.IsRefLikeType)
			{
				declaringCompilation.EnsureIsByRefLikeAttributeExists(diagnostics, location, true);
			}
			if (this.IsReadOnly)
			{
				declaringCompilation.EnsureIsReadOnlyAttributeExists(diagnostics, location, true);
			}
			SourceMemberContainerTypeSymbol.<>c__DisplayClass120_0 CS$<>8__locals1;
			CS$<>8__locals1.baseType = this.BaseTypeNoUseSiteDiagnostics;
			CS$<>8__locals1.interfaces = this.GetInterfacesToEmit();
			if (SourceMemberContainerTypeSymbol.<AfterMembersChecks>g__hasBaseTypeOrInterface|120_3((NamedTypeSymbol t) => t.ContainsNativeInteger(), ref CS$<>8__locals1))
			{
				declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, location, true);
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				byte b;
				if (base.ShouldEmitNullableContextValue(out b))
				{
					declaringCompilation.EnsureNullableContextAttributeExists(diagnostics, location, true);
				}
				if (SourceMemberContainerTypeSymbol.<AfterMembersChecks>g__hasBaseTypeOrInterface|120_3((NamedTypeSymbol t) => t.NeedsNullableAttribute(), ref CS$<>8__locals1))
				{
					declaringCompilation.EnsureNullableAttributeExists(diagnostics, location, true);
				}
			}
			if (CS$<>8__locals1.interfaces.Any<NamedTypeSymbol>((NamedTypeSymbol t) => SourceMemberContainerTypeSymbol.<AfterMembersChecks>g__needsTupleElementNamesAttribute|120_4(t)))
			{
				Binder.ReportMissingTupleElementNamesAttributesIfNeeded(declaringCompilation, location, diagnostics);
			}
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x0019DF40 File Offset: 0x0019C140
		private void CheckMemberNamesDistinctFromType(BindingDiagnosticBag diagnostics)
		{
			foreach (Symbol symbol in this.GetMembersAndInitializers().NonTypeMembers)
			{
				this.CheckMemberNameDistinctFromType(symbol, diagnostics);
			}
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x0019DF7C File Offset: 0x0019C17C
		private void CheckRecordMemberNames(BindingDiagnosticBag diagnostics)
		{
			if (this.declaration.Kind != DeclarationKind.Record && this.declaration.Kind != DeclarationKind.RecordStruct)
			{
				return;
			}
			foreach (Symbol symbol in this.GetMembers("Clone"))
			{
				diagnostics.Add(ErrorCode.ERR_CloneDisallowedInRecord, symbol.Locations[0]);
			}
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x0019DFEC File Offset: 0x0019C1EC
		private void CheckMemberNameConflicts(BindingDiagnosticBag diagnostics)
		{
			Dictionary<string, ImmutableArray<Symbol>> membersByName = this.GetMembersByName();
			this.CheckIndexerNameConflicts(diagnostics, membersByName);
			Dictionary<SourceMemberMethodSymbol, SourceMemberMethodSymbol> dictionary = new Dictionary<SourceMemberMethodSymbol, SourceMemberMethodSymbol>(MemberSignatureComparer.DuplicateSourceComparer);
			Dictionary<SourceMemberMethodSymbol, SourceMemberMethodSymbol> dictionary2 = new Dictionary<SourceMemberMethodSymbol, SourceMemberMethodSymbol>(MemberSignatureComparer.DuplicateSourceComparer);
			HashSet<SourceUserDefinedConversionSymbol> hashSet = new HashSet<SourceUserDefinedConversionSymbol>(ConversionSignatureComparer.Comparer);
			foreach (KeyValuePair<string, ImmutableArray<Symbol>> keyValuePair in membersByName)
			{
				string key = keyValuePair.Key;
				Symbol symbol = this.GetTypeMembers(key).FirstOrDefault<NamedTypeSymbol>();
				dictionary.Clear();
				foreach (Symbol symbol2 in keyValuePair.Value)
				{
					if (symbol2.Kind != SymbolKind.NamedType && !symbol2.IsAccessor() && !symbol2.IsIndexer())
					{
						if (symbol != null)
						{
							if (symbol2.Kind != SymbolKind.Method || symbol.Kind != SymbolKind.Method)
							{
								if ((symbol2.Kind != SymbolKind.Field || !symbol2.IsImplicitlyDeclared) && (this.Locations.Length == 1 || this.IsPartial))
								{
									diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, symbol2.Locations[0], new object[] { this, symbol2.Name });
								}
								if (symbol.Kind == SymbolKind.Method)
								{
									symbol = symbol2;
								}
							}
						}
						else
						{
							symbol = symbol2;
						}
						SourceUserDefinedConversionSymbol sourceUserDefinedConversionSymbol = symbol2 as SourceUserDefinedConversionSymbol;
						SourceMemberMethodSymbol sourceMemberMethodSymbol = symbol2 as SourceMemberMethodSymbol;
						if (sourceUserDefinedConversionSymbol != null)
						{
							if (!hashSet.Add(sourceUserDefinedConversionSymbol))
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateConversionInClass, sourceUserDefinedConversionSymbol.Locations[0], new object[] { this });
							}
							else if (!dictionary2.ContainsKey(sourceUserDefinedConversionSymbol))
							{
								dictionary2.Add(sourceUserDefinedConversionSymbol, sourceUserDefinedConversionSymbol);
							}
							SourceMemberMethodSymbol sourceMemberMethodSymbol2;
							if (dictionary.TryGetValue(sourceUserDefinedConversionSymbol, ref sourceMemberMethodSymbol2))
							{
								this.ReportMethodSignatureCollision(diagnostics, sourceUserDefinedConversionSymbol, sourceMemberMethodSymbol2);
							}
						}
						else if (sourceMemberMethodSymbol != null)
						{
							SourceMemberMethodSymbol sourceMemberMethodSymbol3;
							if (dictionary2.TryGetValue(sourceMemberMethodSymbol, ref sourceMemberMethodSymbol3))
							{
								this.ReportMethodSignatureCollision(diagnostics, sourceMemberMethodSymbol, sourceMemberMethodSymbol3);
							}
							SourceMemberMethodSymbol sourceMemberMethodSymbol4;
							if (dictionary.TryGetValue(sourceMemberMethodSymbol, ref sourceMemberMethodSymbol4))
							{
								this.ReportMethodSignatureCollision(diagnostics, sourceMemberMethodSymbol, sourceMemberMethodSymbol4);
							}
							else
							{
								dictionary.Add(sourceMemberMethodSymbol, sourceMemberMethodSymbol);
							}
						}
					}
				}
			}
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x0019E228 File Offset: 0x0019C428
		private void ReportMethodSignatureCollision(BindingDiagnosticBag diagnostics, SourceMemberMethodSymbol method1, SourceMemberMethodSymbol method2)
		{
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = method1 as SourceOrdinaryMethodSymbol;
			if (sourceOrdinaryMethodSymbol != null)
			{
				SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol2;
				if (sourceOrdinaryMethodSymbol.IsPartialDefinition)
				{
					sourceOrdinaryMethodSymbol2 = method2 as SourceOrdinaryMethodSymbol;
					if (sourceOrdinaryMethodSymbol2 == null)
					{
						goto IL_005E;
					}
					if (sourceOrdinaryMethodSymbol2.IsPartialImplementation)
					{
						return;
					}
					if (!sourceOrdinaryMethodSymbol.IsPartialImplementation)
					{
						goto IL_005E;
					}
				}
				else
				{
					if (!sourceOrdinaryMethodSymbol.IsPartialImplementation)
					{
						goto IL_005E;
					}
					sourceOrdinaryMethodSymbol2 = method2 as SourceOrdinaryMethodSymbol;
					if (sourceOrdinaryMethodSymbol2 == null)
					{
						goto IL_005E;
					}
				}
				if (!sourceOrdinaryMethodSymbol2.IsPartialDefinition)
				{
					goto IL_005E;
				}
				return;
			}
			if (method1 is SynthesizedSimpleProgramEntryPointSymbol)
			{
				if (method2 is SynthesizedSimpleProgramEntryPointSymbol)
				{
					return;
				}
			}
			IL_005E:
			if (method1.MethodKind == MethodKind.Constructor && ((ConstructorDeclarationSyntax)method1.SyntaxRef.GetSyntax(default(CancellationToken))).Identifier.ValueText != this.Name)
			{
				return;
			}
			for (int i = 0; i < method1.ParameterCount; i++)
			{
				RefKind refKind = method1.Parameters[i].RefKind;
				RefKind refKind2 = method2.Parameters[i].RefKind;
				if (refKind != refKind2)
				{
					MessageID messageID = ((method1.MethodKind == MethodKind.Constructor) ? MessageID.IDS_SK_CONSTRUCTOR : MessageID.IDS_SK_METHOD);
					diagnostics.Add(ErrorCode.ERR_OverloadRefKind, method1.Locations[0], new object[]
					{
						this,
						messageID.Localize(),
						refKind.ToParameterDisplayString(),
						refKind2.ToParameterDisplayString()
					});
					return;
				}
			}
			string text = ((method1.MethodKind == MethodKind.Destructor && method2.MethodKind == MethodKind.Destructor) ? ("~" + this.Name) : (method1.IsConstructor() ? this.Name : method1.Name));
			diagnostics.Add(ErrorCode.ERR_MemberAlreadyExists, method1.Locations[0], new object[] { text, this });
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x0019E3E8 File Offset: 0x0019C5E8
		private void CheckIndexerNameConflicts(BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] Dictionary<string, ImmutableArray<Symbol>> membersByName)
		{
			PooledHashSet<string> pooledHashSet = null;
			if (this.Arity > 0)
			{
				pooledHashSet = PooledHashSet<string>.GetInstance();
				foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
				{
					pooledHashSet.Add(typeParameterSymbol.Name);
				}
			}
			Dictionary<PropertySymbol, PropertySymbol> dictionary = new Dictionary<PropertySymbol, PropertySymbol>(MemberSignatureComparer.DuplicateSourceComparer);
			foreach (ImmutableArray<Symbol> immutableArray in membersByName.Values)
			{
				string text = null;
				dictionary.Clear();
				foreach (Symbol symbol in immutableArray)
				{
					if (symbol.IsIndexer())
					{
						PropertySymbol propertySymbol = (PropertySymbol)symbol;
						this.CheckIndexerSignatureCollisions(propertySymbol, diagnostics, membersByName, dictionary, ref text);
						if (pooledHashSet != null)
						{
							string metadataName = propertySymbol.MetadataName;
							if (pooledHashSet.Contains(metadataName))
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, propertySymbol.Locations[0], new object[] { this, metadataName });
							}
						}
					}
				}
			}
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x0019E518 File Offset: 0x0019C718
		private void CheckIndexerSignatureCollisions(PropertySymbol indexer, BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] Dictionary<string, ImmutableArray<Symbol>> membersByName, Dictionary<PropertySymbol, PropertySymbol> indexersBySignature, [global::System.Runtime.CompilerServices.Nullable(2)] ref string lastIndexerName)
		{
			if (!indexer.IsExplicitInterfaceImplementation)
			{
				string metadataName = indexer.MetadataName;
				if (lastIndexerName != null && lastIndexerName != metadataName)
				{
					diagnostics.Add(ErrorCode.ERR_InconsistentIndexerNames, indexer.Locations[0]);
				}
				lastIndexerName = metadataName;
				if ((this.Locations.Length == 1 || this.IsPartial) && membersByName.ContainsKey(metadataName))
				{
					diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, indexer.Locations[0], new object[] { this, metadataName });
				}
			}
			PropertySymbol propertySymbol;
			if (indexersBySignature.TryGetValue(indexer, ref propertySymbol))
			{
				diagnostics.Add(ErrorCode.ERR_MemberAlreadyExists, indexer.Locations[0], new object[]
				{
					SyntaxFacts.GetText(SyntaxKind.ThisKeyword),
					this
				});
				return;
			}
			indexersBySignature[indexer] = indexer;
		}

		// Token: 0x060070B2 RID: 28850 RVA: 0x0019E5F0 File Offset: 0x0019C7F0
		private void CheckSpecialMemberErrors(BindingDiagnosticBag diagnostics)
		{
			TypeConversions typeConversions = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				symbol.AfterAddingTypeMembersChecks(typeConversions, diagnostics);
			}
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x0019E638 File Offset: 0x0019C838
		private void CheckTypeParameterNameConflicts(BindingDiagnosticBag diagnostics)
		{
			if (this.TypeKind == TypeKind.Delegate)
			{
				return;
			}
			if (this.Locations.Length == 1 || this.IsPartial)
			{
				foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
				{
					foreach (Symbol symbol in this.GetMembers(typeParameterSymbol.Name))
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, symbol.Locations[0], new object[] { this, typeParameterSymbol.Name });
					}
				}
			}
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x0019E6E4 File Offset: 0x0019C8E4
		private void CheckAccessorNameConflicts(BindingDiagnosticBag diagnostics)
		{
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				if (!symbol.IsExplicitInterfaceImplementation())
				{
					SymbolKind kind = symbol.Kind;
					if (kind != SymbolKind.Event)
					{
						if (kind == SymbolKind.Property)
						{
							PropertySymbol propertySymbol = (PropertySymbol)symbol;
							this.CheckForMemberConflictWithPropertyAccessor(propertySymbol, true, diagnostics);
							this.CheckForMemberConflictWithPropertyAccessor(propertySymbol, false, diagnostics);
						}
					}
					else
					{
						EventSymbol eventSymbol = (EventSymbol)symbol;
						this.CheckForMemberConflictWithEventAccessor(eventSymbol, true, diagnostics);
						this.CheckForMemberConflictWithEventAccessor(eventSymbol, false, diagnostics);
					}
				}
			}
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x060070B5 RID: 28853 RVA: 0x0019E768 File Offset: 0x0019C968
		internal override bool KnownCircularStruct
		{
			get
			{
				if (this._lazyKnownCircularStruct == 0)
				{
					if (this.TypeKind != TypeKind.Struct)
					{
						Interlocked.CompareExchange(ref this._lazyKnownCircularStruct, 1, 0);
					}
					else
					{
						BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
						int num = (int)this.CheckStructCircularity(instance).ToThreeState();
						if (Interlocked.CompareExchange(ref this._lazyKnownCircularStruct, num, 0) == 0)
						{
							this.AddDeclarationDiagnostics(instance);
						}
						instance.Free();
					}
				}
				return this._lazyKnownCircularStruct == 2;
			}
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x0019E7CF File Offset: 0x0019C9CF
		private bool CheckStructCircularity(BindingDiagnosticBag diagnostics)
		{
			this.CheckFiniteFlatteningGraph(diagnostics);
			return this.HasStructCircularity(diagnostics);
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x0019E7E0 File Offset: 0x0019C9E0
		private bool HasStructCircularity(BindingDiagnosticBag diagnostics)
		{
			foreach (ImmutableArray<Symbol> immutableArray in this.GetMembersByName().Values)
			{
				foreach (Symbol symbol in immutableArray)
				{
					if (symbol.Kind == SymbolKind.Field)
					{
						FieldSymbol fieldSymbol = (FieldSymbol)symbol;
						if (!fieldSymbol.IsStatic)
						{
							TypeSymbol typeSymbol = fieldSymbol.NonPointerType();
							if (typeSymbol != null && typeSymbol.TypeKind == TypeKind.Struct && BaseTypeAnalysis.StructDependsOn((NamedTypeSymbol)typeSymbol, this) && !typeSymbol.IsPrimitiveRecursiveStruct())
							{
								Symbol symbol2 = fieldSymbol.AssociatedSymbol ?? fieldSymbol;
								if (symbol2.Kind == SymbolKind.Parameter)
								{
									symbol2 = fieldSymbol;
								}
								diagnostics.Add(ErrorCode.ERR_StructLayoutCycle, symbol2.Locations[0], new object[] { symbol2, typeSymbol });
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x0019E8FC File Offset: 0x0019CAFC
		private void CheckForProtectedInStaticClass(BindingDiagnosticBag diagnostics)
		{
			if (!this.IsStatic)
			{
				return;
			}
			foreach (ImmutableArray<Symbol> immutableArray in this.GetMembersByName().Values)
			{
				foreach (Symbol symbol in immutableArray)
				{
					if (!(symbol is TypeSymbol) && symbol.DeclaredAccessibility.HasProtected() && (symbol.Kind != SymbolKind.Method || ((MethodSymbol)symbol).MethodKind != MethodKind.Destructor))
					{
						diagnostics.Add(ErrorCode.ERR_ProtectedInStatic, symbol.Locations[0], new object[] { symbol });
					}
				}
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x0019E9C4 File Offset: 0x0019CBC4
		private void CheckForUnmatchedOperators(BindingDiagnosticBag diagnostics)
		{
			this.CheckForUnmatchedOperator(diagnostics, "op_True", "op_False");
			this.CheckForUnmatchedOperator(diagnostics, "op_Equality", "op_Inequality");
			this.CheckForUnmatchedOperator(diagnostics, "op_LessThan", "op_GreaterThan");
			this.CheckForUnmatchedOperator(diagnostics, "op_LessThanOrEqual", "op_GreaterThanOrEqual");
			this.CheckForEqualityAndGetHashCode(diagnostics);
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x0019EA1C File Offset: 0x0019CC1C
		private void CheckForUnmatchedOperator(BindingDiagnosticBag diagnostics, string operatorName1, string operatorName2)
		{
			ImmutableArray<MethodSymbol> operators = base.GetOperators(operatorName1);
			ImmutableArray<MethodSymbol> operators2 = base.GetOperators(operatorName2);
			SourceMemberContainerTypeSymbol.CheckForUnmatchedOperator(diagnostics, operators, operators2, operatorName2);
			SourceMemberContainerTypeSymbol.CheckForUnmatchedOperator(diagnostics, operators2, operators, operatorName1);
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x0019EA4C File Offset: 0x0019CC4C
		private static void CheckForUnmatchedOperator(BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> ops1, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> ops2, string operatorName2)
		{
			foreach (MethodSymbol methodSymbol in ops1)
			{
				bool flag = false;
				foreach (MethodSymbol methodSymbol2 in ops2)
				{
					flag = SourceMemberContainerTypeSymbol.DoOperatorsPair(methodSymbol, methodSymbol2);
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					diagnostics.Add(ErrorCode.ERR_OperatorNeedsMatch, methodSymbol.Locations[0], new object[]
					{
						methodSymbol,
						SyntaxFacts.GetText(SyntaxFacts.GetOperatorKind(operatorName2))
					});
				}
			}
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x0019EAD8 File Offset: 0x0019CCD8
		private static bool DoOperatorsPair(MethodSymbol op1, MethodSymbol op2)
		{
			if (op1.ParameterCount != op2.ParameterCount)
			{
				return false;
			}
			for (int i = 0; i < op1.ParameterCount; i++)
			{
				if (!op1.ParameterTypesWithAnnotations[i].Equals(op2.ParameterTypesWithAnnotations[i], TypeCompareKind.AllIgnoreOptions))
				{
					return false;
				}
			}
			return op1.ReturnType.Equals(op2.ReturnType, TypeCompareKind.AllIgnoreOptions);
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x0019EB4C File Offset: 0x0019CD4C
		private void CheckForEqualityAndGetHashCode(BindingDiagnosticBag diagnostics)
		{
			if (this.IsInterfaceType())
			{
				return;
			}
			if (this.IsRecord || this.IsRecordStruct)
			{
				return;
			}
			bool flag = base.GetOperators("op_Equality").Any<MethodSymbol>() || base.GetOperators("op_Inequality").Any<MethodSymbol>();
			bool flag2 = this.TypeOverridesObjectMethod("Equals");
			if (flag || flag2)
			{
				bool flag3 = this.TypeOverridesObjectMethod("GetHashCode");
				if (flag2 && !flag3)
				{
					diagnostics.Add(ErrorCode.WRN_EqualsWithoutGetHashCode, this.Locations[0], new object[] { this });
				}
				if (flag && !flag2)
				{
					diagnostics.Add(ErrorCode.WRN_EqualityOpWithoutEquals, this.Locations[0], new object[] { this });
				}
				if (flag && !flag3)
				{
					diagnostics.Add(ErrorCode.WRN_EqualityOpWithoutGetHashCode, this.Locations[0], new object[] { this });
				}
			}
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x0019EC3C File Offset: 0x0019CE3C
		private bool TypeOverridesObjectMethod(string name)
		{
			foreach (MethodSymbol methodSymbol in this.GetMembers(name).OfType<MethodSymbol>())
			{
				if (methodSymbol.IsOverride && methodSymbol.GetConstructedLeastOverriddenMethod(this, false).ContainingType.SpecialType == SpecialType.System_Object)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x0019ECB0 File Offset: 0x0019CEB0
		private void CheckFiniteFlatteningGraph(BindingDiagnosticBag diagnostics)
		{
			if (base.AllTypeArgumentCount() == 0)
			{
				return;
			}
			Dictionary<NamedTypeSymbol, NamedTypeSymbol> dictionary = new Dictionary<NamedTypeSymbol, NamedTypeSymbol>(ReferenceEqualityComparer.Instance);
			dictionary.Add(this, this);
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				FieldSymbol fieldSymbol = symbol as FieldSymbol;
				if (fieldSymbol != null && fieldSymbol.IsStatic && fieldSymbol.Type.TypeKind == TypeKind.Struct)
				{
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)fieldSymbol.Type;
					if (SourceMemberContainerTypeSymbol.InfiniteFlatteningGraph(this, namedTypeSymbol, dictionary))
					{
						diagnostics.Add(ErrorCode.ERR_StructLayoutCycle, fieldSymbol.Locations[0], new object[] { fieldSymbol, namedTypeSymbol });
						return;
					}
				}
			}
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x0019ED60 File Offset: 0x0019CF60
		private static bool InfiniteFlatteningGraph(SourceMemberContainerTypeSymbol top, NamedTypeSymbol t, Dictionary<NamedTypeSymbol, NamedTypeSymbol> instanceMap)
		{
			if (!t.ContainsTypeParameter(null))
			{
				return false;
			}
			NamedTypeSymbol originalDefinition = t.OriginalDefinition;
			NamedTypeSymbol namedTypeSymbol;
			if (instanceMap.TryGetValue(originalDefinition, ref namedTypeSymbol))
			{
				return !TypeSymbol.Equals(namedTypeSymbol, t, TypeCompareKind.AllNullableIgnoreOptions) && originalDefinition == top;
			}
			instanceMap.Add(originalDefinition, t);
			bool flag;
			try
			{
				foreach (Symbol symbol in t.GetMembersUnordered())
				{
					FieldSymbol fieldSymbol = symbol as FieldSymbol;
					if (fieldSymbol != null && fieldSymbol.IsStatic && fieldSymbol.Type.TypeKind == TypeKind.Struct)
					{
						NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)fieldSymbol.Type;
						if (SourceMemberContainerTypeSymbol.InfiniteFlatteningGraph(top, namedTypeSymbol2, instanceMap))
						{
							return true;
						}
					}
				}
				flag = false;
			}
			finally
			{
				instanceMap.Remove(originalDefinition);
			}
			return flag;
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x0019EE28 File Offset: 0x0019D028
		private void CheckSequentialOnPartialType(BindingDiagnosticBag diagnostics)
		{
			if (!this.IsPartial || this.Layout.Kind != null)
			{
				return;
			}
			SyntaxReference syntaxReference = null;
			if (this.SyntaxReferences.Length <= 1)
			{
				return;
			}
			foreach (SyntaxReference syntaxReference2 in this.SyntaxReferences)
			{
				TypeDeclarationSyntax typeDeclarationSyntax = syntaxReference2.GetSyntax(default(CancellationToken)) as TypeDeclarationSyntax;
				if (typeDeclarationSyntax != null)
				{
					SyntaxList<MemberDeclarationSyntax>.Enumerator enumerator2 = typeDeclarationSyntax.Members.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						if (SourceMemberContainerTypeSymbol.HasInstanceData(enumerator2.Current))
						{
							if (syntaxReference != null && syntaxReference != syntaxReference2)
							{
								diagnostics.Add(ErrorCode.WRN_SequentialOnPartialClass, this.Locations[0], new object[] { this });
								return;
							}
							syntaxReference = syntaxReference2;
						}
					}
				}
			}
		}

		// Token: 0x060070C2 RID: 28866 RVA: 0x0019EF04 File Offset: 0x0019D104
		private static bool HasInstanceData(MemberDeclarationSyntax m)
		{
			SyntaxKind syntaxKind = m.Kind();
			if (syntaxKind == SyntaxKind.FieldDeclaration)
			{
				FieldDeclarationSyntax fieldDeclarationSyntax = (FieldDeclarationSyntax)m;
				return !SourceMemberContainerTypeSymbol.ContainsModifier(fieldDeclarationSyntax.Modifiers, SyntaxKind.StaticKeyword) && !SourceMemberContainerTypeSymbol.ContainsModifier(fieldDeclarationSyntax.Modifiers, SyntaxKind.ConstKeyword);
			}
			if (syntaxKind == SyntaxKind.EventFieldDeclaration)
			{
				EventFieldDeclarationSyntax eventFieldDeclarationSyntax = (EventFieldDeclarationSyntax)m;
				return !SourceMemberContainerTypeSymbol.ContainsModifier(eventFieldDeclarationSyntax.Modifiers, SyntaxKind.StaticKeyword) && !SourceMemberContainerTypeSymbol.ContainsModifier(eventFieldDeclarationSyntax.Modifiers, SyntaxKind.AbstractKeyword) && !SourceMemberContainerTypeSymbol.ContainsModifier(eventFieldDeclarationSyntax.Modifiers, SyntaxKind.ExternKeyword);
			}
			if (syntaxKind != SyntaxKind.PropertyDeclaration)
			{
				return false;
			}
			PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)m;
			if (!SourceMemberContainerTypeSymbol.ContainsModifier(propertyDeclarationSyntax.Modifiers, SyntaxKind.StaticKeyword) && !SourceMemberContainerTypeSymbol.ContainsModifier(propertyDeclarationSyntax.Modifiers, SyntaxKind.AbstractKeyword) && !SourceMemberContainerTypeSymbol.ContainsModifier(propertyDeclarationSyntax.Modifiers, SyntaxKind.ExternKeyword) && propertyDeclarationSyntax.AccessorList != null)
			{
				return SourceMemberContainerTypeSymbol.All<AccessorDeclarationSyntax>(propertyDeclarationSyntax.AccessorList.Accessors, (AccessorDeclarationSyntax a) => a.Body == null && a.ExpressionBody == null);
			}
			return false;
		}

		// Token: 0x060070C3 RID: 28867 RVA: 0x0019F020 File Offset: 0x0019D220
		private static bool All<[global::System.Runtime.CompilerServices.Nullable(0)] T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<T> list, Func<T, bool> predicate) where T : CSharpSyntaxNode
		{
			foreach (T t in list)
			{
				if (predicate.Invoke(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x0019F054 File Offset: 0x0019D254
		private static bool ContainsModifier(SyntaxTokenList modifiers, SyntaxKind modifier)
		{
			SyntaxTokenList.Enumerator enumerator = modifiers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsKind(modifier))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x0019F088 File Offset: 0x0019D288
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<Symbol>> MakeAllMembers(BindingDiagnosticBag diagnostics)
		{
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this.GetMembersAndInitializers();
			Dictionary<string, ImmutableArray<Symbol>> dictionary;
			if (!membersAndInitializers.HaveIndexers && !this.IsTupleType && this._lazyEarlyAttributeDecodingMembersDictionary != null)
			{
				dictionary = this._lazyEarlyAttributeDecodingMembersDictionary;
			}
			else
			{
				dictionary = membersAndInitializers.NonTypeMembers.ToDictionary<string, Symbol>((Symbol s) => s.Name, StringOrdinalComparer.Instance);
				SourceMemberContainerTypeSymbol.AddNestedTypesToDictionary(dictionary, this.GetTypeMembersDictionary());
			}
			this.MergePartialMembers(ref dictionary, diagnostics);
			return dictionary;
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x0019F104 File Offset: 0x0019D304
		private static void AddNestedTypesToDictionary([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] Dictionary<string, ImmutableArray<Symbol>> membersByName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] Dictionary<string, ImmutableArray<NamedTypeSymbol>> typesByName)
		{
			foreach (KeyValuePair<string, ImmutableArray<NamedTypeSymbol>> keyValuePair in typesByName)
			{
				string key = keyValuePair.Key;
				ImmutableArray<Symbol> immutableArray = StaticCast<Symbol>.From<NamedTypeSymbol>(keyValuePair.Value);
				ImmutableArray<Symbol> immutableArray2;
				if (membersByName.TryGetValue(key, ref immutableArray2))
				{
					membersByName[key] = immutableArray2.Concat<Symbol>(immutableArray);
				}
				else
				{
					membersByName.Add(key, immutableArray);
				}
			}
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x0019F184 File Offset: 0x0019D384
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected virtual SourceMemberContainerTypeSymbol.MembersAndInitializers BuildMembersAndInitializers(BindingDiagnosticBag diagnostics)
		{
			SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers = this.<BuildMembersAndInitializers>g__getDeclaredMembersAndInitializers|152_0();
			if (declaredMembersAndInitializers == null)
			{
				return null;
			}
			SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder membersAndInitializersBuilder = new SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder(declaredMembersAndInitializers);
			this.AddSynthesizedMembers(membersAndInitializersBuilder, declaredMembersAndInitializers, diagnostics);
			if (Volatile.Read<SourceMemberContainerTypeSymbol.MembersAndInitializers>(ref this._lazyMembersAndInitializers) != null)
			{
				membersAndInitializersBuilder.Free();
				return null;
			}
			return membersAndInitializersBuilder.ToReadOnlyAndFree(declaredMembersAndInitializers);
		}

		// Token: 0x060070C8 RID: 28872 RVA: 0x0019F1CC File Offset: 0x0019D3CC
		private void AddSynthesizedMembers(SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder builder, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers, BindingDiagnosticBag diagnostics)
		{
			TypeKind typeKind = this.TypeKind;
			if (typeKind <= TypeKind.Enum)
			{
				if (typeKind != TypeKind.Class && typeKind != TypeKind.Enum)
				{
					return;
				}
			}
			else if (typeKind != TypeKind.Interface && typeKind != TypeKind.Struct && typeKind != TypeKind.Submission)
			{
				return;
			}
			this.AddSynthesizedRecordMembersIfNecessary(builder, declaredMembersAndInitializers, diagnostics);
			this.AddSynthesizedConstructorsIfNecessary(builder, declaredMembersAndInitializers, diagnostics);
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x0019F210 File Offset: 0x0019D410
		private void AddDeclaredNontypeMembers(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder builder, BindingDiagnosticBag diagnostics)
		{
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
			{
				if (singleTypeDeclaration.HasAnyNontypeMembers)
				{
					if (this._lazyMembersAndInitializers != null)
					{
						return;
					}
					SyntaxNode syntax = singleTypeDeclaration.SyntaxReference.GetSyntax(default(CancellationToken));
					SyntaxKind syntaxKind = syntax.Kind();
					if (syntaxKind <= SyntaxKind.NamespaceDeclaration)
					{
						if (syntaxKind == SyntaxKind.CompilationUnit)
						{
							this.AddNonTypeMembers(builder, ((CompilationUnitSyntax)syntax).Members, diagnostics);
							continue;
						}
						if (syntaxKind == SyntaxKind.NamespaceDeclaration)
						{
							this.AddNonTypeMembers(builder, ((NamespaceDeclarationSyntax)syntax).Members, diagnostics);
							continue;
						}
					}
					else
					{
						switch (syntaxKind)
						{
						case SyntaxKind.ClassDeclaration:
						case SyntaxKind.StructDeclaration:
						case SyntaxKind.InterfaceDeclaration:
						{
							TypeDeclarationSyntax typeDeclarationSyntax = (TypeDeclarationSyntax)syntax;
							this.AddNonTypeMembers(builder, typeDeclarationSyntax.Members, diagnostics);
							continue;
						}
						case SyntaxKind.EnumDeclaration:
							this.AddEnumMembers(builder, (EnumDeclarationSyntax)syntax, diagnostics);
							continue;
						case SyntaxKind.DelegateDeclaration:
							SourceDelegateMethodSymbol.AddDelegateMembers(this, builder.NonTypeMembers, (DelegateDeclarationSyntax)syntax, diagnostics);
							continue;
						default:
							if (syntaxKind == SyntaxKind.RecordDeclaration || syntaxKind == SyntaxKind.RecordStructDeclaration)
							{
								RecordDeclarationSyntax recordDeclarationSyntax = (RecordDeclarationSyntax)syntax;
								ParameterListSyntax parameterList = recordDeclarationSyntax.ParameterList;
								this.<AddDeclaredNontypeMembers>g__noteRecordParameters|154_0(recordDeclarationSyntax, parameterList, builder, diagnostics);
								this.AddNonTypeMembers(builder, recordDeclarationSyntax.Members, diagnostics);
								if (syntax.Kind() == SyntaxKind.RecordStructDeclaration && parameterList != null && parameterList.ParameterCount == 0)
								{
									diagnostics.Add(ErrorCode.ERR_StructsCantContainDefaultConstructor, parameterList.Location);
									continue;
								}
								continue;
							}
							break;
						}
					}
					throw ExceptionUtilities.UnexpectedValue(syntax.Kind());
				}
			}
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x0019F3BA File Offset: 0x0019D5BA
		internal Binder GetBinder(CSharpSyntaxNode syntaxNode)
		{
			return this.DeclaringCompilation.GetBinder(syntaxNode);
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x0019F3C8 File Offset: 0x0019D5C8
		private void MergePartialMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] ref Dictionary<string, ImmutableArray<Symbol>> membersByName, BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(membersByName.Count);
			instance.AddRange(membersByName.Keys);
			Dictionary<MethodSymbol, SourceMemberMethodSymbol> dictionary = new Dictionary<MethodSymbol, SourceMemberMethodSymbol>(MemberSignatureComparer.PartialMethodsComparer);
			foreach (string text in instance)
			{
				dictionary.Clear();
				foreach (Symbol symbol in membersByName[text])
				{
					SourceMemberMethodSymbol sourceMemberMethodSymbol = symbol as SourceMemberMethodSymbol;
					if (sourceMemberMethodSymbol != null && sourceMemberMethodSymbol.IsPartial)
					{
						SourceMemberMethodSymbol sourceMemberMethodSymbol2;
						if (dictionary.TryGetValue(sourceMemberMethodSymbol, ref sourceMemberMethodSymbol2))
						{
							SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = (SourceOrdinaryMethodSymbol)sourceMemberMethodSymbol2;
							SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol2 = (SourceOrdinaryMethodSymbol)sourceMemberMethodSymbol;
							if (sourceOrdinaryMethodSymbol2.IsPartialImplementation)
							{
								if (!sourceOrdinaryMethodSymbol.IsPartialImplementation)
								{
									MethodSymbol otherPartOfPartial = sourceOrdinaryMethodSymbol.OtherPartOfPartial;
									if (otherPartOfPartial == null || otherPartOfPartial == sourceOrdinaryMethodSymbol2)
									{
										goto IL_00DD;
									}
								}
								diagnostics.Add(ErrorCode.ERR_PartialMethodOnlyOneActual, sourceOrdinaryMethodSymbol2.Locations[0]);
								continue;
							}
							IL_00DD:
							if (sourceOrdinaryMethodSymbol2.IsPartialDefinition)
							{
								if (!sourceOrdinaryMethodSymbol.IsPartialDefinition)
								{
									MethodSymbol otherPartOfPartial2 = sourceOrdinaryMethodSymbol.OtherPartOfPartial;
									if (otherPartOfPartial2 == null || otherPartOfPartial2 == sourceOrdinaryMethodSymbol2)
									{
										goto IL_0121;
									}
								}
								diagnostics.Add(ErrorCode.ERR_PartialMethodOnlyOneLatent, sourceOrdinaryMethodSymbol2.Locations[0]);
								continue;
							}
							IL_0121:
							if (membersByName == this._lazyEarlyAttributeDecodingMembersDictionary)
							{
								membersByName = new Dictionary<string, ImmutableArray<Symbol>>(membersByName);
							}
							membersByName[text] = SourceMemberContainerTypeSymbol.FixPartialMember(membersByName[text], sourceOrdinaryMethodSymbol, sourceOrdinaryMethodSymbol2);
						}
						else
						{
							dictionary.Add(sourceMemberMethodSymbol, sourceMemberMethodSymbol);
						}
					}
				}
				foreach (SourceMemberMethodSymbol sourceMemberMethodSymbol3 in dictionary.Values)
				{
					SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol3 = (SourceOrdinaryMethodSymbol)sourceMemberMethodSymbol3;
					if (sourceOrdinaryMethodSymbol3.IsPartialImplementation && sourceOrdinaryMethodSymbol3.OtherPartOfPartial == null)
					{
						diagnostics.Add(ErrorCode.ERR_PartialMethodMustHaveLatent, sourceOrdinaryMethodSymbol3.Locations[0], new object[] { sourceOrdinaryMethodSymbol3 });
					}
					else
					{
						MethodSymbol otherPartOfPartial3 = sourceOrdinaryMethodSymbol3.OtherPartOfPartial;
						if (otherPartOfPartial3 != null && MemberSignatureComparer.ConsideringTupleNamesCreatesDifference(sourceOrdinaryMethodSymbol3, otherPartOfPartial3))
						{
							diagnostics.Add(ErrorCode.ERR_PartialMethodInconsistentTupleNames, sourceOrdinaryMethodSymbol3.Locations[0], new object[] { sourceOrdinaryMethodSymbol3, sourceOrdinaryMethodSymbol3.OtherPartOfPartial });
						}
						else if (sourceOrdinaryMethodSymbol3 != null && sourceOrdinaryMethodSymbol3.IsPartialDefinition && sourceOrdinaryMethodSymbol3.OtherPartOfPartial == null && sourceOrdinaryMethodSymbol3.HasExplicitAccessModifier)
						{
							diagnostics.Add(ErrorCode.ERR_PartialMethodWithAccessibilityModsMustHaveImplementation, sourceOrdinaryMethodSymbol3.Locations[0], new object[] { sourceOrdinaryMethodSymbol3 });
						}
					}
				}
			}
			instance.Free();
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x0019F66C File Offset: 0x0019D86C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<Symbol> FixPartialMember([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> symbols, SourceOrdinaryMethodSymbol part1, SourceOrdinaryMethodSymbol part2)
		{
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol;
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol2;
			if (part1.IsPartialDefinition)
			{
				sourceOrdinaryMethodSymbol = part1;
				sourceOrdinaryMethodSymbol2 = part2;
			}
			else
			{
				sourceOrdinaryMethodSymbol = part2;
				sourceOrdinaryMethodSymbol2 = part1;
			}
			SourceOrdinaryMethodSymbol.InitializePartialMethodParts(sourceOrdinaryMethodSymbol, sourceOrdinaryMethodSymbol2);
			return SourceMemberContainerTypeSymbol.Remove(symbols, sourceOrdinaryMethodSymbol2);
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x0019F69C File Offset: 0x0019D89C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<Symbol> Remove([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> symbols, Symbol symbol)
		{
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			foreach (Symbol symbol2 in symbols)
			{
				if (symbol2 != symbol)
				{
					instance.Add(symbol2);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x0019F6DC File Offset: 0x0019D8DC
		private void CheckForMemberConflictWithPropertyAccessor(PropertySymbol propertySymbol, bool getNotSet, BindingDiagnosticBag diagnostics)
		{
			MethodSymbol methodSymbol = (getNotSet ? propertySymbol.GetMethod : propertySymbol.SetMethod);
			string text;
			if (methodSymbol != null)
			{
				text = methodSymbol.Name;
			}
			else
			{
				text = SourcePropertyAccessorSymbol.GetAccessorName(propertySymbol.IsIndexer ? propertySymbol.MetadataName : propertySymbol.Name, getNotSet, propertySymbol.IsCompilationOutputWinMdObj());
			}
			foreach (Symbol symbol in this.GetMembers(text))
			{
				if (symbol.Kind != SymbolKind.Method)
				{
					if (this.Locations.Length == 1 || this.IsPartial)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, SourceMemberContainerTypeSymbol.GetAccessorOrPropertyLocation(propertySymbol, getNotSet), new object[] { this, text });
					}
					return;
				}
				MethodSymbol methodSymbol2 = (MethodSymbol)symbol;
				if (methodSymbol2.MethodKind == MethodKind.Ordinary && SourceMemberContainerTypeSymbol.ParametersMatchPropertyAccessor(propertySymbol, getNotSet, methodSymbol2.Parameters))
				{
					diagnostics.Add(ErrorCode.ERR_MemberReserved, SourceMemberContainerTypeSymbol.GetAccessorOrPropertyLocation(propertySymbol, getNotSet), new object[] { text, this });
					return;
				}
			}
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x0019F7DC File Offset: 0x0019D9DC
		private void CheckForMemberConflictWithEventAccessor(EventSymbol eventSymbol, bool isAdder, BindingDiagnosticBag diagnostics)
		{
			string accessorName = SourceEventSymbol.GetAccessorName(eventSymbol.Name, isAdder);
			foreach (Symbol symbol in this.GetMembers(accessorName))
			{
				if (symbol.Kind != SymbolKind.Method)
				{
					if (this.Locations.Length == 1 || this.IsPartial)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateNameInClass, SourceMemberContainerTypeSymbol.GetAccessorOrEventLocation(eventSymbol, isAdder), new object[] { this, accessorName });
					}
					return;
				}
				MethodSymbol methodSymbol = (MethodSymbol)symbol;
				if (methodSymbol.MethodKind == MethodKind.Ordinary && SourceMemberContainerTypeSymbol.ParametersMatchEventAccessor(eventSymbol, methodSymbol.Parameters))
				{
					diagnostics.Add(ErrorCode.ERR_MemberReserved, SourceMemberContainerTypeSymbol.GetAccessorOrEventLocation(eventSymbol, isAdder), new object[] { accessorName, this });
					return;
				}
			}
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x0019F8A4 File Offset: 0x0019DAA4
		private static Location GetAccessorOrPropertyLocation(PropertySymbol propertySymbol, bool getNotSet)
		{
			return ((getNotSet ? propertySymbol.GetMethod : propertySymbol.SetMethod) ?? propertySymbol).Locations[0];
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x0019F8D8 File Offset: 0x0019DAD8
		private static Location GetAccessorOrEventLocation(EventSymbol propertySymbol, bool isAdder)
		{
			return ((isAdder ? propertySymbol.AddMethod : propertySymbol.RemoveMethod) ?? propertySymbol).Locations[0];
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x0019F90C File Offset: 0x0019DB0C
		private static bool ParametersMatchPropertyAccessor(PropertySymbol propertySymbol, bool getNotSet, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> methodParams)
		{
			ImmutableArray<ParameterSymbol> parameters = propertySymbol.Parameters;
			int num = parameters.Length + (getNotSet ? 0 : 1);
			if (num != methodParams.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				ParameterSymbol parameterSymbol = methodParams[i];
				if (parameterSymbol.RefKind != RefKind.None)
				{
					return false;
				}
				if (!((i == num - 1 && !getNotSet) ? propertySymbol.TypeWithAnnotations : parameters[i].TypeWithAnnotations).Type.Equals(parameterSymbol.Type, TypeCompareKind.AllIgnoreOptions))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x0019F994 File Offset: 0x0019DB94
		private static bool ParametersMatchEventAccessor(EventSymbol eventSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> methodParams)
		{
			return methodParams.Length == 1 && methodParams[0].RefKind == RefKind.None && eventSymbol.Type.Equals(methodParams[0].Type, TypeCompareKind.AllIgnoreOptions);
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x0019F9CC File Offset: 0x0019DBCC
		private void AddEnumMembers(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder result, EnumDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			SourceEnumConstantSymbol sourceEnumConstantSymbol = null;
			int num = 0;
			foreach (EnumMemberDeclarationSyntax enumMemberDeclarationSyntax in syntax.Members)
			{
				EqualsValueClauseSyntax equalsValue = enumMemberDeclarationSyntax.EqualsValue;
				SourceEnumConstantSymbol sourceEnumConstantSymbol2;
				if (equalsValue != null)
				{
					sourceEnumConstantSymbol2 = SourceEnumConstantSymbol.CreateExplicitValuedConstant(this, enumMemberDeclarationSyntax, diagnostics);
				}
				else
				{
					sourceEnumConstantSymbol2 = SourceEnumConstantSymbol.CreateImplicitValuedConstant(this, enumMemberDeclarationSyntax, sourceEnumConstantSymbol, num, diagnostics);
				}
				result.NonTypeMembers.Add(sourceEnumConstantSymbol2);
				if (equalsValue != null || sourceEnumConstantSymbol == null)
				{
					sourceEnumConstantSymbol = sourceEnumConstantSymbol2;
					num = 1;
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x0019FA41 File Offset: 0x0019DC41
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static void AddInitializer(ref ArrayBuilder<FieldOrPropertyInitializer> initializers, FieldSymbol fieldOpt, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode node)
		{
			if (initializers == null)
			{
				initializers = ArrayBuilder<FieldOrPropertyInitializer>.GetInstance();
			}
			else
			{
				int count = initializers.Count;
			}
			initializers.Add(new FieldOrPropertyInitializer(fieldOpt, node));
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x0019FA66 File Offset: 0x0019DC66
		private static void AddInitializers(ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>> allInitializers, [global::System.Runtime.CompilerServices.Nullable(2)] ArrayBuilder<FieldOrPropertyInitializer> siblingsOpt)
		{
			if (siblingsOpt != null)
			{
				allInitializers.Add(siblingsOpt);
			}
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x0019FA74 File Offset: 0x0019DC74
		private static void CheckInterfaceMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> nonTypeMembers, BindingDiagnosticBag diagnostics)
		{
			foreach (Symbol symbol in nonTypeMembers)
			{
				SourceMemberContainerTypeSymbol.CheckInterfaceMember(symbol, diagnostics);
			}
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x0019FAA4 File Offset: 0x0019DCA4
		private static void CheckInterfaceMember(Symbol member, BindingDiagnosticBag diagnostics)
		{
			SymbolKind kind = member.Kind;
			switch (kind)
			{
			case SymbolKind.Event:
			case SymbolKind.Field:
				return;
			case SymbolKind.Label:
			case SymbolKind.Local:
				break;
			case SymbolKind.Method:
			{
				MethodSymbol methodSymbol = (MethodSymbol)member;
				switch (methodSymbol.MethodKind)
				{
				case MethodKind.Constructor:
					diagnostics.Add(ErrorCode.ERR_InterfacesCantContainConstructors, member.Locations[0]);
					return;
				case MethodKind.Conversion:
					diagnostics.Add(ErrorCode.ERR_InterfacesCantContainConversionOrEqualityOperators, member.Locations[0]);
					return;
				case MethodKind.Destructor:
					diagnostics.Add(ErrorCode.ERR_OnlyClassesCanContainDestructors, member.Locations[0]);
					return;
				case MethodKind.EventAdd:
				case MethodKind.EventRemove:
				case MethodKind.ExplicitInterfaceImplementation:
				case MethodKind.Ordinary:
				case MethodKind.PropertyGet:
				case MethodKind.PropertySet:
				case MethodKind.StaticConstructor:
				case MethodKind.LocalFunction:
					return;
				case MethodKind.UserDefinedOperator:
					if (methodSymbol.Name == "op_Equality" || methodSymbol.Name == "op_Inequality")
					{
						diagnostics.Add(ErrorCode.ERR_InterfacesCantContainConversionOrEqualityOperators, member.Locations[0]);
						return;
					}
					return;
				}
				throw ExceptionUtilities.UnexpectedValue(methodSymbol.MethodKind);
			}
			default:
				if (kind == SymbolKind.Property)
				{
					return;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(member.Kind);
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x0019FBF8 File Offset: 0x0019DDF8
		private static void CheckForStructDefaultConstructors(ArrayBuilder<Symbol> members, bool isEnum, BindingDiagnosticBag diagnostics)
		{
			foreach (Symbol symbol in members)
			{
				MethodSymbol methodSymbol = symbol as MethodSymbol;
				if (methodSymbol != null && methodSymbol.MethodKind == MethodKind.Constructor && methodSymbol.ParameterCount == 0)
				{
					if (isEnum)
					{
						diagnostics.Add(ErrorCode.ERR_EnumsCantContainDefaultConstructor, methodSymbol.Locations[0]);
					}
					else
					{
						diagnostics.Add(ErrorCode.ERR_StructsCantContainDefaultConstructor, methodSymbol.Locations[0]);
					}
				}
			}
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x0019FC74 File Offset: 0x0019DE74
		private void CheckForStructBadInitializers(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder builder, BindingDiagnosticBag diagnostics)
		{
			if (builder.RecordDeclarationWithParameters != null)
			{
				return;
			}
			foreach (ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder in builder.InstanceInitializers)
			{
				foreach (FieldOrPropertyInitializer fieldOrPropertyInitializer in arrayBuilder)
				{
					diagnostics.Add(ErrorCode.ERR_FieldInitializerInStruct, (fieldOrPropertyInitializer.FieldOpt.AssociatedSymbol ?? fieldOrPropertyInitializer.FieldOpt).Locations[0], new object[] { this });
				}
			}
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x0019FCFC File Offset: 0x0019DEFC
		private void AddSynthesizedRecordMembersIfNecessary(SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder builder, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers, BindingDiagnosticBag diagnostics)
		{
			SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.diagnostics = diagnostics;
			CS$<>8__locals1.builder = builder;
			DeclarationKind kind = this.declaration.Kind;
			if (kind != DeclarationKind.Record && kind != DeclarationKind.RecordStruct)
			{
				return;
			}
			RecordDeclarationSyntax recordDeclarationWithParameters = declaredMembersAndInitializers.RecordDeclarationWithParameters;
			CS$<>8__locals1.paramList = ((recordDeclarationWithParameters != null) ? recordDeclarationWithParameters.ParameterList : null);
			CS$<>8__locals1.memberSignatures = SourceMemberContainerTypeSymbol.s_duplicateRecordMemberSignatureDictionary.Allocate();
			CS$<>8__locals1.fieldsByName = PooledDictionary<string, Symbol>.GetInstance();
			IReadOnlyCollection<Symbol> nonTypeMembers = CS$<>8__locals1.builder.GetNonTypeMembers(declaredMembersAndInitializers);
			CS$<>8__locals1.members = ArrayBuilder<Symbol>.GetInstance(nonTypeMembers.Count + 1);
			CS$<>8__locals1.memberNames = PooledHashSet<string>.GetInstance();
			foreach (Symbol symbol in nonTypeMembers)
			{
				CS$<>8__locals1.memberNames.Add(symbol.Name);
				if (!(symbol is EventSymbol))
				{
					MethodSymbol methodSymbol = symbol as MethodSymbol;
					if (methodSymbol != null)
					{
						MethodKind methodKind = methodSymbol.MethodKind;
						if (methodKind != MethodKind.Constructor && methodKind != MethodKind.Ordinary)
						{
							continue;
						}
					}
					else if (symbol is FieldSymbol)
					{
						string name = symbol.Name;
						if (!CS$<>8__locals1.fieldsByName.ContainsKey(name))
						{
							CS$<>8__locals1.fieldsByName.Add(name, symbol);
							continue;
						}
						continue;
					}
					if (!CS$<>8__locals1.memberSignatures.ContainsKey(symbol))
					{
						CS$<>8__locals1.memberSignatures.Add(symbol, symbol);
					}
				}
			}
			CS$<>8__locals1.compilation = this.DeclaringCompilation;
			CS$<>8__locals1.isRecordClass = this.declaration.Kind == DeclarationKind.Record;
			bool flag = false;
			if (CS$<>8__locals1.paramList != null)
			{
				SynthesizedRecordConstructor recordPrimaryConstructor = declaredMembersAndInitializers.RecordPrimaryConstructor;
				CS$<>8__locals1.members.Add(recordPrimaryConstructor);
				if (recordPrimaryConstructor.ParameterCount != 0)
				{
					ImmutableArray<Symbol> immutableArray = this.<AddSynthesizedRecordMembersIfNecessary>g__addProperties|172_5(recordPrimaryConstructor.Parameters, ref CS$<>8__locals1);
					this.<AddSynthesizedRecordMembersIfNecessary>g__addDeconstruct|172_0(recordPrimaryConstructor, immutableArray, ref CS$<>8__locals1);
				}
				if (CS$<>8__locals1.isRecordClass)
				{
					flag = recordPrimaryConstructor.ParameterCount == 1 && recordPrimaryConstructor.Parameters[0].Type.Equals(this, TypeCompareKind.AllIgnoreOptions);
				}
			}
			if (CS$<>8__locals1.isRecordClass)
			{
				this.<AddSynthesizedRecordMembersIfNecessary>g__addCopyCtor|172_1(flag, ref CS$<>8__locals1);
				this.<AddSynthesizedRecordMembersIfNecessary>g__addCloneMethod|172_2(ref CS$<>8__locals1);
			}
			PropertySymbol propertySymbol = (CS$<>8__locals1.isRecordClass ? this.<AddSynthesizedRecordMembersIfNecessary>g__addEqualityContract|172_8(ref CS$<>8__locals1) : null);
			MethodSymbol methodSymbol2 = this.<AddSynthesizedRecordMembersIfNecessary>g__addThisEquals|172_9(propertySymbol, ref CS$<>8__locals1);
			if (CS$<>8__locals1.isRecordClass)
			{
				this.<AddSynthesizedRecordMembersIfNecessary>g__addBaseEquals|172_11(ref CS$<>8__locals1);
			}
			this.<AddSynthesizedRecordMembersIfNecessary>g__addObjectEquals|172_6(methodSymbol2, ref CS$<>8__locals1);
			MethodSymbol methodSymbol3 = this.<AddSynthesizedRecordMembersIfNecessary>g__addGetHashCode|172_7(propertySymbol, ref CS$<>8__locals1);
			this.<AddSynthesizedRecordMembersIfNecessary>g__addEqualityOperators|172_12(ref CS$<>8__locals1);
			if (!(methodSymbol2 is SynthesizedRecordEquals) && methodSymbol3 is SynthesizedRecordGetHashCode)
			{
				CS$<>8__locals1.diagnostics.Add(ErrorCode.WRN_RecordEqualsWithoutGetHashCode, methodSymbol2.Locations[0], new object[] { this.declaration.Name });
			}
			MethodSymbol methodSymbol4 = this.<AddSynthesizedRecordMembersIfNecessary>g__addPrintMembersMethod|172_3(ref CS$<>8__locals1);
			this.<AddSynthesizedRecordMembersIfNecessary>g__addToStringMethod|172_4(methodSymbol4, ref CS$<>8__locals1);
			CS$<>8__locals1.memberSignatures.Free();
			CS$<>8__locals1.fieldsByName.Free();
			CS$<>8__locals1.memberNames.Free();
			CS$<>8__locals1.members.AddRange(nonTypeMembers);
			ArrayBuilder<Symbol> nonTypeMembers2 = CS$<>8__locals1.builder.NonTypeMembers;
			if (nonTypeMembers2 != null)
			{
				nonTypeMembers2.Free();
			}
			CS$<>8__locals1.builder.NonTypeMembers = CS$<>8__locals1.members;
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x001A0020 File Offset: 0x0019E220
		private void AddSynthesizedConstructorsIfNecessary(SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder builder, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers, BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (Symbol symbol in builder.GetNonTypeMembers(declaredMembersAndInitializers))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)symbol;
					MethodKind methodKind = methodSymbol.MethodKind;
					if (methodKind != MethodKind.Constructor)
					{
						if (methodKind == MethodKind.StaticConstructor)
						{
							flag3 = true;
						}
					}
					else if (!this.IsRecord || !SynthesizedRecordCopyCtor.HasCopyConstructorSignature(methodSymbol) || methodSymbol is SynthesizedRecordConstructor)
					{
						flag = true;
						flag2 = flag2 || methodSymbol.ParameterCount == 0;
					}
				}
				if (flag && flag3)
				{
					break;
				}
			}
			if ((!flag2 && this.IsStructType()) || (!flag && !this.IsStatic && !this.IsInterface))
			{
				builder.AddNonTypeMember((this.TypeKind == TypeKind.Submission) ? new SynthesizedSubmissionConstructor(this, diagnostics) : new SynthesizedInstanceConstructor(this), declaredMembersAndInitializers);
			}
			if (!flag3 && SourceMemberContainerTypeSymbol.<AddSynthesizedConstructorsIfNecessary>g__hasNonConstantInitializer|173_0(declaredMembersAndInitializers.StaticInitializers))
			{
				builder.AddNonTypeMember(new SynthesizedStaticConstructor(this), declaredMembersAndInitializers);
			}
			if (this.IsScriptClass)
			{
				SynthesizedInteractiveInitializerMethod synthesizedInteractiveInitializerMethod = new SynthesizedInteractiveInitializerMethod(this, diagnostics);
				builder.AddNonTypeMember(synthesizedInteractiveInitializerMethod, declaredMembersAndInitializers);
				SynthesizedEntryPointSymbol synthesizedEntryPointSymbol = SynthesizedEntryPointSymbol.Create(synthesizedInteractiveInitializerMethod, diagnostics);
				builder.AddNonTypeMember(synthesizedEntryPointSymbol, declaredMembersAndInitializers);
			}
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x001A0158 File Offset: 0x0019E358
		private void AddNonTypeMembers(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder builder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, BindingDiagnosticBag diagnostics)
		{
			if (members.Count == 0)
			{
				return;
			}
			MemberDeclarationSyntax memberDeclarationSyntax = members[0];
			Binder binder = this.GetBinder(memberDeclarationSyntax);
			ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder = null;
			ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder2 = null;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			foreach (MemberDeclarationSyntax memberDeclarationSyntax2 in members)
			{
				if (this._lazyMembersAndInitializers != null)
				{
					return;
				}
				bool flag = !memberDeclarationSyntax2.HasErrors;
				SyntaxKind syntaxKind = memberDeclarationSyntax2.Kind();
				if (syntaxKind != SyntaxKind.GlobalStatement)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.FieldDeclaration:
					{
						FieldDeclarationSyntax fieldDeclarationSyntax = (FieldDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = fieldDeclarationSyntax.Declaration.Variables.First().Identifier;
							diagnostics.Add(errorCode, new SourceLocation(in syntaxToken));
						}
						bool flag2;
						DeclarationModifiers declarationModifiers = SourceMemberFieldSymbol.MakeModifiers(this, fieldDeclarationSyntax.Declaration.Variables[0].Identifier, fieldDeclarationSyntax.Modifiers, diagnostics, out flag2);
						foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in fieldDeclarationSyntax.Declaration.Variables)
						{
							SourceMemberFieldSymbolFromDeclarator sourceMemberFieldSymbolFromDeclarator = (((declarationModifiers & DeclarationModifiers.Fixed) == DeclarationModifiers.None) ? new SourceMemberFieldSymbolFromDeclarator(this, variableDeclaratorSyntax, declarationModifiers, flag2, diagnostics) : new SourceFixedFieldSymbol(this, variableDeclaratorSyntax, declarationModifiers, flag2, diagnostics));
							builder.NonTypeMembers.Add(sourceMemberFieldSymbolFromDeclarator);
							builder.UpdateIsNullableEnabledForConstructorsAndFields(sourceMemberFieldSymbolFromDeclarator.IsStatic, declaringCompilation, variableDeclaratorSyntax);
							if (this.IsScriptClass)
							{
								ExpressionFieldFinder.FindExpressionVariables(builder.NonTypeMembers, variableDeclaratorSyntax, this, DeclarationModifiers.Private | (declarationModifiers & DeclarationModifiers.Static), sourceMemberFieldSymbolFromDeclarator);
							}
							if (variableDeclaratorSyntax.Initializer != null)
							{
								if (sourceMemberFieldSymbolFromDeclarator.IsStatic)
								{
									SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder, sourceMemberFieldSymbolFromDeclarator, variableDeclaratorSyntax.Initializer);
								}
								else
								{
									SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder2, sourceMemberFieldSymbolFromDeclarator, variableDeclaratorSyntax.Initializer);
								}
							}
						}
						break;
					}
					case SyntaxKind.EventFieldDeclaration:
					{
						EventFieldDeclarationSyntax eventFieldDeclarationSyntax = (EventFieldDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode2 = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = eventFieldDeclarationSyntax.Declaration.Variables.First().Identifier;
							diagnostics.Add(errorCode2, new SourceLocation(in syntaxToken));
						}
						foreach (VariableDeclaratorSyntax variableDeclaratorSyntax2 in eventFieldDeclarationSyntax.Declaration.Variables)
						{
							SourceFieldLikeEventSymbol sourceFieldLikeEventSymbol = new SourceFieldLikeEventSymbol(this, binder, eventFieldDeclarationSyntax.Modifiers, variableDeclaratorSyntax2, diagnostics);
							builder.NonTypeMembers.Add(sourceFieldLikeEventSymbol);
							FieldSymbol associatedField = sourceFieldLikeEventSymbol.AssociatedField;
							if (this.IsScriptClass)
							{
								ExpressionFieldFinder.FindExpressionVariables(builder.NonTypeMembers, variableDeclaratorSyntax2, this, DeclarationModifiers.Private | (sourceFieldLikeEventSymbol.IsStatic ? DeclarationModifiers.Static : DeclarationModifiers.None), associatedField);
							}
							if (associatedField != null)
							{
								builder.UpdateIsNullableEnabledForConstructorsAndFields(associatedField.IsStatic, declaringCompilation, variableDeclaratorSyntax2);
								if (variableDeclaratorSyntax2.Initializer != null)
								{
									if (associatedField.IsStatic)
									{
										SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder, associatedField, variableDeclaratorSyntax2.Initializer);
									}
									else
									{
										SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder2, associatedField, variableDeclaratorSyntax2.Initializer);
									}
								}
							}
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourceFieldLikeEventSymbol.AddMethod);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourceFieldLikeEventSymbol.RemoveMethod);
						}
						break;
					}
					case SyntaxKind.MethodDeclaration:
					{
						MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode3 = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = methodDeclarationSyntax.Identifier;
							diagnostics.Add(errorCode3, new SourceLocation(in syntaxToken));
						}
						SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = SourceOrdinaryMethodSymbol.CreateMethodSymbol(this, binder, methodDeclarationSyntax, declaringCompilation.IsNullableAnalysisEnabledIn(methodDeclarationSyntax), diagnostics);
						builder.NonTypeMembers.Add(sourceOrdinaryMethodSymbol);
						break;
					}
					case SyntaxKind.OperatorDeclaration:
					{
						OperatorDeclarationSyntax operatorDeclarationSyntax = (OperatorDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode4 = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = operatorDeclarationSyntax.OperatorKeyword;
							diagnostics.Add(errorCode4, new SourceLocation(in syntaxToken));
						}
						SourceUserDefinedOperatorSymbol sourceUserDefinedOperatorSymbol = SourceUserDefinedOperatorSymbol.CreateUserDefinedOperatorSymbol(this, operatorDeclarationSyntax, declaringCompilation.IsNullableAnalysisEnabledIn(operatorDeclarationSyntax), diagnostics);
						builder.NonTypeMembers.Add(sourceUserDefinedOperatorSymbol);
						break;
					}
					case SyntaxKind.ConversionOperatorDeclaration:
					{
						ConversionOperatorDeclarationSyntax conversionOperatorDeclarationSyntax = (ConversionOperatorDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode5 = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = conversionOperatorDeclarationSyntax.OperatorKeyword;
							diagnostics.Add(errorCode5, new SourceLocation(in syntaxToken));
						}
						SourceUserDefinedConversionSymbol sourceUserDefinedConversionSymbol = SourceUserDefinedConversionSymbol.CreateUserDefinedConversionSymbol(this, conversionOperatorDeclarationSyntax, declaringCompilation.IsNullableAnalysisEnabledIn(conversionOperatorDeclarationSyntax), diagnostics);
						builder.NonTypeMembers.Add(sourceUserDefinedConversionSymbol);
						break;
					}
					case SyntaxKind.ConstructorDeclaration:
					{
						ConstructorDeclarationSyntax constructorDeclarationSyntax = (ConstructorDeclarationSyntax)memberDeclarationSyntax2;
						if (this.IsImplicitClass && flag)
						{
							ErrorCode errorCode6 = ErrorCode.ERR_NamespaceUnexpected;
							SyntaxToken syntaxToken = constructorDeclarationSyntax.Identifier;
							diagnostics.Add(errorCode6, new SourceLocation(in syntaxToken));
						}
						bool flag3 = declaringCompilation.IsNullableAnalysisEnabledIn(constructorDeclarationSyntax);
						SourceConstructorSymbol sourceConstructorSymbol = SourceConstructorSymbol.CreateConstructorSymbol(this, constructorDeclarationSyntax, flag3, diagnostics);
						builder.NonTypeMembers.Add(sourceConstructorSymbol);
						ConstructorInitializerSyntax initializer = constructorDeclarationSyntax.Initializer;
						if (initializer == null || initializer.Kind() != SyntaxKind.ThisConstructorInitializer)
						{
							builder.UpdateIsNullableEnabledForConstructorsAndFields(sourceConstructorSymbol.IsStatic, flag3);
						}
						break;
					}
					default:
						switch (syntaxKind)
						{
						case SyntaxKind.DestructorDeclaration:
						{
							DestructorDeclarationSyntax destructorDeclarationSyntax = (DestructorDeclarationSyntax)memberDeclarationSyntax2;
							if (this.IsImplicitClass && flag)
							{
								ErrorCode errorCode7 = ErrorCode.ERR_NamespaceUnexpected;
								SyntaxToken syntaxToken = destructorDeclarationSyntax.Identifier;
								diagnostics.Add(errorCode7, new SourceLocation(in syntaxToken));
							}
							SourceDestructorSymbol sourceDestructorSymbol = new SourceDestructorSymbol(this, destructorDeclarationSyntax, declaringCompilation.IsNullableAnalysisEnabledIn(destructorDeclarationSyntax), diagnostics);
							builder.NonTypeMembers.Add(sourceDestructorSymbol);
							break;
						}
						case SyntaxKind.PropertyDeclaration:
						{
							PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)memberDeclarationSyntax2;
							if (this.IsImplicitClass && flag)
							{
								ErrorCode errorCode8 = ErrorCode.ERR_NamespaceUnexpected;
								SyntaxToken syntaxToken = propertyDeclarationSyntax.Identifier;
								diagnostics.Add(errorCode8, new SourceLocation(in syntaxToken));
							}
							SourcePropertySymbol sourcePropertySymbol = SourcePropertySymbol.Create(this, binder, propertyDeclarationSyntax, diagnostics);
							builder.NonTypeMembers.Add(sourcePropertySymbol);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourcePropertySymbol.GetMethod);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourcePropertySymbol.SetMethod);
							FieldSymbol backingField = sourcePropertySymbol.BackingField;
							if (backingField != null)
							{
								builder.NonTypeMembers.Add(backingField);
								builder.UpdateIsNullableEnabledForConstructorsAndFields(backingField.IsStatic, declaringCompilation, propertyDeclarationSyntax);
								EqualsValueClauseSyntax initializer2 = propertyDeclarationSyntax.Initializer;
								if (initializer2 != null)
								{
									if (this.IsScriptClass)
									{
										ExpressionFieldFinder.FindExpressionVariables(builder.NonTypeMembers, initializer2, this, DeclarationModifiers.Private | (sourcePropertySymbol.IsStatic ? DeclarationModifiers.Static : DeclarationModifiers.None), backingField);
									}
									if (sourcePropertySymbol.IsStatic)
									{
										SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder, backingField, initializer2);
									}
									else
									{
										SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder2, backingField, initializer2);
									}
								}
							}
							break;
						}
						case SyntaxKind.EventDeclaration:
						{
							EventDeclarationSyntax eventDeclarationSyntax = (EventDeclarationSyntax)memberDeclarationSyntax2;
							if (this.IsImplicitClass && flag)
							{
								ErrorCode errorCode9 = ErrorCode.ERR_NamespaceUnexpected;
								SyntaxToken syntaxToken = eventDeclarationSyntax.Identifier;
								diagnostics.Add(errorCode9, new SourceLocation(in syntaxToken));
							}
							SourceCustomEventSymbol sourceCustomEventSymbol = new SourceCustomEventSymbol(this, binder, eventDeclarationSyntax, diagnostics);
							builder.NonTypeMembers.Add(sourceCustomEventSymbol);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourceCustomEventSymbol.AddMethod);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourceCustomEventSymbol.RemoveMethod);
							break;
						}
						case SyntaxKind.IndexerDeclaration:
						{
							IndexerDeclarationSyntax indexerDeclarationSyntax = (IndexerDeclarationSyntax)memberDeclarationSyntax2;
							if (this.IsImplicitClass && flag)
							{
								ErrorCode errorCode10 = ErrorCode.ERR_NamespaceUnexpected;
								SyntaxToken syntaxToken = indexerDeclarationSyntax.ThisKeyword;
								diagnostics.Add(errorCode10, new SourceLocation(in syntaxToken));
							}
							SourcePropertySymbol sourcePropertySymbol2 = SourcePropertySymbol.Create(this, binder, indexerDeclarationSyntax, diagnostics);
							builder.HaveIndexers = true;
							builder.NonTypeMembers.Add(sourcePropertySymbol2);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourcePropertySymbol2.GetMethod);
							this.AddAccessorIfAvailable(builder.NonTypeMembers, sourcePropertySymbol2.SetMethod);
							break;
						}
						}
						break;
					}
				}
				else
				{
					StatementSyntax statement = ((GlobalStatementSyntax)memberDeclarationSyntax2).Statement;
					if (this.IsScriptClass)
					{
						StatementSyntax statementSyntax = statement;
						while (statementSyntax.Kind() == SyntaxKind.LabeledStatement)
						{
							statementSyntax = ((LabeledStatementSyntax)statementSyntax).Statement;
						}
						SyntaxKind syntaxKind2 = statementSyntax.Kind();
						if (syntaxKind2 <= SyntaxKind.YieldReturnStatement)
						{
							if (syntaxKind2 != SyntaxKind.LocalDeclarationStatement)
							{
								if (syntaxKind2 == SyntaxKind.ExpressionStatement || syntaxKind2 - SyntaxKind.ReturnStatement <= 1)
								{
									goto IL_07B4;
								}
							}
							else
							{
								foreach (VariableDeclaratorSyntax variableDeclaratorSyntax3 in ((LocalDeclarationStatementSyntax)statementSyntax).Declaration.Variables)
								{
									ExpressionFieldFinder.FindExpressionVariables(builder.NonTypeMembers, variableDeclaratorSyntax3, this, DeclarationModifiers.Private, null);
								}
							}
						}
						else if (syntaxKind2 == SyntaxKind.ThrowStatement || syntaxKind2 - SyntaxKind.LockStatement <= 1 || syntaxKind2 == SyntaxKind.SwitchStatement)
						{
							goto IL_07B4;
						}
						IL_07C8:
						SourceMemberContainerTypeSymbol.AddInitializer(ref arrayBuilder2, null, statement);
						continue;
						IL_07B4:
						ExpressionFieldFinder.FindExpressionVariables(builder.NonTypeMembers, statementSyntax, this, DeclarationModifiers.Private, null);
						goto IL_07C8;
					}
					if (flag && !SyntaxFacts.IsSimpleProgramTopLevelStatement((GlobalStatementSyntax)memberDeclarationSyntax2))
					{
						diagnostics.Add(ErrorCode.ERR_GlobalStatement, new SourceLocation(statement));
					}
				}
			}
			SourceMemberContainerTypeSymbol.AddInitializers(builder.InstanceInitializers, arrayBuilder2);
			SourceMemberContainerTypeSymbol.AddInitializers(builder.StaticInitializers, arrayBuilder);
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x001A0982 File Offset: 0x0019EB82
		private void AddAccessorIfAvailable(ArrayBuilder<Symbol> symbols, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol accessorOpt)
		{
			if (accessorOpt != null)
			{
				symbols.Add(accessorOpt);
			}
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x001A0990 File Offset: 0x0019EB90
		internal override byte? GetLocalNullableContextValue()
		{
			byte? b;
			if (!this._flags.TryGetNullableContext(out b))
			{
				b = this.ComputeNullableContextValue();
				this._flags.SetNullableContext(b);
			}
			return b;
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x001A09C4 File Offset: 0x0019EBC4
		private byte? ComputeNullableContextValue()
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (!declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				return default(byte?);
			}
			MostCommonNullableValueBuilder mostCommonNullableValueBuilder = default(MostCommonNullableValueBuilder);
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics != null)
			{
				mostCommonNullableValueBuilder.AddValue(TypeWithAnnotations.Create(baseTypeNoUseSiteDiagnostics, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			foreach (NamedTypeSymbol namedTypeSymbol in this.GetInterfacesToEmit())
			{
				mostCommonNullableValueBuilder.AddValue(TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
			{
				typeParameterSymbol.GetCommonNullableValues(declaringCompilation, ref mostCommonNullableValueBuilder);
			}
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				symbol.GetCommonNullableValues(declaringCompilation, ref mostCommonNullableValueBuilder);
			}
			return mostCommonNullableValueBuilder.MostCommonValue;
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x001A0AB0 File Offset: 0x0019ECB0
		internal bool IsNullableEnabledForConstructorsAndInitializers(bool useStatic)
		{
			SourceMemberContainerTypeSymbol.MembersAndInitializers membersAndInitializers = this.GetMembersAndInitializers();
			if (!useStatic)
			{
				return membersAndInitializers.IsNullableEnabledForInstanceConstructorsAndFields;
			}
			return membersAndInitializers.IsNullableEnabledForStaticConstructorsAndFields;
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x001A0AD4 File Offset: 0x0019ECD4
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics != null)
			{
				if (baseTypeNoUseSiteDiagnostics.ContainsDynamic())
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(baseTypeNoUseSiteDiagnostics, 0, RefKind.None));
				}
				if (baseTypeNoUseSiteDiagnostics.ContainsNativeInteger())
				{
					Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, baseTypeNoUseSiteDiagnostics));
				}
				if (baseTypeNoUseSiteDiagnostics.ContainsTupleNames())
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(baseTypeNoUseSiteDiagnostics));
				}
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				byte b;
				if (base.ShouldEmitNullableContextValue(out b))
				{
					Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableContextAttribute(this, b));
				}
				if (baseTypeNoUseSiteDiagnostics != null)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, new byte?(b), TypeWithAnnotations.Create(baseTypeNoUseSiteDiagnostics, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))));
				}
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x060070E3 RID: 28899 RVA: 0x001A0B84 File Offset: 0x0019ED84
		internal bool ContainsExtensionMethods
		{
			get
			{
				if (!this._lazyContainsExtensionMethods.HasValue())
				{
					bool flag = ((this.IsStatic && !base.IsGenericType) || this.IsScriptClass) && this.declaration.ContainsExtensionMethods;
					this._lazyContainsExtensionMethods = flag.ToThreeState();
				}
				return this._lazyContainsExtensionMethods.Value();
			}
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x060070E4 RID: 28900 RVA: 0x001A0BDC File Offset: 0x0019EDDC
		internal bool AnyMemberHasAttributes
		{
			get
			{
				if (!this._lazyAnyMemberHasAttributes.HasValue())
				{
					bool anyMemberHasAttributes = this.declaration.AnyMemberHasAttributes;
					this._lazyAnyMemberHasAttributes = anyMemberHasAttributes.ToThreeState();
				}
				return this._lazyAnyMemberHasAttributes.Value();
			}
		}

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x060070E5 RID: 28901 RVA: 0x001A0C19 File Offset: 0x0019EE19
		public override bool MightContainExtensionMethods
		{
			get
			{
				return this.ContainsExtensionMethods;
			}
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x060070E6 RID: 28902 RVA: 0x001A0C21 File Offset: 0x0019EE21
		public sealed override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x001A0C24 File Offset: 0x0019EE24
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal ImmutableArray<SynthesizedExplicitImplementationForwardingMethod> GetSynthesizedExplicitImplementations(CancellationToken cancellationToken)
		{
			if (this._lazySynthesizedExplicitImplementations.IsDefault)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					this.CheckMembersAgainstBaseType(instance, cancellationToken);
					cancellationToken.ThrowIfCancellationRequested();
					this.CheckAbstractClassImplementations(instance);
					cancellationToken.ThrowIfCancellationRequested();
					this.CheckInterfaceUnification(instance);
					if (this.IsInterface)
					{
						cancellationToken.ThrowIfCancellationRequested();
						this.CheckInterfaceVarianceSafety(instance);
					}
					if (ImmutableInterlocked.InterlockedCompareExchange<SynthesizedExplicitImplementationForwardingMethod>(ref this._lazySynthesizedExplicitImplementations, this.ComputeInterfaceImplementations(instance, cancellationToken), default(ImmutableArray<SynthesizedExplicitImplementationForwardingMethod>)).IsDefault)
					{
						this.AddDeclarationDiagnostics(instance);
						this.state.NotePartComplete(CompletionPart.SynthesizedExplicitImplementations);
					}
				}
				finally
				{
					instance.Free();
				}
			}
			return this._lazySynthesizedExplicitImplementations;
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x001A0CE8 File Offset: 0x0019EEE8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void CheckAbstractClassImplementations(BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			if (this.IsAbstract || baseTypeNoUseSiteDiagnostics == null || !baseTypeNoUseSiteDiagnostics.IsAbstract)
			{
				return;
			}
			foreach (Symbol symbol in base.AbstractMembers)
			{
				if (symbol.Kind == SymbolKind.Method && !(symbol is SynthesizedRecordOrdinaryMethod))
				{
					diagnostics.Add(ErrorCode.ERR_UnimplementedAbstractMethod, this.Locations[0], new object[] { this, symbol });
				}
			}
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x001A0D8C File Offset: 0x0019EF8C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private ImmutableArray<SynthesizedExplicitImplementationForwardingMethod> ComputeInterfaceImplementations(BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			ArrayBuilder<SynthesizedExplicitImplementationForwardingMethod> instance = ArrayBuilder<SynthesizedExplicitImplementationForwardingMethod>.GetInstance();
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics = base.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics;
			foreach (NamedTypeSymbol namedTypeSymbol in base.AllInterfacesNoUseSiteDiagnostics)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics[namedTypeSymbol].Contains(namedTypeSymbol))
				{
					SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult? hasBaseTypeDeclaringInterfaceResult = default(SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult?);
					foreach (Symbol symbol in namedTypeSymbol.GetMembersUnordered())
					{
						cancellationToken.ThrowIfCancellationRequested();
						SymbolKind kind = symbol.Kind;
						if ((kind == SymbolKind.Event || kind == SymbolKind.Method || kind == SymbolKind.Property) && symbol.IsImplementableInterfaceMember())
						{
							TypeSymbol.SymbolAndDiagnostics symbolAndDiagnostics;
							if (this.IsInterface)
							{
								MultiDictionary<Symbol, Symbol>.ValueSet explicitImplementationForInterfaceMember = base.GetExplicitImplementationForInterfaceMember(symbol);
								int count = explicitImplementationForInterfaceMember.Count;
								if (count == 0)
								{
									continue;
								}
								if (count == 1)
								{
									symbolAndDiagnostics = new TypeSymbol.SymbolAndDiagnostics(explicitImplementationForInterfaceMember.Single(), ImmutableBindingDiagnostic<AssemblySymbol>.Empty);
								}
								else
								{
									Diagnostic diagnostic = new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_DuplicateExplicitImpl, new object[] { symbol }), this.Locations[0], false);
									symbolAndDiagnostics = new TypeSymbol.SymbolAndDiagnostics(null, new ImmutableBindingDiagnostic<AssemblySymbol>(ImmutableArray.Create<Diagnostic>(diagnostic), default(ImmutableArray<AssemblySymbol>)));
								}
							}
							else
							{
								symbolAndDiagnostics = base.FindImplementationForInterfaceMemberInNonInterfaceWithDiagnostics(symbol, false);
							}
							Symbol symbol2 = symbolAndDiagnostics.Symbol;
							SynthesizedExplicitImplementationForwardingMethod synthesizedExplicitImplementationForwardingMethod = this.SynthesizeInterfaceMemberImplementation(symbolAndDiagnostics, symbol);
							bool flag = symbol2 != null;
							if (synthesizedExplicitImplementationForwardingMethod != null)
							{
								if (synthesizedExplicitImplementationForwardingMethod.IsVararg)
								{
									diagnostics.Add(ErrorCode.ERR_InterfaceImplementedImplicitlyByVariadic, TypeSymbol.GetImplicitImplementationDiagnosticLocation(symbol, this, symbol2), new object[] { symbol2, symbol, this });
								}
								else
								{
									instance.Add(synthesizedExplicitImplementationForwardingMethod);
								}
							}
							if (flag && kind == SymbolKind.Event)
							{
								EventSymbol eventSymbol = (EventSymbol)symbol;
								EventSymbol eventSymbol2 = (EventSymbol)symbol2;
								EventSymbol eventSymbol3;
								EventSymbol eventSymbol4;
								if (eventSymbol.IsWindowsRuntimeEvent)
								{
									eventSymbol3 = eventSymbol;
									eventSymbol4 = eventSymbol2;
								}
								else
								{
									eventSymbol3 = eventSymbol2;
									eventSymbol4 = eventSymbol;
								}
								if (eventSymbol.IsWindowsRuntimeEvent != eventSymbol2.IsWindowsRuntimeEvent)
								{
									object[] array = new object[] { eventSymbol2, eventSymbol, eventSymbol3, eventSymbol4 };
									CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_MixingWinRTEventWithRegular, array, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<Location>(this.Locations[0]));
									diagnostics.Add(csdiagnosticInfo, eventSymbol2.Locations[0]);
								}
							}
							Symbol symbol3 = ((kind == SymbolKind.Method) ? ((MethodSymbol)symbol).AssociatedSymbol : null);
							if (symbol3 == null || this.ReportAccessorOfInterfacePropertyOrEvent(symbol3) || (flag && !symbol2.IsAccessor()))
							{
								bool flag2 = false;
								if (symbolAndDiagnostics.Diagnostics.Diagnostics.Any<Diagnostic>())
								{
									diagnostics.AddRange(symbolAndDiagnostics.Diagnostics, false);
									flag2 = symbolAndDiagnostics.Diagnostics.Diagnostics.Any<Diagnostic>((Diagnostic d) => d.Severity == DiagnosticSeverity.Error);
								}
								if (!flag2)
								{
									if (!flag || (!symbol2.ContainingType.Equals(this, TypeCompareKind.ConsiderEverything) && symbol2.GetExplicitInterfaceImplementations().Contains(symbol, TypeSymbol.ExplicitInterfaceImplementationTargetMemberEqualityComparer.Instance)))
									{
										hasBaseTypeDeclaringInterfaceResult = new SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult?(hasBaseTypeDeclaringInterfaceResult ?? this.HasBaseClassDeclaringInterface(namedTypeSymbol));
										SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult valueOrDefault = hasBaseTypeDeclaringInterfaceResult.GetValueOrDefault();
										if (valueOrDefault != SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.ExactMatch && flag && symbol2.ContainingType.IsInterface)
										{
											this.HasBaseInterfaceDeclaringInterface(symbol2.ContainingType, namedTypeSymbol, ref valueOrDefault);
										}
										switch (valueOrDefault)
										{
										case SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.NoMatch:
											if (!symbol.MustCallMethodsDirectly() && !symbol.IsIndexedProperty())
											{
												DiagnosticInfo diagnosticInfo = symbol.GetUseSiteInfo().DiagnosticInfo;
												if (diagnosticInfo != null && diagnosticInfo.DefaultSeverity == DiagnosticSeverity.Error)
												{
													diagnostics.Add(diagnosticInfo, this.GetImplementsLocationOrFallback(namedTypeSymbol));
												}
												else
												{
													diagnostics.Add(ErrorCode.ERR_UnimplementedInterfaceMember, this.GetImplementsLocationOrFallback(namedTypeSymbol), new object[] { this, symbol });
												}
											}
											break;
										case SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.IgnoringNullableMatch:
											diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInInterfaceImplementedByBase, this.GetImplementsLocationOrFallback(namedTypeSymbol), new object[] { this, symbol });
											break;
										case SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.ExactMatch:
											break;
										default:
											throw ExceptionUtilities.UnexpectedValue(valueOrDefault);
										}
									}
									if (flag && kind == SymbolKind.Method && (synthesizedExplicitImplementationForwardingMethod != null || TypeSymbol.Equals(symbol2.ContainingType, this, TypeCompareKind.ConsiderEverything)))
									{
										UseSiteInfo<AssemblySymbol> useSiteInfo = symbol.GetUseSiteInfo();
										Location location = (symbol2.IsFromCompilation(this.DeclaringCompilation) ? symbol2.Locations[0] : this.Locations[0]);
										diagnostics.Add(useSiteInfo, location);
									}
								}
							}
						}
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060070EA RID: 28906
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected abstract Location GetCorrespondingBaseListLocation(NamedTypeSymbol @base);

		// Token: 0x060070EB RID: 28907 RVA: 0x001A1234 File Offset: 0x0019F434
		private Location GetImplementsLocationOrFallback(NamedTypeSymbol implementedInterface)
		{
			return this.GetImplementsLocation(implementedInterface) ?? this.Locations[0];
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x001A125C File Offset: 0x0019F45C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal Location GetImplementsLocation(NamedTypeSymbol implementedInterface)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			NamedTypeSymbol namedTypeSymbol = null;
			foreach (NamedTypeSymbol namedTypeSymbol2 in this.InterfacesNoUseSiteDiagnostics(null))
			{
				if (TypeSymbol.Equals(namedTypeSymbol2, implementedInterface, TypeCompareKind.ConsiderEverything))
				{
					namedTypeSymbol = namedTypeSymbol2;
					break;
				}
				if (namedTypeSymbol == null && namedTypeSymbol2.ImplementsInterface(implementedInterface, ref discarded))
				{
					namedTypeSymbol = namedTypeSymbol2;
				}
			}
			return this.GetCorrespondingBaseListLocation(namedTypeSymbol);
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x001A12C0 File Offset: 0x0019F4C0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private bool ReportAccessorOfInterfacePropertyOrEvent(Symbol interfacePropertyOrEvent)
		{
			if (interfacePropertyOrEvent.IsIndexedProperty())
			{
				return true;
			}
			Symbol symbol;
			if (this.IsInterface)
			{
				MultiDictionary<Symbol, Symbol>.ValueSet explicitImplementationForInterfaceMember = base.GetExplicitImplementationForInterfaceMember(interfacePropertyOrEvent);
				int count = explicitImplementationForInterfaceMember.Count;
				if (count == 0)
				{
					return true;
				}
				if (count != 1)
				{
					symbol = null;
				}
				else
				{
					symbol = explicitImplementationForInterfaceMember.Single();
				}
			}
			else
			{
				symbol = base.FindImplementationForInterfaceMemberInNonInterface(interfacePropertyOrEvent, false);
			}
			return symbol != null && (interfacePropertyOrEvent.Kind != SymbolKind.Event || symbol.Kind != SymbolKind.Event || ((EventSymbol)interfacePropertyOrEvent).IsWindowsRuntimeEvent == ((EventSymbol)symbol).IsWindowsRuntimeEvent);
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x001A1344 File Offset: 0x0019F544
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult HasBaseClassDeclaringInterface(NamedTypeSymbol @interface)
		{
			SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult hasBaseTypeDeclaringInterfaceResult = SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.NoMatch;
			NamedTypeSymbol namedTypeSymbol = this.BaseTypeNoUseSiteDiagnostics;
			while (namedTypeSymbol != null && !SourceMemberContainerTypeSymbol.DeclaresBaseInterface(namedTypeSymbol, @interface, ref hasBaseTypeDeclaringInterfaceResult))
			{
				namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
			}
			return hasBaseTypeDeclaringInterfaceResult;
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x001A1374 File Offset: 0x0019F574
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool DeclaresBaseInterface(NamedTypeSymbol currType, NamedTypeSymbol @interface, ref SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult result)
		{
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>.ValueSet valueSet = currType.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics[@interface];
			if (valueSet.Count != 0)
			{
				if (valueSet.Contains(@interface))
				{
					result = SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.ExactMatch;
					return true;
				}
				if (result == SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.NoMatch && valueSet.Contains(@interface, SymbolEqualityComparer.IgnoringNullable))
				{
					result = SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult.IgnoringNullableMatch;
				}
			}
			return false;
		}

		// Token: 0x060070F0 RID: 28912 RVA: 0x001A13C0 File Offset: 0x0019F5C0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void HasBaseInterfaceDeclaringInterface(NamedTypeSymbol baseInterface, NamedTypeSymbol @interface, ref SourceMemberContainerTypeSymbol.HasBaseTypeDeclaringInterfaceResult matchResult)
		{
			if (SourceMemberContainerTypeSymbol.DeclaresBaseInterface(baseInterface, @interface, ref matchResult))
			{
				return;
			}
			foreach (NamedTypeSymbol namedTypeSymbol in base.AllInterfacesNoUseSiteDiagnostics)
			{
				if (namedTypeSymbol != baseInterface && namedTypeSymbol.Equals(baseInterface, TypeCompareKind.CLRSignatureCompareOptions) && SourceMemberContainerTypeSymbol.DeclaresBaseInterface(namedTypeSymbol, @interface, ref matchResult))
				{
					return;
				}
			}
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x001A1414 File Offset: 0x0019F614
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void CheckMembersAgainstBaseType(BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			switch (this.TypeKind)
			{
			case TypeKind.Class:
			case TypeKind.Interface:
			case TypeKind.Struct:
			case TypeKind.Submission:
				foreach (Symbol symbol in this.GetMembersUnordered())
				{
					cancellationToken.ThrowIfCancellationRequested();
					SymbolKind kind = symbol.Kind;
					switch (kind)
					{
					case SymbolKind.Event:
					{
						EventSymbol eventSymbol = (EventSymbol)symbol;
						MethodSymbol addMethod = eventSymbol.AddMethod;
						MethodSymbol removeMethod = eventSymbol.RemoveMethod;
						if (symbol.IsOverride)
						{
							bool flag;
							this.CheckOverrideMember(eventSymbol, eventSymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
							if (!flag)
							{
								if (addMethod != null)
								{
									this.CheckOverrideMember(addMethod, addMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
								}
								if (removeMethod != null)
								{
									this.CheckOverrideMember(removeMethod, removeMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
								}
							}
						}
						else
						{
							bool isNew = ((SourceEventSymbol)eventSymbol).IsNew;
							bool flag;
							SourceMemberContainerTypeSymbol.CheckNonOverrideMember(eventSymbol, isNew, eventSymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
							if (!flag)
							{
								if (addMethod != null)
								{
									SourceMemberContainerTypeSymbol.CheckNonOverrideMember(addMethod, isNew, addMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
								}
								if (removeMethod != null)
								{
									SourceMemberContainerTypeSymbol.CheckNonOverrideMember(removeMethod, isNew, removeMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
								}
							}
						}
						break;
					}
					case SymbolKind.Field:
					{
						SourceFieldSymbol sourceFieldSymbol = symbol as SourceFieldSymbol;
						bool flag2 = sourceFieldSymbol != null && sourceFieldSymbol.IsNew;
						this.CheckNewModifier(symbol, flag2, diagnostics);
						break;
					}
					case SymbolKind.Label:
					case SymbolKind.Local:
					case SymbolKind.NetModule:
						break;
					case SymbolKind.Method:
					{
						MethodSymbol methodSymbol = (MethodSymbol)symbol;
						if (MethodSymbol.CanOverrideOrHide(methodSymbol.MethodKind) && !methodSymbol.IsAccessor())
						{
							if (symbol.IsOverride)
							{
								bool flag;
								this.CheckOverrideMember(methodSymbol, methodSymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
							}
							else
							{
								SourceMemberMethodSymbol sourceMemberMethodSymbol = methodSymbol as SourceMemberMethodSymbol;
								if (sourceMemberMethodSymbol != null)
								{
									bool isNew2 = sourceMemberMethodSymbol.IsNew;
									bool flag;
									SourceMemberContainerTypeSymbol.CheckNonOverrideMember(methodSymbol, isNew2, methodSymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
								}
							}
						}
						else if (methodSymbol.MethodKind == MethodKind.Destructor)
						{
							bool flag3;
							MethodSymbol firstRuntimeOverriddenMethodIgnoringNewSlot = methodSymbol.GetFirstRuntimeOverriddenMethodIgnoringNewSlot(out flag3);
							if (firstRuntimeOverriddenMethodIgnoringNewSlot != null && firstRuntimeOverriddenMethodIgnoringNewSlot.IsMetadataFinal)
							{
								diagnostics.Add(ErrorCode.ERR_CantOverrideSealed, methodSymbol.Locations[0], new object[] { methodSymbol, firstRuntimeOverriddenMethodIgnoringNewSlot });
							}
						}
						break;
					}
					case SymbolKind.NamedType:
						this.CheckNewModifier(symbol, ((SourceMemberContainerTypeSymbol)symbol).IsNew, diagnostics);
						break;
					default:
						if (kind == SymbolKind.Property)
						{
							PropertySymbol propertySymbol = (PropertySymbol)symbol;
							MethodSymbol getMethod = propertySymbol.GetMethod;
							MethodSymbol setMethod = propertySymbol.SetMethod;
							if (symbol.IsOverride)
							{
								bool flag;
								this.CheckOverrideMember(propertySymbol, propertySymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
								if (!flag)
								{
									if (getMethod != null)
									{
										this.CheckOverrideMember(getMethod, getMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
									}
									if (setMethod != null)
									{
										this.CheckOverrideMember(setMethod, setMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
									}
								}
							}
							else
							{
								SourcePropertySymbolBase sourcePropertySymbolBase = propertySymbol as SourcePropertySymbolBase;
								if (sourcePropertySymbolBase != null)
								{
									bool isNew3 = sourcePropertySymbolBase.IsNew;
									bool flag;
									SourceMemberContainerTypeSymbol.CheckNonOverrideMember(propertySymbol, isNew3, propertySymbol.OverriddenOrHiddenMembers, diagnostics, out flag);
									if (!flag)
									{
										if (getMethod != null)
										{
											SourceMemberContainerTypeSymbol.CheckNonOverrideMember(getMethod, isNew3, getMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
										}
										if (setMethod != null)
										{
											SourceMemberContainerTypeSymbol.CheckNonOverrideMember(setMethod, isNew3, setMethod.OverriddenOrHiddenMembers, diagnostics, out flag);
										}
									}
								}
							}
						}
						break;
					}
				}
				return;
			case TypeKind.Delegate:
			case TypeKind.Enum:
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(this.TypeKind);
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x001A177C File Offset: 0x0019F97C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void CheckNewModifier(Symbol symbol, bool isNew, BindingDiagnosticBag diagnostics)
		{
			if (symbol.IsImplicitlyDeclared)
			{
				return;
			}
			if (symbol.ContainingType.IsInterface)
			{
				bool flag;
				SourceMemberContainerTypeSymbol.CheckNonOverrideMember(symbol, isNew, OverriddenOrHiddenMembersHelpers.MakeInterfaceOverriddenOrHiddenMembers(symbol, true), diagnostics, out flag);
				return;
			}
			if (this.BaseTypeNoUseSiteDiagnostics == null)
			{
				return;
			}
			int memberArity = symbol.GetMemberArity();
			Location location = symbol.Locations.FirstOrDefault<Location>();
			bool flag2 = false;
			for (NamedTypeSymbol namedTypeSymbol = this.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol2 in namedTypeSymbol.GetMembers(symbol.Name))
				{
					if (symbol2.Kind != SymbolKind.Method || ((MethodSymbol)symbol2).CanBeHiddenByMemberKind(symbol.Kind))
					{
						CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
						bool flag3 = AccessCheck.IsSymbolAccessible(symbol2, this, ref compoundUseSiteInfo, null);
						diagnostics.Add(location, compoundUseSiteInfo);
						if (flag3 && symbol2.GetMemberArity() == memberArity)
						{
							if (!isNew)
							{
								diagnostics.Add(ErrorCode.WRN_NewRequired, location, new object[] { symbol, symbol2 });
							}
							SourceMemberContainerTypeSymbol.AddHidingAbstractDiagnostic(symbol, location, symbol2, diagnostics, ref flag2);
							return;
						}
					}
				}
			}
			if (isNew)
			{
				diagnostics.Add(ErrorCode.WRN_NewNotRequired, location, new object[] { symbol });
			}
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x001A18A8 File Offset: 0x0019FAA8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void CheckOverrideMember(Symbol overridingMember, OverriddenOrHiddenMembersResult overriddenOrHiddenMembers, BindingDiagnosticBag diagnostics, out bool suppressAccessors)
		{
			suppressAccessors = false;
			bool flag = overridingMember.Kind == SymbolKind.Method;
			bool flag2 = overridingMember.Kind == SymbolKind.Property;
			SymbolKind kind = overridingMember.Kind;
			Location location = overridingMember.Locations[0];
			ImmutableArray<Symbol> overriddenMembers = overriddenOrHiddenMembers.OverriddenMembers;
			if (overriddenMembers.Length == 0)
			{
				ImmutableArray<Symbol> hiddenMembers = overriddenOrHiddenMembers.HiddenMembers;
				if (hiddenMembers.Any<Symbol>())
				{
					ErrorCode errorCode = (flag ? ErrorCode.ERR_CantOverrideNonFunction : (flag2 ? ErrorCode.ERR_CantOverrideNonProperty : ErrorCode.ERR_CantOverrideNonEvent));
					diagnostics.Add(errorCode, location, new object[]
					{
						overridingMember,
						hiddenMembers[0]
					});
				}
				else
				{
					Symbol symbol = null;
					if (flag)
					{
						symbol = ((MethodSymbol)overridingMember).AssociatedSymbol;
					}
					if (symbol == null)
					{
						bool flag3 = false;
						if (flag || overridingMember.IsIndexer())
						{
							foreach (TypeWithAnnotations typeWithAnnotations in (flag ? ((MethodSymbol)overridingMember).ParameterTypesWithAnnotations : ((PropertySymbol)overridingMember).ParameterTypesWithAnnotations))
							{
								if (SourceMemberContainerTypeSymbol.IsOrContainsErrorType(typeWithAnnotations.Type))
								{
									flag3 = true;
									break;
								}
							}
						}
						if (!flag3)
						{
							diagnostics.Add(ErrorCode.ERR_OverrideNotExpected, location, new object[] { overridingMember });
						}
					}
					else if (symbol.Kind == SymbolKind.Property)
					{
						PropertySymbol propertySymbol = (PropertySymbol)symbol;
						PropertySymbol overriddenProperty = propertySymbol.OverriddenProperty;
						if (overriddenProperty != null)
						{
							if (propertySymbol.GetMethod == overridingMember && overriddenProperty.GetMethod == null)
							{
								diagnostics.Add(ErrorCode.ERR_NoGetToOverride, location, new object[] { overridingMember, overriddenProperty });
							}
							else if (propertySymbol.SetMethod == overridingMember && overriddenProperty.SetMethod == null)
							{
								diagnostics.Add(ErrorCode.ERR_NoSetToOverride, location, new object[] { overridingMember, overriddenProperty });
							}
							else
							{
								diagnostics.Add(ErrorCode.ERR_OverrideNotExpected, location, new object[] { overridingMember });
							}
						}
					}
				}
			}
			else
			{
				NamedTypeSymbol containingType = overridingMember.ContainingType;
				if (overriddenMembers.Length > 1)
				{
					diagnostics.Add(ErrorCode.ERR_AmbigOverride, location, new object[]
					{
						overriddenMembers[0].OriginalDefinition,
						overriddenMembers[1].OriginalDefinition,
						containingType
					});
					suppressAccessors = true;
				}
				else
				{
					this.<CheckOverrideMember>g__checkSingleOverriddenMember|201_0(overridingMember, overriddenMembers[0], diagnostics, ref suppressAccessors);
				}
			}
			if (!this.ContainingAssembly.RuntimeSupportsCovariantReturnsOfClasses)
			{
				MethodSymbol methodSymbol = overridingMember as MethodSymbol;
				if (methodSymbol != null)
				{
					bool flag4;
					methodSymbol.RequiresExplicitOverride(out flag4);
					if (flag4)
					{
						MethodSymbol overriddenMethod = methodSymbol.OverriddenMethod;
						diagnostics.Add(ErrorCode.WRN_MultipleRuntimeOverrideMatches, overriddenMethod.Locations[0], new object[] { overriddenMethod, overridingMember });
						suppressAccessors = true;
					}
				}
			}
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x001A1B47 File Offset: 0x0019FD47
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static bool IsOrContainsErrorType(TypeSymbol typeSymbol)
		{
			return typeSymbol.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol currentTypeSymbol, object unused1, bool unused2) => currentTypeSymbol.IsErrorType(), null, false) != null;
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001A1B74 File Offset: 0x0019FD74
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private bool IsValidOverrideReturnType(Symbol overridingSymbol, TypeWithAnnotations overridingReturnType, TypeWithAnnotations overriddenReturnType, BindingDiagnosticBag diagnostics)
		{
			if (overridingSymbol.ContainingAssembly.RuntimeSupportsCovariantReturnsOfClasses && this.DeclaringCompilation.LanguageVersion >= MessageID.IDS_FeatureCovariantReturnsForOverrides.RequiredVersion())
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
				bool flag = this.DeclaringCompilation.Conversions.HasIdentityOrImplicitReferenceConversion(overridingReturnType.Type, overriddenReturnType.Type, ref compoundUseSiteInfo);
				Location location = overridingSymbol.Locations.FirstOrDefault<Location>();
				diagnostics.Add(location, compoundUseSiteInfo);
				return flag;
			}
			return overridingReturnType.Equals(overriddenReturnType, TypeCompareKind.AllIgnoreOptions);
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x001A1BF8 File Offset: 0x0019FDF8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static void CheckValidNullableMethodOverride<TArg>(CSharpCompilation compilation, MethodSymbol baseMethod, MethodSymbol overrideMethod, BindingDiagnosticBag diagnostics, ReportMismatchInReturnType<TArg> reportMismatchInReturnType, ReportMismatchInParameterType<TArg> reportMismatchInParameterType, TArg extraArgument, bool invokedAsExtensionMethod = false)
		{
			if (!SourceMemberContainerTypeSymbol.PerformValidNullableOverrideCheck(compilation, baseMethod, overrideMethod))
			{
				return;
			}
			if ((baseMethod.FlowAnalysisAnnotations & FlowAnalysisAnnotations.DoesNotReturn) == FlowAnalysisAnnotations.DoesNotReturn && (overrideMethod.FlowAnalysisAnnotations & FlowAnalysisAnnotations.DoesNotReturn) != FlowAnalysisAnnotations.DoesNotReturn)
			{
				diagnostics.Add(ErrorCode.WRN_DoesNotReturnMismatch, overrideMethod.Locations[0], new object[]
				{
					new FormattedSymbol(overrideMethod, SymbolDisplayFormat.MinimallyQualifiedFormat)
				});
			}
			ConversionsBase conversionsBase = compilation.Conversions.WithNullability(true);
			SourceMemberContainerTypeSymbol.<>c__DisplayClass206_0<TArg> CS$<>8__locals1;
			CS$<>8__locals1.baseParameters = baseMethod.Parameters;
			CS$<>8__locals1.overrideParameters = overrideMethod.Parameters;
			CS$<>8__locals1.overrideParameterOffset = (invokedAsExtensionMethod ? 1 : 0);
			if (reportMismatchInReturnType != null)
			{
				TypeWithAnnotations typeWithAnnotations = SourceMemberContainerTypeSymbol.<CheckValidNullableMethodOverride>g__getNotNullIfNotNullOutputType|206_0<TArg>(overrideMethod.ReturnTypeWithAnnotations, overrideMethod.ReturnNotNullIfParameterNotNull, ref CS$<>8__locals1);
				if (!SourceMemberContainerTypeSymbol.<CheckValidNullableMethodOverride>g__isValidNullableConversion|206_1<TArg>(conversionsBase, overrideMethod.RefKind, typeWithAnnotations.Type, baseMethod.ReturnTypeWithAnnotations.Type))
				{
					reportMismatchInReturnType(diagnostics, baseMethod, overrideMethod, false, extraArgument);
					return;
				}
				if (!NullableWalker.AreParameterAnnotationsCompatible((overrideMethod.RefKind == RefKind.Ref) ? RefKind.Ref : RefKind.Out, baseMethod.ReturnTypeWithAnnotations, baseMethod.ReturnTypeFlowAnalysisAnnotations, typeWithAnnotations, overrideMethod.ReturnTypeFlowAnalysisAnnotations, false))
				{
					reportMismatchInReturnType(diagnostics, baseMethod, overrideMethod, true, extraArgument);
					return;
				}
			}
			if (reportMismatchInParameterType == null)
			{
				return;
			}
			for (int i = 0; i < CS$<>8__locals1.baseParameters.Length; i++)
			{
				ParameterSymbol parameterSymbol = CS$<>8__locals1.baseParameters[i];
				TypeWithAnnotations typeWithAnnotations2 = parameterSymbol.TypeWithAnnotations;
				ParameterSymbol parameterSymbol2 = CS$<>8__locals1.overrideParameters[i + CS$<>8__locals1.overrideParameterOffset];
				TypeWithAnnotations typeWithAnnotations3 = SourceMemberContainerTypeSymbol.<CheckValidNullableMethodOverride>g__getNotNullIfNotNullOutputType|206_0<TArg>(parameterSymbol2.TypeWithAnnotations, parameterSymbol2.NotNullIfParameterNotNull, ref CS$<>8__locals1);
				if (!SourceMemberContainerTypeSymbol.<CheckValidNullableMethodOverride>g__isValidNullableConversion|206_1<TArg>(conversionsBase, parameterSymbol2.RefKind, typeWithAnnotations2.Type, typeWithAnnotations3.Type))
				{
					reportMismatchInParameterType(diagnostics, baseMethod, overrideMethod, parameterSymbol2, false, extraArgument);
				}
				else if (!NullableWalker.AreParameterAnnotationsCompatible(parameterSymbol2.RefKind, typeWithAnnotations2, parameterSymbol.FlowAnalysisAnnotations, typeWithAnnotations3, parameterSymbol2.FlowAnalysisAnnotations, false))
				{
					reportMismatchInParameterType(diagnostics, baseMethod, overrideMethod, parameterSymbol2, true, extraArgument);
				}
			}
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x001A1DE6 File Offset: 0x0019FFE6
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool PerformValidNullableOverrideCheck(CSharpCompilation compilation, Symbol overriddenMember, Symbol overridingMember)
		{
			return overriddenMember != null && overridingMember != null && compilation != null && compilation.IsFeatureEnabled(MessageID.IDS_FeatureNullableReferenceTypes);
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x001A1DFE File Offset: 0x0019FFFE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static void CheckValidNullableEventOverride<TArg>(CSharpCompilation compilation, EventSymbol overriddenEvent, EventSymbol overridingEvent, BindingDiagnosticBag diagnostics, Action<BindingDiagnosticBag, EventSymbol, EventSymbol, TArg> reportMismatch, TArg extraArgument)
		{
			if (!SourceMemberContainerTypeSymbol.PerformValidNullableOverrideCheck(compilation, overriddenEvent, overridingEvent))
			{
				return;
			}
			if (!compilation.Conversions.WithNullability(true).HasAnyNullabilityImplicitConversion(overriddenEvent.TypeWithAnnotations, overridingEvent.TypeWithAnnotations))
			{
				reportMismatch.Invoke(diagnostics, overriddenEvent, overridingEvent, extraArgument);
			}
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x001A1E38 File Offset: 0x001A0038
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static void CheckNonOverrideMember(Symbol hidingMember, bool hidingMemberIsNew, OverriddenOrHiddenMembersResult overriddenOrHiddenMembers, BindingDiagnosticBag diagnostics, out bool suppressAccessors)
		{
			suppressAccessors = false;
			Location location = hidingMember.Locations[0];
			ImmutableArray<Symbol> hiddenMembers = overriddenOrHiddenMembers.HiddenMembers;
			if (hiddenMembers.Length == 0)
			{
				if (hidingMemberIsNew && !hidingMember.IsAccessor())
				{
					diagnostics.Add(ErrorCode.WRN_NewNotRequired, location, new object[] { hidingMember });
					return;
				}
			}
			else
			{
				bool flag = false;
				if (!hidingMember.ContainingType.IsInterface)
				{
					foreach (Symbol symbol in hiddenMembers)
					{
						flag |= SourceMemberContainerTypeSymbol.AddHidingAbstractDiagnostic(hidingMember, location, symbol, diagnostics, ref suppressAccessors);
						if (!hidingMemberIsNew && symbol.Kind == hidingMember.Kind && !hidingMember.IsAccessor() && (symbol.IsAbstract || symbol.IsVirtual || symbol.IsOverride) && !SourceMemberContainerTypeSymbol.IsShadowingSynthesizedRecordMember(hidingMember))
						{
							diagnostics.Add(ErrorCode.WRN_NewOrOverrideExpected, location, new object[] { hidingMember, symbol });
							flag = true;
						}
						if (flag)
						{
							break;
						}
					}
				}
				if (!hidingMemberIsNew && !SourceMemberContainerTypeSymbol.IsShadowingSynthesizedRecordMember(hidingMember) && !flag && !hidingMember.IsAccessor() && !hidingMember.IsOperator())
				{
					diagnostics.Add(ErrorCode.WRN_NewRequired, location, new object[]
					{
						hidingMember,
						hiddenMembers[0]
					});
				}
			}
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x001A1F65 File Offset: 0x001A0165
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool IsShadowingSynthesizedRecordMember(Symbol hidingMember)
		{
			return hidingMember is SynthesizedRecordEquals || hidingMember is SynthesizedRecordDeconstruct || hidingMember is SynthesizedRecordClone;
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x001A1F84 File Offset: 0x001A0184
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool AddHidingAbstractDiagnostic(Symbol hidingMember, Location hidingMemberLocation, Symbol hiddenMember, BindingDiagnosticBag diagnostics, ref bool suppressAccessors)
		{
			SymbolKind symbolKind = hiddenMember.Kind;
			if (symbolKind != SymbolKind.Event && symbolKind != SymbolKind.Method && symbolKind != SymbolKind.Property)
			{
				return false;
			}
			if (!hiddenMember.IsAbstract || !hidingMember.ContainingType.IsAbstract)
			{
				return false;
			}
			switch (hidingMember.DeclaredAccessibility)
			{
			case Accessibility.Private:
			case Accessibility.ProtectedAndInternal:
			case Accessibility.Internal:
				return false;
			case Accessibility.Protected:
			case Accessibility.ProtectedOrInternal:
			case Accessibility.Public:
				symbolKind = hidingMember.Kind;
				if (symbolKind != SymbolKind.Event)
				{
					if (symbolKind != SymbolKind.Method)
					{
						if (symbolKind != SymbolKind.Property)
						{
							goto IL_00AC;
						}
					}
					else
					{
						Symbol associatedSymbol = ((MethodSymbol)hidingMember).AssociatedSymbol;
						if (associatedSymbol != null)
						{
							diagnostics.Add(ErrorCode.ERR_HidingAbstractMethod, associatedSymbol.Locations[0], new object[] { associatedSymbol, hiddenMember });
							return true;
						}
						goto IL_00AC;
					}
				}
				suppressAccessors = true;
				IL_00AC:
				diagnostics.Add(ErrorCode.ERR_HidingAbstractMethod, hidingMemberLocation, new object[] { hidingMember, hiddenMember });
				return true;
			default:
				throw ExceptionUtilities.UnexpectedValue(hidingMember.DeclaredAccessibility);
			}
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x001A206C File Offset: 0x001A026C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool OverrideHasCorrectAccessibility(Symbol overridden, Symbol overriding)
		{
			if (!overriding.ContainingAssembly.HasInternalAccessTo(overridden.ContainingAssembly) && overridden.DeclaredAccessibility == Accessibility.ProtectedOrInternal)
			{
				return overriding.DeclaredAccessibility == Accessibility.Protected;
			}
			return overridden.DeclaredAccessibility == overriding.DeclaredAccessibility;
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x001A20A4 File Offset: 0x001A02A4
		private void CheckInterfaceUnification(BindingDiagnosticBag diagnostics)
		{
			if (!base.IsGenericType)
			{
				return;
			}
			int count = base.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.Count;
			if (count < 2)
			{
				return;
			}
			NamedTypeSymbol[] array = Enumerable.ToArray<NamedTypeSymbol>(base.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.Keys);
			for (int i = 0; i < count; i++)
			{
				for (int j = i + 1; j < count; j++)
				{
					NamedTypeSymbol namedTypeSymbol = array[i];
					NamedTypeSymbol namedTypeSymbol2 = array[j];
					if (namedTypeSymbol.IsGenericType && namedTypeSymbol2.IsGenericType && TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, namedTypeSymbol2.OriginalDefinition, TypeCompareKind.ConsiderEverything) && namedTypeSymbol.CanUnifyWith(namedTypeSymbol2))
					{
						if (this.GetImplementsLocationOrFallback(namedTypeSymbol).SourceSpan.Start > this.GetImplementsLocationOrFallback(namedTypeSymbol2).SourceSpan.Start)
						{
							NamedTypeSymbol namedTypeSymbol3 = namedTypeSymbol;
							namedTypeSymbol = namedTypeSymbol2;
							namedTypeSymbol2 = namedTypeSymbol3;
						}
						diagnostics.Add(ErrorCode.ERR_UnifyingInterfaceInstantiations, this.Locations[0], new object[] { this, namedTypeSymbol, namedTypeSymbol2 });
					}
				}
			}
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x001A21AC File Offset: 0x001A03AC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private SynthesizedExplicitImplementationForwardingMethod SynthesizeInterfaceMemberImplementation(TypeSymbol.SymbolAndDiagnostics implementingMemberAndDiagnostics, Symbol interfaceMember)
		{
			if (interfaceMember.DeclaredAccessibility != Accessibility.Public)
			{
				return null;
			}
			ImmutableArray<Diagnostic>.Enumerator enumerator = implementingMemberAndDiagnostics.Diagnostics.Diagnostics.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Severity == DiagnosticSeverity.Error)
				{
					return null;
				}
			}
			Symbol symbol = implementingMemberAndDiagnostics.Symbol;
			if (symbol == null || symbol.Kind != SymbolKind.Method)
			{
				return null;
			}
			MethodSymbol methodSymbol = (MethodSymbol)interfaceMember;
			MethodSymbol methodSymbol2 = (MethodSymbol)symbol;
			Symbol associatedSymbol = methodSymbol.AssociatedSymbol;
			if (associatedSymbol != null && associatedSymbol.IsEventOrPropertyWithImplementableNonPublicAccessor())
			{
				return null;
			}
			if (methodSymbol2.ExplicitInterfaceImplementations.Contains(methodSymbol, TypeSymbol.ExplicitInterfaceImplementationTargetMemberEqualityComparer.Instance))
			{
				return null;
			}
			MethodSymbol originalDefinition = methodSymbol2.OriginalDefinition;
			bool flag = true;
			if (MemberSignatureComparer.RuntimeImplicitImplementationComparer.Equals(methodSymbol2, methodSymbol) && SourceMemberContainerTypeSymbol.IsOverrideOfPossibleImplementationUnderRuntimeRules(methodSymbol2, methodSymbol.ContainingType))
			{
				if (this.ContainingModule == originalDefinition.ContainingModule)
				{
					SourceMemberMethodSymbol sourceMemberMethodSymbol = originalDefinition as SourceMemberMethodSymbol;
					if (sourceMemberMethodSymbol != null)
					{
						sourceMemberMethodSymbol.EnsureMetadataVirtual();
						flag = false;
					}
				}
				else if (methodSymbol2.IsMetadataVirtual(true))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				return null;
			}
			return new SynthesizedExplicitImplementationForwardingMethod(methodSymbol, methodSymbol2, this);
		}

		// Token: 0x060070FF RID: 28927 RVA: 0x001A22B0 File Offset: 0x001A04B0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool IsPossibleImplementationUnderRuntimeRules(MethodSymbol implementingMethod, NamedTypeSymbol @interface)
		{
			NamedTypeSymbol containingType = implementingMethod.ContainingType;
			if (containingType.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.ContainsKey(@interface))
			{
				return true;
			}
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = containingType.BaseTypeNoUseSiteDiagnostics;
			return baseTypeNoUseSiteDiagnostics == null || !baseTypeNoUseSiteDiagnostics.AllInterfacesNoUseSiteDiagnostics.Contains(@interface);
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x001A22F4 File Offset: 0x001A04F4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool IsOverrideOfPossibleImplementationUnderRuntimeRules(MethodSymbol implementingMethod, NamedTypeSymbol @interface)
		{
			for (MethodSymbol methodSymbol = implementingMethod; methodSymbol != null; methodSymbol = methodSymbol.OverriddenMethod)
			{
				if (SourceMemberContainerTypeSymbol.IsPossibleImplementationUnderRuntimeRules(methodSymbol, @interface))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x001A231B File Offset: 0x001A051B
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal sealed override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return base.CalculateInterfacesToEmit();
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x001A237C File Offset: 0x001A057C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static bool <TryCalculateSyntaxOffsetOfPositionInInitializer>g__findInitializer|95_0(ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> initializers, int position, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxTree tree, out FieldOrPropertyInitializer found, out int precedingLength)
		{
			precedingLength = 0;
			foreach (ImmutableArray<FieldOrPropertyInitializer> immutableArray in initializers)
			{
				if (!immutableArray.IsEmpty && immutableArray[0].Syntax.SyntaxTree == tree && position < immutableArray.Last<FieldOrPropertyInitializer>().Syntax.Span.End)
				{
					int num = SourceMemberContainerTypeSymbol.IndexOfInitializerContainingPosition(immutableArray, position);
					if (num >= 0)
					{
						precedingLength += SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getPrecedingInitializersLength|95_2(immutableArray, num);
						found = immutableArray[num];
						return true;
					}
					break;
				}
				else
				{
					precedingLength += SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getGroupLength|95_1(immutableArray);
				}
			}
			found = default(FieldOrPropertyInitializer);
			return false;
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x001A2424 File Offset: 0x001A0624
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static int <TryCalculateSyntaxOffsetOfPositionInInitializer>g__getGroupLength|95_1(ImmutableArray<FieldOrPropertyInitializer> initializers)
		{
			int num = 0;
			foreach (FieldOrPropertyInitializer fieldOrPropertyInitializer in initializers)
			{
				num += SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getInitializerLength|95_4(fieldOrPropertyInitializer);
			}
			return num;
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x001A2458 File Offset: 0x001A0658
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static int <TryCalculateSyntaxOffsetOfPositionInInitializer>g__getPrecedingInitializersLength|95_2(ImmutableArray<FieldOrPropertyInitializer> initializers, int index)
		{
			int num = 0;
			for (int i = 0; i < index; i++)
			{
				num += SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getInitializerLength|95_4(initializers[i]);
			}
			return num;
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x001A2484 File Offset: 0x001A0684
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static int <TryCalculateSyntaxOffsetOfPositionInInitializer>g__getInitializersLength|95_3(ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> initializers)
		{
			int num = 0;
			foreach (ImmutableArray<FieldOrPropertyInitializer> immutableArray in initializers)
			{
				num += SourceMemberContainerTypeSymbol.<TryCalculateSyntaxOffsetOfPositionInInitializer>g__getGroupLength|95_1(immutableArray);
			}
			return num;
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x001A24B8 File Offset: 0x001A06B8
		[CompilerGenerated]
		internal static int <TryCalculateSyntaxOffsetOfPositionInInitializer>g__getInitializerLength|95_4(FieldOrPropertyInitializer initializer)
		{
			if (initializer.FieldOpt == null || !initializer.FieldOpt.IsMetadataConstant)
			{
				return initializer.Syntax.Span.Length;
			}
			return 0;
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x001A24F5 File Offset: 0x001A06F5
		[CompilerGenerated]
		internal static bool <AfterMembersChecks>g__hasBaseTypeOrInterface|120_3(Func<NamedTypeSymbol, bool> predicate, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass120_0 A_1)
		{
			return (A_1.baseType != null && predicate.Invoke(A_1.baseType)) || A_1.interfaces.Any<NamedTypeSymbol>(predicate);
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x001A251B File Offset: 0x001A071B
		[CompilerGenerated]
		internal static bool <AfterMembersChecks>g__needsTupleElementNamesAttribute|120_4(TypeSymbol type)
		{
			if (type == null)
			{
				return false;
			}
			return type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol t, object a, bool b) => !t.TupleElementNames.IsDefaultOrEmpty && !t.IsErrorType(), null, false) != null;
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x001A254C File Offset: 0x001A074C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers <BuildMembersAndInitializers>g__getDeclaredMembersAndInitializers|152_0()
		{
			SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers = this._lazyDeclaredMembersAndInitializers;
			if (declaredMembersAndInitializers != SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers.UninitializedSentinel)
			{
				return declaredMembersAndInitializers;
			}
			if (Volatile.Read<SourceMemberContainerTypeSymbol.MembersAndInitializers>(ref this._lazyMembersAndInitializers) != null)
			{
				return null;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			declaredMembersAndInitializers = this.<BuildMembersAndInitializers>g__buildDeclaredMembersAndInitializers|152_1(instance);
			SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers2 = Interlocked.CompareExchange<SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers>(ref this._lazyDeclaredMembersAndInitializers, declaredMembersAndInitializers, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers.UninitializedSentinel);
			if (declaredMembersAndInitializers2 != SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers.UninitializedSentinel)
			{
				instance.Free();
				return declaredMembersAndInitializers2;
			}
			this.AddDeclarationDiagnostics(instance);
			instance.Free();
			return declaredMembersAndInitializers;
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x001A25B8 File Offset: 0x001A07B8
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers <BuildMembersAndInitializers>g__buildDeclaredMembersAndInitializers|152_1(BindingDiagnosticBag diagnostics)
		{
			SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder declaredMembersAndInitializersBuilder = new SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder();
			this.AddDeclaredNontypeMembers(declaredMembersAndInitializersBuilder, diagnostics);
			TypeKind typeKind = this.TypeKind;
			if (typeKind <= TypeKind.Enum)
			{
				if (typeKind != TypeKind.Class)
				{
					if (typeKind == TypeKind.Enum)
					{
						SourceMemberContainerTypeSymbol.CheckForStructDefaultConstructors(declaredMembersAndInitializersBuilder.NonTypeMembers, true, diagnostics);
					}
				}
			}
			else if (typeKind != TypeKind.Interface)
			{
				if (typeKind != TypeKind.Struct)
				{
					if (typeKind != TypeKind.Submission)
					{
					}
				}
				else
				{
					this.CheckForStructBadInitializers(declaredMembersAndInitializersBuilder, diagnostics);
					SourceMemberContainerTypeSymbol.CheckForStructDefaultConstructors(declaredMembersAndInitializersBuilder.NonTypeMembers, false, diagnostics);
				}
			}
			if (this.IsTupleType)
			{
				declaredMembersAndInitializersBuilder.AddOrWrapTupleMembers(this);
			}
			if (Volatile.Read<SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers>(ref this._lazyDeclaredMembersAndInitializers) != SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers.UninitializedSentinel)
			{
				declaredMembersAndInitializersBuilder.Free();
				return null;
			}
			return declaredMembersAndInitializersBuilder.ToReadOnlyAndFree(this.DeclaringCompilation);
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x001A2654 File Offset: 0x001A0854
		[CompilerGenerated]
		private void <AddDeclaredNontypeMembers>g__noteRecordParameters|154_0(RecordDeclarationSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializersBuilder builder, BindingDiagnosticBag diagnostics)
		{
			if (parameterList == null)
			{
				return;
			}
			if (builder.RecordDeclarationWithParameters == null)
			{
				builder.RecordDeclarationWithParameters = syntax;
				SynthesizedRecordConstructor synthesizedRecordConstructor = new SynthesizedRecordConstructor(this, syntax);
				builder.RecordPrimaryConstructor = synthesizedRecordConstructor;
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				builder.UpdateIsNullableEnabledForConstructorsAndFields(synthesizedRecordConstructor.IsStatic, declaringCompilation, parameterList);
				if (syntax != null)
				{
					PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeIfClass = syntax.PrimaryConstructorBaseTypeIfClass;
					if (primaryConstructorBaseTypeIfClass != null)
					{
						ArgumentListSyntax argumentList = primaryConstructorBaseTypeIfClass.ArgumentList;
						if (argumentList != null)
						{
							builder.UpdateIsNullableEnabledForConstructorsAndFields(synthesizedRecordConstructor.IsStatic, declaringCompilation, argumentList);
							return;
						}
					}
				}
			}
			else
			{
				diagnostics.Add(ErrorCode.ERR_MultipleRecordParameterLists, parameterList.Location);
			}
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x001A26D4 File Offset: 0x001A08D4
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addDeconstruct|172_0(SynthesizedRecordConstructor ctor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> positionalMembers, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_3)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol("Deconstruct", this, MethodKind.Ordinary, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ctor.Parameters.SelectAsArray<ParameterSymbol, ParameterSymbol>((ParameterSymbol param) => new SignatureOnlyParameterSymbol(param.TypeWithAnnotations, ImmutableArray<CustomModifier>.Empty, false, RefKind.Out)), RefKind.None, false, TypeWithAnnotations.Create(A_3.compilation.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			Symbol symbol;
			if (!A_3.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
			{
				A_3.members.Add(new SynthesizedRecordDeconstruct(this, ctor, positionalMembers, A_3.members.Count, A_3.diagnostics));
				return;
			}
			MethodSymbol methodSymbol = (MethodSymbol)symbol;
			if (methodSymbol.DeclaredAccessibility != Accessibility.Public)
			{
				A_3.diagnostics.Add(ErrorCode.ERR_NonPublicAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
			}
			if (methodSymbol.ReturnType.SpecialType != SpecialType.System_Void && !methodSymbol.ReturnType.IsErrorType())
			{
				A_3.diagnostics.Add(ErrorCode.ERR_SignatureMismatchInRecord, methodSymbol.Locations[0], new object[] { methodSymbol, signatureOnlyMethodSymbol.ReturnType });
			}
			if (methodSymbol.IsStatic)
			{
				A_3.diagnostics.Add(ErrorCode.ERR_StaticAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
			}
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x001A283C File Offset: 0x001A0A3C
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addCopyCtor|172_1(bool primaryAndCopyCtorAmbiguity, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol(".ctor", this, MethodKind.Constructor, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ImmutableArray.Create<ParameterSymbol>(new SignatureOnlyParameterSymbol(TypeWithAnnotations.Create(this, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, false, RefKind.None)), RefKind.None, false, TypeWithAnnotations.Create(A_2.compilation.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			Symbol symbol;
			if (!A_2.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
			{
				SynthesizedRecordCopyCtor synthesizedRecordCopyCtor = new SynthesizedRecordCopyCtor(this, A_2.members.Count);
				A_2.members.Add(synthesizedRecordCopyCtor);
				if (primaryAndCopyCtorAmbiguity)
				{
					A_2.diagnostics.Add(ErrorCode.ERR_RecordAmbigCtor, synthesizedRecordCopyCtor.Locations[0]);
					return;
				}
			}
			else
			{
				MethodSymbol methodSymbol = (MethodSymbol)symbol;
				if (!this.IsSealed && methodSymbol.DeclaredAccessibility != Accessibility.Public && methodSymbol.DeclaredAccessibility != Accessibility.Protected)
				{
					A_2.diagnostics.Add(ErrorCode.ERR_CopyConstructorWrongAccessibility, methodSymbol.Locations[0], new object[] { methodSymbol });
				}
			}
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x001A2949 File Offset: 0x001A0B49
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addCloneMethod|172_2(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			A_1.members.Add(new SynthesizedRecordClone(this, A_1.members.Count, A_1.diagnostics));
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x001A2970 File Offset: 0x001A0B70
		[CompilerGenerated]
		private MethodSymbol <AddSynthesizedRecordMembersIfNecessary>g__addPrintMembersMethod|172_3(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol("PrintMembers", this, MethodKind.Ordinary, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ImmutableArray.Create<ParameterSymbol>(new SignatureOnlyParameterSymbol(TypeWithAnnotations.Create(A_1.compilation.GetWellKnownType(WellKnownType.System_Text_StringBuilder), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, false, RefKind.None)), RefKind.None, false, TypeWithAnnotations.Create(A_1.compilation.GetSpecialType(SpecialType.System_Boolean), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			Symbol symbol;
			MethodSymbol methodSymbol;
			if (!A_1.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
			{
				methodSymbol = new SynthesizedRecordPrintMembers(this, A_1.members.Count, A_1.diagnostics);
				A_1.members.Add(methodSymbol);
			}
			else
			{
				methodSymbol = (MethodSymbol)symbol;
				if (!A_1.isRecordClass || (this.IsSealed && this.BaseTypeNoUseSiteDiagnostics.IsObjectType()))
				{
					if (methodSymbol.DeclaredAccessibility != Accessibility.Private)
					{
						A_1.diagnostics.Add(ErrorCode.ERR_NonPrivateAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
					}
				}
				else if (methodSymbol.DeclaredAccessibility != Accessibility.Protected)
				{
					A_1.diagnostics.Add(ErrorCode.ERR_NonProtectedAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
				}
				if (!methodSymbol.ReturnType.Equals(signatureOnlyMethodSymbol.ReturnType, TypeCompareKind.AllIgnoreOptions))
				{
					if (!methodSymbol.ReturnType.IsErrorType())
					{
						A_1.diagnostics.Add(ErrorCode.ERR_SignatureMismatchInRecord, methodSymbol.Locations[0], new object[] { methodSymbol, signatureOnlyMethodSymbol.ReturnType });
					}
				}
				else if (A_1.isRecordClass)
				{
					SynthesizedRecordPrintMembers.VerifyOverridesPrintMembersFromBase(methodSymbol, A_1.diagnostics);
				}
				this.<AddSynthesizedRecordMembersIfNecessary>g__reportStaticOrNotOverridableAPIInRecord|172_10(methodSymbol, A_1.diagnostics, ref A_1);
			}
			return methodSymbol;
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x001A2B2C File Offset: 0x001A0D2C
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addToStringMethod|172_4(MethodSymbol printMethod, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol("ToString", this, MethodKind.Ordinary, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ImmutableArray<ParameterSymbol>.Empty, RefKind.None, false, TypeWithAnnotations.Create(A_2.compilation.GetSpecialType(SpecialType.System_String), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			MethodSymbol methodSymbol = this.<AddSynthesizedRecordMembersIfNecessary>g__getBaseToStringMethod|172_14(ref A_2);
			if (methodSymbol != null && methodSymbol.IsSealed)
			{
				if (methodSymbol.ContainingModule != this.ContainingModule && !this.DeclaringCompilation.IsFeatureEnabled(MessageID.IDS_FeatureSealedToStringInRecord))
				{
					LanguageVersion languageVersion = ((CSharpParseOptions)this.Locations[0].SourceTree.Options).LanguageVersion;
					LanguageVersion languageVersion2 = MessageID.IDS_FeatureSealedToStringInRecord.RequiredVersion();
					A_2.diagnostics.Add(ErrorCode.ERR_InheritingFromRecordWithSealedToString, this.Locations[0], new object[]
					{
						languageVersion.ToDisplayString(),
						new CSharpRequiredLanguageVersion(languageVersion2)
					});
					return;
				}
			}
			else
			{
				Symbol symbol;
				if (!A_2.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
				{
					SynthesizedRecordToString synthesizedRecordToString = new SynthesizedRecordToString(this, printMethod, A_2.members.Count, A_2.diagnostics);
					A_2.members.Add(synthesizedRecordToString);
					return;
				}
				MethodSymbol methodSymbol2 = (MethodSymbol)symbol;
				if (!SynthesizedRecordObjectMethod.VerifyOverridesMethodFromObject(methodSymbol2, SpecialMember.System_Object__ToString, A_2.diagnostics) && methodSymbol2.IsSealed && !this.IsSealed)
				{
					MessageID.IDS_FeatureSealedToStringInRecord.CheckFeatureAvailability(A_2.diagnostics, this.DeclaringCompilation, methodSymbol2.Locations[0]);
				}
			}
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x001A2CB8 File Offset: 0x001A0EB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private MethodSymbol <AddSynthesizedRecordMembersIfNecessary>g__getBaseToStringMethod|172_14(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			Symbol specialTypeMember = this.DeclaringCompilation.GetSpecialTypeMember(SpecialMember.System_Object__ToString);
			for (NamedTypeSymbol namedTypeSymbol = this.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol in namedTypeSymbol.GetSimpleNonTypeMembers("ToString"))
				{
					MethodSymbol methodSymbol = symbol as MethodSymbol;
					if (methodSymbol != null && methodSymbol.GetLeastOverriddenMethod(null) == specialTypeMember)
					{
						return methodSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x001A2D2C File Offset: 0x001A0F2C
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Symbol> <AddSynthesizedRecordMembersIfNecessary>g__addProperties|172_5([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> recordParameters, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			SourceMemberContainerTypeSymbol.<>c__DisplayClass172_1 CS$<>8__locals1;
			CS$<>8__locals1.existingOrAddedMembers = ArrayBuilder<Symbol>.GetInstance(recordParameters.Length);
			CS$<>8__locals1.addedCount = 0;
			foreach (ParameterSymbol parameterSymbol in recordParameters)
			{
				SourceMemberContainerTypeSymbol.<>c__DisplayClass172_2 CS$<>8__locals2;
				CS$<>8__locals2.param = parameterSymbol;
				bool flag = false;
				CSharpSyntaxNode nonNullSyntaxNode = CS$<>8__locals2.param.GetNonNullSyntaxNode();
				SignatureOnlyPropertySymbol signatureOnlyPropertySymbol = new SignatureOnlyPropertySymbol(CS$<>8__locals2.param.Name, this, ImmutableArray<ParameterSymbol>.Empty, RefKind.None, CS$<>8__locals2.param.TypeWithAnnotations, ImmutableArray<CustomModifier>.Empty, false, ImmutableArray<PropertySymbol>.Empty);
				Symbol symbol;
				if (!A_2.memberSignatures.TryGetValue(signatureOnlyPropertySymbol, ref symbol) && !A_2.fieldsByName.TryGetValue(CS$<>8__locals2.param.Name, ref symbol))
				{
					symbol = OverriddenOrHiddenMembersHelpers.FindFirstHiddenMemberIfAny(signatureOnlyPropertySymbol, true);
					flag = true;
				}
				if (symbol == null)
				{
					this.<AddSynthesizedRecordMembersIfNecessary>g__addProperty|172_16(new SynthesizedRecordPropertySymbol(this, nonNullSyntaxNode, CS$<>8__locals2.param, false, A_2.diagnostics), ref A_2, ref CS$<>8__locals1, ref CS$<>8__locals2);
				}
				else
				{
					FieldSymbol fieldSymbol = symbol as FieldSymbol;
					if (fieldSymbol != null && !symbol.IsStatic && fieldSymbol.TypeWithAnnotations.Equals(CS$<>8__locals2.param.TypeWithAnnotations, TypeCompareKind.AllIgnoreOptions))
					{
						Binder.CheckFeatureAvailability(nonNullSyntaxNode, MessageID.IDS_FeaturePositionalFieldsInRecords, A_2.diagnostics, null);
						if (!flag || this.<AddSynthesizedRecordMembersIfNecessary>g__checkMemberNotHidden|172_15(fieldSymbol, CS$<>8__locals2.param, ref A_2))
						{
							CS$<>8__locals1.existingOrAddedMembers.Add(fieldSymbol);
						}
					}
					else
					{
						PropertySymbol propertySymbol = symbol as PropertySymbol;
						if (propertySymbol != null && !symbol.IsStatic && propertySymbol.GetMethod != null && propertySymbol.TypeWithAnnotations.Equals(CS$<>8__locals2.param.TypeWithAnnotations, TypeCompareKind.AllIgnoreOptions))
						{
							if (flag && propertySymbol.IsAbstract)
							{
								this.<AddSynthesizedRecordMembersIfNecessary>g__addProperty|172_16(new SynthesizedRecordPropertySymbol(this, nonNullSyntaxNode, CS$<>8__locals2.param, true, A_2.diagnostics), ref A_2, ref CS$<>8__locals1, ref CS$<>8__locals2);
							}
							else if (!flag || this.<AddSynthesizedRecordMembersIfNecessary>g__checkMemberNotHidden|172_15(propertySymbol, CS$<>8__locals2.param, ref A_2))
							{
								CS$<>8__locals1.existingOrAddedMembers.Add(propertySymbol);
							}
						}
						else
						{
							A_2.diagnostics.Add(ErrorCode.ERR_BadRecordMemberForPositionalParameter, CS$<>8__locals2.param.Locations[0], new object[]
							{
								new FormattedSymbol(symbol, SymbolDisplayFormat.CSharpErrorMessageFormat.WithMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType)),
								CS$<>8__locals2.param.TypeWithAnnotations,
								CS$<>8__locals2.param.Name
							});
						}
					}
				}
			}
			return CS$<>8__locals1.existingOrAddedMembers.ToImmutableAndFree();
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x001A2F90 File Offset: 0x001A1190
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addProperty|172_16(SynthesizedRecordPropertySymbol property, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_1 A_3, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_2 A_4)
		{
			A_3.existingOrAddedMembers.Add(property);
			A_2.members.Add(property);
			A_2.members.Add(property.GetMethod);
			A_2.members.Add(property.SetMethod);
			A_2.members.Add(property.BackingField);
			A_2.builder.AddInstanceInitializerForPositionalMembers(new FieldOrPropertyInitializer(property.BackingField, A_2.paramList.Parameters[A_4.param.Ordinal]));
			int addedCount = A_3.addedCount;
			A_3.addedCount = addedCount + 1;
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x001A3030 File Offset: 0x001A1230
		[CompilerGenerated]
		private bool <AddSynthesizedRecordMembersIfNecessary>g__checkMemberNotHidden|172_15(Symbol symbol, ParameterSymbol param, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_3)
		{
			if (A_3.memberNames.Contains(symbol.Name) || this.GetTypeMembersDictionary().ContainsKey(symbol.Name))
			{
				A_3.diagnostics.Add(ErrorCode.ERR_HiddenPositionalMember, param.Locations[0], new object[] { symbol });
				return false;
			}
			return true;
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x001A3090 File Offset: 0x001A1290
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addObjectEquals|172_6(MethodSymbol thisEquals, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			A_2.members.Add(new SynthesizedRecordObjEquals(this, thisEquals, A_2.members.Count, A_2.diagnostics));
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x001A30B8 File Offset: 0x001A12B8
		[CompilerGenerated]
		private MethodSymbol <AddSynthesizedRecordMembersIfNecessary>g__addGetHashCode|172_7([global::System.Runtime.CompilerServices.Nullable(2)] PropertySymbol equalityContract, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol("GetHashCode", this, MethodKind.Ordinary, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ImmutableArray<ParameterSymbol>.Empty, RefKind.None, false, TypeWithAnnotations.Create(A_2.compilation.GetSpecialType(SpecialType.System_Int32), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			Symbol symbol;
			MethodSymbol methodSymbol;
			if (!A_2.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
			{
				methodSymbol = new SynthesizedRecordGetHashCode(this, equalityContract, A_2.members.Count, A_2.diagnostics);
				A_2.members.Add(methodSymbol);
			}
			else
			{
				methodSymbol = (MethodSymbol)symbol;
				if (!SynthesizedRecordObjectMethod.VerifyOverridesMethodFromObject(methodSymbol, SpecialMember.System_Object__GetHashCode, A_2.diagnostics) && methodSymbol.IsSealed && !this.IsSealed)
				{
					A_2.diagnostics.Add(ErrorCode.ERR_SealedAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
				}
			}
			return methodSymbol;
		}

		// Token: 0x06007118 RID: 28952 RVA: 0x001A3194 File Offset: 0x001A1394
		[CompilerGenerated]
		private PropertySymbol <AddSynthesizedRecordMembersIfNecessary>g__addEqualityContract|172_8(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			SignatureOnlyPropertySymbol signatureOnlyPropertySymbol = new SignatureOnlyPropertySymbol("EqualityContract", this, ImmutableArray<ParameterSymbol>.Empty, RefKind.None, TypeWithAnnotations.Create(A_1.compilation.GetWellKnownType(WellKnownType.System_Type), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, false, ImmutableArray<PropertySymbol>.Empty);
			Symbol symbol;
			PropertySymbol propertySymbol;
			if (!A_1.memberSignatures.TryGetValue(signatureOnlyPropertySymbol, ref symbol))
			{
				propertySymbol = new SynthesizedRecordEqualityContractProperty(this, A_1.diagnostics);
				A_1.members.Add(propertySymbol);
				A_1.members.Add(propertySymbol.GetMethod);
			}
			else
			{
				propertySymbol = (PropertySymbol)symbol;
				if (this.IsSealed && this.BaseTypeNoUseSiteDiagnostics.IsObjectType())
				{
					if (propertySymbol.DeclaredAccessibility != Accessibility.Private)
					{
						A_1.diagnostics.Add(ErrorCode.ERR_NonPrivateAPIInRecord, propertySymbol.Locations[0], new object[] { propertySymbol });
					}
				}
				else if (propertySymbol.DeclaredAccessibility != Accessibility.Protected)
				{
					A_1.diagnostics.Add(ErrorCode.ERR_NonProtectedAPIInRecord, propertySymbol.Locations[0], new object[] { propertySymbol });
				}
				if (!propertySymbol.Type.Equals(signatureOnlyPropertySymbol.Type, TypeCompareKind.AllIgnoreOptions))
				{
					if (!propertySymbol.Type.IsErrorType())
					{
						A_1.diagnostics.Add(ErrorCode.ERR_SignatureMismatchInRecord, propertySymbol.Locations[0], new object[] { propertySymbol, signatureOnlyPropertySymbol.Type });
					}
				}
				else
				{
					SynthesizedRecordEqualityContractProperty.VerifyOverridesEqualityContractFromBase(propertySymbol, A_1.diagnostics);
				}
				if (propertySymbol.GetMethod == null)
				{
					A_1.diagnostics.Add(ErrorCode.ERR_EqualityContractRequiresGetter, propertySymbol.Locations[0], new object[] { propertySymbol });
				}
				this.<AddSynthesizedRecordMembersIfNecessary>g__reportStaticOrNotOverridableAPIInRecord|172_10(propertySymbol, A_1.diagnostics, ref A_1);
			}
			return propertySymbol;
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x001A3348 File Offset: 0x001A1548
		[CompilerGenerated]
		private MethodSymbol <AddSynthesizedRecordMembersIfNecessary>g__addThisEquals|172_9([global::System.Runtime.CompilerServices.Nullable(2)] PropertySymbol equalityContract, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_2)
		{
			SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol("Equals", this, MethodKind.Ordinary, CallingConvention.HasThis, ImmutableArray<TypeParameterSymbol>.Empty, ImmutableArray.Create<ParameterSymbol>(new SignatureOnlyParameterSymbol(TypeWithAnnotations.Create(this, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, false, RefKind.None)), RefKind.None, false, TypeWithAnnotations.Create(A_2.compilation.GetSpecialType(SpecialType.System_Boolean), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, ImmutableArray<MethodSymbol>.Empty);
			Symbol symbol;
			MethodSymbol methodSymbol;
			if (!A_2.memberSignatures.TryGetValue(signatureOnlyMethodSymbol, ref symbol))
			{
				methodSymbol = new SynthesizedRecordEquals(this, equalityContract, A_2.members.Count, A_2.diagnostics);
				A_2.members.Add(methodSymbol);
			}
			else
			{
				methodSymbol = (MethodSymbol)symbol;
				if (methodSymbol.DeclaredAccessibility != Accessibility.Public)
				{
					A_2.diagnostics.Add(ErrorCode.ERR_NonPublicAPIInRecord, methodSymbol.Locations[0], new object[] { methodSymbol });
				}
				if (methodSymbol.ReturnType.SpecialType != SpecialType.System_Boolean && !methodSymbol.ReturnType.IsErrorType())
				{
					A_2.diagnostics.Add(ErrorCode.ERR_SignatureMismatchInRecord, methodSymbol.Locations[0], new object[] { methodSymbol, signatureOnlyMethodSymbol.ReturnType });
				}
				this.<AddSynthesizedRecordMembersIfNecessary>g__reportStaticOrNotOverridableAPIInRecord|172_10(methodSymbol, A_2.diagnostics, ref A_2);
			}
			return methodSymbol;
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x001A3488 File Offset: 0x001A1688
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__reportStaticOrNotOverridableAPIInRecord|172_10(Symbol symbol, BindingDiagnosticBag diagnostics, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_3)
		{
			if (A_3.isRecordClass && !this.IsSealed && ((!symbol.IsAbstract && !symbol.IsVirtual && !symbol.IsOverride) || symbol.IsSealed))
			{
				diagnostics.Add(ErrorCode.ERR_NotOverridableAPIInRecord, symbol.Locations[0], new object[] { symbol });
				return;
			}
			if (symbol.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_StaticAPIInRecord, symbol.Locations[0], new object[] { symbol });
			}
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x001A3518 File Offset: 0x001A1718
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addBaseEquals|172_11(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			if (!this.BaseTypeNoUseSiteDiagnostics.IsObjectType())
			{
				A_1.members.Add(new SynthesizedRecordBaseEquals(this, A_1.members.Count, A_1.diagnostics));
			}
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x001A354C File Offset: 0x001A174C
		[CompilerGenerated]
		private void <AddSynthesizedRecordMembersIfNecessary>g__addEqualityOperators|172_12(ref SourceMemberContainerTypeSymbol.<>c__DisplayClass172_0 A_1)
		{
			A_1.members.Add(new SynthesizedRecordEqualityOperator(this, A_1.members.Count, A_1.diagnostics));
			A_1.members.Add(new SynthesizedRecordInequalityOperator(this, A_1.members.Count, A_1.diagnostics));
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x001A359D File Offset: 0x001A179D
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static bool <AddSynthesizedConstructorsIfNecessary>g__hasNonConstantInitializer|173_0(ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> initializers)
		{
			return initializers.Any<ImmutableArray<FieldOrPropertyInitializer>>((ImmutableArray<FieldOrPropertyInitializer> siblings) => siblings.Any<FieldOrPropertyInitializer>((FieldOrPropertyInitializer initializer) => !initializer.FieldOpt.IsConst));
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x001A35C4 File Offset: 0x001A17C4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private void <CheckOverrideMember>g__checkSingleOverriddenMember|201_0(Symbol overridingMember, Symbol overriddenMember, BindingDiagnosticBag diagnostics, ref bool suppressAccessors)
		{
			SourceMemberContainerTypeSymbol.<>c__DisplayClass201_0 CS$<>8__locals1;
			CS$<>8__locals1.overridingMember = overridingMember;
			CS$<>8__locals1.overriddenMember = overriddenMember;
			Location location2 = CS$<>8__locals1.overridingMember.Locations[0];
			bool flag = CS$<>8__locals1.overridingMember.Kind == SymbolKind.Method;
			bool flag2 = CS$<>8__locals1.overridingMember.Kind == SymbolKind.Property;
			bool flag3 = CS$<>8__locals1.overridingMember.Kind == SymbolKind.Event;
			NamedTypeSymbol containingType = CS$<>8__locals1.overridingMember.ContainingType;
			if (CS$<>8__locals1.overriddenMember.MustCallMethodsDirectly())
			{
				diagnostics.Add(ErrorCode.ERR_CantOverrideBogusMethod, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember });
				suppressAccessors = true;
				return;
			}
			if (!CS$<>8__locals1.overriddenMember.IsVirtual && !CS$<>8__locals1.overriddenMember.IsAbstract && !CS$<>8__locals1.overriddenMember.IsOverride && (!flag || ((MethodSymbol)CS$<>8__locals1.overriddenMember).MethodKind != MethodKind.Destructor))
			{
				diagnostics.Add(ErrorCode.ERR_CantOverrideNonVirtual, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember });
				suppressAccessors = true;
				return;
			}
			if (CS$<>8__locals1.overriddenMember.IsSealed)
			{
				diagnostics.Add(ErrorCode.ERR_CantOverrideSealed, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember });
				suppressAccessors = true;
				return;
			}
			if (!SourceMemberContainerTypeSymbol.OverrideHasCorrectAccessibility(CS$<>8__locals1.overriddenMember, CS$<>8__locals1.overridingMember))
			{
				string text = SyntaxFacts.GetText(CS$<>8__locals1.overriddenMember.DeclaredAccessibility);
				diagnostics.Add(ErrorCode.ERR_CantChangeAccessOnOverride, location2, new object[] { CS$<>8__locals1.overridingMember, text, CS$<>8__locals1.overriddenMember });
				suppressAccessors = true;
				return;
			}
			if (CS$<>8__locals1.overridingMember.ContainsTupleNames() && MemberSignatureComparer.ConsideringTupleNamesCreatesDifference(CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember))
			{
				diagnostics.Add(ErrorCode.ERR_CantChangeTupleNamesOnOverride, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember });
				return;
			}
			Symbol leastOverriddenMember = CS$<>8__locals1.overriddenMember.GetLeastOverriddenMember(CS$<>8__locals1.overriddenMember.ContainingType);
			CS$<>8__locals1.overridingMember.ForceCompleteObsoleteAttribute();
			leastOverriddenMember.ForceCompleteObsoleteAttribute();
			bool flag4 = CS$<>8__locals1.overridingMember.ObsoleteState == ThreeState.True;
			bool flag5 = leastOverriddenMember.ObsoleteState == ThreeState.True;
			if (flag4 != flag5)
			{
				ErrorCode errorCode = (flag4 ? ErrorCode.WRN_ObsoleteOverridingNonObsolete : ErrorCode.WRN_NonObsoleteOverridingObsolete);
				diagnostics.Add(errorCode, location2, new object[] { CS$<>8__locals1.overridingMember, leastOverriddenMember });
			}
			if (flag2)
			{
				this.<CheckOverrideMember>g__checkOverriddenProperty|201_2((PropertySymbol)CS$<>8__locals1.overridingMember, (PropertySymbol)CS$<>8__locals1.overriddenMember, diagnostics, ref suppressAccessors, ref CS$<>8__locals1);
			}
			else if (flag3)
			{
				EventSymbol eventSymbol = (EventSymbol)CS$<>8__locals1.overridingMember;
				EventSymbol eventSymbol2 = (EventSymbol)CS$<>8__locals1.overriddenMember;
				TypeWithAnnotations typeWithAnnotations = eventSymbol.TypeWithAnnotations;
				TypeWithAnnotations typeWithAnnotations2 = eventSymbol2.TypeWithAnnotations;
				if (!typeWithAnnotations.Equals(typeWithAnnotations2, TypeCompareKind.AllIgnoreOptions))
				{
					if (!SourceMemberContainerTypeSymbol.IsOrContainsErrorType(typeWithAnnotations.Type))
					{
						diagnostics.Add(ErrorCode.ERR_CantChangeTypeOnOverride, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember, typeWithAnnotations2.Type });
					}
					suppressAccessors = true;
				}
				else
				{
					SourceMemberContainerTypeSymbol.CheckValidNullableEventOverride<Location>(eventSymbol.DeclaringCompilation, eventSymbol2, eventSymbol, diagnostics, delegate(BindingDiagnosticBag diagnostics, EventSymbol overriddenEvent, EventSymbol overridingEvent, Location location)
					{
						diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInTypeOnOverride, location);
					}, location2);
				}
			}
			else
			{
				MethodSymbol methodSymbol = (MethodSymbol)CS$<>8__locals1.overridingMember;
				MethodSymbol methodSymbol2 = (MethodSymbol)CS$<>8__locals1.overriddenMember;
				if (methodSymbol.IsGenericMethod)
				{
					methodSymbol2 = methodSymbol2.Construct(TypeMap.TypeParametersAsTypeSymbolsWithIgnoredAnnotations(methodSymbol.TypeParameters));
				}
				if (methodSymbol.RefKind != methodSymbol2.RefKind)
				{
					diagnostics.Add(ErrorCode.ERR_CantChangeRefReturnOnOverride, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember });
				}
				else if (!this.IsValidOverrideReturnType(methodSymbol, methodSymbol.ReturnTypeWithAnnotations, methodSymbol2.ReturnTypeWithAnnotations, diagnostics))
				{
					if (!SourceMemberContainerTypeSymbol.IsOrContainsErrorType(methodSymbol.ReturnType))
					{
						CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
						if (this.DeclaringCompilation.Conversions.HasIdentityOrImplicitReferenceConversion(methodSymbol.ReturnTypeWithAnnotations.Type, methodSymbol2.ReturnTypeWithAnnotations.Type, ref discarded))
						{
							if (!methodSymbol.ContainingAssembly.RuntimeSupportsCovariantReturnsOfClasses)
							{
								diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportCovariantReturnsOfClasses, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember, methodSymbol2.ReturnType });
							}
							else
							{
								CSDiagnosticInfo featureAvailabilityDiagnosticInfo = MessageID.IDS_FeatureCovariantReturnsForOverrides.GetFeatureAvailabilityDiagnosticInfo(this.DeclaringCompilation);
								if (featureAvailabilityDiagnosticInfo == null)
								{
									throw ExceptionUtilities.Unreachable;
								}
								diagnostics.Add(featureAvailabilityDiagnosticInfo, location2);
							}
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_CantChangeReturnTypeOnOverride, location2, new object[] { CS$<>8__locals1.overridingMember, CS$<>8__locals1.overriddenMember, methodSymbol2.ReturnType });
						}
					}
				}
				else if (methodSymbol2.IsRuntimeFinalizer(false))
				{
					diagnostics.Add(ErrorCode.ERR_OverrideFinalizeDeprecated, location2);
				}
				else if (!methodSymbol.IsAccessor())
				{
					SourceMemberContainerTypeSymbol.<CheckOverrideMember>g__checkValidNullableMethodOverride|201_1(location2, methodSymbol2, methodSymbol, diagnostics, true, true);
				}
			}
			if (Binder.ReportUseSite(CS$<>8__locals1.overriddenMember, diagnostics, CS$<>8__locals1.overridingMember.Locations[0]))
			{
				suppressAccessors = true;
			}
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x001A3AD0 File Offset: 0x001A1CD0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private void <CheckOverrideMember>g__checkOverriddenProperty|201_2(PropertySymbol overridingProperty, PropertySymbol overriddenProperty, BindingDiagnosticBag diagnostics, ref bool suppressAccessors, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass201_0 A_5)
		{
			Location location = overridingProperty.Locations[0];
			NamedTypeSymbol containingType = overridingProperty.ContainingType;
			TypeWithAnnotations typeWithAnnotations = overridingProperty.TypeWithAnnotations;
			TypeWithAnnotations typeWithAnnotations2 = overriddenProperty.TypeWithAnnotations;
			if (overridingProperty.RefKind != overriddenProperty.RefKind)
			{
				diagnostics.Add(ErrorCode.ERR_CantChangeRefReturnOnOverride, location, new object[] { overridingProperty, overriddenProperty });
				suppressAccessors = true;
			}
			else if ((overridingProperty.SetMethod == null) ? (!this.IsValidOverrideReturnType(overridingProperty, typeWithAnnotations, typeWithAnnotations2, diagnostics)) : (!typeWithAnnotations.Equals(typeWithAnnotations2, TypeCompareKind.AllIgnoreOptions)))
			{
				if (!SourceMemberContainerTypeSymbol.IsOrContainsErrorType(typeWithAnnotations.Type))
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					if (overridingProperty.SetMethod == null && this.DeclaringCompilation.Conversions.HasIdentityOrImplicitReferenceConversion(typeWithAnnotations.Type, typeWithAnnotations2.Type, ref discarded))
					{
						if (!overridingProperty.ContainingAssembly.RuntimeSupportsCovariantReturnsOfClasses)
						{
							diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportCovariantPropertiesOfClasses, location, new object[] { A_5.overridingMember, A_5.overriddenMember, typeWithAnnotations2.Type });
						}
						else
						{
							CSDiagnosticInfo featureAvailabilityDiagnosticInfo = MessageID.IDS_FeatureCovariantReturnsForOverrides.GetFeatureAvailabilityDiagnosticInfo(this.DeclaringCompilation);
							diagnostics.Add(featureAvailabilityDiagnosticInfo, location);
						}
					}
					else
					{
						diagnostics.Add(ErrorCode.ERR_CantChangeTypeOnOverride, location, new object[] { A_5.overridingMember, A_5.overriddenMember, typeWithAnnotations2.Type });
					}
				}
				suppressAccessors = true;
			}
			else
			{
				if (overridingProperty.GetMethod != null)
				{
					MethodSymbol ownOrInheritedGetMethod = overriddenProperty.GetOwnOrInheritedGetMethod();
					Location location2 = overridingProperty.GetMethod.Locations[0];
					MethodSymbol methodSymbol = ownOrInheritedGetMethod;
					MethodSymbol getMethod = overridingProperty.GetMethod;
					bool flag = true;
					bool flag2;
					if (overridingProperty.SetMethod != null && !(((ownOrInheritedGetMethod != null) ? ownOrInheritedGetMethod.AssociatedSymbol : null) != overriddenProperty))
					{
						MethodSymbol ownOrInheritedSetMethod = overriddenProperty.GetOwnOrInheritedSetMethod();
						flag2 = ((ownOrInheritedSetMethod != null) ? ownOrInheritedSetMethod.AssociatedSymbol : null) != overriddenProperty;
					}
					else
					{
						flag2 = true;
					}
					SourceMemberContainerTypeSymbol.<CheckOverrideMember>g__checkValidNullableMethodOverride|201_1(location2, methodSymbol, getMethod, diagnostics, flag, flag2);
				}
				if (overridingProperty.SetMethod != null)
				{
					MethodSymbol ownOrInheritedSetMethod2 = overriddenProperty.GetOwnOrInheritedSetMethod();
					SourceMemberContainerTypeSymbol.<CheckOverrideMember>g__checkValidNullableMethodOverride|201_1(overridingProperty.SetMethod.Locations[0], ownOrInheritedSetMethod2, overridingProperty.SetMethod, diagnostics, false, true);
					if (ownOrInheritedSetMethod2 != null && overridingProperty.SetMethod.IsInitOnly != ownOrInheritedSetMethod2.IsInitOnly)
					{
						diagnostics.Add(ErrorCode.ERR_CantChangeInitOnlyOnOverride, location, new object[] { overridingProperty, overriddenProperty });
					}
				}
			}
			if (overridingProperty.IsSealed)
			{
				MethodSymbol ownOrInheritedGetMethod2 = overridingProperty.GetOwnOrInheritedGetMethod();
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, overridingProperty.ContainingAssembly);
				if (overridingProperty.GetMethod != ownOrInheritedGetMethod2 && !AccessCheck.IsSymbolAccessible(ownOrInheritedGetMethod2, containingType, ref compoundUseSiteInfo, null))
				{
					diagnostics.Add(ErrorCode.ERR_NoGetToOverride, location, new object[] { overridingProperty, overriddenProperty });
				}
				MethodSymbol ownOrInheritedSetMethod3 = overridingProperty.GetOwnOrInheritedSetMethod();
				if (overridingProperty.SetMethod != ownOrInheritedSetMethod3 && !AccessCheck.IsSymbolAccessible(ownOrInheritedSetMethod3, containingType, ref compoundUseSiteInfo, null))
				{
					diagnostics.Add(ErrorCode.ERR_NoSetToOverride, location, new object[] { overridingProperty, overriddenProperty });
				}
				diagnostics.Add(location, compoundUseSiteInfo);
			}
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x001A3DAD File Offset: 0x001A1FAD
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static void <CheckOverrideMember>g__checkValidNullableMethodOverride|201_1(Location overridingMemberLocation, MethodSymbol overriddenMethod, MethodSymbol overridingMethod, BindingDiagnosticBag diagnostics, bool checkReturnType, bool checkParameters)
		{
			SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<Location>(overridingMethod.DeclaringCompilation, overriddenMethod, overridingMethod, diagnostics, checkReturnType ? SourceMemberContainerTypeSymbol.ReportBadReturn : null, checkParameters ? SourceMemberContainerTypeSymbol.ReportBadParameter : null, overridingMemberLocation, false);
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x001A3DD8 File Offset: 0x001A1FD8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static TypeWithAnnotations <CheckValidNullableMethodOverride>g__getNotNullIfNotNullOutputType|206_0<TArg>(TypeWithAnnotations outputType, ImmutableHashSet<string> notNullIfParameterNotNull, ref SourceMemberContainerTypeSymbol.<>c__DisplayClass206_0<TArg> A_2)
		{
			if (!notNullIfParameterNotNull.IsEmpty)
			{
				for (int i = 0; i < A_2.baseParameters.Length; i++)
				{
					ParameterSymbol parameterSymbol = A_2.overrideParameters[i + A_2.overrideParameterOffset];
					if (notNullIfParameterNotNull.Contains(parameterSymbol.Name) && !A_2.baseParameters[i].TypeWithAnnotations.NullableAnnotation.IsAnnotated())
					{
						return outputType.AsNotAnnotated();
					}
				}
			}
			return outputType;
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x001A3E4C File Offset: 0x001A204C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		internal static bool <CheckValidNullableMethodOverride>g__isValidNullableConversion|206_1<TArg>(ConversionsBase conversions, RefKind refKind, TypeSymbol sourceType, TypeSymbol targetType)
		{
			if (refKind != RefKind.Ref)
			{
				if (refKind == RefKind.Out)
				{
					TypeSymbol typeSymbol = targetType;
					TypeSymbol typeSymbol2 = sourceType;
					sourceType = typeSymbol;
					targetType = typeSymbol2;
				}
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return conversions.ClassifyImplicitConversionFromType(sourceType, targetType, ref discarded).Kind != ConversionKind.NoConversion;
			}
			return sourceType.Equals(targetType, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers);
		}

		// Token: 0x04001B8D RID: 7053
		private static readonly ObjectPool<PooledDictionary<Symbol, Symbol>> s_duplicateRecordMemberSignatureDictionary = PooledDictionary<Symbol, Symbol>.CreatePool(MemberSignatureComparer.RecordAPISignatureComparer);

		// Token: 0x04001B8E RID: 7054
		protected SymbolCompletionState state;

		// Token: 0x04001B8F RID: 7055
		private SourceMemberContainerTypeSymbol.Flags _flags;

		// Token: 0x04001B90 RID: 7056
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<DiagnosticInfo> _managedKindUseSiteDiagnostics;

		// Token: 0x04001B91 RID: 7057
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<AssemblySymbol> _managedKindUseSiteDependencies;

		// Token: 0x04001B92 RID: 7058
		private readonly DeclarationModifiers _declModifiers;

		// Token: 0x04001B93 RID: 7059
		private readonly NamespaceOrTypeSymbol _containingSymbol;

		// Token: 0x04001B94 RID: 7060
		protected readonly MergedTypeDeclaration declaration;

		// Token: 0x04001B95 RID: 7061
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers _lazyDeclaredMembersAndInitializers = SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers.UninitializedSentinel;

		// Token: 0x04001B96 RID: 7062
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SourceMemberContainerTypeSymbol.MembersAndInitializers _lazyMembersAndInitializers;

		// Token: 0x04001B97 RID: 7063
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<Symbol>> _lazyMembersDictionary;

		// Token: 0x04001B98 RID: 7064
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<Symbol>> _lazyEarlyAttributeDecodingMembersDictionary;

		// Token: 0x04001B99 RID: 7065
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
		private static readonly Dictionary<string, ImmutableArray<NamedTypeSymbol>> s_emptyTypeMembers = new Dictionary<string, ImmutableArray<NamedTypeSymbol>>(EmptyComparer.Instance);

		// Token: 0x04001B9A RID: 7066
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })]
		private Dictionary<string, ImmutableArray<NamedTypeSymbol>> _lazyTypeMembers;

		// Token: 0x04001B9B RID: 7067
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Symbol> _lazyMembersFlattened;

		// Token: 0x04001B9C RID: 7068
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SynthesizedExplicitImplementationForwardingMethod> _lazySynthesizedExplicitImplementations;

		// Token: 0x04001B9D RID: 7069
		private int _lazyKnownCircularStruct;

		// Token: 0x04001B9E RID: 7070
		private LexicalSortKey _lazyLexicalSortKey = LexicalSortKey.NotInitialized;

		// Token: 0x04001B9F RID: 7071
		private ThreeState _lazyContainsExtensionMethods;

		// Token: 0x04001BA0 RID: 7072
		private ThreeState _lazyAnyMemberHasAttributes;

		// Token: 0x04001BA1 RID: 7073
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private static readonly ReportMismatchInReturnType<Location> ReportBadReturn = delegate(BindingDiagnosticBag diagnostics, MethodSymbol overriddenMethod, MethodSymbol overridingMethod, bool topLevel, Location location)
		{
			diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride : ErrorCode.WRN_NullabilityMismatchInReturnTypeOnOverride, location);
		};

		// Token: 0x04001BA2 RID: 7074
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private static readonly ReportMismatchInParameterType<Location> ReportBadParameter = delegate(BindingDiagnosticBag diagnostics, MethodSymbol overriddenMethod, MethodSymbol overridingMethod, ParameterSymbol overridingParameter, bool topLevel, Location location)
		{
			diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride : ErrorCode.WRN_NullabilityMismatchInParameterTypeOnOverride, location, new object[]
			{
				new FormattedSymbol(overridingParameter, SymbolDisplayFormat.ShortFormat)
			});
		};

		// Token: 0x02000920 RID: 2336
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private struct Flags
		{
			// Token: 0x1700289F RID: 10399
			// (get) Token: 0x06008FDE RID: 36830 RVA: 0x001FA037 File Offset: 0x001F8237
			public SpecialType SpecialType
			{
				get
				{
					return (SpecialType)(this._flags & 63);
				}
			}

			// Token: 0x170028A0 RID: 10400
			// (get) Token: 0x06008FDF RID: 36831 RVA: 0x001FA043 File Offset: 0x001F8243
			public ManagedKind ManagedKind
			{
				get
				{
					return (ManagedKind)((this._flags >> 6) & 3);
				}
			}

			// Token: 0x170028A1 RID: 10401
			// (get) Token: 0x06008FE0 RID: 36832 RVA: 0x001FA050 File Offset: 0x001F8250
			public bool FieldDefinitionsNoted
			{
				get
				{
					return (this._flags & 256) != 0;
				}
			}

			// Token: 0x170028A2 RID: 10402
			// (get) Token: 0x06008FE1 RID: 36833 RVA: 0x001FA061 File Offset: 0x001F8261
			public bool FlattenedMembersIsSorted
			{
				get
				{
					return (this._flags & 512) != 0;
				}
			}

			// Token: 0x170028A3 RID: 10403
			// (get) Token: 0x06008FE2 RID: 36834 RVA: 0x001FA072 File Offset: 0x001F8272
			public TypeKind TypeKind
			{
				get
				{
					return (TypeKind)((this._flags >> 10) & 15);
				}
			}

			// Token: 0x06008FE3 RID: 36835 RVA: 0x001FA084 File Offset: 0x001F8284
			public Flags(SpecialType specialType, TypeKind typeKind)
			{
				int num = (int)(specialType & (SpecialType)63);
				int num2 = (int)((int)(typeKind & (TypeKind)15) << 10);
				this._flags = num | num2;
			}

			// Token: 0x06008FE4 RID: 36836 RVA: 0x001FA0A7 File Offset: 0x001F82A7
			public void SetFieldDefinitionsNoted()
			{
				ThreadSafeFlagOperations.Set(ref this._flags, 256);
			}

			// Token: 0x06008FE5 RID: 36837 RVA: 0x001FA0BA File Offset: 0x001F82BA
			public void SetFlattenedMembersIsSorted()
			{
				ThreadSafeFlagOperations.Set(ref this._flags, 512);
			}

			// Token: 0x06008FE6 RID: 36838 RVA: 0x001FA0CD File Offset: 0x001F82CD
			private static bool BitsAreUnsetOrSame(int bits, int mask)
			{
				return (bits & mask) == 0 || (bits & mask) == mask;
			}

			// Token: 0x06008FE7 RID: 36839 RVA: 0x001FA0DC File Offset: 0x001F82DC
			public void SetManagedKind(ManagedKind managedKind)
			{
				int num = (int)((int)(managedKind & ManagedKind.Managed) << 6);
				ThreadSafeFlagOperations.Set(ref this._flags, num);
			}

			// Token: 0x06008FE8 RID: 36840 RVA: 0x001FA0FC File Offset: 0x001F82FC
			public bool TryGetNullableContext(out byte? value)
			{
				return ((NullableContextKind)((this._flags >> 14) & 7)).TryGetByte(out value);
			}

			// Token: 0x06008FE9 RID: 36841 RVA: 0x001FA110 File Offset: 0x001F8310
			public bool SetNullableContext(byte? value)
			{
				return ThreadSafeFlagOperations.Set(ref this._flags, (int)((int)(value.ToNullableContextFlags() & (NullableContextKind)7) << 14));
			}

			// Token: 0x0400277A RID: 10106
			private int _flags;

			// Token: 0x0400277B RID: 10107
			private const int SpecialTypeOffset = 0;

			// Token: 0x0400277C RID: 10108
			private const int SpecialTypeSize = 6;

			// Token: 0x0400277D RID: 10109
			private const int ManagedKindOffset = 6;

			// Token: 0x0400277E RID: 10110
			private const int ManagedKindSize = 2;

			// Token: 0x0400277F RID: 10111
			private const int FieldDefinitionsNotedOffset = 8;

			// Token: 0x04002780 RID: 10112
			private const int FieldDefinitionsNotedSize = 1;

			// Token: 0x04002781 RID: 10113
			private const int FlattenedMembersIsSortedOffset = 9;

			// Token: 0x04002782 RID: 10114
			private const int FlattenedMembersIsSortedSize = 1;

			// Token: 0x04002783 RID: 10115
			private const int TypeKindOffset = 10;

			// Token: 0x04002784 RID: 10116
			private const int TypeKindSize = 4;

			// Token: 0x04002785 RID: 10117
			private const int NullableContextOffset = 14;

			// Token: 0x04002786 RID: 10118
			private const int NullableContextSize = 3;

			// Token: 0x04002787 RID: 10119
			private const int SpecialTypeMask = 63;

			// Token: 0x04002788 RID: 10120
			private const int ManagedKindMask = 3;

			// Token: 0x04002789 RID: 10121
			private const int TypeKindMask = 15;

			// Token: 0x0400278A RID: 10122
			private const int NullableContextMask = 7;

			// Token: 0x0400278B RID: 10123
			private const int FieldDefinitionsNotedBit = 256;

			// Token: 0x0400278C RID: 10124
			private const int FlattenedMembersIsSortedBit = 512;
		}

		// Token: 0x02000921 RID: 2337
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected sealed class MembersAndInitializers
		{
			// Token: 0x06008FEA RID: 36842 RVA: 0x001FA128 File Offset: 0x001F8328
			public MembersAndInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> nonTypeMembers, ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> staticInitializers, ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> instanceInitializers, bool haveIndexers, bool isNullableEnabledForInstanceConstructorsAndFields, bool isNullableEnabledForStaticConstructorsAndFields)
			{
				this.NonTypeMembers = nonTypeMembers;
				this.StaticInitializers = staticInitializers;
				this.InstanceInitializers = instanceInitializers;
				this.HaveIndexers = haveIndexers;
				this.IsNullableEnabledForInstanceConstructorsAndFields = isNullableEnabledForInstanceConstructorsAndFields;
				this.IsNullableEnabledForStaticConstructorsAndFields = isNullableEnabledForStaticConstructorsAndFields;
			}

			// Token: 0x0400278D RID: 10125
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal readonly ImmutableArray<Symbol> NonTypeMembers;

			// Token: 0x0400278E RID: 10126
			internal readonly ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> StaticInitializers;

			// Token: 0x0400278F RID: 10127
			internal readonly ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> InstanceInitializers;

			// Token: 0x04002790 RID: 10128
			internal readonly bool HaveIndexers;

			// Token: 0x04002791 RID: 10129
			internal readonly bool IsNullableEnabledForInstanceConstructorsAndFields;

			// Token: 0x04002792 RID: 10130
			internal readonly bool IsNullableEnabledForStaticConstructorsAndFields;
		}

		// Token: 0x02000922 RID: 2338
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class DeclaredMembersAndInitializersBuilder
		{
			// Token: 0x06008FEB RID: 36843 RVA: 0x001FA160 File Offset: 0x001F8360
			public SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers ToReadOnlyAndFree(CSharpCompilation compilation)
			{
				return new SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers(this.NonTypeMembers.ToImmutableAndFree(), SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder.ToReadOnlyAndFree(this.StaticInitializers), SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder.ToReadOnlyAndFree(this.InstanceInitializers), this.HaveIndexers, this.RecordDeclarationWithParameters, this.RecordPrimaryConstructor, this.IsNullableEnabledForInstanceConstructorsAndFields, this.IsNullableEnabledForStaticConstructorsAndFields, compilation);
			}

			// Token: 0x06008FEC RID: 36844 RVA: 0x001FA1B2 File Offset: 0x001F83B2
			public void UpdateIsNullableEnabledForConstructorsAndFields(bool useStatic, CSharpCompilation compilation, CSharpSyntaxNode syntax)
			{
				ref bool isNullableEnabledForConstructorsAndFields = ref this.GetIsNullableEnabledForConstructorsAndFields(useStatic);
				isNullableEnabledForConstructorsAndFields = isNullableEnabledForConstructorsAndFields || compilation.IsNullableAnalysisEnabledIn(syntax);
			}

			// Token: 0x06008FED RID: 36845 RVA: 0x001FA1CA File Offset: 0x001F83CA
			public unsafe void UpdateIsNullableEnabledForConstructorsAndFields(bool useStatic, bool value)
			{
				*this.GetIsNullableEnabledForConstructorsAndFields(useStatic) = value;
			}

			// Token: 0x06008FEE RID: 36846 RVA: 0x001FA1D8 File Offset: 0x001F83D8
			private ref bool GetIsNullableEnabledForConstructorsAndFields(bool useStatic)
			{
				if (!useStatic)
				{
					return ref this.IsNullableEnabledForInstanceConstructorsAndFields;
				}
				return ref this.IsNullableEnabledForStaticConstructorsAndFields;
			}

			// Token: 0x06008FEF RID: 36847 RVA: 0x001FA1EC File Offset: 0x001F83EC
			public void Free()
			{
				this.NonTypeMembers.Free();
				foreach (ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder in this.StaticInitializers)
				{
					arrayBuilder.Free();
				}
				this.StaticInitializers.Free();
				foreach (ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder2 in this.InstanceInitializers)
				{
					arrayBuilder2.Free();
				}
				this.InstanceInitializers.Free();
			}

			// Token: 0x06008FF0 RID: 36848 RVA: 0x001FA260 File Offset: 0x001F8460
			internal void AddOrWrapTupleMembers(SourceMemberContainerTypeSymbol type)
			{
				this.NonTypeMembers = type.AddOrWrapTupleMembers(this.NonTypeMembers.ToImmutableAndFree());
			}

			// Token: 0x04002793 RID: 10131
			public ArrayBuilder<Symbol> NonTypeMembers = ArrayBuilder<Symbol>.GetInstance();

			// Token: 0x04002794 RID: 10132
			public readonly ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>> StaticInitializers = ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>>.GetInstance();

			// Token: 0x04002795 RID: 10133
			public readonly ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>> InstanceInitializers = ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>>.GetInstance();

			// Token: 0x04002796 RID: 10134
			public bool HaveIndexers;

			// Token: 0x04002797 RID: 10135
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public RecordDeclarationSyntax RecordDeclarationWithParameters;

			// Token: 0x04002798 RID: 10136
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public SynthesizedRecordConstructor RecordPrimaryConstructor;

			// Token: 0x04002799 RID: 10137
			public bool IsNullableEnabledForInstanceConstructorsAndFields;

			// Token: 0x0400279A RID: 10138
			public bool IsNullableEnabledForStaticConstructorsAndFields;
		}

		// Token: 0x02000923 RID: 2339
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected sealed class DeclaredMembersAndInitializers
		{
			// Token: 0x06008FF2 RID: 36850 RVA: 0x001FA2A2 File Offset: 0x001F84A2
			private DeclaredMembersAndInitializers()
			{
			}

			// Token: 0x06008FF3 RID: 36851 RVA: 0x001FA2AC File Offset: 0x001F84AC
			public DeclaredMembersAndInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> nonTypeMembers, ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> staticInitializers, ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> instanceInitializers, bool haveIndexers, [global::System.Runtime.CompilerServices.Nullable(2)] RecordDeclarationSyntax recordDeclarationWithParameters, [global::System.Runtime.CompilerServices.Nullable(2)] SynthesizedRecordConstructor recordPrimaryConstructor, bool isNullableEnabledForInstanceConstructorsAndFields, bool isNullableEnabledForStaticConstructorsAndFields, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
			{
				this.NonTypeMembers = nonTypeMembers;
				this.StaticInitializers = staticInitializers;
				this.InstanceInitializers = instanceInitializers;
				this.HaveIndexers = haveIndexers;
				this.RecordDeclarationWithParameters = recordDeclarationWithParameters;
				this.RecordPrimaryConstructor = recordPrimaryConstructor;
				this.IsNullableEnabledForInstanceConstructorsAndFields = isNullableEnabledForInstanceConstructorsAndFields;
				this.IsNullableEnabledForStaticConstructorsAndFields = isNullableEnabledForStaticConstructorsAndFields;
			}

			// Token: 0x06008FF4 RID: 36852 RVA: 0x001FA2FC File Offset: 0x001F84FC
			[Conditional("DEBUG")]
			public static void AssertInitializers(ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> initializers, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
			{
				if (initializers.IsEmpty)
				{
					return;
				}
				foreach (ImmutableArray<FieldOrPropertyInitializer> immutableArray in initializers)
				{
				}
				for (int i = 0; i < initializers.Length; i++)
				{
					int num = i + 1;
					int length = initializers.Length;
					int length2 = initializers[i].Length;
				}
			}

			// Token: 0x0400279B RID: 10139
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableArray<Symbol> NonTypeMembers;

			// Token: 0x0400279C RID: 10140
			public readonly ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> StaticInitializers;

			// Token: 0x0400279D RID: 10141
			public readonly ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> InstanceInitializers;

			// Token: 0x0400279E RID: 10142
			public readonly bool HaveIndexers;

			// Token: 0x0400279F RID: 10143
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public readonly RecordDeclarationSyntax RecordDeclarationWithParameters;

			// Token: 0x040027A0 RID: 10144
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public readonly SynthesizedRecordConstructor RecordPrimaryConstructor;

			// Token: 0x040027A1 RID: 10145
			public readonly bool IsNullableEnabledForInstanceConstructorsAndFields;

			// Token: 0x040027A2 RID: 10146
			public readonly bool IsNullableEnabledForStaticConstructorsAndFields;

			// Token: 0x040027A3 RID: 10147
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers UninitializedSentinel = new SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers();
		}

		// Token: 0x02000924 RID: 2340
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class MembersAndInitializersBuilder
		{
			// Token: 0x06008FF6 RID: 36854 RVA: 0x001FA36F File Offset: 0x001F856F
			public MembersAndInitializersBuilder(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembersAndInitializers)
			{
				this.IsNullableEnabledForInstanceConstructorsAndFields = declaredMembersAndInitializers.IsNullableEnabledForInstanceConstructorsAndFields;
				this.IsNullableEnabledForStaticConstructorsAndFields = declaredMembersAndInitializers.IsNullableEnabledForStaticConstructorsAndFields;
			}

			// Token: 0x06008FF7 RID: 36855 RVA: 0x001FA390 File Offset: 0x001F8590
			public SourceMemberContainerTypeSymbol.MembersAndInitializers ToReadOnlyAndFree(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembers)
			{
				SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder.<>c__DisplayClass5_0 CS$<>8__locals1;
				CS$<>8__locals1.declaredMembers = declaredMembers;
				CS$<>8__locals1.<>4__this = this;
				ArrayBuilder<Symbol> nonTypeMembers = this.NonTypeMembers;
				ImmutableArray<Symbol> immutableArray = ((nonTypeMembers != null) ? nonTypeMembers.ToImmutableAndFree() : CS$<>8__locals1.declaredMembers.NonTypeMembers);
				ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> immutableArray2 = ((this.InstanceInitializersForPositionalMembers == null) ? CS$<>8__locals1.declaredMembers.InstanceInitializers : this.<ToReadOnlyAndFree>g__mergeInitializers|5_0(ref CS$<>8__locals1));
				return new SourceMemberContainerTypeSymbol.MembersAndInitializers(immutableArray, CS$<>8__locals1.declaredMembers.StaticInitializers, immutableArray2, CS$<>8__locals1.declaredMembers.HaveIndexers, this.IsNullableEnabledForInstanceConstructorsAndFields, this.IsNullableEnabledForStaticConstructorsAndFields);
			}

			// Token: 0x06008FF8 RID: 36856 RVA: 0x001FA40F File Offset: 0x001F860F
			public void AddInstanceInitializerForPositionalMembers(FieldOrPropertyInitializer initializer)
			{
				if (this.InstanceInitializersForPositionalMembers == null)
				{
					this.InstanceInitializersForPositionalMembers = ArrayBuilder<FieldOrPropertyInitializer>.GetInstance();
				}
				this.InstanceInitializersForPositionalMembers.Add(initializer);
			}

			// Token: 0x06008FF9 RID: 36857 RVA: 0x001FA430 File Offset: 0x001F8630
			public IReadOnlyCollection<Symbol> GetNonTypeMembers(SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembers)
			{
				IReadOnlyCollection<Symbol> nonTypeMembers = this.NonTypeMembers;
				return nonTypeMembers ?? declaredMembers.NonTypeMembers;
			}

			// Token: 0x06008FFA RID: 36858 RVA: 0x001FA454 File Offset: 0x001F8654
			public void AddNonTypeMember(Symbol member, SourceMemberContainerTypeSymbol.DeclaredMembersAndInitializers declaredMembers)
			{
				if (this.NonTypeMembers == null)
				{
					this.NonTypeMembers = ArrayBuilder<Symbol>.GetInstance(declaredMembers.NonTypeMembers.Length + 1);
					this.NonTypeMembers.AddRange(declaredMembers.NonTypeMembers);
				}
				this.NonTypeMembers.Add(member);
			}

			// Token: 0x06008FFB RID: 36859 RVA: 0x001FA4A1 File Offset: 0x001F86A1
			public void UpdateIsNullableEnabledForConstructorsAndFields(bool useStatic, CSharpCompilation compilation, CSharpSyntaxNode syntax)
			{
				ref bool isNullableEnabledForConstructorsAndFields = ref this.GetIsNullableEnabledForConstructorsAndFields(useStatic);
				isNullableEnabledForConstructorsAndFields = isNullableEnabledForConstructorsAndFields || compilation.IsNullableAnalysisEnabledIn(syntax);
			}

			// Token: 0x06008FFC RID: 36860 RVA: 0x001FA4B9 File Offset: 0x001F86B9
			private ref bool GetIsNullableEnabledForConstructorsAndFields(bool useStatic)
			{
				if (!useStatic)
				{
					return ref this.IsNullableEnabledForInstanceConstructorsAndFields;
				}
				return ref this.IsNullableEnabledForStaticConstructorsAndFields;
			}

			// Token: 0x06008FFD RID: 36861 RVA: 0x001FA4CC File Offset: 0x001F86CC
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal static ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> ToReadOnlyAndFree([global::System.Runtime.CompilerServices.Nullable(1)] ArrayBuilder<ArrayBuilder<FieldOrPropertyInitializer>> initializers)
			{
				if (initializers.Count == 0)
				{
					initializers.Free();
					return ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>>.Empty;
				}
				ArrayBuilder<ImmutableArray<FieldOrPropertyInitializer>> instance = ArrayBuilder<ImmutableArray<FieldOrPropertyInitializer>>.GetInstance(initializers.Count);
				foreach (ArrayBuilder<FieldOrPropertyInitializer> arrayBuilder in initializers)
				{
					instance.Add(arrayBuilder.ToImmutableAndFree());
				}
				initializers.Free();
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06008FFE RID: 36862 RVA: 0x001FA52B File Offset: 0x001F872B
			public void Free()
			{
				ArrayBuilder<Symbol> nonTypeMembers = this.NonTypeMembers;
				if (nonTypeMembers != null)
				{
					nonTypeMembers.Free();
				}
				ArrayBuilder<FieldOrPropertyInitializer> instanceInitializersForPositionalMembers = this.InstanceInitializersForPositionalMembers;
				if (instanceInitializersForPositionalMembers == null)
				{
					return;
				}
				instanceInitializersForPositionalMembers.Free();
			}

			// Token: 0x06008FFF RID: 36863 RVA: 0x001FA550 File Offset: 0x001F8750
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			private ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> <ToReadOnlyAndFree>g__mergeInitializers|5_0(ref SourceMemberContainerTypeSymbol.MembersAndInitializersBuilder.<>c__DisplayClass5_0 A_1)
			{
				int length = A_1.declaredMembers.InstanceInitializers.Length;
				if (length == 0)
				{
					return ImmutableArray.Create<ImmutableArray<FieldOrPropertyInitializer>>(this.InstanceInitializersForPositionalMembers.ToImmutableAndFree());
				}
				CSharpCompilation declaringCompilation = A_1.declaredMembers.RecordPrimaryConstructor.DeclaringCompilation;
				LexicalSortKey lexicalSortKey = new LexicalSortKey(this.InstanceInitializersForPositionalMembers.First().Syntax, declaringCompilation);
				int num = 0;
				while (num < length && LexicalSortKey.Compare(lexicalSortKey, new LexicalSortKey(A_1.declaredMembers.InstanceInitializers[num][0].Syntax, declaringCompilation)) >= 0)
				{
					num++;
				}
				ArrayBuilder<ImmutableArray<FieldOrPropertyInitializer>> arrayBuilder;
				if (num != length && A_1.declaredMembers.RecordDeclarationWithParameters.SyntaxTree == A_1.declaredMembers.InstanceInitializers[num][0].Syntax.SyntaxTree && A_1.declaredMembers.RecordDeclarationWithParameters.Span.Contains(A_1.declaredMembers.InstanceInitializers[num][0].Syntax.Span.Start))
				{
					ImmutableArray<FieldOrPropertyInitializer> immutableArray = A_1.declaredMembers.InstanceInitializers[num];
					ArrayBuilder<FieldOrPropertyInitializer> instanceInitializersForPositionalMembers = this.InstanceInitializersForPositionalMembers;
					instanceInitializersForPositionalMembers.AddRange(immutableArray);
					arrayBuilder = ArrayBuilder<ImmutableArray<FieldOrPropertyInitializer>>.GetInstance(length);
					arrayBuilder.AddRange(A_1.declaredMembers.InstanceInitializers, num);
					arrayBuilder.Add(instanceInitializersForPositionalMembers.ToImmutableAndFree());
					arrayBuilder.AddRange(A_1.declaredMembers.InstanceInitializers, num + 1, length - (num + 1));
				}
				else
				{
					arrayBuilder = ArrayBuilder<ImmutableArray<FieldOrPropertyInitializer>>.GetInstance(length + 1);
					arrayBuilder.AddRange(A_1.declaredMembers.InstanceInitializers, num);
					arrayBuilder.Add(this.InstanceInitializersForPositionalMembers.ToImmutableAndFree());
					arrayBuilder.AddRange(A_1.declaredMembers.InstanceInitializers, num, length - num);
				}
				return arrayBuilder.ToImmutableAndFree();
			}

			// Token: 0x040027A4 RID: 10148
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			public ArrayBuilder<Symbol> NonTypeMembers;

			// Token: 0x040027A5 RID: 10149
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private ArrayBuilder<FieldOrPropertyInitializer> InstanceInitializersForPositionalMembers;

			// Token: 0x040027A6 RID: 10150
			private bool IsNullableEnabledForInstanceConstructorsAndFields;

			// Token: 0x040027A7 RID: 10151
			private bool IsNullableEnabledForStaticConstructorsAndFields;
		}

		// Token: 0x02000925 RID: 2341
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum HasBaseTypeDeclaringInterfaceResult
		{
			// Token: 0x040027A9 RID: 10153
			NoMatch,
			// Token: 0x040027AA RID: 10154
			IgnoringNullableMatch,
			// Token: 0x040027AB RID: 10155
			ExactMatch
		}
	}
}
