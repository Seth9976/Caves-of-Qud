using System;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FB5 RID: 4021
	[Serializable]
	public class ReshephsCrypt : IPart
	{
		// Token: 0x06009FD5 RID: 40917 RVA: 0x003A1A29 File Offset: 0x0039FC29
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FD6 RID: 40918 RVA: 0x003A1A2C File Offset: 0x0039FC2C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x003A1A2F File Offset: 0x0039FC2F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("BeforeClosing");
			Registrar.Register("SyncClosed");
			Registrar.Register("ObjectEntered");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009FD8 RID: 40920 RVA: 0x003A1A68 File Offset: 0x0039FC68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && this.ParentObject.Physics.CurrentCell.ParentZone.FindObject("Biographer") == null && this.ParentObject.HasIntProperty("Sealed"))
			{
				IComponent<GameObject>.XDidY(this.ParentObject, "open", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.RemoveIntProperty("Sealed");
				this.ParentObject.FireEvent("SyncOpened");
				foreach (GameObject gameObject in this.ParentObject.Physics.CurrentCell.ParentZone.GetObjectsWithPart("ReshephsCrypt"))
				{
					if (gameObject != this.ParentObject)
					{
						gameObject.RemoveIntProperty("Sealed");
						gameObject.FireEvent("SyncOpened");
					}
				}
			}
			if (E.ID == "SyncClosed" && this.transitallowed)
			{
				this.transitallowed = false;
				GameObject crypt = null;
				foreach (GameObject gameObject2 in this.ParentObject.Physics.currentCell.ParentZone.GetObjectsWithPart("ReshephsCrypt"))
				{
					gameObject2.SetStringProperty("EntombTriggered", "1", false);
					gameObject2.SetIntProperty("Sealed", 1, false);
					gameObject2.FireEvent("SyncClosed");
					crypt = gameObject2;
				}
				GameManager.Instance.gameQueue.queueTask(delegate
				{
					ThinWorld.TransitToThinWorld(crypt, false);
				}, 0);
			}
			if (E.ID == "BeforeClosing")
			{
				if (this.ParentObject.HasStringProperty("EntombTriggered"))
				{
					Popup.Show("The sarcophagus is inert.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.transitallowed = false;
					return true;
				}
				if (!(Popup.AskString("The sealing mechanisms inside this sarcophagus will certainly kill you if you close " + IComponent<GameObject>.ThePlayer.itself + " inside. Are you sure you want to enter the sarcophagus?\n\nType 'ENTOMB' to confirm.", "", "Sounds/UI/ui_notification", null, "ENTOMB", 6, 0, false, true, new bool?(false)).ToUpper() == "ENTOMB"))
				{
					this.transitallowed = false;
					return false;
				}
				if ((!IComponent<GameObject>.ThePlayer.IsOriginalPlayerBody() && IComponent<GameObject>.ThePlayer.HasEffect<Dominated>()) || IComponent<GameObject>.ThePlayer.HasEffect<WakingDream>())
				{
					Popup.Show("You mind swerves from afar, and a force of repulsion from inside the sarcophagus prevents your entry.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.transitallowed = false;
					return false;
				}
				if (!IComponent<GameObject>.ThePlayer.HasMarkOfDeath(null))
				{
					Popup.Show("A force of repulsion from inside the sarcophagus prevents your entry.\n\nYou do not bear the Mark of Death.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.transitallowed = false;
					return false;
				}
				Popup.Show("You climb into the sarcophagus.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				if (!The.Game.HasQuest("Tomb of the Eaters"))
				{
					The.Game.StartQuest("Tomb of the Eaters", null, null, null);
				}
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				Cell currentCell = IComponent<GameObject>.ThePlayer.GetCurrentCell();
				currentCell.RemoveObject(IComponent<GameObject>.ThePlayer, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				currentCell.ParentZone.LightAll(LightLevel.Light);
				currentCell.ParentZone.VisAll();
				currentCell.ParentZone.Render(scrapBuffer);
				scrapBuffer.Draw();
				currentCell.AddObject(IComponent<GameObject>.ThePlayer, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				GameManager.Instance.ForceGameView();
				Thread.Sleep(1000);
				this.transitallowed = true;
			}
			if (E.ID == "ObjectEntered")
			{
				bool flag = this.transitallowed;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009FD9 RID: 40921 RVA: 0x003A1E48 File Offset: 0x003A0048
		public static void AddMarkOfDeath()
		{
			XRLCore.Core.Game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
		}

		// Token: 0x04004272 RID: 17010
		public string Inscription = "";

		// Token: 0x04004273 RID: 17011
		public string Prefix = "";

		// Token: 0x04004274 RID: 17012
		public string Postfix = "";

		// Token: 0x04004275 RID: 17013
		public bool bFirst = true;

		// Token: 0x04004276 RID: 17014
		public string faction;

		// Token: 0x04004277 RID: 17015
		public string name;

		// Token: 0x04004278 RID: 17016
		public bool transitallowed;
	}
}
