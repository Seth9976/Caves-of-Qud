using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000189 RID: 393
	public class CodeMatcher
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00014F14 File Offset: 0x00013114
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00014F1C File Offset: 0x0001311C
		public int Pos { get; private set; } = -1;

		// Token: 0x0600060A RID: 1546 RVA: 0x00014F25 File Offset: 0x00013125
		private void FixStart()
		{
			this.Pos = Math.Max(0, this.Pos);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00014F39 File Offset: 0x00013139
		private void SetOutOfBounds(int direction)
		{
			this.Pos = ((direction > 0) ? this.Length : (-1));
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00014F4E File Offset: 0x0001314E
		public int Length
		{
			get
			{
				return this.codes.Count;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00014F5B File Offset: 0x0001315B
		public bool IsValid
		{
			get
			{
				return this.Pos >= 0 && this.Pos < this.Length;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00014F76 File Offset: 0x00013176
		public bool IsInvalid
		{
			get
			{
				return this.Pos < 0 || this.Pos >= this.Length;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00014F94 File Offset: 0x00013194
		public int Remaining
		{
			get
			{
				return this.Length - Math.Max(0, this.Pos);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00014FA9 File Offset: 0x000131A9
		public ref OpCode Opcode
		{
			get
			{
				return ref this.codes[this.Pos].opcode;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00014FC1 File Offset: 0x000131C1
		public ref object Operand
		{
			get
			{
				return ref this.codes[this.Pos].operand;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00014FD9 File Offset: 0x000131D9
		public ref List<Label> Labels
		{
			get
			{
				return ref this.codes[this.Pos].labels;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00014FF1 File Offset: 0x000131F1
		public ref List<ExceptionBlock> Blocks
		{
			get
			{
				return ref this.codes[this.Pos].blocks;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015009 File Offset: 0x00013209
		public CodeMatcher()
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00015030 File Offset: 0x00013230
		public CodeMatcher(IEnumerable<CodeInstruction> instructions, ILGenerator generator = null)
		{
			this.generator = generator;
			this.codes = instructions.Select<CodeInstruction, CodeInstruction>((CodeInstruction c) => new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00015098 File Offset: 0x00013298
		public CodeMatcher Clone()
		{
			return new CodeMatcher(this.codes, this.generator)
			{
				Pos = this.Pos,
				lastMatches = this.lastMatches,
				lastError = this.lastError,
				lastMatchCall = this.lastMatchCall
			};
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000150E6 File Offset: 0x000132E6
		public CodeInstruction Instruction
		{
			get
			{
				return this.codes[this.Pos];
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000150F9 File Offset: 0x000132F9
		public CodeInstruction InstructionAt(int offset)
		{
			return this.codes[this.Pos + offset];
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001510E File Offset: 0x0001330E
		public List<CodeInstruction> Instructions()
		{
			return this.codes;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015116 File Offset: 0x00013316
		public IEnumerable<CodeInstruction> InstructionEnumeration()
		{
			return this.codes.AsEnumerable<CodeInstruction>();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015123 File Offset: 0x00013323
		public List<CodeInstruction> Instructions(int count)
		{
			return (from c in this.codes.GetRange(this.Pos, count)
				select new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00015160 File Offset: 0x00013360
		public List<CodeInstruction> InstructionsInRange(int start, int end)
		{
			List<CodeInstruction> list = this.codes;
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			return (from c in list.GetRange(start, end - start + 1)
				select new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000151B1 File Offset: 0x000133B1
		public List<CodeInstruction> InstructionsWithOffsets(int startOffset, int endOffset)
		{
			return this.InstructionsInRange(this.Pos + startOffset, this.Pos + endOffset);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000151C9 File Offset: 0x000133C9
		public List<Label> DistinctLabels(IEnumerable<CodeInstruction> instructions)
		{
			return instructions.SelectMany<CodeInstruction, Label>((CodeInstruction instruction) => instruction.labels).Distinct<Label>().ToList<Label>();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000151FC File Offset: 0x000133FC
		public bool ReportFailure(MethodBase method, Action<string> logger)
		{
			if (this.IsValid)
			{
				return false;
			}
			string text = this.lastError ?? "Unexpected code";
			logger(string.Format("{0} in {1}", text, method));
			return true;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00015236 File Offset: 0x00013436
		public CodeMatcher ThrowIfInvalid(string explanation)
		{
			if (explanation == null)
			{
				throw new ArgumentNullException("explanation");
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException(explanation + " - Current state is invalid");
			}
			return this;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00015260 File Offset: 0x00013460
		public CodeMatcher ThrowIfNotMatch(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfInvalid(explanation);
			if (!this.MatchSequence(this.Pos, matches))
			{
				throw new InvalidOperationException(explanation + " - Match failed");
			}
			return this;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001528C File Offset: 0x0001348C
		private void ThrowIfNotMatch(string explanation, int direction, CodeMatch[] matches)
		{
			this.ThrowIfInvalid(explanation);
			int pos = this.Pos;
			try
			{
				if (this.Match(matches, direction, false).IsInvalid)
				{
					throw new InvalidOperationException(explanation + " - Match failed");
				}
			}
			finally
			{
				this.Pos = pos;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000152E4 File Offset: 0x000134E4
		public CodeMatcher ThrowIfNotMatchForward(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfNotMatch(explanation, 1, matches);
			return this;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000152F0 File Offset: 0x000134F0
		public CodeMatcher ThrowIfNotMatchBack(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfNotMatch(explanation, -1, matches);
			return this;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000152FC File Offset: 0x000134FC
		public CodeMatcher ThrowIfFalse(string explanation, Func<CodeMatcher, bool> stateCheckFunc)
		{
			if (stateCheckFunc == null)
			{
				throw new ArgumentNullException("stateCheckFunc");
			}
			this.ThrowIfInvalid(explanation);
			if (!stateCheckFunc(this))
			{
				throw new InvalidOperationException(explanation + " - Check function returned false");
			}
			return this;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001532F File Offset: 0x0001352F
		public CodeMatcher SetInstruction(CodeInstruction instruction)
		{
			this.codes[this.Pos] = instruction;
			return this;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00015344 File Offset: 0x00013544
		public CodeMatcher SetInstructionAndAdvance(CodeInstruction instruction)
		{
			this.SetInstruction(instruction);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001536A File Offset: 0x0001356A
		public unsafe CodeMatcher Set(OpCode opcode, object operand)
		{
			*this.Opcode = opcode;
			*this.Operand = operand;
			return this;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015384 File Offset: 0x00013584
		public CodeMatcher SetAndAdvance(OpCode opcode, object operand)
		{
			this.Set(opcode, operand);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000153AC File Offset: 0x000135AC
		public unsafe CodeMatcher SetOpcodeAndAdvance(OpCode opcode)
		{
			*this.Opcode = opcode;
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000153D8 File Offset: 0x000135D8
		public unsafe CodeMatcher SetOperandAndAdvance(object operand)
		{
			*this.Operand = operand;
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000153FE File Offset: 0x000135FE
		public unsafe CodeMatcher CreateLabel(out Label label)
		{
			label = this.generator.DefineLabel();
			this.Labels->Add(label);
			return this;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00015424 File Offset: 0x00013624
		public CodeMatcher CreateLabelAt(int position, out Label label)
		{
			label = this.generator.DefineLabel();
			this.AddLabelsAt(position, new Label[] { label });
			return this;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00015453 File Offset: 0x00013653
		public CodeMatcher CreateLabelWithOffsets(int offset, out Label label)
		{
			label = this.generator.DefineLabel();
			return this.AddLabelsAt(this.Pos + offset, new Label[] { label });
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00015487 File Offset: 0x00013687
		public unsafe CodeMatcher AddLabels(IEnumerable<Label> labels)
		{
			this.Labels->AddRange(labels);
			return this;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00015497 File Offset: 0x00013697
		public CodeMatcher AddLabelsAt(int position, IEnumerable<Label> labels)
		{
			this.codes[position].labels.AddRange(labels);
			return this;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000154B1 File Offset: 0x000136B1
		public CodeMatcher SetJumpTo(OpCode opcode, int destination, out Label label)
		{
			this.CreateLabelAt(destination, out label);
			return this.Set(opcode, label);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000154CE File Offset: 0x000136CE
		public CodeMatcher Insert(params CodeInstruction[] instructions)
		{
			this.codes.InsertRange(this.Pos, instructions);
			return this;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000154CE File Offset: 0x000136CE
		public CodeMatcher Insert(IEnumerable<CodeInstruction> instructions)
		{
			this.codes.InsertRange(this.Pos, instructions);
			return this;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000154E4 File Offset: 0x000136E4
		public CodeMatcher InsertBranch(OpCode opcode, int destination)
		{
			Label label;
			this.CreateLabelAt(destination, out label);
			this.codes.Insert(this.Pos, new CodeInstruction(opcode, label));
			return this;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001551C File Offset: 0x0001371C
		public CodeMatcher InsertAndAdvance(params CodeInstruction[] instructions)
		{
			foreach (CodeInstruction codeInstruction in instructions)
			{
				this.Insert(new CodeInstruction[] { codeInstruction });
				int pos = this.Pos;
				this.Pos = pos + 1;
			}
			return this;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00015560 File Offset: 0x00013760
		public CodeMatcher InsertAndAdvance(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction codeInstruction in instructions)
			{
				this.InsertAndAdvance(new CodeInstruction[] { codeInstruction });
			}
			return this;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000155B4 File Offset: 0x000137B4
		public CodeMatcher InsertBranchAndAdvance(OpCode opcode, int destination)
		{
			this.InsertBranch(opcode, destination);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000155DB File Offset: 0x000137DB
		public CodeMatcher RemoveInstruction()
		{
			this.codes.RemoveAt(this.Pos);
			return this;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000155EF File Offset: 0x000137EF
		public CodeMatcher RemoveInstructions(int count)
		{
			this.codes.RemoveRange(this.Pos, count);
			return this;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00015604 File Offset: 0x00013804
		public CodeMatcher RemoveInstructionsInRange(int start, int end)
		{
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			this.codes.RemoveRange(start, end - start + 1);
			return this;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00015622 File Offset: 0x00013822
		public CodeMatcher RemoveInstructionsWithOffsets(int startOffset, int endOffset)
		{
			return this.RemoveInstructionsInRange(this.Pos + startOffset, this.Pos + endOffset);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001563A File Offset: 0x0001383A
		public CodeMatcher Advance(int offset)
		{
			this.Pos += offset;
			if (!this.IsValid)
			{
				this.SetOutOfBounds(offset);
			}
			return this;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001565A File Offset: 0x0001385A
		public CodeMatcher Start()
		{
			this.Pos = 0;
			return this;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00015664 File Offset: 0x00013864
		public CodeMatcher End()
		{
			this.Pos = this.Length - 1;
			return this;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00015675 File Offset: 0x00013875
		public CodeMatcher SearchForward(Func<CodeInstruction, bool> predicate)
		{
			return this.Search(predicate, 1);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001567F File Offset: 0x0001387F
		public CodeMatcher SearchBackwards(Func<CodeInstruction, bool> predicate)
		{
			return this.Search(predicate, -1);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001568C File Offset: 0x0001388C
		private CodeMatcher Search(Func<CodeInstruction, bool> predicate, int direction)
		{
			this.FixStart();
			while (this.IsValid && !predicate(this.Instruction))
			{
				this.Pos += direction;
			}
			this.lastError = (this.IsInvalid ? string.Format("Cannot find {0}", predicate) : null);
			return this;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000156E2 File Offset: 0x000138E2
		public CodeMatcher MatchStartForward(params CodeMatch[] matches)
		{
			return this.Match(matches, 1, false);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000156ED File Offset: 0x000138ED
		public CodeMatcher MatchEndForward(params CodeMatch[] matches)
		{
			return this.Match(matches, 1, true);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000156F8 File Offset: 0x000138F8
		public CodeMatcher MatchStartBackwards(params CodeMatch[] matches)
		{
			return this.Match(matches, -1, false);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00015703 File Offset: 0x00013903
		public CodeMatcher MatchEndBackwards(params CodeMatch[] matches)
		{
			return this.Match(matches, -1, true);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00015710 File Offset: 0x00013910
		private CodeMatcher Match(CodeMatch[] matches, int direction, bool useEnd)
		{
			this.lastMatchCall = delegate
			{
				this.FixStart();
				while (this.IsValid)
				{
					if (this.MatchSequence(this.Pos, matches))
					{
						if (useEnd)
						{
							this.Pos += matches.Length - 1;
							break;
						}
						break;
					}
					else
					{
						this.Pos += direction;
					}
				}
				this.lastError = (this.IsInvalid ? ("Cannot find " + matches.Join<CodeMatch>(null, ", ")) : null);
				return this;
			};
			return this.lastMatchCall();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001575C File Offset: 0x0001395C
		public CodeMatcher Repeat(Action<CodeMatcher> matchAction, Action<string> notFoundAction = null)
		{
			int num = 0;
			if (this.lastMatchCall == null)
			{
				throw new InvalidOperationException("No previous Match operation - cannot repeat");
			}
			while (this.IsValid)
			{
				matchAction(this);
				this.lastMatchCall();
				num++;
			}
			this.lastMatchCall = null;
			if (num == 0 && notFoundAction != null)
			{
				notFoundAction(this.lastError);
			}
			return this;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000157B7 File Offset: 0x000139B7
		public CodeInstruction NamedMatch(string name)
		{
			return this.lastMatches[name];
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000157C8 File Offset: 0x000139C8
		private bool MatchSequence(int start, CodeMatch[] matches)
		{
			if (start < 0)
			{
				return false;
			}
			this.lastMatches = new Dictionary<string, CodeInstruction>();
			foreach (CodeMatch codeMatch in matches)
			{
				if (start >= this.Length || !codeMatch.Matches(this.codes, this.codes[start]))
				{
					return false;
				}
				if (codeMatch.name != null)
				{
					this.lastMatches.Add(codeMatch.name, this.codes[start]);
				}
				start++;
			}
			return true;
		}

		// Token: 0x040001F3 RID: 499
		private readonly ILGenerator generator;

		// Token: 0x040001F4 RID: 500
		private readonly List<CodeInstruction> codes = new List<CodeInstruction>();

		// Token: 0x040001F6 RID: 502
		private Dictionary<string, CodeInstruction> lastMatches = new Dictionary<string, CodeInstruction>();

		// Token: 0x040001F7 RID: 503
		private string lastError;

		// Token: 0x040001F8 RID: 504
		private CodeMatcher.MatchDelegate lastMatchCall;

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x0600064B RID: 1611
		private delegate CodeMatcher MatchDelegate();
	}
}
