using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000524 RID: 1316
	internal static class ModifierUtils
	{
		// Token: 0x06006EA2 RID: 28322 RVA: 0x00195CCC File Offset: 0x00193ECC
		internal static DeclarationModifiers MakeAndCheckNontypeMemberModifiers(SyntaxTokenList modifiers, DeclarationModifiers defaultAccess, DeclarationModifiers allowedModifiers, Location errorLocation, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			DeclarationModifiers declarationModifiers = modifiers.ToDeclarationModifiers(diagnostics.DiagnosticBag ?? new DiagnosticBag());
			declarationModifiers = ModifierUtils.CheckModifiers(declarationModifiers, allowedModifiers, errorLocation, diagnostics, new SyntaxTokenList?(modifiers), out modifierErrors);
			if ((declarationModifiers & DeclarationModifiers.AccessibilityMask) == DeclarationModifiers.None)
			{
				declarationModifiers |= defaultAccess;
			}
			return declarationModifiers;
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x00195D14 File Offset: 0x00193F14
		internal static DeclarationModifiers CheckModifiers(DeclarationModifiers modifiers, DeclarationModifiers allowedModifiers, Location errorLocation, BindingDiagnosticBag diagnostics, SyntaxTokenList? modifierTokens, out bool modifierErrors)
		{
			modifierErrors = false;
			DeclarationModifiers declarationModifiers = modifiers & ~allowedModifiers;
			DeclarationModifiers declarationModifiers2 = modifiers & allowedModifiers;
			while (declarationModifiers != DeclarationModifiers.None)
			{
				DeclarationModifiers declarationModifiers3 = declarationModifiers & ~(declarationModifiers - 1U);
				declarationModifiers &= ~declarationModifiers3;
				if (declarationModifiers3 == DeclarationModifiers.Partial)
				{
					ModifierUtils.ReportPartialError(errorLocation, diagnostics, modifierTokens);
				}
				else
				{
					diagnostics.Add(ErrorCode.ERR_BadMemberFlag, errorLocation, new object[] { ModifierUtils.ConvertSingleModifierToSyntaxText(declarationModifiers3) });
				}
				modifierErrors = true;
			}
			if ((declarationModifiers2 & DeclarationModifiers.PrivateProtected) != DeclarationModifiers.None)
			{
				modifierErrors |= !Binder.CheckFeatureAvailability(errorLocation.SourceTree, MessageID.IDS_FeaturePrivateProtected, diagnostics, errorLocation);
			}
			return declarationModifiers2;
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x00195D98 File Offset: 0x00193F98
		private static void ReportPartialError(Location errorLocation, BindingDiagnosticBag diagnostics, SyntaxTokenList? modifierTokens)
		{
			if (modifierTokens != null)
			{
				SyntaxToken syntaxToken = modifierTokens.Value.FirstOrDefault(SyntaxKind.PartialKeyword);
				if (syntaxToken != default(SyntaxToken))
				{
					diagnostics.Add(ErrorCode.ERR_PartialMisplaced, syntaxToken.GetLocation());
					return;
				}
			}
			diagnostics.Add(ErrorCode.ERR_PartialMisplaced, errorLocation);
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x00195DF4 File Offset: 0x00193FF4
		internal static void ReportDefaultInterfaceImplementationModifiers(bool hasBody, DeclarationModifiers modifiers, DeclarationModifiers defaultInterfaceImplementationModifiers, Location errorLocation, BindingDiagnosticBag diagnostics)
		{
			if (!hasBody && (modifiers & defaultInterfaceImplementationModifiers) != DeclarationModifiers.None)
			{
				LanguageVersion languageVersion = ((CSharpParseOptions)errorLocation.SourceTree.Options).LanguageVersion;
				LanguageVersion languageVersion2 = MessageID.IDS_DefaultInterfaceImplementation.RequiredVersion();
				if (languageVersion < languageVersion2)
				{
					DeclarationModifiers declarationModifiers = modifiers & defaultInterfaceImplementationModifiers;
					CSharpRequiredLanguageVersion csharpRequiredLanguageVersion = new CSharpRequiredLanguageVersion(languageVersion2);
					string text = languageVersion.ToDisplayString();
					while (declarationModifiers != DeclarationModifiers.None)
					{
						DeclarationModifiers declarationModifiers2 = declarationModifiers & ~(declarationModifiers - 1U);
						declarationModifiers &= ~declarationModifiers2;
						diagnostics.Add(ErrorCode.ERR_InvalidModifierForLanguageVersion, errorLocation, new object[]
						{
							ModifierUtils.ConvertSingleModifierToSyntaxText(declarationModifiers2),
							text,
							csharpRequiredLanguageVersion
						});
					}
				}
			}
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x00195E7C File Offset: 0x0019407C
		internal static DeclarationModifiers AdjustModifiersForAnInterfaceMember(DeclarationModifiers mods, bool hasBody, bool isExplicitInterfaceImplementation)
		{
			if (isExplicitInterfaceImplementation)
			{
				if ((mods & DeclarationModifiers.Abstract) != DeclarationModifiers.None)
				{
					mods |= DeclarationModifiers.Sealed;
				}
			}
			else if ((mods & (DeclarationModifiers.Abstract | DeclarationModifiers.Static | DeclarationModifiers.Private | DeclarationModifiers.Partial | DeclarationModifiers.Virtual)) == DeclarationModifiers.None)
			{
				if (hasBody || (mods & (DeclarationModifiers.Sealed | DeclarationModifiers.Extern)) != DeclarationModifiers.None)
				{
					if ((mods & DeclarationModifiers.Sealed) == DeclarationModifiers.None)
					{
						mods |= DeclarationModifiers.Virtual;
					}
					else
					{
						mods &= ~DeclarationModifiers.Sealed;
					}
				}
				else
				{
					mods |= DeclarationModifiers.Abstract;
				}
			}
			if ((mods & DeclarationModifiers.AccessibilityMask) == DeclarationModifiers.None)
			{
				if ((mods & DeclarationModifiers.Partial) == DeclarationModifiers.None && !isExplicitInterfaceImplementation)
				{
					mods |= DeclarationModifiers.Public;
				}
				else
				{
					mods |= DeclarationModifiers.Private;
				}
			}
			return mods;
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x00195EF4 File Offset: 0x001940F4
		private static string ConvertSingleModifierToSyntaxText(DeclarationModifiers modifier)
		{
			if (modifier <= DeclarationModifiers.Const)
			{
				if (modifier <= DeclarationModifiers.Internal)
				{
					if (modifier <= DeclarationModifiers.New)
					{
						switch (modifier)
						{
						case DeclarationModifiers.Abstract:
							return SyntaxFacts.GetText(SyntaxKind.AbstractKeyword);
						case DeclarationModifiers.Sealed:
							return SyntaxFacts.GetText(SyntaxKind.SealedKeyword);
						case DeclarationModifiers.Abstract | DeclarationModifiers.Sealed:
							break;
						case DeclarationModifiers.Static:
							return SyntaxFacts.GetText(SyntaxKind.StaticKeyword);
						default:
							if (modifier == DeclarationModifiers.New)
							{
								return SyntaxFacts.GetText(SyntaxKind.NewKeyword);
							}
							break;
						}
					}
					else
					{
						if (modifier == DeclarationModifiers.Public)
						{
							return SyntaxFacts.GetText(SyntaxKind.PublicKeyword);
						}
						if (modifier == DeclarationModifiers.Protected)
						{
							return SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword);
						}
						if (modifier == DeclarationModifiers.Internal)
						{
							return SyntaxFacts.GetText(SyntaxKind.InternalKeyword);
						}
					}
				}
				else if (modifier <= DeclarationModifiers.Private)
				{
					if (modifier == DeclarationModifiers.ProtectedInternal)
					{
						return SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword) + " " + SyntaxFacts.GetText(SyntaxKind.InternalKeyword);
					}
					if (modifier == DeclarationModifiers.Private)
					{
						return SyntaxFacts.GetText(SyntaxKind.PrivateKeyword);
					}
				}
				else
				{
					if (modifier == DeclarationModifiers.PrivateProtected)
					{
						return SyntaxFacts.GetText(SyntaxKind.PrivateKeyword) + " " + SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword);
					}
					if (modifier == DeclarationModifiers.ReadOnly)
					{
						return SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword);
					}
					if (modifier == DeclarationModifiers.Const)
					{
						return SyntaxFacts.GetText(SyntaxKind.ConstKeyword);
					}
				}
			}
			else if (modifier <= DeclarationModifiers.Fixed)
			{
				if (modifier <= DeclarationModifiers.Extern)
				{
					if (modifier == DeclarationModifiers.Volatile)
					{
						return SyntaxFacts.GetText(SyntaxKind.VolatileKeyword);
					}
					if (modifier == DeclarationModifiers.Extern)
					{
						return SyntaxFacts.GetText(SyntaxKind.ExternKeyword);
					}
				}
				else
				{
					if (modifier == DeclarationModifiers.Partial)
					{
						return SyntaxFacts.GetText(SyntaxKind.PartialKeyword);
					}
					if (modifier == DeclarationModifiers.Unsafe)
					{
						return SyntaxFacts.GetText(SyntaxKind.UnsafeKeyword);
					}
					if (modifier == DeclarationModifiers.Fixed)
					{
						return SyntaxFacts.GetText(SyntaxKind.FixedKeyword);
					}
				}
			}
			else if (modifier <= DeclarationModifiers.Override)
			{
				if (modifier == DeclarationModifiers.Virtual)
				{
					return SyntaxFacts.GetText(SyntaxKind.VirtualKeyword);
				}
				if (modifier == DeclarationModifiers.Override)
				{
					return SyntaxFacts.GetText(SyntaxKind.OverrideKeyword);
				}
			}
			else
			{
				if (modifier == DeclarationModifiers.Async)
				{
					return SyntaxFacts.GetText(SyntaxKind.AsyncKeyword);
				}
				if (modifier == DeclarationModifiers.Ref)
				{
					return SyntaxFacts.GetText(SyntaxKind.RefKeyword);
				}
				if (modifier == DeclarationModifiers.Data)
				{
					return SyntaxFacts.GetText(SyntaxKind.DataKeyword);
				}
			}
			throw ExceptionUtilities.UnexpectedValue(modifier);
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x00196160 File Offset: 0x00194360
		private static DeclarationModifiers ToDeclarationModifier(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.UnsafeKeyword)
			{
				switch (kind)
				{
				case SyntaxKind.PublicKeyword:
					return DeclarationModifiers.Public;
				case SyntaxKind.PrivateKeyword:
					return DeclarationModifiers.Private;
				case SyntaxKind.InternalKeyword:
					return DeclarationModifiers.Internal;
				case SyntaxKind.ProtectedKeyword:
					return DeclarationModifiers.Protected;
				case SyntaxKind.StaticKeyword:
					return DeclarationModifiers.Static;
				case SyntaxKind.ReadOnlyKeyword:
					return DeclarationModifiers.ReadOnly;
				case SyntaxKind.SealedKeyword:
					return DeclarationModifiers.Sealed;
				case SyntaxKind.ConstKeyword:
					return DeclarationModifiers.Const;
				case SyntaxKind.FixedKeyword:
					return DeclarationModifiers.Fixed;
				case SyntaxKind.StackAllocKeyword:
				case SyntaxKind.EventKeyword:
					break;
				case SyntaxKind.VolatileKeyword:
					return DeclarationModifiers.Volatile;
				case SyntaxKind.NewKeyword:
					return DeclarationModifiers.New;
				case SyntaxKind.OverrideKeyword:
					return DeclarationModifiers.Override;
				case SyntaxKind.AbstractKeyword:
					return DeclarationModifiers.Abstract;
				case SyntaxKind.VirtualKeyword:
					return DeclarationModifiers.Virtual;
				case SyntaxKind.ExternKeyword:
					return DeclarationModifiers.Extern;
				case SyntaxKind.RefKeyword:
					return DeclarationModifiers.Ref;
				default:
					if (kind == SyntaxKind.UnsafeKeyword)
					{
						return DeclarationModifiers.Unsafe;
					}
					break;
				}
			}
			else
			{
				if (kind == SyntaxKind.PartialKeyword)
				{
					return DeclarationModifiers.Partial;
				}
				if (kind == SyntaxKind.AsyncKeyword)
				{
					return DeclarationModifiers.Async;
				}
				if (kind == SyntaxKind.DataKeyword)
				{
					return DeclarationModifiers.Data;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(kind);
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x00196258 File Offset: 0x00194458
		public static DeclarationModifiers ToDeclarationModifiers(this SyntaxTokenList modifiers, DiagnosticBag diagnostics)
		{
			DeclarationModifiers declarationModifiers = DeclarationModifiers.None;
			bool flag = true;
			bool flag2 = true;
			foreach (SyntaxToken syntaxToken in modifiers)
			{
				DeclarationModifiers declarationModifiers2 = ModifierUtils.ToDeclarationModifier(syntaxToken.ContextualKind());
				ModifierUtils.ReportDuplicateModifiers(syntaxToken, declarationModifiers2, declarationModifiers, ref flag, ref flag2, diagnostics);
				declarationModifiers |= declarationModifiers2;
			}
			DeclarationModifiers declarationModifiers3 = declarationModifiers & DeclarationModifiers.AccessibilityMask;
			if (declarationModifiers3 != (DeclarationModifiers.Protected | DeclarationModifiers.Internal))
			{
				if (declarationModifiers3 == (DeclarationModifiers.Protected | DeclarationModifiers.Private))
				{
					declarationModifiers &= ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected);
					declarationModifiers |= DeclarationModifiers.PrivateProtected;
				}
			}
			else
			{
				declarationModifiers &= ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected);
				declarationModifiers |= DeclarationModifiers.ProtectedInternal;
			}
			return declarationModifiers;
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x001962E1 File Offset: 0x001944E1
		private static void ReportDuplicateModifiers(SyntaxToken modifierToken, DeclarationModifiers modifierKind, DeclarationModifiers allModifiers, ref bool seenNoDuplicates, ref bool seenNoAccessibilityDuplicates, DiagnosticBag diagnostics)
		{
			if ((allModifiers & modifierKind) != DeclarationModifiers.None && seenNoDuplicates)
			{
				diagnostics.Add(ErrorCode.ERR_DuplicateModifier, modifierToken.GetLocation(), new object[] { SyntaxFacts.GetText(modifierToken.Kind()) });
				seenNoDuplicates = false;
			}
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x00196318 File Offset: 0x00194518
		internal static CSDiagnosticInfo CheckAccessibility(DeclarationModifiers modifiers, Symbol symbol, bool isExplicitInterfaceImplementation)
		{
			if (!ModifierUtils.IsValidAccessibility(modifiers))
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_BadMemberProtection);
			}
			if (!isExplicitInterfaceImplementation && (symbol.Kind != SymbolKind.Method || (modifiers & DeclarationModifiers.Partial) == DeclarationModifiers.None) && (modifiers & DeclarationModifiers.Static) == DeclarationModifiers.None)
			{
				DeclarationModifiers declarationModifiers = modifiers & DeclarationModifiers.AccessibilityMask;
				if (declarationModifiers == DeclarationModifiers.Protected || declarationModifiers == DeclarationModifiers.ProtectedInternal || declarationModifiers == DeclarationModifiers.PrivateProtected)
				{
					NamedTypeSymbol containingType = symbol.ContainingType;
					if (containingType != null && containingType.IsInterface && !symbol.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
					{
						return new CSDiagnosticInfo(ErrorCode.ERR_RuntimeDoesNotSupportProtectedAccessForInterfaceMember);
					}
				}
			}
			return null;
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x0019639C File Offset: 0x0019459C
		internal static Accessibility EffectiveAccessibility(DeclarationModifiers modifiers)
		{
			DeclarationModifiers declarationModifiers = modifiers & DeclarationModifiers.AccessibilityMask;
			if (declarationModifiers <= DeclarationModifiers.Protected)
			{
				if (declarationModifiers == DeclarationModifiers.None)
				{
					return Accessibility.NotApplicable;
				}
				if (declarationModifiers == DeclarationModifiers.Public)
				{
					return Accessibility.Public;
				}
				if (declarationModifiers == DeclarationModifiers.Protected)
				{
					return Accessibility.Protected;
				}
			}
			else if (declarationModifiers <= DeclarationModifiers.ProtectedInternal)
			{
				if (declarationModifiers == DeclarationModifiers.Internal)
				{
					return Accessibility.Internal;
				}
				if (declarationModifiers == DeclarationModifiers.ProtectedInternal)
				{
					return Accessibility.ProtectedOrInternal;
				}
			}
			else
			{
				if (declarationModifiers == DeclarationModifiers.Private)
				{
					return Accessibility.Private;
				}
				if (declarationModifiers == DeclarationModifiers.PrivateProtected)
				{
					return Accessibility.ProtectedAndInternal;
				}
			}
			return Accessibility.Public;
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x00196400 File Offset: 0x00194600
		internal static bool IsValidAccessibility(DeclarationModifiers modifiers)
		{
			DeclarationModifiers declarationModifiers = modifiers & DeclarationModifiers.AccessibilityMask;
			if (declarationModifiers <= DeclarationModifiers.Protected)
			{
				if (declarationModifiers != DeclarationModifiers.None && declarationModifiers != DeclarationModifiers.Public && declarationModifiers != DeclarationModifiers.Protected)
				{
					return false;
				}
			}
			else if (declarationModifiers <= DeclarationModifiers.ProtectedInternal)
			{
				if (declarationModifiers != DeclarationModifiers.Internal && declarationModifiers != DeclarationModifiers.ProtectedInternal)
				{
					return false;
				}
			}
			else if (declarationModifiers != DeclarationModifiers.Private && declarationModifiers != DeclarationModifiers.PrivateProtected)
			{
				return false;
			}
			return true;
		}
	}
}
