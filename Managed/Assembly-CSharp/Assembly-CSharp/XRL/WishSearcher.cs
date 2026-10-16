using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World;

namespace XRL
{
	// Token: 0x02000511 RID: 1297
	public class WishSearcher
	{
		// Token: 0x0600377B RID: 14203 RVA: 0x0010E2D0 File Offset: 0x0010C4D0
		public static int WishResultSort(WishResult x, WishResult y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return 1;
			}
			if (y == null)
			{
				return -1;
			}
			int num = x.Distance.CompareTo(y.Distance);
			if (num != 0)
			{
				return num;
			}
			int num2 = x.NegativeMarks.CompareTo(y.NegativeMarks);
			if (num2 != 0)
			{
				return num2;
			}
			return x.AddOrder.CompareTo(y.AddOrder);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x0010E32C File Offset: 0x0010C52C
		public static WishResult SearchForWish(string Search)
		{
			List<WishResult> list = new List<WishResult>();
			list.Add(WishSearcher.SearchForZone(Search));
			list.Add(WishSearcher.SearchForBlueprint(Search));
			list.Add(WishSearcher.SearchForQuest(Search));
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x0010E388 File Offset: 0x0010C588
		public static WishResult SearchForZone(string Search)
		{
			List<WishResult> list = new List<WishResult>();
			string text = Search.ToLower();
			text = text.Replace("zone:", "");
			foreach (string text2 in WorldFactory.Factory.ZoneDisplayToID.Keys)
			{
				list.Add(new WishResult
				{
					Distance = Grammar.LevenshteinDistance(text, text2, true),
					Result = WorldFactory.Factory.ZoneDisplayToID[text2],
					Type = WishResultType.Zone,
					AddOrder = list.Count
				});
			}
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count == 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x0010E468 File Offset: 0x0010C668
		public static WishResult SearchForCrayonBlueprint(string Search)
		{
			List<WishResult> list = new List<WishResult>();
			string text = Search.ToLower();
			text = text.Replace("object:", "");
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.HasPart("Physics") && !gameObjectBlueprint.HasTag("BaseObject", false) && !gameObjectBlueprint.Name.StartsWith("Base", StringComparison.InvariantCultureIgnoreCase))
				{
					int num = Grammar.LevenshteinDistance(text, gameObjectBlueprint.Name, true);
					int num2 = 99999;
					string text2 = null;
					string partParameter = gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", null);
					if (!string.IsNullOrEmpty(partParameter))
					{
						text2 = partParameter.ToLower();
						num2 = Grammar.LevenshteinDistance(text, text2, true);
					}
					if (text2 != null && !text2.StartsWith("["))
					{
						WishResult wishResult = new WishResult();
						if (num < num2)
						{
							wishResult.Distance = num;
						}
						else
						{
							wishResult.Distance = num2;
						}
						wishResult.Result = gameObjectBlueprint.Name;
						wishResult.Type = WishResultType.Blueprint;
						wishResult.AddOrder = list.Count;
						list.Add(wishResult);
					}
				}
			}
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0010E5DC File Offset: 0x0010C7DC
		public static WishResult SearchForBlueprint(string Search)
		{
			if (Search.Contains("object:"))
			{
				Search = Search.Replace("object:", "");
			}
			else if (Search.Contains("Object:"))
			{
				Search = Search.Replace("Object:", "");
			}
			else if (Search.Contains("OBJECT:"))
			{
				Search = Search.Replace("OBJECT:", "");
			}
			if (GameObjectFactory.Factory.Blueprints.ContainsKey(Search))
			{
				return new WishResult
				{
					Distance = 0,
					Result = Search,
					Type = WishResultType.Blueprint
				};
			}
			int num = int.MaxValue;
			string text = Search.ToLower();
			List<WishResult> list = new List<WishResult>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.HasPart("Physics"))
				{
					int num2 = Math.Min(Grammar.LevenshteinDistance(text, gameObjectBlueprint.CachedNameLC, true), Grammar.LevenshteinDistance(text, gameObjectBlueprint.CachedDisplayNameStrippedLC, true));
					if (num2 <= 10)
					{
						if (num2 > 0)
						{
							num2 = Math.Min(num2, Grammar.LevenshteinDistance(Search, gameObjectBlueprint.Name, true));
						}
						if (num2 > 0)
						{
							num2 = Math.Min(num2, Grammar.LevenshteinDistance(Search, gameObjectBlueprint.CachedDisplayNameStripped, false));
						}
						if (num2 <= num)
						{
							WishResult wishResult = new WishResult();
							wishResult.Distance = num2;
							wishResult.Result = gameObjectBlueprint.Name;
							wishResult.Type = WishResultType.Blueprint;
							if (!gameObjectBlueprint.GetPartParameter<bool>("Physics", "IsReal", false))
							{
								wishResult.NegativeMarks++;
							}
							if (gameObjectBlueprint.Name.StartsWith("Base", StringComparison.InvariantCultureIgnoreCase))
							{
								wishResult.NegativeMarks++;
							}
							if (gameObjectBlueprint.HasTag("BaseObject", false))
							{
								wishResult.NegativeMarks++;
							}
							if (!gameObjectBlueprint.HasPart("Render"))
							{
								wishResult.NegativeMarks++;
							}
							if (num2 < num)
							{
								list.Clear();
								num = num2;
							}
							else
							{
								wishResult.AddOrder = list.Count;
							}
							list.Add(wishResult);
						}
					}
				}
			}
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0010E848 File Offset: 0x0010CA48
		public static WishResult SearchForQuest(string Search)
		{
			List<WishResult> list = new List<WishResult>();
			string text = Search.ToLower();
			text = text.Replace("quest:", "");
			foreach (string text2 in QuestLoader.Loader.QuestsByID.Keys)
			{
				list.Add(new WishResult
				{
					Type = WishResultType.Quest,
					Result = text2,
					Distance = Grammar.LevenshteinDistance(text, text2, true)
				});
			}
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0010E90C File Offset: 0x0010CB0C
		public static WishResult SearchForEffect(string Search)
		{
			List<WishResult> list = new List<WishResult>();
			list.Sort(new Comparison<WishResult>(WishSearcher.WishResultSort));
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}
	}
}
