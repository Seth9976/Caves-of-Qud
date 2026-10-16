using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit.NoPia;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x0200049B RID: 1179
	internal sealed class EmbeddedEvent : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedEvent
	{
		// Token: 0x060061BF RID: 25023 RVA: 0x001782F6 File Offset: 0x001764F6
		public EmbeddedEvent(EventSymbol underlyingEvent, EmbeddedMethod adder, EmbeddedMethod remover)
			: base(underlyingEvent, adder, remover, null)
		{
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x00178302 File Offset: 0x00176502
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return base.UnderlyingEvent.AdaptedEventSymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x060061C1 RID: 25025 RVA: 0x00178315 File Offset: 0x00176515
		protected override bool IsRuntimeSpecial
		{
			get
			{
				return base.UnderlyingEvent.AdaptedEventSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x00178327 File Offset: 0x00176527
		protected override bool IsSpecialName
		{
			get
			{
				return base.UnderlyingEvent.AdaptedEventSymbol.HasSpecialName;
			}
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x00178339 File Offset: 0x00176539
		protected override ITypeReference GetType(PEModuleBuilder moduleBuilder, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return moduleBuilder.Translate(base.UnderlyingEvent.AdaptedEventSymbol.Type, syntaxNodeOpt, diagnostics);
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x00178353 File Offset: 0x00176553
		protected override EmbeddedType ContainingType
		{
			get
			{
				return base.AnAccessor.ContainingType;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x060061C5 RID: 25029 RVA: 0x00178360 File Offset: 0x00176560
		protected override TypeMemberVisibility Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(base.UnderlyingEvent.AdaptedEventSymbol);
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x060061C6 RID: 25030 RVA: 0x00178372 File Offset: 0x00176572
		protected override string Name
		{
			get
			{
				return base.UnderlyingEvent.AdaptedEventSymbol.MetadataName;
			}
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00178384 File Offset: 0x00176584
		protected override void EmbedCorrespondingComEventInterfaceMethodInternal(SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool isUsedForComAwareEventBinding)
		{
			NamedTypeSymbol adaptedNamedTypeSymbol = this.ContainingType.UnderlyingNamedType.AdaptedNamedTypeSymbol;
			foreach (CSharpAttributeData csharpAttributeData in adaptedNamedTypeSymbol.GetAttributes())
			{
				if (csharpAttributeData.IsTargetAttribute(adaptedNamedTypeSymbol, AttributeDescription.ComEventInterfaceAttribute))
				{
					bool flag = false;
					NamedTypeSymbol namedTypeSymbol = null;
					if (csharpAttributeData.CommonConstructorArguments.Length == 2)
					{
						namedTypeSymbol = csharpAttributeData.CommonConstructorArguments[0].ValueInternal as NamedTypeSymbol;
						if (namedTypeSymbol != null)
						{
							flag = this.EmbedMatchingInterfaceMethods(namedTypeSymbol, syntaxNodeOpt, diagnostics);
							foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
							{
								if (this.EmbedMatchingInterfaceMethods(namedTypeSymbol2, syntaxNodeOpt, diagnostics))
								{
									flag = true;
								}
							}
						}
					}
					if (flag || !isUsedForComAwareEventBinding)
					{
						break;
					}
					if (namedTypeSymbol == null)
					{
						EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_MissingSourceInterface, syntaxNodeOpt, new object[]
						{
							adaptedNamedTypeSymbol,
							base.UnderlyingEvent.AdaptedEventSymbol
						});
						return;
					}
					CompoundUseSiteInfo<AssemblySymbol> discardedDependencies = CompoundUseSiteInfo<AssemblySymbol>.DiscardedDependencies;
					namedTypeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref discardedDependencies);
					diagnostics.Add((syntaxNodeOpt == null) ? NoLocation.Singleton : syntaxNodeOpt.Location, discardedDependencies.Diagnostics);
					EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_MissingMethodOnSourceInterface, syntaxNodeOpt, new object[]
					{
						namedTypeSymbol,
						base.UnderlyingEvent.AdaptedEventSymbol.MetadataName,
						base.UnderlyingEvent.AdaptedEventSymbol
					});
					return;
				}
			}
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x001784F8 File Offset: 0x001766F8
		private bool EmbedMatchingInterfaceMethods(NamedTypeSymbol sourceInterface, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			bool flag = false;
			foreach (Symbol symbol in sourceInterface.GetMembers(base.UnderlyingEvent.AdaptedEventSymbol.MetadataName))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					this.TypeManager.EmbedMethodIfNeedTo(((MethodSymbol)symbol).GetCciAdapter(), syntaxNodeOpt, diagnostics);
					flag = true;
				}
			}
			return flag;
		}
	}
}
