using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001239 RID: 4665
	[Serializable]
	public class TrashRifling : IPart
	{
		// Token: 0x0600B985 RID: 47493 RVA: 0x0043C14C File Offset: 0x0043A34C
		public override void Initialize()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Rifle through Trash", TrashRifling.COMMAND_NAME, "Skills", null, "%", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.Initialize();
		}

		// Token: 0x0600B986 RID: 47494 RVA: 0x0043C193 File Offset: 0x0043A393
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.Remove();
		}

		// Token: 0x0600B987 RID: 47495 RVA: 0x0043C1A9 File Offset: 0x0043A3A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<NeedPartSupportEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600B988 RID: 47496 RVA: 0x0043C1E0 File Offset: 0x0043A3E0
		public void CollectStats(Templates.StatCollector stats)
		{
			bool flag = this.ParentObject.HasPart<Customs_TrashDivining>();
			bool flag2 = this.ParentObject.HasPart<Tinkering_Scavenger>();
			if (flag2 && flag)
			{
				stats.Set("RiflingSkills", "Scavenger,TrashDivining", false, 0);
			}
			else if (flag2)
			{
				stats.Set("RiflingSkills", "Scavenger", false, 0);
			}
			else if (flag)
			{
				stats.Set("RiflingSkills", "TrashDivining", false, 0);
			}
			stats.Set("Chance", 5, false, 0);
		}

		// Token: 0x0600B989 RID: 47497 RVA: 0x0043C258 File Offset: 0x0043A458
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B98A RID: 47498 RVA: 0x0043C27C File Offset: 0x0043A47C
		public override bool HandleEvent(AIBoredEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && this.ParentObject.Brain != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				List<GameObject> list = Event.NewGameObjectList();
				foreach (GameObject gameObject in currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 8, "Garbage", this.ParentObject, false, false, null))
				{
					if (this.ParentObject.HasLOSTo(gameObject, true, false, false, true, null))
					{
						list.Add(gameObject);
					}
				}
				if (list.Count > 1)
				{
					list.Sort((GameObject a, GameObject b) => this.ParentObject.DistanceTo(a).CompareTo(this.ParentObject.DistanceTo(b)));
				}
				GameObject randomElement = list.GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					Cell currentCell2 = randomElement.CurrentCell;
					if (currentCell2 != null)
					{
						this.ParentObject.Brain.PushGoal(new MoveTo(currentCell2, false, false, 0, false, false, false, 3, -1, 95));
					}
				}
				else
				{
					this.ParentObject.Brain.Think("I can't find any trash.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B98B RID: 47499 RVA: 0x0043C3C4 File Offset: 0x0043A5C4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == TrashRifling.COMMAND_NAME)
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				AutoAct.ResetZoneAutoexploreAction(Garbage.RIFLE_INTERACTION);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B98C RID: 47500 RVA: 0x0043C40C File Offset: 0x0043A60C
		public override bool HandleEvent(NeedPartSupportEvent E)
		{
			if (E.Type == "TrashRifling" && !PartSupportEvent.Check(E, this))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B98D RID: 47501 RVA: 0x0043C43C File Offset: 0x0043A63C
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command == null && E.Item.HasPart<Garbage>() && base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				E.Command = Garbage.RIFLE_INTERACTION;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B98E RID: 47502 RVA: 0x0043C47C File Offset: 0x0043A67C
		public bool IsActive()
		{
			return base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null) && base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null);
		}

		// Token: 0x0600B98F RID: 47503 RVA: 0x0043C49C File Offset: 0x0043A69C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004A98 RID: 19096
		public const string SUPPORT_TYPE = "TrashRifling";

		// Token: 0x04004A99 RID: 19097
		public static readonly string COMMAND_NAME = "CommandToggleTrashRifling";

		// Token: 0x04004A9A RID: 19098
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
