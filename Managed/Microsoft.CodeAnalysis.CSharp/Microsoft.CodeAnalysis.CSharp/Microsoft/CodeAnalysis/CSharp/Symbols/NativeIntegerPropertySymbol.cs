using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000500 RID: 1280
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NativeIntegerPropertySymbol : WrappedPropertySymbol, IReference
	{
		// Token: 0x06006C4B RID: 27723 RVA: 0x0019030C File Offset: 0x0018E50C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal NativeIntegerPropertySymbol(NativeIntegerTypeSymbol container, PropertySymbol underlyingProperty, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 1, 2, 2 })] Func<NativeIntegerTypeSymbol, NativeIntegerPropertySymbol, MethodSymbol, NativeIntegerMethodSymbol> getAccessor)
			: base(underlyingProperty)
		{
			this._container = container;
			this.GetMethod = getAccessor.Invoke(container, this, underlyingProperty.GetMethod);
			this.SetMethod = getAccessor.Invoke(container, this, underlyingProperty.SetMethod);
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06006C4C RID: 27724 RVA: 0x00190344 File Offset: 0x0018E544
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._container;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06006C4D RID: 27725 RVA: 0x0019034C File Offset: 0x0018E54C
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._container.SubstituteUnderlyingType(this._underlyingProperty.TypeWithAnnotations);
			}
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06006C4E RID: 27726 RVA: 0x00190364 File Offset: 0x0018E564
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return base.UnderlyingProperty.RefCustomModifiers;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x06006C4F RID: 27727 RVA: 0x00190371 File Offset: 0x0018E571
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x06006C50 RID: 27728 RVA: 0x00190378 File Offset: 0x0018E578
		public override MethodSymbol GetMethod { get; }

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06006C51 RID: 27729 RVA: 0x00190380 File Offset: 0x0018E580
		public override MethodSymbol SetMethod { get; }

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06006C52 RID: 27730 RVA: 0x00190388 File Offset: 0x0018E588
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<PropertySymbol>.Empty;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06006C53 RID: 27731 RVA: 0x0019038F File Offset: 0x0018E58F
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return this._underlyingProperty.MustCallMethodsDirectly;
			}
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x0019039C File Offset: 0x0018E59C
		public override bool Equals(Symbol other, TypeCompareKind comparison)
		{
			return NativeIntegerTypeSymbol.EqualsHelper<NativeIntegerPropertySymbol>(this, other, comparison, (NativeIntegerPropertySymbol symbol) => symbol._underlyingProperty);
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x001903C5 File Offset: 0x0018E5C5
		public override int GetHashCode()
		{
			return this._underlyingProperty.GetHashCode();
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x001903D2 File Offset: 0x0018E5D2
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001AC8 RID: 6856
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly NativeIntegerTypeSymbol _container;
	}
}
