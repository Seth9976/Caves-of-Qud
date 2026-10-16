using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Genkit;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x02000E16 RID: 3606
	[HasGameBasedStaticCache]
	[HasWishCommand]
	[Serializable]
	public class MarkovBook : IPart
	{
		// Token: 0x060092DE RID: 37598 RVA: 0x0034EDE0 File Offset: 0x0034CFE0
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x0034EDE3 File Offset: 0x0034CFE3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x0034EE09 File Offset: 0x0034D009
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return (E.Actor != The.Player || !this.GetHasBeenRead()) && base.HandleEvent(E);
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x0034EE2C File Offset: 0x0034D02C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, this.GetHasBeenRead() ? 15 : 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x0034EE70 File Offset: 0x0034D070
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read" && E.Actor.IsPlayer())
			{
				if (new Random(this.BookSeed ^ The.Game.GetWorldSeed("FullBookChance")).Next(100) < 1)
				{
					if (this.Pages.IsNullOrEmpty<BookPage>())
					{
						this.GenerateFormattedPage();
					}
					BookUI.ShowBook(this, "Sounds/Interact/sfx_interact_book_read", null, null);
				}
				else
				{
					if (this.Sentence.IsNullOrEmpty())
					{
						this.GenerateFormattedSentence();
					}
					Popup.Show(string.Concat(new string[] { "You read one of the few legible excerpts from {{Y|", this.Title, "}}:\n\n\"", this.Sentence, "\"" }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				AfterReadBookEvent.Send(E.Actor, this.ParentObject, this, E);
				if (!this.GetHasBeenRead())
				{
					this.SetHasBeenRead(true);
					JournalAPI.AddAccomplishment("You read " + this.Title + ".", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", =name= penned the influential book, ",
						this.Title,
						"."
					}), string.Concat(new string[]
					{
						"At a remote library near ",
						JournalAPI.GetLandmarkNearestPlayer().Text,
						", =name= met with a group of blind scribes and together they penned the beloved codex ",
						this.Title,
						"."
					}), null, "general", MuralCategory.CreatesSomething, MuralWeight.VeryLow, null, -1L, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092E3 RID: 37603 RVA: 0x0034F018 File Offset: 0x0034D218
		public string GetBookKey()
		{
			return "AlreadyRead_" + this.BookSeed.ToString();
		}

		// Token: 0x060092E4 RID: 37604 RVA: 0x0034F02F File Offset: 0x0034D22F
		public bool GetHasBeenRead()
		{
			return The.Game.GetStringGameState(this.GetBookKey(), "") == "Yes";
		}

		// Token: 0x060092E5 RID: 37605 RVA: 0x0034F050 File Offset: 0x0034D250
		public void SetHasBeenRead(bool flag)
		{
			if (flag)
			{
				The.Game.SetStringGameState(this.GetBookKey(), "Yes");
				return;
			}
			The.Game.SetStringGameState(this.GetBookKey(), "");
		}

		// Token: 0x060092E6 RID: 37606 RVA: 0x0034F080 File Offset: 0x0034D280
		public static void EnsureCorpusLoaded(string Corpus)
		{
			try
			{
				MarkovChainData markovChainData;
				if (!MarkovBook.CorpusData.TryGetValue(Corpus, out markovChainData))
				{
					markovChainData = MarkovChainData.LoadFromFile(DataManager.FilePath(Corpus));
					MarkovBook.CorpusData.Add(Corpus, markovChainData);
					MarkovBook.PostprocessLoadedCorpus(markovChainData);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("EnsureCorpusLoaded:" + Corpus, ex, "game_exception");
			}
		}

		// Token: 0x060092E7 RID: 37607 RVA: 0x0034F0E8 File Offset: 0x0034D2E8
		public static void PostprocessLoadedCorpus(MarkovChainData Corpus)
		{
			if (The.Game == null)
			{
				return;
			}
			string[] array = new string[6];
			for (int i = 0; i < 5; i++)
			{
				string text = "*Sultan" + (i + 1).ToString() + "Name*";
				array[i] = The.Game.GetStringGameState(text, "");
				Corpus.Replace(text, array[i]);
			}
			array[5] = "Resheph";
			Corpus.Replace("*sultan*", array);
			int num;
			int num2;
			if (The.Game.IntGameState.TryGetValue("RuinofHouseIsner_xCoordinate", out num) && The.Game.IntGameState.TryGetValue("RuinofHouseIsner_yCoordinate", out num2))
			{
				Stat.ReseedFrom("HouseIsnerLore", false);
				for (int j = 0; j < 46; j++)
				{
					string text2 = LoreGenerator.RuinOfHouseIsnerLore(num, num2);
					MarkovChain.AppendSecret(Corpus, text2, false);
				}
			}
		}

		// Token: 0x060092E8 RID: 37608 RVA: 0x0034F1C0 File Offset: 0x0034D3C0
		public void SetContents(int Seed, string Corpus)
		{
			this.BookSeed = Seed;
			this.BookCorpus = Corpus;
			MarkovBook.EnsureCorpusLoaded(Corpus);
			Stat.ReseedFrom(this.BookSeed + Hash.String("Title"), false);
			this.Title = MarkovChain.GenerateTitle(MarkovBook.CorpusData[Corpus]);
			if (20.in100())
			{
				if (50.in100())
				{
					string[] array = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
					this.Title = this.Title + ", Vol. " + array.GetRandomElement<string>(null);
				}
				else if (30.in100())
				{
					this.Title += ": Unabridged";
				}
				else
				{
					int num = ((Stat.Random(1, 10) == 1) ? Stat.Random(1, 100) : Stat.Random(1, 5));
					switch ((num == 11 || num == 12) ? 0 : (num % 10))
					{
					case 1:
						this.Title = this.Title + ", " + num.ToString() + "st Edition";
						break;
					case 2:
						this.Title = this.Title + ", " + num.ToString() + "nd Edition";
						break;
					case 3:
						this.Title = this.Title + ", " + num.ToString() + "rd Edition";
						break;
					default:
						this.Title = this.Title + ", " + num.ToString() + "th Edition";
						break;
					}
				}
			}
			this.ParentObject.Render.DisplayName = this.Title;
		}

		// Token: 0x060092E9 RID: 37609 RVA: 0x0034F3AC File Offset: 0x0034D5AC
		public void GeneratePages()
		{
			this.Pages = new List<BookPage>();
			MarkovBook.EnsureCorpusLoaded(this.BookCorpus);
			Stat.ReseedFrom(this.BookSeed + Hash.String("Pages"), false);
			StringBuilder stringBuilder = new StringBuilder();
			if (10.in100())
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				string text = stringBuilder.ToString();
				BookPage bookPage = new BookPage(this.Title, text);
				bookPage.TopMargin = 2;
				bookPage.LeftMargin = 2;
				bookPage.RightMargin = 2;
				this.Pages.Add(bookPage);
			}
			string text2 = "\n";
			BookPage bookPage2 = new BookPage(this.Title, text2);
			bookPage2.TopMargin = 2;
			bookPage2.LeftMargin = 2;
			bookPage2.RightMargin = 2;
			this.Pages.Add(bookPage2);
			int num = Stat.Random(5, 7);
			for (int i = 0; i < num; i++)
			{
				string text3;
				do
				{
					text3 = MarkovChain.GenerateParagraph(MarkovBook.CorpusData[this.BookCorpus]).Replace("\n", " ").Replace("  ", " ")
						.Trim();
				}
				while (text3.Contains("="));
				BookPage bookPage3 = new BookPage(this.Title, text3);
				bookPage3.TopMargin = 2;
				bookPage3.LeftMargin = 2;
				bookPage3.RightMargin = 2;
				this.Pages.Add(bookPage3);
			}
		}

		// Token: 0x060092EA RID: 37610 RVA: 0x0034F510 File Offset: 0x0034D710
		public void GenerateFormattedPage()
		{
			this.Pages = new List<BookPage>();
			MarkovBook.EnsureCorpusLoaded(this.BookCorpus);
			Stat.ReseedFrom(this.BookSeed + Hash.String("Pages"), false);
			string text = "";
			if (10.in100())
			{
				if (50.in100())
				{
					text = text + "{{C|Editor's note:}} " + MarkovChain.GenerateSentence(MarkovBook.CorpusData[this.BookCorpus], null) + "\n\n";
				}
				else
				{
					text = text + "{{C|Author's note:}} " + MarkovChain.GenerateSentence(MarkovBook.CorpusData[this.BookCorpus], null) + "\n\n";
				}
			}
			int i = 0;
			int num = Stat.Random(8, 30);
			while (i < num)
			{
				text += MarkovChain.GenerateParagraph(MarkovBook.CorpusData[this.BookCorpus]);
				i++;
			}
			if (5.in100())
			{
				int j = 0;
				int num2 = Stat.Random(150, 450);
				while (j < num2)
				{
					text += MarkovChain.GenerateParagraph(MarkovBook.CorpusData[this.BookCorpus]);
					j++;
				}
			}
			string text2 = "Auto";
			string text3 = "1,2,2,2";
			this.Pages.AddRange(BookUI.AutoformatPages(this.Title, text, text2, text3));
			int count = Regex.Matches(text, "parasangs").Count;
			text.Split(' ', StringSplitOptions.None);
		}

		// Token: 0x060092EB RID: 37611 RVA: 0x0034F66C File Offset: 0x0034D86C
		public void GenerateFormattedSentence()
		{
			MarkovBook.EnsureCorpusLoaded(this.BookCorpus);
			Stat.ReseedFrom(this.BookSeed + Hash.String("Sentence"), false);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(MarkovChain.GenerateSentence(MarkovBook.CorpusData[this.BookCorpus], null));
			stringBuilder.TrimEnd(' ');
			this.Sentence = Event.FinalizeString(stringBuilder);
		}

		// Token: 0x060092EC RID: 37612 RVA: 0x0034F6D4 File Offset: 0x0034D8D4
		[WishCommand("markovsultansentence", null)]
		public static void WishSultanMarkov()
		{
			string[] array = new string[5];
			for (int i = 0; i < 5; i++)
			{
				string text = "*Sultan" + (i + 1).ToString() + "Name*";
				array[i] = The.Game.GetStringGameState(text, "");
			}
			for (int j = 0; j < 10000; j++)
			{
				string sentence = GameText.GenerateMarkovMessageSentence();
				if (array.Any<string>((string x) => sentence.Contains(x)))
				{
					Popup.Show(sentence, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
			}
		}

		// Token: 0x04003CC8 RID: 15560
		public const int FULL_BOOK_CHANCE = 1;

		// Token: 0x04003CC9 RID: 15561
		public const int NOTE_CHANCE = 10;

		// Token: 0x04003CCA RID: 15562
		public int BookSeed;

		// Token: 0x04003CCB RID: 15563
		public string BookCorpus;

		// Token: 0x04003CCC RID: 15564
		public string Title;

		// Token: 0x04003CCD RID: 15565
		public const int NUMBER_ISNER_SECRETS = 46;

		// Token: 0x04003CCE RID: 15566
		[NonSerialized]
		public List<BookPage> Pages;

		// Token: 0x04003CCF RID: 15567
		[NonSerialized]
		public string Sentence;

		// Token: 0x04003CD0 RID: 15568
		[GameBasedStaticCache(true, false)]
		[NonSerialized]
		public static Dictionary<string, MarkovChainData> CorpusData = new Dictionary<string, MarkovChainData>();
	}
}
