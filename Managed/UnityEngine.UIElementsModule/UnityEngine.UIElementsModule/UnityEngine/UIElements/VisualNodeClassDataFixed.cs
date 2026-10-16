using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F8 RID: 1528
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeClassDataFixed
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000A2050 File Offset: 0x000A0250
		public unsafe int Count
		{
			get
			{
				fixed (int* ptr = &this.__Child0)
				{
					int* ptr2 = ptr;
					int i;
					for (i = 0; i < 8; i++)
					{
						bool flag = ptr2[i] == 0;
						if (flag)
						{
							return i;
						}
					}
					return i;
				}
			}
		}

		// Token: 0x17000B2D RID: 2861
		public unsafe int this[int index]
		{
			get
			{
				bool flag = index >= 8;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (int* ptr = &this.__Child0)
				{
					int* ptr2 = ptr;
					return ptr2[index];
				}
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000A20D4 File Offset: 0x000A02D4
		public unsafe int* GetUnsafePtr()
		{
			fixed (int* ptr = &this.__Child0)
			{
				return ptr;
			}
		}

		// Token: 0x04001487 RID: 5255
		private const int k_VisualNodeClassDataFixedCapacity = 8;

		// Token: 0x04001488 RID: 5256
		[FieldOffset(0)]
		private readonly int __Child0;

		// Token: 0x04001489 RID: 5257
		[FieldOffset(4)]
		private readonly int __Child1;

		// Token: 0x0400148A RID: 5258
		[FieldOffset(8)]
		private readonly int __Child2;

		// Token: 0x0400148B RID: 5259
		[FieldOffset(12)]
		private readonly int __Child3;

		// Token: 0x0400148C RID: 5260
		[FieldOffset(16)]
		private readonly int __Child4;

		// Token: 0x0400148D RID: 5261
		[FieldOffset(20)]
		private readonly int __Child5;

		// Token: 0x0400148E RID: 5262
		[FieldOffset(24)]
		private readonly int __Child6;

		// Token: 0x0400148F RID: 5263
		[FieldOffset(28)]
		private readonly int __Child7;
	}
}
