using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CC RID: 1484
	internal sealed class PropertySymbol : Symbol, IPropertySymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007C27 RID: 31783 RVA: 0x001C10B9 File Offset: 0x001BF2B9
		public PropertySymbol(PropertySymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x06007C28 RID: 31784 RVA: 0x001C10C8 File Offset: 0x001BF2C8
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x06007C29 RID: 31785 RVA: 0x001C10D0 File Offset: 0x001BF2D0
		bool IPropertySymbol.IsIndexer
		{
			get
			{
				return this._underlying.IsIndexer;
			}
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x06007C2A RID: 31786 RVA: 0x001C10DD File Offset: 0x001BF2DD
		ITypeSymbol IPropertySymbol.Type
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

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x06007C2B RID: 31787 RVA: 0x001C110A File Offset: 0x001BF30A
		NullableAnnotation IPropertySymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x06007C2C RID: 31788 RVA: 0x001C111C File Offset: 0x001BF31C
		ImmutableArray<IParameterSymbol> IPropertySymbol.Parameters
		{
			get
			{
				return this._underlying.Parameters.GetPublicSymbols();
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x06007C2D RID: 31789 RVA: 0x001C112E File Offset: 0x001BF32E
		IMethodSymbol IPropertySymbol.GetMethod
		{
			get
			{
				return this._underlying.GetMethod.GetPublicSymbol();
			}
		}

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x06007C2E RID: 31790 RVA: 0x001C1140 File Offset: 0x001BF340
		IMethodSymbol IPropertySymbol.SetMethod
		{
			get
			{
				return this._underlying.SetMethod.GetPublicSymbol();
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x06007C2F RID: 31791 RVA: 0x001C1152 File Offset: 0x001BF352
		IPropertySymbol IPropertySymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x06007C30 RID: 31792 RVA: 0x001C1164 File Offset: 0x001BF364
		IPropertySymbol IPropertySymbol.OverriddenProperty
		{
			get
			{
				return this._underlying.OverriddenProperty.GetPublicSymbol();
			}
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x06007C31 RID: 31793 RVA: 0x001C1176 File Offset: 0x001BF376
		ImmutableArray<IPropertySymbol> IPropertySymbol.ExplicitInterfaceImplementations
		{
			get
			{
				return this._underlying.ExplicitInterfaceImplementations.GetPublicSymbols();
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x06007C32 RID: 31794 RVA: 0x001C1188 File Offset: 0x001BF388
		bool IPropertySymbol.IsReadOnly
		{
			get
			{
				return this._underlying.IsReadOnly;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x06007C33 RID: 31795 RVA: 0x001C1195 File Offset: 0x001BF395
		bool IPropertySymbol.IsWriteOnly
		{
			get
			{
				return this._underlying.IsWriteOnly;
			}
		}

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x06007C34 RID: 31796 RVA: 0x001C11A2 File Offset: 0x001BF3A2
		bool IPropertySymbol.IsWithEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x06007C35 RID: 31797 RVA: 0x001C11A8 File Offset: 0x001BF3A8
		ImmutableArray<CustomModifier> IPropertySymbol.TypeCustomModifiers
		{
			get
			{
				return this._underlying.TypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x06007C36 RID: 31798 RVA: 0x001C11C8 File Offset: 0x001BF3C8
		ImmutableArray<CustomModifier> IPropertySymbol.RefCustomModifiers
		{
			get
			{
				return this._underlying.RefCustomModifiers;
			}
		}

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x06007C37 RID: 31799 RVA: 0x001C11D5 File Offset: 0x001BF3D5
		bool IPropertySymbol.ReturnsByRef
		{
			get
			{
				return this._underlying.ReturnsByRef;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x06007C38 RID: 31800 RVA: 0x001C11E2 File Offset: 0x001BF3E2
		bool IPropertySymbol.ReturnsByRefReadonly
		{
			get
			{
				return this._underlying.ReturnsByRefReadonly;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x06007C39 RID: 31801 RVA: 0x001C11EF File Offset: 0x001BF3EF
		RefKind IPropertySymbol.RefKind
		{
			get
			{
				return this._underlying.RefKind;
			}
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x001C11FC File Offset: 0x001BF3FC
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitProperty(this);
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x001C1205 File Offset: 0x001BF405
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitProperty(this);
		}

		// Token: 0x04001D50 RID: 7504
		private readonly PropertySymbol _underlying;

		// Token: 0x04001D51 RID: 7505
		private ITypeSymbol _lazyType;
	}
}
