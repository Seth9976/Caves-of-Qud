using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C41 RID: 3137
	[Serializable]
	public class Broken : Effect, ITierInitialized, IBusted
	{
		// Token: 0x06007FFB RID: 32763 RVA: 0x002F277F File Offset: 0x002F097F
		public Broken()
		{
			this.DisplayName = "{{r|broken}}";
			this.Duration = 1;
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x002F2799 File Offset: 0x002F0999
		public Broken(bool FromDamage = false, bool FromExamine = false, bool FromOverload = false, bool FromModding = false)
			: this()
		{
			this.FromDamage = FromDamage;
			this.FromExamine = FromExamine;
			this.FromOverload = FromOverload;
			this.FromModding = FromModding;
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x002F27BE File Offset: 0x002F09BE
		public override int GetEffectType()
		{
			return 100664320;
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x002F27C5 File Offset: 0x002F09C5
		public override string GetDetails()
		{
			return "Can't be equipped or used.";
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x002F27CC File Offset: 0x002F09CC
		public override bool Apply(GameObject Object)
		{
			if (Object.IsBroken())
			{
				return false;
			}
			if (!Object.HasTagOrProperty("Breakable"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyBroken"))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_mechanicalRupture", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			GameObject holder = Object.Holder;
			if (holder != null)
			{
				holder.ParticleText("*" + Object.ShortDisplayNameStripped + " broken*", IComponent<GameObject>.ConsequentialColorChar(null, holder), false, 1.5f, -8f);
				Event @event = Event.New("CommandUnequipObject", 0, 0, 0);
				@event.SetParameter("BodyPart", holder.FindEquippedObject(Object));
				@event.SetFlag("SemiForced", true);
				holder.FireEvent(@event);
			}
			else
			{
				Object.ParticleText("*" + Object.ShortDisplayNameStripped + " broken*", 'R', false, 1.5f, -8f);
			}
			return true;
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x002F28C6 File Offset: 0x002F0AC6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x002F28FC File Offset: 0x002F0AFC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				E.AddTag("[{{r|broken}}]", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x002F291A File Offset: 0x002F0B1A
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(0.01);
			return base.HandleEvent(E);
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x002F2932 File Offset: 0x002F0B32
		public override bool HandleEvent(IsRepairableEvent E)
		{
			return false;
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x002F2935 File Offset: 0x002F0B35
		public override bool HandleEvent(RepairedEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x002F294C File Offset: 0x002F0B4C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x002F2965 File Offset: 0x002F0B65
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingEquipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008007 RID: 32775 RVA: 0x002F297C File Offset: 0x002F0B7C
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
					" broken!"
				});
				if (E.GetIntParameter("AutoEquipTry", 0) > 0)
				{
					E.SetParameter("FailureMessage", text);
				}
				else if (E.GetGameObjectParameter("Equipper").IsPlayer())
				{
					Popup.ShowFail(text, true, true, true);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038E3 RID: 14563
		public bool FromDamage;

		// Token: 0x040038E4 RID: 14564
		public bool FromExamine;

		// Token: 0x040038E5 RID: 14565
		public bool FromOverload;

		// Token: 0x040038E6 RID: 14566
		public bool FromModding;
	}
}
