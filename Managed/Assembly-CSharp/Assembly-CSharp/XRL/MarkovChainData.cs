using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace XRL
{
	// Token: 0x020004B8 RID: 1208
	[Serializable]
	public class MarkovChainData
	{
		// Token: 0x060034C7 RID: 13511 RVA: 0x000FEEB0 File Offset: 0x000FD0B0
		public void OnBeforeSerialize()
		{
			this.keys.Clear();
			this.values.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.Chain)
			{
				this.keys.Add(keyValuePair.Key);
				stringBuilder.Length = 0;
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append('\u0001');
					}
					stringBuilder.Append(keyValuePair.Value[i]);
				}
				this.values.Add(stringBuilder.ToString());
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000FEF78 File Offset: 0x000FD178
		public void OnAfterDeserialize()
		{
			this.Chain.Clear();
			if (this.keys.Count != this.values.Count)
			{
				throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", Array.Empty<object>()));
			}
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.Chain.Add(this.keys[i], new List<string>(this.values[i].Split('\u0001', StringSplitOptions.None)));
			}
			this.keys.Clear();
			this.values.Clear();
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000FF018 File Offset: 0x000FD218
		public void Replace(string Old, string New)
		{
			for (int i = this.OpeningWords.Count - 1; i >= 0; i--)
			{
				this.OpeningWords[i] = this.OpeningWords[i].Replace(Old, New);
			}
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.Chain)
			{
				if (keyValuePair.Key.Contains(Old))
				{
					this.keys.Add(keyValuePair.Key);
				}
				for (int j = 0; j < keyValuePair.Value.Count; j++)
				{
					keyValuePair.Value[j] = keyValuePair.Value[j].Replace(Old, New);
				}
			}
			foreach (string text in this.keys)
			{
				List<string> list = this.Chain[text];
				this.Chain.Remove(text);
				this.Chain[text.Replace(Old, New)] = list;
			}
			this.keys.Clear();
		}

		/// <summary>Expand chain with each possible state of a text variable.</summary>
		// Token: 0x060034CA RID: 13514 RVA: 0x000FF170 File Offset: 0x000FD370
		public void Replace(string Old, IList<string> New)
		{
			if (New.IsNullOrEmpty<string>())
			{
				return;
			}
			this.Expand(this.OpeningWords, Old, New);
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.Chain)
			{
				if (keyValuePair.Key.Contains(Old))
				{
					this.keys.Add(keyValuePair.Key);
				}
				this.Expand(keyValuePair.Value, Old, New);
			}
			foreach (string text in this.keys)
			{
				List<string> list = this.Chain[text];
				this.Chain.Remove(text);
				foreach (string text2 in New)
				{
					this.Chain[text.Replace(Old, text2)] = list;
				}
			}
			this.keys.Clear();
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000FF2B0 File Offset: 0x000FD4B0
		private void Expand(List<string> Values, string Old, IList<string> New)
		{
			for (int i = Values.Count - 1; i >= 0; i--)
			{
				string text = Values[i];
				if (text.Contains(Old))
				{
					Values[i] = text.Replace(Old, New[0]);
					for (int j = 1; j < New.Count; j++)
					{
						Values.Insert(i + j, text.Replace(Old, New[j]));
					}
				}
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000FF31E File Offset: 0x000FD51E
		public void SaveToFile(string FileName)
		{
			this.OnBeforeSerialize();
			File.WriteAllText(FileName, JsonUtility.ToJson(this));
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000FF332 File Offset: 0x000FD532
		public static MarkovChainData LoadFromFile(string FileName)
		{
			MarkovChainData markovChainData = JsonUtility.FromJson<MarkovChainData>(File.ReadAllText(FileName));
			markovChainData.OnAfterDeserialize();
			return markovChainData;
		}

		// Token: 0x04001ABA RID: 6842
		public int order;

		// Token: 0x04001ABB RID: 6843
		public List<string> OpeningWords = new List<string>();

		// Token: 0x04001ABC RID: 6844
		public List<string> keys = new List<string>();

		// Token: 0x04001ABD RID: 6845
		public List<string> values = new List<string>();

		// Token: 0x04001ABE RID: 6846
		[NonSerialized]
		public Dictionary<string, List<string>> Chain = new Dictionary<string, List<string>>();
	}
}
