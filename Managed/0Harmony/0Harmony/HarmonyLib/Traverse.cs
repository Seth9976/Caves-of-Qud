using System;

namespace HarmonyLib
{
	// Token: 0x02000198 RID: 408
	public class Traverse<T>
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00002AED File Offset: 0x00000CED
		private Traverse()
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000172F0 File Offset: 0x000154F0
		public Traverse(Traverse traverse)
		{
			this.traverse = traverse;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000172FF File Offset: 0x000154FF
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001730C File Offset: 0x0001550C
		public T Value
		{
			get
			{
				return this.traverse.GetValue<T>();
			}
			set
			{
				this.traverse.SetValue(value);
			}
		}

		// Token: 0x04000218 RID: 536
		private readonly Traverse traverse;
	}
}
