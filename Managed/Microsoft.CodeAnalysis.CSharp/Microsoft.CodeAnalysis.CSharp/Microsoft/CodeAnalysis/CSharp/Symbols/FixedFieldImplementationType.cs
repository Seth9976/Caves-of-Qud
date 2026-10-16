using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053A RID: 1338
	internal sealed class FixedFieldImplementationType : SynthesizedContainer
	{
		// Token: 0x06007026 RID: 28710 RVA: 0x0019BED0 File Offset: 0x0019A0D0
		public FixedFieldImplementationType(SourceMemberFieldSymbol field)
			: base(GeneratedNames.MakeFixedFieldImplementationName(field.Name), ImmutableArray<TypeParameterSymbol>.Empty, TypeMap.Empty)
		{
			this._field = field;
			this._constructor = new SynthesizedInstanceConstructor(this);
			this._internalField = new SynthesizedFieldSymbol(this, ((PointerTypeSymbol)field.Type).PointedAtType, "FixedElementField", true, false, false);
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06007027 RID: 28711 RVA: 0x0019BF2F File Offset: 0x0019A12F
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._field.ContainingType;
			}
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x0019BF3C File Offset: 0x0019A13C
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Struct;
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x0019BF40 File Offset: 0x0019A140
		internal override MethodSymbol Constructor
		{
			get
			{
				return this._constructor;
			}
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x0600702A RID: 28714 RVA: 0x0019BF48 File Offset: 0x0019A148
		internal override TypeLayout Layout
		{
			get
			{
				int fixedSize = this._field.FixedSize;
				int num = ((PointerTypeSymbol)this._field.Type).PointedAtType.FixedBufferElementSizeInBytes();
				int num2 = fixedSize * num;
				return new TypeLayout(0, num2, 0);
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x0600702B RID: 28715 RVA: 0x0019BF86 File Offset: 0x0019A186
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return this._field.ContainingType.MarshallingCharSet;
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x0600702C RID: 28716 RVA: 0x0019BF98 File Offset: 0x0019A198
		internal override FieldSymbol FixedElementField
		{
			get
			{
				return this._internalField;
			}
		}

		// Token: 0x0600702D RID: 28717 RVA: 0x0019BFA0 File Offset: 0x0019A1A0
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.ContainingSymbol.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_UnsafeValueTypeAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x0600702E RID: 28718 RVA: 0x0019BFE2 File Offset: 0x0019A1E2
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return SpecializedCollections.SingletonEnumerable<string>("FixedElementField");
			}
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x0019BFEE File Offset: 0x0019A1EE
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray.Create<Symbol>(this._constructor, this._internalField);
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x0019C001 File Offset: 0x0019A201
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			if (name == this._constructor.Name)
			{
				return ImmutableArray.Create<Symbol>(this._constructor);
			}
			if (!(name == "FixedElementField"))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return ImmutableArray.Create<Symbol>(this._internalField);
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06007031 RID: 28721 RVA: 0x0019C040 File Offset: 0x0019A240
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Public;
			}
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06007032 RID: 28722 RVA: 0x0019C043 File Offset: 0x0019A243
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this.ContainingAssembly.GetSpecialType(SpecialType.System_ValueType);
			}
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06007033 RID: 28723 RVA: 0x0019C051 File Offset: 0x0019A251
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x0019C058 File Offset: 0x0019A258
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06007035 RID: 28725 RVA: 0x0019C05B File Offset: 0x0019A25B
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x0019C05E File Offset: 0x0019A25E
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x04001B7B RID: 7035
		internal const string FixedElementFieldName = "FixedElementField";

		// Token: 0x04001B7C RID: 7036
		private readonly SourceMemberFieldSymbol _field;

		// Token: 0x04001B7D RID: 7037
		private readonly MethodSymbol _constructor;

		// Token: 0x04001B7E RID: 7038
		private readonly FieldSymbol _internalField;
	}
}
