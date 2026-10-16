using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using ConsoleLib.Console;
using Occult.Engine.CodeGeneration;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DD1 RID: 3537
	[HasWishCommand]
	[GenerateSerializationPartial]
	[Serializable]
	public class TerrainTravel : IPart
	{
		// Token: 0x06009007 RID: 36871 RVA: 0x0033F06C File Offset: 0x0033D26C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.LostChance);
			Writer.WriteOptimized(this.Segments);
			Writer.WriteOptimized(this.TravelClass);
			Writer.WriteComposite<EncounterEntry>(this.Encounters);
		}

		// Token: 0x06009008 RID: 36872 RVA: 0x0033F09E File Offset: 0x0033D29E
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.LostChance = Reader.ReadOptimizedInt32();
			this.Segments = Reader.ReadOptimizedInt32();
			this.TravelClass = Reader.ReadOptimizedString();
			this.Encounters = Reader.ReadCompositeList<EncounterEntry>();
		}

		// Token: 0x06009009 RID: 36873 RVA: 0x0033F0D0 File Offset: 0x0033D2D0
		public override bool SameAs(IPart Part)
		{
			TerrainTravel terrainTravel = Part as TerrainTravel;
			return terrainTravel.LostChance == this.LostChance && terrainTravel.Segments == this.Segments && !(terrainTravel.TravelClass != this.TravelClass) && terrainTravel.Encounters == null && this.Encounters == null && base.SameAs(Part);
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x0033F132 File Offset: 0x0033D332
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID || ID == ObjectLeavingCellEvent.ID;
		}

		// Token: 0x0600900B RID: 36875 RVA: 0x0033F150 File Offset: 0x0033D350
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object.IsPlayer())
			{
				if (!this.ParentObject.FireEvent("CheckLostChance"))
				{
					return false;
				}
				if (!this.Encounters.IsNullOrEmpty<EncounterEntry>() && TutorialManager.AllowOverlandEncounters())
				{
					int num = 10;
					int num2 = num;
					if (Options.DebugEncounterChance)
					{
						int @for = EncounterChanceEvent.GetFor(E.Object, this.TravelClass, 0, null);
						num2 = num * (100 + @for) / 100;
						IComponent<GameObject>.AddPlayerMessage("Base encounter chance: " + num2.ToString() + "%", null, true);
					}
					foreach (EncounterEntry encounterEntry in this.Encounters)
					{
						if (encounterEntry.Enabled)
						{
							int for2 = EncounterChanceEvent.GetFor(E.Object, this.TravelClass, 0, encounterEntry);
							int num3 = num;
							if (for2 != 0)
							{
								num3 = num3 * (100 + for2) / 100;
							}
							if (num3 != num2 && Options.DebugEncounterChance)
							{
								IComponent<GameObject>.AddPlayerMessage("Modified encounter chance: " + num3.ToString() + "%", null, true);
							}
							if (num3.in100())
							{
								if (Options.DebugEncounterChance)
								{
									IComponent<GameObject>.AddPlayerMessage("Triggered encounter chance: " + num3.ToString() + "%", null, true);
								}
								if (!encounterEntry.secretID.IsNullOrEmpty() && JournalAPI.GetMapNote(encounterEntry.secretID).Revealed)
								{
									break;
								}
								SoundManager.PlayUISound("ui_worldmap_encounter", 1f, false, false, SoundRequest.SoundEffectType.None);
								if (!encounterEntry.Optional || Popup.ShowYesNo(encounterEntry.Text, "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
								{
									if (!encounterEntry.Optional && !encounterEntry.Text.IsNullOrEmpty())
									{
										Popup.Show(encounterEntry.Text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									Zone zone = The.ZoneManager.SetActiveZone(encounterEntry.Zone);
									zone.CheckWeather();
									E.Object.SystemMoveTo(zone.GetPullDownLocation(E.Object), null, false, true, false, false);
									The.ZoneManager.ProcessGoToPartyLeader();
									this.RemoveEncounter(encounterEntry);
									return false;
								}
								break;
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x0033F3AC File Offset: 0x0033D5AC
		public override bool HandleEvent(ObjectLeavingCellEvent E)
		{
			if (E.Object != null && E.Object.IsPlayer() && !E.Forced && (E.Type == null || E.Type == "SystemLongDistanceMove") && !this.ParentObject.CurrentZone.ZoneID.Contains("Serenity"))
			{
				int num = 0;
				if (E.Object == null)
				{
					MetricsManager.LogError(new Exception("no object in ObjectLeavingCellEvent"));
					return base.HandleEvent(E);
				}
				if (!this.HandleLeavingCell(E.Object, ref num))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x0033F448 File Offset: 0x0033D648
		public int GetTravelSegments(GameObject Object)
		{
			int @for = TravelSpeedEvent.GetFor(Object, this.TravelClass, 0);
			return (int)(300f / ((float)Object.Speed * (100f + (float)@for) / 100f) * (float)this.Segments);
		}

		// Token: 0x0600900E RID: 36878 RVA: 0x0033F488 File Offset: 0x0033D688
		private bool HandleLeavingCell(GameObject Object, ref int TotalSegments)
		{
			Cell currentCell = The.Player.CurrentCell;
			XRLGame game = The.Game;
			ZoneManager zoneManager = game.ZoneManager;
			ActionManager actionManager = game.ActionManager;
			if (Object.IsPlayer())
			{
				game.SetStringGameState("LastLocationOnSurface", "");
			}
			int @for = GetLostChanceEvent.GetFor(The.Player, this.TravelClass, 0, 95, false);
			TotalSegments = this.GetTravelSegments(Object);
			int num = Math.Max(this.LostChance * (100 - @for) / 100, 0);
			if (The.Core.IDKFA || !TutorialManager.AllowOverlandEncounters())
			{
				num = 0;
			}
			if (Object.IsPlayer() && Options.DebugGetLostChance)
			{
				IComponent<GameObject>.AddPlayerMessage("Get lost chance: " + num.ToString() + "%", null, true);
			}
			if (num.in100())
			{
				string zoneWorld = currentCell.ParentZone.GetZoneWorld();
				int x = currentCell.X;
				int y = currentCell.Y;
				int num2 = 10;
				string text = ZoneID.Assemble(zoneWorld, x, y, Stat.Random(0, 2), Stat.Random(0, 2), num2);
				if (!The.ZoneManager.IsZoneBuilt(text))
				{
					Zone zone = The.ZoneManager.GetZone(text);
					Cell pullDownLocation = zone.GetPullDownLocation(The.Player);
					if (pullDownLocation.IsPassable(The.Player, true))
					{
						Lost lost = new Lost(9999, text, zoneWorld, 10, false);
						if (Object.ApplyEffect(lost, null))
						{
							zone.CheckWeather();
							The.Player.SystemMoveTo(pullDownLocation, null, false, true, false, false);
							Popup.ShowSpace("You're lost! Regain your bearings by exploring your surroundings.", null, "Sounds/UI/ui_notification", null, true, true, null);
							The.Player.FireEvent(Event.New("AfterLost", "FromCell", currentCell));
							return false;
						}
					}
				}
			}
			Cell currentCell2 = Object.CurrentCell;
			if (Object.IsPlayer() && Options.DebugTravelSpeed)
			{
				IComponent<GameObject>.AddPlayerMessage("Travel speed: " + TotalSegments.ToString() + " segments/parasang", null, true);
			}
			bool flag = false;
			XRLCore core = The.Core;
			int num3 = Math.Max(1, TotalSegments / 10);
			for (int i = 0; i < TotalSegments; i++)
			{
				actionManager.RunCommands(true);
				if (currentCell2 != Object.CurrentCell)
				{
					return false;
				}
				if (i % 10 == 0)
				{
					game.TimeTicks += 1L;
					if (!BeforeBeginTakeActionEvent.Check(Object) || !BeginTakeActionEvent.Check(Object, true, ref flag))
					{
						return false;
					}
					if (currentCell2 != Object.CurrentCell)
					{
						return false;
					}
					if (!CommandTakeActionEvent.Check(Object))
					{
						return false;
					}
					if (currentCell2 != Object.CurrentCell)
					{
						return false;
					}
					actionManager.ProcessIndependentEndTurn(Object);
					if (currentCell2 != Object.CurrentCell)
					{
						return false;
					}
					if (core.HPWarning)
					{
						core.HPWarning = false;
						if (Popup.ShowYesNo("{{R|Your health has dropped below {{C|" + Globals.HPWarningThreshold.ToString() + "%}}!}} Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							return false;
						}
					}
					MinEvent.ResetPools();
				}
			}
			actionManager.ProcessTurnTick(game.TimeTicks, num3);
			actionManager.TickAbilityCooldowns(TotalSegments);
			Object.Energy.BaseValue = 2200;
			Object.CleanEffects();
			AfterTravelEvent.Send(Object, this.ParentObject, TotalSegments);
			Object.FireEvent(Event.New("AfterTravel", "Segments", TotalSegments));
			if (Object.IsPlayer())
			{
				The.Game.Player.Messages.BeginPlayerTurn();
			}
			return true;
		}

		// Token: 0x0600900F RID: 36879 RVA: 0x0033F7C0 File Offset: 0x0033D9C0
		public void AddEncounter(EncounterEntry Entry)
		{
			if (this.Encounters == null)
			{
				this.Encounters = new List<EncounterEntry>();
			}
			this.Encounters.Add(Entry);
			if (this.ParentObject.HasTag("Immutable") || this.ParentObject.HasTag("ImmutableWhenUnexplored"))
			{
				this.ParentObject.SetIntProperty("ForceMutableSave", 1, false);
			}
		}

		// Token: 0x06009010 RID: 36880 RVA: 0x0033F823 File Offset: 0x0033DA23
		public void RemoveEncounter(EncounterEntry Entry)
		{
			if (this.Encounters == null)
			{
				return;
			}
			this.Encounters.Remove(Entry);
			if (this.Encounters.Count == 0)
			{
				this.Encounters = null;
			}
		}

		// Token: 0x06009011 RID: 36881 RVA: 0x0033F84F File Offset: 0x0033DA4F
		public void ClearEncounters()
		{
			this.Encounters = null;
		}

		// Token: 0x06009012 RID: 36882 RVA: 0x0033F858 File Offset: 0x0033DA58
		[WishCommand("terrainencounters", null)]
		public static void ShowTerrainEncounters()
		{
			Keys keys = Keys.None;
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			Zone zone = The.ZoneManager.GetZone(The.ActiveZone.ZoneWorld);
			List<EncounterEntry> list = new List<EncounterEntry>();
			do
			{
				list.Clear();
				int num = -1;
				int num2 = -1;
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick")
				{
					num = Keyboard.CurrentMouseEvent.x;
					num2 = Keyboard.CurrentMouseEvent.y;
				}
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						int k = 0;
						int num3 = 0;
						int count = zone.Map[j][i].Objects.Count;
						while (k < count)
						{
							TerrainTravel firstPartDescendedFrom = zone.Map[j][i].Objects[k].GetFirstPartDescendedFrom<TerrainTravel>();
							if (firstPartDescendedFrom != null)
							{
								num3 += firstPartDescendedFrom.Encounters.Count;
								scrapBuffer.WriteAt(j, i, (num3 < 10) ? num3.ToString() : "+", true);
								if (num == j && num2 == i)
								{
									list.AddRange(firstPartDescendedFrom.Encounters);
								}
							}
							k++;
						}
					}
				}
				int num4 = ((num2 < 12) ? 23 : 1);
				foreach (EncounterEntry encounterEntry in list)
				{
					ScreenBuffer screenBuffer = scrapBuffer;
					int num5 = 1;
					int num6;
					if (num2 >= 12)
					{
						num4 = (num6 = num4) + 1;
					}
					else
					{
						num4 = (num6 = num4) - 1;
					}
					screenBuffer.WriteAt(num5, num6, "{{W|" + encounterEntry.Text + "}}", true);
				}
				scrapBuffer.Draw();
			}
			while ((keys = Keyboard.getvk(false, false, true)) != Keys.Escape);
		}

		// Token: 0x04003C17 RID: 15383
		public int LostChance = 12;

		// Token: 0x04003C18 RID: 15384
		public int Segments = 50;

		// Token: 0x04003C19 RID: 15385
		public string TravelClass = "";

		// Token: 0x04003C1A RID: 15386
		public List<EncounterEntry> Encounters;
	}
}
