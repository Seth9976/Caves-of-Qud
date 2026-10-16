using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004A RID: 74
	public static class ILOpCodeExtensions
	{
		/// <summary>Verifies if the specified op-code is a branch to a label.</summary>
		/// <param name="opCode" />
		/// <returns>
		///   <see langword="true" /> if the specified op-code is a branch to a label, <see langword="false" /> otherwise.</returns>
		// Token: 0x060002DB RID: 731 RVA: 0x00008D4D File Offset: 0x00006F4D
		public static bool IsBranch(this ILOpCode opCode)
		{
			return opCode - ILOpCode.Br_s <= 25 || opCode - ILOpCode.Leave <= 1;
		}

		/// <summary>Calculates the size of the specified branch instruction operand.</summary>
		/// <param name="opCode">The branch op-code.</param>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="opCode" /> is not a branch op-code.</exception>
		/// <returns>1 if <paramref name="opCode" /> is a short branch, or 4 if it is a long branch.</returns>
		// Token: 0x060002DC RID: 732 RVA: 0x00008D64 File Offset: 0x00006F64
		public static int GetBranchOperandSize(this ILOpCode opCode)
		{
			if (opCode <= ILOpCode.Blt_un)
			{
				if (opCode - ILOpCode.Br_s > 12)
				{
					if (opCode - ILOpCode.Br > 12)
					{
						goto IL_002B;
					}
					return 4;
				}
			}
			else
			{
				if (opCode == ILOpCode.Leave)
				{
					return 4;
				}
				if (opCode != ILOpCode.Leave_s)
				{
					goto IL_002B;
				}
			}
			return 1;
			IL_002B:
			throw new ArgumentException(SR.Format(SR.UnexpectedOpCode, opCode), "opCode");
		}

		/// <summary>Gets a short form of the specified branch op-code.</summary>
		/// <param name="opCode">The branch op-code.</param>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="opCode" /> is not a branch op-code.</exception>
		/// <returns>The short form of the branch op-code.</returns>
		// Token: 0x060002DD RID: 733 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public static ILOpCode GetShortBranch(this ILOpCode opCode)
		{
			switch (opCode)
			{
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
				break;
			case ILOpCode.Br:
				return ILOpCode.Br_s;
			case ILOpCode.Brfalse:
				return ILOpCode.Brfalse_s;
			case ILOpCode.Brtrue:
				return ILOpCode.Brtrue_s;
			case ILOpCode.Beq:
				return ILOpCode.Beq_s;
			case ILOpCode.Bge:
				return ILOpCode.Bge_s;
			case ILOpCode.Bgt:
				return ILOpCode.Bgt_s;
			case ILOpCode.Ble:
				return ILOpCode.Ble_s;
			case ILOpCode.Blt:
				return ILOpCode.Blt_s;
			case ILOpCode.Bne_un:
				return ILOpCode.Bne_un_s;
			case ILOpCode.Bge_un:
				return ILOpCode.Bge_un_s;
			case ILOpCode.Bgt_un:
				return ILOpCode.Bgt_un_s;
			case ILOpCode.Ble_un:
				return ILOpCode.Ble_un_s;
			case ILOpCode.Blt_un:
				return ILOpCode.Blt_un_s;
			default:
				if (opCode == ILOpCode.Leave)
				{
					return ILOpCode.Leave_s;
				}
				if (opCode != ILOpCode.Leave_s)
				{
					throw new ArgumentException(SR.Format(SR.UnexpectedOpCode, opCode), "opCode");
				}
				break;
			}
			return opCode;
		}

		/// <summary>Gets a long form of the specified branch op-code.</summary>
		/// <param name="opCode">The branch op-code.</param>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="opCode" /> is not a branch op-code.</exception>
		/// <returns>The long form of the branch op-code.</returns>
		// Token: 0x060002DE RID: 734 RVA: 0x00008E90 File Offset: 0x00007090
		public static ILOpCode GetLongBranch(this ILOpCode opCode)
		{
			switch (opCode)
			{
			case ILOpCode.Br_s:
				return ILOpCode.Br;
			case ILOpCode.Brfalse_s:
				return ILOpCode.Brfalse;
			case ILOpCode.Brtrue_s:
				return ILOpCode.Brtrue;
			case ILOpCode.Beq_s:
				return ILOpCode.Beq;
			case ILOpCode.Bge_s:
				return ILOpCode.Bge;
			case ILOpCode.Bgt_s:
				return ILOpCode.Bgt;
			case ILOpCode.Ble_s:
				return ILOpCode.Ble;
			case ILOpCode.Blt_s:
				return ILOpCode.Blt;
			case ILOpCode.Bne_un_s:
				return ILOpCode.Bne_un;
			case ILOpCode.Bge_un_s:
				return ILOpCode.Bge_un;
			case ILOpCode.Bgt_un_s:
				return ILOpCode.Bgt_un;
			case ILOpCode.Ble_un_s:
				return ILOpCode.Ble_un;
			case ILOpCode.Blt_un_s:
				return ILOpCode.Blt_un;
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
				break;
			default:
				if (opCode != ILOpCode.Leave)
				{
					if (opCode != ILOpCode.Leave_s)
					{
						throw new ArgumentException(SR.Format(SR.UnexpectedOpCode, opCode), "opCode");
					}
					return ILOpCode.Leave;
				}
				break;
			}
			return opCode;
		}
	}
}
