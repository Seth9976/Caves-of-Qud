using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Language;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BFF RID: 3071
	public abstract class IKithAndKinPart : IConversationPart
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06007B5C RID: 31580 RVA: 0x002E5934 File Offset: 0x002E3B34
		// (set) Token: 0x06007B5D RID: 31581 RVA: 0x002E5952 File Offset: 0x002E3B52
		protected List<JournalObservation> Circumstances
		{
			get
			{
				List<JournalObservation> list;
				if (!IConversationPart.TryGetState<List<JournalObservation>>("Circumstances", out list))
				{
					return null;
				}
				return list;
			}
			set
			{
				IConversationPart.State["Circumstances"] = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06007B5E RID: 31582 RVA: 0x002E5964 File Offset: 0x002E3B64
		// (set) Token: 0x06007B5F RID: 31583 RVA: 0x002E5982 File Offset: 0x002E3B82
		protected List<JournalObservation> Motives
		{
			get
			{
				List<JournalObservation> list;
				if (!IConversationPart.TryGetState<List<JournalObservation>>("Motives", out list))
				{
					return null;
				}
				return list;
			}
			set
			{
				IConversationPart.State["Motives"] = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06007B60 RID: 31584 RVA: 0x002E5994 File Offset: 0x002E3B94
		// (set) Token: 0x06007B61 RID: 31585 RVA: 0x002E59B2 File Offset: 0x002E3BB2
		protected JournalObservation Circumstance
		{
			get
			{
				JournalObservation journalObservation;
				if (!IConversationPart.TryGetState<JournalObservation>("Circumstance", out journalObservation))
				{
					return null;
				}
				return journalObservation;
			}
			set
			{
				IConversationPart.State["Circumstance"] = value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06007B62 RID: 31586 RVA: 0x002E59C4 File Offset: 0x002E3BC4
		// (set) Token: 0x06007B63 RID: 31587 RVA: 0x002E59E2 File Offset: 0x002E3BE2
		protected JournalObservation Motive
		{
			get
			{
				JournalObservation journalObservation;
				if (!IConversationPart.TryGetState<JournalObservation>("Motive", out journalObservation))
				{
					return null;
				}
				return journalObservation;
			}
			set
			{
				IConversationPart.State["Motive"] = value;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x002E59F4 File Offset: 0x002E3BF4
		protected List<string> Eliminated
		{
			get
			{
				if (this._Eliminated != null)
				{
					return this._Eliminated;
				}
				object obj;
				if (The.Game.ObjectGameState.TryGetValue("KithAndKinEliminated", out obj))
				{
					return this._Eliminated = (List<string>)obj;
				}
				this._Eliminated = new List<string>();
				The.Game.ObjectGameState["KithAndKinEliminated"] = this._Eliminated;
				return this._Eliminated;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06007B65 RID: 31589 RVA: 0x002E5A64 File Offset: 0x002E3C64
		protected string CircumstanceInfluence
		{
			get
			{
				string text;
				if (this.Circumstance == null || !this.Circumstance.TryGetAttribute("influence:", out text))
				{
					text = "trade";
				}
				return text;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x002E5A94 File Offset: 0x002E3C94
		protected string MotiveInfluence
		{
			get
			{
				string text;
				if (this.Motive == null || !this.Motive.TryGetAttribute("influence:", out text))
				{
					text = "craft";
				}
				return text;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06007B67 RID: 31591 RVA: 0x002E5AC4 File Offset: 0x002E3CC4
		protected string Thief
		{
			get
			{
				string text;
				if (this.Motive == null || !this.Motive.TryGetAttribute("motive:", out text))
				{
					text = "keh";
				}
				return text;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x002E5AF4 File Offset: 0x002E3CF4
		protected string ThiefName
		{
			get
			{
				string thief = this.Thief;
				string text;
				if (!(thief == "kendren"))
				{
					if (!(thief == "esk"))
					{
						if (!(thief == "kese"))
						{
							text = "Keh";
						}
						else
						{
							text = "Kesehind";
						}
					}
					else
					{
						text = "Eskhind";
					}
				}
				else
				{
					text = "a kendren";
				}
				return text;
			}
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x002E5B50 File Offset: 0x002E3D50
		public static string KeyOf(string Key)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Key);
			if (num <= 2864341632U)
			{
				if (num <= 1694669692U)
				{
					if (num != 1570347558U)
					{
						if (num == 1694669692U)
						{
							if (Key == "tumultuous")
							{
								return "Tumultuous";
							}
						}
					}
					else if (Key == "esk")
					{
						return "Esk";
					}
				}
				else if (num != 2031477551U)
				{
					if (num == 2864341632U)
					{
						if (Key == "kendren")
						{
							return "Kendren";
						}
					}
				}
				else if (Key == "keh")
				{
					return "Keh";
				}
			}
			else if (num <= 3576140497U)
			{
				if (num != 3298209185U)
				{
					if (num == 3576140497U)
					{
						if (Key == "love")
						{
							return "Love";
						}
					}
				}
				else if (Key == "prosperous")
				{
					return "Prosperous";
				}
			}
			else if (num != 3845391352U)
			{
				if (num != 3882058845U)
				{
					if (num == 3904113410U)
					{
						if (Key == "nolove")
						{
							return "NoLove";
						}
					}
				}
				else if (Key == "kese")
				{
					return "Kese";
				}
			}
			else if (Key == "mixed")
			{
				return "Mixed";
			}
			return Grammar.InitCap(Key);
		}

		// Token: 0x04003826 RID: 14374
		protected List<string> _Eliminated;
	}
}
