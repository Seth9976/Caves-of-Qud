using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013EE RID: 5102
	[Serializable]
	public class Kindle : BaseMutation
	{
		// Token: 0x0600C56D RID: 50541 RVA: 0x0047DE35 File Offset: 0x0047C035
		public Kindle()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C56E RID: 50542 RVA: 0x0047DE48 File Offset: 0x0047C048
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C56F RID: 50543 RVA: 0x0047DE4B File Offset: 0x0047C04B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C570 RID: 50544 RVA: 0x0047DE61 File Offset: 0x0047C061
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 12 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandKindle", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C571 RID: 50545 RVA: 0x0047DE94 File Offset: 0x0047C094
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C572 RID: 50546 RVA: 0x0047DE97 File Offset: 0x0047C097
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandKindle");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C573 RID: 50547 RVA: 0x0047DEAC File Offset: 0x0047C0AC
		public override string GetDescription()
		{
			return "" + "You ignite a small fire with your mind.\n\n" + "Range: 12\n" + "Cooldown: 50";
		}

		// Token: 0x0600C574 RID: 50548 RVA: 0x0047DED1 File Offset: 0x0047C0D1
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C575 RID: 50549 RVA: 0x0047DED8 File Offset: 0x0047C0D8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandKindle")
			{
				Cell cell = base.PickDestinationCell(12, AllowVis.Any, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Kindle", false, null);
				if (cell == null)
				{
					return false;
				}
				if (this.ParentObject.DistanceTo(cell) > 12)
				{
					return this.ParentObject.ShowFailure("That is out of range (12 squares)");
				}
				GameObject gameObject = GameObject.Create("Kindleflame");
				gameObject.RequirePart<TorchProperties>(false).LastThrower = this.ParentObject;
				gameObject.RequirePart<Temporary>(false).Duration = Stat.Random(50, 75);
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				base.UseEnergy(1000, "Mental Mutation Kindle");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				IComponent<GameObject>.XDidY(gameObject, "appear", null, null, null, null, null, null, false, true, null, null, false, true, false, false, false, null);
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_pyrokinesis_active", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C576 RID: 50550 RVA: 0x0047E002 File Offset: 0x0047C202
		public int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x0600C577 RID: 50551 RVA: 0x0047E006 File Offset: 0x0047C206
		public int GetRange(int Level)
		{
			return 12;
		}

		// Token: 0x0600C578 RID: 50552 RVA: 0x0047E00A File Offset: 0x0047C20A
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", this.GetRange(Level), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C579 RID: 50553 RVA: 0x0047E03B File Offset: 0x0047C23B
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C57A RID: 50554 RVA: 0x0047E044 File Offset: 0x0047C244
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Kindle", "CommandKindle", "Mental Mutations", null, "\u00a8", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C57B RID: 50555 RVA: 0x0047E08D File Offset: 0x0047C28D
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
