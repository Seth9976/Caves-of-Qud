using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200094E RID: 2382
	public class GameObjectBlueprint
	{
		// Token: 0x06006152 RID: 24914 RVA: 0x001DAA8C File Offset: 0x001D8C8C
		public bool HasPartParameter(string Part, string Parameter)
		{
			GamePartBlueprint part = this.GetPart(Part);
			return ((part != null) ? new bool?(part.HasParameter(Parameter)) : null).GetValueOrDefault();
		}

		/// <summary>
		///     Type safe version of GetPartParameter that uses the parsed property value.
		///     If you want the unparsed string, GetPart(PartName)?.GetParameterString(Parameter) ?? Default.
		/// </summary>
		// Token: 0x06006153 RID: 24915 RVA: 0x001DAAC4 File Offset: 0x001D8CC4
		public ResultType GetPartParameter<ResultType>(string PartName, string ParameterName, ResultType Default = default(ResultType))
		{
			GamePartBlueprint part = this.GetPart(PartName);
			ResultType resultType;
			if (part != null && part.TryGetParameter<ResultType>(ParameterName, out resultType))
			{
				return resultType;
			}
			return Default;
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x001DAAEC File Offset: 0x001D8CEC
		public bool TryGetPartParameter<T>(string PartName, string ParameterName, out T Result)
		{
			GamePartBlueprint gamePartBlueprint;
			if (this.Parts.TryGetValue(PartName, out gamePartBlueprint))
			{
				return gamePartBlueprint.TryGetParameter<T>(ParameterName, out Result);
			}
			Result = default(T);
			return false;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x001DAB1F File Offset: 0x001D8D1F
		public string DisplayName()
		{
			return this.GetPartParameter<string>("Render", "DisplayName", null);
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06006156 RID: 24918 RVA: 0x001DAB34 File Offset: 0x001D8D34
		public string CachedDisplayNameStripped
		{
			get
			{
				string text;
				if ((text = this._DisplayNameStripped) == null)
				{
					text = (this._DisplayNameStripped = this.DisplayName().Strip());
				}
				return text;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06006157 RID: 24919 RVA: 0x001DAB5F File Offset: 0x001D8D5F
		public string CachedNameLC
		{
			get
			{
				if (this._NameLC == null && this.Name != null)
				{
					this._NameLC = this.Name.ToLower();
				}
				return this._NameLC;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06006158 RID: 24920 RVA: 0x001DAB88 File Offset: 0x001D8D88
		public string CachedDisplayNameStrippedLC
		{
			get
			{
				if (this._DisplayNameStrippedLC == null)
				{
					this._DisplayNameStrippedLC = this.CachedDisplayNameStripped.ToLower();
				}
				return this._DisplayNameStrippedLC;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06006159 RID: 24921 RVA: 0x001DABA9 File Offset: 0x001D8DA9
		public string CachedDisplayNameStrippedTitleCase
		{
			get
			{
				if (this._DisplayNameStrippedTitleCase == null)
				{
					this._DisplayNameStrippedTitleCase = Grammar.MakeTitleCase(this.CachedDisplayNameStripped);
				}
				return this._DisplayNameStrippedTitleCase;
			}
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x001DABCC File Offset: 0x001D8DCC
		public string GetPrimaryFaction()
		{
			string partParameter = this.GetPartParameter<string>("Brain", "Factions", null);
			if (partParameter.IsNullOrEmpty())
			{
				return null;
			}
			if (partParameter.EndsWith("-100") && !partParameter.Contains(","))
			{
				return partParameter.Substring(0, partParameter.Length - 4);
			}
			return Brain.GetPrimaryFaction(partParameter);
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001DAC28 File Offset: 0x001D8E28
		public bool InheritsFrom(string what)
		{
			string text = this.Inherits;
			while (!text.IsNullOrEmpty())
			{
				if (text == what)
				{
					return true;
				}
				text = GameObjectFactory.Factory.Blueprints[text].Inherits;
			}
			return false;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x001DAC68 File Offset: 0x001D8E68
		public int Tier
		{
			get
			{
				if (this._Tier == -999)
				{
					if (this.Tags.ContainsKey("Tier"))
					{
						this._Tier = Convert.ToInt32(this.Tags["Tier"]);
					}
					else if (this.HasStat("Level"))
					{
						int num = this.GetStat("Level", null).BaseValue / 5 + 1;
						if (num < 1)
						{
							num = 1;
						}
						if (num > 8)
						{
							num = 8;
						}
						this._Tier = num;
					}
				}
				return this._Tier;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x001DACF0 File Offset: 0x001D8EF0
		public int TechTier
		{
			get
			{
				if (this._TechTier == -999)
				{
					if (this.Tags.ContainsKey("TechTier"))
					{
						this._TechTier = Convert.ToInt32(this.Tags["TechTier"]);
					}
					else if (this.Tier != -999)
					{
						this._TechTier = this.Tier;
					}
					else if (this.HasStat("Level"))
					{
						int num = this.GetStat("Level", null).BaseValue / 5 + 1;
						if (num < 1)
						{
							num = 1;
						}
						if (num > 8)
						{
							num = 8;
						}
						this._TechTier = num;
					}
				}
				return this._TechTier;
			}
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x001DAD94 File Offset: 0x001D8F94
		public string GetBase()
		{
			if (this.IsBaseBlueprint())
			{
				return this.Name;
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (this.Inherits != null && GameObjectFactory.Factory.Blueprints.TryGetValue(this.Inherits, out gameObjectBlueprint))
			{
				return gameObjectBlueprint.GetBase();
			}
			return "BaseObject";
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x001DADE0 File Offset: 0x001D8FE0
		public string GetBaseTypeName()
		{
			string text = this.GetBase();
			if (text.StartsWith("Base"))
			{
				text = text.Substring(4);
			}
			return text;
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x001DAE0C File Offset: 0x001D900C
		public static string GetBase(string Blueprint)
		{
			GameObjectBlueprint gameObjectBlueprint;
			if (GameObjectFactory.Factory.Blueprints.TryGetValue(Blueprint, out gameObjectBlueprint))
			{
				return gameObjectBlueprint.GetBase();
			}
			return "BaseObject";
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x001DAE3C File Offset: 0x001D903C
		public bool IsBaseBlueprint()
		{
			bool flag = this._BaseBlueprint.GetValueOrDefault();
			if (this._BaseBlueprint == null)
			{
				flag = this.HasTag("BaseObject", false);
				this._BaseBlueprint = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x001DAE80 File Offset: 0x001D9080
		public bool IsExcludedFromDynamicEncounters()
		{
			bool flag = this._ExcludeFromDynamicEncounters.GetValueOrDefault();
			if (this._ExcludeFromDynamicEncounters == null)
			{
				string text;
				flag = this.HasTag("ExcludeFromDynamicEncounters", false) || (this.TryGetTag("ExcludeFromDynamicEncountersOption", out text) && (text.StartsWith("!") ? (!Options.GetOptionBool(text.Substring(1))) : Options.GetOptionBool(text)));
				this._ExcludeFromDynamicEncounters = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x001DAEFD File Offset: 0x001D90FD
		public bool TryGetTag(string Tag, out string Value)
		{
			Value = null;
			if (this.HasTag(Tag, false))
			{
				Value = this.GetTag(Tag, "");
				return true;
			}
			return false;
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x001DAF20 File Offset: 0x001D9120
		public bool DescendsFrom(string Blueprint)
		{
			GameObjectBlueprint gameObjectBlueprint;
			return !Blueprint.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(Blueprint, out gameObjectBlueprint) && this.DescendsFrom(gameObjectBlueprint);
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x001DAF54 File Offset: 0x001D9154
		public bool DescendsFrom(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return false;
			}
			GameObjectBlueprint gameObjectBlueprint = this;
			while (gameObjectBlueprint != Blueprint)
			{
				gameObjectBlueprint = gameObjectBlueprint.ShallowParent;
				if (gameObjectBlueprint == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x001DAF79 File Offset: 0x001D9179
		public void RemovePart(string ID)
		{
			if (!this.RemovedParts.Contains(ID))
			{
				this.RemovedParts.Add(ID);
			}
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x001DAF98 File Offset: 0x001D9198
		public void UpdatePart(string ID, GamePartBlueprint Part)
		{
			GamePartBlueprint gamePartBlueprint;
			if (this.Parts.TryGetValue(ID, out gamePartBlueprint))
			{
				gamePartBlueprint.CopyFrom(Part);
				return;
			}
			this.Parts.Add(ID, Part);
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x001DAFCA File Offset: 0x001D91CA
		public bool HasPart(string ID)
		{
			return this.Parts.ContainsKey(ID);
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001DAFD8 File Offset: 0x001D91D8
		public GamePartBlueprint GetPart(string ID)
		{
			GamePartBlueprint gamePartBlueprint;
			if (this.Parts.TryGetValue(ID, out gamePartBlueprint))
			{
				return gamePartBlueprint;
			}
			return null;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600616A RID: 24938 RVA: 0x001DAFF8 File Offset: 0x001D91F8
		public Dictionary<string, GamePartBlueprint> allparts
		{
			get
			{
				return this.Parts;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x001DB000 File Offset: 0x001D9200
		public GameObjectBlueprint ShallowParent
		{
			get
			{
				if (this._ShallowParent == null && !this.Inherits.IsNullOrEmpty())
				{
					GameObjectFactory.Factory.Blueprints.TryGetValue(this.Inherits, out this._ShallowParent);
				}
				return this._ShallowParent;
			}
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x001DB039 File Offset: 0x001D9239
		public GameObject createOne()
		{
			return GameObjectFactory.Factory.CreateObject(this.Name);
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x001DB04B File Offset: 0x001D924B
		public GameObject createSample()
		{
			return GameObjectFactory.Factory.CreateSampleObject(this.Name, null);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x001DB05E File Offset: 0x001D925E
		public GameObject createUnmodified()
		{
			return GameObjectFactory.Factory.CreateUnmodifiedObject(this.Name, null, null);
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x001DB072 File Offset: 0x001D9272
		public void UpdateStat(string id, Statistic stat)
		{
			this.Stats[id] = stat;
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x001DB081 File Offset: 0x001D9281
		public bool HasStat(string id)
		{
			return this.Stats.ContainsKey(id);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x001DB090 File Offset: 0x001D9290
		public Statistic GetStat(string id, Statistic def = null)
		{
			Statistic statistic = null;
			if (!this.Stats.TryGetValue(id, out statistic))
			{
				return def;
			}
			if (statistic == null)
			{
				return def;
			}
			return statistic;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x001DB0B8 File Offset: 0x001D92B8
		public int Stat(string id, int def = 0)
		{
			Statistic statistic = null;
			if (!this.Stats.TryGetValue(id, out statistic))
			{
				return def;
			}
			if (statistic == null)
			{
				return def;
			}
			return statistic.Value;
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x001DB0E4 File Offset: 0x001D92E4
		public int BaseStat(string id, int def = 0)
		{
			Statistic statistic = null;
			if (!this.Stats.TryGetValue(id, out statistic))
			{
				return def;
			}
			if (statistic == null)
			{
				return def;
			}
			return statistic.BaseValue;
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x001DB110 File Offset: 0x001D9310
		public bool HasTagOrProperty(string id)
		{
			return this.HasTag(id, false) || this.HasProperty(id);
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x001DB125 File Offset: 0x001D9325
		public bool HasProperty(string id)
		{
			return this.Props.ContainsKey(id) || this.IntProps.ContainsKey(id);
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x001DB148 File Offset: 0x001D9348
		public bool HasTag(string id, bool excludeNoInherit = false)
		{
			return this.Tags != null && this.Tags.ContainsKey(id) && (!excludeNoInherit || this.Tags[id] != "*noinherit");
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x001DB180 File Offset: 0x001D9380
		public string GetProp(string Name, string Default = null)
		{
			string text;
			if (this.Props.TryGetValue(Name, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x001DB1A0 File Offset: 0x001D93A0
		public string GetxTag(string Tag, string Value, string Default = null)
		{
			Dictionary<string, string> dictionary;
			string text;
			if (this.xTags != null && this.xTags.TryGetValue(Tag, out dictionary) && dictionary.TryGetValue(Value, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x001DB1D4 File Offset: 0x001D93D4
		public string GetxTag_CommaDelimited(string Tag, string Value, string Default = null, Random R = null)
		{
			Dictionary<string, string> dictionary;
			string text;
			if (this.xTags != null && this.xTags.TryGetValue(Tag, out dictionary) && dictionary.TryGetValue(Value, out text))
			{
				return text.GetRandomSubstring(',', false, R);
			}
			return Default;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x001DB214 File Offset: 0x001D9414
		public string GetPropertyOrTag(string id)
		{
			string text;
			if (this.Props.TryGetValue(id, out text))
			{
				return text;
			}
			int num;
			if (this.IntProps.TryGetValue(id, out num))
			{
				return num.ToString();
			}
			return this.GetTag(id, null);
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x001DB254 File Offset: 0x001D9454
		public string GetStringPropertyOrTag(string Key, string Default = null)
		{
			string text;
			if (this.Props.TryGetValue(Key, out text) || this.Tags.TryGetValue(Key, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x001DB284 File Offset: 0x001D9484
		public bool TryGetStringPropertyOrTag(string Key, out string Value)
		{
			return this.Props.TryGetValue(Key, out Value) || this.Tags.TryGetValue(Key, out Value);
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x001DB2A4 File Offset: 0x001D94A4
		public string GetTag(string Name, string Default = "")
		{
			string text;
			if (this.Tags != null && this.Tags.TryGetValue(Name, out text))
			{
				return text;
			}
			return Default;
		}

		/// <summary>
		/// Get the tile of this blueprint, painted with mask if it's a wall or fence.
		/// </summary>
		/// <param name="Mask">A bit mask indicating connecting neighbours, most significant bit is north and continues clockwise.</param>
		// Token: 0x0600617E RID: 24958 RVA: 0x001DB2CC File Offset: 0x001D94CC
		public string GetPaintedTile(int Mask = 0)
		{
			string text;
			if (this.TryGetStringPropertyOrTag("PaintedWall", out text))
			{
				text = text.GetRandomSubstring(',', false, XRL.Rules.Stat.Rnd2);
				return Event.FinalizeString(Event.NewStringBuilder(null).Append(this.GetStringPropertyOrTag("PaintedWallAtlas", "Tiles/")).Append(text)
					.Append('-')
					.AppendMask(Mask, 8)
					.Append(this.GetStringPropertyOrTag("PaintedWallExtension", ".bmp")));
			}
			if (this.TryGetStringPropertyOrTag("PaintedFence", out text))
			{
				text = text.GetRandomSubstring(',', false, XRL.Rules.Stat.Rnd2);
				return Event.FinalizeString(Event.NewStringBuilder(null).Append(this.GetStringPropertyOrTag("PaintedFenceAtlas", "Tiles/")).Append(text)
					.Append('_')
					.Append(Mask.HasBit(128) ? "n" : "")
					.Append(Mask.HasBit(8) ? "s" : "")
					.Append(Mask.HasBit(32) ? "e" : "")
					.Append(Mask.HasBit(2) ? "w" : "")
					.Append(this.GetStringPropertyOrTag("PaintedFenceExtension", ".bmp")));
			}
			return this.GetPartParameter<string>("Render", "Tile", null);
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x001DB424 File Offset: 0x001D9624
		public bool IsBodyPartOccupied(string Name)
		{
			if (this.Inventory != null)
			{
				foreach (InventoryObject inventoryObject in this.Inventory)
				{
					GameObjectBlueprint gameObjectBlueprint;
					if (GameObjectFactory.Factory.Blueprints.TryGetValue(inventoryObject.Blueprint, out gameObjectBlueprint) && (gameObjectBlueprint.HasPart("NaturalEquipment") || gameObjectBlueprint.HasTag("NaturalGear", false) || ((gameObjectBlueprint.HasTagOrProperty("AlwaysEquipAsWeapon") || gameObjectBlueprint.HasTagOrProperty("AlwaysEquipAsArmor")) && gameObjectBlueprint.HasPart("Cursed"))))
					{
						string tag = gameObjectBlueprint.GetTag("UsesSlots", null);
						if (tag != null)
						{
							if (tag.CachedCommaExpansion().Contains(Name))
							{
								return true;
							}
						}
						else if (gameObjectBlueprint.GetPartParameter<string>("MeleeWeapon", "Slot", null) == Name)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x001DB524 File Offset: 0x001D9724
		public bool ReinitializePart(IPart Part)
		{
			if (Part == null)
			{
				return false;
			}
			GamePartBlueprint gamePartBlueprint = null;
			if (this.allparts.TryGetValue(Part.GetType().Name, out gamePartBlueprint))
			{
				try
				{
					gamePartBlueprint.InitializePartInstance(Part);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("exception reinitializing part " + Part.Name, ex, "game_exception");
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x001DB58C File Offset: 0x001D978C
		public bool IsNatural()
		{
			bool flag = this._Natural.GetValueOrDefault();
			if (this._Natural == null)
			{
				flag = this.IntProps.ContainsKey("Natural") || this.Props.ContainsKey("Natural") || this.HasTag("Natural", false) || this.IntProps.ContainsKey("NaturalGear") || this.Props.ContainsKey("NaturalGear") || this.HasTag("NaturalGear", false);
				this._Natural = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x001DB628 File Offset: 0x001D9828
		public bool IsWall()
		{
			bool flag = this._Wall.GetValueOrDefault();
			if (this._Wall == null)
			{
				flag = this.Tags.ContainsKey("Wall");
				this._Wall = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x001DB66E File Offset: 0x001D986E
		public bool Seen()
		{
			if (this._Seen)
			{
				return false;
			}
			The.Game.BlueprintSeen(this.Name);
			this._Seen = true;
			return true;
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x001DB694 File Offset: 0x001D9894
		public bool HasProperName()
		{
			bool flag = this._Proper.GetValueOrDefault();
			if (this._Proper == null)
			{
				flag = this.GetxTag("Grammar", "Proper", null) == "true";
				this._Proper = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x001DB6E8 File Offset: 0x001D98E8
		public bool HasBeenSeen()
		{
			return this._Seen || (this._Seen = XRLCore.Core.Game.HasBlueprintBeenSeen(this.Name));
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x001DB71D File Offset: 0x001D991D
		public void ClearGameCache()
		{
			this._Seen = false;
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x001DB726 File Offset: 0x001D9926
		public Renderable GetRenderable()
		{
			return new Renderable(this);
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x001DB72E File Offset: 0x001D992E
		private string EscapeXML(string str)
		{
			return SecurityElement.Escape(str);
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x001DB738 File Offset: 0x001D9938
		public string BlueprintXML()
		{
			Dictionary<string, StringBuilder> dictionary = new Dictionary<string, StringBuilder>();
			foreach (string text in this.Tags.Keys)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.AppendFormat("<tag Name=\"{0}\"", this.EscapeXML(text));
				if (!string.IsNullOrEmpty(this.Tags[text]))
				{
					stringBuilder.AppendFormat(" Value=\"{0}\"", this.EscapeXML(this.Tags[text]));
				}
				stringBuilder.Append(" />");
				dictionary.Add("tag:" + text, stringBuilder);
			}
			if (this.xTags != null)
			{
				foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in this.xTags)
				{
					StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
					stringBuilder2.AppendFormat("<xtag Name=\"{0}\"", this.EscapeXML(keyValuePair.Key));
					foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair.Value)
					{
						stringBuilder2.AppendFormat(" {0}=\"{1}\"", keyValuePair2.Key, this.EscapeXML(keyValuePair2.Value));
					}
					stringBuilder2.Append(" />");
					dictionary.Add("xtag:" + keyValuePair.Key, stringBuilder2);
				}
			}
			foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair3 in this.Parts)
			{
				StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
				stringBuilder3.AppendFormat("<part Name=\"{0}\"", this.EscapeXML(keyValuePair3.Value.Name));
				foreach (KeyValuePair<string, string> keyValuePair4 in keyValuePair3.Value.GetParameterStrings())
				{
					if (keyValuePair4.Key != "Name")
					{
						stringBuilder3.AppendFormat(" {0}=\"{1}\"", keyValuePair4.Key, this.EscapeXML(keyValuePair4.Value));
					}
				}
				stringBuilder3.Append(" />");
				dictionary.Add("apart:" + keyValuePair3.Value.Name, stringBuilder3);
			}
			foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair5 in this.Mutations)
			{
				StringBuilder stringBuilder4 = Event.NewStringBuilder(null);
				stringBuilder4.AppendFormat("<mutation Name=\"{0}\"", this.EscapeXML(keyValuePair5.Value.Name));
				foreach (KeyValuePair<string, string> keyValuePair6 in keyValuePair5.Value.GetParameterStrings())
				{
					if (keyValuePair6.Key != "Name")
					{
						stringBuilder4.AppendFormat(" {0}=\"{1}\"", keyValuePair6.Key, this.EscapeXML(keyValuePair6.Value));
					}
				}
				stringBuilder4.Append(" />");
				dictionary.Add("mutation:" + keyValuePair5.Value.Name, stringBuilder4);
			}
			foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair7 in this.Skills)
			{
				StringBuilder stringBuilder5 = Event.NewStringBuilder(null);
				stringBuilder5.AppendFormat("<skill Name=\"{0}\"", this.EscapeXML(keyValuePair7.Value.Name));
				foreach (KeyValuePair<string, string> keyValuePair8 in keyValuePair7.Value.GetParameterStrings())
				{
					if (keyValuePair8.Key != "Name")
					{
						stringBuilder5.AppendFormat(" {0}=\"{1}\"", keyValuePair8.Key, this.EscapeXML(keyValuePair8.Value));
					}
				}
				stringBuilder5.Append(" />");
				dictionary.Add("skill:" + keyValuePair7.Value.Name, stringBuilder5);
			}
			foreach (KeyValuePair<string, string> keyValuePair9 in this.Props)
			{
				StringBuilder stringBuilder6 = Event.NewStringBuilder(null);
				stringBuilder6.AppendFormat("<property Name=\"{0}\" Value=\"{1}\" />", keyValuePair9.Key, this.EscapeXML(keyValuePair9.Value));
				dictionary.Add("prop:" + keyValuePair9.Key, stringBuilder6);
			}
			foreach (KeyValuePair<string, int> keyValuePair10 in this.IntProps)
			{
				StringBuilder stringBuilder7 = Event.NewStringBuilder(null);
				stringBuilder7.AppendFormat("<intproperty Name=\"{0}\" Value=\"{1}\" />", keyValuePair10.Key, keyValuePair10.Value);
				dictionary.Add("propi:" + keyValuePair10.Key, stringBuilder7);
			}
			foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair11 in this.Builders)
			{
				StringBuilder stringBuilder8 = Event.NewStringBuilder(null);
				stringBuilder8.AppendFormat("<builder Name=\"{0}\"", keyValuePair11.Value.Name);
				foreach (KeyValuePair<string, string> keyValuePair12 in keyValuePair11.Value.GetParameterStrings())
				{
					if (keyValuePair12.Key != "Name")
					{
						stringBuilder8.AppendFormat(" {0}=\"{1}\"", keyValuePair12.Key, this.EscapeXML(keyValuePair12.Value));
					}
				}
				stringBuilder8.Append(" />");
				dictionary.Add("builder:" + keyValuePair11.Value.Name, stringBuilder8);
			}
			foreach (KeyValuePair<string, Statistic> keyValuePair13 in this.Stats)
			{
				StringBuilder stringBuilder9 = Event.NewStringBuilder(null);
				bool flag = false;
				stringBuilder9.AppendFormat("<stat Name=\"{0}\"", keyValuePair13.Value.Name);
				if (keyValuePair13.Value.Boost != 0)
				{
					flag = true;
					stringBuilder9.AppendFormat(" {0}=\"{1}\"", "Boost", keyValuePair13.Value.Boost);
				}
				if (keyValuePair13.Value.Min > 0)
				{
					flag = true;
					stringBuilder9.AppendFormat(" {0}=\"{1}\"", "Min", keyValuePair13.Value.Min);
				}
				if (keyValuePair13.Value.Max > 0 && keyValuePair13.Value.Max != 2147483647)
				{
					flag = true;
					stringBuilder9.AppendFormat(" {0}=\"{1}\"", "Max", keyValuePair13.Value.Max);
				}
				if (keyValuePair13.Value.BaseValue > 0)
				{
					flag = true;
					stringBuilder9.AppendFormat(" {0}=\"{1}\"", "Value", keyValuePair13.Value.BaseValue);
				}
				if (!string.IsNullOrEmpty(keyValuePair13.Value.sValue))
				{
					flag = true;
					stringBuilder9.AppendFormat(" {0}=\"{1}\"", "sValue", this.EscapeXML(keyValuePair13.Value.sValue));
				}
				stringBuilder9.Append(" />");
				if (flag)
				{
					dictionary.Add("stat:" + keyValuePair13.Value.Name, stringBuilder9);
				}
			}
			int num = 0;
			if (this.Inventory != null)
			{
				foreach (InventoryObject inventoryObject in this.Inventory)
				{
					StringBuilder stringBuilder10 = Event.NewStringBuilder(null);
					stringBuilder10.AppendFormat("<inventoryobject Blueprint=\"{0}\"", this.EscapeXML(inventoryObject.Blueprint));
					stringBuilder10.AppendFormat(" Number=\"{0}\"", this.EscapeXML(inventoryObject.Number));
					stringBuilder10.AppendFormat(" Chance=\"{0}\"", inventoryObject.Chance);
					if (inventoryObject.NoEquip)
					{
						stringBuilder10.AppendFormat(" NoEquip=\"true\"", Array.Empty<object>());
					}
					if (inventoryObject.NoSell)
					{
						stringBuilder10.AppendFormat(" NoSell=\"true\"", Array.Empty<object>());
					}
					if (inventoryObject.NotReal)
					{
						stringBuilder10.AppendFormat(" NotReal=\"true\"", Array.Empty<object>());
					}
					if (inventoryObject.Full)
					{
						stringBuilder10.AppendFormat(" Full=\"true\"", Array.Empty<object>());
					}
					if (inventoryObject.CellChance != null)
					{
						stringBuilder10.AppendFormat(" CellChance=\"{0}\"", inventoryObject.CellChance);
					}
					if (!string.IsNullOrEmpty(inventoryObject.CellType))
					{
						stringBuilder10.AppendFormat(" CellType=\"{0}\"", inventoryObject.CellType);
					}
					if (inventoryObject.StringProperties != null)
					{
						stringBuilder10.Append(" StringProperties=\"");
						bool flag2 = true;
						foreach (KeyValuePair<string, string> keyValuePair14 in inventoryObject.StringProperties)
						{
							if (!flag2)
							{
								stringBuilder10.Append(",");
							}
							flag2 = false;
							stringBuilder10.AppendFormat("{0}:{1}", this.EscapeXML(keyValuePair14.Key), this.EscapeXML(keyValuePair14.Value));
						}
						stringBuilder10.Append("\"");
					}
					if (inventoryObject.IntProperties != null)
					{
						stringBuilder10.Append(" IntProperties=\"");
						bool flag3 = true;
						foreach (KeyValuePair<string, int> keyValuePair15 in inventoryObject.IntProperties)
						{
							if (!flag3)
							{
								stringBuilder10.Append(",");
							}
							flag3 = false;
							stringBuilder10.AppendFormat("{0}:{1}", this.EscapeXML(keyValuePair15.Key), keyValuePair15.Value);
						}
						stringBuilder10.Append("\"");
					}
					if (inventoryObject.BoostModChance)
					{
						stringBuilder10.Append(" BoostModChance=\"true\"");
					}
					if (inventoryObject.SetMods > 0)
					{
						stringBuilder10.Append(" SetMods=\"" + inventoryObject.SetMods.ToString() + "\"");
					}
					if (inventoryObject.AutoMod != null)
					{
						stringBuilder10.Append(" AutoMod=\"" + inventoryObject.AutoMod + "\"");
					}
					stringBuilder10.Append(" />");
					dictionary.Add("inventory:" + num++.ToString(), stringBuilder10);
				}
			}
			StringBuilder stringBuilder11 = Event.NewStringBuilder(null);
			stringBuilder11.AppendFormat("<object Name=\"{0}\"", this.Name);
			if (!string.IsNullOrEmpty(this.Inherits))
			{
				stringBuilder11.AppendFormat(" Inherits=\"{0}\"", this.Inherits);
			}
			stringBuilder11.Append(">").AppendLine();
			List<string> list = dictionary.Keys.ToList<string>();
			List<string> prioParts = new List<string> { "tag:BaseObject", "apart:Physics", "apart:Render", "apart:Description" };
			list.Sort(delegate(string k1, string k2)
			{
				int num2 = prioParts.FindIndex((string k) => k == k1);
				int num3 = prioParts.FindIndex((string k) => k == k2);
				if (num2 == -1)
				{
					if (num3 == -1)
					{
						return string.Compare(k1, k2);
					}
					return 1;
				}
				else
				{
					if (num3 == -1)
					{
						return -1;
					}
					return num2 - num3;
				}
			});
			foreach (string text2 in list)
			{
				stringBuilder11.Append("  ").Append(dictionary[text2]).AppendLine();
			}
			stringBuilder11.AppendLine("</object>");
			return stringBuilder11.ToString();
		}

		// Token: 0x04002DD6 RID: 11734
		public string Name = "";

		// Token: 0x04002DD7 RID: 11735
		public string Inherits = "";

		// Token: 0x04002DD8 RID: 11736
		public string Load = "";

		// Token: 0x04002DD9 RID: 11737
		public bool hasChildren;

		// Token: 0x04002DDA RID: 11738
		private string _DisplayNameStripped;

		// Token: 0x04002DDB RID: 11739
		private string _NameLC;

		// Token: 0x04002DDC RID: 11740
		private string _DisplayNameStrippedLC;

		// Token: 0x04002DDD RID: 11741
		private string _DisplayNameStrippedTitleCase;

		// Token: 0x04002DDE RID: 11742
		private int _Tier = -999;

		// Token: 0x04002DDF RID: 11743
		private int _TechTier = -999;

		// Token: 0x04002DE0 RID: 11744
		private bool? _BaseBlueprint;

		// Token: 0x04002DE1 RID: 11745
		private bool? _ExcludeFromDynamicEncounters;

		// Token: 0x04002DE2 RID: 11746
		private GameObjectBlueprint _ShallowParent;

		// Token: 0x04002DE3 RID: 11747
		public Dictionary<string, GamePartBlueprint> Parts = new Dictionary<string, GamePartBlueprint>();

		// Token: 0x04002DE4 RID: 11748
		public List<string> RemovedParts = new List<string>();

		// Token: 0x04002DE5 RID: 11749
		public Dictionary<string, GamePartBlueprint> Mutations = new Dictionary<string, GamePartBlueprint>();

		// Token: 0x04002DE6 RID: 11750
		public Dictionary<string, GamePartBlueprint> Skills = new Dictionary<string, GamePartBlueprint>();

		// Token: 0x04002DE7 RID: 11751
		public Dictionary<string, GamePartBlueprint> Builders = new Dictionary<string, GamePartBlueprint>();

		// Token: 0x04002DE8 RID: 11752
		public readonly Dictionary<string, Statistic> Stats = new Dictionary<string, Statistic>(0);

		// Token: 0x04002DE9 RID: 11753
		public Dictionary<string, string> Props = new Dictionary<string, string>(0);

		// Token: 0x04002DEA RID: 11754
		public Dictionary<string, int> IntProps = new Dictionary<string, int>(0);

		// Token: 0x04002DEB RID: 11755
		public Dictionary<string, string> Tags = new Dictionary<string, string>(0);

		// Token: 0x04002DEC RID: 11756
		public Dictionary<string, Dictionary<string, string>> xTags;

		// Token: 0x04002DED RID: 11757
		public List<InventoryObject> Inventory;

		// Token: 0x04002DEE RID: 11758
		private bool? _Natural;

		// Token: 0x04002DEF RID: 11759
		private bool? _Wall;

		// Token: 0x04002DF0 RID: 11760
		private bool _Seen;

		// Token: 0x04002DF1 RID: 11761
		private bool? _Proper;
	}
}
