using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E6D RID: 3693
	[Serializable]
	public class AICryptHelpBroadcaster : AIBehaviorPart
	{
		// Token: 0x06009538 RID: 38200 RVA: 0x0035FC90 File Offset: 0x0035DE90
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009539 RID: 38201 RVA: 0x0035FC93 File Offset: 0x0035DE93
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDestroyObjectEvent.ID;
		}

		// Token: 0x0600953A RID: 38202 RVA: 0x0035FCA9 File Offset: 0x0035DEA9
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.BroadcastHelp();
			return base.HandleEvent(E);
		}

		// Token: 0x0600953B RID: 38203 RVA: 0x0035FCB8 File Offset: 0x0035DEB8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600953C RID: 38204 RVA: 0x0035FCBB File Offset: 0x0035DEBB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Opening");
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600953D RID: 38205 RVA: 0x0035FCDC File Offset: 0x0035DEDC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage")
			{
				if (this.ParentObject.isDamaged(0.9, true))
				{
					this.BroadcastHelp();
				}
			}
			else if (E.ID == "Opening")
			{
				this.BroadcastHelp();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600953E RID: 38206 RVA: 0x0035FD3C File Offset: 0x0035DF3C
		public void BroadcastHelp()
		{
			if (this.ParentObject.IsNowhere())
			{
				return;
			}
			foreach (GameObject gameObject in this.ParentObject.CurrentCell.FastFloodVisibility("CryptSitterBehavior", 10))
			{
				gameObject.FireEvent("AICryptHelpBroadcast");
			}
		}
	}
}
