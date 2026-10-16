using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Qud.UI;
using XRL.Messages;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014CB RID: 5323
	[HasGameBasedStaticCache]
	public class AbilityManager
	{
		// Token: 0x0600D089 RID: 53385 RVA: 0x004D1A44 File Offset: 0x004CFC44
		public static void RefreshPlayerAbilities()
		{
			AbilityManager.PlayerAbilityLock.EnterWriteLock();
			try
			{
				AbilityManager.PlayerAbilities.Clear();
				AbilityManager.PlayerAbilitiesByCommand.Clear();
				AbilityManager.PlayerAbilitiesByClass.Clear();
				GameObject player = The.Player;
				Dictionary<Guid, ActivatedAbilityEntry> dictionary;
				if (player == null)
				{
					dictionary = null;
				}
				else
				{
					ActivatedAbilities abilities = player.Abilities;
					dictionary = ((abilities != null) ? abilities.AbilityByGuid : null);
				}
				Dictionary<Guid, ActivatedAbilityEntry> dictionary2 = dictionary;
				if (!dictionary2.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
				{
					foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in dictionary2)
					{
						Guid guid;
						ActivatedAbilityEntry activatedAbilityEntry;
						keyValuePair.Deconstruct(out guid, out activatedAbilityEntry);
						ActivatedAbilityEntry activatedAbilityEntry2 = activatedAbilityEntry;
						AbilityManager.PlayerAbilities.Add(activatedAbilityEntry2);
						AbilityManager.PlayerAbilitiesByCommand.TryAdd(activatedAbilityEntry2.Command, activatedAbilityEntry2);
						List<ActivatedAbilityEntry> list;
						if (!AbilityManager.PlayerAbilitiesByClass.TryGetValue(activatedAbilityEntry2.Class, out list))
						{
							list = (AbilityManager.PlayerAbilitiesByClass[activatedAbilityEntry2.Class] = new List<ActivatedAbilityEntry>());
						}
						list.Add(activatedAbilityEntry2);
					}
				}
			}
			finally
			{
				AbilityManager.PlayerAbilityLock.ExitWriteLock();
			}
		}

		// Token: 0x0600D08A RID: 53386 RVA: 0x004D1B54 File Offset: 0x004CFD54
		public static bool IsWorldMapUsable(string AbilityCommand)
		{
			AbilityManager.PlayerAbilityLock.EnterReadLock();
			bool flag;
			try
			{
				ActivatedAbilityEntry activatedAbilityEntry;
				flag = !AbilityManager.PlayerAbilitiesByCommand.TryGetValue(AbilityCommand, out activatedAbilityEntry) || activatedAbilityEntry.IsWorldMapUsable;
			}
			finally
			{
				AbilityManager.PlayerAbilityLock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x0600D08B RID: 53387 RVA: 0x004D1BA4 File Offset: 0x004CFDA4
		public static void UpdateFavorites()
		{
			AbilityManager.RefreshPlayerAbilities();
		}

		// Token: 0x0600D08C RID: 53388 RVA: 0x004D1BAC File Offset: 0x004CFDAC
		private static void BuildNodes(List<AbilityNode> NodeList, GameObject GO)
		{
			NodeList.Clear();
			ActivatedAbilities activatedAbilities = GO.ActivatedAbilities;
			if (activatedAbilities == null)
			{
				return;
			}
			if (AbilityManager.sortMode == AbilityManagerScreen.SortMode.Class)
			{
				using (Dictionary<Guid, ActivatedAbilityEntry>.Enumerator enumerator = activatedAbilities.AbilityByGuid.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair = enumerator.Current;
						Guid guid;
						ActivatedAbilityEntry activatedAbilityEntry;
						keyValuePair.Deconstruct(out guid, out activatedAbilityEntry);
						ActivatedAbilityEntry activatedAbilityEntry2 = activatedAbilityEntry;
						int num = -1;
						AbilityNode abilityNode = null;
						for (int i = NodeList.Count - 1; i >= 0; i--)
						{
							AbilityNode abilityNode2 = NodeList[i];
							if (abilityNode2.ParentNode != null)
							{
								if (abilityNode2.ParentNode.Category == activatedAbilityEntry2.Class)
								{
									num = i + 1;
									abilityNode = abilityNode2.ParentNode;
									break;
								}
							}
							else if (abilityNode2.Category == activatedAbilityEntry2.Class)
							{
								num = i + 1;
								abilityNode = abilityNode2;
								break;
							}
						}
						if (abilityNode == null)
						{
							abilityNode = new AbilityNode(null, activatedAbilityEntry2.Class, null);
							NodeList.Add(abilityNode);
							num = NodeList.Count;
						}
						NodeList.Insert(num, new AbilityNode(activatedAbilityEntry2, "", abilityNode));
					}
					return;
				}
			}
			foreach (ActivatedAbilityEntry activatedAbilityEntry3 in activatedAbilities.GetAbilityListOrderedByPreference())
			{
				NodeList.Add(new AbilityNode(activatedAbilityEntry3, "", null));
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600D08D RID: 53389 RVA: 0x004D1D28 File Offset: 0x004CFF28
		// (set) Token: 0x0600D08E RID: 53390 RVA: 0x004D1D34 File Offset: 0x004CFF34
		public static AbilityManagerScreen.SortMode sortMode
		{
			get
			{
				return SingletonWindowBase<AbilityManagerScreen>.instance.sortMode;
			}
			set
			{
				SingletonWindowBase<AbilityManagerScreen>.instance.sortMode = value;
			}
		}

		// Token: 0x0600D08F RID: 53391 RVA: 0x004D1D44 File Offset: 0x004CFF44
		public static string Show(GameObject GO)
		{
			if (!BeforeAbilityManagerOpenEvent.Check(The.Player))
			{
				return null;
			}
			if (!Options.ModernUI)
			{
				GameManager.Instance.PushGameView("AbilityManager", true);
				TextConsole.LoadScrapBuffers();
				ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				List<AbilityNode> list = new List<AbilityNode>();
				int num = 0;
				int num2 = 0;
				ActivatedAbilityEntry activatedAbilityEntry;
				for (;;)
				{
					IL_0083:
					list.Clear();
					AbilityManager.BuildNodes(list, GO);
					AbilityManager.<>c__DisplayClass11_0 CS$<>8__locals1;
					CS$<>8__locals1.hotkeySpread = ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav" });
					while (!flag)
					{
						Dictionary<char, string> dictionary;
						Dictionary<char, ActivatedAbilityEntry> dictionary2;
						Keys keys;
						char c;
						for (;;)
						{
							Event.ResetPool(true);
							int num3 = 0;
							dictionary = new Dictionary<char, string>();
							dictionary2 = new Dictionary<char, ActivatedAbilityEntry>();
							scrapBuffer.Clear();
							scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
							scrapBuffer.Goto(3, 0);
							scrapBuffer.Write("[ {{W|Manage Abilities}} ]", true, false, false, null, -1);
							scrapBuffer.X += 2;
							scrapBuffer.Write(string.Concat(new string[]
							{
								" ",
								Markup.Color("W", ControlManager.getCommandInputDescription("Toggle", false, false, ControlManager.InputDeviceType.Unknown, false)),
								"-",
								AbilityManagerScreen.sortModeDescription,
								" "
							}), true, false, false, null, -1);
							scrapBuffer.Goto(65, 0);
							scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}-exit ", true, false, false, null, -1);
							int num4 = 2;
							int num5 = 0;
							ConsoleTreeNode<AbilityNode>.NextVisible(list, ref num, 0);
							if (num2 < num)
							{
								num = num2;
							}
							int num6 = num;
							bool flag4 = GO.IsRealityDistortionUsable(null, null);
							while (num4 <= 21 && num6 <= list.Count)
							{
								while (num6 < list.Count && list[num6].ParentNode != null && !list[num6].ParentNode.Expand)
								{
									num6++;
								}
								if (num6 < list.Count)
								{
									if (list[num6].Category != "")
									{
										scrapBuffer.Goto(4, num4);
										if (list[num6].Expand)
										{
											scrapBuffer.Write("[-] ", true, false, false, null, -1);
										}
										else
										{
											scrapBuffer.Write("[+] ", true, false, false, null, -1);
										}
										scrapBuffer.Write("{{W|" + list[num6].Category + "}}", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Goto(3, num4);
										StringBuilder stringBuilder = new StringBuilder();
										ActivatedAbilityEntry ability = list[num6].Ability;
										scrapBuffer.Write(ability.GetUITile());
										dictionary[AbilityManager.<Show>g__getHotkeyChar|11_1(num3, ref CS$<>8__locals1)] = ability.Command;
										dictionary2[AbilityManager.<Show>g__getHotkeyChar|11_1(num3, ref CS$<>8__locals1)] = ability;
										if (!ability.Enabled)
										{
											stringBuilder.Append(string.Concat(new string[]
											{
												"  {{K|",
												AbilityManager.<Show>g__getHotkeyDisplay|11_0(num3, ref CS$<>8__locals1),
												") ",
												ability.DisplayName,
												ability.IsAttack ? " [attack]" : "",
												" [disabled]}}"
											}));
										}
										else if (ability.Cooldown <= 0)
										{
											if (ability.IsRealityDistortionBased && !flag4)
											{
												stringBuilder.Append(string.Concat(new string[]
												{
													"  {{K|",
													AbilityManager.<Show>g__getHotkeyDisplay|11_0(num3, ref CS$<>8__locals1),
													") ",
													ability.DisplayName,
													ability.IsAttack ? " [attack]" : "",
													" [astrally tethered]}}"
												}));
											}
											else
											{
												stringBuilder.Append(string.Concat(new string[]
												{
													"  ",
													AbilityManager.<Show>g__getHotkeyDisplay|11_0(num3, ref CS$<>8__locals1),
													") ",
													ability.DisplayName,
													ability.IsAttack ? " [{{W|attack}}]" : ""
												}));
											}
										}
										else if (ability.IsRealityDistortionBased && !flag4)
										{
											stringBuilder.Append(string.Concat(new string[]
											{
												"  {{K|",
												AbilityManager.<Show>g__getHotkeyDisplay|11_0(num3, ref CS$<>8__locals1),
												"}}) ",
												ability.DisplayName,
												" [{{C|",
												ability.CooldownRounds.ToString(),
												"}} turn cooldown, astrally tethered]"
											}));
										}
										else
										{
											stringBuilder.Append(string.Concat(new string[]
											{
												"  {{K|",
												AbilityManager.<Show>g__getHotkeyDisplay|11_0(num3, ref CS$<>8__locals1),
												"}}) ",
												ability.DisplayName,
												" [{{C|",
												ability.CooldownRounds.ToString(),
												"}} turn cooldown]"
											}));
										}
										if (ability.Toggleable)
										{
											if (ability.ToggleState)
											{
												stringBuilder.Append(" {{K|[{{g|Toggled on}}]}}");
											}
											else
											{
												stringBuilder.Append(" {{K|[{{y|Toggled off}}]}}");
											}
										}
										string commandInputDescription = ControlManager.getCommandInputDescription(ability.Command, true, false, ControlManager.InputDeviceType.Unknown, false);
										if (commandInputDescription != null && !string.IsNullOrEmpty(commandInputDescription))
										{
											stringBuilder.Append(" {{Y|<{{w|" + commandInputDescription + "}}>}}");
										}
										num3++;
										scrapBuffer.Write(stringBuilder.ClipExceptFormatting(40, "..."), true, false, false, null, -1);
									}
									num5 = num6;
								}
								if (num6 == num2)
								{
									scrapBuffer.Goto(2, num4);
									scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
								}
								num4++;
								num6++;
							}
							if (list.Count == 0)
							{
								goto Block_26;
							}
							if (flag3)
							{
								num2 = num5;
								flag3 = false;
							}
							else
							{
								if (list[num2].Ability != null)
								{
									TextBlock textBlock = new TextBlock(list[num2].Ability.Description, 30, 20, false, 0);
									for (int i = 0; i < textBlock.Lines.Count; i++)
									{
										scrapBuffer.Goto(45, i + 2);
										scrapBuffer.Write(textBlock.Lines[i], true, false, false, null, -1);
									}
								}
								scrapBuffer.Goto(2, 24);
								scrapBuffer.Write("[ ", true, false, false, null, -1);
								scrapBuffer.Write(Markup.Color("W", ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false)) + "-Use Ability", true, false, false, null, -1);
								if (list[num2].Ability != null)
								{
									scrapBuffer.Write(" " + Markup.Color("W", ControlManager.getCommandInputDescription("CmdInsert", true, false, ControlManager.InputDeviceType.Unknown, false)) + "-Map key", true, false, false, null, -1);
									scrapBuffer.Write(" " + Markup.Color("W", ControlManager.getCommandInputDescription("CmdDelete", true, false, ControlManager.InputDeviceType.Unknown, false)) + "-unbind", true, false, false, null, -1);
								}
								if (AbilityManager.sortMode == AbilityManagerScreen.SortMode.Custom)
								{
									scrapBuffer.Write(string.Concat(new string[]
									{
										" ",
										Markup.Color("W", ControlManager.getCommandInputDescription("V Negative", true, false, ControlManager.InputDeviceType.Unknown, false)),
										"/",
										Markup.Color("W", ControlManager.getCommandInputDescription("V Positive", true, false, ControlManager.InputDeviceType.Unknown, false)),
										"-Change Order"
									}), true, false, false, null, -1);
								}
								scrapBuffer.Write(" ]", true, false, false, null, -1);
								if (num != 0)
								{
									scrapBuffer.Goto(4, 1);
									scrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
								}
								if (num6 < list.Count)
								{
									scrapBuffer.Goto(4, 22);
									scrapBuffer.Write("{{W|<More...>}}", true, false, false, null, -1);
								}
								if (num2 > num5 && num < list.Count)
								{
									num++;
								}
								else if (flag2)
								{
									num2 = num;
									flag2 = false;
								}
								else
								{
									Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
									keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
									c = ("" + ((char)Keyboard.Char).ToString() + " ").ToLower()[0];
									if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
									{
										flag = true;
									}
									if (keys == Keys.NumPad2 && num2 < list.Count - 1)
									{
										ConsoleTreeNode<AbilityNode>.NextVisible(list, ref num2, 1);
									}
									if (keys == Keys.NumPad8 && num2 > 0)
									{
										ConsoleTreeNode<AbilityNode>.PrevVisible(list, ref num2, -1);
									}
									if (keys == Keys.NumPad4)
									{
										if (list[num2].Category != "")
										{
											list[num2].Expand = false;
										}
										else
										{
											if (list[num2].ParentNode != null)
											{
												list[num2].ParentNode.Expand = false;
											}
											ConsoleTreeNode<AbilityNode>.PrevVisible(list, ref num2, 0);
										}
									}
									if (keys == Keys.NumPad6)
									{
										list[num2].Expand = true;
									}
									if (keys == Keys.Next)
									{
										if (num5 == num2 && num6 < list.Count)
										{
											ConsoleTreeNode<AbilityNode>.NextVisible(list, ref num2, 1);
											num = Math.Min(num2, list.Count - 20);
											flag3 = true;
										}
										else
										{
											num2 = num5;
										}
									}
									if (keys == Keys.Prior)
									{
										if (num == num2 && num > 0)
										{
											ConsoleTreeNode<AbilityNode>.PrevVisible(list, ref num2, -1);
											num = Math.Max(num2 - 21, 0);
											flag2 = true;
										}
										else
										{
											num2 = num;
										}
									}
									if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Toggle")
									{
										goto Block_54;
									}
									if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Negative")
									{
										if (AbilityManager.sortMode == AbilityManagerScreen.SortMode.Class)
										{
											list.ForEach(delegate(AbilityNode n)
											{
												n.Expand = false;
											});
											ConsoleTreeNode<AbilityNode>.PrevVisible(list, ref num2, 0);
											continue;
										}
										if (num2 > 0)
										{
											goto Block_60;
										}
									}
									if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive")
									{
										if (AbilityManager.sortMode == AbilityManagerScreen.SortMode.Class)
										{
											list.ForEach(delegate(AbilityNode n)
											{
												n.Expand = true;
											});
											continue;
										}
										if (num2 < list.Count - 1)
										{
											goto Block_66;
										}
									}
									activatedAbilityEntry = null;
									if (keys != Keys.MouseEvent || !(Keyboard.CurrentMouseEvent.Event == "Command:CmdInsert") || list[num2].Ability == null)
									{
										break;
									}
									AbilityManagerScreen.HandleRebindAsync(list[num2].Ability, "Menu").Wait();
								}
							}
						}
						if (keys >= Keys.A && keys <= Keys.Z && dictionary.ContainsKey(c))
						{
							activatedAbilityEntry = dictionary2[c];
						}
						if (Keyboard.RawCode == Keys.Space)
						{
							if (list[num2].Ability != null)
							{
								activatedAbilityEntry = list[num2].Ability;
							}
							else
							{
								list[num2].Expand = !list[num2].Expand;
							}
						}
						if (activatedAbilityEntry != null)
						{
							string notUsableDescription = activatedAbilityEntry.NotUsableDescription;
							if (string.IsNullOrEmpty(notUsableDescription))
							{
								goto IL_0C73;
							}
							if (Options.GetOption("OptionAbilityCooldownWarningAsMessage", "").ToUpper() == "YES")
							{
								MessageQueue.AddPlayerMessage("You must wait {{C|" + notUsableDescription + "}} to use that ability again.", null, true);
							}
							else
							{
								Popup.Show(notUsableDescription, null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
						if (keys == Keys.Escape || keys == Keys.NumPad5)
						{
							flag = true;
						}
						if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")
						{
							flag = true;
							continue;
						}
						continue;
						Block_54:
						if (AbilityManager.sortMode == AbilityManagerScreen.SortMode.Class)
						{
							AbilityManager.sortMode = AbilityManagerScreen.SortMode.Custom;
							goto IL_0083;
						}
						AbilityManager.sortMode = AbilityManagerScreen.SortMode.Class;
						goto IL_0083;
						Block_60:
						List<string> list2 = list.Select<AbilityNode, string>(delegate(AbilityNode n)
						{
							ActivatedAbilityEntry ability3 = n.Ability;
							if (ability3 == null)
							{
								return null;
							}
							return ability3.Command;
						}).ToList<string>();
						string text = list2[num2];
						list2.RemoveAt(num2);
						list2.Insert(num2 - 1, text);
						ActivatedAbilities.PreferenceOrder = list2;
						num2--;
						goto IL_0083;
						Block_66:
						List<string> list3 = list.Select<AbilityNode, string>(delegate(AbilityNode n)
						{
							ActivatedAbilityEntry ability4 = n.Ability;
							if (ability4 == null)
							{
								return null;
							}
							return ability4.Command;
						}).ToList<string>();
						string text2 = list3[num2];
						list3.RemoveAt(num2);
						list3.Insert(num2 + 1, text2);
						ActivatedAbilities.PreferenceOrder = list3;
						num2++;
						goto IL_0083;
					}
					goto Block_82;
				}
				Block_26:
				Popup.Show("You have no abilities to manage!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				GameManager.Instance.PopGameView(false);
				return "";
				IL_0C73:
				return activatedAbilityEntry.Command;
				Block_82:
				GameManager.Instance.PopGameView(false);
				return null;
			}
			AbilityManagerScreen.Result result = default(AbilityManagerScreen.Result);
			try
			{
				result = AbilityManagerScreen.OpenAbilityManager(GO).Result;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("AbilityManagerScreen.OpenAbilityManager", ex, "game_exception");
			}
			ActivatedAbilityEntry ability2 = result.ability;
			if (ability2 == null)
			{
				return null;
			}
			return ability2.Command;
		}

		// Token: 0x0600D092 RID: 53394 RVA: 0x004D2A54 File Offset: 0x004D0C54
		[CompilerGenerated]
		internal static string <Show>g__getHotkeyDisplay|11_0(int n, ref AbilityManager.<>c__DisplayClass11_0 A_1)
		{
			if (A_1.hotkeySpread.Count <= n)
			{
				return " ";
			}
			return Keyboard.ConvertKeycodeToLowercaseChar(A_1.hotkeySpread[n]).ToString() ?? "";
		}

		// Token: 0x0600D093 RID: 53395 RVA: 0x004D2A97 File Offset: 0x004D0C97
		[CompilerGenerated]
		internal static char <Show>g__getHotkeyChar|11_1(int n, ref AbilityManager.<>c__DisplayClass11_0 A_1)
		{
			if (A_1.hotkeySpread.Count <= n)
			{
				return '\0';
			}
			return Keyboard.ConvertKeycodeToLowercaseChar(A_1.hotkeySpread[n]);
		}

		// Token: 0x04005238 RID: 21048
		public static readonly List<ActivatedAbilityEntry> PlayerAbilities = new List<ActivatedAbilityEntry>();

		// Token: 0x04005239 RID: 21049
		public static readonly Dictionary<string, ActivatedAbilityEntry> PlayerAbilitiesByCommand = new Dictionary<string, ActivatedAbilityEntry>();

		// Token: 0x0400523A RID: 21050
		public static readonly Dictionary<string, List<ActivatedAbilityEntry>> PlayerAbilitiesByClass = new Dictionary<string, List<ActivatedAbilityEntry>>();

		// Token: 0x0400523B RID: 21051
		public static readonly ReaderWriterLockSlim PlayerAbilityLock = new ReaderWriterLockSlim();
	}
}
