using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E1 RID: 5089
	[Serializable]
	public class Beguiling : BaseMutation
	{
		// Token: 0x0600C493 RID: 50323 RVA: 0x00478842 File Offset: 0x00476A42
		public Beguiling()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C494 RID: 50324 RVA: 0x00478855 File Offset: 0x00476A55
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionLimitEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C495 RID: 50325 RVA: 0x00478874 File Offset: 0x00476A74
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Beguiling" && E.Actor == this.ParentObject && this.ActivatedAbilityID != Guid.Empty)
			{
				E.Limit++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C496 RID: 50326 RVA: 0x004788C8 File Offset: 0x00476AC8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C497 RID: 50327 RVA: 0x004788EB File Offset: 0x00476AEB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C498 RID: 50328 RVA: 0x004788EE File Offset: 0x00476AEE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanCompanionRestoreParty");
			Registrar.Register("CommandBeguileCreature");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C499 RID: 50329 RVA: 0x00478910 File Offset: 0x00476B10
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandBeguileCreature")
			{
				if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
				}
				else
				{
					Beguiling.Cast(this.ParentObject, this, E, 1);
				}
			}
			else if (E.ID == "CanCompanionRestorePartyLeader")
			{
				if (this.ParentObject.SupportsFollower(E.GetGameObjectParameter("Companion"), 2))
				{
					return false;
				}
			}
			else if (E.ID == "GetMaxBeguiled")
			{
				E.ModParameter("Amount", 1);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C49A RID: 50330 RVA: 0x004789B8 File Offset: 0x00476BB8
		private bool OurEffect(Effect FX)
		{
			Beguiled beguiled = FX as Beguiled;
			return beguiled != null && beguiled.Beguiler == this.ParentObject;
		}

		// Token: 0x0600C49B RID: 50331 RVA: 0x004789DF File Offset: 0x00476BDF
		public override string GetDescription()
		{
			return "You beguile a nearby creature into serving you loyally.";
		}

		// Token: 0x0600C49C RID: 50332 RVA: 0x004789E8 File Offset: 0x00476BE8
		public override string GetLevelText(int Level)
		{
			return "Mental attack versus a creature with a mind\n" + "Success roll: {{rules|mutation rank}} or Ego mod (whichever is higher) + character level + 1d8 VS. Defender MA + character level\n" + "Range: 1\n" + "Beguiled creature: +{{rules|" + (Level * 5).ToString() + "}} bonus hit points\n" + "Cooldown: 50 rounds";
		}

		// Token: 0x0600C49D RID: 50333 RVA: 0x00478A34 File Offset: 0x00476C34
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			int num = Math.Max(this.ParentObject.StatMod("Ego", 0), Level) + this.ParentObject.GetStat("Level").Value;
			if (num == 0)
			{
				stats.Set("Attack", "1d8", !stats.mode.Contains("ability"), 0);
			}
			else if (num > 0)
			{
				stats.Set("Attack", "1d8+" + num.ToString(), !stats.mode.Contains("ability"), 0);
			}
			else
			{
				stats.Set("Attack", "1d8" + num.ToString(), !stats.mode.Contains("ability"), 0);
			}
			stats.Set("BonusHP", Level * 5, !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C49E RID: 50334 RVA: 0x00478B3C File Offset: 0x00476D3C
		public static bool Cast(GameObject who, Beguiling mutation = null, Event ev = null, int genericLevel = 1)
		{
			bool flag = false;
			if (mutation == null)
			{
				mutation = new Beguiling();
				mutation.Level = genericLevel;
				mutation.ParentObject = who;
				flag = true;
			}
			Cell cell = mutation.PickDirection("[Select a direction to beguile a creature]", null);
			if (cell != null)
			{
				List<GameObject> objects = cell.GetObjects((GameObject x) => x.IsCombatObject() && x.IsValid() && x.HasStat("Hitpoints") && x.HasStat("Level"));
				if (objects.IsNullOrEmpty<GameObject>())
				{
					return who.ShowFailure("There are no valid targets in that square.");
				}
				GameObject gameObject = ((objects.Count > 2) ? Popup.PickGameObject("Who would you like to beguile?", objects, true, false, false, null, 0, false) : objects[0]);
				if (gameObject == null)
				{
					return false;
				}
				if (mutation.RealityDistortionBased)
				{
					Event @event = Event.New("InitiateRealityDistortionTransit", "Object", who, "Mutation", mutation, "Cell", cell);
					if (!who.FireEvent(@event, ev) || !cell.FireEvent(@event, ev))
					{
						RealityStabilized.ShowGenericInterdictMessage(who, null);
						return false;
					}
				}
				if (gameObject != null && gameObject != who && gameObject.IsValid() && gameObject.HasStat("Level") && gameObject.HasStat("Hitpoints"))
				{
					if (gameObject.HasCopyRelationship(who) || gameObject.IsOriginalPlayerBody())
					{
						if (who.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You can't beguile " + who.itself + "!", 'R', true);
						}
						return false;
					}
					if (!gameObject.FireEvent("CanApplyBeguile") || !CanApplyEffectEvent.Check(gameObject, "Beguile", 0))
					{
						if (who.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(gameObject.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " utterly impervious to your charms.", null, true);
						}
						return false;
					}
					if (!gameObject.CheckInfluence(mutation.Name, who, false))
					{
						return false;
					}
					if (gameObject.HasEffect(typeof(Beguiled), new Predicate<Effect>(mutation.OurEffect)))
					{
						if (who.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You have already beguiled " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", 'R', true);
						}
						return false;
					}
					if (gameObject.IsLedBy(who) && who.IsPlayer() && Popup.ShowYesNo(gameObject.Does("are", 2147483647, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " already your follower. Do you want to beguile " + gameObject.them + " anyway?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
					if (mutation != null)
					{
						mutation.UseEnergy(1000, "Mental Mutation Beguiling");
					}
					if (mutation != null)
					{
						mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, 50, null, null, false);
					}
					if (Options.SifrahRecruitment)
					{
						int num = who.Stat("Level", 0) + who.StatMod("Ego", 0);
						int num2 = gameObject.Stat("Level", 0);
						if (gameObject.HasEffect<Proselytized>())
						{
							num2++;
						}
						if (gameObject.HasEffect<Rebuked>())
						{
							num2++;
						}
						if (gameObject.HasEffect<Beguiled>())
						{
							Beguiled effect = gameObject.GetEffect<Beguiled>();
							num2 += effect.LevelApplied;
						}
						BeguilingSifrah beguilingSifrah = new BeguilingSifrah(gameObject, mutation.Level, flag, num, num2);
						beguilingSifrah.Play(gameObject);
						return beguilingSifrah.Success;
					}
					if (gameObject.HasEffect<Beguiled>() && gameObject.GetEffect<Beguiled>().LevelApplied + "1d8".RollCached() > mutation.Level + "1d8".RollCached())
					{
						if (who.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You fail to outshine the current object of " + gameObject.poss("affection", true) + ".", 'R', true);
						}
						return false;
					}
					int num3 = who.Stat("Level", 0) + Math.Max(who.StatMod("Ego", 0), mutation.Level);
					return Mental.PerformAttack(new Mental.Attack(mutation.Beguile), who, gameObject, null, "Beguiling", "1d8", 1, int.MinValue, int.MinValue, num3, gameObject.Stat("Level", 0));
				}
			}
			return false;
		}

		// Token: 0x0600C49F RID: 50335 RVA: 0x00478F40 File Offset: 0x00477140
		private bool Beguile(MentalAttackEvent E)
		{
			GameObject defender = E.Defender;
			bool flag = this.ActivatedAbilityID == Guid.Empty;
			if (E.Penetrations <= 0 || !defender.FireEvent("CanApplyBeguile") || !CanApplyEffectEvent.Check(defender, "Beguile", 0) || !defender.ApplyEffect(new Beguiled(E.Attacker, base.Level, flag), null))
			{
				IComponent<GameObject>.AddPlayerMessage("Your coquetry infuriates " + defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", 'r', true);
				defender.AddOpinion<OpinionCoquetry>(E.Attacker, 1f);
				return false;
			}
			return true;
		}

		// Token: 0x0600C4A0 RID: 50336 RVA: 0x00478FF4 File Offset: 0x004771F4
		public IEnumerable<GameObject> YieldTargets()
		{
			if (this.ParentObject.Brain == null)
			{
				yield break;
			}
			foreach (KeyValuePair<int, PartyMember> keyValuePair in this.ParentObject.Brain.PartyMembers)
			{
				if (keyValuePair.Value.Flags.HasBit(2))
				{
					GameObject gameObject = keyValuePair.Value.Reference.Object ?? GameObject.FindByID(keyValuePair.Key);
					if (gameObject != null)
					{
						yield return gameObject;
					}
				}
			}
			Dictionary<int, PartyMember>.Enumerator enumerator = default(Dictionary<int, PartyMember>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600C4A1 RID: 50337 RVA: 0x00479004 File Offset: 0x00477204
		public static void SyncTarget(GameObject Beguiler, GameObject Target = null, bool Independent = false)
		{
			if (Beguiler.Brain == null)
			{
				return;
			}
			int num = GetCompanionLimitEvent.GetFor(Beguiler, "Beguiling", 0);
			if (Target == null)
			{
				num++;
			}
			PartyCollection partyMembers = Beguiler.Brain.PartyMembers;
			int[] array = (from x in partyMembers
				where x.Value.Flags.HasBit(2)
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
				partyMembers[Target] = 2;
				if (Independent)
				{
					PartyCollection partyCollection = partyMembers;
					partyCollection[Target] |= 8388608;
				}
			}
		}

		// Token: 0x0600C4A2 RID: 50338 RVA: 0x004790FC File Offset: 0x004772FC
		public override bool ChangeLevel(int NewLevel)
		{
			using (IEnumerator<GameObject> enumerator = this.YieldTargets().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Beguiled beguiled;
					if (enumerator.Current.TryGetEffect<Beguiled>(out beguiled))
					{
						beguiled.SyncToMutation();
					}
				}
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C4A3 RID: 50339 RVA: 0x00479158 File Offset: 0x00477358
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Beguile Creature", "CommandBeguileCreature", "Mental Mutations", "Beguile", "\u0003", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4A4 RID: 50340 RVA: 0x004791AA File Offset: 0x004773AA
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			Beguiling.SyncTarget(GO, null, false);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C4A5 RID: 50341 RVA: 0x004791C9 File Offset: 0x004773C9
		public int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x04004DD8 RID: 19928
		public const string PROPERTY_ID = "BeguilingTargetID";

		// Token: 0x04004DD9 RID: 19929
		public bool RealityDistortionBased;
	}
}
