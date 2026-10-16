using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B3 RID: 1459
	internal abstract class WrappedNamedTypeSymbol : NamedTypeSymbol
	{
		// Token: 0x06007A95 RID: 31381 RVA: 0x001BF85C File Offset: 0x001BDA5C
		public WrappedNamedTypeSymbol(NamedTypeSymbol underlyingType, NamedTypeSymbol.TupleExtraData tupleData)
			: base(tupleData)
		{
			this._underlyingType = underlyingType;
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x06007A96 RID: 31382 RVA: 0x001BF86C File Offset: 0x001BDA6C
		public NamedTypeSymbol UnderlyingNamedType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x06007A97 RID: 31383 RVA: 0x001BF874 File Offset: 0x001BDA74
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingType.IsImplicitlyDeclared;
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x06007A98 RID: 31384 RVA: 0x001BF881 File Offset: 0x001BDA81
		public override int Arity
		{
			get
			{
				return this._underlyingType.Arity;
			}
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x06007A99 RID: 31385 RVA: 0x001BF88E File Offset: 0x001BDA8E
		public override bool MightContainExtensionMethods
		{
			get
			{
				return this._underlyingType.MightContainExtensionMethods;
			}
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x06007A9A RID: 31386 RVA: 0x001BF89B File Offset: 0x001BDA9B
		public override string Name
		{
			get
			{
				return this._underlyingType.Name;
			}
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06007A9B RID: 31387 RVA: 0x001BF8A8 File Offset: 0x001BDAA8
		public override string MetadataName
		{
			get
			{
				return this._underlyingType.MetadataName;
			}
		}

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x06007A9C RID: 31388 RVA: 0x001BF8B5 File Offset: 0x001BDAB5
		internal override bool HasSpecialName
		{
			get
			{
				return this._underlyingType.HasSpecialName;
			}
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06007A9D RID: 31389 RVA: 0x001BF8C2 File Offset: 0x001BDAC2
		internal override bool MangleName
		{
			get
			{
				return this._underlyingType.MangleName;
			}
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x001BF8CF File Offset: 0x001BDACF
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingType.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06007A9F RID: 31391 RVA: 0x001BF8DF File Offset: 0x001BDADF
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._underlyingType.DeclaredAccessibility;
			}
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06007AA0 RID: 31392 RVA: 0x001BF8EC File Offset: 0x001BDAEC
		public override TypeKind TypeKind
		{
			get
			{
				return this._underlyingType.TypeKind;
			}
		}

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06007AA1 RID: 31393 RVA: 0x001BF8F9 File Offset: 0x001BDAF9
		internal override bool IsInterface
		{
			get
			{
				return this._underlyingType.IsInterface;
			}
		}

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x06007AA2 RID: 31394 RVA: 0x001BF906 File Offset: 0x001BDB06
		public override ImmutableArray<Location> Locations
		{
			get
			{
				if (this.IsTupleType)
				{
					return base.TupleData.Locations;
				}
				return this._underlyingType.Locations;
			}
		}

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x06007AA3 RID: 31395 RVA: 0x001BF927 File Offset: 0x001BDB27
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				if (this.IsTupleType)
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<CSharpSyntaxNode>(base.TupleData.Locations);
				}
				return this._underlyingType.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x06007AA4 RID: 31396 RVA: 0x001BF94D File Offset: 0x001BDB4D
		public override bool IsStatic
		{
			get
			{
				return this._underlyingType.IsStatic;
			}
		}

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x06007AA5 RID: 31397 RVA: 0x001BF95A File Offset: 0x001BDB5A
		public override bool IsAbstract
		{
			get
			{
				return this._underlyingType.IsAbstract;
			}
		}

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x06007AA6 RID: 31398 RVA: 0x001BF967 File Offset: 0x001BDB67
		internal override bool IsMetadataAbstract
		{
			get
			{
				return this._underlyingType.IsMetadataAbstract;
			}
		}

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x06007AA7 RID: 31399 RVA: 0x001BF974 File Offset: 0x001BDB74
		public override bool IsSealed
		{
			get
			{
				return this._underlyingType.IsSealed;
			}
		}

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x06007AA8 RID: 31400 RVA: 0x001BF981 File Offset: 0x001BDB81
		internal override bool IsMetadataSealed
		{
			get
			{
				return this._underlyingType.IsMetadataSealed;
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x06007AA9 RID: 31401 RVA: 0x001BF98E File Offset: 0x001BDB8E
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return this._underlyingType.HasCodeAnalysisEmbeddedAttribute;
			}
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x06007AAA RID: 31402 RVA: 0x001BF99B File Offset: 0x001BDB9B
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return this._underlyingType.ObsoleteAttributeData;
			}
		}

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x06007AAB RID: 31403 RVA: 0x001BF9A8 File Offset: 0x001BDBA8
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return this._underlyingType.ShouldAddWinRTMembers;
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x06007AAC RID: 31404 RVA: 0x001BF9B5 File Offset: 0x001BDBB5
		internal override bool IsWindowsRuntimeImport
		{
			get
			{
				return this._underlyingType.IsWindowsRuntimeImport;
			}
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x06007AAD RID: 31405 RVA: 0x001BF9C2 File Offset: 0x001BDBC2
		internal override TypeLayout Layout
		{
			get
			{
				return this._underlyingType.Layout;
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x06007AAE RID: 31406 RVA: 0x001BF9CF File Offset: 0x001BDBCF
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return this._underlyingType.MarshallingCharSet;
			}
		}

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x06007AAF RID: 31407 RVA: 0x001BF9DC File Offset: 0x001BDBDC
		public override bool IsSerializable
		{
			get
			{
				return this._underlyingType.IsSerializable;
			}
		}

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x06007AB0 RID: 31408 RVA: 0x001BF9E9 File Offset: 0x001BDBE9
		public override bool IsRefLikeType
		{
			get
			{
				return this._underlyingType.IsRefLikeType;
			}
		}

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x06007AB1 RID: 31409 RVA: 0x001BF9F6 File Offset: 0x001BDBF6
		public override bool IsReadOnly
		{
			get
			{
				return this._underlyingType.IsReadOnly;
			}
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x06007AB2 RID: 31410 RVA: 0x001BFA03 File Offset: 0x001BDC03
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return this._underlyingType.HasDeclarativeSecurity;
			}
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x001BFA10 File Offset: 0x001BDC10
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			return this._underlyingType.GetSecurityInformation();
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x001BFA1D File Offset: 0x001BDC1D
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return this._underlyingType.GetAppliedConditionalSymbols();
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x001BFA2A File Offset: 0x001BDC2A
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return this._underlyingType.GetAttributeUsageInfo();
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x001BFA37 File Offset: 0x001BDC37
		internal override bool GetGuidString(out string guidString)
		{
			return this._underlyingType.GetGuidString(out guidString);
		}

		// Token: 0x04001D2F RID: 7471
		protected readonly NamedTypeSymbol _underlyingType;
	}
}
