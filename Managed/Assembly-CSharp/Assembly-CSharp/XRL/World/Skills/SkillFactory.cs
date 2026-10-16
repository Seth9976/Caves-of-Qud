using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;

namespace XRL.World.Skills
{
	// Token: 0x02000BA9 RID: 2985
	[HasModSensitiveStaticCache]
	[Serializable]
	public class SkillFactory : IPart
	{
		// Token: 0x060076D3 RID: 30419 RVA: 0x002CBD54 File Offset: 0x002C9F54
		public SkillEntry GetSkill(string Name)
		{
			if (Name == null)
			{
				throw new Exception("cannot retrieve skill with null name");
			}
			SkillEntry skillEntry;
			if (this.SkillList.TryGetValue(Name, out skillEntry))
			{
				return skillEntry;
			}
			throw new Exception("no such skill: " + Name);
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x002CBD94 File Offset: 0x002C9F94
		public SkillEntry GetSkillIfExists(string Name)
		{
			SkillEntry skillEntry;
			if (Name != null && this.SkillList.TryGetValue(Name, out skillEntry))
			{
				return skillEntry;
			}
			return null;
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x002CBDB8 File Offset: 0x002C9FB8
		public IBaseSkillEntry GetFirstEntry(string Class, IBaseSkillEntry Default = null)
		{
			List<IBaseSkillEntry> list;
			if (!this.EntriesByClass.TryGetValue(Class, out list))
			{
				return Default;
			}
			return list.First<IBaseSkillEntry>();
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x002CBDE0 File Offset: 0x002C9FE0
		public bool TryGetFirstEntry(string Class, out IBaseSkillEntry Entry)
		{
			List<IBaseSkillEntry> list;
			if (!this.EntriesByClass.TryGetValue(Class, out list))
			{
				Entry = null;
				return false;
			}
			Entry = list.First<IBaseSkillEntry>();
			return true;
		}

		// Token: 0x060076D7 RID: 30423 RVA: 0x002CBE0C File Offset: 0x002CA00C
		public static List<PowerEntry> GetUnknownPowersFor(GameObject Object, bool ReadOnly = true)
		{
			List<PowerEntry> list = (ReadOnly ? SkillFactory.ReadOnlyPowers : new List<PowerEntry>());
			list.Clear();
			foreach (PowerEntry powerEntry in SkillFactory.GetPowers())
			{
				if (!Object.HasPart(powerEntry.Class))
				{
					list.Add(powerEntry);
				}
			}
			return list;
		}

		// Token: 0x060076D8 RID: 30424 RVA: 0x002CBE84 File Offset: 0x002CA084
		public static List<SkillEntry> GetSkillPool(GameObject Object = null, Predicate<SkillEntry> Filter = null, int MinCost = 0, int MaxCost = 2147483647, bool CheckRequirements = false, bool IncludeInitiatory = false, bool ReadOnly = true)
		{
			List<SkillEntry> list = (ReadOnly ? SkillFactory.ReadOnlySkills : new List<SkillEntry>());
			list.Clear();
			foreach (SkillEntry skillEntry in SkillFactory.GetSkills())
			{
				if ((Object == null || (!Object.HasPart(skillEntry.Class) && (!CheckRequirements || skillEntry.MeetsRequirements(Object, false)))) && !skillEntry.ExcludeFromPool && skillEntry.Cost <= MaxCost && skillEntry.Cost >= MinCost && (!skillEntry.Initiatory || IncludeInitiatory) && (Filter == null || Filter(skillEntry)))
				{
					list.Add(skillEntry);
				}
			}
			return list;
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x002CBF40 File Offset: 0x002CA140
		public static List<PowerEntry> GetPowerPool(GameObject Object = null, Predicate<PowerEntry> Filter = null, int MinCost = 0, int MaxCost = 2147483647, bool CheckRequirements = false, bool IncludeInitiatory = false, bool ReadOnly = true)
		{
			List<PowerEntry> list = (ReadOnly ? SkillFactory.ReadOnlyPowers : new List<PowerEntry>());
			list.Clear();
			foreach (PowerEntry powerEntry in SkillFactory.GetPowers())
			{
				if ((Object == null || (!Object.HasPart(powerEntry.Class) && (!CheckRequirements || powerEntry.MeetsRequirements(Object, false)))) && !powerEntry.ExcludeFromPool && powerEntry.Cost <= MaxCost && powerEntry.Cost >= MinCost && (!powerEntry.IsSkillInitiatory || (IncludeInitiatory && Object != null && powerEntry.MeetsRequirements(Object, false))) && (Filter == null || Filter(powerEntry)))
				{
					list.Add(powerEntry);
				}
			}
			return list;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060076DA RID: 30426 RVA: 0x002CC008 File Offset: 0x002CA208
		public static SkillFactory Factory
		{
			get
			{
				if (SkillFactory._Factory == null)
				{
					SkillFactory._Factory = new SkillFactory();
					Loading.LoadTask("Loading Skills.xml", new Action(SkillFactory._Factory.LoadSkills), true);
				}
				return SkillFactory._Factory;
			}
		}

		// Token: 0x060076DB RID: 30427 RVA: 0x002CC03C File Offset: 0x002CA23C
		protected SkillFactory()
		{
			this.rootNode = new Dictionary<string, Action<XmlDataHelper>> { 
			{
				"skills",
				new Action<XmlDataHelper>(this.HandleSkillsNode)
			} };
			this.skillsNodeChildren = new Dictionary<string, Action<XmlDataHelper>> { 
			{
				"skill",
				new Action<XmlDataHelper>(this.HandleSkillNode)
			} };
			this.skillNodeChildren = new Dictionary<string, Action<XmlDataHelper>> { 
			{
				"power",
				new Action<XmlDataHelper>(this.HandlePowerNode)
			} };
		}

		// Token: 0x060076DC RID: 30428 RVA: 0x002CC0E4 File Offset: 0x002CA2E4
		private void LoadSkills()
		{
			this.SkillList = new Dictionary<string, SkillEntry>();
			this.SkillByClass = new Dictionary<string, SkillEntry>();
			this.PowersByClass = new Dictionary<string, PowerEntry>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Skills", true, true))
			{
				try
				{
					xmlDataHelper.HandleNodes(this.rootNode, null);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
			foreach (KeyValuePair<string, SkillEntry> keyValuePair in this.SkillList)
			{
				if (!this.SkillByClass.ContainsKey(keyValuePair.Value.Class))
				{
					this.SkillByClass.Add(keyValuePair.Value.Class, keyValuePair.Value);
				}
				foreach (KeyValuePair<string, PowerEntry> keyValuePair2 in keyValuePair.Value.Powers)
				{
					if (keyValuePair2.Value.Class != null && !this.PowersByClass.ContainsKey(keyValuePair2.Value.Class))
					{
						this.PowersByClass.Add(keyValuePair2.Value.Class, keyValuePair2.Value);
					}
				}
			}
			this.EntriesByClass = new Dictionary<string, List<IBaseSkillEntry>>(this.SkillByClass.Count + this.PowersByClass.Count);
			foreach (KeyValuePair<string, SkillEntry> keyValuePair3 in this.SkillByClass)
			{
				List<IBaseSkillEntry> list;
				if (!this.EntriesByClass.TryGetValue(keyValuePair3.Key, out list))
				{
					list = (this.EntriesByClass[keyValuePair3.Key] = new List<IBaseSkillEntry>(1));
				}
				list.Add(keyValuePair3.Value);
			}
			foreach (KeyValuePair<string, PowerEntry> keyValuePair4 in this.PowersByClass)
			{
				List<IBaseSkillEntry> list2;
				if (!this.EntriesByClass.TryGetValue(keyValuePair4.Key, out list2))
				{
					list2 = (this.EntriesByClass[keyValuePair4.Key] = new List<IBaseSkillEntry>(1));
				}
				list2.Add(keyValuePair4.Value);
			}
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x002CC398 File Offset: 0x002CA598
		public void HandleSkillsNode(XmlDataHelper xml)
		{
			xml.HandleNodes(this.skillsNodeChildren, null);
		}

		// Token: 0x060076DE RID: 30430 RVA: 0x002CC3A8 File Offset: 0x002CA5A8
		public void HandleSkillNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("Name", null, true, null);
			if (text.StartsWith('-'))
			{
				text = text.TrimStart('-');
				MetricsManager.LogPotentialModError(xml.modInfo, DataManager.SanitizePathForDisplay(xml.BaseURI) + ":" + xml.LineNumber.ToString() + ": Entry removal discontinued, set Hidden attribute instead.");
			}
			if (!this.SkillList.TryGetValue(text, out this.NewSkill))
			{
				this.NewSkill = new SkillEntry
				{
					Name = text,
					Cost = -999
				};
				this.SkillList.Add(text, this.NewSkill);
			}
			this.NewSkill.HandleXMLNode(xml);
			xml.HandleNodes(this.skillNodeChildren, null);
			this.NewSkill = null;
		}

		// Token: 0x060076DF RID: 30431 RVA: 0x002CC470 File Offset: 0x002CA670
		public void HandlePowerNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("Name", null, true, null);
			if (text.StartsWith('-'))
			{
				text = text.TrimStart('-');
				MetricsManager.LogPotentialModError(xml.modInfo, DataManager.SanitizePathForDisplay(xml.BaseURI) + ":" + xml.LineNumber.ToString() + ": Entry removal discontinued, set Hidden attribute instead.");
			}
			PowerEntry powerEntry;
			if (!this.NewSkill.Powers.TryGetValue(text, out powerEntry))
			{
				powerEntry = new PowerEntry
				{
					Name = text,
					Cost = -999,
					ParentSkill = this.NewSkill,
					Hidden = this.NewSkill.Hidden,
					ExcludeFromPool = this.NewSkill.ExcludeFromPool
				};
				if (this.NewSkill.Initiatory)
				{
					PowerEntry powerEntry2 = powerEntry;
					PowerEntry powerEntry3 = this.NewSkill.PowerList.LastOrDefault<PowerEntry>();
					powerEntry2.Requires = ((powerEntry3 != null) ? powerEntry3.Class : null);
				}
				this.NewSkill.Add(powerEntry);
			}
			powerEntry.HandleXMLNode(xml);
			xml.DoneWithElement();
		}

		// Token: 0x060076E0 RID: 30432 RVA: 0x002CC578 File Offset: 0x002CA778
		public static List<SkillEntry> GetSkills()
		{
			if (SkillFactory.Skills == null)
			{
				SkillFactory.Skills = new List<SkillEntry>();
				foreach (KeyValuePair<string, SkillEntry> keyValuePair in SkillFactory.Factory.SkillList)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Value.Class))
					{
						SkillFactory.Skills.Add(keyValuePair.Value);
					}
				}
			}
			return SkillFactory.Skills;
		}

		// Token: 0x060076E1 RID: 30433 RVA: 0x002CC604 File Offset: 0x002CA804
		public static List<PowerEntry> GetPowers()
		{
			if (SkillFactory.Powers == null)
			{
				SkillFactory.Powers = new List<PowerEntry>();
				foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillList.Values)
				{
					foreach (PowerEntry powerEntry in skillEntry.Powers.Values)
					{
						if (!string.IsNullOrEmpty(powerEntry.Class))
						{
							SkillFactory.Powers.Add(powerEntry);
						}
					}
				}
			}
			return SkillFactory.Powers;
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x002CC6C8 File Offset: 0x002CA8C8
		public static string GetRandomPowerClass()
		{
			PowerEntry randomElement = SkillFactory.GetPowers().GetRandomElement<PowerEntry>(null);
			if (randomElement == null)
			{
				return null;
			}
			return randomElement.Class;
		}

		// Token: 0x060076E3 RID: 30435 RVA: 0x002CC6E0 File Offset: 0x002CA8E0
		public static string GetSkillOrPowerName(string ClassName)
		{
			SkillEntry skillEntry;
			if (SkillFactory.Factory.SkillByClass.TryGetValue(ClassName, out skillEntry))
			{
				return skillEntry.Name;
			}
			PowerEntry powerEntry;
			if (SkillFactory.Factory.PowersByClass.TryGetValue(ClassName, out powerEntry))
			{
				return powerEntry.Name;
			}
			return ClassName;
		}

		// Token: 0x040036B3 RID: 14003
		public Dictionary<string, SkillEntry> SkillList = new Dictionary<string, SkillEntry>();

		// Token: 0x040036B4 RID: 14004
		public Dictionary<string, SkillEntry> SkillByClass = new Dictionary<string, SkillEntry>();

		// Token: 0x040036B5 RID: 14005
		public Dictionary<string, PowerEntry> PowersByClass = new Dictionary<string, PowerEntry>();

		// Token: 0x040036B6 RID: 14006
		public Dictionary<string, List<IBaseSkillEntry>> EntriesByClass = new Dictionary<string, List<IBaseSkillEntry>>();

		// Token: 0x040036B7 RID: 14007
		[ModSensitiveStaticCache(false)]
		[NonSerialized]
		private static List<SkillEntry> Skills;

		// Token: 0x040036B8 RID: 14008
		[ModSensitiveStaticCache(false)]
		[NonSerialized]
		private static List<PowerEntry> Powers;

		// Token: 0x040036B9 RID: 14009
		[NonSerialized]
		private static readonly List<SkillEntry> ReadOnlySkills = new List<SkillEntry>();

		// Token: 0x040036BA RID: 14010
		[NonSerialized]
		private static readonly List<PowerEntry> ReadOnlyPowers = new List<PowerEntry>();

		// Token: 0x040036BB RID: 14011
		[ModSensitiveStaticCache(false)]
		private static SkillFactory _Factory;

		// Token: 0x040036BC RID: 14012
		protected Dictionary<string, Action<XmlDataHelper>> rootNode;

		// Token: 0x040036BD RID: 14013
		protected Dictionary<string, Action<XmlDataHelper>> skillsNodeChildren;

		// Token: 0x040036BE RID: 14014
		protected Dictionary<string, Action<XmlDataHelper>> skillNodeChildren;

		// Token: 0x040036BF RID: 14015
		protected SkillEntry NewSkill;
	}
}
