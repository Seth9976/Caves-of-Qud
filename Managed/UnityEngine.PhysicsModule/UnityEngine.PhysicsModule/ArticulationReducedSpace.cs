using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationReducedSpace
	{
		// Token: 0x1700002C RID: 44
		public unsafe float this[int i]
		{
			get
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return *((ref this.x.FixedElementField) + (IntPtr)i * 4);
			}
			set
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				*((ref this.x.FixedElementField) + (IntPtr)i * 4) = value;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029A9 File Offset: 0x00000BA9
		public ArticulationReducedSpace(float a)
		{
			this.x.FixedElementField = a;
			this.dofCount = 1;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029C0 File Offset: 0x00000BC0
		public unsafe ArticulationReducedSpace(float a, float b)
		{
			this.x.FixedElementField = a;
			*((ref this.x.FixedElementField) + 4) = b;
			this.dofCount = 2;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029E6 File Offset: 0x00000BE6
		public unsafe ArticulationReducedSpace(float a, float b, float c)
		{
			this.x.FixedElementField = a;
			*((ref this.x.FixedElementField) + 4) = b;
			*((ref this.x.FixedElementField) + (IntPtr)2 * 4) = c;
			this.dofCount = 3;
		}

		// Token: 0x0400004B RID: 75
		[FixedBuffer(typeof(float), 3)]
		private ArticulationReducedSpace.<x>e__FixedBuffer x;

		// Token: 0x0400004C RID: 76
		public int dofCount;

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		public struct <x>e__FixedBuffer
		{
			// Token: 0x0400004D RID: 77
			public float FixedElementField;
		}
	}
}
