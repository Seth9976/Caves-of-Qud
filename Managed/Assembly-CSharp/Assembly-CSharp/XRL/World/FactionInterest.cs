using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using ConsoleLib.Console;
using Occult.Engine.CodeGeneration;
using Qud.API;
using XRL.Language;

namespace XRL.World
{
	// Token: 0x0200084F RID: 2127
	[GenerateSerializationPartial]
	[Serializable]
	public class FactionInterest : IComposite
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x00194EF6 File Offset: 0x001930F6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x00194EFC File Offset: 0x001930FC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this._Tags);
			Writer.Write(this._TagList);
			Writer.Write(this.WillBuy);
			Writer.Write(this.WillSell);
			Writer.Write(this.MatchAny);
			Writer.Write(this.Inverse);
			Writer.WriteOptimized(this.Description);
			Writer.WriteOptimized(this.Weight);
			Writer.WriteOptimized(this.SourceFileName);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x00194F78 File Offset: 0x00193178
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this._Tags = Reader.ReadOptimizedString();
			this._TagList = Reader.ReadList<string>();
			this.WillBuy = Reader.ReadBoolean();
			this.WillSell = Reader.ReadBoolean();
			this.MatchAny = Reader.ReadBoolean();
			this.Inverse = Reader.ReadBoolean();
			this.Description = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.SourceFileName = Reader.ReadOptimizedString();
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060054C6 RID: 21702 RVA: 0x00194FF1 File Offset: 0x001931F1
		// (set) Token: 0x060054C7 RID: 21703 RVA: 0x00194FF9 File Offset: 0x001931F9
		public string Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this._TagList = null;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060054C8 RID: 21704 RVA: 0x00195009 File Offset: 0x00193209
		// (set) Token: 0x060054C9 RID: 21705 RVA: 0x00195047 File Offset: 0x00193247
		public List<string> TagList
		{
			get
			{
				if (this._TagList == null)
				{
					if (this._Tags != null)
					{
						this._TagList = new List<string>(this._Tags.Split(',', StringSplitOptions.None));
					}
					else
					{
						this._TagList = new List<string>();
					}
				}
				return this._TagList;
			}
			set
			{
				this.Tags = string.Join(",", value.ToArray());
				this._TagList = value;
			}
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00195068 File Offset: 0x00193268
		public bool SameAs(FactionInterest o)
		{
			return !(o._Tags != this._Tags) && o.WillBuy == this.WillBuy && o.WillSell == this.WillSell && o.MatchAny == this.MatchAny && o.Inverse == this.Inverse && !(o.Description != this.Description);
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x001950E0 File Offset: 0x001932E0
		public string DebugName
		{
			get
			{
				string text = this.Tags;
				if (this.MatchAny)
				{
					text = text.Replace(",", "/");
				}
				else
				{
					text = text.Replace(",", "+");
				}
				if (this.Inverse)
				{
					text = "!" + text;
				}
				text += (this.WillBuy ? "+B" : "-B");
				text += (this.WillSell ? "+S" : "-S");
				if (!string.IsNullOrEmpty(this.SourceFileName))
				{
					text = string.Concat(new string[]
					{
						text,
						"(",
						this.SourceFileName,
						":",
						this.SourceLineNumber.ToString(),
						this.SourceWasMod ? "[mod]" : "",
						")"
					});
				}
				return text;
			}
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x001951CE File Offset: 0x001933CE
		public string GetDescription(Faction faction)
		{
			if (this.DescriptionCache == null)
			{
				this.DescriptionCache = (string.IsNullOrEmpty(this.Description) ? this.GenerateDescription(faction) : this.Description);
			}
			return this.DescriptionCache;
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00195200 File Offset: 0x00193400
		public bool HasDescriptionOverride()
		{
			return !string.IsNullOrEmpty(this.Description);
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00195210 File Offset: 0x00193410
		public string GenerateDescription(Faction faction)
		{
			if (this.TagList.Count == 1)
			{
				string tags = this.Tags;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(tags);
				if (num <= 2415111481U)
				{
					if (num <= 1456392516U)
					{
						if (num <= 789356349U)
						{
							if (num <= 509710637U)
							{
								if (num != 439160533U)
								{
									if (num == 509710637U)
									{
										if (tags == "sultanTombPropaganda")
										{
											return "sultan tomb inscriptions";
										}
									}
								}
								else if (tags == "merchant")
								{
									if (!(faction.Name == "Merchants"))
									{
										return "the locations of merchants";
									}
									return "the locations of other merchants";
								}
							}
							else if (num != 595253191U)
							{
								if (num == 789356349U)
								{
									if (tags == "*")
									{
										return "everything";
									}
								}
							}
							else if (tags == "bananagrove")
							{
								return "locations in the banana grove";
							}
						}
						else if (num <= 996239994U)
						{
							if (num != 891568605U)
							{
								if (num == 996239994U)
								{
									if (tags == "mountainstream")
									{
										return "locations along mountain streams";
									}
								}
							}
							else if (tags == "lakehinnom")
							{
								return "locations around Lake Hinnom";
							}
						}
						else if (num != 1393363718U)
						{
							if (num != 1427856550U)
							{
								if (num == 1456392516U)
								{
									if (tags == "consortium")
									{
										return "the locations of Consortium merchants";
									}
								}
							}
							else if (tags == "girsh")
							{
								return "the locations of Girsh lairs";
							}
						}
						else if (tags == "Ptoh")
						{
							return "the darkling star";
						}
					}
					else if (num <= 1826555366U)
					{
						if (num <= 1528037655U)
						{
							if (num != 1523042100U)
							{
								if (num == 1528037655U)
								{
									if (tags == "rebekah")
									{
										return "Resheph's healer Rebekah";
									}
								}
							}
							else if (tags == "saltdunes")
							{
								return "locations in the salt dunes";
							}
						}
						else if (num != 1826029124U)
						{
							if (num == 1826555366U)
							{
								if (tags == "fungal")
								{
									return "the locations of fungus forests";
								}
							}
						}
						else if (tags == "ruins")
						{
							return "the locations of ruins";
						}
					}
					else if (num <= 2058893219U)
					{
						if (num != 1870312031U)
						{
							if (num == 2058893219U)
							{
								if (tags == "flowerfields")
								{
									return "locations in the flower fields";
								}
							}
						}
						else if (tags == "slimy")
						{
							return "the locations of slime bogs";
						}
					}
					else if (num != 2125174819U)
					{
						if (num != 2371564874U)
						{
							if (num == 2415111481U)
							{
								if (tags == "desertcanyons")
								{
									return "locations in the desert canyons";
								}
							}
						}
						else if (tags == "settlement")
						{
							return "the locations of all settlements";
						}
					}
					else if (tags == "artifact")
					{
						return "the locations of storied items";
					}
				}
				else if (num <= 3247737400U)
				{
					if (num <= 2841083834U)
					{
						if (num <= 2746840902U)
						{
							if (num != 2416767106U)
							{
								if (num == 2746840902U)
								{
									if (tags == "underground")
									{
										return "all underground locations";
									}
								}
							}
							else if (tags == "cybernetics")
							{
								return "the locations of becoming nooks";
							}
						}
						else if (num != 2751005041U)
						{
							if (num == 2841083834U)
							{
								if (tags == "rusty")
								{
									return "the locations of rust bogs";
								}
							}
						}
						else if (tags == "tech")
						{
							return "technology";
						}
					}
					else if (num <= 3090410921U)
					{
						if (num != 3036976910U)
						{
							if (num == 3090410921U)
							{
								if (tags == "moonstair")
								{
									return "locations in the Moon Stair";
								}
							}
						}
						else if (tags == "palladiumreef")
						{
							return "locations in the Palladium Reef";
						}
					}
					else if (num != 3123093164U)
					{
						if (num != 3235235352U)
						{
							if (num == 3247737400U)
							{
								if (tags == "book")
								{
									return "books";
								}
							}
						}
						else if (tags == "gossip")
						{
							return "all sorts of gossip";
						}
					}
					else if (tags == "saltmarsh")
					{
						return "locations in the salt marsh";
					}
				}
				else if (num <= 3890070996U)
				{
					if (num <= 3670449724U)
					{
						if (num != 3436686084U)
						{
							if (num == 3670449724U)
							{
								if (tags == "weep")
								{
									return "all weep locations";
								}
							}
						}
						else if (tags == "historic")
						{
							return "the locations of historic sites";
						}
					}
					else if (num != 3775004542U)
					{
						if (num != 3854471918U)
						{
							if (num == 3890070996U)
							{
								if (tags == "stopsvalinn")
								{
									return "the location of Stopsvalinn";
								}
							}
						}
						else if (tags == "sultan")
						{
							return "all sultans";
						}
					}
					else if (tags == "oddity")
					{
						return "the locations of oddities";
					}
				}
				else if (num <= 4042198052U)
				{
					if (num != 3967719375U)
					{
						if (num == 4042198052U)
						{
							if (tags == "jungle")
							{
								return "locations in the jungle";
							}
						}
					}
					else if (tags == "recipe")
					{
						return "cooking recipes";
					}
				}
				else if (num != 4118073805U)
				{
					if (num != 4243777795U)
					{
						if (num == 4249647454U)
						{
							if (tags == "dromad")
							{
								if (!(faction.Name == "Dromad"))
								{
									return "the locations of dromad merchants";
								}
								return "the locations of other dromad merchants";
							}
						}
					}
					else if (tags == "mountains")
					{
						return "locations in the mountains";
					}
				}
				else if (tags == "kindrish")
				{
					return "the location of the ancestral bracelet Kindrish";
				}
				if (this.Tags.StartsWith("river"))
				{
					return "locations along the River " + char.ToUpper(this.Tags[5]).ToString() + this.Tags.Substring(6);
				}
				if (this.Tags.EndsWith("duskwaters"))
				{
					string text = this.Tags.Replace("duskwaters", "");
					return "locations in the River " + char.ToUpper(text[0]).ToString() + text.Substring(1) + "'s duskwaters";
				}
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (gameObjectBlueprint.GetTag("LairAdjectives", "") == this.Tags)
					{
						string text2 = Grammar.MakePossessive(Grammar.Pluralize(ColorUtility.StripFormatting(gameObjectBlueprint.DisplayName())));
						string text3 = Grammar.Pluralize(gameObjectBlueprint.GetTag("LairName", "lair"));
						return "the locations of " + text2 + " " + text3;
					}
				}
				if (!this.Tags.StartsWith("gossip:"))
				{
					goto IL_082D;
				}
				string text4 = this.Tags.Substring(7);
				if (text4 == faction.Name)
				{
					return "gossip that's about them";
				}
				Faction ifExists = Factions.GetIfExists(text4);
				if (ifExists != null)
				{
					return "gossip that's about " + ifExists.GetFormattedName();
				}
				return "gossip that's about " + text4;
			}
			IL_082D:
			if (this.TagList.Count == 2 && this.TagList[1] == "lair")
			{
				if (this.TagList[0] == "templar")
				{
					return "the locations of Putus Templar enclaves";
				}
				if (this.TagList[0] == "oboroqoru")
				{
					return "Oboroqoru's lair";
				}
				if (faction.DisplayName.Contains(this.TagList[0]))
				{
					return "the locations of other " + Grammar.Pluralize(string.Join(" ", this.TagList.ToArray()));
				}
			}
			if (this.TagList.Count == 2 && this.TagList[1] == "settlement")
			{
				if (this.TagList[0] == "snapjaw")
				{
					if (faction.Name == "Snapjaws")
					{
						return "the locations of their forts";
					}
					return "the locations of snapjaw forts";
				}
				else
				{
					if (this.TagList[0] == "pig")
					{
						return "the locations of pig farms";
					}
					if (this.TagList[0] == "apple")
					{
						return "the locations of apple farms";
					}
				}
			}
			return "the locations of " + Grammar.Pluralize(string.Join(" ", this.TagList.ToArray()));
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00195BC0 File Offset: 0x00193DC0
		public bool AppliesTo(IBaseJournalEntry note)
		{
			if (this.Tags == "*")
			{
				return !this.Inverse;
			}
			if (this.MatchAny)
			{
				foreach (string text in this.TagList)
				{
					if (note.Has(text))
					{
						return !this.Inverse;
					}
				}
				return this.Inverse;
			}
			foreach (string text2 in this.TagList)
			{
				if (!note.Has(text2))
				{
					return this.Inverse;
				}
			}
			return !this.Inverse;
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00195CA4 File Offset: 0x00193EA4
		public bool AppliesTo(List<string> Parts)
		{
			if (this.Tags == "*")
			{
				return !this.Inverse;
			}
			if (this.MatchAny)
			{
				foreach (string text in this.TagList)
				{
					if (Parts.Contains(text))
					{
						return !this.Inverse;
					}
				}
				return this.Inverse;
			}
			foreach (string text2 in this.TagList)
			{
				if (!Parts.Contains(text2))
				{
					return this.Inverse;
				}
			}
			return !this.Inverse;
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x00195D88 File Offset: 0x00193F88
		public bool AppliesTo(string Spec)
		{
			return this.AppliesTo(Spec.CachedCommaExpansion());
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00195D96 File Offset: 0x00193F96
		public bool Includes(IBaseJournalEntry note, bool Buy, bool Sell)
		{
			return (!Buy || this.WillBuy) && (!Sell || this.WillSell) && this.AppliesTo(note);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00195DB9 File Offset: 0x00193FB9
		public bool Includes(List<string> Parts, bool Buy, bool Sell)
		{
			return (!Buy || this.WillBuy) && (!Sell || this.WillSell) && this.AppliesTo(Parts);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00195DDC File Offset: 0x00193FDC
		public bool Includes(string Spec, bool Buy, bool Sell)
		{
			return this.Includes(Spec.CachedCommaExpansion(), Buy, Sell);
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x00195DEC File Offset: 0x00193FEC
		public bool Excludes(IBaseJournalEntry note, bool Buy, bool Sell)
		{
			return (!Buy || !this.WillBuy) && (!Sell || !this.WillSell) && this.AppliesTo(note);
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x00195E0F File Offset: 0x0019400F
		public bool Excludes(List<string> Parts, bool Buy, bool Sell)
		{
			return (!Buy || !this.WillBuy) && (!Sell || !this.WillSell) && this.AppliesTo(Parts);
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x00195E32 File Offset: 0x00194032
		public bool Excludes(string Spec, bool Buy, bool Sell)
		{
			return this.Excludes(Spec.CachedCommaExpansion(), Buy, Sell);
		}

		// Token: 0x04002AAA RID: 10922
		public string _Tags;

		// Token: 0x04002AAB RID: 10923
		public List<string> _TagList;

		// Token: 0x04002AAC RID: 10924
		public bool WillBuy = true;

		// Token: 0x04002AAD RID: 10925
		public bool WillSell = true;

		// Token: 0x04002AAE RID: 10926
		public bool MatchAny;

		// Token: 0x04002AAF RID: 10927
		public bool Inverse;

		// Token: 0x04002AB0 RID: 10928
		public string Description;

		// Token: 0x04002AB1 RID: 10929
		public int Weight;

		// Token: 0x04002AB2 RID: 10930
		public string SourceFileName;

		// Token: 0x04002AB3 RID: 10931
		[NonSerialized]
		public int SourceLineNumber;

		// Token: 0x04002AB4 RID: 10932
		[NonSerialized]
		public bool SourceWasMod;

		// Token: 0x04002AB5 RID: 10933
		[NonSerialized]
		private string DescriptionCache;
	}
}
