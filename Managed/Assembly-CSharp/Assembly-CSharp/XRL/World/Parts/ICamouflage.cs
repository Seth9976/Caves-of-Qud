using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200104E RID: 4174
	[Serializable]
	public abstract class ICamouflage : IActivePart
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600A62E RID: 42542 RVA: 0x003CD0F9 File Offset: 0x003CB2F9
		// (set) Token: 0x0600A62F RID: 42543 RVA: 0x003CD101 File Offset: 0x003CB301
		public string EffectClass
		{
			get
			{
				return this._EffectClass;
			}
			set
			{
				this._EffectClass = value;
				this.EffectType = ModManager.ResolveType("XRL.World.Effects." + this._EffectClass, false, false, true);
			}
		}

		// Token: 0x0600A630 RID: 42544 RVA: 0x003CD128 File Offset: 0x003CB328
		public ICamouflage()
		{
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A631 RID: 42545 RVA: 0x003CD140 File Offset: 0x003CB340
		public override bool SameAs(IPart p)
		{
			ICamouflage camouflage = p as ICamouflage;
			return camouflage.Level == this.Level && !(camouflage._EffectClass != this._EffectClass) && base.SameAs(p);
		}

		// Token: 0x0600A632 RID: 42546 RVA: 0x003CD180 File Offset: 0x003CB380
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.Description)) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A633 RID: 42547 RVA: 0x003CD1B4 File Offset: 0x003CB3B4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			string text = this.Description;
			if (text.Contains("=level="))
			{
				text = text.Replace("=level=", this.Level.ToString());
			}
			E.Postfix.AppendRules(text, base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A634 RID: 42548 RVA: 0x003CD207 File Offset: 0x003CB407
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, EnteredCellEvent.ID, 0, true);
			this.CheckCamouflage(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A635 RID: 42549 RVA: 0x003CD22A File Offset: 0x003CB42A
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, EnteredCellEvent.ID);
			this.CheckCamouflage(true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A636 RID: 42550 RVA: 0x003CD24B File Offset: 0x003CB44B
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckCamouflage(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A637 RID: 42551 RVA: 0x003CD25B File Offset: 0x003CB45B
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A638 RID: 42552 RVA: 0x003CD25E File Offset: 0x003CB45E
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckCamouflage(false);
		}

		// Token: 0x0600A639 RID: 42553 RVA: 0x003CD267 File Offset: 0x003CB467
		private ICamouflageEffect FindEffect(GameObject who)
		{
			return who.GetEffect<ICamouflageEffect>();
		}

		// Token: 0x0600A63A RID: 42554 RVA: 0x003CD270 File Offset: 0x003CB470
		private void CheckCamouflage(GameObject who)
		{
			ICamouflageEffect camouflageEffect = this.FindEffect(who);
			bool flag = camouflageEffect != null;
			if (!flag)
			{
				camouflageEffect = (ICamouflageEffect)Activator.CreateInstance(this.EffectType);
			}
			if (who.CurrentCell != null && who.CurrentCell.HasObjectOtherThan(new Predicate<GameObject>(camouflageEffect.EnablesCamouflage), who))
			{
				if (!flag)
				{
					who.ApplyEffect(camouflageEffect, null);
				}
				camouflageEffect.SetContribution(this.ParentObject, this.Level);
				return;
			}
			if (flag)
			{
				camouflageEffect.RemoveContribution(this.ParentObject);
			}
		}

		// Token: 0x0600A63B RID: 42555 RVA: 0x003CD2F0 File Offset: 0x003CB4F0
		private void RemoveCamouflage(GameObject who)
		{
			ICamouflageEffect camouflageEffect = this.FindEffect(who);
			if (camouflageEffect != null)
			{
				who.RemoveEffect(camouflageEffect, true);
			}
		}

		// Token: 0x0600A63C RID: 42556 RVA: 0x003CD311 File Offset: 0x003CB511
		private void CheckCamouflage(bool ForceRemove = false)
		{
			if (ForceRemove)
			{
				this.ForeachActivePartSubject(new Action<GameObject>(this.RemoveCamouflage), false);
				return;
			}
			this.ForeachActivePartSubject(new Action<GameObject>(this.CheckCamouflage), false);
		}

		// Token: 0x0400449F RID: 17567
		public int Level = 2;

		// Token: 0x040044A0 RID: 17568
		public string Description;

		// Token: 0x040044A1 RID: 17569
		public string _EffectClass;

		// Token: 0x040044A2 RID: 17570
		[NonSerialized]
		private Type EffectType;
	}
}
