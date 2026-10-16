using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BE RID: 1470
	internal sealed class FieldSymbol : Symbol, IFieldSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B44 RID: 31556 RVA: 0x001C02DF File Offset: 0x001BE4DF
		public FieldSymbol(FieldSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x001C02EE File Offset: 0x001BE4EE
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x06007B46 RID: 31558 RVA: 0x001C02F6 File Offset: 0x001BE4F6
		ISymbol IFieldSymbol.AssociatedSymbol
		{
			get
			{
				return this._underlying.AssociatedSymbol.GetPublicSymbol();
			}
		}

		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x001C0308 File Offset: 0x001BE508
		ITypeSymbol IFieldSymbol.Type
		{
			get
			{
				if (this._lazyType == null)
				{
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyType, this._underlying.TypeWithAnnotations.GetPublicSymbol(), null);
				}
				return this._lazyType;
			}
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x06007B48 RID: 31560 RVA: 0x001C0335 File Offset: 0x001BE535
		NullableAnnotation IFieldSymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x001C0348 File Offset: 0x001BE548
		ImmutableArray<CustomModifier> IFieldSymbol.CustomModifiers
		{
			get
			{
				return this._underlying.TypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x06007B4A RID: 31562 RVA: 0x001C0368 File Offset: 0x001BE568
		IFieldSymbol IFieldSymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x06007B4B RID: 31563 RVA: 0x001C037A File Offset: 0x001BE57A
		IFieldSymbol IFieldSymbol.CorrespondingTupleField
		{
			get
			{
				return this._underlying.CorrespondingTupleField.GetPublicSymbol();
			}
		}

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x06007B4C RID: 31564 RVA: 0x001C038C File Offset: 0x001BE58C
		bool IFieldSymbol.IsExplicitlyNamedTupleElement
		{
			get
			{
				return this._underlying.IsExplicitlyNamedTupleElement;
			}
		}

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x06007B4D RID: 31565 RVA: 0x001C0399 File Offset: 0x001BE599
		bool IFieldSymbol.IsConst
		{
			get
			{
				return this._underlying.IsConst;
			}
		}

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x001C03A6 File Offset: 0x001BE5A6
		bool IFieldSymbol.IsReadOnly
		{
			get
			{
				return this._underlying.IsReadOnly;
			}
		}

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x06007B4F RID: 31567 RVA: 0x001C03B3 File Offset: 0x001BE5B3
		bool IFieldSymbol.IsVolatile
		{
			get
			{
				return this._underlying.IsVolatile;
			}
		}

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x06007B50 RID: 31568 RVA: 0x001C03C0 File Offset: 0x001BE5C0
		bool IFieldSymbol.IsFixedSizeBuffer
		{
			get
			{
				return this._underlying.IsFixedSizeBuffer;
			}
		}

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x06007B51 RID: 31569 RVA: 0x001C03CD File Offset: 0x001BE5CD
		bool IFieldSymbol.HasConstantValue
		{
			get
			{
				return this._underlying.HasConstantValue;
			}
		}

		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x06007B52 RID: 31570 RVA: 0x001C03DA File Offset: 0x001BE5DA
		object IFieldSymbol.ConstantValue
		{
			get
			{
				return this._underlying.ConstantValue;
			}
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x001C03E7 File Offset: 0x001BE5E7
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitField(this);
		}

		// Token: 0x06007B54 RID: 31572 RVA: 0x001C03F0 File Offset: 0x001BE5F0
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitField(this);
		}

		// Token: 0x04001D3C RID: 7484
		private readonly FieldSymbol _underlying;

		// Token: 0x04001D3D RID: 7485
		private ITypeSymbol _lazyType;
	}
}
