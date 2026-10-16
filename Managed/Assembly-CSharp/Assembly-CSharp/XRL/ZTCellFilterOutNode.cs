using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using UnityEngine;
using XRL.UI;

namespace XRL
{
	// Token: 0x02000542 RID: 1346
	public class ZTCellFilterOutNode : ZoneTemplateNode
	{
		// Token: 0x06003998 RID: 14744 RVA: 0x0011C6F4 File Offset: 0x0011A8F4
		public override bool Execute(ZoneTemplateGenerationContext Context)
		{
			InfluenceMapRegion influenceMapRegion = Context.Regions.Regions[Context.CurrentRegion];
			InfluenceMapRegion influenceMapRegion2 = influenceMapRegion.deepCopy();
			List<Location2D> list = new List<Location2D>();
			string[] array = this.Filter.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				using (List<Location2D>.Enumerator enumerator = influenceMapRegion.Cells.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Location2D loc = enumerator.Current;
						Context.Z.GetCell(loc);
						string text = array[i].ToLower();
						bool flag = false;
						if (text[0] == '!')
						{
							flag = true;
							text = text.Substring(1);
						}
						List<InfluenceMapRegion> regions = Context.Regions.Regions;
						if (text.StartsWith("regionhassemantictag:"))
						{
							string[] tags = text.Split(':', StringSplitOptions.None)[1].Split(';', StringSplitOptions.None);
							if (flag)
							{
								if (!Context.currentSector.Cells.Any<Location2D>((Location2D loc) => tags.Any<string>((string tag) => Context.Z.GetCell(loc).HasSemanticTag(tag))))
								{
									list.Add(loc);
								}
							}
							else if (Context.currentSector.Cells.Any<Location2D>((Location2D loc) => tags.Any<string>((string tag) => Context.Z.GetCell(loc).HasSemanticTag(tag))))
							{
								list.Add(loc);
							}
						}
						else if (text == "reachable")
						{
							if (flag)
							{
								if (!Context.Z.GetCell(loc).IsReachable())
								{
									list.Add(loc);
								}
							}
							else if (Context.Z.GetCell(loc).IsReachable())
							{
								list.Add(loc);
							}
						}
						else if (text == "liquid")
						{
							if (flag)
							{
								if (!Context.Z.GetCell(loc).HasOpenLiquidVolume())
								{
									list.Add(loc);
								}
							}
							else if (Context.Z.GetCell(loc).HasOpenLiquidVolume())
							{
								list.Add(loc);
							}
						}
						else if (text == "furthest")
						{
							if (!Context.Regions.Regions[0].Contains(loc))
							{
								list.Add(loc);
							}
						}
						else if (text == "isolated")
						{
							if (flag)
							{
								if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => !R.ConnectsToTag("connection", null) && !R.HasTag("connected") && R.Contains(loc)))
								{
									list.Add(loc);
								}
							}
							else if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => (R.HasTag("connected") || R.ConnectsToTag("connection", null)) && R.Contains(loc)))
							{
								list.Add(loc);
							}
						}
						else if (text == "pocket")
						{
							if (flag)
							{
								if (regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => R.AdjacentRegions.Count > 0 && R.Contains(loc)))
								{
									list.Add(loc);
								}
							}
							else if (regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => R.AdjacentRegions.Count == 0 && R.Contains(loc)))
							{
								list.Add(loc);
							}
						}
						else if (text == "connection")
						{
							if (flag)
							{
								if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => !R.HasTag("connection") && R.Contains(loc)))
								{
									list.Add(loc);
								}
							}
							else if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => R.HasTag("connection") && R.Contains(loc)))
							{
								list.Add(loc);
							}
						}
						else
						{
							if (!(text == "deadend"))
							{
								Debug.LogWarning("Unknown criteria: " + text);
								return false;
							}
							if (flag)
							{
								if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => R.AdjacentRegions.Count != 1 && R.Contains(loc)))
								{
									list.Add(loc);
								}
							}
							else if (!regions.Any<InfluenceMapRegion>((InfluenceMapRegion R) => R.AdjacentRegions.Count == 1 && R.Contains(loc)))
							{
								list.Add(loc);
							}
						}
					}
				}
			}
			foreach (Location2D location2D in list)
			{
				influenceMapRegion2.removeCell(location2D);
			}
			Context.Regions.Regions[Context.CurrentRegion] = influenceMapRegion2;
			if (Options.DrawInfluenceMaps)
			{
				influenceMapRegion2.draw(true, true, "&B");
			}
			for (int j = 0; j < this.Children.Count; j++)
			{
				if (this.Children[j].TestCriteria(Context))
				{
					int num = this.Children[j].CheckChance(Context);
					for (int k = 0; k < num; k++)
					{
						if (!this.Children[j].Execute(Context))
						{
							Context.Regions.Regions[Context.CurrentRegion] = influenceMapRegion;
							return false;
						}
					}
				}
			}
			Context.Regions.Regions[Context.CurrentRegion] = influenceMapRegion;
			return true;
		}
	}
}
