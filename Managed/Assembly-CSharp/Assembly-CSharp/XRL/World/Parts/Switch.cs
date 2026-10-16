using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001298 RID: 4760
	[Serializable]
	public class Switch : IPart
	{
		// Token: 0x0600BC34 RID: 48180 RVA: 0x0044CFE8 File Offset: 0x0044B1E8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC35 RID: 48181 RVA: 0x0044CFEB File Offset: 0x0044B1EB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600BC36 RID: 48182 RVA: 0x0044D024 File Offset: 0x0044B224
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (this.Enabled && E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, -1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC37 RID: 48183 RVA: 0x0044D081 File Offset: 0x0044B281
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600BC38 RID: 48184 RVA: 0x0044D084 File Offset: 0x0044B284
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (this.FlipSwitch(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC39 RID: 48185 RVA: 0x0044D0A4 File Offset: 0x0044B2A4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Flip", "flip", "FlipSwitch", null, 'f', false, 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC3A RID: 48186 RVA: 0x0044D0DC File Offset: 0x0044B2DC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "FlipSwitch" && this.FlipSwitch(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC3B RID: 48187 RVA: 0x0044D10B File Offset: 0x0044B30B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BC3C RID: 48188 RVA: 0x0044D10E File Offset: 0x0044B30E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Enable");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC3D RID: 48189 RVA: 0x0044D124 File Offset: 0x0044B324
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Enable")
			{
				if (!this.Enabled)
				{
					this.Enabled = true;
					this.ParentObject.Render.SetForegroundColor('G');
					if (base.Visible())
					{
						Popup.Show("You hear a loud click from across the room.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("You hear a distant click.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						SoundManager.PlaySound("Sounds/Interact/sfx_interact_powerSwitch_on", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					}
				}
			}
			else if (E.ID == "Disable" && this.Enabled)
			{
				this.Enabled = false;
				this.ParentObject.Render.SetForegroundColor('R');
				if (base.Visible())
				{
					Popup.Show("You hear a loud click from across the room.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("You hear a distant click.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BC3E RID: 48190 RVA: 0x0044D22D File Offset: 0x0044B42D
		public bool FlipSwitch(GameObject who)
		{
			if (!this.Enabled)
			{
				if (who.IsPlayer())
				{
					Popup.Show("Nothing happens.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return this.ParentObject.FireEvent("SwitchActivated");
		}

		// Token: 0x04004B6F RID: 19311
		public bool Enabled = true;
	}
}
