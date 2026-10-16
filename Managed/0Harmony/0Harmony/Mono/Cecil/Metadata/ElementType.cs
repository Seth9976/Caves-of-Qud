using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A1 RID: 673
	internal enum ElementType : byte
	{
		// Token: 0x0400065E RID: 1630
		None,
		// Token: 0x0400065F RID: 1631
		Void,
		// Token: 0x04000660 RID: 1632
		Boolean,
		// Token: 0x04000661 RID: 1633
		Char,
		// Token: 0x04000662 RID: 1634
		I1,
		// Token: 0x04000663 RID: 1635
		U1,
		// Token: 0x04000664 RID: 1636
		I2,
		// Token: 0x04000665 RID: 1637
		U2,
		// Token: 0x04000666 RID: 1638
		I4,
		// Token: 0x04000667 RID: 1639
		U4,
		// Token: 0x04000668 RID: 1640
		I8,
		// Token: 0x04000669 RID: 1641
		U8,
		// Token: 0x0400066A RID: 1642
		R4,
		// Token: 0x0400066B RID: 1643
		R8,
		// Token: 0x0400066C RID: 1644
		String,
		// Token: 0x0400066D RID: 1645
		Ptr,
		// Token: 0x0400066E RID: 1646
		ByRef,
		// Token: 0x0400066F RID: 1647
		ValueType,
		// Token: 0x04000670 RID: 1648
		Class,
		// Token: 0x04000671 RID: 1649
		Var,
		// Token: 0x04000672 RID: 1650
		Array,
		// Token: 0x04000673 RID: 1651
		GenericInst,
		// Token: 0x04000674 RID: 1652
		TypedByRef,
		// Token: 0x04000675 RID: 1653
		I = 24,
		// Token: 0x04000676 RID: 1654
		U,
		// Token: 0x04000677 RID: 1655
		FnPtr = 27,
		// Token: 0x04000678 RID: 1656
		Object,
		// Token: 0x04000679 RID: 1657
		SzArray,
		// Token: 0x0400067A RID: 1658
		MVar,
		// Token: 0x0400067B RID: 1659
		CModReqD,
		// Token: 0x0400067C RID: 1660
		CModOpt,
		// Token: 0x0400067D RID: 1661
		Internal,
		// Token: 0x0400067E RID: 1662
		Modifier = 64,
		// Token: 0x0400067F RID: 1663
		Sentinel,
		// Token: 0x04000680 RID: 1664
		Pinned = 69,
		// Token: 0x04000681 RID: 1665
		Type = 80,
		// Token: 0x04000682 RID: 1666
		Boxed,
		// Token: 0x04000683 RID: 1667
		Enum = 85
	}
}
