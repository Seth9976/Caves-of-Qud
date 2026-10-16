using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200135F RID: 4959
	[Serializable]
	public class Persuasion_RebukeRobot : BaseSkill
	{
		// Token: 0x0600C117 RID: 49431 RVA: 0x0046A508 File Offset: 0x00468708
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionLimitEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C118 RID: 49432 RVA: 0x0046A526 File Offset: 0x00468726
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C119 RID: 49433 RVA: 0x0046A54C File Offset: 0x0046874C
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Rebuke" && E.Actor == this.ParentObject && this.ActivatedAbilityID != Guid.Empty)
			{
				E.Limit++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C11A RID: 49434 RVA: 0x0046A5A0 File Offset: 0x004687A0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C11B RID: 49435 RVA: 0x0046A5A3 File Offset: 0x004687A3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanCompanionRestorePartyLeader");
			Registrar.Register("CommandRebukeRobot");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C11C RID: 49436 RVA: 0x0046A5C4 File Offset: 0x004687C4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandRebukeRobot")
			{
				if (!this.AttemptRebuke())
				{
					return false;
				}
			}
			else if (E.ID == "CanCompanionRestorePartyLeader" && this.ParentObject.SupportsFollower(E.GetGameObjectParameter("Companion"), 8))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C11D RID: 49437 RVA: 0x0046A624 File Offset: 0x00468824
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Rebuke Robot", "CommandRebukeRobot", "Skills", "You admonish a robot into following your commands. Level + Ego-based difficulty check.", "\u0003", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C11E RID: 49438 RVA: 0x0046A66A File Offset: 0x0046886A
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			Persuasion_RebukeRobot.SyncTarget(GO, null, false);
			return true;
		}

		// Token: 0x0600C11F RID: 49439 RVA: 0x0046A684 File Offset: 0x00468884
		public bool AttemptRebuke()
		{
			if (this.ParentObject.IsMissingTongue())
			{
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You cannot rebuke without a tongue.", null, true);
				}
				return false;
			}
			if (!this.ParentObject.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			Cell cell = base.PickDirection("Rebuke what robot?", null);
			if (cell == null)
			{
				return false;
			}
			bool flag = false;
			foreach (GameObject gameObject in cell.GetObjectsWithPart("Brain"))
			{
				if (gameObject != this.ParentObject && gameObject.Statistics.ContainsKey("Level") && gameObject.HasPart<Robot>())
				{
					GameObject gameObject2 = gameObject;
					GameObject parentObject = this.ParentObject;
					if (!gameObject2.CheckInfluence(base.Name, parentObject, false))
					{
						return false;
					}
					flag = true;
					int num = gameObject.Stat("Level", 0) * 4 / 5;
					if (gameObject.HasEffect<Proselytized>())
					{
						num++;
					}
					if (gameObject.HasEffect<Rebuked>())
					{
						num++;
					}
					Beguiled beguiled;
					if (gameObject.TryGetEffect<Beguiled>(out beguiled))
					{
						num += beguiled.LevelApplied;
					}
					int num2 = GetRebukeLevelEvent.GetFor(this.ParentObject, gameObject);
					num2 = this.ParentObject.StatMod("Ego", 0) + num2 * 4 / 5;
					if (Options.SifrahRecruitment)
					{
						new RebukingSifrah(gameObject, num2, num).Play(gameObject);
					}
					else
					{
						base.PerformMentalAttack(new Mental.Attack(this.Rebuke), this.ParentObject, gameObject, null, "Rebuke Robot", null, 2, int.MinValue, int.MinValue, num2, num);
					}
					this.ParentObject.UseEnergy(1000, "Skill Rebuke Robot", null, null, false);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 100, null, null, false);
				}
			}
			if (!flag && this.ParentObject.IsPlayer())
			{
				Popup.Show("There is nothing there to rebuke.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x0600C120 RID: 49440 RVA: 0x0046A898 File Offset: 0x00468A98
		public bool Rebuke(MentalAttackEvent E)
		{
			int penetrations = E.Penetrations;
			GameObject defender = E.Defender;
			if (penetrations <= 0)
			{
				IComponent<GameObject>.AddPlayerMessage("Your argument does not compute.", null, true);
				return false;
			}
			if (penetrations <= 2)
			{
				IComponent<GameObject>.XDidY(defender, "wander", "away disinterestedly", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				this.Neutralize(defender);
			}
			else
			{
				this.FinalizeRebuke(E.Attacker, defender);
			}
			return true;
		}

		// Token: 0x0600C121 RID: 49441 RVA: 0x0046A900 File Offset: 0x00468B00
		public bool FinalizeRebuke(GameObject Actor, GameObject Robot)
		{
			this.Neutralize(Robot);
			return Robot.ApplyEffect(new Rebuked(Actor), null);
		}

		// Token: 0x0600C122 RID: 49442 RVA: 0x0046A916 File Offset: 0x00468B16
		public static bool Rebuke(GameObject Actor, GameObject Robot)
		{
			Persuasion_RebukeRobot part = Actor.GetPart<Persuasion_RebukeRobot>();
			return part != null && part.FinalizeRebuke(Actor, Robot);
		}

		// Token: 0x0600C123 RID: 49443 RVA: 0x0046A92C File Offset: 0x00468B2C
		private bool OurEffect(Effect FX)
		{
			Rebuked rebuked = FX as Rebuked;
			return rebuked != null && rebuked.Rebuker == this.ParentObject;
		}

		// Token: 0x0600C124 RID: 49444 RVA: 0x0046A954 File Offset: 0x00468B54
		public static void SyncTarget(GameObject Rebuker, GameObject Target = null, bool Independent = false)
		{
			if (Rebuker.Brain == null)
			{
				return;
			}
			int num = GetCompanionLimitEvent.GetFor(Rebuker, "Rebuke", 0);
			if (Target == null)
			{
				num++;
			}
			PartyCollection partyMembers = Rebuker.Brain.PartyMembers;
			int[] array = (from x in partyMembers
				where x.Value.Flags.HasBit(8)
				orderby Brain.PartyMemberOrder(x) descending
				select x.Key).ToArray<int>();
			int num2 = 0;
			for (int i = array.Length; i >= num; i--)
			{
				partyMembers.Remove(array[num2]);
				num2++;
			}
			if (Target != null)
			{
				partyMembers[Target] = 8;
				if (Independent)
				{
					PartyCollection partyCollection = partyMembers;
					partyCollection[Target] |= 8388608;
				}
			}
		}

		// Token: 0x0600C125 RID: 49445 RVA: 0x0046AA4C File Offset: 0x00468C4C
		public static void Neutralize(GameObject Actor, GameObject Object)
		{
			Brain brain = Object.Brain;
			if (brain != null)
			{
				brain.StopFighting(false);
				brain.Goals.Clear();
				brain.Allegiance.Hostile = false;
				brain.Target = null;
				brain.Wanders = true;
				brain.AddOpinion<OpinionRebuke>(Actor, 1f);
			}
		}

		// Token: 0x0600C126 RID: 49446 RVA: 0x0046AA9B File Offset: 0x00468C9B
		public void Neutralize(GameObject Object)
		{
			Persuasion_RebukeRobot.Neutralize(this.ParentObject, Object);
		}

		// Token: 0x04004D6D RID: 19821
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
