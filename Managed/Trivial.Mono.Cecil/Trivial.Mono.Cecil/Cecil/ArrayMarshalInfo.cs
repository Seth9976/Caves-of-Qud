using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200000C RID: 12
	public sealed class ArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002BEA File Offset: 0x00000DEA
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002BF2 File Offset: 0x00000DF2
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002BFB File Offset: 0x00000DFB
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002C03 File Offset: 0x00000E03
		public int SizeParameterIndex
		{
			get
			{
				return this.size_parameter_index;
			}
			set
			{
				this.size_parameter_index = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002C14 File Offset: 0x00000E14
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002C1D File Offset: 0x00000E1D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002C25 File Offset: 0x00000E25
		public int SizeParameterMultiplier
		{
			get
			{
				return this.size_parameter_multiplier;
			}
			set
			{
				this.size_parameter_multiplier = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C2E File Offset: 0x00000E2E
		public ArrayMarshalInfo()
			: base(NativeType.Array)
		{
			this.element_type = NativeType.None;
			this.size_parameter_index = -1;
			this.size = -1;
			this.size_parameter_multiplier = -1;
		}

		// Token: 0x0400000C RID: 12
		internal NativeType element_type;

		// Token: 0x0400000D RID: 13
		internal int size_parameter_index;

		// Token: 0x0400000E RID: 14
		internal int size;

		// Token: 0x0400000F RID: 15
		internal int size_parameter_multiplier;
	}
}
