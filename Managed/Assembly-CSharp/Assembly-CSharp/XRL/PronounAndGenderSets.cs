using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World;

namespace XRL
{
	// Token: 0x020004EA RID: 1258
	public static class PronounAndGenderSets
	{
		// Token: 0x06003652 RID: 13906 RVA: 0x00104D6C File Offset: 0x00102F6C
		public static void AssignGender(GameObject Player, out Gender ChosenGender, out Gender CustomGender, string Spec = null)
		{
			if (!string.IsNullOrEmpty(Spec))
			{
				if (Spec.Contains(","))
				{
					ChosenGender = Gender.Get(Spec.Split(',', StringSplitOptions.None).GetRandomElement<string>(null), false);
				}
				else
				{
					ChosenGender = Gender.Get(Spec, false);
				}
				CustomGender = null;
				return;
			}
			if (Gender.EnableGeneration && Stat.Random(1, 10) == 1)
			{
				Gender gender;
				CustomGender = (gender = Gender.Generate(false, 0, 0, null, null, null, null, null, null));
				ChosenGender = gender;
				return;
			}
			ChosenGender = Gender.GetAnyGenericPersonalSingular();
			CustomGender = null;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x00104DE8 File Offset: 0x00102FE8
		public static void AssignGender(GameObject Player)
		{
			Gender gender;
			Gender gender2;
			PronounAndGenderSets.AssignGender(Player, out gender, out gender2, null);
			Player.SetGender(gender.Register());
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x00104E0C File Offset: 0x0010300C
		public static void AssignPronounSet(GameObject Player, out PronounSet ChosenPronounSet, out PronounSet CustomPronounSet, bool Always = false)
		{
			if (!Always && Stat.Random(1, 10) != 1)
			{
				ChosenPronounSet = null;
				CustomPronounSet = null;
				return;
			}
			if (PronounSet.EnableGeneration && Stat.Random(1, 10) == 1)
			{
				PronounSet pronounSet;
				CustomPronounSet = (pronounSet = PronounSet.Generate(false, 0, 0, null, null, null, null, null, null));
				ChosenPronounSet = pronounSet;
				return;
			}
			ChosenPronounSet = PronounSet.GetAnyGenericPersonalSingular();
			CustomPronounSet = null;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x00104E60 File Offset: 0x00103060
		public static void AssignPronounSet(GameObject Player)
		{
			PronounSet pronounSet;
			PronounSet pronounSet2;
			PronounAndGenderSets.AssignPronounSet(Player, out pronounSet, out pronounSet2, false);
			if (pronounSet == null)
			{
				Player.ClearPronounSet();
				return;
			}
			Player.SetPronounSet(pronounSet.Register());
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x00104E8E File Offset: 0x0010308E
		public static void AssignGenderAndPronounSet(GameObject Player, out Gender ChosenGender, out PronounSet ChosenPronounSet, out Gender CustomGender, out PronounSet CustomPronounSet)
		{
			PronounAndGenderSets.AssignGender(Player, out ChosenGender, out CustomGender, null);
			PronounAndGenderSets.AssignPronounSet(Player, out ChosenPronounSet, out CustomPronounSet, false);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x00104EA3 File Offset: 0x001030A3
		public static void AssignGenderAndPronounSet(GameObject Player)
		{
			PronounAndGenderSets.AssignGender(Player);
			PronounAndGenderSets.AssignPronounSet(Player);
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x00104EB4 File Offset: 0x001030B4
		public static void ShowChangePronounSet(GameObject Player)
		{
			if (PronounAndGenderSets._Console == null)
			{
				PronounAndGenderSets._Console = Popup._TextConsole;
			}
			ScreenBuffer screenBuffer = ScreenBuffer.create(80, 25);
			GameManager.Instance.PushGameView("ChangePronounSet", true);
			Keyboard.ClearMouseEvents(false);
			int num = 0;
			PronounSet pronounSet = Player.GetPronounSet();
			PronounSet pronounSet2 = pronounSet;
			PronounSet pronounSet3 = null;
			List<PronounSet> list = PronounSet.GetAllGenericPersonal();
			if (pronounSet2 != null && !list.Contains(pronounSet2))
			{
				list = new List<PronounSet>(list);
				list.Add(pronounSet2);
			}
			if (pronounSet2 != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == pronounSet2)
					{
						num = i + 1;
						break;
					}
				}
			}
			for (;;)
			{
				screenBuffer.Clear();
				GameManager.Instance.ClearRegions();
				PronounSet pronounSet4;
				if (num == 0)
				{
					pronounSet4 = null;
				}
				else if (num <= list.Count)
				{
					pronounSet4 = list[num - 1];
				}
				else
				{
					pronounSet4 = pronounSet3;
				}
				screenBuffer.Goto(3, 2);
				screenBuffer.Write("&WPronoun Set&y", true, false, false, null, -1);
				int num2 = 4;
				screenBuffer.Goto(3, num2);
				if (pronounSet2 == null)
				{
					screenBuffer.Write("&k^Y<from gender>&y&k", true, false, false, null, -1);
				}
				else
				{
					screenBuffer.Write("<from gender>", true, false, false, null, -1);
				}
				GameManager.Instance.AddRegion(3, num2, 30, num2, "Select::0", null, null);
				num2++;
				for (int j = 0; j < list.Count; j++)
				{
					screenBuffer.Goto(3, num2);
					string text = list[j].GetShortName();
					if (list[j] == pronounSet2)
					{
						text = "{{&k^Y|" + text + "}}";
					}
					screenBuffer.Write(text, true, false, false, null, -1);
					GameManager.Instance.AddRegion(3, num2, 30, num2, "Select:" + list[j].Name + ":" + (j + 1).ToString(), null, null);
					num2++;
				}
				if (pronounSet3 != null)
				{
					screenBuffer.Goto(3, num2);
					string text2 = pronounSet3.GetShortName();
					if (pronounSet3 == pronounSet2)
					{
						text2 = "{{&k^Y|" + text2 + "}}";
					}
					screenBuffer.Write(text2, true, false, false, null, -1);
					GameManager.Instance.AddRegion(3, num2, 30, num2, "Select:" + pronounSet3.Name + ":" + (list.Count + 1).ToString(), null, null);
					num2++;
				}
				screenBuffer.Goto(3, ++num2);
				if (pronounSet4 == pronounSet2)
				{
					if (PronounSet.EnableGeneration)
					{
						screenBuffer.Write("&Wenter &yto copy and customize", true, false, false, null, -1);
					}
				}
				else
				{
					screenBuffer.Write("&Wenter &yto select", true, false, false, null, -1);
				}
				screenBuffer.Goto(3, ++num2);
				screenBuffer.Write("&WF1 &yfor random", true, false, false, null, -1);
				if (PronounSet.EnableGeneration)
				{
					screenBuffer.Goto(3, num2 + 1);
					screenBuffer.Write("&WF2 &yto generate", true, false, false, null, -1);
				}
				screenBuffer.Goto(2, 4 + num);
				screenBuffer.Write("&Y>", true, false, false, null, -1);
				TextBlock textBlock = new TextBlock((pronounSet4 != null) ? pronounSet4.GetBasicSummary() : Player.GetGender(false).GetBasicSummary(), 45, 22, false, 0);
				screenBuffer.SingleBox(33, 23 - textBlock.Lines.Count, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				for (int k = 0; k < textBlock.Lines.Count; k++)
				{
					screenBuffer.Goto(34, 24 - textBlock.Lines.Count + k);
					screenBuffer.Write(textBlock.Lines[k].PadRight(45), true, false, false, null, -1);
				}
				screenBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				screenBuffer.Goto(28, 0);
				screenBuffer.Write("[ &YChange Pronoun Set&y ]", true, false, false, null, -1);
				screenBuffer.Goto(3, 24);
				if (pronounSet2 != pronounSet)
				{
					screenBuffer.Write(" press {{hotkey|Space}} to keep changes and continue ", true, false, false, null, -1);
				}
				else
				{
					screenBuffer.Write(" press {{hotkey|Space}} to return ", true, false, false, null, -1);
				}
				PronounAndGenderSets._Console.DrawBuffer(screenBuffer, null, false);
				Event.ResetPool(true);
				Keys keys = Keyboard.getvk(true, false, true);
				if ((keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && (pronounSet2 == pronounSet || Popup.ShowYesNo("Are you sure you want to discard your changes?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes))
				{
					break;
				}
				if (keys == Keys.Space || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "SilentContinue"))
				{
					goto IL_0483;
				}
				if (keys == Keys.Enter)
				{
					if (pronounSet2 == pronounSet4)
					{
						if (PronounSet.EnableGeneration)
						{
							PronounSet pronounSet5 = ((pronounSet2 != null) ? new PronounSet(pronounSet2) : new PronounSet(Player.GetGender(false)));
							if (pronounSet5.CustomizeAsync().Result)
							{
								pronounSet3 = PronounSet.GetIfExists(pronounSet5.Name) ?? pronounSet5;
								pronounSet2 = pronounSet3;
								num = list.Count + 1;
								for (int l = 0; l < list.Count; l++)
								{
									if (list[l] == pronounSet3)
									{
										num = l + 1;
										pronounSet3 = null;
										break;
									}
								}
							}
						}
					}
					else
					{
						pronounSet2 = pronounSet4;
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Select:"))
				{
					string[] array = Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None);
					if (array[1] == "")
					{
						pronounSet2 = null;
					}
					else
					{
						pronounSet2 = PronounSet.Get(array[1]);
					}
					num = Convert.ToInt32(array[2]);
				}
				if (keys == Keys.F1)
				{
					PronounSet pronounSet6;
					PronounAndGenderSets.AssignPronounSet(Player, out pronounSet2, out pronounSet6, true);
					if (pronounSet6 != null)
					{
						pronounSet3 = pronounSet6;
					}
					num = 0;
					if (pronounSet3 == pronounSet2)
					{
						num = list.Count;
					}
					else
					{
						for (int m = 0; m < list.Count; m++)
						{
							if (list[m] == pronounSet2)
							{
								num = m + 1;
								break;
							}
						}
					}
				}
				if (keys == Keys.F2 && PronounSet.EnableGeneration)
				{
					pronounSet3 = (pronounSet2 = PronounSet.Generate(false, 0, 0, null, null, null, null, null, null));
					num = list.Count + 1;
				}
				if (keys == Keys.NumPad8 && num > 0)
				{
					num--;
				}
				if (keys == Keys.NumPad2 && (num < list.Count || (pronounSet3 != null && num < list.Count + 1)))
				{
					num++;
				}
			}
			GameManager.Instance.PopGameView(false);
			return;
			IL_0483:
			if (pronounSet2 == null)
			{
				Player.ClearPronounSet();
			}
			else if (pronounSet == null || pronounSet2.Name != pronounSet.Name)
			{
				Player.SetPronounSet(pronounSet2.Register());
				Player.ModIntProperty("PronounSetTick", 1, false);
			}
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x00105530 File Offset: 0x00103730
		public static void ShowPickGenderAndPronounSet(GameObject Player, string Spec = null)
		{
			ScreenBuffer screenBuffer = ScreenBuffer.create(80, 25);
			GameManager.Instance.PushGameView("PickGenderAndPronounSet", true);
			Keyboard.ClearMouseEvents(false);
			int num = 0;
			int num2 = 0;
			Gender gender = Player.GetGender(false);
			Gender gender2 = gender;
			Gender gender3 = gender;
			Gender gender4 = null;
			PronounSet pronounSet = Player.GetPronounSet();
			PronounSet pronounSet2 = pronounSet;
			PronounSet pronounSet3 = null;
			List<PronounSet> allGenericPersonal = PronounSet.GetAllGenericPersonal();
			bool flag = false;
			bool enableGeneration = PronounSet.EnableGeneration;
			List<Gender> list;
			if (!string.IsNullOrEmpty(Spec))
			{
				string[] array = Spec.Split(',', StringSplitOptions.None);
				list = new List<Gender>(array.Length);
				foreach (string text in array)
				{
					if (text == "generate")
					{
						flag = Gender.EnableGeneration;
					}
					else
					{
						list.Add(Gender.Get(text, false));
					}
				}
				if (list.Count == 0)
				{
					MetricsManager.LogError("no usable base genders");
					Popup.ShowFail("Cannot proceed, no usable base genders", true, true, true);
					GameManager.Instance.PopGameView(false);
					return;
				}
			}
			else
			{
				list = Gender.GetAllGenericPersonalSingular();
				flag = Gender.EnableGeneration;
			}
			if (gender != null)
			{
				num2 = list.IndexOf(gender);
				if (num2 == -1)
				{
					gender4 = gender;
					num2 = list.Count;
				}
			}
			if (pronounSet != null && !allGenericPersonal.Contains(pronounSet))
			{
				pronounSet3 = pronounSet;
			}
			for (;;)
			{
				screenBuffer.Clear();
				GameManager.Instance.ClearRegions();
				Gender gender5 = null;
				PronounSet pronounSet4 = null;
				if (num == 0)
				{
					gender5 = ((num2 == list.Count) ? gender4 : list[num2]);
					if (gender2 == null)
					{
						gender2 = gender5;
					}
					if (gender3 == null)
					{
						gender3 = gender2;
					}
				}
				else if (num == 1)
				{
					if (num2 == 0)
					{
						pronounSet4 = null;
					}
					else if (num2 <= allGenericPersonal.Count)
					{
						pronounSet4 = allGenericPersonal[num2 - 1];
					}
					else
					{
						pronounSet4 = pronounSet3;
					}
				}
				screenBuffer.Goto(3, 2);
				screenBuffer.Write("&WGender&y", true, false, false, null, -1);
				if (PronounSet.EnableSelection)
				{
					screenBuffer.Goto(42, 2);
					screenBuffer.Write("&WPronoun Set&y", true, false, false, null, -1);
				}
				int num3 = 4;
				int num4 = 4;
				for (int j = 0; j < list.Count; j++)
				{
					screenBuffer.Goto(3, num3);
					string text2 = Grammar.MakeTitleCase(list[j].Name);
					if (list[j] == gender3)
					{
						text2 = "&k^Y" + text2 + "&y^k";
					}
					screenBuffer.Write(text2, true, false, false, null, -1);
					GameManager.Instance.AddRegion(3, num3, 30, num3, "Select:" + list[j].Name + ":0:" + j.ToString(), null, null);
					num3++;
				}
				if (gender4 != null)
				{
					screenBuffer.Goto(3, num3);
					string text3 = Grammar.MakeTitleCase(gender4.Name);
					if (gender4 == gender3)
					{
						text3 = "&k^Y" + text3 + "&y^k";
					}
					screenBuffer.Write(text3, true, false, false, null, -1);
					GameManager.Instance.AddRegion(3, num3, 30, num3, "Select:" + gender4.Name + ":0:" + list.Count.ToString(), null, null);
					num3++;
				}
				if (PronounSet.EnableSelection)
				{
					screenBuffer.Goto(42, num4);
					if (pronounSet2 == null)
					{
						screenBuffer.Write("&k^Y<from gender>&y&k", true, false, false, null, -1);
					}
					else
					{
						screenBuffer.Write("<from gender>", true, false, false, null, -1);
					}
					GameManager.Instance.AddRegion(42, num4, 70, num4, "Select::1:0", null, null);
					num4++;
					for (int k = 0; k < allGenericPersonal.Count; k++)
					{
						screenBuffer.Goto(42, num4);
						string text4 = allGenericPersonal[k].GetShortName();
						if (allGenericPersonal[k] == pronounSet2)
						{
							text4 = "{{&k^Y|" + text4 + "}}";
						}
						screenBuffer.Write(text4, true, false, false, null, -1);
						GameManager.Instance.AddRegion(3, num4, 30, num4, "Select:" + allGenericPersonal[k].Name + ":1:" + (k + 1).ToString(), null, null);
						num4++;
					}
					if (pronounSet3 != null)
					{
						screenBuffer.Goto(42, num4);
						string text5 = pronounSet3.GetShortName();
						if (pronounSet3 == pronounSet2)
						{
							text5 = "{{&k^Y|" + text5 + "}}";
						}
						screenBuffer.Write(text5, true, false, false, null, -1);
						GameManager.Instance.AddRegion(3, num3, 30, num3, "Select:" + pronounSet3.Name + ":1:" + (allGenericPersonal.Count + 1).ToString(), null, null);
						num4++;
					}
				}
				if (num == 0)
				{
					if (gender5 == gender3)
					{
						if (flag)
						{
							screenBuffer.Goto(3, ++num3);
							screenBuffer.Write("&Wenter &yto copy and customize", true, false, false, null, -1);
						}
					}
					else
					{
						screenBuffer.Goto(3, ++num3);
						screenBuffer.Write("&Wenter &yto select", true, false, false, null, -1);
					}
					screenBuffer.Goto(3, ++num3);
					screenBuffer.Write(PronounSet.EnableSelection ? "&WF1 &yfor both random" : "&WF1 &yfor random", true, false, false, null, -1);
					if (flag)
					{
						screenBuffer.Goto(3, num3 + 1);
						screenBuffer.Write("&WF2 &yto generate gender", true, false, false, null, -1);
					}
				}
				else if (num == 1)
				{
					if (pronounSet4 == pronounSet2)
					{
						if (enableGeneration)
						{
							screenBuffer.Goto(42, ++num4);
							screenBuffer.Write("&Wenter &yto copy and customize", true, false, false, null, -1);
						}
					}
					else
					{
						screenBuffer.Goto(42, ++num4);
						screenBuffer.Write("&Wenter &yto select", true, false, false, null, -1);
					}
					screenBuffer.Goto(42, ++num4);
					screenBuffer.Write("&WF1 &yfor both random", true, false, false, null, -1);
					if (enableGeneration)
					{
						screenBuffer.Goto(42, num4 + 1);
						screenBuffer.Write("&WF2 &yto generate pronoun set", true, false, false, null, -1);
					}
				}
				if (num == 0)
				{
					screenBuffer.Goto(2, 4 + num2);
					screenBuffer.Write("&Y>", true, false, false, null, -1);
					TextBlock textBlock = new TextBlock(gender5.GetBasicSummary(), 45, 22, false, 0);
					screenBuffer.SingleBox(33, 23 - textBlock.Lines.Count, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					for (int l = 0; l < textBlock.Lines.Count; l++)
					{
						screenBuffer.Goto(34, 24 - textBlock.Lines.Count + l);
						screenBuffer.Write(textBlock.Lines[l].PadRight(45), true, false, false, null, -1);
					}
				}
				else if (num == 1)
				{
					screenBuffer.Goto(41, 4 + num2);
					screenBuffer.Write("{{Y|>}}", true, false, false, null, -1);
					TextBlock textBlock2 = new TextBlock(((pronounSet4 != null) ? pronounSet4.GetBasicSummary() : null) ?? gender3.GetBasicSummary(), 38, 22, false, 0);
					screenBuffer.SingleBox(0, 23 - textBlock2.Lines.Count, 39, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					for (int m = 0; m < textBlock2.Lines.Count; m++)
					{
						screenBuffer.Goto(1, 24 - textBlock2.Lines.Count + m);
						screenBuffer.Write(textBlock2.Lines[m].PadRight(38), true, false, false, null, -1);
					}
				}
				screenBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				if (PronounSet.EnableSelection)
				{
					screenBuffer.Goto(13, 0);
					screenBuffer.Write("[ &WCharacter Creation &y- &YChoose Gender and Pronoun Set&y ]", true, false, false, null, -1);
				}
				else
				{
					screenBuffer.Goto(21, 0);
					screenBuffer.Write("[ &WCharacter Creation &y- &YChoose Gender&y ]", true, false, false, null, -1);
				}
				screenBuffer.Goto(3, 24);
				if (gender3 != gender || pronounSet2 != pronounSet)
				{
					screenBuffer.Write(" press {{hotkey|Space}} to keep changes and continue ", true, false, false, null, -1);
				}
				else
				{
					screenBuffer.Write(" press {{hotkey|Space}} to return ", true, false, false, null, -1);
				}
				PronounAndGenderSets._Console.DrawBuffer(screenBuffer, null, false);
				Event.ResetPool(true);
				Keys keys = Keyboard.getvk(true, false, true);
				if ((keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && ((gender3 == gender && pronounSet2 == pronounSet) || Popup.ShowYesNo("Are you sure you want to discard your changes?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes))
				{
					break;
				}
				if (keys == Keys.Space || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "SilentContinue"))
				{
					goto IL_084F;
				}
				if (keys == Keys.Enter)
				{
					if (num == 0)
					{
						if (gender3 == gender5)
						{
							if (flag)
							{
								Gender gender6 = new Gender(gender3);
								if (gender6.CustomizeAsync().Result)
								{
									gender4 = (gender3 = gender6);
									num2 = list.Count;
								}
							}
						}
						else
						{
							gender3 = gender5;
						}
					}
					else if (num == 1)
					{
						if (pronounSet2 == pronounSet4)
						{
							PronounSet pronounSet5 = ((pronounSet2 != null) ? new PronounSet(pronounSet2) : new PronounSet(gender3));
							if (pronounSet5.CustomizeAsync().Result)
							{
								pronounSet3 = PronounSet.GetIfExists(pronounSet5.Name) ?? pronounSet5;
								pronounSet2 = pronounSet3;
								num2 = allGenericPersonal.Count + 1;
								for (int n = 0; n < allGenericPersonal.Count; n++)
								{
									if (allGenericPersonal[n] == pronounSet3)
									{
										num2 = n + 1;
										pronounSet3 = null;
										break;
									}
								}
							}
						}
						else
						{
							pronounSet2 = pronounSet4;
						}
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Select:"))
				{
					num = 0;
					string[] array3 = Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None);
					if (array3[2] == "0")
					{
						gender3 = Gender.Get(array3[1], false);
						num2 = Convert.ToInt32(array3[3]);
					}
					else if (array3[2] == "1")
					{
						if (array3[1] == "")
						{
							pronounSet2 = null;
						}
						else
						{
							pronounSet2 = PronounSet.Get(array3[1]);
						}
						num2 = Convert.ToInt32(array3[3]);
					}
				}
				if (keys == Keys.F1)
				{
					if (PronounSet.EnableSelection)
					{
						Gender gender7;
						PronounSet pronounSet6;
						PronounAndGenderSets.AssignGenderAndPronounSet(Player, out gender3, out pronounSet2, out gender7, out pronounSet6);
						if (gender7 != null)
						{
							gender4 = gender7;
						}
						if (pronounSet6 != null)
						{
							pronounSet3 = pronounSet6;
						}
					}
					else
					{
						Gender gender8;
						PronounAndGenderSets.AssignGender(Player, out gender3, out gender8, null);
						if (gender8 != null)
						{
							gender4 = gender8;
						}
					}
					num2 = 0;
					if (num == 0)
					{
						if (gender4 == gender3)
						{
							num2 = list.Count;
						}
						else
						{
							for (int num5 = 0; num5 < list.Count; num5++)
							{
								if (list[num5] == gender3)
								{
									num2 = num5;
									break;
								}
							}
						}
					}
					else if (num == 1)
					{
						if (pronounSet3 == pronounSet2)
						{
							num2 = allGenericPersonal.Count;
						}
						else
						{
							for (int num6 = 0; num6 < allGenericPersonal.Count; num6++)
							{
								if (allGenericPersonal[num6] == pronounSet2)
								{
									num2 = num6;
									break;
								}
							}
						}
					}
				}
				if (keys == Keys.F2)
				{
					if (num == 0)
					{
						if (flag)
						{
							gender4 = (gender3 = Gender.Generate(false, 0, 0, null, null, null, null, null, null));
							num2 = list.Count;
						}
					}
					else if (num == 1 && enableGeneration)
					{
						pronounSet3 = (pronounSet2 = PronounSet.Generate(false, 0, 0, null, null, null, null, null, null));
						num2 = allGenericPersonal.Count + 1;
					}
				}
				if (keys == Keys.NumPad8 && num2 > 0)
				{
					num2--;
				}
				if (keys == Keys.NumPad2)
				{
					if (num == 0)
					{
						if (num2 < list.Count - 1 || (gender4 != null && num2 < list.Count))
						{
							num2++;
						}
					}
					else if (num == 1 && (num2 < allGenericPersonal.Count || (pronounSet3 != null && num2 < allGenericPersonal.Count + 1)))
					{
						num2++;
					}
				}
				if (keys == Keys.NumPad4 && PronounSet.EnableSelection)
				{
					if (num == 1)
					{
						num = 0;
					}
					num2 = Math.Min(num2, (gender4 == null) ? (list.Count - 1) : list.Count);
				}
				if (keys == Keys.NumPad6 && PronounSet.EnableSelection)
				{
					if (num == 0)
					{
						num = 1;
					}
					num2 = Math.Min(num2, (pronounSet3 == null) ? allGenericPersonal.Count : (allGenericPersonal.Count + 1));
				}
			}
			GameManager.Instance.PopGameView(false);
			return;
			IL_084F:
			Player.SetGender(gender3.Register());
			if (pronounSet2 == null)
			{
				Player.ClearPronounSet();
			}
			else
			{
				Player.SetPronounSet(pronounSet2.Register());
			}
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x04001B33 RID: 6963
		private static TextConsole _Console;
	}
}
