using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200019A RID: 410
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct IReferenceOrISignature : IEquatable<IReferenceOrISignature>
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x000301EC File Offset: 0x0002E3EC
		public IReferenceOrISignature(IReference item)
		{
			this._item = item;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000301F5 File Offset: 0x0002E3F5
		public IReferenceOrISignature(ISignature item)
		{
			this._item = item;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000301FE File Offset: 0x0002E3FE
		public IReferenceOrISignature(IMethodReference item)
		{
			this._item = item;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00030207 File Offset: 0x0002E407
		public bool Equals(IReferenceOrISignature other)
		{
			return this._item == other._item;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00030217 File Offset: 0x0002E417
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003021A File Offset: 0x0002E41A
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this._item);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00030227 File Offset: 0x0002E427
		public override string ToString()
		{
			return this._item.ToString() ?? "null";
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003023D File Offset: 0x0002E43D
		internal object AsObject()
		{
			return this._item;
		}

		// Token: 0x04000510 RID: 1296
		private readonly object _item;
	}
}
