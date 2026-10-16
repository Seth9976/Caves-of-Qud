using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C9 RID: 1481
	internal sealed class ParameterSymbol : Symbol, IParameterSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007BE7 RID: 31719 RVA: 0x001C0DD3 File Offset: 0x001BEFD3
		public ParameterSymbol(ParameterSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x001C0DE2 File Offset: 0x001BEFE2
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06007BE9 RID: 31721 RVA: 0x001C0DEA File Offset: 0x001BEFEA
		ITypeSymbol IParameterSymbol.Type
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

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x06007BEA RID: 31722 RVA: 0x001C0E17 File Offset: 0x001BF017
		NullableAnnotation IParameterSymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x06007BEB RID: 31723 RVA: 0x001C0E2C File Offset: 0x001BF02C
		ImmutableArray<CustomModifier> IParameterSymbol.CustomModifiers
		{
			get
			{
				return this._underlying.TypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x06007BEC RID: 31724 RVA: 0x001C0E4C File Offset: 0x001BF04C
		ImmutableArray<CustomModifier> IParameterSymbol.RefCustomModifiers
		{
			get
			{
				return this._underlying.RefCustomModifiers;
			}
		}

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x06007BED RID: 31725 RVA: 0x001C0E59 File Offset: 0x001BF059
		IParameterSymbol IParameterSymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x06007BEE RID: 31726 RVA: 0x001C0E6B File Offset: 0x001BF06B
		RefKind IParameterSymbol.RefKind
		{
			get
			{
				return this._underlying.RefKind;
			}
		}

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x06007BEF RID: 31727 RVA: 0x001C0E78 File Offset: 0x001BF078
		bool IParameterSymbol.IsDiscard
		{
			get
			{
				return this._underlying.IsDiscard;
			}
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x06007BF0 RID: 31728 RVA: 0x001C0E85 File Offset: 0x001BF085
		bool IParameterSymbol.IsParams
		{
			get
			{
				return this._underlying.IsParams;
			}
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x001C0E92 File Offset: 0x001BF092
		bool IParameterSymbol.IsOptional
		{
			get
			{
				return this._underlying.IsOptional;
			}
		}

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x06007BF2 RID: 31730 RVA: 0x001C0E9F File Offset: 0x001BF09F
		bool IParameterSymbol.IsThis
		{
			get
			{
				return this._underlying.IsThis;
			}
		}

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x001C0EAC File Offset: 0x001BF0AC
		int IParameterSymbol.Ordinal
		{
			get
			{
				return this._underlying.Ordinal;
			}
		}

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06007BF4 RID: 31732 RVA: 0x001C0EB9 File Offset: 0x001BF0B9
		bool IParameterSymbol.HasExplicitDefaultValue
		{
			get
			{
				return this._underlying.HasExplicitDefaultValue;
			}
		}

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06007BF5 RID: 31733 RVA: 0x001C0EC6 File Offset: 0x001BF0C6
		object IParameterSymbol.ExplicitDefaultValue
		{
			get
			{
				return this._underlying.ExplicitDefaultValue;
			}
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x001C0ED3 File Offset: 0x001BF0D3
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitParameter(this);
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001C0EDC File Offset: 0x001BF0DC
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x04001D4B RID: 7499
		private readonly ParameterSymbol _underlying;

		// Token: 0x04001D4C RID: 7500
		private ITypeSymbol _lazyType;
	}
}
