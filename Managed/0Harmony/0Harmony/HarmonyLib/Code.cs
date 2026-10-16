using System;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x020000A2 RID: 162
	public static class Code
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00010F5D File Offset: 0x0000F15D
		public static Code.Operand_ Operand
		{
			get
			{
				return new Code.Operand_();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00010F64 File Offset: 0x0000F164
		public static Code.Nop_ Nop
		{
			get
			{
				return new Code.Nop_
				{
					opcode = OpCodes.Nop
				};
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00010F76 File Offset: 0x0000F176
		public static Code.Break_ Break
		{
			get
			{
				return new Code.Break_
				{
					opcode = OpCodes.Break
				};
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00010F88 File Offset: 0x0000F188
		public static Code.Ldarg_0_ Ldarg_0
		{
			get
			{
				return new Code.Ldarg_0_
				{
					opcode = OpCodes.Ldarg_0
				};
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00010F9A File Offset: 0x0000F19A
		public static Code.Ldarg_1_ Ldarg_1
		{
			get
			{
				return new Code.Ldarg_1_
				{
					opcode = OpCodes.Ldarg_1
				};
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00010FAC File Offset: 0x0000F1AC
		public static Code.Ldarg_2_ Ldarg_2
		{
			get
			{
				return new Code.Ldarg_2_
				{
					opcode = OpCodes.Ldarg_2
				};
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00010FBE File Offset: 0x0000F1BE
		public static Code.Ldarg_3_ Ldarg_3
		{
			get
			{
				return new Code.Ldarg_3_
				{
					opcode = OpCodes.Ldarg_3
				};
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		public static Code.Ldloc_0_ Ldloc_0
		{
			get
			{
				return new Code.Ldloc_0_
				{
					opcode = OpCodes.Ldloc_0
				};
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00010FE2 File Offset: 0x0000F1E2
		public static Code.Ldloc_1_ Ldloc_1
		{
			get
			{
				return new Code.Ldloc_1_
				{
					opcode = OpCodes.Ldloc_1
				};
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public static Code.Ldloc_2_ Ldloc_2
		{
			get
			{
				return new Code.Ldloc_2_
				{
					opcode = OpCodes.Ldloc_2
				};
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00011006 File Offset: 0x0000F206
		public static Code.Ldloc_3_ Ldloc_3
		{
			get
			{
				return new Code.Ldloc_3_
				{
					opcode = OpCodes.Ldloc_3
				};
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011018 File Offset: 0x0000F218
		public static Code.Stloc_0_ Stloc_0
		{
			get
			{
				return new Code.Stloc_0_
				{
					opcode = OpCodes.Stloc_0
				};
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0001102A File Offset: 0x0000F22A
		public static Code.Stloc_1_ Stloc_1
		{
			get
			{
				return new Code.Stloc_1_
				{
					opcode = OpCodes.Stloc_1
				};
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001103C File Offset: 0x0000F23C
		public static Code.Stloc_2_ Stloc_2
		{
			get
			{
				return new Code.Stloc_2_
				{
					opcode = OpCodes.Stloc_2
				};
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0001104E File Offset: 0x0000F24E
		public static Code.Stloc_3_ Stloc_3
		{
			get
			{
				return new Code.Stloc_3_
				{
					opcode = OpCodes.Stloc_3
				};
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00011060 File Offset: 0x0000F260
		public static Code.Ldarg_S_ Ldarg_S
		{
			get
			{
				return new Code.Ldarg_S_
				{
					opcode = OpCodes.Ldarg_S
				};
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00011072 File Offset: 0x0000F272
		public static Code.Ldarga_S_ Ldarga_S
		{
			get
			{
				return new Code.Ldarga_S_
				{
					opcode = OpCodes.Ldarga_S
				};
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00011084 File Offset: 0x0000F284
		public static Code.Starg_S_ Starg_S
		{
			get
			{
				return new Code.Starg_S_
				{
					opcode = OpCodes.Starg_S
				};
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00011096 File Offset: 0x0000F296
		public static Code.Ldloc_S_ Ldloc_S
		{
			get
			{
				return new Code.Ldloc_S_
				{
					opcode = OpCodes.Ldloc_S
				};
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public static Code.Ldloca_S_ Ldloca_S
		{
			get
			{
				return new Code.Ldloca_S_
				{
					opcode = OpCodes.Ldloca_S
				};
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000110BA File Offset: 0x0000F2BA
		public static Code.Stloc_S_ Stloc_S
		{
			get
			{
				return new Code.Stloc_S_
				{
					opcode = OpCodes.Stloc_S
				};
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000110CC File Offset: 0x0000F2CC
		public static Code.Ldnull_ Ldnull
		{
			get
			{
				return new Code.Ldnull_
				{
					opcode = OpCodes.Ldnull
				};
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000110DE File Offset: 0x0000F2DE
		public static Code.Ldc_I4_M1_ Ldc_I4_M1
		{
			get
			{
				return new Code.Ldc_I4_M1_
				{
					opcode = OpCodes.Ldc_I4_M1
				};
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public static Code.Ldc_I4_0_ Ldc_I4_0
		{
			get
			{
				return new Code.Ldc_I4_0_
				{
					opcode = OpCodes.Ldc_I4_0
				};
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00011102 File Offset: 0x0000F302
		public static Code.Ldc_I4_1_ Ldc_I4_1
		{
			get
			{
				return new Code.Ldc_I4_1_
				{
					opcode = OpCodes.Ldc_I4_1
				};
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00011114 File Offset: 0x0000F314
		public static Code.Ldc_I4_2_ Ldc_I4_2
		{
			get
			{
				return new Code.Ldc_I4_2_
				{
					opcode = OpCodes.Ldc_I4_2
				};
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011126 File Offset: 0x0000F326
		public static Code.Ldc_I4_3_ Ldc_I4_3
		{
			get
			{
				return new Code.Ldc_I4_3_
				{
					opcode = OpCodes.Ldc_I4_3
				};
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00011138 File Offset: 0x0000F338
		public static Code.Ldc_I4_4_ Ldc_I4_4
		{
			get
			{
				return new Code.Ldc_I4_4_
				{
					opcode = OpCodes.Ldc_I4_4
				};
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001114A File Offset: 0x0000F34A
		public static Code.Ldc_I4_5_ Ldc_I4_5
		{
			get
			{
				return new Code.Ldc_I4_5_
				{
					opcode = OpCodes.Ldc_I4_5
				};
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001115C File Offset: 0x0000F35C
		public static Code.Ldc_I4_6_ Ldc_I4_6
		{
			get
			{
				return new Code.Ldc_I4_6_
				{
					opcode = OpCodes.Ldc_I4_6
				};
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001116E File Offset: 0x0000F36E
		public static Code.Ldc_I4_7_ Ldc_I4_7
		{
			get
			{
				return new Code.Ldc_I4_7_
				{
					opcode = OpCodes.Ldc_I4_7
				};
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011180 File Offset: 0x0000F380
		public static Code.Ldc_I4_8_ Ldc_I4_8
		{
			get
			{
				return new Code.Ldc_I4_8_
				{
					opcode = OpCodes.Ldc_I4_8
				};
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00011192 File Offset: 0x0000F392
		public static Code.Ldc_I4_S_ Ldc_I4_S
		{
			get
			{
				return new Code.Ldc_I4_S_
				{
					opcode = OpCodes.Ldc_I4_S
				};
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000111A4 File Offset: 0x0000F3A4
		public static Code.Ldc_I4_ Ldc_I4
		{
			get
			{
				return new Code.Ldc_I4_
				{
					opcode = OpCodes.Ldc_I4
				};
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000111B6 File Offset: 0x0000F3B6
		public static Code.Ldc_I8_ Ldc_I8
		{
			get
			{
				return new Code.Ldc_I8_
				{
					opcode = OpCodes.Ldc_I8
				};
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public static Code.Ldc_R4_ Ldc_R4
		{
			get
			{
				return new Code.Ldc_R4_
				{
					opcode = OpCodes.Ldc_R4
				};
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000111DA File Offset: 0x0000F3DA
		public static Code.Ldc_R8_ Ldc_R8
		{
			get
			{
				return new Code.Ldc_R8_
				{
					opcode = OpCodes.Ldc_R8
				};
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static Code.Dup_ Dup
		{
			get
			{
				return new Code.Dup_
				{
					opcode = OpCodes.Dup
				};
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000111FE File Offset: 0x0000F3FE
		public static Code.Pop_ Pop
		{
			get
			{
				return new Code.Pop_
				{
					opcode = OpCodes.Pop
				};
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00011210 File Offset: 0x0000F410
		public static Code.Jmp_ Jmp
		{
			get
			{
				return new Code.Jmp_
				{
					opcode = OpCodes.Jmp
				};
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00011222 File Offset: 0x0000F422
		public static Code.Call_ Call
		{
			get
			{
				return new Code.Call_
				{
					opcode = OpCodes.Call
				};
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00011234 File Offset: 0x0000F434
		public static Code.Calli_ Calli
		{
			get
			{
				return new Code.Calli_
				{
					opcode = OpCodes.Calli
				};
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00011246 File Offset: 0x0000F446
		public static Code.Ret_ Ret
		{
			get
			{
				return new Code.Ret_
				{
					opcode = OpCodes.Ret
				};
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00011258 File Offset: 0x0000F458
		public static Code.Br_S_ Br_S
		{
			get
			{
				return new Code.Br_S_
				{
					opcode = OpCodes.Br_S
				};
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001126A File Offset: 0x0000F46A
		public static Code.Brfalse_S_ Brfalse_S
		{
			get
			{
				return new Code.Brfalse_S_
				{
					opcode = OpCodes.Brfalse_S
				};
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0001127C File Offset: 0x0000F47C
		public static Code.Brtrue_S_ Brtrue_S
		{
			get
			{
				return new Code.Brtrue_S_
				{
					opcode = OpCodes.Brtrue_S
				};
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001128E File Offset: 0x0000F48E
		public static Code.Beq_S_ Beq_S
		{
			get
			{
				return new Code.Beq_S_
				{
					opcode = OpCodes.Beq_S
				};
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public static Code.Bge_S_ Bge_S
		{
			get
			{
				return new Code.Bge_S_
				{
					opcode = OpCodes.Bge_S
				};
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000112B2 File Offset: 0x0000F4B2
		public static Code.Bgt_S_ Bgt_S
		{
			get
			{
				return new Code.Bgt_S_
				{
					opcode = OpCodes.Bgt_S
				};
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000112C4 File Offset: 0x0000F4C4
		public static Code.Ble_S_ Ble_S
		{
			get
			{
				return new Code.Ble_S_
				{
					opcode = OpCodes.Ble_S
				};
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000112D6 File Offset: 0x0000F4D6
		public static Code.Blt_S_ Blt_S
		{
			get
			{
				return new Code.Blt_S_
				{
					opcode = OpCodes.Blt_S
				};
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public static Code.Bne_Un_S_ Bne_Un_S
		{
			get
			{
				return new Code.Bne_Un_S_
				{
					opcode = OpCodes.Bne_Un_S
				};
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000112FA File Offset: 0x0000F4FA
		public static Code.Bge_Un_S_ Bge_Un_S
		{
			get
			{
				return new Code.Bge_Un_S_
				{
					opcode = OpCodes.Bge_Un_S
				};
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001130C File Offset: 0x0000F50C
		public static Code.Bgt_Un_S_ Bgt_Un_S
		{
			get
			{
				return new Code.Bgt_Un_S_
				{
					opcode = OpCodes.Bgt_Un_S
				};
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001131E File Offset: 0x0000F51E
		public static Code.Ble_Un_S_ Ble_Un_S
		{
			get
			{
				return new Code.Ble_Un_S_
				{
					opcode = OpCodes.Ble_Un_S
				};
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00011330 File Offset: 0x0000F530
		public static Code.Blt_Un_S_ Blt_Un_S
		{
			get
			{
				return new Code.Blt_Un_S_
				{
					opcode = OpCodes.Blt_Un_S
				};
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00011342 File Offset: 0x0000F542
		public static Code.Br_ Br
		{
			get
			{
				return new Code.Br_
				{
					opcode = OpCodes.Br
				};
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00011354 File Offset: 0x0000F554
		public static Code.Brfalse_ Brfalse
		{
			get
			{
				return new Code.Brfalse_
				{
					opcode = OpCodes.Brfalse
				};
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011366 File Offset: 0x0000F566
		public static Code.Brtrue_ Brtrue
		{
			get
			{
				return new Code.Brtrue_
				{
					opcode = OpCodes.Brtrue
				};
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00011378 File Offset: 0x0000F578
		public static Code.Beq_ Beq
		{
			get
			{
				return new Code.Beq_
				{
					opcode = OpCodes.Beq
				};
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001138A File Offset: 0x0000F58A
		public static Code.Bge_ Bge
		{
			get
			{
				return new Code.Bge_
				{
					opcode = OpCodes.Bge
				};
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001139C File Offset: 0x0000F59C
		public static Code.Bgt_ Bgt
		{
			get
			{
				return new Code.Bgt_
				{
					opcode = OpCodes.Bgt
				};
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000113AE File Offset: 0x0000F5AE
		public static Code.Ble_ Ble
		{
			get
			{
				return new Code.Ble_
				{
					opcode = OpCodes.Ble
				};
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public static Code.Blt_ Blt
		{
			get
			{
				return new Code.Blt_
				{
					opcode = OpCodes.Blt
				};
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000113D2 File Offset: 0x0000F5D2
		public static Code.Bne_Un_ Bne_Un
		{
			get
			{
				return new Code.Bne_Un_
				{
					opcode = OpCodes.Bne_Un
				};
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public static Code.Bge_Un_ Bge_Un
		{
			get
			{
				return new Code.Bge_Un_
				{
					opcode = OpCodes.Bge_Un
				};
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000113F6 File Offset: 0x0000F5F6
		public static Code.Bgt_Un_ Bgt_Un
		{
			get
			{
				return new Code.Bgt_Un_
				{
					opcode = OpCodes.Bgt_Un
				};
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011408 File Offset: 0x0000F608
		public static Code.Ble_Un_ Ble_Un
		{
			get
			{
				return new Code.Ble_Un_
				{
					opcode = OpCodes.Ble_Un
				};
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001141A File Offset: 0x0000F61A
		public static Code.Blt_Un_ Blt_Un
		{
			get
			{
				return new Code.Blt_Un_
				{
					opcode = OpCodes.Blt_Un
				};
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001142C File Offset: 0x0000F62C
		public static Code.Switch_ Switch
		{
			get
			{
				return new Code.Switch_
				{
					opcode = OpCodes.Switch
				};
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001143E File Offset: 0x0000F63E
		public static Code.Ldind_I1_ Ldind_I1
		{
			get
			{
				return new Code.Ldind_I1_
				{
					opcode = OpCodes.Ldind_I1
				};
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00011450 File Offset: 0x0000F650
		public static Code.Ldind_U1_ Ldind_U1
		{
			get
			{
				return new Code.Ldind_U1_
				{
					opcode = OpCodes.Ldind_U1
				};
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00011462 File Offset: 0x0000F662
		public static Code.Ldind_I2_ Ldind_I2
		{
			get
			{
				return new Code.Ldind_I2_
				{
					opcode = OpCodes.Ldind_I2
				};
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011474 File Offset: 0x0000F674
		public static Code.Ldind_U2_ Ldind_U2
		{
			get
			{
				return new Code.Ldind_U2_
				{
					opcode = OpCodes.Ldind_U2
				};
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00011486 File Offset: 0x0000F686
		public static Code.Ldind_I4_ Ldind_I4
		{
			get
			{
				return new Code.Ldind_I4_
				{
					opcode = OpCodes.Ldind_I4
				};
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011498 File Offset: 0x0000F698
		public static Code.Ldind_U4_ Ldind_U4
		{
			get
			{
				return new Code.Ldind_U4_
				{
					opcode = OpCodes.Ldind_U4
				};
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000114AA File Offset: 0x0000F6AA
		public static Code.Ldind_I8_ Ldind_I8
		{
			get
			{
				return new Code.Ldind_I8_
				{
					opcode = OpCodes.Ldind_I8
				};
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000114BC File Offset: 0x0000F6BC
		public static Code.Ldind_I_ Ldind_I
		{
			get
			{
				return new Code.Ldind_I_
				{
					opcode = OpCodes.Ldind_I
				};
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000114CE File Offset: 0x0000F6CE
		public static Code.Ldind_R4_ Ldind_R4
		{
			get
			{
				return new Code.Ldind_R4_
				{
					opcode = OpCodes.Ldind_R4
				};
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public static Code.Ldind_R8_ Ldind_R8
		{
			get
			{
				return new Code.Ldind_R8_
				{
					opcode = OpCodes.Ldind_R8
				};
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000114F2 File Offset: 0x0000F6F2
		public static Code.Ldind_Ref_ Ldind_Ref
		{
			get
			{
				return new Code.Ldind_Ref_
				{
					opcode = OpCodes.Ldind_Ref
				};
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00011504 File Offset: 0x0000F704
		public static Code.Stind_Ref_ Stind_Ref
		{
			get
			{
				return new Code.Stind_Ref_
				{
					opcode = OpCodes.Stind_Ref
				};
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00011516 File Offset: 0x0000F716
		public static Code.Stind_I1_ Stind_I1
		{
			get
			{
				return new Code.Stind_I1_
				{
					opcode = OpCodes.Stind_I1
				};
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00011528 File Offset: 0x0000F728
		public static Code.Stind_I2_ Stind_I2
		{
			get
			{
				return new Code.Stind_I2_
				{
					opcode = OpCodes.Stind_I2
				};
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001153A File Offset: 0x0000F73A
		public static Code.Stind_I4_ Stind_I4
		{
			get
			{
				return new Code.Stind_I4_
				{
					opcode = OpCodes.Stind_I4
				};
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001154C File Offset: 0x0000F74C
		public static Code.Stind_I8_ Stind_I8
		{
			get
			{
				return new Code.Stind_I8_
				{
					opcode = OpCodes.Stind_I8
				};
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001155E File Offset: 0x0000F75E
		public static Code.Stind_R4_ Stind_R4
		{
			get
			{
				return new Code.Stind_R4_
				{
					opcode = OpCodes.Stind_R4
				};
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00011570 File Offset: 0x0000F770
		public static Code.Stind_R8_ Stind_R8
		{
			get
			{
				return new Code.Stind_R8_
				{
					opcode = OpCodes.Stind_R8
				};
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00011582 File Offset: 0x0000F782
		public static Code.Add_ Add
		{
			get
			{
				return new Code.Add_
				{
					opcode = OpCodes.Add
				};
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011594 File Offset: 0x0000F794
		public static Code.Sub_ Sub
		{
			get
			{
				return new Code.Sub_
				{
					opcode = OpCodes.Sub
				};
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000115A6 File Offset: 0x0000F7A6
		public static Code.Mul_ Mul
		{
			get
			{
				return new Code.Mul_
				{
					opcode = OpCodes.Mul
				};
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000115B8 File Offset: 0x0000F7B8
		public static Code.Div_ Div
		{
			get
			{
				return new Code.Div_
				{
					opcode = OpCodes.Div
				};
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000115CA File Offset: 0x0000F7CA
		public static Code.Div_Un_ Div_Un
		{
			get
			{
				return new Code.Div_Un_
				{
					opcode = OpCodes.Div_Un
				};
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000115DC File Offset: 0x0000F7DC
		public static Code.Rem_ Rem
		{
			get
			{
				return new Code.Rem_
				{
					opcode = OpCodes.Rem
				};
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000115EE File Offset: 0x0000F7EE
		public static Code.Rem_Un_ Rem_Un
		{
			get
			{
				return new Code.Rem_Un_
				{
					opcode = OpCodes.Rem_Un
				};
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00011600 File Offset: 0x0000F800
		public static Code.And_ And
		{
			get
			{
				return new Code.And_
				{
					opcode = OpCodes.And
				};
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011612 File Offset: 0x0000F812
		public static Code.Or_ Or
		{
			get
			{
				return new Code.Or_
				{
					opcode = OpCodes.Or
				};
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00011624 File Offset: 0x0000F824
		public static Code.Xor_ Xor
		{
			get
			{
				return new Code.Xor_
				{
					opcode = OpCodes.Xor
				};
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011636 File Offset: 0x0000F836
		public static Code.Shl_ Shl
		{
			get
			{
				return new Code.Shl_
				{
					opcode = OpCodes.Shl
				};
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00011648 File Offset: 0x0000F848
		public static Code.Shr_ Shr
		{
			get
			{
				return new Code.Shr_
				{
					opcode = OpCodes.Shr
				};
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001165A File Offset: 0x0000F85A
		public static Code.Shr_Un_ Shr_Un
		{
			get
			{
				return new Code.Shr_Un_
				{
					opcode = OpCodes.Shr_Un
				};
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001166C File Offset: 0x0000F86C
		public static Code.Neg_ Neg
		{
			get
			{
				return new Code.Neg_
				{
					opcode = OpCodes.Neg
				};
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001167E File Offset: 0x0000F87E
		public static Code.Not_ Not
		{
			get
			{
				return new Code.Not_
				{
					opcode = OpCodes.Not
				};
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00011690 File Offset: 0x0000F890
		public static Code.Conv_I1_ Conv_I1
		{
			get
			{
				return new Code.Conv_I1_
				{
					opcode = OpCodes.Conv_I1
				};
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000116A2 File Offset: 0x0000F8A2
		public static Code.Conv_I2_ Conv_I2
		{
			get
			{
				return new Code.Conv_I2_
				{
					opcode = OpCodes.Conv_I2
				};
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public static Code.Conv_I4_ Conv_I4
		{
			get
			{
				return new Code.Conv_I4_
				{
					opcode = OpCodes.Conv_I4
				};
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000116C6 File Offset: 0x0000F8C6
		public static Code.Conv_I8_ Conv_I8
		{
			get
			{
				return new Code.Conv_I8_
				{
					opcode = OpCodes.Conv_I8
				};
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public static Code.Conv_R4_ Conv_R4
		{
			get
			{
				return new Code.Conv_R4_
				{
					opcode = OpCodes.Conv_R4
				};
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000116EA File Offset: 0x0000F8EA
		public static Code.Conv_R8_ Conv_R8
		{
			get
			{
				return new Code.Conv_R8_
				{
					opcode = OpCodes.Conv_R8
				};
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000116FC File Offset: 0x0000F8FC
		public static Code.Conv_U4_ Conv_U4
		{
			get
			{
				return new Code.Conv_U4_
				{
					opcode = OpCodes.Conv_U4
				};
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001170E File Offset: 0x0000F90E
		public static Code.Conv_U8_ Conv_U8
		{
			get
			{
				return new Code.Conv_U8_
				{
					opcode = OpCodes.Conv_U8
				};
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00011720 File Offset: 0x0000F920
		public static Code.Callvirt_ Callvirt
		{
			get
			{
				return new Code.Callvirt_
				{
					opcode = OpCodes.Callvirt
				};
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00011732 File Offset: 0x0000F932
		public static Code.Cpobj_ Cpobj
		{
			get
			{
				return new Code.Cpobj_
				{
					opcode = OpCodes.Cpobj
				};
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011744 File Offset: 0x0000F944
		public static Code.Ldobj_ Ldobj
		{
			get
			{
				return new Code.Ldobj_
				{
					opcode = OpCodes.Ldobj
				};
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00011756 File Offset: 0x0000F956
		public static Code.Ldstr_ Ldstr
		{
			get
			{
				return new Code.Ldstr_
				{
					opcode = OpCodes.Ldstr
				};
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011768 File Offset: 0x0000F968
		public static Code.Newobj_ Newobj
		{
			get
			{
				return new Code.Newobj_
				{
					opcode = OpCodes.Newobj
				};
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001177A File Offset: 0x0000F97A
		public static Code.Castclass_ Castclass
		{
			get
			{
				return new Code.Castclass_
				{
					opcode = OpCodes.Castclass
				};
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001178C File Offset: 0x0000F98C
		public static Code.Isinst_ Isinst
		{
			get
			{
				return new Code.Isinst_
				{
					opcode = OpCodes.Isinst
				};
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001179E File Offset: 0x0000F99E
		public static Code.Conv_R_Un_ Conv_R_Un
		{
			get
			{
				return new Code.Conv_R_Un_
				{
					opcode = OpCodes.Conv_R_Un
				};
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000117B0 File Offset: 0x0000F9B0
		public static Code.Unbox_ Unbox
		{
			get
			{
				return new Code.Unbox_
				{
					opcode = OpCodes.Unbox
				};
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000117C2 File Offset: 0x0000F9C2
		public static Code.Throw_ Throw
		{
			get
			{
				return new Code.Throw_
				{
					opcode = OpCodes.Throw
				};
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public static Code.Ldfld_ Ldfld
		{
			get
			{
				return new Code.Ldfld_
				{
					opcode = OpCodes.Ldfld
				};
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000117E6 File Offset: 0x0000F9E6
		public static Code.Ldflda_ Ldflda
		{
			get
			{
				return new Code.Ldflda_
				{
					opcode = OpCodes.Ldflda
				};
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public static Code.Stfld_ Stfld
		{
			get
			{
				return new Code.Stfld_
				{
					opcode = OpCodes.Stfld
				};
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001180A File Offset: 0x0000FA0A
		public static Code.Ldsfld_ Ldsfld
		{
			get
			{
				return new Code.Ldsfld_
				{
					opcode = OpCodes.Ldsfld
				};
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001181C File Offset: 0x0000FA1C
		public static Code.Ldsflda_ Ldsflda
		{
			get
			{
				return new Code.Ldsflda_
				{
					opcode = OpCodes.Ldsflda
				};
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001182E File Offset: 0x0000FA2E
		public static Code.Stsfld_ Stsfld
		{
			get
			{
				return new Code.Stsfld_
				{
					opcode = OpCodes.Stsfld
				};
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011840 File Offset: 0x0000FA40
		public static Code.Stobj_ Stobj
		{
			get
			{
				return new Code.Stobj_
				{
					opcode = OpCodes.Stobj
				};
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00011852 File Offset: 0x0000FA52
		public static Code.Conv_Ovf_I1_Un_ Conv_Ovf_I1_Un
		{
			get
			{
				return new Code.Conv_Ovf_I1_Un_
				{
					opcode = OpCodes.Conv_Ovf_I1_Un
				};
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011864 File Offset: 0x0000FA64
		public static Code.Conv_Ovf_I2_Un_ Conv_Ovf_I2_Un
		{
			get
			{
				return new Code.Conv_Ovf_I2_Un_
				{
					opcode = OpCodes.Conv_Ovf_I2_Un
				};
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00011876 File Offset: 0x0000FA76
		public static Code.Conv_Ovf_I4_Un_ Conv_Ovf_I4_Un
		{
			get
			{
				return new Code.Conv_Ovf_I4_Un_
				{
					opcode = OpCodes.Conv_Ovf_I4_Un
				};
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011888 File Offset: 0x0000FA88
		public static Code.Conv_Ovf_I8_Un_ Conv_Ovf_I8_Un
		{
			get
			{
				return new Code.Conv_Ovf_I8_Un_
				{
					opcode = OpCodes.Conv_Ovf_I8_Un
				};
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001189A File Offset: 0x0000FA9A
		public static Code.Conv_Ovf_U1_Un_ Conv_Ovf_U1_Un
		{
			get
			{
				return new Code.Conv_Ovf_U1_Un_
				{
					opcode = OpCodes.Conv_Ovf_U1_Un
				};
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000118AC File Offset: 0x0000FAAC
		public static Code.Conv_Ovf_U2_Un_ Conv_Ovf_U2_Un
		{
			get
			{
				return new Code.Conv_Ovf_U2_Un_
				{
					opcode = OpCodes.Conv_Ovf_U2_Un
				};
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000118BE File Offset: 0x0000FABE
		public static Code.Conv_Ovf_U4_Un_ Conv_Ovf_U4_Un
		{
			get
			{
				return new Code.Conv_Ovf_U4_Un_
				{
					opcode = OpCodes.Conv_Ovf_U4_Un
				};
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public static Code.Conv_Ovf_U8_Un_ Conv_Ovf_U8_Un
		{
			get
			{
				return new Code.Conv_Ovf_U8_Un_
				{
					opcode = OpCodes.Conv_Ovf_U8_Un
				};
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000118E2 File Offset: 0x0000FAE2
		public static Code.Conv_Ovf_I_Un_ Conv_Ovf_I_Un
		{
			get
			{
				return new Code.Conv_Ovf_I_Un_
				{
					opcode = OpCodes.Conv_Ovf_I_Un
				};
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000118F4 File Offset: 0x0000FAF4
		public static Code.Conv_Ovf_U_Un_ Conv_Ovf_U_Un
		{
			get
			{
				return new Code.Conv_Ovf_U_Un_
				{
					opcode = OpCodes.Conv_Ovf_U_Un
				};
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00011906 File Offset: 0x0000FB06
		public static Code.Box_ Box
		{
			get
			{
				return new Code.Box_
				{
					opcode = OpCodes.Box
				};
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00011918 File Offset: 0x0000FB18
		public static Code.Newarr_ Newarr
		{
			get
			{
				return new Code.Newarr_
				{
					opcode = OpCodes.Newarr
				};
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001192A File Offset: 0x0000FB2A
		public static Code.Ldlen_ Ldlen
		{
			get
			{
				return new Code.Ldlen_
				{
					opcode = OpCodes.Ldlen
				};
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001193C File Offset: 0x0000FB3C
		public static Code.Ldelema_ Ldelema
		{
			get
			{
				return new Code.Ldelema_
				{
					opcode = OpCodes.Ldelema
				};
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001194E File Offset: 0x0000FB4E
		public static Code.Ldelem_I1_ Ldelem_I1
		{
			get
			{
				return new Code.Ldelem_I1_
				{
					opcode = OpCodes.Ldelem_I1
				};
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00011960 File Offset: 0x0000FB60
		public static Code.Ldelem_U1_ Ldelem_U1
		{
			get
			{
				return new Code.Ldelem_U1_
				{
					opcode = OpCodes.Ldelem_U1
				};
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00011972 File Offset: 0x0000FB72
		public static Code.Ldelem_I2_ Ldelem_I2
		{
			get
			{
				return new Code.Ldelem_I2_
				{
					opcode = OpCodes.Ldelem_I2
				};
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011984 File Offset: 0x0000FB84
		public static Code.Ldelem_U2_ Ldelem_U2
		{
			get
			{
				return new Code.Ldelem_U2_
				{
					opcode = OpCodes.Ldelem_U2
				};
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00011996 File Offset: 0x0000FB96
		public static Code.Ldelem_I4_ Ldelem_I4
		{
			get
			{
				return new Code.Ldelem_I4_
				{
					opcode = OpCodes.Ldelem_I4
				};
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public static Code.Ldelem_U4_ Ldelem_U4
		{
			get
			{
				return new Code.Ldelem_U4_
				{
					opcode = OpCodes.Ldelem_U4
				};
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000119BA File Offset: 0x0000FBBA
		public static Code.Ldelem_I8_ Ldelem_I8
		{
			get
			{
				return new Code.Ldelem_I8_
				{
					opcode = OpCodes.Ldelem_I8
				};
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000119CC File Offset: 0x0000FBCC
		public static Code.Ldelem_I_ Ldelem_I
		{
			get
			{
				return new Code.Ldelem_I_
				{
					opcode = OpCodes.Ldelem_I
				};
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000119DE File Offset: 0x0000FBDE
		public static Code.Ldelem_R4_ Ldelem_R4
		{
			get
			{
				return new Code.Ldelem_R4_
				{
					opcode = OpCodes.Ldelem_R4
				};
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public static Code.Ldelem_R8_ Ldelem_R8
		{
			get
			{
				return new Code.Ldelem_R8_
				{
					opcode = OpCodes.Ldelem_R8
				};
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00011A02 File Offset: 0x0000FC02
		public static Code.Ldelem_Ref_ Ldelem_Ref
		{
			get
			{
				return new Code.Ldelem_Ref_
				{
					opcode = OpCodes.Ldelem_Ref
				};
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00011A14 File Offset: 0x0000FC14
		public static Code.Stelem_I_ Stelem_I
		{
			get
			{
				return new Code.Stelem_I_
				{
					opcode = OpCodes.Stelem_I
				};
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00011A26 File Offset: 0x0000FC26
		public static Code.Stelem_I1_ Stelem_I1
		{
			get
			{
				return new Code.Stelem_I1_
				{
					opcode = OpCodes.Stelem_I1
				};
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00011A38 File Offset: 0x0000FC38
		public static Code.Stelem_I2_ Stelem_I2
		{
			get
			{
				return new Code.Stelem_I2_
				{
					opcode = OpCodes.Stelem_I2
				};
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00011A4A File Offset: 0x0000FC4A
		public static Code.Stelem_I4_ Stelem_I4
		{
			get
			{
				return new Code.Stelem_I4_
				{
					opcode = OpCodes.Stelem_I4
				};
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public static Code.Stelem_I8_ Stelem_I8
		{
			get
			{
				return new Code.Stelem_I8_
				{
					opcode = OpCodes.Stelem_I8
				};
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011A6E File Offset: 0x0000FC6E
		public static Code.Stelem_R4_ Stelem_R4
		{
			get
			{
				return new Code.Stelem_R4_
				{
					opcode = OpCodes.Stelem_R4
				};
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00011A80 File Offset: 0x0000FC80
		public static Code.Stelem_R8_ Stelem_R8
		{
			get
			{
				return new Code.Stelem_R8_
				{
					opcode = OpCodes.Stelem_R8
				};
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00011A92 File Offset: 0x0000FC92
		public static Code.Stelem_Ref_ Stelem_Ref
		{
			get
			{
				return new Code.Stelem_Ref_
				{
					opcode = OpCodes.Stelem_Ref
				};
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public static Code.Ldelem_ Ldelem
		{
			get
			{
				return new Code.Ldelem_
				{
					opcode = OpCodes.Ldelem
				};
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00011AB6 File Offset: 0x0000FCB6
		public static Code.Stelem_ Stelem
		{
			get
			{
				return new Code.Stelem_
				{
					opcode = OpCodes.Stelem
				};
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public static Code.Unbox_Any_ Unbox_Any
		{
			get
			{
				return new Code.Unbox_Any_
				{
					opcode = OpCodes.Unbox_Any
				};
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00011ADA File Offset: 0x0000FCDA
		public static Code.Conv_Ovf_I1_ Conv_Ovf_I1
		{
			get
			{
				return new Code.Conv_Ovf_I1_
				{
					opcode = OpCodes.Conv_Ovf_I1
				};
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public static Code.Conv_Ovf_U1_ Conv_Ovf_U1
		{
			get
			{
				return new Code.Conv_Ovf_U1_
				{
					opcode = OpCodes.Conv_Ovf_U1
				};
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00011AFE File Offset: 0x0000FCFE
		public static Code.Conv_Ovf_I2_ Conv_Ovf_I2
		{
			get
			{
				return new Code.Conv_Ovf_I2_
				{
					opcode = OpCodes.Conv_Ovf_I2
				};
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00011B10 File Offset: 0x0000FD10
		public static Code.Conv_Ovf_U2_ Conv_Ovf_U2
		{
			get
			{
				return new Code.Conv_Ovf_U2_
				{
					opcode = OpCodes.Conv_Ovf_U2
				};
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00011B22 File Offset: 0x0000FD22
		public static Code.Conv_Ovf_I4_ Conv_Ovf_I4
		{
			get
			{
				return new Code.Conv_Ovf_I4_
				{
					opcode = OpCodes.Conv_Ovf_I4
				};
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00011B34 File Offset: 0x0000FD34
		public static Code.Conv_Ovf_U4_ Conv_Ovf_U4
		{
			get
			{
				return new Code.Conv_Ovf_U4_
				{
					opcode = OpCodes.Conv_Ovf_U4
				};
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00011B46 File Offset: 0x0000FD46
		public static Code.Conv_Ovf_I8_ Conv_Ovf_I8
		{
			get
			{
				return new Code.Conv_Ovf_I8_
				{
					opcode = OpCodes.Conv_Ovf_I8
				};
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00011B58 File Offset: 0x0000FD58
		public static Code.Conv_Ovf_U8_ Conv_Ovf_U8
		{
			get
			{
				return new Code.Conv_Ovf_U8_
				{
					opcode = OpCodes.Conv_Ovf_U8
				};
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00011B6A File Offset: 0x0000FD6A
		public static Code.Refanyval_ Refanyval
		{
			get
			{
				return new Code.Refanyval_
				{
					opcode = OpCodes.Refanyval
				};
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public static Code.Ckfinite_ Ckfinite
		{
			get
			{
				return new Code.Ckfinite_
				{
					opcode = OpCodes.Ckfinite
				};
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00011B8E File Offset: 0x0000FD8E
		public static Code.Mkrefany_ Mkrefany
		{
			get
			{
				return new Code.Mkrefany_
				{
					opcode = OpCodes.Mkrefany
				};
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public static Code.Ldtoken_ Ldtoken
		{
			get
			{
				return new Code.Ldtoken_
				{
					opcode = OpCodes.Ldtoken
				};
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		public static Code.Conv_U2_ Conv_U2
		{
			get
			{
				return new Code.Conv_U2_
				{
					opcode = OpCodes.Conv_U2
				};
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public static Code.Conv_U1_ Conv_U1
		{
			get
			{
				return new Code.Conv_U1_
				{
					opcode = OpCodes.Conv_U1
				};
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00011BD6 File Offset: 0x0000FDD6
		public static Code.Conv_I_ Conv_I
		{
			get
			{
				return new Code.Conv_I_
				{
					opcode = OpCodes.Conv_I
				};
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public static Code.Conv_Ovf_I_ Conv_Ovf_I
		{
			get
			{
				return new Code.Conv_Ovf_I_
				{
					opcode = OpCodes.Conv_Ovf_I
				};
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00011BFA File Offset: 0x0000FDFA
		public static Code.Conv_Ovf_U_ Conv_Ovf_U
		{
			get
			{
				return new Code.Conv_Ovf_U_
				{
					opcode = OpCodes.Conv_Ovf_U
				};
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00011C0C File Offset: 0x0000FE0C
		public static Code.Add_Ovf_ Add_Ovf
		{
			get
			{
				return new Code.Add_Ovf_
				{
					opcode = OpCodes.Add_Ovf
				};
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00011C1E File Offset: 0x0000FE1E
		public static Code.Add_Ovf_Un_ Add_Ovf_Un
		{
			get
			{
				return new Code.Add_Ovf_Un_
				{
					opcode = OpCodes.Add_Ovf_Un
				};
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00011C30 File Offset: 0x0000FE30
		public static Code.Mul_Ovf_ Mul_Ovf
		{
			get
			{
				return new Code.Mul_Ovf_
				{
					opcode = OpCodes.Mul_Ovf
				};
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00011C42 File Offset: 0x0000FE42
		public static Code.Mul_Ovf_Un_ Mul_Ovf_Un
		{
			get
			{
				return new Code.Mul_Ovf_Un_
				{
					opcode = OpCodes.Mul_Ovf_Un
				};
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00011C54 File Offset: 0x0000FE54
		public static Code.Sub_Ovf_ Sub_Ovf
		{
			get
			{
				return new Code.Sub_Ovf_
				{
					opcode = OpCodes.Sub_Ovf
				};
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00011C66 File Offset: 0x0000FE66
		public static Code.Sub_Ovf_Un_ Sub_Ovf_Un
		{
			get
			{
				return new Code.Sub_Ovf_Un_
				{
					opcode = OpCodes.Sub_Ovf_Un
				};
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00011C78 File Offset: 0x0000FE78
		public static Code.Endfinally_ Endfinally
		{
			get
			{
				return new Code.Endfinally_
				{
					opcode = OpCodes.Endfinally
				};
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00011C8A File Offset: 0x0000FE8A
		public static Code.Leave_ Leave
		{
			get
			{
				return new Code.Leave_
				{
					opcode = OpCodes.Leave
				};
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00011C9C File Offset: 0x0000FE9C
		public static Code.Leave_S_ Leave_S
		{
			get
			{
				return new Code.Leave_S_
				{
					opcode = OpCodes.Leave_S
				};
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00011CAE File Offset: 0x0000FEAE
		public static Code.Stind_I_ Stind_I
		{
			get
			{
				return new Code.Stind_I_
				{
					opcode = OpCodes.Stind_I
				};
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		public static Code.Conv_U_ Conv_U
		{
			get
			{
				return new Code.Conv_U_
				{
					opcode = OpCodes.Conv_U
				};
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00011CD2 File Offset: 0x0000FED2
		public static Code.Prefix7_ Prefix7
		{
			get
			{
				return new Code.Prefix7_
				{
					opcode = OpCodes.Prefix7
				};
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public static Code.Prefix6_ Prefix6
		{
			get
			{
				return new Code.Prefix6_
				{
					opcode = OpCodes.Prefix6
				};
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		public static Code.Prefix5_ Prefix5
		{
			get
			{
				return new Code.Prefix5_
				{
					opcode = OpCodes.Prefix5
				};
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00011D08 File Offset: 0x0000FF08
		public static Code.Prefix4_ Prefix4
		{
			get
			{
				return new Code.Prefix4_
				{
					opcode = OpCodes.Prefix4
				};
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00011D1A File Offset: 0x0000FF1A
		public static Code.Prefix3_ Prefix3
		{
			get
			{
				return new Code.Prefix3_
				{
					opcode = OpCodes.Prefix3
				};
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public static Code.Prefix2_ Prefix2
		{
			get
			{
				return new Code.Prefix2_
				{
					opcode = OpCodes.Prefix2
				};
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00011D3E File Offset: 0x0000FF3E
		public static Code.Prefix1_ Prefix1
		{
			get
			{
				return new Code.Prefix1_
				{
					opcode = OpCodes.Prefix1
				};
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00011D50 File Offset: 0x0000FF50
		public static Code.Prefixref_ Prefixref
		{
			get
			{
				return new Code.Prefixref_
				{
					opcode = OpCodes.Prefixref
				};
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00011D62 File Offset: 0x0000FF62
		public static Code.Arglist_ Arglist
		{
			get
			{
				return new Code.Arglist_
				{
					opcode = OpCodes.Arglist
				};
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00011D74 File Offset: 0x0000FF74
		public static Code.Ceq_ Ceq
		{
			get
			{
				return new Code.Ceq_
				{
					opcode = OpCodes.Ceq
				};
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00011D86 File Offset: 0x0000FF86
		public static Code.Cgt_ Cgt
		{
			get
			{
				return new Code.Cgt_
				{
					opcode = OpCodes.Cgt
				};
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00011D98 File Offset: 0x0000FF98
		public static Code.Cgt_Un_ Cgt_Un
		{
			get
			{
				return new Code.Cgt_Un_
				{
					opcode = OpCodes.Cgt_Un
				};
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00011DAA File Offset: 0x0000FFAA
		public static Code.Clt_ Clt
		{
			get
			{
				return new Code.Clt_
				{
					opcode = OpCodes.Clt
				};
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00011DBC File Offset: 0x0000FFBC
		public static Code.Clt_Un_ Clt_Un
		{
			get
			{
				return new Code.Clt_Un_
				{
					opcode = OpCodes.Clt_Un
				};
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00011DCE File Offset: 0x0000FFCE
		public static Code.Ldftn_ Ldftn
		{
			get
			{
				return new Code.Ldftn_
				{
					opcode = OpCodes.Ldftn
				};
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public static Code.Ldvirtftn_ Ldvirtftn
		{
			get
			{
				return new Code.Ldvirtftn_
				{
					opcode = OpCodes.Ldvirtftn
				};
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00011DF2 File Offset: 0x0000FFF2
		public static Code.Ldarg_ Ldarg
		{
			get
			{
				return new Code.Ldarg_
				{
					opcode = OpCodes.Ldarg
				};
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00011E04 File Offset: 0x00010004
		public static Code.Ldarga_ Ldarga
		{
			get
			{
				return new Code.Ldarga_
				{
					opcode = OpCodes.Ldarga
				};
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00011E16 File Offset: 0x00010016
		public static Code.Starg_ Starg
		{
			get
			{
				return new Code.Starg_
				{
					opcode = OpCodes.Starg
				};
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00011E28 File Offset: 0x00010028
		public static Code.Ldloc_ Ldloc
		{
			get
			{
				return new Code.Ldloc_
				{
					opcode = OpCodes.Ldloc
				};
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00011E3A File Offset: 0x0001003A
		public static Code.Ldloca_ Ldloca
		{
			get
			{
				return new Code.Ldloca_
				{
					opcode = OpCodes.Ldloca
				};
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00011E4C File Offset: 0x0001004C
		public static Code.Stloc_ Stloc
		{
			get
			{
				return new Code.Stloc_
				{
					opcode = OpCodes.Stloc
				};
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00011E5E File Offset: 0x0001005E
		public static Code.Localloc_ Localloc
		{
			get
			{
				return new Code.Localloc_
				{
					opcode = OpCodes.Localloc
				};
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00011E70 File Offset: 0x00010070
		public static Code.Endfilter_ Endfilter
		{
			get
			{
				return new Code.Endfilter_
				{
					opcode = OpCodes.Endfilter
				};
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00011E82 File Offset: 0x00010082
		public static Code.Unaligned_ Unaligned
		{
			get
			{
				return new Code.Unaligned_
				{
					opcode = OpCodes.Unaligned
				};
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00011E94 File Offset: 0x00010094
		public static Code.Volatile_ Volatile
		{
			get
			{
				return new Code.Volatile_
				{
					opcode = OpCodes.Volatile
				};
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00011EA6 File Offset: 0x000100A6
		public static Code.Tailcall_ Tailcall
		{
			get
			{
				return new Code.Tailcall_
				{
					opcode = OpCodes.Tailcall
				};
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00011EB8 File Offset: 0x000100B8
		public static Code.Initobj_ Initobj
		{
			get
			{
				return new Code.Initobj_
				{
					opcode = OpCodes.Initobj
				};
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00011ECA File Offset: 0x000100CA
		public static Code.Constrained_ Constrained
		{
			get
			{
				return new Code.Constrained_
				{
					opcode = OpCodes.Constrained
				};
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00011EDC File Offset: 0x000100DC
		public static Code.Cpblk_ Cpblk
		{
			get
			{
				return new Code.Cpblk_
				{
					opcode = OpCodes.Cpblk
				};
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00011EEE File Offset: 0x000100EE
		public static Code.Initblk_ Initblk
		{
			get
			{
				return new Code.Initblk_
				{
					opcode = OpCodes.Initblk
				};
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00011F00 File Offset: 0x00010100
		public static Code.Rethrow_ Rethrow
		{
			get
			{
				return new Code.Rethrow_
				{
					opcode = OpCodes.Rethrow
				};
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00011F12 File Offset: 0x00010112
		public static Code.Sizeof_ Sizeof
		{
			get
			{
				return new Code.Sizeof_
				{
					opcode = OpCodes.Sizeof
				};
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00011F24 File Offset: 0x00010124
		public static Code.Refanytype_ Refanytype
		{
			get
			{
				return new Code.Refanytype_
				{
					opcode = OpCodes.Refanytype
				};
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00011F36 File Offset: 0x00010136
		public static Code.Readonly_ Readonly
		{
			get
			{
				return new Code.Readonly_
				{
					opcode = OpCodes.Readonly
				};
			}
		}

		// Token: 0x020000A3 RID: 163
		public class Operand_ : CodeMatch
		{
			// Token: 0x170000F9 RID: 249
			public Code.Operand_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Operand_)base.Set(operand, name);
				}
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x00011F58 File Offset: 0x00010158
			public Operand_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A4 RID: 164
		public class Nop_ : CodeMatch
		{
			// Token: 0x170000FA RID: 250
			public Code.Nop_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Nop_)base.Set(operand, name);
				}
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x00011F88 File Offset: 0x00010188
			public Nop_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A5 RID: 165
		public class Break_ : CodeMatch
		{
			// Token: 0x170000FB RID: 251
			public Code.Break_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Break_)base.Set(operand, name);
				}
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x00011FB8 File Offset: 0x000101B8
			public Break_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A6 RID: 166
		public class Ldarg_0_ : CodeMatch
		{
			// Token: 0x170000FC RID: 252
			public Code.Ldarg_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_0_)base.Set(operand, name);
				}
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00011FE8 File Offset: 0x000101E8
			public Ldarg_0_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A7 RID: 167
		public class Ldarg_1_ : CodeMatch
		{
			// Token: 0x170000FD RID: 253
			public Code.Ldarg_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_1_)base.Set(operand, name);
				}
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x00012018 File Offset: 0x00010218
			public Ldarg_1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A8 RID: 168
		public class Ldarg_2_ : CodeMatch
		{
			// Token: 0x170000FE RID: 254
			public Code.Ldarg_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00012048 File Offset: 0x00010248
			public Ldarg_2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000A9 RID: 169
		public class Ldarg_3_ : CodeMatch
		{
			// Token: 0x170000FF RID: 255
			public Code.Ldarg_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_3_)base.Set(operand, name);
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x00012078 File Offset: 0x00010278
			public Ldarg_3_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AA RID: 170
		public class Ldloc_0_ : CodeMatch
		{
			// Token: 0x17000100 RID: 256
			public Code.Ldloc_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_0_)base.Set(operand, name);
				}
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x000120A8 File Offset: 0x000102A8
			public Ldloc_0_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AB RID: 171
		public class Ldloc_1_ : CodeMatch
		{
			// Token: 0x17000101 RID: 257
			public Code.Ldloc_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x000120D8 File Offset: 0x000102D8
			public Ldloc_1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AC RID: 172
		public class Ldloc_2_ : CodeMatch
		{
			// Token: 0x17000102 RID: 258
			public Code.Ldloc_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00012108 File Offset: 0x00010308
			public Ldloc_2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AD RID: 173
		public class Ldloc_3_ : CodeMatch
		{
			// Token: 0x17000103 RID: 259
			public Code.Ldloc_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_3_)base.Set(operand, name);
				}
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x00012138 File Offset: 0x00010338
			public Ldloc_3_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AE RID: 174
		public class Stloc_0_ : CodeMatch
		{
			// Token: 0x17000104 RID: 260
			public Code.Stloc_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_0_)base.Set(operand, name);
				}
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00012168 File Offset: 0x00010368
			public Stloc_0_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000AF RID: 175
		public class Stloc_1_ : CodeMatch
		{
			// Token: 0x17000105 RID: 261
			public Code.Stloc_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_1_)base.Set(operand, name);
				}
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x00012198 File Offset: 0x00010398
			public Stloc_1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B0 RID: 176
		public class Stloc_2_ : CodeMatch
		{
			// Token: 0x17000106 RID: 262
			public Code.Stloc_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x000121C8 File Offset: 0x000103C8
			public Stloc_2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B1 RID: 177
		public class Stloc_3_ : CodeMatch
		{
			// Token: 0x17000107 RID: 263
			public Code.Stloc_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_3_)base.Set(operand, name);
				}
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x000121F8 File Offset: 0x000103F8
			public Stloc_3_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B2 RID: 178
		public class Ldarg_S_ : CodeMatch
		{
			// Token: 0x17000108 RID: 264
			public Code.Ldarg_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000455 RID: 1109 RVA: 0x00012228 File Offset: 0x00010428
			public Ldarg_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B3 RID: 179
		public class Ldarga_S_ : CodeMatch
		{
			// Token: 0x17000109 RID: 265
			public Code.Ldarga_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarga_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x00012258 File Offset: 0x00010458
			public Ldarga_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B4 RID: 180
		public class Starg_S_ : CodeMatch
		{
			// Token: 0x1700010A RID: 266
			public Code.Starg_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Starg_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x00012288 File Offset: 0x00010488
			public Starg_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B5 RID: 181
		public class Ldloc_S_ : CodeMatch
		{
			// Token: 0x1700010B RID: 267
			public Code.Ldloc_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x000122B8 File Offset: 0x000104B8
			public Ldloc_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B6 RID: 182
		public class Ldloca_S_ : CodeMatch
		{
			// Token: 0x1700010C RID: 268
			public Code.Ldloca_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloca_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x000122E8 File Offset: 0x000104E8
			public Ldloca_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B7 RID: 183
		public class Stloc_S_ : CodeMatch
		{
			// Token: 0x1700010D RID: 269
			public Code.Stloc_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x00012318 File Offset: 0x00010518
			public Stloc_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B8 RID: 184
		public class Ldnull_ : CodeMatch
		{
			// Token: 0x1700010E RID: 270
			public Code.Ldnull_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldnull_)base.Set(operand, name);
				}
			}

			// Token: 0x06000461 RID: 1121 RVA: 0x00012348 File Offset: 0x00010548
			public Ldnull_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000B9 RID: 185
		public class Ldc_I4_M1_ : CodeMatch
		{
			// Token: 0x1700010F RID: 271
			public Code.Ldc_I4_M1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_M1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x00012378 File Offset: 0x00010578
			public Ldc_I4_M1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BA RID: 186
		public class Ldc_I4_0_ : CodeMatch
		{
			// Token: 0x17000110 RID: 272
			public Code.Ldc_I4_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_0_)base.Set(operand, name);
				}
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x000123A8 File Offset: 0x000105A8
			public Ldc_I4_0_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BB RID: 187
		public class Ldc_I4_1_ : CodeMatch
		{
			// Token: 0x17000111 RID: 273
			public Code.Ldc_I4_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x000123D8 File Offset: 0x000105D8
			public Ldc_I4_1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BC RID: 188
		public class Ldc_I4_2_ : CodeMatch
		{
			// Token: 0x17000112 RID: 274
			public Code.Ldc_I4_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00012408 File Offset: 0x00010608
			public Ldc_I4_2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BD RID: 189
		public class Ldc_I4_3_ : CodeMatch
		{
			// Token: 0x17000113 RID: 275
			public Code.Ldc_I4_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_3_)base.Set(operand, name);
				}
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00012438 File Offset: 0x00010638
			public Ldc_I4_3_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BE RID: 190
		public class Ldc_I4_4_ : CodeMatch
		{
			// Token: 0x17000114 RID: 276
			public Code.Ldc_I4_4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x00012468 File Offset: 0x00010668
			public Ldc_I4_4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000BF RID: 191
		public class Ldc_I4_5_ : CodeMatch
		{
			// Token: 0x17000115 RID: 277
			public Code.Ldc_I4_5_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_5_)base.Set(operand, name);
				}
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x00012498 File Offset: 0x00010698
			public Ldc_I4_5_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C0 RID: 192
		public class Ldc_I4_6_ : CodeMatch
		{
			// Token: 0x17000116 RID: 278
			public Code.Ldc_I4_6_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_6_)base.Set(operand, name);
				}
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x000124C8 File Offset: 0x000106C8
			public Ldc_I4_6_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C1 RID: 193
		public class Ldc_I4_7_ : CodeMatch
		{
			// Token: 0x17000117 RID: 279
			public Code.Ldc_I4_7_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_7_)base.Set(operand, name);
				}
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x000124F8 File Offset: 0x000106F8
			public Ldc_I4_7_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C2 RID: 194
		public class Ldc_I4_8_ : CodeMatch
		{
			// Token: 0x17000118 RID: 280
			public Code.Ldc_I4_8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_8_)base.Set(operand, name);
				}
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x00012528 File Offset: 0x00010728
			public Ldc_I4_8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C3 RID: 195
		public class Ldc_I4_S_ : CodeMatch
		{
			// Token: 0x17000119 RID: 281
			public Code.Ldc_I4_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x00012558 File Offset: 0x00010758
			public Ldc_I4_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C4 RID: 196
		public class Ldc_I4_ : CodeMatch
		{
			// Token: 0x1700011A RID: 282
			public Code.Ldc_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x00012588 File Offset: 0x00010788
			public Ldc_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C5 RID: 197
		public class Ldc_I8_ : CodeMatch
		{
			// Token: 0x1700011B RID: 283
			public Code.Ldc_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x000125B8 File Offset: 0x000107B8
			public Ldc_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C6 RID: 198
		public class Ldc_R4_ : CodeMatch
		{
			// Token: 0x1700011C RID: 284
			public Code.Ldc_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x000125E8 File Offset: 0x000107E8
			public Ldc_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C7 RID: 199
		public class Ldc_R8_ : CodeMatch
		{
			// Token: 0x1700011D RID: 285
			public Code.Ldc_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600047F RID: 1151 RVA: 0x00012618 File Offset: 0x00010818
			public Ldc_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C8 RID: 200
		public class Dup_ : CodeMatch
		{
			// Token: 0x1700011E RID: 286
			public Code.Dup_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Dup_)base.Set(operand, name);
				}
			}

			// Token: 0x06000481 RID: 1153 RVA: 0x00012648 File Offset: 0x00010848
			public Dup_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000C9 RID: 201
		public class Pop_ : CodeMatch
		{
			// Token: 0x1700011F RID: 287
			public Code.Pop_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Pop_)base.Set(operand, name);
				}
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x00012678 File Offset: 0x00010878
			public Pop_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CA RID: 202
		public class Jmp_ : CodeMatch
		{
			// Token: 0x17000120 RID: 288
			public Code.Jmp_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Jmp_)base.Set(operand, name);
				}
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x000126A8 File Offset: 0x000108A8
			public Jmp_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CB RID: 203
		public class Call_ : CodeMatch
		{
			// Token: 0x17000121 RID: 289
			public Code.Call_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Call_)base.Set(operand, name);
				}
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x000126D8 File Offset: 0x000108D8
			public Call_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CC RID: 204
		public class Calli_ : CodeMatch
		{
			// Token: 0x17000122 RID: 290
			public Code.Calli_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Calli_)base.Set(operand, name);
				}
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x00012708 File Offset: 0x00010908
			public Calli_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CD RID: 205
		public class Ret_ : CodeMatch
		{
			// Token: 0x17000123 RID: 291
			public Code.Ret_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ret_)base.Set(operand, name);
				}
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x00012738 File Offset: 0x00010938
			public Ret_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CE RID: 206
		public class Br_S_ : CodeMatch
		{
			// Token: 0x17000124 RID: 292
			public Code.Br_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Br_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00012768 File Offset: 0x00010968
			public Br_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000CF RID: 207
		public class Brfalse_S_ : CodeMatch
		{
			// Token: 0x17000125 RID: 293
			public Code.Brfalse_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brfalse_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00012798 File Offset: 0x00010998
			public Brfalse_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D0 RID: 208
		public class Brtrue_S_ : CodeMatch
		{
			// Token: 0x17000126 RID: 294
			public Code.Brtrue_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brtrue_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x000127C8 File Offset: 0x000109C8
			public Brtrue_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D1 RID: 209
		public class Beq_S_ : CodeMatch
		{
			// Token: 0x17000127 RID: 295
			public Code.Beq_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Beq_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x000127F8 File Offset: 0x000109F8
			public Beq_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D2 RID: 210
		public class Bge_S_ : CodeMatch
		{
			// Token: 0x17000128 RID: 296
			public Code.Bge_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x00012828 File Offset: 0x00010A28
			public Bge_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D3 RID: 211
		public class Bgt_S_ : CodeMatch
		{
			// Token: 0x17000129 RID: 297
			public Code.Bgt_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x00012858 File Offset: 0x00010A58
			public Bgt_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D4 RID: 212
		public class Ble_S_ : CodeMatch
		{
			// Token: 0x1700012A RID: 298
			public Code.Ble_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_S_)base.Set(operand, name);
				}
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x00012888 File Offset: 0x00010A88
			public Ble_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D5 RID: 213
		public class Blt_S_ : CodeMatch
		{
			// Token: 0x1700012B RID: 299
			public Code.Blt_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x000128B8 File Offset: 0x00010AB8
			public Blt_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D6 RID: 214
		public class Bne_Un_S_ : CodeMatch
		{
			// Token: 0x1700012C RID: 300
			public Code.Bne_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bne_Un_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x000128E8 File Offset: 0x00010AE8
			public Bne_Un_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D7 RID: 215
		public class Bge_Un_S_ : CodeMatch
		{
			// Token: 0x1700012D RID: 301
			public Code.Bge_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_Un_S_)base.Set(operand, name);
				}
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x00012918 File Offset: 0x00010B18
			public Bge_Un_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D8 RID: 216
		public class Bgt_Un_S_ : CodeMatch
		{
			// Token: 0x1700012E RID: 302
			public Code.Bgt_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_Un_S_)base.Set(operand, name);
				}
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x00012948 File Offset: 0x00010B48
			public Bgt_Un_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000D9 RID: 217
		public class Ble_Un_S_ : CodeMatch
		{
			// Token: 0x1700012F RID: 303
			public Code.Ble_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_Un_S_)base.Set(operand, name);
				}
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x00012978 File Offset: 0x00010B78
			public Ble_Un_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DA RID: 218
		public class Blt_Un_S_ : CodeMatch
		{
			// Token: 0x17000130 RID: 304
			public Code.Blt_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_Un_S_)base.Set(operand, name);
				}
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x000129A8 File Offset: 0x00010BA8
			public Blt_Un_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DB RID: 219
		public class Br_ : CodeMatch
		{
			// Token: 0x17000131 RID: 305
			public Code.Br_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Br_)base.Set(operand, name);
				}
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x000129D8 File Offset: 0x00010BD8
			public Br_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DC RID: 220
		public class Brfalse_ : CodeMatch
		{
			// Token: 0x17000132 RID: 306
			public Code.Brfalse_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brfalse_)base.Set(operand, name);
				}
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x00012A08 File Offset: 0x00010C08
			public Brfalse_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DD RID: 221
		public class Brtrue_ : CodeMatch
		{
			// Token: 0x17000133 RID: 307
			public Code.Brtrue_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brtrue_)base.Set(operand, name);
				}
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x00012A38 File Offset: 0x00010C38
			public Brtrue_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DE RID: 222
		public class Beq_ : CodeMatch
		{
			// Token: 0x17000134 RID: 308
			public Code.Beq_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Beq_)base.Set(operand, name);
				}
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00012A68 File Offset: 0x00010C68
			public Beq_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000DF RID: 223
		public class Bge_ : CodeMatch
		{
			// Token: 0x17000135 RID: 309
			public Code.Bge_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_)base.Set(operand, name);
				}
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x00012A98 File Offset: 0x00010C98
			public Bge_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E0 RID: 224
		public class Bgt_ : CodeMatch
		{
			// Token: 0x17000136 RID: 310
			public Code.Bgt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_)base.Set(operand, name);
				}
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00012AC8 File Offset: 0x00010CC8
			public Bgt_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E1 RID: 225
		public class Ble_ : CodeMatch
		{
			// Token: 0x17000137 RID: 311
			public Code.Ble_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_)base.Set(operand, name);
				}
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x00012AF8 File Offset: 0x00010CF8
			public Ble_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E2 RID: 226
		public class Blt_ : CodeMatch
		{
			// Token: 0x17000138 RID: 312
			public Code.Blt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_)base.Set(operand, name);
				}
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x00012B28 File Offset: 0x00010D28
			public Blt_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E3 RID: 227
		public class Bne_Un_ : CodeMatch
		{
			// Token: 0x17000139 RID: 313
			public Code.Bne_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bne_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x00012B58 File Offset: 0x00010D58
			public Bne_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E4 RID: 228
		public class Bge_Un_ : CodeMatch
		{
			// Token: 0x1700013A RID: 314
			public Code.Bge_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x00012B88 File Offset: 0x00010D88
			public Bge_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E5 RID: 229
		public class Bgt_Un_ : CodeMatch
		{
			// Token: 0x1700013B RID: 315
			public Code.Bgt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x00012BB8 File Offset: 0x00010DB8
			public Bgt_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E6 RID: 230
		public class Ble_Un_ : CodeMatch
		{
			// Token: 0x1700013C RID: 316
			public Code.Ble_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x00012BE8 File Offset: 0x00010DE8
			public Ble_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E7 RID: 231
		public class Blt_Un_ : CodeMatch
		{
			// Token: 0x1700013D RID: 317
			public Code.Blt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x00012C18 File Offset: 0x00010E18
			public Blt_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E8 RID: 232
		public class Switch_ : CodeMatch
		{
			// Token: 0x1700013E RID: 318
			public Code.Switch_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Switch_)base.Set(operand, name);
				}
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x00012C48 File Offset: 0x00010E48
			public Switch_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000E9 RID: 233
		public class Ldind_I1_ : CodeMatch
		{
			// Token: 0x1700013F RID: 319
			public Code.Ldind_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x00012C78 File Offset: 0x00010E78
			public Ldind_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000EA RID: 234
		public class Ldind_U1_ : CodeMatch
		{
			// Token: 0x17000140 RID: 320
			public Code.Ldind_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U1_)base.Set(operand, name);
				}
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x00012CA8 File Offset: 0x00010EA8
			public Ldind_U1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000EB RID: 235
		public class Ldind_I2_ : CodeMatch
		{
			// Token: 0x17000141 RID: 321
			public Code.Ldind_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x00012CD8 File Offset: 0x00010ED8
			public Ldind_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000EC RID: 236
		public class Ldind_U2_ : CodeMatch
		{
			// Token: 0x17000142 RID: 322
			public Code.Ldind_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U2_)base.Set(operand, name);
				}
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00012D08 File Offset: 0x00010F08
			public Ldind_U2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000ED RID: 237
		public class Ldind_I4_ : CodeMatch
		{
			// Token: 0x17000143 RID: 323
			public Code.Ldind_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00012D38 File Offset: 0x00010F38
			public Ldind_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000EE RID: 238
		public class Ldind_U4_ : CodeMatch
		{
			// Token: 0x17000144 RID: 324
			public Code.Ldind_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U4_)base.Set(operand, name);
				}
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00012D68 File Offset: 0x00010F68
			public Ldind_U4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000EF RID: 239
		public class Ldind_I8_ : CodeMatch
		{
			// Token: 0x17000145 RID: 325
			public Code.Ldind_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00012D98 File Offset: 0x00010F98
			public Ldind_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F0 RID: 240
		public class Ldind_I_ : CodeMatch
		{
			// Token: 0x17000146 RID: 326
			public Code.Ldind_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I_)base.Set(operand, name);
				}
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00012DC8 File Offset: 0x00010FC8
			public Ldind_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F1 RID: 241
		public class Ldind_R4_ : CodeMatch
		{
			// Token: 0x17000147 RID: 327
			public Code.Ldind_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x00012DF8 File Offset: 0x00010FF8
			public Ldind_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F2 RID: 242
		public class Ldind_R8_ : CodeMatch
		{
			// Token: 0x17000148 RID: 328
			public Code.Ldind_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x00012E28 File Offset: 0x00011028
			public Ldind_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F3 RID: 243
		public class Ldind_Ref_ : CodeMatch
		{
			// Token: 0x17000149 RID: 329
			public Code.Ldind_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_Ref_)base.Set(operand, name);
				}
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00012E58 File Offset: 0x00011058
			public Ldind_Ref_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F4 RID: 244
		public class Stind_Ref_ : CodeMatch
		{
			// Token: 0x1700014A RID: 330
			public Code.Stind_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_Ref_)base.Set(operand, name);
				}
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00012E88 File Offset: 0x00011088
			public Stind_Ref_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F5 RID: 245
		public class Stind_I1_ : CodeMatch
		{
			// Token: 0x1700014B RID: 331
			public Code.Stind_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00012EB8 File Offset: 0x000110B8
			public Stind_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F6 RID: 246
		public class Stind_I2_ : CodeMatch
		{
			// Token: 0x1700014C RID: 332
			public Code.Stind_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00012EE8 File Offset: 0x000110E8
			public Stind_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F7 RID: 247
		public class Stind_I4_ : CodeMatch
		{
			// Token: 0x1700014D RID: 333
			public Code.Stind_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00012F18 File Offset: 0x00011118
			public Stind_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F8 RID: 248
		public class Stind_I8_ : CodeMatch
		{
			// Token: 0x1700014E RID: 334
			public Code.Stind_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00012F48 File Offset: 0x00011148
			public Stind_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000F9 RID: 249
		public class Stind_R4_ : CodeMatch
		{
			// Token: 0x1700014F RID: 335
			public Code.Stind_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00012F78 File Offset: 0x00011178
			public Stind_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FA RID: 250
		public class Stind_R8_ : CodeMatch
		{
			// Token: 0x17000150 RID: 336
			public Code.Stind_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00012FA8 File Offset: 0x000111A8
			public Stind_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FB RID: 251
		public class Add_ : CodeMatch
		{
			// Token: 0x17000151 RID: 337
			public Code.Add_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_)base.Set(operand, name);
				}
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00012FD8 File Offset: 0x000111D8
			public Add_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FC RID: 252
		public class Sub_ : CodeMatch
		{
			// Token: 0x17000152 RID: 338
			public Code.Sub_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_)base.Set(operand, name);
				}
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00013008 File Offset: 0x00011208
			public Sub_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FD RID: 253
		public class Mul_ : CodeMatch
		{
			// Token: 0x17000153 RID: 339
			public Code.Mul_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_)base.Set(operand, name);
				}
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x00013038 File Offset: 0x00011238
			public Mul_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FE RID: 254
		public class Div_ : CodeMatch
		{
			// Token: 0x17000154 RID: 340
			public Code.Div_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Div_)base.Set(operand, name);
				}
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x00013068 File Offset: 0x00011268
			public Div_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x020000FF RID: 255
		public class Div_Un_ : CodeMatch
		{
			// Token: 0x17000155 RID: 341
			public Code.Div_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Div_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x00013098 File Offset: 0x00011298
			public Div_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000100 RID: 256
		public class Rem_ : CodeMatch
		{
			// Token: 0x17000156 RID: 342
			public Code.Rem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rem_)base.Set(operand, name);
				}
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x000130C8 File Offset: 0x000112C8
			public Rem_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000101 RID: 257
		public class Rem_Un_ : CodeMatch
		{
			// Token: 0x17000157 RID: 343
			public Code.Rem_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rem_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000130F8 File Offset: 0x000112F8
			public Rem_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000102 RID: 258
		public class And_ : CodeMatch
		{
			// Token: 0x17000158 RID: 344
			public Code.And_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.And_)base.Set(operand, name);
				}
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x00013128 File Offset: 0x00011328
			public And_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000103 RID: 259
		public class Or_ : CodeMatch
		{
			// Token: 0x17000159 RID: 345
			public Code.Or_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Or_)base.Set(operand, name);
				}
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x00013158 File Offset: 0x00011358
			public Or_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000104 RID: 260
		public class Xor_ : CodeMatch
		{
			// Token: 0x1700015A RID: 346
			public Code.Xor_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Xor_)base.Set(operand, name);
				}
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00013188 File Offset: 0x00011388
			public Xor_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000105 RID: 261
		public class Shl_ : CodeMatch
		{
			// Token: 0x1700015B RID: 347
			public Code.Shl_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shl_)base.Set(operand, name);
				}
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x000131B8 File Offset: 0x000113B8
			public Shl_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000106 RID: 262
		public class Shr_ : CodeMatch
		{
			// Token: 0x1700015C RID: 348
			public Code.Shr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shr_)base.Set(operand, name);
				}
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x000131E8 File Offset: 0x000113E8
			public Shr_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000107 RID: 263
		public class Shr_Un_ : CodeMatch
		{
			// Token: 0x1700015D RID: 349
			public Code.Shr_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shr_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00013218 File Offset: 0x00011418
			public Shr_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000108 RID: 264
		public class Neg_ : CodeMatch
		{
			// Token: 0x1700015E RID: 350
			public Code.Neg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Neg_)base.Set(operand, name);
				}
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x00013248 File Offset: 0x00011448
			public Neg_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000109 RID: 265
		public class Not_ : CodeMatch
		{
			// Token: 0x1700015F RID: 351
			public Code.Not_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Not_)base.Set(operand, name);
				}
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x00013278 File Offset: 0x00011478
			public Not_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010A RID: 266
		public class Conv_I1_ : CodeMatch
		{
			// Token: 0x17000160 RID: 352
			public Code.Conv_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x000132A8 File Offset: 0x000114A8
			public Conv_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010B RID: 267
		public class Conv_I2_ : CodeMatch
		{
			// Token: 0x17000161 RID: 353
			public Code.Conv_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x000132D8 File Offset: 0x000114D8
			public Conv_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010C RID: 268
		public class Conv_I4_ : CodeMatch
		{
			// Token: 0x17000162 RID: 354
			public Code.Conv_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00013308 File Offset: 0x00011508
			public Conv_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010D RID: 269
		public class Conv_I8_ : CodeMatch
		{
			// Token: 0x17000163 RID: 355
			public Code.Conv_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00013338 File Offset: 0x00011538
			public Conv_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010E RID: 270
		public class Conv_R4_ : CodeMatch
		{
			// Token: 0x17000164 RID: 356
			public Code.Conv_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x00013368 File Offset: 0x00011568
			public Conv_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200010F RID: 271
		public class Conv_R8_ : CodeMatch
		{
			// Token: 0x17000165 RID: 357
			public Code.Conv_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x00013398 File Offset: 0x00011598
			public Conv_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000110 RID: 272
		public class Conv_U4_ : CodeMatch
		{
			// Token: 0x17000166 RID: 358
			public Code.Conv_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x000133C8 File Offset: 0x000115C8
			public Conv_U4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000111 RID: 273
		public class Conv_U8_ : CodeMatch
		{
			// Token: 0x17000167 RID: 359
			public Code.Conv_U8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U8_)base.Set(operand, name);
				}
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x000133F8 File Offset: 0x000115F8
			public Conv_U8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000112 RID: 274
		public class Callvirt_ : CodeMatch
		{
			// Token: 0x17000168 RID: 360
			public Code.Callvirt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Callvirt_)base.Set(operand, name);
				}
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00013428 File Offset: 0x00011628
			public Callvirt_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000113 RID: 275
		public class Cpobj_ : CodeMatch
		{
			// Token: 0x17000169 RID: 361
			public Code.Cpobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cpobj_)base.Set(operand, name);
				}
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x00013458 File Offset: 0x00011658
			public Cpobj_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000114 RID: 276
		public class Ldobj_ : CodeMatch
		{
			// Token: 0x1700016A RID: 362
			public Code.Ldobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldobj_)base.Set(operand, name);
				}
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x00013488 File Offset: 0x00011688
			public Ldobj_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000115 RID: 277
		public class Ldstr_ : CodeMatch
		{
			// Token: 0x1700016B RID: 363
			public Code.Ldstr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldstr_)base.Set(operand, name);
				}
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x000134B8 File Offset: 0x000116B8
			public Ldstr_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000116 RID: 278
		public class Newobj_ : CodeMatch
		{
			// Token: 0x1700016C RID: 364
			public Code.Newobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Newobj_)base.Set(operand, name);
				}
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x000134E8 File Offset: 0x000116E8
			public Newobj_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000117 RID: 279
		public class Castclass_ : CodeMatch
		{
			// Token: 0x1700016D RID: 365
			public Code.Castclass_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Castclass_)base.Set(operand, name);
				}
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x00013518 File Offset: 0x00011718
			public Castclass_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000118 RID: 280
		public class Isinst_ : CodeMatch
		{
			// Token: 0x1700016E RID: 366
			public Code.Isinst_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Isinst_)base.Set(operand, name);
				}
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x00013548 File Offset: 0x00011748
			public Isinst_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000119 RID: 281
		public class Conv_R_Un_ : CodeMatch
		{
			// Token: 0x1700016F RID: 367
			public Code.Conv_R_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x00013578 File Offset: 0x00011778
			public Conv_R_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011A RID: 282
		public class Unbox_ : CodeMatch
		{
			// Token: 0x17000170 RID: 368
			public Code.Unbox_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unbox_)base.Set(operand, name);
				}
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x000135A8 File Offset: 0x000117A8
			public Unbox_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011B RID: 283
		public class Throw_ : CodeMatch
		{
			// Token: 0x17000171 RID: 369
			public Code.Throw_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Throw_)base.Set(operand, name);
				}
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x000135D8 File Offset: 0x000117D8
			public Throw_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011C RID: 284
		public class Ldfld_ : CodeMatch
		{
			// Token: 0x17000172 RID: 370
			public Code.Ldfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldfld_)base.Set(operand, name);
				}
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x00013608 File Offset: 0x00011808
			public Ldfld_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011D RID: 285
		public class Ldflda_ : CodeMatch
		{
			// Token: 0x17000173 RID: 371
			public Code.Ldflda_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldflda_)base.Set(operand, name);
				}
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x00013638 File Offset: 0x00011838
			public Ldflda_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011E RID: 286
		public class Stfld_ : CodeMatch
		{
			// Token: 0x17000174 RID: 372
			public Code.Stfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stfld_)base.Set(operand, name);
				}
			}

			// Token: 0x0600052D RID: 1325 RVA: 0x00013668 File Offset: 0x00011868
			public Stfld_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200011F RID: 287
		public class Ldsfld_ : CodeMatch
		{
			// Token: 0x17000175 RID: 373
			public Code.Ldsfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldsfld_)base.Set(operand, name);
				}
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x00013698 File Offset: 0x00011898
			public Ldsfld_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000120 RID: 288
		public class Ldsflda_ : CodeMatch
		{
			// Token: 0x17000176 RID: 374
			public Code.Ldsflda_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldsflda_)base.Set(operand, name);
				}
			}

			// Token: 0x06000531 RID: 1329 RVA: 0x000136C8 File Offset: 0x000118C8
			public Ldsflda_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000121 RID: 289
		public class Stsfld_ : CodeMatch
		{
			// Token: 0x17000177 RID: 375
			public Code.Stsfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stsfld_)base.Set(operand, name);
				}
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x000136F8 File Offset: 0x000118F8
			public Stsfld_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000122 RID: 290
		public class Stobj_ : CodeMatch
		{
			// Token: 0x17000178 RID: 376
			public Code.Stobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stobj_)base.Set(operand, name);
				}
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x00013728 File Offset: 0x00011928
			public Stobj_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000123 RID: 291
		public class Conv_Ovf_I1_Un_ : CodeMatch
		{
			// Token: 0x17000179 RID: 377
			public Code.Conv_Ovf_I1_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I1_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x00013758 File Offset: 0x00011958
			public Conv_Ovf_I1_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000124 RID: 292
		public class Conv_Ovf_I2_Un_ : CodeMatch
		{
			// Token: 0x1700017A RID: 378
			public Code.Conv_Ovf_I2_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I2_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x00013788 File Offset: 0x00011988
			public Conv_Ovf_I2_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000125 RID: 293
		public class Conv_Ovf_I4_Un_ : CodeMatch
		{
			// Token: 0x1700017B RID: 379
			public Code.Conv_Ovf_I4_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I4_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x000137B8 File Offset: 0x000119B8
			public Conv_Ovf_I4_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000126 RID: 294
		public class Conv_Ovf_I8_Un_ : CodeMatch
		{
			// Token: 0x1700017C RID: 380
			public Code.Conv_Ovf_I8_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I8_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x000137E8 File Offset: 0x000119E8
			public Conv_Ovf_I8_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000127 RID: 295
		public class Conv_Ovf_U1_Un_ : CodeMatch
		{
			// Token: 0x1700017D RID: 381
			public Code.Conv_Ovf_U1_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U1_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00013818 File Offset: 0x00011A18
			public Conv_Ovf_U1_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000128 RID: 296
		public class Conv_Ovf_U2_Un_ : CodeMatch
		{
			// Token: 0x1700017E RID: 382
			public Code.Conv_Ovf_U2_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U2_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x00013848 File Offset: 0x00011A48
			public Conv_Ovf_U2_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000129 RID: 297
		public class Conv_Ovf_U4_Un_ : CodeMatch
		{
			// Token: 0x1700017F RID: 383
			public Code.Conv_Ovf_U4_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U4_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x00013878 File Offset: 0x00011A78
			public Conv_Ovf_U4_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012A RID: 298
		public class Conv_Ovf_U8_Un_ : CodeMatch
		{
			// Token: 0x17000180 RID: 384
			public Code.Conv_Ovf_U8_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U8_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x000138A8 File Offset: 0x00011AA8
			public Conv_Ovf_U8_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012B RID: 299
		public class Conv_Ovf_I_Un_ : CodeMatch
		{
			// Token: 0x17000181 RID: 385
			public Code.Conv_Ovf_I_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x000138D8 File Offset: 0x00011AD8
			public Conv_Ovf_I_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012C RID: 300
		public class Conv_Ovf_U_Un_ : CodeMatch
		{
			// Token: 0x17000182 RID: 386
			public Code.Conv_Ovf_U_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x00013908 File Offset: 0x00011B08
			public Conv_Ovf_U_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012D RID: 301
		public class Box_ : CodeMatch
		{
			// Token: 0x17000183 RID: 387
			public Code.Box_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Box_)base.Set(operand, name);
				}
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x00013938 File Offset: 0x00011B38
			public Box_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012E RID: 302
		public class Newarr_ : CodeMatch
		{
			// Token: 0x17000184 RID: 388
			public Code.Newarr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Newarr_)base.Set(operand, name);
				}
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x00013968 File Offset: 0x00011B68
			public Newarr_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200012F RID: 303
		public class Ldlen_ : CodeMatch
		{
			// Token: 0x17000185 RID: 389
			public Code.Ldlen_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldlen_)base.Set(operand, name);
				}
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x00013998 File Offset: 0x00011B98
			public Ldlen_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000130 RID: 304
		public class Ldelema_ : CodeMatch
		{
			// Token: 0x17000186 RID: 390
			public Code.Ldelema_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelema_)base.Set(operand, name);
				}
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x000139C8 File Offset: 0x00011BC8
			public Ldelema_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000131 RID: 305
		public class Ldelem_I1_ : CodeMatch
		{
			// Token: 0x17000187 RID: 391
			public Code.Ldelem_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x000139F8 File Offset: 0x00011BF8
			public Ldelem_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000132 RID: 306
		public class Ldelem_U1_ : CodeMatch
		{
			// Token: 0x17000188 RID: 392
			public Code.Ldelem_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x00013A28 File Offset: 0x00011C28
			public Ldelem_U1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000133 RID: 307
		public class Ldelem_I2_ : CodeMatch
		{
			// Token: 0x17000189 RID: 393
			public Code.Ldelem_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x00013A58 File Offset: 0x00011C58
			public Ldelem_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000134 RID: 308
		public class Ldelem_U2_ : CodeMatch
		{
			// Token: 0x1700018A RID: 394
			public Code.Ldelem_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x00013A88 File Offset: 0x00011C88
			public Ldelem_U2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000135 RID: 309
		public class Ldelem_I4_ : CodeMatch
		{
			// Token: 0x1700018B RID: 395
			public Code.Ldelem_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x00013AB8 File Offset: 0x00011CB8
			public Ldelem_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000136 RID: 310
		public class Ldelem_U4_ : CodeMatch
		{
			// Token: 0x1700018C RID: 396
			public Code.Ldelem_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x00013AE8 File Offset: 0x00011CE8
			public Ldelem_U4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000137 RID: 311
		public class Ldelem_I8_ : CodeMatch
		{
			// Token: 0x1700018D RID: 397
			public Code.Ldelem_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x00013B18 File Offset: 0x00011D18
			public Ldelem_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000138 RID: 312
		public class Ldelem_I_ : CodeMatch
		{
			// Token: 0x1700018E RID: 398
			public Code.Ldelem_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I_)base.Set(operand, name);
				}
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x00013B48 File Offset: 0x00011D48
			public Ldelem_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000139 RID: 313
		public class Ldelem_R4_ : CodeMatch
		{
			// Token: 0x1700018F RID: 399
			public Code.Ldelem_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x00013B78 File Offset: 0x00011D78
			public Ldelem_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013A RID: 314
		public class Ldelem_R8_ : CodeMatch
		{
			// Token: 0x17000190 RID: 400
			public Code.Ldelem_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x00013BA8 File Offset: 0x00011DA8
			public Ldelem_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013B RID: 315
		public class Ldelem_Ref_ : CodeMatch
		{
			// Token: 0x17000191 RID: 401
			public Code.Ldelem_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_Ref_)base.Set(operand, name);
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00013BD8 File Offset: 0x00011DD8
			public Ldelem_Ref_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013C RID: 316
		public class Stelem_I_ : CodeMatch
		{
			// Token: 0x17000192 RID: 402
			public Code.Stelem_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I_)base.Set(operand, name);
				}
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00013C08 File Offset: 0x00011E08
			public Stelem_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013D RID: 317
		public class Stelem_I1_ : CodeMatch
		{
			// Token: 0x17000193 RID: 403
			public Code.Stelem_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00013C38 File Offset: 0x00011E38
			public Stelem_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013E RID: 318
		public class Stelem_I2_ : CodeMatch
		{
			// Token: 0x17000194 RID: 404
			public Code.Stelem_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00013C68 File Offset: 0x00011E68
			public Stelem_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200013F RID: 319
		public class Stelem_I4_ : CodeMatch
		{
			// Token: 0x17000195 RID: 405
			public Code.Stelem_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x00013C98 File Offset: 0x00011E98
			public Stelem_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000140 RID: 320
		public class Stelem_I8_ : CodeMatch
		{
			// Token: 0x17000196 RID: 406
			public Code.Stelem_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x00013CC8 File Offset: 0x00011EC8
			public Stelem_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000141 RID: 321
		public class Stelem_R4_ : CodeMatch
		{
			// Token: 0x17000197 RID: 407
			public Code.Stelem_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_R4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x00013CF8 File Offset: 0x00011EF8
			public Stelem_R4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000142 RID: 322
		public class Stelem_R8_ : CodeMatch
		{
			// Token: 0x17000198 RID: 408
			public Code.Stelem_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_R8_)base.Set(operand, name);
				}
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00013D28 File Offset: 0x00011F28
			public Stelem_R8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000143 RID: 323
		public class Stelem_Ref_ : CodeMatch
		{
			// Token: 0x17000199 RID: 409
			public Code.Stelem_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_Ref_)base.Set(operand, name);
				}
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00013D58 File Offset: 0x00011F58
			public Stelem_Ref_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000144 RID: 324
		public class Ldelem_ : CodeMatch
		{
			// Token: 0x1700019A RID: 410
			public Code.Ldelem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_)base.Set(operand, name);
				}
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x00013D88 File Offset: 0x00011F88
			public Ldelem_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000145 RID: 325
		public class Stelem_ : CodeMatch
		{
			// Token: 0x1700019B RID: 411
			public Code.Stelem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_)base.Set(operand, name);
				}
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x00013DB8 File Offset: 0x00011FB8
			public Stelem_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000146 RID: 326
		public class Unbox_Any_ : CodeMatch
		{
			// Token: 0x1700019C RID: 412
			public Code.Unbox_Any_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unbox_Any_)base.Set(operand, name);
				}
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x00013DE8 File Offset: 0x00011FE8
			public Unbox_Any_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000147 RID: 327
		public class Conv_Ovf_I1_ : CodeMatch
		{
			// Token: 0x1700019D RID: 413
			public Code.Conv_Ovf_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I1_)base.Set(operand, name);
				}
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x00013E18 File Offset: 0x00012018
			public Conv_Ovf_I1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000148 RID: 328
		public class Conv_Ovf_U1_ : CodeMatch
		{
			// Token: 0x1700019E RID: 414
			public Code.Conv_Ovf_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00013E48 File Offset: 0x00012048
			public Conv_Ovf_U1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000149 RID: 329
		public class Conv_Ovf_I2_ : CodeMatch
		{
			// Token: 0x1700019F RID: 415
			public Code.Conv_Ovf_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x00013E78 File Offset: 0x00012078
			public Conv_Ovf_I2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014A RID: 330
		public class Conv_Ovf_U2_ : CodeMatch
		{
			// Token: 0x170001A0 RID: 416
			public Code.Conv_Ovf_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x00013EA8 File Offset: 0x000120A8
			public Conv_Ovf_U2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014B RID: 331
		public class Conv_Ovf_I4_ : CodeMatch
		{
			// Token: 0x170001A1 RID: 417
			public Code.Conv_Ovf_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x00013ED8 File Offset: 0x000120D8
			public Conv_Ovf_I4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014C RID: 332
		public class Conv_Ovf_U4_ : CodeMatch
		{
			// Token: 0x170001A2 RID: 418
			public Code.Conv_Ovf_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U4_)base.Set(operand, name);
				}
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x00013F08 File Offset: 0x00012108
			public Conv_Ovf_U4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014D RID: 333
		public class Conv_Ovf_I8_ : CodeMatch
		{
			// Token: 0x170001A3 RID: 419
			public Code.Conv_Ovf_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00013F38 File Offset: 0x00012138
			public Conv_Ovf_I8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014E RID: 334
		public class Conv_Ovf_U8_ : CodeMatch
		{
			// Token: 0x170001A4 RID: 420
			public Code.Conv_Ovf_U8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U8_)base.Set(operand, name);
				}
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x00013F68 File Offset: 0x00012168
			public Conv_Ovf_U8_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200014F RID: 335
		public class Refanyval_ : CodeMatch
		{
			// Token: 0x170001A5 RID: 421
			public Code.Refanyval_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Refanyval_)base.Set(operand, name);
				}
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00013F98 File Offset: 0x00012198
			public Refanyval_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000150 RID: 336
		public class Ckfinite_ : CodeMatch
		{
			// Token: 0x170001A6 RID: 422
			public Code.Ckfinite_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ckfinite_)base.Set(operand, name);
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00013FC8 File Offset: 0x000121C8
			public Ckfinite_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000151 RID: 337
		public class Mkrefany_ : CodeMatch
		{
			// Token: 0x170001A7 RID: 423
			public Code.Mkrefany_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mkrefany_)base.Set(operand, name);
				}
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x00013FF8 File Offset: 0x000121F8
			public Mkrefany_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000152 RID: 338
		public class Ldtoken_ : CodeMatch
		{
			// Token: 0x170001A8 RID: 424
			public Code.Ldtoken_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldtoken_)base.Set(operand, name);
				}
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x00014028 File Offset: 0x00012228
			public Ldtoken_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000153 RID: 339
		public class Conv_U2_ : CodeMatch
		{
			// Token: 0x170001A9 RID: 425
			public Code.Conv_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U2_)base.Set(operand, name);
				}
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00014058 File Offset: 0x00012258
			public Conv_U2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000154 RID: 340
		public class Conv_U1_ : CodeMatch
		{
			// Token: 0x170001AA RID: 426
			public Code.Conv_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U1_)base.Set(operand, name);
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00014088 File Offset: 0x00012288
			public Conv_U1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000155 RID: 341
		public class Conv_I_ : CodeMatch
		{
			// Token: 0x170001AB RID: 427
			public Code.Conv_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I_)base.Set(operand, name);
				}
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x000140B8 File Offset: 0x000122B8
			public Conv_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000156 RID: 342
		public class Conv_Ovf_I_ : CodeMatch
		{
			// Token: 0x170001AC RID: 428
			public Code.Conv_Ovf_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I_)base.Set(operand, name);
				}
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x000140E8 File Offset: 0x000122E8
			public Conv_Ovf_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000157 RID: 343
		public class Conv_Ovf_U_ : CodeMatch
		{
			// Token: 0x170001AD RID: 429
			public Code.Conv_Ovf_U_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U_)base.Set(operand, name);
				}
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00014118 File Offset: 0x00012318
			public Conv_Ovf_U_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000158 RID: 344
		public class Add_Ovf_ : CodeMatch
		{
			// Token: 0x170001AE RID: 430
			public Code.Add_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_Ovf_)base.Set(operand, name);
				}
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00014148 File Offset: 0x00012348
			public Add_Ovf_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000159 RID: 345
		public class Add_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001AF RID: 431
			public Code.Add_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_Ovf_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00014178 File Offset: 0x00012378
			public Add_Ovf_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015A RID: 346
		public class Mul_Ovf_ : CodeMatch
		{
			// Token: 0x170001B0 RID: 432
			public Code.Mul_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_Ovf_)base.Set(operand, name);
				}
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x000141A8 File Offset: 0x000123A8
			public Mul_Ovf_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015B RID: 347
		public class Mul_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001B1 RID: 433
			public Code.Mul_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_Ovf_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x000141D8 File Offset: 0x000123D8
			public Mul_Ovf_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015C RID: 348
		public class Sub_Ovf_ : CodeMatch
		{
			// Token: 0x170001B2 RID: 434
			public Code.Sub_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_Ovf_)base.Set(operand, name);
				}
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00014208 File Offset: 0x00012408
			public Sub_Ovf_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015D RID: 349
		public class Sub_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001B3 RID: 435
			public Code.Sub_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_Ovf_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00014238 File Offset: 0x00012438
			public Sub_Ovf_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015E RID: 350
		public class Endfinally_ : CodeMatch
		{
			// Token: 0x170001B4 RID: 436
			public Code.Endfinally_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Endfinally_)base.Set(operand, name);
				}
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00014268 File Offset: 0x00012468
			public Endfinally_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200015F RID: 351
		public class Leave_ : CodeMatch
		{
			// Token: 0x170001B5 RID: 437
			public Code.Leave_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Leave_)base.Set(operand, name);
				}
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00014298 File Offset: 0x00012498
			public Leave_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000160 RID: 352
		public class Leave_S_ : CodeMatch
		{
			// Token: 0x170001B6 RID: 438
			public Code.Leave_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Leave_S_)base.Set(operand, name);
				}
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x000142C8 File Offset: 0x000124C8
			public Leave_S_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000161 RID: 353
		public class Stind_I_ : CodeMatch
		{
			// Token: 0x170001B7 RID: 439
			public Code.Stind_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I_)base.Set(operand, name);
				}
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x000142F8 File Offset: 0x000124F8
			public Stind_I_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000162 RID: 354
		public class Conv_U_ : CodeMatch
		{
			// Token: 0x170001B8 RID: 440
			public Code.Conv_U_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U_)base.Set(operand, name);
				}
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x00014328 File Offset: 0x00012528
			public Conv_U_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000163 RID: 355
		public class Prefix7_ : CodeMatch
		{
			// Token: 0x170001B9 RID: 441
			public Code.Prefix7_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix7_)base.Set(operand, name);
				}
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x00014358 File Offset: 0x00012558
			public Prefix7_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000164 RID: 356
		public class Prefix6_ : CodeMatch
		{
			// Token: 0x170001BA RID: 442
			public Code.Prefix6_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix6_)base.Set(operand, name);
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x00014388 File Offset: 0x00012588
			public Prefix6_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000165 RID: 357
		public class Prefix5_ : CodeMatch
		{
			// Token: 0x170001BB RID: 443
			public Code.Prefix5_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix5_)base.Set(operand, name);
				}
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x000143B8 File Offset: 0x000125B8
			public Prefix5_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000166 RID: 358
		public class Prefix4_ : CodeMatch
		{
			// Token: 0x170001BC RID: 444
			public Code.Prefix4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix4_)base.Set(operand, name);
				}
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x000143E8 File Offset: 0x000125E8
			public Prefix4_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000167 RID: 359
		public class Prefix3_ : CodeMatch
		{
			// Token: 0x170001BD RID: 445
			public Code.Prefix3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix3_)base.Set(operand, name);
				}
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x00014418 File Offset: 0x00012618
			public Prefix3_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000168 RID: 360
		public class Prefix2_ : CodeMatch
		{
			// Token: 0x170001BE RID: 446
			public Code.Prefix2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix2_)base.Set(operand, name);
				}
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x00014448 File Offset: 0x00012648
			public Prefix2_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000169 RID: 361
		public class Prefix1_ : CodeMatch
		{
			// Token: 0x170001BF RID: 447
			public Code.Prefix1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix1_)base.Set(operand, name);
				}
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x00014478 File Offset: 0x00012678
			public Prefix1_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016A RID: 362
		public class Prefixref_ : CodeMatch
		{
			// Token: 0x170001C0 RID: 448
			public Code.Prefixref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefixref_)base.Set(operand, name);
				}
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x000144A8 File Offset: 0x000126A8
			public Prefixref_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016B RID: 363
		public class Arglist_ : CodeMatch
		{
			// Token: 0x170001C1 RID: 449
			public Code.Arglist_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Arglist_)base.Set(operand, name);
				}
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x000144D8 File Offset: 0x000126D8
			public Arglist_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016C RID: 364
		public class Ceq_ : CodeMatch
		{
			// Token: 0x170001C2 RID: 450
			public Code.Ceq_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ceq_)base.Set(operand, name);
				}
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x00014508 File Offset: 0x00012708
			public Ceq_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016D RID: 365
		public class Cgt_ : CodeMatch
		{
			// Token: 0x170001C3 RID: 451
			public Code.Cgt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cgt_)base.Set(operand, name);
				}
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x00014538 File Offset: 0x00012738
			public Cgt_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016E RID: 366
		public class Cgt_Un_ : CodeMatch
		{
			// Token: 0x170001C4 RID: 452
			public Code.Cgt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cgt_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x00014568 File Offset: 0x00012768
			public Cgt_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200016F RID: 367
		public class Clt_ : CodeMatch
		{
			// Token: 0x170001C5 RID: 453
			public Code.Clt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Clt_)base.Set(operand, name);
				}
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x00014598 File Offset: 0x00012798
			public Clt_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000170 RID: 368
		public class Clt_Un_ : CodeMatch
		{
			// Token: 0x170001C6 RID: 454
			public Code.Clt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Clt_Un_)base.Set(operand, name);
				}
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x000145C8 File Offset: 0x000127C8
			public Clt_Un_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000171 RID: 369
		public class Ldftn_ : CodeMatch
		{
			// Token: 0x170001C7 RID: 455
			public Code.Ldftn_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldftn_)base.Set(operand, name);
				}
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x000145F8 File Offset: 0x000127F8
			public Ldftn_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000172 RID: 370
		public class Ldvirtftn_ : CodeMatch
		{
			// Token: 0x170001C8 RID: 456
			public Code.Ldvirtftn_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldvirtftn_)base.Set(operand, name);
				}
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x00014628 File Offset: 0x00012828
			public Ldvirtftn_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000173 RID: 371
		public class Ldarg_ : CodeMatch
		{
			// Token: 0x170001C9 RID: 457
			public Code.Ldarg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_)base.Set(operand, name);
				}
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x00014658 File Offset: 0x00012858
			public Ldarg_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000174 RID: 372
		public class Ldarga_ : CodeMatch
		{
			// Token: 0x170001CA RID: 458
			public Code.Ldarga_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarga_)base.Set(operand, name);
				}
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x00014688 File Offset: 0x00012888
			public Ldarga_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000175 RID: 373
		public class Starg_ : CodeMatch
		{
			// Token: 0x170001CB RID: 459
			public Code.Starg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Starg_)base.Set(operand, name);
				}
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x000146B8 File Offset: 0x000128B8
			public Starg_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000176 RID: 374
		public class Ldloc_ : CodeMatch
		{
			// Token: 0x170001CC RID: 460
			public Code.Ldloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_)base.Set(operand, name);
				}
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x000146E8 File Offset: 0x000128E8
			public Ldloc_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000177 RID: 375
		public class Ldloca_ : CodeMatch
		{
			// Token: 0x170001CD RID: 461
			public Code.Ldloca_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloca_)base.Set(operand, name);
				}
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x00014718 File Offset: 0x00012918
			public Ldloca_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000178 RID: 376
		public class Stloc_ : CodeMatch
		{
			// Token: 0x170001CE RID: 462
			public Code.Stloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_)base.Set(operand, name);
				}
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x00014748 File Offset: 0x00012948
			public Stloc_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000179 RID: 377
		public class Localloc_ : CodeMatch
		{
			// Token: 0x170001CF RID: 463
			public Code.Localloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Localloc_)base.Set(operand, name);
				}
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x00014778 File Offset: 0x00012978
			public Localloc_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017A RID: 378
		public class Endfilter_ : CodeMatch
		{
			// Token: 0x170001D0 RID: 464
			public Code.Endfilter_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Endfilter_)base.Set(operand, name);
				}
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x000147A8 File Offset: 0x000129A8
			public Endfilter_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017B RID: 379
		public class Unaligned_ : CodeMatch
		{
			// Token: 0x170001D1 RID: 465
			public Code.Unaligned_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unaligned_)base.Set(operand, name);
				}
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x000147D8 File Offset: 0x000129D8
			public Unaligned_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017C RID: 380
		public class Volatile_ : CodeMatch
		{
			// Token: 0x170001D2 RID: 466
			public Code.Volatile_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Volatile_)base.Set(operand, name);
				}
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x00014808 File Offset: 0x00012A08
			public Volatile_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017D RID: 381
		public class Tailcall_ : CodeMatch
		{
			// Token: 0x170001D3 RID: 467
			public Code.Tailcall_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Tailcall_)base.Set(operand, name);
				}
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x00014838 File Offset: 0x00012A38
			public Tailcall_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017E RID: 382
		public class Initobj_ : CodeMatch
		{
			// Token: 0x170001D4 RID: 468
			public Code.Initobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Initobj_)base.Set(operand, name);
				}
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x00014868 File Offset: 0x00012A68
			public Initobj_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x0200017F RID: 383
		public class Constrained_ : CodeMatch
		{
			// Token: 0x170001D5 RID: 469
			public Code.Constrained_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Constrained_)base.Set(operand, name);
				}
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x00014898 File Offset: 0x00012A98
			public Constrained_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000180 RID: 384
		public class Cpblk_ : CodeMatch
		{
			// Token: 0x170001D6 RID: 470
			public Code.Cpblk_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cpblk_)base.Set(operand, name);
				}
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x000148C8 File Offset: 0x00012AC8
			public Cpblk_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000181 RID: 385
		public class Initblk_ : CodeMatch
		{
			// Token: 0x170001D7 RID: 471
			public Code.Initblk_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Initblk_)base.Set(operand, name);
				}
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x000148F8 File Offset: 0x00012AF8
			public Initblk_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000182 RID: 386
		public class Rethrow_ : CodeMatch
		{
			// Token: 0x170001D8 RID: 472
			public Code.Rethrow_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rethrow_)base.Set(operand, name);
				}
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00014928 File Offset: 0x00012B28
			public Rethrow_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000183 RID: 387
		public class Sizeof_ : CodeMatch
		{
			// Token: 0x170001D9 RID: 473
			public Code.Sizeof_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sizeof_)base.Set(operand, name);
				}
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x00014958 File Offset: 0x00012B58
			public Sizeof_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000184 RID: 388
		public class Refanytype_ : CodeMatch
		{
			// Token: 0x170001DA RID: 474
			public Code.Refanytype_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Refanytype_)base.Set(operand, name);
				}
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x00014988 File Offset: 0x00012B88
			public Refanytype_()
				: base(null, null, null)
			{
			}
		}

		// Token: 0x02000185 RID: 389
		public class Readonly_ : CodeMatch
		{
			// Token: 0x170001DB RID: 475
			public Code.Readonly_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Readonly_)base.Set(operand, name);
				}
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x000149B8 File Offset: 0x00012BB8
			public Readonly_()
				: base(null, null, null)
			{
			}
		}
	}
}
