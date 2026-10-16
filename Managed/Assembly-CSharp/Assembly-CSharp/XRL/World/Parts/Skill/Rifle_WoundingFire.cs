using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001379 RID: 4985
	[Serializable]
	public class Rifle_WoundingFire : BaseSkill
	{
		// Token: 0x0600C193 RID: 49555 RVA: 0x0046C560 File Offset: 0x0046A760
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandWoundingFire");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C194 RID: 49556 RVA: 0x0046C578 File Offset: 0x0046A778
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandWoundingFire")
			{
				List<GameObject> missileWeapons = this.ParentObject.GetMissileWeapons(null, null);
				bool flag = false;
				bool flag2 = false;
				string text = null;
				GameObject gameObject = null;
				if (missileWeapons != null && missileWeapons.Count > 0)
				{
					foreach (GameObject gameObject2 in missileWeapons)
					{
						MissileWeapon part = gameObject2.GetPart<MissileWeapon>();
						if (part != null && (part.Skill == "Rifle" || part.Skill == "Bow"))
						{
							flag2 = true;
							if (part.ReadyToFire())
							{
								flag = true;
								break;
							}
							if (text == null)
							{
								text = part.GetNotReadyToFireMessage();
								gameObject = part.ParentObject;
							}
						}
					}
				}
				if (!flag2)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.Show("You do not have a bow or rifle equipped!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else if (!flag)
				{
					if (this.ParentObject.IsPlayer())
					{
						SoundManager.PlaySound((gameObject != null) ? gameObject.GetSoundTag("NoAmmoSound", null) : null, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
						Popup.Show(text ?? ("You need to reload! (" + ControlManager.getCommandInputDescription("CmdReload", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + ")"), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else
				{
					Combat.FireMissileWeapon(this.ParentObject, null, null, FireType.WoundingFire, null, 0, 0, 90, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C195 RID: 49557 RVA: 0x0046C71C File Offset: 0x0046A91C
		public override bool AddSkill(GameObject GO)
		{
			return base.AddSkill(GO);
		}

		// Token: 0x0600C196 RID: 49558 RVA: 0x0046C725 File Offset: 0x0046A925
		public override bool RemoveSkill(GameObject GO)
		{
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D7C RID: 19836
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
