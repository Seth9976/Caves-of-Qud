using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using ConsoleLib.Console;
using HarmonyLib;
using ModelShark;
using Platform.IO;
using Qud.API;
using Qud.UI;
using TMPro;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014AA RID: 5290
	[HasOptionFlagUpdate]
	[HasModSensitiveStaticCache]
	public class Options
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600CFAE RID: 53166 RVA: 0x004C57BD File Offset: 0x004C39BD
		public static int ThrottleAnimationRate
		{
			get
			{
				if (Options.ThrottleAnimation)
				{
					return 100;
				}
				return 20;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600CFAF RID: 53167 RVA: 0x004C57CB File Offset: 0x004C39CB
		// (set) Token: 0x0600CFB0 RID: 53168 RVA: 0x004C57D2 File Offset: 0x004C39D2
		public static bool PickTargetLocked
		{
			get
			{
				return Options._PickTargetLocked;
			}
			set
			{
				Options.SetOption("OptionPickTargetLocked", value);
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600CFB1 RID: 53169 RVA: 0x004C57DF File Offset: 0x004C39DF
		// (set) Token: 0x0600CFB2 RID: 53170 RVA: 0x004C57E6 File Offset: 0x004C39E6
		public static bool LookLocked
		{
			get
			{
				return Options._LookLocked;
			}
			set
			{
				Options.SetOption("OptionLookLocked", value);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600CFB3 RID: 53171 RVA: 0x004C57F3 File Offset: 0x004C39F3
		public static bool UseParticleVFX
		{
			get
			{
				return Options.UseImposters && Options._UseParticleVFX;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x0600CFB4 RID: 53172 RVA: 0x004C5803 File Offset: 0x004C3A03
		public static bool UseImposters
		{
			get
			{
				return !Options.DisableImposters;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600CFB5 RID: 53173 RVA: 0x004C580D File Offset: 0x004C3A0D
		public static bool UseTiles
		{
			get
			{
				return Globals.RenderMode == RenderModeType.Tiles;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600CFB6 RID: 53174 RVA: 0x004C5818 File Offset: 0x004C3A18
		public static Options.PlayAreaScaleTypes PlayScale
		{
			get
			{
				Options.PlayAreaScaleTypes? playScaleOverride = Options.PlayScaleOverride;
				if (playScaleOverride == null)
				{
					return Options._PlayScaleBind;
				}
				return playScaleOverride.GetValueOrDefault();
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (set) Token: 0x0600CFB7 RID: 53175 RVA: 0x004C5844 File Offset: 0x004C3A44
		[OptionFlag("OptionPlayScale")]
		private static string PlayScaleBind
		{
			set
			{
				Options.PlayAreaScaleTypes playAreaScaleTypes;
				if (!(value == "Fit"))
				{
					if (!(value == "Cover"))
					{
						if (!(value == "Pixel Perfect"))
						{
							playAreaScaleTypes = Options.PlayAreaScaleTypes.Fit;
						}
						else
						{
							playAreaScaleTypes = Options.PlayAreaScaleTypes.PixelPerfect;
						}
					}
					else
					{
						playAreaScaleTypes = Options.PlayAreaScaleTypes.Cover;
					}
				}
				else
				{
					playAreaScaleTypes = Options.PlayAreaScaleTypes.Fit;
				}
				Options._PlayScaleBind = playAreaScaleTypes;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (set) Token: 0x0600CFB8 RID: 53176 RVA: 0x004C5890 File Offset: 0x004C3A90
		[OptionFlag("AbilityBarMode")]
		private static string AbilityBarModeBind
		{
			set
			{
				int num;
				if (value == "Compact")
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
				Options.AbilityBarMode = num;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (set) Token: 0x0600CFB9 RID: 53177 RVA: 0x004C58B8 File Offset: 0x004C3AB8
		[OptionFlag("OptionMouseCursor")]
		private static string MouseCursorBind
		{
			set
			{
				int num;
				if (!(value == "System"))
				{
					if (!(value == "Default"))
					{
						if (!(value == "Alternate"))
						{
							num = 1;
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						num = 1;
					}
				}
				else
				{
					num = 0;
				}
				Options.MouseCursor = num;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (set) Token: 0x0600CFBA RID: 53178 RVA: 0x004C5902 File Offset: 0x004C3B02
		[OptionFlag("OptionAutoexploreIgnoreEasyEnemies")]
		private static string AutoexploreIgnoreEasyEnemiesBind
		{
			set
			{
				Options.AutoexploreIgnoreEasyEnemies = DifficultyEvaluation.GetDifficultyFromDescription(value);
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (set) Token: 0x0600CFBB RID: 53179 RVA: 0x004C5910 File Offset: 0x004C3B10
		[OptionFlag("OptionAutoexploreIgnoreDistantEnemies")]
		private static string AutoexploreIgnoreDistantEnemiesBind
		{
			set
			{
				int num;
				Options.AutoexploreIgnoreDistantEnemies = (int.TryParse(value, out num) ? num : 9999999);
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600CFBC RID: 53180 RVA: 0x004C5934 File Offset: 0x004C3B34
		public static int TileScale
		{
			get
			{
				int num;
				if (Options.PlayScale == Options.PlayAreaScaleTypes.PixelPerfect)
				{
					num = Options._TileScale;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (set) Token: 0x0600CFBD RID: 53181 RVA: 0x004C5954 File Offset: 0x004C3B54
		[OptionFlag("OptionDockMovable")]
		private static string DockMovableBind
		{
			set
			{
				int num;
				if (!(value == "Flip"))
				{
					if (!(value == "Right"))
					{
						if (!(value == "Left"))
						{
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
					else
					{
						num = 2;
					}
				}
				else
				{
					num = 3;
				}
				Options.DockMovable = num;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (set) Token: 0x0600CFBE RID: 53182 RVA: 0x004C59A0 File Offset: 0x004C3BA0
		[OptionFlag("OptionDockOpacity")]
		private static string DockOpacityBind
		{
			set
			{
				int num;
				if (int.TryParse(value, out num))
				{
					Options.DockOpacity = (float)num / 100f;
					return;
				}
				Options.DockOpacity = 1f;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (set) Token: 0x0600CFBF RID: 53183 RVA: 0x004C59D0 File Offset: 0x004C3BD0
		[OptionFlag("OptionPrereleaseStageScale")]
		private static string StageScaleBind
		{
			set
			{
				Options.StageScaleRaw = value;
				double num = 1.0;
				if (value.StartsWith("auto"))
				{
					if (value == "auto x1.25")
					{
						num = 1.25;
					}
					if (value == "auto x1.5")
					{
						num = 1.5;
					}
					Options.StageScale = Math.Min((double)Screen.width * num / 1920.0, (double)Screen.height * num / 640.0);
					return;
				}
				if (double.TryParse(value, out num))
				{
					Options.StageScale = num;
					return;
				}
				Options.StageScale = 1.0;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600CFC0 RID: 53184 RVA: 0x004C5A76 File Offset: 0x004C3C76
		public static bool PrereleaseInputManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600CFC1 RID: 53185 RVA: 0x004C5A79 File Offset: 0x004C3C79
		public static bool SifrahExamine
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahExamine", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600CFC2 RID: 53186 RVA: 0x004C5AA0 File Offset: 0x004C3CA0
		public static string SifrahExamineAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahExamineAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600CFC3 RID: 53187 RVA: 0x004C5AEE File Offset: 0x004C3CEE
		public static bool SifrahRepair
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahRepair", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600CFC4 RID: 53188 RVA: 0x004C5B14 File Offset: 0x004C3D14
		public static string SifrahRepairAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahRepairAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600CFC5 RID: 53189 RVA: 0x004C5B62 File Offset: 0x004C3D62
		public static bool SifrahReverseEngineer
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahReverseEngineer", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600CFC6 RID: 53190 RVA: 0x004C5B86 File Offset: 0x004C3D86
		public static bool SifrahDisarming
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahDisarming", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600CFC7 RID: 53191 RVA: 0x004C5BAC File Offset: 0x004C3DAC
		public static string SifrahDisarmingAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahDisarmingAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600CFC8 RID: 53192 RVA: 0x004C5BFA File Offset: 0x004C3DFA
		public static bool SifrahHaggling
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahHaggling", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600CFC9 RID: 53193 RVA: 0x004C5C1E File Offset: 0x004C3E1E
		public static bool SifrahRecruitment
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahRecruitment", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600CFCA RID: 53194 RVA: 0x004C5C44 File Offset: 0x004C3E44
		public static string SifrahRecruitmentAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahRecruitmentAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600CFCB RID: 53195 RVA: 0x004C5C92 File Offset: 0x004C3E92
		public static bool SifrahHacking
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahHacking", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600CFCC RID: 53196 RVA: 0x004C5CB8 File Offset: 0x004C3EB8
		public static string SifrahHackingAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahHackingAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600CFCD RID: 53197 RVA: 0x004C5D08 File Offset: 0x004C3F08
		public static string SifrahHackingLowLevel
		{
			get
			{
				string text = Options.GetOption("OptionSifrahHackingLowLevel", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600CFCE RID: 53198 RVA: 0x004C5D56 File Offset: 0x004C3F56
		public static bool SifrahItemNaming
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahItemNaming", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600CFCF RID: 53199 RVA: 0x004C5D7C File Offset: 0x004C3F7C
		public static string SifrahItemModding
		{
			get
			{
				if (!SifrahGame.Installed)
				{
					return "Never";
				}
				string text = Options.GetOption("OptionSifrahItemModding", "");
				if (text.IsNullOrEmpty())
				{
					text = "Never";
				}
				return text;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600CFD0 RID: 53200 RVA: 0x004C5DB5 File Offset: 0x004C3FB5
		public static bool SifrahRealityDistortion
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahRealityDistortion", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600CFD1 RID: 53201 RVA: 0x004C5DDC File Offset: 0x004C3FDC
		public static string SifrahRealityDistortionAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahRealityDistortionAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600CFD2 RID: 53202 RVA: 0x004C5E2A File Offset: 0x004C402A
		public static bool SifrahPsychicCombat
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahPsychicCombat", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600CFD3 RID: 53203 RVA: 0x004C5E50 File Offset: 0x004C4050
		public static string SifrahPsychicCombatAuto
		{
			get
			{
				string text = Options.GetOption("OptionSifrahPsychicCombatAuto", "");
				if (text.EqualsNoCase("Yes"))
				{
					text = "Always";
				}
				else if (text.EqualsNoCase("No") || text.IsNullOrEmpty())
				{
					text = "Ask";
				}
				return text;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600CFD4 RID: 53204 RVA: 0x004C5EA0 File Offset: 0x004C40A0
		public static string SifrahWaterRitual
		{
			get
			{
				if (!SifrahGame.Installed)
				{
					return "Never";
				}
				string text = Options.GetOption("OptionSifrahWaterRitual", "");
				if (text.IsNullOrEmpty())
				{
					text = "Never";
				}
				return text;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600CFD5 RID: 53205 RVA: 0x004C5ED9 File Offset: 0x004C40D9
		public static bool SifrahBaetylOfferings
		{
			get
			{
				return SifrahGame.Installed && Options.GetOption("OptionSifrahBaetylOfferings", "").EqualsNoCase("Yes");
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600CFD6 RID: 53206 RVA: 0x004C5F00 File Offset: 0x004C4100
		public static bool AnySifrah
		{
			get
			{
				return SifrahGame.Installed && (Options.SifrahExamine || Options.SifrahRepair || Options.SifrahReverseEngineer || Options.SifrahDisarming || Options.SifrahHaggling || Options.SifrahRecruitment || Options.SifrahHacking || Options.SifrahItemNaming || Options.SifrahItemModding != "Never" || Options.SifrahRealityDistortion || Options.SifrahPsychicCombat || Options.SifrahWaterRitual != "Never" || Options.SifrahBaetylOfferings);
			}
		}

		// Token: 0x0600CFD7 RID: 53207 RVA: 0x004C5F85 File Offset: 0x004C4185
		public static void SetOption(string ID, bool Value)
		{
			Options.SetOption(ID, Value ? "Yes" : "No");
		}

		// Token: 0x0600CFD8 RID: 53208 RVA: 0x004C5F9C File Offset: 0x004C419C
		public static void SetOption(string ID, string Value)
		{
			using (Options.Lock.TakeWriteLock())
			{
				Options.Bag.SetValue(ID, Value, true);
			}
			Options.UpdateFlags(ID, false);
		}

		// Token: 0x0600CFD9 RID: 53209 RVA: 0x004C5FE8 File Offset: 0x004C41E8
		public static bool HasOption(string ID)
		{
			bool flag;
			using (Options.Lock.TakeReadLock())
			{
				flag = Options.OptionsByID.ContainsKey(ID);
			}
			return flag;
		}

		// Token: 0x0600CFDA RID: 53210 RVA: 0x004C6030 File Offset: 0x004C4230
		public static bool HasOptionValue(string ID)
		{
			if (Options.Bag == null)
			{
				Debug.LogWarning("Accessing options pre-init: " + ID);
				return false;
			}
			bool flag;
			using (Options.Lock.TakeReadLock())
			{
				flag = Options.Bag.GetValue(ID, null) != null;
			}
			return flag;
		}

		// Token: 0x0600CFDB RID: 53211 RVA: 0x004C6094 File Offset: 0x004C4294
		public static string GetOption(string ID, string Default = "")
		{
			if (Options.Bag == null)
			{
				Debug.LogWarning("Accessing options pre-init: " + ID);
				return Default;
			}
			string text;
			using (Options.Lock.TakeReadLock())
			{
				string value = Options.Bag.GetValue(ID, null);
				if (value != null)
				{
					text = value;
				}
				else if (Options.DefaultsByID.TryGetValue(ID, out value))
				{
					text = value;
				}
				else
				{
					text = Default;
				}
			}
			return text;
		}

		// Token: 0x0600CFDC RID: 53212 RVA: 0x004C6110 File Offset: 0x004C4310
		public static bool GetOptionBool(string ID)
		{
			return Options.GetOption(ID, "").EqualsNoCase("Yes");
		}

		// Token: 0x0600CFDD RID: 53213 RVA: 0x004C6128 File Offset: 0x004C4328
		public static bool ShouldCheckRequirements()
		{
			if (Options.lastOptionsRequirementState == null)
			{
				Options.lastOptionsRequirementState = new Dictionary<string, bool>();
			}
			bool flag = false;
			foreach (KeyValuePair<string, GameOption> keyValuePair in Options.OptionsByID)
			{
				GameOption.RequiresSpec requires = keyValuePair.Value.Requires;
				bool flag2 = requires == null || requires.RequirementsMet;
				bool flag3;
				if (Options.lastOptionsRequirementState.TryGetValue(keyValuePair.Key, out flag3))
				{
					if (flag3 != flag2)
					{
						flag = true;
						Options.lastOptionsRequirementState.Set(keyValuePair.Key, flag2);
					}
				}
				else
				{
					flag = true;
					Options.lastOptionsRequirementState.Set(keyValuePair.Key, flag2);
				}
			}
			return flag;
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600CFDE RID: 53214 RVA: 0x004C61E4 File Offset: 0x004C43E4
		public static int MessageLogLineSizeAdjustment
		{
			get
			{
				return Options._MessageLogLineSizeAdjustment;
			}
		}

		// Token: 0x0600CFDF RID: 53215 RVA: 0x004C61EC File Offset: 0x004C43EC
		public static void UpdateBindings(string ID = null, bool Refresh = false)
		{
			if (!Options.BindingsInitialized || Refresh)
			{
				Options.OptionBindings.Clear();
				foreach (Type type in ModManager.GetTypesWithAttribute(typeof(HasOptionFlagUpdate), false))
				{
					HasOptionFlagUpdate customAttribute = type.GetCustomAttribute<HasOptionFlagUpdate>();
					string text = customAttribute.Prefix ?? "Option";
					bool fieldFlags = customAttribute.FieldFlags;
					BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
					foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
					{
						bool flag = !fieldFlags || !fieldInfo.IsPublic;
						Options.<UpdateBindings>g__EvaluateMember|270_0(fieldInfo.FieldType, fieldInfo, text, flag);
					}
					foreach (PropertyInfo propertyInfo in type.GetProperties(bindingFlags))
					{
						if (propertyInfo.CanWrite)
						{
							bool flag2 = !fieldFlags || !propertyInfo.SetMethod.IsPublic;
							Options.<UpdateBindings>g__EvaluateMember|270_0(propertyInfo.PropertyType, propertyInfo, text, flag2);
						}
					}
				}
				Options.BindingsInitialized = true;
			}
			if (!ID.IsNullOrEmpty())
			{
				Rack<MemberInfo> rack;
				if (Options.OptionBindings.TryGetValue(ID, out rack))
				{
					Options.<UpdateBindings>g__Update|270_1(ID, rack);
					return;
				}
			}
			else
			{
				foreach (KeyValuePair<string, Rack<MemberInfo>> keyValuePair in Options.OptionBindings)
				{
					string text2;
					Rack<MemberInfo> rack2;
					keyValuePair.Deconstruct(out text2, out rack2);
					string text3 = text2;
					Rack<MemberInfo> rack3 = rack2;
					Options.<UpdateBindings>g__Update|270_1(text3, rack3);
				}
			}
		}

		// Token: 0x0600CFE0 RID: 53216 RVA: 0x004C6398 File Offset: 0x004C4598
		public static void UpdateFlags(string ID = null, bool Refresh = false)
		{
			Options.UpdateBindings(ID, Refresh);
			ObjectFinder instance = ObjectFinder.instance;
			if (instance != null)
			{
				instance.ReadOptions();
			}
			MouseBlocker instance2 = SingletonWindowBase<MouseBlocker>.instance;
			if (instance2 != null)
			{
				instance2.UpdateOptions();
			}
			CursorManager instance3 = CursorManager.instance;
			if (instance3 != null)
			{
				instance3.UpdateOptions();
			}
			if (Options.CapInputBuffer)
			{
				GameManager.bCapInputBuffer = true;
			}
			else
			{
				GameManager.bCapInputBuffer = false;
			}
			int.TryParse(Options.GetOption("OptionMessageLineLogScale", ""), out Options._MessageLogLineSizeAdjustment);
			if (Options.GetOption("OptionUseTiles", "").EqualsNoCase("Yes"))
			{
				Globals.RenderMode = RenderModeType.Tiles;
			}
			else
			{
				Globals.RenderMode = RenderModeType.Text;
			}
			if (Options.ModernUI)
			{
				GameManager.Instance.ModernUI = true;
			}
			else
			{
				GameManager.Instance.ModernUI = false;
			}
			if (Options.Analytics)
			{
				Globals.EnableMetrics = true;
			}
			else
			{
				Globals.EnableMetrics = false;
			}
			if (Options.Sound)
			{
				Globals.EnableSound = true;
			}
			else
			{
				Globals.EnableSound = false;
			}
			if (Options.Music)
			{
				Globals.EnableMusic = true;
			}
			else
			{
				Globals.EnableMusic = false;
			}
			if (Options.Ambient)
			{
				Globals.EnableAmbient = true;
			}
			else
			{
				Globals.EnableAmbient = false;
			}
			Globals.AmbientVolume = (float)Options.AmbientVolume / 100f * 0.5f;
			Globals.InterfaceVolume = (float)Options.InterfaceVolume / 100f;
			Globals.CombatVolume = (float)Options.CombatVolume / 100f;
			int num;
			if (int.TryParse(Options.GetOption("OptionDisplayHPWarning", "40%").TrimEnd('%'), out num))
			{
				Globals.HPWarningThreshold = num;
			}
			else
			{
				Globals.HPWarningThreshold = int.MinValue;
			}
			if (Options.MouseInput)
			{
				GameManager.Instance.MouseInput = true;
			}
			else
			{
				GameManager.Instance.MouseInput = false;
			}
			SoundManager.WriteSoundsToLog = Options.GetOption("OptionWriteSoundsToLog", "").EqualsNoCase("Yes");
			AchievementManager.Enabled = !Options.DisableAchievements;
			int masterVolume = Options.MasterVolume;
			int musicVolume = Options.MusicVolume;
			int soundVolume = Options.SoundVolume;
			GameManager.Instance.compassScale = (float)Convert.ToInt32(Options.GetOption("OptionOverlayCompassScale", "100")) / 100f;
			GameManager.Instance.nearbyObjectsListScale = (float)Convert.ToInt32(Options.GetOption("OptionOverlayNearbyObjectsScale", "100")) / 100f;
			GameManager.Instance.minimapScale = (float)Convert.ToInt32(Options.GetOption("OptionMinimapScale", "100")) / 100f;
			GameManager.Instance.TileScale = Options.TileScale;
			GameManager.Instance.StageScale = Options.StageScale;
			GameManager.Instance.DockMovable = Options.DockMovable;
			GameManager.Instance.DisplayMinimap = Options.OverlayMinimap;
			Harmony.DEBUG = Options.HarmonyDebug;
			int num2 = 2000;
			int.TryParse(Options.GetOption("OptionTooltipDelay", ""), out num2);
			TooltipManager.Instance.tooltipDelay = (float)num2 / 1000f;
			BaseLineWithTooltip.TOOLTIP_DELAY = (float)num2 / 1000f;
			Queue<SoundRequest> soundRequests = SoundManager.SoundRequests;
			lock (soundRequests)
			{
				SoundManager.MasterVolume = (float)masterVolume / 100f;
				SoundManager.MusicVolume = (float)musicVolume / 100f;
				SoundManager.SoundVolume = (float)soundVolume / 100f;
				SoundManager.MusicSources.StopFadeAsync();
			}
			float num3 = (float)Convert.ToInt32(Options.GetOption("OptionKeyRepeatDelay", "")) / 100f;
			float num4 = (float)Convert.ToInt32(Options.GetOption("OptionKeyRepeatRate", "")) / 100f;
			ControlManager.delaytime = 0.1f + 2f * num3;
			ControlManager.repeattime = 0f + 0.2f * (1f - num4);
			ControlManager.updateFont = true;
			Leveler.PlayerLedPrompt = Options.DisplayLedLevelUp;
			IBaseJournalEntry.NotedPrompt = Options.PopupJournalNote;
			if (ModManager.Initialized)
			{
				foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(OptionFlagUpdate), typeof(HasOptionFlagUpdate), true))
				{
					try
					{
						methodInfo.Invoke(null, Array.Empty<object>());
					}
					catch (Exception ex)
					{
						MetricsManager.LogAssemblyError(methodInfo, string.Format("Error invoking {0}.{1}: {2}", methodInfo.DeclaringType.FullName, methodInfo.Name, ex));
					}
				}
			}
			CursorManager instance4 = CursorManager.instance;
			if (instance4 != null)
			{
				instance4.Sync();
			}
			if (Options.GetOption("OptionBugDeckKeyboardShift", "") == "Yes")
			{
				TMP_InputField.keycodeMapper = new Func<global::UnityEngine.KeyCode, char>(Keyboard.UcKeycodeMapper);
				TMP_InputField.enableHorribleDeckWorkaround = true;
			}
			else
			{
				TMP_InputField.enableHorribleDeckWorkaround = false;
			}
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				GameObject.Find("Main Camera").GetComponent<CC_AnalogTV>().enabled = Options.DisplayScanlines;
				GameObject.Find("Main Camera").GetComponent<CC_FastVignette>().enabled = Options.DisplayVignette;
				GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness = (float)Math.Max(-70, Options.DisplayBrightness);
				GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().contrast = (float)Math.Max(-70, Options.DisplayContrast);
				if (Options.DisplayFullscreen)
				{
					string text = Options.FullscreenResolution;
					if (text == "*Max")
					{
						Resolution resolution = GameManager.resolutions.Last<Resolution>();
						text = resolution.width.ToString() + "x" + resolution.height.ToString();
					}
					if (text == "Screen")
					{
						text = Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();
					}
					if (text == "Unset")
					{
						Screen.fullScreen = true;
					}
					else
					{
						string[] array = text.Split('x', StringSplitOptions.None);
						int num5 = Convert.ToInt32(array[0]);
						int num6 = Convert.ToInt32(array[0]);
						Screen.SetResolution(num5, num6, FullScreenMode.FullScreenWindow);
						Screen.SetResolution(num5, num6, FullScreenMode.FullScreenWindow);
					}
				}
				else
				{
					Screen.fullScreen = false;
				}
				if (Options.MusicBackground)
				{
					Application.runInBackground = true;
					SoundManager.MusicSources.SetMusicBackground(true);
				}
				else
				{
					Application.runInBackground = false;
					SoundManager.MusicSources.SetMusicBackground(false);
				}
				string displayFramerate = Options.DisplayFramerate;
				if (displayFramerate == "Unlimited")
				{
					QualitySettings.vSyncCount = 0;
					Application.targetFrameRate = 0;
					return;
				}
				if (displayFramerate == "VSync")
				{
					QualitySettings.vSyncCount = 1;
					Application.targetFrameRate = 60;
					return;
				}
				try
				{
					Application.targetFrameRate = (int)Convert.ToInt16(displayFramerate);
					QualitySettings.vSyncCount = 0;
				}
				catch
				{
					Application.targetFrameRate = 60;
					QualitySettings.vSyncCount = 0;
				}
			}, 0);
		}

		// Token: 0x0600CFE1 RID: 53217 RVA: 0x004C685C File Offset: 0x004C4A5C
		[ModSensitiveCacheInit]
		public static void LoadAllOptions()
		{
			if (GameManager.AwakeComplete)
			{
				Options.LoadOptions();
				Options.LoadModOptions();
				Options.UpdateFlags(null, false);
			}
		}

		// Token: 0x0600CFE2 RID: 53218 RVA: 0x004C6878 File Offset: 0x004C4A78
		public static void LoadOptions()
		{
			Options.OptionsByCategory = new Dictionary<string, List<GameOption>>();
			Options.OptionsByID = new Dictionary<string, GameOption>();
			Options.Bag = new NameValueBag(DataManager.LocalPath("PlayerOptions.json"));
			Options.DefaultsByID = new Dictionary<string, string>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Options", true, false))
			{
				try
				{
					Options.HandleNodes(xmlDataHelper);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Error loading base options", ex, "game_exception");
				}
			}
			Options.Bag.Load();
			if (Options.Bag.GetValue("OptionAutogetDroppedLiquid", null) == null && Options.Bag.GetValue("OptionAutogetNoDroppedLiquid", null).EqualsNoCase("Yes"))
			{
				Options.Bag.SetValue("OptionAutogetDroppedLiquid", "No", true);
			}
		}

		// Token: 0x0600CFE3 RID: 53219 RVA: 0x004C696C File Offset: 0x004C4B6C
		private static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(Options._Nodes, null);
		}

		// Token: 0x0600CFE4 RID: 53220 RVA: 0x004C697C File Offset: 0x004C4B7C
		public static void LoadModOptions()
		{
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Options", false, true))
			{
				try
				{
					Options.HandleNodes(xmlDataHelper);
				}
				catch (Exception ex)
				{
					xmlDataHelper.modInfo.Error(ex);
				}
			}
			SifrahGame.Installed = GlobalConfig.GetBoolSetting("EnableSifrah", false);
		}

		// Token: 0x0600CFE5 RID: 53221 RVA: 0x004C69FC File Offset: 0x004C4BFC
		public static void LoadModOptionDefaults()
		{
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				foreach (FileInfo fileInfo in modInfo.Directory.EnumerateFiles("*Option*.xml", SearchOption.AllDirectories))
				{
					Options.CacheDefaults(fileInfo.FullName);
				}
			}
		}

		// Token: 0x0600CFE6 RID: 53222 RVA: 0x004C6A90 File Offset: 0x004C4C90
		private static void CacheDefaults(string Path)
		{
			Options.Lock.EnterWriteLock();
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(Path))
				{
					while (xmlReader.Read())
					{
						if (!xmlReader.Name.IsNullOrEmpty() && !(xmlReader.Name == "xml"))
						{
							if (!xmlReader.Name.EqualsNoCase("Options"))
							{
								return;
							}
							IL_0096:
							while (xmlReader.Read())
							{
								if (xmlReader.Name.EqualsNoCase("Option") && xmlReader.NodeType != XmlNodeType.EndElement)
								{
									string attribute = xmlReader.GetAttribute("ID");
									string attribute2 = xmlReader.GetAttribute("Default");
									if (attribute != null && attribute2 != null)
									{
										Options.DefaultsByID[attribute] = attribute2;
									}
								}
							}
							goto IL_00AA;
						}
					}
					goto IL_0096;
				}
				IL_00AA:;
			}
			catch (Exception)
			{
			}
			finally
			{
				Options.Lock.ExitWriteLock();
			}
		}

		// Token: 0x0600CFE7 RID: 53223 RVA: 0x004C6B80 File Offset: 0x004C4D80
		private static void HandleOptionNode(XmlDataHelper xml)
		{
			GameOption gameOption = Options.LoadOptionNode(xml);
			if (!Options.OptionsByCategory.ContainsKey(gameOption.Category))
			{
				Options.OptionsByCategory.Add(gameOption.Category, new List<GameOption>());
			}
			Options.OptionsByCategory[gameOption.Category].Add(gameOption);
			Options.OptionsByID.Add(gameOption.ID, gameOption);
		}

		// Token: 0x0600CFE8 RID: 53224 RVA: 0x004C6BE4 File Offset: 0x004C4DE4
		private static GameOption LoadOptionNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("ID", null, true, null);
			GameOption Option;
			if (Options.OptionsByID.TryGetValue(text, out Option))
			{
				Options.OptionsByCategory[Option.Category].Remove(Option);
				Options.OptionsByID.Remove(text);
			}
			else
			{
				Option = new GameOption();
				Option.ID = text;
			}
			Option.DisplayText = xml.ParseAttribute<string>("DisplayText", Option.DisplayText, false, null);
			Option.ConsoleDisplayText = xml.ParseAttribute<string>("ConsoleDisplayText", Option.DisplayText, false, null);
			Option.SwitchDisplayText = xml.ParseAttribute<string>("SwitchDisplayText", Option.ConsoleDisplayText, false, null);
			Option.Category = xml.ParseAttribute<string>("Category", Option.Category ?? "No Category", true, null);
			Option.Requires = xml.ParseAttribute<GameOption.RequiresSpec>("Requires", null, false, null);
			Option.Type = xml.ParseAttribute<string>("Type", Option.Type, false, null);
			Option.SearchKeywords = xml.ParseAttribute<string>("SearchKeywords", Option.SearchKeywords, false, null);
			Option.RequiresCapability = xml.ParseAttribute<CapabilityManager.CapabilityType?>("RequiresCapability", Option.RequiresCapability, false, null);
			Option.Default = CapabilityManager.instance.GetDefaultOptionOverrideForCapabilities(Option.ID, xml.ParseAttribute<string>("Default", Option.Default, false, null));
			Options.DefaultsByID[text] = Option.Default;
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
			{
				Option.DisplayText = Option.ConsoleDisplayText;
			}
			if (Option.Type == "Button")
			{
				Option.OnClick = xml.ParseAttribute<MethodInfo>("OnClick", null, true, null);
			}
			string attribute = xml.GetAttribute("Values");
			if (!attribute.IsNullOrEmpty())
			{
				if (xml.GetAttribute("Values") == "*Resolution")
				{
					HashSet<string> hashSet = new HashSet<string>(GameManager.resolutions.Count);
					foreach (Resolution resolution in GameManager.resolutions)
					{
						string text2 = resolution.width.ToString() + "x" + resolution.height.ToString();
						hashSet.Add(text2);
					}
					hashSet.Add("Screen");
					hashSet.Add("Unset");
					Option.DisplayValues = (Option.Values = hashSet.ToArray<string>());
				}
				else
				{
					using (ScopeDisposedList<string> fromPool = ScopeDisposedList<string>.GetFromPool())
					{
						using (ScopeDisposedList<string> fromPool2 = ScopeDisposedList<string>.GetFromPool())
						{
							foreach (ReadOnlySpan<char> readOnlySpan in attribute.DelimitedBy(','))
							{
								ReadOnlySpan<char> readOnlySpan2;
								ReadOnlySpan<char> readOnlySpan3;
								readOnlySpan.Split('|', out readOnlySpan2, out readOnlySpan3);
								string text3 = new string(readOnlySpan2);
								if (readOnlySpan3.Length != 0)
								{
									if (fromPool2.Count == 0)
									{
										fromPool2.AddRange(fromPool);
									}
									fromPool2.Add(new string(readOnlySpan3));
								}
								else if (fromPool2.Count > 0)
								{
									fromPool2.Add(text3);
								}
								fromPool.Add(text3);
							}
							Option.Values = fromPool.ToArray();
							Option.DisplayValues = ((fromPool2.Count == 0) ? Option.Values : fromPool2.ToArray());
						}
					}
				}
			}
			Option.Min = xml.GetAttributeInt("Min", Option.Min, false);
			Option.Max = xml.GetAttributeInt("Max", Option.Max, false);
			Option.Increment = xml.GetAttributeInt("Increment", Option.Increment, false);
			Option.Restart = xml.GetAttributeBool("Restart", false, false);
			xml.HandleNodes(new Dictionary<string, Action<XmlDataHelper>> { 
			{
				"helptext",
				delegate(XmlDataHelper xml)
				{
					Option.HelpText = xml.GetTextNode();
				}
			} }, null);
			if (Option.Restart)
			{
				string text4 = "This option requires a game restart to take effect.";
				if (Option.HelpText.IsNullOrEmpty())
				{
					Option.HelpText = text4;
				}
				else
				{
					GameOption option = Option;
					option.HelpText = option.HelpText + "\n\n" + text4;
				}
			}
			return Option;
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600CFE9 RID: 53225 RVA: 0x004C70FC File Offset: 0x004C52FC
		public static string StageViewID
		{
			get
			{
				return "Stage";
			}
		}

		// Token: 0x0600CFEC RID: 53228 RVA: 0x004C7198 File Offset: 0x004C5398
		[CompilerGenerated]
		internal static void <UpdateBindings>g__EvaluateMember|270_0(Type Type, MemberInfo Info, string Prefix, bool RequireAttribute)
		{
			OptionFlag customAttribute = Info.GetCustomAttribute<OptionFlag>();
			if (RequireAttribute && customAttribute == null)
			{
				return;
			}
			if (Type != typeof(bool) && Type != typeof(string) && Type != typeof(int) && Type != typeof(float))
			{
				return;
			}
			bool flag = customAttribute != null && customAttribute.AllowMissing;
			string text = ((customAttribute != null) ? customAttribute.ID : null) ?? Info.Name;
			string text2 = Prefix + text;
			if (flag || Options.HasOption(text2))
			{
				text = text2;
			}
			else if (!Options.HasOption(text))
			{
				MetricsManager.LogAssemblyError(Info, string.Concat(new string[]
				{
					"No option by ID '",
					text2,
					"' or '",
					text,
					"' could be resolved for '",
					Info.DeclaringType.FullName,
					".",
					Info.Name,
					"'."
				}));
				return;
			}
			Rack<MemberInfo> rack;
			if (!Options.OptionBindings.TryGetValue(text, out rack))
			{
				rack = (Options.OptionBindings[text] = new Rack<MemberInfo>());
			}
			rack.Add(Info);
		}

		// Token: 0x0600CFED RID: 53229 RVA: 0x004C72AC File Offset: 0x004C54AC
		[CompilerGenerated]
		internal static void <UpdateBindings>g__Update|270_1(string ID, Rack<MemberInfo> Members)
		{
			string option = Options.GetOption(ID, "");
			foreach (MemberInfo memberInfo in Members)
			{
				try
				{
					FieldInfo fieldInfo = memberInfo as FieldInfo;
					if (fieldInfo != null)
					{
						Type fieldType = fieldInfo.FieldType;
						if (fieldType == typeof(bool))
						{
							fieldInfo.SetValue(null, option.EqualsNoCase("Yes"));
						}
						else if (fieldType == typeof(string))
						{
							fieldInfo.SetValue(null, option);
						}
						else if (fieldType == typeof(int))
						{
							int num;
							fieldInfo.SetValue(null, int.TryParse(option, out num) ? num : 0);
						}
						else if (fieldType == typeof(float))
						{
							float num2;
							fieldInfo.SetValue(null, float.TryParse(option, out num2) ? num2 : 0f);
						}
					}
					else
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null)
						{
							Type propertyType = propertyInfo.PropertyType;
							if (propertyType == typeof(bool))
							{
								propertyInfo.SetValue(null, option.EqualsNoCase("Yes"));
							}
							else if (propertyType == typeof(string))
							{
								propertyInfo.SetValue(null, option);
							}
							else if (propertyType == typeof(int))
							{
								int num3;
								propertyInfo.SetValue(null, int.TryParse(option, out num3) ? num3 : 0);
							}
							else if (propertyType == typeof(float))
							{
								float num4;
								propertyInfo.SetValue(null, float.TryParse(option, out num4) ? num4 : 0f);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(memberInfo, ex);
				}
			}
		}

		// Token: 0x040050D8 RID: 20696
		[OptionFlag]
		public static bool ModernUI;

		// Token: 0x040050D9 RID: 20697
		[OptionFlag]
		public static bool ModernCharacterSheet;

		// Token: 0x040050DA RID: 20698
		[OptionFlag]
		public static int MasterVolume;

		// Token: 0x040050DB RID: 20699
		[OptionFlag]
		public static bool Sound;

		// Token: 0x040050DC RID: 20700
		[OptionFlag]
		public static int SoundVolume;

		// Token: 0x040050DD RID: 20701
		[OptionFlag]
		public static bool Music;

		// Token: 0x040050DE RID: 20702
		[OptionFlag]
		public static bool Ambient;

		// Token: 0x040050DF RID: 20703
		[OptionFlag]
		public static int AmbientVolume;

		// Token: 0x040050E0 RID: 20704
		[OptionFlag]
		public static int InterfaceVolume;

		// Token: 0x040050E1 RID: 20705
		[OptionFlag]
		public static int CombatVolume;

		// Token: 0x040050E2 RID: 20706
		[OptionFlag]
		public static int MusicVolume;

		// Token: 0x040050E3 RID: 20707
		[OptionFlag]
		public static bool MusicBackground;

		// Token: 0x040050E4 RID: 20708
		[OptionFlag]
		public static bool AutoexploreChests;

		// Token: 0x040050E5 RID: 20709
		[OptionFlag]
		public static bool AskForWorldmap;

		// Token: 0x040050E6 RID: 20710
		[OptionFlag]
		public static bool AskForOneItem;

		// Token: 0x040050E7 RID: 20711
		[OptionFlag]
		public static bool AskAutostair;

		// Token: 0x040050E8 RID: 20712
		[OptionFlag]
		public static bool ConfirmSwimming;

		// Token: 0x040050E9 RID: 20713
		[OptionFlag]
		public static bool ConfirmDangerousLiquid;

		// Token: 0x040050EA RID: 20714
		[OptionFlag]
		public static bool DisplayLedLevelUp;

		// Token: 0x040050EB RID: 20715
		[OptionFlag]
		public static bool PopupJournalNote;

		// Token: 0x040050EC RID: 20716
		[OptionFlag("Option@AlwaysHPColor")]
		public static bool AlwaysHPColor;

		// Token: 0x040050ED RID: 20717
		[OptionFlag("Option@HPColor")]
		public static bool HPColor;

		// Token: 0x040050EE RID: 20718
		[OptionFlag("Option@MutationColor")]
		public static bool MutationColor;

		// Token: 0x040050EF RID: 20719
		[OptionFlag]
		public static bool StripUIColorText;

		// Token: 0x040050F0 RID: 20720
		[OptionFlag]
		public static bool ShowSidebarAbilities;

		// Token: 0x040050F1 RID: 20721
		[OptionFlag]
		public static bool ShowCurrentCellPopup;

		// Token: 0x040050F2 RID: 20722
		[OptionFlag]
		public static bool ShowDetailedWeaponStats;

		// Token: 0x040050F3 RID: 20723
		[OptionFlag]
		public static bool ShowMonsterHPHearts;

		// Token: 0x040050F4 RID: 20724
		[OptionFlag]
		public static bool ShiftHidesSidebar;

		// Token: 0x040050F5 RID: 20725
		[OptionFlag]
		public static bool ShowNumberOfItems;

		// Token: 0x040050F6 RID: 20726
		[OptionFlag]
		public static bool DisableFloorTextures;

		// Token: 0x040050F7 RID: 20727
		[OptionFlag]
		public static bool HighlightStairs;

		// Token: 0x040050F8 RID: 20728
		[OptionFlag]
		public static bool LocationIntseadOfName;

		// Token: 0x040050F9 RID: 20729
		[OptionFlag]
		public static bool AlphanumericBits;

		// Token: 0x040050FA RID: 20730
		[OptionFlag]
		public static bool AutogetSpecialItems;

		// Token: 0x040050FB RID: 20731
		[OptionFlag]
		public static bool AutogetScrap;

		// Token: 0x040050FC RID: 20732
		[OptionFlag]
		public static bool AutogetFood;

		// Token: 0x040050FD RID: 20733
		[OptionFlag]
		public static bool AutogetBooks;

		// Token: 0x040050FE RID: 20734
		[OptionFlag]
		public static bool AutogetZeroWeight;

		// Token: 0x040050FF RID: 20735
		[OptionFlag]
		public static bool AutogetIfHostiles;

		// Token: 0x04005100 RID: 20736
		[OptionFlag]
		public static bool AutogetFromNearby;

		// Token: 0x04005101 RID: 20737
		[OptionFlag]
		public static bool AutogetDroppedLiquid;

		// Token: 0x04005102 RID: 20738
		[OptionFlag]
		public static bool TakeallCorpses;

		// Token: 0x04005103 RID: 20739
		[OptionFlag]
		public static bool AutogetNuggets;

		// Token: 0x04005104 RID: 20740
		[OptionFlag]
		public static bool AutogetTradeGoods;

		// Token: 0x04005105 RID: 20741
		[OptionFlag]
		public static bool AutogetFreshWater;

		// Token: 0x04005106 RID: 20742
		[OptionFlag]
		public static bool DebugShowFullZoneDuringBuild;

		// Token: 0x04005107 RID: 20743
		[OptionFlag]
		public static bool DebugDamagePenetrations;

		// Token: 0x04005108 RID: 20744
		[OptionFlag]
		public static bool DebugSavingThrows;

		// Token: 0x04005109 RID: 20745
		[OptionFlag]
		public static bool DebugGetLostChance;

		// Token: 0x0400510A RID: 20746
		[OptionFlag]
		public static bool DebugStatShift;

		// Token: 0x0400510B RID: 20747
		[OptionFlag]
		public static bool DebugEncounterChance;

		// Token: 0x0400510C RID: 20748
		[OptionFlag]
		public static bool DebugTravelSpeed;

		// Token: 0x0400510D RID: 20749
		[OptionFlag]
		public static bool DebugInternals;

		// Token: 0x0400510E RID: 20750
		[OptionFlag]
		public static bool DebugAttitude;

		// Token: 0x0400510F RID: 20751
		[OptionFlag]
		public static bool InventoryConsistencyCheck;

		// Token: 0x04005110 RID: 20752
		[OptionFlag]
		public static bool ShowReachable;

		// Token: 0x04005111 RID: 20753
		[OptionFlag]
		public static bool ShowOverlandEncounters;

		// Token: 0x04005112 RID: 20754
		[OptionFlag]
		public static bool ShowOverlandRegions;

		// Token: 0x04005113 RID: 20755
		[OptionFlag("OptionShowQuickstart")]
		public static bool ShowQuickstartOption;

		// Token: 0x04005114 RID: 20756
		[OptionFlag]
		public static bool AllowReallydie;

		// Token: 0x04005115 RID: 20757
		[OptionFlag]
		public static bool AllowSaveLoad;

		// Token: 0x04005116 RID: 20758
		[OptionFlag]
		public static bool DisablePermadeath;

		// Token: 0x04005117 RID: 20759
		[OptionFlag]
		public static bool EnablePrereleaseContent;

		// Token: 0x04005118 RID: 20760
		[OptionFlag]
		public static bool EnableWishRegionNames;

		// Token: 0x04005119 RID: 20761
		[OptionFlag]
		public static bool DisableTryLimit;

		// Token: 0x0400511A RID: 20762
		[OptionFlag]
		public static bool DisableDefectLimit;

		// Token: 0x0400511B RID: 20763
		[OptionFlag]
		public static bool GivesRepShowsCurrentRep;

		// Token: 0x0400511C RID: 20764
		[OptionFlag]
		public static bool DisableImposters;

		// Token: 0x0400511D RID: 20765
		[OptionFlag]
		public static bool DisableAchievements;

		// Token: 0x0400511E RID: 20766
		[OptionFlag]
		public static bool CheckMemory;

		// Token: 0x0400511F RID: 20767
		[OptionFlag]
		public static bool DrawPopulationHintMaps;

		// Token: 0x04005120 RID: 20768
		[OptionFlag]
		public static bool DrawInfluenceMaps;

		// Token: 0x04005121 RID: 20769
		[OptionFlag]
		public static bool DrawPathfinder;

		// Token: 0x04005122 RID: 20770
		[OptionFlag]
		public static bool DrawPathfinderHalt;

		// Token: 0x04005123 RID: 20771
		[OptionFlag]
		public static bool DrawNavigationWeightMaps;

		// Token: 0x04005124 RID: 20772
		[OptionFlag]
		public static bool DrawCASystems;

		// Token: 0x04005125 RID: 20773
		[OptionFlag]
		public static bool DrawFloodVis;

		// Token: 0x04005126 RID: 20774
		[OptionFlag]
		public static bool DrawFloodAud;

		// Token: 0x04005127 RID: 20775
		[OptionFlag]
		public static bool DrawFloodOlf;

		// Token: 0x04005128 RID: 20776
		[OptionFlag]
		public static bool DrawFloodExplore;

		// Token: 0x04005129 RID: 20777
		[OptionFlag]
		public static bool DrawArcs;

		// Token: 0x0400512A RID: 20778
		[OptionFlag]
		public static bool DisablePlayerbrain;

		// Token: 0x0400512B RID: 20779
		[OptionFlag]
		public static bool DisableZoneCaching2;

		// Token: 0x0400512C RID: 20780
		[OptionFlag]
		public static bool DisableZoneThawRestrict;

		// Token: 0x0400512D RID: 20781
		[OptionFlag]
		public static bool DebugShowConversationNode;

		// Token: 0x0400512E RID: 20782
		[OptionFlag]
		public static bool AllowCSMods;

		// Token: 0x0400512F RID: 20783
		[OptionFlag]
		public static bool HarmonyDebug;

		// Token: 0x04005130 RID: 20784
		[OptionFlag]
		public static bool TextBuilderDebug;

		// Token: 0x04005131 RID: 20785
		[OptionFlag]
		public static bool OutputModAssembly;

		// Token: 0x04005132 RID: 20786
		public static bool DisableCacheCompression;

		// Token: 0x04005133 RID: 20787
		[OptionFlag]
		public static bool CacheEarly;

		// Token: 0x04005134 RID: 20788
		[OptionFlag]
		public static bool CollectEarly;

		// Token: 0x04005135 RID: 20789
		[OptionFlag]
		public static bool DisableFloorTextureObjects;

		// Token: 0x04005136 RID: 20790
		[OptionFlag]
		public static bool ThrottleAnimation;

		// Token: 0x04005137 RID: 20791
		[OptionFlag]
		public static bool DrawStepImmediately;

		// Token: 0x04005138 RID: 20792
		[OptionFlag]
		public static bool EnableSeed;

		// Token: 0x04005139 RID: 20793
		[OptionFlag]
		public static bool Analytics;

		// Token: 0x0400513A RID: 20794
		[OptionFlag]
		public static bool DisableBloodsplatter;

		// Token: 0x0400513B RID: 20795
		[OptionFlag]
		public static bool DisableSmoke;

		// Token: 0x0400513C RID: 20796
		[OptionFlag]
		public static bool MapDirectionsToKeypad;

		// Token: 0x0400513D RID: 20797
		[OptionFlag]
		public static bool CapInputBuffer;

		// Token: 0x0400513E RID: 20798
		[OptionFlag]
		public static bool LogTurnSeparator;

		// Token: 0x0400513F RID: 20799
		[OptionFlag]
		public static bool IndentBodyParts;

		// Token: 0x04005140 RID: 20800
		[OptionFlag]
		public static bool AbilityCooldownWarningAsMessage;

		// Token: 0x04005141 RID: 20801
		[OptionFlag]
		public static bool PressingRightInInventoryEquips;

		// Token: 0x04005142 RID: 20802
		[OptionFlag]
		public static bool AllowFramelessZoomOut;

		// Token: 0x04005143 RID: 20803
		[OptionFlag]
		public static bool DropAll;

		// Token: 0x04005144 RID: 20804
		[OptionFlag]
		public static bool OverlayNearbyObjectsLocal;

		// Token: 0x04005145 RID: 20805
		[OptionFlag]
		public static bool OverlayNearbyObjectsTakeable;

		// Token: 0x04005146 RID: 20806
		[OptionFlag]
		public static bool OverlayNearbyObjectsPools;

		// Token: 0x04005147 RID: 20807
		[OptionFlag]
		public static bool OverlayNearbyObjectsPlants;

		// Token: 0x04005148 RID: 20808
		[OptionFlag]
		public static bool UseOverlayCombatEffects;

		// Token: 0x04005149 RID: 20809
		[OptionFlag]
		public static bool AutoSip;

		// Token: 0x0400514A RID: 20810
		[OptionFlag]
		public static string AutoSipLevel;

		// Token: 0x0400514B RID: 20811
		[OptionFlag]
		public static int AutosaveInterval;

		// Token: 0x0400514C RID: 20812
		[OptionFlag]
		public static bool AutoTorch;

		// Token: 0x0400514D RID: 20813
		[OptionFlag]
		public static bool AutoDisassembleScrap;

		// Token: 0x0400514E RID: 20814
		[OptionFlag]
		public static bool ShowScavengeItemAsMessage;

		// Token: 0x0400514F RID: 20815
		[OptionFlag]
		public static bool DismemberAsPopup;

		// Token: 0x04005150 RID: 20816
		[OptionFlag]
		public static bool MouseMovement;

		// Token: 0x04005151 RID: 20817
		[OptionFlag]
		public static bool MouseScrollWheel;

		// Token: 0x04005152 RID: 20818
		public static int MinimapScale;

		// Token: 0x04005153 RID: 20819
		[OptionFlag]
		public static int KeyRepeatDelay;

		// Token: 0x04005154 RID: 20820
		[OptionFlag]
		public static int KeyRepeatRate;

		// Token: 0x04005155 RID: 20821
		public static bool OverlayUI;

		// Token: 0x04005156 RID: 20822
		[OptionFlag]
		public static bool DisplayVignette;

		// Token: 0x04005157 RID: 20823
		[OptionFlag]
		public static bool DisplayScanlines;

		// Token: 0x04005158 RID: 20824
		[OptionFlag]
		public static int DisplayBrightness;

		// Token: 0x04005159 RID: 20825
		[OptionFlag]
		public static int DisplayContrast;

		// Token: 0x0400515A RID: 20826
		[OptionFlag("OptionDisplayResolution")]
		public static string FullscreenResolution;

		// Token: 0x0400515B RID: 20827
		[OptionFlag]
		public static bool DisplayFullscreen;

		// Token: 0x0400515C RID: 20828
		[OptionFlag]
		public static string DisplayFramerate;

		// Token: 0x0400515D RID: 20829
		[OptionFlag]
		public static bool ShowErrorPopups;

		// Token: 0x0400515E RID: 20830
		[OptionFlag]
		public static bool UseCombatSounds;

		// Token: 0x0400515F RID: 20831
		[OptionFlag]
		public static bool UseInterfaceSounds;

		// Token: 0x04005160 RID: 20832
		[OptionFlag]
		public static bool MouseInput;

		// Token: 0x04005161 RID: 20833
		[OptionFlag]
		public static bool OverlayMinimap;

		// Token: 0x04005162 RID: 20834
		[OptionFlag]
		public static bool OverlayNearbyObjects;

		// Token: 0x04005163 RID: 20835
		[OptionFlag]
		public static bool DigOnMove;

		// Token: 0x04005164 RID: 20836
		[OptionFlag]
		public static int AutoexploreRate;

		// Token: 0x04005165 RID: 20837
		[OptionFlag]
		public static bool AutogetAmmo;

		// Token: 0x04005166 RID: 20838
		[OptionFlag]
		public static bool AutogetArtifacts;

		// Token: 0x04005167 RID: 20839
		[OptionFlag]
		public static bool AutogetPrimitiveAmmo;

		// Token: 0x04005168 RID: 20840
		[OptionFlag]
		public static bool DisableFullscreenColorEffects;

		// Token: 0x04005169 RID: 20841
		[OptionFlag]
		public static bool DisableFullscreenWarpEffects;

		// Token: 0x0400516A RID: 20842
		[OptionFlag("OptionPlayFireSounds")]
		public static bool UseFireSounds;

		// Token: 0x0400516B RID: 20843
		[OptionFlag("OptionUseOverlayDamageText")]
		public static bool UseCombatText;

		// Token: 0x0400516C RID: 20844
		[OptionFlag]
		public static bool UseTextParticleVFX;

		// Token: 0x0400516D RID: 20845
		[OptionFlag]
		public static bool AutoexploreAttackIgnoredAdjacentEnemies;

		// Token: 0x0400516E RID: 20846
		[OptionFlag]
		public static bool InterruptHeldMovement;

		// Token: 0x0400516F RID: 20847
		[OptionFlag]
		public static bool DisplayMousableZoneTransitionBorder;

		// Token: 0x04005170 RID: 20848
		[OptionFlag("PickTargetLocked", AllowMissing = true)]
		private static bool _PickTargetLocked;

		// Token: 0x04005171 RID: 20849
		[OptionFlag("LookLocked", AllowMissing = true)]
		private static bool _LookLocked;

		// Token: 0x04005172 RID: 20850
		[OptionFlag("OptionUseParticleVFX")]
		private static bool _UseParticleVFX;

		// Token: 0x04005173 RID: 20851
		public static Options.PlayAreaScaleTypes? PlayScaleOverride = null;

		// Token: 0x04005174 RID: 20852
		private static Options.PlayAreaScaleTypes _PlayScaleBind;

		// Token: 0x04005175 RID: 20853
		public static int AbilityBarMode;

		// Token: 0x04005176 RID: 20854
		public static int MouseCursor;

		// Token: 0x04005177 RID: 20855
		public static int AutoexploreIgnoreEasyEnemies;

		// Token: 0x04005178 RID: 20856
		public static int AutoexploreIgnoreDistantEnemies;

		// Token: 0x04005179 RID: 20857
		[OptionFlag("OptionTileScale")]
		private static int _TileScale;

		// Token: 0x0400517A RID: 20858
		public static int DockMovable;

		// Token: 0x0400517B RID: 20859
		public static float DockOpacity = 1f;

		// Token: 0x0400517C RID: 20860
		public static double StageScale = 1.0;

		// Token: 0x0400517D RID: 20861
		public static string StageScaleRaw = "1.0";

		// Token: 0x0400517E RID: 20862
		[OptionFlag]
		public static bool DisableTextAnimationEffects;

		// Token: 0x0400517F RID: 20863
		[OptionFlag]
		public static bool DisableAllIdleTileAnimations;

		// Token: 0x04005180 RID: 20864
		[OptionFlag("OptionUseTextAutowalkThreatIndicator")]
		public static bool UseTextAutoactInterruptionIndicator;

		// Token: 0x04005181 RID: 20865
		[OptionFlag]
		public static bool EnableMods;

		// Token: 0x04005182 RID: 20866
		public static NameValueBag Bag;

		// Token: 0x04005183 RID: 20867
		public static GameOptions Map;

		// Token: 0x04005184 RID: 20868
		public static Dictionary<string, List<GameOption>> OptionsByCategory;

		// Token: 0x04005185 RID: 20869
		public static Dictionary<string, GameOption> OptionsByID;

		// Token: 0x04005186 RID: 20870
		public static Dictionary<string, string> DefaultsByID;

		// Token: 0x04005187 RID: 20871
		private static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

		// Token: 0x04005188 RID: 20872
		private static Dictionary<string, bool> lastOptionsRequirementState;

		// Token: 0x04005189 RID: 20873
		public static int _MessageLogLineSizeAdjustment;

		// Token: 0x0400518A RID: 20874
		private static Dictionary<string, Rack<MemberInfo>> OptionBindings = new Dictionary<string, Rack<MemberInfo>>();

		// Token: 0x0400518B RID: 20875
		private static bool BindingsInitialized = false;

		// Token: 0x0400518C RID: 20876
		private static readonly Dictionary<string, Action<XmlDataHelper>> _Nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"options",
				new Action<XmlDataHelper>(Options.HandleNodes)
			},
			{
				"option",
				new Action<XmlDataHelper>(Options.HandleOptionNode)
			}
		};

		// Token: 0x02001DBF RID: 7615
		public enum PlayAreaScaleTypes
		{
			// Token: 0x040073F4 RID: 29684
			Fit,
			// Token: 0x040073F5 RID: 29685
			Cover,
			// Token: 0x040073F6 RID: 29686
			PixelPerfect
		}
	}
}
