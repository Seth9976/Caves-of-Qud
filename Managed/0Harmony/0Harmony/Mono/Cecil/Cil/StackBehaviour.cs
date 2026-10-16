using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002C4 RID: 708
	public enum StackBehaviour
	{
		// Token: 0x04000831 RID: 2097
		Pop0,
		// Token: 0x04000832 RID: 2098
		Pop1,
		// Token: 0x04000833 RID: 2099
		Pop1_pop1,
		// Token: 0x04000834 RID: 2100
		Popi,
		// Token: 0x04000835 RID: 2101
		Popi_pop1,
		// Token: 0x04000836 RID: 2102
		Popi_popi,
		// Token: 0x04000837 RID: 2103
		Popi_popi8,
		// Token: 0x04000838 RID: 2104
		Popi_popi_popi,
		// Token: 0x04000839 RID: 2105
		Popi_popr4,
		// Token: 0x0400083A RID: 2106
		Popi_popr8,
		// Token: 0x0400083B RID: 2107
		Popref,
		// Token: 0x0400083C RID: 2108
		Popref_pop1,
		// Token: 0x0400083D RID: 2109
		Popref_popi,
		// Token: 0x0400083E RID: 2110
		Popref_popi_popi,
		// Token: 0x0400083F RID: 2111
		Popref_popi_popi8,
		// Token: 0x04000840 RID: 2112
		Popref_popi_popr4,
		// Token: 0x04000841 RID: 2113
		Popref_popi_popr8,
		// Token: 0x04000842 RID: 2114
		Popref_popi_popref,
		// Token: 0x04000843 RID: 2115
		PopAll,
		// Token: 0x04000844 RID: 2116
		Push0,
		// Token: 0x04000845 RID: 2117
		Push1,
		// Token: 0x04000846 RID: 2118
		Push1_push1,
		// Token: 0x04000847 RID: 2119
		Pushi,
		// Token: 0x04000848 RID: 2120
		Pushi8,
		// Token: 0x04000849 RID: 2121
		Pushr4,
		// Token: 0x0400084A RID: 2122
		Pushr8,
		// Token: 0x0400084B RID: 2123
		Pushref,
		// Token: 0x0400084C RID: 2124
		Varpop,
		// Token: 0x0400084D RID: 2125
		Varpush
	}
}
