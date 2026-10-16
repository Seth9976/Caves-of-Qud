using System;
using System.Collections.Generic;
using System.Reflection;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B39 RID: 2873
	public static class ItemModding
	{
		// Token: 0x06007412 RID: 29714 RVA: 0x002BD540 File Offset: 0x002BB740
		public static bool CanMod(GameObject Object, GameObject Actor = null)
		{
			if (Actor == null)
			{
				Actor = The.Player;
			}
			if (Actor == null || !Actor.HasSkill("Tinkering"))
			{
				return false;
			}
			string text = ItemModding.ModKey(Object);
			if (text != null && (!Actor.IsPlayerControlled() || Object.Understood()))
			{
				foreach (TinkerData tinkerData in TinkerData.KnownRecipes)
				{
					if (tinkerData.Type == "Mod" && ItemModding.ModAppropriate(Object, tinkerData, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007413 RID: 29715 RVA: 0x002BD5E4 File Offset: 0x002BB7E4
		public static string ModKey(GameObject Object)
		{
			string propertyOrTag = Object.GetPropertyOrTag("Mods", null);
			if (!propertyOrTag.IsNullOrEmpty() && propertyOrTag != "None" && Object.GetModificationSlotsUsed() < RuleSettings.MAXIMUM_ITEM_MODS)
			{
				return propertyOrTag;
			}
			return null;
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x002BD623 File Offset: 0x002BB823
		public static bool ModAppropriate(GameObject Object, TinkerData ModData, string Key)
		{
			return Key != null && ModData.CanMod(Key) && ItemModding.ModificationApplicable(ModData.PartName, Object, null, null);
		}

		// Token: 0x06007415 RID: 29717 RVA: 0x002BD641 File Offset: 0x002BB841
		public static bool ModAppropriate(GameObject Object, TinkerData ModData)
		{
			return ItemModding.ModAppropriate(Object, ModData, ItemModding.ModKey(Object));
		}

		// Token: 0x06007416 RID: 29718 RVA: 0x002BD650 File Offset: 0x002BB850
		public static bool ModificationApplicable(string Name, GameObject Object, GameObject Actor = null, string Key = null)
		{
			if ((Key ?? ItemModding.ModKey(Object)) == null)
			{
				return false;
			}
			if (Object.HasPart(Name))
			{
				return false;
			}
			if (Actor != null && Actor.IsPlayerControlled() && !Object.Understood())
			{
				return false;
			}
			if (!CanBeModdedEvent.Check(Actor, Object, Name))
			{
				return false;
			}
			IModification modification;
			if (!ItemModding.ModificationInstances.TryGetValue(Name, out modification))
			{
				Type type = ModManager.ResolveType("XRL.World.Parts." + Name, false, false, true);
				if (type == null)
				{
					return false;
				}
				modification = Activator.CreateInstance(type) as IModification;
				if (modification == null)
				{
					return false;
				}
				ItemModding.ModificationInstances[Name] = modification;
			}
			return modification.ModificationApplicable(Object);
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x002BD6EC File Offset: 0x002BB8EC
		public static string GetModificationDescription(string Name, int Tier)
		{
			if (Name.Contains("[mod]"))
			{
				Name = Name.Replace("[mod]", "");
			}
			MethodInfo method = ModManager.ResolveType("XRL.World.Parts." + Name, false, false, true).GetMethod("GetDescription", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, ItemModding.ModDescArgs1, null);
			if (method != null)
			{
				return (string)method.Invoke(null, new object[] { Tier });
			}
			return ModificationFactory.ModsByPart[Name].Description;
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x002BD778 File Offset: 0x002BB978
		public static string GetModificationDescription(string Name, GameObject obj)
		{
			if (obj == null)
			{
				return ItemModding.GetModificationDescription(Name, 1);
			}
			if (Name.Contains("[mod]"))
			{
				Name = Name.Replace("[mod]", "");
			}
			Type type = ModManager.ResolveType("XRL.World.Parts." + Name, false, false, true);
			MethodInfo method = type.GetMethod("GetDescription", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, ItemModding.ModDescArgs2, null);
			if (method != null)
			{
				return (string)method.Invoke(null, new object[]
				{
					Math.Max(obj.GetTier(), 1),
					obj
				});
			}
			MethodInfo method2 = type.GetMethod("GetDescription", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, ItemModding.ModDescArgs1, null);
			if (method2 != null)
			{
				return (string)method2.Invoke(null, new object[] { Math.Max(obj.GetTier(), 1) });
			}
			return ModificationFactory.ModsByPart[Name].Description;
		}

		// Token: 0x06007419 RID: 29721 RVA: 0x002BD864 File Offset: 0x002BBA64
		public static bool ApplyModification(GameObject Object, IModification ModPart, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			if (Actor != null && !ModPart.BeingAppliedBy(Object, Actor))
			{
				return false;
			}
			Object.AddPart(ModPart, DoRegistration, Creation);
			if (Object.HasPart<Commerce>() && ModificationFactory.ModsByPart.ContainsKey(ModPart.Name))
			{
				Object.GetPart<Commerce>().Value *= ModificationFactory.ModsByPart[ModPart.Name].Value;
			}
			ModPart.ApplyModification(Object);
			ModificationAppliedEvent.Send(Object, ModPart);
			TinkeringHelpers.CheckMakersMark(Object, Actor, ModPart, null);
			return true;
		}

		// Token: 0x0600741A RID: 29722 RVA: 0x002BD8E4 File Offset: 0x002BBAE4
		public static bool ApplyModification(GameObject Object, string ModPartName, out IModification ModPart, int Tier, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			Type type = ModManager.ResolveType("XRL.World.Parts." + ModPartName, false, false, true);
			if (type == null)
			{
				MetricsManager.LogError("ApplyModification", "Couldn't resolve unknown mod part: " + ModPartName);
				ModPart = null;
				return false;
			}
			Tier.Constrain(ref Tier);
			object[] array = new object[] { Tier };
			ModPart = Activator.CreateInstance(type, array) as IModification;
			if (ModPart == null)
			{
				if (!(Activator.CreateInstance(type, array) is IPart))
				{
					string text = "failed to load ";
					Type type2 = type;
					MetricsManager.LogError(text + ((type2 != null) ? type2.ToString() : null));
				}
				else
				{
					Type type3 = type;
					MetricsManager.LogError(((type3 != null) ? type3.ToString() : null) + " is not an IModification");
				}
				return false;
			}
			return ItemModding.ApplyModification(Object, ModPart, DoRegistration, Actor, Creation);
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x002BD9AC File Offset: 0x002BBBAC
		public static bool ApplyModification(GameObject Object, string ModPartName, int Tier, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			IModification modification;
			return ItemModding.ApplyModification(Object, ModPartName, out modification, Tier, DoRegistration, Actor, Creation);
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x002BD9C8 File Offset: 0x002BBBC8
		public static bool ApplyModification(GameObject Object, string ModPartName, out IModification ModPart, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			return ItemModding.ApplyModification(Object, ModPartName, out ModPart, Object.GetTier(), DoRegistration, Actor, Creation);
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x002BD9E0 File Offset: 0x002BBBE0
		public static bool ApplyModification(GameObject Object, string ModPartName, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			IModification modification;
			return ItemModding.ApplyModification(Object, ModPartName, out modification, DoRegistration, Actor, Creation);
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x002BD9FC File Offset: 0x002BBBFC
		public static bool ApplyModificationFromPopulationTable(GameObject Object, string Table, int Tier, bool Creation = false)
		{
			string blueprint = PopulationManager.RollOneFrom(Table, null, null).Blueprint;
			return ItemModding.ApplyModification(Object, blueprint, Tier, Creation, null, false);
		}

		// Token: 0x0600741F RID: 29727 RVA: 0x002BDA24 File Offset: 0x002BBC24
		public static bool ApplyModificationFromPopulationTable(GameObject Object, string Table, bool Creation = false)
		{
			string blueprint = PopulationManager.RollOneFrom(Table, null, null).Blueprint;
			return ItemModding.ApplyModification(Object, blueprint, Creation, null, false);
		}

		// Token: 0x04003580 RID: 13696
		[NonSerialized]
		private static Dictionary<string, IModification> ModificationInstances = new Dictionary<string, IModification>(64);

		// Token: 0x04003581 RID: 13697
		private static Type[] ModDescArgs2 = new Type[]
		{
			typeof(int),
			typeof(GameObject)
		};

		// Token: 0x04003582 RID: 13698
		private static Type[] ModDescArgs1 = new Type[] { typeof(int) };
	}
}
