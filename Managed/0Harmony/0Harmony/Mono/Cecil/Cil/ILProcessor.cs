using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BB RID: 699
	public sealed class ILProcessor
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x000395CB File Offset: 0x000377CB
		public MethodBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000395D3 File Offset: 0x000377D3
		internal ILProcessor(MethodBody body)
		{
			this.body = body;
			this.instructions = body.Instructions;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000395EE File Offset: 0x000377EE
		public Instruction Create(OpCode opcode)
		{
			return Instruction.Create(opcode);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000395F6 File Offset: 0x000377F6
		public Instruction Create(OpCode opcode, TypeReference type)
		{
			return Instruction.Create(opcode, type);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000395FF File Offset: 0x000377FF
		public Instruction Create(OpCode opcode, CallSite site)
		{
			return Instruction.Create(opcode, site);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00039608 File Offset: 0x00037808
		public Instruction Create(OpCode opcode, MethodReference method)
		{
			return Instruction.Create(opcode, method);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00039611 File Offset: 0x00037811
		public Instruction Create(OpCode opcode, FieldReference field)
		{
			return Instruction.Create(opcode, field);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003961A File Offset: 0x0003781A
		public Instruction Create(OpCode opcode, string value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00039623 File Offset: 0x00037823
		public Instruction Create(OpCode opcode, sbyte value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003962C File Offset: 0x0003782C
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

		// Token: 0x060011B3 RID: 4531 RVA: 0x00039684 File Offset: 0x00037884
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

		// Token: 0x060011B4 RID: 4532 RVA: 0x000396D9 File Offset: 0x000378D9
		public Instruction Create(OpCode opcode, long value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000396E2 File Offset: 0x000378E2
		public Instruction Create(OpCode opcode, float value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000396EB File Offset: 0x000378EB
		public Instruction Create(OpCode opcode, double value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000396F4 File Offset: 0x000378F4
		public Instruction Create(OpCode opcode, Instruction target)
		{
			return Instruction.Create(opcode, target);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000396FD File Offset: 0x000378FD
		public Instruction Create(OpCode opcode, Instruction[] targets)
		{
			return Instruction.Create(opcode, targets);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00039706 File Offset: 0x00037906
		public Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			return Instruction.Create(opcode, variable);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003970F File Offset: 0x0003790F
		public Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			return Instruction.Create(opcode, parameter);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00039718 File Offset: 0x00037918
		public void Emit(OpCode opcode)
		{
			this.Append(this.Create(opcode));
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00039727 File Offset: 0x00037927
		public void Emit(OpCode opcode, TypeReference type)
		{
			this.Append(this.Create(opcode, type));
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00039737 File Offset: 0x00037937
		public void Emit(OpCode opcode, MethodReference method)
		{
			this.Append(this.Create(opcode, method));
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00039747 File Offset: 0x00037947
		public void Emit(OpCode opcode, CallSite site)
		{
			this.Append(this.Create(opcode, site));
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00039757 File Offset: 0x00037957
		public void Emit(OpCode opcode, FieldReference field)
		{
			this.Append(this.Create(opcode, field));
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00039767 File Offset: 0x00037967
		public void Emit(OpCode opcode, string value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00039777 File Offset: 0x00037977
		public void Emit(OpCode opcode, byte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00039787 File Offset: 0x00037987
		public void Emit(OpCode opcode, sbyte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00039797 File Offset: 0x00037997
		public void Emit(OpCode opcode, int value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000397A7 File Offset: 0x000379A7
		public void Emit(OpCode opcode, long value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000397B7 File Offset: 0x000379B7
		public void Emit(OpCode opcode, float value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000397C7 File Offset: 0x000379C7
		public void Emit(OpCode opcode, double value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000397D7 File Offset: 0x000379D7
		public void Emit(OpCode opcode, Instruction target)
		{
			this.Append(this.Create(opcode, target));
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000397E7 File Offset: 0x000379E7
		public void Emit(OpCode opcode, Instruction[] targets)
		{
			this.Append(this.Create(opcode, targets));
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000397F7 File Offset: 0x000379F7
		public void Emit(OpCode opcode, VariableDefinition variable)
		{
			this.Append(this.Create(opcode, variable));
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00039807 File Offset: 0x00037A07
		public void Emit(OpCode opcode, ParameterDefinition parameter)
		{
			this.Append(this.Create(opcode, parameter));
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00039818 File Offset: 0x00037A18
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

		// Token: 0x060011CC RID: 4556 RVA: 0x0003986C File Offset: 0x00037A6C
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

		// Token: 0x060011CD RID: 4557 RVA: 0x000398C0 File Offset: 0x00037AC0
		public void InsertAfter(int index, Instruction instruction)
		{
			if (index < 0 || index >= this.instructions.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instructions.Insert(index + 1, instruction);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000398FC File Offset: 0x00037AFC
		public void Append(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instructions.Add(instruction);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00039918 File Offset: 0x00037B18
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

		// Token: 0x060011D0 RID: 4560 RVA: 0x00039945 File Offset: 0x00037B45
		public void Replace(int index, Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.InsertAfter(index, instruction);
			this.RemoveAt(index);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00039964 File Offset: 0x00037B64
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

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003998D File Offset: 0x00037B8D
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.instructions.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.instructions.RemoveAt(index);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000399B8 File Offset: 0x00037BB8
		public void Clear()
		{
			this.instructions.Clear();
		}

		// Token: 0x040007F5 RID: 2037
		private readonly MethodBody body;

		// Token: 0x040007F6 RID: 2038
		private readonly Collection<Instruction> instructions;
	}
}
