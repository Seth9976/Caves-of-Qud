using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011F RID: 287
	public struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00023A8C File Offset: 0x00021C8C
		public string Name
		{
			get
			{
				return OpCodeNames.names[(int)this.Code];
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00023A9A File Offset: 0x00021C9A
		public int Size
		{
			get
			{
				if (this.op1 != 255)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00023AAC File Offset: 0x00021CAC
		public byte Op1
		{
			get
			{
				return this.op1;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public byte Op2
		{
			get
			{
				return this.op2;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00023ABC File Offset: 0x00021CBC
		public short Value
		{
			get
			{
				if (this.op1 != 255)
				{
					return (short)(((int)this.op1 << 8) | (int)this.op2);
				}
				return (short)this.op2;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00023AE2 File Offset: 0x00021CE2
		public Code Code
		{
			get
			{
				return (Code)this.code;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00023AEA File Offset: 0x00021CEA
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow_control;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00023AF2 File Offset: 0x00021CF2
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.opcode_type;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00023AFA File Offset: 0x00021CFA
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.operand_type;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00023B02 File Offset: 0x00021D02
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_pop;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00023B0A File Offset: 0x00021D0A
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_push;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00023B14 File Offset: 0x00021D14
		internal OpCode(int x, int y)
		{
			this.op1 = (byte)(x & 255);
			this.op2 = (byte)((x >> 8) & 255);
			this.code = (byte)((x >> 16) & 255);
			this.flow_control = (byte)((x >> 24) & 255);
			this.opcode_type = (byte)(y & 255);
			this.operand_type = (byte)((y >> 8) & 255);
			this.stack_behavior_pop = (byte)((y >> 16) & 255);
			this.stack_behavior_push = (byte)((y >> 24) & 255);
			if (this.op1 == 255)
			{
				OpCodes.OneByteOpCode[(int)this.op2] = this;
				return;
			}
			OpCodes.TwoBytesOpCode[(int)this.op2] = this;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00023BDB File Offset: 0x00021DDB
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public override bool Equals(object obj)
		{
			if (!(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return this.op1 == opCode.op1 && this.op2 == opCode.op2;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00023C20 File Offset: 0x00021E20
		public bool Equals(OpCode opcode)
		{
			return this.op1 == opcode.op1 && this.op2 == opcode.op2;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00023C20 File Offset: 0x00021E20
		public static bool operator ==(OpCode one, OpCode other)
		{
			return one.op1 == other.op1 && one.op2 == other.op2;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00023C40 File Offset: 0x00021E40
		public static bool operator !=(OpCode one, OpCode other)
		{
			return one.op1 != other.op1 || one.op2 != other.op2;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00023C63 File Offset: 0x00021E63
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400058E RID: 1422
		private readonly byte op1;

		// Token: 0x0400058F RID: 1423
		private readonly byte op2;

		// Token: 0x04000590 RID: 1424
		private readonly byte code;

		// Token: 0x04000591 RID: 1425
		private readonly byte flow_control;

		// Token: 0x04000592 RID: 1426
		private readonly byte opcode_type;

		// Token: 0x04000593 RID: 1427
		private readonly byte operand_type;

		// Token: 0x04000594 RID: 1428
		private readonly byte stack_behavior_pop;

		// Token: 0x04000595 RID: 1429
		private readonly byte stack_behavior_push;
	}
}
