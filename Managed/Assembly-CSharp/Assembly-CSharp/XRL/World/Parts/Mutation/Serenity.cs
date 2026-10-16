using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F8 RID: 5112
	[Serializable]
	public class Serenity : BaseMutation
	{
		// Token: 0x0600C631 RID: 50737 RVA: 0x00482A44 File Offset: 0x00480C44
		public Serenity()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C632 RID: 50738 RVA: 0x00482A57 File Offset: 0x00480C57
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C633 RID: 50739 RVA: 0x00482A6D File Offset: 0x00480C6D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C634 RID: 50740 RVA: 0x00482A90 File Offset: 0x00480C90
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSerenity");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C635 RID: 50741 RVA: 0x00482AA8 File Offset: 0x00480CA8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSerenity")
			{
				string text = "SerenityWorld.40.12.1.1.10";
				Zone zone = The.ZoneManager.SetActiveZone(text);
				Cell cell = null;
				for (int i = zone.Height - 2; i >= 0; i--)
				{
					for (int j = zone.Width / 2; j >= 0; j--)
					{
						Cell cell2 = zone.GetCell(j, i);
						if (cell2.IsReachable() && cell2.IsEmpty())
						{
							cell = cell2;
							break;
						}
						Cell cell3 = zone.GetCell(40 - j, i);
						if (cell3.IsReachable() && cell3.IsEmpty())
						{
							cell = cell3;
							break;
						}
						if (cell != null)
						{
							break;
						}
					}
					if (cell != null)
					{
						this.ParentObject.SystemMoveTo(cell, null, false, true, false, false);
						The.ZoneManager.ProcessGoToPartyLeader();
						base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Math.Max(300 - 20 * base.Level, 5), null, null, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C636 RID: 50742 RVA: 0x00482BAC File Offset: 0x00480DAC
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C637 RID: 50743 RVA: 0x00482BB8 File Offset: 0x00480DB8
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Serenity", "CommandSerenity", "Mental Mutations", null, "\u0017", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C638 RID: 50744 RVA: 0x00482C01 File Offset: 0x00480E01
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
