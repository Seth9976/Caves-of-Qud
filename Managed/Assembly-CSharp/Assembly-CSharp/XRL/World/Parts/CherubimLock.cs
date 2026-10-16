using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001008 RID: 4104
	[Serializable]
	public class CherubimLock : IPart
	{
		// Token: 0x0600A385 RID: 41861 RVA: 0x003BC193 File Offset: 0x003BA393
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A386 RID: 41862 RVA: 0x003BC196 File Offset: 0x003BA396
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeContentsTaken");
			Registrar.Register("AfterContentsTaken");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A387 RID: 41863 RVA: 0x003BC1B6 File Offset: 0x003BA3B6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDestroyObjectEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneThawedEvent.ID || ID == AfterZoneBuiltEvent.ID;
		}

		// Token: 0x0600A388 RID: 41864 RVA: 0x003BC1E4 File Offset: 0x003BA3E4
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.Obliterate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A389 RID: 41865 RVA: 0x003BC1F3 File Offset: 0x003BA3F3
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.Obliterate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A38A RID: 41866 RVA: 0x003BC202 File Offset: 0x003BA402
		public override bool HandleEvent(AfterZoneBuiltEvent E)
		{
			this.Obliterate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A38B RID: 41867 RVA: 0x003BC211 File Offset: 0x003BA411
		public void Obliterate()
		{
			if (The.Game.GetIntGameState("RobberChimesTriggered", 0) == 1 && !this.ParentObject.HasIntProperty("Raised"))
			{
				this.ParentObject.Obliterate(null, false, null);
			}
		}

		// Token: 0x0600A38C RID: 41868 RVA: 0x003BC247 File Offset: 0x003BA447
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.Chime();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A38D RID: 41869 RVA: 0x003BC258 File Offset: 0x003BA458
		public void Chime()
		{
			if (!The.Game.HasIntGameState("RobberChimesTriggered"))
			{
				The.Game.SetIntGameState("RobberChimesTriggered", 1);
				Popup.Show("Robber chimes ring through the vault, and you hear the grating of stone on stone as the other imperial reliquaries slide into the unreachable recesses of the Tomb.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.ParentObject.SetIntProperty("Raised", 1, false);
				GameObject sibling = this.ParentObject.GetPart<DoubleContainer>().GetSibling(null);
				if (sibling != null)
				{
					sibling.SetIntProperty("Raised", 1, false);
				}
				AIHelpBroadcastEvent.Send(this.ParentObject, The.Player, null, "Cherubim", 80, 10f, HelpCause.Theft);
				foreach (Zone zone in The.ZoneManager.CachedZones.Values)
				{
					foreach (GameObject gameObject in zone.IterateObjects())
					{
						if (this.IsLocked(gameObject))
						{
							gameObject.Obliterate(null, false, null);
						}
					}
				}
			}
		}

		// Token: 0x0600A38E RID: 41870 RVA: 0x003BC370 File Offset: 0x003BA570
		private bool IsLocked(GameObject Object)
		{
			return !Object.HasIntProperty("Raised") && Object.HasPart(typeof(CherubimLock));
		}

		// Token: 0x0600A38F RID: 41871 RVA: 0x003BC394 File Offset: 0x003BA594
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeContentsTaken")
			{
				if (this.ParentObject.CurrentCell.ParentZone.GetObjectsWithTagOrProperty("CherubimLock", false).Count > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Taker");
					if (gameObjectParameter != null && gameObjectParameter.IsPlayerControlled())
					{
						base.EmitMessage("The protective force of the cherubim prevents " + gameObjectParameter.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " from taking anything from the reliquary.", ' ', false, gameObjectParameter.IsPlayer(), false, null, null);
					}
					return false;
				}
			}
			else if (E.ID == "AfterContentsTaken")
			{
				this.Chime();
				return true;
			}
			return base.FireEvent(E);
		}
	}
}
