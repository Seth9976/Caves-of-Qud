using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Emit.NoPia;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x020004A0 RID: 1184
	internal sealed class EmbeddedType : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedType
	{
		// Token: 0x06006209 RID: 25097 RVA: 0x001789CE File Offset: 0x00176BCE
		public EmbeddedType(EmbeddedTypesManager typeManager, NamedTypeSymbol underlyingNamedType)
			: base(typeManager, underlyingNamedType)
		{
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x001789D8 File Offset: 0x00176BD8
		public void EmbedAllMembersOfImplementedInterface(SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			if (this._embeddedAllMembersOfImplementedInterface)
			{
				return;
			}
			this._embeddedAllMembersOfImplementedInterface = true;
			foreach (MethodSymbol methodSymbol in this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetMethodsToEmit())
			{
				if (methodSymbol != null)
				{
					this.TypeManager.EmbedMethod(this, methodSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics);
				}
			}
			foreach (NamedTypeSymbol namedTypeSymbol in this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetInterfacesToEmit())
			{
				this.TypeManager.ModuleBeingBuilt.Translate(namedTypeSymbol, syntaxNodeOpt, diagnostics, true, false);
			}
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x00178A94 File Offset: 0x00176C94
		protected override int GetAssemblyRefIndex()
		{
			return this.TypeManager.ModuleBeingBuilt.SourceModule.GetReferencedAssemblySymbols().IndexOf(this.UnderlyingNamedType.AdaptedNamedTypeSymbol.ContainingAssembly, ReferenceEqualityComparer.Instance);
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x00178AC5 File Offset: 0x00176CC5
		protected override bool IsPublic
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.DeclaredAccessibility == Accessibility.Public;
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x00178ADC File Offset: 0x00176CDC
		protected override ITypeReference GetBaseClass(PEModuleBuilder moduleBuilder, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.UnderlyingNamedType.AdaptedNamedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics == null)
			{
				return null;
			}
			return moduleBuilder.Translate(baseTypeNoUseSiteDiagnostics, syntaxNodeOpt, diagnostics, false, false);
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00178B0A File Offset: 0x00176D0A
		protected override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetFieldsToEmit();
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x00178B1C File Offset: 0x00176D1C
		protected override IEnumerable<MethodSymbol> GetMethodsToEmit()
		{
			return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetMethodsToEmit();
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x00178B2E File Offset: 0x00176D2E
		protected override IEnumerable<EventSymbol> GetEventsToEmit()
		{
			return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetEventsToEmit();
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x00178B40 File Offset: 0x00176D40
		protected override IEnumerable<PropertySymbol> GetPropertiesToEmit()
		{
			return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetPropertiesToEmit();
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x00178B52 File Offset: 0x00176D52
		protected override IEnumerable<TypeReferenceWithAttributes> GetInterfaces(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			foreach (NamedTypeSymbol namedTypeSymbol in this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetInterfacesToEmit())
			{
				INamedTypeReference namedTypeReference = moduleBeingBuilt.Translate(namedTypeSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
				TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				yield return typeWithAnnotations.GetTypeRefWithAttributes(moduleBeingBuilt, this.UnderlyingNamedType.AdaptedNamedTypeSymbol, namedTypeReference);
			}
			ImmutableArray<NamedTypeSymbol>.Enumerator enumerator = default(ImmutableArray<NamedTypeSymbol>.Enumerator);
			yield break;
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06006213 RID: 25107 RVA: 0x00178B69 File Offset: 0x00176D69
		protected override bool IsAbstract
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsMetadataAbstract;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x00178B7C File Offset: 0x00176D7C
		protected override bool IsBeforeFieldInit
		{
			get
			{
				switch (this.UnderlyingNamedType.AdaptedNamedTypeSymbol.TypeKind)
				{
				case TypeKind.Delegate:
				case TypeKind.Enum:
				case TypeKind.Interface:
					return false;
				}
				return true;
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06006215 RID: 25109 RVA: 0x00178BBB File Offset: 0x00176DBB
		protected override bool IsComImport
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsComImport;
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x00178BCD File Offset: 0x00176DCD
		protected override bool IsInterface
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsInterfaceType();
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x00178BDF File Offset: 0x00176DDF
		protected override bool IsDelegate
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsDelegateType();
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x00178BF1 File Offset: 0x00176DF1
		protected override bool IsSerializable
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsSerializable;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06006219 RID: 25113 RVA: 0x00178C03 File Offset: 0x00176E03
		protected override bool IsSpecialName
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.HasSpecialName;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x0600621A RID: 25114 RVA: 0x00178C15 File Offset: 0x00176E15
		protected override bool IsWindowsRuntimeImport
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsWindowsRuntimeImport;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x0600621B RID: 25115 RVA: 0x00178C27 File Offset: 0x00176E27
		protected override bool IsSealed
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsMetadataSealed;
			}
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x00178C3C File Offset: 0x00176E3C
		protected override TypeLayout? GetTypeLayoutIfStruct()
		{
			if (this.UnderlyingNamedType.AdaptedNamedTypeSymbol.IsStructType())
			{
				return new TypeLayout?(this.UnderlyingNamedType.AdaptedNamedTypeSymbol.Layout);
			}
			return default(TypeLayout?);
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x0600621D RID: 25117 RVA: 0x00178C7A File Offset: 0x00176E7A
		protected override CharSet StringFormat
		{
			get
			{
				return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.MarshallingCharSet;
			}
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x00178C8C File Offset: 0x00176E8C
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x00178CA0 File Offset: 0x00176EA0
		protected override CSharpAttributeData CreateTypeIdentifierAttribute(bool hasGuid, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			WellKnownMember wellKnownMember = (hasGuid ? WellKnownMember.System_Runtime_InteropServices_TypeIdentifierAttribute__ctor : WellKnownMember.System_Runtime_InteropServices_TypeIdentifierAttribute__ctorStringString);
			MethodSymbol wellKnownMethod = this.TypeManager.GetWellKnownMethod(wellKnownMember, syntaxNodeOpt, diagnostics);
			if (wellKnownMethod == null)
			{
				return null;
			}
			if (hasGuid)
			{
				return new SynthesizedAttributeData(wellKnownMethod, ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			NamedTypeSymbol systemStringType = this.TypeManager.GetSystemStringType(syntaxNodeOpt, diagnostics);
			if (systemStringType != null)
			{
				string assemblyGuidString = this.TypeManager.GetAssemblyGuidString(this.UnderlyingNamedType.AdaptedNamedTypeSymbol.ContainingAssembly);
				return new SynthesizedAttributeData(wellKnownMethod, ImmutableArray.Create<TypedConstant>(new TypedConstant(systemStringType, TypedConstantKind.Primitive, assemblyGuidString), new TypedConstant(systemStringType, TypedConstantKind.Primitive, this.UnderlyingNamedType.AdaptedNamedTypeSymbol.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat))), ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return null;
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x00178D42 File Offset: 0x00176F42
		protected override void ReportMissingAttribute(AttributeDescription description, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			EmbeddedTypesManager.Error(diagnostics, ErrorCode.ERR_InteropTypeMissingAttribute, syntaxNodeOpt, new object[]
			{
				this.UnderlyingNamedType.AdaptedNamedTypeSymbol,
				description.FullName
			});
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x00178D70 File Offset: 0x00176F70
		protected override void EmbedDefaultMembers(string defaultMember, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			foreach (Symbol symbol in this.UnderlyingNamedType.AdaptedNamedTypeSymbol.GetMembers(defaultMember))
			{
				SymbolKind kind = symbol.Kind;
				switch (kind)
				{
				case SymbolKind.Event:
					this.TypeManager.EmbedEvent(this, ((EventSymbol)symbol).GetCciAdapter(), syntaxNodeOpt, diagnostics, false);
					break;
				case SymbolKind.Field:
					this.TypeManager.EmbedField(this, ((FieldSymbol)symbol).GetCciAdapter(), syntaxNodeOpt, diagnostics);
					break;
				case SymbolKind.Label:
				case SymbolKind.Local:
					break;
				case SymbolKind.Method:
					this.TypeManager.EmbedMethod(this, ((MethodSymbol)symbol).GetCciAdapter(), syntaxNodeOpt, diagnostics);
					break;
				default:
					if (kind == SymbolKind.Property)
					{
						this.TypeManager.EmbedProperty(this, ((PropertySymbol)symbol).GetCciAdapter(), syntaxNodeOpt, diagnostics);
					}
					break;
				}
			}
		}

		// Token: 0x04001977 RID: 6519
		private bool _embeddedAllMembersOfImplementedInterface;
	}
}
