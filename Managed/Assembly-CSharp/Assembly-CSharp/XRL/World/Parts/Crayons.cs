using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200100B RID: 4107
	[Serializable]
	public class Crayons : IPart
	{
		// Token: 0x0600A39B RID: 41883 RVA: 0x003BC8F6 File Offset: 0x003BAAF6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A39C RID: 41884 RVA: 0x003BC8F9 File Offset: 0x003BAAF9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A39D RID: 41885 RVA: 0x003BC920 File Offset: 0x003BAB20
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Draw", "draw", "DrawWithCrayons", null, 'w', false, 0, 10, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A39E RID: 41886 RVA: 0x003BC958 File Offset: 0x003BAB58
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "DrawWithCrayons")
			{
				Cell currentCell = E.Actor.CurrentCell;
				if (currentCell == null)
				{
					return false;
				}
				if (currentCell.OnWorldMap())
				{
					return E.Actor.Fail("You cannot do that on the world map.");
				}
				string text = Popup.AskString("What do you want to draw?", "", "Sounds/UI/ui_notification", null, null, 64, 0, false, true, null);
				if (text.IsNullOrEmpty())
				{
					return false;
				}
				string text2 = Popup.ShowColorPicker("What color do you want to draw with?", 0, null, 60, false, false, null, "", false, false, false, null);
				if (text2.IsNullOrEmpty())
				{
					return false;
				}
				string text3 = XRL.UI.PickDirection.ShowPicker("Color");
				if (text3 == null)
				{
					return false;
				}
				Cell cell = currentCell.GetCellFromDirection(text3, true);
				if (cell == null)
				{
					return false;
				}
				if (this.ParentObject.GetLongProperty("Nanocrayons", 0L) == 1L)
				{
					WishResult wishResult = WishSearcher.SearchForCrayonBlueprint(text);
					if (wishResult.Result.IsNullOrEmpty())
					{
						Popup.Show("You're not talented enough to draw that.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						MetricsManager.LogEvent("NanocrayonFail:" + text);
					}
					else
					{
						cell.PlayWorldSound("Sounds/Interact/sfx_interact_crayon_draw", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						Popup.Show("You draw a pretty picture.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						Popup.Show("The picture stretches into the 3rd dimension and becomes real.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						if (cell.IsSolid())
						{
							cell = cell.getClosestPassableCell(currentCell) ?? cell;
						}
						GameObject gameObject = cell.AddObject(wishResult.Result, null, null, null, null);
						Temporary.CarryOver(this.ParentObject, gameObject, false);
						Phase.carryOver(E.Actor.PhaseMatches(gameObject) ? this.ParentObject : E.Actor, gameObject);
						Render render = gameObject.Render;
						if (render != null)
						{
							render.SetForegroundColor(text2);
						}
						this.ParentObject.Destroy(null, false, false, null);
						MetricsManager.LogEvent("Nanocrayon:" + text + ":" + text2);
						E.Actor.UseEnergy(1000, "Item Crayons", null, null, false);
						E.RequestInterfaceExit();
					}
				}
				else
				{
					MetricsManager.LogEvent("Crayon:" + text + ":" + text2);
					GameObject highestRenderLayerObject = cell.GetHighestRenderLayerObject();
					if (highestRenderLayerObject != null && highestRenderLayerObject.Render != null)
					{
						highestRenderLayerObject.Render.DetailColor = text2;
					}
					cell.PlayWorldSound("Sounds/Interact/sfx_interact_crayon_draw", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					Popup.Show("You draw a pretty picture.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					E.Actor.UseEnergy(1000, "Item Crayons", null, null, false);
					E.RequestInterfaceExit();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A39F RID: 41887 RVA: 0x003BCC34 File Offset: 0x003BAE34
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (2.in1000())
			{
				E.Object.SetLongProperty("Nanocrayons", 1L);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3A0 RID: 41888 RVA: 0x003BCC58 File Offset: 0x003BAE58
		public static string GetSubterraneanGrowthColor()
		{
			int num = Stat.Random(1, 4);
			if (num == 1)
			{
				return "r";
			}
			if (num == 2)
			{
				return "c";
			}
			if (num == 3)
			{
				return "w";
			}
			return "Y";
		}

		// Token: 0x0600A3A1 RID: 41889 RVA: 0x003BCC90 File Offset: 0x003BAE90
		public static string GetRandomColor(Random R = null)
		{
			return Crayons.BrightColors.GetRandomElement<string>(R);
		}

		// Token: 0x0600A3A2 RID: 41890 RVA: 0x003BCC9D File Offset: 0x003BAE9D
		public static string GetRandomDarkColor(Random R = null)
		{
			return Crayons.DarkColors.GetRandomElement<string>(R);
		}

		// Token: 0x0600A3A3 RID: 41891 RVA: 0x003BCCAC File Offset: 0x003BAEAC
		public static string GetRandomColorExcept(Predicate<string> test = null, Random R = null)
		{
			return Crayons.AllColors.Where<string>((string c) => !test(c)).GetRandomElement<string>(R);
		}

		// Token: 0x0600A3A4 RID: 41892 RVA: 0x003BCCE2 File Offset: 0x003BAEE2
		public static string GetRandomColorAll(Random R = null)
		{
			return Crayons.AllColors.GetRandomElement<string>(R);
		}

		// Token: 0x0600A3A5 RID: 41893 RVA: 0x003BCCF0 File Offset: 0x003BAEF0
		public static List<string> GetRandomDistinctColorsAll(int numColors)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < numColors; i++)
			{
				int num = 0;
				string randomColorAll;
				do
				{
					randomColorAll = Crayons.GetRandomColorAll(null);
					num++;
				}
				while (list.Contains(randomColorAll) && num < 20);
				list.Add(randomColorAll);
			}
			return list;
		}

		// Token: 0x040043C5 RID: 17349
		public static readonly string[] BrightColors = new string[] { "R", "W", "G", "B", "M", "C", "Y" };

		// Token: 0x040043C6 RID: 17350
		public static readonly string[] BrightColorStrings = Crayons.BrightColors.Select<string, string>((string x) => "&" + x).ToArray<string>();

		// Token: 0x040043C7 RID: 17351
		public static readonly string[] DarkColors = new string[] { "r", "w", "g", "b", "m", "c", "K" };

		// Token: 0x040043C8 RID: 17352
		public static readonly string[] AllColors = new string[]
		{
			"R", "W", "G", "B", "M", "C", "Y", "r", "w", "g",
			"b", "m", "c", "y"
		};
	}
}
