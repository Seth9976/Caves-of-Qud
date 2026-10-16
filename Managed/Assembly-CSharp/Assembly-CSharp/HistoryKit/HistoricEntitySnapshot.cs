using System;
using System.Collections.Generic;
using System.Text;
using XRL.Annals;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace HistoryKit
{
	// Token: 0x0200036C RID: 876
	[Serializable]
	public class HistoricEntitySnapshot : IComposite
	{
		// Token: 0x06002090 RID: 8336 RVA: 0x0009FE64 File Offset: 0x0009E064
		public HistoricEntitySnapshot()
		{
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0009FE90 File Offset: 0x0009E090
		public HistoricEntitySnapshot(HistoricEntity sourceEntity)
		{
			this.entity = sourceEntity;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0009FEC3 File Offset: 0x0009E0C3
		public int Tier
		{
			get
			{
				if (this._Tier == -1)
				{
					this._Tier = XRL.World.Capabilities.Tier.Constrain(int.Parse(this.GetProperty("tier", "unknown")));
				}
				return this._Tier;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
		public int TechTier
		{
			get
			{
				if (this._TechTier == -1)
				{
					this._TechTier = (this.hasProperty("techTier") ? XRL.World.Capabilities.Tier.Constrain(int.Parse(this.GetProperty("techTier", "unknown"))) : this.Tier);
				}
				return this._TechTier;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0009FF45 File Offset: 0x0009E145
		public string Name
		{
			get
			{
				return this.GetProperty("name", "unknown");
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0009FF57 File Offset: 0x0009E157
		public string sacredThing
		{
			get
			{
				return QudHistoryHelpers.GetRandomProperty(this, this.GetProperty("defaultSacredThing", "unknown"), new string[] { "sacredThings" });
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x0009FF7D File Offset: 0x0009E17D
		public string profaneThing
		{
			get
			{
				return QudHistoryHelpers.GetRandomProperty(this, this.GetProperty("defaultProfaneThing", "unknown"), new string[] { "profaneThings" });
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0009FFA4 File Offset: 0x0009E1A4
		public void setProperty(string name, string value)
		{
			if (this.properties.ContainsKey(name))
			{
				this.properties[name] = value;
			}
			else
			{
				this.properties.Add(name, value);
			}
			if (name == "tier" || name == "techTier")
			{
				this._Tier = -1;
				this._TechTier = -1;
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000A0004 File Offset: 0x0009E204
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("---snapshot-----");
			stringBuilder.AppendLine("|");
			stringBuilder.AppendLine("|--properties---");
			stringBuilder.AppendLine("|");
			foreach (KeyValuePair<string, string> keyValuePair in this.properties)
			{
				stringBuilder.Append("| ");
				stringBuilder.Append(keyValuePair.Key + " = " + keyValuePair.Value);
			}
			if (this.listProperties.Count > 0)
			{
				stringBuilder.AppendLine("|");
				stringBuilder.AppendLine("|");
				stringBuilder.AppendLine("|");
				stringBuilder.AppendLine("|--list properties---");
				foreach (KeyValuePair<string, List<string>> keyValuePair2 in this.listProperties)
				{
					stringBuilder.AppendLine("|");
					stringBuilder.Append("| <" + keyValuePair2.Key + ">");
					foreach (string text in keyValuePair2.Value)
					{
						stringBuilder.Append("| ");
						stringBuilder.Append(text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000A01B4 File Offset: 0x0009E3B4
		public bool hasProperty(string name)
		{
			return this.properties.ContainsKey(name);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000A01C2 File Offset: 0x0009E3C2
		public bool hasListProperty(string name)
		{
			return this.listProperties.ContainsKey(name);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000A01D0 File Offset: 0x0009E3D0
		public string GetRandomElementFromListProperty(string Name, string Default = null, Random R = null)
		{
			List<string> list;
			if (!this.listProperties.TryGetValue(Name, out list) || list.Count == 0)
			{
				return Default;
			}
			if (R == null)
			{
				R = Stat.Rand;
			}
			return this.listProperties[Name][R.Next(0, this.listProperties[Name].Count)];
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000A022C File Offset: 0x0009E42C
		public List<string> GetList(string name)
		{
			List<string> list;
			if (this.listProperties.TryGetValue(name, out list))
			{
				return list;
			}
			return HistoricEntitySnapshot._empty;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000A0250 File Offset: 0x0009E450
		public string GetProperty(string name, string defaultValue = "unknown")
		{
			string text;
			if (this.properties.TryGetValue(name, out text))
			{
				return text;
			}
			return defaultValue;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000A0270 File Offset: 0x0009E470
		public HistoricPerspective getPerspective(HistoricEvent theEvent)
		{
			return theEvent.GetPerspective(this);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000A0279 File Offset: 0x0009E479
		public HistoricPerspective requirePerspective(HistoricEvent theEvent, object useFeeling = null)
		{
			return theEvent.RequirePerspective(this, useFeeling);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000A0284 File Offset: 0x0009E484
		public HistoricEvent findEventBySameEntityPerspectiveColors(string main, string support)
		{
			foreach (HistoricEvent historicEvent in this.entity.history.events)
			{
				if (historicEvent.perspectives != null && historicEvent.perspectives.ContainsKey(this.entity.id))
				{
					HistoricPerspective historicPerspective = historicEvent.perspectives[this.entity.id];
					if (historicPerspective.mainColor == main && historicPerspective.supportColor == support)
					{
						return historicEvent;
					}
				}
			}
			return null;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000A0334 File Offset: 0x0009E534
		public void supplyPerspectiveColors(HistoricPerspective perspective)
		{
			List<string> list = this.GetList("palette");
			string text = null;
			string text2 = null;
			int num = 0;
			if (list != null)
			{
				do
				{
					text = list.GetRandomElement<string>(null);
					text2 = list.GetRandomElement<string>(null);
					if (text == text2)
					{
						foreach (string text3 in list)
						{
							if (text3 != text)
							{
								text2 = text3;
								break;
							}
						}
					}
					if (this.findEventBySameEntityPerspectiveColors(text, text2) == null)
					{
						break;
					}
				}
				while (++num < 5);
			}
			else
			{
				do
				{
					text = Crayons.GetRandomColor(null);
					text2 = Crayons.GetRandomColor(null);
				}
				while (text == text2 || (this.findEventBySameEntityPerspectiveColors(text, text2) != null && ++num < 5));
			}
			perspective.mainColor = text;
			perspective.supportColor = text2;
		}

		// Token: 0x04001085 RID: 4229
		public HistoricEntity entity;

		// Token: 0x04001086 RID: 4230
		public Dictionary<string, string> properties = new Dictionary<string, string>();

		// Token: 0x04001087 RID: 4231
		public Dictionary<string, List<string>> listProperties = new Dictionary<string, List<string>>();

		// Token: 0x04001088 RID: 4232
		[NonSerialized]
		private int _Tier = -1;

		// Token: 0x04001089 RID: 4233
		[NonSerialized]
		private int _TechTier = -1;

		// Token: 0x0400108A RID: 4234
		public static List<string> _empty = new List<string>();
	}
}
