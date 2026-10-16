using System;
using System.Text;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200145D RID: 5213
	[Serializable]
	public class RepellingForce : BaseMutation
	{
		// Token: 0x0600CC42 RID: 52290 RVA: 0x004A35B8 File Offset: 0x004A17B8
		public RepellingForce()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600CC43 RID: 52291 RVA: 0x004A35CB File Offset: 0x004A17CB
		public override string GetDescription()
		{
			return "You invoke a repelling force in the surrounding area, throwing enemies back.";
		}

		// Token: 0x0600CC44 RID: 52292 RVA: 0x004A35D4 File Offset: 0x004A17D4
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Compound("Area: 7x7", '\n');
			if (Level == base.Level)
			{
				stringBuilder.Compound("Creatures are pushed away from center of blast.", '\n');
			}
			else
			{
				stringBuilder.Compound("{{rules|Increased push force}}", '\n');
			}
			stringBuilder.Compound("Cooldown: ", '\n').Append(30).Append(" rounds");
			return stringBuilder.ToString();
		}

		// Token: 0x0600CC45 RID: 52293 RVA: 0x004A3644 File Offset: 0x004A1844
		public override bool Mutate(GameObject Object, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(base.GetDisplayName(true), "CommandRepellingForce", "Mental Mutations", null, "#", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(Object, Level);
		}

		// Token: 0x0600CC46 RID: 52294 RVA: 0x004A368F File Offset: 0x004A188F
		public override bool Unmutate(GameObject Object)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(Object);
		}

		// Token: 0x0600CC47 RID: 52295 RVA: 0x004A36A6 File Offset: 0x004A18A6
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "7x7", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 30, 0);
		}

		// Token: 0x0600CC48 RID: 52296 RVA: 0x004A36D0 File Offset: 0x004A18D0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CC49 RID: 52297 RVA: 0x004A36EE File Offset: 0x004A18EE
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 2 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandRepellingForce", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC4A RID: 52298 RVA: 0x004A3720 File Offset: 0x004A1920
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC4B RID: 52299 RVA: 0x004A3743 File Offset: 0x004A1943
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CC4C RID: 52300 RVA: 0x004A3746 File Offset: 0x004A1946
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandRepellingForce");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CC4D RID: 52301 RVA: 0x004A375C File Offset: 0x004A195C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandRepellingForce")
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.ShowFailure("You cannot use " + base.GetDisplayName(false) + " on the world map.");
				}
				base.DidX("flip", "polarity", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				StunningForce.Concussion(this.ParentObject.CurrentCell, this.ParentObject, base.Level, 3, this.ParentObject.GetPhase(), null, false, false);
				base.UseEnergy(1000);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 30, null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004F3A RID: 20282
		public const string ABL_CMD = "CommandRepellingForce";

		// Token: 0x04004F3B RID: 20283
		public const int ABL_CLD = 30;
	}
}
