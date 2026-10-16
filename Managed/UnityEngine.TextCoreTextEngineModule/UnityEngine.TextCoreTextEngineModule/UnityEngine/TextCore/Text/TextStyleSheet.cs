using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003E RID: 62
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextStyleSheet : ScriptableObject
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		internal List<TextStyle> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000BDDC File Offset: 0x00009FDC
		private void Reset()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		public TextStyle GetStyle(int hashCode)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				object obj = this.styleLookupLock;
				lock (obj)
				{
					bool flag3 = this.m_StyleLookupDictionary == null;
					if (flag3)
					{
						this.LoadStyleDictionaryInternal();
					}
				}
			}
			TextStyle textStyle;
			bool flag4 = this.m_StyleLookupDictionary.TryGetValue(hashCode, out textStyle);
			TextStyle textStyle2;
			if (flag4)
			{
				textStyle2 = textStyle;
			}
			else
			{
				textStyle2 = null;
			}
			return textStyle2;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public TextStyle GetStyle(string name)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
			TextStyle textStyle;
			bool flag2 = this.m_StyleLookupDictionary.TryGetValue(hashCodeCaseInSensitive, out textStyle);
			TextStyle textStyle2;
			if (flag2)
			{
				textStyle2 = textStyle;
			}
			else
			{
				textStyle2 = null;
			}
			return textStyle2;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		private void LoadStyleDictionaryInternal()
		{
			Dictionary<int, TextStyle> dictionary = this.m_StyleLookupDictionary;
			bool flag = dictionary == null;
			if (flag)
			{
				dictionary = new Dictionary<int, TextStyle>();
			}
			else
			{
				dictionary.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				bool flag2 = !dictionary.ContainsKey(this.m_StyleList[i].hashCode);
				if (flag2)
				{
					dictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive("Normal");
			bool flag3 = !dictionary.ContainsKey(hashCodeCaseInSensitive);
			if (flag3)
			{
				TextStyle textStyle = new TextStyle("Normal", string.Empty, string.Empty);
				this.m_StyleList.Add(textStyle);
				dictionary.Add(hashCodeCaseInSensitive, textStyle);
			}
			this.m_StyleLookupDictionary = dictionary;
		}

		// Token: 0x04000185 RID: 389
		[SerializeField]
		private List<TextStyle> m_StyleList = new List<TextStyle>(1);

		// Token: 0x04000186 RID: 390
		private Dictionary<int, TextStyle> m_StyleLookupDictionary;

		// Token: 0x04000187 RID: 391
		private object styleLookupLock = new object();
	}
}
