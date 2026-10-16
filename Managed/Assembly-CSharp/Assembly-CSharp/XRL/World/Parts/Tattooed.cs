using System;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E10 RID: 3600
	[Obsolete]
	[Serializable]
	public class Tattooed : IPart
	{
		// Token: 0x060092A4 RID: 37540 RVA: 0x0034D2B0 File Offset: 0x0034B4B0
		public override bool SameAs(IPart p)
		{
			Tattooed tattooed = p as Tattooed;
			return tattooed.ChanceOneIn == this.ChanceOneIn && tattooed.CanTattoo == this.CanTattoo && tattooed.CanEngrave == this.CanEngrave && base.SameAs(p);
		}

		// Token: 0x060092A5 RID: 37541 RVA: 0x0034D2FB File Offset: 0x0034B4FB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x060092A6 RID: 37542 RVA: 0x0034D314 File Offset: 0x0034B514
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (Stat.Random(1, this.ChanceOneIn) == 1)
			{
				string text = "LibraryCorpus.json";
				MarkovBook.EnsureCorpusLoaded(text);
				MarkovChainData markovChainData = MarkovBook.CorpusData[text];
				try
				{
					string text2 = Grammar.InitLower(Grammar.RemoveBadTitleEndingWords(MarkovChain.GenerateFragment(markovChainData, MarkovChain.GenerateSeedFromWord(markovChainData, Grammar.GetWeightedStartingArticle()), Stat.Random(1, 5))));
					if (!text2.IsNullOrEmpty())
					{
						Tattoos.ApplyTattoo(this.ParentObject, this.CanTattoo, this.CanEngrave, text2, null, null);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("tattoo generation", ex);
				}
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003CBD RID: 15549
		public int ChanceOneIn = 10000;

		// Token: 0x04003CBE RID: 15550
		public bool CanTattoo = true;

		// Token: 0x04003CBF RID: 15551
		public bool CanEngrave = true;
	}
}
