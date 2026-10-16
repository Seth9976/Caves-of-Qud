using System;
using System.Reflection.Metadata;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C6 RID: 1222
	internal static class ILOpCodeExtensions
	{
		// Token: 0x06003134 RID: 12596 RVA: 0x0008CB9C File Offset: 0x0008AD9C
		public static int Size(this ILOpCode opcode)
		{
			if (opcode <= (ILOpCode)255)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x0008CBA9 File Offset: 0x0008ADA9
		public static ILOpCode GetLeaveOpcode(this ILOpCode opcode)
		{
			if (opcode == ILOpCode.Br_s)
			{
				return ILOpCode.Leave_s;
			}
			if (opcode == ILOpCode.Br)
			{
				return ILOpCode.Leave;
			}
			throw ExceptionUtilities.UnexpectedValue(opcode);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0008CBCC File Offset: 0x0008ADCC
		public static bool HasVariableStackBehavior(this ILOpCode opcode)
		{
			return opcode - ILOpCode.Call <= 2 || opcode == ILOpCode.Callvirt || opcode == ILOpCode.Newobj;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0008CBE4 File Offset: 0x0008ADE4
		public static bool IsControlTransfer(this ILOpCode opcode)
		{
			if (opcode.IsBranch())
			{
				return true;
			}
			if (opcode <= ILOpCode.Switch)
			{
				if (opcode != ILOpCode.Jmp && opcode != ILOpCode.Ret && opcode != ILOpCode.Switch)
				{
					return false;
				}
			}
			else if (opcode <= ILOpCode.Endfinally)
			{
				if (opcode != ILOpCode.Throw && opcode != ILOpCode.Endfinally)
				{
					return false;
				}
			}
			else if (opcode != ILOpCode.Endfilter && opcode != ILOpCode.Rethrow)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x0008CC3B File Offset: 0x0008AE3B
		public static bool IsConditionalBranch(this ILOpCode opcode)
		{
			return opcode - ILOpCode.Brfalse_s <= 11 || opcode - ILOpCode.Brfalse <= 11;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x0008CC50 File Offset: 0x0008AE50
		public static bool IsRelationalBranch(this ILOpCode opcode)
		{
			return opcode - ILOpCode.Beq_s <= 9 || opcode - ILOpCode.Beq <= 9;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x0008CC65 File Offset: 0x0008AE65
		public static bool CanFallThrough(this ILOpCode opcode)
		{
			if (opcode <= ILOpCode.Br)
			{
				if (opcode != ILOpCode.Jmp && opcode - ILOpCode.Ret > 1 && opcode != ILOpCode.Br)
				{
					return true;
				}
			}
			else if (opcode != ILOpCode.Throw && opcode - ILOpCode.Endfinally > 2 && opcode != ILOpCode.Rethrow)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0008CC99 File Offset: 0x0008AE99
		public static int NetStackBehavior(this ILOpCode opcode)
		{
			return opcode.StackPushCount() - opcode.StackPopCount();
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0008CCA8 File Offset: 0x0008AEA8
		public static int StackPopCount(this ILOpCode opcode)
		{
			switch (opcode)
			{
			case ILOpCode.Nop:
			case ILOpCode.Break:
			case ILOpCode.Ldarg_0:
			case ILOpCode.Ldarg_1:
			case ILOpCode.Ldarg_2:
			case ILOpCode.Ldarg_3:
			case ILOpCode.Ldloc_0:
			case ILOpCode.Ldloc_1:
			case ILOpCode.Ldloc_2:
			case ILOpCode.Ldloc_3:
				return 0;
			case ILOpCode.Stloc_0:
			case ILOpCode.Stloc_1:
			case ILOpCode.Stloc_2:
			case ILOpCode.Stloc_3:
				return 1;
			case ILOpCode.Ldarg_s:
			case ILOpCode.Ldarga_s:
				return 0;
			case ILOpCode.Starg_s:
				return 1;
			case ILOpCode.Ldloc_s:
			case ILOpCode.Ldloca_s:
				return 0;
			case ILOpCode.Stloc_s:
				return 1;
			case ILOpCode.Ldnull:
			case ILOpCode.Ldc_i4_m1:
			case ILOpCode.Ldc_i4_0:
			case ILOpCode.Ldc_i4_1:
			case ILOpCode.Ldc_i4_2:
			case ILOpCode.Ldc_i4_3:
			case ILOpCode.Ldc_i4_4:
			case ILOpCode.Ldc_i4_5:
			case ILOpCode.Ldc_i4_6:
			case ILOpCode.Ldc_i4_7:
			case ILOpCode.Ldc_i4_8:
			case ILOpCode.Ldc_i4_s:
			case ILOpCode.Ldc_i4:
			case ILOpCode.Ldc_i8:
			case ILOpCode.Ldc_r4:
			case ILOpCode.Ldc_r8:
				return 0;
			case (ILOpCode)36:
			case (ILOpCode)119:
			case (ILOpCode)120:
			case (ILOpCode)166:
			case (ILOpCode)167:
			case (ILOpCode)168:
			case (ILOpCode)169:
			case (ILOpCode)170:
			case (ILOpCode)171:
			case (ILOpCode)172:
			case (ILOpCode)173:
			case (ILOpCode)174:
			case (ILOpCode)175:
			case (ILOpCode)176:
			case (ILOpCode)177:
			case (ILOpCode)178:
			case (ILOpCode)187:
			case (ILOpCode)188:
			case (ILOpCode)189:
			case (ILOpCode)190:
			case (ILOpCode)191:
			case (ILOpCode)192:
			case (ILOpCode)193:
			case (ILOpCode)196:
			case (ILOpCode)197:
			case (ILOpCode)199:
			case (ILOpCode)200:
			case (ILOpCode)201:
			case (ILOpCode)202:
			case (ILOpCode)203:
			case (ILOpCode)204:
			case (ILOpCode)205:
			case (ILOpCode)206:
			case (ILOpCode)207:
				break;
			case ILOpCode.Dup:
			case ILOpCode.Pop:
				return 1;
			case ILOpCode.Jmp:
				return 0;
			case ILOpCode.Call:
			case ILOpCode.Calli:
			case ILOpCode.Ret:
				return -1;
			case ILOpCode.Br_s:
				return 0;
			case ILOpCode.Brfalse_s:
			case ILOpCode.Brtrue_s:
				return 1;
			case ILOpCode.Beq_s:
			case ILOpCode.Bge_s:
			case ILOpCode.Bgt_s:
			case ILOpCode.Ble_s:
			case ILOpCode.Blt_s:
			case ILOpCode.Bne_un_s:
			case ILOpCode.Bge_un_s:
			case ILOpCode.Bgt_un_s:
			case ILOpCode.Ble_un_s:
			case ILOpCode.Blt_un_s:
				return 2;
			case ILOpCode.Br:
				return 0;
			case ILOpCode.Brfalse:
			case ILOpCode.Brtrue:
				return 1;
			case ILOpCode.Beq:
			case ILOpCode.Bge:
			case ILOpCode.Bgt:
			case ILOpCode.Ble:
			case ILOpCode.Blt:
			case ILOpCode.Bne_un:
			case ILOpCode.Bge_un:
			case ILOpCode.Bgt_un:
			case ILOpCode.Ble_un:
			case ILOpCode.Blt_un:
				return 2;
			case ILOpCode.Switch:
			case ILOpCode.Ldind_i1:
			case ILOpCode.Ldind_u1:
			case ILOpCode.Ldind_i2:
			case ILOpCode.Ldind_u2:
			case ILOpCode.Ldind_i4:
			case ILOpCode.Ldind_u4:
			case ILOpCode.Ldind_i8:
			case ILOpCode.Ldind_i:
			case ILOpCode.Ldind_r4:
			case ILOpCode.Ldind_r8:
			case ILOpCode.Ldind_ref:
				return 1;
			case ILOpCode.Stind_ref:
			case ILOpCode.Stind_i1:
			case ILOpCode.Stind_i2:
			case ILOpCode.Stind_i4:
			case ILOpCode.Stind_i8:
			case ILOpCode.Stind_r4:
			case ILOpCode.Stind_r8:
			case ILOpCode.Add:
			case ILOpCode.Sub:
			case ILOpCode.Mul:
			case ILOpCode.Div:
			case ILOpCode.Div_un:
			case ILOpCode.Rem:
			case ILOpCode.Rem_un:
			case ILOpCode.And:
			case ILOpCode.Or:
			case ILOpCode.Xor:
			case ILOpCode.Shl:
			case ILOpCode.Shr:
			case ILOpCode.Shr_un:
				return 2;
			case ILOpCode.Neg:
			case ILOpCode.Not:
			case ILOpCode.Conv_i1:
			case ILOpCode.Conv_i2:
			case ILOpCode.Conv_i4:
			case ILOpCode.Conv_i8:
			case ILOpCode.Conv_r4:
			case ILOpCode.Conv_r8:
			case ILOpCode.Conv_u4:
			case ILOpCode.Conv_u8:
				return 1;
			case ILOpCode.Callvirt:
				return -1;
			case ILOpCode.Cpobj:
				return 2;
			case ILOpCode.Ldobj:
				return 1;
			case ILOpCode.Ldstr:
				return 0;
			case ILOpCode.Newobj:
				return -1;
			case ILOpCode.Castclass:
			case ILOpCode.Isinst:
			case ILOpCode.Conv_r_un:
			case ILOpCode.Unbox:
			case ILOpCode.Throw:
			case ILOpCode.Ldfld:
			case ILOpCode.Ldflda:
				return 1;
			case ILOpCode.Stfld:
				return 2;
			case ILOpCode.Ldsfld:
			case ILOpCode.Ldsflda:
				return 0;
			case ILOpCode.Stsfld:
				return 1;
			case ILOpCode.Stobj:
				return 2;
			case ILOpCode.Conv_ovf_i1_un:
			case ILOpCode.Conv_ovf_i2_un:
			case ILOpCode.Conv_ovf_i4_un:
			case ILOpCode.Conv_ovf_i8_un:
			case ILOpCode.Conv_ovf_u1_un:
			case ILOpCode.Conv_ovf_u2_un:
			case ILOpCode.Conv_ovf_u4_un:
			case ILOpCode.Conv_ovf_u8_un:
			case ILOpCode.Conv_ovf_i_un:
			case ILOpCode.Conv_ovf_u_un:
			case ILOpCode.Box:
			case ILOpCode.Newarr:
			case ILOpCode.Ldlen:
				return 1;
			case ILOpCode.Ldelema:
			case ILOpCode.Ldelem_i1:
			case ILOpCode.Ldelem_u1:
			case ILOpCode.Ldelem_i2:
			case ILOpCode.Ldelem_u2:
			case ILOpCode.Ldelem_i4:
			case ILOpCode.Ldelem_u4:
			case ILOpCode.Ldelem_i8:
			case ILOpCode.Ldelem_i:
			case ILOpCode.Ldelem_r4:
			case ILOpCode.Ldelem_r8:
			case ILOpCode.Ldelem_ref:
				return 2;
			case ILOpCode.Stelem_i:
			case ILOpCode.Stelem_i1:
			case ILOpCode.Stelem_i2:
			case ILOpCode.Stelem_i4:
			case ILOpCode.Stelem_i8:
			case ILOpCode.Stelem_r4:
			case ILOpCode.Stelem_r8:
			case ILOpCode.Stelem_ref:
				return 3;
			case ILOpCode.Ldelem:
				return 2;
			case ILOpCode.Stelem:
				return 3;
			case ILOpCode.Unbox_any:
			case ILOpCode.Conv_ovf_i1:
			case ILOpCode.Conv_ovf_u1:
			case ILOpCode.Conv_ovf_i2:
			case ILOpCode.Conv_ovf_u2:
			case ILOpCode.Conv_ovf_i4:
			case ILOpCode.Conv_ovf_u4:
			case ILOpCode.Conv_ovf_i8:
			case ILOpCode.Conv_ovf_u8:
			case ILOpCode.Refanyval:
			case ILOpCode.Ckfinite:
			case ILOpCode.Mkrefany:
				return 1;
			case ILOpCode.Ldtoken:
				return 0;
			case ILOpCode.Conv_u2:
			case ILOpCode.Conv_u1:
			case ILOpCode.Conv_i:
			case ILOpCode.Conv_ovf_i:
			case ILOpCode.Conv_ovf_u:
				return 1;
			case ILOpCode.Add_ovf:
			case ILOpCode.Add_ovf_un:
			case ILOpCode.Mul_ovf:
			case ILOpCode.Mul_ovf_un:
			case ILOpCode.Sub_ovf:
			case ILOpCode.Sub_ovf_un:
				return 2;
			case ILOpCode.Endfinally:
			case ILOpCode.Leave:
			case ILOpCode.Leave_s:
				return 0;
			case ILOpCode.Stind_i:
				return 2;
			case ILOpCode.Conv_u:
				return 1;
			default:
				switch (opcode)
				{
				case ILOpCode.Arglist:
					return 0;
				case ILOpCode.Ceq:
				case ILOpCode.Cgt:
				case ILOpCode.Cgt_un:
				case ILOpCode.Clt:
				case ILOpCode.Clt_un:
					return 2;
				case ILOpCode.Ldftn:
					return 0;
				case ILOpCode.Ldvirtftn:
					return 1;
				case ILOpCode.Ldarg:
				case ILOpCode.Ldarga:
					return 0;
				case ILOpCode.Starg:
					return 1;
				case ILOpCode.Ldloc:
				case ILOpCode.Ldloca:
					return 0;
				case ILOpCode.Stloc:
				case ILOpCode.Localloc:
				case ILOpCode.Endfilter:
					return 1;
				case ILOpCode.Unaligned:
				case ILOpCode.Volatile:
				case ILOpCode.Tail:
					return 0;
				case ILOpCode.Initobj:
					return 1;
				case ILOpCode.Constrained:
					return 0;
				case ILOpCode.Cpblk:
				case ILOpCode.Initblk:
					return 3;
				case ILOpCode.Rethrow:
				case ILOpCode.Sizeof:
					return 0;
				case ILOpCode.Refanytype:
					return 1;
				case ILOpCode.Readonly:
					return 0;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(opcode);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0008D144 File Offset: 0x0008B344
		public static int StackPushCount(this ILOpCode opcode)
		{
			switch (opcode)
			{
			case ILOpCode.Nop:
			case ILOpCode.Break:
				return 0;
			case ILOpCode.Ldarg_0:
			case ILOpCode.Ldarg_1:
			case ILOpCode.Ldarg_2:
			case ILOpCode.Ldarg_3:
			case ILOpCode.Ldloc_0:
			case ILOpCode.Ldloc_1:
			case ILOpCode.Ldloc_2:
			case ILOpCode.Ldloc_3:
				return 1;
			case ILOpCode.Stloc_0:
			case ILOpCode.Stloc_1:
			case ILOpCode.Stloc_2:
			case ILOpCode.Stloc_3:
				return 0;
			case ILOpCode.Ldarg_s:
			case ILOpCode.Ldarga_s:
				return 1;
			case ILOpCode.Starg_s:
				return 0;
			case ILOpCode.Ldloc_s:
			case ILOpCode.Ldloca_s:
				return 1;
			case ILOpCode.Stloc_s:
				return 0;
			case ILOpCode.Ldnull:
			case ILOpCode.Ldc_i4_m1:
			case ILOpCode.Ldc_i4_0:
			case ILOpCode.Ldc_i4_1:
			case ILOpCode.Ldc_i4_2:
			case ILOpCode.Ldc_i4_3:
			case ILOpCode.Ldc_i4_4:
			case ILOpCode.Ldc_i4_5:
			case ILOpCode.Ldc_i4_6:
			case ILOpCode.Ldc_i4_7:
			case ILOpCode.Ldc_i4_8:
			case ILOpCode.Ldc_i4_s:
			case ILOpCode.Ldc_i4:
			case ILOpCode.Ldc_i8:
			case ILOpCode.Ldc_r4:
			case ILOpCode.Ldc_r8:
				return 1;
			case (ILOpCode)36:
			case (ILOpCode)119:
			case (ILOpCode)120:
			case (ILOpCode)166:
			case (ILOpCode)167:
			case (ILOpCode)168:
			case (ILOpCode)169:
			case (ILOpCode)170:
			case (ILOpCode)171:
			case (ILOpCode)172:
			case (ILOpCode)173:
			case (ILOpCode)174:
			case (ILOpCode)175:
			case (ILOpCode)176:
			case (ILOpCode)177:
			case (ILOpCode)178:
			case (ILOpCode)187:
			case (ILOpCode)188:
			case (ILOpCode)189:
			case (ILOpCode)190:
			case (ILOpCode)191:
			case (ILOpCode)192:
			case (ILOpCode)193:
			case (ILOpCode)196:
			case (ILOpCode)197:
			case (ILOpCode)199:
			case (ILOpCode)200:
			case (ILOpCode)201:
			case (ILOpCode)202:
			case (ILOpCode)203:
			case (ILOpCode)204:
			case (ILOpCode)205:
			case (ILOpCode)206:
			case (ILOpCode)207:
				goto IL_045E;
			case ILOpCode.Dup:
				return 2;
			case ILOpCode.Pop:
			case ILOpCode.Jmp:
				return 0;
			case ILOpCode.Call:
			case ILOpCode.Calli:
				return -1;
			case ILOpCode.Ret:
			case ILOpCode.Br_s:
			case ILOpCode.Brfalse_s:
			case ILOpCode.Brtrue_s:
			case ILOpCode.Beq_s:
			case ILOpCode.Bge_s:
			case ILOpCode.Bgt_s:
			case ILOpCode.Ble_s:
			case ILOpCode.Blt_s:
			case ILOpCode.Bne_un_s:
			case ILOpCode.Bge_un_s:
			case ILOpCode.Bgt_un_s:
			case ILOpCode.Ble_un_s:
			case ILOpCode.Blt_un_s:
			case ILOpCode.Br:
			case ILOpCode.Brfalse:
			case ILOpCode.Brtrue:
			case ILOpCode.Beq:
			case ILOpCode.Bge:
			case ILOpCode.Bgt:
			case ILOpCode.Ble:
			case ILOpCode.Blt:
			case ILOpCode.Bne_un:
			case ILOpCode.Bge_un:
			case ILOpCode.Bgt_un:
			case ILOpCode.Ble_un:
			case ILOpCode.Blt_un:
			case ILOpCode.Switch:
				return 0;
			case ILOpCode.Ldind_i1:
			case ILOpCode.Ldind_u1:
			case ILOpCode.Ldind_i2:
			case ILOpCode.Ldind_u2:
			case ILOpCode.Ldind_i4:
			case ILOpCode.Ldind_u4:
			case ILOpCode.Ldind_i8:
			case ILOpCode.Ldind_i:
			case ILOpCode.Ldind_r4:
			case ILOpCode.Ldind_r8:
			case ILOpCode.Ldind_ref:
				return 1;
			case ILOpCode.Stind_ref:
			case ILOpCode.Stind_i1:
			case ILOpCode.Stind_i2:
			case ILOpCode.Stind_i4:
			case ILOpCode.Stind_i8:
			case ILOpCode.Stind_r4:
			case ILOpCode.Stind_r8:
				return 0;
			case ILOpCode.Add:
			case ILOpCode.Sub:
			case ILOpCode.Mul:
			case ILOpCode.Div:
			case ILOpCode.Div_un:
			case ILOpCode.Rem:
			case ILOpCode.Rem_un:
			case ILOpCode.And:
			case ILOpCode.Or:
			case ILOpCode.Xor:
			case ILOpCode.Shl:
			case ILOpCode.Shr:
			case ILOpCode.Shr_un:
			case ILOpCode.Neg:
			case ILOpCode.Not:
			case ILOpCode.Conv_i1:
			case ILOpCode.Conv_i2:
			case ILOpCode.Conv_i4:
			case ILOpCode.Conv_i8:
			case ILOpCode.Conv_r4:
			case ILOpCode.Conv_r8:
			case ILOpCode.Conv_u4:
			case ILOpCode.Conv_u8:
				return 1;
			case ILOpCode.Callvirt:
				return -1;
			case ILOpCode.Cpobj:
				return 0;
			case ILOpCode.Ldobj:
			case ILOpCode.Ldstr:
			case ILOpCode.Newobj:
			case ILOpCode.Castclass:
			case ILOpCode.Isinst:
			case ILOpCode.Conv_r_un:
			case ILOpCode.Unbox:
				return 1;
			case ILOpCode.Throw:
				return 0;
			case ILOpCode.Ldfld:
			case ILOpCode.Ldflda:
				return 1;
			case ILOpCode.Stfld:
				return 0;
			case ILOpCode.Ldsfld:
			case ILOpCode.Ldsflda:
				return 1;
			case ILOpCode.Stsfld:
			case ILOpCode.Stobj:
				return 0;
			case ILOpCode.Conv_ovf_i1_un:
			case ILOpCode.Conv_ovf_i2_un:
			case ILOpCode.Conv_ovf_i4_un:
			case ILOpCode.Conv_ovf_i8_un:
			case ILOpCode.Conv_ovf_u1_un:
			case ILOpCode.Conv_ovf_u2_un:
			case ILOpCode.Conv_ovf_u4_un:
			case ILOpCode.Conv_ovf_u8_un:
			case ILOpCode.Conv_ovf_i_un:
			case ILOpCode.Conv_ovf_u_un:
			case ILOpCode.Box:
			case ILOpCode.Newarr:
			case ILOpCode.Ldlen:
			case ILOpCode.Ldelema:
			case ILOpCode.Ldelem_i1:
			case ILOpCode.Ldelem_u1:
			case ILOpCode.Ldelem_i2:
			case ILOpCode.Ldelem_u2:
			case ILOpCode.Ldelem_i4:
			case ILOpCode.Ldelem_u4:
			case ILOpCode.Ldelem_i8:
			case ILOpCode.Ldelem_i:
			case ILOpCode.Ldelem_r4:
			case ILOpCode.Ldelem_r8:
			case ILOpCode.Ldelem_ref:
				return 1;
			case ILOpCode.Stelem_i:
			case ILOpCode.Stelem_i1:
			case ILOpCode.Stelem_i2:
			case ILOpCode.Stelem_i4:
			case ILOpCode.Stelem_i8:
			case ILOpCode.Stelem_r4:
			case ILOpCode.Stelem_r8:
			case ILOpCode.Stelem_ref:
				return 0;
			case ILOpCode.Ldelem:
				return 1;
			case ILOpCode.Stelem:
				return 0;
			case ILOpCode.Unbox_any:
			case ILOpCode.Conv_ovf_i1:
			case ILOpCode.Conv_ovf_u1:
			case ILOpCode.Conv_ovf_i2:
			case ILOpCode.Conv_ovf_u2:
			case ILOpCode.Conv_ovf_i4:
			case ILOpCode.Conv_ovf_u4:
			case ILOpCode.Conv_ovf_i8:
			case ILOpCode.Conv_ovf_u8:
			case ILOpCode.Refanyval:
			case ILOpCode.Ckfinite:
			case ILOpCode.Mkrefany:
			case ILOpCode.Ldtoken:
			case ILOpCode.Conv_u2:
			case ILOpCode.Conv_u1:
			case ILOpCode.Conv_i:
			case ILOpCode.Conv_ovf_i:
			case ILOpCode.Conv_ovf_u:
			case ILOpCode.Add_ovf:
			case ILOpCode.Add_ovf_un:
			case ILOpCode.Mul_ovf:
			case ILOpCode.Mul_ovf_un:
			case ILOpCode.Sub_ovf:
			case ILOpCode.Sub_ovf_un:
				return 1;
			case ILOpCode.Endfinally:
			case ILOpCode.Leave:
			case ILOpCode.Leave_s:
			case ILOpCode.Stind_i:
				return 0;
			case ILOpCode.Conv_u:
				break;
			default:
				switch (opcode)
				{
				case ILOpCode.Arglist:
				case ILOpCode.Ceq:
				case ILOpCode.Cgt:
				case ILOpCode.Cgt_un:
				case ILOpCode.Clt:
				case ILOpCode.Clt_un:
				case ILOpCode.Ldftn:
				case ILOpCode.Ldvirtftn:
				case ILOpCode.Ldarg:
				case ILOpCode.Ldarga:
					break;
				case (ILOpCode)65032:
				case (ILOpCode)65040:
				case (ILOpCode)65049:
				case (ILOpCode)65051:
					goto IL_045E;
				case ILOpCode.Starg:
					return 0;
				case ILOpCode.Ldloc:
				case ILOpCode.Ldloca:
					return 1;
				case ILOpCode.Stloc:
					return 0;
				case ILOpCode.Localloc:
					return 1;
				case ILOpCode.Endfilter:
				case ILOpCode.Unaligned:
				case ILOpCode.Volatile:
				case ILOpCode.Tail:
				case ILOpCode.Initobj:
				case ILOpCode.Constrained:
				case ILOpCode.Cpblk:
				case ILOpCode.Initblk:
				case ILOpCode.Rethrow:
					return 0;
				case ILOpCode.Sizeof:
				case ILOpCode.Refanytype:
					return 1;
				case ILOpCode.Readonly:
					return 0;
				default:
					goto IL_045E;
				}
				break;
			}
			return 1;
			IL_045E:
			throw ExceptionUtilities.UnexpectedValue(opcode);
		}
	}
}
