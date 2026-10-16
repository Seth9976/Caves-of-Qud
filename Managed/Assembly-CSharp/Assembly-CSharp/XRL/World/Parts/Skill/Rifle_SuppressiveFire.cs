using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001377 RID: 4983
	[Serializable]
	public class Rifle_SuppressiveFire : BaseSkill
	{
		// Token: 0x0600C18C RID: 49548 RVA: 0x0046C35D File Offset: 0x0046A55D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSuppressiveFire");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C18D RID: 49549 RVA: 0x0046C374 File Offset: 0x0046A574
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSuppressiveFire")
			{
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return true;
				}
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
					Combat.FireMissileWeapon(this.ParentObject, null, null, FireType.SuppressingFire, null, 0, 0, 90, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C18E RID: 49550 RVA: 0x0046C52C File Offset: 0x0046A72C
		public override bool AddSkill(GameObject GO)
		{
			return base.AddSkill(GO);
		}

		// Token: 0x0600C18F RID: 49551 RVA: 0x0046C535 File Offset: 0x0046A735
		public override bool RemoveSkill(GameObject GO)
		{
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D7B RID: 19835
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
