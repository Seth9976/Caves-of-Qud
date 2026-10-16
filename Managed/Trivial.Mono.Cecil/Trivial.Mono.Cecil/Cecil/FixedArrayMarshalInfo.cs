using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200003E RID: 62
	public sealed class FixedArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005E13 File Offset: 0x00004013
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00005E1B File Offset: 0x0000401B
		public NativeType ElementType
		{
			get
			{
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00005E2C File Offset: 0x0000402C
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005E35 File Offset: 0x00004035
		public FixedArrayMarshalInfo()
			: base(NativeType.FixedArray)
		{
			this.element_type = NativeType.None;
		}

		// Token: 0x04000087 RID: 135
		internal NativeType element_type;

		// Token: 0x04000088 RID: 136
		internal int size;
	}
}
