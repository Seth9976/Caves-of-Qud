using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F3 RID: 499
	internal enum SpecialMember
	{
		// Token: 0x04000633 RID: 1587
		System_String__CtorSZArrayChar,
		// Token: 0x04000634 RID: 1588
		System_String__ConcatStringString,
		// Token: 0x04000635 RID: 1589
		System_String__ConcatStringStringString,
		// Token: 0x04000636 RID: 1590
		System_String__ConcatStringStringStringString,
		// Token: 0x04000637 RID: 1591
		System_String__ConcatStringArray,
		// Token: 0x04000638 RID: 1592
		System_String__ConcatObject,
		// Token: 0x04000639 RID: 1593
		System_String__ConcatObjectObject,
		// Token: 0x0400063A RID: 1594
		System_String__ConcatObjectObjectObject,
		// Token: 0x0400063B RID: 1595
		System_String__ConcatObjectArray,
		// Token: 0x0400063C RID: 1596
		System_String__op_Equality,
		// Token: 0x0400063D RID: 1597
		System_String__op_Inequality,
		// Token: 0x0400063E RID: 1598
		System_String__Length,
		// Token: 0x0400063F RID: 1599
		System_String__Chars,
		// Token: 0x04000640 RID: 1600
		System_String__Format,
		// Token: 0x04000641 RID: 1601
		System_String__Substring,
		// Token: 0x04000642 RID: 1602
		System_Double__IsNaN,
		// Token: 0x04000643 RID: 1603
		System_Single__IsNaN,
		// Token: 0x04000644 RID: 1604
		System_Delegate__Combine,
		// Token: 0x04000645 RID: 1605
		System_Delegate__Remove,
		// Token: 0x04000646 RID: 1606
		System_Delegate__op_Equality,
		// Token: 0x04000647 RID: 1607
		System_Delegate__op_Inequality,
		// Token: 0x04000648 RID: 1608
		System_Decimal__Zero,
		// Token: 0x04000649 RID: 1609
		System_Decimal__MinusOne,
		// Token: 0x0400064A RID: 1610
		System_Decimal__One,
		// Token: 0x0400064B RID: 1611
		System_Decimal__CtorInt32,
		// Token: 0x0400064C RID: 1612
		System_Decimal__CtorUInt32,
		// Token: 0x0400064D RID: 1613
		System_Decimal__CtorInt64,
		// Token: 0x0400064E RID: 1614
		System_Decimal__CtorUInt64,
		// Token: 0x0400064F RID: 1615
		System_Decimal__CtorSingle,
		// Token: 0x04000650 RID: 1616
		System_Decimal__CtorDouble,
		// Token: 0x04000651 RID: 1617
		System_Decimal__CtorInt32Int32Int32BooleanByte,
		// Token: 0x04000652 RID: 1618
		System_Decimal__op_Addition,
		// Token: 0x04000653 RID: 1619
		System_Decimal__op_Subtraction,
		// Token: 0x04000654 RID: 1620
		System_Decimal__op_Multiply,
		// Token: 0x04000655 RID: 1621
		System_Decimal__op_Division,
		// Token: 0x04000656 RID: 1622
		System_Decimal__op_Modulus,
		// Token: 0x04000657 RID: 1623
		System_Decimal__op_UnaryNegation,
		// Token: 0x04000658 RID: 1624
		System_Decimal__op_Increment,
		// Token: 0x04000659 RID: 1625
		System_Decimal__op_Decrement,
		// Token: 0x0400065A RID: 1626
		System_Decimal__NegateDecimal,
		// Token: 0x0400065B RID: 1627
		System_Decimal__RemainderDecimalDecimal,
		// Token: 0x0400065C RID: 1628
		System_Decimal__AddDecimalDecimal,
		// Token: 0x0400065D RID: 1629
		System_Decimal__SubtractDecimalDecimal,
		// Token: 0x0400065E RID: 1630
		System_Decimal__MultiplyDecimalDecimal,
		// Token: 0x0400065F RID: 1631
		System_Decimal__DivideDecimalDecimal,
		// Token: 0x04000660 RID: 1632
		System_Decimal__ModuloDecimalDecimal,
		// Token: 0x04000661 RID: 1633
		System_Decimal__CompareDecimalDecimal,
		// Token: 0x04000662 RID: 1634
		System_Decimal__op_Equality,
		// Token: 0x04000663 RID: 1635
		System_Decimal__op_Inequality,
		// Token: 0x04000664 RID: 1636
		System_Decimal__op_GreaterThan,
		// Token: 0x04000665 RID: 1637
		System_Decimal__op_GreaterThanOrEqual,
		// Token: 0x04000666 RID: 1638
		System_Decimal__op_LessThan,
		// Token: 0x04000667 RID: 1639
		System_Decimal__op_LessThanOrEqual,
		// Token: 0x04000668 RID: 1640
		System_Decimal__op_Implicit_FromByte,
		// Token: 0x04000669 RID: 1641
		System_Decimal__op_Implicit_FromChar,
		// Token: 0x0400066A RID: 1642
		System_Decimal__op_Implicit_FromInt16,
		// Token: 0x0400066B RID: 1643
		System_Decimal__op_Implicit_FromInt32,
		// Token: 0x0400066C RID: 1644
		System_Decimal__op_Implicit_FromInt64,
		// Token: 0x0400066D RID: 1645
		System_Decimal__op_Implicit_FromSByte,
		// Token: 0x0400066E RID: 1646
		System_Decimal__op_Implicit_FromUInt16,
		// Token: 0x0400066F RID: 1647
		System_Decimal__op_Implicit_FromUInt32,
		// Token: 0x04000670 RID: 1648
		System_Decimal__op_Implicit_FromUInt64,
		// Token: 0x04000671 RID: 1649
		System_Decimal__op_Explicit_ToByte,
		// Token: 0x04000672 RID: 1650
		System_Decimal__op_Explicit_ToUInt16,
		// Token: 0x04000673 RID: 1651
		System_Decimal__op_Explicit_ToSByte,
		// Token: 0x04000674 RID: 1652
		System_Decimal__op_Explicit_ToInt16,
		// Token: 0x04000675 RID: 1653
		System_Decimal__op_Explicit_ToSingle,
		// Token: 0x04000676 RID: 1654
		System_Decimal__op_Explicit_ToDouble,
		// Token: 0x04000677 RID: 1655
		System_Decimal__op_Explicit_ToChar,
		// Token: 0x04000678 RID: 1656
		System_Decimal__op_Explicit_ToUInt64,
		// Token: 0x04000679 RID: 1657
		System_Decimal__op_Explicit_ToInt32,
		// Token: 0x0400067A RID: 1658
		System_Decimal__op_Explicit_ToUInt32,
		// Token: 0x0400067B RID: 1659
		System_Decimal__op_Explicit_ToInt64,
		// Token: 0x0400067C RID: 1660
		System_Decimal__op_Explicit_FromDouble,
		// Token: 0x0400067D RID: 1661
		System_Decimal__op_Explicit_FromSingle,
		// Token: 0x0400067E RID: 1662
		System_DateTime__MinValue,
		// Token: 0x0400067F RID: 1663
		System_DateTime__CtorInt64,
		// Token: 0x04000680 RID: 1664
		System_DateTime__CompareDateTimeDateTime,
		// Token: 0x04000681 RID: 1665
		System_DateTime__op_Equality,
		// Token: 0x04000682 RID: 1666
		System_DateTime__op_Inequality,
		// Token: 0x04000683 RID: 1667
		System_DateTime__op_GreaterThan,
		// Token: 0x04000684 RID: 1668
		System_DateTime__op_GreaterThanOrEqual,
		// Token: 0x04000685 RID: 1669
		System_DateTime__op_LessThan,
		// Token: 0x04000686 RID: 1670
		System_DateTime__op_LessThanOrEqual,
		// Token: 0x04000687 RID: 1671
		System_Collections_IEnumerable__GetEnumerator,
		// Token: 0x04000688 RID: 1672
		System_Collections_IEnumerator__Current,
		// Token: 0x04000689 RID: 1673
		System_Collections_IEnumerator__get_Current,
		// Token: 0x0400068A RID: 1674
		System_Collections_IEnumerator__MoveNext,
		// Token: 0x0400068B RID: 1675
		System_Collections_IEnumerator__Reset,
		// Token: 0x0400068C RID: 1676
		System_Collections_Generic_IEnumerable_T__GetEnumerator,
		// Token: 0x0400068D RID: 1677
		System_Collections_Generic_IEnumerator_T__Current,
		// Token: 0x0400068E RID: 1678
		System_Collections_Generic_IEnumerator_T__get_Current,
		// Token: 0x0400068F RID: 1679
		System_IDisposable__Dispose,
		// Token: 0x04000690 RID: 1680
		System_Array__Length,
		// Token: 0x04000691 RID: 1681
		System_Array__LongLength,
		// Token: 0x04000692 RID: 1682
		System_Array__GetLowerBound,
		// Token: 0x04000693 RID: 1683
		System_Array__GetUpperBound,
		// Token: 0x04000694 RID: 1684
		System_Object__GetHashCode,
		// Token: 0x04000695 RID: 1685
		System_Object__Equals,
		// Token: 0x04000696 RID: 1686
		System_Object__EqualsObjectObject,
		// Token: 0x04000697 RID: 1687
		System_Object__ToString,
		// Token: 0x04000698 RID: 1688
		System_Object__ReferenceEquals,
		// Token: 0x04000699 RID: 1689
		System_IntPtr__op_Explicit_ToPointer,
		// Token: 0x0400069A RID: 1690
		System_IntPtr__op_Explicit_ToInt32,
		// Token: 0x0400069B RID: 1691
		System_IntPtr__op_Explicit_ToInt64,
		// Token: 0x0400069C RID: 1692
		System_IntPtr__op_Explicit_FromPointer,
		// Token: 0x0400069D RID: 1693
		System_IntPtr__op_Explicit_FromInt32,
		// Token: 0x0400069E RID: 1694
		System_IntPtr__op_Explicit_FromInt64,
		// Token: 0x0400069F RID: 1695
		System_UIntPtr__op_Explicit_ToPointer,
		// Token: 0x040006A0 RID: 1696
		System_UIntPtr__op_Explicit_ToUInt32,
		// Token: 0x040006A1 RID: 1697
		System_UIntPtr__op_Explicit_ToUInt64,
		// Token: 0x040006A2 RID: 1698
		System_UIntPtr__op_Explicit_FromPointer,
		// Token: 0x040006A3 RID: 1699
		System_UIntPtr__op_Explicit_FromUInt32,
		// Token: 0x040006A4 RID: 1700
		System_UIntPtr__op_Explicit_FromUInt64,
		// Token: 0x040006A5 RID: 1701
		System_Nullable_T_GetValueOrDefault,
		// Token: 0x040006A6 RID: 1702
		System_Nullable_T_get_Value,
		// Token: 0x040006A7 RID: 1703
		System_Nullable_T_get_HasValue,
		// Token: 0x040006A8 RID: 1704
		System_Nullable_T__ctor,
		// Token: 0x040006A9 RID: 1705
		System_Nullable_T__op_Implicit_FromT,
		// Token: 0x040006AA RID: 1706
		System_Nullable_T__op_Explicit_ToT,
		// Token: 0x040006AB RID: 1707
		System_Runtime_CompilerServices_RuntimeFeature__DefaultImplementationsOfInterfaces,
		// Token: 0x040006AC RID: 1708
		System_Runtime_CompilerServices_RuntimeFeature__UnmanagedSignatureCallingConvention,
		// Token: 0x040006AD RID: 1709
		System_Runtime_CompilerServices_RuntimeFeature__CovariantReturnsOfClasses,
		// Token: 0x040006AE RID: 1710
		System_Runtime_CompilerServices_PreserveBaseOverridesAttribute__ctor,
		// Token: 0x040006AF RID: 1711
		Count
	}
}
