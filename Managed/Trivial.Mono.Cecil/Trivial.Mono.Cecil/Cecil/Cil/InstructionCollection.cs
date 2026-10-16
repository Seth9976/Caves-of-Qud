using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000117 RID: 279
	internal class InstructionCollection : Collection<Instruction>
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0002340D File Offset: 0x0002160D
		internal InstructionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002341C File Offset: 0x0002161C
		internal InstructionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002342C File Offset: 0x0002162C
		protected override void OnAdd(Instruction item, int index)
		{
			if (index != 0)
			{
				Instruction instruction = this.items[index - 1];
				instruction.next = item;
				item.previous = instruction;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00023458 File Offset: 0x00021658
		protected override void OnInsert(Instruction item, int index)
		{
			if (this.size == 0)
			{
				return;
			}
			Instruction instruction = this.items[index];
			if (instruction == null)
			{
				Instruction instruction2 = this.items[index - 1];
				instruction2.next = item;
				item.previous = instruction2;
				return;
			}
			Instruction previous = instruction.previous;
			if (previous != null)
			{
				previous.next = item;
				item.previous = previous;
			}
			instruction.previous = item;
			item.next = instruction;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000234BC File Offset: 0x000216BC
		protected override void OnSet(Instruction item, int index)
		{
			Instruction instruction = this.items[index];
			item.previous = instruction.previous;
			item.next = instruction.next;
			instruction.previous = null;
			instruction.next = null;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000234F8 File Offset: 0x000216F8
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
			item.previous = null;
			item.next = null;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00023548 File Offset: 0x00021748
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

		// Token: 0x0400057A RID: 1402
		private readonly MethodDefinition method;
	}
}
