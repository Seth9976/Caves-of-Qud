using System;

namespace XRL.World.Parts
{
	// Token: 0x0200106A RID: 4202
	[Serializable]
	public class MovespeedInLiquid : IPart
	{
		// Token: 0x0600A750 RID: 42832 RVA: 0x003D2210 File Offset: 0x003D0410
		public override bool SameAs(IPart p)
		{
			MovespeedInLiquid movespeedInLiquid = p as MovespeedInLiquid;
			return movespeedInLiquid.MS == this.MS && movespeedInLiquid.MinVolume == this.MinVolume && !(movespeedInLiquid.Liquid != this.Liquid) && movespeedInLiquid.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A751 RID: 42833 RVA: 0x003D2270 File Offset: 0x003D0470
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A752 RID: 42834 RVA: 0x003D2296 File Offset: 0x003D0496
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "EndTurn");
			E.Actor.RegisterPartEvent(this, "EnteredCell");
			this.CurrentBonus = 0;
			this.CheckLiquid();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A753 RID: 42835 RVA: 0x003D22D0 File Offset: 0x003D04D0
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.CurrentBonus > 0)
			{
				if (E.Actor != null)
				{
					base.StatShifter.RemoveStatShift(E.Actor, "MoveSpeed", false);
				}
				this.CurrentBonus = 0;
			}
			E.Actor.UnregisterPartEvent(this, "EndTurn");
			E.Actor.UnregisterPartEvent(this, "EnteredCell");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A754 RID: 42836 RVA: 0x003D2338 File Offset: 0x003D0538
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription && this.MS != 0)
			{
				E.Postfix.Append("\n&C");
				if (this.MS > 0)
				{
					E.Postfix.Append('+').Append(this.MS);
				}
				else
				{
					E.Postfix.Append(this.MS);
				}
				E.Postfix.Append(" move speed while moving through at least ").Append(this.MinVolume).Append(' ')
					.Append((this.MinVolume == 1) ? "dram" : "drams")
					.Append(" of ")
					.Append(string.IsNullOrEmpty(this.Liquid) ? "liquid" : this.Liquid);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A755 RID: 42837 RVA: 0x003D2410 File Offset: 0x003D0610
		private void CheckLiquid()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped == null)
			{
				return;
			}
			Cell currentCell = equipped.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			bool flag = false;
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				LiquidVolume liquidVolume = currentCell.Objects[i].LiquidVolume;
				if (liquidVolume != null && liquidVolume.IsOpenVolume() && liquidVolume.Volume >= this.MinVolume && (string.IsNullOrEmpty(this.Liquid) || liquidVolume.Amount(this.Liquid) >= this.MinVolume))
				{
					flag = true;
					break;
				}
				i++;
			}
			if (flag)
			{
				if (this.CurrentBonus == 0)
				{
					this.CurrentBonus = this.MS;
					base.StatShifter.SetStatShift(equipped, "MoveSpeed", -this.MS, false);
					return;
				}
			}
			else if (this.CurrentBonus > 0)
			{
				base.StatShifter.RemoveStatShift(equipped, "MoveSpeed", false);
				this.CurrentBonus = 0;
			}
		}

		// Token: 0x0600A756 RID: 42838 RVA: 0x003D24FD File Offset: 0x003D06FD
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A757 RID: 42839 RVA: 0x003D2500 File Offset: 0x003D0700
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" || E.ID == "EndTurn")
			{
				this.CheckLiquid();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004512 RID: 17682
		public int MS = 50;

		// Token: 0x04004513 RID: 17683
		public int MinVolume = 200;

		// Token: 0x04004514 RID: 17684
		public int CurrentBonus;

		// Token: 0x04004515 RID: 17685
		public string Liquid;

		// Token: 0x04004516 RID: 17686
		public bool ShowInShortDescription = true;
	}
}
