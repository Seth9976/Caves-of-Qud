using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000186 RID: 390
	public class CodeMatch : CodeInstruction
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x000149D6 File Offset: 0x00012BD6
		internal CodeMatch Set(object operand, string name)
		{
			if (this.operand == null)
			{
				this.operand = operand;
			}
			if (this.name == null)
			{
				this.name = name;
			}
			return this;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000149F8 File Offset: 0x00012BF8
		public CodeMatch(OpCode? opcode = null, object operand = null, string name = null)
		{
			if (opcode != null)
			{
				OpCode valueOrDefault = opcode.GetValueOrDefault();
				this.opcode = valueOrDefault;
				this.opcodes.Add(valueOrDefault);
			}
			if (operand != null)
			{
				this.operands.Add(operand);
			}
			this.operand = operand;
			this.name = name;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00014A78 File Offset: 0x00012C78
		public CodeMatch(Expression<Action> expression, string name = null)
		{
			this.opcodes.AddRange(new OpCode[]
			{
				OpCodes.Call,
				OpCodes.Callvirt
			});
			this.operand = SymbolExtensions.GetMethodInfo(expression);
			this.name = name;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00014AF4 File Offset: 0x00012CF4
		public CodeMatch(LambdaExpression expression, string name = null)
		{
			this.opcodes.AddRange(new OpCode[]
			{
				OpCodes.Call,
				OpCodes.Callvirt
			});
			this.operand = SymbolExtensions.GetMethodInfo(expression);
			this.name = name;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00014B6F File Offset: 0x00012D6F
		public CodeMatch(CodeInstruction instruction, string name = null)
			: this(new OpCode?(instruction.opcode), instruction.operand, name)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00014B8C File Offset: 0x00012D8C
		public CodeMatch(Func<CodeInstruction, bool> predicate, string name = null)
		{
			this.predicate = predicate;
			this.name = name;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00014BDC File Offset: 0x00012DDC
		internal bool Matches(List<CodeInstruction> codes, CodeInstruction instruction)
		{
			if (this.predicate != null)
			{
				return this.predicate(instruction);
			}
			if (this.opcodes.Count > 0 && !this.opcodes.Contains(instruction.opcode))
			{
				return false;
			}
			if (this.operands.Count > 0 && !this.operands.Contains(instruction.operand))
			{
				return false;
			}
			if (this.labels.Count > 0 && !this.labels.Intersect<Label>(instruction.labels).Any<Label>())
			{
				return false;
			}
			if (this.blocks.Count > 0 && !this.blocks.Intersect<ExceptionBlock>(instruction.blocks).Any<ExceptionBlock>())
			{
				return false;
			}
			if (this.jumpsFrom.Count > 0 && !this.jumpsFrom.Select<int, object>((int index) => codes[index].operand).OfType<Label>().Intersect<Label>(instruction.labels)
				.Any<Label>())
			{
				return false;
			}
			if (this.jumpsTo.Count > 0)
			{
				object operand = instruction.operand;
				if (operand == null || operand.GetType() != typeof(Label))
				{
					return false;
				}
				Label label = (Label)operand;
				IEnumerable<int> enumerable = from idx in Enumerable.Range(0, codes.Count)
					where codes[idx].labels.Contains(label)
					select idx;
				if (!this.jumpsTo.Intersect<int>(enumerable).Any<int>())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00014D68 File Offset: 0x00012F68
		public override string ToString()
		{
			string text = "[";
			if (this.name != null)
			{
				text = text + this.name + ": ";
			}
			if (this.opcodes.Count > 0)
			{
				text = text + "opcodes=" + this.opcodes.Join<OpCode>(null, ", ") + " ";
			}
			if (this.operands.Count > 0)
			{
				text = text + "operands=" + this.operands.Join<object>(null, ", ") + " ";
			}
			if (this.labels.Count > 0)
			{
				text = text + "labels=" + this.labels.Join<Label>(null, ", ") + " ";
			}
			if (this.blocks.Count > 0)
			{
				text = text + "blocks=" + this.blocks.Join<ExceptionBlock>(null, ", ") + " ";
			}
			if (this.jumpsFrom.Count > 0)
			{
				text = text + "jumpsFrom=" + this.jumpsFrom.Join<int>(null, ", ") + " ";
			}
			if (this.jumpsTo.Count > 0)
			{
				text = text + "jumpsTo=" + this.jumpsTo.Join<int>(null, ", ") + " ";
			}
			if (this.predicate != null)
			{
				text += "predicate=yes ";
			}
			return text.TrimEnd(Array.Empty<char>()) + "]";
		}

		// Token: 0x040001EA RID: 490
		public string name;

		// Token: 0x040001EB RID: 491
		public List<OpCode> opcodes = new List<OpCode>();

		// Token: 0x040001EC RID: 492
		public List<object> operands = new List<object>();

		// Token: 0x040001ED RID: 493
		public List<int> jumpsFrom = new List<int>();

		// Token: 0x040001EE RID: 494
		public List<int> jumpsTo = new List<int>();

		// Token: 0x040001EF RID: 495
		public Func<CodeInstruction, bool> predicate;
	}
}
