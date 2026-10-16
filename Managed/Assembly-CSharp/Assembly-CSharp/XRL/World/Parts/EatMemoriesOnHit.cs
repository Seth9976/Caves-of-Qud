using System;
using Qud.API;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020010DC RID: 4316
	[Serializable]
	public class EatMemoriesOnHit : IPart
	{
		// Token: 0x0600AB5C RID: 43868 RVA: 0x003EB441 File Offset: 0x003E9641
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponAfterAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB5D RID: 43869 RVA: 0x003EB464 File Offset: 0x003E9664
		public static bool CheckEatMemories(GameObject attacker, GameObject defender, GameObject weapon, int chance)
		{
			return defender != null && defender.IsPlayer() && GetSpecialEffectChanceEvent.GetFor(attacker, weapon, "Part EatMemoriesOnHit Activation", chance, defender, null, true, false).in100() && Stat.rollMentalAttackPenetrations(attacker, defender) > 0;
		}

		// Token: 0x0600AB5E RID: 43870 RVA: 0x003EB4A4 File Offset: 0x003E96A4
		public static void EatMemories(GameObject attacker, GameObject defender, GameObject weapon, string memoriesLost)
		{
			bool flag = false;
			GameManager.Instance.Fuzzing = true;
			defender.CurrentZone.GetExploredCells().ForEach(delegate(Cell c)
			{
				c.SetExplored(false);
			});
			defender.ParticlePulse("&K?");
			int i = 0;
			int num = Stat.Roll(memoriesLost, null);
			while (i < num)
			{
				IBaseJournalEntry randomRevealedNote = JournalAPI.GetRandomRevealedNote((IBaseJournalEntry c) => c.Forgettable());
				if (randomRevealedNote != null)
				{
					randomRevealedNote.Forget(false);
					flag = true;
				}
				i++;
			}
			if (flag)
			{
				IComponent<GameObject>.AddPlayerMessage("{{R|You forget something.}}", null, true);
				GenericDeepNotifyEvent.Send(defender, "MemoriesEaten", defender, weapon, 0);
				return;
			}
			IComponent<GameObject>.AddPlayerMessage(attacker.Does("try", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to eat your memories, but " + attacker.does("starve", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + ".", null, true);
		}

		// Token: 0x0600AB5F RID: 43871 RVA: 0x003EB5CC File Offset: 0x003E97CC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (GameObject.Validate(ref gameObjectParameter) && EatMemoriesOnHit.CheckEatMemories(this.ParentObject.equippedOrSelf(), gameObjectParameter, this.ParentObject, this.Chance))
				{
					E.SetParameter("DidSpecialEffect", 1);
					string text = E.GetStringParameter("Properties", "") ?? "";
					if (!text.Contains("AteMemories"))
					{
						E.SetParameter("Properties", (text == "") ? "AteMemories" : (text + ",AteMemories"));
					}
				}
			}
			else if (E.ID == "WeaponAfterAttack")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Weapon");
				if (GameObject.Validate(ref gameObjectParameter2) && GameObject.Validate(ref gameObjectParameter3) && GameObject.Validate(ref gameObjectParameter4) && (E.GetStringParameter("Properties", "") ?? "").Contains("AteMemories"))
				{
					EatMemoriesOnHit.EatMemories(gameObjectParameter2, gameObjectParameter3, gameObjectParameter4, this.MemoriesLost);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046AD RID: 18093
		public int Chance = 100;

		// Token: 0x040046AE RID: 18094
		public string MemoriesLost = "1d2";
	}
}
