using System;
using System.Collections.Immutable;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DF RID: 1247
	internal sealed class ErrorPropertySymbol : PropertySymbol
	{
		// Token: 0x0600696B RID: 26987 RVA: 0x0018A938 File Offset: 0x00188B38
		public ErrorPropertySymbol(Symbol containingSymbol, TypeSymbol type, string name, bool isIndexer, bool isIndexedProperty)
		{
			this._containingSymbol = containingSymbol;
			this._typeWithAnnotations = TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this._name = name;
			this._isIndexer = isIndexer;
			this._isIndexedProperty = isIndexedProperty;
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x0600696C RID: 26988 RVA: 0x0018A97F File Offset: 0x00188B7F
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x0018A987 File Offset: 0x00188B87
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x0018A98A File Offset: 0x00188B8A
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._typeWithAnnotations;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x0600696F RID: 26991 RVA: 0x0018A992 File Offset: 0x00188B92
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06006970 RID: 26992 RVA: 0x0018A99A File Offset: 0x00188B9A
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06006971 RID: 26993 RVA: 0x0018A99D File Offset: 0x00188B9D
		public override bool IsIndexer
		{
			get
			{
				return this._isIndexer;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06006972 RID: 26994 RVA: 0x0018A9A5 File Offset: 0x00188BA5
		public override bool IsIndexedProperty
		{
			get
			{
				return this._isIndexedProperty;
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06006973 RID: 26995 RVA: 0x0018A9AD File Offset: 0x00188BAD
		public override MethodSymbol GetMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x0018A9B0 File Offset: 0x00188BB0
		public override MethodSymbol SetMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06006975 RID: 26997 RVA: 0x0018A9B3 File Offset: 0x00188BB3
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06006976 RID: 26998 RVA: 0x0018A9BA File Offset: 0x00188BBA
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06006977 RID: 26999 RVA: 0x0018A9C1 File Offset: 0x00188BC1
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06006978 RID: 27000 RVA: 0x0018A9C4 File Offset: 0x00188BC4
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06006979 RID: 27001 RVA: 0x0018A9C7 File Offset: 0x00188BC7
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x0600697A RID: 27002 RVA: 0x0018A9CA File Offset: 0x00188BCA
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x0600697B RID: 27003 RVA: 0x0018A9CD File Offset: 0x00188BCD
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x0600697C RID: 27004 RVA: 0x0018A9D0 File Offset: 0x00188BD0
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x0600697D RID: 27005 RVA: 0x0018A9D3 File Offset: 0x00188BD3
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x0600697E RID: 27006 RVA: 0x0018A9D6 File Offset: 0x00188BD6
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x0600697F RID: 27007 RVA: 0x0018A9D9 File Offset: 0x00188BD9
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06006980 RID: 27008 RVA: 0x0018A9E0 File Offset: 0x00188BE0
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06006981 RID: 27009 RVA: 0x0018A9E3 File Offset: 0x00188BE3
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06006982 RID: 27010 RVA: 0x0018A9E6 File Offset: 0x00188BE6
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<PropertySymbol>.Empty;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06006983 RID: 27011 RVA: 0x0018A9ED File Offset: 0x00188BED
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x04001A44 RID: 6724
		private readonly Symbol _containingSymbol;

		// Token: 0x04001A45 RID: 6725
		private readonly TypeWithAnnotations _typeWithAnnotations;

		// Token: 0x04001A46 RID: 6726
		private readonly string _name;

		// Token: 0x04001A47 RID: 6727
		private readonly bool _isIndexer;

		// Token: 0x04001A48 RID: 6728
		private readonly bool _isIndexedProperty;
	}
}
