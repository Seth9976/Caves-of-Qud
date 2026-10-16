using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000F44 RID: 3908
	[Serializable]
	public class CyberneticsSchemasoft : IPart
	{
		// Token: 0x06009BE1 RID: 39905 RVA: 0x0038883C File Offset: 0x00386A3C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BE2 RID: 39906 RVA: 0x0038883F File Offset: 0x00386A3F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009BE3 RID: 39907 RVA: 0x00388842 File Offset: 0x00386A42
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == ObjectCreatedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BE4 RID: 39908 RVA: 0x00388870 File Offset: 0x00386A70
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (!this.Category.IsNullOrEmpty())
			{
				if (this.AddOn)
				{
					E.Add(string.Concat(new string[]
					{
						"You gain access to every schematic of ",
						this.GetTierDisplay(false),
						" ",
						this.Category,
						"."
					}));
				}
				else
				{
					E.Description = string.Concat(new string[]
					{
						"You gain access to every schematic of ",
						this.GetTierDisplay(false),
						" ",
						this.Category,
						"."
					});
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BE5 RID: 39909 RVA: 0x00388918 File Offset: 0x00386B18
		public override bool HandleEvent(ImplantedEvent E)
		{
			GameObject implantee = E.Implantee;
			if (implantee != null && implantee.IsActivePlayerBody())
			{
				this.RecipesAdded = "";
				E.Implantee.PlayWorldOrUISound("sfx_characterMod_tinkerSchematic_learn", null);
				foreach (TinkerData tinkerData in TinkerData.TinkerRecipes)
				{
					if (tinkerData.Category == this.Category && tinkerData.Tier <= this.MaxTier && !TinkerData.KnownRecipes.Contains(tinkerData))
					{
						if (this.RecipesAdded != "")
						{
							this.RecipesAdded += ",";
						}
						this.RecipesAdded += tinkerData.Blueprint;
						TinkerData.KnownRecipes.Add(tinkerData);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BE6 RID: 39910 RVA: 0x00388A24 File Offset: 0x00386C24
		public override bool HandleEvent(UnimplantedEvent E)
		{
			GameObject implantee = E.Implantee;
			if (implantee != null && implantee.IsActivePlayerBody() && !this.RecipesAdded.IsNullOrEmpty())
			{
				foreach (string text in this.RecipesAdded.Split(',', StringSplitOptions.None))
				{
					if (!text.IsNullOrEmpty())
					{
						TinkerData.UnlearnRecipe(text);
					}
				}
				this.RecipesAdded = "";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BE7 RID: 39911 RVA: 0x00388A93 File Offset: 0x00386C93
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.InitChip(true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BE8 RID: 39912 RVA: 0x00388AA3 File Offset: 0x00386CA3
		public override void AddedAfterCreation()
		{
			this.InitChip(false);
			base.AddedAfterCreation();
		}

		// Token: 0x06009BE9 RID: 39913 RVA: 0x00388AB2 File Offset: 0x00386CB2
		public static string GetTierDisplay(int MaxTier, bool Capitalized = false)
		{
			if (MaxTier >= 6)
			{
				if (!Capitalized)
				{
					return "high tier";
				}
				return "High Tier";
			}
			else if (MaxTier >= 4)
			{
				if (!Capitalized)
				{
					return "mid tier";
				}
				return "Mid Tier";
			}
			else
			{
				if (!Capitalized)
				{
					return "low tier";
				}
				return "Low Tier";
			}
		}

		// Token: 0x06009BEA RID: 39914 RVA: 0x00388AE8 File Offset: 0x00386CE8
		public string GetTierDisplay(bool Capitalized = false)
		{
			return CyberneticsSchemasoft.GetTierDisplay(this.MaxTier, Capitalized);
		}

		// Token: 0x06009BEB RID: 39915 RVA: 0x00388AF8 File Offset: 0x00386CF8
		public void InitChip(bool ChangeName = true)
		{
			if (this.Category == null)
			{
				List<string> list = new List<string>(new string[] { "ammo and energy cells", "pistols", "rifles", "melee weapons", "grenades", "tonics", "utility", "armor", "heavy weapons" });
				this.Category = list.GetRandomElement<string>(null);
				bool flag = false;
				foreach (TinkerData tinkerData in TinkerData.TinkerRecipes)
				{
					if (tinkerData.Category == this.Category && tinkerData.Type == "Build" && tinkerData.Tier <= this.MaxTier)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					MetricsManager.LogError("generated empty schemasoft, " + this.Category + " max tier " + this.MaxTier.ToString());
				}
				if (ChangeName && this.ParentObject != null)
				{
					this.ParentObject.Render.DisplayName = string.Concat(new string[]
					{
						"{{Y|Schemasoft [{{C|",
						Grammar.MakeTitleCase(this.Category),
						", ",
						this.GetTierDisplay(true),
						"}}]}}"
					});
				}
			}
		}

		// Token: 0x04004090 RID: 16528
		public int MaxTier = 3;

		// Token: 0x04004091 RID: 16529
		public string RecipesAdded;

		// Token: 0x04004092 RID: 16530
		public string Category;

		// Token: 0x04004093 RID: 16531
		public bool AddOn;
	}
}
