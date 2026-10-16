using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200121F RID: 4639
	[Serializable]
	public class RebornOnDeathInThinWorld : IPart
	{
		// Token: 0x0600B83C RID: 47164 RVA: 0x0042FF74 File Offset: 0x0042E174
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B83D RID: 47165 RVA: 0x0042FF77 File Offset: 0x0042E177
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("BeforeTakeAction");
			Registrar.Register("BeforeDie");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B83E RID: 47166 RVA: 0x0042FFA4 File Offset: 0x0042E1A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTakeAction" || E.ID == "EnteredCell")
			{
				if (this.ParentObject.CurrentZone != null && this.ParentObject.CurrentZone.ZoneWorld != "ThinWorld")
				{
					this.ParentObject.Obliterate(null, false, null);
				}
				else if (this.Reborn)
				{
					this.Reborn = false;
					if (this.ParentObject.Brain != null)
					{
						this.ParentObject.Brain.Goals.Clear();
						this.ParentObject.Brain.Opinions.Clear();
					}
				}
			}
			else if (E.ID == "BeforeDie")
			{
				this.ParentObject.RestorePristineHealth(false, false);
				this.ParentObject.DilationSplat();
				this.ParentObject.SmallTeleportSwirl(null, "&C", true, null, false);
				if (this.ParentObject.IsPlayer())
				{
					Popup.Show("Death has no meaning here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (this.ParentObject.Brain != null)
				{
					this.ParentObject.Brain.Goals.Clear();
					this.ParentObject.Brain.Opinions.Clear();
					this.Reborn = true;
				}
				base.DidX("continue", "being", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004A20 RID: 18976
		public bool Reborn;
	}
}
