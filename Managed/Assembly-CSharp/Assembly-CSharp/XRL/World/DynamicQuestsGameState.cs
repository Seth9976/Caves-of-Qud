using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Core;
using XRL.UI;
using XRL.Wish;
using XRL.World.WorldBuilders;
using XRL.World.ZoneBuilders;

namespace XRL.World
{
	// Token: 0x0200092D RID: 2349
	[GameStateSingleton]
	[HasWishCommand]
	[Serializable]
	public class DynamicQuestsGameState : IGameStateSingleton, IComposite
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x001B4CD5 File Offset: 0x001B2ED5
		public static DynamicQuestsGameState Instance
		{
			get
			{
				return XRLCore.Core.Game.RequireGameState<DynamicQuestsGameState>("DynamicQuestsGameState", () => new DynamicQuestsGameState());
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x001B4D0A File Offset: 0x001B2F0A
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x001B4D10 File Offset: 0x001B2F10
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.NextID);
			Writer.WriteOptimized(this.Quests.Count);
			foreach (KeyValuePair<string, Quest> keyValuePair in this.Quests)
			{
				Writer.WriteOptimized(keyValuePair.Key);
				keyValuePair.Value.Save(Writer);
			}
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x001B4D94 File Offset: 0x001B2F94
		public void Read(SerializationReader Reader)
		{
			this.NextID = Reader.ReadOptimizedInt64();
			int num = Reader.ReadOptimizedInt32();
			for (int i = 0; i < num; i++)
			{
				this.Quests.Add(Reader.ReadOptimizedString(), Quest.Load(Reader));
			}
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x001B4DD8 File Offset: 0x001B2FD8
		public void LegacyRead(SerializationReader reader)
		{
			this.NextID = reader.ReadInt64();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				this.Quests.Add(reader.ReadString(), Quest.Load(reader));
			}
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x001B4E1B File Offset: 0x001B301B
		public static void Add(Quest Quest)
		{
			DynamicQuestsGameState.Instance.Quests.Add(Quest.ID, Quest);
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x001B4E34 File Offset: 0x001B3034
		[WishCommand("godynamicquest", null)]
		public static void Wish(string Param)
		{
			Popup.Suppress = true;
			try
			{
				string text;
				string text2;
				Param.Split(':', out text, out text2);
				if (text2.IsNullOrEmpty() || text2.Contains("giver", StringComparison.OrdinalIgnoreCase))
				{
					DynamicQuestsGameState.FindVillageQuest(text);
				}
				else if (text2.Contains("target", StringComparison.OrdinalIgnoreCase))
				{
					DynamicQuestsGameState.FindQuestTarget(text);
				}
			}
			finally
			{
				Popup.Suppress = false;
			}
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x001B4EA0 File Offset: 0x001B30A0
		private static void FindQuestTarget(string Type)
		{
			foreach (KeyValuePair<string, Quest> keyValuePair in DynamicQuestsGameState.Instance.Quests)
			{
				string text;
				Quest quest;
				keyValuePair.Deconstruct(out text, out quest);
				Quest quest2 = quest;
				Quest quest3;
				if (The.Game.Quests.TryGetValue(quest2.ID, out quest3))
				{
					if (!quest3.StepsByID.Any<KeyValuePair<string, QuestStep>>((KeyValuePair<string, QuestStep> x) => x.Value.Finished))
					{
						QuestManager manager = quest2.Manager;
						FindASiteDynamicQuestManager findASiteDynamicQuestManager = manager as FindASiteDynamicQuestManager;
						if (findASiteDynamicQuestManager != null && Type.Contains("site", StringComparison.OrdinalIgnoreCase))
						{
							Cell randomElement = The.ZoneManager.GetZone(findASiteDynamicQuestManager._zoneID).GetEmptyCells(null, null).GetRandomElement<Cell>(null);
							The.Player.DirectMoveTo(randomElement, 0, true, false, false, null);
							return;
						}
						if ((manager is FindASpecificItemDynamicQuestManager && Type.Contains("item", StringComparison.OrdinalIgnoreCase)) || (manager is InteractWithAnObjectDynamicQuestManager && Type.Contains("interact", StringComparison.OrdinalIgnoreCase)))
						{
							FindASpecificItemDynamicQuestManager findASpecificItemDynamicQuestManager = manager as FindASpecificItemDynamicQuestManager;
							string text2;
							if ((text2 = ((findASpecificItemDynamicQuestManager != null) ? findASpecificItemDynamicQuestManager._itemID : null)) == null)
							{
								InteractWithAnObjectDynamicQuestManager interactWithAnObjectDynamicQuestManager = manager as InteractWithAnObjectDynamicQuestManager;
								text2 = ((interactWithAnObjectDynamicQuestManager != null) ? interactWithAnObjectDynamicQuestManager._itemID : null);
							}
							string text3 = text2;
							foreach (KeyValuePair<string, ZoneBuilderCollection> keyValuePair2 in The.ZoneManager.ZoneBuilders)
							{
								ZoneBuilderCollection zoneBuilderCollection;
								keyValuePair2.Deconstruct(out text, out zoneBuilderCollection);
								string text4 = text;
								ZoneBuilderCollection zoneBuilderCollection2 = zoneBuilderCollection;
								int i = 0;
								int count = zoneBuilderCollection2.Count;
								while (i < count)
								{
									ZoneBuilderBlueprint zoneBuilderBlueprint = zoneBuilderCollection2[i];
									if (zoneBuilderBlueprint.Class.EndsWith("FabricateQuestItem"))
									{
										string text5 = (string)zoneBuilderBlueprint.GetParameter("deliveryItemID", null);
										if (text5 == text3)
										{
											GameObject gameObject = The.ZoneManager.GetZone(text4).FindObjectByID(text5);
											The.Player.DirectMoveTo(gameObject.GetCurrentCell().GetFirstAdjacentCell((Cell x) => x.IsEmptyFor(The.Player)) ?? gameObject.CurrentCell, 0, true, false, false, null);
											return;
										}
									}
									i++;
								}
							}
						}
					}
				}
			}
			Popup.Show("Matching quest target not found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x001B514C File Offset: 0x001B334C
		private static void FindVillageQuest(string Type)
		{
			foreach (GeneratedLocationInfo generatedLocationInfo in ((WorldInfo)The.Game.GetObjectGameState("JoppaWorldInfo")).villages)
			{
				string targetZone = generatedLocationInfo.targetZone;
				if (!The.ZoneManager.IsZoneBuilt(targetZone))
				{
					ZoneBuilderCollection builderCollection = The.ZoneManager.GetBuilderCollection(targetZone);
					if (builderCollection != null)
					{
						int i = 0;
						int count = builderCollection.Count;
						while (i < count)
						{
							ZoneBuilderBlueprint zoneBuilderBlueprint = builderCollection[i];
							if (zoneBuilderBlueprint.Class.EndsWith("FabricateQuestGiver") && ((zoneBuilderBlueprint.Class.StartsWith("FindASpecificSite") && Type.Contains("site", StringComparison.OrdinalIgnoreCase)) || (zoneBuilderBlueprint.Class.StartsWith("FindASpecificItem") && Type.Contains("item", StringComparison.OrdinalIgnoreCase)) || (zoneBuilderBlueprint.Class.StartsWith("InteractWithAnObject") && Type.Contains("interact", StringComparison.OrdinalIgnoreCase))))
							{
								DynamicQuestsGameState.GoToQuestGiver(targetZone, Type);
								return;
							}
							i++;
						}
					}
				}
			}
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x001B527C File Offset: 0x001B347C
		private static void GoToQuestGiver(string ZoneID, string Type)
		{
			foreach (GameObject gameObject in The.ZoneManager.GetZone(ZoneID).IterateObjects())
			{
				if (gameObject.Brain != null)
				{
					string stringProperty = gameObject.GetStringProperty("GivesDynamicQuest", null);
					Quest quest;
					if (!stringProperty.IsNullOrEmpty() && DynamicQuestsGameState.Instance.Quests.TryGetValue(stringProperty, out quest))
					{
						QuestManager manager = quest.Manager;
						if ((manager is FindASiteDynamicQuestManager && Type.Contains("site", StringComparison.OrdinalIgnoreCase)) || (manager is FindASpecificItemDynamicQuestManager && Type.Contains("item", StringComparison.OrdinalIgnoreCase)) || (manager is InteractWithAnObjectDynamicQuestManager && Type.Contains("interact", StringComparison.OrdinalIgnoreCase)))
						{
							Cell cell = gameObject.CurrentCell.GetFirstAdjacentCell((Cell x) => x.IsEmptyFor(The.Player)) ?? gameObject.CurrentCell;
							The.Player.DirectMoveTo(cell, 0, true, true, false, null);
							return;
						}
					}
				}
			}
		}

		// Token: 0x04002C9C RID: 11420
		public long NextID;

		// Token: 0x04002C9D RID: 11421
		public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();
	}
}
