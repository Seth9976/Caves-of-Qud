using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200010D RID: 269
	public sealed class ILProcessor
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00022A3B File Offset: 0x00020C3B
		public MethodBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00022A43 File Offset: 0x00020C43
		internal ILProcessor(MethodBody body)
		{
			this.body = body;
			this.instructions = body.Instructions;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00022A5E File Offset: 0x00020C5E
		public Instruction Create(OpCode opcode)
		{
			return Instruction.Create(opcode);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00022A66 File Offset: 0x00020C66
		public Instruction Create(OpCode opcode, TypeReference type)
		{
			return Instruction.Create(opcode, type);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00022A6F File Offset: 0x00020C6F
		public Instruction Create(OpCode opcode, CallSite site)
		{
			return Instruction.Create(opcode, site);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00022A78 File Offset: 0x00020C78
		public Instruction Create(OpCode opcode, MethodReference method)
		{
			return Instruction.Create(opcode, method);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00022A81 File Offset: 0x00020C81
		public Instruction Create(OpCode opcode, FieldReference field)
		{
			return Instruction.Create(opcode, field);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00022A8A File Offset: 0x00020C8A
		public Instruction Create(OpCode opcode, string value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00022A93 File Offset: 0x00020C93
		public Instruction Create(OpCode opcode, sbyte value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00022A9C File Offset: 0x00020C9C
		public Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType == OperandType.ShortInlineVar)
			{
				return Instruction.Create(opcode, this.body.Variables[(int)value]);
			}
			if (opcode.OperandType == OperandType.ShortInlineArg)
			{
				return Instruction.Create(opcode, this.body.GetParameter((int)value));
			}
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022AF4 File Offset: 0x00020CF4
		public Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType == OperandType.InlineVar)
			{
				return Instruction.Create(opcode, this.body.Variables[value]);
			}
			if (opcode.OperandType == OperandType.InlineArg)
			{
				return Instruction.Create(opcode, this.body.GetParameter(value));
			}
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00022B49 File Offset: 0x00020D49
		public Instruction Create(OpCode opcode, long value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00022B52 File Offset: 0x00020D52
		public Instruction Create(OpCode opcode, float value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00022B5B File Offset: 0x00020D5B
		public Instruction Create(OpCode opcode, double value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00022B64 File Offset: 0x00020D64
		public Instruction Create(OpCode opcode, Instruction target)
		{
			return Instruction.Create(opcode, target);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00022B6D File Offset: 0x00020D6D
		public Instruction Create(OpCode opcode, Instruction[] targets)
		{
			return Instruction.Create(opcode, targets);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00022B76 File Offset: 0x00020D76
		public Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			return Instruction.Create(opcode, variable);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00022B7F File Offset: 0x00020D7F
		public Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			return Instruction.Create(opcode, parameter);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00022B88 File Offset: 0x00020D88
		public void Emit(OpCode opcode)
		{
			this.Append(this.Create(opcode));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00022B97 File Offset: 0x00020D97
		public void Emit(OpCode opcode, TypeReference type)
		{
			this.Append(this.Create(opcode, type));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00022BA7 File Offset: 0x00020DA7
		public void Emit(OpCode opcode, MethodReference method)
		{
			this.Append(this.Create(opcode, method));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00022BB7 File Offset: 0x00020DB7
		public void Emit(OpCode opcode, CallSite site)
		{
			this.Append(this.Create(opcode, site));
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00022BC7 File Offset: 0x00020DC7
		public void Emit(OpCode opcode, FieldReference field)
		{
			this.Append(this.Create(opcode, field));
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00022BD7 File Offset: 0x00020DD7
		public void Emit(OpCode opcode, string value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00022BE7 File Offset: 0x00020DE7
		public void Emit(OpCode opcode, byte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00022BF7 File Offset: 0x00020DF7
		public void Emit(OpCode opcode, sbyte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00022C07 File Offset: 0x00020E07
		public void Emit(OpCode opcode, int value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00022C17 File Offset: 0x00020E17
		public void Emit(OpCode opcode, long value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00022C27 File Offset: 0x00020E27
		public void Emit(OpCode opcode, float value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00022C37 File Offset: 0x00020E37
		public void Emit(OpCode opcode, double value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00022C47 File Offset: 0x00020E47
		public void Emit(OpCode opcode, Instruction target)
		{
			this.Append(this.Create(opcode, target));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00022C57 File Offset: 0x00020E57
		public void Emit(OpCode opcode, Instruction[] targets)
		{
			this.Append(this.Create(opcode, targets));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00022C67 File Offset: 0x00020E67
		public void Emit(OpCode opcode, VariableDefinition variable)
		{
			this.Append(this.Create(opcode, variable));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00022C77 File Offset: 0x00020E77
		public void Emit(OpCode opcode, ParameterDefinition parameter)
		{
			this.Append(this.Create(opcode, parameter));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00022C88 File Offset: 0x00020E88
		public void InsertBefore(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = this.instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			this.instructions.Insert(num, instruction);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00022CDC File Offset: 0x00020EDC
		public void InsertAfter(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = this.instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			this.instructions.Insert(num + 1, instruction);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00022D30 File Offset: 0x00020F30
		public void Append(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instructions.Add(instruction);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00022D4C File Offset: 0x00020F4C
		public void Replace(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.InsertAfter(target, instruction);
			this.Remove(target);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00022D79 File Offset: 0x00020F79
		public void Remove(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			if (!this.instructions.Remove(instruction))
			{
				throw new ArgumentOutOfRangeException("instruction");
			}
		}

		// Token: 0x04000552 RID: 1362
		private readonly MethodBody body;

		// Token: 0x04000553 RID: 1363
		private readonly Collection<Instruction> instructions;
	}
}
