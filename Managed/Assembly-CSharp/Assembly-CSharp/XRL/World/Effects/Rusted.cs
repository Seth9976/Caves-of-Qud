using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D7E RID: 3454
	[Serializable]
	public class Rusted : Effect, ITierInitialized, IBusted
	{
		// Token: 0x06008916 RID: 35094 RVA: 0x00311468 File Offset: 0x0030F668
		public Rusted()
		{
			this.DisplayName = "{{r|rusted}}";
			this.Duration = 1;
		}

		// Token: 0x06008917 RID: 35095 RVA: 0x00311482 File Offset: 0x0030F682
		public Rusted(bool StartMessageUsePopup = false)
			: this()
		{
			this.StartMessageUsePopup = StartMessageUsePopup;
		}

		// Token: 0x06008918 RID: 35096 RVA: 0x00311491 File Offset: 0x0030F691
		public override int GetEffectType()
		{
			return 117441536;
		}

		// Token: 0x06008919 RID: 35097 RVA: 0x00311498 File Offset: 0x0030F698
		public override string GetDetails()
		{
			if (base.Object.IsCreature)
			{
				return "-70 Quickness\nDoesn't function properly.";
			}
			return "Doesn't function properly and can't be equipped.\nWill erode into dust if rusted again.";
		}

		// Token: 0x0600891A RID: 35098 RVA: 0x003114B4 File Offset: 0x0030F6B4
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasPart<Metal>())
			{
				return false;
			}
			if (!Object.FireEvent("ApplyRusted"))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_rusted", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.HasEffect<Rusted>())
			{
				if (Object.IsCreature)
				{
					return false;
				}
				GameObject gameObject = Object.Holder ?? Object;
				if (IComponent<GameObject>.Visible(gameObject))
				{
					gameObject.ParticleText("*" + Object.DisplayNameOnlyStripped + " reduced to dust*", 'r', false, 1.5f, -8f);
				}
				base.DidX("are", "reduced to dust", "!", null, null, null, gameObject, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				Object.Die(null, null, "You were reduced to dust.", Object.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@reduced to dust.", false, null, null, false, false, "", null, null);
			}
			else
			{
				GameObject equipped = Object.Equipped;
				GameObject gameObject2;
				if ((gameObject2 = equipped) == null)
				{
					gameObject2 = Object.InInventory ?? Object;
				}
				GameObject gameObject3 = gameObject2;
				if (IComponent<GameObject>.Visible(gameObject3))
				{
					gameObject3.ParticleText("*" + Object.DisplayNameOnlyStripped + " rusted*", 'r', false, 1.5f, -8f);
				}
				base.DidX("rust", null, null, null, null, null, gameObject3, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				if (equipped != null)
				{
					equipped.FireEvent(Event.New("CommandUnequipObject", "BodyPart", equipped.FindEquippedObject(Object), "SemiForced", 1));
				}
				this.ApplyStats();
			}
			return true;
		}

		// Token: 0x0600891B RID: 35099 RVA: 0x00311667 File Offset: 0x0030F867
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x0600891C RID: 35100 RVA: 0x00311676 File Offset: 0x0030F876
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x0600891D RID: 35101 RVA: 0x003116A4 File Offset: 0x0030F8A4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.AddTag("[{{r|rusted}}]", 20);
			return base.HandleEvent(E);
		}

		// Token: 0x0600891E RID: 35102 RVA: 0x003116BA File Offset: 0x0030F8BA
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(0.01);
			return base.HandleEvent(E);
		}

		// Token: 0x0600891F RID: 35103 RVA: 0x003116D2 File Offset: 0x0030F8D2
		public override bool HandleEvent(IsRepairableEvent E)
		{
			return false;
		}

		// Token: 0x06008920 RID: 35104 RVA: 0x003116D5 File Offset: 0x0030F8D5
		public override bool HandleEvent(RepairedEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06008921 RID: 35105 RVA: 0x003116EC File Offset: 0x0030F8EC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginBeingEquipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008922 RID: 35106 RVA: 0x00311718 File Offset: 0x0030F918
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginBeingEquipped")
			{
				string text = string.Concat(new string[]
				{
					"You can't equip ",
					base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					", ",
					base.Object.itis,
					" badly rusted!"
				});
				if (E.GetIntParameter("AutoEquipTry", 0) > 0)
				{
					E.SetParameter("FailureMessage", text);
				}
				else if (E.GetGameObjectParameter("Equipper").IsPlayer())
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x00311815 File Offset: 0x0030FA15
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&r", "w", Rusted.ICON_COLOR_FOREGROUND_PRIORITY, Rusted.ICON_COLOR_DETAIL_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x00311838 File Offset: 0x0030FA38
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "Speed", -70, false);
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x00311854 File Offset: 0x0030FA54
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x04003A6D RID: 14957
		public static readonly int ICON_COLOR_FOREGROUND_PRIORITY = 60;

		// Token: 0x04003A6E RID: 14958
		public static readonly int ICON_COLOR_DETAIL_PRIORITY = 120;

		// Token: 0x04003A6F RID: 14959
		public bool StartMessageUsePopup;
	}
}
