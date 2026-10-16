using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000483 RID: 1155
	internal struct MethodImplKey : IEquatable<MethodImplKey>
	{
		// Token: 0x06002D87 RID: 11655 RVA: 0x000810B8 File Offset: 0x0007F2B8
		internal MethodImplKey(int implementingMethod, int index)
		{
			this.ImplementingMethod = implementingMethod;
			this.Index = index;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000810C8 File Offset: 0x0007F2C8
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MethodImplKey && this.Equals((MethodImplKey)obj);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000810E0 File Offset: 0x0007F2E0
		public bool Equals(MethodImplKey other)
		{
			return this.ImplementingMethod == other.ImplementingMethod && this.Index == other.Index;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00081100 File Offset: 0x0007F300
		public override int GetHashCode()
		{
			return Hash.Combine(this.ImplementingMethod, this.Index);
		}

		// Token: 0x04001207 RID: 4615
		internal readonly int ImplementingMethod;

		// Token: 0x04001208 RID: 4616
		internal readonly int Index;
	}
}
