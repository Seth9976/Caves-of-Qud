using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001369 RID: 4969
	[Serializable]
	public class Pistol_EmptyTheClips : BaseSkill
	{
		// Token: 0x0600C150 RID: 49488 RVA: 0x0046B6DB File Offset: 0x004698DB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C151 RID: 49489 RVA: 0x0046B709 File Offset: 0x00469909
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C152 RID: 49490 RVA: 0x0046B72C File Offset: 0x0046992C
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", Pistol_EmptyTheClips.DURATION, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Pistol_EmptyTheClips.COOLDOWN, 0);
		}

		// Token: 0x0600C153 RID: 49491 RVA: 0x0046B75C File Offset: 0x0046995C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.Enabled = !this.ParentObject.HasEffect<EmptyTheClips>();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C154 RID: 49492 RVA: 0x0046B798 File Offset: 0x00469998
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Pistol_EmptyTheClips.COMMAND_NAME && E.Actor == this.ParentObject && this.ParentObject.CanMoveExtremities(null, true, false, false))
			{
				this.ParentObject.ApplyEffect(new EmptyTheClips(Pistol_EmptyTheClips.DURATION + 1), null);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Pistol_EmptyTheClips.COOLDOWN, null, "Agility", false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C155 RID: 49493 RVA: 0x0046B810 File Offset: 0x00469A10
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.HasMissileWeapon(null, new Predicate<MissileWeapon>(Pistol_EmptyTheClips.IsPistol)) && E.Actor.CanMoveExtremities(null, false, false, false) && 50.in100())
			{
				E.Add(Pistol_EmptyTheClips.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C156 RID: 49494 RVA: 0x0046B878 File Offset: 0x00469A78
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Empty the Clips", Pistol_EmptyTheClips.COMMAND_NAME, "Skills", string.Concat(new string[]
			{
				"Cooldown ",
				Pistol_EmptyTheClips.COOLDOWN.ToString(),
				". For ",
				Pistol_EmptyTheClips.DURATION.Things("round", null),
				", the action cost of firing pistols is reduced from 1000 to 500."
			}), "®", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C157 RID: 49495 RVA: 0x0046B905 File Offset: 0x00469B05
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C158 RID: 49496 RVA: 0x0046B91C File Offset: 0x00469B1C
		public static bool IsPistol(MissileWeapon MW)
		{
			return ((MW != null) ? MW.Skill : null) == "Pistol";
		}

		// Token: 0x0600C159 RID: 49497 RVA: 0x0046B934 File Offset: 0x00469B34
		public static bool IsPistol(GameObject Object)
		{
			return Pistol_EmptyTheClips.IsPistol(Object.GetPart<MissileWeapon>());
		}

		// Token: 0x04004D72 RID: 19826
		public static readonly string COMMAND_NAME = "CommandPistolEmptyTheClips";

		// Token: 0x04004D73 RID: 19827
		public static readonly int DURATION = 20;

		// Token: 0x04004D74 RID: 19828
		public static readonly int COOLDOWN = 200;

		// Token: 0x04004D75 RID: 19829
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
