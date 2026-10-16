using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005B RID: 91
	[Flags]
	internal enum BinaryOperatorKind
	{
		// Token: 0x04000247 RID: 583
		TypeMask = 255,
		// Token: 0x04000248 RID: 584
		Int = 5,
		// Token: 0x04000249 RID: 585
		UInt = 6,
		// Token: 0x0400024A RID: 586
		Long = 7,
		// Token: 0x0400024B RID: 587
		ULong = 8,
		// Token: 0x0400024C RID: 588
		NInt = 9,
		// Token: 0x0400024D RID: 589
		NUInt = 10,
		// Token: 0x0400024E RID: 590
		Char = 11,
		// Token: 0x0400024F RID: 591
		Float = 12,
		// Token: 0x04000250 RID: 592
		Double = 13,
		// Token: 0x04000251 RID: 593
		Decimal = 14,
		// Token: 0x04000252 RID: 594
		Bool = 15,
		// Token: 0x04000253 RID: 595
		Object = 16,
		// Token: 0x04000254 RID: 596
		String = 17,
		// Token: 0x04000255 RID: 597
		StringAndObject = 18,
		// Token: 0x04000256 RID: 598
		ObjectAndString = 19,
		// Token: 0x04000257 RID: 599
		Enum = 20,
		// Token: 0x04000258 RID: 600
		EnumAndUnderlying = 21,
		// Token: 0x04000259 RID: 601
		UnderlyingAndEnum = 22,
		// Token: 0x0400025A RID: 602
		Delegate = 23,
		// Token: 0x0400025B RID: 603
		Pointer = 24,
		// Token: 0x0400025C RID: 604
		PointerAndInt = 25,
		// Token: 0x0400025D RID: 605
		PointerAndUInt = 32,
		// Token: 0x0400025E RID: 606
		PointerAndLong = 33,
		// Token: 0x0400025F RID: 607
		PointerAndULong = 34,
		// Token: 0x04000260 RID: 608
		IntAndPointer = 35,
		// Token: 0x04000261 RID: 609
		UIntAndPointer = 36,
		// Token: 0x04000262 RID: 610
		LongAndPointer = 37,
		// Token: 0x04000263 RID: 611
		ULongAndPointer = 38,
		// Token: 0x04000264 RID: 612
		NullableNull = 39,
		// Token: 0x04000265 RID: 613
		UserDefined = 40,
		// Token: 0x04000266 RID: 614
		Dynamic = 41,
		// Token: 0x04000267 RID: 615
		OpMask = 65280,
		// Token: 0x04000268 RID: 616
		Multiplication = 4096,
		// Token: 0x04000269 RID: 617
		Addition = 4352,
		// Token: 0x0400026A RID: 618
		Subtraction = 4608,
		// Token: 0x0400026B RID: 619
		Division = 4864,
		// Token: 0x0400026C RID: 620
		Remainder = 5120,
		// Token: 0x0400026D RID: 621
		LeftShift = 5376,
		// Token: 0x0400026E RID: 622
		RightShift = 5632,
		// Token: 0x0400026F RID: 623
		Equal = 5888,
		// Token: 0x04000270 RID: 624
		NotEqual = 6144,
		// Token: 0x04000271 RID: 625
		GreaterThan = 6400,
		// Token: 0x04000272 RID: 626
		LessThan = 6656,
		// Token: 0x04000273 RID: 627
		GreaterThanOrEqual = 6912,
		// Token: 0x04000274 RID: 628
		LessThanOrEqual = 7168,
		// Token: 0x04000275 RID: 629
		And = 7424,
		// Token: 0x04000276 RID: 630
		Xor = 7680,
		// Token: 0x04000277 RID: 631
		Or = 7936,
		// Token: 0x04000278 RID: 632
		Lifted = 65536,
		// Token: 0x04000279 RID: 633
		Logical = 131072,
		// Token: 0x0400027A RID: 634
		Checked = 262144,
		// Token: 0x0400027B RID: 635
		Error = 0,
		// Token: 0x0400027C RID: 636
		IntMultiplication = 4101,
		// Token: 0x0400027D RID: 637
		UIntMultiplication = 4102,
		// Token: 0x0400027E RID: 638
		LongMultiplication = 4103,
		// Token: 0x0400027F RID: 639
		ULongMultiplication = 4104,
		// Token: 0x04000280 RID: 640
		NIntMultiplication = 4105,
		// Token: 0x04000281 RID: 641
		NUIntMultiplication = 4106,
		// Token: 0x04000282 RID: 642
		FloatMultiplication = 4108,
		// Token: 0x04000283 RID: 643
		DoubleMultiplication = 4109,
		// Token: 0x04000284 RID: 644
		DecimalMultiplication = 4110,
		// Token: 0x04000285 RID: 645
		UserDefinedMultiplication = 4136,
		// Token: 0x04000286 RID: 646
		LiftedIntMultiplication = 69637,
		// Token: 0x04000287 RID: 647
		LiftedUIntMultiplication = 69638,
		// Token: 0x04000288 RID: 648
		LiftedLongMultiplication = 69639,
		// Token: 0x04000289 RID: 649
		LiftedULongMultiplication = 69640,
		// Token: 0x0400028A RID: 650
		LiftedNIntMultiplication = 69641,
		// Token: 0x0400028B RID: 651
		LiftedNUIntMultiplication = 69642,
		// Token: 0x0400028C RID: 652
		LiftedFloatMultiplication = 69644,
		// Token: 0x0400028D RID: 653
		LiftedDoubleMultiplication = 69645,
		// Token: 0x0400028E RID: 654
		LiftedDecimalMultiplication = 69646,
		// Token: 0x0400028F RID: 655
		LiftedUserDefinedMultiplication = 69672,
		// Token: 0x04000290 RID: 656
		DynamicMultiplication = 4137,
		// Token: 0x04000291 RID: 657
		IntDivision = 4869,
		// Token: 0x04000292 RID: 658
		UIntDivision = 4870,
		// Token: 0x04000293 RID: 659
		LongDivision = 4871,
		// Token: 0x04000294 RID: 660
		ULongDivision = 4872,
		// Token: 0x04000295 RID: 661
		NIntDivision = 4873,
		// Token: 0x04000296 RID: 662
		NUIntDivision = 4874,
		// Token: 0x04000297 RID: 663
		FloatDivision = 4876,
		// Token: 0x04000298 RID: 664
		DoubleDivision = 4877,
		// Token: 0x04000299 RID: 665
		DecimalDivision = 4878,
		// Token: 0x0400029A RID: 666
		UserDefinedDivision = 4904,
		// Token: 0x0400029B RID: 667
		LiftedIntDivision = 70405,
		// Token: 0x0400029C RID: 668
		LiftedUIntDivision = 70406,
		// Token: 0x0400029D RID: 669
		LiftedLongDivision = 70407,
		// Token: 0x0400029E RID: 670
		LiftedULongDivision = 70408,
		// Token: 0x0400029F RID: 671
		LiftedNIntDivision = 70409,
		// Token: 0x040002A0 RID: 672
		LiftedNUIntDivision = 70410,
		// Token: 0x040002A1 RID: 673
		LiftedFloatDivision = 70412,
		// Token: 0x040002A2 RID: 674
		LiftedDoubleDivision = 70413,
		// Token: 0x040002A3 RID: 675
		LiftedDecimalDivision = 70414,
		// Token: 0x040002A4 RID: 676
		LiftedUserDefinedDivision = 70440,
		// Token: 0x040002A5 RID: 677
		DynamicDivision = 4905,
		// Token: 0x040002A6 RID: 678
		IntRemainder = 5125,
		// Token: 0x040002A7 RID: 679
		UIntRemainder = 5126,
		// Token: 0x040002A8 RID: 680
		LongRemainder = 5127,
		// Token: 0x040002A9 RID: 681
		ULongRemainder = 5128,
		// Token: 0x040002AA RID: 682
		NIntRemainder = 5129,
		// Token: 0x040002AB RID: 683
		NUIntRemainder = 5130,
		// Token: 0x040002AC RID: 684
		FloatRemainder = 5132,
		// Token: 0x040002AD RID: 685
		DoubleRemainder = 5133,
		// Token: 0x040002AE RID: 686
		DecimalRemainder = 5134,
		// Token: 0x040002AF RID: 687
		UserDefinedRemainder = 5160,
		// Token: 0x040002B0 RID: 688
		LiftedIntRemainder = 70661,
		// Token: 0x040002B1 RID: 689
		LiftedUIntRemainder = 70662,
		// Token: 0x040002B2 RID: 690
		LiftedLongRemainder = 70663,
		// Token: 0x040002B3 RID: 691
		LiftedULongRemainder = 70664,
		// Token: 0x040002B4 RID: 692
		LiftedNIntRemainder = 70665,
		// Token: 0x040002B5 RID: 693
		LiftedNUIntRemainder = 70666,
		// Token: 0x040002B6 RID: 694
		LiftedFloatRemainder = 70668,
		// Token: 0x040002B7 RID: 695
		LiftedDoubleRemainder = 70669,
		// Token: 0x040002B8 RID: 696
		LiftedDecimalRemainder = 70670,
		// Token: 0x040002B9 RID: 697
		LiftedUserDefinedRemainder = 70696,
		// Token: 0x040002BA RID: 698
		DynamicRemainder = 5161,
		// Token: 0x040002BB RID: 699
		IntAddition = 4357,
		// Token: 0x040002BC RID: 700
		UIntAddition = 4358,
		// Token: 0x040002BD RID: 701
		LongAddition = 4359,
		// Token: 0x040002BE RID: 702
		ULongAddition = 4360,
		// Token: 0x040002BF RID: 703
		NIntAddition = 4361,
		// Token: 0x040002C0 RID: 704
		NUIntAddition = 4362,
		// Token: 0x040002C1 RID: 705
		FloatAddition = 4364,
		// Token: 0x040002C2 RID: 706
		DoubleAddition = 4365,
		// Token: 0x040002C3 RID: 707
		DecimalAddition = 4366,
		// Token: 0x040002C4 RID: 708
		EnumAndUnderlyingAddition = 4373,
		// Token: 0x040002C5 RID: 709
		UnderlyingAndEnumAddition = 4374,
		// Token: 0x040002C6 RID: 710
		UserDefinedAddition = 4392,
		// Token: 0x040002C7 RID: 711
		LiftedIntAddition = 69893,
		// Token: 0x040002C8 RID: 712
		LiftedUIntAddition = 69894,
		// Token: 0x040002C9 RID: 713
		LiftedLongAddition = 69895,
		// Token: 0x040002CA RID: 714
		LiftedULongAddition = 69896,
		// Token: 0x040002CB RID: 715
		LiftedNIntAddition = 69897,
		// Token: 0x040002CC RID: 716
		LiftedNUIntAddition = 69898,
		// Token: 0x040002CD RID: 717
		LiftedFloatAddition = 69900,
		// Token: 0x040002CE RID: 718
		LiftedDoubleAddition = 69901,
		// Token: 0x040002CF RID: 719
		LiftedDecimalAddition = 69902,
		// Token: 0x040002D0 RID: 720
		LiftedEnumAndUnderlyingAddition = 69909,
		// Token: 0x040002D1 RID: 721
		LiftedUnderlyingAndEnumAddition = 69910,
		// Token: 0x040002D2 RID: 722
		LiftedUserDefinedAddition = 69928,
		// Token: 0x040002D3 RID: 723
		PointerAndIntAddition = 4377,
		// Token: 0x040002D4 RID: 724
		PointerAndUIntAddition = 4384,
		// Token: 0x040002D5 RID: 725
		PointerAndLongAddition = 4385,
		// Token: 0x040002D6 RID: 726
		PointerAndULongAddition = 4386,
		// Token: 0x040002D7 RID: 727
		IntAndPointerAddition = 4387,
		// Token: 0x040002D8 RID: 728
		UIntAndPointerAddition = 4388,
		// Token: 0x040002D9 RID: 729
		LongAndPointerAddition = 4389,
		// Token: 0x040002DA RID: 730
		ULongAndPointerAddition = 4390,
		// Token: 0x040002DB RID: 731
		StringConcatenation = 4369,
		// Token: 0x040002DC RID: 732
		StringAndObjectConcatenation = 4370,
		// Token: 0x040002DD RID: 733
		ObjectAndStringConcatenation = 4371,
		// Token: 0x040002DE RID: 734
		DelegateCombination = 4375,
		// Token: 0x040002DF RID: 735
		DynamicAddition = 4393,
		// Token: 0x040002E0 RID: 736
		IntSubtraction = 4613,
		// Token: 0x040002E1 RID: 737
		UIntSubtraction = 4614,
		// Token: 0x040002E2 RID: 738
		LongSubtraction = 4615,
		// Token: 0x040002E3 RID: 739
		ULongSubtraction = 4616,
		// Token: 0x040002E4 RID: 740
		NIntSubtraction = 4617,
		// Token: 0x040002E5 RID: 741
		NUIntSubtraction = 4618,
		// Token: 0x040002E6 RID: 742
		FloatSubtraction = 4620,
		// Token: 0x040002E7 RID: 743
		DoubleSubtraction = 4621,
		// Token: 0x040002E8 RID: 744
		DecimalSubtraction = 4622,
		// Token: 0x040002E9 RID: 745
		EnumSubtraction = 4628,
		// Token: 0x040002EA RID: 746
		EnumAndUnderlyingSubtraction = 4629,
		// Token: 0x040002EB RID: 747
		UnderlyingAndEnumSubtraction = 4630,
		// Token: 0x040002EC RID: 748
		UserDefinedSubtraction = 4648,
		// Token: 0x040002ED RID: 749
		LiftedIntSubtraction = 70149,
		// Token: 0x040002EE RID: 750
		LiftedUIntSubtraction = 70150,
		// Token: 0x040002EF RID: 751
		LiftedLongSubtraction = 70151,
		// Token: 0x040002F0 RID: 752
		LiftedULongSubtraction = 70152,
		// Token: 0x040002F1 RID: 753
		LiftedNIntSubtraction = 70153,
		// Token: 0x040002F2 RID: 754
		LiftedNUIntSubtraction = 70154,
		// Token: 0x040002F3 RID: 755
		LiftedFloatSubtraction = 70156,
		// Token: 0x040002F4 RID: 756
		LiftedDoubleSubtraction = 70157,
		// Token: 0x040002F5 RID: 757
		LiftedDecimalSubtraction = 70158,
		// Token: 0x040002F6 RID: 758
		LiftedEnumSubtraction = 70164,
		// Token: 0x040002F7 RID: 759
		LiftedEnumAndUnderlyingSubtraction = 70165,
		// Token: 0x040002F8 RID: 760
		LiftedUnderlyingAndEnumSubtraction = 70166,
		// Token: 0x040002F9 RID: 761
		LiftedUserDefinedSubtraction = 70184,
		// Token: 0x040002FA RID: 762
		DelegateRemoval = 4631,
		// Token: 0x040002FB RID: 763
		PointerAndIntSubtraction = 4633,
		// Token: 0x040002FC RID: 764
		PointerAndUIntSubtraction = 4640,
		// Token: 0x040002FD RID: 765
		PointerAndLongSubtraction = 4641,
		// Token: 0x040002FE RID: 766
		PointerAndULongSubtraction = 4642,
		// Token: 0x040002FF RID: 767
		PointerSubtraction = 4632,
		// Token: 0x04000300 RID: 768
		DynamicSubtraction = 4649,
		// Token: 0x04000301 RID: 769
		IntLeftShift = 5381,
		// Token: 0x04000302 RID: 770
		UIntLeftShift = 5382,
		// Token: 0x04000303 RID: 771
		LongLeftShift = 5383,
		// Token: 0x04000304 RID: 772
		ULongLeftShift = 5384,
		// Token: 0x04000305 RID: 773
		NIntLeftShift = 5385,
		// Token: 0x04000306 RID: 774
		NUIntLeftShift = 5386,
		// Token: 0x04000307 RID: 775
		UserDefinedLeftShift = 5416,
		// Token: 0x04000308 RID: 776
		LiftedIntLeftShift = 70917,
		// Token: 0x04000309 RID: 777
		LiftedUIntLeftShift = 70918,
		// Token: 0x0400030A RID: 778
		LiftedLongLeftShift = 70919,
		// Token: 0x0400030B RID: 779
		LiftedULongLeftShift = 70920,
		// Token: 0x0400030C RID: 780
		LiftedNIntLeftShift = 70921,
		// Token: 0x0400030D RID: 781
		LiftedNUIntLeftShift = 70922,
		// Token: 0x0400030E RID: 782
		LiftedUserDefinedLeftShift = 70952,
		// Token: 0x0400030F RID: 783
		DynamicLeftShift = 5417,
		// Token: 0x04000310 RID: 784
		IntRightShift = 5637,
		// Token: 0x04000311 RID: 785
		UIntRightShift = 5638,
		// Token: 0x04000312 RID: 786
		LongRightShift = 5639,
		// Token: 0x04000313 RID: 787
		ULongRightShift = 5640,
		// Token: 0x04000314 RID: 788
		NIntRightShift = 5641,
		// Token: 0x04000315 RID: 789
		NUIntRightShift = 5642,
		// Token: 0x04000316 RID: 790
		UserDefinedRightShift = 5672,
		// Token: 0x04000317 RID: 791
		LiftedIntRightShift = 71173,
		// Token: 0x04000318 RID: 792
		LiftedUIntRightShift = 71174,
		// Token: 0x04000319 RID: 793
		LiftedLongRightShift = 71175,
		// Token: 0x0400031A RID: 794
		LiftedULongRightShift = 71176,
		// Token: 0x0400031B RID: 795
		LiftedNIntRightShift = 71177,
		// Token: 0x0400031C RID: 796
		LiftedNUIntRightShift = 71178,
		// Token: 0x0400031D RID: 797
		LiftedUserDefinedRightShift = 71208,
		// Token: 0x0400031E RID: 798
		DynamicRightShift = 5673,
		// Token: 0x0400031F RID: 799
		IntEqual = 5893,
		// Token: 0x04000320 RID: 800
		UIntEqual = 5894,
		// Token: 0x04000321 RID: 801
		LongEqual = 5895,
		// Token: 0x04000322 RID: 802
		ULongEqual = 5896,
		// Token: 0x04000323 RID: 803
		NIntEqual = 5897,
		// Token: 0x04000324 RID: 804
		NUIntEqual = 5898,
		// Token: 0x04000325 RID: 805
		FloatEqual = 5900,
		// Token: 0x04000326 RID: 806
		DoubleEqual = 5901,
		// Token: 0x04000327 RID: 807
		DecimalEqual = 5902,
		// Token: 0x04000328 RID: 808
		BoolEqual = 5903,
		// Token: 0x04000329 RID: 809
		EnumEqual = 5908,
		// Token: 0x0400032A RID: 810
		NullableNullEqual = 5927,
		// Token: 0x0400032B RID: 811
		UserDefinedEqual = 5928,
		// Token: 0x0400032C RID: 812
		LiftedIntEqual = 71429,
		// Token: 0x0400032D RID: 813
		LiftedUIntEqual = 71430,
		// Token: 0x0400032E RID: 814
		LiftedLongEqual = 71431,
		// Token: 0x0400032F RID: 815
		LiftedULongEqual = 71432,
		// Token: 0x04000330 RID: 816
		LiftedNIntEqual = 71433,
		// Token: 0x04000331 RID: 817
		LiftedNUIntEqual = 71434,
		// Token: 0x04000332 RID: 818
		LiftedFloatEqual = 71436,
		// Token: 0x04000333 RID: 819
		LiftedDoubleEqual = 71437,
		// Token: 0x04000334 RID: 820
		LiftedDecimalEqual = 71438,
		// Token: 0x04000335 RID: 821
		LiftedBoolEqual = 71439,
		// Token: 0x04000336 RID: 822
		LiftedEnumEqual = 71444,
		// Token: 0x04000337 RID: 823
		LiftedUserDefinedEqual = 71464,
		// Token: 0x04000338 RID: 824
		ObjectEqual = 5904,
		// Token: 0x04000339 RID: 825
		StringEqual = 5905,
		// Token: 0x0400033A RID: 826
		DelegateEqual = 5911,
		// Token: 0x0400033B RID: 827
		PointerEqual = 5912,
		// Token: 0x0400033C RID: 828
		DynamicEqual = 5929,
		// Token: 0x0400033D RID: 829
		IntNotEqual = 6149,
		// Token: 0x0400033E RID: 830
		UIntNotEqual = 6150,
		// Token: 0x0400033F RID: 831
		LongNotEqual = 6151,
		// Token: 0x04000340 RID: 832
		ULongNotEqual = 6152,
		// Token: 0x04000341 RID: 833
		NIntNotEqual = 6153,
		// Token: 0x04000342 RID: 834
		NUIntNotEqual = 6154,
		// Token: 0x04000343 RID: 835
		FloatNotEqual = 6156,
		// Token: 0x04000344 RID: 836
		DoubleNotEqual = 6157,
		// Token: 0x04000345 RID: 837
		DecimalNotEqual = 6158,
		// Token: 0x04000346 RID: 838
		BoolNotEqual = 6159,
		// Token: 0x04000347 RID: 839
		EnumNotEqual = 6164,
		// Token: 0x04000348 RID: 840
		NullableNullNotEqual = 6183,
		// Token: 0x04000349 RID: 841
		UserDefinedNotEqual = 6184,
		// Token: 0x0400034A RID: 842
		LiftedIntNotEqual = 71685,
		// Token: 0x0400034B RID: 843
		LiftedUIntNotEqual = 71686,
		// Token: 0x0400034C RID: 844
		LiftedLongNotEqual = 71687,
		// Token: 0x0400034D RID: 845
		LiftedULongNotEqual = 71688,
		// Token: 0x0400034E RID: 846
		LiftedNIntNotEqual = 71689,
		// Token: 0x0400034F RID: 847
		LiftedNUIntNotEqual = 71690,
		// Token: 0x04000350 RID: 848
		LiftedFloatNotEqual = 71692,
		// Token: 0x04000351 RID: 849
		LiftedDoubleNotEqual = 71693,
		// Token: 0x04000352 RID: 850
		LiftedDecimalNotEqual = 71694,
		// Token: 0x04000353 RID: 851
		LiftedBoolNotEqual = 71695,
		// Token: 0x04000354 RID: 852
		LiftedEnumNotEqual = 71700,
		// Token: 0x04000355 RID: 853
		LiftedUserDefinedNotEqual = 71720,
		// Token: 0x04000356 RID: 854
		ObjectNotEqual = 6160,
		// Token: 0x04000357 RID: 855
		StringNotEqual = 6161,
		// Token: 0x04000358 RID: 856
		DelegateNotEqual = 6167,
		// Token: 0x04000359 RID: 857
		PointerNotEqual = 6168,
		// Token: 0x0400035A RID: 858
		DynamicNotEqual = 6185,
		// Token: 0x0400035B RID: 859
		IntLessThan = 6661,
		// Token: 0x0400035C RID: 860
		UIntLessThan = 6662,
		// Token: 0x0400035D RID: 861
		LongLessThan = 6663,
		// Token: 0x0400035E RID: 862
		ULongLessThan = 6664,
		// Token: 0x0400035F RID: 863
		NIntLessThan = 6665,
		// Token: 0x04000360 RID: 864
		NUIntLessThan = 6666,
		// Token: 0x04000361 RID: 865
		FloatLessThan = 6668,
		// Token: 0x04000362 RID: 866
		DoubleLessThan = 6669,
		// Token: 0x04000363 RID: 867
		DecimalLessThan = 6670,
		// Token: 0x04000364 RID: 868
		EnumLessThan = 6676,
		// Token: 0x04000365 RID: 869
		UserDefinedLessThan = 6696,
		// Token: 0x04000366 RID: 870
		LiftedIntLessThan = 72197,
		// Token: 0x04000367 RID: 871
		LiftedUIntLessThan = 72198,
		// Token: 0x04000368 RID: 872
		LiftedLongLessThan = 72199,
		// Token: 0x04000369 RID: 873
		LiftedULongLessThan = 72200,
		// Token: 0x0400036A RID: 874
		LiftedNIntLessThan = 72201,
		// Token: 0x0400036B RID: 875
		LiftedNUIntLessThan = 72202,
		// Token: 0x0400036C RID: 876
		LiftedFloatLessThan = 72204,
		// Token: 0x0400036D RID: 877
		LiftedDoubleLessThan = 72205,
		// Token: 0x0400036E RID: 878
		LiftedDecimalLessThan = 72206,
		// Token: 0x0400036F RID: 879
		LiftedEnumLessThan = 72212,
		// Token: 0x04000370 RID: 880
		LiftedUserDefinedLessThan = 72232,
		// Token: 0x04000371 RID: 881
		PointerLessThan = 6680,
		// Token: 0x04000372 RID: 882
		DynamicLessThan = 6697,
		// Token: 0x04000373 RID: 883
		IntGreaterThan = 6405,
		// Token: 0x04000374 RID: 884
		UIntGreaterThan = 6406,
		// Token: 0x04000375 RID: 885
		LongGreaterThan = 6407,
		// Token: 0x04000376 RID: 886
		ULongGreaterThan = 6408,
		// Token: 0x04000377 RID: 887
		NIntGreaterThan = 6409,
		// Token: 0x04000378 RID: 888
		NUIntGreaterThan = 6410,
		// Token: 0x04000379 RID: 889
		FloatGreaterThan = 6412,
		// Token: 0x0400037A RID: 890
		DoubleGreaterThan = 6413,
		// Token: 0x0400037B RID: 891
		DecimalGreaterThan = 6414,
		// Token: 0x0400037C RID: 892
		EnumGreaterThan = 6420,
		// Token: 0x0400037D RID: 893
		UserDefinedGreaterThan = 6440,
		// Token: 0x0400037E RID: 894
		LiftedIntGreaterThan = 71941,
		// Token: 0x0400037F RID: 895
		LiftedUIntGreaterThan = 71942,
		// Token: 0x04000380 RID: 896
		LiftedLongGreaterThan = 71943,
		// Token: 0x04000381 RID: 897
		LiftedULongGreaterThan = 71944,
		// Token: 0x04000382 RID: 898
		LiftedNIntGreaterThan = 71945,
		// Token: 0x04000383 RID: 899
		LiftedNUIntGreaterThan = 71946,
		// Token: 0x04000384 RID: 900
		LiftedFloatGreaterThan = 71948,
		// Token: 0x04000385 RID: 901
		LiftedDoubleGreaterThan = 71949,
		// Token: 0x04000386 RID: 902
		LiftedDecimalGreaterThan = 71950,
		// Token: 0x04000387 RID: 903
		LiftedEnumGreaterThan = 71956,
		// Token: 0x04000388 RID: 904
		LiftedUserDefinedGreaterThan = 71976,
		// Token: 0x04000389 RID: 905
		PointerGreaterThan = 6424,
		// Token: 0x0400038A RID: 906
		DynamicGreaterThan = 6441,
		// Token: 0x0400038B RID: 907
		IntLessThanOrEqual = 7173,
		// Token: 0x0400038C RID: 908
		UIntLessThanOrEqual = 7174,
		// Token: 0x0400038D RID: 909
		LongLessThanOrEqual = 7175,
		// Token: 0x0400038E RID: 910
		ULongLessThanOrEqual = 7176,
		// Token: 0x0400038F RID: 911
		NIntLessThanOrEqual = 7177,
		// Token: 0x04000390 RID: 912
		NUIntLessThanOrEqual = 7178,
		// Token: 0x04000391 RID: 913
		FloatLessThanOrEqual = 7180,
		// Token: 0x04000392 RID: 914
		DoubleLessThanOrEqual = 7181,
		// Token: 0x04000393 RID: 915
		DecimalLessThanOrEqual = 7182,
		// Token: 0x04000394 RID: 916
		EnumLessThanOrEqual = 7188,
		// Token: 0x04000395 RID: 917
		UserDefinedLessThanOrEqual = 7208,
		// Token: 0x04000396 RID: 918
		LiftedIntLessThanOrEqual = 72709,
		// Token: 0x04000397 RID: 919
		LiftedUIntLessThanOrEqual = 72710,
		// Token: 0x04000398 RID: 920
		LiftedLongLessThanOrEqual = 72711,
		// Token: 0x04000399 RID: 921
		LiftedULongLessThanOrEqual = 72712,
		// Token: 0x0400039A RID: 922
		LiftedNIntLessThanOrEqual = 72713,
		// Token: 0x0400039B RID: 923
		LiftedNUIntLessThanOrEqual = 72714,
		// Token: 0x0400039C RID: 924
		LiftedFloatLessThanOrEqual = 72716,
		// Token: 0x0400039D RID: 925
		LiftedDoubleLessThanOrEqual = 72717,
		// Token: 0x0400039E RID: 926
		LiftedDecimalLessThanOrEqual = 72718,
		// Token: 0x0400039F RID: 927
		LiftedEnumLessThanOrEqual = 72724,
		// Token: 0x040003A0 RID: 928
		LiftedUserDefinedLessThanOrEqual = 72744,
		// Token: 0x040003A1 RID: 929
		PointerLessThanOrEqual = 7192,
		// Token: 0x040003A2 RID: 930
		DynamicLessThanOrEqual = 7209,
		// Token: 0x040003A3 RID: 931
		IntGreaterThanOrEqual = 6917,
		// Token: 0x040003A4 RID: 932
		UIntGreaterThanOrEqual = 6918,
		// Token: 0x040003A5 RID: 933
		LongGreaterThanOrEqual = 6919,
		// Token: 0x040003A6 RID: 934
		ULongGreaterThanOrEqual = 6920,
		// Token: 0x040003A7 RID: 935
		NIntGreaterThanOrEqual = 6921,
		// Token: 0x040003A8 RID: 936
		NUIntGreaterThanOrEqual = 6922,
		// Token: 0x040003A9 RID: 937
		FloatGreaterThanOrEqual = 6924,
		// Token: 0x040003AA RID: 938
		DoubleGreaterThanOrEqual = 6925,
		// Token: 0x040003AB RID: 939
		DecimalGreaterThanOrEqual = 6926,
		// Token: 0x040003AC RID: 940
		EnumGreaterThanOrEqual = 6932,
		// Token: 0x040003AD RID: 941
		UserDefinedGreaterThanOrEqual = 6952,
		// Token: 0x040003AE RID: 942
		LiftedIntGreaterThanOrEqual = 72453,
		// Token: 0x040003AF RID: 943
		LiftedUIntGreaterThanOrEqual = 72454,
		// Token: 0x040003B0 RID: 944
		LiftedLongGreaterThanOrEqual = 72455,
		// Token: 0x040003B1 RID: 945
		LiftedULongGreaterThanOrEqual = 72456,
		// Token: 0x040003B2 RID: 946
		LiftedNIntGreaterThanOrEqual = 72457,
		// Token: 0x040003B3 RID: 947
		LiftedNUIntGreaterThanOrEqual = 72458,
		// Token: 0x040003B4 RID: 948
		LiftedFloatGreaterThanOrEqual = 72460,
		// Token: 0x040003B5 RID: 949
		LiftedDoubleGreaterThanOrEqual = 72461,
		// Token: 0x040003B6 RID: 950
		LiftedDecimalGreaterThanOrEqual = 72462,
		// Token: 0x040003B7 RID: 951
		LiftedEnumGreaterThanOrEqual = 72468,
		// Token: 0x040003B8 RID: 952
		LiftedUserDefinedGreaterThanOrEqual = 72488,
		// Token: 0x040003B9 RID: 953
		PointerGreaterThanOrEqual = 6936,
		// Token: 0x040003BA RID: 954
		DynamicGreaterThanOrEqual = 6953,
		// Token: 0x040003BB RID: 955
		IntAnd = 7429,
		// Token: 0x040003BC RID: 956
		UIntAnd = 7430,
		// Token: 0x040003BD RID: 957
		LongAnd = 7431,
		// Token: 0x040003BE RID: 958
		ULongAnd = 7432,
		// Token: 0x040003BF RID: 959
		NIntAnd = 7433,
		// Token: 0x040003C0 RID: 960
		NUIntAnd = 7434,
		// Token: 0x040003C1 RID: 961
		EnumAnd = 7444,
		// Token: 0x040003C2 RID: 962
		BoolAnd = 7439,
		// Token: 0x040003C3 RID: 963
		UserDefinedAnd = 7464,
		// Token: 0x040003C4 RID: 964
		LiftedIntAnd = 72965,
		// Token: 0x040003C5 RID: 965
		LiftedUIntAnd = 72966,
		// Token: 0x040003C6 RID: 966
		LiftedLongAnd = 72967,
		// Token: 0x040003C7 RID: 967
		LiftedULongAnd = 72968,
		// Token: 0x040003C8 RID: 968
		LiftedNIntAnd = 72969,
		// Token: 0x040003C9 RID: 969
		LiftedNUIntAnd = 72970,
		// Token: 0x040003CA RID: 970
		LiftedEnumAnd = 72980,
		// Token: 0x040003CB RID: 971
		LiftedBoolAnd = 72975,
		// Token: 0x040003CC RID: 972
		LiftedUserDefinedAnd = 73000,
		// Token: 0x040003CD RID: 973
		DynamicAnd = 7465,
		// Token: 0x040003CE RID: 974
		LogicalAnd = 138496,
		// Token: 0x040003CF RID: 975
		LogicalBoolAnd = 138511,
		// Token: 0x040003D0 RID: 976
		LogicalUserDefinedAnd = 138536,
		// Token: 0x040003D1 RID: 977
		DynamicLogicalAnd = 138537,
		// Token: 0x040003D2 RID: 978
		IntOr = 7941,
		// Token: 0x040003D3 RID: 979
		UIntOr = 7942,
		// Token: 0x040003D4 RID: 980
		LongOr = 7943,
		// Token: 0x040003D5 RID: 981
		ULongOr = 7944,
		// Token: 0x040003D6 RID: 982
		NIntOr = 7945,
		// Token: 0x040003D7 RID: 983
		NUIntOr = 7946,
		// Token: 0x040003D8 RID: 984
		EnumOr = 7956,
		// Token: 0x040003D9 RID: 985
		BoolOr = 7951,
		// Token: 0x040003DA RID: 986
		UserDefinedOr = 7976,
		// Token: 0x040003DB RID: 987
		LiftedIntOr = 73477,
		// Token: 0x040003DC RID: 988
		LiftedUIntOr = 73478,
		// Token: 0x040003DD RID: 989
		LiftedLongOr = 73479,
		// Token: 0x040003DE RID: 990
		LiftedULongOr = 73480,
		// Token: 0x040003DF RID: 991
		LiftedNIntOr = 73481,
		// Token: 0x040003E0 RID: 992
		LiftedNUIntOr = 73482,
		// Token: 0x040003E1 RID: 993
		LiftedEnumOr = 73492,
		// Token: 0x040003E2 RID: 994
		LiftedBoolOr = 73487,
		// Token: 0x040003E3 RID: 995
		LiftedUserDefinedOr = 73512,
		// Token: 0x040003E4 RID: 996
		DynamicOr = 7977,
		// Token: 0x040003E5 RID: 997
		LogicalOr = 139008,
		// Token: 0x040003E6 RID: 998
		LogicalBoolOr = 139023,
		// Token: 0x040003E7 RID: 999
		LogicalUserDefinedOr = 139048,
		// Token: 0x040003E8 RID: 1000
		DynamicLogicalOr = 139049,
		// Token: 0x040003E9 RID: 1001
		IntXor = 7685,
		// Token: 0x040003EA RID: 1002
		UIntXor = 7686,
		// Token: 0x040003EB RID: 1003
		LongXor = 7687,
		// Token: 0x040003EC RID: 1004
		ULongXor = 7688,
		// Token: 0x040003ED RID: 1005
		NIntXor = 7689,
		// Token: 0x040003EE RID: 1006
		NUIntXor = 7690,
		// Token: 0x040003EF RID: 1007
		EnumXor = 7700,
		// Token: 0x040003F0 RID: 1008
		BoolXor = 7695,
		// Token: 0x040003F1 RID: 1009
		UserDefinedXor = 7720,
		// Token: 0x040003F2 RID: 1010
		LiftedIntXor = 73221,
		// Token: 0x040003F3 RID: 1011
		LiftedUIntXor = 73222,
		// Token: 0x040003F4 RID: 1012
		LiftedLongXor = 73223,
		// Token: 0x040003F5 RID: 1013
		LiftedULongXor = 73224,
		// Token: 0x040003F6 RID: 1014
		LiftedNIntXor = 73225,
		// Token: 0x040003F7 RID: 1015
		LiftedNUIntXor = 73226,
		// Token: 0x040003F8 RID: 1016
		LiftedEnumXor = 73236,
		// Token: 0x040003F9 RID: 1017
		LiftedBoolXor = 73231,
		// Token: 0x040003FA RID: 1018
		LiftedUserDefinedXor = 73256,
		// Token: 0x040003FB RID: 1019
		DynamicXor = 7721
	}
}
