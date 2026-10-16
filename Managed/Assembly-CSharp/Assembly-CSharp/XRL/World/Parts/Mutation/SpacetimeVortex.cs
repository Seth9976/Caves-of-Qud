using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F9 RID: 5113
	[Serializable]
	public class SpacetimeVortex : BaseMutation
	{
		// Token: 0x0600C639 RID: 50745 RVA: 0x00482C18 File Offset: 0x00480E18
		public SpacetimeVortex()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C63A RID: 50746 RVA: 0x00482C2B File Offset: 0x00480E2B
		public override string GetDescription()
		{
			return "You sunder spacetime, sending things nearby careening through a tear in the cosmic fabric.";
		}

		// Token: 0x0600C63B RID: 50747 RVA: 0x00482C34 File Offset: 0x00480E34
		public override string GetLevelText(int Level)
		{
			string text = "Summons a vortex that swallows everything in its path.\n";
			if (Level > 10)
			{
				text = text + "Bonus duration: {{rules|" + (Level - 10).ToString() + "}} rounds\n";
			}
			text = text + "Cooldown: {{rules|" + this.GetCooldown(Level).ToString() + "}} rounds\n";
			text += "You may enter the vortex to teleport to a random location in Qud.\n";
			return text + "+200 reputation with {{w|highly entropic beings}}";
		}

		// Token: 0x0600C63C RID: 50748 RVA: 0x00482CA4 File Offset: 0x00480EA4
		public int GetCooldown(int Level)
		{
			int num = 550 - 50 * Level;
			if (num < 5)
			{
				num = 5;
			}
			return num;
		}

		// Token: 0x0600C63D RID: 50749 RVA: 0x00482CC4 File Offset: 0x00480EC4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			if (Level > 10)
			{
				stats.Set("BonusDuration", "\n\nBonus duration: " + (Level - 10).ToString() + " rounds", !flag, 0);
			}
			else
			{
				stats.Set("BonusDuration", "", !flag, 0);
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C63E RID: 50750 RVA: 0x00482D44 File Offset: 0x00480F44
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C63F RID: 50751 RVA: 0x00482D62 File Offset: 0x00480F62
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 5 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSpaceTimeVortex", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C640 RID: 50752 RVA: 0x00482D94 File Offset: 0x00480F94
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("time", this.BaseElementWeight);
				E.Add("chance", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C641 RID: 50753 RVA: 0x00482DCD File Offset: 0x00480FCD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSpaceTimeVortex");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C642 RID: 50754 RVA: 0x00482DE4 File Offset: 0x00480FE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSpaceTimeVortex")
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				if (currentZone == null)
				{
					return false;
				}
				if (currentZone.IsWorldMap())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You may not use this mutation on the world map.", true, true, true);
					}
					return false;
				}
				Cell cell = base.PickDestinationCell(5, AllowVis.OnlyVisible, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Space-Time Vortex", false, null);
				if (cell == null)
				{
					return false;
				}
				if (cell.PathDistanceTo(this.ParentObject.CurrentCell) > 5)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("That target is out of range! (5 squares)", true, true, true);
					}
					return false;
				}
				Event @event = Event.New("InitiateRealityDistortionTransit", "Object", this.ParentObject, "Mutation", this, "Cell", cell);
				if (!this.ParentObject.FireEvent(@event, E) || !cell.FireEvent(@event, E))
				{
					return false;
				}
				base.UseEnergy(1000, "Mental Mutation SpaceTimeVortex");
				int num = Math.Max(550 - 50 * base.Level, 5);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num, null, null, false);
				this.Vortex(cell);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C643 RID: 50755 RVA: 0x00482F08 File Offset: 0x00481108
		public void Vortex(Cell C)
		{
			if (C != null)
			{
				List<Cell> adjacentCells = C.GetAdjacentCells(true);
				if (this.ParentObject.IsPlayer())
				{
					adjacentCells.Add(C);
				}
				Cell randomElement = adjacentCells.GetRandomElement<Cell>(null);
				GameObject gameObject = GameObject.Create("Space-Time Vortex");
				Temporary part = gameObject.GetPart<Temporary>();
				part.Duration = Stat.Random(15, 18);
				if (base.Level > 10)
				{
					part.Duration += base.Level - 10;
				}
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{G|You sunder spacetime.}}", null, true);
				}
				IComponent<GameObject>.XDidY(gameObject, "appear", null, "!", null, null, null, null, false, true, null, null, false, true, false, false, false, null);
				randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x0600C644 RID: 50756 RVA: 0x00482FCD File Offset: 0x004811CD
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C645 RID: 50757 RVA: 0x00482FD8 File Offset: 0x004811D8
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spacetime Vortex", "CommandSpaceTimeVortex", "Mental Mutations", null, "\u0015", null, false, false, false, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C646 RID: 50758 RVA: 0x00483021 File Offset: 0x00481221
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
