using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E0A RID: 3594
	[Serializable]
	public class BaetylHostility : IPart
	{
		// Token: 0x0600911F RID: 37151 RVA: 0x00345433 File Offset: 0x00343633
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x00345436 File Offset: 0x00343636
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009121 RID: 37153 RVA: 0x00345439 File Offset: 0x00343639
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckBaetylHostility();
		}

		// Token: 0x06009122 RID: 37154 RVA: 0x00345441 File Offset: 0x00343641
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009123 RID: 37155 RVA: 0x00345444 File Offset: 0x00343644
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanBeAngeredByBeingAttacked");
			Registrar.Register("CanBeAngeredByDamage");
			Registrar.Register("CanBeAngeredByFriendlyFire");
			Registrar.Register("CanBeAngeredByPropertyCrime");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x0034547C File Offset: 0x0034367C
		public override bool FireEvent(Event E)
		{
			return ((!(E.ID == "CanBeAngeredByBeingAttacked") && !(E.ID == "CanBeAngeredByDamage") && !(E.ID == "CanBeAngeredByFriendlyFire") && !(E.ID == "CanBeAngeredByPropertyCrime")) || this.ParentObject.Health() <= this.AngerThreshold) && base.FireEvent(E);
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x003454F0 File Offset: 0x003436F0
		public void CheckBaetylHostility()
		{
			bool flag = IComponent<GameObject>.ThePlayer != null && this.ParentObject.IsHostileTowards(IComponent<GameObject>.ThePlayer);
			if ((flag || this.ParentObject.Target != null) && this.ParentObject.Health() <= this.AngerThreshold)
			{
				if (flag && !string.IsNullOrEmpty(this.Message) && base.Visible())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				this.ParentObject.FireEvent("BaetylLeaving");
				if (this.ExplodeForce > 0)
				{
					base.DidX("explode", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
					this.ParentObject.Explode(this.ExplodeForce, null, this.ExplodeDamage, 1f, this.ExplodeNeutron, false, false, 0, null);
				}
				else
				{
					base.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
					this.ParentObject.Obliterate(null, false, null);
				}
				if (!string.IsNullOrEmpty(this.LeaveBehind))
				{
					currentCell.AddObject(this.LeaveBehind, null, null, null, null);
				}
			}
		}

		// Token: 0x04003C82 RID: 15490
		public double AngerThreshold = 0.2;

		// Token: 0x04003C83 RID: 15491
		public int ExplodeForce = 25000;

		// Token: 0x04003C84 RID: 15492
		public string ExplodeDamage = "10d10+75";

		// Token: 0x04003C85 RID: 15493
		public bool ExplodeNeutron = true;

		// Token: 0x04003C86 RID: 15494
		public string LeaveBehind = "Space-Time Vortex";

		// Token: 0x04003C87 RID: 15495
		public string Message = "";
	}
}
