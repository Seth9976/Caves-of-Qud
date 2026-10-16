using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C4 RID: 1476
	internal abstract class NamedTypeSymbol : TypeSymbol, INamedTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007BA9 RID: 31657 RVA: 0x001C0A06 File Offset: 0x001BEC06
		public NamedTypeSymbol(NullableAnnotation nullableAnnotation = NullableAnnotation.None)
			: base(nullableAnnotation)
		{
		}

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06007BAA RID: 31658
		internal abstract NamedTypeSymbol UnderlyingNamedTypeSymbol { get; }

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x001C0A0F File Offset: 0x001BEC0F
		int INamedTypeSymbol.Arity
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.Arity;
			}
		}

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x06007BAC RID: 31660 RVA: 0x001C0A1C File Offset: 0x001BEC1C
		ImmutableArray<IMethodSymbol> INamedTypeSymbol.InstanceConstructors
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.InstanceConstructors.GetPublicSymbols();
			}
		}

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x001C0A2E File Offset: 0x001BEC2E
		ImmutableArray<IMethodSymbol> INamedTypeSymbol.StaticConstructors
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.StaticConstructors.GetPublicSymbols();
			}
		}

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x06007BAE RID: 31662 RVA: 0x001C0A40 File Offset: 0x001BEC40
		ImmutableArray<IMethodSymbol> INamedTypeSymbol.Constructors
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.Constructors.GetPublicSymbols();
			}
		}

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x001C0A52 File Offset: 0x001BEC52
		IEnumerable<string> INamedTypeSymbol.MemberNames
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.MemberNames;
			}
		}

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06007BB0 RID: 31664 RVA: 0x001C0A5F File Offset: 0x001BEC5F
		ImmutableArray<ITypeParameterSymbol> INamedTypeSymbol.TypeParameters
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.TypeParameters.GetPublicSymbols();
			}
		}

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x001C0A74 File Offset: 0x001BEC74
		ImmutableArray<ITypeSymbol> INamedTypeSymbol.TypeArguments
		{
			get
			{
				if (this._lazyTypeArguments.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ITypeSymbol>(ref this._lazyTypeArguments, this.UnderlyingNamedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.GetPublicSymbols(), default(ImmutableArray<ITypeSymbol>));
				}
				return this._lazyTypeArguments;
			}
		}

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x06007BB2 RID: 31666 RVA: 0x001C0AB9 File Offset: 0x001BECB9
		ImmutableArray<NullableAnnotation> INamedTypeSymbol.TypeArgumentNullableAnnotations
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.ToPublicAnnotations();
			}
		}

		// Token: 0x06007BB3 RID: 31667 RVA: 0x001C0ACC File Offset: 0x001BECCC
		ImmutableArray<CustomModifier> INamedTypeSymbol.GetTypeArgumentCustomModifiers(int ordinal)
		{
			return this.UnderlyingNamedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[ordinal].CustomModifiers;
		}

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x001C0AF5 File Offset: 0x001BECF5
		INamedTypeSymbol INamedTypeSymbol.OriginalDefinition
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x06007BB5 RID: 31669 RVA: 0x001C0B07 File Offset: 0x001BED07
		IMethodSymbol INamedTypeSymbol.DelegateInvokeMethod
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.DelegateInvokeMethod.GetPublicSymbol();
			}
		}

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x06007BB6 RID: 31670 RVA: 0x001C0B19 File Offset: 0x001BED19
		INamedTypeSymbol INamedTypeSymbol.EnumUnderlyingType
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.EnumUnderlyingType.GetPublicSymbol();
			}
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x001C0B2B File Offset: 0x001BED2B
		INamedTypeSymbol INamedTypeSymbol.ConstructedFrom
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.ConstructedFrom.GetPublicSymbol();
			}
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x001C0B3D File Offset: 0x001BED3D
		INamedTypeSymbol INamedTypeSymbol.Construct(params ITypeSymbol[] typeArguments)
		{
			return this.UnderlyingNamedTypeSymbol.Construct(Symbol.ConstructTypeArguments(typeArguments), false).GetPublicSymbol();
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x001C0B56 File Offset: 0x001BED56
		INamedTypeSymbol INamedTypeSymbol.Construct(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
		{
			return this.UnderlyingNamedTypeSymbol.Construct(Symbol.ConstructTypeArguments(typeArguments, typeArgumentNullableAnnotations), false).GetPublicSymbol();
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x001C0B70 File Offset: 0x001BED70
		INamedTypeSymbol INamedTypeSymbol.ConstructUnboundGenericType()
		{
			return this.UnderlyingNamedTypeSymbol.ConstructUnboundGenericType().GetPublicSymbol();
		}

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x06007BBB RID: 31675 RVA: 0x001C0B82 File Offset: 0x001BED82
		ISymbol INamedTypeSymbol.AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x06007BBC RID: 31676 RVA: 0x001C0B85 File Offset: 0x001BED85
		ImmutableArray<IFieldSymbol> INamedTypeSymbol.TupleElements
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.TupleElements.GetPublicSymbols();
			}
		}

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x06007BBD RID: 31677 RVA: 0x001C0B98 File Offset: 0x001BED98
		INamedTypeSymbol INamedTypeSymbol.TupleUnderlyingType
		{
			get
			{
				NamedTypeSymbol underlyingNamedTypeSymbol = this.UnderlyingNamedTypeSymbol;
				NamedTypeSymbol tupleUnderlyingType = underlyingNamedTypeSymbol.TupleUnderlyingType;
				if (!underlyingNamedTypeSymbol.Equals(tupleUnderlyingType, TypeCompareKind.ConsiderEverything))
				{
					return tupleUnderlyingType.GetPublicSymbol();
				}
				return null;
			}
		}

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x06007BBE RID: 31678 RVA: 0x001C0BC3 File Offset: 0x001BEDC3
		bool INamedTypeSymbol.IsComImport
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsComImport;
			}
		}

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x06007BBF RID: 31679 RVA: 0x001C0BD0 File Offset: 0x001BEDD0
		bool INamedTypeSymbol.IsGenericType
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsGenericType;
			}
		}

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x06007BC0 RID: 31680 RVA: 0x001C0BDD File Offset: 0x001BEDDD
		bool INamedTypeSymbol.IsUnboundGenericType
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsUnboundGenericType;
			}
		}

		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x06007BC1 RID: 31681 RVA: 0x001C0BEA File Offset: 0x001BEDEA
		bool INamedTypeSymbol.IsScriptClass
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsScriptClass;
			}
		}

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x06007BC2 RID: 31682 RVA: 0x001C0BF7 File Offset: 0x001BEDF7
		bool INamedTypeSymbol.IsImplicitClass
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsImplicitClass;
			}
		}

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x06007BC3 RID: 31683 RVA: 0x001C0C04 File Offset: 0x001BEE04
		bool INamedTypeSymbol.MightContainExtensionMethods
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.MightContainExtensionMethods;
			}
		}

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x001C0C11 File Offset: 0x001BEE11
		bool INamedTypeSymbol.IsSerializable
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.IsSerializable;
			}
		}

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x06007BC5 RID: 31685 RVA: 0x001C0C1E File Offset: 0x001BEE1E
		INamedTypeSymbol INamedTypeSymbol.NativeIntegerUnderlyingType
		{
			get
			{
				return this.UnderlyingNamedTypeSymbol.NativeIntegerUnderlyingType.GetPublicSymbol();
			}
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x001C0C30 File Offset: 0x001BEE30
		protected sealed override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitNamedType(this);
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x001C0C39 File Offset: 0x001BEE39
		protected sealed override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitNamedType(this);
		}

		// Token: 0x04001D47 RID: 7495
		private ImmutableArray<ITypeSymbol> _lazyTypeArguments;
	}
}
