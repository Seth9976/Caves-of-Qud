using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Skills;

namespace XRL.World.Encounters
{
	// Token: 0x02000BA3 RID: 2979
	[Serializable]
	public class DimensionManager : IComposite
	{
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060076B1 RID: 30385 RVA: 0x002CA3E5 File Offset: 0x002C85E5
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x002CA408 File Offset: 0x002C8608
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.PsychicFactions.Count);
			foreach (PsychicFaction psychicFaction in this.PsychicFactions)
			{
				Writer.WriteComposite<PsychicFaction>(psychicFaction);
			}
			Writer.WriteOptimized(this.ExtraDimensions.Count);
			foreach (ExtraDimension extraDimension in this.ExtraDimensions)
			{
				Writer.WriteComposite<ExtraDimension>(extraDimension);
			}
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x002CA4C0 File Offset: 0x002C86C0
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			this.PsychicFactions.EnsureCapacity<PsychicFaction>(num);
			for (int i = 0; i < num; i++)
			{
				this.PsychicFactions.Add(Reader.ReadComposite<PsychicFaction>());
			}
			num = Reader.ReadOptimizedInt32();
			this.ExtraDimensions.EnsureCapacity<ExtraDimension>(num);
			for (int j = 0; j < num; j++)
			{
				this.ExtraDimensions.Add(Reader.ReadComposite<ExtraDimension>());
			}
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x002CA530 File Offset: 0x002C8730
		public static void Init()
		{
			DimensionManager dimensionManager = new DimensionManager();
			for (int i = 1; i < 4; i++)
			{
				dimensionManager.PsychicFactions.Add(dimensionManager.InitializeFaction());
			}
			dimensionManager.GenerateMoreDimensions();
			XRLCore.Core.Game.SetObjectGameState("DimensionManager", dimensionManager);
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x002CA57C File Offset: 0x002C877C
		public PsychicFaction InitializeFaction()
		{
			PsychicFaction psychicFaction = new PsychicFaction();
			int num = 0;
			while (++num < 1000)
			{
				psychicFaction.factionName = Factions.GetRandomPotentiallyExtradimensionalFaction().Name;
				if (!this.IsValueTaken_PsychicFactions("factionName", psychicFaction.factionName))
				{
					if (80.in100())
					{
						List<MutationEntry> mutationsOfCategory = MutationFactory.GetMutationsOfCategory("Mental", false);
						mutationsOfCategory.ShuffleInPlace<MutationEntry>(null);
						MutationEntry mutationEntry = null;
						foreach (MutationEntry mutationEntry2 in mutationsOfCategory)
						{
							if (mutationEntry2.Category != null && mutationEntry2.Category.Name == "Mental" && mutationEntry2.Cost > 1 && !this.IsValueTaken_PsychicFactions("preferredMutation", mutationEntry2.Class))
							{
								mutationEntry = mutationEntry2;
								break;
							}
						}
						psychicFaction.preferredMutation = mutationEntry.Class;
					}
					else
					{
						psychicFaction.preferredMutation = "none";
					}
					psychicFaction.mainColor = Crayons.GetRandomColorAll(null);
					num = 0;
					string randomColorAll;
					do
					{
						randomColorAll = Crayons.GetRandomColorAll(null);
					}
					while (randomColorAll == psychicFaction.mainColor && ++num < 100);
					psychicFaction.a = Grammar.weirdLowerAs.GetRandomElement<char>(null).ToString();
					psychicFaction.A = Grammar.weirdUpperAs.GetRandomElement<char>(null).ToString();
					psychicFaction.e = Grammar.weirdLowerEs.GetRandomElement<char>(null).ToString();
					psychicFaction.E = Grammar.weirdUpperEs.GetRandomElement<char>(null).ToString();
					psychicFaction.i = Grammar.weirdLowerIs.GetRandomElement<char>(null).ToString();
					psychicFaction.I = Grammar.weirdUpperIs.GetRandomElement<char>(null).ToString();
					psychicFaction.o = Grammar.weirdLowerOs.GetRandomElement<char>(null).ToString();
					psychicFaction.O = Grammar.weirdUpperOs.GetRandomElement<char>(null).ToString();
					psychicFaction.u = Grammar.weirdLowerUs.GetRandomElement<char>(null).ToString();
					psychicFaction.U = Grammar.weirdUpperUs.GetRandomElement<char>(null).ToString();
					psychicFaction.c = Grammar.weirdLowerCs.GetRandomElement<char>(null).ToString();
					psychicFaction.f = Grammar.weirdLowerFs.GetRandomElement<char>(null).ToString();
					psychicFaction.n = Grammar.weirdLowerNs.GetRandomElement<char>(null).ToString();
					psychicFaction.t = Grammar.weirdLowerTs.GetRandomElement<char>(null).ToString();
					psychicFaction.y = Grammar.weirdLowerYs.GetRandomElement<char>(null).ToString();
					psychicFaction.B = Grammar.weirdUpperBs.GetRandomElement<char>(null).ToString();
					psychicFaction.C = Grammar.weirdUpperCs.GetRandomElement<char>(null).ToString();
					psychicFaction.Y = Grammar.weirdUpperYs.GetRandomElement<char>(null).ToString();
					psychicFaction.L = Grammar.weirdUpperLs.GetRandomElement<char>(null).ToString();
					psychicFaction.R = Grammar.weirdUpperRs.GetRandomElement<char>(null).ToString();
					psychicFaction.N = Grammar.weirdUpperNs.GetRandomElement<char>(null).ToString();
					num = 0;
					do
					{
						psychicFaction.cultSymbol = int.Parse(HistoricStringExpander.ExpandString("<spice.extradimensional.cultSymbols.!random>", null, null, null, null));
					}
					while ((this.IsValueTaken_PsychicFactions("cultSymbol", psychicFaction.cultSymbol) || this.IsValueTaken_PsychicFactions("dimensionSymbol", psychicFaction.cultSymbol)) && ++num < 100);
					if (psychicFaction.preferredMutation == "none")
					{
						num = 0;
						do
						{
							psychicFaction.cultForm = Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.extradimensional.cultForms.!random>", null, null, null, null));
							if (!this.IsValueTaken_PsychicFactions("cultForm", psychicFaction.cultForm))
							{
								break;
							}
						}
						while (++num < 100);
					}
					else
					{
						string text = HistoricStringExpander.ExpandString("<spice.extradimensional." + psychicFaction.preferredMutation + ".!random>", null);
						num = 0;
						do
						{
							psychicFaction.cultForm = Grammar.MakeTitleCase(text.Replace("*cult*", HistoricStringExpander.ExpandString("<spice.extradimensional.cultForms.!random>", null)));
						}
						while (this.IsValueTaken_PsychicFactions("cultForm", psychicFaction.cultForm) && ++num < 100);
					}
					num = 0;
					do
					{
						psychicFaction.dimensionSymbol = int.Parse(HistoricStringExpander.ExpandString("<spice.extradimensional.cultSymbols.!random>", null, null, null, null));
					}
					while (this.IsValueTaken_PsychicFactions("dimensionSymbol", psychicFaction.dimensionSymbol) || (this.IsValueTaken_PsychicFactions("cultSymbol", psychicFaction.cultSymbol) && ++num < 100));
					num = 0;
					do
					{
						psychicFaction.dimensionName = "the " + Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.extradimensional.dimensionNames.!random>", null, null, null, null));
					}
					while (this.IsValueTaken_PsychicFactions("dimensionName", psychicFaction.dimensionName) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalWeaponIndex = Stat.Random(0, 17);
					}
					while (this.IsValueTaken_PsychicFactions("dimensionalWeaponIndex", psychicFaction.dimensionalWeaponIndex) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalMissileWeaponIndex = Stat.Random(0, 17);
					}
					while (this.IsValueTaken_PsychicFactions("dimensionalMissileWeaponIndex", psychicFaction.dimensionalMissileWeaponIndex) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalArmorIndex = Stat.Random(0, 19);
					}
					while (this.IsValueTaken_PsychicFactions("dimensionalArmorIndex", psychicFaction.dimensionalArmorIndex) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalShieldIndex = Stat.Random(0, 18);
					}
					while (this.IsValueTaken_PsychicFactions("dimensionalShieldIndex", psychicFaction.dimensionalShieldIndex) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalMiscIndex = Stat.Random(0, 21);
					}
					while (this.IsValueTaken_PsychicFactions("dimensionalMiscIndex", psychicFaction.dimensionalMiscIndex) && ++num < 100);
					num = 0;
					do
					{
						psychicFaction.dimensionalTraining = DimensionManager.GenerateRandomTraining();
					}
					while (psychicFaction.dimensionalTraining != null && this.IsValueTaken_PsychicFactions("dimensionalTraining", psychicFaction.dimensionalTraining) && ++num < 100);
					string text2 = Guid.NewGuid().ToString();
					JournalAPI.AddObservation("There exists a dimension known as {{O|" + psychicFaction.dimensionName.Replace("*DimensionSymbol*", ((char)psychicFaction.dimensionSymbol).ToString()) + "}}.", text2, "Dimensions", text2, new string[] { "dimension" }, false, -1L, null, false, true);
					psychicFaction.dimensionSecretID = text2;
					return psychicFaction;
				}
			}
			throw new Exception("infinitely looping on psychic faction base choice");
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x002CABF8 File Offset: 0x002C8DF8
		public static string GenerateRandomTraining()
		{
			if (!70.in100())
			{
				return Statistic.Attributes.GetRandomElement<string>(null);
			}
			PowerEntry randomElement = SkillFactory.GetPowers().GetRandomElement<PowerEntry>(null);
			if (randomElement.ParentSkill != null && (randomElement.Cost <= 0 || randomElement.IsSkillInitiatory))
			{
				return randomElement.ParentSkill.Class;
			}
			return randomElement.Class;
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x002CAC54 File Offset: 0x002C8E54
		public void GenerateMoreDimensions()
		{
			for (int i = 1; i <= DimensionManager.NUM_EXTRA_DIMENSIONS; i++)
			{
				ExtraDimension extraDimension = new ExtraDimension();
				extraDimension.mainColor = Crayons.GetRandomColorAll(null);
				int num = 0;
				string randomColorAll;
				do
				{
					randomColorAll = Crayons.GetRandomColorAll(null);
				}
				while (randomColorAll == extraDimension.mainColor && ++num < 100);
				extraDimension.a = Grammar.weirdLowerAs.GetRandomElement<char>(null).ToString();
				extraDimension.A = Grammar.weirdUpperAs.GetRandomElement<char>(null).ToString();
				extraDimension.e = Grammar.weirdLowerEs.GetRandomElement<char>(null).ToString();
				extraDimension.E = Grammar.weirdUpperEs.GetRandomElement<char>(null).ToString();
				extraDimension.i = Grammar.weirdLowerIs.GetRandomElement<char>(null).ToString();
				extraDimension.I = Grammar.weirdUpperIs.GetRandomElement<char>(null).ToString();
				extraDimension.o = Grammar.weirdLowerOs.GetRandomElement<char>(null).ToString();
				extraDimension.O = Grammar.weirdUpperOs.GetRandomElement<char>(null).ToString();
				extraDimension.u = Grammar.weirdLowerUs.GetRandomElement<char>(null).ToString();
				extraDimension.U = Grammar.weirdUpperUs.GetRandomElement<char>(null).ToString();
				extraDimension.c = Grammar.weirdLowerCs.GetRandomElement<char>(null).ToString();
				extraDimension.f = Grammar.weirdLowerFs.GetRandomElement<char>(null).ToString();
				extraDimension.n = Grammar.weirdLowerNs.GetRandomElement<char>(null).ToString();
				extraDimension.t = Grammar.weirdLowerTs.GetRandomElement<char>(null).ToString();
				extraDimension.y = Grammar.weirdLowerYs.GetRandomElement<char>(null).ToString();
				extraDimension.B = Grammar.weirdUpperBs.GetRandomElement<char>(null).ToString();
				extraDimension.C = Grammar.weirdUpperCs.GetRandomElement<char>(null).ToString();
				extraDimension.Y = Grammar.weirdUpperYs.GetRandomElement<char>(null).ToString();
				extraDimension.L = Grammar.weirdUpperLs.GetRandomElement<char>(null).ToString();
				extraDimension.R = Grammar.weirdUpperRs.GetRandomElement<char>(null).ToString();
				extraDimension.N = Grammar.weirdUpperNs.GetRandomElement<char>(null).ToString();
				num = 0;
				do
				{
					extraDimension.Symbol = int.Parse(HistoricStringExpander.ExpandString("<spice.extradimensional.cultSymbols.!random>", null));
				}
				while ((this.IsValueTaken_PsychicFactions("dimensionSymbol", extraDimension.Symbol) || this.IsValueTaken_PsychicFactions("cultSymbol", extraDimension.Symbol) || this.IsValueTaken_ExtraDimensions("Symbol", extraDimension.Symbol)) && ++num < 100);
				num = 0;
				do
				{
					extraDimension.Name = "the " + Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.extradimensional.dimensionNames.!random>", null));
				}
				while ((this.IsValueTaken_PsychicFactions("dimensionName", extraDimension.Name) || this.IsValueTaken_ExtraDimensions("Name", extraDimension.Name)) && ++num < 100);
				num = 0;
				do
				{
					extraDimension.WeaponIndex = Stat.Random(0, 15);
				}
				while ((this.IsValueTaken_PsychicFactions("dimensionalWeaponIndex", extraDimension.WeaponIndex) || this.IsValueTaken_ExtraDimensions("WeaponIndex", extraDimension.WeaponIndex)) && ++num < 40);
				num = 0;
				do
				{
					extraDimension.MissileWeaponIndex = Stat.Random(0, 13);
				}
				while ((this.IsValueTaken_PsychicFactions("dimensionalWeaponIndex", extraDimension.WeaponIndex) || this.IsValueTaken_ExtraDimensions("WeaponIndex", extraDimension.WeaponIndex)) && ++num < 40);
				do
				{
					extraDimension.ArmorIndex = Stat.Random(0, 19);
				}
				while (this.IsValueTaken_PsychicFactions("dimensionalArmorIndex", extraDimension.ArmorIndex) || this.IsValueTaken_ExtraDimensions("ArmorIndex", extraDimension.ArmorIndex));
				do
				{
					extraDimension.ShieldIndex = Stat.Random(0, 18);
				}
				while (this.IsValueTaken_PsychicFactions("dimensionalShieldIndex", extraDimension.ShieldIndex) || this.IsValueTaken_ExtraDimensions("ShieldIndex", extraDimension.ShieldIndex));
				do
				{
					extraDimension.MiscIndex = Stat.Random(0, 21);
				}
				while (this.IsValueTaken_PsychicFactions("dimensionalMiscIndex", extraDimension.MiscIndex) || this.IsValueTaken_ExtraDimensions("MiscIndex", extraDimension.MiscIndex));
				num = 0;
				do
				{
					extraDimension.Training = DimensionManager.GenerateRandomTraining();
				}
				while (extraDimension.Training != null && this.IsValueTaken_ExtraDimensions("Training", extraDimension.Training) && ++num < 100);
				string text = Guid.NewGuid().ToString();
				JournalAPI.AddObservation("There exists a dimension known as {{O|" + extraDimension.Name.Replace("*DimensionSymbol*", ((char)extraDimension.Symbol).ToString()) + "}}.", text, "Dimensions", text, new string[] { "dimension" }, false, -1L, null, false, true);
				extraDimension.SecretID = text;
				this.ExtraDimensions.Add(extraDimension);
			}
			JournalAPI.AddObservation("There exists a pocket dimension known as {{O|Tzimtzlum}}.", "$Tzimtzlum", "Dimensions", "$Tzimtzlum", new string[] { "dimension" }, false, -1L, null, false, true);
		}

		// Token: 0x060076B9 RID: 30393 RVA: 0x002CB1B0 File Offset: 0x002C93B0
		public bool IsValueTaken_PsychicFactions(string property, object value)
		{
			foreach (PsychicFaction psychicFaction in this.PsychicFactions)
			{
				if (psychicFaction.GetType().GetField(property).GetValue(psychicFaction)
					.ToString() == value.ToString())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060076BA RID: 30394 RVA: 0x002CB228 File Offset: 0x002C9428
		public bool IsValueTaken_ExtraDimensions(string property, object value)
		{
			foreach (ExtraDimension extraDimension in this.ExtraDimensions)
			{
				if (extraDimension.GetType().GetField(property).GetValue(extraDimension)
					.ToString() == value.ToString())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400369F RID: 13983
		public static readonly int GLIMMER_FLOOR = 20;

		// Token: 0x040036A0 RID: 13984
		public static readonly int GLIMMER_EXTRADIMENSIONAL_FLOOR = 40;

		// Token: 0x040036A1 RID: 13985
		public static readonly int NUM_EXTRA_DIMENSIONS = 8;

		// Token: 0x040036A2 RID: 13986
		public List<PsychicFaction> PsychicFactions = new List<PsychicFaction>();

		// Token: 0x040036A3 RID: 13987
		public List<ExtraDimension> ExtraDimensions = new List<ExtraDimension>();
	}
}
