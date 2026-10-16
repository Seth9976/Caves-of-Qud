using System;

namespace XRL.World
{
	// Token: 0x02000953 RID: 2387
	public interface IPronounProvider
	{
		/// The name of the category providing the pronouns: male, female, neuter, nonspecific, masculine, feminine, etc.
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06006263 RID: 25187
		string Name { get; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06006264 RID: 25188
		string CapitalizedName { get; }

		/// Whether the pronoun-providing category is generic to the world, as opposed to being specific to an entity, species,
		/// or culture.
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06006265 RID: 25189
		bool Generic { get; }

		/// Whether the pronoun-providing category is procedurally generated.
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06006266 RID: 25190
		bool Generated { get; }

		/// Whether the entity is plural.
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06006267 RID: 25191
		bool Plural { get; }

		/// Whether the entity is treated plural only when being referred to by pronoun.
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06006268 RID: 25192
		bool PseudoPlural { get; }

		/// Subjective-case personal pronoun: he, she, it, they, etc.
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06006269 RID: 25193
		string Subjective { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600626A RID: 25194
		string CapitalizedSubjective { get; }

		/// Objective-case personal pronoun: him, her, it, them, etc.
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600626B RID: 25195
		string Objective { get; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600626C RID: 25196
		string CapitalizedObjective { get; }

		/// Adjectival possessive pronoun: his, her, its, their, etc.
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600626D RID: 25197
		string PossessiveAdjective { get; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600626E RID: 25198
		string CapitalizedPossessiveAdjective { get; }

		/// Substantive possessive pronoun: his, hers, its, theirs, etc.
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600626F RID: 25199
		string SubstantivePossessive { get; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06006270 RID: 25200
		string CapitalizedSubstantivePossessive { get; }

		/// Reflexive personal pronoun: himself, herself, itself, themselves, etc.
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06006271 RID: 25201
		string Reflexive { get; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06006272 RID: 25202
		string CapitalizedReflexive { get; }

		/// The term for a mature person with the pronouns: man, woman, etc.
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06006273 RID: 25203
		string PersonTerm { get; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06006274 RID: 25204
		string CapitalizedPersonTerm { get; }

		/// The term for an immature person with the pronouns: boy, girl, etc.
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06006275 RID: 25205
		string ImmaturePersonTerm { get; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06006276 RID: 25206
		string CapitalizedImmaturePersonTerm { get; }

		/// Formal form of address: sir, madam, etc.
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06006277 RID: 25207
		string FormalAddressTerm { get; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06006278 RID: 25208
		string CapitalizedFormalAddressTerm { get; }

		/// Term for entity as offspring: son, daughter, etc.
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06006279 RID: 25209
		string OffspringTerm { get; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600627A RID: 25210
		string CapitalizedOffspringTerm { get; }

		/// Term for entity as sibling: brother, sister, etc.
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600627B RID: 25211
		string SiblingTerm { get; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600627C RID: 25212
		string CapitalizedSiblingTerm { get; }

		/// Term for entity as parent: father, mother, etc.
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600627D RID: 25213
		string ParentTerm { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600627E RID: 25214
		string CapitalizedParentTerm { get; }

		/// Proximal indicative pronoun: this, these.
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600627F RID: 25215
		string IndicativeProximal { get; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06006280 RID: 25216
		string CapitalizedIndicativeProximal { get; }

		/// Distal indicative pronoun: that, those.
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06006281 RID: 25217
		string IndicativeDistal { get; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06006282 RID: 25218
		string CapitalizedIndicativeDistal { get; }

		/// Whether it is acceptable to use a bare indicative pronoun, for example
		/// saying "look at that".  (This would be objectifying and insulting if
		/// referring to a person; if one isn't being intentionally hostile one
		/// would say either "look at him" or "look at that man" instead.)
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06006283 RID: 25219
		bool UseBareIndicative { get; }
	}
}
