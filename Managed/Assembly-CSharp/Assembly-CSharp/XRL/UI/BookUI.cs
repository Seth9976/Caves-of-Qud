using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014B7 RID: 5303
	[HasGameBasedStaticCache]
	[HasModSensitiveStaticCache]
	[UIView("BookUI", false, false, false, null, null, false, 0, false)]
	public class BookUI : IWantsTextConsoleInit
	{
		// Token: 0x0600D032 RID: 53298 RVA: 0x004CC7D0 File Offset: 0x004CA9D0
		[GameBasedCacheInit]
		public static void Reset()
		{
			foreach (string text in BookUI.DynamicBooks)
			{
				BookUI.Books.Remove(text);
			}
			foreach (KeyValuePair<string, BookInfo> keyValuePair in BookUI.Books)
			{
				keyValuePair.Value.Reset();
			}
			BookUI.DynamicBooks.Clear();
		}

		// Token: 0x0600D033 RID: 53299 RVA: 0x004CC878 File Offset: 0x004CAA78
		[ModSensitiveCacheInit]
		public static void CacheInit()
		{
			BookUI.Books.Clear();
			BookUI.BookCorpus.Clear();
		}

		// Token: 0x0600D034 RID: 53300 RVA: 0x004CC88E File Offset: 0x004CAA8E
		public void Init(TextConsole _Console, ScreenBuffer _Buffer)
		{
			BookUI.Console = _Console;
			BookUI.Buffer = _Buffer;
			BookUI.InitBooks(false);
		}

		// Token: 0x0600D035 RID: 53301 RVA: 0x004CC8A2 File Offset: 0x004CAAA2
		public static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(BookUI._Nodes, null);
		}

		// Token: 0x0600D036 RID: 53302 RVA: 0x004CC8B0 File Offset: 0x004CAAB0
		public static void InitBooks(bool bIncludeCorpusData = false)
		{
			if (!BookUI.Books.IsNullOrEmpty<KeyValuePair<string, BookInfo>>())
			{
				if (BookUI.IncludeCorpusData == bIncludeCorpusData)
				{
					BookUI.Reset();
					return;
				}
				BookUI.Books.Clear();
				BookUI.CorpusBuilder.Clear();
			}
			BookUI.IncludeCorpusData = bIncludeCorpusData;
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("books", true, true))
			{
				xmlDataHelper.HandleNodes(BookUI._Nodes, null);
			}
		}

		// Token: 0x0600D037 RID: 53303 RVA: 0x004CC93C File Offset: 0x004CAB3C
		public static void HandlePageNode(XmlDataHelper Reader)
		{
			string text = Markup.Transform(Reader.ReadString(), true);
			if (BookUI.IncludeCorpusData)
			{
				BookUI.CorpusBuilder.Append(text).Append('\n');
			}
			text = text.Replace("[[", "").Replace("]]", "");
			BookUI.CurrentBook.Texts.Add(text);
			Reader.DoneWithElement();
		}

		// Token: 0x0600D038 RID: 53304 RVA: 0x004CC9AC File Offset: 0x004CABAC
		public static void ParseMargins(string Margins, ref int Top, ref int Right, ref int Bottom, ref int Left)
		{
			if (Margins.IsNullOrEmpty())
			{
				return;
			}
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			ReadOnlySpan<char> readOnlySpan3;
			ReadOnlySpan<char> readOnlySpan4;
			Margins.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2, out readOnlySpan3, out readOnlySpan4);
			int num;
			if (int.TryParse(readOnlySpan, out num))
			{
				Top = num;
			}
			if (int.TryParse(readOnlySpan2, out num))
			{
				Right = num;
			}
			if (int.TryParse(readOnlySpan3, out num))
			{
				Bottom = num;
			}
			if (int.TryParse(readOnlySpan4, out num))
			{
				Left = num;
			}
		}

		// Token: 0x0600D039 RID: 53305 RVA: 0x004CCA0C File Offset: 0x004CAC0C
		public static void HandleBookNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("ID");
			BookInfo bookInfo;
			if (!BookUI.Books.TryGetValue(attribute, out bookInfo))
			{
				bookInfo = (BookUI.Books[attribute] = new BookInfo());
				bookInfo.Texts = new List<string>();
			}
			else
			{
				bookInfo.Texts.Clear();
			}
			BookUI.CurrentBook = bookInfo;
			bookInfo.Title = Reader.GetAttribute("Title");
			bookInfo.Format = Reader.GetAttribute("Format");
			BookUI.ParseMargins(Reader.GetAttribute("Margins"), ref bookInfo.TopMargin, ref bookInfo.RightMargin, ref bookInfo.BottomMargin, ref bookInfo.LeftMargin);
			BookUI.CorpusBuilder.Clear();
			Reader.HandleNodes(BookUI._BookNodes, null);
			if (BookUI.IncludeCorpusData)
			{
				Match match = Regex.Match(BookUI.CorpusBuilder.ToString(), "\\[\\[.*?\\]\\]");
				while (match != null && !string.IsNullOrEmpty(match.Value))
				{
					BookUI.CorpusBuilder.Replace("[[" + match.Groups[0].Value + "]]", "");
					string text = "Removing book header from corpus: ";
					Group group = match.Groups[0];
					Debug.Log(text + ((group != null) ? group.ToString() : null) + "...");
					match = match.NextMatch();
				}
				BookUI.BookCorpus[attribute] = GameText.VariableReplace(BookUI.CorpusBuilder, false);
			}
		}

		// Token: 0x0600D03A RID: 53306 RVA: 0x004CCB70 File Offset: 0x004CAD70
		public static int NextWordLength(string Text, int Pos, StringBuilder Line, int LineWidth)
		{
			int num = 0;
			while (Pos < Text.Length)
			{
				if (Text[Pos] == ' ')
				{
					return num;
				}
				if (Text[Pos] == '\n')
				{
					return num;
				}
				if (Text[Pos] == '&' || Text[Pos] == '^')
				{
					Line.Append(Text[Pos]);
					Pos++;
					Line.Append(Text[Pos]);
				}
				else
				{
					num++;
					Line.Append(Text[Pos]);
				}
				Pos++;
			}
			return num;
		}

		// Token: 0x0600D03B RID: 53307 RVA: 0x004CCBF8 File Offset: 0x004CADF8
		public static bool NextLine(string Text, ref int Pos, StringBuilder Line, int LineWidth)
		{
			int num = 0;
			while (Pos < Text.Length)
			{
				if (Text[Pos] == '&' || Text[Pos] == '^')
				{
					Line.Append(Text[Pos]);
					Pos++;
					Line.Append(Text[Pos]);
				}
				else
				{
					num++;
					Line.Append(Text[Pos]);
				}
				Pos++;
				if (Pos < Text.Length)
				{
					if (Text[Pos] == '\n')
					{
						Line.Append('\n');
						Pos++;
						break;
					}
					if (num + BookUI.NextWordLength(Text, Pos, Line, LineWidth) >= LineWidth)
					{
						Line.Append('\n');
						break;
					}
				}
			}
			return Pos < Text.Length;
		}

		// Token: 0x0600D03C RID: 53308 RVA: 0x004CCCC0 File Offset: 0x004CAEC0
		public static List<BookPage> AutoformatPages(string Title, string Text, string Format, string Margins)
		{
			int num = 2;
			int num2 = 2;
			int num3 = 2;
			int num4 = 2;
			BookUI.ParseMargins(Margins, ref num, ref num2, ref num3, ref num4);
			return BookUI.AutoformatPages(Title, Text, Format, num4, num2, num, num3);
		}

		// Token: 0x0600D03D RID: 53309 RVA: 0x004CCCF0 File Offset: 0x004CAEF0
		public static List<BookPage> AutoformatPages(string Title, string Text, string Format, int LeftMargin = 2, int RightMargin = 2, int TopMargin = 2, int BottomMargin = 2)
		{
			List<BookPage> list = new List<BookPage>();
			int num = 80 - LeftMargin - RightMargin;
			int num2 = 24 - TopMargin - BottomMargin;
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num3 = 0;
			List<string> list2 = StringFormat.ClipTextToArray(GameText.VariableReplace(Text, false), num, out num3, true, true, true, false);
			int num4 = 0;
			for (int i = 0; i < list2.Count; i++)
			{
				stringBuilder.Append(list2[i]);
				num4++;
				if (num4 >= num2 || i == list2.Count - 1)
				{
					num4 = 0;
					list.Add(new BookPage(Title, stringBuilder.ToString())
					{
						Format = Format,
						LeftMargin = LeftMargin,
						RightMargin = RightMargin,
						TopMargin = TopMargin,
						BottomMargin = BottomMargin
					});
					stringBuilder.Length = 0;
				}
				else
				{
					stringBuilder.Append("\n");
				}
			}
			return list;
		}

		// Token: 0x0600D03E RID: 53310 RVA: 0x004CCDD8 File Offset: 0x004CAFD8
		public static void RenderDynamicBook(string BookID)
		{
			string text = BookID.Substring(1);
			Type type = ModManager.ResolveType("XRL.World.Parts." + text, false, false, true);
			object obj = Activator.CreateInstance(type);
			List<IBookContents.BookPageInfo> list = (List<IBookContents.BookPageInfo>)type.GetMethod("GetContents").Invoke(obj, new object[0]);
			List<BookPage> list2 = new List<BookPage>();
			foreach (IBookContents.BookPageInfo bookPageInfo in list)
			{
				if (bookPageInfo.Format == "Auto")
				{
					list2.AddRange(BookUI.AutoformatPages(bookPageInfo.Title, GameText.VariableReplace(bookPageInfo.Text, false), bookPageInfo.Format, bookPageInfo.Margins));
				}
				else
				{
					list2.Add(new BookPage(bookPageInfo.Title, GameText.VariableReplace(bookPageInfo.Text, false)));
				}
			}
			BookInfo bookInfo = new BookInfo
			{
				Dynamic = true
			};
			try
			{
				MethodInfo method = type.GetMethod("GetTitle");
				bookInfo.Title = (string)method.Invoke(obj, new object[0]);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Exception executing GetTitle on dynamic book", ex, "game_exception");
			}
			bookInfo.Pages = list2;
			BookUI.Books.Add(BookID, bookInfo);
			BookUI.DynamicBooks.Add(BookID);
		}

		// Token: 0x0600D03F RID: 53311 RVA: 0x004CCF40 File Offset: 0x004CB140
		public static void RenderPage(string BookID, int nPage)
		{
			BookPage bookPage;
			if (BookID[0] == '@')
			{
				if (!BookUI.Books.ContainsKey(BookID))
				{
					BookUI.RenderDynamicBook(BookID);
				}
				bookPage = BookUI.Books[BookID].Pages[nPage];
			}
			else
			{
				bookPage = BookUI.Books[BookID].Pages[nPage];
			}
			int num = 1 + bookPage.TopMargin;
			int num2 = 0;
			int num3 = num2;
			while (num3 < bookPage.Lines.Count && num < 24)
			{
				BookUI.Buffer.Goto(bookPage.LeftMargin, num);
				BookUI.Buffer.Write(GameText.VariableReplace(bookPage.Lines[num3], false), true, false, false, null, -1);
				num++;
				num3++;
			}
			BookUI.Buffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			BookUI.Buffer.Goto(2, 0);
			BookUI.Buffer.Write("[ {{Y|" + bookPage.Title + "}} ]", true, false, false, null, -1);
			BookUI.Buffer.Goto(2, 24);
			BookUI.Buffer.Write(string.Concat(new string[]
			{
				"[ Page {{C|",
				(nPage + 1).ToString(),
				"}} of {{C|",
				BookUI.Books[BookID].Count.ToString(),
				"}} ]"
			}), true, false, false, null, -1);
			BookUI.Buffer.Write(string.Concat(new string[]
			{
				" [",
				ControlManager.getCommandInputFormatted("UI:Navigate/left", false, false),
				"]-Previous Page [",
				ControlManager.getCommandInputFormatted("UI:Navigate/right", false, false),
				"]-Next Page [",
				ControlManager.getCommandInputFormatted("Cancel", false, false),
				"]-Exit]"
			}), true, false, false, null, -1);
			if (bookPage.Lines.Count > 23)
			{
				for (int i = 1; i < 24; i++)
				{
					BookUI.Buffer.Goto(79, i);
					BookUI.Buffer.Write(177, ConsoleLib.Console.ColorUtility.Bright(0), 0);
				}
				int num4 = (int)Math.Ceiling((double)bookPage.Lines.Count / 23.0);
				int num5 = (int)((double)((int)Math.Ceiling((double)bookPage.Lines.Count + 23.0)) / 23.0);
				if (num5 <= 0)
				{
					num5 = 1;
				}
				int num6 = 23 / num5;
				if (num6 <= 0)
				{
					num6 = 1;
				}
				int num7 = (int)((double)(23 - num6) * ((double)num2 / (double)(bookPage.Lines.Count - 23)));
				num7++;
				for (int j = num7; j < num7 + num6; j++)
				{
					BookUI.Buffer.Goto(79, j);
					BookUI.Buffer.Write(219, ConsoleLib.Console.ColorUtility.Bright(7), 0);
				}
			}
		}

		// Token: 0x0600D040 RID: 53312 RVA: 0x004CD21C File Offset: 0x004CB41C
		public static void RenderPage(MarkovBook Book, int nPage)
		{
			BookPage bookPage = Book.Pages[nPage];
			int num = 1 + bookPage.TopMargin;
			int num2 = 0;
			int num3 = num2;
			while (num3 < bookPage.Lines.Count && num < 24)
			{
				BookUI.Buffer.Goto(bookPage.LeftMargin, num);
				BookUI.Buffer.Write(GameText.VariableReplace(bookPage.Lines[num3], false), true, false, false, null, -1);
				num++;
				num3++;
			}
			BookUI.Buffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			BookUI.Buffer.Goto(2, 0);
			BookUI.Buffer.Write("[ {{Y|" + bookPage.Title + "}} ]", true, false, false, null, -1);
			BookUI.Buffer.Goto(2, 24);
			BookUI.Buffer.Write(string.Concat(new string[]
			{
				"[ Page {{C|",
				(nPage + 1).ToString(),
				"}} of {{C|",
				Book.Pages.Count.ToString(),
				"}} ]"
			}), true, false, false, null, -1);
			BookUI.Buffer.Write(string.Concat(new string[]
			{
				" [",
				ControlManager.getCommandInputFormatted("UI:Navigate/left", false, false),
				"]-Previous Page [",
				ControlManager.getCommandInputFormatted("UI:Navigate/right", false, false),
				"]-Next Page [",
				ControlManager.getCommandInputFormatted("Cancel", false, false),
				"]-Exit]"
			}), true, false, false, null, -1);
			if (bookPage.Lines.Count > 23)
			{
				for (int i = 1; i < 24; i++)
				{
					BookUI.Buffer.Goto(79, i);
					BookUI.Buffer.Write(177, ConsoleLib.Console.ColorUtility.Bright(0), 0);
				}
				int num4 = (int)Math.Ceiling((double)bookPage.Lines.Count / 23.0);
				int num5 = (int)((double)((int)Math.Ceiling((double)bookPage.Lines.Count + 23.0)) / 23.0);
				if (num5 <= 0)
				{
					num5 = 1;
				}
				int num6 = 23 / num5;
				if (num6 <= 0)
				{
					num6 = 1;
				}
				int num7 = (int)((double)(23 - num6) * ((double)num2 / (double)(bookPage.Lines.Count - 23)));
				num7++;
				for (int j = num7; j < num7 + num6; j++)
				{
					BookUI.Buffer.Goto(79, j);
					BookUI.Buffer.Write(219, ConsoleLib.Console.ColorUtility.Bright(7), 0);
				}
			}
		}

		// Token: 0x0600D041 RID: 53313 RVA: 0x004CD4B0 File Offset: 0x004CB6B0
		public static void ShowBookByID(string BookID, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null)
		{
			if (Options.ModernUI)
			{
				bool result = BookScreen.show(BookID, Sound, OnShowPage, null).Result;
				return;
			}
			GameManager.Instance.PushGameView("Book", true);
			SoundManager.PlaySound(Sound, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			int num = 0;
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer2(true);
			Keys keys;
			do
			{
				XRL.World.Event.ResetPool(false);
				BookUI.Buffer.Clear();
				BookUI.Buffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				BookUI.RenderPage(BookID, num);
				BookUI.Console.DrawBuffer(BookUI.Buffer, null, false);
				if (OnShowPage != null)
				{
					OnShowPage(num);
				}
				keys = Keyboard.getvk(true, false, true);
				if ((keys == Keys.NumPad6 || keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right")) && num < BookUI.Books[BookID].Count - 1)
				{
					SoundManager.PlaySound("Sounds/Interact/sfx_interact_book_pageTurn", 0.2f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					num++;
				}
				if ((keys == Keys.NumPad4 || keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left")) && num > 0)
				{
					SoundManager.PlaySound("Sounds/Interact/sfx_interact_book_pageTurn", 0.2f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					num--;
				}
			}
			while (keys != Keys.Escape);
			GameManager.Instance.PopGameView(true);
			scrapBuffer.Draw();
		}

		// Token: 0x0600D042 RID: 53314 RVA: 0x004CD624 File Offset: 0x004CB824
		public static void ShowBook(MarkovBook Book, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null, Action<int> AfterShowPage = null)
		{
			if (Options.ModernUI)
			{
				bool result = BookScreen.show(Book, Sound, OnShowPage, AfterShowPage).Result;
				return;
			}
			GameManager.Instance.PushGameView("Book", true);
			SoundManager.PlaySound(Sound, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			int num = 0;
			Keys keys;
			do
			{
				XRL.World.Event.ResetPool(false);
				BookUI.Buffer.Clear();
				BookUI.Buffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				BookUI.RenderPage(Book, num);
				BookUI.Console.DrawBuffer(BookUI.Buffer, null, false);
				if (OnShowPage != null)
				{
					OnShowPage(num);
				}
				keys = Keyboard.getvk(true, false, true);
				if (keys == Keys.NumPad6 && num < Book.Pages.Count - 1)
				{
					SoundManager.PlaySound("Sounds/Interact/sfx_interact_book_pageTurn", 0.2f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					if (AfterShowPage != null)
					{
						AfterShowPage(num);
					}
					num++;
				}
				if (keys == Keys.NumPad4 && num > 0)
				{
					SoundManager.PlaySound("Sounds/Interact/sfx_interact_book_pageTurn", 0.2f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					if (AfterShowPage != null)
					{
						AfterShowPage(num);
					}
					num--;
				}
			}
			while (keys != Keys.Escape);
			if (AfterShowPage != null)
			{
				AfterShowPage(num);
			}
			GameManager.Instance.PopGameView(true);
		}

		// Token: 0x0600D043 RID: 53315 RVA: 0x004CD75C File Offset: 0x004CB95C
		public static void ShowBook(string PageText, string BookTitle, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null, Action<int> AfterShowPage = null)
		{
			PageText = Markup.Transform(PageText, false);
			BookTitle = Markup.Transform(BookTitle, false);
			MarkovBook markovBook = new MarkovBook();
			markovBook.Title = BookTitle;
			markovBook.Pages = new List<BookPage>();
			string text = "Auto";
			string text2 = "1,2,2,2";
			markovBook.Pages.AddRange(BookUI.AutoformatPages(BookTitle, PageText, text, text2));
			BookUI.ShowBook(markovBook, Sound, OnShowPage, AfterShowPage);
		}

		// Token: 0x0600D044 RID: 53316 RVA: 0x004CD7C8 File Offset: 0x004CB9C8
		public static void ShowBook(List<string> PageText, string BookTitle, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null, Action<int> AfterShowPage = null)
		{
			BookTitle = Markup.Transform(BookTitle, false);
			MarkovBook markovBook = new MarkovBook();
			markovBook.Title = BookTitle;
			markovBook.Pages = new List<BookPage>();
			string text = "Auto";
			string text2 = "1,2,2,2";
			foreach (string text3 in PageText)
			{
				markovBook.Pages.AddRange(BookUI.AutoformatPages(BookTitle, Markup.Transform(text3, false), text, text2));
			}
			BookUI.ShowBook(markovBook, Sound, OnShowPage, AfterShowPage);
		}

		// Token: 0x040051B3 RID: 20915
		public static int ScrollPosition;

		// Token: 0x040051B4 RID: 20916
		public static int IndexPosition;

		// Token: 0x040051B5 RID: 20917
		public static TextConsole Console;

		// Token: 0x040051B6 RID: 20918
		public static ScreenBuffer Buffer;

		// Token: 0x040051B7 RID: 20919
		public static Dictionary<string, string> BookCorpus = new Dictionary<string, string>();

		// Token: 0x040051B8 RID: 20920
		public static Dictionary<string, BookInfo> Books = new Dictionary<string, BookInfo>();

		// Token: 0x040051B9 RID: 20921
		public static List<string> DynamicBooks = new List<string>();

		// Token: 0x040051BA RID: 20922
		public static readonly Dictionary<string, Action<XmlDataHelper>> _Nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"books",
				new Action<XmlDataHelper>(BookUI.HandleNodes)
			},
			{
				"book",
				new Action<XmlDataHelper>(BookUI.HandleBookNode)
			}
		};

		// Token: 0x040051BB RID: 20923
		public static readonly Dictionary<string, Action<XmlDataHelper>> _BookNodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"page",
			new Action<XmlDataHelper>(BookUI.HandlePageNode)
		} };

		// Token: 0x040051BC RID: 20924
		private static bool IncludeCorpusData = false;

		// Token: 0x040051BD RID: 20925
		private static StringBuilder CorpusBuilder = new StringBuilder();

		// Token: 0x040051BE RID: 20926
		private static BookInfo CurrentBook;
	}
}
