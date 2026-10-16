using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Genkit;
using UnityEngine;
using XRL.Rules;

namespace XRL
{
	// Token: 0x0200053F RID: 1343
	public abstract class ZoneTemplateNode
	{
		// Token: 0x0600398E RID: 14734 RVA: 0x0011C1E4 File Offset: 0x0011A3E4
		public string VariableReplace(string input, ZoneTemplateGenerationContext Context)
		{
			foreach (string text in Context.Variables.Keys)
			{
				input = input.Replace(text, Context.Variables[text]);
			}
			return input;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x0011C24C File Offset: 0x0011A44C
		public int CheckChance(ZoneTemplateGenerationContext Context)
		{
			if (string.IsNullOrEmpty(this.Chance))
			{
				return 1;
			}
			int num = 0;
			string[] array = this.Chance.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains("."))
				{
					throw new NotImplementedException();
				}
				int num2 = Convert.ToInt32(array[i]);
				if (Stat.Random(1, 100) <= num2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x0011C2B8 File Offset: 0x0011A4B8
		public virtual bool TestCriteria(ZoneTemplateGenerationContext Context)
		{
			if (string.IsNullOrEmpty(this.Criteria))
			{
				return true;
			}
			InfluenceMapRegion influenceMapRegion = Context.Regions.Regions[Context.CurrentRegion];
			string[] array = this.Criteria.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].ToLower();
				bool flag = false;
				if (text[0] == '!')
				{
					flag = true;
					text = text.Substring(1);
				}
				if (text.StartsWith("regionhassemantictag:"))
				{
					string[] tags2 = text.Split(':', StringSplitOptions.None)[1].Split(';', StringSplitOptions.None);
					if (!Context.currentSector.Cells.Any<Location2D>((Location2D loc) => tags2.Any<string>((string tag) => Context.Z.GetCell(loc).HasSemanticTag(tag))))
					{
						return false;
					}
				}
				else if (text.StartsWith("zonehassemantictag:"))
				{
					string[] tags = text.Split(':', StringSplitOptions.None)[1].Split(';', StringSplitOptions.None);
					if (!Context.Z.GetSemanticTags().Any<string>((string t) => tags.Contains(t.ToLower())))
					{
						return false;
					}
				}
				else
				{
					if (text == "furthest")
					{
						return Context.CurrentRegion == 0;
					}
					if (text == "pocket")
					{
						if (flag)
						{
							if (influenceMapRegion.AdjacentRegions.Count == 0)
							{
								return false;
							}
						}
						else if (influenceMapRegion.AdjacentRegions.Count != 0)
						{
							return false;
						}
					}
					else if (text == "isolated")
					{
						if (flag)
						{
							if (!influenceMapRegion.ConnectsToTag("connection", null) && !influenceMapRegion.HasTag("connected"))
							{
								return false;
							}
						}
						else if (influenceMapRegion.HasTag("connected") || influenceMapRegion.ConnectsToTag("connection", null))
						{
							return false;
						}
					}
					else if (text == "connection")
					{
						if (flag)
						{
							if (influenceMapRegion.HasTag("connection"))
							{
								return false;
							}
						}
						else if (!influenceMapRegion.HasTag("connection"))
						{
							return false;
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
							if (influenceMapRegion.AdjacentRegions.Count == 1)
							{
								return false;
							}
						}
						else if (influenceMapRegion.AdjacentRegions.Count > 1)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0011C528 File Offset: 0x0011A728
		public virtual bool Execute(ZoneTemplateGenerationContext Context)
		{
			if (!(this.Style == "pickeach"))
			{
				if (this.Style == "pickone")
				{
					int num = 0;
					List<ZoneTemplateNode> list = new List<ZoneTemplateNode>();
					foreach (ZoneTemplateNode zoneTemplateNode in this.Children)
					{
						if (zoneTemplateNode.TestCriteria(Context))
						{
							list.Add(zoneTemplateNode);
							num += zoneTemplateNode.Weight;
						}
					}
					int num2 = Stat.Random(0, num - 1);
					num = 0;
					using (List<ZoneTemplateNode>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ZoneTemplateNode zoneTemplateNode2 = enumerator.Current;
							if (num2 >= num && num2 < num + zoneTemplateNode2.Weight)
							{
								if (!zoneTemplateNode2.Execute(Context))
								{
									return false;
								}
								break;
							}
							else
							{
								num += zoneTemplateNode2.Weight;
							}
						}
						return true;
					}
				}
				throw new Exception("Unknown group style " + this.Style);
			}
			for (int i = 0; i < this.Children.Count; i++)
			{
				if (this.Children[i].TestCriteria(Context))
				{
					int num3 = this.Children[i].CheckChance(Context);
					for (int j = 0; j < num3; j++)
					{
						if (!this.Children[i].Execute(Context))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0011C6B4 File Offset: 0x0011A8B4
		public virtual void Load(XmlReader Reader)
		{
		}

		// Token: 0x04001CFC RID: 7420
		public string Name;

		// Token: 0x04001CFD RID: 7421
		public string Chance;

		// Token: 0x04001CFE RID: 7422
		public string Criteria;

		// Token: 0x04001CFF RID: 7423
		public string Filter;

		// Token: 0x04001D00 RID: 7424
		public string Hint;

		// Token: 0x04001D01 RID: 7425
		public string Style = "pickeach";

		// Token: 0x04001D02 RID: 7426
		public int Weight = 1;

		// Token: 0x04001D03 RID: 7427
		public int MaxApplications;

		// Token: 0x04001D04 RID: 7428
		public List<ZoneTemplateNode> Children = new List<ZoneTemplateNode>();
	}
}
