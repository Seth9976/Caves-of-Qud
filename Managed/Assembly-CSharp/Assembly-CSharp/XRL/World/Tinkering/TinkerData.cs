using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.Language;
using XRL.UI;
using XRL.Wish;
using XRL.World.Parts;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B35 RID: 2869
	[HasWishCommand]
	[HasGameBasedStaticCache]
	[GenerateSerializationPartial]
	[Serializable]
	public sealed class TinkerData : IComposite
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x002BAA86 File Offset: 0x002B8C86
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x002BAA8C File Offset: 0x002B8C8C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.DisplayName);
			Writer.WriteOptimized(this.Blueprint);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this.Type);
			Writer.WriteOptimized(this.Tier);
			Writer.WriteOptimized(this.Cost);
			Writer.WriteOptimized(this.Ingredient);
			Writer.WriteOptimized(this.DescriptionLineCount);
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x002BAAFC File Offset: 0x002B8CFC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Read(SerializationReader Reader)
		{
			this.DisplayName = Reader.ReadOptimizedString();
			this.Blueprint = Reader.ReadOptimizedString();
			this.Category = Reader.ReadOptimizedString();
			this.Type = Reader.ReadOptimizedString();
			this.Tier = Reader.ReadOptimizedInt32();
			this.Cost = Reader.ReadOptimizedString();
			this.Ingredient = Reader.ReadOptimizedString();
			this.DescriptionLineCount = Reader.ReadOptimizedInt32();
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060073CF RID: 29647 RVA: 0x002BAB69 File Offset: 0x002B8D69
		public string PartName
		{
			get
			{
				if (this._PartName == null)
				{
					this._PartName = this.Blueprint.Replace("[mod]", "");
				}
				return this._PartName;
			}
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x002BAB94 File Offset: 0x002B8D94
		public static TinkerData LegacyRead(SerializationReader reader)
		{
			return new TinkerData
			{
				DisplayName = reader.ReadString(),
				Blueprint = reader.ReadString(),
				Category = reader.ReadString(),
				Type = reader.ReadString(),
				Tier = reader.ReadInt32(),
				Cost = reader.ReadString(),
				Ingredient = reader.ReadString(),
				DescriptionLineCount = reader.ReadInt32()
			};
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060073D1 RID: 29649 RVA: 0x002BAC08 File Offset: 0x002B8E08
		public string UICategory
		{
			get
			{
				if (!TinkerData.InventoryCateogires.ContainsKey(this))
				{
					if (GameObjectFactory.Factory.Blueprints.ContainsKey(this.Blueprint))
					{
						TinkerData.InventoryCateogires.Add(this, TinkeringHelpers.TinkeredItemInventoryCategory(this.Blueprint));
					}
					else
					{
						TinkerData.InventoryCateogires.Add(this, "<invalid blueprint>");
					}
				}
				return TinkerData.InventoryCateogires[this];
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060073D2 RID: 29650 RVA: 0x002BAC6C File Offset: 0x002B8E6C
		public string LongDisplayName
		{
			get
			{
				if (!TinkerData.DataNames.ContainsKey(this))
				{
					if (GameObjectFactory.Factory.Blueprints.ContainsKey(this.Blueprint))
					{
						TinkerData.DataNames.Add(this, TinkeringHelpers.TinkeredItemDisplayName(this.Blueprint));
					}
					else
					{
						TinkerData.DataNames.Add(this, "<invalid blueprint>");
					}
				}
				return TinkerData.DataNames[this];
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060073D3 RID: 29651 RVA: 0x002BACD0 File Offset: 0x002B8ED0
		public string UnclippedDescription
		{
			get
			{
				if (!TinkerData.UnclippedDataDescriptions.ContainsKey(this))
				{
					if (GameObjectFactory.Factory.HasBlueprint(this.Blueprint))
					{
						GameObject gameObject = GameObject.CreateSample(this.Blueprint);
						TinkeringHelpers.StripForTinkering(gameObject);
						TinkeringHelpers.ForceToBePowered(gameObject);
						if (gameObject.HasPart<Description>())
						{
							StringBuilder stringBuilder = Event.NewStringBuilder(null);
							stringBuilder.Append('\n');
							TinkerItem part = gameObject.GetPart<TinkerItem>();
							if (part != null && part.NumberMade > 1)
							{
								stringBuilder.Append("{{rules|Makes a batch of ").Append(Grammar.Cardinal(part.NumberMade)).Append(".}}\n\n");
							}
							Description part2 = gameObject.GetPart<Description>();
							if (part2 != null)
							{
								stringBuilder.Append(part2.GetShortDescription(true, true, "Tinkering"));
							}
							stringBuilder.Append('\n');
							TinkerData.UnclippedDataDescriptions.Add(this, stringBuilder.ToString());
							this.DescriptionLineCount = TinkerData.UnclippedDataDescriptions[this].Count<char>((char c) => c == '\n');
						}
						else
						{
							TinkerData.UnclippedDataDescriptions.Add(this, "<none>");
						}
						gameObject.Obliterate(null, false, null);
					}
					else
					{
						TinkerData.UnclippedDataDescriptions.Add(this, "<none>");
					}
				}
				return TinkerData.UnclippedDataDescriptions[this];
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060073D4 RID: 29652 RVA: 0x002BAE14 File Offset: 0x002B9014
		public string Description
		{
			get
			{
				if (!TinkerData.DataDescriptions.ContainsKey(this))
				{
					if (GameObjectFactory.Factory.HasBlueprint(this.Blueprint))
					{
						GameObject gameObject = GameObject.CreateSample(this.Blueprint);
						TinkeringHelpers.StripForTinkering(gameObject);
						TinkeringHelpers.ForceToBePowered(gameObject);
						if (gameObject.HasPart<Description>())
						{
							StringBuilder stringBuilder = Event.NewStringBuilder(null);
							stringBuilder.Append('\n');
							TinkerItem part = gameObject.GetPart<TinkerItem>();
							if (part != null && part.NumberMade > 1)
							{
								stringBuilder.Append("{{rules|Makes a batch of ").Append(Grammar.Cardinal(part.NumberMade)).Append(".}}\n\n");
							}
							Description part2 = gameObject.GetPart<Description>();
							if (part2 != null)
							{
								stringBuilder.Append(part2.GetShortDescription(true, true, "Tinkering"));
							}
							stringBuilder.Append('\n');
							TinkerData.DataDescriptions.Add(this, StringFormat.ClipText(stringBuilder.ToString(), 76, true, true, false));
							this.DescriptionLineCount = TinkerData.DataDescriptions[this].Count<char>((char c) => c == '\n');
						}
						else
						{
							TinkerData.DataDescriptions.Add(this, "<none>");
						}
						gameObject.Obliterate(null, false, null);
					}
					else
					{
						TinkerData.DataDescriptions.Add(this, "<none>");
					}
				}
				return TinkerData.DataDescriptions[this];
			}
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x002BAF64 File Offset: 0x002B9164
		public bool CanMod(string ModTag)
		{
			ModEntry modEntry;
			if (!ModificationFactory.ModsByPart.TryGetValue(this.PartName, out modEntry))
			{
				return false;
			}
			if (!modEntry.TinkerAllowed)
			{
				return false;
			}
			List<string> list = ModTag.CachedCommaExpansion();
			foreach (string text in modEntry.TableList)
			{
				if (list.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060073D6 RID: 29654 RVA: 0x002BAFBF File Offset: 0x002B91BF
		public bool Known()
		{
			return TinkerData.KnownRecipes.Any<TinkerData>((TinkerData r) => r == this || r.Blueprint == this.Blueprint);
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x002BAFD8 File Offset: 0x002B91D8
		public static void UnlearnRecipe(string blueprint)
		{
			for (int i = 0; i < TinkerData.KnownRecipes.Count; i++)
			{
				if (TinkerData.KnownRecipes[i].Blueprint == blueprint)
				{
					TinkerData.KnownRecipes.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x002BB020 File Offset: 0x002B9220
		[WishCommand(null, null)]
		public static bool DataDisk(string blueprint)
		{
			if (blueprint.StartsWith("mod:"))
			{
				blueprint = "[mod]" + blueprint.Substring(4);
			}
			blueprint = blueprint.ToLower();
			GameObject gameObject = TinkerData.createDataDisk(TinkerData.TinkerRecipes.FirstOrDefault<TinkerData>((TinkerData t) => t.Blueprint.ToLower() == blueprint));
			The.Player.ReceiveObject(gameObject, false, null);
			return true;
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x002BB0A4 File Offset: 0x002B92A4
		[WishCommand(null, null)]
		public static bool DataDisk()
		{
			int num = Popup.PickOption("Pick a Blueprint:", null, "", "Sounds/UI/ui_notification", TinkerData.TinkerRecipes.ConvertAll<string>((TinkerData t) => t.Blueprint).ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
			GameObject gameObject = TinkerData.createDataDisk(TinkerData.TinkerRecipes[num]);
			The.Player.ReceiveObject(gameObject, false, null);
			return true;
		}

		// Token: 0x060073DA RID: 29658 RVA: 0x002BB128 File Offset: 0x002B9328
		public static GameObject createDataDisk(string blueprint)
		{
			if (blueprint.StartsWith("mod:"))
			{
				string modBlueprint = "[mod]" + blueprint.Substring(4);
				return TinkerData.createDataDisk(TinkerData.TinkerRecipes.FirstOrDefault<TinkerData>((TinkerData t) => t.Type == "Mod" && t.Blueprint == modBlueprint));
			}
			return TinkerData.createDataDisk(TinkerData.TinkerRecipes.FirstOrDefault<TinkerData>((TinkerData t) => t.Type == "Build" && t.Blueprint == blueprint));
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x002BB1B0 File Offset: 0x002B93B0
		public static GameObject createDataDisk(TinkerData data)
		{
			return GameObjectFactory.Factory.CreateObject("DataDisk", delegate(GameObject o)
			{
				o.GetPart<DataDisk>().Data = data;
			});
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x002BB1E8 File Offset: 0x002B93E8
		public static List<TinkerData> TinkerRecipes
		{
			get
			{
				if (TinkerData._TinkerRecipes.Count == 0)
				{
					ModificationFactory.CheckInit();
					foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
					{
						TinkerData tinkerData = TinkerItem.LoadBlueprint(gameObjectBlueprint);
						if (tinkerData != null)
						{
							TinkerData._TinkerRecipes.Add(tinkerData);
						}
					}
					foreach (ModEntry modEntry in ModificationFactory.ModList)
					{
						if (modEntry.TinkerAllowed)
						{
							TinkerData._TinkerRecipes.Add(new TinkerData
							{
								Blueprint = "[mod]" + modEntry.Part,
								DisplayName = modEntry.TinkerDisplayName,
								Cost = "",
								Ingredient = modEntry.TinkerIngredient,
								Tier = modEntry.TinkerTier,
								Category = modEntry.TinkerCategory,
								Type = "Mod"
							});
						}
					}
				}
				return TinkerData._TinkerRecipes;
			}
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x002BB314 File Offset: 0x002B9514
		public static void LearnBlueprint(string blueprint)
		{
			TinkerData tinkerData = TinkerData.TinkerRecipes.FirstOrDefault<TinkerData>((TinkerData t) => t.Type == "Build" && t.Blueprint == blueprint);
			if (tinkerData != null && !TinkerData.KnownRecipes.Contains(tinkerData))
			{
				TinkerData.KnownRecipes.Add(tinkerData);
			}
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x002BB360 File Offset: 0x002B9560
		public static void LearnMod(string blueprint)
		{
			string ModBlueprint = "[mod]" + blueprint;
			TinkerData tinkerData = TinkerData.TinkerRecipes.FirstOrDefault<TinkerData>((TinkerData t) => t.Type == "Mod" && t.Blueprint == ModBlueprint);
			if (tinkerData != null && !TinkerData.KnownRecipes.Contains(tinkerData))
			{
				TinkerData.KnownRecipes.Add(tinkerData);
			}
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x002BB3B8 File Offset: 0x002B95B8
		public static bool RecipeKnown(TinkerData Data)
		{
			using (List<TinkerData>.Enumerator enumerator = TinkerData.KnownRecipes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Blueprint == Data.Blueprint)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400354D RID: 13645
		public string DisplayName;

		// Token: 0x0400354E RID: 13646
		public string Blueprint;

		// Token: 0x0400354F RID: 13647
		public string Category;

		// Token: 0x04003550 RID: 13648
		public string Type;

		// Token: 0x04003551 RID: 13649
		public int Tier;

		// Token: 0x04003552 RID: 13650
		public string Cost;

		// Token: 0x04003553 RID: 13651
		public string Ingredient;

		// Token: 0x04003554 RID: 13652
		public int DescriptionLineCount = 1;

		// Token: 0x04003555 RID: 13653
		[NonSerialized]
		private string _PartName;

		// Token: 0x04003556 RID: 13654
		[GameBasedStaticCache(true, false, ClearInstance = true)]
		public static List<TinkerData> _TinkerRecipes = new List<TinkerData>();

		// Token: 0x04003557 RID: 13655
		[GameBasedStaticCache(true, false)]
		public static List<TinkerData> KnownRecipes = new List<TinkerData>();

		// Token: 0x04003558 RID: 13656
		public static Dictionary<TinkerData, string> UnclippedDataDescriptions = new Dictionary<TinkerData, string>();

		// Token: 0x04003559 RID: 13657
		public static Dictionary<TinkerData, string> DataDescriptions = new Dictionary<TinkerData, string>();

		// Token: 0x0400355A RID: 13658
		public static Dictionary<TinkerData, string> DataNames = new Dictionary<TinkerData, string>();

		// Token: 0x0400355B RID: 13659
		public static Dictionary<TinkerData, string> InventoryCateogires = new Dictionary<TinkerData, string>();
	}
}
