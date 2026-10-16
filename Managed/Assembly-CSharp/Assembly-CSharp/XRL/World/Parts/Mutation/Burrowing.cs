using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001412 RID: 5138
	[Serializable]
	public class Burrowing : BaseMutation
	{
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600C78F RID: 51087 RVA: 0x0048AD33 File Offset: 0x00488F33
		public Brain pBrain
		{
			get
			{
				if (this._pBrain == null)
				{
					this._pBrain = this.ParentObject.Brain;
				}
				return this._pBrain;
			}
		}

		// Token: 0x0600C791 RID: 51089 RVA: 0x0048AD63 File Offset: 0x00488F63
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C792 RID: 51090 RVA: 0x0048AD80 File Offset: 0x00488F80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == AIGetDefensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C793 RID: 51091 RVA: 0x0048ADB6 File Offset: 0x00488FB6
		public override bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			if (!E.Actor.HasEffect<Burrowed>() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandBurrow", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C794 RID: 51092 RVA: 0x0048ADEC File Offset: 0x00488FEC
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.bBurrowWhenBored && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && (this.ParentObject.GetIntProperty("Librarian", 0) <= 0 || this.ParentObject.Target != null))
			{
				CommandEvent.Send(E.Actor, "CommandBurrow", null, null, 0, false, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C795 RID: 51093 RVA: 0x0048AE54 File Offset: 0x00489054
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			if (this.ParentObject.HasEffect<Burrowed>())
			{
				base.DisableMyActivatedAbility(this.ActivatedAbilityID, null);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
			}
			else
			{
				base.EnableMyActivatedAbility(this.ActivatedAbilityID, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C796 RID: 51094 RVA: 0x0048AEB0 File Offset: 0x004890B0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandBurrow")
			{
				if (this.ParentObject.HasEffect<Burrowed>())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You are already burrowed.", true, true, true);
					}
					return false;
				}
				if (this.ParentObject.OnWorldMap())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You cannot burrow on the world map.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CanChangeBodyPosition("Burrowing", true, false, false, false))
				{
					return false;
				}
				if (!this.ParentObject.CanChangeMovementMode("Burrowing", true, false, false, false))
				{
					return false;
				}
				this.ParentObject.BodyPositionChanged(null, false);
				this.ParentObject.MovementModeChanged(null, false);
				this.ParentObject.ApplyEffect(new Burrowed(this.GetMSPenalty(base.Level)), null);
				this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				if (this.ParentObject.HasEffect<Burrowed>())
				{
					base.DisableMyActivatedAbility(this.ActivatedAbilityID, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C797 RID: 51095 RVA: 0x0048AFEB File Offset: 0x004891EB
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C798 RID: 51096 RVA: 0x0048B00E File Offset: 0x0048920E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIBurrow");
			Registrar.Register("BeginAttack");
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C799 RID: 51097 RVA: 0x0048B039 File Offset: 0x00489239
		public override string GetDescription()
		{
			return "You can travel underground by burrowing.";
		}

		// Token: 0x0600C79A RID: 51098 RVA: 0x0048B040 File Offset: 0x00489240
		public override string GetLevelText(int Level)
		{
			return "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds\n";
		}

		// Token: 0x0600C79B RID: 51099 RVA: 0x0048B06B File Offset: 0x0048926B
		public int GetCooldown(int Level)
		{
			return Math.Max(15 - Level, 5);
		}

		// Token: 0x0600C79C RID: 51100 RVA: 0x0048B077 File Offset: 0x00489277
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.bBurrowWhenBored = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C79D RID: 51101 RVA: 0x0048B099 File Offset: 0x00489299
		public int GetMSPenalty(int Level)
		{
			return 100 - 10 * Level;
		}

		// Token: 0x0600C79E RID: 51102 RVA: 0x0048B0A2 File Offset: 0x004892A2
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C79F RID: 51103 RVA: 0x0048B0AC File Offset: 0x004892AC
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Burrow", "CommandBurrow", "Physical Mutations", null, "\u0019", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C7A0 RID: 51104 RVA: 0x0048B0F5 File Offset: 0x004892F5
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E5E RID: 20062
		public GameObject Target;

		// Token: 0x04004E5F RID: 20063
		public bool bBurrowWhenBored = true;

		// Token: 0x04004E60 RID: 20064
		public Brain _pBrain;
	}
}
