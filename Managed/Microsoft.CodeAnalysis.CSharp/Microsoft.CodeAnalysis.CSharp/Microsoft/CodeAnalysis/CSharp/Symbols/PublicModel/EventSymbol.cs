using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BD RID: 1469
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EventSymbol : Symbol, IEventSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B36 RID: 31542 RVA: 0x001C0205 File Offset: 0x001BE405
		public EventSymbol(EventSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06007B37 RID: 31543 RVA: 0x001C0214 File Offset: 0x001BE414
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x06007B38 RID: 31544 RVA: 0x001C021C File Offset: 0x001BE41C
		internal EventSymbol UnderlyingEventSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x06007B39 RID: 31545 RVA: 0x001C0224 File Offset: 0x001BE424
		ITypeSymbol IEventSymbol.Type
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

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x06007B3A RID: 31546 RVA: 0x001C0251 File Offset: 0x001BE451
		NullableAnnotation IEventSymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x06007B3B RID: 31547 RVA: 0x001C0263 File Offset: 0x001BE463
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IMethodSymbol IEventSymbol.AddMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._underlying.AddMethod.GetPublicSymbol();
			}
		}

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x06007B3C RID: 31548 RVA: 0x001C0275 File Offset: 0x001BE475
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IMethodSymbol IEventSymbol.RemoveMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._underlying.RemoveMethod.GetPublicSymbol();
			}
		}

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x06007B3D RID: 31549 RVA: 0x001C0287 File Offset: 0x001BE487
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IMethodSymbol IEventSymbol.RaiseMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x06007B3E RID: 31550 RVA: 0x001C028A File Offset: 0x001BE48A
		IEventSymbol IEventSymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x06007B3F RID: 31551 RVA: 0x001C029C File Offset: 0x001BE49C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IEventSymbol IEventSymbol.OverriddenEvent
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._underlying.OverriddenEvent.GetPublicSymbol();
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x06007B40 RID: 31552 RVA: 0x001C02AE File Offset: 0x001BE4AE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IEventSymbol> IEventSymbol.ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlying.ExplicitInterfaceImplementations.GetPublicSymbols();
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x06007B41 RID: 31553 RVA: 0x001C02C0 File Offset: 0x001BE4C0
		bool IEventSymbol.IsWindowsRuntimeEvent
		{
			get
			{
				return this._underlying.IsWindowsRuntimeEvent;
			}
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x001C02CD File Offset: 0x001BE4CD
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitEvent(this);
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x001C02D6 File Offset: 0x001BE4D6
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitEvent(this);
		}

		// Token: 0x04001D3A RID: 7482
		private readonly EventSymbol _underlying;

		// Token: 0x04001D3B RID: 7483
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ITypeSymbol _lazyType;
	}
}
