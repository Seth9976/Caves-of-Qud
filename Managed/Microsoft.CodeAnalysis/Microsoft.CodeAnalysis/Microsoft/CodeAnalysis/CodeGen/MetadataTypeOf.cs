using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D3 RID: 1235
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MetadataTypeOf : IMetadataExpression
	{
		// Token: 0x060031A1 RID: 12705 RVA: 0x0008DED5 File Offset: 0x0008C0D5
		public MetadataTypeOf(ITypeReference typeToGet, ITypeReference systemType)
		{
			this._typeToGet = typeToGet;
			this._systemType = systemType;
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x0008DEEB File Offset: 0x0008C0EB
		public ITypeReference TypeToGet
		{
			get
			{
				return this._typeToGet;
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0008DEF3 File Offset: 0x0008C0F3
		void IMetadataExpression.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x0008DEFC File Offset: 0x0008C0FC
		ITypeReference IMetadataExpression.Type
		{
			get
			{
				return this._systemType;
			}
		}

		// Token: 0x04001326 RID: 4902
		private readonly ITypeReference _typeToGet;

		// Token: 0x04001327 RID: 4903
		private readonly ITypeReference _systemType;
	}
}
