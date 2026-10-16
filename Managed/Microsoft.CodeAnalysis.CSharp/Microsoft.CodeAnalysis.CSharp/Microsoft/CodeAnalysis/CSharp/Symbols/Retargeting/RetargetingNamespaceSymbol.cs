using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D9 RID: 1497
	internal sealed class RetargetingNamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x06007D4A RID: 32074 RVA: 0x001C2D67 File Offset: 0x001C0F67
		public RetargetingNamespaceSymbol(RetargetingModuleSymbol retargetingModule, NamespaceSymbol underlyingNamespace)
		{
			this._retargetingModule = retargetingModule;
			this._underlyingNamespace = underlyingNamespace;
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x06007D4B RID: 32075 RVA: 0x001C2D7D File Offset: 0x001C0F7D
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x06007D4C RID: 32076 RVA: 0x001C2D8A File Offset: 0x001C0F8A
		public NamespaceSymbol UnderlyingNamespace
		{
			get
			{
				return this._underlyingNamespace;
			}
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06007D4D RID: 32077 RVA: 0x001C2D92 File Offset: 0x001C0F92
		internal override NamespaceExtent Extent
		{
			get
			{
				return new NamespaceExtent(this._retargetingModule);
			}
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x001C2D9F File Offset: 0x001C0F9F
		public override ImmutableArray<Symbol> GetMembers()
		{
			return this.RetargetMembers(this._underlyingNamespace.GetMembers());
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x001C2DB4 File Offset: 0x001C0FB4
		private ImmutableArray<Symbol> RetargetMembers(ImmutableArray<Symbol> underlyingMembers)
		{
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(underlyingMembers.Length);
			foreach (Symbol symbol in underlyingMembers)
			{
				if (symbol.Kind != SymbolKind.NamedType || !((NamedTypeSymbol)symbol).IsExplicitDefinitionOfNoPiaLocalType)
				{
					instance.Add(this.RetargetingTranslator.Retarget(symbol));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x001C2E18 File Offset: 0x001C1018
		internal override ImmutableArray<Symbol> GetMembersUnordered()
		{
			return this.RetargetMembers(this._underlyingNamespace.GetMembersUnordered());
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x001C2E2B File Offset: 0x001C102B
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return this.RetargetMembers(this._underlyingNamespace.GetMembers(name));
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x001C2E3F File Offset: 0x001C103F
		internal override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.RetargetTypeMembers(this._underlyingNamespace.GetTypeMembersUnordered());
		}

		// Token: 0x06007D53 RID: 32083 RVA: 0x001C2E52 File Offset: 0x001C1052
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return this.RetargetTypeMembers(this._underlyingNamespace.GetTypeMembers());
		}

		// Token: 0x06007D54 RID: 32084 RVA: 0x001C2E68 File Offset: 0x001C1068
		private ImmutableArray<NamedTypeSymbol> RetargetTypeMembers(ImmutableArray<NamedTypeSymbol> underlyingMembers)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance(underlyingMembers.Length);
			foreach (NamedTypeSymbol namedTypeSymbol in underlyingMembers)
			{
				if (!namedTypeSymbol.IsExplicitDefinitionOfNoPiaLocalType)
				{
					instance.Add(this.RetargetingTranslator.Retarget(namedTypeSymbol, RetargetOptions.RetargetPrimitiveTypesByName));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x001C2EBE File Offset: 0x001C10BE
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return this.RetargetTypeMembers(this._underlyingNamespace.GetTypeMembers(name));
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x001C2ED2 File Offset: 0x001C10D2
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.RetargetTypeMembers(this._underlyingNamespace.GetTypeMembers(name, arity));
		}

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06007D57 RID: 32087 RVA: 0x001C2EE7 File Offset: 0x001C10E7
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingNamespace.ContainingSymbol);
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06007D58 RID: 32088 RVA: 0x001C2EFF File Offset: 0x001C10FF
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._retargetingModule.Locations;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06007D59 RID: 32089 RVA: 0x001C2F0C File Offset: 0x001C110C
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._underlyingNamespace.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x06007D5A RID: 32090 RVA: 0x001C2F19 File Offset: 0x001C1119
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x06007D5B RID: 32091 RVA: 0x001C2F26 File Offset: 0x001C1126
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x06007D5C RID: 32092 RVA: 0x001C2F2E File Offset: 0x001C112E
		public override bool IsGlobalNamespace
		{
			get
			{
				return this._underlyingNamespace.IsGlobalNamespace;
			}
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x06007D5D RID: 32093 RVA: 0x001C2F3B File Offset: 0x001C113B
		public override string Name
		{
			get
			{
				return this._underlyingNamespace.Name;
			}
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x001C2F48 File Offset: 0x001C1148
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingNamespace.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x001C2F58 File Offset: 0x001C1158
		internal override NamedTypeSymbol LookupMetadataType(ref MetadataTypeName typeName)
		{
			NamedTypeSymbol namedTypeSymbol = this._underlyingNamespace.LookupMetadataType(ref typeName);
			if (!namedTypeSymbol.IsErrorType() && namedTypeSymbol.IsExplicitDefinitionOfNoPiaLocalType)
			{
				return new MissingMetadataTypeSymbol.TopLevel(this._retargetingModule, ref typeName, null);
			}
			return this.RetargetingTranslator.Retarget(namedTypeSymbol, RetargetOptions.RetargetPrimitiveTypesByName);
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x001C2FA0 File Offset: 0x001C11A0
		internal override void GetExtensionMethods(ArrayBuilder<MethodSymbol> methods, string nameOpt, int arity, LookupOptions options)
		{
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			this._underlyingNamespace.GetExtensionMethods(instance, nameOpt, arity, options);
			foreach (MethodSymbol methodSymbol in instance)
			{
				methods.Add(this.RetargetingTranslator.Retarget(methodSymbol));
			}
			instance.Free();
		}

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x06007D61 RID: 32097 RVA: 0x001C2FF5 File Offset: 0x001C11F5
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D83 RID: 7555
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D84 RID: 7556
		private readonly NamespaceSymbol _underlyingNamespace;
	}
}
