using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AC RID: 1452
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UnboundArgumentErrorTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x06007A00 RID: 31232 RVA: 0x001BE950 File Offset: 0x001BCB50
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static ImmutableArray<TypeWithAnnotations> CreateTypeArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeParameterSymbol> typeParameters, int n, [global::System.Runtime.CompilerServices.Nullable(1)] DiagnosticInfo errorInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			for (int i = 0; i < n; i++)
			{
				string text = ((i < typeParameters.Length) ? typeParameters[i].Name : string.Empty);
				instance.Add(TypeWithAnnotations.Create(new UnboundArgumentErrorTypeSymbol(text, errorInfo, null), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x001BE9B1 File Offset: 0x001BCBB1
		private UnboundArgumentErrorTypeSymbol(string name, DiagnosticInfo errorInfo, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this._name = name;
			this._errorInfo = errorInfo;
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x001BE9C8 File Offset: 0x001BCBC8
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new UnboundArgumentErrorTypeSymbol(this._name, this._errorInfo, newData);
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x06007A03 RID: 31235 RVA: 0x001BE9DC File Offset: 0x001BCBDC
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x06007A04 RID: 31236 RVA: 0x001BE9E4 File Offset: 0x001BCBE4
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x06007A05 RID: 31237 RVA: 0x001BE9E7 File Offset: 0x001BCBE7
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				return this._errorInfo;
			}
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001BE9F0 File Offset: 0x001BCBF0
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			if (t2 == this)
			{
				return true;
			}
			UnboundArgumentErrorTypeSymbol unboundArgumentErrorTypeSymbol = t2 as UnboundArgumentErrorTypeSymbol;
			return unboundArgumentErrorTypeSymbol != null && string.Equals(unboundArgumentErrorTypeSymbol._name, this._name, 4) && object.Equals(unboundArgumentErrorTypeSymbol._errorInfo, this._errorInfo);
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001BEA34 File Offset: 0x001BCC34
		public override int GetHashCode()
		{
			if (this._errorInfo != null)
			{
				return Hash.Combine<string>(this._name, this._errorInfo.Code);
			}
			return this._name.GetHashCode();
		}

		// Token: 0x04001D26 RID: 7462
		public static readonly ErrorTypeSymbol Instance = new UnboundArgumentErrorTypeSymbol(string.Empty, new CSDiagnosticInfo(ErrorCode.ERR_UnexpectedUnboundGenericName), null);

		// Token: 0x04001D27 RID: 7463
		private readonly string _name;

		// Token: 0x04001D28 RID: 7464
		private readonly DiagnosticInfo _errorInfo;
	}
}
