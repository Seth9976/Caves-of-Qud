using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F4 RID: 1524
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeChildrenFixed
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000A1E24 File Offset: 0x000A0024
		public unsafe int Count
		{
			get
			{
				fixed (VisualNodeHandle* ptr = &this.__Child0)
				{
					VisualNodeHandle* ptr2 = ptr;
					int i;
					for (i = 0; i < 4; i++)
					{
						bool flag = ptr2[i].Id == 0;
						if (flag)
						{
							return i;
						}
					}
					return i;
				}
			}
		}

		// Token: 0x17000B24 RID: 2852
		public unsafe VisualNodeHandle this[int index]
		{
			get
			{
				bool flag = index >= 4;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (VisualNodeHandle* ptr = &this.__Child0)
				{
					VisualNodeHandle* ptr2 = ptr;
					return ptr2[index];
				}
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000A1EB8 File Offset: 0x000A00B8
		public unsafe VisualNodeHandle* GetUnsafePtr()
		{
			fixed (VisualNodeHandle* ptr = &this.__Child0)
			{
				return ptr;
			}
		}

		// Token: 0x04001478 RID: 5240
		private const int k_VisualNodeChildrenFixedCapacity = 4;

		// Token: 0x04001479 RID: 5241
		[FieldOffset(0)]
		private readonly VisualNodeHandle __Child0;

		// Token: 0x0400147A RID: 5242
		[FieldOffset(8)]
		private readonly VisualNodeHandle __Child1;

		// Token: 0x0400147B RID: 5243
		[FieldOffset(16)]
		private readonly VisualNodeHandle __Child2;

		// Token: 0x0400147C RID: 5244
		[FieldOffset(24)]
		private readonly VisualNodeHandle __Child3;
	}
}
