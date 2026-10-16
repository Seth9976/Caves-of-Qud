using System;

namespace XRL
{
	// Token: 0x020004B3 RID: 1203
	public static class Achievement
	{
		// Token: 0x04001A1B RID: 6683
		public static readonly AchievementInfo DIE = new AchievementInfo("ACH_DIE", "Welcome to Qud", "welcome.png", "Die.", true);

		// Token: 0x04001A1C RID: 6684
		public static readonly AchievementInfo VIOLATE_PAULI = new AchievementInfo("ACH_VIOLATE_PAULI", "The Laws of Physics Are Mere Suggestions, Vol. 1", "pauli1.png", "Violate the Pauli exclusion principle.", false);

		// Token: 0x04001A1D RID: 6685
		public static readonly AchievementInfo EAT_BEAR = new AchievementInfo("ACH_EAT_BEAR", "Eat an Entire Bear", "bear.png", "Kill a bear and eat it. Just eat an entire bear.", false);

		// Token: 0x04001A1E RID: 6686
		public static readonly AchievementInfo WEAR_OWN_FACE = new AchievementInfo("ACH_WEAR_OWN_FACE", "To Thine Own Self Be True", "face.png", "Wear your own severed face on your face.", false);

		// Token: 0x04001A1F RID: 6687
		public static readonly AchievementInfo DRINK_LAVA = new AchievementInfo("ACH_DRINK_LAVA", "On the Rocks", "lava.png", "Drink lava.", true);

		// Token: 0x04001A20 RID: 6688
		public static readonly AchievementInfo GET_FUNGAL_INFECTIONS = new AchievementInfo("ACH_GET_FUNGAL_INFECTIONS", "Friend to Fungi", "fungi.png", "Host three different fungal infections at once.", false);

		// Token: 0x04001A21 RID: 6689
		public static readonly AchievementInfo INHABIT_GOAT = new AchievementInfo("ACH_INHABIT_GOAT", "Goat Simulator", "goat.png", "Project your mind into a goat's body.", false);

		// Token: 0x04001A22 RID: 6690
		public static readonly AchievementInfo KILLED_BY_TWIN = new AchievementInfo("ACH_KILLED_BY_TWIN", "Your Better Half", "eviltwin.png", "Get killed by your evil twin.", true);

		// Token: 0x04001A23 RID: 6691
		public static readonly AchievementInfo FIND_ISNER = new AchievementInfo("ACH_FIND_ISNER", "The Spirit of Vengeance", "isner.png", "Recover the Ruin of House Isner.", false);

		// Token: 0x04001A24 RID: 6692
		public static readonly AchievementInfo FIND_STOPSVALINN = new AchievementInfo("ACH_FIND_STOPSVALINN", "Knight Conical", "stop.png", "Recover Stopsvalinn.", false);

		// Token: 0x04001A25 RID: 6693
		public static readonly AchievementInfo LOVE_SIGN = new AchievementInfo("ACH_LOVE_SIGN", "Love at First Sign", "sign.png", "Fall in love with a sign.", false);

		// Token: 0x04001A26 RID: 6694
		public static readonly AchievementInfo INSTALL_IMPLANT = new AchievementInfo("ACH_INSTALL_IMPLANT", "You Are Becoming", "becoming.png", "Install a cybernetic implant at a becoming nook.", true);

		// Token: 0x04001A27 RID: 6695
		public static readonly AchievementInfo INSTALL_IMPLANT_EVERY_SLOT = new AchievementInfo("ACH_INSTALL_IMPLANT_EVERY_SLOT", "You Became", "became.png", "Have a cybernetic implant installed on every implantable body part.", true);

		// Token: 0x04001A28 RID: 6696
		public static readonly AchievementInfo KILL_PYRAMID = new AchievementInfo("ACH_KILL_PYRAMID", "Pyramid Scheme", "pyramid.png", "Kill a chrome pyramid.", false);

		// Token: 0x04001A29 RID: 6697
		public static readonly AchievementInfo GET_GLOTROT = new AchievementInfo("ACH_GET_GLOTROT", "Say No More", "glotrot.png", "Contract glotrot.", true);

		// Token: 0x04001A2A RID: 6698
		public static readonly AchievementInfo GET_IRONSHANK = new AchievementInfo("ACH_GET_IRONSHANK", "Metal Pedal", "ironshank.png", "Contract ironshank.", true);

		// Token: 0x04001A2B RID: 6699
		public static readonly AchievementInfo GET_MONOCHROME = new AchievementInfo("ACH_GET_MONOCHROME", "All the Leaves are Grey", "monochrome.png", "Contract monochrome.", true);

		// Token: 0x04001A2C RID: 6700
		public static readonly AchievementInfo EAT_OWN_LIMB = new AchievementInfo("ACH_EAT_OWN_LIMB", "You Are What You Eat", "eat.png", "Eat one of your own limbs.", true);

		// Token: 0x04001A2D RID: 6701
		public static readonly AchievementInfo KILLED_BY_CHUTE_CRAB = new AchievementInfo("ACH_KILLED_BY_CHUTE_CRAB", "Shoot.", "chutecrab.png", "Get killed by a chute crab.", true);

		// Token: 0x04001A2E RID: 6702
		public static readonly AchievementInfo FORESEE_DEATH = new AchievementInfo("ACH_FORESEE_DEATH", "Dark Tidings", "tidings.png", "Foresee your own death.", false);

		// Token: 0x04001A2F RID: 6703
		public static readonly AchievementInfo LOVE_YOURSELF = new AchievementInfo("ACH_LOVE_YOURSELF", "Love Thyself", "thyself.png", "Fall in love with yourself.", false);

		// Token: 0x04001A30 RID: 6704
		public static readonly AchievementInfo LOVED_BY_FACTION = new AchievementInfo("ACH_LOVED_BY_FACTION", "A Bond Knit with Trust", "bond.png", "Become loved by a faction.", false);

		// Token: 0x04001A31 RID: 6705
		public static readonly AchievementInfo HATED_BY_JOPPA = new AchievementInfo("ACH_HATED_BY_JOPPA", "The Woe of Joppa", "joppa.png", "Become hated by the villagers of Joppa.", false);

		// Token: 0x04001A32 RID: 6706
		public static readonly AchievementInfo LOVED_BY_NEW_BEINGS = new AchievementInfo("ACH_LOVED_BY_NEW_BEINGS", "Peekaboo", "sentient.png", "Become loved by newly sentient beings.", false);

		// Token: 0x04001A33 RID: 6707
		public static readonly AchievementInfo CRUSHED_UNDER_SUNS = new AchievementInfo("ACH_CRUSHED_UNDER_SUNS", "Starry Demise", "suns.png", "Be crushed under the weight of a thousand suns.", false);

		// Token: 0x04001A34 RID: 6708
		public static readonly AchievementInfo SPEAK_ALCHEMIST = new AchievementInfo("ACH_SPEAK_ALCHEMIST", "Quiet This Metal", "alchemist.png", "Speak to the alchemist.", false);

		// Token: 0x04001A35 RID: 6709
		public static readonly AchievementInfo HAVE_10_MUTATIONS = new AchievementInfo("ACH_HAVE_10_MUTATIONS", "Proteus", "proteus.png", "Have 10 mutations.", false);

		// Token: 0x04001A36 RID: 6710
		public static readonly AchievementInfo READ_10_BOOKS = new AchievementInfo("ACH_READ_10_BOOKS", "Litteratus", "books.png", "Read 10 books.", "STAT_BOOKS_READ", 10, int.MaxValue, true);

		// Token: 0x04001A37 RID: 6711
		public static readonly AchievementInfo READ_100_BOOKS = new AchievementInfo("ACH_READ_100_BOOKS", "So Powerful is the Charm of Words", "books2.png", "Read 100 books.", "STAT_BOOKS_READ", 100, int.MaxValue, true);

		// Token: 0x04001A38 RID: 6712
		public static readonly AchievementInfo KILL_100_SNAPJAWS = new AchievementInfo("ACH_KILL_100_SNAPJAWS", "Jawsnapper", "snapjaw.png", "Kill 100 snapjaws.", "STAT_SNAPJAWS_KILLED", 100, int.MaxValue, false);

		// Token: 0x04001A39 RID: 6713
		public static readonly AchievementInfo WATER_RITUAL_100_TIMES = new AchievementInfo("ACH_WATER_RITUAL_50_TIMES", "Your Thirst Is Mine, My Water Is Yours", "water.png", "Perform the introductory water ritual 100 times.", "STAT_WATER_RITUALS_PERFORMED", 100, int.MaxValue, false);

		// Token: 0x04001A3A RID: 6714
		public static readonly AchievementInfo GET_MUTATION_LEVEL_15 = new AchievementInfo("ACH_GET_MUTATION_LEVEL_15", "Mutagenic Mastery", "anomaly.png", "Advance a mutation to level 15.", false);

		// Token: 0x04001A3B RID: 6715
		public static readonly AchievementInfo CURE_GLOTROT = new AchievementInfo("ACH_CURE_GLOTROT", "Tongue in Cheek", "glotrot2.png", "Cure glotrot via traditional methods.", true);

		// Token: 0x04001A3C RID: 6716
		public static readonly AchievementInfo CURE_IRONSHANK = new AchievementInfo("ACH_CURE_IRONSHANK", "Footloose", "ironshank2.png", "Cure ironshank via traditional methods.", true);

		// Token: 0x04001A3D RID: 6717
		public static readonly AchievementInfo CURE_MONOCHROME = new AchievementInfo("ACH_CURE_MONOCHROME", "Rainbow Reading", "monochrome2.png", "Cure monochrome via traditional methods.", true);

		// Token: 0x04001A3E RID: 6718
		public static readonly AchievementInfo LEARN_ONE_SULTAN_HISTORY = new AchievementInfo("ACH_LEARN_ONE_SULTAN_HISTORY", "Biographer", "sultan.png", "Learn the complete history of a single sultan.", false);

		// Token: 0x04001A3F RID: 6719
		public static readonly AchievementInfo VIOLATE_WATER_RITUAL = new AchievementInfo("ACH_VIOLATE_WATER_RITUAL", "Oathbreaker", "pariah.png", "Violate the sacred covenant of the water ritual.", true);

		// Token: 0x04001A40 RID: 6720
		public static readonly AchievementInfo ACTIVATE_TIMECUBE = new AchievementInfo("ACH_ACTIVATE_TIMECUBE", "Cubic and Wisest Human", "time.png", "Activate a time cube.", false);

		// Token: 0x04001A41 RID: 6721
		public static readonly AchievementInfo WIELD_PRISM = new AchievementInfo("ACH_WIELD_PRISM", "Go on. Do it.", "prism.png", "Wield the amaranthine prism.", false);

		// Token: 0x04001A42 RID: 6722
		public static readonly AchievementInfo WATER_RITUAL_OBOROQORU = new AchievementInfo("ACH_WATER_RITUAL_OBOROQORU", "In Contemplation of Eons", "oboroqoru.png", "Perform the water ritual with Oboroqoru, Ape God.", false);

		// Token: 0x04001A43 RID: 6723
		public static readonly AchievementInfo REGENERATE_LIMB = new AchievementInfo("ACH_REGENERATE_LIMB", "Synolymb", "limb.png", "Regenerate a limb.", true);

		// Token: 0x04001A44 RID: 6724
		public static readonly AchievementInfo DIE_BY_FALLING = new AchievementInfo("ACH_DIE_BY_FALLING", "Free Falling", "falling.png", "Fall to your death.", true);

		// Token: 0x04001A45 RID: 6725
		public static readonly AchievementInfo KILL_TRISLUDGE = new AchievementInfo("ACH_KILL_TRISLUDGE", "Three-Sludge Monte", "sludge_3.png", "Kill a trisludge.", false);

		// Token: 0x04001A46 RID: 6726
		public static readonly AchievementInfo KILL_PENTASLUDGE = new AchievementInfo("ACH_KILL_PENTASLUDGE", "Five-Sludge Monte", "sludge_5.png", "Kill a pentasludge.", false);

		// Token: 0x04001A47 RID: 6727
		public static readonly AchievementInfo KILL_DECASLUDGE = new AchievementInfo("ACH_KILL_DECASLUDGE", "Ten-Sludge Monte", "sludge_10.png", "Kill a decasludge.", false);

		// Token: 0x04001A48 RID: 6728
		public static readonly AchievementInfo GET_DECAPITATED = new AchievementInfo("ACH_GET_DECAPITATED", "Hole Like a Head", "head.png", "Get decapitated.", true);

		// Token: 0x04001A49 RID: 6729
		public static readonly AchievementInfo FIND_APPRENTICE = new AchievementInfo("ACH_FIND_APPRENTICE", "What With the Disembowelment and All", "apprentice.png", "Find one of Argyve's old apprentices.", false);

		// Token: 0x04001A4A RID: 6730
		public static readonly AchievementInfo FIND_GLOWPAD = new AchievementInfo("ACH_FIND_GLOWPAD", "Psst.", "glowpad.png", "Find the oddly-hued glowpad.", true);

		// Token: 0x04001A4B RID: 6731
		public static readonly AchievementInfo COMPLIMENT_QGIRL = new AchievementInfo("ACH_COMPLIMENT_QGIRL", "That Was Nice", "qgirl.png", "Compliment Q Girl.", true);

		// Token: 0x04001A4C RID: 6732
		public static readonly AchievementInfo WIELD_CAS_POL = new AchievementInfo("ACH_WIELD_CAS_POL", "Gemini", "swords.png", "Wield Caslainard and Polluxus.", false);

		// Token: 0x04001A4D RID: 6733
		public static readonly AchievementInfo DONATE_ITEM_200_REP = new AchievementInfo("ACH_DONATE_ITEM_200_REP", "Donation Level: Kasaphescence", "donation.png", "Make an offering at the Sacred Well of an artifact worth at least 200 reputation.", false);

		// Token: 0x04001A4E RID: 6734
		public static readonly AchievementInfo LEARN_JUMP = new AchievementInfo("ACH_LEARN_JUMP", "Leap, Frog.", "frog.png", "Convince a frog to teach you how to jump.", false);

		// Token: 0x04001A4F RID: 6735
		public static readonly AchievementInfo SIX_ARMS = new AchievementInfo("ACH_SIX_ARMS", "Six Arms None the Richer", "six_arms.png", "Have six arms.", false);

		// Token: 0x04001A50 RID: 6736
		public static readonly AchievementInfo VORTICES_ENTERED = new AchievementInfo("ACH_VORTICES_ENTERED", "Cosmo-Chrononaut", "vortex.png", "Enter 25 spacetime vortices.", "STAT_VORTICES_ENTERED", 25, int.MaxValue, true);

		// Token: 0x04001A51 RID: 6737
		public static readonly AchievementInfo RECRUIT_HIGH_PRIEST = new AchievementInfo("ACH_RECRUIT_HIGH_PRIEST", "Mechanimist Reformer", "priest.png", "Convince the High Priest of the Stilt to join your party.", false);

		// Token: 0x04001A52 RID: 6738
		public static readonly AchievementInfo OVERDOSE_HULKHONEY = new AchievementInfo("ACH_OVERDOSE_HULKHONEY", "Aaaaaaaaargh!", "rage.png", "Overdose on a hulk honey tonic.", true);

		// Token: 0x04001A53 RID: 6739
		public static readonly AchievementInfo WATER_RITUAL_MAMON = new AchievementInfo("ACH_WATER_RITUAL_MAMON", "Feast Upon the Goat Hearts! *cheers*", "mamon.png", "Perform the water ritual with Mamon Souldrinker.", false);

		// Token: 0x04001A54 RID: 6740
		public static readonly AchievementInfo EQUIP_FLUMEFLIER = new AchievementInfo("ACH_EQUIP_FLUMEFLIER", "Rocket Bear", "flume.png", "Equip the Flume-Flier of the Sky-Bear.", false);

		// Token: 0x04001A55 RID: 6741
		public static readonly AchievementInfo CLONE_CTESIPHUS = new AchievementInfo("ACH_CLONE_CTESIPHUS", "Two Cats Are Better Than One", "ctesiphus.png", "Clone Ctesiphus.", false);

		// Token: 0x04001A56 RID: 6742
		public static readonly AchievementInfo CLONES_10 = new AchievementInfo("ACH_10_CLONES", "Me, Myself, and I", "clones.png", "Share the map with 10 clones of yourself.", false);

		// Token: 0x04001A57 RID: 6743
		public static readonly AchievementInfo CLONES_30 = new AchievementInfo("ACH_30_CLONES", "Clonal Colony", "clones2.png", "Share the map with 30 clones of yourself.", false);

		// Token: 0x04001A58 RID: 6744
		public static readonly AchievementInfo KILL_OBOROQORU = new AchievementInfo("ACH_KILL_OBOROQORU", "The Woe of Apes", "woeofapes.png", "Kill Oboroqoru, Ape God.", false);

		// Token: 0x04001A59 RID: 6745
		public static readonly AchievementInfo KILL_AOYG = new AchievementInfo("ACH_KILL_AOYG", "Bricks Can Be Thrown, Too", "brick.png", "Kill Aoyg-no-Longer.", false);

		// Token: 0x04001A5A RID: 6746
		public static readonly AchievementInfo RECOVER_KINDRISH = new AchievementInfo("ACH_RECOVER_KINDRISH", "Close the Loop", "kindrish.png", "Recover Kindrish.", false);

		// Token: 0x04001A5B RID: 6747
		public static readonly AchievementInfo VILLAGES_100 = new AchievementInfo("ACH_100_VILLAGES", "Tourist", "tourist.png", "Visit 100 generated villages.", "STAT_VILLAGES_VISITED", 100, int.MaxValue, false);

		// Token: 0x04001A5C RID: 6748
		public static readonly AchievementInfo GLIMMER_20 = new AchievementInfo("ACH_20_GLIMMER", "Sight Older Than Sight", "glimmer.png", "Discover psychic glimmer.", true);

		// Token: 0x04001A5D RID: 6749
		public static readonly AchievementInfo GLIMMER_40 = new AchievementInfo("ACH_40_GLIMMER", "What Are Directions on a Space That Cannot Be Ordered?", "glimmer40.png", "Attain 40 psychic glimmer and discover the vast psychic aether.", true);

		// Token: 0x04001A5E RID: 6750
		public static readonly AchievementInfo GLIMMER_100 = new AchievementInfo("ACH_100_GLIMMER", "Star-Eye Esper", "glimmer100.png", "Attain 100 psychic glimmer.", true);

		// Token: 0x04001A5F RID: 6751
		public static readonly AchievementInfo GLIMMER_200 = new AchievementInfo("ACH_200_GLIMMER", "The Quasar Mind", "quasar.png", "Attain 200 psychic glimmer.", true);

		// Token: 0x04001A60 RID: 6752
		public static readonly AchievementInfo ABSORB_PSYCHE = new AchievementInfo("ACH_ABSORB_PSYCHE", "There Can Be Only One", "onlyone.png", "Encode the psionic bits of someone's psyche onto the holographic boundary of your own psyche.", false);

		// Token: 0x04001A61 RID: 6753
		public static readonly AchievementInfo TRUE_GLIMMER = new AchievementInfo("ACH_TRUE_GLIMMER", "Glimmer of Truth", "glimmertruth.png", "Attain 20 psychic glimmer as a true kin.", true);

		// Token: 0x04001A62 RID: 6754
		public static readonly AchievementInfo RECIPES_100 = new AchievementInfo("ACH_100_RECIPES", "Sultan of Salt", "recipes.png", "Invent 100 recipes.", "STAT_RECIPES_INVENTED", 100, int.MaxValue, false);

		// Token: 0x04001A63 RID: 6755
		public static readonly AchievementInfo COOKED_FLUX = new AchievementInfo("ACH_COOKED_FLUX", "Absolute Unit", "absoluteunit.png", "Successfully cook a meal with neutron flux as an ingredient.", false);

		// Token: 0x04001A64 RID: 6756
		public static readonly AchievementInfo COOKED_EXTRADIMENSIONAL = new AchievementInfo("ACH_COOKED_EXTRADIMENSIONAL", "Non-Locally Sourced", "nonlocal.png", "Cook a meal with an extradimensional limb as an ingredient.", false);

		// Token: 0x04001A65 RID: 6757
		public static readonly AchievementInfo ATE_SURPRISE = new AchievementInfo("ACH_ATE_SURPRISE", "Surprise!", "cloaca.png", "Eat the Cloaca Surprise.", false);

		// Token: 0x04001A66 RID: 6758
		public static readonly AchievementInfo ATE_CRYSTAL_DELIGHT = new AchievementInfo("ACH_ATE_CRYSTAL_DELIGHT", "My Many Facets", "crystaldelight.png", "Eat the Crystal Delight.", false);

		// Token: 0x04001A67 RID: 6759
		public static readonly AchievementInfo ATE_HUMBLE_PIE = new AchievementInfo("ACH_ATE_HUMBLE_PIE", "On Second Thought", "pie.png", "Eat a serving of humble pie.", false);

		// Token: 0x04001A68 RID: 6760
		public static readonly AchievementInfo CROSS_BRIGHTSHEOL = new AchievementInfo("ACH_CROSS_BRIGHTSHEOL", "From Thyn Heres Shaken the Wet and Olde Lif", "brightsheol.png", "Cross into Brightsheol.", true);

		// Token: 0x04001A69 RID: 6761
		public static readonly AchievementInfo TRAVEL_TZIMTZLUM = new AchievementInfo("ACH_TRAVEL_TZIMTZLUM", "All Those Who Wander", "wander.png", "Travel to the pocket dimension Tzimtzlum.", true);

		// Token: 0x04001A6A RID: 6762
		public static readonly AchievementInfo WEAR_OTHERPEARL = new AchievementInfo("ACH_WEAR_OTHERPEARL", "The Recitation of the Drowning of Eudoxia by the Witches of Moonhearth", "otherpearl.png", "Wear the Otherpearl.", true);

		// Token: 0x04001A6B RID: 6763
		public static readonly AchievementInfo ENTER_MAK_CLAM = new AchievementInfo("ACH_ENTER_MAK_CLAM", "A Clammy Reception", "clam.png", "Enter Mak's clam in the Yd Freehold.", true);

		// Token: 0x04001A6C RID: 6764
		public static readonly AchievementInfo COMPLETE_WATERVINE = new AchievementInfo("ACH_COMPLETE_WATERVINE", "What's Eating the Watervine?", "mehmet.png", "Complete the quest What's Eating the Watervine?", false);

		// Token: 0x04001A6D RID: 6765
		public static readonly AchievementInfo CLAMS_ENTERED_100 = new AchievementInfo("ACH_100_CLAMS_ENTERED", "Byevalve", "ticket.png", "Enter 100 giant clams.", "STAT_CLAMS_ENTERED", 100, int.MaxValue, false);

		// Token: 0x04001A6E RID: 6766
		public static readonly AchievementInfo RECAME = new AchievementInfo("ACH_RECAME", "You Recame", "recame.png", "Rematerialize in the corporeal realm.", true);

		// Token: 0x04001A6F RID: 6767
		public static readonly AchievementInfo GAVEALL_REPULSIVE_DEVICE = new AchievementInfo("ACH_GAVEALL_REPULSIVE_DEVICE", "Dayenu", "children.png", "Give the repulsive device to all four children of the Tomb.", "STAT_GAVE_REPULSIVE_DEVICE", new string[] { "STAT_GAVE_REPULSIVE_DEVICE_NACHAM", "STAT_GAVE_REPULSIVE_DEVICE_VAAM", "STAT_GAVE_REPULSIVE_DEVICE_DAGASHA", "STAT_GAVE_REPULSIVE_DEVICE_KAH" }, false);

		// Token: 0x04001A70 RID: 6768
		public static readonly AchievementInfo SLYNTH_HOME = new AchievementInfo("ACH_SLYNTH_HOME", "Belong, Friends", "belong.png", "Help the slynth find a new home.", false);

		// Token: 0x04001A71 RID: 6769
		public static readonly AchievementInfo GIFTED_10ASTERISK = new AchievementInfo("ACH_GIFTED_10ASTERISK", "Token of Gratitude", "asterisk.png", "Be gifted a 10-pointed asterisk.", false);

		// Token: 0x04001A72 RID: 6770
		public static readonly AchievementInfo BEAMSPLIT_SPACE_INVERTER = new AchievementInfo("ACH_BEAMSPLIT_SPACE_INVERTER", "The Laws of Physics Are Mere Suggestions, Vol. 2", "physics2.png", "Exploit wave-particle duality to clone yourself.", false);

		// Token: 0x04001A73 RID: 6771
		public static readonly AchievementInfo TURNED_STONE = new AchievementInfo("ACH_TURNED_STONE", "Aetalag", "aetalag.png", "Be turned to stone.", true);

		// Token: 0x04001A74 RID: 6772
		public static readonly AchievementInfo WINKED_OUT = new AchievementInfo("ACH_WINKED_OUT", "I-", "wink.png", "Wink out of existence.", false);

		// Token: 0x04001A75 RID: 6773
		public static readonly AchievementInfo SWALLOWED_WHOLE = new AchievementInfo("ACH_SWALLOWED_WHOLE", "*gulp*", "gulp.png", "Be swallowed whole.", true);

		// Token: 0x04001A76 RID: 6774
		public static readonly AchievementInfo TRANSMUTED_GEM = new AchievementInfo("ACH_TRANSMUTED_GEM", "Jeweled Dusk", "gemstone.png", "Be transmuted into a gemstone.", true);

		// Token: 0x04001A77 RID: 6775
		public static readonly AchievementInfo WEAR_6_FACES = new AchievementInfo("ACH_WEAR_6_FACES", "The Narrowing Sky", "face_overlap.png", "Wear each of the six Faces of the sultanate.", "STAT_WEAR_FACES", new string[] { "STAT_WEAR_FACE_1", "STAT_WEAR_FACE_2", "STAT_WEAR_FACE_3", "STAT_WEAR_FACE_4", "STAT_WEAR_FACE_5", "STAT_WEAR_FACE_6" }, false);

		// Token: 0x04001A78 RID: 6776
		public static readonly AchievementInfo AURORAL = new AchievementInfo("ACH_AURORAL", "Dawnglider", "auroral.png", "Become auroral.", false);

		// Token: 0x04001A79 RID: 6777
		public static readonly AchievementInfo CHAOS_SPIEL = new AchievementInfo("ACH_CHAOS_SPIEL", "Was It Something I Said?", "chaosspiel.png", "Invoke the Chaos Spiel.", false);

		// Token: 0x04001A7A RID: 6778
		public static readonly AchievementInfo MUTATION_FROM_GAMMAMOTH = new AchievementInfo("ACH_MUTATION_FROM_GAMMAMOTH", "Lottery Winner", "lottery.png", "Gain a mutation from a gamma moth's mutating gaze.", false);

		// Token: 0x04001A7B RID: 6779
		public static readonly AchievementInfo DRAMS_BRAIN_BRINE_20 = new AchievementInfo("ACH_20_DRAMS_BRAIN_BRINE", "The Psychal Chorus", "brain.png", "Drink 20 drams of brain brine.", "STAT_BRAIN_BRINE_DRAMS_DRUNK", 20, int.MaxValue, false);

		// Token: 0x04001A7C RID: 6780
		public static readonly AchievementInfo BESTOW_LIFE_20 = new AchievementInfo("ACH_BESTOW_LIFE_20", "Become as Gods", "becomeasgods.png", "Bestow life to 20 inanimate objects.", "STAT_BESTOW_LIFE", 20, int.MaxValue, false);

		// Token: 0x04001A7D RID: 6781
		public static readonly AchievementInfo TATTOO_SELF = new AchievementInfo("ACH_TATTOO_SELF", "Live and Ink", "tattoo.png", "Tattoo yourself.", false);

		// Token: 0x04001A7E RID: 6782
		public static readonly AchievementInfo DISAPPOINT_HEB = new AchievementInfo("ACH_DISAPPOINT_HEB", "tsk tsk", "disappoint.png", "Disappoint a highly entropic being.", false);

		// Token: 0x04001A7F RID: 6783
		public static readonly AchievementInfo RECOVER_RELIC = new AchievementInfo("ACH_RECOVER_RELIC", "Raisins in the Layer Cake", "relic.png", "Recover a relic from a historic site.", false);

		// Token: 0x04001A80 RID: 6784
		public static readonly AchievementInfo SIX_DAY_STILT = new AchievementInfo("ACH_SIX_DAY_STILT", "May the Ground Shake But the Six Day Stilt Never Tumble", "stilt.png", "Travel to the Six Day Stilt.", false);

		// Token: 0x04001A81 RID: 6785
		public static readonly AchievementInfo STONED_RED_ROCK = new AchievementInfo("ACH_STONED_RED_ROCK", "Red Rock Hazing Ritual", "red_rock.png", "Be stoned to death by a baboon on the surface of Red Rock.", true);

		// Token: 0x04001A82 RID: 6786
		public static readonly AchievementInfo LEARN_SECRET_FROM_MUMBLEMOUTH = new AchievementInfo("ACH_LEARN_SECRET_FROM_MUMBLEMOUTH", "Mumblecore", "mumble.png", "Learn a secret from the microbiome.", false);

		// Token: 0x04001A83 RID: 6787
		public static readonly AchievementInfo HEAD_EXPLODE = new AchievementInfo("ACH_HEAD_EXPLODE", "Open Your Mind", "skull2.png", "Have your head explode.", false);

		// Token: 0x04001A84 RID: 6788
		public static readonly AchievementInfo WEIRDWIRE_CONDUIT = new AchievementInfo("ACH_WEIRDWIRE_CONDUIT", "Weirdwire Conduit... Eureka!", "weirdwire.png", "Help Argyve build the contraption.", false);

		// Token: 0x04001A85 RID: 6789
		public static readonly AchievementInfo WILLING_SPIRIT = new AchievementInfo("ACH_WILLING_SPIRIT", "More Than a Willing Spirit", "waydroid.png", "Return from Golgotha with a repaired waydroid and gain admittance to Grit Gate.", false);

		// Token: 0x04001A86 RID: 6790
		public static readonly AchievementInfo DECODING_SIGNAL = new AchievementInfo("ACH_DECODING_SIGNAL", "Decoding the Signal", "signal.png", "Travel to the Temple of the Rock and decrypt the mysterious signal.", true);

		// Token: 0x04001A87 RID: 6791
		public static readonly AchievementInfo OMONPORCH_EARL = new AchievementInfo("ACH_OMONPORCH_EARL", "The Earl of Omonporch", "earl.png", "Strike a deal with Asphodel the Lovely for control of the Spindlegrounds.", true);

		// Token: 0x04001A88 RID: 6792
		public static readonly AchievementInfo CALL_TO_ARMS = new AchievementInfo("ACH_CALL_TO_ARMS", "A Call to Arms", "calltoarms.png", "Defend Grit Gate from the Putus Templar.", true);

		// Token: 0x04001A89 RID: 6793
		public static readonly AchievementInfo PAX_KLANQ = new AchievementInfo("ACH_PAX_KLANQ", "Pax Klanq, I Presume?", "klanq.png", "Deliver a message to the mushroom progidy.", true);

		// Token: 0x04001A8A RID: 6794
		public static readonly AchievementInfo EATER_TOMB = new AchievementInfo("ACH_EATER_TOMB", "Tomb of the Eaters", "eater.png", "Travel to Brightsheol and disable the Spindle's magnetic field.", true);

		// Token: 0x04001A8B RID: 6795
		public static readonly AchievementInfo GOLEM = new AchievementInfo("ACH_GOLEM", "The Golem", "golem.png", "Form large creature.", true);

		// Token: 0x04001A8C RID: 6796
		public static readonly AchievementInfo RECLAMATION = new AchievementInfo("ACH_RECLAMATION", "Reclamation", "omonporch.png", "Recover the Spindlegrounds from the Putus Templar war party.", true);

		// Token: 0x04001A8D RID: 6797
		public static readonly AchievementInfo ALEPH = new AchievementInfo("ACH_ALEPH", "Aleph", "nephil1.png", "Defeat one of the Girsh nephilim.", false);

		// Token: 0x04001A8E RID: 6798
		public static readonly AchievementInfo BET = new AchievementInfo("ACH_BET", "Bet", "nephil2.png", "Defeat two of the Girsh nephilim in a single playthrough.", false);

		// Token: 0x04001A8F RID: 6799
		public static readonly AchievementInfo GIMEL = new AchievementInfo("ACH_GIMEL", "Gimel", "nephil3.png", "Defeat three of the Girsh nephilim in a single playthrough.", false);

		// Token: 0x04001A90 RID: 6800
		public static readonly AchievementInfo DALET = new AchievementInfo("ACH_DALET", "Dalet", "nephil4.png", "Defeat four of the Girsh nephilim in a single playthrough.", false);

		// Token: 0x04001A91 RID: 6801
		public static readonly AchievementInfo HE = new AchievementInfo("ACH_HE", "He", "nephil5.png", "Defeat five of the Girsh nephilim in a single playthrough.", false);

		// Token: 0x04001A92 RID: 6802
		public static readonly AchievementInfo VAV = new AchievementInfo("ACH_VAV", "Vav", "nephil6.png", "Defeat six of the Girsh nephilim in a single playthrough.", false);

		// Token: 0x04001A93 RID: 6803
		public static readonly AchievementInfo ZAYIN = new AchievementInfo("ACH_ZAYIN", "Zayin", "nephil7.png", "Defeat all seven of the Girsh nephilim in a single playthrough.", true);

		// Token: 0x04001A94 RID: 6804
		public static readonly AchievementInfo PACIFY_ALL = new AchievementInfo("ACH_PACIFY_ALL", "Delight in the Abundance of Peace", "pacify.png", "Pacify all seven of the Girsh nephilim in a single playthrough.", true);

		// Token: 0x04001A95 RID: 6805
		public static readonly AchievementInfo GLITCH_OBJECT = new AchievementInfo("ACH_GLITCH_OBJECT", "The Laws of Physics Are Mere Suggestions, Vol. 3", "physics3.png", "Prove the information paradox by causing your surroundings to glitch.", false);

		// Token: 0x04001A96 RID: 6806
		public static readonly AchievementInfo GLITCH_SELF = new AchievementInfo("ACH_GLITCH_SELF", "Total Makeover", "static.png", "Pour a dram of pure warm static on yourself.", false);

		// Token: 0x04001A97 RID: 6807
		public static readonly AchievementInfo DEEP_DREAM = new AchievementInfo("ACH_DEEP_DREAM", "Dream within a Dream", "dream.png", "Enter a waking dream from within a waking dream.", false);

		// Token: 0x04001A98 RID: 6808
		public static readonly AchievementInfo SWOLLEN_BULB = new AchievementInfo("ACH_SWOLLEN_BULB", "Swollen Bulb", "sunslag.png", "Consume 10 drams of sunslag in a single playthrough.", "STAT_SUNSLAG_DRAMS_DRANK", 10, int.MaxValue, false);

		// Token: 0x04001A99 RID: 6809
		public static readonly AchievementInfo WANDER_DARKLING = new AchievementInfo("ACH_WANDER_DARKLING", "Wander Darkling in the Eternal Space", "darkling.png", "Lose your will to live.", false);

		// Token: 0x04001A9A RID: 6810
		public static readonly AchievementInfo SOUND_SLEEPER = new AchievementInfo("ACH_SOUND_SLEEPER", "Sound Sleeper", "sleeper.png", "Wake peacefully from twenty dreams.", "STAT_WAKING_DREAM_PEACEFUL", 20, int.MaxValue, false);

		// Token: 0x04001A9B RID: 6811
		public static readonly AchievementInfo SMOKE_HOOKAH = new AchievementInfo("ACH_SMOKE_HOOKAH", "Waterpipe and Rest", "hookah.png", "Smoke from a hookah.", false);

		// Token: 0x04001A9C RID: 6812
		public static readonly AchievementInfo ARCONAUT = new AchievementInfo("ACH_ARCONAUT", "Arconaut", "arconaut.png", "Descend 50 strata deep.", false);

		// Token: 0x04001A9D RID: 6813
		public static readonly AchievementInfo INFILTRATE_MECHA = new AchievementInfo("ACH_INFILTRATE_MECHA", "Get in the Robot", "robot.png", "Infiltrate a temple mecha.", false);

		// Token: 0x04001A9E RID: 6814
		public static readonly AchievementInfo VAPORIZED = new AchievementInfo("ACH_VAPORIZED", "Heat Death", "heat.png", "Get vaporized.", true);

		// Token: 0x04001A9F RID: 6815
		public static readonly AchievementInfo DIE_THIRST = new AchievementInfo("ACH_DIE_THIRST", "0 for 2", "thirst.png", "Die of thirst.", true);

		// Token: 0x04001AA0 RID: 6816
		public static readonly AchievementInfo DIE_COMPANION = new AchievementInfo("ACH_DIE_COMPANION", "Oops", "oops.png", "Be killed by a companion on accident.", true);

		// Token: 0x04001AA1 RID: 6817
		public static readonly AchievementInfo DIE_NORMALITY = new AchievementInfo("ACH_DIE_NORMALITY", "Imagine a Rubber Sheet", "rubber.png", "Dash yourself on the crags of spacetime.", false);

		// Token: 0x04001AA2 RID: 6818
		public static readonly AchievementInfo LAIRS_100 = new AchievementInfo("ACH_LAIRS_100", "Lair Cake", "cake.png", "Discover 100 lairs.", "STAT_LAIRS_VISITED", 100, int.MaxValue, false);

		// Token: 0x04001AA3 RID: 6819
		public static readonly AchievementInfo TAU_THEN = new AchievementInfo("ACH_TAU_THEN", "Then", "then.png", "Help Chavvah complete the ritual of -elseing and sever Tau at Taproot.", true);

		// Token: 0x04001AA4 RID: 6820
		public static readonly AchievementInfo TAU_ELSE = new AchievementInfo("ACH_TAU_ELSE", "Else", "else.png", "Help Chavvah complete the ritual of -elseing and sever Tau at Taproot.", true);

		// Token: 0x04001AA5 RID: 6821
		public static readonly AchievementInfo BEY_LAH_COMEDY = new AchievementInfo("ACH_BEY_LAH_COMEDY", "The Comedy", "comedy.png", "Settle the dispute in Bey Lah in Eskhind's favor.", true);

		// Token: 0x04001AA6 RID: 6822
		public static readonly AchievementInfo BEY_LAH_HISTORY = new AchievementInfo("ACH_BEY_LAH_HISTORY", "The History", "history.png", "Settle the dispute in Bey Lah in Hindriarch Keh's favor.", true);

		// Token: 0x04001AA7 RID: 6823
		public static readonly AchievementInfo BEY_LAH_TRAGEDY = new AchievementInfo("ACH_BEY_LAH_TRAGEDY", "The Tragedy", "tragedy.png", "Settle the dispute in Bey Lah and doom the village.", true);

		// Token: 0x04001AA8 RID: 6824
		public static readonly AchievementInfo BEY_LAH_SONNET = new AchievementInfo("ACH_BEY_LAH_SONNET", "The Sonnet", "sonnet.png", "Return the crumpled sheet of paper to its intended recipient or its author.", true);

		// Token: 0x04001AA9 RID: 6825
		public static readonly AchievementInfo STARFREIGHT = new AchievementInfo("ACH_STARFREIGHT", "We Are Starfreight", "spindle.png", "Ascend the Spindle.", true);

		// Token: 0x04001AAA RID: 6826
		public static readonly AchievementInfo KLANQ_SUN = new AchievementInfo("ACH_KLANQ_SUN", "Klanq Puff at Sun", "klanqsun.png", "Spread Klanq in space.", true);

		// Token: 0x04001AAB RID: 6827
		public static readonly AchievementInfo CAVES_OF_QUD = new AchievementInfo("ACH_CAVES_OF_QUD", "Caves of Qud", "coq.png", "Beat the game.", false);

		// Token: 0x04001AAC RID: 6828
		public static readonly AchievementInfo END_COVENANT = new AchievementInfo("ACH_END_COVENANT", "Seraphic Covenant", "covenant.png", "Enter into a covenant with Resheph and help prepare Qud for the Coven's return.", true);

		// Token: 0x04001AAD RID: 6829
		public static readonly AchievementInfo END_LAUNCH = new AchievementInfo("ACH_END_LAUNCH", "The Dusted Cosmos", "spaceship.png", "Leave the solar system on a starship.", true);

		// Token: 0x04001AAE RID: 6830
		public static readonly AchievementInfo END_RETURN = new AchievementInfo("ACH_END_RETURN", "Seraphic Heresy", "return.png", "Spurn Resheph and return to Qud.", true);

		// Token: 0x04001AAF RID: 6831
		public static readonly AchievementInfo END_ACCEDE = new AchievementInfo("ACH_END_ACCEDE", "The Tillage of the Noosphere", "accede.png", "Accede to Resheph's plan for purging the world of higher life.", true);

		// Token: 0x04001AB0 RID: 6832
		public static readonly AchievementInfo UNGYRE = new AchievementInfo("ACH_UNGYRE", "Ungyre", "ungyre.png", "Annul the plagues of the Gyre.", true);

		// Token: 0x04001AB1 RID: 6833
		public static readonly StatInfo STAT_BOOKS_READ = StatInfo.Create("STAT_BOOKS_READ", 1);

		// Token: 0x04001AB2 RID: 6834
		public static readonly StatInfo STAT_GAVE_REPULSIVE_DEVICE_NACHAM = StatInfo.Create("STAT_GAVE_REPULSIVE_DEVICE_NACHAM", 1);

		// Token: 0x04001AB3 RID: 6835
		public static readonly StatInfo STAT_GAVE_REPULSIVE_DEVICE_VAAM = StatInfo.Create("STAT_GAVE_REPULSIVE_DEVICE_VAAM", 1);

		// Token: 0x04001AB4 RID: 6836
		public static readonly StatInfo STAT_GAVE_REPULSIVE_DEVICE_DAGASHA = StatInfo.Create("STAT_GAVE_REPULSIVE_DEVICE_DAGASHA", 1);

		// Token: 0x04001AB5 RID: 6837
		public static readonly StatInfo STAT_GAVE_REPULSIVE_DEVICE_KAH = StatInfo.Create("STAT_GAVE_REPULSIVE_DEVICE_KAH", 1);
	}
}
