using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Qud.API;
using XRL.UI;
using XRL.Wish;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200130F RID: 4879
	[HasWishCommand]
	[HasModSensitiveStaticCache]
	public class WishMenu : IPlayerPart
	{
		// Token: 0x0600BEDC RID: 48860 RVA: 0x0045E03C File Offset: 0x0045C23C
		public static void CacheInit()
		{
			if (WishMenu.MenuItems == null)
			{
				WishMenu.MenuItems = new List<WishMenu.WishCommandXML>();
			}
			if (WishMenu.MenuItems.Count == 0)
			{
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("wishcommands", true, true))
				{
					xmlDataHelper.HandleNodes(WishMenu.nodes, null);
				}
			}
		}

		// Token: 0x0600BEDD RID: 48861 RVA: 0x0045E0B0 File Offset: 0x0045C2B0
		public static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(WishMenu.nodes, null);
		}

		/// <summary>
		///  A really quiet attempt at creating a blueprint hopefully.  The normal createSample throws loud errors
		/// </summary>
		// Token: 0x0600BEDE RID: 48862 RVA: 0x0045E0C0 File Offset: 0x0045C2C0
		private static bool TryCreateSample(string blueprint, out GameObject result)
		{
			result = null;
			bool flag;
			try
			{
				result = GameObjectFactory.Factory.CreateObject(blueprint, -9999, 0, null, null, null, "Sample", null);
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600BEDF RID: 48863 RVA: 0x0045E108 File Offset: 0x0045C308
		public static void HandleWishNode(XmlDataHelper xml)
		{
			WishMenu.WishCommandXML wishCommandXML = new WishMenu.WishCommandXML();
			wishCommandXML.Renderable = Renderable.UITile("empty", 'k', 'k', " ", '\0');
			wishCommandXML.Commands = xml.ParseAttribute<List<string>>("Commands", wishCommandXML.Commands, true, null);
			string text = wishCommandXML.Commands[0];
			try
			{
				if (text.StartsWith("goto:"))
				{
					string text2 = text.Substring(5);
					Zone zone = The.ZoneManager.GetZone(text2);
					GameObject firstObjectWithPart = The.ZoneManager.GetZone(zone.GetZoneWorld()).GetCell(zone.wX, zone.wY).GetFirstObjectWithPart("TerrainTravel");
					wishCommandXML.DisplayName = "Teleport to " + firstObjectWithPart.the + firstObjectWithPart.DisplayName;
					wishCommandXML.Renderable = new Renderable(firstObjectWithPart.RenderForUI(null, false));
				}
				else
				{
					if (text.StartsWith("item:"))
					{
						text = text.Substring(5);
					}
					if (GameObjectFactory.Factory.Blueprints.ContainsKey(text))
					{
						GameObject gameObject;
						WishMenu.TryCreateSample(text, out gameObject);
						wishCommandXML.DisplayName = "Spawn " + gameObject.an(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, true, false, null, false, null, false);
						wishCommandXML.Renderable = new Renderable(gameObject.RenderForUI(null, false));
						gameObject.Destroy(null, false, false, null);
					}
				}
			}
			catch (Exception)
			{
			}
			wishCommandXML.DisplayName = xml.ParseAttribute<string>("DisplayName", wishCommandXML.DisplayName, false, null);
			wishCommandXML.Author = xml.ParseAttribute<string>("Author", wishCommandXML.Author, false, null);
			wishCommandXML.ModName = xml.ParseAttribute<string>("ModName", wishCommandXML.ModName, false, null);
			string text3 = xml.ParseAttribute<string>("Icon", null, false, null);
			if (!string.IsNullOrEmpty(text3))
			{
				string text4 = xml.ParseAttribute<string>("ColorPair", "yw", false, null);
				wishCommandXML.Renderable = Renderable.UITile(text3, text4[0], text4[1], " ", '\0');
			}
			WishMenu.MenuItems.Add(wishCommandXML);
			xml.DoneWithElement();
		}

		// Token: 0x0600BEE0 RID: 48864 RVA: 0x0045E334 File Offset: 0x0045C534
		[WishCommand("menu", null)]
		public static void OpenWishMenu()
		{
			WishMenu.CacheInit();
			int num = Popup.PickOption("Wish Menu", null, "", "Sounds/UI/ui_notification", WishMenu.MenuItems.Select<WishMenu.WishCommandXML, string>((WishMenu.WishCommandXML a) => a.DisplayText).ToArray<string>(), null, WishMenu.MenuItems.Select<WishMenu.WishCommandXML, IRenderable>((WishMenu.WishCommandXML a) => a.Renderable).ToArray<IRenderable>(), null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
			if (num >= 0)
			{
				foreach (string text in WishMenu.MenuItems[num].Commands)
				{
					Wishing.HandleWish(The.Player, text);
				}
			}
		}

		// Token: 0x0600BEE1 RID: 48865 RVA: 0x0045E424 File Offset: 0x0045C624
		[WishCommand("revealandgoto", null)]
		public static void RevealAndGoto(string rest)
		{
			JournalMapNote mapNote = JournalAPI.GetMapNote(rest);
			if (mapNote != null)
			{
				if (!mapNote.Revealed)
				{
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
				Wishing.HandleWish(The.Player, "goto:" + mapNote.ZoneID);
			}
		}

		// Token: 0x0600BEE2 RID: 48866 RVA: 0x0045E465 File Offset: 0x0045C665
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600BEE3 RID: 48867 RVA: 0x0045E47B File Offset: 0x0045C67B
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == WishMenu.COMMAND_NAME)
			{
				WishMenu.OpenWishMenu();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004CF6 RID: 19702
		public static string COMMAND_NAME = "CmdWishMenu";

		// Token: 0x04004CF7 RID: 19703
		[ModSensitiveStaticCache(false)]
		public static List<WishMenu.WishCommandXML> MenuItems = new List<WishMenu.WishCommandXML>();

		// Token: 0x04004CF8 RID: 19704
		public static Dictionary<string, Action<XmlDataHelper>> nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"wishcommands",
				new Action<XmlDataHelper>(WishMenu.HandleNodes)
			},
			{
				"wish",
				new Action<XmlDataHelper>(WishMenu.HandleWishNode)
			}
		};

		// Token: 0x02001D2F RID: 7471
		public class WishCommandXML
		{
			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x0600F370 RID: 62320 RVA: 0x0054FF7C File Offset: 0x0054E17C
			public string DisplayText
			{
				get
				{
					string text = this.DisplayName + "\n";
					if (!string.IsNullOrEmpty(this.Author) && !string.IsNullOrEmpty(this.ModName))
					{
						text = text + Markup.Color("c", "- From \"" + this.ModName + "\" by " + this.Author) + "\n";
					}
					foreach (string text2 in this.Commands)
					{
						text = text + Markup.Color("K", "- wish: " + text2) + "\n";
					}
					return text;
				}
			}

			// Token: 0x0400720D RID: 29197
			public string DisplayName = "default";

			// Token: 0x0400720E RID: 29198
			public List<string> Commands;

			// Token: 0x0400720F RID: 29199
			public string Author;

			// Token: 0x04007210 RID: 29200
			public string ModName;

			// Token: 0x04007211 RID: 29201
			public IRenderable Renderable;
		}
	}
}
