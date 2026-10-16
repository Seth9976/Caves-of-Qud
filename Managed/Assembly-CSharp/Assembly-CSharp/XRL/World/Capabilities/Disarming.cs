using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E4 RID: 2532
	public static class Disarming
	{
		// Token: 0x06006C07 RID: 27655 RVA: 0x0022E318 File Offset: 0x0022C518
		public static GameObject Disarm(GameObject Subject, GameObject Disarmer, int SaveTarget, string SaveStat = "Strength", string DisarmerStat = "Agility", GameObject Weapon = null, GameObject DisarmingWeapon = null)
		{
			if (Weapon == null)
			{
				Body body = Subject.Body;
				if (body == null)
				{
					return null;
				}
				List<GameObject> list = Event.NewGameObjectList();
				foreach (BodyPart bodyPart in body.GetParts())
				{
					GameObject equipped = bodyPart.Equipped;
					MeleeWeapon meleeWeapon;
					if (equipped != null && !list.Contains(equipped) && equipped.IsReal && equipped.CanBeUnequipped(null, null, false, true) && (bodyPart.Type == "Missile Weapon" || (equipped.TryGetPart<MeleeWeapon>(out meleeWeapon) && meleeWeapon.AttackFromPart(bodyPart))))
					{
						list.Add(equipped);
					}
				}
				Weapon = list.GetRandomElement<GameObject>(null);
				if (Weapon == null)
				{
					return null;
				}
			}
			else if (!Weapon.CanBeUnequipped(null, null, false, true))
			{
				return null;
			}
			try
			{
				if (!Subject.MakeSave(SaveStat, SaveTarget, Disarmer, DisarmerStat, "Disarm", false, false, false, false, DisarmingWeapon) && Subject.FireEvent(Event.New("CommandForceUnequipObject", "Object", Weapon).SetSilent(true)))
				{
					string text = "disarm";
					string text2 = null;
					string text3 = "of";
					GameObject gameObject = Weapon;
					string text4 = null;
					string text5 = "!";
					string text6 = null;
					string text7 = null;
					GameObject gameObject2 = null;
					GameObject gameObject3 = Subject;
					Messaging.WDidXToYWithZ(Disarmer, text, text2, Subject, text3, gameObject, text4, text5, text6, text7, gameObject2, Subject, false, false, false, false, false, false, false, false, null, null, gameObject3, null, false, false, false, false, Subject.IsPlayerControlled(), null);
					if (InventoryActionEvent.Check(Subject, Subject, Weapon, "CommandDropObject", false, false, false, true, true, 0, 0, 0, null, null, null, null))
					{
						Weapon.Move(Directions.GetRandomDirection(), true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
					}
					return Weapon;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error when disarming " + Weapon.DebugName + " from " + Subject.DebugName, ex);
			}
			return null;
		}
	}
}
