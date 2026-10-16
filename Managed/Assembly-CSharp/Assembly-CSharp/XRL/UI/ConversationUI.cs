using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Qud.UI;
using Wintellect.PowerCollections;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;

namespace XRL.UI
{
	// Token: 0x02001481 RID: 5249
	[UIView("Conversation", true, false, false, "Conversation", null, false, 0, true)]
	public class ConversationUI : IWantsTextConsoleInit
	{
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600CE97 RID: 52887 RVA: 0x004B272E File Offset: 0x004B092E
		public static int Width
		{
			get
			{
				return ConversationUI.X2 - ConversationUI.XP * 2 - ConversationUI.X1;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600CE98 RID: 52888 RVA: 0x004B2743 File Offset: 0x004B0943
		public static int Height
		{
			get
			{
				return ConversationUI.Y2 - ConversationUI.YP * 2 - ConversationUI.Y1;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600CE99 RID: 52889 RVA: 0x004B2758 File Offset: 0x004B0958
		public static int Top
		{
			get
			{
				return ConversationUI.Y1 + ConversationUI.YP;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600CE9A RID: 52890 RVA: 0x004B2765 File Offset: 0x004B0965
		public static int Bottom
		{
			get
			{
				return ConversationUI.Y2 - ConversationUI.YP;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600CE9B RID: 52891 RVA: 0x004B2772 File Offset: 0x004B0972
		public static int Left
		{
			get
			{
				return ConversationUI.X1 + ConversationUI.XP;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600CE9C RID: 52892 RVA: 0x004B277F File Offset: 0x004B097F
		public static int Right
		{
			get
			{
				return ConversationUI.X2 - ConversationUI.XP;
			}
		}

		// Token: 0x0600CE9D RID: 52893 RVA: 0x004B278C File Offset: 0x004B098C
		public void Init(TextConsole TextConsole, ScreenBuffer ScreenBuffer)
		{
			ConversationUI.TC = TextConsole;
			ConversationUI.SB = ScreenBuffer;
		}

		// Token: 0x0600CE9E RID: 52894 RVA: 0x004B279C File Offset: 0x004B099C
		public static void RenderClassic()
		{
			int top = ConversationUI.Top;
			ConversationUI.RenderableLines renderableLines = new ConversationUI.RenderableLines(ref top, ConversationUI.CurrentNode);
			List<ConversationUI.RenderableSelection> list = new List<ConversationUI.RenderableSelection>(ConversationUI.CurrentChoices.Count);
			for (int i = 0; i < ConversationUI.CurrentChoices.Count; i++)
			{
				list.Add(new ConversationUI.RenderableSelection(i, ref top, ConversationUI.CurrentChoices[i]));
			}
			int num = 0;
			int end = list.Last<ConversationUI.RenderableSelection>().End;
			do
			{
				ConversationUI.RenderBox();
				GameManager.Instance.ClearRegions();
				if (ConversationUI.SelectedChoice >= ConversationUI.CurrentChoices.Count)
				{
					ConversationUI.SelectedChoice = 0;
					renderableLines.ScrollIntoView(ref num);
				}
				else if (ConversationUI.SelectedChoice < 0)
				{
					if (renderableLines.ScrollIntoView(ref num))
					{
						ConversationUI.SelectedChoice = 0;
					}
					else
					{
						ConversationUI.SelectedChoice = ConversationUI.CurrentChoices.Count - 1;
					}
				}
				list[ConversationUI.SelectedChoice].ScrollIntoView(ref num);
				renderableLines.Render(num);
				foreach (ConversationUI.RenderableSelection renderableSelection in list)
				{
					renderableSelection.Render(num);
				}
				if (num > 0)
				{
					ConversationUI.SB.WriteAt(ConversationUI.Left, ConversationUI.Top - 1, "{{W|<more...>}}", true);
				}
				if (end - num - ConversationUI.Top > ConversationUI.Height)
				{
					ConversationUI.SB.WriteAt(ConversationUI.Left, ConversationUI.Bottom + 1, "{{W|<more...>}}", true);
				}
				if (num > 0 || end - ConversationUI.Top >= ConversationUI.Height)
				{
					ScrollbarHelper.Paint(ConversationUI.SB, ConversationUI.Top, ConversationUI.X2, ConversationUI.Height, ScrollbarHelper.Orientation.Vertical, 0, end, num, num + ConversationUI.Bottom, null);
				}
				ConversationUI.TC.DrawBuffer(ConversationUI.SB, null, false);
			}
			while (ConversationUI.Input());
		}

		// Token: 0x0600CE9F RID: 52895 RVA: 0x004B2968 File Offset: 0x004B0B68
		public static void RenderBox()
		{
			if (ConversationUI.X1 > 0 || ConversationUI.Y1 > 0 || ConversationUI.X2 < 79 || ConversationUI.Y2 < 24)
			{
				ConversationUI.SB.RenderBase();
			}
			else
			{
				ConversationUI.SB.Clear();
			}
			ConversationUI.SB.SingleBox(ConversationUI.X1, ConversationUI.Y1, ConversationUI.X2, ConversationUI.Y2, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			string title = ConversationUI.GetTitle();
			IRenderable icon = ConversationUI.Icon;
			RenderNodeEvent.Send(ConversationUI.CurrentNode, ref title, ref icon);
			if (!title.IsNullOrEmpty())
			{
				ConversationUI.SB.WriteAt(ConversationUI.Left, ConversationUI.Y1, "{{y|[ " + title + " ]}}", true);
			}
			if (Trade.Visible)
			{
				ConversationUI.SB.WriteAt(ConversationUI.Width - 15, ConversationUI.Y2, " {{keybind|" + ControlManager.getCommandInputDescription("CmdStartTrade", true, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Trade ", true);
			}
		}

		// Token: 0x0600CEA0 RID: 52896 RVA: 0x004B2A58 File Offset: 0x004B0C58
		public static bool Input()
		{
			Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
			string text = CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null);
			if (keys == Keys.Escape)
			{
				return !ConversationUI.Escape();
			}
			if (keys == Keys.Space || keys == Keys.Enter)
			{
				return !ConversationUI.Select(-1);
			}
			if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event != null)
			{
				string @event = Keyboard.CurrentMouseEvent.Event;
				if (@event == "Close")
				{
					return !ConversationUI.Escape();
				}
				int num;
				if (@event.Contains("Click:") && int.TryParse(@event.Split(':', StringSplitOptions.None)[1], out num))
				{
					return !ConversationUI.Select(num);
				}
				if (@event.Contains("Hover:") && int.TryParse(@event.Split(':', StringSplitOptions.None)[1], out num))
				{
					ConversationUI.SelectedChoice = num;
				}
				if (@event == "Command:CmdStartTrade" && Trade.Visible)
				{
					Trade.ShowScreen();
				}
			}
			else if (keys == Keys.Next)
			{
				ConversationUI.SelectedChoice = ConversationUI.CurrentChoices.Count - 1;
			}
			else if (keys == Keys.Prior)
			{
				ConversationUI.SelectedChoice = 0;
			}
			else
			{
				if (keys >= Keys.D1 && keys <= Keys.D9)
				{
					return !ConversationUI.Select(keys - Keys.D1);
				}
				if (keys >= Keys.A && keys <= Keys.Z)
				{
					return !ConversationUI.Select(9 + (keys - Keys.A));
				}
				if (text == "CmdMoveS")
				{
					ConversationUI.SelectedChoice++;
				}
				else if (text == "CmdMoveN")
				{
					ConversationUI.SelectedChoice--;
				}
			}
			return true;
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600CEA1 RID: 52897 RVA: 0x004B2BDC File Offset: 0x004B0DDC
		[Obsolete("Field renamed to CurrentConversation.")]
		public static Conversation CurrentDialogue
		{
			get
			{
				return ConversationUI.CurrentConversation;
			}
		}

		// Token: 0x0600CEA2 RID: 52898 RVA: 0x004B2BE4 File Offset: 0x004B0DE4
		public static void HaveConversation(string ConversationID, GameObject Speaker = null, GameObject Listener = null, GameObject Transmitter = null, GameObject Receiver = null, bool TradeEnabled = true, bool Physical = false, bool Mental = false)
		{
			ConversationXMLBlueprint conversationXMLBlueprint;
			if (Conversation.Blueprints.TryGetValue(ConversationID, out conversationXMLBlueprint))
			{
				ConversationUI.HaveConversation(conversationXMLBlueprint, Speaker, Listener, Transmitter, Receiver, TradeEnabled, Physical, Mental);
				return;
			}
			MetricsManager.LogError("Unknown conversation '" + ConversationID + "'");
		}

		// Token: 0x0600CEA3 RID: 52899 RVA: 0x004B2C28 File Offset: 0x004B0E28
		public static void HaveConversation(ConversationXMLBlueprint Blueprint, GameObject Speaker = null, GameObject Listener = null, GameObject Transmitter = null, GameObject Receiver = null, bool TradeEnabled = true, bool Physical = false, bool Mental = false)
		{
			GameObject player = The.Player;
			if (Listener == null)
			{
				Listener = player;
			}
			ConversationUI.Speaker = Speaker;
			ConversationUI.Listener = Listener;
			ConversationUI.Transmitter = Transmitter;
			ConversationUI.Receiver = Receiver;
			ConversationUI.Physical = Physical;
			ConversationUI.Mental = Mental;
			ConversationUI.HaveConversation(new Conversation(Blueprint), Speaker, Listener, Transmitter, Receiver, TradeEnabled, Physical, Mental);
		}

		// Token: 0x0600CEA4 RID: 52900 RVA: 0x004B2C80 File Offset: 0x004B0E80
		public static void HaveConversation(Conversation CurrentConversation, GameObject Speaker = null, GameObject Listener = null, GameObject Transmitter = null, GameObject Receiver = null, bool TradeEnabled = true, bool Physical = false, bool Mental = false)
		{
			GameObject player = The.Player;
			if (Listener == null)
			{
				Listener = player;
			}
			ConversationUI.CurrentConversation = CurrentConversation;
			ConversationUI.Speaker = Speaker;
			ConversationUI.Listener = Listener;
			ConversationUI.Transmitter = Transmitter;
			ConversationUI.Receiver = Receiver;
			ConversationUI.Physical = Physical;
			ConversationUI.Mental = Mental;
			try
			{
				GameManager.Instance.PushGameView("Conversation", false);
				if (Listener != player)
				{
					The.Game.Player.SetBody(Listener, true);
				}
				Trade.CheckEnabled(TradeEnabled, Physical, Mental);
				ConversationUI.InternalConversation();
			}
			finally
			{
				if (Listener != player && Listener.IsPlayer())
				{
					The.Game.Player.SetBody(player, true);
				}
				ConversationUI.Reset();
				GameManager.Instance.PopGameView(true);
			}
		}

		// Token: 0x0600CEA5 RID: 52901 RVA: 0x004B2D3C File Offset: 0x004B0F3C
		private static void InternalConversation()
		{
			if (!CanHaveConversationEvent.Check(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Enabled, ConversationUI.Physical, ConversationUI.Mental))
			{
				return;
			}
			if (!BeforeConversationEvent.Check(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Enabled, ConversationUI.Physical, ConversationUI.Mental))
			{
				return;
			}
			ConversationUI.CurrentConversation.Awake();
			if (!ConversationUI.CurrentConversation.Enter())
			{
				return;
			}
			GameObject gameObject = ConversationUI.Transmitter ?? ConversationUI.Speaker;
			ConversationUI.Icon = ((gameObject != null) ? gameObject.RenderForUI("Conversation", false) : null);
			ConversationUI.CurrentChoices = new List<Choice>();
			ConversationUI.StartNode = (ConversationUI.CurrentNode = ConversationUI.CurrentConversation.GetStart(null));
			if (ConversationUI.StartNode == null || !ConversationUI.StartNode.Enter())
			{
				return;
			}
			if (!BeginConversationEvent.Check(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, ConversationUI.StartNode, ref ConversationUI.Icon, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental))
			{
				return;
			}
			if (ConversationUI.Speaker == null || !ConversationUI.Speaker.FireEvent("ObjectTalking"))
			{
				return;
			}
			ConversationUI.CurrentConversation.Entered();
			ConversationUI.CurrentConversation.Prepare();
			ConversationUI.StartNode.Entered();
			ConversationUI.CheckLost();
			while (ConversationUI.CurrentNode != null)
			{
				ConversationUI.Prepare();
				if (UIManager.UseNewPopups)
				{
					ConversationUI.Render();
				}
				else
				{
					ConversationUI.RenderClassic();
				}
			}
			ConversationUI.CurrentConversation.Leave();
			ConversationUI.CurrentConversation.Left();
			AfterConversationEvent.Send(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental);
			ConversationUI.CurrentConversation.Dispose();
		}

		// Token: 0x0600CEA6 RID: 52902 RVA: 0x004B2EFC File Offset: 0x004B10FC
		public static void Reset()
		{
			ConversationUI.Speaker = null;
			ConversationUI.Listener = null;
			ConversationUI.CurrentConversation = null;
			ConversationUI.StartNode = null;
			ConversationUI.CurrentNode = null;
			ConversationUI.LastChoice = null;
			ConversationUI.CurrentChoices = null;
			ConversationUI.Icon = null;
			ConversationUI.SelectedChoice = 0;
			ConversationUI.Physical = false;
			ConversationUI.Mental = false;
			Trade.Reset();
		}

		// Token: 0x0600CEA7 RID: 52903 RVA: 0x004B2F50 File Offset: 0x004B1150
		public static void CheckLost()
		{
			Lost lost;
			if (ConversationUI.Listener.TryGetEffect<Lost>(out lost))
			{
				if (CanGiveDirectionsEvent.Check(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental))
				{
					lost.Duration = int.MinValue;
					ConversationUI.Listener.RemoveEffect(lost, true);
					Popup.Show("You ask about your location and are no longer lost.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					GaveDirectionsEvent.Send(ConversationUI.Listener, ConversationUI.Speaker, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental);
					return;
				}
			}
			else if (ConversationUI.Speaker.TryGetEffect<Lost>(out lost) && CanGiveDirectionsEvent.Check(ConversationUI.Speaker, ConversationUI.Listener, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental))
			{
				lost.Duration = int.MinValue;
				ConversationUI.Speaker.RemoveEffect(lost, true);
				Popup.Show(string.Concat(new string[]
				{
					ConversationUI.Speaker.Does("ask", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
					" about ",
					ConversationUI.Speaker.its,
					" location and ",
					ConversationUI.Speaker.GetVerb("are", false, false, false, true, false),
					" no longer lost."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				ConversationUI.Listener.AwardXP(100, -1, 0, int.MaxValue, null, null, null, null, null, null);
				GaveDirectionsEvent.Send(ConversationUI.Speaker, ConversationUI.Listener, ConversationUI.Transmitter, ConversationUI.Receiver, ConversationUI.CurrentConversation, Trade.Visible, ConversationUI.Physical, ConversationUI.Mental);
			}
		}

		// Token: 0x0600CEA8 RID: 52904 RVA: 0x004B3128 File Offset: 0x004B1328
		public static string GetTitle()
		{
			if (Options.DebugShowConversationNode)
			{
				if (ConversationUI.Speaker != null)
				{
					return string.Concat(new string[]
					{
						"{{W|",
						ConversationUI.Speaker.DisplayName,
						" - ",
						ConversationUI.CurrentConversation.ID,
						" - ",
						ConversationUI.CurrentNode.ID,
						"}}"
					});
				}
				return string.Concat(new string[]
				{
					"{{W|",
					ConversationUI.CurrentConversation.ID,
					" - ",
					ConversationUI.CurrentNode.ID,
					"}}"
				});
			}
			else
			{
				if (ConversationUI.Speaker != null)
				{
					return "{{W|" + ConversationUI.Speaker.DisplayName + "}}";
				}
				return "";
			}
		}

		// Token: 0x0600CEA9 RID: 52905 RVA: 0x004B3200 File Offset: 0x004B1400
		public static void Prepare()
		{
			Event.ResetPool(true);
			ConversationEvent.ResetPools();
			Trade.CheckVisible();
			ConversationUI.CurrentNode.Prepare();
			for (int i = ConversationUI.CurrentChoices.Count - 1; i >= 0; i--)
			{
				ConversationUI.CurrentChoices[i].Reset();
				ConversationUI.CurrentChoices.RemoveAt(i);
			}
			foreach (IConversationElement conversationElement in ConversationUI.CurrentNode.Elements)
			{
				conversationElement.Awake();
				Choice choice = conversationElement as Choice;
				if (choice != null && choice.IsVisible())
				{
					Choice lastChoice = ConversationUI.LastChoice;
					ulong? num = ((lastChoice != null) ? new ulong?(lastChoice.Hash) : null);
					ulong hash = choice.Hash;
					if (!((num.GetValueOrDefault() == hash) & (num != null)) || !HideElementEvent.Check(conversationElement, "LastChoice"))
					{
						choice.Prepare();
						ConversationUI.CurrentChoices.Add(choice);
					}
				}
			}
			Algorithms.StableSortInPlace<Choice>(ConversationUI.CurrentChoices);
		}

		// Token: 0x0600CEAA RID: 52906 RVA: 0x004B3320 File Offset: 0x004B1520
		public static void Render()
		{
			GameObject gameObject = ConversationUI.Transmitter ?? ConversationUI.Speaker;
			List<string> list = ConversationUI.CurrentChoices.Select<Choice, string>((Choice x) => x.GetDisplayText(true)).ToList<string>();
			string title = ConversationUI.GetTitle();
			IRenderable icon = ConversationUI.Icon;
			RenderNodeEvent.Send(ConversationUI.CurrentNode, ref title, ref icon);
			string text = title;
			IRenderable renderable = icon;
			string displayText = ConversationUI.CurrentNode.GetDisplayText(true);
			List<string> list2 = list;
			bool visible = Trade.Visible;
			bool allowEscape = ConversationUI.CurrentNode.AllowEscape;
			Conversation currentConversation = ConversationUI.CurrentConversation;
			int num = Popup.ShowConversation(text, renderable, displayText, list2, visible, allowEscape, currentConversation != null && currentConversation.HasState("RenderMapBehind"));
			if (num == -1)
			{
				ConversationUI.Escape();
				return;
			}
			if (num == -2)
			{
				Trade.ShowScreen();
				return;
			}
			if (num == -3)
			{
				InventoryActionEvent.Check(gameObject, ConversationUI.Listener, gameObject, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				return;
			}
			ConversationUI.Select(num);
		}

		// Token: 0x0600CEAB RID: 52907 RVA: 0x004B3404 File Offset: 0x004B1604
		public static bool Select(int Choice = -1)
		{
			if (Choice >= 0)
			{
				if (Choice >= ConversationUI.CurrentChoices.Count)
				{
					return false;
				}
				ConversationUI.SelectedChoice = Choice;
			}
			Choice choice = ConversationUI.CurrentChoices[ConversationUI.SelectedChoice];
			if (!choice.Enter())
			{
				return false;
			}
			if (!ConversationUI.CurrentNode.Leave())
			{
				return false;
			}
			Node targetNode = ConversationUI.GetTargetNode(choice);
			if (targetNode != null)
			{
				targetNode.Awake();
			}
			if (targetNode != null && !targetNode.Enter())
			{
				return false;
			}
			ConversationUI.LastChoice = ConversationUI.CurrentChoices[ConversationUI.SelectedChoice];
			choice.Entered();
			ConversationUI.CurrentNode.Left();
			ConversationUI.CurrentNode = targetNode;
			if (targetNode != null)
			{
				targetNode.Entered();
			}
			GameObject speaker = ConversationUI.Speaker;
			if (speaker != null)
			{
				string text = "ChatNodeSound";
				string text2 = "sfx_interact_chat_dialognode";
				GameObject listener = ConversationUI.Listener;
				speaker.PlayWorldSoundTag(text, text2, (listener != null) ? listener.CurrentCell : null, 0.5f);
			}
			return true;
		}

		// Token: 0x0600CEAC RID: 52908 RVA: 0x004B34D3 File Offset: 0x004B16D3
		public static bool Escape()
		{
			if (!ConversationUI.CurrentNode.AllowEscape)
			{
				return false;
			}
			if (!ConversationUI.CurrentNode.Leave())
			{
				return false;
			}
			ConversationUI.LastChoice = null;
			ConversationUI.CurrentNode.Left();
			ConversationUI.CurrentNode = null;
			return true;
		}

		// Token: 0x0600CEAD RID: 52909 RVA: 0x004B3508 File Offset: 0x004B1708
		public static Node GetTargetNode(Choice Choice)
		{
			string target = Choice.Target;
			GetTargetElementEvent.Send(Choice, ref target);
			if (target.IsNullOrEmpty())
			{
				return ConversationUI.CurrentNode;
			}
			if (target == "End")
			{
				return null;
			}
			if (target == "Start")
			{
				return ConversationUI.StartNode;
			}
			foreach (IConversationElement conversationElement in ConversationUI.CurrentConversation.Elements)
			{
				if (conversationElement.ID == target)
				{
					Node node = conversationElement as Node;
					if (node != null)
					{
						node.Awake();
						return node;
					}
				}
			}
			MetricsManager.LogError(string.Concat(new string[]
			{
				"Invalid target: '",
				target,
				"', [",
				ConversationUI.CurrentConversation.ID,
				".",
				ConversationUI.CurrentNode.ID,
				".",
				Choice.ID ?? "Unknown",
				"]"
			}));
			return null;
		}

		// Token: 0x04005030 RID: 20528
		public static TextConsole TC;

		// Token: 0x04005031 RID: 20529
		public static ScreenBuffer SB;

		// Token: 0x04005032 RID: 20530
		public static int X1 = 0;

		// Token: 0x04005033 RID: 20531
		public static int X2 = 79;

		// Token: 0x04005034 RID: 20532
		public static int XP = 2;

		// Token: 0x04005035 RID: 20533
		public static int Y1 = 0;

		// Token: 0x04005036 RID: 20534
		public static int Y2 = 24;

		// Token: 0x04005037 RID: 20535
		public static int YP = 2;

		// Token: 0x04005038 RID: 20536
		public static GameObject Speaker;

		// Token: 0x04005039 RID: 20537
		public static GameObject Listener;

		/// <summary>A communication device used by the speaker.</summary>
		// Token: 0x0400503A RID: 20538
		public static GameObject Transmitter;

		/// <summary>A communication device used by the listener.</summary>
		// Token: 0x0400503B RID: 20539
		public static GameObject Receiver;

		// Token: 0x0400503C RID: 20540
		public static Conversation CurrentConversation;

		// Token: 0x0400503D RID: 20541
		public static Node StartNode;

		// Token: 0x0400503E RID: 20542
		public static Node CurrentNode;

		// Token: 0x0400503F RID: 20543
		public static Choice LastChoice;

		// Token: 0x04005040 RID: 20544
		public static List<Choice> CurrentChoices;

		// Token: 0x04005041 RID: 20545
		public static IRenderable Icon;

		// Token: 0x04005042 RID: 20546
		public static int SelectedChoice = 0;

		// Token: 0x04005043 RID: 20547
		public static bool Physical;

		// Token: 0x04005044 RID: 20548
		public static bool Mental;

		// Token: 0x02001D9A RID: 7578
		public class RenderableLines
		{
			// Token: 0x17000FEE RID: 4078
			// (get) Token: 0x0600F4C1 RID: 62657 RVA: 0x00553C42 File Offset: 0x00551E42
			public int End
			{
				get
				{
					return this.Position + this.Lines.Count - 1;
				}
			}

			// Token: 0x17000FEF RID: 4079
			// (get) Token: 0x0600F4C2 RID: 62658 RVA: 0x00553C58 File Offset: 0x00551E58
			public virtual int MaxWidth
			{
				get
				{
					return ConversationUI.Width;
				}
			}

			// Token: 0x0600F4C3 RID: 62659 RVA: 0x00553C5F File Offset: 0x00551E5F
			public RenderableLines(ref int Position, IConversationElement Element)
			{
				this.Position = Position;
				this.Lines = StringFormat.ClipTextToArray(Element.GetDisplayText(true), this.MaxWidth, true, true);
				Position += this.Lines.Count + 1;
			}

			// Token: 0x0600F4C4 RID: 62660 RVA: 0x00553C9C File Offset: 0x00551E9C
			public virtual void Render(int Scroll)
			{
				int num = this.Position - Scroll;
				int i = 0;
				int count = this.Lines.Count;
				while (i < count)
				{
					if (num >= ConversationUI.Top)
					{
						if (num > ConversationUI.Bottom)
						{
							break;
						}
						ConversationUI.SB.WriteAt(ConversationUI.Left, num, this.Lines[i], true);
					}
					i++;
					num++;
				}
			}

			// Token: 0x0600F4C5 RID: 62661 RVA: 0x00553CFC File Offset: 0x00551EFC
			public bool ScrollIntoView(ref int Scroll)
			{
				if (this.End > ConversationUI.Bottom + Scroll)
				{
					Scroll = this.End - ConversationUI.Bottom;
					return true;
				}
				if (this.Position < ConversationUI.Top + Scroll)
				{
					Scroll = this.Position - ConversationUI.Top;
					return true;
				}
				return false;
			}

			// Token: 0x0400736F RID: 29551
			public List<string> Lines;

			// Token: 0x04007370 RID: 29552
			public int Position;
		}

		// Token: 0x02001D9B RID: 7579
		public class RenderableSelection : ConversationUI.RenderableLines
		{
			// Token: 0x17000FF0 RID: 4080
			// (get) Token: 0x0600F4C6 RID: 62662 RVA: 0x00553D4A File Offset: 0x00551F4A
			public override int MaxWidth
			{
				get
				{
					return base.MaxWidth - 5;
				}
			}

			// Token: 0x0600F4C7 RID: 62663 RVA: 0x00553D54 File Offset: 0x00551F54
			public RenderableSelection(int Index, ref int Position, IConversationElement Element)
				: base(ref Position, Element)
			{
				this.Index = Index;
			}

			// Token: 0x17000FF1 RID: 4081
			// (get) Token: 0x0600F4C8 RID: 62664 RVA: 0x00553D65 File Offset: 0x00551F65
			public char DisplayChar
			{
				get
				{
					return (char)((this.Index <= 8) ? (49 + this.Index) : (65 + (this.Index - 9)));
				}
			}

			// Token: 0x0600F4C9 RID: 62665 RVA: 0x00553D88 File Offset: 0x00551F88
			public override void Render(int Scroll)
			{
				int num = this.Position - Scroll;
				int num2 = -1;
				int num3 = -1;
				int i = 0;
				int count = this.Lines.Count;
				while (i < count)
				{
					if (num >= ConversationUI.Top)
					{
						if (num > ConversationUI.Bottom)
						{
							break;
						}
						if (num2 == -1)
						{
							num2 = num;
						}
						if (i == 0)
						{
							ConversationUI.SB.WriteAt(ConversationUI.Left, num, ((this.Index == ConversationUI.SelectedChoice) ? "{{Y|>}} " : "  ") + "{{W|" + this.DisplayChar.ToString() + "}}) ", true);
						}
						ConversationUI.SB.WriteAt(ConversationUI.Left + 5, num3 = num, this.Lines[i], true);
					}
					i++;
					num++;
				}
				if (num2 != -1)
				{
					GameManager.Instance.AddRegion(ConversationUI.Left, num2, ConversationUI.Right, num3, "Click:" + this.Index.ToString(), "Close", "Hover:" + this.Index.ToString());
				}
			}

			// Token: 0x04007371 RID: 29553
			public int Index;
		}
	}
}
