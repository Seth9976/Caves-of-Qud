using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013FF RID: 5119
	[Serializable]
	public class TeleportOther : BaseMutation
	{
		// Token: 0x0600C69D RID: 50845 RVA: 0x00486551 File Offset: 0x00484751
		public TeleportOther()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C69E RID: 50846 RVA: 0x00486564 File Offset: 0x00484764
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C69F RID: 50847 RVA: 0x00486584 File Offset: 0x00484784
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, E.Target.CurrentCell, E.Actor, null, this, null, 0))
			{
				E.Add("CommandTeleportOther", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6A0 RID: 50848 RVA: 0x004865F8 File Offset: 0x004847F8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6A1 RID: 50849 RVA: 0x00486620 File Offset: 0x00484820
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandTeleportOther");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C6A2 RID: 50850 RVA: 0x00486635 File Offset: 0x00484835
		public override string GetDescription()
		{
			return "You teleport an adjacent creature to a random nearby location.";
		}

		// Token: 0x0600C6A3 RID: 50851 RVA: 0x0048663C File Offset: 0x0048483C
		public override string GetLevelText(int Level)
		{
			return "Cooldown: {{rules|" + this.GetCooldownTurns(Level).ToString() + "}} rounds";
		}

		// Token: 0x0600C6A4 RID: 50852 RVA: 0x00486667 File Offset: 0x00484867
		public int GetCooldownTurns(int Level)
		{
			return Math.Max(115 - 10 * Level, 5);
		}

		// Token: 0x0600C6A5 RID: 50853 RVA: 0x00486676 File Offset: 0x00484876
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldownTurns(Level), 0);
		}

		// Token: 0x0600C6A6 RID: 50854 RVA: 0x00486694 File Offset: 0x00484894
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandTeleportOther")
			{
				if (!this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
					return false;
				}
				Cell cell = base.PickDirection("Teleport Other", null);
				if (cell == null)
				{
					return false;
				}
				if (cell == this.ParentObject.CurrentCell)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You may not teleport " + this.ParentObject.itself + " with Teleport Other!", true, true, true);
					}
					return false;
				}
				GameObject gameObject = null;
				foreach (GameObject gameObject2 in cell.GetObjectsInCell())
				{
					if (gameObject2.IsCombat)
					{
						MentalMirror part = gameObject2.GetPart<MentalMirror>();
						if (part != null && part.CheckActive())
						{
							part.Activate();
							part.ReflectMessage(gameObject2);
							gameObject = this.ParentObject;
							break;
						}
						gameObject = gameObject2;
						break;
					}
				}
				if (gameObject == null)
				{
					return false;
				}
				if (!gameObject.RandomTeleport(true, this, null, null, E, 0, 0, true, null, false, true, false, false))
				{
					return false;
				}
				if (this.ParentObject.IsPlayer())
				{
					base.DidX("teleport", gameObject.the + gameObject.ShortDisplayNameWithoutTitles + " away", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
				base.UseEnergy(1000, "Mental Mutation TeleportOther");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldownTurns(base.Level), null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C6A7 RID: 50855 RVA: 0x00486830 File Offset: 0x00484A30
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C6A8 RID: 50856 RVA: 0x0048683C File Offset: 0x00484A3C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Teleport Other", "CommandTeleportOther", "Mental Mutations", null, "\u001b", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C6A9 RID: 50857 RVA: 0x00486885 File Offset: 0x00484A85
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
