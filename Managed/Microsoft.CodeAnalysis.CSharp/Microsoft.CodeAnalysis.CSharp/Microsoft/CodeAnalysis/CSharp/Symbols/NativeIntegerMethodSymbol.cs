using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FE RID: 1278
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NativeIntegerMethodSymbol : WrappedMethodSymbol, IReference
	{
		// Token: 0x06006C34 RID: 27700 RVA: 0x00190165 File Offset: 0x0018E365
		internal NativeIntegerMethodSymbol(NativeIntegerTypeSymbol container, MethodSymbol underlyingMethod, [global::System.Runtime.CompilerServices.Nullable(2)] NativeIntegerPropertySymbol associatedSymbol)
		{
			this._container = container;
			this._associatedSymbol = associatedSymbol;
			this.UnderlyingMethod = underlyingMethod;
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x00190182 File Offset: 0x0018E382
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x06006C36 RID: 27702 RVA: 0x0019018A File Offset: 0x0018E38A
		public override MethodSymbol UnderlyingMethod { get; }

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x06006C37 RID: 27703 RVA: 0x00190192 File Offset: 0x0018E392
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._container.SubstituteUnderlyingType(this.UnderlyingMethod.ReturnTypeWithAnnotations);
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x06006C38 RID: 27704 RVA: 0x001901AA File Offset: 0x0018E3AA
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06006C39 RID: 27705 RVA: 0x001901B1 File Offset: 0x0018E3B1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x001901B8 File Offset: 0x0018E3B8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyParameters.IsDefault)
				{
					ImmutableArray<ParameterSymbol> immutableArray = this.UnderlyingMethod.Parameters.SelectAsArray((ParameterSymbol p, NativeIntegerMethodSymbol m) => new NativeIntegerParameterSymbol(m._container, m, p), this);
					ImmutableInterlocked.InterlockedInitialize<ParameterSymbol>(ref this._lazyParameters, immutableArray);
				}
				return this._lazyParameters;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06006C3B RID: 27707 RVA: 0x00190216 File Offset: 0x0018E416
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06006C3C RID: 27708 RVA: 0x0019021D File Offset: 0x0018E41D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.UnderlyingMethod.RefCustomModifiers;
			}
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x0019022A File Offset: 0x0018E42A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return this.UnderlyingMethod.GetUnmanagedCallersOnlyAttributeData(forceComplete);
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06006C3E RID: 27710 RVA: 0x00190238 File Offset: 0x0018E438
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._associatedSymbol;
			}
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x00190240 File Offset: 0x0018E440
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x00190247 File Offset: 0x0018E447
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x0019024E File Offset: 0x0018E44E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(Symbol other, TypeCompareKind comparison)
		{
			return NativeIntegerTypeSymbol.EqualsHelper<NativeIntegerMethodSymbol>(this, other, comparison, (NativeIntegerMethodSymbol symbol) => symbol.UnderlyingMethod);
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x00190277 File Offset: 0x0018E477
		public override int GetHashCode()
		{
			return this.UnderlyingMethod.GetHashCode();
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x00190284 File Offset: 0x0018E484
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001AC2 RID: 6850
		private readonly NativeIntegerTypeSymbol _container;

		// Token: 0x04001AC3 RID: 6851
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly NativeIntegerPropertySymbol _associatedSymbol;

		// Token: 0x04001AC4 RID: 6852
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ParameterSymbol> _lazyParameters;
	}
}
