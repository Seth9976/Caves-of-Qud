using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FF RID: 1279
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NativeIntegerParameterSymbol : WrappedParameterSymbol, IReference
	{
		// Token: 0x06006C44 RID: 27716 RVA: 0x0019028B File Offset: 0x0018E48B
		internal NativeIntegerParameterSymbol(NativeIntegerTypeSymbol containingType, NativeIntegerMethodSymbol container, ParameterSymbol underlyingParameter)
			: base(underlyingParameter)
		{
			this._containingType = containingType;
			this._container = container;
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x06006C45 RID: 27717 RVA: 0x001902A2 File Offset: 0x0018E4A2
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x06006C46 RID: 27718 RVA: 0x001902AA File Offset: 0x0018E4AA
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._containingType.SubstituteUnderlyingType(this._underlyingParameter.TypeWithAnnotations);
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x06006C47 RID: 27719 RVA: 0x001902C2 File Offset: 0x0018E4C2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlyingParameter.RefCustomModifiers;
			}
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x001902CF File Offset: 0x0018E4CF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(Symbol other, TypeCompareKind comparison)
		{
			return NativeIntegerTypeSymbol.EqualsHelper<NativeIntegerParameterSymbol>(this, other, comparison, (NativeIntegerParameterSymbol symbol) => symbol._underlyingParameter);
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x001902F8 File Offset: 0x0018E4F8
		public override int GetHashCode()
		{
			return this._underlyingParameter.GetHashCode();
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x00190305 File Offset: 0x0018E505
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001AC6 RID: 6854
		private readonly NativeIntegerTypeSymbol _containingType;

		// Token: 0x04001AC7 RID: 6855
		private readonly NativeIntegerMethodSymbol _container;
	}
}
