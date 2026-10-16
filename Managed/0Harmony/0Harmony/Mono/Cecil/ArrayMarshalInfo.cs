using System;

namespace Mono.Cecil
{
	// Token: 0x02000229 RID: 553
	internal sealed class ArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000297AF File Offset: 0x000279AF
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x000297B7 File Offset: 0x000279B7
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000297C0 File Offset: 0x000279C0
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x000297C8 File Offset: 0x000279C8
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000297D1 File Offset: 0x000279D1
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x000297D9 File Offset: 0x000279D9
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000297E2 File Offset: 0x000279E2
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x000297EA File Offset: 0x000279EA
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

		// Token: 0x06000BFF RID: 3071 RVA: 0x000297F3 File Offset: 0x000279F3
		public ArrayMarshalInfo()
			: base(NativeType.Array)
		{
			this.element_type = NativeType.None;
			this.size_parameter_index = -1;
			this.size = -1;
			this.size_parameter_multiplier = -1;
		}

		// Token: 0x0400035C RID: 860
		internal NativeType element_type;

		// Token: 0x0400035D RID: 861
		internal int size_parameter_index;

		// Token: 0x0400035E RID: 862
		internal int size;

		// Token: 0x0400035F RID: 863
		internal int size_parameter_multiplier;
	}
}
