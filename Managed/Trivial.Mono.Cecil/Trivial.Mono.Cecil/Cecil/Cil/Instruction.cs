using System;
using System.Text;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000116 RID: 278
	public sealed class Instruction
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00022EEE File Offset: 0x000210EE
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00022EF6 File Offset: 0x000210F6
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00022EFF File Offset: 0x000210FF
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00022F07 File Offset: 0x00021107
		public OpCode OpCode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00022F10 File Offset: 0x00021110
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00022F18 File Offset: 0x00021118
		public object Operand
		{
			get
			{
				return this.operand;
			}
			set
			{
				this.operand = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00022F21 File Offset: 0x00021121
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00022F29 File Offset: 0x00021129
		public Instruction Previous
		{
			get
			{
				return this.previous;
			}
			set
			{
				this.previous = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00022F32 File Offset: 0x00021132
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00022F3A File Offset: 0x0002113A
		public Instruction Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00022F43 File Offset: 0x00021143
		internal Instruction(int offset, OpCode opCode)
		{
			this.offset = offset;
			this.opcode = opCode;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00022F59 File Offset: 0x00021159
		internal Instruction(OpCode opcode, object operand)
		{
			this.opcode = opcode;
			this.operand = operand;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00022F70 File Offset: 0x00021170
		public int GetSize()
		{
			int size = this.opcode.Size;
			switch (this.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				return size + 4;
			case OperandType.InlineI8:
			case OperandType.InlineR:
				return size + 8;
			case OperandType.InlineSwitch:
				return size + (1 + ((Instruction[])this.operand).Length) * 4;
			case OperandType.InlineVar:
			case OperandType.InlineArg:
				return size + 2;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
			case OperandType.ShortInlineArg:
				return size + 1;
			}
			return size;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00023014 File Offset: 0x00021214
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Instruction.AppendLabel(stringBuilder, this);
			stringBuilder.Append(':');
			stringBuilder.Append(' ');
			stringBuilder.Append(this.opcode.Name);
			if (this.operand == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append(' ');
			OperandType operandType = this.opcode.OperandType;
			if (operandType <= OperandType.InlineString)
			{
				if (operandType != OperandType.InlineBrTarget)
				{
					if (operandType != OperandType.InlineString)
					{
						goto IL_00D4;
					}
					stringBuilder.Append('"');
					stringBuilder.Append(this.operand);
					stringBuilder.Append('"');
					goto IL_00E1;
				}
			}
			else
			{
				if (operandType == OperandType.InlineSwitch)
				{
					Instruction[] array = (Instruction[])this.operand;
					for (int i = 0; i < array.Length; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(',');
						}
						Instruction.AppendLabel(stringBuilder, array[i]);
					}
					goto IL_00E1;
				}
				if (operandType != OperandType.ShortInlineBrTarget)
				{
					goto IL_00D4;
				}
			}
			Instruction.AppendLabel(stringBuilder, (Instruction)this.operand);
			goto IL_00E1;
			IL_00D4:
			stringBuilder.Append(this.operand);
			IL_00E1:
			return stringBuilder.ToString();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00023108 File Offset: 0x00021308
		private static void AppendLabel(StringBuilder builder, Instruction instruction)
		{
			builder.Append("IL_");
			builder.Append(instruction.offset.ToString("x4"));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002312D File Offset: 0x0002132D
		public static Instruction Create(OpCode opcode)
		{
			if (opcode.OperandType != OperandType.InlineNone)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, null);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002314B File Offset: 0x0002134B
		public static Instruction Create(OpCode opcode, TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (opcode.OperandType != OperandType.InlineType && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, type);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00023183 File Offset: 0x00021383
		public static Instruction Create(OpCode opcode, CallSite site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			if (opcode.Code != Code.Calli)
			{
				throw new ArgumentException("code");
			}
			return new Instruction(opcode, site);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000231B0 File Offset: 0x000213B0
		public static Instruction Create(OpCode opcode, MethodReference method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (opcode.OperandType != OperandType.InlineMethod && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, method);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000231E7 File Offset: 0x000213E7
		public static Instruction Create(OpCode opcode, FieldReference field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (opcode.OperandType != OperandType.InlineField && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, field);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002321E File Offset: 0x0002141E
		public static Instruction Create(OpCode opcode, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (opcode.OperandType != OperandType.InlineString)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002324B File Offset: 0x0002144B
		public static Instruction Create(OpCode opcode, sbyte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI && opcode != OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002327C File Offset: 0x0002147C
		public static Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI || opcode == OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000232AD File Offset: 0x000214AD
		public static Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType != OperandType.InlineI)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000232D0 File Offset: 0x000214D0
		public static Instruction Create(OpCode opcode, long value)
		{
			if (opcode.OperandType != OperandType.InlineI8)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000232F3 File Offset: 0x000214F3
		public static Instruction Create(OpCode opcode, float value)
		{
			if (opcode.OperandType != OperandType.ShortInlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00023317 File Offset: 0x00021517
		public static Instruction Create(OpCode opcode, double value)
		{
			if (opcode.OperandType != OperandType.InlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002333A File Offset: 0x0002153A
		public static Instruction Create(OpCode opcode, Instruction target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (opcode.OperandType != OperandType.InlineBrTarget && opcode.OperandType != OperandType.ShortInlineBrTarget)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, target);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00023370 File Offset: 0x00021570
		public static Instruction Create(OpCode opcode, Instruction[] targets)
		{
			if (targets == null)
			{
				throw new ArgumentNullException("targets");
			}
			if (opcode.OperandType != OperandType.InlineSwitch)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, targets);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002339D File Offset: 0x0002159D
		public static Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (opcode.OperandType != OperandType.ShortInlineVar && opcode.OperandType != OperandType.InlineVar)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, variable);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000233D5 File Offset: 0x000215D5
		public static Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			if (opcode.OperandType != OperandType.ShortInlineArg && opcode.OperandType != OperandType.InlineArg)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, parameter);
		}

		// Token: 0x04000575 RID: 1397
		internal int offset;

		// Token: 0x04000576 RID: 1398
		internal OpCode opcode;

		// Token: 0x04000577 RID: 1399
		internal object operand;

		// Token: 0x04000578 RID: 1400
		internal Instruction previous;

		// Token: 0x04000579 RID: 1401
		internal Instruction next;
	}
}
