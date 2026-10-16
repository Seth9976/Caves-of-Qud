using System;

namespace XRL.World.Parts
{
	// Token: 0x0200105B RID: 4187
	[Serializable]
	public class LifeSaver : IPoweredPart
	{
		// Token: 0x0600A6A2 RID: 42658 RVA: 0x003CF40A File Offset: 0x003CD60A
		public LifeSaver()
		{
			this.ChargeUse = 0;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A6A3 RID: 42659 RVA: 0x003CF449 File Offset: 0x003CD649
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A6A4 RID: 42660 RVA: 0x003CF44C File Offset: 0x003CD64C
		public override bool SameAs(IPart p)
		{
			LifeSaver lifeSaver = p as LifeSaver;
			return !(lifeSaver.DefaultActivationMessage != this.DefaultActivationMessage) && lifeSaver.ChanceVsLethal == this.ChanceVsLethal && !(lifeSaver.LethalMessage != this.LethalMessage) && lifeSaver.ChanceVsMaxHitpointsThreshold == this.ChanceVsMaxHitpointsThreshold && !(lifeSaver.MaxHitpointsThreshold != this.MaxHitpointsThreshold) && !(lifeSaver.MaxHitpointsThresholdMessage != this.MaxHitpointsThresholdMessage) && lifeSaver.ChanceVsCurrentHitpointsThreshold == this.ChanceVsCurrentHitpointsThreshold && !(lifeSaver.CurrentHitpointsThreshold != this.CurrentHitpointsThreshold) && !(lifeSaver.CurrentHitpointsThresholdMessage != this.CurrentHitpointsThresholdMessage) && lifeSaver.MaxUses == this.MaxUses && lifeSaver.DestroyWhenUsedUp == this.DestroyWhenUsedUp && !(lifeSaver.DestroyWhenUsedUpMessage != this.DestroyWhenUsedUpMessage) && lifeSaver.Used == this.Used && base.SameAs(p);
		}

		// Token: 0x0600A6A5 RID: 42661 RVA: 0x003CF55A File Offset: 0x003CD75A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600A6A6 RID: 42662 RVA: 0x003CF588 File Offset: 0x003CD788
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "LateBeforeApplyDamage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6A7 RID: 42663 RVA: 0x003CF5A2 File Offset: 0x003CD7A2
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "LateBeforeApplyDamage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6A8 RID: 42664 RVA: 0x003CF5BC File Offset: 0x003CD7BC
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "LateBeforeApplyDamage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6A9 RID: 42665 RVA: 0x003CF5D6 File Offset: 0x003CD7D6
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "LateBeforeApplyDamage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6AA RID: 42666 RVA: 0x003CF5F0 File Offset: 0x003CD7F0
		public bool CheckLifeSaverActivation(GameObject who, Damage dmg)
		{
			if (who == null)
			{
				return false;
			}
			if (dmg == null)
			{
				return false;
			}
			if (this.Used >= this.MaxUses)
			{
				return false;
			}
			if (!this.IsObjectActivePartSubject(who))
			{
				return false;
			}
			int num = 0;
			string text = null;
			int hitpoints = who.hitpoints;
			if (dmg.Amount >= hitpoints)
			{
				num = this.ChanceVsLethal;
				text = this.LethalMessage;
			}
			else if (!this.MaxHitpointsThreshold.IsNullOrEmpty() && dmg.Amount >= who.baseHitpoints * this.MaxHitpointsThreshold.RollCached() / 100)
			{
				num = this.ChanceVsMaxHitpointsThreshold;
				text = this.MaxHitpointsThresholdMessage;
			}
			else if (!this.CurrentHitpointsThreshold.IsNullOrEmpty() && dmg.Amount >= hitpoints * this.CurrentHitpointsThreshold.RollCached() / 100)
			{
				num = this.ChanceVsCurrentHitpointsThreshold;
				text = this.CurrentHitpointsThresholdMessage;
			}
			if (text.IsNullOrEmpty())
			{
				text = this.DefaultActivationMessage;
			}
			if (!num.in100())
			{
				return false;
			}
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			if (!text.IsNullOrEmpty())
			{
				IComponent<GameObject>.EmitMessage(who, GameText.VariableReplace(text, who, this.ParentObject, true), ' ', true, false, false, null, null);
			}
			this.Used++;
			if (this.DestroyWhenUsedUp && this.Used >= this.MaxUses)
			{
				if (!this.DestroyWhenUsedUpMessage.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(who, GameText.VariableReplace(this.DestroyWhenUsedUpMessage, who, this.ParentObject, true), ' ', true, false, false, null, null);
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return true;
		}

		// Token: 0x0600A6AB RID: 42667 RVA: 0x003CF77C File Offset: 0x003CD97C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LateBeforeApplyDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				Damage damage = E.GetParameter("Damage") as Damage;
				if (this.CheckLifeSaverActivation(gameObjectParameter, damage))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040044D4 RID: 17620
		public string DefaultActivationMessage;

		// Token: 0x040044D5 RID: 17621
		public int ChanceVsLethal = 100;

		// Token: 0x040044D6 RID: 17622
		public string LethalMessage;

		// Token: 0x040044D7 RID: 17623
		public int ChanceVsMaxHitpointsThreshold = 100;

		// Token: 0x040044D8 RID: 17624
		public string MaxHitpointsThreshold = "50";

		// Token: 0x040044D9 RID: 17625
		public string MaxHitpointsThresholdMessage;

		// Token: 0x040044DA RID: 17626
		public int ChanceVsCurrentHitpointsThreshold;

		// Token: 0x040044DB RID: 17627
		public string CurrentHitpointsThreshold;

		// Token: 0x040044DC RID: 17628
		public string CurrentHitpointsThresholdMessage;

		// Token: 0x040044DD RID: 17629
		public int MaxUses = 1;

		// Token: 0x040044DE RID: 17630
		public bool DestroyWhenUsedUp = true;

		// Token: 0x040044DF RID: 17631
		public string DestroyWhenUsedUpMessage;

		// Token: 0x040044E0 RID: 17632
		public int Used;
	}
}
