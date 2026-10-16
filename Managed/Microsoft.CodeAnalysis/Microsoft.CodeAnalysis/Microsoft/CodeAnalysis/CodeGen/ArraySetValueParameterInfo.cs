using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004BD RID: 1213
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ArraySetValueParameterInfo : ArrayMethodParameterInfo
	{
		// Token: 0x06003094 RID: 12436 RVA: 0x0008A701 File Offset: 0x00088901
		internal ArraySetValueParameterInfo(ushort index, IArrayTypeReference arrayType)
			: base(index)
		{
			this._arrayType = arrayType;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0008A711 File Offset: 0x00088911
		public override ITypeReference GetType(EmitContext context)
		{
			return this._arrayType.GetElementType(context);
		}

		// Token: 0x040012D6 RID: 4822
		private readonly IArrayTypeReference _arrayType;
	}
}
