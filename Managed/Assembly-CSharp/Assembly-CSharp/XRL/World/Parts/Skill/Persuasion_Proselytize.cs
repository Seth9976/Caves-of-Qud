using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200135E RID: 4958
	[Serializable]
	public class Persuasion_Proselytize : BaseSkill
	{
		// Token: 0x0600C107 RID: 49415 RVA: 0x00469D5E File Offset: 0x00467F5E
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Persuasion_Proselytize.COOLDOWN, 0);
		}

		// Token: 0x0600C108 RID: 49416 RVA: 0x00469D79 File Offset: 0x00467F79
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionLimitEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C109 RID: 49417 RVA: 0x00469D9F File Offset: 0x00467F9F
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C10A RID: 49418 RVA: 0x00469DC2 File Offset: 0x00467FC2
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C10B RID: 49419 RVA: 0x00469DE8 File Offset: 0x00467FE8
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Proselytize" && E.Actor == this.ParentObject && this.ActivatedAbilityID != Guid.Empty)
			{
				E.Limit++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C10C RID: 49420 RVA: 0x00469E3C File Offset: 0x0046803C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C10D RID: 49421 RVA: 0x00469E3F File Offset: 0x0046803F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanCompanionRestorePartyLeader");
			Registrar.Register("CommandProselytize");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C10E RID: 49422 RVA: 0x00469E60 File Offset: 0x00468060
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandProselytize")
			{
				if (!this.AttemptProselytization())
				{
					return false;
				}
			}
			else if (E.ID == "CanCompanionRestorePartyLeader" && this.ParentObject.SupportsFollower(E.GetGameObjectParameter("Companion"), 1))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C10F RID: 49423 RVA: 0x00469EC0 File Offset: 0x004680C0
		private bool OurEffect(Effect FX)
		{
			Proselytized proselytized = FX as Proselytized;
			return proselytized != null && proselytized.Proselytizer == this.ParentObject;
		}

		// Token: 0x0600C110 RID: 49424 RVA: 0x00469EE8 File Offset: 0x004680E8
		public static void SyncTarget(GameObject Proselytizer, GameObject Target = null, bool Independent = false)
		{
			if (Proselytizer.Brain == null)
			{
				return;
			}
			int num = GetCompanionLimitEvent.GetFor(Proselytizer, "Proselytize", 0);
			if (Target == null)
			{
				num++;
			}
			PartyCollection partyMembers = Proselytizer.Brain.PartyMembers;
			int[] array = (from x in partyMembers
				where x.Value.Flags.HasBit(1)
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
				partyMembers[Target] = 1;
				if (Independent)
				{
					PartyCollection partyCollection = partyMembers;
					partyCollection[Target] |= 8388608;
				}
			}
		}

		// Token: 0x0600C111 RID: 49425 RVA: 0x00469FE0 File Offset: 0x004681E0
		public override bool AddSkill(GameObject obj)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Proselytize", "CommandProselytize", "Skills", null, "\u0003", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			if (obj.IsPlayer())
			{
				SocialSifrah.AwardInsight();
			}
			return base.AddSkill(obj);
		}

		// Token: 0x0600C112 RID: 49426 RVA: 0x0046A035 File Offset: 0x00468235
		public override bool RemoveSkill(GameObject obj)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			Persuasion_Proselytize.SyncTarget(obj, null, false);
			return base.RemoveSkill(obj);
		}

		// Token: 0x0600C113 RID: 49427 RVA: 0x0046A054 File Offset: 0x00468254
		public bool AttemptProselytization()
		{
			bool flag = this.ParentObject.IsMissingTongue();
			if (flag && !this.ParentObject.HasPart<Telepathy>())
			{
				return this.ParentObject.Fail("You cannot proselytize without a tongue.");
			}
			if (!this.ParentObject.CheckFrozen(true, false, false, null))
			{
				return false;
			}
			Cell cell = base.PickDirection("Proselytize", null);
			if (cell == null)
			{
				return false;
			}
			List<GameObject> objects = cell.GetObjects((GameObject x) => x.IsCombatObject() && x.IsValid() && x.HasStat("MA") && x.HasStat("Level"));
			if (objects.IsNullOrEmpty<GameObject>())
			{
				return this.ParentObject.ShowFailure("There are no valid targets in that square.");
			}
			GameObject gameObject = ((objects.Count > 1) ? Popup.PickGameObject("Who would you like to proselytize?", objects, true, false, false, null, 0, false) : objects[0]);
			if (gameObject == null)
			{
				return false;
			}
			if (gameObject == this.ParentObject || gameObject.HasCopyRelationship(this.ParentObject) || gameObject.IsOriginalPlayerBody())
			{
				return this.ParentObject.Fail("You can't proselytize " + this.ParentObject.itself + "!");
			}
			if (flag && !this.ParentObject.CanMakeTelepathicContactWith(gameObject))
			{
				if (this.ParentObject.HasPart<Telepathy>())
				{
					return this.ParentObject.Fail(string.Concat(new string[]
					{
						"Without a tongue, you cannot proselytize ",
						gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						", as you cannot make telepathic contact with ",
						gameObject.them,
						"."
					}));
				}
				return this.ParentObject.Fail("Without a tongue, you cannot proselytize " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
			}
			else
			{
				if (!this.ParentObject.CheckFrozen(true, false, true, gameObject))
				{
					return this.ParentObject.Fail("Frozen solid, you cannot proselytize " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				if (gameObject.HasEffect(typeof(Proselytized), new Predicate<Effect>(this.OurEffect)))
				{
					return this.ParentObject.Fail("You have already proselytized " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				if (!ConversationScript.IsPhysicalConversationPossible(this.ParentObject, gameObject, true, true, true, 0) && !ConversationScript.IsMentalConversationPossible(this.ParentObject, gameObject, true, true, 0))
				{
					return false;
				}
				if (gameObject.PartyLeader == this.ParentObject && this.ParentObject.IsPlayer() && Popup.ShowYesNo(gameObject.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " already your follower. Do you want to proselytize " + gameObject.them + " anyway?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return false;
				}
				GameObject gameObject2 = gameObject;
				GameObject parentObject = this.ParentObject;
				if (!gameObject2.CheckInfluence(base.Name, parentObject, false))
				{
					return false;
				}
				int num = Math.Max(gameObject.Stat("Level", 0) - this.ParentObject.Stat("Level", 0), 0);
				if (gameObject.HasEffect<Proselytized>())
				{
					num++;
				}
				if (gameObject.HasEffect<Rebuked>())
				{
					num++;
				}
				Beguiled effect = gameObject.GetEffect<Beguiled>();
				if (effect != null)
				{
					num += effect.LevelApplied;
				}
				int num2 = this.ParentObject.StatMod("Ego", 0);
				if (Options.SifrahRecruitment)
				{
					new ProselytizationSifrah(gameObject, num2, num).Play(gameObject);
				}
				else
				{
					base.PerformMentalAttack(new Mental.Attack(Persuasion_Proselytize.Proselytize), this.ParentObject, gameObject, null, "Proselytize", "1d8-6", 2, int.MinValue, int.MinValue, num2, num);
				}
				this.ParentObject.UseEnergy(1000, "Skill Proselytize", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Persuasion_Proselytize.COOLDOWN, null, null, false);
				return true;
			}
		}

		// Token: 0x0600C114 RID: 49428 RVA: 0x0046A478 File Offset: 0x00468678
		public static bool Proselytize(MentalAttackEvent E)
		{
			GameObject defender = E.Defender;
			return (E.Penetrations > 0 && defender.ApplyEffect(new Proselytized(E.Attacker), null)) || E.Attacker.Fail(defender.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unconvinced by your pleas.");
		}

		// Token: 0x04004D6B RID: 19819
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D6C RID: 19820
		public static readonly int COOLDOWN = 25;
	}
}
