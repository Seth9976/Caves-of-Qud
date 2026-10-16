using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000504 RID: 1284
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NoPiaIllegalGenericInstantiationSymbol : ErrorTypeSymbol
	{
		// Token: 0x06006C74 RID: 27764 RVA: 0x00190813 File Offset: 0x0018EA13
		public NoPiaIllegalGenericInstantiationSymbol(ModuleSymbol exposingModule, NamedTypeSymbol underlyingSymbol)
			: base(null)
		{
			this._exposingModule = exposingModule;
			this._underlyingSymbol = underlyingSymbol;
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x0019082A File Offset: 0x0018EA2A
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new NoPiaIllegalGenericInstantiationSymbol(this._exposingModule, this._underlyingSymbol);
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x0019083D File Offset: 0x0018EA3D
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06006C77 RID: 27767 RVA: 0x00190840 File Offset: 0x0018EA40
		public NamedTypeSymbol UnderlyingSymbol
		{
			get
			{
				return this._underlyingSymbol;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x06006C78 RID: 27768 RVA: 0x00190848 File Offset: 0x0018EA48
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				if (this._underlyingSymbol.IsErrorType())
				{
					DiagnosticInfo errorInfo = ((ErrorTypeSymbol)this._underlyingSymbol).ErrorInfo;
					if (errorInfo != null)
					{
						return errorInfo;
					}
				}
				return new CSDiagnosticInfo(ErrorCode.ERR_GenericsUsedAcrossAssemblies, new object[]
				{
					this._underlyingSymbol,
					this._exposingModule.ContainingAssembly
				});
			}
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x0019089F File Offset: 0x0018EA9F
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x001908A7 File Offset: 0x0018EAA7
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this == t2;
		}

		// Token: 0x04001AD1 RID: 6865
		private readonly ModuleSymbol _exposingModule;

		// Token: 0x04001AD2 RID: 6866
		private readonly NamedTypeSymbol _underlyingSymbol;
	}
}
