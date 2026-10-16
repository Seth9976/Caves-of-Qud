using System;
using System.Collections.Generic;

namespace MonoMod.Utils
{
	// Token: 0x0200045C RID: 1116
	internal sealed class WeakReferenceComparer : EqualityComparer<WeakReference>
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x000535CD File Offset: 0x000517CD
		public override bool Equals(WeakReference x, WeakReference y)
		{
			return x.SafeGetTarget() == y.SafeGetTarget() && x.SafeGetIsAlive() == y.SafeGetIsAlive();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000535ED File Offset: 0x000517ED
		public override int GetHashCode(WeakReference obj)
		{
			object obj2 = obj.SafeGetTarget();
			if (obj2 == null)
			{
				return 0;
			}
			return obj2.GetHashCode();
		}
	}
}
