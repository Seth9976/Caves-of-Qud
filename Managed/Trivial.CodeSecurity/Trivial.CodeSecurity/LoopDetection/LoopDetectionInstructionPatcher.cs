using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.CodeSecurity.LoopDetection
{
	// Token: 0x02000012 RID: 18
	internal sealed class LoopDetectionInstructionPatcher
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C6A File Offset: 0x00001E6A
		public MethodReference EnterExecutionTimingMethod
		{
			get
			{
				if (this.enterExecutionTimingMethod == null)
				{
					this.enterExecutionTimingMethod = this.module.ImportReference(typeof(ExecutionTimingServices).GetMethod("EnterTimedExecutionContext"));
				}
				return this.enterExecutionTimingMethod;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003C9F File Offset: 0x00001E9F
		public MethodReference ExitExecutionTimingMethod
		{
			get
			{
				if (this.exitExecutionTimingMethod == null)
				{
					this.exitExecutionTimingMethod = this.module.ImportReference(typeof(ExecutionTimingServices).GetMethod("ExitTimedExecutionContext"));
				}
				return this.exitExecutionTimingMethod;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public LoopDetectionInstructionPatcher(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public bool DetectPotentialLoopInstructions(Collection<Instruction> instructions)
		{
			this.foundLoopingInstructions.Clear();
			foreach (Instruction instruction in instructions)
			{
				if (instruction.Operand is Instruction)
				{
					Instruction instruction2 = instruction.Operand as Instruction;
					if (instruction.Offset > instruction2.Offset)
					{
						this.foundLoopingInstructions.Add(new LoopDetectionInstructionPatcher.LoopingInstruction
						{
							loopStart = instruction2,
							loopJump = instruction
						});
					}
				}
			}
			return this.foundLoopingInstructions.Count > 0;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D9C File Offset: 0x00001F9C
		public bool PatchPotentialLoopInstructions(Collection<Instruction> instructions, LoopDetectionHashGenerator hashGenerator)
		{
			int num = 0;
			foreach (LoopDetectionInstructionPatcher.LoopingInstruction loopingInstruction in this.foundLoopingInstructions)
			{
				int nextHash = hashGenerator.GetNextHash();
				int num2 = instructions.IndexOf(loopingInstruction.loopStart) + 1;
				Instruction instruction = Instruction.Create(OpCodes.Ldc_I4, nextHash);
				Instruction instruction2 = Instruction.Create(OpCodes.Call, this.EnterExecutionTimingMethod);
				instructions.Insert(num2, instruction);
				instructions.Insert(num2 + 1, instruction2);
				int num3 = instructions.IndexOf(loopingInstruction.loopJump) + 1;
				Instruction instruction3 = Instruction.Create(OpCodes.Call, this.ExitExecutionTimingMethod);
				instructions.Insert(num3, instruction);
				instructions.Insert(num3 + 1, instruction3);
				num++;
			}
			return num > 0;
		}

		// Token: 0x0400003C RID: 60
		private List<LoopDetectionInstructionPatcher.LoopingInstruction> foundLoopingInstructions = new List<LoopDetectionInstructionPatcher.LoopingInstruction>();

		// Token: 0x0400003D RID: 61
		private ModuleDefinition module;

		// Token: 0x0400003E RID: 62
		private MethodReference enterExecutionTimingMethod;

		// Token: 0x0400003F RID: 63
		private MethodReference exitExecutionTimingMethod;

		// Token: 0x02000022 RID: 34
		public struct LoopingInstruction
		{
			// Token: 0x0400007D RID: 125
			public Instruction loopStart;

			// Token: 0x0400007E RID: 126
			public Instruction loopJump;
		}
	}
}
