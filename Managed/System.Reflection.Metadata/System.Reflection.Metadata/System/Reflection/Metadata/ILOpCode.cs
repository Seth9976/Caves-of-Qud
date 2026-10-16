using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000049 RID: 73
	public enum ILOpCode : ushort
	{
		// Token: 0x040001C5 RID: 453
		Nop,
		// Token: 0x040001C6 RID: 454
		Break,
		// Token: 0x040001C7 RID: 455
		Ldarg_0,
		// Token: 0x040001C8 RID: 456
		Ldarg_1,
		// Token: 0x040001C9 RID: 457
		Ldarg_2,
		// Token: 0x040001CA RID: 458
		Ldarg_3,
		// Token: 0x040001CB RID: 459
		Ldloc_0,
		// Token: 0x040001CC RID: 460
		Ldloc_1,
		// Token: 0x040001CD RID: 461
		Ldloc_2,
		// Token: 0x040001CE RID: 462
		Ldloc_3,
		// Token: 0x040001CF RID: 463
		Stloc_0,
		// Token: 0x040001D0 RID: 464
		Stloc_1,
		// Token: 0x040001D1 RID: 465
		Stloc_2,
		// Token: 0x040001D2 RID: 466
		Stloc_3,
		// Token: 0x040001D3 RID: 467
		Ldarg_s,
		// Token: 0x040001D4 RID: 468
		Ldarga_s,
		// Token: 0x040001D5 RID: 469
		Starg_s,
		// Token: 0x040001D6 RID: 470
		Ldloc_s,
		// Token: 0x040001D7 RID: 471
		Ldloca_s,
		// Token: 0x040001D8 RID: 472
		Stloc_s,
		// Token: 0x040001D9 RID: 473
		Ldnull,
		// Token: 0x040001DA RID: 474
		Ldc_i4_m1,
		// Token: 0x040001DB RID: 475
		Ldc_i4_0,
		// Token: 0x040001DC RID: 476
		Ldc_i4_1,
		// Token: 0x040001DD RID: 477
		Ldc_i4_2,
		// Token: 0x040001DE RID: 478
		Ldc_i4_3,
		// Token: 0x040001DF RID: 479
		Ldc_i4_4,
		// Token: 0x040001E0 RID: 480
		Ldc_i4_5,
		// Token: 0x040001E1 RID: 481
		Ldc_i4_6,
		// Token: 0x040001E2 RID: 482
		Ldc_i4_7,
		// Token: 0x040001E3 RID: 483
		Ldc_i4_8,
		// Token: 0x040001E4 RID: 484
		Ldc_i4_s,
		// Token: 0x040001E5 RID: 485
		Ldc_i4,
		// Token: 0x040001E6 RID: 486
		Ldc_i8,
		// Token: 0x040001E7 RID: 487
		Ldc_r4,
		// Token: 0x040001E8 RID: 488
		Ldc_r8,
		// Token: 0x040001E9 RID: 489
		Dup = 37,
		// Token: 0x040001EA RID: 490
		Pop,
		// Token: 0x040001EB RID: 491
		Jmp,
		// Token: 0x040001EC RID: 492
		Call,
		// Token: 0x040001ED RID: 493
		Calli,
		// Token: 0x040001EE RID: 494
		Ret,
		// Token: 0x040001EF RID: 495
		Br_s,
		// Token: 0x040001F0 RID: 496
		Brfalse_s,
		// Token: 0x040001F1 RID: 497
		Brtrue_s,
		// Token: 0x040001F2 RID: 498
		Beq_s,
		// Token: 0x040001F3 RID: 499
		Bge_s,
		// Token: 0x040001F4 RID: 500
		Bgt_s,
		// Token: 0x040001F5 RID: 501
		Ble_s,
		// Token: 0x040001F6 RID: 502
		Blt_s,
		// Token: 0x040001F7 RID: 503
		Bne_un_s,
		// Token: 0x040001F8 RID: 504
		Bge_un_s,
		// Token: 0x040001F9 RID: 505
		Bgt_un_s,
		// Token: 0x040001FA RID: 506
		Ble_un_s,
		// Token: 0x040001FB RID: 507
		Blt_un_s,
		// Token: 0x040001FC RID: 508
		Br,
		// Token: 0x040001FD RID: 509
		Brfalse,
		// Token: 0x040001FE RID: 510
		Brtrue,
		// Token: 0x040001FF RID: 511
		Beq,
		// Token: 0x04000200 RID: 512
		Bge,
		// Token: 0x04000201 RID: 513
		Bgt,
		// Token: 0x04000202 RID: 514
		Ble,
		// Token: 0x04000203 RID: 515
		Blt,
		// Token: 0x04000204 RID: 516
		Bne_un,
		// Token: 0x04000205 RID: 517
		Bge_un,
		// Token: 0x04000206 RID: 518
		Bgt_un,
		// Token: 0x04000207 RID: 519
		Ble_un,
		// Token: 0x04000208 RID: 520
		Blt_un,
		// Token: 0x04000209 RID: 521
		Switch,
		// Token: 0x0400020A RID: 522
		Ldind_i1,
		// Token: 0x0400020B RID: 523
		Ldind_u1,
		// Token: 0x0400020C RID: 524
		Ldind_i2,
		// Token: 0x0400020D RID: 525
		Ldind_u2,
		// Token: 0x0400020E RID: 526
		Ldind_i4,
		// Token: 0x0400020F RID: 527
		Ldind_u4,
		// Token: 0x04000210 RID: 528
		Ldind_i8,
		// Token: 0x04000211 RID: 529
		Ldind_i,
		// Token: 0x04000212 RID: 530
		Ldind_r4,
		// Token: 0x04000213 RID: 531
		Ldind_r8,
		// Token: 0x04000214 RID: 532
		Ldind_ref,
		// Token: 0x04000215 RID: 533
		Stind_ref,
		// Token: 0x04000216 RID: 534
		Stind_i1,
		// Token: 0x04000217 RID: 535
		Stind_i2,
		// Token: 0x04000218 RID: 536
		Stind_i4,
		// Token: 0x04000219 RID: 537
		Stind_i8,
		// Token: 0x0400021A RID: 538
		Stind_r4,
		// Token: 0x0400021B RID: 539
		Stind_r8,
		// Token: 0x0400021C RID: 540
		Add,
		// Token: 0x0400021D RID: 541
		Sub,
		// Token: 0x0400021E RID: 542
		Mul,
		// Token: 0x0400021F RID: 543
		Div,
		// Token: 0x04000220 RID: 544
		Div_un,
		// Token: 0x04000221 RID: 545
		Rem,
		// Token: 0x04000222 RID: 546
		Rem_un,
		// Token: 0x04000223 RID: 547
		And,
		// Token: 0x04000224 RID: 548
		Or,
		// Token: 0x04000225 RID: 549
		Xor,
		// Token: 0x04000226 RID: 550
		Shl,
		// Token: 0x04000227 RID: 551
		Shr,
		// Token: 0x04000228 RID: 552
		Shr_un,
		// Token: 0x04000229 RID: 553
		Neg,
		// Token: 0x0400022A RID: 554
		Not,
		// Token: 0x0400022B RID: 555
		Conv_i1,
		// Token: 0x0400022C RID: 556
		Conv_i2,
		// Token: 0x0400022D RID: 557
		Conv_i4,
		// Token: 0x0400022E RID: 558
		Conv_i8,
		// Token: 0x0400022F RID: 559
		Conv_r4,
		// Token: 0x04000230 RID: 560
		Conv_r8,
		// Token: 0x04000231 RID: 561
		Conv_u4,
		// Token: 0x04000232 RID: 562
		Conv_u8,
		// Token: 0x04000233 RID: 563
		Callvirt,
		// Token: 0x04000234 RID: 564
		Cpobj,
		// Token: 0x04000235 RID: 565
		Ldobj,
		// Token: 0x04000236 RID: 566
		Ldstr,
		// Token: 0x04000237 RID: 567
		Newobj,
		// Token: 0x04000238 RID: 568
		Castclass,
		// Token: 0x04000239 RID: 569
		Isinst,
		// Token: 0x0400023A RID: 570
		Conv_r_un,
		// Token: 0x0400023B RID: 571
		Unbox = 121,
		// Token: 0x0400023C RID: 572
		Throw,
		// Token: 0x0400023D RID: 573
		Ldfld,
		// Token: 0x0400023E RID: 574
		Ldflda,
		// Token: 0x0400023F RID: 575
		Stfld,
		// Token: 0x04000240 RID: 576
		Ldsfld,
		// Token: 0x04000241 RID: 577
		Ldsflda,
		// Token: 0x04000242 RID: 578
		Stsfld,
		// Token: 0x04000243 RID: 579
		Stobj,
		// Token: 0x04000244 RID: 580
		Conv_ovf_i1_un,
		// Token: 0x04000245 RID: 581
		Conv_ovf_i2_un,
		// Token: 0x04000246 RID: 582
		Conv_ovf_i4_un,
		// Token: 0x04000247 RID: 583
		Conv_ovf_i8_un,
		// Token: 0x04000248 RID: 584
		Conv_ovf_u1_un,
		// Token: 0x04000249 RID: 585
		Conv_ovf_u2_un,
		// Token: 0x0400024A RID: 586
		Conv_ovf_u4_un,
		// Token: 0x0400024B RID: 587
		Conv_ovf_u8_un,
		// Token: 0x0400024C RID: 588
		Conv_ovf_i_un,
		// Token: 0x0400024D RID: 589
		Conv_ovf_u_un,
		// Token: 0x0400024E RID: 590
		Box,
		// Token: 0x0400024F RID: 591
		Newarr,
		// Token: 0x04000250 RID: 592
		Ldlen,
		// Token: 0x04000251 RID: 593
		Ldelema,
		// Token: 0x04000252 RID: 594
		Ldelem_i1,
		// Token: 0x04000253 RID: 595
		Ldelem_u1,
		// Token: 0x04000254 RID: 596
		Ldelem_i2,
		// Token: 0x04000255 RID: 597
		Ldelem_u2,
		// Token: 0x04000256 RID: 598
		Ldelem_i4,
		// Token: 0x04000257 RID: 599
		Ldelem_u4,
		// Token: 0x04000258 RID: 600
		Ldelem_i8,
		// Token: 0x04000259 RID: 601
		Ldelem_i,
		// Token: 0x0400025A RID: 602
		Ldelem_r4,
		// Token: 0x0400025B RID: 603
		Ldelem_r8,
		// Token: 0x0400025C RID: 604
		Ldelem_ref,
		// Token: 0x0400025D RID: 605
		Stelem_i,
		// Token: 0x0400025E RID: 606
		Stelem_i1,
		// Token: 0x0400025F RID: 607
		Stelem_i2,
		// Token: 0x04000260 RID: 608
		Stelem_i4,
		// Token: 0x04000261 RID: 609
		Stelem_i8,
		// Token: 0x04000262 RID: 610
		Stelem_r4,
		// Token: 0x04000263 RID: 611
		Stelem_r8,
		// Token: 0x04000264 RID: 612
		Stelem_ref,
		// Token: 0x04000265 RID: 613
		Ldelem,
		// Token: 0x04000266 RID: 614
		Stelem,
		// Token: 0x04000267 RID: 615
		Unbox_any,
		// Token: 0x04000268 RID: 616
		Conv_ovf_i1 = 179,
		// Token: 0x04000269 RID: 617
		Conv_ovf_u1,
		// Token: 0x0400026A RID: 618
		Conv_ovf_i2,
		// Token: 0x0400026B RID: 619
		Conv_ovf_u2,
		// Token: 0x0400026C RID: 620
		Conv_ovf_i4,
		// Token: 0x0400026D RID: 621
		Conv_ovf_u4,
		// Token: 0x0400026E RID: 622
		Conv_ovf_i8,
		// Token: 0x0400026F RID: 623
		Conv_ovf_u8,
		// Token: 0x04000270 RID: 624
		Refanyval = 194,
		// Token: 0x04000271 RID: 625
		Ckfinite,
		// Token: 0x04000272 RID: 626
		Mkrefany = 198,
		// Token: 0x04000273 RID: 627
		Ldtoken = 208,
		// Token: 0x04000274 RID: 628
		Conv_u2,
		// Token: 0x04000275 RID: 629
		Conv_u1,
		// Token: 0x04000276 RID: 630
		Conv_i,
		// Token: 0x04000277 RID: 631
		Conv_ovf_i,
		// Token: 0x04000278 RID: 632
		Conv_ovf_u,
		// Token: 0x04000279 RID: 633
		Add_ovf,
		// Token: 0x0400027A RID: 634
		Add_ovf_un,
		// Token: 0x0400027B RID: 635
		Mul_ovf,
		// Token: 0x0400027C RID: 636
		Mul_ovf_un,
		// Token: 0x0400027D RID: 637
		Sub_ovf,
		// Token: 0x0400027E RID: 638
		Sub_ovf_un,
		// Token: 0x0400027F RID: 639
		Endfinally,
		// Token: 0x04000280 RID: 640
		Leave,
		// Token: 0x04000281 RID: 641
		Leave_s,
		// Token: 0x04000282 RID: 642
		Stind_i,
		// Token: 0x04000283 RID: 643
		Conv_u,
		// Token: 0x04000284 RID: 644
		Arglist = 65024,
		// Token: 0x04000285 RID: 645
		Ceq,
		// Token: 0x04000286 RID: 646
		Cgt,
		// Token: 0x04000287 RID: 647
		Cgt_un,
		// Token: 0x04000288 RID: 648
		Clt,
		// Token: 0x04000289 RID: 649
		Clt_un,
		// Token: 0x0400028A RID: 650
		Ldftn,
		// Token: 0x0400028B RID: 651
		Ldvirtftn,
		// Token: 0x0400028C RID: 652
		Ldarg = 65033,
		// Token: 0x0400028D RID: 653
		Ldarga,
		// Token: 0x0400028E RID: 654
		Starg,
		// Token: 0x0400028F RID: 655
		Ldloc,
		// Token: 0x04000290 RID: 656
		Ldloca,
		// Token: 0x04000291 RID: 657
		Stloc,
		// Token: 0x04000292 RID: 658
		Localloc,
		// Token: 0x04000293 RID: 659
		Endfilter = 65041,
		// Token: 0x04000294 RID: 660
		Unaligned,
		// Token: 0x04000295 RID: 661
		Volatile,
		// Token: 0x04000296 RID: 662
		Tail,
		// Token: 0x04000297 RID: 663
		Initobj,
		// Token: 0x04000298 RID: 664
		Constrained,
		// Token: 0x04000299 RID: 665
		Cpblk,
		// Token: 0x0400029A RID: 666
		Initblk,
		// Token: 0x0400029B RID: 667
		Rethrow = 65050,
		// Token: 0x0400029C RID: 668
		Sizeof = 65052,
		// Token: 0x0400029D RID: 669
		Refanytype,
		// Token: 0x0400029E RID: 670
		Readonly
	}
}
