using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B3A RID: 2874
	public static class TinkeringHelpers
	{
		// Token: 0x06007421 RID: 29729 RVA: 0x002BDAA4 File Offset: 0x002BBCA4
		public static string TinkeredItemInventoryCategory(string Blueprint)
		{
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			TinkeringHelpers.StripForTinkering(gameObject);
			TinkeringHelpers.ForceToBePowered(gameObject);
			if (gameObject.HasPart<Examiner>())
			{
				gameObject.GetPart<Examiner>().EpistemicStatus = 2;
			}
			string inventoryCategory = gameObject.GetInventoryCategory(true);
			gameObject.Obliterate(null, false, null);
			return inventoryCategory;
		}

		// Token: 0x06007422 RID: 29730 RVA: 0x002BDAEC File Offset: 0x002BBCEC
		public static string TinkeredItemDisplayName(string Blueprint)
		{
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			TinkeringHelpers.StripForTinkering(gameObject);
			TinkeringHelpers.ForceToBePowered(gameObject);
			string referenceDisplayName = gameObject.GetReferenceDisplayName(int.MaxValue, null, "Tinkering", false, false, false, false, false, false, false, false, null, false);
			gameObject.Obliterate(null, false, null);
			return referenceDisplayName;
		}

		// Token: 0x06007423 RID: 29731 RVA: 0x002BDB34 File Offset: 0x002BBD34
		public static string TinkeredItemShortDisplayName(string Blueprint)
		{
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			TinkeringHelpers.StripForTinkering(gameObject);
			TinkeringHelpers.ForceToBePowered(gameObject);
			string referenceDisplayName = gameObject.GetReferenceDisplayName(int.MaxValue, null, "Tinkering", false, false, false, false, true, false, false, false, null, false);
			gameObject.Obliterate(null, false, null);
			return referenceDisplayName;
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x002BDB79 File Offset: 0x002BBD79
		public static void ProcessTinkeredItem(GameObject Object, GameObject Actor)
		{
			TinkeringHelpers.StripForTinkering(Object);
			Object.MakeUnderstood(false);
			Object.SetIntProperty("TinkeredItem", 1, false);
			TinkeringHelpers.CheckMakersMark(Object, Actor, null, "Tinkering");
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x002BDBA4 File Offset: 0x002BBDA4
		public static bool EligibleForMakersMark(GameObject Object)
		{
			return GameObject.Validate(ref Object) && !Object.HasTag("AlwaysStack") && Object.HasPart<Description>() && !TinkeringHelpers.ConsiderStandardScrap(Object);
		}

		// Token: 0x06007426 RID: 29734 RVA: 0x002BDBD8 File Offset: 0x002BBDD8
		public static void CheckMakersMark(GameObject Object, GameObject Actor, IModification ModAdded = null, string Context = null)
		{
			if (!TinkeringHelpers.EligibleForMakersMark(Object))
			{
				return;
			}
			if (!GameObject.Validate(ref Actor))
			{
				return;
			}
			if (GlobalConfig.GetBoolSetting("DynamicMakersMarks", false) && !(Actor.IsPlayer() ? The.Game.GetBooleanGameState("PlayerMakersMarkDone", false) : Actor.HasPart<HasMakersMark>()) && TriggersMakersMarkCreationEvent.Check(Object, Actor, ModAdded, Context))
			{
				if (Actor.IsPlayer())
				{
					List<string> usable = MakersMark.GetUsable(true, true);
					usable.Insert(0, "none");
					char[] array = new char[usable.Count];
					char c = 'a';
					int i = 0;
					int count = usable.Count;
					while (i < count)
					{
						char[] array2 = array;
						int num = i;
						int num2;
						if (c > 'z')
						{
							num2 = 32;
						}
						else
						{
							c = (char)((num2 = (int)c) + 1);
						}
						array2[num] = num2;
						i++;
					}
					int num3 = Popup.PickOption("Select your maker's mark.", null, "", "Sounds/UI/ui_notification", usable.ToArray(), array, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
					The.Game.SetBooleanGameState("PlayerMakersMarkDone", true);
					if (num3 > 0)
					{
						string text = usable[num3];
						The.Game.SetStringGameState("PlayerMakersMark", text);
						MakersMark.RecordUsage(text);
						string text2 = Popup.ShowColorPicker("Choose a color for your maker's mark.", 0, null, 60, false, false, "R", "", false, false, false, text);
						The.Game.SetStringGameState("PlayerMakersMarkColor", text2);
					}
				}
				else
				{
					HasMakersMark hasMakersMark = Actor.RequirePart<HasMakersMark>(false);
					hasMakersMark.Mark = MakersMark.Generate(true);
					hasMakersMark.Color = Crayons.GetRandomColor(null);
				}
			}
			HasMakersMark part = Actor.GetPart<HasMakersMark>();
			string text3 = ((part != null) ? part.Mark : null);
			string text4 = ((part != null) ? part.Color : null);
			if (Actor.IsPlayer())
			{
				text3 = The.Game.GetStringGameState("PlayerMakersMark", text3);
				text4 = The.Game.GetStringGameState("PlayerMakersMarkColor", text4);
			}
			if (!text3.IsNullOrEmpty())
			{
				Object.RequirePart<MakersMark>(false).AddCrafter(Actor, text3, text4 ?? "R");
			}
		}

		// Token: 0x06007427 RID: 29735 RVA: 0x002BDDC0 File Offset: 0x002BBFC0
		public static void StripForTinkering(GameObject Object)
		{
			EnergyCellSocket energyCellSocket;
			if (Object.TryGetPart<EnergyCellSocket>(out energyCellSocket) && energyCellSocket.Cell != null)
			{
				GameObject cell = energyCellSocket.Cell;
				CellChangedEvent.Send(null, Object, cell, null);
				energyCellSocket.Cell = null;
				cell.Obliterate(null, false, null);
			}
			MagazineAmmoLoader magazineAmmoLoader;
			if (Object.TryGetPart<MagazineAmmoLoader>(out magazineAmmoLoader))
			{
				magazineAmmoLoader.Ammo = null;
			}
			Object.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
			{
				P.TinkerInitialize();
			});
			LiquidVolume liquidVolume = Object.LiquidVolume;
			if (liquidVolume == null)
			{
				return;
			}
			liquidVolume.Empty(false, false);
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x002BDE48 File Offset: 0x002BC048
		public static void ForceToBePowered(GameObject Object)
		{
			int num = QueryDrawEvent.GetFor(Object) * 2;
			if (num < 1000)
			{
				num = 1000;
			}
			FreePower freePower = Object.RequirePart<FreePower>(false);
			if (freePower.ChargeFulfilled < num)
			{
				freePower.ChargeFulfilled = num;
			}
			Gaslight part = Object.GetPart<Gaslight>();
			if (part == null)
			{
				return;
			}
			part.SyncActiveState();
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x002BDE94 File Offset: 0x002BC094
		public static bool ConsiderStandardScrap(GameObject Object)
		{
			return Object.HasTagOrProperty("Scrap");
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x002BDEA1 File Offset: 0x002BC0A1
		public static bool ConsiderScrap(GameObject Object, GameObject Actor = null)
		{
			return TinkeringHelpers.ConsiderStandardScrap(Object) || (Actor != null && Tinkering_Disassemble.CheckScrapToggle(Object));
		}

		// Token: 0x0600742B RID: 29739 RVA: 0x002BDEB8 File Offset: 0x002BC0B8
		public static bool CanBeDisassembled(GameObject Object, GameObject Actor = null)
		{
			TinkerItem tinkerItem;
			return Object.TryGetPart<TinkerItem>(out tinkerItem) && tinkerItem.CanBeDisassembled(Actor);
		}
	}
}
