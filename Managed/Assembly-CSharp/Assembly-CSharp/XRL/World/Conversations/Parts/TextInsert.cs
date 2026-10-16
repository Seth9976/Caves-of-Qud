using System;
using System.Text;

namespace XRL.World.Conversations.Parts
{
	/// <summary>Appends or prepends a text that can be either spoken or unspoken to the element.</summary>
	// Token: 0x02000C1B RID: 3099
	public class TextInsert : IPredicatePart
	{
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x002E9AE4 File Offset: 0x002E7CE4
		// (set) Token: 0x06007C0C RID: 31756 RVA: 0x002E9AEF File Offset: 0x002E7CEF
		public bool Append
		{
			get
			{
				return !this.Prepend;
			}
			set
			{
				this.Prepend = !value;
			}
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x002E9AFC File Offset: 0x002E7CFC
		public void Insert(StringBuilder SB)
		{
			if (this.Text.IsNullOrEmpty())
			{
				return;
			}
			if (this.IsKeyboardHotkey && !CapabilityManager.AllowKeyboardHotkeys)
			{
				return;
			}
			if (!this.Check(this.AnyPredicate, null))
			{
				return;
			}
			if (this.IsKeyboardHotkey)
			{
				this.Text = this.Text.Replace("{CmdStartTrade}", ControlManager.getCommandInputFormatted("CmdStartTrade", true, false));
			}
			this.Text = this.Text.GetRandomSubstring('~', false, null);
			if (this.Prepend)
			{
				if (!this.Text.EndsWith("\n"))
				{
					SB.Insert(0, "\n", this.NewLines);
				}
				SB.Insert(0, this.Text);
				return;
			}
			if (!SB.EndsWith('\n'))
			{
				SB.Append('\n', this.NewLines);
			}
			SB.Append(this.Text);
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x002E9BE0 File Offset: 0x002E7DE0
		public override void LoadText(string Text)
		{
			this.Text = Text;
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x002E9BE9 File Offset: 0x002E7DE9
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || (ID == PrepareTextEvent.ID && this.Spoken) || (ID == DisplayTextEvent.ID && !this.Spoken);
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x002E9C1A File Offset: 0x002E7E1A
		public override bool HandleEvent(PrepareTextEvent E)
		{
			this.Insert(E.Text);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x002E9C2F File Offset: 0x002E7E2F
		public override bool HandleEvent(DisplayTextEvent E)
		{
			this.Insert(E.Text);
			return base.HandleEvent(E);
		}

		// Token: 0x04003868 RID: 14440
		public string Text;

		// Token: 0x04003869 RID: 14441
		public bool Prepend;

		// Token: 0x0400386A RID: 14442
		public bool Spoken = true;

		// Token: 0x0400386B RID: 14443
		public int NewLines;

		// Token: 0x0400386C RID: 14444
		public bool IsKeyboardHotkey;

		/// <summary>Require one predicate to match rather than all.</summary>
		// Token: 0x0400386D RID: 14445
		public bool AnyPredicate;
	}
}
