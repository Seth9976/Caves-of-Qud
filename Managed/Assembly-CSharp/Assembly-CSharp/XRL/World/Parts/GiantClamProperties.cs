using System;
using System.Linq;
using Qud.API;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E76 RID: 3702
	public class GiantClamProperties : IPart
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06009574 RID: 38260 RVA: 0x00360F00 File Offset: 0x0035F100
		public ClamSystem ClamSystem
		{
			get
			{
				return The.Game.RequireSystem<ClamSystem>(() => new ClamSystem());
			}
		}

		// Token: 0x06009575 RID: 38261 RVA: 0x00360F2B File Offset: 0x0035F12B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == ObjectStoppedFlyingEvent.ID;
		}

		// Token: 0x06009576 RID: 38262 RVA: 0x00360F51 File Offset: 0x0035F151
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Smart)
			{
				E.MinWeight(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009577 RID: 38263 RVA: 0x00360F6A File Offset: 0x0035F16A
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return !this.CheckTeleport(E.Object, E.Cell) && base.HandleEvent(E);
		}

		// Token: 0x06009578 RID: 38264 RVA: 0x00360F89 File Offset: 0x0035F189
		public override bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return !this.CheckTeleport(E.Object, E.Cell) && base.HandleEvent(E);
		}

		// Token: 0x06009579 RID: 38265 RVA: 0x00360FA8 File Offset: 0x0035F1A8
		public bool CheckTeleport(GameObject Object, Cell C = null)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			if (!this.IsValidTarget(Object))
			{
				return false;
			}
			if (C == null)
			{
				C = Object.GetCurrentCell();
			}
			if (C.ParentZone.Built)
			{
				if (Object.IsPlayer() && !this.ParentObject.HasIntProperty("EnteredByPlayer"))
				{
					this.ParentObject.SetIntProperty("EnteredByPlayer", 1, false);
					Achievement.CLAMS_ENTERED_100.Progress.Increment(1);
					if (this.ParentObject.HasIntProperty("MakClam"))
					{
						Achievement.ENTER_MAK_CLAM.Unlock();
					}
				}
				if (this.ParentObject.CurrentZone.ZoneID.StartsWith("JoppaWorld."))
				{
					if (Object.IsPlayer() && GiantClamProperties.ReadyForClamWorld(Object))
					{
						this.TeleportToClamWorld(Object);
					}
					else
					{
						this.TeleportJoppaWorld(Object);
					}
				}
				else
				{
					this.TeleportFromClamWorld(Object);
				}
			}
			return true;
		}

		// Token: 0x0600957A RID: 38266 RVA: 0x00361088 File Offset: 0x0035F288
		public bool IsValidTarget(GameObject Object)
		{
			return Object != null && Object != this.ParentObject && Object.IsReal && Object.Brain != null && Object.GetLongProperty("ClamTeleportTurn", 0L) != The.Game.Turns && this.ParentObject.PhaseAndFlightMatches(Object) && !Object.GetBlueprint(true).DescendsFrom("Giant Clam");
		}

		// Token: 0x0600957B RID: 38267 RVA: 0x003610F0 File Offset: 0x0035F2F0
		public static bool Teleport(GameObject Object, Cell TargetCell, char Color)
		{
			Object.SetLongProperty("ClamTeleportTurn", The.Game.Turns);
			string text = "&" + char.ToUpper(Color).ToString();
			string text2 = "&" + char.ToLower(Color).ToString();
			bool flag = Object.TeleportTo(TargetCell, new int?(0), true, false, false, false, "disappear", null);
			if (Object.IsPlayer())
			{
				Object.TeleportSwirl(null, text, false, null, 'ù', false);
			}
			else
			{
				Object.SmallTeleportSwirl(null, text, false, null, false);
			}
			Object.Physics.PlayWorldSound("Sounds/Foley/fly_tileMove_water_wade", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			Object.Heartspray(text, text2, "&C", "&Y", 'ù');
			Object.Heartspray(text, text2, "&C", text2, 'ú');
			Object.Heartspray(text, text2, "&C", "&Y", 'ø');
			Object.MakeActive(false);
			return flag;
		}

		// Token: 0x0600957C RID: 38268 RVA: 0x003611F8 File Offset: 0x0035F3F8
		public GameObject GetLinkedClam(Zone Z)
		{
			int id;
			if (this.ParentObject.TryGetIntProperty("ClamId", out id))
			{
				GameObject gameObject = Z.FindObject((GameObject o) => o.GetIntProperty("ClamId", -1) == id);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return Z.GetObjects("Giant Clam").GetRandomElement<GameObject>(null);
		}

		// Token: 0x0600957D RID: 38269 RVA: 0x0036124C File Offset: 0x0035F44C
		public void TeleportToClamWorld(GameObject Object)
		{
			bool flag = The.ZoneManager.IsZoneBuilt(this.ClamSystem.ClamWorldId);
			Zone clamZone = this.ClamSystem.GetClamZone();
			GameObject linkedClam = this.GetLinkedClam(clamZone);
			Cell cell = ((linkedClam != null) ? linkedClam.CurrentCell : null);
			if (cell == null)
			{
				IComponent<GameObject>.AddPlayerMessage("You hear a shloop and then a hitch. Nothing happens.", null, true);
				if (clamZone.CountObjects((GameObject x) => x.IsReal) == 0)
				{
					MetricsManager.LogError("Tzimtzlum empty, zone " + (flag ? "was" : "was not") + " built previously.");
					if (Popup.ShowYesNo("This zone didn't build properly, do you wish to rebuild it?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						The.ZoneManager.SuspendZone(clamZone);
						The.ZoneManager.DeleteZone(clamZone);
						this.TeleportToClamWorld(Object);
					}
				}
				return;
			}
			Object.Physics.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_spacetimeWeirdness", 1f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			SoundManager.PlayMusic("Music/Clam Dimension", "music", true, 20f, 1f, null, true);
			Popup.Show("You hear a shloop, and the world around you warps and shifts violently.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("One dram of {{neutronic|neutron}} flux evaporates from your inventory.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("In the midst of your disorientation, you find a passageway to another dimension.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Object.UseDrams(1, "neutronflux", null, null, null, null, false);
			GiantClamProperties.Teleport(Object, cell, 'O');
			if (!The.Game.HasBooleanGameState("VisitedTzimtzlum"))
			{
				The.Game.SetBooleanGameState("VisitedTzimtzlum", true);
				JournalAPI.AddAccomplishment("While dimensionally lost, you discovered a passageway to the pocket dimension Tzimtzlum and traveled there.", "Via the power of an imaginative mind at cosmological scales, =name= created the pocket dimension Tzimtzlum and traveled there, immanentizing it for all astral wanderers.", "While lost in the warrens of space and time, =name= discovered the pocket dimension Tzimtzlum. There " + The.Player.GetPronounProvider(false).Subjective + " sat at the conjunction of leylines and befriended three hundred clams.", null, "general", MuralCategory.VisitsLocation, MuralWeight.High, null, -1L, true);
				Achievement.TRAVEL_TZIMTZLUM.Unlock();
				MetricsManager.SendTelemetry("game_event", "travel.clam_world", string.Concat(new string[]
				{
					Object.DisplayNameOnlyDirect,
					":",
					Object.Level.ToString(),
					":",
					The.Game.Turns.ToString()
				}));
			}
		}

		// Token: 0x0600957E RID: 38270 RVA: 0x00361488 File Offset: 0x0035F688
		public void TeleportFromClamWorld(GameObject Object)
		{
			Zone joppaZone = this.ClamSystem.GetJoppaZone(this.ParentObject.GetIntProperty("ClamId", 0));
			GameObject linkedClam = this.GetLinkedClam(joppaZone);
			Cell cell = ((linkedClam != null) ? linkedClam.CurrentCell : null);
			if (cell == null)
			{
				if (Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You hear a shloop and then a hitch. Nothing happens.", null, true);
				}
				return;
			}
			GiantClamProperties.Teleport(Object, cell, 'B');
			if (Object.IsPlayer())
			{
				Popup.Show("You find a passageway back to your home dimension.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600957F RID: 38271 RVA: 0x00361508 File Offset: 0x0035F708
		public void TeleportJoppaWorld(GameObject Object)
		{
			Zone joppaZone = this.ClamSystem.GetJoppaZone(this.ParentObject.CurrentZone.ZoneID);
			GameObject randomElement = joppaZone.GetObjects("Giant Clam").GetRandomElement<GameObject>(null);
			Cell cell = ((randomElement != null) ? randomElement.CurrentCell : null);
			if (cell == null)
			{
				cell = joppaZone.GetEmptyCellsShuffled().FirstOrDefault<Cell>();
				if (cell == null)
				{
					IComponent<GameObject>.AddPlayerMessage("You hear a shloop and then a hitch. Nothing happens.", null, true);
					return;
				}
				cell.AddObject("Giant Clam", null, null, null, null);
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You hear a shloop and the world around you shifts.", null, true);
			}
			GiantClamProperties.Teleport(Object, cell, 'B');
		}

		// Token: 0x06009580 RID: 38272 RVA: 0x0036159E File Offset: 0x0035F79E
		public static bool ReadyForClamWorld(GameObject go)
		{
			return go.HasEffect<Lost>() && go.GetFreeDrams("neutronflux", null, null, null, false) >= 1;
		}
	}
}
