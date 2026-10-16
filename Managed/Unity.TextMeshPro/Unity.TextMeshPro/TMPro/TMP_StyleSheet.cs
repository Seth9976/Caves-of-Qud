using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200004E RID: 78
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_StyleSheet : ScriptableObject
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000142C6 File Offset: 0x000124C6
		internal List<TMP_Style> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000142CE File Offset: 0x000124CE
		private void Reset()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000142D8 File Offset: 0x000124D8
		public TMP_Style GetStyle(int hashCode)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			TMP_Style tmp_Style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out tmp_Style))
			{
				return tmp_Style;
			}
			return null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00014308 File Offset: 0x00012508
		public TMP_Style GetStyle(string name)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCode = TMP_TextParsingUtilities.GetHashCode(name);
			TMP_Style tmp_Style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out tmp_Style))
			{
				return tmp_Style;
			}
			return null;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001433D File Offset: 0x0001253D
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00014348 File Offset: 0x00012548
		private void LoadStyleDictionaryInternal()
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.m_StyleLookupDictionary = new Dictionary<int, TMP_Style>();
			}
			else
			{
				this.m_StyleLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				if (!this.m_StyleLookupDictionary.ContainsKey(this.m_StyleList[i].hashCode))
				{
					this.m_StyleLookupDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
			int hashCode = TMP_TextParsingUtilities.GetHashCode("Normal");
			if (!this.m_StyleLookupDictionary.ContainsKey(hashCode))
			{
				TMP_Style tmp_Style = new TMP_Style("Normal", string.Empty, string.Empty);
				this.m_StyleList.Add(tmp_Style);
				this.m_StyleLookupDictionary.Add(hashCode, tmp_Style);
			}
		}

		// Token: 0x04000318 RID: 792
		[SerializeField]
		private List<TMP_Style> m_StyleList = new List<TMP_Style>(1);

		// Token: 0x04000319 RID: 793
		private Dictionary<int, TMP_Style> m_StyleLookupDictionary;
	}
}
