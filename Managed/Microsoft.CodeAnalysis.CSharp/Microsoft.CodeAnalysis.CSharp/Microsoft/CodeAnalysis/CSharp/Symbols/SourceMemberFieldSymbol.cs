using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000540 RID: 1344
	internal abstract class SourceMemberFieldSymbol : SourceFieldSymbolWithSyntaxReference
	{
		// Token: 0x0600712B RID: 28971 RVA: 0x001A3E93 File Offset: 0x001A2093
		internal SourceMemberFieldSymbol(SourceMemberContainerTypeSymbol containingType, DeclarationModifiers modifiers, string name, SyntaxReference syntax, Location location)
			: base(containingType, name, syntax, location)
		{
			this._modifiers = modifiers;
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x0600712C RID: 28972 RVA: 0x001A3EA8 File Offset: 0x001A20A8
		protected sealed override DeclarationModifiers Modifiers
		{
			get
			{
				return this._modifiers;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x0600712D RID: 28973
		protected abstract TypeSyntax TypeSyntax { get; }

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x0600712E RID: 28974
		protected abstract SyntaxTokenList ModifiersTokenList { get; }

		// Token: 0x0600712F RID: 28975 RVA: 0x001A3EB0 File Offset: 0x001A20B0
		protected void TypeChecks(TypeSymbol type, BindingDiagnosticBag diagnostics)
		{
			if (type.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_VarDeclIsStaticClass, this.ErrorLocation, new object[] { type });
			}
			else if (type.IsVoidType())
			{
				ErrorCode errorCode = ErrorCode.ERR_FieldCantHaveVoidType;
				TypeSyntax typeSyntax = this.TypeSyntax;
				diagnostics.Add(errorCode, ((typeSyntax != null) ? typeSyntax.Location : null) ?? this.Locations[0]);
			}
			else if (type.IsRestrictedType(true))
			{
				ErrorCode errorCode2 = ErrorCode.ERR_FieldCantBeRefAny;
				TypeSyntax typeSyntax2 = this.TypeSyntax;
				diagnostics.Add(errorCode2, ((typeSyntax2 != null) ? typeSyntax2.Location : null) ?? this.Locations[0], new object[] { type });
			}
			else if (type.IsRefLikeType && (this.IsStatic || !this.containingType.IsRefLikeType))
			{
				ErrorCode errorCode3 = ErrorCode.ERR_FieldAutoPropCantBeByRefLike;
				TypeSyntax typeSyntax3 = this.TypeSyntax;
				diagnostics.Add(errorCode3, ((typeSyntax3 != null) ? typeSyntax3.Location : null) ?? this.Locations[0], new object[] { type });
			}
			else if (this.IsConst && !type.CanBeConst())
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				foreach (SyntaxToken syntaxToken2 in this.ModifiersTokenList)
				{
					if (syntaxToken2.Kind() == SyntaxKind.ConstKeyword)
					{
						syntaxToken = syntaxToken2;
						break;
					}
				}
				diagnostics.Add(ErrorCode.ERR_BadConstType, syntaxToken.GetLocation(), new object[] { type });
			}
			else if (this.IsVolatile && !type.IsValidVolatileFieldType())
			{
				diagnostics.Add(ErrorCode.ERR_VolatileStruct, this.ErrorLocation, new object[] { this, type });
			}
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.IsNoMoreVisibleThan(type, ref compoundUseSiteInfo))
			{
				diagnostics.Add(ErrorCode.ERR_BadVisFieldType, this.ErrorLocation, new object[] { this, type });
			}
			diagnostics.Add(this.ErrorLocation, compoundUseSiteInfo);
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x06007130 RID: 28976
		public abstract bool HasInitializer { get; }

		// Token: 0x06007131 RID: 28977 RVA: 0x001A40AC File Offset: 0x001A22AC
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			ConstantValue constantValue = this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
			if (this.IsConst && constantValue != null && base.Type.SpecialType == SpecialType.System_Decimal)
			{
				FieldWellKnownAttributeData decodedWellKnownAttributeData = base.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null || decodedWellKnownAttributeData.ConstValue == Microsoft.CodeAnalysis.ConstantValue.Unset)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDecimalConstantAttribute(constantValue.DecimalValue));
				}
			}
		}

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06007132 RID: 28978 RVA: 0x001A4123 File Offset: 0x001A2323
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06007133 RID: 28979 RVA: 0x001A4126 File Offset: 0x001A2326
		public override int FixedSize
		{
			get
			{
				this.state.NotePartComplete(CompletionPart.Members);
				return 0;
			}
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x001A413C File Offset: 0x001A233C
		internal static DeclarationModifiers MakeModifiers(NamedTypeSymbol containingType, SyntaxToken firstIdentifier, SyntaxTokenList modifiers, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			DeclarationModifiers declarationModifiers = (containingType.IsInterface ? DeclarationModifiers.Public : DeclarationModifiers.Private);
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.Abstract | DeclarationModifiers.Static | DeclarationModifiers.New | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.ReadOnly | DeclarationModifiers.Const | DeclarationModifiers.Volatile | DeclarationModifiers.Unsafe | DeclarationModifiers.Fixed;
			SourceLocation sourceLocation = new SourceLocation(in firstIdentifier);
			DeclarationModifiers declarationModifiers3 = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, declarationModifiers, declarationModifiers2, sourceLocation, diagnostics, out modifierErrors);
			if ((declarationModifiers3 & DeclarationModifiers.Abstract) != DeclarationModifiers.None)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractField, sourceLocation);
				declarationModifiers3 &= ~DeclarationModifiers.Abstract;
			}
			if ((declarationModifiers3 & DeclarationModifiers.Fixed) != DeclarationModifiers.None)
			{
				if ((declarationModifiers3 & DeclarationModifiers.Static) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.StaticKeyword) });
				}
				if ((declarationModifiers3 & DeclarationModifiers.ReadOnly) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword) });
				}
				if ((declarationModifiers3 & DeclarationModifiers.Const) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.ConstKeyword) });
				}
				if ((declarationModifiers3 & DeclarationModifiers.Volatile) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.VolatileKeyword) });
				}
				declarationModifiers3 &= ~(DeclarationModifiers.Static | DeclarationModifiers.ReadOnly | DeclarationModifiers.Const | DeclarationModifiers.Volatile);
			}
			if ((declarationModifiers3 & DeclarationModifiers.Const) != DeclarationModifiers.None)
			{
				if ((declarationModifiers3 & DeclarationModifiers.Static) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_StaticConstant, sourceLocation, new object[] { firstIdentifier.ValueText });
				}
				if ((declarationModifiers3 & DeclarationModifiers.ReadOnly) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword) });
				}
				if ((declarationModifiers3 & DeclarationModifiers.Volatile) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.VolatileKeyword) });
				}
				if ((declarationModifiers3 & DeclarationModifiers.Unsafe) != DeclarationModifiers.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, sourceLocation, new object[] { SyntaxFacts.GetText(SyntaxKind.UnsafeKeyword) });
				}
				declarationModifiers3 |= DeclarationModifiers.Static;
			}
			else
			{
				containingType.CheckUnsafeModifier(declarationModifiers3, sourceLocation, diagnostics);
			}
			return declarationModifiers3;
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x001A42E8 File Offset: 0x001A24E8
		internal sealed override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this.state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.Attributes)
				{
					if (nextIncompletePart == CompletionPart.None)
					{
						break;
					}
					if (nextIncompletePart != CompletionPart.Attributes)
					{
						goto IL_0066;
					}
					this.GetAttributes();
				}
				else if (nextIncompletePart != CompletionPart.Type)
				{
					if (nextIncompletePart != CompletionPart.Members)
					{
						if (nextIncompletePart != CompletionPart.TypeMembers)
						{
							goto IL_0066;
						}
						this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
					}
					else
					{
						int fixedSize = this.FixedSize;
					}
				}
				else
				{
					this.GetFieldType(ConsList<FieldSymbol>.Empty);
				}
				IL_0077:
				this.state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_0066:
				this.state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.StartBaseType | CompletionPart.FinishBaseType | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
				goto IL_0077;
			}
		}

		// Token: 0x06007136 RID: 28982 RVA: 0x001A437D File Offset: 0x001A257D
		internal override NamedTypeSymbol FixedImplementationType(PEModuleBuilder emitModule)
		{
			return null;
		}

		// Token: 0x04001BA3 RID: 7075
		private readonly DeclarationModifiers _modifiers;
	}
}
