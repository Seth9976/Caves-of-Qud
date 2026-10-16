using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C7 RID: 1223
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleComplexSelector : ISerializationCallbackReceiver
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000883F8 File Offset: 0x000865F8
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x00088410 File Offset: 0x00086610
		public int specificity
		{
			get
			{
				return this.m_Specificity;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Specificity = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x0008841A File Offset: 0x0008661A
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x00088422 File Offset: 0x00086622
		public StyleRule rule
		{
			get; [VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set;
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x0008842C File Offset: 0x0008662C
		public bool isSimple
		{
			get
			{
				return this.m_isSimple;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00088444 File Offset: 0x00086644
		// (set) Token: 0x06002591 RID: 9617 RVA: 0x0008845C File Offset: 0x0008665C
		public StyleSelector[] selectors
		{
			get
			{
				return this.m_Selectors;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Selectors = value;
				this.m_isSimple = this.m_Selectors.Length == 1;
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00002152 File Offset: 0x00000352
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00088477 File Offset: 0x00086677
		public virtual void OnAfterDeserialize()
		{
			this.m_isSimple = this.m_Selectors.Length == 1;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0008848C File Offset: 0x0008668C
		internal void CachePseudoStateMasks()
		{
			bool flag = StyleComplexSelector.s_PseudoStates == null;
			if (flag)
			{
				StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
				StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
				StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
				StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
				StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
				StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
				StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
				StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
			}
			int i = 0;
			int num = this.selectors.Length;
			while (i < num)
			{
				StyleSelector styleSelector = this.selectors[i];
				StyleSelectorPart[] parts = styleSelector.parts;
				PseudoStates pseudoStates = (PseudoStates)0;
				PseudoStates pseudoStates2 = (PseudoStates)0;
				for (int j = 0; j < styleSelector.parts.Length; j++)
				{
					bool flag2 = styleSelector.parts[j].type == StyleSelectorType.PseudoClass;
					if (flag2)
					{
						StyleComplexSelector.PseudoStateData pseudoStateData;
						bool flag3 = StyleComplexSelector.s_PseudoStates.TryGetValue(parts[j].value, out pseudoStateData);
						if (flag3)
						{
							bool flag4 = !pseudoStateData.negate;
							if (flag4)
							{
								pseudoStates |= pseudoStateData.state;
							}
							else
							{
								pseudoStates2 |= pseudoStateData.state;
							}
						}
						else
						{
							Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", new object[] { parts[j].value });
						}
					}
				}
				styleSelector.pseudoStateMask = (int)pseudoStates;
				styleSelector.negatedPseudoStateMask = (int)pseudoStates2;
				i++;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0008868C File Offset: 0x0008688C
		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(", ", this.m_Selectors.Select<StyleSelector, string>((StyleSelector x) => x.ToString()).ToArray<string>()));
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000886E4 File Offset: 0x000868E4
		private static int StyleSelectorPartCompare(StyleSelectorPart x, StyleSelectorPart y)
		{
			bool flag = y.type < x.type;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				bool flag2 = y.type > x.type;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = y.value.CompareTo(x.value);
				}
			}
			return num;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0008873C File Offset: 0x0008693C
		internal unsafe void CalculateHashes()
		{
			bool isSimple = this.isSimple;
			if (!isSimple)
			{
				for (int i = this.selectors.Length - 2; i > -1; i--)
				{
					StyleComplexSelector.m_HashList.AddRange(this.selectors[i].parts);
				}
				StyleComplexSelector.m_HashList.RemoveAll((StyleSelectorPart p) => p.type != StyleSelectorType.Class && p.type != StyleSelectorType.ID && p.type != StyleSelectorType.Type);
				StyleComplexSelector.m_HashList.Sort(new Comparison<StyleSelectorPart>(StyleComplexSelector.StyleSelectorPartCompare));
				bool flag = true;
				StyleSelectorType styleSelectorType = StyleSelectorType.Unknown;
				string text = "";
				int num = 0;
				int num2 = Math.Min(4, StyleComplexSelector.m_HashList.Count);
				for (int j = 0; j < num2; j++)
				{
					bool flag2 = flag;
					if (flag2)
					{
						flag = false;
					}
					else
					{
						while (num < StyleComplexSelector.m_HashList.Count && StyleComplexSelector.m_HashList[num].type == styleSelectorType && StyleComplexSelector.m_HashList[num].value == text)
						{
							num++;
						}
						bool flag3 = num == StyleComplexSelector.m_HashList.Count;
						if (flag3)
						{
							break;
						}
					}
					styleSelectorType = StyleComplexSelector.m_HashList[num].type;
					text = StyleComplexSelector.m_HashList[num].value;
					bool flag4 = styleSelectorType == StyleSelectorType.ID;
					Salt salt;
					if (flag4)
					{
						salt = Salt.IdSalt;
					}
					else
					{
						bool flag5 = styleSelectorType == StyleSelectorType.Class;
						if (flag5)
						{
							salt = Salt.ClassSalt;
						}
						else
						{
							salt = Salt.TagNameSalt;
						}
					}
					*((ref this.ancestorHashes.hashes.FixedElementField) + (IntPtr)j * 4) = text.GetHashCode() * (int)salt;
				}
				StyleComplexSelector.m_HashList.Clear();
			}
		}

		// Token: 0x040010AB RID: 4267
		[NonSerialized]
		public Hashes ancestorHashes;

		// Token: 0x040010AC RID: 4268
		[SerializeField]
		private int m_Specificity;

		// Token: 0x040010AE RID: 4270
		[NonSerialized]
		private bool m_isSimple;

		// Token: 0x040010AF RID: 4271
		[SerializeField]
		private StyleSelector[] m_Selectors;

		// Token: 0x040010B0 RID: 4272
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int ruleIndex;

		// Token: 0x040010B1 RID: 4273
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[NonSerialized]
		internal StyleComplexSelector nextInTable;

		// Token: 0x040010B2 RID: 4274
		[NonSerialized]
		internal int orderInStyleSheet;

		// Token: 0x040010B3 RID: 4275
		private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;

		// Token: 0x040010B4 RID: 4276
		private static List<StyleSelectorPart> m_HashList = new List<StyleSelectorPart>();

		// Token: 0x020004C8 RID: 1224
		private struct PseudoStateData
		{
			// Token: 0x0600259A RID: 9626 RVA: 0x00088914 File Offset: 0x00086B14
			public PseudoStateData(PseudoStates state, bool negate)
			{
				this.state = state;
				this.negate = negate;
			}

			// Token: 0x040010B5 RID: 4277
			public readonly PseudoStates state;

			// Token: 0x040010B6 RID: 4278
			public readonly bool negate;
		}
	}
}
