using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005A RID: 90
	[Flags]
	internal enum UnaryOperatorKind
	{
		// Token: 0x0400014D RID: 333
		TypeMask = 255,
		// Token: 0x0400014E RID: 334
		SByte = 1,
		// Token: 0x0400014F RID: 335
		Byte = 2,
		// Token: 0x04000150 RID: 336
		Short = 3,
		// Token: 0x04000151 RID: 337
		UShort = 4,
		// Token: 0x04000152 RID: 338
		Int = 5,
		// Token: 0x04000153 RID: 339
		UInt = 6,
		// Token: 0x04000154 RID: 340
		Long = 7,
		// Token: 0x04000155 RID: 341
		ULong = 8,
		// Token: 0x04000156 RID: 342
		NInt = 9,
		// Token: 0x04000157 RID: 343
		NUInt = 10,
		// Token: 0x04000158 RID: 344
		Char = 11,
		// Token: 0x04000159 RID: 345
		Float = 12,
		// Token: 0x0400015A RID: 346
		Double = 13,
		// Token: 0x0400015B RID: 347
		Decimal = 14,
		// Token: 0x0400015C RID: 348
		Bool = 15,
		// Token: 0x0400015D RID: 349
		_Object = 16,
		// Token: 0x0400015E RID: 350
		_String = 17,
		// Token: 0x0400015F RID: 351
		_StringAndObject = 18,
		// Token: 0x04000160 RID: 352
		_ObjectAndString = 19,
		// Token: 0x04000161 RID: 353
		Enum = 20,
		// Token: 0x04000162 RID: 354
		_EnumAndUnderlying = 21,
		// Token: 0x04000163 RID: 355
		_UnderlyingAndEnum = 22,
		// Token: 0x04000164 RID: 356
		_Delegate = 23,
		// Token: 0x04000165 RID: 357
		Pointer = 24,
		// Token: 0x04000166 RID: 358
		_PointerAndInt = 25,
		// Token: 0x04000167 RID: 359
		_PointerAndUInt = 32,
		// Token: 0x04000168 RID: 360
		_PointerAndLong = 33,
		// Token: 0x04000169 RID: 361
		_PointerAndULong = 34,
		// Token: 0x0400016A RID: 362
		_IntAndPointer = 35,
		// Token: 0x0400016B RID: 363
		_UIntAndPointer = 36,
		// Token: 0x0400016C RID: 364
		_LongAndPointer = 37,
		// Token: 0x0400016D RID: 365
		_ULongAndPointer = 38,
		// Token: 0x0400016E RID: 366
		_NullableNull = 39,
		// Token: 0x0400016F RID: 367
		UserDefined = 40,
		// Token: 0x04000170 RID: 368
		Dynamic = 41,
		// Token: 0x04000171 RID: 369
		OpMask = 65280,
		// Token: 0x04000172 RID: 370
		PostfixIncrement = 4096,
		// Token: 0x04000173 RID: 371
		PostfixDecrement = 4352,
		// Token: 0x04000174 RID: 372
		PrefixIncrement = 4608,
		// Token: 0x04000175 RID: 373
		PrefixDecrement = 4864,
		// Token: 0x04000176 RID: 374
		UnaryPlus = 5120,
		// Token: 0x04000177 RID: 375
		UnaryMinus = 5376,
		// Token: 0x04000178 RID: 376
		LogicalNegation = 5632,
		// Token: 0x04000179 RID: 377
		BitwiseComplement = 5888,
		// Token: 0x0400017A RID: 378
		True = 6144,
		// Token: 0x0400017B RID: 379
		False = 6400,
		// Token: 0x0400017C RID: 380
		Lifted = 65536,
		// Token: 0x0400017D RID: 381
		_Logical = 131072,
		// Token: 0x0400017E RID: 382
		Checked = 262144,
		// Token: 0x0400017F RID: 383
		Error = 0,
		// Token: 0x04000180 RID: 384
		SBytePostfixIncrement = 4097,
		// Token: 0x04000181 RID: 385
		BytePostfixIncrement = 4098,
		// Token: 0x04000182 RID: 386
		ShortPostfixIncrement = 4099,
		// Token: 0x04000183 RID: 387
		UShortPostfixIncrement = 4100,
		// Token: 0x04000184 RID: 388
		IntPostfixIncrement = 4101,
		// Token: 0x04000185 RID: 389
		UIntPostfixIncrement = 4102,
		// Token: 0x04000186 RID: 390
		LongPostfixIncrement = 4103,
		// Token: 0x04000187 RID: 391
		ULongPostfixIncrement = 4104,
		// Token: 0x04000188 RID: 392
		NIntPostfixIncrement = 4105,
		// Token: 0x04000189 RID: 393
		NUIntPostfixIncrement = 4106,
		// Token: 0x0400018A RID: 394
		CharPostfixIncrement = 4107,
		// Token: 0x0400018B RID: 395
		FloatPostfixIncrement = 4108,
		// Token: 0x0400018C RID: 396
		DoublePostfixIncrement = 4109,
		// Token: 0x0400018D RID: 397
		DecimalPostfixIncrement = 4110,
		// Token: 0x0400018E RID: 398
		EnumPostfixIncrement = 4116,
		// Token: 0x0400018F RID: 399
		UserDefinedPostfixIncrement = 4136,
		// Token: 0x04000190 RID: 400
		LiftedSBytePostfixIncrement = 69633,
		// Token: 0x04000191 RID: 401
		LiftedBytePostfixIncrement = 69634,
		// Token: 0x04000192 RID: 402
		LiftedShortPostfixIncrement = 69635,
		// Token: 0x04000193 RID: 403
		LiftedUShortPostfixIncrement = 69636,
		// Token: 0x04000194 RID: 404
		LiftedIntPostfixIncrement = 69637,
		// Token: 0x04000195 RID: 405
		LiftedUIntPostfixIncrement = 69638,
		// Token: 0x04000196 RID: 406
		LiftedLongPostfixIncrement = 69639,
		// Token: 0x04000197 RID: 407
		LiftedULongPostfixIncrement = 69640,
		// Token: 0x04000198 RID: 408
		LiftedNIntPostfixIncrement = 69641,
		// Token: 0x04000199 RID: 409
		LiftedNUIntPostfixIncrement = 69642,
		// Token: 0x0400019A RID: 410
		LiftedCharPostfixIncrement = 69643,
		// Token: 0x0400019B RID: 411
		LiftedFloatPostfixIncrement = 69644,
		// Token: 0x0400019C RID: 412
		LiftedDoublePostfixIncrement = 69645,
		// Token: 0x0400019D RID: 413
		LiftedDecimalPostfixIncrement = 69646,
		// Token: 0x0400019E RID: 414
		LiftedEnumPostfixIncrement = 69652,
		// Token: 0x0400019F RID: 415
		LiftedUserDefinedPostfixIncrement = 69672,
		// Token: 0x040001A0 RID: 416
		PointerPostfixIncrement = 4120,
		// Token: 0x040001A1 RID: 417
		DynamicPostfixIncrement = 4137,
		// Token: 0x040001A2 RID: 418
		SBytePrefixIncrement = 4609,
		// Token: 0x040001A3 RID: 419
		BytePrefixIncrement = 4610,
		// Token: 0x040001A4 RID: 420
		ShortPrefixIncrement = 4611,
		// Token: 0x040001A5 RID: 421
		UShortPrefixIncrement = 4612,
		// Token: 0x040001A6 RID: 422
		IntPrefixIncrement = 4613,
		// Token: 0x040001A7 RID: 423
		UIntPrefixIncrement = 4614,
		// Token: 0x040001A8 RID: 424
		LongPrefixIncrement = 4615,
		// Token: 0x040001A9 RID: 425
		ULongPrefixIncrement = 4616,
		// Token: 0x040001AA RID: 426
		NIntPrefixIncrement = 4617,
		// Token: 0x040001AB RID: 427
		NUIntPrefixIncrement = 4618,
		// Token: 0x040001AC RID: 428
		CharPrefixIncrement = 4619,
		// Token: 0x040001AD RID: 429
		FloatPrefixIncrement = 4620,
		// Token: 0x040001AE RID: 430
		DoublePrefixIncrement = 4621,
		// Token: 0x040001AF RID: 431
		DecimalPrefixIncrement = 4622,
		// Token: 0x040001B0 RID: 432
		EnumPrefixIncrement = 4628,
		// Token: 0x040001B1 RID: 433
		UserDefinedPrefixIncrement = 4648,
		// Token: 0x040001B2 RID: 434
		LiftedSBytePrefixIncrement = 70145,
		// Token: 0x040001B3 RID: 435
		LiftedBytePrefixIncrement = 70146,
		// Token: 0x040001B4 RID: 436
		LiftedShortPrefixIncrement = 70147,
		// Token: 0x040001B5 RID: 437
		LiftedUShortPrefixIncrement = 70148,
		// Token: 0x040001B6 RID: 438
		LiftedIntPrefixIncrement = 70149,
		// Token: 0x040001B7 RID: 439
		LiftedUIntPrefixIncrement = 70150,
		// Token: 0x040001B8 RID: 440
		LiftedLongPrefixIncrement = 70151,
		// Token: 0x040001B9 RID: 441
		LiftedULongPrefixIncrement = 70152,
		// Token: 0x040001BA RID: 442
		LiftedNIntPrefixIncrement = 70153,
		// Token: 0x040001BB RID: 443
		LiftedNUIntPrefixIncrement = 70154,
		// Token: 0x040001BC RID: 444
		LiftedCharPrefixIncrement = 70155,
		// Token: 0x040001BD RID: 445
		LiftedFloatPrefixIncrement = 70156,
		// Token: 0x040001BE RID: 446
		LiftedDoublePrefixIncrement = 70157,
		// Token: 0x040001BF RID: 447
		LiftedDecimalPrefixIncrement = 70158,
		// Token: 0x040001C0 RID: 448
		LiftedEnumPrefixIncrement = 70164,
		// Token: 0x040001C1 RID: 449
		LiftedUserDefinedPrefixIncrement = 70184,
		// Token: 0x040001C2 RID: 450
		PointerPrefixIncrement = 4632,
		// Token: 0x040001C3 RID: 451
		DynamicPrefixIncrement = 4649,
		// Token: 0x040001C4 RID: 452
		SBytePostfixDecrement = 4353,
		// Token: 0x040001C5 RID: 453
		BytePostfixDecrement = 4354,
		// Token: 0x040001C6 RID: 454
		ShortPostfixDecrement = 4355,
		// Token: 0x040001C7 RID: 455
		UShortPostfixDecrement = 4356,
		// Token: 0x040001C8 RID: 456
		IntPostfixDecrement = 4357,
		// Token: 0x040001C9 RID: 457
		UIntPostfixDecrement = 4358,
		// Token: 0x040001CA RID: 458
		LongPostfixDecrement = 4359,
		// Token: 0x040001CB RID: 459
		ULongPostfixDecrement = 4360,
		// Token: 0x040001CC RID: 460
		NIntPostfixDecrement = 4361,
		// Token: 0x040001CD RID: 461
		NUIntPostfixDecrement = 4362,
		// Token: 0x040001CE RID: 462
		CharPostfixDecrement = 4363,
		// Token: 0x040001CF RID: 463
		FloatPostfixDecrement = 4364,
		// Token: 0x040001D0 RID: 464
		DoublePostfixDecrement = 4365,
		// Token: 0x040001D1 RID: 465
		DecimalPostfixDecrement = 4366,
		// Token: 0x040001D2 RID: 466
		EnumPostfixDecrement = 4372,
		// Token: 0x040001D3 RID: 467
		UserDefinedPostfixDecrement = 4392,
		// Token: 0x040001D4 RID: 468
		LiftedSBytePostfixDecrement = 69889,
		// Token: 0x040001D5 RID: 469
		LiftedBytePostfixDecrement = 69890,
		// Token: 0x040001D6 RID: 470
		LiftedShortPostfixDecrement = 69891,
		// Token: 0x040001D7 RID: 471
		LiftedUShortPostfixDecrement = 69892,
		// Token: 0x040001D8 RID: 472
		LiftedIntPostfixDecrement = 69893,
		// Token: 0x040001D9 RID: 473
		LiftedUIntPostfixDecrement = 69894,
		// Token: 0x040001DA RID: 474
		LiftedLongPostfixDecrement = 69895,
		// Token: 0x040001DB RID: 475
		LiftedULongPostfixDecrement = 69896,
		// Token: 0x040001DC RID: 476
		LiftedNIntPostfixDecrement = 69897,
		// Token: 0x040001DD RID: 477
		LiftedNUIntPostfixDecrement = 69898,
		// Token: 0x040001DE RID: 478
		LiftedCharPostfixDecrement = 69899,
		// Token: 0x040001DF RID: 479
		LiftedFloatPostfixDecrement = 69900,
		// Token: 0x040001E0 RID: 480
		LiftedDoublePostfixDecrement = 69901,
		// Token: 0x040001E1 RID: 481
		LiftedDecimalPostfixDecrement = 69902,
		// Token: 0x040001E2 RID: 482
		LiftedEnumPostfixDecrement = 69908,
		// Token: 0x040001E3 RID: 483
		LiftedUserDefinedPostfixDecrement = 69928,
		// Token: 0x040001E4 RID: 484
		PointerPostfixDecrement = 4376,
		// Token: 0x040001E5 RID: 485
		DynamicPostfixDecrement = 4393,
		// Token: 0x040001E6 RID: 486
		SBytePrefixDecrement = 4865,
		// Token: 0x040001E7 RID: 487
		BytePrefixDecrement = 4866,
		// Token: 0x040001E8 RID: 488
		ShortPrefixDecrement = 4867,
		// Token: 0x040001E9 RID: 489
		UShortPrefixDecrement = 4868,
		// Token: 0x040001EA RID: 490
		IntPrefixDecrement = 4869,
		// Token: 0x040001EB RID: 491
		UIntPrefixDecrement = 4870,
		// Token: 0x040001EC RID: 492
		LongPrefixDecrement = 4871,
		// Token: 0x040001ED RID: 493
		ULongPrefixDecrement = 4872,
		// Token: 0x040001EE RID: 494
		NIntPrefixDecrement = 4873,
		// Token: 0x040001EF RID: 495
		NUIntPrefixDecrement = 4874,
		// Token: 0x040001F0 RID: 496
		CharPrefixDecrement = 4875,
		// Token: 0x040001F1 RID: 497
		FloatPrefixDecrement = 4876,
		// Token: 0x040001F2 RID: 498
		DoublePrefixDecrement = 4877,
		// Token: 0x040001F3 RID: 499
		DecimalPrefixDecrement = 4878,
		// Token: 0x040001F4 RID: 500
		EnumPrefixDecrement = 4884,
		// Token: 0x040001F5 RID: 501
		UserDefinedPrefixDecrement = 4904,
		// Token: 0x040001F6 RID: 502
		LiftedSBytePrefixDecrement = 70401,
		// Token: 0x040001F7 RID: 503
		LiftedBytePrefixDecrement = 70402,
		// Token: 0x040001F8 RID: 504
		LiftedShortPrefixDecrement = 70403,
		// Token: 0x040001F9 RID: 505
		LiftedUShortPrefixDecrement = 70404,
		// Token: 0x040001FA RID: 506
		LiftedIntPrefixDecrement = 70405,
		// Token: 0x040001FB RID: 507
		LiftedUIntPrefixDecrement = 70406,
		// Token: 0x040001FC RID: 508
		LiftedLongPrefixDecrement = 70407,
		// Token: 0x040001FD RID: 509
		LiftedULongPrefixDecrement = 70408,
		// Token: 0x040001FE RID: 510
		LiftedNIntPrefixDecrement = 70409,
		// Token: 0x040001FF RID: 511
		LiftedNUIntPrefixDecrement = 70410,
		// Token: 0x04000200 RID: 512
		LiftedCharPrefixDecrement = 70411,
		// Token: 0x04000201 RID: 513
		LiftedFloatPrefixDecrement = 70412,
		// Token: 0x04000202 RID: 514
		LiftedDoublePrefixDecrement = 70413,
		// Token: 0x04000203 RID: 515
		LiftedDecimalPrefixDecrement = 70414,
		// Token: 0x04000204 RID: 516
		LiftedEnumPrefixDecrement = 70420,
		// Token: 0x04000205 RID: 517
		LiftedUserDefinedPrefixDecrement = 70440,
		// Token: 0x04000206 RID: 518
		PointerPrefixDecrement = 4888,
		// Token: 0x04000207 RID: 519
		DynamicPrefixDecrement = 4905,
		// Token: 0x04000208 RID: 520
		IntUnaryPlus = 5125,
		// Token: 0x04000209 RID: 521
		UIntUnaryPlus = 5126,
		// Token: 0x0400020A RID: 522
		LongUnaryPlus = 5127,
		// Token: 0x0400020B RID: 523
		ULongUnaryPlus = 5128,
		// Token: 0x0400020C RID: 524
		NIntUnaryPlus = 5129,
		// Token: 0x0400020D RID: 525
		NUIntUnaryPlus = 5130,
		// Token: 0x0400020E RID: 526
		FloatUnaryPlus = 5132,
		// Token: 0x0400020F RID: 527
		DoubleUnaryPlus = 5133,
		// Token: 0x04000210 RID: 528
		DecimalUnaryPlus = 5134,
		// Token: 0x04000211 RID: 529
		UserDefinedUnaryPlus = 5160,
		// Token: 0x04000212 RID: 530
		LiftedIntUnaryPlus = 70661,
		// Token: 0x04000213 RID: 531
		LiftedUIntUnaryPlus = 70662,
		// Token: 0x04000214 RID: 532
		LiftedLongUnaryPlus = 70663,
		// Token: 0x04000215 RID: 533
		LiftedULongUnaryPlus = 70664,
		// Token: 0x04000216 RID: 534
		LiftedNIntUnaryPlus = 70665,
		// Token: 0x04000217 RID: 535
		LiftedNUIntUnaryPlus = 70666,
		// Token: 0x04000218 RID: 536
		LiftedFloatUnaryPlus = 70668,
		// Token: 0x04000219 RID: 537
		LiftedDoubleUnaryPlus = 70669,
		// Token: 0x0400021A RID: 538
		LiftedDecimalUnaryPlus = 70670,
		// Token: 0x0400021B RID: 539
		LiftedUserDefinedUnaryPlus = 70696,
		// Token: 0x0400021C RID: 540
		DynamicUnaryPlus = 5161,
		// Token: 0x0400021D RID: 541
		IntUnaryMinus = 5381,
		// Token: 0x0400021E RID: 542
		LongUnaryMinus = 5383,
		// Token: 0x0400021F RID: 543
		NIntUnaryMinus = 5385,
		// Token: 0x04000220 RID: 544
		FloatUnaryMinus = 5388,
		// Token: 0x04000221 RID: 545
		DoubleUnaryMinus = 5389,
		// Token: 0x04000222 RID: 546
		DecimalUnaryMinus = 5390,
		// Token: 0x04000223 RID: 547
		UserDefinedUnaryMinus = 5416,
		// Token: 0x04000224 RID: 548
		LiftedIntUnaryMinus = 70917,
		// Token: 0x04000225 RID: 549
		LiftedLongUnaryMinus = 70919,
		// Token: 0x04000226 RID: 550
		LiftedNIntUnaryMinus = 70921,
		// Token: 0x04000227 RID: 551
		LiftedFloatUnaryMinus = 70924,
		// Token: 0x04000228 RID: 552
		LiftedDoubleUnaryMinus = 70925,
		// Token: 0x04000229 RID: 553
		LiftedDecimalUnaryMinus = 70926,
		// Token: 0x0400022A RID: 554
		LiftedUserDefinedUnaryMinus = 70952,
		// Token: 0x0400022B RID: 555
		DynamicUnaryMinus = 5417,
		// Token: 0x0400022C RID: 556
		BoolLogicalNegation = 5647,
		// Token: 0x0400022D RID: 557
		UserDefinedLogicalNegation = 5672,
		// Token: 0x0400022E RID: 558
		LiftedBoolLogicalNegation = 71183,
		// Token: 0x0400022F RID: 559
		LiftedUserDefinedLogicalNegation = 71208,
		// Token: 0x04000230 RID: 560
		DynamicLogicalNegation = 5673,
		// Token: 0x04000231 RID: 561
		IntBitwiseComplement = 5893,
		// Token: 0x04000232 RID: 562
		UIntBitwiseComplement = 5894,
		// Token: 0x04000233 RID: 563
		LongBitwiseComplement = 5895,
		// Token: 0x04000234 RID: 564
		ULongBitwiseComplement = 5896,
		// Token: 0x04000235 RID: 565
		NIntBitwiseComplement = 5897,
		// Token: 0x04000236 RID: 566
		NUIntBitwiseComplement = 5898,
		// Token: 0x04000237 RID: 567
		EnumBitwiseComplement = 5908,
		// Token: 0x04000238 RID: 568
		UserDefinedBitwiseComplement = 5928,
		// Token: 0x04000239 RID: 569
		LiftedIntBitwiseComplement = 71429,
		// Token: 0x0400023A RID: 570
		LiftedUIntBitwiseComplement = 71430,
		// Token: 0x0400023B RID: 571
		LiftedLongBitwiseComplement = 71431,
		// Token: 0x0400023C RID: 572
		LiftedULongBitwiseComplement = 71432,
		// Token: 0x0400023D RID: 573
		LiftedNIntBitwiseComplement = 71433,
		// Token: 0x0400023E RID: 574
		LiftedNUIntBitwiseComplement = 71434,
		// Token: 0x0400023F RID: 575
		LiftedEnumBitwiseComplement = 71444,
		// Token: 0x04000240 RID: 576
		LiftedUserDefinedBitwiseComplement = 71464,
		// Token: 0x04000241 RID: 577
		DynamicBitwiseComplement = 5929,
		// Token: 0x04000242 RID: 578
		UserDefinedTrue = 6184,
		// Token: 0x04000243 RID: 579
		UserDefinedFalse = 6440,
		// Token: 0x04000244 RID: 580
		DynamicTrue = 6185,
		// Token: 0x04000245 RID: 581
		DynamicFalse = 6441
	}
}
