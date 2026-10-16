using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000951 RID: 2385
	[Serializable]
	public abstract class BasePronounProvider : IPronounProvider
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060061C6 RID: 25030
		public abstract string Name { get; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060061C7 RID: 25031
		public abstract string CapitalizedName { get; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060061C9 RID: 25033 RVA: 0x001E0317 File Offset: 0x001DE517
		// (set) Token: 0x060061C8 RID: 25032 RVA: 0x001E0307 File Offset: 0x001DE507
		public bool Generic
		{
			get
			{
				return this._Generic;
			}
			set
			{
				this._Generic = value;
				this.ConfigurationUpdated(true);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x001E032F File Offset: 0x001DE52F
		// (set) Token: 0x060061CA RID: 25034 RVA: 0x001E031F File Offset: 0x001DE51F
		public bool Generated
		{
			get
			{
				return this._Generated;
			}
			set
			{
				this._Generated = value;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x001E0347 File Offset: 0x001DE547
		// (set) Token: 0x060061CC RID: 25036 RVA: 0x001E0337 File Offset: 0x001DE537
		public bool Plural
		{
			get
			{
				return this._Plural;
			}
			set
			{
				this._Plural = value;
				this.ConfigurationUpdated(true);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060061CF RID: 25039 RVA: 0x001E035F File Offset: 0x001DE55F
		// (set) Token: 0x060061CE RID: 25038 RVA: 0x001E034F File Offset: 0x001DE54F
		public bool PseudoPlural
		{
			get
			{
				return this._PseudoPlural;
			}
			set
			{
				this._PseudoPlural = value;
				this.ConfigurationUpdated(true);
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x001E037E File Offset: 0x001DE57E
		// (set) Token: 0x060061D0 RID: 25040 RVA: 0x001E0367 File Offset: 0x001DE567
		public string Subjective
		{
			get
			{
				return this._Subjective;
			}
			set
			{
				this._Subjective = value;
				this._CapitalizedSubjective = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x001E0386 File Offset: 0x001DE586
		public string CapitalizedSubjective
		{
			get
			{
				if (this._CapitalizedSubjective == null)
				{
					this._CapitalizedSubjective = ColorUtility.CapitalizeExceptFormatting(this.Subjective);
				}
				return this._CapitalizedSubjective;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x001E03BE File Offset: 0x001DE5BE
		// (set) Token: 0x060061D3 RID: 25043 RVA: 0x001E03A7 File Offset: 0x001DE5A7
		public string Objective
		{
			get
			{
				if (this._Objective == null)
				{
					this._Objective = this.Subjective;
				}
				return this._Objective;
			}
			set
			{
				this._Objective = value;
				this._CapitalizedObjective = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x001E03DA File Offset: 0x001DE5DA
		public string CapitalizedObjective
		{
			get
			{
				if (this._CapitalizedObjective == null)
				{
					this._CapitalizedObjective = ColorUtility.CapitalizeExceptFormatting(this.Objective);
				}
				return this._CapitalizedObjective;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x001E0412 File Offset: 0x001DE612
		// (set) Token: 0x060061D6 RID: 25046 RVA: 0x001E03FB File Offset: 0x001DE5FB
		public string PossessiveAdjective
		{
			get
			{
				if (this._PossessiveAdjective == null)
				{
					this._PossessiveAdjective = this.ExpectedPossessiveAdjective();
				}
				return this._PossessiveAdjective;
			}
			set
			{
				this._PossessiveAdjective = value;
				this._CapitalizedPossessiveAdjective = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x001E042E File Offset: 0x001DE62E
		public string CapitalizedPossessiveAdjective
		{
			get
			{
				if (this._CapitalizedPossessiveAdjective == null)
				{
					this._CapitalizedPossessiveAdjective = ColorUtility.CapitalizeExceptFormatting(this.PossessiveAdjective);
				}
				return this._CapitalizedPossessiveAdjective;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x001E0466 File Offset: 0x001DE666
		// (set) Token: 0x060061D9 RID: 25049 RVA: 0x001E044F File Offset: 0x001DE64F
		public string SubstantivePossessive
		{
			get
			{
				if (this._SubstantivePossessive == null)
				{
					this._SubstantivePossessive = this.ExpectedSubstantivePossessive();
				}
				return this._SubstantivePossessive;
			}
			set
			{
				this._SubstantivePossessive = value;
				this._CapitalizedSubstantivePossessive = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060061DB RID: 25051 RVA: 0x001E0482 File Offset: 0x001DE682
		public string CapitalizedSubstantivePossessive
		{
			get
			{
				if (this._CapitalizedSubstantivePossessive == null)
				{
					this._CapitalizedSubstantivePossessive = ColorUtility.CapitalizeExceptFormatting(this.SubstantivePossessive);
				}
				return this._CapitalizedSubstantivePossessive;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x001E04BA File Offset: 0x001DE6BA
		// (set) Token: 0x060061DC RID: 25052 RVA: 0x001E04A3 File Offset: 0x001DE6A3
		public string Reflexive
		{
			get
			{
				if (this._Reflexive == null)
				{
					this._Reflexive = this.ExpectedReflexive();
				}
				return this._Reflexive;
			}
			set
			{
				this._Reflexive = value;
				this._CapitalizedReflexive = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x001E04D6 File Offset: 0x001DE6D6
		public string CapitalizedReflexive
		{
			get
			{
				if (this._CapitalizedReflexive == null)
				{
					this._CapitalizedReflexive = ColorUtility.CapitalizeExceptFormatting(this.Reflexive);
				}
				return this._CapitalizedReflexive;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060061E0 RID: 25056 RVA: 0x001E050E File Offset: 0x001DE70E
		// (set) Token: 0x060061DF RID: 25055 RVA: 0x001E04F7 File Offset: 0x001DE6F7
		public string PersonTerm
		{
			get
			{
				return this._PersonTerm;
			}
			set
			{
				this._PersonTerm = value;
				this._CapitalizedPersonTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x001E0516 File Offset: 0x001DE716
		public string CapitalizedPersonTerm
		{
			get
			{
				if (this._CapitalizedPersonTerm == null)
				{
					this._CapitalizedPersonTerm = ColorUtility.CapitalizeExceptFormatting(this.PersonTerm);
				}
				return this._CapitalizedPersonTerm;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x001E054E File Offset: 0x001DE74E
		// (set) Token: 0x060061E2 RID: 25058 RVA: 0x001E0537 File Offset: 0x001DE737
		public string ImmaturePersonTerm
		{
			get
			{
				return this._ImmaturePersonTerm;
			}
			set
			{
				this._ImmaturePersonTerm = value;
				this._CapitalizedImmaturePersonTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x001E0556 File Offset: 0x001DE756
		public string CapitalizedImmaturePersonTerm
		{
			get
			{
				if (this._CapitalizedImmaturePersonTerm == null)
				{
					this._CapitalizedImmaturePersonTerm = ColorUtility.CapitalizeExceptFormatting(this.ImmaturePersonTerm);
				}
				return this._CapitalizedImmaturePersonTerm;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x001E058E File Offset: 0x001DE78E
		// (set) Token: 0x060061E5 RID: 25061 RVA: 0x001E0577 File Offset: 0x001DE777
		public string FormalAddressTerm
		{
			get
			{
				if (this._FormalAddressTerm == null)
				{
					this._FormalAddressTerm = this.Objective;
				}
				return this._FormalAddressTerm;
			}
			set
			{
				this._FormalAddressTerm = value;
				this._CapitalizedFormalAddressTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x001E05AA File Offset: 0x001DE7AA
		public string CapitalizedFormalAddressTerm
		{
			get
			{
				if (this._CapitalizedFormalAddressTerm == null)
				{
					this._CapitalizedFormalAddressTerm = ColorUtility.CapitalizeExceptFormatting(this.FormalAddressTerm);
				}
				return this._CapitalizedFormalAddressTerm;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060061E9 RID: 25065 RVA: 0x001E05E2 File Offset: 0x001DE7E2
		// (set) Token: 0x060061E8 RID: 25064 RVA: 0x001E05CB File Offset: 0x001DE7CB
		public string OffspringTerm
		{
			get
			{
				if (this._OffspringTerm == null)
				{
					this._OffspringTerm = this.Objective;
				}
				return this._OffspringTerm;
			}
			set
			{
				this._OffspringTerm = value;
				this._CapitalizedOffspringTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x001E05FE File Offset: 0x001DE7FE
		public string CapitalizedOffspringTerm
		{
			get
			{
				if (this._CapitalizedOffspringTerm == null)
				{
					this._CapitalizedOffspringTerm = ColorUtility.CapitalizeExceptFormatting(this.OffspringTerm);
				}
				return this._CapitalizedOffspringTerm;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x001E0636 File Offset: 0x001DE836
		// (set) Token: 0x060061EB RID: 25067 RVA: 0x001E061F File Offset: 0x001DE81F
		public string SiblingTerm
		{
			get
			{
				if (this._SiblingTerm == null)
				{
					this._SiblingTerm = this.Objective;
				}
				return this._SiblingTerm;
			}
			set
			{
				this._SiblingTerm = value;
				this._CapitalizedSiblingTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x001E0652 File Offset: 0x001DE852
		public string CapitalizedSiblingTerm
		{
			get
			{
				if (this._CapitalizedSiblingTerm == null)
				{
					this._CapitalizedSiblingTerm = ColorUtility.CapitalizeExceptFormatting(this.SiblingTerm);
				}
				return this._CapitalizedSiblingTerm;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060061EF RID: 25071 RVA: 0x001E068A File Offset: 0x001DE88A
		// (set) Token: 0x060061EE RID: 25070 RVA: 0x001E0673 File Offset: 0x001DE873
		public string ParentTerm
		{
			get
			{
				if (this._ParentTerm == null)
				{
					this._ParentTerm = this.Objective;
				}
				return this._ParentTerm;
			}
			set
			{
				this._ParentTerm = value;
				this._CapitalizedParentTerm = null;
				this.ConfigurationUpdated(false);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060061F0 RID: 25072 RVA: 0x001E06A6 File Offset: 0x001DE8A6
		public string CapitalizedParentTerm
		{
			get
			{
				if (this._CapitalizedParentTerm == null)
				{
					this._CapitalizedParentTerm = ColorUtility.CapitalizeExceptFormatting(this.ParentTerm);
				}
				return this._CapitalizedParentTerm;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060061F1 RID: 25073 RVA: 0x001E06C7 File Offset: 0x001DE8C7
		public string IndicativeProximal
		{
			get
			{
				if (!this.Plural)
				{
					return "this";
				}
				return "these";
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060061F2 RID: 25074 RVA: 0x001E06DC File Offset: 0x001DE8DC
		public string CapitalizedIndicativeProximal
		{
			get
			{
				if (!this.Plural)
				{
					return "This";
				}
				return "These";
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060061F3 RID: 25075 RVA: 0x001E06F1 File Offset: 0x001DE8F1
		public string IndicativeDistal
		{
			get
			{
				if (!this.Plural)
				{
					return "that";
				}
				return "those";
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060061F4 RID: 25076 RVA: 0x001E0706 File Offset: 0x001DE906
		public string CapitalizedIndicativeDistal
		{
			get
			{
				if (!this.Plural)
				{
					return "That";
				}
				return "Those";
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x001E072B File Offset: 0x001DE92B
		// (set) Token: 0x060061F5 RID: 25077 RVA: 0x001E071B File Offset: 0x001DE91B
		public bool UseBareIndicative
		{
			get
			{
				return this._UseBareIndicative;
			}
			set
			{
				this._UseBareIndicative = value;
				this.ConfigurationUpdated(true);
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x001E0733 File Offset: 0x001DE933
		protected virtual void ConfigurationUpdated(bool Categorizing = false)
		{
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x001E0738 File Offset: 0x001DE938
		public BasePronounProvider(bool _Generic = false, bool _Generated = false, bool _Plural = false, bool _PseudoPlural = false, string _Subjective = null, string _Objective = null, string _PossessiveAdjective = null, string _SubstantivePossessive = null, string _Reflexive = null, string _PersonTerm = null, string _ImmaturePersonTerm = null, string _FormalAddressTerm = null, string _OffspringTerm = null, string _SiblingTerm = null, string _ParentTerm = null, bool _UseBareIndicative = false)
		{
			this.Generic = _Generic;
			this.Generated = _Generated;
			this.Plural = _Plural;
			this.PseudoPlural = _PseudoPlural;
			if (_Subjective != null)
			{
				this.Subjective = _Subjective;
			}
			if (_Objective != null)
			{
				this.Objective = _Objective;
			}
			if (_PossessiveAdjective != null)
			{
				this.PossessiveAdjective = _PossessiveAdjective;
			}
			if (_SubstantivePossessive != null)
			{
				this.SubstantivePossessive = _SubstantivePossessive;
			}
			if (_Reflexive != null)
			{
				this.Reflexive = _Reflexive;
			}
			if (_PersonTerm != null)
			{
				this.PersonTerm = _PersonTerm;
			}
			if (_ImmaturePersonTerm != null)
			{
				this.PersonTerm = _PersonTerm;
			}
			if (_FormalAddressTerm != null)
			{
				this.FormalAddressTerm = _FormalAddressTerm;
			}
			if (_OffspringTerm != null)
			{
				this.OffspringTerm = _OffspringTerm;
			}
			if (_SiblingTerm != null)
			{
				this.SiblingTerm = _SiblingTerm;
			}
			if (_ParentTerm != null)
			{
				this.ParentTerm = _ParentTerm;
			}
			this.UseBareIndicative = _UseBareIndicative;
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x001E0890 File Offset: 0x001DEA90
		public BasePronounProvider(BasePronounProvider Original)
		{
			this.CopyFrom(Original);
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x001E0944 File Offset: 0x001DEB44
		public virtual void CopyFrom(BasePronounProvider Original)
		{
			this._Generic = Original._Generic;
			this._Generated = Original._Generated;
			this._Plural = Original._Plural;
			this._PseudoPlural = Original._PseudoPlural;
			this._Subjective = Original._Subjective;
			this._Objective = Original._Objective;
			this._PossessiveAdjective = Original._PossessiveAdjective;
			this._SubstantivePossessive = Original._SubstantivePossessive;
			this._Reflexive = Original._Reflexive;
			this._PersonTerm = Original._PersonTerm;
			this._ImmaturePersonTerm = Original._ImmaturePersonTerm;
			this._FormalAddressTerm = Original._FormalAddressTerm;
			this._OffspringTerm = Original._OffspringTerm;
			this._SiblingTerm = Original._SiblingTerm;
			this._ParentTerm = Original._ParentTerm;
			this._UseBareIndicative = Original._UseBareIndicative;
			this.ConfigurationUpdated(true);
		}

		// Token: 0x060061FB RID: 25083
		public abstract BasePronounProvider Clone();

		// Token: 0x060061FC RID: 25084 RVA: 0x001E0A18 File Offset: 0x001DEC18
		public virtual string ExpectedPossessiveAdjective()
		{
			return this.Objective;
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x001E0A20 File Offset: 0x001DEC20
		public virtual string ExpectedSubstantivePossessive()
		{
			if (!this.PossessiveAdjective.EndsWith("s") && !this.PossessiveAdjective.EndsWith("z"))
			{
				return this.PossessiveAdjective + "s";
			}
			return this.PossessiveAdjective;
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x001E0A5D File Offset: 0x001DEC5D
		public virtual string ExpectedReflexive()
		{
			return this.Objective + (this.Plural ? "selves" : "self");
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x001E0A80 File Offset: 0x001DEC80
		public virtual void Save(SerializationWriter Writer)
		{
			Writer.Write(this._Generic);
			Writer.Write(this._Generated);
			Writer.Write(this._Plural);
			Writer.Write(this._PseudoPlural);
			Writer.Write(this._Subjective);
			Writer.Write(this._Objective);
			Writer.Write(this._PossessiveAdjective);
			Writer.Write(this._SubstantivePossessive);
			Writer.Write(this._Reflexive);
			Writer.Write(this._PersonTerm);
			Writer.Write(this._ImmaturePersonTerm);
			Writer.Write(this._FormalAddressTerm);
			Writer.Write(this._OffspringTerm);
			Writer.Write(this._SiblingTerm);
			Writer.Write(this._ParentTerm);
			Writer.Write(this._UseBareIndicative);
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x001E0B50 File Offset: 0x001DED50
		public static void Load(SerializationReader Reader, BasePronounProvider Obj)
		{
			Obj._Generic = Reader.ReadBoolean();
			Obj._Generated = Reader.ReadBoolean();
			Obj._Plural = Reader.ReadBoolean();
			Obj._PseudoPlural = Reader.ReadBoolean();
			Obj._Subjective = Reader.ReadString();
			Obj._CapitalizedSubjective = null;
			Obj._Objective = Reader.ReadString();
			Obj._CapitalizedObjective = null;
			Obj._PossessiveAdjective = Reader.ReadString();
			Obj._CapitalizedPossessiveAdjective = null;
			Obj._SubstantivePossessive = Reader.ReadString();
			Obj._CapitalizedSubstantivePossessive = null;
			Obj._Reflexive = Reader.ReadString();
			Obj._CapitalizedReflexive = null;
			Obj._PersonTerm = Reader.ReadString();
			Obj._CapitalizedPersonTerm = null;
			Obj._ImmaturePersonTerm = Reader.ReadString();
			Obj._CapitalizedImmaturePersonTerm = null;
			Obj._FormalAddressTerm = Reader.ReadString();
			Obj._CapitalizedFormalAddressTerm = null;
			Obj._OffspringTerm = Reader.ReadString();
			Obj._CapitalizedOffspringTerm = null;
			Obj._SiblingTerm = Reader.ReadString();
			Obj._CapitalizedSiblingTerm = null;
			Obj._ParentTerm = Reader.ReadString();
			Obj._CapitalizedParentTerm = null;
			Obj._UseBareIndicative = Reader.ReadBoolean();
		}

		// Token: 0x06006201 RID: 25089
		public abstract Task<bool> CustomizeAsync();

		// Token: 0x06006202 RID: 25090 RVA: 0x001E0C6C File Offset: 0x001DEE6C
		protected async Task<bool> Customize(string What)
		{
			BasePronounProvider Temp = this.Clone();
			TaskAwaiter<bool> taskAwaiter = this.CustomizeProcess(What).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			bool flag;
			if (taskAwaiter.GetResult())
			{
				flag = true;
			}
			else
			{
				this.CopyFrom(Temp);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x001E0CB8 File Offset: 0x001DEEB8
		protected virtual async Task<bool> CustomizeProcess(string What)
		{
			this._Generic = false;
			this._Generated = false;
			DialogResult dialogResult = await Popup.ShowYesNoCancelAsync("Should your " + What + " be treated as fully plural, with you being addressed as a multiple subject in all circumstances?");
			bool flag;
			if (dialogResult == DialogResult.Cancel)
			{
				flag = false;
			}
			else
			{
				this._Plural = dialogResult == DialogResult.Yes;
				if (this._Plural)
				{
					this._PseudoPlural = false;
				}
				else
				{
					dialogResult = await Popup.ShowYesNoCancelAsync("Should your " + What + " be treated as conditionally plural, with you being addressed as a multiple subject only following a pronoun, as with with singular \"they\"?");
					this._PseudoPlural = dialogResult == DialogResult.Yes;
				}
				this._Subjective = await Popup.AskStringAsync("What subjective pronoun (he, she, they, etc.) should be used for this " + What + "?", this.Subjective, 6, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
				if (string.IsNullOrEmpty(this._Subjective))
				{
					flag = false;
				}
				else
				{
					this._Objective = await Popup.AskStringAsync("What objective pronoun (him, her, them, etc.) should be used for this " + What + "?", this.Objective, 6, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
					if (string.IsNullOrEmpty(this._Objective))
					{
						flag = false;
					}
					else
					{
						this._PossessiveAdjective = await Popup.AskStringAsync("What possessive adjective (his, her, their, etc.) should be used for this " + What + "?", this.PossessiveAdjective, 6, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
						if (string.IsNullOrEmpty(this._PossessiveAdjective))
						{
							flag = false;
						}
						else
						{
							this._SubstantivePossessive = null;
							this._SubstantivePossessive = await Popup.AskStringAsync("What substantive possessive (his, hers, theirs, etc.) should be used for this " + What + "?", this.SubstantivePossessive, 6, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
							if (string.IsNullOrEmpty(this._SubstantivePossessive))
							{
								flag = false;
							}
							else
							{
								this._Reflexive = null;
								this._Reflexive = await Popup.AskStringAsync("What reflexive pronoun (himself, herself, themself, themselves, etc.) should be used for this " + What + "?", this.Reflexive, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
								if (string.IsNullOrEmpty(this._Reflexive))
								{
									flag = false;
								}
								else
								{
									this._PersonTerm = await Popup.AskStringAsync("What term should be used for a mature person of this " + What + "? (Man, woman, person, etc.)", this.PersonTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
									if (string.IsNullOrEmpty(this._PersonTerm))
									{
										flag = false;
									}
									else
									{
										this._ImmaturePersonTerm = await Popup.AskStringAsync("What term should be used for an immature person of this " + What + "? (Boy, girl, child, etc.)", this.ImmaturePersonTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
										if (string.IsNullOrEmpty(this._ImmaturePersonTerm))
										{
											flag = false;
										}
										else
										{
											this._FormalAddressTerm = await Popup.AskStringAsync("What term should be used to formally address a person of this " + What + "? (Sir, madam, friend, etc.)", this.FormalAddressTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
											if (string.IsNullOrEmpty(this._FormalAddressTerm))
											{
												flag = false;
											}
											else
											{
												this._OffspringTerm = await Popup.AskStringAsync("What term should be used to address a person of this " + What + " as an offspring? (Son, daughter, child, etc.)", this.OffspringTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
												if (string.IsNullOrEmpty(this._OffspringTerm))
												{
													flag = false;
												}
												else
												{
													this._SiblingTerm = await Popup.AskStringAsync("What term should be used to address a person of this " + What + " as a sibling? (Brother, sister, sib, etc.)", this.SiblingTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
													if (string.IsNullOrEmpty(this._SiblingTerm))
													{
														flag = false;
													}
													else
													{
														this._ParentTerm = await Popup.AskStringAsync("What term should be used to address a person of this " + What + " as a parent? (Father, mother, progenitor, etc.)", this.ParentTerm, 12, 0, "abcdefghijklmnopqrstuvwxyz", false, true, null, false, null);
														if (string.IsNullOrEmpty(this._ParentTerm))
														{
															flag = false;
														}
														else
														{
															dialogResult = await Popup.ShowYesNoCancelAsync(string.Concat(new string[]
															{
																"Is an entity with this ", What, " treated grammatically as a person, such that it would be improper to say \"look at ", this.IndicativeDistal, "\" in reference to ", this.Objective, " -- one would say \"look at ", this.IndicativeDistal, " ", this.PersonTerm,
																"\" or \"look at ", this.Objective, "\" instead?"
															}));
															if (dialogResult == DialogResult.Cancel)
															{
																flag = false;
															}
															else
															{
																this._UseBareIndicative = dialogResult > DialogResult.Yes;
																flag = true;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04002E02 RID: 11778
		public bool _Generic;

		// Token: 0x04002E03 RID: 11779
		public bool _Generated;

		// Token: 0x04002E04 RID: 11780
		public bool _Plural;

		// Token: 0x04002E05 RID: 11781
		public bool _PseudoPlural;

		// Token: 0x04002E06 RID: 11782
		public string _Subjective = "they";

		// Token: 0x04002E07 RID: 11783
		[NonSerialized]
		public string _CapitalizedSubjective = "They";

		// Token: 0x04002E08 RID: 11784
		public string _Objective = "them";

		// Token: 0x04002E09 RID: 11785
		[NonSerialized]
		public string _CapitalizedObjective = "Them";

		// Token: 0x04002E0A RID: 11786
		public string _PossessiveAdjective = "their";

		// Token: 0x04002E0B RID: 11787
		[NonSerialized]
		public string _CapitalizedPossessiveAdjective = "Their";

		// Token: 0x04002E0C RID: 11788
		public string _SubstantivePossessive;

		// Token: 0x04002E0D RID: 11789
		[NonSerialized]
		public string _CapitalizedSubstantivePossessive;

		// Token: 0x04002E0E RID: 11790
		public string _Reflexive;

		// Token: 0x04002E0F RID: 11791
		[NonSerialized]
		public string _CapitalizedReflexive;

		// Token: 0x04002E10 RID: 11792
		public string _PersonTerm = "human";

		// Token: 0x04002E11 RID: 11793
		[NonSerialized]
		public string _CapitalizedPersonTerm = "Human";

		// Token: 0x04002E12 RID: 11794
		public string _ImmaturePersonTerm = "child";

		// Token: 0x04002E13 RID: 11795
		[NonSerialized]
		public string _CapitalizedImmaturePersonTerm = "Child";

		// Token: 0x04002E14 RID: 11796
		public string _FormalAddressTerm = "friend";

		// Token: 0x04002E15 RID: 11797
		[NonSerialized]
		public string _CapitalizedFormalAddressTerm;

		// Token: 0x04002E16 RID: 11798
		public string _OffspringTerm = "child";

		// Token: 0x04002E17 RID: 11799
		[NonSerialized]
		public string _CapitalizedOffspringTerm;

		// Token: 0x04002E18 RID: 11800
		public string _SiblingTerm = "sib";

		// Token: 0x04002E19 RID: 11801
		[NonSerialized]
		public string _CapitalizedSiblingTerm;

		// Token: 0x04002E1A RID: 11802
		public string _ParentTerm = "progenitor";

		// Token: 0x04002E1B RID: 11803
		[NonSerialized]
		public string _CapitalizedParentTerm;

		// Token: 0x04002E1C RID: 11804
		public bool _UseBareIndicative;
	}
}
