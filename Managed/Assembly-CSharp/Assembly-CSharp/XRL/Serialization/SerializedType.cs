using System;

namespace XRL.Serialization
{
	/// <summary>
	/// Stores information about a type or type/value.
	/// Internal use only.
	/// </summary>
	// Token: 0x02000572 RID: 1394
	internal enum SerializedType : byte
	{
		// Token: 0x04001D9B RID: 7579
		NullType = 128,
		// Token: 0x04001D9C RID: 7580
		NullSequenceType,
		// Token: 0x04001D9D RID: 7581
		DBNullType,
		// Token: 0x04001D9E RID: 7582
		DBNullSequenceType,
		// Token: 0x04001D9F RID: 7583
		OtherType,
		// Token: 0x04001DA0 RID: 7584
		BooleanTrueType,
		// Token: 0x04001DA1 RID: 7585
		BooleanFalseType,
		// Token: 0x04001DA2 RID: 7586
		ByteType,
		// Token: 0x04001DA3 RID: 7587
		SByteType,
		// Token: 0x04001DA4 RID: 7588
		CharType,
		// Token: 0x04001DA5 RID: 7589
		DecimalType,
		// Token: 0x04001DA6 RID: 7590
		DoubleType,
		// Token: 0x04001DA7 RID: 7591
		SingleType,
		// Token: 0x04001DA8 RID: 7592
		Int16Type,
		// Token: 0x04001DA9 RID: 7593
		Int32Type,
		// Token: 0x04001DAA RID: 7594
		Int64Type,
		// Token: 0x04001DAB RID: 7595
		UInt16Type,
		// Token: 0x04001DAC RID: 7596
		UInt32Type,
		// Token: 0x04001DAD RID: 7597
		UInt64Type,
		// Token: 0x04001DAE RID: 7598
		ZeroByteType,
		// Token: 0x04001DAF RID: 7599
		ZeroSByteType,
		// Token: 0x04001DB0 RID: 7600
		ZeroCharType,
		// Token: 0x04001DB1 RID: 7601
		ZeroDecimalType,
		// Token: 0x04001DB2 RID: 7602
		ZeroDoubleType,
		// Token: 0x04001DB3 RID: 7603
		ZeroSingleType,
		// Token: 0x04001DB4 RID: 7604
		ZeroInt16Type,
		// Token: 0x04001DB5 RID: 7605
		ZeroInt32Type,
		// Token: 0x04001DB6 RID: 7606
		ZeroInt64Type,
		// Token: 0x04001DB7 RID: 7607
		ZeroUInt16Type,
		// Token: 0x04001DB8 RID: 7608
		ZeroUInt32Type,
		// Token: 0x04001DB9 RID: 7609
		ZeroUInt64Type,
		// Token: 0x04001DBA RID: 7610
		OneByteType,
		// Token: 0x04001DBB RID: 7611
		OneSByteType,
		// Token: 0x04001DBC RID: 7612
		OneCharType,
		// Token: 0x04001DBD RID: 7613
		OneDecimalType,
		// Token: 0x04001DBE RID: 7614
		OneDoubleType,
		// Token: 0x04001DBF RID: 7615
		OneSingleType,
		// Token: 0x04001DC0 RID: 7616
		OneInt16Type,
		// Token: 0x04001DC1 RID: 7617
		OneInt32Type,
		// Token: 0x04001DC2 RID: 7618
		OneInt64Type,
		// Token: 0x04001DC3 RID: 7619
		OneUInt16Type,
		// Token: 0x04001DC4 RID: 7620
		OneUInt32Type,
		// Token: 0x04001DC5 RID: 7621
		OneUInt64Type,
		// Token: 0x04001DC6 RID: 7622
		MinusOneInt16Type,
		// Token: 0x04001DC7 RID: 7623
		MinusOneInt32Type,
		// Token: 0x04001DC8 RID: 7624
		MinusOneInt64Type,
		// Token: 0x04001DC9 RID: 7625
		OptimizedInt16Type,
		// Token: 0x04001DCA RID: 7626
		OptimizedInt16NegativeType,
		// Token: 0x04001DCB RID: 7627
		OptimizedUInt16Type,
		// Token: 0x04001DCC RID: 7628
		OptimizedInt32Type,
		// Token: 0x04001DCD RID: 7629
		OptimizedInt32NegativeType,
		// Token: 0x04001DCE RID: 7630
		OptimizedUInt32Type,
		// Token: 0x04001DCF RID: 7631
		OptimizedInt64Type,
		// Token: 0x04001DD0 RID: 7632
		OptimizedInt64NegativeType,
		// Token: 0x04001DD1 RID: 7633
		OptimizedUInt64Type,
		// Token: 0x04001DD2 RID: 7634
		OptimizedDateTimeType,
		// Token: 0x04001DD3 RID: 7635
		OptimizedTimeSpanType,
		// Token: 0x04001DD4 RID: 7636
		EmptyStringType,
		// Token: 0x04001DD5 RID: 7637
		SingleSpaceType,
		// Token: 0x04001DD6 RID: 7638
		SingleCharStringType,
		// Token: 0x04001DD7 RID: 7639
		YStringType,
		// Token: 0x04001DD8 RID: 7640
		NStringType,
		// Token: 0x04001DD9 RID: 7641
		DateTimeType,
		// Token: 0x04001DDA RID: 7642
		MinDateTimeType,
		// Token: 0x04001DDB RID: 7643
		MaxDateTimeType,
		// Token: 0x04001DDC RID: 7644
		TimeSpanType,
		// Token: 0x04001DDD RID: 7645
		ZeroTimeSpanType,
		// Token: 0x04001DDE RID: 7646
		GuidType,
		// Token: 0x04001DDF RID: 7647
		EmptyGuidType,
		// Token: 0x04001DE0 RID: 7648
		BitVector32Type,
		// Token: 0x04001DE1 RID: 7649
		DuplicateValueType,
		// Token: 0x04001DE2 RID: 7650
		DuplicateValueSequenceType,
		// Token: 0x04001DE3 RID: 7651
		BitArrayType,
		// Token: 0x04001DE4 RID: 7652
		TypeType,
		// Token: 0x04001DE5 RID: 7653
		SingleInstanceType,
		// Token: 0x04001DE6 RID: 7654
		ArrayListType,
		// Token: 0x04001DE7 RID: 7655
		ObjectArrayType,
		// Token: 0x04001DE8 RID: 7656
		EmptyTypedArrayType,
		// Token: 0x04001DE9 RID: 7657
		EmptyObjectArrayType,
		// Token: 0x04001DEA RID: 7658
		NonOptimizedTypedArrayType,
		// Token: 0x04001DEB RID: 7659
		FullyOptimizedTypedArrayType,
		// Token: 0x04001DEC RID: 7660
		PartiallyOptimizedTypedArrayType,
		// Token: 0x04001DED RID: 7661
		OtherTypedArrayType,
		// Token: 0x04001DEE RID: 7662
		BooleanArrayType,
		// Token: 0x04001DEF RID: 7663
		ByteArrayType,
		// Token: 0x04001DF0 RID: 7664
		CharArrayType,
		// Token: 0x04001DF1 RID: 7665
		DateTimeArrayType,
		// Token: 0x04001DF2 RID: 7666
		DecimalArrayType,
		// Token: 0x04001DF3 RID: 7667
		DoubleArrayType,
		// Token: 0x04001DF4 RID: 7668
		SingleArrayType,
		// Token: 0x04001DF5 RID: 7669
		GuidArrayType,
		// Token: 0x04001DF6 RID: 7670
		Int16ArrayType,
		// Token: 0x04001DF7 RID: 7671
		Int32ArrayType,
		// Token: 0x04001DF8 RID: 7672
		Int64ArrayType,
		// Token: 0x04001DF9 RID: 7673
		SByteArrayType,
		// Token: 0x04001DFA RID: 7674
		TimeSpanArrayType,
		// Token: 0x04001DFB RID: 7675
		UInt16ArrayType,
		// Token: 0x04001DFC RID: 7676
		UInt32ArrayType,
		// Token: 0x04001DFD RID: 7677
		UInt64ArrayType,
		// Token: 0x04001DFE RID: 7678
		StringArrayType,
		// Token: 0x04001DFF RID: 7679
		OwnedDataSerializableAndRecreatableType,
		// Token: 0x04001E00 RID: 7680
		EnumType,
		// Token: 0x04001E01 RID: 7681
		OptimizedEnumType,
		// Token: 0x04001E02 RID: 7682
		SurrogateHandledType,
		// Token: 0x04001E03 RID: 7683
		StringListType,
		// Token: 0x04001E04 RID: 7684
		GameObjectType,
		// Token: 0x04001E05 RID: 7685
		IDictionaryType,
		// Token: 0x04001E06 RID: 7686
		IListType,
		// Token: 0x04001E07 RID: 7687
		ICompositeType,
		// Token: 0x04001E08 RID: 7688
		ICompositeFieldType,
		// Token: 0x04001E09 RID: 7689
		Reserved18,
		// Token: 0x04001E0A RID: 7690
		Reserved17,
		// Token: 0x04001E0B RID: 7691
		Reserved16,
		// Token: 0x04001E0C RID: 7692
		Reserved15,
		// Token: 0x04001E0D RID: 7693
		Reserved14,
		// Token: 0x04001E0E RID: 7694
		Reserved13,
		// Token: 0x04001E0F RID: 7695
		Reserved12,
		// Token: 0x04001E10 RID: 7696
		Reserved11,
		// Token: 0x04001E11 RID: 7697
		Reserved10,
		// Token: 0x04001E12 RID: 7698
		Reserved9,
		// Token: 0x04001E13 RID: 7699
		Reserved8,
		// Token: 0x04001E14 RID: 7700
		Reserved7,
		// Token: 0x04001E15 RID: 7701
		Reserved6,
		// Token: 0x04001E16 RID: 7702
		Reserved5,
		// Token: 0x04001E17 RID: 7703
		Reserved4,
		// Token: 0x04001E18 RID: 7704
		Reserved3,
		// Token: 0x04001E19 RID: 7705
		Reserved2,
		// Token: 0x04001E1A RID: 7706
		Reserved1
	}
}
