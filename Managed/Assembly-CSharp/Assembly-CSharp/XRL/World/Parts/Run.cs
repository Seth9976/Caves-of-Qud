using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001231 RID: 4657
	[Serializable]
	public class Run : IPart
	{
		// Token: 0x0600B8F6 RID: 47350 RVA: 0x0043842A File Offset: 0x0043662A
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.Remove();
		}

		// Token: 0x0600B8F7 RID: 47351 RVA: 0x00438440 File Offset: 0x00436640
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<CommandTakeActionEvent>.ID || ID == PooledEvent<NeedPartSupportEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600B8F8 RID: 47352 RVA: 0x00438478 File Offset: 0x00436678
		public void CollectStats(Templates.StatCollector stats)
		{
			string text;
			string text2;
			string text3;
			string text4;
			int num;
			bool flag;
			GetRunningBehaviorEvent.Retrieve(this.ParentObject, out text, out text2, out text3, out text4, out num, out flag, stats);
			if (this.ParentObject.HasPart<Tactics_Hurdle>())
			{
				stats.Set("DV", 0, true, 5);
				stats.postfix += "\nDV penalty and no jumping removed due to Hurdle skill.";
			}
			else
			{
				stats.Set("DV", -5, false, 0);
			}
			if (this.ParentObject.HasPart<Pistol_SlingAndRun>())
			{
				stats.Set("MissileAccuracy", "Pistol", false, 0);
				stats.postfix += "\nPistol accuracy penalty removed due to Sling and Run skill.";
			}
			else
			{
				stats.Set("MissileAccuracy", "", false, 0);
			}
			Running.GetMovespeedMultiplier(this.ParentObject, flag, stats);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 100, 0);
		}

		// Token: 0x0600B8F9 RID: 47353 RVA: 0x0043854D File Offset: 0x0043674D
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8FA RID: 47354 RVA: 0x00438570 File Offset: 0x00436770
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance >= 6 && !this.ActiveAbilityName.IsNullOrEmpty() && !E.Actor.IsFlying && !E.Actor.HasEffect<Running>() && E.Actor.CanChangeMovementMode(this.ActiveEffectMessageName, false, false, false, false) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandToggleRunning", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8FB RID: 47355 RVA: 0x004385EB File Offset: 0x004367EB
		public override bool HandleEvent(NeedPartSupportEvent E)
		{
			if (E.Type == "Run" && !PartSupportEvent.Check(E, this))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8FC RID: 47356 RVA: 0x0043861C File Offset: 0x0043681C
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.ToggleState = this.IsRunning();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8FD RID: 47357 RVA: 0x0043864D File Offset: 0x0043684D
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == "CommandToggleRunning") || this.ToggleRunning()) && base.HandleEvent(E);
		}

		// Token: 0x0600B8FE RID: 47358 RVA: 0x00438672 File Offset: 0x00436872
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B8FF RID: 47359 RVA: 0x00438678 File Offset: 0x00436878
		public void SyncAbility(bool Silent = false)
		{
			string text;
			string text2;
			string text3;
			string text4;
			int num;
			bool flag;
			GetRunningBehaviorEvent.Retrieve(this.ParentObject, out text, out text2, out text3, out text4, out num, out flag, null);
			if (this.ActivatedAbilityID == Guid.Empty || text != this.ActiveAbilityName || text2 != this.ActiveVerb || text3 != this.ActiveEffectDisplayName || text4 != this.ActiveEffectMessageName || num != this.ActiveEffectDuration || flag != this.ActiveSpringingEffective)
			{
				bool flag2 = this.ActiveAbilityName == text;
				base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
				if (!flag2)
				{
					this.ParentObject.RemoveAllEffects<Running>(true);
				}
				this.ActiveAbilityName = text;
				this.ActiveVerb = text2;
				this.ActiveEffectDisplayName = text3;
				this.ActiveEffectMessageName = text4;
				this.ActiveEffectDuration = num;
				this.ActiveSpringingEffective = flag;
				if (!this.ActiveAbilityName.IsNullOrEmpty())
				{
					this.ActivatedAbilityID = base.AddMyActivatedAbility(this.ActiveAbilityName, "CommandToggleRunning", "Maneuvers", null, "\u001a", null, true, false, true, false, false, false, Silent || flag2, false, true, true, false, -1, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x0600B900 RID: 47360 RVA: 0x0043879C File Offset: 0x0043699C
		public static void SyncAbility(GameObject who, bool Silent = false)
		{
			Run part = who.GetPart<Run>();
			if (part == null)
			{
				return;
			}
			part.SyncAbility(Silent);
		}

		// Token: 0x0600B901 RID: 47361 RVA: 0x004387AF File Offset: 0x004369AF
		public bool ToggleRunning()
		{
			if (!this.IsRunning())
			{
				return this.StartRunning();
			}
			return this.StopRunning();
		}

		// Token: 0x0600B902 RID: 47362 RVA: 0x004387C6 File Offset: 0x004369C6
		public bool IsRunning()
		{
			return this.ParentObject.HasEffect<Running>();
		}

		// Token: 0x0600B903 RID: 47363 RVA: 0x004387D4 File Offset: 0x004369D4
		public bool StartRunning()
		{
			if (this.IsRunning())
			{
				return false;
			}
			if (!this.ParentObject.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (this.ActiveAbilityName.IsNullOrEmpty())
			{
				return false;
			}
			if (this.ParentObject.OnWorldMap())
			{
				Popup.ShowFail("You cannot " + this.ActiveVerb + " on the world map.", true, true, true);
				return false;
			}
			if (!this.ParentObject.CanChangeMovementMode(this.ActiveEffectMessageName, true, false, false, false))
			{
				return false;
			}
			this.ParentObject.ApplyEffect(new Running(this.ActiveEffectDuration, this.ActiveEffectDisplayName, this.ActiveEffectMessageName, this.ActiveSpringingEffective), null);
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.ToggleState = true;
			}
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 100, null, "Agility", false);
			return true;
		}

		// Token: 0x0600B904 RID: 47364 RVA: 0x004388AC File Offset: 0x00436AAC
		public bool StopRunning()
		{
			if (!this.IsRunning())
			{
				return false;
			}
			this.ParentObject.RemoveAllEffects<Running>(true);
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.ToggleState = false;
			}
			return true;
		}

		// Token: 0x04004A72 RID: 19058
		public const string SUPPORT_TYPE = "Run";

		// Token: 0x04004A73 RID: 19059
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004A74 RID: 19060
		public string ActiveAbilityName;

		// Token: 0x04004A75 RID: 19061
		public string ActiveVerb;

		// Token: 0x04004A76 RID: 19062
		public string ActiveEffectDisplayName;

		// Token: 0x04004A77 RID: 19063
		public string ActiveEffectMessageName;

		// Token: 0x04004A78 RID: 19064
		public int ActiveEffectDuration;

		// Token: 0x04004A79 RID: 19065
		public bool ActiveSpringingEffective;
	}
}
