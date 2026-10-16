using System;
using System.Text;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BC RID: 700
	public sealed class Instruction
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000399C5 File Offset: 0x00037BC5
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x000399CD File Offset: 0x00037BCD
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x000399D6 File Offset: 0x00037BD6
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x000399DE File Offset: 0x00037BDE
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000399E7 File Offset: 0x00037BE7
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x000399EF File Offset: 0x00037BEF
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000399F8 File Offset: 0x00037BF8
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x00039A00 File Offset: 0x00037C00
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00039A09 File Offset: 0x00037C09
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x00039A11 File Offset: 0x00037C11
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

		// Token: 0x060011DE RID: 4574 RVA: 0x00039A1A File Offset: 0x00037C1A
		internal Instruction(int offset, OpCode opCode)
		{
			this.offset = offset;
			this.opcode = opCode;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00039A30 File Offset: 0x00037C30
		internal Instruction(OpCode opcode, object operand)
		{
			this.opcode = opcode;
			this.operand = operand;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00039A48 File Offset: 0x00037C48
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

		// Token: 0x060011E1 RID: 4577 RVA: 0x00039AEC File Offset: 0x00037CEC
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

		// Token: 0x060011E2 RID: 4578 RVA: 0x00039BE0 File Offset: 0x00037DE0
		private static void AppendLabel(StringBuilder builder, Instruction instruction)
		{
			builder.Append("IL_");
			builder.Append(instruction.offset.ToString("x4"));
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00039C05 File Offset: 0x00037E05
		public static Instruction Create(OpCode opcode)
		{
			if (opcode.OperandType != OperandType.InlineNone)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, null);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00039C23 File Offset: 0x00037E23
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

		// Token: 0x060011E5 RID: 4581 RVA: 0x00039C5B File Offset: 0x00037E5B
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

		// Token: 0x060011E6 RID: 4582 RVA: 0x00039C88 File Offset: 0x00037E88
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

		// Token: 0x060011E7 RID: 4583 RVA: 0x00039CBF File Offset: 0x00037EBF
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

		// Token: 0x060011E8 RID: 4584 RVA: 0x00039CF6 File Offset: 0x00037EF6
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

		// Token: 0x060011E9 RID: 4585 RVA: 0x00039D23 File Offset: 0x00037F23
		public static Instruction Create(OpCode opcode, sbyte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI && opcode != OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00039D54 File Offset: 0x00037F54
		public static Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI || opcode == OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00039D85 File Offset: 0x00037F85
		public static Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType != OperandType.InlineI)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00039DA8 File Offset: 0x00037FA8
		public static Instruction Create(OpCode opcode, long value)
		{
			if (opcode.OperandType != OperandType.InlineI8)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00039DCB File Offset: 0x00037FCB
		public static Instruction Create(OpCode opcode, float value)
		{
			if (opcode.OperandType != OperandType.ShortInlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00039DEF File Offset: 0x00037FEF
		public static Instruction Create(OpCode opcode, double value)
		{
			if (opcode.OperandType != OperandType.InlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00039E12 File Offset: 0x00038012
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

		// Token: 0x060011F0 RID: 4592 RVA: 0x00039E48 File Offset: 0x00038048
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

		// Token: 0x060011F1 RID: 4593 RVA: 0x00039E75 File Offset: 0x00038075
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

		// Token: 0x060011F2 RID: 4594 RVA: 0x00039EAD File Offset: 0x000380AD
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

		// Token: 0x040007F7 RID: 2039
		internal int offset;

		// Token: 0x040007F8 RID: 2040
		internal OpCode opcode;

		// Token: 0x040007F9 RID: 2041
		internal object operand;

		// Token: 0x040007FA RID: 2042
		internal Instruction previous;

		// Token: 0x040007FB RID: 2043
		internal Instruction next;
	}
}
