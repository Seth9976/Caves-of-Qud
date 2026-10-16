using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E5 RID: 1253
	internal sealed class SynthesizedLocalOrdinalsDispenser
	{
		// Token: 0x0600327E RID: 12926 RVA: 0x0008FACC File Offset: 0x0008DCCC
		private static long MakeKey(SynthesizedLocalKind localKind, int syntaxOffset)
		{
			return ((long)syntaxOffset << 8) | (long)localKind;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x0008FAD5 File Offset: 0x0008DCD5
		public void Free()
		{
			if (this._lazyMap != null)
			{
				this._lazyMap.Free();
				this._lazyMap = null;
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x0008FAF4 File Offset: 0x0008DCF4
		public int AssignLocalOrdinal(SynthesizedLocalKind localKind, int syntaxOffset)
		{
			if (localKind == SynthesizedLocalKind.UserDefined)
			{
				return 0;
			}
			long num = SynthesizedLocalOrdinalsDispenser.MakeKey(localKind, syntaxOffset);
			int num2;
			if (this._lazyMap == null)
			{
				this._lazyMap = PooledDictionary<long, int>.GetInstance();
				num2 = 0;
			}
			else if (!this._lazyMap.TryGetValue(num, ref num2))
			{
				num2 = 0;
			}
			this._lazyMap[num] = num2 + 1;
			return num2;
		}

		// Token: 0x0400137B RID: 4987
		[Nullable(2)]
		private PooledDictionary<long, int> _lazyMap;
	}
}
