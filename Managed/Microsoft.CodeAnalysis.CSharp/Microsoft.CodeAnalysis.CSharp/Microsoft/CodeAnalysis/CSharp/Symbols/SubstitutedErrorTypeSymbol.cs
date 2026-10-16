using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E1 RID: 1249
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SubstitutedErrorTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x060069C8 RID: 27080 RVA: 0x0018AC80 File Offset: 0x00188E80
		protected SubstitutedErrorTypeSymbol(ErrorTypeSymbol originalDefinition, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this._originalDefinition = originalDefinition;
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x060069C9 RID: 27081 RVA: 0x0018AC90 File Offset: 0x00188E90
		public override NamedTypeSymbol OriginalDefinition
		{
			get
			{
				return this._originalDefinition;
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x0018AC98 File Offset: 0x00188E98
		internal override bool MangleName
		{
			get
			{
				return this._originalDefinition.MangleName;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x060069CB RID: 27083 RVA: 0x0018ACA5 File Offset: 0x00188EA5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override DiagnosticInfo ErrorInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._originalDefinition.ErrorInfo;
			}
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x0018ACB2 File Offset: 0x00188EB2
		public override int Arity
		{
			get
			{
				return this._originalDefinition.Arity;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x060069CD RID: 27085 RVA: 0x0018ACBF File Offset: 0x00188EBF
		public override string Name
		{
			get
			{
				return this._originalDefinition.Name;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x060069CE RID: 27086 RVA: 0x0018ACCC File Offset: 0x00188ECC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._originalDefinition.Locations;
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x060069CF RID: 27087 RVA: 0x0018ACD9 File Offset: 0x00188ED9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> CandidateSymbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._originalDefinition.CandidateSymbols;
			}
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x060069D0 RID: 27088 RVA: 0x0018ACE6 File Offset: 0x00188EE6
		internal override LookupResultKind ResultKind
		{
			get
			{
				return this._originalDefinition.ResultKind;
			}
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x0018ACF3 File Offset: 0x00188EF3
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return this._originalDefinition.GetUseSiteInfo();
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x0018AD00 File Offset: 0x00188F00
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = this.ComputeHashCode();
			}
			return this._hashCode;
		}

		// Token: 0x04001A4B RID: 6731
		private readonly ErrorTypeSymbol _originalDefinition;

		// Token: 0x04001A4C RID: 6732
		private int _hashCode;
	}
}
