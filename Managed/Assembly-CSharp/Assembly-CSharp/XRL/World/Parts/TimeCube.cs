using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010B4 RID: 4276
	[Serializable]
	public class TimeCube : IPart
	{
		// Token: 0x0600AA5D RID: 43613 RVA: 0x003E4E2C File Offset: 0x003E302C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AA5E RID: 43614 RVA: 0x003E4E64 File Offset: 0x003E3064
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivateTimeCube", null, 'a', false, 0, 0, false, false, true, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA5F RID: 43615 RVA: 0x003E4E9B File Offset: 0x003E309B
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateTimeCube")
			{
				this.Activate(E.Actor, false, null);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA60 RID: 43616 RVA: 0x003E4ECB File Offset: 0x003E30CB
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AA61 RID: 43617 RVA: 0x003E4EE1 File Offset: 0x003E30E1
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AA62 RID: 43618 RVA: 0x003E4EF7 File Offset: 0x003E30F7
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("time", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA63 RID: 43619 RVA: 0x003E4F1B File Offset: 0x003E311B
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			return (E.Pass != 1 || !GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) || !Chance.in100() || !this.Activate(E.Actor, true, E)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA64 RID: 43620 RVA: 0x003E4F54 File Offset: 0x003E3154
		public bool Activate(GameObject Actor, bool SilentIfFailed = false, IExamineEvent Identify = null)
		{
			if (!IComponent<GameObject>.CheckRealityDistortionUsability(Actor, null, Actor, this.ParentObject, null, null, 0) || !IComponent<GameObject>.CheckRealityDistortionUsability(this.ParentObject, null, Actor, this.ParentObject, null, null, 0))
			{
				if (!SilentIfFailed)
				{
					if (Actor.IsPlayer())
					{
						IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_longBeam_attack_chargeUp", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show("{{R|Fraudulent}} {{W|ONEness}} is taught by {{O|evil}} {{G|educators}}! Nothing happens!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					Actor.UseEnergy(1000, "Item Failure", null, null, false);
				}
				return false;
			}
			Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_timeCube_activate", null);
			if (Actor.IsPlayer())
			{
				Achievement.ACTIVATE_TIMECUBE.Unlock();
			}
			Actor.ApplyEffect(new TimeCubed(), null);
			if (Identify != null)
			{
				Identify.IdentifyImmediately();
			}
			this.ParentObject.Destroy(null, false, false, null);
			return true;
		}
	}
}
