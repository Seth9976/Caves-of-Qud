using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B1 RID: 1457
	internal abstract class WrappedFieldSymbol : FieldSymbol
	{
		// Token: 0x06007A47 RID: 31303 RVA: 0x001BF471 File Offset: 0x001BD671
		public WrappedFieldSymbol(FieldSymbol underlyingField)
		{
			this._underlyingField = underlyingField;
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x001BF480 File Offset: 0x001BD680
		public FieldSymbol UnderlyingField
		{
			get
			{
				return this._underlyingField;
			}
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x06007A49 RID: 31305 RVA: 0x001BF488 File Offset: 0x001BD688
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingField.IsImplicitlyDeclared;
			}
		}

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x06007A4A RID: 31306 RVA: 0x001BF495 File Offset: 0x001BD695
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return this._underlyingField.FlowAnalysisAnnotations;
			}
		}

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x06007A4B RID: 31307 RVA: 0x001BF4A2 File Offset: 0x001BD6A2
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._underlyingField.DeclaredAccessibility;
			}
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x06007A4C RID: 31308 RVA: 0x001BF4AF File Offset: 0x001BD6AF
		public override string Name
		{
			get
			{
				return this._underlyingField.Name;
			}
		}

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x06007A4D RID: 31309 RVA: 0x001BF4BC File Offset: 0x001BD6BC
		internal override bool HasSpecialName
		{
			get
			{
				return this._underlyingField.HasSpecialName;
			}
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x001BF4C9 File Offset: 0x001BD6C9
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this._underlyingField.HasRuntimeSpecialName;
			}
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x001BF4D6 File Offset: 0x001BD6D6
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingField.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x06007A50 RID: 31312 RVA: 0x001BF4E6 File Offset: 0x001BD6E6
		internal override bool IsNotSerialized
		{
			get
			{
				return this._underlyingField.IsNotSerialized;
			}
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x06007A51 RID: 31313 RVA: 0x001BF4F3 File Offset: 0x001BD6F3
		internal override bool HasPointerType
		{
			get
			{
				return this._underlyingField.HasPointerType;
			}
		}

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x001BF500 File Offset: 0x001BD700
		internal override bool IsMarshalledExplicitly
		{
			get
			{
				return this._underlyingField.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x06007A53 RID: 31315 RVA: 0x001BF50D File Offset: 0x001BD70D
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._underlyingField.MarshallingInformation;
			}
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x06007A54 RID: 31316 RVA: 0x001BF51A File Offset: 0x001BD71A
		internal override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return this._underlyingField.MarshallingDescriptor;
			}
		}

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x06007A55 RID: 31317 RVA: 0x001BF527 File Offset: 0x001BD727
		public override bool IsFixedSizeBuffer
		{
			get
			{
				return this._underlyingField.IsFixedSizeBuffer;
			}
		}

		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x001BF534 File Offset: 0x001BD734
		internal override int? TypeLayoutOffset
		{
			get
			{
				return this._underlyingField.TypeLayoutOffset;
			}
		}

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x06007A57 RID: 31319 RVA: 0x001BF541 File Offset: 0x001BD741
		public override bool IsReadOnly
		{
			get
			{
				return this._underlyingField.IsReadOnly;
			}
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x001BF54E File Offset: 0x001BD74E
		public override bool IsVolatile
		{
			get
			{
				return this._underlyingField.IsVolatile;
			}
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x06007A59 RID: 31321 RVA: 0x001BF55B File Offset: 0x001BD75B
		public override bool IsConst
		{
			get
			{
				return this._underlyingField.IsConst;
			}
		}

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x06007A5A RID: 31322 RVA: 0x001BF568 File Offset: 0x001BD768
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return this._underlyingField.ObsoleteAttributeData;
			}
		}

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x06007A5B RID: 31323 RVA: 0x001BF575 File Offset: 0x001BD775
		public override object ConstantValue
		{
			get
			{
				return this._underlyingField.ConstantValue;
			}
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x001BF582 File Offset: 0x001BD782
		internal override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
		{
			return this._underlyingField.GetConstantValue(inProgress, earlyDecodingWellKnownAttributes);
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x06007A5D RID: 31325 RVA: 0x001BF591 File Offset: 0x001BD791
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingField.Locations;
			}
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x06007A5E RID: 31326 RVA: 0x001BF59E File Offset: 0x001BD79E
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._underlyingField.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x06007A5F RID: 31327 RVA: 0x001BF5AB File Offset: 0x001BD7AB
		public override bool IsStatic
		{
			get
			{
				return this._underlyingField.IsStatic;
			}
		}

		// Token: 0x04001D2E RID: 7470
		protected readonly FieldSymbol _underlyingField;
	}
}
