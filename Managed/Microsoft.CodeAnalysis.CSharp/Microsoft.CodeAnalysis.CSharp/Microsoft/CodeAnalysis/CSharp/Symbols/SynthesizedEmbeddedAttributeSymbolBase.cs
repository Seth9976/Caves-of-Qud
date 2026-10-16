using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000584 RID: 1412
	internal abstract class SynthesizedEmbeddedAttributeSymbolBase : NamedTypeSymbol
	{
		// Token: 0x06007605 RID: 30213 RVA: 0x001B6F4C File Offset: 0x001B514C
		public SynthesizedEmbeddedAttributeSymbolBase(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol baseType)
			: base(null)
		{
			this._name = name;
			this._namespace = containingNamespace;
			this._module = containingModule;
			this._baseType = baseType;
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x06007606 RID: 30214
		public new abstract ImmutableArray<MethodSymbol> Constructors { get; }

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06007607 RID: 30215 RVA: 0x001B6F72 File Offset: 0x001B5172
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06007608 RID: 30216 RVA: 0x001B6F75 File Offset: 0x001B5175
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06007609 RID: 30217 RVA: 0x001B6F7C File Offset: 0x001B517C
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x001B6F7F File Offset: 0x001B517F
		internal override ManagedKind GetManagedKind(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return ManagedKind.Managed;
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x0600760B RID: 30219 RVA: 0x001B6F82 File Offset: 0x001B5182
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x0600760C RID: 30220 RVA: 0x001B6F85 File Offset: 0x001B5185
		public override bool MightContainExtensionMethods
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x0600760D RID: 30221 RVA: 0x001B6F88 File Offset: 0x001B5188
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x0600760E RID: 30222 RVA: 0x001B6F90 File Offset: 0x001B5190
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return this.Constructors.Select<MethodSymbol, string>((MethodSymbol m) => m.Name);
			}
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x0600760F RID: 30223 RVA: 0x001B6FBC File Offset: 0x001B51BC
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Internal;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x06007610 RID: 30224 RVA: 0x001B6FBF File Offset: 0x001B51BF
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Class;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x06007611 RID: 30225 RVA: 0x001B6FC2 File Offset: 0x001B51C2
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x06007612 RID: 30226 RVA: 0x001B6FCA File Offset: 0x001B51CA
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06007613 RID: 30227 RVA: 0x001B6FD2 File Offset: 0x001B51D2
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._module.ContainingAssembly;
			}
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x06007614 RID: 30228 RVA: 0x001B6FDF File Offset: 0x001B51DF
		public override NamespaceSymbol ContainingNamespace
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x06007615 RID: 30229 RVA: 0x001B6FE7 File Offset: 0x001B51E7
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x06007616 RID: 30230 RVA: 0x001B6FEE File Offset: 0x001B51EE
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x06007617 RID: 30231 RVA: 0x001B6FF5 File Offset: 0x001B51F5
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x06007618 RID: 30232 RVA: 0x001B6FF8 File Offset: 0x001B51F8
		public override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x06007619 RID: 30233 RVA: 0x001B6FFB File Offset: 0x001B51FB
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x0600761A RID: 30234 RVA: 0x001B6FFE File Offset: 0x001B51FE
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x0600761B RID: 30235 RVA: 0x001B7001 File Offset: 0x001B5201
		public override bool IsSealed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x0600761C RID: 30236 RVA: 0x001B7004 File Offset: 0x001B5204
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x0600761D RID: 30237 RVA: 0x001B700B File Offset: 0x001B520B
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x0600761E RID: 30238 RVA: 0x001B700E File Offset: 0x001B520E
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x0600761F RID: 30239 RVA: 0x001B7011 File Offset: 0x001B5211
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x06007620 RID: 30240 RVA: 0x001B7014 File Offset: 0x001B5214
		internal override bool IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x06007621 RID: 30241 RVA: 0x001B7017 File Offset: 0x001B5217
		internal override bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x06007622 RID: 30242 RVA: 0x001B701A File Offset: 0x001B521A
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x06007623 RID: 30243 RVA: 0x001B701D File Offset: 0x001B521D
		public override bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x06007624 RID: 30244 RVA: 0x001B7020 File Offset: 0x001B5220
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingModule.AreLocalsZeroed;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x06007625 RID: 30245 RVA: 0x001B7030 File Offset: 0x001B5230
		internal override TypeLayout Layout
		{
			get
			{
				return default(TypeLayout);
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x06007626 RID: 30246 RVA: 0x001B7046 File Offset: 0x001B5246
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return base.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x06007627 RID: 30247 RVA: 0x001B704E File Offset: 0x001B524E
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x06007628 RID: 30248 RVA: 0x001B7051 File Offset: 0x001B5251
		internal override bool IsInterface
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x06007629 RID: 30249 RVA: 0x001B7054 File Offset: 0x001B5254
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x0600762A RID: 30250 RVA: 0x001B705C File Offset: 0x001B525C
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x001B705F File Offset: 0x001B525F
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600762C RID: 30252 RVA: 0x001B7068 File Offset: 0x001B5268
		public override ImmutableArray<Symbol> GetMembers()
		{
			return this.Constructors.CastArray<Symbol>();
		}

		// Token: 0x0600762D RID: 30253 RVA: 0x001B7084 File Offset: 0x001B5284
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			if (!(name == ".ctor"))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return this.Constructors.CastArray<Symbol>();
		}

		// Token: 0x0600762E RID: 30254 RVA: 0x001B70B2 File Offset: 0x001B52B2
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600762F RID: 30255 RVA: 0x001B70B9 File Offset: 0x001B52B9
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007630 RID: 30256 RVA: 0x001B70C0 File Offset: 0x001B52C0
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x001B70C7 File Offset: 0x001B52C7
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x001B70CE File Offset: 0x001B52CE
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return AttributeUsageInfo.Default;
		}

		// Token: 0x06007633 RID: 30259 RVA: 0x001B70D5 File Offset: 0x001B52D5
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this._baseType;
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x001B70DD File Offset: 0x001B52DD
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x001B70E4 File Offset: 0x001B52E4
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.GetMembers();
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x001B70EC File Offset: 0x001B52EC
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			return this.GetMembers(name);
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x001B70F5 File Offset: 0x001B52F5
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return SpecializedCollections.EmptyEnumerable<FieldSymbol>();
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x001B70FC File Offset: 0x001B52FC
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x001B7103 File Offset: 0x001B5303
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			return null;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x001B7106 File Offset: 0x001B5306
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved = null)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600763B RID: 30267 RVA: 0x001B7110 File Offset: 0x001B5310
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeEmbeddedAttribute());
			AttributeUsageInfo attributeUsageInfo = this.GetAttributeUsageInfo();
			if (attributeUsageInfo != AttributeUsageInfo.Default)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.Compilation.SynthesizeAttributeUsageAttribute(attributeUsageInfo.ValidTargets, attributeUsageInfo.AllowMultiple, attributeUsageInfo.Inherited));
			}
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x001B7191 File Offset: 0x001B5391
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x0600763D RID: 30269 RVA: 0x001B7198 File Offset: 0x001B5398
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001CB1 RID: 7345
		private readonly string _name;

		// Token: 0x04001CB2 RID: 7346
		private readonly NamedTypeSymbol _baseType;

		// Token: 0x04001CB3 RID: 7347
		private readonly NamespaceSymbol _namespace;

		// Token: 0x04001CB4 RID: 7348
		private readonly ModuleSymbol _module;
	}
}
