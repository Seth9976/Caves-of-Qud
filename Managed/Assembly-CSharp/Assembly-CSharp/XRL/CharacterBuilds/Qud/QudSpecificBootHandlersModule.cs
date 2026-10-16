using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Annals;
using XRL.UI;
using XRL.World;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Encounters;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.CharacterBuilds.Qud
{
	/// <summary>
	/// Contains all the caves of qud specific boot handler logic
	/// </summary>
	// Token: 0x020005AE RID: 1454
	public class QudSpecificBootHandlersModule : AbstractEmbarkBuilderModule
	{
		// Token: 0x06003DEF RID: 15855 RVA: 0x0013005A File Offset: 0x0012E25A
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x0013005C File Offset: 0x0012E25C
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_AFTERINITIALIZEWORLDS)
			{
				The.Core.CreateCures();
				The.Core.CreateMarkOfDeath();
				DimensionManager.Init();
				BookUI.InitBooks(false);
			}
			else if (id == QudGameBootModule.BOOTEVENT_INITIALIZEHISTORY)
			{
				game.sultanHistory = QudHistoryFactory.GenerateNewSultanHistory();
				game.sultanHistory = QudHistoryFactory.GenerateVillageEraHistory(game.sultanHistory);
				game.sultanHistory = info.fireBootEvent<History>(QudGameBootModule.BOOTEVENT_INITIALIZESULTANHISTORY, game, game.sultanHistory);
				game.sultanHistory = info.fireBootEvent<History>(QudGameBootModule.BOOTEVENT_AFTERINITIALIZESULTANHISTORY, game, game.sultanHistory);
			}
			else if (id == QudGameBootModule.BOOTEVENT_INITIALIZESYSTEMS)
			{
				game.AddSystem(new AmbientSoundsSystem(), true);
				game.AddSystem(new CheckpointingSystem(), true);
				game.AddSystem(new WanderSystem(), true);
				game.AddSystem(new HolyPlaceSystem(), true);
				game.AddSystem(new PsychicHunterSystem(), true);
				game.AddSystem(new SvardymSystem(), true);
				game.AddSystem(new ChavvahSystem(), true);
				game.AddSystem(new NorthShevaSystem(), true);
			}
			else if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				GameObject gameObject = (GameObject)element;
				Body body = gameObject.Body;
				if (body != null)
				{
					BodyPart randomElement = body.GetPart("Hand").GetRandomElement<BodyPart>(null);
					if (randomElement != null)
					{
						randomElement.SetAsPreferredDefault(true);
					}
				}
				gameObject.Brain.PerformEquip(true, false, null);
				gameObject.Brain.DoReequip = false;
			}
			else if (id == QudGameBootModule.BOOTEVENT_GAMESTARTING)
			{
				try
				{
					The.Player.SetStringProperty("OriginalPlayerBody", "1", false);
					Event @event = Event.New("GameStart", 0, 0, 0);
					The.Player.FireEvent(@event);
					The.Player.Body.FireEventOnBodyparts(@event);
					foreach (GameObject gameObject2 in The.Player.Inventory.GetObjectsDirect())
					{
						gameObject2.FireEvent(@event);
					}
					QudCustomizeCharacterModuleData data = info.getData<QudCustomizeCharacterModuleData>();
					string text = ((data != null) ? data.pet : null);
					if (!string.IsNullOrEmpty(text))
					{
						GameObject gameObject3 = GameObject.Create(text);
						Cell playerCell = The.PlayerCell;
						gameObject3.SetIntProperty("NoXP", 1, false);
						gameObject3.SetAlliedLeader<AllyPet>(The.Player, 0, false);
						gameObject3.MakeActive(false);
						Brain brain = gameObject3.Brain;
						if (brain != null)
						{
							brain.GoToPartyLeader();
						}
						if (gameObject3.CurrentCell == null)
						{
							Cell cell;
							if ((cell = playerCell.GetFirstEmptyAdjacentCell(1, 1)) == null && (cell = playerCell.GetFirstEmptyAdjacentCell(1, 2)) == null && (cell = playerCell.GetFirstEmptyAdjacentCell(1, 3)) == null && (cell = playerCell.GetFirstEmptyAdjacentCell(1, 4)) == null)
							{
								cell = playerCell.GetRandomLocalAdjacentCell() ?? playerCell;
							}
							Cell cell2 = cell;
							gameObject3.SystemLongDistanceMoveTo(cell2, null, false, true);
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Error spawning pet: " + ex.ToString());
				}
				GameObject player = The.Player;
				bool flag;
				if (player == null)
				{
					flag = false;
				}
				else
				{
					Mutations part = player.GetPart<Mutations>();
					int? num;
					if (part == null)
					{
						num = null;
					}
					else
					{
						List<BaseMutation> mutationList = part.MutationList;
						num = ((mutationList != null) ? new int?(mutationList.Count) : null);
					}
					int? num2 = num;
					int num3 = 10;
					flag = (num2.GetValueOrDefault() >= num3) & (num2 != null);
				}
				if (flag)
				{
					Achievement.HAVE_10_MUTATIONS.Unlock();
				}
				Popup.Show("You embark for the caves of Qud.", null, "Sounds/Misc/sfx_newLocation_discovered", true, true, true, true, null);
			}
			return base.handleBootEvent(id, game, info, element);
		}
	}
}
