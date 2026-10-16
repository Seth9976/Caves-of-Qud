using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005D0 RID: 1488
	internal sealed class TypeParameterSymbol : TypeSymbol, ITypeParameterSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007C71 RID: 31857 RVA: 0x001C15B4 File Offset: 0x001BF7B4
		public TypeParameterSymbol(TypeParameterSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x001C15C4 File Offset: 0x001BF7C4
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new TypeParameterSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x06007C73 RID: 31859 RVA: 0x001C15D2 File Offset: 0x001BF7D2
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x06007C74 RID: 31860 RVA: 0x001C15DA File Offset: 0x001BF7DA
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x06007C75 RID: 31861 RVA: 0x001C15E2 File Offset: 0x001BF7E2
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x06007C76 RID: 31862 RVA: 0x001C15EA File Offset: 0x001BF7EA
		internal TypeParameterSymbol UnderlyingTypeParameterSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x06007C77 RID: 31863 RVA: 0x001C15F4 File Offset: 0x001BF7F4
		NullableAnnotation ITypeParameterSymbol.ReferenceTypeConstraintNullableAnnotation
		{
			get
			{
				bool? referenceTypeConstraintIsNullable = this._underlying.ReferenceTypeConstraintIsNullable;
				NullableAnnotation nullableAnnotation;
				if (referenceTypeConstraintIsNullable != null)
				{
					if (!referenceTypeConstraintIsNullable.GetValueOrDefault())
					{
						if (!this._underlying.HasReferenceTypeConstraint)
						{
							nullableAnnotation = NullableAnnotation.None;
						}
						else
						{
							nullableAnnotation = NullableAnnotation.NotAnnotated;
						}
					}
					else
					{
						nullableAnnotation = NullableAnnotation.Annotated;
					}
				}
				else
				{
					nullableAnnotation = NullableAnnotation.None;
				}
				return nullableAnnotation;
			}
		}

		// Token: 0x17002473 RID: 9331
		// (get) Token: 0x06007C78 RID: 31864 RVA: 0x001C163B File Offset: 0x001BF83B
		TypeParameterKind ITypeParameterSymbol.TypeParameterKind
		{
			get
			{
				return this._underlying.TypeParameterKind;
			}
		}

		// Token: 0x17002474 RID: 9332
		// (get) Token: 0x06007C79 RID: 31865 RVA: 0x001C1648 File Offset: 0x001BF848
		IMethodSymbol ITypeParameterSymbol.DeclaringMethod
		{
			get
			{
				return this._underlying.DeclaringMethod.GetPublicSymbol();
			}
		}

		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x06007C7A RID: 31866 RVA: 0x001C165A File Offset: 0x001BF85A
		INamedTypeSymbol ITypeParameterSymbol.DeclaringType
		{
			get
			{
				return this._underlying.DeclaringType.GetPublicSymbol();
			}
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x06007C7B RID: 31867 RVA: 0x001C166C File Offset: 0x001BF86C
		ImmutableArray<ITypeSymbol> ITypeParameterSymbol.ConstraintTypes
		{
			get
			{
				return this._underlying.ConstraintTypesNoUseSiteDiagnostics.GetPublicSymbols();
			}
		}

		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x06007C7C RID: 31868 RVA: 0x001C167E File Offset: 0x001BF87E
		ImmutableArray<NullableAnnotation> ITypeParameterSymbol.ConstraintNullableAnnotations
		{
			get
			{
				return this._underlying.ConstraintTypesNoUseSiteDiagnostics.ToPublicAnnotations();
			}
		}

		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x06007C7D RID: 31869 RVA: 0x001C1690 File Offset: 0x001BF890
		ITypeParameterSymbol ITypeParameterSymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x06007C7E RID: 31870 RVA: 0x001C16A2 File Offset: 0x001BF8A2
		ITypeParameterSymbol ITypeParameterSymbol.ReducedFrom
		{
			get
			{
				return this._underlying.ReducedFrom.GetPublicSymbol();
			}
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x06007C7F RID: 31871 RVA: 0x001C16B4 File Offset: 0x001BF8B4
		int ITypeParameterSymbol.Ordinal
		{
			get
			{
				return this._underlying.Ordinal;
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x06007C80 RID: 31872 RVA: 0x001C16C1 File Offset: 0x001BF8C1
		VarianceKind ITypeParameterSymbol.Variance
		{
			get
			{
				return this._underlying.Variance;
			}
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06007C81 RID: 31873 RVA: 0x001C16CE File Offset: 0x001BF8CE
		bool ITypeParameterSymbol.HasReferenceTypeConstraint
		{
			get
			{
				return this._underlying.HasReferenceTypeConstraint;
			}
		}

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06007C82 RID: 31874 RVA: 0x001C16DB File Offset: 0x001BF8DB
		bool ITypeParameterSymbol.HasValueTypeConstraint
		{
			get
			{
				return this._underlying.HasValueTypeConstraint;
			}
		}

		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06007C83 RID: 31875 RVA: 0x001C16E8 File Offset: 0x001BF8E8
		bool ITypeParameterSymbol.HasUnmanagedTypeConstraint
		{
			get
			{
				return this._underlying.HasUnmanagedTypeConstraint;
			}
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06007C84 RID: 31876 RVA: 0x001C16F5 File Offset: 0x001BF8F5
		bool ITypeParameterSymbol.HasNotNullConstraint
		{
			get
			{
				return this._underlying.HasNotNullConstraint;
			}
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x06007C85 RID: 31877 RVA: 0x001C1702 File Offset: 0x001BF902
		bool ITypeParameterSymbol.HasConstructorConstraint
		{
			get
			{
				return this._underlying.HasConstructorConstraint;
			}
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x001C170F File Offset: 0x001BF90F
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitTypeParameter(this);
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x001C1718 File Offset: 0x001BF918
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameter(this);
		}

		// Token: 0x04001D54 RID: 7508
		private readonly TypeParameterSymbol _underlying;
	}
}
