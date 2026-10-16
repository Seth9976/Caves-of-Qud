using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using XRL.Core;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000974 RID: 2420
	[GameStateSingleton]
	[Serializable]
	public class KithAndKinGameState : IGameStateSingleton, IComposite
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06006413 RID: 25619 RVA: 0x001EDE11 File Offset: 0x001EC011
		public static KithAndKinGameState Instance
		{
			get
			{
				if (XRLCore.Core == null)
				{
					return null;
				}
				if (XRLCore.Core.Game == null)
				{
					return null;
				}
				return XRLCore.Core.Game.GetObjectGameState("KithAndKinGameState") as KithAndKinGameState;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x001EDE43 File Offset: 0x001EC043
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x001EDE46 File Offset: 0x001EC046
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteComposite<HindrenClueLook>(this.lookClues);
			Writer.WriteComposite<HindrenClueRumor>(this.rumors);
			Writer.Write(this.clueItems);
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x001EDE6C File Offset: 0x001EC06C
		public void Read(SerializationReader Reader)
		{
			this.lookClues = Reader.ReadCompositeList<HindrenClueLook>();
			this.rumors = Reader.ReadCompositeList<HindrenClueRumor>();
			this.clueItems = Reader.ReadStringList();
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x001EDE92 File Offset: 0x001EC092
		public List<JournalObservation> getKnownFreeClues()
		{
			return JournalAPI.Observations.FindAll((JournalObservation o) => o.Revealed && o.Has("hindrenclue") && o.Has("free"));
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x001EDEBD File Offset: 0x001EC0BD
		public List<JournalObservation> getKnownMotiveClues()
		{
			return JournalAPI.Observations.FindAll(delegate(JournalObservation o)
			{
				if (o.Revealed && o.Has("hindrenclue"))
				{
					return o.Attributes.Any<string>((string s) => s.StartsWith("motive:"));
				}
				return false;
			});
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x001EDEE8 File Offset: 0x001EC0E8
		public List<string> getKnownPotentialCircumstances(string thief)
		{
			List<string> list = new List<string>();
			foreach (JournalObservation journalObservation in this.getKnownFreeClues())
			{
				list.Add(journalObservation.Text);
			}
			return list;
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x001EDF48 File Offset: 0x001EC148
		public List<string> getKnownPotentialThieves()
		{
			List<JournalObservation> list = JournalAPI.Observations.FindAll(delegate(JournalObservation o)
			{
				if (o.Revealed && o.Has("hindrenclue"))
				{
					return o.Attributes.Any<string>((string s) => s.StartsWith("motive:"));
				}
				return false;
			});
			List<string> list2 = new List<string>();
			foreach (JournalObservation journalObservation in list)
			{
				foreach (string text in journalObservation.Attributes)
				{
					if (text.StartsWith("motive:"))
					{
						string text2 = text.Split(':', StringSplitOptions.None)[1];
						if (!list2.Contains(text2))
						{
							list2.Add(text2 + " [" + journalObservation.Text + "]");
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x001EE040 File Offset: 0x001EC240
		public void foundClue()
		{
			XRLCore.Core.Game.SetIntGameState("FoundClue", 1);
			if (!XRLCore.Core.Game.HasGameState("FoundAccusationClueSet") && this.getKnownFreeClues().Count > 0 && this.getKnownMotiveClues().Count > 0)
			{
				XRLCore.Core.Game.SetIntGameState("FoundAccusationClueSet", 1);
			}
			if (XRLCore.Core.Game.HasQuest("Kith and Kin"))
			{
				if (!JournalAPI.Observations.Any<JournalObservation>((JournalObservation o) => !o.Revealed && o.Has("hindrenclue")))
				{
					XRLCore.Core.Game.FinishQuestStep("Kith and Kin", "Discover all the evidence", -1, true, null);
				}
			}
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x001EE108 File Offset: 0x001EC308
		public HindrenClueRumor getRumorForVillagerCategory(string villagerCategory)
		{
			if (!this.rumors.Any<HindrenClueRumor>((HindrenClueRumor r) => r.villagerCategory == villagerCategory))
			{
				return null;
			}
			List<HindrenClueRumor> list = this.rumors.Where<HindrenClueRumor>((HindrenClueRumor r) => r.villagerCategory == villagerCategory).ToList<HindrenClueRumor>();
			if (list.Count == 0)
			{
				return null;
			}
			int num = Stat.Random(0, list.Count - 1);
			HindrenClueRumor hindrenClueRumor = list[num];
			this.rumors.Remove(hindrenClueRumor);
			return hindrenClueRumor;
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x001EE18C File Offset: 0x001EC38C
		public void initRumorClue(string journaltext, string conversationText, string villagerCategory, string secret_id, string influence, string evidenceType)
		{
			JournalAPI.AddObservation(journaltext, secret_id, KithAndKinGameState.HINT_CATEGORY, secret_id, new string[]
			{
				"hindrenclue",
				influence,
				evidenceType,
				"influence:" + influence
			}, false, -1L, null, false, false);
			HindrenClueRumor hindrenClueRumor = new HindrenClueRumor();
			hindrenClueRumor.text = conversationText;
			hindrenClueRumor.secret = secret_id;
			hindrenClueRumor.villagerCategory = villagerCategory;
			this.rumors.Add(hindrenClueRumor);
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x001EE200 File Offset: 0x001EC400
		public void initLookClue(string text, string revealText, string lookText, string blueprint, string influence, string evidenceType)
		{
			JournalAPI.AddObservation(text, blueprint, KithAndKinGameState.HINT_CATEGORY, blueprint, new string[]
			{
				"hindrenclue",
				influence,
				evidenceType,
				"influence:" + influence
			}, false, -1L, revealText, false, false);
			HindrenClueLook hindrenClueLook = new HindrenClueLook(blueprint, lookText);
			this.lookClues.Add(hindrenClueLook);
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x001EE260 File Offset: 0x001EC460
		public void initItemClue(string text, string revealText, string blueprint, string influence, string evidenceType)
		{
			JournalAPI.AddObservation(text, blueprint, KithAndKinGameState.HINT_CATEGORY, blueprint, new string[]
			{
				"hindrenclue",
				influence,
				evidenceType,
				"influence:" + influence
			}, false, -1L, revealText, false, false);
			this.clueItems.Add(blueprint);
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x001EE2B3 File Offset: 0x001EC4B3
		public Zone getBeyLahZone()
		{
			return ZoneManager.instance.GetZone(XRLCore.Core.Game.GetStringGameState("BeyLahZoneID", ""));
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x001EE2D8 File Offset: 0x001EC4D8
		public void Initialize()
		{
			List<string> potentialRumorHavers = new List<string> { "*villager", "*faundren", "*scout" };
			List<Action> list = new List<Action>
			{
				delegate
				{
					this.initItemClue("I found a bloody patch of fur near the village paths, a sign of recent violence.", "Is that a patch of hindren fur? There's blood on it...  ", "Clue_BloodyPatchOfFur", "violence", "free");
				},
				delegate
				{
					this.initItemClue("I discovered a patch of watervine stained red with blood, a sign of recent violence.", "How odd, this watervine is stained red with blood.  ", "Clue_BloodyWatervine", "violence", "free");
				},
				delegate
				{
					this.initItemClue("I found a weapon haft, broken from use, on the paths, a sign of recent violence.", "How odd, the broken haft of a weapon, a sign of recent violence.  ", "Clue_SplinteredWeaponHaft", "violence", "free");
				},
				delegate
				{
					this.initItemClue("Someone left a sealed merchant's waterskin on the paths, a sign of recent trade.", "Hmm, this waterskin is still sealed with wax.  ", "Clue_BulgingWaterskin", "trade", "free");
				},
				delegate
				{
					this.initItemClue("Someone dropped a copper nugget on the paths, a sign of recent trade.", "How odd, there's a copper nugget just lying on the ground.  ", "Clue_CopperNugget", "trade", "free");
				},
				delegate
				{
					this.initItemClue("I discovered a footprint from a great saltback on the paths, a sign of recent trade.", "Now that's a huge footprint.  ", "Clue_GreatSaltbackPrint", "trade", "free");
				},
				delegate
				{
					this.initItemClue("I found a detached tongue on the ground, rotted with infection: a sign of recent illness.", "Oh gross, there's a rotten tongue on the ground!  ", "Clue_SeveredTongue", "illness", "free");
				},
				delegate
				{
					this.initItemClue("I found the discarded husks of the curative yuckwheat plant, a sign of recent illness.", "How odd, some discarded chaff from a yuckwheat plant.  ", "Clue_YuckwheatChaff", "illness", "free");
				},
				delegate
				{
					this.initItemClue("I found a few scraps from some kind of leatherworking project, a sign of recent craft.", "How odd, someone dropped some scraps of cut leather here.  ", "Clue_LeatherScraps", "craft", "free");
				},
				delegate
				{
					this.initItemClue("Someone discarded a worn but still functional leather bracer on the paths, a sign of recent craft.", "This leather bracer has seen some use. Why is it out here?  ", "Clue_DiscardedBracer", "craft", "free");
				},
				delegate
				{
					this.initItemClue("I found a small leatherworking hammer on the paths, a sign of recent craft.", "Someone has left a small hammer on the path.  ", "Clue_LeatherworkingHammer", "craft", "free");
				},
				delegate
				{
					this.initRumorClue("One villager was frightened by sounds of fighting at night, indicating recent violence.", "I heard the sounds of fighting the other night! I stayed in the next day, so afeared was I.", "*villager", "Clue_HeardFighting", "violence", "free");
				},
				delegate
				{
					this.initRumorClue("One of Bey Lah's scouts heard a fight in the watervine paddies, indicating recent violence.", "I thought I heard splashing from a fight in the marsh grids, but I couldn't find anyone.", "*scout", "Clue_HeardLoudSplashing", "violence", "free");
				},
				delegate
				{
					this.initRumorClue("Faundren saw a merchant from outside the village sneaking around, indicating recent trade.", "I saw a kendren merchant the other day! They were being very sneaky, so I think they were meeting someone in secret! Isn't that interesting?", "*faundren", "Clue_SawATrader", "trade", "free");
				},
				delegate
				{
					this.initRumorClue("The faundren heard the voice of a hindren and a kendren discussing goods late at night, indicating recent trade.", "I heard a hindren talking about buying and selling to a kendren late at night! I thought that all kendren had to report to Grand-Doe?", "*faundren", "Clue_TalkingLateAtNight", "trade", "free");
				},
				delegate
				{
					this.initRumorClue("The villagers are on edge due to rumors of sore throats in the village, indicating recent illness.", "I heard that someone in the village might have a sore throat, so I am staying at least ten handspan away from everyone today.", "*villager", "Clue_SoreThroats", "illness", "free");
				},
				delegate
				{
					this.initRumorClue("The faundren are complaining about the smell of yuckwheat tea, indicating recent illness.", "I smelled yuckwheat tea the other day! That smell is so terrible! Is someone sick?", "*faundren", "Clue_YuckwheatStench", "illness", "free");
				},
				delegate
				{
					this.initRumorClue("The scouts report that someone has taken leather from the village stores, indicating recent craft.", "I went to do some repairs to my boots, but someone's been in our stores of boar leather! We have far less than we should.", "*scout", "Clue_LeatherGoneMissing", "craft", "free");
				},
				delegate
				{
					this.initRumorClue("The scouts heard someone doing work with tools late at night, indicating recent craft.", "I heard someone doing some kind of toolwork during a night watch shift. I didn't interrupt, but it seemed peculiar.", "*scout", "Clue_SoundOfToolsAtNight", "craft", "free");
				}
			};
			List<Action> list2 = new List<Action>
			{
				delegate
				{
					this.initItemClue("I found an empty injector near the Hindriarch's bedroll. Why doesn't anyone know she was sick?", "Hmm. This injector has been used. ", "Clue_EmptyInjector", "illness", "motive:keh");
				},
				delegate
				{
					this.initItemClue("I found a bloody arrow on the paths of Bey Lah. Its fletching resembles the ones in Hindriarch Keh's quiver.", "Why, this arrow is covered with blood!  ", "Clue_BloodyArrow", "violence", "motive:keh");
				},
				delegate
				{
					this.initRumorClue("Apparently, the Hindriarch isolated herself to work on a mystery project, possibly to hide Kindrish.", "You know, Grand-Doe spent some late nights working on something she wouldn't talk about. I wonder what that was.", potentialRumorHavers.RemoveRandomElement<string>(null), Guid.NewGuid().ToString(), "craft", "motive:keh");
				},
				delegate
				{
					this.initRumorClue("Rumor has it that the village is flush with water, perhaps because Keh made a big trade.", "By the Eaters, how prosperous we've been. I've never seen the village so flush with fresh water.", "*villager", Guid.NewGuid().ToString(), "trade", "motive:keh");
				}
			};
			List<Action> list3 = new List<Action>
			{
				delegate
				{
					this.initRumorClue("The faundren claim that Eskhind left the village sporting a poorly-made leather bracer, possibly to conceal Kindrish.", "Just before auntie Eskhind left the village, I saw her wearing a new bracer. It looked terrible!", "*faundren", Guid.NewGuid().ToString(), "craft", "motive:esk");
				},
				delegate
				{
					this.initRumorClue("The faundren tell me that Eskhind's sister had a period of complete silence; she may have been ill.", "You know auntie Eskhind's little sister, Liihart? She's always been quiet, but she didn't talk at all for, uh, two whole weeks before they left!", "*faundren", Guid.NewGuid().ToString(), "illness", "motive:esk");
				},
				delegate
				{
					this.initItemClue("I found the village stores of Bey Lah untouched; Eskhind must have traded something for her basics.", "Whoever stole Kindrish didn't steal from the village stores. These chests are simply packed with dried lah, vinewafers, and waterskins.", "Clue_VillageStores", "trade", "motive:esk");
				},
				delegate
				{
					this.initLookClue("Eskhind is showing signs of a leg injury. Perhaps she was in a fight. ", "Eskhind appears to be favoring a leg, though she has concealed any injury. ", "Eskhind is favoring a leg.", "Eskhind", "violence", "motive:esk");
				}
			};
			List<Action> list4 = new List<Action>
			{
				delegate
				{
					this.initRumorClue("The faundren tell me that Kesehind went quiet for a few weeks, only to become louder than ever again.", "For weeks and weeks, Kesehind was so quiet, but now he's talking more than ever! He's so loud!", "*faundren", Guid.NewGuid().ToString(), "illness", "motive:kese");
				},
				delegate
				{
					this.initRumorClue("Rumor has it that Kesehind has always worn hand-me-down armor, but he was recently seen with a brand new set.", "Did you see Kesehind's armor? He always used to wear hand-me-downs from the scouts, but what he's wearing now looks brand new!", potentialRumorHavers.RemoveRandomElement<string>(null), Guid.NewGuid().ToString(), "trade", "motive:kese");
				},
				delegate
				{
					this.initItemClue("I found a worn-out bracer abandoned near Kesehind's bedroll.", "That's strange. This bracer is heavily worn, but surprisingly clean.  ", "Clue_WornBracer", "craft", "motive:kese");
				},
				delegate
				{
					this.initLookClue("Kesehind tried to clean blood off his armor, but missed a spot. ", "Carmine streaks on Kesehind's armor suggest that he tried and failed to wipe away spilled blood. ", "Kesehind's armor has bloodstains that he failed to remove. ", "Kesehind", "violence", "motive:kese");
				}
			};
			List<Action> list5 = new List<Action>
			{
				delegate
				{
					this.initRumorClue("The Bey Lah scouts have had trouble with violent kendren. What if one slipped by?", "The kendren are becoming more brazen in their hostility. I've had to misdirect and fight them off several times this month alone.", "*scout", Guid.NewGuid().ToString(), "violence", "motive:kendren");
				},
				delegate
				{
					this.initRumorClue("The scouts have seen kendren treasure hunters about. That type will take anything not nailed down.", "I came across some kendren who claimed to be treasure hunters. They said that they would pay good water for unique artifacts or treasures.", "*scout", Guid.NewGuid().ToString(), "trade", "motive:kendren");
				},
				delegate
				{
					this.initRumorClue("Theft by kendren is very common during an outbreak of disease like the one the villagers describe.", "I've heard that there is an outbreak of glotrot in the kendren villages outside of Bey Lah. Now I want to leave here even less!", "*villager", Guid.NewGuid().ToString(), "illness", "motive:kendren");
				},
				delegate
				{
					this.initItemClue("I found a set of foreign leatherworking tools on the village paths. A kendren crafter has been here.", "This set of leatherworking tools is shockingly well-made.  ", "Clue_ForeignLeatherworkingTools", "craft", "motive:kendren");
				}
			};
			list.ShuffleInPlace<Action>(null);
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				list[i]();
			}
			list2.GetRandomElement<Action>(null)();
			list3.GetRandomElement<Action>(null)();
			list4.GetRandomElement<Action>(null)();
			list5.GetRandomElement<Action>(null)();
		}

		// Token: 0x04002EF9 RID: 12025
		public static string HINT_CATEGORY = "Kith and Kin";

		// Token: 0x04002EFA RID: 12026
		public List<HindrenClueLook> lookClues = new List<HindrenClueLook>();

		// Token: 0x04002EFB RID: 12027
		public List<HindrenClueRumor> rumors = new List<HindrenClueRumor>();

		// Token: 0x04002EFC RID: 12028
		public List<string> clueItems = new List<string>();
	}
}
