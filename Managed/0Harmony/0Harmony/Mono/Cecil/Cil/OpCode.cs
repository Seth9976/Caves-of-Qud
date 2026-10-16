using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002C5 RID: 709
	public struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0003A682 File Offset: 0x00038882
		public string Name
		{
			get
			{
				return OpCodeNames.names[(int)this.Code];
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0003A690 File Offset: 0x00038890
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

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0003A6A2 File Offset: 0x000388A2
		public byte Op1
		{
			get
			{
				return this.op1;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0003A6AA File Offset: 0x000388AA
		public byte Op2
		{
			get
			{
				return this.op2;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0003A6B2 File Offset: 0x000388B2
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0003A6D8 File Offset: 0x000388D8
		public Code Code
		{
			get
			{
				return (Code)this.code;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0003A6E0 File Offset: 0x000388E0
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow_control;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0003A6E8 File Offset: 0x000388E8
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.opcode_type;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003A6F0 File Offset: 0x000388F0
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.operand_type;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0003A6F8 File Offset: 0x000388F8
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_pop;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0003A700 File Offset: 0x00038900
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_push;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003A708 File Offset: 0x00038908
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

		// Token: 0x06001221 RID: 4641 RVA: 0x0003A7CF File Offset: 0x000389CF
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003A7D8 File Offset: 0x000389D8
		public override bool Equals(object obj)
		{
			if (!(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return this.op1 == opCode.op1 && this.op2 == opCode.op2;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003A814 File Offset: 0x00038A14
		public bool Equals(OpCode opcode)
		{
			return this.op1 == opcode.op1 && this.op2 == opcode.op2;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003A814 File Offset: 0x00038A14
		public static bool operator ==(OpCode one, OpCode other)
		{
			return one.op1 == other.op1 && one.op2 == other.op2;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003A834 File Offset: 0x00038A34
		public static bool operator !=(OpCode one, OpCode other)
		{
			return one.op1 != other.op1 || one.op2 != other.op2;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003A857 File Offset: 0x00038A57
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400084E RID: 2126
		private readonly byte op1;

		// Token: 0x0400084F RID: 2127
		private readonly byte op2;

		// Token: 0x04000850 RID: 2128
		private readonly byte code;

		// Token: 0x04000851 RID: 2129
		private readonly byte flow_control;

		// Token: 0x04000852 RID: 2130
		private readonly byte opcode_type;

		// Token: 0x04000853 RID: 2131
		private readonly byte operand_type;

		// Token: 0x04000854 RID: 2132
		private readonly byte stack_behavior_pop;

		// Token: 0x04000855 RID: 2133
		private readonly byte stack_behavior_push;
	}
}
