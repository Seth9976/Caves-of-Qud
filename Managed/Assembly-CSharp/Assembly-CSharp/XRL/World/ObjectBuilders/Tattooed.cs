using System;
using XRL.Language;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A1C RID: 2588
	[Serializable]
	public class Tattooed : IObjectBuilder
	{
		// Token: 0x06006D42 RID: 27970 RVA: 0x00249A58 File Offset: 0x00247C58
		public override void Initialize()
		{
			this.CanTattoo = true;
			this.CanEngrave = true;
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x00249A68 File Offset: 0x00247C68
		public override void Apply(GameObject Object, string Context)
		{
			string text = "LibraryCorpus.json";
			MarkovBook.EnsureCorpusLoaded(text);
			MarkovChainData markovChainData = MarkovBook.CorpusData[text];
			try
			{
				string text2 = Grammar.InitLower(Grammar.RemoveBadTitleEndingWords(MarkovChain.GenerateFragment(markovChainData, MarkovChain.GenerateSeedFromWord(markovChainData, Grammar.GetWeightedStartingArticle()), Stat.Random(1, 5))));
				if (!text2.IsNullOrEmpty())
				{
					Tattoos.ApplyTattoo(Object, this.CanTattoo, this.CanEngrave, text2, null, null);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("tattoo generation", ex);
			}
		}

		// Token: 0x040031EC RID: 12780
		public bool CanTattoo;

		// Token: 0x040031ED RID: 12781
		public bool CanEngrave;
	}
}
