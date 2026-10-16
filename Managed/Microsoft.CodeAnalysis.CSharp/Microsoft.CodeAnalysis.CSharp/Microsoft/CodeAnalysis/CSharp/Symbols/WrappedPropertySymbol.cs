using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B5 RID: 1461
	internal abstract class WrappedPropertySymbol : PropertySymbol
	{
		// Token: 0x06007AD4 RID: 31444 RVA: 0x001BFBC0 File Offset: 0x001BDDC0
		public WrappedPropertySymbol(PropertySymbol underlyingProperty)
		{
			this._underlyingProperty = underlyingProperty;
		}

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x06007AD5 RID: 31445 RVA: 0x001BFBCF File Offset: 0x001BDDCF
		public PropertySymbol UnderlyingProperty
		{
			get
			{
				return this._underlyingProperty;
			}
		}

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x06007AD6 RID: 31446 RVA: 0x001BFBD7 File Offset: 0x001BDDD7
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingProperty.IsImplicitlyDeclared;
			}
		}

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x06007AD7 RID: 31447 RVA: 0x001BFBE4 File Offset: 0x001BDDE4
		public override RefKind RefKind
		{
			get
			{
				return this._underlyingProperty.RefKind;
			}
		}

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x06007AD8 RID: 31448 RVA: 0x001BFBF1 File Offset: 0x001BDDF1
		public override bool IsIndexer
		{
			get
			{
				return this._underlyingProperty.IsIndexer;
			}
		}

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x06007AD9 RID: 31449 RVA: 0x001BFBFE File Offset: 0x001BDDFE
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this._underlyingProperty.CallingConvention;
			}
		}

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x001BFC0B File Offset: 0x001BDE0B
		public override string Name
		{
			get
			{
				return this._underlyingProperty.Name;
			}
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x06007ADB RID: 31451 RVA: 0x001BFC18 File Offset: 0x001BDE18
		internal override bool HasSpecialName
		{
			get
			{
				return this._underlyingProperty.HasSpecialName;
			}
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x001BFC25 File Offset: 0x001BDE25
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingProperty.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x06007ADD RID: 31453 RVA: 0x001BFC35 File Offset: 0x001BDE35
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingProperty.Locations;
			}
		}

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x001BFC42 File Offset: 0x001BDE42
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._underlyingProperty.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x06007ADF RID: 31455 RVA: 0x001BFC4F File Offset: 0x001BDE4F
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._underlyingProperty.DeclaredAccessibility;
			}
		}

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x001BFC5C File Offset: 0x001BDE5C
		public override bool IsStatic
		{
			get
			{
				return this._underlyingProperty.IsStatic;
			}
		}

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x06007AE1 RID: 31457 RVA: 0x001BFC69 File Offset: 0x001BDE69
		public override bool IsVirtual
		{
			get
			{
				return this._underlyingProperty.IsVirtual;
			}
		}

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x06007AE2 RID: 31458 RVA: 0x001BFC76 File Offset: 0x001BDE76
		public override bool IsOverride
		{
			get
			{
				return this._underlyingProperty.IsOverride;
			}
		}

		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x06007AE3 RID: 31459 RVA: 0x001BFC83 File Offset: 0x001BDE83
		public override bool IsAbstract
		{
			get
			{
				return this._underlyingProperty.IsAbstract;
			}
		}

		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x06007AE4 RID: 31460 RVA: 0x001BFC90 File Offset: 0x001BDE90
		public override bool IsSealed
		{
			get
			{
				return this._underlyingProperty.IsSealed;
			}
		}

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x06007AE5 RID: 31461 RVA: 0x001BFC9D File Offset: 0x001BDE9D
		public override bool IsExtern
		{
			get
			{
				return this._underlyingProperty.IsExtern;
			}
		}

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x001BFCAA File Offset: 0x001BDEAA
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return this._underlyingProperty.ObsoleteAttributeData;
			}
		}

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x06007AE7 RID: 31463 RVA: 0x001BFCB7 File Offset: 0x001BDEB7
		public override string MetadataName
		{
			get
			{
				return this._underlyingProperty.MetadataName;
			}
		}

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x06007AE8 RID: 31464 RVA: 0x001BFCC4 File Offset: 0x001BDEC4
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this._underlyingProperty.HasRuntimeSpecialName;
			}
		}

		// Token: 0x04001D31 RID: 7473
		protected readonly PropertySymbol _underlyingProperty;
	}
}
