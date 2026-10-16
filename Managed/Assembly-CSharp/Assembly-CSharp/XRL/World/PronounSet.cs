using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ConsoleLib.Console;
using XRL.Names;
using XRL.Rules;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000954 RID: 2388
	[HasModSensitiveStaticCache]
	public class PronounSet : BasePronounProvider
	{
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06006284 RID: 25220 RVA: 0x001E305D File Offset: 0x001E125D
		public override string Name
		{
			get
			{
				if (this._Name == null)
				{
					this._Name = this.CalculateName();
				}
				return this._Name;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06006285 RID: 25221 RVA: 0x001E3079 File Offset: 0x001E1279
		public override string CapitalizedName
		{
			get
			{
				if (this._CapitalizedName == null)
				{
					this._CapitalizedName = ColorUtility.CapitalizeExceptFormatting(this.Name);
				}
				return this._CapitalizedName;
			}
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x001E309C File Offset: 0x001E129C
		public PronounSet(bool _Generic = false, bool _Generated = false, bool _Plural = false, bool _PseudoPlural = false, string _Subjective = null, string _Objective = null, string _PossessiveAdjective = null, string _SubstantivePossessive = null, string _Reflexive = null, string _PersonTerm = null, string _ImmaturePersonTerm = null, string _FormalAddressTerm = null, string _OffspringTerm = null, string _SiblingTerm = null, string _ParentTerm = null, bool _UseBareIndicative = false, bool _FromGender = false)
			: base(_Generic, _Generated, _Plural, _PseudoPlural, _Subjective, _Objective, _PossessiveAdjective, _SubstantivePossessive, _Reflexive, _PersonTerm, _ImmaturePersonTerm, _FormalAddressTerm, _OffspringTerm, _SiblingTerm, _ParentTerm, _UseBareIndicative)
		{
			this.FromGender = _FromGender;
		}

		/// <summary>Copy from <paramref name="Original" /></summary>
		// Token: 0x06006287 RID: 25223 RVA: 0x001E30D4 File Offset: 0x001E12D4
		public PronounSet(BasePronounProvider Original)
			: base(Original)
		{
		}

		/// <summary>Empty constructor for serialization purposes</summary>
		// Token: 0x06006288 RID: 25224 RVA: 0x001E30E0 File Offset: 0x001E12E0
		public PronounSet()
			: base(false, false, false, false, null, null, null, null, null, null, null, null, null, null, null, false)
		{
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x001E3104 File Offset: 0x001E1304
		public override void CopyFrom(BasePronounProvider Original)
		{
			if (Original is Gender)
			{
				this.FromGender = true;
			}
			PronounSet pronounSet = Original as PronounSet;
			if (pronounSet != null)
			{
				this.FromGender = pronounSet.FromGender;
			}
			base.CopyFrom(Original);
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x001E313D File Offset: 0x001E133D
		public override BasePronounProvider Clone()
		{
			return new PronounSet(this);
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x001E3148 File Offset: 0x001E1348
		public static void SaveAll(SerializationWriter Writer)
		{
			Writer.Write(PronounSet.PronounSets.Count);
			foreach (string text in PronounSet.PronounSets.Keys)
			{
				PronounSet.PronounSets[text].Save(Writer);
			}
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001E31BC File Offset: 0x001E13BC
		public static void LoadAll(SerializationReader Reader)
		{
			if (PronounSet.PronounSets == null)
			{
				PronounSet.PronounSets = new Dictionary<string, PronounSet>(64);
			}
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				PronounSet pronounSet = PronounSet.Load(Reader);
				PronounSet.PronounSets[pronounSet.Name] = pronounSet;
			}
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x001E3207 File Offset: 0x001E1407
		public override void Save(SerializationWriter Writer)
		{
			base.Save(Writer);
			Writer.Write(this.FromGender);
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x001E321C File Offset: 0x001E141C
		public static PronounSet Load(SerializationReader Reader)
		{
			PronounSet pronounSet = new PronounSet();
			BasePronounProvider.Load(Reader, pronounSet);
			pronounSet.FromGender = Reader.ReadBoolean();
			return pronounSet;
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x001E3244 File Offset: 0x001E1444
		private static void SetDefaults(Dictionary<string, PronounSet> PronounSets)
		{
			if (PronounSets.TryGetValue("player", out PronounSet.DefaultPlayer))
			{
				PronounSets.Remove("player");
			}
			else
			{
				MetricsManager.LogError("Default pronoun set 'player' not found.");
			}
			if (PronounSets.TryGetValue("plural/player", out PronounSet.DefaultPlayerPlural))
			{
				PronounSets.Remove("plural/player");
				return;
			}
			MetricsManager.LogError("Default pronoun set 'plural/player' not found.");
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x001E32A4 File Offset: 0x001E14A4
		public override async Task<bool> CustomizeAsync()
		{
			return await base.Customize("pronoun set");
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x001E32E8 File Offset: 0x001E14E8
		protected override async Task<bool> CustomizeProcess(string What)
		{
			this.FromGender = false;
			return await base.CustomizeProcess(What);
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x001E3333 File Offset: 0x001E1533
		protected override void ConfigurationUpdated(bool Categorizing = false)
		{
			this._Name = null;
			if (Categorizing)
			{
				this.FlushRelevantCaches();
			}
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x001E3348 File Offset: 0x001E1548
		public StringBuilder GetSummary(StringBuilder sb)
		{
			return sb.Append("Short Name: ").Append(this.GetShortName()).Append('\n')
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
				.Append("From Gender: ")
				.Append(this.FromGender ? "Yes" : "No")
				.Append('\n');
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x001E35DA File Offset: 0x001E17DA
		public string GetSummary()
		{
			return this.GetSummary(Event.NewStringBuilder(null)).ToString();
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x001E35F0 File Offset: 0x001E17F0
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

		// Token: 0x06006296 RID: 25238 RVA: 0x001E376E File Offset: 0x001E196E
		public string GetBasicSummary()
		{
			return this.GetBasicSummary(Event.NewStringBuilder(null)).ToString();
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x001E3784 File Offset: 0x001E1984
		public string CalculateName()
		{
			StringBuilder stringBuilder = new StringBuilder(48);
			if (base.Plural)
			{
				stringBuilder.Append("plural/");
			}
			if (base.PseudoPlural)
			{
				stringBuilder.Append("pseudo-plural/");
			}
			if (base.UseBareIndicative)
			{
				stringBuilder.Append("nonperson/");
			}
			stringBuilder.Append(base.Subjective).Append('/').Append(base.Objective)
				.Append('/')
				.Append(base.PossessiveAdjective)
				.Append('/')
				.Append(base.SubstantivePossessive)
				.Append('/')
				.Append(base.Reflexive)
				.Append('/')
				.Append(base.PersonTerm)
				.Append('/')
				.Append(base.ImmaturePersonTerm)
				.Append('/')
				.Append(base.FormalAddressTerm)
				.Append('/')
				.Append(base.OffspringTerm)
				.Append('/')
				.Append(base.SiblingTerm)
				.Append('/')
				.Append(base.ParentTerm);
			return stringBuilder.ToString();
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x001E389C File Offset: 0x001E1A9C
		public string GetShortName()
		{
			PronounSet.SB.Clear();
			if (base.Plural)
			{
				PronounSet.SB.Append("plural/");
			}
			if (base.UseBareIndicative)
			{
				PronounSet.SB.Append("nonperson/");
			}
			PronounSet.SB.Append(base.Subjective).Append('/').Append(base.Objective);
			if (base.PossessiveAdjective != base.Objective)
			{
				PronounSet.SB.Append('/').Append(base.PossessiveAdjective);
			}
			if (base.SubstantivePossessive != this.ExpectedSubstantivePossessive())
			{
				PronounSet.SB.Append('/').Append(base.SubstantivePossessive);
			}
			if (base.Reflexive != this.ExpectedReflexive())
			{
				PronounSet.SB.Append('/').Append(base.Reflexive);
			}
			return PronounSet.SB.ToString();
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x001E3994 File Offset: 0x001E1B94
		public static PronounSet ConstructFromName(string Name)
		{
			List<string> list = new List<string>(Name.Split('/', StringSplitOptions.None));
			PronounSet pronounSet = new PronounSet();
			if (list[0] == "plural")
			{
				pronounSet.Plural = true;
				list.RemoveAt(0);
			}
			if (list[0] == "pseudo-plural")
			{
				pronounSet.PseudoPlural = true;
				list.RemoveAt(0);
			}
			if (list[0] == "nonperson")
			{
				pronounSet.UseBareIndicative = true;
				list.RemoveAt(0);
			}
			if (list.Count == 11)
			{
				pronounSet.Subjective = list[0];
				pronounSet.Objective = list[1];
				pronounSet.PossessiveAdjective = list[2];
				pronounSet.SubstantivePossessive = list[3];
				pronounSet.Reflexive = list[4];
				pronounSet.PersonTerm = list[5];
				pronounSet.ImmaturePersonTerm = list[6];
				pronounSet.FormalAddressTerm = list[7];
				pronounSet.OffspringTerm = list[8];
				pronounSet.SiblingTerm = list[9];
				pronounSet.ParentTerm = list[10];
			}
			else
			{
				int num = 0;
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.Subjective = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.Objective = list[num++];
				if (list.Count <= num)
				{
					pronounSet.PossessiveAdjective = null;
					return pronounSet;
				}
				string text = (pronounSet.Plural ? "selves" : "self");
				if (list[num].EndsWith(text))
				{
					pronounSet.PossessiveAdjective = null;
					pronounSet.Reflexive = list[num++];
				}
				else
				{
					pronounSet.PossessiveAdjective = list[num++];
					if (list.Count <= num)
					{
						return pronounSet;
					}
					if (list[num].EndsWith(text))
					{
						pronounSet.Reflexive = list[num++];
					}
					else
					{
						pronounSet.SubstantivePossessive = list[num++];
						if (list.Count <= num)
						{
							return pronounSet;
						}
						pronounSet.Reflexive = list[num++];
					}
				}
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.PersonTerm = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.ImmaturePersonTerm = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.FormalAddressTerm = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.OffspringTerm = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.SiblingTerm = list[num++];
				if (list.Count <= num)
				{
					return pronounSet;
				}
				pronounSet.ParentTerm = list[num++];
			}
			return pronounSet;
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x001E3C5C File Offset: 0x001E1E5C
		public static void Clear()
		{
			PronounSet.PronounSets = null;
			PronounSet.All = null;
			PronounSet.GenericPlurals = null;
			PronounSet.Generics = null;
			PronounSet.GenericSingulars = null;
			PronounSet.Plurals = null;
			PronounSet.Singulars = null;
			PronounSet.GenericPersonalPlurals = null;
			PronounSet.GenericPersonals = null;
			PronounSet.GenericPersonalSingulars = null;
			PronounSet.Personals = null;
			PronounSet.PersonalPlurals = null;
			PronounSet.PersonalSingulars = null;
			PronounSet.GenericNonpersonalPlurals = null;
			PronounSet.GenericNonpersonals = null;
			PronounSet.GenericNonpersonalSingulars = null;
			PronounSet.Nonpersonals = null;
			PronounSet.NonpersonalPlurals = null;
			PronounSet.NonpersonalSingulars = null;
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x001E3CDB File Offset: 0x001E1EDB
		[ModSensitiveCacheInit]
		public static void Reinit()
		{
			PronounSet.Clear();
			Loading.LoadTask("Loading PronounSets.xml", new Action(PronounSet.Init), true);
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x001E3CFC File Offset: 0x001E1EFC
		public static void Init()
		{
			if (PronounSet.PronounSets == null)
			{
				PronounSet.PronounSets = new Dictionary<string, PronounSet>(64);
			}
			PronounSet.TempPronounSets = new Dictionary<string, PronounSet>(16);
			string GenerateGenericSpec = null;
			bool ReplicateGenders = false;
			Action<string, bool> action = delegate(string file, bool mod)
			{
				using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(file))
				{
					streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
					while (streamingAssetsXMLStream.Read())
					{
						if (streamingAssetsXMLStream.Name == "pronounsets")
						{
							string attribute = streamingAssetsXMLStream.GetAttribute("EnableSelection");
							if (!string.IsNullOrEmpty(attribute))
							{
								PronounSet.EnableSelection = attribute.EqualsNoCase("true");
							}
							string attribute2 = streamingAssetsXMLStream.GetAttribute("EnableGeneration");
							if (!string.IsNullOrEmpty(attribute2))
							{
								PronounSet.EnableGeneration = attribute2.EqualsNoCase("true");
							}
							string attribute3 = streamingAssetsXMLStream.GetAttribute("EnableConversationalExchange");
							if (!string.IsNullOrEmpty(attribute3))
							{
								PronounSet.EnableConversationalExchange = attribute3.EqualsNoCase("true");
							}
							string attribute4 = streamingAssetsXMLStream.GetAttribute("GenerateGeneric");
							if (!string.IsNullOrEmpty(attribute4))
							{
								GenerateGenericSpec = attribute4;
							}
							string attribute5 = streamingAssetsXMLStream.GetAttribute("ReplicateGenders");
							if (attribute5.EqualsNoCase("true"))
							{
								ReplicateGenders = true;
							}
							else if (attribute5.EqualsNoCase("false"))
							{
								ReplicateGenders = false;
							}
							PronounSet.LoadPronounSetsNode(streamingAssetsXMLStream, mod);
						}
						if (streamingAssetsXMLStream.NodeType == XmlNodeType.EndElement && streamingAssetsXMLStream.Name == "pronounsets")
						{
							break;
						}
					}
					streamingAssetsXMLStream.Close();
				}
			};
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("PronounSets"))
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
			if (ReplicateGenders)
			{
				foreach (Gender gender in Gender.GetAll())
				{
					if (!gender.DoNotReplicateAsPronounSet)
					{
						PronounSet pronounSet = new PronounSet(gender);
						if (!PronounSet.PronounSets.ContainsKey(pronounSet.Name))
						{
							PronounSet.PronounSets.Add(pronounSet.Name, pronounSet);
						}
					}
				}
			}
			PronounSet.SetDefaults(PronounSet.TempPronounSets);
			foreach (PronounSet pronounSet2 in PronounSet.TempPronounSets.Values)
			{
				if (!PronounSet.PronounSets.ContainsKey(pronounSet2.Name))
				{
					PronounSet.PronounSets.Add(pronounSet2.Name, pronounSet2);
				}
			}
			PronounSet.TempPronounSets = null;
			if (!string.IsNullOrEmpty(GenerateGenericSpec))
			{
				int num = Stat.Roll(GenerateGenericSpec, null);
				int num2 = 0;
				foreach (string text in PronounSet.PronounSets.Keys)
				{
					PronounSet pronounSet3 = PronounSet.PronounSets[text];
					if (pronounSet3.Generic && pronounSet3.Generated && !pronounSet3.FromGender)
					{
						num2++;
					}
				}
				for (int i = num2; i < num; i++)
				{
					PronounSet.Generate(true, 0, 0, null, null, null, null, null, null).Register();
				}
			}
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x001E3F68 File Offset: 0x001E2168
		public static void LoadPronounSetsNode(XmlTextReader Reader, bool Mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "pronounset")
				{
					PronounSet pronounSet = new PronounSet(true, false, false, false, null, null, null, null, null, null, null, null, null, null, null, false, false);
					string text = Reader.GetAttribute("Generic");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.Generic = text.EqualsNoCase("true");
					}
					text = Reader.GetAttribute("Plural");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.Plural = text.EqualsNoCase("true");
					}
					text = Reader.GetAttribute("PseudoPlural");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.PseudoPlural = text.EqualsNoCase("true");
					}
					text = Reader.GetAttribute("Subjective");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.Subjective = text;
					}
					text = Reader.GetAttribute("Objective");
					if (text != null)
					{
						pronounSet.Objective = ((text == "") ? null : text);
					}
					text = Reader.GetAttribute("PossessiveAdjective");
					if (text != null)
					{
						pronounSet.PossessiveAdjective = ((text == "") ? null : text);
					}
					text = Reader.GetAttribute("SubstantivePossessive");
					if (text != null)
					{
						pronounSet.SubstantivePossessive = ((text == "") ? null : text);
					}
					text = Reader.GetAttribute("Reflexive");
					if (text != null)
					{
						pronounSet.Reflexive = ((text == "") ? null : text);
					}
					text = Reader.GetAttribute("PersonTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.PersonTerm = text;
					}
					text = Reader.GetAttribute("ImmaturePersonTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.ImmaturePersonTerm = text;
					}
					text = Reader.GetAttribute("FormalAddressTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.FormalAddressTerm = text;
					}
					text = Reader.GetAttribute("OffspringTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.OffspringTerm = text;
					}
					text = Reader.GetAttribute("SiblingTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.SiblingTerm = text;
					}
					text = Reader.GetAttribute("ParentTerm");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.ParentTerm = text;
					}
					text = Reader.GetAttribute("UseBareIndicative");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet.UseBareIndicative = text == "true";
					}
					text = Reader.GetAttribute("Name");
					if (!string.IsNullOrEmpty(text))
					{
						pronounSet._Name = text;
					}
					if (!PronounSet.TempPronounSets.ContainsKey(pronounSet.Name))
					{
						PronounSet.TempPronounSets.Add(pronounSet.Name, pronounSet);
					}
				}
				else if (Reader.Name == "removepronounset")
				{
					string attribute = Reader.GetAttribute("Name");
					if (string.IsNullOrEmpty(attribute))
					{
						throw new Exception("removepronounset tag had no Name attribute");
					}
					if (PronounSet.PronounSets.ContainsKey(attribute))
					{
						PronounSet.PronounSets.Remove(attribute);
					}
					if (PronounSet.TempPronounSets.ContainsKey(attribute))
					{
						PronounSet.TempPronounSets.Remove(attribute);
					}
				}
			}
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x001E423D File Offset: 0x001E243D
		public PronounSet Register()
		{
			if (!PronounSet.PronounSets.ContainsKey(this.Name))
			{
				PronounSet.PronounSets.Add(this.Name, this);
				PronounSet.All = null;
				this.FlushRelevantCaches();
			}
			return this;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x001E4270 File Offset: 0x001E2470
		public void FlushRelevantCaches()
		{
			if (base.Generic)
			{
				if (base.Plural)
				{
					PronounSet.GenericPlurals = null;
				}
				else
				{
					PronounSet.GenericSingulars = null;
				}
				if (base.UseBareIndicative)
				{
					PronounSet.GenericNonpersonals = null;
					if (base.Plural)
					{
						PronounSet.GenericNonpersonalPlurals = null;
						return;
					}
					PronounSet.GenericNonpersonalSingulars = null;
					return;
				}
				else
				{
					PronounSet.GenericPersonals = null;
					if (base.Plural)
					{
						PronounSet.GenericPersonalPlurals = null;
						return;
					}
					PronounSet.GenericPersonalSingulars = null;
					return;
				}
			}
			else
			{
				if (base.Plural)
				{
					PronounSet.Plurals = null;
				}
				else
				{
					PronounSet.Singulars = null;
				}
				if (base.UseBareIndicative)
				{
					PronounSet.Nonpersonals = null;
					if (base.Plural)
					{
						PronounSet.NonpersonalPlurals = null;
						return;
					}
					PronounSet.NonpersonalSingulars = null;
					return;
				}
				else
				{
					PronounSet.Personals = null;
					if (base.Plural)
					{
						PronounSet.PersonalPlurals = null;
						return;
					}
					PronounSet.PersonalSingulars = null;
					return;
				}
			}
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x001E4330 File Offset: 0x001E2530
		public static PronounSet Get(string Name)
		{
			if (Name == null)
			{
				return null;
			}
			if (PronounSet.PronounSets.ContainsKey(Name))
			{
				return PronounSet.PronounSets[Name];
			}
			if (!Name.Contains("/"))
			{
				throw new Exception("pronoun set " + Name + " not known and does not appear to be an explicit specification");
			}
			PronounSet pronounSet = PronounSet.ConstructFromName(Name);
			if (!PronounSet.PronounSets.ContainsKey(pronounSet.Name))
			{
				pronounSet.Register();
			}
			return pronounSet;
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x001E439F File Offset: 0x001E259F
		public static PronounSet GetIfExists(string Name)
		{
			if (Name == null)
			{
				return null;
			}
			if (PronounSet.PronounSets.ContainsKey(Name))
			{
				return PronounSet.PronounSets[Name];
			}
			return null;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x001E43C0 File Offset: 0x001E25C0
		public static bool Exists(string Name)
		{
			return PronounSet.PronounSets.ContainsKey(Name);
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x001E43D0 File Offset: 0x001E25D0
		public static PronounSet Generate(bool Generic = false, int PluralChance = 0, int UseBareIndicativeChance = 0, GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null)
		{
			if (PronounSet.EnableGeneration)
			{
				int num = 0;
				while (++num <= 1000)
				{
					PronounSet pronounSet = new PronounSet(Generic, true, false, false, null, null, null, null, null, null, null, null, null, null, null, false, false);
					if (PluralChance.in100())
					{
						pronounSet.Plural = true;
					}
					if (UseBareIndicativeChance.in100())
					{
						pronounSet.UseBareIndicative = true;
					}
					string text;
					string text2;
					string text3;
					Gender.GenerateBasePronouns(out text, out text2, out text3);
					pronounSet.Subjective = text;
					pronounSet.Objective = text2;
					pronounSet.PossessiveAdjective = text3;
					if (!pronounSet.Plural && pronounSet.Subjective.EndsWith("y"))
					{
						pronounSet.PseudoPlural = true;
					}
					if (!PronounSet.PronounSets.ContainsKey(pronounSet.Name))
					{
						NameMaker.MakeName(ref pronounSet._PersonTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderPersonTerm", null, null, false, null, null);
						NameMaker.MakeName(ref pronounSet._ImmaturePersonTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderImmaturePersonTerm", null, null, false, null, null);
						NameMaker.MakeName(ref pronounSet._FormalAddressTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderFormalAddressTerm", null, null, false, null, null);
						NameMaker.MakeName(ref pronounSet._OffspringTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderOffspringTerm", null, null, false, null, null);
						NameMaker.MakeName(ref pronounSet._SiblingTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderSiblingTerm", null, null, false, null, null);
						NameMaker.MakeName(ref pronounSet._ParentTerm, For, Genotype, Subtype, Species, Culture, Faction, null, null, null, null, "GenderParentTerm", null, null, false, null, null);
						return pronounSet;
					}
				}
				throw new Exception("cannot generate pronouns set with unused name");
			}
			bool flag = PluralChance.in100();
			bool flag2 = UseBareIndicativeChance.in100();
			if (Generic)
			{
				if (flag)
				{
					if (!flag2)
					{
						return PronounSet.GetAnyGenericPersonalPlural();
					}
					return PronounSet.GetAnyGenericNonpersonalPlural();
				}
				else
				{
					if (!flag2)
					{
						return PronounSet.GetAnyGenericPersonalSingular();
					}
					return PronounSet.GetAnyGenericNonpersonalSingular();
				}
			}
			else if (flag)
			{
				if (!flag2)
				{
					return PronounSet.GetAnyPersonalPlural();
				}
				return PronounSet.GetAnyNonpersonalPlural();
			}
			else
			{
				if (!flag2)
				{
					return PronounSet.GetAnyPersonalSingular();
				}
				return PronounSet.GetAnyNonpersonalSingular();
			}
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x001E4624 File Offset: 0x001E2824
		public static List<PronounSet> Find(Predicate<PronounSet> Filter)
		{
			int num = 0;
			foreach (string text in PronounSet.PronounSets.Keys)
			{
				if (Filter(PronounSet.PronounSets[text]))
				{
					num++;
				}
			}
			List<PronounSet> list = new List<PronounSet>(num);
			if (num > 0)
			{
				foreach (string text2 in PronounSet.PronounSets.Keys)
				{
					PronounSet pronounSet = PronounSet.PronounSets[text2];
					if (Filter(pronounSet))
					{
						list.Add(pronounSet);
					}
				}
			}
			return list;
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x001E46FC File Offset: 0x001E28FC
		public static List<PronounSet> GetAll()
		{
			if (PronounSet.All == null)
			{
				PronounSet.All = PronounSet.Find((PronounSet PS) => true);
			}
			return PronounSet.All;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x001E4733 File Offset: 0x001E2933
		public static PronounSet GetAny()
		{
			return PronounSet.GetAll().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x001E4740 File Offset: 0x001E2940
		public static string GetAnyName()
		{
			return PronounSet.GetAny().Name;
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x001E474C File Offset: 0x001E294C
		public static List<PronounSet> GetAllGeneric()
		{
			if (PronounSet.Generics == null)
			{
				PronounSet.Generics = PronounSet.Find((PronounSet PS) => PS.Generic);
			}
			return PronounSet.Generics;
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x001E4783 File Offset: 0x001E2983
		public static PronounSet GetAnyGeneric()
		{
			return PronounSet.GetAllGeneric().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x001E4790 File Offset: 0x001E2990
		public static string GetAnyGenericName()
		{
			return PronounSet.GetAnyGeneric().Name;
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x001E479C File Offset: 0x001E299C
		public static List<PronounSet> GetAllPlural()
		{
			if (PronounSet.Plurals == null)
			{
				PronounSet.Plurals = PronounSet.Find((PronounSet PS) => PS.Plural);
			}
			return PronounSet.Plurals;
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x001E47D3 File Offset: 0x001E29D3
		public static PronounSet GetAnyPlural()
		{
			return PronounSet.GetAllPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x001E47E0 File Offset: 0x001E29E0
		public static string GetAnyPluralName()
		{
			return PronounSet.GetAnyPlural().Name;
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x001E47EC File Offset: 0x001E29EC
		public static List<PronounSet> GetAllSingular()
		{
			if (PronounSet.Singulars == null)
			{
				PronounSet.Singulars = PronounSet.Find((PronounSet PS) => !PS.Plural);
			}
			return PronounSet.Singulars;
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x001E4823 File Offset: 0x001E2A23
		public static PronounSet GetAnySingular()
		{
			return PronounSet.GetAllSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x001E4830 File Offset: 0x001E2A30
		public static string GetAnySingularName()
		{
			return PronounSet.GetAnySingular().Name;
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x001E483C File Offset: 0x001E2A3C
		public static List<PronounSet> GetAllGenericSingular()
		{
			if (PronounSet.GenericSingulars == null)
			{
				PronounSet.GenericSingulars = PronounSet.Find((PronounSet PS) => PS.Generic && !PS.Plural);
			}
			return PronounSet.GenericSingulars;
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x001E4873 File Offset: 0x001E2A73
		public static PronounSet GetAnyGenericSingular()
		{
			return PronounSet.GetAllGenericSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x001E4880 File Offset: 0x001E2A80
		public static string GetAnyGenericSingularName()
		{
			return PronounSet.GetAnyGenericSingular().Name;
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x001E488C File Offset: 0x001E2A8C
		public static List<PronounSet> GetAllGenericPlural()
		{
			if (PronounSet.GenericPlurals == null)
			{
				PronounSet.GenericPlurals = PronounSet.Find((PronounSet PS) => PS.Generic && PS.Plural);
			}
			return PronounSet.GenericPlurals;
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x001E48C3 File Offset: 0x001E2AC3
		public static PronounSet GetAnyGenericPlural()
		{
			return PronounSet.GetAllGenericPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x001E48D0 File Offset: 0x001E2AD0
		public static string GetAnyGenericPluralName()
		{
			return PronounSet.GetAnyGenericPlural().Name;
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x001E48DC File Offset: 0x001E2ADC
		public static List<PronounSet> GetAllPersonal()
		{
			if (PronounSet.Personals == null)
			{
				PronounSet.Personals = PronounSet.Find((PronounSet PS) => !PS.UseBareIndicative);
			}
			return PronounSet.Personals;
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x001E4913 File Offset: 0x001E2B13
		public static PronounSet GetAnyPersonal()
		{
			return PronounSet.GetAllPersonal().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x001E4920 File Offset: 0x001E2B20
		public static string GetAnyPersonalName()
		{
			return PronounSet.GetAnyPersonal().Name;
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x001E492C File Offset: 0x001E2B2C
		public static List<PronounSet> GetAllPersonalPlural()
		{
			if (PronounSet.PersonalPlurals == null)
			{
				PronounSet.PersonalPlurals = PronounSet.Find((PronounSet PS) => !PS.UseBareIndicative && PS.Plural);
			}
			return PronounSet.PersonalPlurals;
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x001E4963 File Offset: 0x001E2B63
		public static PronounSet GetAnyPersonalPlural()
		{
			return PronounSet.GetAllPersonalPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x001E4970 File Offset: 0x001E2B70
		public static string GetAnyPersonalPluralName()
		{
			return PronounSet.GetAnyPersonalPlural().Name;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x001E497C File Offset: 0x001E2B7C
		public static List<PronounSet> GetAllPersonalSingular()
		{
			if (PronounSet.PersonalSingulars == null)
			{
				PronounSet.PersonalSingulars = PronounSet.Find((PronounSet PS) => !PS.UseBareIndicative && !PS.Plural);
			}
			return PronounSet.PersonalSingulars;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x001E49B3 File Offset: 0x001E2BB3
		public static PronounSet GetAnyPersonalSingular()
		{
			return PronounSet.GetAllPersonalSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x001E49C0 File Offset: 0x001E2BC0
		public static string GetAnyPersonalSingularName()
		{
			return PronounSet.GetAnyPersonalSingular().Name;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x001E49CC File Offset: 0x001E2BCC
		public static List<PronounSet> GetAllGenericPersonal()
		{
			if (PronounSet.GenericPersonals == null)
			{
				PronounSet.GenericPersonals = PronounSet.Find((PronounSet PS) => PS.Generic && !PS.UseBareIndicative);
			}
			return PronounSet.GenericPersonals;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x001E4A03 File Offset: 0x001E2C03
		public static PronounSet GetAnyGenericPersonal()
		{
			return PronounSet.GetAllGenericPersonal().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x001E4A10 File Offset: 0x001E2C10
		public static string GetAnyGenericPersonalName()
		{
			return PronounSet.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x001E4A1C File Offset: 0x001E2C1C
		public static List<PronounSet> GetAllGenericPersonalPlural()
		{
			if (PronounSet.GenericPersonalPlurals == null)
			{
				PronounSet.GenericPersonalPlurals = PronounSet.Find((PronounSet PS) => PS.Generic && !PS.UseBareIndicative && PS.Plural);
			}
			return PronounSet.GenericPersonalPlurals;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x001E4A53 File Offset: 0x001E2C53
		public static PronounSet GetAnyGenericPersonalPlural()
		{
			return PronounSet.GetAllGenericPersonalPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x001E4A60 File Offset: 0x001E2C60
		public static string GetAnyGenericPersonalPluralName()
		{
			return PronounSet.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x001E4A6C File Offset: 0x001E2C6C
		public static List<PronounSet> GetAllGenericPersonalSingular()
		{
			if (PronounSet.GenericPersonalSingulars == null)
			{
				PronounSet.GenericPersonalSingulars = PronounSet.Find((PronounSet PS) => PS.Generic && !PS.UseBareIndicative && !PS.Plural);
			}
			return PronounSet.GenericPersonalSingulars;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x001E4AA3 File Offset: 0x001E2CA3
		public static PronounSet GetAnyGenericPersonalSingular()
		{
			return PronounSet.GetAllGenericPersonalSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x001E4AB0 File Offset: 0x001E2CB0
		public static string GetAnyGenericPersonalSingularName()
		{
			return PronounSet.GetAnyGenericPersonalSingular().Name;
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x001E4ABC File Offset: 0x001E2CBC
		public static List<PronounSet> GetAllNonpersonal()
		{
			if (PronounSet.Nonpersonals == null)
			{
				PronounSet.Nonpersonals = PronounSet.Find((PronounSet PS) => PS.UseBareIndicative);
			}
			return PronounSet.Nonpersonals;
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x001E4AF3 File Offset: 0x001E2CF3
		public static PronounSet GetAnyNonpersonal()
		{
			return PronounSet.GetAllNonpersonal().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x001E4B00 File Offset: 0x001E2D00
		public static string GetAnyNonpersonalName()
		{
			return PronounSet.GetAnyNonpersonal().Name;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x001E4B0C File Offset: 0x001E2D0C
		public static List<PronounSet> GetAllNonpersonalPlural()
		{
			if (PronounSet.NonpersonalPlurals == null)
			{
				PronounSet.NonpersonalPlurals = PronounSet.Find((PronounSet PS) => PS.UseBareIndicative && PS.Plural);
			}
			return PronounSet.NonpersonalPlurals;
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x001E4B43 File Offset: 0x001E2D43
		public static PronounSet GetAnyNonpersonalPlural()
		{
			return PronounSet.GetAllNonpersonalPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001E4B50 File Offset: 0x001E2D50
		public static string GetAnyNonpersonalPluralName()
		{
			return PronounSet.GetAnyNonpersonalPlural().Name;
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001E4B5C File Offset: 0x001E2D5C
		public static List<PronounSet> GetAllNonpersonalSingular()
		{
			if (PronounSet.NonpersonalSingulars == null)
			{
				PronounSet.NonpersonalSingulars = PronounSet.Find((PronounSet PS) => PS.UseBareIndicative && !PS.Plural);
			}
			return PronounSet.NonpersonalSingulars;
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x001E4B93 File Offset: 0x001E2D93
		public static PronounSet GetAnyNonpersonalSingular()
		{
			return PronounSet.GetAllNonpersonalSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x001E4BA0 File Offset: 0x001E2DA0
		public static string GetAnyNonpersonalSingularName()
		{
			return PronounSet.GetAnyNonpersonalSingular().Name;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x001E4BAC File Offset: 0x001E2DAC
		public static List<PronounSet> GetAllGenericNonpersonal()
		{
			if (PronounSet.GenericNonpersonals == null)
			{
				PronounSet.GenericNonpersonals = PronounSet.Find((PronounSet PS) => PS.Generic && PS.UseBareIndicative);
			}
			return PronounSet.GenericNonpersonals;
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x001E4BE3 File Offset: 0x001E2DE3
		public static PronounSet GetAnyGenericNonpersonal()
		{
			return PronounSet.GetAllGenericNonpersonal().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x001E4BF0 File Offset: 0x001E2DF0
		public static string GetAnyGenericNonpersonalName()
		{
			return PronounSet.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x001E4BFC File Offset: 0x001E2DFC
		public static List<PronounSet> GetAllGenericNonpersonalPlural()
		{
			if (PronounSet.GenericNonpersonalPlurals == null)
			{
				PronounSet.GenericNonpersonalPlurals = PronounSet.Find((PronounSet PS) => PS.Generic && PS.UseBareIndicative && PS.Plural);
			}
			return PronounSet.GenericNonpersonalPlurals;
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x001E4C33 File Offset: 0x001E2E33
		public static PronounSet GetAnyGenericNonpersonalPlural()
		{
			return PronounSet.GetAllGenericNonpersonalPlural().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x001E4C40 File Offset: 0x001E2E40
		public static string GetAnyGenericNonpersonalPluralName()
		{
			return PronounSet.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x001E4C4C File Offset: 0x001E2E4C
		public static List<PronounSet> GetAllGenericNonpersonalSingular()
		{
			if (PronounSet.GenericNonpersonalSingulars == null)
			{
				PronounSet.GenericNonpersonalSingulars = PronounSet.Find((PronounSet PS) => PS.Generic && PS.UseBareIndicative && !PS.Plural);
			}
			return PronounSet.GenericNonpersonalSingulars;
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x001E4C83 File Offset: 0x001E2E83
		public static PronounSet GetAnyGenericNonpersonalSingular()
		{
			return PronounSet.GetAllGenericNonpersonalSingular().GetRandomElement<PronounSet>(null);
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x001E4C90 File Offset: 0x001E2E90
		public static string GetAnyGenericNonpersonalSingularName()
		{
			return PronounSet.GetAnyGenericNonpersonalSingular().Name;
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x001E4C9C File Offset: 0x001E2E9C
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
									return PronounSet.GetAnyGenericNonpersonalName();
								}
							}
						}
						else if (Name == "genericnonpersonalplural")
						{
							return PronounSet.GetAnyGenericNonpersonalPluralName();
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
									return PronounSet.GetAnyGenericSingularName();
								}
							}
						}
						else if (Name == "personal")
						{
							return PronounSet.GetAnyPersonalName();
						}
					}
					else if (Name == "genericpersonal")
					{
						return PronounSet.GetAnyGenericPersonalName();
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
									return PronounSet.GetAnyGenericPersonalPluralName();
								}
							}
						}
						else if (Name == "any")
						{
							return PronounSet.GetAnyName();
						}
					}
					else if (Name == "nonpersonalsingular")
					{
						return PronounSet.GetAnyNonpersonalSingularName();
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
								return PronounSet.GetAnyGenericName();
							}
						}
					}
					else if (Name == "genericplural")
					{
						return PronounSet.GetAnyGenericPluralName();
					}
				}
				else if (Name == "generatemaybeplural")
				{
					return PronounSet.Generate(false, 10, 0, null, null, null, null, null, null).Register().Name;
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
								return PronounSet.GetAnyNonpersonalName();
							}
						}
					}
					else if (Name == "nonpersonalplural")
					{
						return PronounSet.GetAnyNonpersonalPluralName();
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
								return PronounSet.Generate(false, 0, 10, null, null, null, null, null, null).Register().Name;
							}
						}
					}
					else if (Name == "personalsingular")
					{
						return PronounSet.GetAnyPersonalSingularName();
					}
				}
				else if (Name == "anysingular")
				{
					return PronounSet.GetAnySingularName();
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
								return PronounSet.GetAnyPersonalPluralName();
							}
						}
					}
					else if (Name == "genericpersonalsingular")
					{
						return PronounSet.GetAnyGenericPersonalSingularName();
					}
				}
				else if (Name == "generatemaybepluralmaybenonpersonal")
				{
					return PronounSet.Generate(false, 10, 10, null, null, null, null, null, null).Register().Name;
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
							return PronounSet.GetAnyGenericNonpersonalSingularName();
						}
					}
				}
				else if (Name == "anyplural")
				{
					return PronounSet.GetAnyPluralName();
				}
			}
			else if (Name == "generate")
			{
				return PronounSet.Generate(false, 0, 0, null, null, null, null, null, null).Register().Name;
			}
			return Name;
		}

		// Token: 0x04002E49 RID: 11849
		public static bool EnableSelection = false;

		// Token: 0x04002E4A RID: 11850
		public static bool EnableGeneration = false;

		// Token: 0x04002E4B RID: 11851
		public static bool EnableConversationalExchange = false;

		// Token: 0x04002E4C RID: 11852
		public static Dictionary<string, PronounSet> PronounSets = null;

		// Token: 0x04002E4D RID: 11853
		public static Dictionary<string, PronounSet> TempPronounSets = null;

		// Token: 0x04002E4E RID: 11854
		[NonSerialized]
		public static List<PronounSet> All = null;

		// Token: 0x04002E4F RID: 11855
		[NonSerialized]
		public static List<PronounSet> GenericPlurals = null;

		// Token: 0x04002E50 RID: 11856
		[NonSerialized]
		public static List<PronounSet> Generics = null;

		// Token: 0x04002E51 RID: 11857
		[NonSerialized]
		public static List<PronounSet> GenericSingulars = null;

		// Token: 0x04002E52 RID: 11858
		[NonSerialized]
		public static List<PronounSet> Plurals = null;

		// Token: 0x04002E53 RID: 11859
		[NonSerialized]
		public static List<PronounSet> Singulars = null;

		// Token: 0x04002E54 RID: 11860
		[NonSerialized]
		public static List<PronounSet> GenericPersonalPlurals = null;

		// Token: 0x04002E55 RID: 11861
		[NonSerialized]
		public static List<PronounSet> GenericPersonals = null;

		// Token: 0x04002E56 RID: 11862
		[NonSerialized]
		public static List<PronounSet> GenericPersonalSingulars = null;

		// Token: 0x04002E57 RID: 11863
		[NonSerialized]
		public static List<PronounSet> Personals = null;

		// Token: 0x04002E58 RID: 11864
		[NonSerialized]
		public static List<PronounSet> PersonalPlurals = null;

		// Token: 0x04002E59 RID: 11865
		[NonSerialized]
		public static List<PronounSet> PersonalSingulars = null;

		// Token: 0x04002E5A RID: 11866
		[NonSerialized]
		public static List<PronounSet> GenericNonpersonalPlurals = null;

		// Token: 0x04002E5B RID: 11867
		[NonSerialized]
		public static List<PronounSet> GenericNonpersonals = null;

		// Token: 0x04002E5C RID: 11868
		[NonSerialized]
		public static List<PronounSet> GenericNonpersonalSingulars = null;

		// Token: 0x04002E5D RID: 11869
		[NonSerialized]
		public static List<PronounSet> Nonpersonals = null;

		// Token: 0x04002E5E RID: 11870
		[NonSerialized]
		public static List<PronounSet> NonpersonalPlurals = null;

		// Token: 0x04002E5F RID: 11871
		[NonSerialized]
		public static List<PronounSet> NonpersonalSingulars = null;

		// Token: 0x04002E60 RID: 11872
		[NonSerialized]
		public static PronounSet DefaultPlayer = null;

		// Token: 0x04002E61 RID: 11873
		[NonSerialized]
		public static PronounSet DefaultPlayerPlural = null;

		// Token: 0x04002E62 RID: 11874
		public bool FromGender;

		// Token: 0x04002E63 RID: 11875
		[NonSerialized]
		public string _Name;

		// Token: 0x04002E64 RID: 11876
		[NonSerialized]
		public string _CapitalizedName;

		// Token: 0x04002E65 RID: 11877
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();
	}
}
