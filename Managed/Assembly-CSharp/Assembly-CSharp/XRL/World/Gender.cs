using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Language;
using XRL.Names;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000952 RID: 2386
	[HasModSensitiveStaticCache]
	public class Gender : BasePronounProvider
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x001E0D03 File Offset: 0x001DEF03
		public override string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06006205 RID: 25093 RVA: 0x001E0D0C File Offset: 0x001DEF0C
		public override string CapitalizedName
		{
			get
			{
				string text;
				if ((text = this._CapitalizedName) == null)
				{
					text = (this._CapitalizedName = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(this.Name));
				}
				return text;
			}
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x001E0D38 File Offset: 0x001DEF38
		public Gender(string __Name = null, bool _Generic = false, bool _Generated = false, bool _Plural = false, bool _PseudoPlural = false, string _Subjective = null, string _Objective = null, string _PossessiveAdjective = null, string _SubstantivePossessive = null, string _Reflexive = null, string _PersonTerm = null, string _ImmaturePersonTerm = null, string _FormalAddressTerm = null, string _OffspringTerm = null, string _SiblingTerm = null, string _ParentTerm = null, bool _UseBareIndicative = false, bool _DoNotReplicateAsPronounSet = false)
			: base(_Generic, _Generated, _Plural, _PseudoPlural, _Subjective, _Objective, _PossessiveAdjective, _SubstantivePossessive, _Reflexive, _PersonTerm, _ImmaturePersonTerm, _FormalAddressTerm, _OffspringTerm, _SiblingTerm, _ParentTerm, _UseBareIndicative)
		{
			this._Name = __Name;
			this.DoNotReplicateAsPronounSet = _DoNotReplicateAsPronounSet;
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x001E0D83 File Offset: 0x001DEF83
		public Gender(BasePronounProvider Original)
			: base(Original)
		{
		}

		/// <summary>Empty Constructor for deserialization purpose, you probably want to pass something.</summary>
		// Token: 0x06006208 RID: 25096 RVA: 0x001E0D98 File Offset: 0x001DEF98
		public Gender()
			: base(false, false, false, false, null, null, null, null, null, null, null, null, null, null, null, false)
		{
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x001E0DC8 File Offset: 0x001DEFC8
		public override void CopyFrom(BasePronounProvider Original)
		{
			Gender gender = Original as Gender;
			if (gender != null)
			{
				this._Name = gender.Name;
				this.DoNotReplicateAsPronounSet = gender.DoNotReplicateAsPronounSet;
			}
			base.CopyFrom(Original);
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x001E0DFE File Offset: 0x001DEFFE
		public override BasePronounProvider Clone()
		{
			return new Gender(this);
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x001E0E08 File Offset: 0x001DF008
		public static void SaveAll(SerializationWriter Writer)
		{
			Writer.Write(Gender.Genders.Count);
			foreach (string text in Gender.Genders.Keys)
			{
				Gender.Genders[text].Save(Writer);
			}
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x001E0E7C File Offset: 0x001DF07C
		public static void LoadAll(SerializationReader Reader)
		{
			if (Gender.Genders == null)
			{
				Gender.Genders = new Dictionary<string, Gender>(32);
			}
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				Gender gender = Gender.Load(Reader);
				Gender.Genders[gender.Name] = gender;
			}
			Gender.SetDefaults();
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x001E0ECC File Offset: 0x001DF0CC
		public override void Save(SerializationWriter Writer)
		{
			base.Save(Writer);
			Writer.Write(this._Name);
			Writer.Write(this.DoNotReplicateAsPronounSet);
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x001E0EF0 File Offset: 0x001DF0F0
		public static Gender Load(SerializationReader Reader)
		{
			Gender gender = new Gender();
			BasePronounProvider.Load(Reader, gender);
			gender._Name = Reader.ReadString();
			gender.DoNotReplicateAsPronounSet = Reader.ReadBoolean();
			return gender;
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x001E0F24 File Offset: 0x001DF124
		private static void SetDefaults()
		{
			if (!Gender.Genders.TryGetValue("neuter", out Gender.DefaultNeuter))
			{
				MetricsManager.LogError("Default gender 'neuter' not found.");
			}
			if (!Gender.Genders.TryGetValue("nonspecific", out Gender.DefaultNonspecific))
			{
				MetricsManager.LogError("Default gender 'nonspecific' not found.");
			}
			if (!Gender.Genders.TryGetValue("plural", out Gender.DefaultPlural))
			{
				MetricsManager.LogError("Default gender 'plural' not found.");
			}
			if (!Gender.Genders.TryGetValue("collective", out Gender.DefaultNonspecific))
			{
				MetricsManager.LogError("Default gender 'collective' not found.");
			}
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x001E0FB4 File Offset: 0x001DF1B4
		public override async Task<bool> CustomizeAsync()
		{
			return await base.Customize("gender");
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x001E0FF8 File Offset: 0x001DF1F8
		protected override async Task<bool> CustomizeProcess(string What)
		{
			for (;;)
			{
				string text = await Popup.AskStringAsync("What name should be used for your " + What + "? (Male, female, etc.)", this.Name, 12, 0, "abcdefghijklmnopqrstuvwxyz ", false, true, null, false, null);
				this._Name = text;
				this._Name = this._Name.Trim(' ');
				while (this._Name.Contains("  "))
				{
					this._Name = this._Name.Replace("  ", " ");
				}
				if (this._Name.IsNullOrEmpty())
				{
					break;
				}
				if (!Gender.Genders.ContainsKey(this._Name))
				{
					goto IL_018A;
				}
				await Popup.ShowAsync("That name is already in use.", true, true, true, true, false);
			}
			return false;
			IL_018A:
			this.DoNotReplicateAsPronounSet = false;
			return await base.CustomizeProcess(What);
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x001E1044 File Offset: 0x001DF244
		public StringBuilder GetSummary(StringBuilder sb)
		{
			return sb.Append("Name: ").Append(this.Name).Append('\n')
				.Append("Generic: ")
				.Append(base.Generic ? "Yes" : "No")
				.Append('\n')
				.Append("Generated: ")
				.Append(base.Generated ? "Yes" : "No")
				.Append('\n')
				.Append("Plural: ")
				.Append(base.Plural ? "Yes" : "No")
				.Append('\n')
				.Append("Pseudo-Plural: ")
				.Append(base.PseudoPlural ? "Yes" : "No")
				.Append('\n')
				.Append("Subjective Pronoun: ")
				.Append(base.Subjective)
				.Append('\n')
				.Append("Objective Pronoun: ")
				.Append(base.Objective)
				.Append('\n')
				.Append("Possessive Adjective: ")
				.Append(base.PossessiveAdjective)
				.Append('\n')
				.Append("Substantive Possessive: ")
				.Append(base.SubstantivePossessive)
				.Append('\n')
				.Append("Reflexive Pronoun: ")
				.Append(base.Reflexive)
				.Append('\n')
				.Append("Person Term: ")
				.Append(base.PersonTerm)
				.Append('\n')
				.Append("Immature Person Term: ")
				.Append(base.ImmaturePersonTerm)
				.Append('\n')
				.Append("Formal Address Term: ")
				.Append(base.FormalAddressTerm)
				.Append('\n')
				.Append("Offspring Term: ")
				.Append(base.OffspringTerm)
				.Append('\n')
				.Append("Sibling Term: ")
				.Append(base.SiblingTerm)
				.Append('\n')
				.Append("Parent Term: ")
				.Append(base.ParentTerm)
				.Append('\n')
				.Append("Indicative Proximal: ")
				.Append(base.IndicativeProximal)
				.Append('\n')
				.Append("Indicative Distal: ")
				.Append(base.IndicativeDistal)
				.Append('\n')
				.Append("Use Bare Indicative: ")
				.Append(base.UseBareIndicative ? "Yes" : "No")
				.Append('\n')
				.Append("Do Not Replicate As Pronoun Set: ")
				.Append(this.DoNotReplicateAsPronounSet ? "Yes" : "No")
				.Append('\n');
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x001E12D6 File Offset: 0x001DF4D6
		public string GetSummary()
		{
			return this.GetSummary(Event.NewStringBuilder(null)).ToString();
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x001E12EC File Offset: 0x001DF4EC
		public StringBuilder GetBasicSummary(StringBuilder sb)
		{
			if (base.Plural)
			{
				sb.Append("Plural\n");
			}
			if (base.PseudoPlural)
			{
				sb.Append("Pseudo-Plural\n");
			}
			if (base.UseBareIndicative)
			{
				sb.Append("Nonpersonal\n");
			}
			return sb.Append("Subjective Pronoun: ").Append(base.Subjective).Append('\n')
				.Append("Objective Pronoun: ")
				.Append(base.Objective)
				.Append('\n')
				.Append("Possessive Adjective: ")
				.Append(base.PossessiveAdjective)
				.Append('\n')
				.Append("Substantive Possessive: ")
				.Append(base.SubstantivePossessive)
				.Append('\n')
				.Append("Reflexive Pronoun: ")
				.Append(base.Reflexive)
				.Append('\n')
				.Append("Person Term: ")
				.Append(base.PersonTerm)
				.Append('\n')
				.Append("Immature Person Term: ")
				.Append(base.ImmaturePersonTerm)
				.Append('\n')
				.Append("Formal Address Term: ")
				.Append(base.FormalAddressTerm)
				.Append('\n')
				.Append("Offspring Term: ")
				.Append(base.OffspringTerm)
				.Append('\n')
				.Append("Sibling Term: ")
				.Append(base.SiblingTerm)
				.Append('\n')
				.Append("Parent Term: ")
				.Append(base.ParentTerm)
				.Append('\n');
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x001E146A File Offset: 0x001DF66A
		public string GetBasicSummary()
		{
			return this.GetBasicSummary(Event.NewStringBuilder(null)).ToString();
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x001E147D File Offset: 0x001DF67D
		public Gender Register()
		{
			if (!Gender.Genders.ContainsKey(this.Name))
			{
				Gender.Genders.Add(this.Name, this);
				Gender.All = null;
			}
			return this;
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x001E14AC File Offset: 0x001DF6AC
		public static Gender Get(string Name, bool GenerateOnFail = false)
		{
			Gender gender;
			if (Gender.Genders.TryGetValue(Name, out gender))
			{
				return gender;
			}
			if (GenerateOnFail)
			{
				Debug.LogError("failed to retrieve gender '" + Name + "', generating a random placeholder");
				return Gender.Generate(false, 0, 0, null, null, null, null, null, null).Register();
			}
			throw new Exception("request for unknown gender '" + Name + "'");
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x001E150C File Offset: 0x001DF70C
		public static Gender GetIfExists(string Name)
		{
			if (Name == null)
			{
				return null;
			}
			Gender gender;
			if (Gender.Genders.TryGetValue(Name, out gender))
			{
				return gender;
			}
			return null;
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x001E1530 File Offset: 0x001DF730
		public static bool Exists(string Name)
		{
			return Gender.Genders.ContainsKey(Name);
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x001E1540 File Offset: 0x001DF740
		private static string GenerateBasePronoun(string stem, string stemVowel, string mainVowel, Dictionary<string, int> suffixes)
		{
			string text;
			if (stemVowel != null)
			{
				text = stem;
			}
			else
			{
				string text2 = (Gender.PronounAltVowelChance.in100() ? Gender.PronounVowels.GetRandomElement<string>(null) : mainVowel);
				text = stem + text2;
			}
			string randomElement = suffixes.GetRandomElement<string>(null);
			if (!randomElement.IsNullOrEmpty() && !text.EndsWith(randomElement))
			{
				text += randomElement;
			}
			if (stem.Length == 1)
			{
				string randomElement2 = Gender.PronounPrefixes.GetRandomElement<string>(null);
				if (!randomElement2.IsNullOrEmpty() && randomElement2 != stem)
				{
					text = randomElement2 + text;
				}
			}
			foreach (string text3 in Gender.PronounMapStrings.Keys)
			{
				if (text.Contains(text3))
				{
					text = text.Replace(text3, Gender.PronounMapStrings[text3]);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair in Gender.PronounMapPatterns)
			{
				text = Regex.Replace(text, keyValuePair.Key, keyValuePair.Value);
			}
			return text;
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x001E167C File Offset: 0x001DF87C
		private static bool CullPronoun(string pronoun)
		{
			foreach (string text in Gender.PronounCullStrings)
			{
				if (pronoun.Contains(text))
				{
					return true;
				}
			}
			foreach (string text2 in Gender.PronounCullPatterns)
			{
				if (Regex.IsMatch(pronoun, text2))
				{
					return true;
				}
			}
			return Grammar.ContainsBadWords(pronoun);
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x001E1728 File Offset: 0x001DF928
		public static void GenerateBasePronouns(out string subjective, out string objective, out string possessive)
		{
			do
			{
				string text = Gender.PronounStems.GetRandomElement<string>(null);
				string text2 = (Gender.PronounStemVowelChance.in100() ? Gender.PronounVowels.GetRandomElement<string>(null) : null);
				if (text2 != null)
				{
					text = text2 + text;
				}
				string text3 = ((text2 != null) ? null : Gender.PronounVowels.GetRandomElement<string>(null));
				subjective = Gender.GenerateBasePronoun(text, text2, text3, Gender.PronounSubjectiveSuffixes);
				objective = Gender.GenerateBasePronoun(text, text2, text3, Gender.PronounObjectiveSuffixes);
				possessive = Gender.GenerateBasePronoun(text, text2, text3, Gender.PronounPossessiveSuffixes);
			}
			while (subjective == possessive || objective == possessive || Gender.CullPronoun(subjective) || Gender.CullPronoun(objective) || Gender.CullPronoun(possessive));
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x001E17E4 File Offset: 0x001DF9E4
		public static Gender Generate(bool Generic = false, int PluralChance = 0, int UseBareIndicativeChance = 0, GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null)
		{
			if (Gender.EnableGeneration)
			{
				string text;
				string text2;
				string text3;
				Gender.GenerateBasePronouns(out text, out text2, out text3);
				int num = 0;
				while (++num <= 1000)
				{
					string text4 = NameMaker.MakeName(For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderName", null, null, false, false, null, null);
					if (!Gender.Genders.ContainsKey(text4) && (num >= 20 || text4.Contains(text, CompareOptions.IgnoreCase) || text4.Contains(text2, CompareOptions.IgnoreCase) || text4.Contains(text3, CompareOptions.IgnoreCase)))
					{
						Gender gender = new Gender(text4, Generic, true, false, false, null, null, null, null, null, null, null, null, null, null, null, false, false);
						if (PluralChance.in100())
						{
							gender.Plural = true;
						}
						if (UseBareIndicativeChance.in100())
						{
							gender.UseBareIndicative = true;
						}
						gender.Subjective = text;
						gender.Objective = text2;
						gender.PossessiveAdjective = text3;
						if (!gender.Plural && gender.Subjective.EndsWith("y"))
						{
							gender.PseudoPlural = true;
						}
						NameMaker.MakeName(ref gender._PersonTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderPersonTerm", null, null, false, null, null);
						NameMaker.MakeName(ref gender._ImmaturePersonTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderImmaturePersonTerm", null, null, false, null, null);
						NameMaker.MakeName(ref gender._FormalAddressTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderFormalAddressTerm", null, null, false, null, null);
						NameMaker.MakeName(ref gender._OffspringTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderOffspringTerm", null, null, false, null, null);
						NameMaker.MakeName(ref gender._SiblingTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderSiblingTerm", null, null, false, null, null);
						NameMaker.MakeName(ref gender._ParentTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderParentTerm", null, null, false, null, null);
						return gender;
					}
				}
				throw new Exception("cannot generate unused gender name");
			}
			bool flag = PluralChance.in100();
			bool flag2 = UseBareIndicativeChance.in100();
			if (Generic)
			{
				if (flag)
				{
					if (!flag2)
					{
						return Gender.GetAnyGenericPersonalPlural();
					}
					return Gender.GetAnyGenericNonpersonalPlural();
				}
				else
				{
					if (!flag2)
					{
						return Gender.GetAnyGenericPersonalSingular();
					}
					return Gender.GetAnyGenericNonpersonalSingular();
				}
			}
			else if (flag)
			{
				if (!flag2)
				{
					return Gender.GetAnyPersonalPlural();
				}
				return Gender.GetAnyNonpersonalPlural();
			}
			else
			{
				if (!flag2)
				{
					return Gender.GetAnyPersonalSingular();
				}
				return Gender.GetAnyNonpersonalSingular();
			}
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x001E1AA0 File Offset: 0x001DFCA0
		public static void Clear()
		{
			Gender.Genders = null;
			Gender.All = null;
			Gender.GenericPlurals = null;
			Gender.Generics = null;
			Gender.GenericSingulars = null;
			Gender.Plurals = null;
			Gender.Singulars = null;
			Gender.GenericPersonalPlurals = null;
			Gender.GenericPersonals = null;
			Gender.GenericPersonalSingulars = null;
			Gender.Personals = null;
			Gender.PersonalPlurals = null;
			Gender.PersonalSingulars = null;
			Gender.GenericNonpersonalPlurals = null;
			Gender.GenericNonpersonals = null;
			Gender.GenericNonpersonalSingulars = null;
			Gender.Nonpersonals = null;
			Gender.NonpersonalPlurals = null;
			Gender.NonpersonalSingulars = null;
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x001E1B1F File Offset: 0x001DFD1F
		[ModSensitiveCacheInit]
		public static void ResetModSensitiveStaticCache()
		{
			Gender.Clear();
			Loading.LoadTask("Loading genders", new Action(Gender.Init), true);
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x001E1B40 File Offset: 0x001DFD40
		public static void Init()
		{
			if (Gender.Genders == null)
			{
				Gender.Genders = new Dictionary<string, Gender>(32);
			}
			Gender.PronounAltVowelChance = 0;
			Gender.PronounStemVowelChance = 0;
			Gender.PronounStems = new Dictionary<string, int>(64);
			Gender.PronounVowels = new Dictionary<string, int>(64);
			Gender.PronounPrefixes = new Dictionary<string, int>(16);
			Gender.PronounSubjectiveSuffixes = new Dictionary<string, int>(16);
			Gender.PronounObjectiveSuffixes = new Dictionary<string, int>(16);
			Gender.PronounPossessiveSuffixes = new Dictionary<string, int>(16);
			Gender.PronounCullStrings = new List<string>(64);
			Gender.PronounCullPatterns = new List<string>(32);
			Gender.PronounMapStrings = new Dictionary<string, string>(64);
			Gender.PronounMapPatterns = new Dictionary<string, string>(32);
			Gender.NamePrefixes = new List<string>(16);
			Gender.NameInfixes = new List<string>(16);
			Gender.NameSuffixes = new List<string>(16);
			string GenerateGenericSpec = null;
			Action<string, bool> action = delegate(string file, bool mod)
			{
				using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(file))
				{
					streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
					while (streamingAssetsXMLStream.Read())
					{
						if (streamingAssetsXMLStream.NodeType == XmlNodeType.Element)
						{
							if (streamingAssetsXMLStream.Name == "genders")
							{
								Gender.LoadGendersNode(streamingAssetsXMLStream, ref GenerateGenericSpec, mod);
							}
						}
						else if (streamingAssetsXMLStream.NodeType == XmlNodeType.EndElement && streamingAssetsXMLStream.Name == "genders")
						{
							break;
						}
					}
					streamingAssetsXMLStream.Close();
				}
			};
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("genders"))
			{
				try
				{
					action(dataFile, dataFile.IsMod);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(dataFile.Mod, ex);
				}
			}
			if (!GenerateGenericSpec.IsNullOrEmpty() && Gender.EnableGeneration)
			{
				int num = GenerateGenericSpec.Roll();
				int num2 = 0;
				foreach (string text in Gender.Genders.Keys)
				{
					Gender gender = Gender.Genders[text];
					if (gender.Generic && gender.Generated)
					{
						num2++;
					}
				}
				for (int i = num2; i < num; i++)
				{
					Gender.Generate(true, 0, 0, null, null, null, null, null, null).Register();
				}
			}
			Gender.SetDefaults();
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x001E1D54 File Offset: 0x001DFF54
		public static void LoadGendersNode(XmlTextReader Reader, ref string GenerateGenericSpec, bool mod = false)
		{
			string attribute = Reader.GetAttribute("EnableSelection");
			if (!attribute.IsNullOrEmpty())
			{
				Gender.EnableSelection = Convert.ToBoolean(attribute);
			}
			string attribute2 = Reader.GetAttribute("EnableGeneration");
			if (!attribute2.IsNullOrEmpty())
			{
				Gender.EnableGeneration = Convert.ToBoolean(attribute2);
			}
			string attribute3 = Reader.GetAttribute("EnableDisplay");
			if (!attribute3.IsNullOrEmpty())
			{
				Gender.EnableDisplay = Convert.ToBoolean(attribute3);
			}
			string attribute4 = Reader.GetAttribute("GenerateGeneric");
			if (!attribute4.IsNullOrEmpty())
			{
				GenerateGenericSpec = attribute4;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "gender")
					{
						Gender.LoadGenderNode(Reader, mod);
					}
					else if (Reader.Name == "removegender")
					{
						string attribute5 = Reader.GetAttribute("Name");
						if (attribute5.IsNullOrEmpty())
						{
							throw new Exception("removegender tag had no Name attribute");
						}
						if (Gender.Genders.ContainsKey(attribute5))
						{
							Gender.Genders.Remove(attribute5);
						}
					}
					else if (Reader.Name == "genderPronounGeneration")
					{
						Gender.LoadGenderPronounGenerationNode(Reader, mod, Reader.Name);
					}
				}
			}
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x001E1E80 File Offset: 0x001E0080
		public static void LoadGenderNode(XmlTextReader Reader, bool mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception("gender tag had no Name attribute");
			}
			Gender gender;
			if (!Gender.Genders.TryGetValue(attribute, out gender))
			{
				gender = new Gender(attribute, true, false, false, false, null, null, null, null, null, null, null, null, null, null, null, false, false);
				Gender.Genders.Add(attribute, gender);
			}
			string text = Reader.GetAttribute("Generic");
			if (!text.IsNullOrEmpty())
			{
				gender.Generic = Convert.ToBoolean(text);
			}
			text = Reader.GetAttribute("Plural");
			if (!text.IsNullOrEmpty())
			{
				gender.Plural = Convert.ToBoolean(text);
			}
			text = Reader.GetAttribute("PseudoPlural");
			if (!text.IsNullOrEmpty())
			{
				gender.PseudoPlural = Convert.ToBoolean(text);
			}
			text = Reader.GetAttribute("Subjective");
			if (!text.IsNullOrEmpty())
			{
				gender.Subjective = text;
			}
			text = Reader.GetAttribute("Objective");
			if (text != null)
			{
				gender.Objective = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("PossessiveAdjective");
			if (text != null)
			{
				gender.PossessiveAdjective = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("SubstantivePossessive");
			if (text != null)
			{
				gender.SubstantivePossessive = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("Reflexive");
			if (text != null)
			{
				gender.Reflexive = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("PersonTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.PersonTerm = text;
			}
			text = Reader.GetAttribute("ImmaturePersonTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.ImmaturePersonTerm = text;
			}
			text = Reader.GetAttribute("FormalAddressTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.FormalAddressTerm = text;
			}
			text = Reader.GetAttribute("OffspringTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.OffspringTerm = text;
			}
			text = Reader.GetAttribute("SiblingTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.SiblingTerm = text;
			}
			text = Reader.GetAttribute("ParentTerm");
			if (!text.IsNullOrEmpty())
			{
				gender.ParentTerm = text;
			}
			text = Reader.GetAttribute("UseBareIndicative");
			if (!text.IsNullOrEmpty())
			{
				gender.UseBareIndicative = Convert.ToBoolean(text);
			}
			text = Reader.GetAttribute("DoNotReplicateAsPronounSet");
			if (!text.IsNullOrEmpty())
			{
				gender.DoNotReplicateAsPronounSet = Convert.ToBoolean(text);
			}
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x001E20D4 File Offset: 0x001E02D4
		private static int TryInt(string Spec, string What)
		{
			try
			{
				return Convert.ToInt32(Spec);
			}
			catch
			{
				Debug.LogError("Error in " + What + ": " + Spec);
			}
			return 0;
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x001E2118 File Offset: 0x001E0318
		private static void LoadTextAndWeightNode(XmlTextReader Reader, bool Mod, Dictionary<string, int> store)
		{
			string attribute = Reader.GetAttribute("Text");
			if (attribute == null)
			{
				throw new Exception(Reader.Name + " tag had no Text attribute");
			}
			string attribute2 = Reader.GetAttribute("Weight");
			if (attribute2 == null)
			{
				throw new Exception(Reader.Name + " tag had no Weight attribute");
			}
			int num = Gender.TryInt(attribute2, "Weight attribute");
			if (store.ContainsKey(attribute))
			{
				if (!Mod)
				{
					Debug.LogError("duplicate " + Reader.Name + " entry on " + attribute);
				}
				store[attribute] = num;
				return;
			}
			store.Add(attribute, num);
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x001E21B4 File Offset: 0x001E03B4
		private static void LoadTextNode(XmlTextReader Reader, bool Mod, List<string> store)
		{
			string attribute = Reader.GetAttribute("Text");
			if (attribute == null)
			{
				throw new Exception(Reader.Name + " tag had no Text attribute");
			}
			if (store.Contains(attribute))
			{
				if (!Mod)
				{
					Debug.LogError("duplicate " + Reader.Name + " entry on " + attribute);
					return;
				}
			}
			else
			{
				store.Add(attribute);
			}
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x001E2218 File Offset: 0x001E0418
		private static void LoadRemoveTextNode(XmlTextReader Reader, bool Mod, List<string> store)
		{
			string attribute = Reader.GetAttribute("Text");
			if (attribute == null)
			{
				throw new Exception(Reader.Name + " tag had no Text attribute");
			}
			if (store.Contains(attribute))
			{
				store.Remove(attribute);
			}
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x001E225C File Offset: 0x001E045C
		private static void LoadSearchReplaceNode(XmlTextReader Reader, bool Mod, Dictionary<string, string> store)
		{
			string attribute = Reader.GetAttribute("Search");
			if (attribute == null)
			{
				throw new Exception(Reader.Name + " tag had no Search attribute");
			}
			string attribute2 = Reader.GetAttribute("Replace");
			if (attribute2 == null)
			{
				throw new Exception(Reader.Name + " tag had no Replace attribute");
			}
			if (store.ContainsKey(attribute))
			{
				if (!Mod)
				{
					Debug.LogError("duplicate " + Reader.Name + " entry on " + attribute);
				}
				store[attribute] = attribute2;
				return;
			}
			store.Add(attribute, attribute2);
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x001E22EC File Offset: 0x001E04EC
		private static void LoadRemoveSearchReplaceNode(XmlTextReader Reader, bool Mod, Dictionary<string, string> store)
		{
			string attribute = Reader.GetAttribute("Search");
			if (attribute == null)
			{
				throw new Exception(Reader.Name + " tag had no Search attribute");
			}
			string attribute2 = Reader.GetAttribute("Replace");
			if (attribute2 == null)
			{
				throw new Exception(Reader.Name + " tag had no Replace attribute");
			}
			if (store.ContainsKey(attribute) && store[attribute] == attribute2)
			{
				store.Remove(attribute);
			}
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x001E2364 File Offset: 0x001E0564
		public static void LoadGenderPronounGenerationNode(XmlTextReader Reader, bool Mod, string MainTag)
		{
			string attribute = Reader.GetAttribute("AltVowelChance");
			if (!attribute.IsNullOrEmpty())
			{
				Gender.PronounAltVowelChance = Convert.ToInt32(attribute);
			}
			string attribute2 = Reader.GetAttribute("StemVowelChance");
			if (!attribute2.IsNullOrEmpty())
			{
				Gender.PronounStemVowelChance = Convert.ToInt32(attribute2);
			}
			while (Reader.Read())
			{
				if (Reader.Name == "stem")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounStems);
				}
				else if (Reader.Name == "vowel")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounVowels);
				}
				else if (Reader.Name == "prefix")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounPrefixes);
				}
				else if (Reader.Name == "subjectivesuffix")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounSubjectiveSuffixes);
				}
				else if (Reader.Name == "objectivesuffix")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounObjectiveSuffixes);
				}
				else if (Reader.Name == "possessivesuffix")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounPossessiveSuffixes);
				}
				else if (Reader.Name == "possessivesuffix")
				{
					Gender.LoadTextAndWeightNode(Reader, Mod, Gender.PronounPossessiveSuffixes);
				}
				else if (Reader.Name == "cullstring")
				{
					Gender.LoadTextNode(Reader, Mod, Gender.PronounCullStrings);
				}
				else if (Reader.Name == "removecullstring")
				{
					Gender.LoadRemoveTextNode(Reader, Mod, Gender.PronounCullStrings);
				}
				else if (Reader.Name == "cullpattern")
				{
					Gender.LoadTextNode(Reader, Mod, Gender.PronounCullPatterns);
				}
				else if (Reader.Name == "removecullpattern")
				{
					Gender.LoadRemoveTextNode(Reader, Mod, Gender.PronounCullPatterns);
				}
				else if (Reader.Name == "mapstring")
				{
					Gender.LoadSearchReplaceNode(Reader, Mod, Gender.PronounMapStrings);
				}
				else if (Reader.Name == "removemapstring")
				{
					Gender.LoadRemoveSearchReplaceNode(Reader, Mod, Gender.PronounMapStrings);
				}
				else if (Reader.Name == "mappattern")
				{
					Gender.LoadSearchReplaceNode(Reader, Mod, Gender.PronounMapPatterns);
				}
				else if (Reader.Name == "removemappattern")
				{
					Gender.LoadRemoveSearchReplaceNode(Reader, Mod, Gender.PronounMapPatterns);
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == MainTag)
				{
					break;
				}
			}
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x001E25D8 File Offset: 0x001E07D8
		public static List<Gender> Find(Predicate<Gender> Filter)
		{
			int num = 0;
			if (Gender.Genders != null)
			{
				foreach (string text in Gender.Genders.Keys)
				{
					if (Filter(Gender.Genders[text]))
					{
						num++;
					}
				}
			}
			List<Gender> list = new List<Gender>(num);
			if (num > 0)
			{
				foreach (string text2 in Gender.Genders.Keys)
				{
					Gender gender = Gender.Genders[text2];
					if (Filter(gender))
					{
						list.Add(gender);
					}
				}
			}
			return list;
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x001E26B4 File Offset: 0x001E08B4
		public static List<Gender> GetAll()
		{
			if (Gender.All == null)
			{
				Gender.All = Gender.Find((Gender G) => true);
			}
			return Gender.All;
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001E26EB File Offset: 0x001E08EB
		public static Gender GetAny()
		{
			return Gender.GetAll().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x001E26F8 File Offset: 0x001E08F8
		public static string GetAnyName()
		{
			return Gender.GetAny().Name;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001E2704 File Offset: 0x001E0904
		public static List<Gender> GetAllGeneric()
		{
			if (Gender.Generics == null)
			{
				Gender.Generics = Gender.Find((Gender G) => G.Generic);
			}
			return Gender.Generics;
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x001E273B File Offset: 0x001E093B
		public static Gender GetAnyGeneric()
		{
			return Gender.GetAllGeneric().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001E2748 File Offset: 0x001E0948
		public static string GetAnyGenericName()
		{
			return Gender.GetAnyGeneric().Name;
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x001E2754 File Offset: 0x001E0954
		public static List<Gender> GetAllPlural()
		{
			if (Gender.Plurals == null)
			{
				Gender.Plurals = Gender.Find((Gender G) => G.Plural);
			}
			return Gender.Plurals;
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x001E278B File Offset: 0x001E098B
		public static Gender GetAnyPlural()
		{
			return Gender.GetAllPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x001E2798 File Offset: 0x001E0998
		public static string GetAnyPluralName()
		{
			return Gender.GetAnyPlural().Name;
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x001E27A4 File Offset: 0x001E09A4
		public static List<Gender> GetAllSingular()
		{
			if (Gender.Singulars == null)
			{
				Gender.Singulars = Gender.Find((Gender G) => !G.Plural);
			}
			return Gender.Singulars;
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x001E27DB File Offset: 0x001E09DB
		public static Gender GetAnySingular()
		{
			return Gender.GetAllSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x001E27E8 File Offset: 0x001E09E8
		public static string GetAnySingularName()
		{
			return Gender.GetAnySingular().Name;
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x001E27F4 File Offset: 0x001E09F4
		public static List<Gender> GetAllGenericSingular()
		{
			if (Gender.GenericSingulars == null)
			{
				Gender.GenericSingulars = Gender.Find((Gender G) => G.Generic && !G.Plural);
			}
			return Gender.GenericSingulars;
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x001E282B File Offset: 0x001E0A2B
		public static Gender GetAnyGenericSingular()
		{
			return Gender.GetAllGenericSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x001E2838 File Offset: 0x001E0A38
		public static string GetAnyGenericSingularName()
		{
			return Gender.GetAnyGenericSingular().Name;
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x001E2844 File Offset: 0x001E0A44
		public static List<Gender> GetAllGenericPlural()
		{
			if (Gender.GenericPlurals == null)
			{
				Gender.GenericPlurals = Gender.Find((Gender G) => G.Generic && G.Plural);
			}
			return Gender.GenericPlurals;
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x001E287B File Offset: 0x001E0A7B
		public static Gender GetAnyGenericPlural()
		{
			return Gender.GetAllGenericPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x001E2888 File Offset: 0x001E0A88
		public static string GetAnyGenericPluralName()
		{
			return Gender.GetAnyGenericPlural().Name;
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x001E2894 File Offset: 0x001E0A94
		public static List<Gender> GetAllPersonal()
		{
			if (Gender.Personals == null)
			{
				Gender.Personals = Gender.Find((Gender G) => !G.UseBareIndicative);
			}
			return Gender.Personals;
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x001E28CB File Offset: 0x001E0ACB
		public static Gender GetAnyPersonal()
		{
			return Gender.GetAllPersonal().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x001E28D8 File Offset: 0x001E0AD8
		public static string GetAnyPersonalName()
		{
			return Gender.GetAnyPersonal().Name;
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x001E28E4 File Offset: 0x001E0AE4
		public static List<Gender> GetAllPersonalPlural()
		{
			if (Gender.PersonalPlurals == null)
			{
				Gender.PersonalPlurals = Gender.Find((Gender G) => !G.UseBareIndicative && G.Plural);
			}
			return Gender.PersonalPlurals;
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x001E291B File Offset: 0x001E0B1B
		public static Gender GetAnyPersonalPlural()
		{
			return Gender.GetAllPersonalPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x001E2928 File Offset: 0x001E0B28
		public static string GetAnyPersonalPluralName()
		{
			return Gender.GetAnyPersonalPlural().Name;
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x001E2934 File Offset: 0x001E0B34
		public static List<Gender> GetAllPersonalSingular()
		{
			if (Gender.PersonalSingulars == null)
			{
				Gender.PersonalSingulars = Gender.Find((Gender G) => !G.UseBareIndicative && !G.Plural);
			}
			return Gender.PersonalSingulars;
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x001E296B File Offset: 0x001E0B6B
		public static Gender GetAnyPersonalSingular()
		{
			return Gender.GetAllPersonalSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x001E2978 File Offset: 0x001E0B78
		public static string GetAnyPersonalSingularName()
		{
			return Gender.GetAnyPersonalSingular().Name;
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x001E2984 File Offset: 0x001E0B84
		public static List<Gender> GetAllGenericPersonal()
		{
			if (Gender.GenericPersonals == null)
			{
				Gender.GenericPersonals = Gender.Find((Gender G) => G.Generic && !G.UseBareIndicative);
			}
			return Gender.GenericPersonals;
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x001E29BB File Offset: 0x001E0BBB
		public static Gender GetAnyGenericPersonal()
		{
			return Gender.GetAllGenericPersonal().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x001E29C8 File Offset: 0x001E0BC8
		public static string GetAnyGenericPersonalName()
		{
			return Gender.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x001E29D4 File Offset: 0x001E0BD4
		public static List<Gender> GetAllGenericPersonalPlural()
		{
			if (Gender.GenericPersonalPlurals == null)
			{
				Gender.GenericPersonalPlurals = Gender.Find((Gender G) => G.Generic && !G.UseBareIndicative && G.Plural);
			}
			return Gender.GenericPersonalPlurals;
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x001E2A0B File Offset: 0x001E0C0B
		public static Gender GetAnyGenericPersonalPlural()
		{
			return Gender.GetAllGenericPersonalPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x001E2A18 File Offset: 0x001E0C18
		public static string GetAnyGenericPersonalPluralName()
		{
			return Gender.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001E2A24 File Offset: 0x001E0C24
		public static List<Gender> GetAllGenericPersonalSingular()
		{
			if (Gender.GenericPersonalSingulars == null)
			{
				Gender.GenericPersonalSingulars = Gender.Find((Gender G) => G.Generic && !G.UseBareIndicative && !G.Plural);
			}
			return Gender.GenericPersonalSingulars;
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x001E2A5B File Offset: 0x001E0C5B
		public static Gender GetAnyGenericPersonalSingular()
		{
			return Gender.GetAllGenericPersonalSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x001E2A68 File Offset: 0x001E0C68
		public static string GetAnyGenericPersonalSingularName()
		{
			return Gender.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x001E2A74 File Offset: 0x001E0C74
		public static List<Gender> GetAllNonpersonal()
		{
			if (Gender.Nonpersonals == null)
			{
				Gender.Nonpersonals = Gender.Find((Gender G) => G.UseBareIndicative);
			}
			return Gender.Nonpersonals;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x001E2AAB File Offset: 0x001E0CAB
		public static Gender GetAnyNonpersonal()
		{
			return Gender.GetAllNonpersonal().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x001E2AB8 File Offset: 0x001E0CB8
		public static string GetAnyNonpersonalName()
		{
			return Gender.GetAnyNonpersonal().Name;
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x001E2AC4 File Offset: 0x001E0CC4
		public static List<Gender> GetAllNonpersonalPlural()
		{
			if (Gender.NonpersonalPlurals == null)
			{
				Gender.NonpersonalPlurals = Gender.Find((Gender G) => G.UseBareIndicative && G.Plural);
			}
			return Gender.NonpersonalPlurals;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x001E2AFB File Offset: 0x001E0CFB
		public static Gender GetAnyNonpersonalPlural()
		{
			return Gender.GetAllNonpersonalPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x001E2B08 File Offset: 0x001E0D08
		public static string GetAnyNonpersonalPluralName()
		{
			return Gender.GetAnyNonpersonalPlural().Name;
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x001E2B14 File Offset: 0x001E0D14
		public static List<Gender> GetAllNonpersonalSingular()
		{
			if (Gender.NonpersonalSingulars == null)
			{
				Gender.NonpersonalSingulars = Gender.Find((Gender G) => G.UseBareIndicative && !G.Plural);
			}
			return Gender.NonpersonalSingulars;
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x001E2B4B File Offset: 0x001E0D4B
		public static Gender GetAnyNonpersonalSingular()
		{
			return Gender.GetAllNonpersonalSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x001E2B58 File Offset: 0x001E0D58
		public static string GetAnyNonpersonalSingularName()
		{
			return Gender.GetAnyNonpersonalSingular().Name;
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x001E2B64 File Offset: 0x001E0D64
		public static List<Gender> GetAllGenericNonpersonal()
		{
			if (Gender.GenericNonpersonals == null)
			{
				Gender.GenericNonpersonals = Gender.Find((Gender G) => G.Generic && G.UseBareIndicative);
			}
			return Gender.GenericNonpersonals;
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x001E2B9B File Offset: 0x001E0D9B
		public static Gender GetAnyGenericNonpersonal()
		{
			return Gender.GetAllGenericNonpersonal().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x001E2BA8 File Offset: 0x001E0DA8
		public static string GetAnyGenericNonpersonalName()
		{
			return Gender.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x001E2BB4 File Offset: 0x001E0DB4
		public static List<Gender> GetAllGenericNonpersonalPlural()
		{
			if (Gender.GenericNonpersonalPlurals == null)
			{
				Gender.GenericNonpersonalPlurals = Gender.Find((Gender G) => G.Generic && G.UseBareIndicative && G.Plural);
			}
			return Gender.GenericNonpersonalPlurals;
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x001E2BEB File Offset: 0x001E0DEB
		public static Gender GetAnyGenericNonpersonalPlural()
		{
			return Gender.GetAllGenericNonpersonalPlural().GetRandomElement<Gender>(null);
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x001E2BF8 File Offset: 0x001E0DF8
		public static string GetAnyGenericNonpersonalPluralName()
		{
			return Gender.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x001E2C04 File Offset: 0x001E0E04
		public static List<Gender> GetAllGenericNonpersonalSingular()
		{
			if (Gender.GenericNonpersonalSingulars == null)
			{
				Gender.GenericNonpersonalSingulars = Gender.Find((Gender G) => G.Generic && G.UseBareIndicative && !G.Plural);
			}
			return Gender.GenericNonpersonalSingulars;
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001E2C3B File Offset: 0x001E0E3B
		public static Gender GetAnyGenericNonpersonalSingular()
		{
			return Gender.GetAllGenericNonpersonalSingular().GetRandomElement<Gender>(null);
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x001E2C48 File Offset: 0x001E0E48
		public static string GetAnyGenericNonpersonalSingularName()
		{
			return Gender.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001E2C54 File Offset: 0x001E0E54
		public static string CheckSpecial(string Name)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Name);
			if (num <= 1372385274U)
			{
				if (num <= 382704287U)
				{
					if (num <= 116991181U)
					{
						if (num != 39591351U)
						{
							if (num == 116991181U)
							{
								if (Name == "genericnonpersonal")
								{
									return Gender.GetAnyGenericNonpersonalName();
								}
							}
						}
						else if (Name == "genericnonpersonalplural")
						{
							return Gender.GetAnyGenericNonpersonalPluralName();
						}
					}
					else if (num != 130654714U)
					{
						if (num != 205118861U)
						{
							if (num == 382704287U)
							{
								if (Name == "genericsingular")
								{
									return Gender.GetAnyGenericSingularName();
								}
							}
						}
						else if (Name == "personal")
						{
							return Gender.GetAnyPersonalName();
						}
					}
					else if (Name == "genericpersonal")
					{
						return Gender.GetAnyGenericPersonalName();
					}
				}
				else if (num <= 872738708U)
				{
					if (num != 631220345U)
					{
						if (num != 740945997U)
						{
							if (num == 872738708U)
							{
								if (Name == "genericpersonalplural")
								{
									return Gender.GetAnyGenericPersonalPluralName();
								}
							}
						}
						else if (Name == "any")
						{
							return Gender.GetAnyName();
						}
					}
					else if (Name == "nonpersonalsingular")
					{
						return Gender.GetAnyNonpersonalSingularName();
					}
				}
				else if (num != 1038892768U)
				{
					if (num != 1199426964U)
					{
						if (num == 1372385274U)
						{
							if (Name == "generic")
							{
								return Gender.GetAnyGenericName();
							}
						}
					}
					else if (Name == "genericplural")
					{
						return Gender.GetAnyGenericPluralName();
					}
				}
				else if (Name == "generatemaybeplural")
				{
					return Gender.Generate(false, 10, 0, null, null, null, null, null, null).Register().Name;
				}
			}
			else if (num <= 3106482513U)
			{
				if (num <= 2057849500U)
				{
					if (num != 1926810242U)
					{
						if (num == 2057849500U)
						{
							if (Name == "nonpersonal")
							{
								return Gender.GetAnyNonpersonalName();
							}
						}
					}
					else if (Name == "nonpersonalplural")
					{
						return Gender.GetAnyNonpersonalPluralName();
					}
				}
				else if (num != 2155262776U)
				{
					if (num != 2196100728U)
					{
						if (num == 3106482513U)
						{
							if (Name == "generatemaybenonpersonal")
							{
								return Gender.Generate(false, 0, 10, null, null, null, null, null, null).Register().Name;
							}
						}
					}
					else if (Name == "personalsingular")
					{
						return Gender.GetAnyPersonalSingularName();
					}
				}
				else if (Name == "anysingular")
				{
					return Gender.GetAnySingularName();
				}
			}
			else if (num <= 3353631607U)
			{
				if (num != 3270848199U)
				{
					if (num != 3295710367U)
					{
						if (num == 3353631607U)
						{
							if (Name == "personalplural")
							{
								return Gender.GetAnyPersonalPluralName();
							}
						}
					}
					else if (Name == "genericpersonalsingular")
					{
						return Gender.GetAnyGenericPersonalSingularName();
					}
				}
				else if (Name == "generatemaybepluralmaybenonpersonal")
				{
					return Gender.Generate(false, 10, 10, null, null, null, null, null, null).Register().Name;
				}
			}
			else if (num != 3447982738U)
			{
				if (num != 3567947063U)
				{
					if (num == 4264032184U)
					{
						if (Name == "genericnonpersonalsingular")
						{
							return Gender.GetAnyGenericNonpersonalSingularName();
						}
					}
				}
				else if (Name == "anyplural")
				{
					return Gender.GetAnyPluralName();
				}
			}
			else if (Name == "generate")
			{
				return Gender.Generate(false, 0, 0, null, null, null, null, null, null).Register().Name;
			}
			return Name;
		}

		// Token: 0x04002E1D RID: 11805
		public static bool EnableSelection;

		// Token: 0x04002E1E RID: 11806
		public static bool EnableGeneration;

		// Token: 0x04002E1F RID: 11807
		public static bool EnableDisplay;

		// Token: 0x04002E20 RID: 11808
		public static Dictionary<string, Gender> Genders;

		// Token: 0x04002E21 RID: 11809
		[NonSerialized]
		public static int PronounAltVowelChance;

		// Token: 0x04002E22 RID: 11810
		[NonSerialized]
		public static int PronounStemVowelChance;

		// Token: 0x04002E23 RID: 11811
		[NonSerialized]
		public static Dictionary<string, int> PronounStems;

		// Token: 0x04002E24 RID: 11812
		[NonSerialized]
		public static Dictionary<string, int> PronounVowels;

		// Token: 0x04002E25 RID: 11813
		[NonSerialized]
		public static Dictionary<string, int> PronounPrefixes;

		// Token: 0x04002E26 RID: 11814
		[NonSerialized]
		public static Dictionary<string, int> PronounSubjectiveSuffixes;

		// Token: 0x04002E27 RID: 11815
		[NonSerialized]
		public static Dictionary<string, int> PronounObjectiveSuffixes;

		// Token: 0x04002E28 RID: 11816
		[NonSerialized]
		public static Dictionary<string, int> PronounPossessiveSuffixes;

		// Token: 0x04002E29 RID: 11817
		[NonSerialized]
		public static List<string> PronounCullStrings;

		// Token: 0x04002E2A RID: 11818
		[NonSerialized]
		public static List<string> PronounCullPatterns;

		// Token: 0x04002E2B RID: 11819
		[NonSerialized]
		public static Dictionary<string, string> PronounMapStrings;

		// Token: 0x04002E2C RID: 11820
		[NonSerialized]
		public static Dictionary<string, string> PronounMapPatterns;

		// Token: 0x04002E2D RID: 11821
		[NonSerialized]
		public static List<string> NamePrefixes;

		// Token: 0x04002E2E RID: 11822
		[NonSerialized]
		public static List<string> NameInfixes;

		// Token: 0x04002E2F RID: 11823
		[NonSerialized]
		public static List<string> NameSuffixes;

		// Token: 0x04002E30 RID: 11824
		[NonSerialized]
		public static List<Gender> All;

		// Token: 0x04002E31 RID: 11825
		[NonSerialized]
		public static List<Gender> GenericPlurals;

		// Token: 0x04002E32 RID: 11826
		[NonSerialized]
		public static List<Gender> Generics;

		// Token: 0x04002E33 RID: 11827
		[NonSerialized]
		public static List<Gender> GenericSingulars;

		// Token: 0x04002E34 RID: 11828
		[NonSerialized]
		public static List<Gender> Plurals;

		// Token: 0x04002E35 RID: 11829
		[NonSerialized]
		public static List<Gender> Singulars;

		// Token: 0x04002E36 RID: 11830
		[NonSerialized]
		public static List<Gender> GenericPersonalPlurals;

		// Token: 0x04002E37 RID: 11831
		[NonSerialized]
		public static List<Gender> GenericPersonals;

		// Token: 0x04002E38 RID: 11832
		[NonSerialized]
		public static List<Gender> GenericPersonalSingulars;

		// Token: 0x04002E39 RID: 11833
		[NonSerialized]
		public static List<Gender> Personals;

		// Token: 0x04002E3A RID: 11834
		[NonSerialized]
		public static List<Gender> PersonalPlurals;

		// Token: 0x04002E3B RID: 11835
		[NonSerialized]
		public static List<Gender> PersonalSingulars;

		// Token: 0x04002E3C RID: 11836
		[NonSerialized]
		public static List<Gender> GenericNonpersonalPlurals;

		// Token: 0x04002E3D RID: 11837
		[NonSerialized]
		public static List<Gender> GenericNonpersonals;

		// Token: 0x04002E3E RID: 11838
		[NonSerialized]
		public static List<Gender> GenericNonpersonalSingulars;

		// Token: 0x04002E3F RID: 11839
		[NonSerialized]
		public static List<Gender> Nonpersonals;

		// Token: 0x04002E40 RID: 11840
		[NonSerialized]
		public static List<Gender> NonpersonalPlurals;

		// Token: 0x04002E41 RID: 11841
		[NonSerialized]
		public static List<Gender> NonpersonalSingulars;

		// Token: 0x04002E42 RID: 11842
		[NonSerialized]
		public static Gender DefaultNeuter;

		// Token: 0x04002E43 RID: 11843
		[NonSerialized]
		public static Gender DefaultNonspecific;

		// Token: 0x04002E44 RID: 11844
		[NonSerialized]
		public static Gender DefaultPlural;

		// Token: 0x04002E45 RID: 11845
		[NonSerialized]
		public static Gender DefaultCollective;

		// Token: 0x04002E46 RID: 11846
		public bool DoNotReplicateAsPronounSet;

		// Token: 0x04002E47 RID: 11847
		public string _Name = string.Empty;

		// Token: 0x04002E48 RID: 11848
		[NonSerialized]
		public string _CapitalizedName;
	}
}
