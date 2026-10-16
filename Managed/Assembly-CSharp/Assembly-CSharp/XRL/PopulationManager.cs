using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Qud.API;
using UnityEngine;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.Capabilities;

namespace XRL
{
	// Token: 0x02000537 RID: 1335
	[HasModSensitiveStaticCache]
	[HasWishCommand]
	public class PopulationManager
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x001189DB File Offset: 0x00116BDB
		public static Dictionary<string, PopulationInfo> Populations
		{
			get
			{
				PopulationManager.CheckInit();
				return PopulationManager._Populations;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x001189E7 File Offset: 0x00116BE7
		public static Dictionary<string, MethodInfo> PopulationCalls
		{
			get
			{
				if (PopulationManager._PopulationCalls == null)
				{
					PopulationManager._PopulationCalls = new Dictionary<string, MethodInfo>();
				}
				return PopulationManager._PopulationCalls;
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x00118A00 File Offset: 0x00116C00
		public static string resolveCallBlueprintSlug(string slug)
		{
			if (slug.StartsWith("$CALLBLUEPRINTMETHOD:", StringComparison.InvariantCultureIgnoreCase))
			{
				MethodInfo methodInfo = null;
				if (!PopulationManager.PopulationCalls.TryGetValue(slug, out methodInfo))
				{
					string text = slug.Substring("$CALLBLUEPRINTMETHOD:".Length);
					int num = text.LastIndexOf(".");
					string text2 = text.Substring(0, num);
					string text3 = text.Substring(num + 1, text.Length - num - 1);
					PopulationManager.PopulationCalls.Add(slug, null);
					Type type = ModManager.ResolveType(text2, false, false, true);
					if (type != null)
					{
						methodInfo = type.GetMethod(text3);
						PopulationManager.PopulationCalls[slug] = methodInfo;
					}
				}
				if (methodInfo != null)
				{
					if (methodInfo.ReturnType == typeof(string))
					{
						int num2 = methodInfo.GetParameters().Length;
						return (string)methodInfo.Invoke(null, (num2 == 0) ? null : new object[num2]);
					}
					MetricsManager.LogError("Can't resolve resturn value to a string: " + slug);
				}
				else
				{
					MetricsManager.LogError("Undefined population call: " + slug);
				}
			}
			else
			{
				MetricsManager.LogError("Trying to resolve non-call slug: " + slug);
			}
			return null;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x00118B18 File Offset: 0x00116D18
		public static List<XRL.World.GameObject> resolveCallObjectSlug(string slug)
		{
			List<XRL.World.GameObject> list = XRL.World.Event.NewGameObjectList();
			if (slug.StartsWith("$CALLOBJECTMETHOD:", StringComparison.InvariantCultureIgnoreCase))
			{
				MethodInfo methodInfo = null;
				if (!PopulationManager.PopulationCalls.TryGetValue(slug, out methodInfo))
				{
					string text = slug.Substring("$CALLOBJECTMETHOD:".Length);
					int num = text.LastIndexOf(".");
					string text2 = text.Substring(0, num);
					string text3 = text.Substring(num + 1, text.Length - num - 1);
					PopulationManager.PopulationCalls.Add(slug, null);
					Type type = ModManager.ResolveType(text2, false, false, true);
					if (type != null)
					{
						methodInfo = type.GetMethod(text3);
						PopulationManager.PopulationCalls[slug] = methodInfo;
					}
				}
				if (methodInfo != null)
				{
					if (methodInfo.ReturnType == typeof(XRL.World.GameObject))
					{
						int num2 = methodInfo.GetParameters().Length;
						list.Add((XRL.World.GameObject)methodInfo.Invoke(null, (num2 == 0) ? null : new object[num2]));
					}
					else if ((IEnumerable<XRL.World.GameObject>)methodInfo.ReturnType != null)
					{
						int num3 = methodInfo.GetParameters().Length;
						list.AddRange((IEnumerable<XRL.World.GameObject>)methodInfo.Invoke(null, (num3 == 0) ? null : new object[num3]));
					}
					else
					{
						MetricsManager.LogError("Can't resolve resturn value to a GameObject or GameObject collection: " + slug);
					}
				}
				else
				{
					MetricsManager.LogError("Undefined population call: " + slug);
				}
			}
			else
			{
				MetricsManager.LogError("Trying to resolve non-call slug: " + slug);
			}
			return list;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x00118C7A File Offset: 0x00116E7A
		public static void CheckInit()
		{
			if (PopulationManager._Populations == null)
			{
				Loading.LoadTask("Loading PopulationTables.xml", new Action(PopulationManager.LoadFiles), true);
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x00118C9C File Offset: 0x00116E9C
		public static List<string> ResultToStringList(List<PopulationResult> Result)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < Result.Count; i++)
			{
				for (int j = 0; j < Result[i].Number; j++)
				{
					list.Add(Result[i].Blueprint);
				}
			}
			return list;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00118CEA File Offset: 0x00116EEA
		public static bool TableExists(string Name)
		{
			PopulationManager.RequireTable(Name);
			return PopulationManager._Populations.ContainsKey(Name);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00118D00 File Offset: 0x00116F00
		public static void RequireTable(string Name)
		{
			PopulationManager.CheckInit();
			if (PopulationManager._Populations.ContainsKey(Name))
			{
				return;
			}
			if (!Name.Contains(':', StringComparison.Ordinal))
			{
				return;
			}
			if (Name.StartsWith("DynamicObjectsTable:", StringComparison.Ordinal))
			{
				string[] array = Name.Split(':', StringSplitOptions.None);
				if (array.Length <= 2 || !array[2].StartsWith("Tier"))
				{
					GameObjectFactory.Factory.FabricateDynamicObjectsTable(Name);
					return;
				}
				int num;
				int num2;
				if (PopulationManager.ResolveTier(array[2], out num, out num2))
				{
					string targetTag = "DynamicObjectsTable:" + array[1];
					GameObjectFactory.Factory.FabricateMultitierDynamicPopulationTable(Name, GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>((GameObjectBlueprint b) => EncountersAPI.IsEligibleForDynamicEncounters(b) && b.HasTag(targetTag, false)), num, num2);
					return;
				}
			}
			else
			{
				if (Name.StartsWith("StaticObjectsTable:", StringComparison.Ordinal))
				{
					GameObjectFactory.Factory.FabricateStaticObjectsTable(Name);
					return;
				}
				if (Name.StartsWith("DynamicSemanticTable:", StringComparison.Ordinal))
				{
					GameObjectFactory.Factory.FabricateDynamicSemanticTable(Name);
					return;
				}
				if (Name.StartsWith("DynamicArtifactsTable:", StringComparison.Ordinal))
				{
					GameObjectFactory.Factory.FabricateDynamicArtifactsTable();
					return;
				}
				if (Name.StartsWith("DynamicHasPartTable:", StringComparison.Ordinal))
				{
					string[] parts2 = Name.Split(':', StringSplitOptions.None);
					int num3;
					int num4;
					if (parts2.Length > 2 && parts2[2].StartsWith("Tier") && PopulationManager.ResolveTier(parts2[2], out num3, out num4))
					{
						GameObjectFactory.Factory.FabricateMultitierDynamicPopulationTable(Name, GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>((GameObjectBlueprint b) => EncountersAPI.IsEligibleForDynamicEncounters(b) && b.HasPart(parts2[1])), num3, num4);
					}
					if (!PopulationManager._Populations.ContainsKey(Name))
					{
						string text = Name.Split(':', StringSplitOptions.None)[1];
						GameObjectFactory.Factory.FabricateDynamicHasPartTable(text);
						return;
					}
				}
				else if (Name.StartsWith("DynamicInheritsTable:", StringComparison.Ordinal))
				{
					string[] parts = Name.Split(':', StringSplitOptions.None);
					int num5;
					int num6;
					if (parts.Length > 2 && parts[2].StartsWith("Tier") && PopulationManager.ResolveTier(parts[2], out num5, out num6))
					{
						GameObjectFactory.Factory.FabricateMultitierDynamicPopulationTable(Name, GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>((GameObjectBlueprint b) => EncountersAPI.IsEligibleForDynamicEncounters(b) && b.DescendsFrom(parts[1])), num5, num6);
					}
					if (!PopulationManager._Populations.ContainsKey(Name))
					{
						string text2 = Name.Split(':', StringSplitOptions.None)[1];
						GameObjectFactory.Factory.FabricateDynamicInheritsTable(text2);
					}
				}
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x00118F68 File Offset: 0x00117168
		public static bool ResolveTier(string origSpec, out int low, out int high)
		{
			low = 0;
			high = 0;
			if (!origSpec.StartsWith("Tier"))
			{
				Debug.LogError("Tier specification does not start with Tier: " + origSpec);
				return false;
			}
			string text = origSpec.Substring(4);
			string text2 = null;
			while (text.Contains("{zonetier"))
			{
				if (text == text2)
				{
					Debug.LogError("Stalled parsing " + origSpec + " at " + text);
					return false;
				}
				text2 = text;
				if (text == "{zonetier}")
				{
					low = (high = ZoneManager.zoneGenerationContextTier);
					return true;
				}
				if (text == "{zonetier+1}")
				{
					low = (high = Tier.Constrain(ZoneManager.zoneGenerationContextTier + 1));
					return true;
				}
				if (PopulationManager.zonetierSplitter == null)
				{
					PopulationManager.zonetierSplitter = new string[] { "{zonetier" };
				}
				string[] array = text.Split(PopulationManager.zonetierSplitter, StringSplitOptions.None);
				if (array.Length > 1 || array[0] != "" || (array.Length > 1 && array[1].Length > 3))
				{
					StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
					stringBuilder.Append(array[0]);
					for (int i = 1; i < array.Length; i++)
					{
						int num = array[i].IndexOf('}');
						if (num != 0)
						{
							if (num > 0 && array[i].StartsWith("+"))
							{
								string text3 = array[i].Substring(1, num - 1);
								try
								{
									int num2 = Convert.ToInt32(text3);
									stringBuilder.Append(Tier.Constrain(ZoneManager.zoneGenerationContextTier + num2)).Append(array[i].Substring(num + 1));
									goto IL_024A;
								}
								catch (Exception ex)
								{
									MetricsManager.LogError("Bad zone tier offset in " + origSpec + ": " + text3, ex);
									return false;
								}
							}
							if (num > 0 && array[i].StartsWith("-"))
							{
								string text4 = array[i].Substring(1, num - 1);
								try
								{
									int num3 = Convert.ToInt32(text4);
									stringBuilder.Append(Tier.Constrain(ZoneManager.zoneGenerationContextTier - num3)).Append(array[i].Substring(num + 1));
									goto IL_024A;
								}
								catch (Exception ex2)
								{
									MetricsManager.LogError("Bad zone tier offset in " + origSpec + ": " + text4, ex2);
									return false;
								}
							}
							MetricsManager.LogError("Bad zone tier offset in " + origSpec + " at " + array[i]);
							return false;
						}
						stringBuilder.Append(ZoneManager.zoneGenerationContextTier).Append(array[i].Substring(1));
						IL_024A:;
					}
					text = stringBuilder.ToString();
					continue;
				}
				int num4 = array[1].IndexOf('}');
				if (num4 != array[1].Length - 1)
				{
					MetricsManager.LogError("Internal inconsistency parsing " + origSpec + " at " + array[1]);
					return false;
				}
				if (num4 == 0)
				{
					MetricsManager.LogWarning("This case for " + origSpec + " should be unreachable");
					low = (high = ZoneManager.zoneGenerationContextTier);
					return true;
				}
				if (num4 > 0 && array[1].StartsWith("+"))
				{
					string text5 = array[1].Substring(1, num4 - 1);
					try
					{
						int num5 = Convert.ToInt32(text5);
						low = (high = Tier.Constrain(ZoneManager.zoneGenerationContextTier + num5));
						return true;
					}
					catch (Exception ex3)
					{
						MetricsManager.LogError("Bad zone tier offset in " + origSpec + ": " + text5, ex3);
						return false;
					}
				}
				if (num4 > 0 && array[1].StartsWith("-"))
				{
					string text6 = array[1].Substring(1, num4 - 1);
					try
					{
						int num6 = Convert.ToInt32(text6);
						low = (high = Tier.Constrain(ZoneManager.zoneGenerationContextTier - num6));
						return true;
					}
					catch (Exception ex4)
					{
						MetricsManager.LogError("Bad zone tier offset in " + origSpec + ": " + text6, ex4);
						return false;
					}
				}
				MetricsManager.LogError("Bad zone tier offset in " + origSpec + " at " + array[1]);
				return false;
			}
			int num7 = text.IndexOf('-');
			if (num7 != -1)
			{
				string text7 = text.Substring(0, num7);
				string text8 = text.Substring(num7 + 1);
				try
				{
					low = Tier.Constrain(Convert.ToInt32(text7));
				}
				catch (Exception ex5)
				{
					MetricsManager.LogError("Bad low tier specification in " + origSpec + ": " + text7, ex5);
					return false;
				}
				high = low;
				try
				{
					high = Tier.Constrain(Convert.ToInt32(text8));
				}
				catch (Exception ex6)
				{
					MetricsManager.LogError("Bad high tier specification in " + origSpec + ": " + text8, ex6);
					return false;
				}
				return true;
			}
			bool flag;
			try
			{
				low = (high = Tier.Constrain(Convert.ToInt32(text)));
				flag = true;
			}
			catch (Exception ex7)
			{
				MetricsManager.LogError("Bad tier specification in " + origSpec + ": " + text, ex7);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x00119458 File Offset: 0x00117658
		public static PopulationInfo ResolvePopulation(string Name, bool MissingOkay = false)
		{
			PopulationManager.RequireTable(Name);
			PopulationInfo populationInfo;
			if (PopulationManager._Populations.TryGetValue(Name, out populationInfo))
			{
				return populationInfo;
			}
			if (!MissingOkay)
			{
				Debug.LogWarning("Unknown Population table: " + Name);
			}
			return null;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x00119490 File Offset: 0x00117690
		public static bool TryResolvePopulation(string Name, out PopulationInfo Population)
		{
			if (Name == null)
			{
				Population = null;
				return false;
			}
			PopulationManager.RequireTable(Name);
			return PopulationManager._Populations.TryGetValue(Name, out Population);
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x001194AC File Offset: 0x001176AC
		public static bool TryResolvePopulation(string Name, Dictionary<string, string> Vars, out PopulationInfo Population)
		{
			if (Name == null)
			{
				Population = null;
				return false;
			}
			if (!Vars.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				PopulationManager.ReplaceVariables(ref Name, Vars);
			}
			PopulationManager.RequireTable(Name);
			return PopulationManager._Populations.TryGetValue(Name, out Population);
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x001194D8 File Offset: 0x001176D8
		public static bool HasPopulation(string PopulationName)
		{
			return PopulationManager.ResolvePopulation(PopulationName, true) != null;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x001194E4 File Offset: 0x001176E4
		public static XRL.World.GameObject CreateOneFrom(string PopulationName, Dictionary<string, string> Variables = null, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, string Context = null, Action<XRL.World.GameObject> BeforeObjectCreated = null, Action<XRL.World.GameObject> AfterObjectCreated = null, List<XRL.World.GameObject> ProvideInventory = null)
		{
			string blueprint = PopulationManager.RollOneFrom(PopulationName, Variables, null).Blueprint;
			return GameObjectFactory.Factory.CreateObject(blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00119518 File Offset: 0x00117718
		public static PopulationResult RollOneFrom(string PopulationName, Dictionary<string, string> Vars = null, string DefaultIfNull = null)
		{
			List<PopulationResult> list = null;
			try
			{
				list = PopulationManager.Generate(PopulationName, Vars, null);
			}
			catch (Exception ex)
			{
				list = new List<PopulationResult>();
				Popup.Show("Error generating population:" + PopulationName + "\n\n, please report this error to support@freeholdgames.com", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				MetricsManager.LogError(ex);
			}
			if (list.Count == 0)
			{
				return new PopulationResult(DefaultIfNull, 0, null, null);
			}
			return list.GetRandomElement<PopulationResult>(null);
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00119588 File Offset: 0x00117788
		public static bool sameAs(List<PopulationResult> r1, List<PopulationResult> r2)
		{
			if (r1.Count != r2.Count)
			{
				return false;
			}
			using (List<PopulationResult>.Enumerator enumerator = r1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PopulationResult entry = enumerator.Current;
					if (!r2.Any<PopulationResult>((PopulationResult e) => e.Blueprint == entry.Blueprint && e.Number == entry.Number))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00119608 File Offset: 0x00117808
		public static List<List<PopulationResult>> RollDistinctFrom(string PopulationName, int n, Dictionary<string, string> vars = null, string defaultIfNull = null)
		{
			List<List<PopulationResult>> list = new List<List<PopulationResult>>();
			int num = n;
			int num2 = 100;
			while (num > 0 && num2 > 0)
			{
				List<PopulationResult> Ret = PopulationManager.Generate(PopulationName, vars, null);
				if (!list.Any<List<PopulationResult>>((List<PopulationResult> r) => PopulationManager.sameAs(r, Ret)))
				{
					list.Add(Ret);
					num--;
				}
				if (num <= 0)
				{
					break;
				}
				num2--;
			}
			return list;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x0011966C File Offset: 0x0011786C
		public static List<XRL.World.GameObject> Expand(List<PopulationResult> Input)
		{
			int num = 0;
			List<XRL.World.GameObject> list = new List<XRL.World.GameObject>();
			for (int i = 0; i < Input.Count; i++)
			{
				for (int j = 0; j < Input[i].Number; j++)
				{
					list.Add(GameObjectFactory.Factory.CreateObject(Input[i].Blueprint));
					list[list.Count - 1].SetLongProperty("Batch", (long)num);
					num++;
					if (num >= 2147483646)
					{
						num = 0;
					}
				}
			}
			return list;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x001196F0 File Offset: 0x001178F0
		public static bool HasTable(string PopulationName)
		{
			PopulationManager.CheckInit();
			if (string.IsNullOrEmpty(PopulationName))
			{
				return false;
			}
			if (!PopulationManager._Populations.ContainsKey(PopulationName))
			{
				if (!PopulationName.StartsWith("Dynamic"))
				{
					return false;
				}
				PopulationInfo populationInfo = new PopulationInfo();
				PopulationTable populationTable = new PopulationTable();
				populationTable.Name = PopulationName;
				populationInfo.Items.Add(populationTable);
				populationInfo.Name = PopulationName;
				populationInfo.Generate(new Dictionary<string, string>(), null);
			}
			return true;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x0011975C File Offset: 0x0011795C
		public static List<string> GetEach(string PopulationName, Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			if (!Vars.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				PopulationManager.ReplaceVariables(ref PopulationName, Vars);
			}
			PopulationInfo populationInfo = PopulationManager.ResolvePopulation(PopulationName, false);
			if (populationInfo == null)
			{
				return new List<string>();
			}
			List<string> list = new List<string>();
			foreach (PopulationItem populationItem in populationInfo.Items)
			{
				populationItem.GetEachUniqueObject(list);
			}
			return list;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x001197D8 File Offset: 0x001179D8
		public static List<PopulationResult> GenerateSemantic(List<string> tags, int tier, int techTier, Dictionary<string, string> vars, string DefaultHint = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DynamicSemanticTable:");
			for (int i = 0; i < tags.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(tags[i]);
			}
			stringBuilder.Append(":").Append(tier).Append(":")
				.Append(techTier);
			return PopulationManager.Generate(stringBuilder.ToString(), vars, DefaultHint);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x00119858 File Offset: 0x00117A58
		public static List<PopulationResult> Generate(string PopulationName, Dictionary<string, string> Vars, string DefaultHint = null)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(PopulationName, Vars, out populationInfo))
			{
				Debug.LogError("Failed to resolve: " + PopulationName);
				return new List<PopulationResult>();
			}
			return populationInfo.Generate(Vars, DefaultHint);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x00119890 File Offset: 0x00117A90
		public static List<PopulationResult> Generate(string PopulationName, string VariableName1 = null, string VariableValue1 = null, string VariableName2 = null, string VariableValue2 = null, string DefaultHint = null)
		{
			Dictionary<string, string> dictionary = PopulationManager.GetDictionary(VariableName1, VariableValue1, VariableName2, VariableValue2);
			return PopulationManager.Generate(PopulationName, dictionary, DefaultHint);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x001198B4 File Offset: 0x00117AB4
		public static PopulationResult GenerateOne(string PopulationName, Dictionary<string, string> Vars, string DefaultHint = null)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(PopulationName, Vars, out populationInfo))
			{
				Debug.LogError("Failed to resolve: " + PopulationName);
				return null;
			}
			return populationInfo.GenerateOne(Vars, DefaultHint);
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x001198E8 File Offset: 0x00117AE8
		public static PopulationResult GenerateOne(string PopulationName, string VariableName1 = null, string VariableValue1 = null, string VariableName2 = null, string VariableValue2 = null, string DefaultHint = null)
		{
			Dictionary<string, string> dictionary = PopulationManager.GetDictionary(VariableName1, VariableValue1, VariableName2, VariableValue2);
			return PopulationManager.GenerateOne(PopulationName, dictionary, DefaultHint);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x00119909 File Offset: 0x00117B09
		public static List<PopulationResult> Generate(PopulationInfo Population, Dictionary<string, string> Vars, string DefaultHint = null)
		{
			return Population.Generate(Vars, DefaultHint);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x00119913 File Offset: 0x00117B13
		public static PopulationResult GenerateOne(PopulationInfo Population, Dictionary<string, string> Vars, string DefaultHint = null)
		{
			return Population.GenerateOne(Vars, DefaultHint);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x00119920 File Offset: 0x00117B20
		public static PopulationStructuredResult GenerateStructured(string PopulationName, Dictionary<string, string> Vars, string DefaultHint = null)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(PopulationName, Vars, out populationInfo))
			{
				Debug.LogError("Failed to resolve: " + PopulationName);
				return new PopulationStructuredResult();
			}
			PopulationStructuredResult populationStructuredResult = new PopulationStructuredResult();
			populationStructuredResult.Hint = populationInfo.Hint ?? DefaultHint;
			populationInfo.GenerateStructured(populationStructuredResult, Vars);
			return populationStructuredResult;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x00119970 File Offset: 0x00117B70
		public static PopulationStructuredResult GenerateStructured(string PopulationName, string VariableName1 = null, string VariableValue1 = null, string VariableName2 = null, string VariableValue2 = null, string DefaultHint = null)
		{
			Dictionary<string, string> dictionary = PopulationManager.GetDictionary(VariableName1, VariableValue1, VariableName2, VariableValue2);
			return PopulationManager.GenerateStructured(PopulationName, dictionary, DefaultHint);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00119994 File Offset: 0x00117B94
		private static Dictionary<string, string> GetDictionary(string VariableName1 = null, string VariableValue1 = null, string VariableName2 = null, string VariableValue2 = null)
		{
			Dictionary<string, string> dictionary = null;
			if (VariableName1 != null)
			{
				dictionary = new Dictionary<string, string>();
				dictionary.Add(VariableName1, VariableValue1);
			}
			if (VariableName2 != null)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, string>();
				}
				dictionary.Add(VariableName2, VariableValue2);
			}
			return dictionary;
		}

		/// <summary>Add items to a population, next to a sibling object. </summary>
		/// <param name="table">The Name of the target population.</param>
		/// <param name="sibling">The Blueprint of the sibling object.</param>
		/// <param name="items">The items to add.</param>
		/// <returns>A boolean indicating success or failure.</returns>
		// Token: 0x06003942 RID: 14658 RVA: 0x001199CC File Offset: 0x00117BCC
		public static bool AddToPopulation(string table, string sibling, params PopulationItem[] items)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.Populations.TryGetValue(table, out populationInfo))
			{
				return false;
			}
			PopulationGroup populationGroup = PopulationManager.FindGroup(populationInfo, sibling);
			if (populationGroup == null)
			{
				return false;
			}
			populationGroup.Items.AddRange(items);
			return true;
		}

		/// <summary>
		/// Recursively searches for a population group with the specified object blueprint or table name.
		/// </summary>
		// Token: 0x06003943 RID: 14659 RVA: 0x00119A04 File Offset: 0x00117C04
		public static PopulationGroup FindGroup(PopulationGroup target, string needle)
		{
			foreach (PopulationItem populationItem in target.Items)
			{
				if (populationItem.Name == needle && populationItem is PopulationTable)
				{
					return target;
				}
				PopulationObject populationObject = populationItem as PopulationObject;
				if (((populationObject != null) ? populationObject.Blueprint : null) == needle)
				{
					return target;
				}
				PopulationGroup populationGroup = populationItem as PopulationGroup;
				if (populationGroup != null && populationGroup.Items.Count > 0)
				{
					populationGroup = PopulationManager.FindGroup(populationGroup, needle);
					if (populationGroup != null)
					{
						return populationGroup;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Recursively searches for a population group with the specified object blueprint or table name.
		/// </summary>
		// Token: 0x06003944 RID: 14660 RVA: 0x00119AB4 File Offset: 0x00117CB4
		public static PopulationGroup FindGroup(PopulationInfo target, string needle)
		{
			foreach (PopulationItem populationItem in target.Items)
			{
				PopulationGroup populationGroup = populationItem as PopulationGroup;
				if (populationGroup != null && populationGroup.Items.Count > 0)
				{
					populationGroup = PopulationManager.FindGroup(populationGroup, needle);
					if (populationGroup != null)
					{
						return populationGroup;
					}
				}
			}
			return null;
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x00119B28 File Offset: 0x00117D28
		public static void ReplaceVariables(ref string Blueprint, Dictionary<string, string> Variables)
		{
			int num = Blueprint.IndexOf('{');
			if (num == -1)
			{
				return;
			}
			int length = Blueprint.Length;
			StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null).Append(Blueprint, 0, num);
			int num3;
			for (;;)
			{
				int num2 = Blueprint.IndexOf('}', num + 1, length - num - 1);
				if (num2 == -1)
				{
					break;
				}
				num3 = num2;
				string text = Blueprint.Substring(num + 1, num3 - num - 1);
				string text2;
				if (Variables.TryGetValue(text, out text2))
				{
					stringBuilder.Append(text2);
				}
				else
				{
					Debug.LogWarning("Unknown population variable key:" + text);
					stringBuilder.Append(Blueprint, num, num3 - num + 1);
				}
				num2 = Blueprint.IndexOf('{', num3 + 1, length - num3 - 1);
				if (num2 == -1)
				{
					goto Block_4;
				}
				num = num2;
				stringBuilder.Append(Blueprint, num3 + 1, num - num3 - 1);
			}
			stringBuilder.Append(Blueprint, num, length - num);
			goto IL_00E0;
			Block_4:
			stringBuilder.Append(Blueprint, num3 + 1, length - num3 - 1);
			IL_00E0:
			Blueprint = stringBuilder.ToString();
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x00119C20 File Offset: 0x00117E20
		private static void LoadFiles()
		{
			PopulationManager._Populations = new Dictionary<string, PopulationInfo>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Populations", true, true))
			{
				try
				{
					PopulationManager.HandleNodes(xmlDataHelper);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Error reading " + DataManager.SanitizePathForDisplay(xmlDataHelper.BaseURI), ex);
				}
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00119CA8 File Offset: 0x00117EA8
		private static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(PopulationManager.Nodes, null);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x00119CB8 File Offset: 0x00117EB8
		private static void HandlePopulationNode(XmlDataHelper Reader)
		{
			if (PopulationManager.CurrentReadingPopulation != null)
			{
				Reader.ParseWarning("Loading a new population when we seemingly never parsed the old one.");
			}
			PopulationInfo populationInfo = new PopulationInfo();
			PopulationManager.CurrentReadingPopulation = populationInfo;
			populationInfo.Name = Reader.ParseAttribute<string>("Name", populationInfo.Name, true, null);
			populationInfo.Style = Reader.GetAttribute("Style");
			populationInfo.Hint = Reader.GetAttribute("Hint");
			populationInfo.Load = Reader.GetAttribute("Load").Coalesce("Merge");
			Reader.HandleNodes(PopulationManager.PopulationSubNodes, null);
			PopulationInfo populationInfo2;
			if (populationInfo.Merge && PopulationManager.Populations.TryGetValue(populationInfo.Name, out populationInfo2))
			{
				populationInfo2.MergeFrom(populationInfo, populationInfo2);
			}
			else if (populationInfo.Remove)
			{
				PopulationManager.Populations.Remove(populationInfo.Name);
			}
			else if (populationInfo.Replace)
			{
				PopulationManager.Populations[populationInfo.Name] = populationInfo;
			}
			else
			{
				PopulationManager.Populations.Add(populationInfo.Name, populationInfo);
			}
			PopulationManager.CurrentReadingPopulation = null;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x00119DB8 File Offset: 0x00117FB8
		private static void HandleObjectNode(XmlDataHelper xml)
		{
			PopulationObject populationObject = PopulationManager.LoadPopulationObject(xml);
			PopulationManager.CurrentReadingPopulation.Items.Add(populationObject);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00119DDC File Offset: 0x00117FDC
		private static void HandleTableNode(XmlDataHelper xml)
		{
			PopulationTable populationTable = PopulationManager.LoadPopulationTable(xml);
			PopulationManager.CurrentReadingPopulation.Items.Add(populationTable);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x00119E00 File Offset: 0x00118000
		private static void HandleGroupNode(XmlDataHelper xml)
		{
			PopulationGroup populationGroup = PopulationManager.LoadPopulationGroup(xml, PopulationManager.CurrentReadingPopulation);
			if (populationGroup != null)
			{
				populationGroup.Parent = PopulationManager.CurrentReadingPopulation;
				PopulationManager.CurrentReadingPopulation.Items.Add(populationGroup);
			}
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x00119E38 File Offset: 0x00118038
		private static void HandleGroupObjectNode(XmlDataHelper xml)
		{
			PopulationObject populationObject = PopulationManager.LoadPopulationObject(xml);
			PopulationManager.CurrentReadingGroup.Items.Add(populationObject);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x00119E5C File Offset: 0x0011805C
		private static void HandleGroupTableNode(XmlDataHelper xml)
		{
			PopulationTable populationTable = PopulationManager.LoadPopulationTable(xml);
			PopulationManager.CurrentReadingGroup.Items.Add(populationTable);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x00119E80 File Offset: 0x00118080
		private static void HandleGroupGroupNode(XmlDataHelper xml)
		{
			PopulationGroup populationGroup = PopulationManager.LoadPopulationGroup(xml, PopulationManager.CurrentReadingPopulation);
			if (populationGroup != null)
			{
				populationGroup.Parent = PopulationManager.CurrentReadingGroup;
				PopulationManager.CurrentReadingGroup.Items.Add(populationGroup);
			}
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x00119EB8 File Offset: 0x001180B8
		private static PopulationGroup LoadPopulationGroup(XmlDataHelper Reader, PopulationInfo Info)
		{
			PopulationGroup populationGroup = new PopulationGroup();
			populationGroup.Name = Reader.ParseAttribute<string>("Name", populationGroup.Name, true, null);
			populationGroup.Style = Reader.GetAttribute("Style");
			populationGroup.Chance = Reader.GetAttribute("Chance");
			populationGroup.Number = Reader.GetAttribute("Number");
			populationGroup.Hint = Reader.GetAttribute("Hint");
			populationGroup.Load = Reader.GetAttribute("Load").Coalesce("Merge");
			uint num;
			if (uint.TryParse(Reader.GetAttribute("Weight"), out num))
			{
				populationGroup.Weight = num;
			}
			if (!populationGroup.Name.IsNullOrEmpty() && Info.GroupLookup.ContainsKey(populationGroup.Name))
			{
				throw new XmlException("Duplicate group name '" + populationGroup.Name + "'", Reader);
			}
			PopulationGroup currentReadingGroup = PopulationManager.CurrentReadingGroup;
			PopulationManager.CurrentReadingGroup = populationGroup;
			Reader.HandleNodes(PopulationManager.GroupSubNodes, null);
			PopulationManager.CurrentReadingGroup = currentReadingGroup;
			if (!populationGroup.Name.IsNullOrEmpty())
			{
				Info.GroupLookup[populationGroup.Name] = populationGroup;
			}
			return populationGroup;
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x00119FD8 File Offset: 0x001181D8
		private static PopulationObject LoadPopulationObject(XmlDataHelper Reader)
		{
			PopulationObject populationObject = new PopulationObject();
			populationObject.Name = Reader.GetAttribute("Name");
			populationObject.Blueprint = Reader.GetAttribute("Blueprint");
			populationObject.Number = Reader.GetAttribute("Number");
			populationObject.Chance = Reader.GetAttribute("Chance");
			populationObject.Hint = Reader.GetAttribute("Hint");
			populationObject.Load = Reader.GetAttribute("Load");
			populationObject.Builder = Reader.GetAttribute("Builder");
			uint num;
			if (uint.TryParse(Reader.GetAttribute("Weight"), out num))
			{
				populationObject.Weight = num;
			}
			Reader.DoneWithElement();
			return populationObject;
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x0011A084 File Offset: 0x00118284
		private static PopulationTable LoadPopulationTable(XmlDataHelper Reader)
		{
			PopulationTable populationTable = new PopulationTable();
			populationTable.Name = Reader.GetAttribute("Name");
			populationTable.Number = Reader.GetAttribute("Number");
			populationTable.Chance = Reader.GetAttribute("Chance");
			populationTable.Hint = Reader.GetAttribute("Hint");
			populationTable.Load = Reader.GetAttribute("Load");
			uint num;
			if (uint.TryParse(Reader.GetAttribute("Weight"), out num))
			{
				populationTable.Weight = num;
			}
			Reader.DoneWithElement();
			return populationTable;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x0011A110 File Offset: 0x00118310
		[WishCommand("population:generate", null)]
		private static void WishGenerate(string Value)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Value.AsDelimitedSpans('#', out readOnlySpan, out readOnlySpan2);
			int num;
			if (!int.TryParse(readOnlySpan2, out num))
			{
				if (!readOnlySpan2.IsEmpty)
				{
					Popup.Show("'" + new string(readOnlySpan2) + "' is not a valid integer.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				num = 1000;
			}
			string text = new string(readOnlySpan);
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(text, out populationInfo))
			{
				Popup.Show("No table by the name '" + text + "' could be resolved.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			HashSet<string> hashSet = new HashSet<string>();
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				foreach (PopulationResult populationResult in populationInfo.Generate(null, null))
				{
					if (hashSet.Add(populationResult.Blueprint))
					{
						int value = dictionary.GetValue(populationResult.Blueprint, 0);
						dictionary[populationResult.Blueprint] = value + 1;
					}
				}
				hashSet.Clear();
				int num3 = i * 100 / num;
				if (num3 > num2)
				{
					num2 = num3;
					Loading.SetLoadingStatus(string.Format("Generating {0} {1}%...", text, num3), false);
				}
			}
			Loading.SetLoadingStatus("Generating " + text + " 100%...", false);
			Loading.SetLoadingStatus(null, false);
			StringBuilder stringBuilder = Strings.SB.Clear();
			foreach (KeyValuePair<string, int> keyValuePair in dictionary.OrderByDescending<KeyValuePair<string, int>, int>((KeyValuePair<string, int> x) => x.Value))
			{
				string text2;
				int num4;
				keyValuePair.Deconstruct(out text2, out num4);
				string text3 = text2;
				int num5 = num4;
				stringBuilder.Append('\n').Append(text3).Append(": ")
					.Append(((double)num5 / (double)num).ToString("P5"));
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append("The population '").Append(readOnlySpan).Append("' did not generate any results.");
			}
			else
			{
				stringBuilder.Insert(0, " generations;\n").Insert(0, num).Insert(0, "' distributed as follows over ")
					.Insert(0, readOnlySpan)
					.Insert(0, "The population '");
			}
			string text4 = stringBuilder.ToString();
			ClipboardHelper.SetClipboardData(text4);
			Popup.Show(text4, null, "Sounds/UI/ui_notification", true, true, true, false, null);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x0011A3B0 File Offset: 0x001185B0
		[WishCommand("population:findblueprint", null)]
		private static void WishFindBlueprint(string Blueprint)
		{
			Loading.LoadTask("Fabricating semantic tables...", new Action(PopulationManager.RequireSemanticTables), true);
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			Stack<PopulationInfo> stack = new Stack<PopulationInfo>();
			for (;;)
			{
				IL_0023:
				int count = PopulationManager.Populations.Count;
				foreach (KeyValuePair<string, PopulationInfo> keyValuePair in PopulationManager.Populations)
				{
					string text;
					PopulationInfo populationInfo;
					keyValuePair.Deconstruct(out text, out populationInfo);
					string text2 = text;
					PopulationInfo populationInfo2 = populationInfo;
					if (!dictionary.ContainsKey(text2))
					{
						Loading.SetLoadingStatus("Analyzing " + text2 + "...", false);
						try
						{
							double num = PopulationManager.FindBlueprintProbability(populationInfo2, Blueprint, stack, dictionary);
							dictionary[text2] = num;
						}
						catch (Exception ex)
						{
							string text3 = text2;
							string text4 = " analyzation: ";
							Exception ex2 = ex;
							Debug.LogError(text3 + text4 + ((ex2 != null) ? ex2.ToString() : null));
						}
						if (PopulationManager.Populations.Count != count)
						{
							goto IL_0023;
						}
					}
				}
				break;
			}
			Loading.SetLoadingStatus(null, false);
			StringBuilder stringBuilder = Strings.SB.Clear();
			foreach (KeyValuePair<string, double> keyValuePair2 in dictionary.OrderByDescending<KeyValuePair<string, double>, double>((KeyValuePair<string, double> x) => x.Value))
			{
				string text;
				double num2;
				keyValuePair2.Deconstruct(out text, out num2);
				string text5 = text;
				double num3 = num2;
				if (num3 > 0.0)
				{
					stringBuilder.Append('\n').Append(text5).Append(": ")
						.Append(num3.ToString("P5"));
				}
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append("The blueprint '").Append(Blueprint).Append("' was not found in any population tables.");
			}
			else
			{
				stringBuilder.Insert(0, "' has the approximate probability of generating at least once from the following tables;\n").Insert(0, Blueprint).Insert(0, "The blueprint '");
			}
			if (!GameObjectFactory.Factory.Blueprints.ContainsKey(Blueprint))
			{
				stringBuilder.Insert(0, "' was not found in any object blueprints.\n").Insert(0, Blueprint).Insert(0, "The blueprint '");
			}
			string text6 = stringBuilder.ToString();
			ClipboardHelper.SetClipboardData(text6);
			Popup.Show(text6, null, "Sounds/UI/ui_notification", true, true, true, false, null);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x0011A608 File Offset: 0x00118808
		private static double FindListProbability(PopulationList List, string Blueprint, Stack<PopulationInfo> Stack, Dictionary<string, double> TableProbabilities)
		{
			double num2;
			if (List.Style.EqualsNoCase("pickone"))
			{
				ulong totalWeight = List.TotalWeight;
				double num = 1.0;
				foreach (PopulationItem populationItem in List.Items)
				{
					num *= 1.0 - populationItem.Weight / totalWeight * PopulationManager.FindBlueprintProbability(populationItem, Blueprint, Stack, TableProbabilities);
				}
				num2 = 1.0 - num;
			}
			else
			{
				double num3 = 1.0;
				foreach (PopulationItem populationItem2 in List.Items)
				{
					num3 *= 1.0 - PopulationManager.FindBlueprintProbability(populationItem2, Blueprint, Stack, TableProbabilities);
				}
				num2 = 1.0 - num3;
			}
			return num2;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x0011A724 File Offset: 0x00118924
		private static double FindBlueprintProbability(PopulationItem Item, string Blueprint, Stack<PopulationInfo> Stack, Dictionary<string, double> TableProbabilities)
		{
			if (Item == null)
			{
				return 0.0;
			}
			PopulationObject populationObject = Item as PopulationObject;
			if (populationObject != null)
			{
				if (!(populationObject.Blueprint == Blueprint))
				{
					return 0.0;
				}
				return populationObject.Probability();
			}
			else
			{
				PopulationGroup populationGroup = Item as PopulationGroup;
				if (populationGroup != null)
				{
					double num = PopulationManager.FindListProbability(populationGroup, Blueprint, Stack, TableProbabilities);
					num = 1.0 - Math.Pow(1.0 - num, populationGroup.AverageNumber());
					return num * populationGroup.Probability();
				}
				PopulationInfo populationInfo = Item as PopulationInfo;
				if (populationInfo != null)
				{
					double num2;
					if (TableProbabilities.TryGetValue(populationInfo.Name, out num2))
					{
						return num2;
					}
					int num3 = 0;
					int count = Stack.Count;
					using (Stack<PopulationInfo>.Enumerator enumerator = Stack.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == populationInfo)
							{
								num3++;
							}
						}
					}
					if ((count >= 30 && num3 >= 1) || (count >= 20 && num3 >= 3) || num3 >= 5)
					{
						return 0.0;
					}
					Stack.Push(populationInfo);
					num2 = PopulationManager.FindListProbability(populationInfo, Blueprint, Stack, TableProbabilities);
					TableProbabilities.TryAdd(Item.Name, num2);
					PopulationInfo populationInfo2 = Stack.Pop();
					if (populationInfo2 != populationInfo)
					{
						Debug.LogError(string.Concat(new string[] { "Evaluation stack is invalid, expected table '", populationInfo.Name, "' but got '", populationInfo2.Name, "'." }));
					}
					return num2;
				}
				else
				{
					PopulationTable populationTable = Item as PopulationTable;
					if (populationTable != null)
					{
						string name = populationTable.Name;
						double num4 = 0.0;
						if (name.Contains("tier}"))
						{
							for (int i = 1; i <= 8; i++)
							{
								string text = i.ToString();
								PopulationInfo populationInfo3 = PopulationManager.ResolvePopulation(name.Replace("{zonetier}", text).Replace("{ownertier}", text), true);
								num4 += PopulationManager.FindBlueprintProbability(populationInfo3, Blueprint, Stack, TableProbabilities);
							}
							num4 /= 8.0;
						}
						else
						{
							num4 = PopulationManager.FindBlueprintProbability(PopulationManager.ResolvePopulation(populationTable.Name, true), Blueprint, Stack, TableProbabilities);
						}
						num4 = 1.0 - Math.Pow(1.0 - num4, populationTable.AverageNumber());
						return num4 * populationTable.Probability();
					}
					Debug.LogError("Unsupported inheritor of PopulationItem: " + Item.GetType().Name + ".");
					return 0.0;
				}
			}
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x0011A99C File Offset: 0x00118B9C
		private static void RequireSemanticTables()
		{
			HashSet<string> hashSet = new HashSet<string>();
			StringBuilder sb = Strings.SB;
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				foreach (KeyValuePair<string, string> keyValuePair in gameObjectBlueprint.Tags)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct(out text, out text2);
					string text3 = text;
					if (text3.StartsWith("Semantic") && hashSet.Add(text3))
					{
						sb.Clear().Append("DynamicSemanticTable:").Append(text3, 8, text3.Length - 8)
							.Append("::");
						int i = 1;
						int length = sb.Length;
						while (i <= 8)
						{
							sb.Length = length;
							sb.Append(i);
							string text4 = sb.ToString();
							if (!PopulationManager._Populations.ContainsKey(text4))
							{
								Loading.SetLoadingStatus("Fabricating " + text4 + "...", false);
								PopulationManager.RequireTable(text4);
							}
							i++;
						}
					}
				}
			}
			Loading.SetLoadingStatus(null, false);
		}

		// Token: 0x04001CDD RID: 7389
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, PopulationInfo> _Populations = null;

		// Token: 0x04001CDE RID: 7390
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, MethodInfo> _PopulationCalls = null;

		// Token: 0x04001CDF RID: 7391
		private static string[] zonetierSplitter = null;

		// Token: 0x04001CE0 RID: 7392
		private static Dictionary<string, Action<XmlDataHelper>> Nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"populations",
				new Action<XmlDataHelper>(PopulationManager.HandleNodes)
			},
			{
				"population",
				new Action<XmlDataHelper>(PopulationManager.HandlePopulationNode)
			}
		};

		// Token: 0x04001CE1 RID: 7393
		private static PopulationInfo CurrentReadingPopulation;

		// Token: 0x04001CE2 RID: 7394
		private static Dictionary<string, Action<XmlDataHelper>> PopulationSubNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"object",
				new Action<XmlDataHelper>(PopulationManager.HandleObjectNode)
			},
			{
				"table",
				new Action<XmlDataHelper>(PopulationManager.HandleTableNode)
			},
			{
				"group",
				new Action<XmlDataHelper>(PopulationManager.HandleGroupNode)
			}
		};

		// Token: 0x04001CE3 RID: 7395
		private static Dictionary<string, Action<XmlDataHelper>> GroupSubNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"object",
				new Action<XmlDataHelper>(PopulationManager.HandleGroupObjectNode)
			},
			{
				"table",
				new Action<XmlDataHelper>(PopulationManager.HandleGroupTableNode)
			},
			{
				"group",
				new Action<XmlDataHelper>(PopulationManager.HandleGroupGroupNode)
			}
		};

		// Token: 0x04001CE4 RID: 7396
		private static PopulationGroup CurrentReadingGroup;
	}
}
