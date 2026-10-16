using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200012C RID: 300
	public enum StackBehaviour
	{
		// Token: 0x040006AE RID: 1710
		Pop0,
		// Token: 0x040006AF RID: 1711
		Pop1,
		// Token: 0x040006B0 RID: 1712
		Pop1_pop1,
		// Token: 0x040006B1 RID: 1713
		Popi,
		// Token: 0x040006B2 RID: 1714
		Popi_pop1,
		// Token: 0x040006B3 RID: 1715
		Popi_popi,
		// Token: 0x040006B4 RID: 1716
		Popi_popi8,
		// Token: 0x040006B5 RID: 1717
		Popi_popi_popi,
		// Token: 0x040006B6 RID: 1718
		Popi_popr4,
		// Token: 0x040006B7 RID: 1719
		Popi_popr8,
		// Token: 0x040006B8 RID: 1720
		Popref,
		// Token: 0x040006B9 RID: 1721
		Popref_pop1,
		// Token: 0x040006BA RID: 1722
		Popref_popi,
		// Token: 0x040006BB RID: 1723
		Popref_popi_popi,
		// Token: 0x040006BC RID: 1724
		Popref_popi_popi8,
		// Token: 0x040006BD RID: 1725
		Popref_popi_popr4,
		// Token: 0x040006BE RID: 1726
		Popref_popi_popr8,
		// Token: 0x040006BF RID: 1727
		Popref_popi_popref,
		// Token: 0x040006C0 RID: 1728
		PopAll,
		// Token: 0x040006C1 RID: 1729
		Push0,
		// Token: 0x040006C2 RID: 1730
		Push1,
		// Token: 0x040006C3 RID: 1731
		Push1_push1,
		// Token: 0x040006C4 RID: 1732
		Pushi,
		// Token: 0x040006C5 RID: 1733
		Pushi8,
		// Token: 0x040006C6 RID: 1734
		Pushr4,
		// Token: 0x040006C7 RID: 1735
		Pushr8,
		// Token: 0x040006C8 RID: 1736
		Pushref,
		// Token: 0x040006C9 RID: 1737
		Varpop,
		// Token: 0x040006CA RID: 1738
		Varpush
	}
}
