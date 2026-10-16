using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000591 RID: 1425
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class VisualElementAsset : UxmlAsset, ISerializationCallbackReceiver
	{
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000996DC File Offset: 0x000978DC
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000996F4 File Offset: 0x000978F4
		public int ruleIndex
		{
			get
			{
				return this.m_RuleIndex;
			}
			set
			{
				this.m_RuleIndex = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00099700 File Offset: 0x00097900
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x00099718 File Offset: 0x00097918
		public string[] classes
		{
			get
			{
				return this.m_Classes;
			}
			set
			{
				this.m_Classes = value;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x00099724 File Offset: 0x00097924
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x0009974E File Offset: 0x0009794E
		public List<string> stylesheetPaths
		{
			get
			{
				List<string> list;
				if ((list = this.m_StylesheetPaths) == null)
				{
					list = (this.m_StylesheetPaths = new List<string>());
				}
				return list;
			}
			set
			{
				this.m_StylesheetPaths = value;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00099758 File Offset: 0x00097958
		public bool hasStylesheetPaths
		{
			get
			{
				return this.m_StylesheetPaths != null;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x00099764 File Offset: 0x00097964
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x00099789 File Offset: 0x00097989
		public List<StyleSheet> stylesheets
		{
			get
			{
				List<StyleSheet> list;
				if ((list = this.m_Stylesheets) == null)
				{
					list = (this.m_Stylesheets = new List<StyleSheet>());
				}
				return list;
			}
			set
			{
				this.m_Stylesheets = value;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00099792 File Offset: 0x00097992
		public bool hasStylesheets
		{
			get
			{
				return this.m_Stylesheets != null;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x0009979D File Offset: 0x0009799D
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x000997A5 File Offset: 0x000979A5
		public UxmlSerializedData serializedData
		{
			get
			{
				return this.m_SerializedData;
			}
			set
			{
				this.m_SerializedData = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x000997AE File Offset: 0x000979AE
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x000997B6 File Offset: 0x000979B6
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool skipClone
		{
			get
			{
				return this.m_SkipClone;
			}
			set
			{
				this.m_SkipClone = value;
			}
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000997BF File Offset: 0x000979BF
		public VisualElementAsset(string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
			: base(fullTypeName, xmlNamespace)
		{
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00002152 File Offset: 0x00000352
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000997F0 File Offset: 0x000979F0
		public void OnAfterDeserialize()
		{
			bool flag = !string.IsNullOrEmpty(this.m_Name) && !this.m_Properties.Contains("name");
			if (flag)
			{
				base.SetAttribute("name", this.m_Name);
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_Text) && !this.m_Properties.Contains("text");
			if (flag2)
			{
				base.SetAttribute("text", this.m_Text);
			}
			bool flag3 = this.m_PickingMode != PickingMode.Position && !this.m_Properties.Contains("picking-mode") && !this.m_Properties.Contains("pickingMode");
			if (flag3)
			{
				base.SetAttribute("picking-mode", this.m_PickingMode.ToString());
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000998C8 File Offset: 0x00097AC8
		private static bool IdsPathMatchesAttributeOverrideIdsPath(List<int> idsPath, List<int> attributeOverrideIdsPath, int templateId)
		{
			bool flag = idsPath == null || attributeOverrideIdsPath == null || idsPath.Count == 0 || attributeOverrideIdsPath.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num = idsPath.IndexOf(templateId);
				bool flag3 = idsPath.Count != attributeOverrideIdsPath.Count + num + 1;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					for (int i = idsPath.Count - 1; i > num; i--)
					{
						bool flag4 = idsPath[i] != attributeOverrideIdsPath[i - num - 1];
						if (flag4)
						{
							return false;
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00099968 File Offset: 0x00097B68
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual VisualElement Instantiate(CreationContext cc)
		{
			VisualElement visualElement = (VisualElement)this.serializedData.CreateInstance();
			this.serializedData.Deserialize(visualElement);
			bool hasOverrides = cc.hasOverrides;
			if (hasOverrides)
			{
				cc.veaIdsPath.Add(base.id);
				for (int i = cc.serializedDataOverrides.Count - 1; i >= 0; i--)
				{
					foreach (TemplateAsset.UxmlSerializedDataOverride uxmlSerializedDataOverride in cc.serializedDataOverrides[i].attributeOverrides)
					{
						bool flag = uxmlSerializedDataOverride.m_ElementId == base.id && VisualElementAsset.IdsPathMatchesAttributeOverrideIdsPath(cc.veaIdsPath, uxmlSerializedDataOverride.m_ElementIdsPath, cc.serializedDataOverrides[i].templateId);
						if (flag)
						{
							uxmlSerializedDataOverride.m_SerializedData.Deserialize(visualElement);
						}
					}
				}
				cc.veaIdsPath.Remove(base.id);
			}
			bool hasStylesheetPaths = this.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int j = 0; j < this.stylesheetPaths.Count; j++)
				{
					visualElement.AddStyleSheetPath(this.stylesheetPaths[j]);
				}
			}
			bool hasStylesheets = this.hasStylesheets;
			if (hasStylesheets)
			{
				for (int k = 0; k < this.stylesheets.Count; k++)
				{
					bool flag2 = this.stylesheets[k] != null;
					if (flag2)
					{
						visualElement.styleSheets.Add(this.stylesheets[k]);
					}
				}
			}
			bool flag3 = this.classes != null;
			if (flag3)
			{
				for (int l = 0; l < this.classes.Length; l++)
				{
					visualElement.AddToClassList(this.classes[l]);
				}
			}
			return visualElement;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00099B80 File Offset: 0x00097D80
		public override string ToString()
		{
			return string.Format("{0}({1})({2})", this.m_Name, base.fullTypeName, base.id);
		}

		// Token: 0x04001317 RID: 4887
		[SerializeField]
		private string m_Name = string.Empty;

		// Token: 0x04001318 RID: 4888
		[SerializeField]
		private int m_RuleIndex = -1;

		// Token: 0x04001319 RID: 4889
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x0400131A RID: 4890
		[SerializeField]
		private PickingMode m_PickingMode = PickingMode.Position;

		// Token: 0x0400131B RID: 4891
		[SerializeField]
		private string[] m_Classes;

		// Token: 0x0400131C RID: 4892
		[SerializeField]
		private List<string> m_StylesheetPaths;

		// Token: 0x0400131D RID: 4893
		[SerializeField]
		private List<StyleSheet> m_Stylesheets;

		// Token: 0x0400131E RID: 4894
		[SerializeReference]
		internal UxmlSerializedData m_SerializedData;

		// Token: 0x0400131F RID: 4895
		[SerializeField]
		private bool m_SkipClone;
	}
}
