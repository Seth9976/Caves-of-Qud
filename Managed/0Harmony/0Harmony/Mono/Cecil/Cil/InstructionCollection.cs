using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BF RID: 703
	internal class InstructionCollection : Collection<Instruction>
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x0003A248 File Offset: 0x00038448
		internal InstructionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003A257 File Offset: 0x00038457
		internal InstructionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003A268 File Offset: 0x00038468
		protected override void OnAdd(Instruction item, int index)
		{
			if (index == 0)
			{
				return;
			}
			Instruction instruction = this.items[index - 1];
			instruction.next = item;
			item.previous = instruction;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003A294 File Offset: 0x00038494
		protected override void OnInsert(Instruction item, int index)
		{
			if (this.size != 0)
			{
				Instruction instruction = this.items[index];
				if (instruction == null)
				{
					Instruction instruction2 = this.items[index - 1];
					instruction2.next = item;
					item.previous = instruction2;
					return;
				}
				int offset = instruction.Offset;
				Instruction previous = instruction.previous;
				if (previous != null)
				{
					previous.next = item;
					item.previous = previous;
				}
				instruction.previous = item;
				item.next = instruction;
			}
			this.UpdateLocalScopes(null, null);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003A304 File Offset: 0x00038504
		protected override void OnSet(Instruction item, int index)
		{
			Instruction instruction = this.items[index];
			item.previous = instruction.previous;
			item.next = instruction.next;
			instruction.previous = null;
			instruction.next = null;
			this.UpdateLocalScopes(item, instruction);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0003A348 File Offset: 0x00038548
		protected override void OnRemove(Instruction item, int index)
		{
			Instruction previous = item.previous;
			if (previous != null)
			{
				previous.next = item.next;
			}
			Instruction next = item.next;
			if (next != null)
			{
				next.previous = item.previous;
			}
			this.RemoveSequencePoint(item);
			this.UpdateLocalScopes(item, next ?? previous);
			item.previous = null;
			item.next = null;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003A3A4 File Offset: 0x000385A4
		private void RemoveSequencePoint(Instruction instruction)
		{
			MethodDebugInformation debug_info = this.method.debug_info;
			if (debug_info == null || !debug_info.HasSequencePoints)
			{
				return;
			}
			Collection<SequencePoint> sequence_points = debug_info.sequence_points;
			for (int i = 0; i < sequence_points.Count; i++)
			{
				if (sequence_points[i].Offset == instruction.offset)
				{
					sequence_points.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0003A400 File Offset: 0x00038600
		private void UpdateLocalScopes(Instruction removedInstruction, Instruction existingInstruction)
		{
			MethodDebugInformation debug_info = this.method.debug_info;
			if (debug_info == null)
			{
				return;
			}
			InstructionCollection.InstructionOffsetCache instructionOffsetCache = new InstructionCollection.InstructionOffsetCache
			{
				Offset = 0,
				Index = 0,
				Instruction = this.items[0]
			};
			this.UpdateLocalScope(debug_info.Scope, removedInstruction, existingInstruction, ref instructionOffsetCache);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003A458 File Offset: 0x00038658
		private void UpdateLocalScope(ScopeDebugInformation scope, Instruction removedInstruction, Instruction existingInstruction, ref InstructionCollection.InstructionOffsetCache cache)
		{
			if (scope == null)
			{
				return;
			}
			if (!scope.Start.IsResolved)
			{
				scope.Start = this.ResolveInstructionOffset(scope.Start, ref cache);
			}
			if (!scope.Start.IsEndOfMethod && scope.Start.ResolvedInstruction == removedInstruction)
			{
				scope.Start = new InstructionOffset(existingInstruction);
			}
			if (scope.HasScopes)
			{
				foreach (ScopeDebugInformation scopeDebugInformation in scope.Scopes)
				{
					this.UpdateLocalScope(scopeDebugInformation, removedInstruction, existingInstruction, ref cache);
				}
			}
			if (!scope.End.IsResolved)
			{
				scope.End = this.ResolveInstructionOffset(scope.End, ref cache);
			}
			if (!scope.End.IsEndOfMethod && scope.End.ResolvedInstruction == removedInstruction)
			{
				scope.End = new InstructionOffset(existingInstruction);
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0003A560 File Offset: 0x00038760
		private InstructionOffset ResolveInstructionOffset(InstructionOffset inputOffset, ref InstructionCollection.InstructionOffsetCache cache)
		{
			if (inputOffset.IsResolved)
			{
				return inputOffset;
			}
			int offset = inputOffset.Offset;
			if (cache.Offset == offset)
			{
				return new InstructionOffset(cache.Instruction);
			}
			if (cache.Offset > offset)
			{
				int num = 0;
				for (int i = 0; i < this.items.Length; i++)
				{
					if (num == offset)
					{
						return new InstructionOffset(this.items[i]);
					}
					if (num > offset)
					{
						return new InstructionOffset(this.items[i - 1]);
					}
					num += this.items[i].GetSize();
				}
				return default(InstructionOffset);
			}
			int num2 = cache.Offset;
			for (int j = cache.Index; j < this.items.Length; j++)
			{
				cache.Index = j;
				cache.Offset = num2;
				Instruction instruction = this.items[j];
				if (instruction == null)
				{
					break;
				}
				cache.Instruction = instruction;
				if (cache.Offset == offset)
				{
					return new InstructionOffset(cache.Instruction);
				}
				if (cache.Offset > offset)
				{
					return new InstructionOffset(this.items[j - 1]);
				}
				num2 += instruction.GetSize();
			}
			return default(InstructionOffset);
		}

		// Token: 0x04000806 RID: 2054
		private readonly MethodDefinition method;

		// Token: 0x020002C0 RID: 704
		private struct InstructionOffsetCache
		{
			// Token: 0x04000807 RID: 2055
			public int Offset;

			// Token: 0x04000808 RID: 2056
			public int Index;

			// Token: 0x04000809 RID: 2057
			public Instruction Instruction;
		}
	}
}
