using System;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C1 RID: 1473
	internal sealed class LocalSymbol : Symbol, ILocalSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B62 RID: 31586 RVA: 0x001C048E File Offset: 0x001BE68E
		public LocalSymbol(LocalSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x06007B63 RID: 31587 RVA: 0x001C049D File Offset: 0x001BE69D
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x001C04A5 File Offset: 0x001BE6A5
		ITypeSymbol ILocalSymbol.Type
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

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x06007B65 RID: 31589 RVA: 0x001C04D2 File Offset: 0x001BE6D2
		NullableAnnotation ILocalSymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x001C04E4 File Offset: 0x001BE6E4
		bool ILocalSymbol.IsFunctionValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x06007B67 RID: 31591 RVA: 0x001C04E7 File Offset: 0x001BE6E7
		bool ILocalSymbol.IsConst
		{
			get
			{
				return this._underlying.IsConst;
			}
		}

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x001C04F4 File Offset: 0x001BE6F4
		bool ILocalSymbol.IsRef
		{
			get
			{
				return this._underlying.IsRef;
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x06007B69 RID: 31593 RVA: 0x001C0501 File Offset: 0x001BE701
		RefKind ILocalSymbol.RefKind
		{
			get
			{
				return this._underlying.RefKind;
			}
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x06007B6A RID: 31594 RVA: 0x001C050E File Offset: 0x001BE70E
		bool ILocalSymbol.HasConstantValue
		{
			get
			{
				return this._underlying.HasConstantValue;
			}
		}

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x06007B6B RID: 31595 RVA: 0x001C051B File Offset: 0x001BE71B
		object ILocalSymbol.ConstantValue
		{
			get
			{
				return this._underlying.ConstantValue;
			}
		}

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x06007B6C RID: 31596 RVA: 0x001C0528 File Offset: 0x001BE728
		bool ILocalSymbol.IsFixed
		{
			get
			{
				return this._underlying.IsFixed;
			}
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x001C0535 File Offset: 0x001BE735
		protected sealed override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitLocal(this);
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x001C053E File Offset: 0x001BE73E
		protected sealed override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitLocal(this);
		}

		// Token: 0x04001D40 RID: 7488
		private readonly LocalSymbol _underlying;

		// Token: 0x04001D41 RID: 7489
		private ITypeSymbol _lazyType;
	}
}
