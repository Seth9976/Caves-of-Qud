using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Collections;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200122A RID: 4650
	[Serializable]
	public class PreparedCookingIngredient : IPart
	{
		// Token: 0x0600B8AF RID: 47279 RVA: 0x00435C78 File Offset: 0x00433E78
		public override bool SameAs(IPart p)
		{
			PreparedCookingIngredient preparedCookingIngredient = p as PreparedCookingIngredient;
			return preparedCookingIngredient != null && !(this.type != preparedCookingIngredient.type) && this.charges == preparedCookingIngredient.charges && !(this.descriptionPostfix != preparedCookingIngredient.descriptionPostfix) && base.SameAs(p);
		}

		// Token: 0x0600B8B0 RID: 47280 RVA: 0x00435CD2 File Offset: 0x00433ED2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B8B1 RID: 47281 RVA: 0x00435CF8 File Offset: 0x00433EF8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.ParentObject.HasTag("SpecialCookingIngredient"))
			{
				if (this.charges == 1)
				{
					E.AddTag("{{y|[{{C|1}} cooking serving]}}", 0);
				}
				else if (this.charges > 1)
				{
					E.AddTag("{{y|[{{C|" + this.charges.ToString() + "}} cooking servings]}}", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8B2 RID: 47282 RVA: 0x00435D5F File Offset: 0x00433F5F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.descriptionPostfix);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8B3 RID: 47283 RVA: 0x00435D7C File Offset: 0x00433F7C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			List<string> list = this.type.CachedCommaExpansion();
			List<string> list2 = new List<string>(list.Count);
			foreach (string text in list)
			{
				string tag = GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + text].GetTag("Description", "");
				if (!string.IsNullOrEmpty(tag) && !list2.Contains(tag))
				{
					list2.Add(tag);
				}
			}
			this.descriptionPostfix = "Adds " + Grammar.MakeOrList(list2, true) + " effects to cooked meals.";
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8B4 RID: 47284 RVA: 0x00435E40 File Offset: 0x00434040
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SelfRenderStackerDisplay");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B8B5 RID: 47285 RVA: 0x00435E58 File Offset: 0x00434058
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SelfRenderStackerDisplay")
			{
				int number = this.ParentObject.Stacker.Number;
				int num = this.charges;
				if (number > 1)
				{
					if (num == 1)
					{
						E.SetParameter("Display", "[{{C|1 serving] x" + number.ToString());
					}
					else
					{
						E.SetParameter("Display", "[{{C|" + num.ToString() + "}} servings] x" + number.ToString());
					}
				}
				else if (num == 1)
				{
					E.SetParameter("Display", "[{{C|1 serving]");
				}
				else
				{
					E.SetParameter("Display", "[{{C|" + num.ToString() + "}} servings]");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B8B6 RID: 47286 RVA: 0x00435F26 File Offset: 0x00434126
		public static IReadOnlyList<string> GetRandomTypeList()
		{
			return PreparedCookingIngredient.GetRandomTypeListInternal("RandomWeight", null, null);
		}

		// Token: 0x0600B8B7 RID: 47287 RVA: 0x00435F34 File Offset: 0x00434134
		public static void GetRandomTypeList(IList<string> FillList)
		{
			PreparedCookingIngredient.GetRandomTypeListInternal("RandomWeight", FillList, null);
		}

		// Token: 0x0600B8B8 RID: 47288 RVA: 0x00435F43 File Offset: 0x00434143
		public static IReadOnlyList<string> GetRandomHighTierTypeList()
		{
			return PreparedCookingIngredient.GetRandomTypeListInternal("RandomHighTierWeight", null, null);
		}

		// Token: 0x0600B8B9 RID: 47289 RVA: 0x00435F51 File Offset: 0x00434151
		public static void GetRandomHighTierTypeList(IList<string> FillList)
		{
			PreparedCookingIngredient.GetRandomTypeListInternal("RandomHighTierWeight", FillList, null);
		}

		// Token: 0x0600B8BA RID: 47290 RVA: 0x00435F60 File Offset: 0x00434160
		private static IReadOnlyList<string> GetRandomTypeListInternal(string Tag, IList<string> FillList = null, IReadOnlyList<string> Skip = null)
		{
			IList<string> list = FillList ?? PreparedCookingIngredient.RandomTypeList;
			if (FillList == null)
			{
				list.Clear();
			}
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.InheritsFrom("IngredientMapping"))
				{
					int num;
					int i = (int.TryParse(gameObjectBlueprint.GetTag(Tag, ""), out num) ? num : 0);
					string text;
					if ((text = gameObjectBlueprint.GetTag("CookingDomain", null)) == null)
					{
						string[] array = gameObjectBlueprint.Name.Split('_', StringSplitOptions.None);
						text = array[array.Length - 1];
					}
					string text2 = text;
					if (Skip == null || !Skip.Contains(text2))
					{
						while (i > 0)
						{
							list.Add(text2);
							i--;
						}
					}
				}
			}
			if (FillList == null)
			{
				return PreparedCookingIngredient.RandomTypeList;
			}
			return null;
		}

		// Token: 0x0600B8BB RID: 47291 RVA: 0x0043603C File Offset: 0x0043423C
		public static string GetRandomType()
		{
			return PreparedCookingIngredient.GetRandomTypeList().GetRandomElement<string>(null);
		}

		// Token: 0x0600B8BC RID: 47292 RVA: 0x00436049 File Offset: 0x00434249
		public static string GetRandomHighTierType()
		{
			return PreparedCookingIngredient.GetRandomHighTierTypeList().GetRandomElement<string>(null);
		}

		// Token: 0x0600B8BD RID: 47293 RVA: 0x00436058 File Offset: 0x00434258
		[Obsolete("Use GetTypeOptions(ListToFill)")]
		public IReadOnlyList<string> GetTypeOptions()
		{
			if (this.type == "random")
			{
				return PreparedCookingIngredient.GetRandomTypeList();
			}
			if (this.type == "randomHighTier")
			{
				return PreparedCookingIngredient.GetRandomHighTierTypeList();
			}
			if (this.type.Contains(","))
			{
				return new List<string>(this.type.Split(',', StringSplitOptions.None));
			}
			return new List<string>(new string[] { this.type });
		}

		// Token: 0x0600B8BE RID: 47294 RVA: 0x004360D0 File Offset: 0x004342D0
		public void GetTypeOptions(IList<string> FillList)
		{
			if (this.type == "random")
			{
				PreparedCookingIngredient.GetRandomTypeList(FillList);
				return;
			}
			if (this.type == "randomHighTier")
			{
				PreparedCookingIngredient.GetRandomHighTierTypeList(FillList);
				return;
			}
			if (this.type.Contains(","))
			{
				FillList.AddRange<string>(this.type.Split(',', StringSplitOptions.None));
				return;
			}
			FillList.Add(this.type);
		}

		// Token: 0x0600B8BF RID: 47295 RVA: 0x00436144 File Offset: 0x00434344
		public string GetTypeInstance()
		{
			if (this.type == "random")
			{
				return PreparedCookingIngredient.GetRandomType();
			}
			if (this.type == "randomHighTier")
			{
				return PreparedCookingIngredient.GetRandomHighTierType();
			}
			if (this.type.Contains(","))
			{
				return this.type.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
			}
			return this.type;
		}

		// Token: 0x0600B8C0 RID: 47296 RVA: 0x004361B0 File Offset: 0x004343B0
		public string GetTypeInstanceAvoidingList(IReadOnlyList<string> Skip)
		{
			if (this.type == "random")
			{
				PreparedCookingIngredient.RandomTypeList.Clear();
				PreparedCookingIngredient.GetRandomTypeListInternal("RandomWeight", PreparedCookingIngredient.RandomTypeList, Skip);
				return PreparedCookingIngredient.RandomTypeList.GetRandomElement<string>(null);
			}
			if (this.type == "randomHighTier")
			{
				PreparedCookingIngredient.RandomTypeList.Clear();
				PreparedCookingIngredient.GetRandomTypeListInternal("RandomHighTierWeight", PreparedCookingIngredient.RandomTypeList, Skip);
				return PreparedCookingIngredient.RandomTypeList.GetRandomElement<string>(null);
			}
			if (this.type.Contains(","))
			{
				using (ScopeDisposedList<string> fromPoolFilledWith = ScopeDisposedList<string>.GetFromPoolFilledWith(from l in this.type.Split(',', StringSplitOptions.None)
					where !Skip.Contains(l)
					select l))
				{
					return fromPoolFilledWith.GetRandomElement<string>(null);
				}
			}
			if (Skip.Contains(this.type))
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x0600B8C1 RID: 47297 RVA: 0x004362C0 File Offset: 0x004344C0
		public bool HasTypeOption(string findType)
		{
			if (this.type == "random")
			{
				return PreparedCookingIngredient.GetRandomTypeList().Contains(findType);
			}
			if (this.type == "randomHighTier")
			{
				return PreparedCookingIngredient.GetRandomHighTierTypeList().Contains(findType);
			}
			if (this.type.Contains(","))
			{
				return this.type.Split(',', StringSplitOptions.None).Contains(findType);
			}
			return this.type == findType;
		}

		// Token: 0x04004A5E RID: 19038
		public string type;

		// Token: 0x04004A5F RID: 19039
		public string descriptionPostfix;

		// Token: 0x04004A60 RID: 19040
		public int charges;

		// Token: 0x04004A61 RID: 19041
		[NonSerialized]
		private static List<string> RandomTypeList = new List<string>(256);
	}
}
