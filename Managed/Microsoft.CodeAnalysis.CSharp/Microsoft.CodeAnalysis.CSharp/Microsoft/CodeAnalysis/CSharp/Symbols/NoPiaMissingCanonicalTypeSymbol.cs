using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000505 RID: 1285
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NoPiaMissingCanonicalTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x06006C7B RID: 27771 RVA: 0x001908AD File Offset: 0x0018EAAD
		public NoPiaMissingCanonicalTypeSymbol(AssemblySymbol embeddingAssembly, string fullTypeName, string guid, string scope, string identifier, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this._embeddingAssembly = embeddingAssembly;
			this._fullTypeName = fullTypeName;
			this._guid = guid;
			this._scope = scope;
			this._identifier = identifier;
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x001908DC File Offset: 0x0018EADC
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new NoPiaMissingCanonicalTypeSymbol(this._embeddingAssembly, this._fullTypeName, this._guid, this._scope, this._identifier, newData);
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x06006C7D RID: 27773 RVA: 0x00190902 File Offset: 0x0018EB02
		public AssemblySymbol EmbeddingAssembly
		{
			get
			{
				return this._embeddingAssembly;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06006C7E RID: 27774 RVA: 0x0019090A File Offset: 0x0018EB0A
		public string FullTypeName
		{
			get
			{
				return this._fullTypeName;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06006C7F RID: 27775 RVA: 0x00190912 File Offset: 0x0018EB12
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06006C80 RID: 27776 RVA: 0x00190915 File Offset: 0x0018EB15
		public string Guid
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06006C81 RID: 27777 RVA: 0x0019091D File Offset: 0x0018EB1D
		public string Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06006C82 RID: 27778 RVA: 0x00190925 File Offset: 0x0018EB25
		public string Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06006C83 RID: 27779 RVA: 0x0019092D File Offset: 0x0018EB2D
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_NoCanonicalView, new object[] { this._fullTypeName });
			}
		}

		// Token: 0x06006C84 RID: 27780 RVA: 0x00190948 File Offset: 0x0018EB48
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06006C85 RID: 27781 RVA: 0x00190950 File Offset: 0x0018EB50
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this == t2;
		}

		// Token: 0x04001AD3 RID: 6867
		private readonly AssemblySymbol _embeddingAssembly;

		// Token: 0x04001AD4 RID: 6868
		private readonly string _fullTypeName;

		// Token: 0x04001AD5 RID: 6869
		private readonly string _guid;

		// Token: 0x04001AD6 RID: 6870
		private readonly string _scope;

		// Token: 0x04001AD7 RID: 6871
		private readonly string _identifier;
	}
}
