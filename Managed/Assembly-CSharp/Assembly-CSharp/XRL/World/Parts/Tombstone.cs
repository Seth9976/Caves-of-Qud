using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Names;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FBB RID: 4027
	[Serializable]
	public class Tombstone : IPart
	{
		// Token: 0x0600A00E RID: 40974 RVA: 0x003A374A File Offset: 0x003A194A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A00F RID: 40975 RVA: 0x003A374D File Offset: 0x003A194D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A010 RID: 40976 RVA: 0x003A377B File Offset: 0x003A197B
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600A011 RID: 40977 RVA: 0x003A3794 File Offset: 0x003A1994
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Look.ShowLooker(0, currentCell.X, currentCell.Y);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A012 RID: 40978 RVA: 0x003A37D7 File Offset: 0x003A19D7
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.NeedsGeneration)
			{
				this.GenerateTombstone();
			}
			E.Prefix.Append(this.Inscription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A013 RID: 40979 RVA: 0x003A3800 File Offset: 0x003A1A00
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.Render.Tile = "terrain/sw_tombstone_" + Stat.Random(1, 4).ToString() + ".bmp";
			return base.HandleEvent(E);
		}

		// Token: 0x0600A014 RID: 40980 RVA: 0x003A3842 File Offset: 0x003A1A42
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A015 RID: 40981 RVA: 0x003A3848 File Offset: 0x003A1A48
		public void GenerateTombstone()
		{
			if (this.name == null)
			{
				this.name = NameMaker.MakeName(null, null, null, null, null, this.faction, null, null, null, null, null, null, null, false, false, null, null);
			}
			string text = HistoricStringExpander.ExpandString("<spice.tombstones.intro.!random>", null);
			GameObject anonLegendaryCreature = EncountersAPI.GetANonLegendaryCreature(null);
			int num = Stat.Random(0, 850);
			string text2;
			if (num < 100)
			{
				text2 = new string[] { "Stabbed to death", "Shanked", "Gunned down", "Poisoned", "Pushed off a cliff", "Tricked into jumping into a pool of lava", "Drowned in a lake", "Thrown into a pool of acid" }.GetRandomElement<string>(null) + " by " + anonLegendaryCreature.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false);
			}
			else if (num < 200)
			{
				GameObject gameObject = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Item"));
				text2 = new string[] { "Killed in a duel over ", "Succumbed to despair after losing " }.GetRandomElement<string>(null);
				text2 += gameObject.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				gameObject.Obliterate(null, false, null);
			}
			else if (num < 300)
			{
				GameObject gameObject2 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Item"));
				text2 = new string[] { "Crushed to death by a falling ", "Fell from a cliff trying to recover a lost " }.GetRandomElement<string>(null);
				text2 += gameObject2.GetDisplayName(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
				gameObject2.Obliterate(null, false, null);
			}
			else if (num < 400)
			{
				string randomFaction = GenerateFriendOrFoe.getRandomFaction(IComponent<GameObject>.ThePlayer);
				text2 = new string[]
				{
					"Murdered under mysterious circumstances by ", "Eaten alive by ", "Sacrificed by ", "Assassinated after disparaging ", "Killed after cooking a rancid meal for ", "Burned at the stake by ", "Thrown from a cliff by ", " Buried alive by ", "Brained in retaliation for stealing from ", "Shanked in retaliation for stealing from ",
					"Shot in retaliation for stealing from ", "Cooked for sustenance by ", "Mummified by ", "Chopped into small pieces by ", "Covered in molten wax by ", "Drawn and quartered by "
				}.GetRandomElement<string>(null) + Faction.GetFormattedName(randomFaction);
			}
			else if (num < 450)
			{
				GameObject gameObject3 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Food"));
				text2 = new string[] { "Choked on ", "Ate too much ", "Died of malnutrition after eating only " }.GetRandomElement<string>(null);
				if (text2.StartsWith("Choked"))
				{
					text2 += gameObject3.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				}
				else
				{
					text2 += gameObject3.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(false), false, null, false, true);
				}
				gameObject3.Obliterate(null, false, null);
			}
			else if (num < 475)
			{
				GameObject gameObject4 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Tonic"));
				text2 = new string[] { "Swallowed ", "Overdosed on ", "Accidentally ingested " }.GetRandomElement<string>(null) + gameObject4.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				gameObject4.Obliterate(null, false, null);
			}
			else if (num < 500)
			{
				string[] array = new string[] { "Injected one " };
				GameObject gameObject5 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Tonic"));
				string displayName = gameObject5.GetDisplayName(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
				text2 = array.GetRandomElement<string>(null) + displayName + " too many";
				gameObject5.Obliterate(null, false, null);
			}
			else if (num < 525)
			{
				GameObject gameObject6 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Gas"));
				text2 = new string[] { "Choked to death on ", "Suffocated in ", "Breathed too much ", "Released a canister of " }.GetRandomElement<string>(null);
				if (text2.StartsWith("Choked"))
				{
					text2 += gameObject6.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				}
				else
				{
					text2 += gameObject6.GetDisplayName(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
				}
				if (text2.StartsWith("Released"))
				{
					text2 += " in a locked room";
				}
				gameObject6.Obliterate(null, false, null);
			}
			else if (num < 575)
			{
				GameObject gameObject7 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Grenade"));
				text2 = new string[] { "Sat on ", "Swallowed ", "Forgot about ", "Knocked over " }.GetRandomElement<string>(null) + gameObject7.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				gameObject7.Obliterate(null, false, null);
			}
			else if (num < 600)
			{
				GameObject gameObject8 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Book"));
				string[] array2 = new string[] { "Knocked over the head with a copy of ", "Burned at the stake for promulgating " };
				string displayName2 = gameObject8.GetDisplayName(int.MaxValue, null, null, true, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
				text2 = array2.GetRandomElement<string>(null) + displayName2;
				gameObject8.Obliterate(null, false, null);
			}
			else if (num < 625)
			{
				GameObject gameObject9 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("Book"));
				string displayName3 = gameObject9.GetDisplayName(int.MaxValue, null, null, true, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
				text2 = "Became obsessed with " + displayName3 + " and forgot to eat";
				gameObject9.Obliterate(null, false, null);
			}
			else if (num < 675)
			{
				string[] array3 = new string[] { "Burned to death by ", "Immolated in ", "Engulfed in flame by ", "Fell asleep on " };
				GameObject gameObject10 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("LightSource"));
				text2 = array3.GetRandomElement<string>(null) + gameObject10.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				gameObject10.Obliterate(null, false, null);
			}
			else if (num < 700)
			{
				string[] array4 = new string[] { "Drank from a poisoned " };
				GameObject gameObject11 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("WaterContainer"));
				text2 = array4.GetRandomElement<string>(null) + gameObject11.GetDisplayName(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
				gameObject11.Obliterate(null, false, null);
			}
			else if (num < 750)
			{
				string[] array5 = new string[] { "Made too many mocking sounds at " };
				GameObject gameObject12 = GameObject.CreateSample(EncountersAPI.GetARandomDescendantOf("BaseApe"));
				text2 = array5.GetRandomElement<string>(null) + gameObject12.an(int.MaxValue, null, null, 75.in100(), false, false, false, true, false, true, false, false, true, false, null, false, null, false);
				gameObject12.Obliterate(null, false, null);
			}
			else if (num < 800)
			{
				text2 = new string[] { "Died of old age", "Died of natural causes" }.GetRandomElement<string>(null);
			}
			else
			{
				string[] array6 = new string[] { "natural", "unnatural", "mysterious", "metaphysical", "mathematical", "chemical", "ceremonial", "unknown", "magmatic" };
				text2 = "Died of " + array6.GetRandomElement<string>(null) + " causes";
			}
			if (string.IsNullOrEmpty(this.Inscription))
			{
				text2 = Markup.Wrap(text2);
			}
			else
			{
				text2 = Markup.Wrap(this.Inscription);
			}
			List<string> list = new List<string>(16);
			int num2 = 25;
			list.Add("");
			foreach (string text3 in StringFormat.ClipText(text, num2, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text3);
			}
			list.Add("");
			list.Add("");
			foreach (string text4 in StringFormat.ClipText(this.name, num2, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text4);
			}
			list.Add("");
			list.Add("");
			foreach (string text5 in StringFormat.ClipText(text2, num2, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text5);
			}
			list.Add("");
			this.Inscription = Event.NewStringBuilder(null).Append('ÿ').Append('ÿ')
				.Append('ÿ')
				.Append('É')
				.Append('Í', 31)
				.Append('»')
				.ToString();
			for (int j = 0; j < list.Count; j++)
			{
				this.Inscription += "\nÿÿÿ";
				if (j % 2 == 0)
				{
					this.Inscription += "º";
				}
				else
				{
					this.Inscription += "¶";
				}
				int num3 = ColorUtility.LengthExceptFormatting(list[j]);
				this.Inscription += list[j].PadLeft(16 + num3 / 2, 'ÿ').PadRight(31, 'ÿ');
				if (j % 2 == 0)
				{
					this.Inscription += "º";
				}
				else
				{
					this.Inscription += "Ç";
				}
			}
			this.Inscription += Event.NewStringBuilder(null).Append('\n').Append('ÿ')
				.Append('ÿ')
				.Append('ÿ')
				.Append('Ó')
				.Append('Ä', 31)
				.Append('½')
				.ToString();
			this.NeedsGeneration = false;
		}

		// Token: 0x04004286 RID: 17030
		public string Inscription;

		// Token: 0x04004287 RID: 17031
		public bool NeedsGeneration = true;

		// Token: 0x04004288 RID: 17032
		public string faction;

		// Token: 0x04004289 RID: 17033
		public string name;
	}
}
