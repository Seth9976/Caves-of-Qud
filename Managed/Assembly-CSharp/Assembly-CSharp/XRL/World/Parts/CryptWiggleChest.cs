using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E6C RID: 3692
	[Serializable]
	public class CryptWiggleChest : IPart
	{
		// Token: 0x06009530 RID: 38192 RVA: 0x0035FB21 File Offset: 0x0035DD21
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009531 RID: 38193 RVA: 0x0035FB24 File Offset: 0x0035DD24
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDestroyObjectEvent.ID;
		}

		// Token: 0x06009532 RID: 38194 RVA: 0x0035FB3A File Offset: 0x0035DD3A
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.BroadcastHelp();
			return base.HandleEvent(E);
		}

		// Token: 0x06009533 RID: 38195 RVA: 0x0035FB49 File Offset: 0x0035DD49
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009534 RID: 38196 RVA: 0x0035FB4C File Offset: 0x0035DD4C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009535 RID: 38197 RVA: 0x0035FB64 File Offset: 0x0035DD64
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				CombatJuice.playPrefabAnimation(this.ParentObject, "Abilities/AbilityVFXWiggle", this.ParentObject.ID, string.Concat(new string[]
				{
					this.ParentObject.Render.Tile,
					";",
					this.ParentObject.Render.GetTileForegroundColor(),
					";",
					this.ParentObject.Render.getDetailColor().ToString()
				}), null, false);
				this.BroadcastHelp();
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009536 RID: 38198 RVA: 0x0035FC10 File Offset: 0x0035DE10
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
