using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000590 RID: 1424
	internal abstract class SynthesizedFieldSymbolBase : FieldSymbol
	{
		// Token: 0x060076B4 RID: 30388 RVA: 0x001B7A80 File Offset: 0x001B5C80
		public SynthesizedFieldSymbolBase(NamedTypeSymbol containingType, string name, bool isPublic, bool isReadOnly, bool isStatic)
		{
			this._containingType = containingType;
			this._name = name;
			this._modifiers = (isPublic ? DeclarationModifiers.Public : DeclarationModifiers.Private) | (isReadOnly ? DeclarationModifiers.ReadOnly : DeclarationModifiers.None) | (isStatic ? DeclarationModifiers.Static : DeclarationModifiers.None);
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x060076B5 RID: 30389
		internal abstract bool SuppressDynamicAttribute { get; }

		// Token: 0x060076B6 RID: 30390 RVA: 0x001B7AC0 File Offset: 0x001B5CC0
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations typeWithAnnotations = base.TypeWithAnnotations;
			TypeSymbol type = typeWithAnnotations.Type;
			if (!this._containingType.IsImplicitlyDeclared)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			if (!this.SuppressDynamicAttribute && type.ContainsDynamic() && declaringCompilation.HasDynamicEmitAttributes(BindingDiagnosticBag.Discarded, Location.None) && declaringCompilation.CanEmitBoolean())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(type, typeWithAnnotations.CustomModifiers.Length, RefKind.None));
			}
			if (type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, type));
			}
			if (type.ContainsTupleNames() && declaringCompilation.HasTupleNamesAttributes(BindingDiagnosticBag.Discarded, Location.None) && declaringCompilation.CanEmitSpecialType(SpecialType.System_String))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(base.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.ContainingType.GetNullableContextValue(), typeWithAnnotations));
			}
		}

		// Token: 0x060076B7 RID: 30391
		internal abstract override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound);

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x060076B8 RID: 30392 RVA: 0x001B7BD0 File Offset: 0x001B5DD0
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x060076B9 RID: 30393 RVA: 0x001B7BD3 File Offset: 0x001B5DD3
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x001B7BDB File Offset: 0x001B5DDB
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x060076BB RID: 30395 RVA: 0x001B7BDE File Offset: 0x001B5DDE
		public override bool IsReadOnly
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x001B7BEF File Offset: 0x001B5DEF
		public override bool IsVolatile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x060076BD RID: 30397 RVA: 0x001B7BF2 File Offset: 0x001B5DF2
		public override bool IsConst
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x060076BE RID: 30398 RVA: 0x001B7BF5 File Offset: 0x001B5DF5
		internal override bool IsNotSerialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x060076BF RID: 30399 RVA: 0x001B7BF8 File Offset: 0x001B5DF8
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x060076C0 RID: 30400 RVA: 0x001B7BFC File Offset: 0x001B5DFC
		internal override int? TypeLayoutOffset
		{
			get
			{
				return default(int?);
			}
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x001B7C12 File Offset: 0x001B5E12
		internal override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
		{
			return null;
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x060076C2 RID: 30402 RVA: 0x001B7C15 File Offset: 0x001B5E15
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x060076C3 RID: 30403 RVA: 0x001B7C18 File Offset: 0x001B5E18
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x060076C4 RID: 30404 RVA: 0x001B7C20 File Offset: 0x001B5E20
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x060076C5 RID: 30405 RVA: 0x001B7C28 File Offset: 0x001B5E28
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x060076C6 RID: 30406 RVA: 0x001B7C2F File Offset: 0x001B5E2F
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x060076C7 RID: 30407 RVA: 0x001B7C36 File Offset: 0x001B5E36
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this._modifiers);
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x060076C8 RID: 30408 RVA: 0x001B7C43 File Offset: 0x001B5E43
		public override bool IsStatic
		{
			get
			{
				return (this._modifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x060076C9 RID: 30409 RVA: 0x001B7C50 File Offset: 0x001B5E50
		internal override bool HasSpecialName
		{
			get
			{
				return this.HasRuntimeSpecialName;
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x060076CA RID: 30410 RVA: 0x001B7C58 File Offset: 0x001B5E58
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this.Name == "value__";
			}
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x060076CB RID: 30411 RVA: 0x001B7C6A File Offset: 0x001B5E6A
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001CCA RID: 7370
		private readonly NamedTypeSymbol _containingType;

		// Token: 0x04001CCB RID: 7371
		private readonly string _name;

		// Token: 0x04001CCC RID: 7372
		private readonly DeclarationModifiers _modifiers;
	}
}
