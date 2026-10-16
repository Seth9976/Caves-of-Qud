using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using XRL.Language;
using XRL.Rules;

namespace XRL
{
	// Token: 0x020004B9 RID: 1209
	public static class MarkovChain
	{
		// Token: 0x060034CF RID: 13519 RVA: 0x000FF37C File Offset: 0x000FD57C
		public static MarkovChainData BuildChain(string Corpus, int order)
		{
			MarkovChainData markovChainData = new MarkovChainData();
			markovChainData.order = order;
			string[] array = Corpus.Split(' ', StringSplitOptions.None);
			bool flag = false;
			for (int i = 0; i < array.Length - order; i++)
			{
				string text = array[i];
				text = text.TrimStart(' ');
				text = text.TrimEnd(' ');
				string text2;
				if (i == 0)
				{
					text2 = " ";
				}
				else
				{
					text2 = array[i - 1];
				}
				if (text2 == "")
				{
					text2 = " ";
				}
				if (text2[Math.Max(text2.Length - 1, 0)] == '.')
				{
					flag = true;
				}
				for (int j = 1; j < order; j++)
				{
					text = text + " " + array[i + j];
				}
				if (flag)
				{
					markovChainData.OpeningWords.Add(text);
					flag = false;
				}
				if (markovChainData.Chain.ContainsKey(text))
				{
					markovChainData.Chain[text].Add(array[i + order]);
				}
				else
				{
					markovChainData.Chain[text] = new List<string> { array[i + order] };
				}
			}
			return markovChainData;
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000FF490 File Offset: 0x000FD690
		public static MarkovChainData AppendCorpus(MarkovChainData Data, string Corpus, bool addOpeningWords = true)
		{
			string[] array = Corpus.Split(' ', StringSplitOptions.None);
			bool flag = false;
			for (int i = 0; i < array.Length - Data.order; i++)
			{
				string text = array[i];
				text = text.TrimStart(' ');
				text = text.TrimEnd(' ');
				string text2;
				if (i == 0)
				{
					text2 = " ";
				}
				else
				{
					text2 = array[i - 1];
				}
				if (text2 == "")
				{
					text2 = " ";
				}
				if (text2[Math.Max(text2.Length - 1, 0)] == '.')
				{
					flag = true;
				}
				for (int j = 1; j < Data.order; j++)
				{
					text = text + " " + array[i + j];
				}
				if (flag && addOpeningWords)
				{
					Data.OpeningWords.Add(text);
					flag = false;
				}
				if (Data.Chain.ContainsKey(text))
				{
					Data.Chain[text].Add(array[i + Data.order]);
				}
				else
				{
					Data.Chain[text] = new List<string> { array[i + Data.order] };
				}
			}
			return Data;
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000FF5AC File Offset: 0x000FD7AC
		public static MarkovChainData AppendSecret(MarkovChainData Data, string Secret, bool addOpeningWords = false)
		{
			string text = "";
			Match match = Regex.Match(Secret, "{.*?}");
			if (match != null && !string.IsNullOrEmpty(match.Value))
			{
				text = match.Groups[0].Value;
				Secret = Secret.Replace(text, "{");
			}
			string[] array = Secret.Split(' ', StringSplitOptions.None);
			bool flag = false;
			for (int i = 0; i < array.Length - Data.order; i++)
			{
				string text2 = array[i];
				text2 = text2.TrimStart(' ');
				text2 = text2.TrimEnd(' ');
				string text3;
				if (i == 0)
				{
					text3 = " ";
				}
				else
				{
					text3 = array[i - 1];
				}
				if (text3 == "")
				{
					text3 = " ";
				}
				if (text3[Math.Max(text3.Length - 1, 0)] == '.')
				{
					flag = true;
				}
				for (int j = 1; j < Data.order; j++)
				{
					text2 = text2 + " " + array[i + j];
				}
				if (flag && addOpeningWords)
				{
					Data.OpeningWords.Add(text2);
					flag = false;
				}
				if (Data.Chain.ContainsKey(text2))
				{
					Data.Chain[text2].Add(array[i + Data.order].Replace("{", text).Replace("{", "").Replace("}", ""));
				}
				else
				{
					Data.Chain[text2] = new List<string> { array[i + Data.order].Replace("{", text).Replace("{", "").Replace("}", "") };
				}
			}
			return Data;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000FF764 File Offset: 0x000FD964
		public static string GenerateSentence(MarkovChainData Data, string seed = null)
		{
			if (string.IsNullOrEmpty(seed))
			{
				seed = Data.OpeningWords[Stat.Random(0, Data.OpeningWords.Count - 1)];
			}
			List<string> list = new List<string>();
			for (int i = 0; i < Data.order; i++)
			{
				list.Add(seed.Split(' ', StringSplitOptions.None)[i]);
			}
			for (int j = 0; j < 100; j++)
			{
				string text = list[j];
				for (int k = 1; k < Data.order; k++)
				{
					text = text + " " + list[j + k];
				}
				try
				{
					string text2 = Data.Chain[text][Stat.Random(0, Data.Chain[text].Count - 1)];
					list.Add(text2);
					if (text2.Contains("."))
					{
						return string.Join(" ", list.ToArray()) + " ";
					}
				}
				catch
				{
					Debug.Log("Error on phrase: " + text);
					if (!Data.Chain.ContainsKey(text))
					{
						Debug.Log("Phrase not found in Data.Chain");
					}
					else
					{
						string text3 = "Data was ";
						List<string> list2 = Data.Chain[text];
						Debug.Log(text3 + ((list2 != null) ? list2.ToString() : null));
					}
					throw;
				}
			}
			return MarkovChain.GenerateSentence(Data, null);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000FF8D8 File Offset: 0x000FDAD8
		public static string GenerateShortSentence(MarkovChainData Data, string seed = null, int maxWords = 18)
		{
			int num = 50;
			string text;
			do
			{
				text = MarkovChain.GenerateSentence(Data, seed);
				num--;
			}
			while (text.Split(' ', StringSplitOptions.None).Length > maxWords && num > 0);
			return text;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000FF908 File Offset: 0x000FDB08
		public static string GenerateParagraph(MarkovChainData Data)
		{
			string text = "";
			int num = Stat.Random(3, 6);
			for (int i = 0; i < num; i++)
			{
				text += MarkovChain.GenerateSentence(Data, null);
			}
			return text + "\n\n";
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000FF94C File Offset: 0x000FDB4C
		public static string GenerateTitle(MarkovChainData Data)
		{
			string text3;
			do
			{
				string text = Data.Chain.Keys.ToArray<string>()[Stat.Random(0, Data.Chain.Keys.Count - 1)];
				string text2 = MarkovChain.GenerateFragment(Data, text, Stat.Random(1, 5));
				while (string.IsNullOrEmpty(text2))
				{
					text2 = MarkovChain.GenerateFragment(Data, Data.Chain.Keys.ToArray<string>()[Stat.Random(0, Data.Chain.Keys.Count - 1)], Stat.Random(1, 5));
				}
				text3 = Grammar.MakeTitleCase(Grammar.RemoveBadTitleStartingWords(Grammar.RemoveBadTitleEndingWords(text2)));
			}
			while (string.IsNullOrEmpty(text3) || (text3.Distinct<char>().Count<char>() == 1 && text3.Contains(' ')));
			return text3;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000FFA1C File Offset: 0x000FDC1C
		public static string GenerateFragment(MarkovChainData Data, string Seed, int Length)
		{
			if (Seed.Contains("."))
			{
				return null;
			}
			string text = Seed;
			string text2 = "";
			for (int i = 0; i < Length; i++)
			{
				if (Data.Chain.ContainsKey(Seed))
				{
					text2 = Data.Chain[Seed][Stat.Random(0, Data.Chain[Seed].Count - 1)];
				}
				text += " ";
				text += text2;
				if (text2.Contains("."))
				{
					break;
				}
				Seed = Seed + " " + text2;
				Seed = Seed.Substring(Seed.Split(' ', StringSplitOptions.None)[0].Length).TrimStart(' ');
			}
			return text.Replace(",", "").Replace(".", "").Replace("\"", "")
				.Replace("(", "")
				.Replace(")", "")
				.Replace("\r", "");
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000FFB34 File Offset: 0x000FDD34
		public static string GenerateSeedFromWord(MarkovChainData Data, string word)
		{
			string[] array = Data.Chain.Keys.ToArray<string>();
			int num = array.Length;
			int num2 = Stat.Random(0, num - 1);
			while (!array[num2].StartsWith(word + " ") && !(word == "0"))
			{
				num2++;
				if (num2 >= num)
				{
					num2 = 0;
				}
			}
			return array[num2];
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000FFB94 File Offset: 0x000FDD94
		public static int Count(MarkovChainData Data)
		{
			int num = 0;
			foreach (List<string> list in Data.Chain.Values)
			{
				num += list.Count;
			}
			return num;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000FFBF4 File Offset: 0x000FDDF4
		public static int[] GetMostCommonPhrases(MarkovChainData Data)
		{
			int[] array = new int[3];
			string[] array2 = new string[] { "", "", "" };
			foreach (string text in Data.Chain.Keys)
			{
				if (Data.Chain[text].Count > array[0])
				{
					array[2] = array[1];
					array[1] = array[0];
					array[0] = Data.Chain[text].Count;
					array2[2] = array2[1];
					array2[1] = array2[0];
					array2[0] = text;
				}
				else if (Data.Chain[text].Count > array[1])
				{
					array[2] = array[1];
					array[1] = Data.Chain[text].Count;
					array2[2] = array2[1];
					array2[1] = text;
				}
				else if (Data.Chain[text].Count > array[2])
				{
					array[2] = Data.Chain[text].Count;
					array2[2] = text;
				}
			}
			return array;
		}
	}
}
