using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000944 RID: 2372
	[HasModSensitiveStaticCache]
	public static class RandomAltarBaetylRewardManager
	{
		// Token: 0x06005BB1 RID: 23473 RVA: 0x001BBBD2 File Offset: 0x001B9DD2
		public static void CheckInit()
		{
			if (RandomAltarBaetylRewardManager.ByName.Count == 0)
			{
				RandomAltarBaetylRewardManager.Init();
			}
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x001BBBE5 File Offset: 0x001B9DE5
		[ModSensitiveCacheInit]
		private static void Init()
		{
			RandomAltarBaetylRewardManager.ByName.Clear();
			Loading.LoadTask("Loading SparkingBaetyls.xml", delegate
			{
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("sparkingbaetyls", true, true))
				{
					xmlDataHelper.HandleNodes(RandomAltarBaetylRewardManager._outerNodes, null);
				}
			}, true);
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001BBC1B File Offset: 0x001B9E1B
		public static void HandleInnerNode(XmlDataHelper xml)
		{
			xml.HandleNodes(RandomAltarBaetylRewardManager._innerNodes, null);
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001BBC2C File Offset: 0x001B9E2C
		public static void HandleRewardNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			RandomAltarBaetylReward randomAltarBaetylReward = null;
			if (Reader.GetAttribute("Load") != "Merge" || !RandomAltarBaetylRewardManager.ByName.TryGetValue(attribute, out randomAltarBaetylReward))
			{
				randomAltarBaetylReward = new RandomAltarBaetylReward();
				randomAltarBaetylReward.Name = attribute;
				RandomAltarBaetylRewardManager.ByName[attribute] = randomAltarBaetylReward;
			}
			if (Reader.HasAttribute("Description"))
			{
				randomAltarBaetylReward.Description = Reader.GetAttribute("Description");
			}
			else if (randomAltarBaetylReward.Description.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Description attribute");
			}
			if (Reader.HasAttribute("Chance"))
			{
				try
				{
					randomAltarBaetylReward.Chance = Convert.ToInt32(Reader.GetAttribute("Chance"));
				}
				catch
				{
					throw new Exception(Reader.Name + " tag had invalid Chance attribute");
				}
			}
			if (Reader.HasAttribute("Weight"))
			{
				try
				{
					randomAltarBaetylReward.Weight = Convert.ToInt32(Reader.GetAttribute("Weight"));
				}
				catch
				{
					throw new Exception(Reader.Name + " tag had invalid Weight attribute");
				}
			}
			if (Reader.HasAttribute("Item"))
			{
				randomAltarBaetylReward.Item = Reader.GetAttribute("Item");
			}
			if (Reader.HasAttribute("ItemMod"))
			{
				randomAltarBaetylReward.ItemMod = Reader.GetAttribute("ItemMod");
			}
			if (Reader.HasAttribute("ItemBonusModChance"))
			{
				randomAltarBaetylReward.ItemBonusModChance = Reader.GetAttribute("ItemBonusModChance");
			}
			if (Reader.HasAttribute("ItemSetModNumber"))
			{
				randomAltarBaetylReward.ItemSetModNumber = Reader.GetAttribute("ItemSetModNumber");
			}
			if (Reader.HasAttribute("ItemMinorBestowalChance"))
			{
				randomAltarBaetylReward.ItemMinorBestowalChance = Reader.GetAttribute("ItemMinorBestowalChance");
			}
			if (Reader.HasAttribute("ItemElementBestowalChance"))
			{
				randomAltarBaetylReward.ItemElementBestowalChance = Reader.GetAttribute("ItemElementBestowalChance");
			}
			if (Reader.HasAttribute("AttributePoints"))
			{
				randomAltarBaetylReward.AttributePoints = Reader.GetAttribute("AttributePoints");
			}
			if (Reader.HasAttribute("MutationPoints"))
			{
				randomAltarBaetylReward.MutationPoints = Reader.GetAttribute("MutationPoints");
			}
			if (Reader.HasAttribute("SkillPoints"))
			{
				randomAltarBaetylReward.SkillPoints = Reader.GetAttribute("SkillPoints");
			}
			if (Reader.HasAttribute("ExperiencePoints"))
			{
				randomAltarBaetylReward.ExperiencePoints = Reader.GetAttribute("ExperiencePoints");
			}
			if (Reader.HasAttribute("LicensePoints"))
			{
				randomAltarBaetylReward.LicensePoints = Reader.GetAttribute("LicensePoints");
			}
			if (Reader.HasAttribute("Reputation"))
			{
				randomAltarBaetylReward.Reputation = Reader.GetAttribute("Reputation");
			}
			if (Reader.HasAttribute("ReputationFaction"))
			{
				randomAltarBaetylReward.ReputationFaction = Reader.GetAttribute("ReputationFaction");
			}
			if (Reader.HasAttribute("OnlyIfSkill"))
			{
				randomAltarBaetylReward.OnlyIfSkill = Reader.GetAttribute("OnlyIfSkill");
			}
			if (Reader.HasAttribute("OnlyIfPart"))
			{
				randomAltarBaetylReward.OnlyIfPart = Reader.GetAttribute("OnlyIfPart");
			}
			if (Reader.HasAttribute("OnlyIfEffect"))
			{
				randomAltarBaetylReward.OnlyIfEffect = Reader.GetAttribute("OnlyIfEffect");
			}
			if (Reader.HasAttribute("MutantOnly"))
			{
				try
				{
					randomAltarBaetylReward.MutantOnly = Convert.ToBoolean(Reader.GetAttribute("MutantOnly"));
				}
				catch
				{
					throw new Exception(Reader.Name + " tag had invalid MutantOnly attribute");
				}
			}
			if (Reader.HasAttribute("TrueKinOnly"))
			{
				try
				{
					randomAltarBaetylReward.TrueKinOnly = Convert.ToBoolean(Reader.GetAttribute("TrueKinOnly"));
				}
				catch
				{
					throw new Exception(Reader.Name + " tag had invalid TrueKinOnly attribute");
				}
			}
			Reader.DoneWithElement();
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001BBFF4 File Offset: 0x001BA1F4
		public static RandomAltarBaetylReward GetReward(string Name)
		{
			RandomAltarBaetylRewardManager.CheckInit();
			RandomAltarBaetylReward randomAltarBaetylReward;
			if (Name != null && RandomAltarBaetylRewardManager.ByName.TryGetValue(Name, out randomAltarBaetylReward))
			{
				return randomAltarBaetylReward;
			}
			return null;
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x001BC01C File Offset: 0x001BA21C
		public static void GetRewardPool(GameObject Subject, List<RandomAltarBaetylReward> Store)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return;
			}
			RandomAltarBaetylRewardManager.CheckInit();
			foreach (RandomAltarBaetylReward randomAltarBaetylReward in RandomAltarBaetylRewardManager.ByName.Values)
			{
				if (randomAltarBaetylReward.Weight > 0 && (!randomAltarBaetylReward.MutantOnly || Subject.IsMutant()) && (!randomAltarBaetylReward.TrueKinOnly || Subject.IsTrueKin()) && (randomAltarBaetylReward.OnlyIfSkill.IsNullOrEmpty() || Subject.HasSkill(randomAltarBaetylReward.OnlyIfSkill)) && (randomAltarBaetylReward.OnlyIfPart.IsNullOrEmpty() || Subject.HasPart(randomAltarBaetylReward.OnlyIfPart)) && (randomAltarBaetylReward.OnlyIfEffect.IsNullOrEmpty() || Subject.HasEffect(randomAltarBaetylReward.OnlyIfEffect)) && randomAltarBaetylReward.Chance.in100())
				{
					for (int i = 0; i < randomAltarBaetylReward.Weight; i++)
					{
						Store.Add(randomAltarBaetylReward);
					}
				}
			}
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x001BC128 File Offset: 0x001BA328
		public static List<RandomAltarBaetylReward> GetRewardPool(GameObject Subject)
		{
			List<RandomAltarBaetylReward> list = new List<RandomAltarBaetylReward>();
			RandomAltarBaetylRewardManager.GetRewardPool(Subject, list);
			return list;
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001BC143 File Offset: 0x001BA343
		public static RandomAltarBaetylReward GetRandomReward(GameObject Subject)
		{
			RandomAltarBaetylRewardManager.Work.Clear();
			RandomAltarBaetylRewardManager.GetRewardPool(Subject, RandomAltarBaetylRewardManager.Work);
			return RandomAltarBaetylRewardManager.Work.GetRandomElement<RandomAltarBaetylReward>(null);
		}

		// Token: 0x04002D50 RID: 11600
		public static Dictionary<string, RandomAltarBaetylReward> ByName = new Dictionary<string, RandomAltarBaetylReward>();

		// Token: 0x04002D51 RID: 11601
		private static readonly Dictionary<string, Action<XmlDataHelper>> _outerNodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"sparkingbaetyls",
			new Action<XmlDataHelper>(RandomAltarBaetylRewardManager.HandleInnerNode)
		} };

		// Token: 0x04002D52 RID: 11602
		private static readonly Dictionary<string, Action<XmlDataHelper>> _innerNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"rewards",
				new Action<XmlDataHelper>(RandomAltarBaetylRewardManager.HandleInnerNode)
			},
			{
				"reward",
				new Action<XmlDataHelper>(RandomAltarBaetylRewardManager.HandleRewardNode)
			}
		};

		// Token: 0x04002D53 RID: 11603
		private static List<RandomAltarBaetylReward> Work = new List<RandomAltarBaetylReward>();
	}
}
