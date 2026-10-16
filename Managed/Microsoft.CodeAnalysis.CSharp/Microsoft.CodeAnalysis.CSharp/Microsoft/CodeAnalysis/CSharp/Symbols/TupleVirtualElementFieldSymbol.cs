using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A3 RID: 1443
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TupleVirtualElementFieldSymbol : TupleElementFieldSymbol
	{
		// Token: 0x060078AC RID: 30892 RVA: 0x001B97B2 File Offset: 0x001B79B2
		public TupleVirtualElementFieldSymbol(NamedTypeSymbol container, FieldSymbol underlyingField, string name, int tupleElementIndex, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations, bool cannotUse, bool isImplicitlyDeclared, [global::System.Runtime.CompilerServices.Nullable(2)] FieldSymbol correspondingDefaultFieldOpt)
			: base(container, underlyingField, tupleElementIndex, locations, isImplicitlyDeclared, correspondingDefaultFieldOpt)
		{
			this._name = name;
			this._cannotUse = cannotUse;
		}

		// Token: 0x060078AD RID: 30893 RVA: 0x001B97D4 File Offset: 0x001B79D4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (this._cannotUse)
			{
				return new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_TupleInferredNamesNotAvailable, new object[]
				{
					this._name,
					new CSharpRequiredLanguageVersion(MessageID.IDS_FeatureInferredTupleNames.RequiredVersion())
				}));
			}
			return base.GetUseSiteInfo();
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x060078AE RID: 30894 RVA: 0x001B9820 File Offset: 0x001B7A20
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x060078AF RID: 30895 RVA: 0x001B9828 File Offset: 0x001B7A28
		internal override int? TypeLayoutOffset
		{
			get
			{
				return default(int?);
			}
		}

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x060078B0 RID: 30896 RVA: 0x001B983E File Offset: 0x001B7A3E
		public override FieldSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x060078B1 RID: 30897 RVA: 0x001B9841 File Offset: 0x001B7A41
		public override bool IsVirtualTupleField
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x001B9844 File Offset: 0x001B7A44
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._underlyingField.GetFieldType(fieldsBeingBound);
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x001B9852 File Offset: 0x001B7A52
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._underlyingField.GetAttributes();
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x001B9860 File Offset: 0x001B7A60
		internal override FieldSymbol AsMember(NamedTypeSymbol newOwner)
		{
			NamedTypeSymbol newUnderlyingOwner = base.GetNewUnderlyingOwner(newOwner);
			FieldSymbol fieldSymbol = null;
			if (this._correspondingDefaultField != this)
			{
				fieldSymbol = this._correspondingDefaultField.OriginalDefinition.AsMember(newOwner);
			}
			return new TupleVirtualElementFieldSymbol(newOwner, this._underlyingField.OriginalDefinition.AsMember(newUnderlyingOwner), this._name, this.TupleElementIndex, this.Locations, this._cannotUse, this.IsImplicitlyDeclared, fieldSymbol);
		}

		// Token: 0x04001D0B RID: 7435
		private readonly string _name;

		// Token: 0x04001D0C RID: 7436
		private readonly bool _cannotUse;
	}
}
