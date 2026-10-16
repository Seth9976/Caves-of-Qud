using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000541 RID: 1345
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00095610 File Offset: 0x00093810
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x00095628 File Offset: 0x00093828
		public string templateAlias
		{
			get
			{
				return this.m_TemplateAlias;
			}
			set
			{
				this.m_TemplateAlias = value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x00095632 File Offset: 0x00093832
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x0009563A File Offset: 0x0009383A
		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return this.m_AttributeOverrides;
			}
			set
			{
				this.m_AttributeOverrides = value;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x00095644 File Offset: 0x00093844
		public bool hasAttributeOverride
		{
			get
			{
				List<TemplateAsset.AttributeOverride> attributeOverrides = this.m_AttributeOverrides;
				return attributeOverrides != null && attributeOverrides.Count > 0;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x00095667 File Offset: 0x00093867
		// (set) Token: 0x0600293E RID: 10558 RVA: 0x0009566F File Offset: 0x0009386F
		public List<TemplateAsset.UxmlSerializedDataOverride> serializedDataOverrides
		{
			get
			{
				return this.m_SerializedDataOverride;
			}
			set
			{
				this.m_SerializedDataOverride = value;
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00095678 File Offset: 0x00093878
		internal override VisualElement Instantiate(CreationContext cc)
		{
			TemplateContainer templateContainer = (TemplateContainer)base.Instantiate(cc);
			bool flag = templateContainer.templateSource == null;
			if (flag)
			{
				TemplateContainer templateContainer2 = templateContainer;
				VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
				templateContainer2.templateSource = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(templateContainer.templateId) : null);
				bool flag2 = templateContainer.templateSource == null;
				if (flag2)
				{
					templateContainer.Add(new Label("Unknown Template: '" + templateContainer.templateId + "'"));
					return templateContainer;
				}
			}
			List<CreationContext.AttributeOverrideRange> list;
			VisualElement visualElement;
			using (CollectionPool<List<CreationContext.AttributeOverrideRange>, CreationContext.AttributeOverrideRange>.Get(out list))
			{
				List<CreationContext.SerializedDataOverrideRange> list2;
				using (CollectionPool<List<CreationContext.SerializedDataOverrideRange>, CreationContext.SerializedDataOverrideRange>.Get(out list2))
				{
					bool flag3 = cc.attributeOverrides != null;
					if (flag3)
					{
						list.AddRange(cc.attributeOverrides);
					}
					bool flag4 = this.attributeOverrides.Count > 0;
					if (flag4)
					{
						list.Add(new CreationContext.AttributeOverrideRange(cc.visualTreeAsset, this.attributeOverrides));
					}
					bool flag5 = cc.serializedDataOverrides != null;
					if (flag5)
					{
						list2.AddRange(cc.serializedDataOverrides);
					}
					bool flag6 = this.serializedDataOverrides.Count > 0;
					if (flag6)
					{
						list2.Add(new CreationContext.SerializedDataOverrideRange(cc.visualTreeAsset, this.serializedDataOverrides, base.id));
					}
					List<int> list3 = ((cc.veaIdsPath != null) ? new List<int>(cc.veaIdsPath) : new List<int>());
					CreationContext creationContext = new CreationContext(cc.slotInsertionPoints, list, list2, null, null, list3, null);
					templateContainer.templateSource.CloneTree(templateContainer, creationContext);
					visualElement = templateContainer;
				}
			}
			return visualElement;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x00095838 File Offset: 0x00093A38
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x00095850 File Offset: 0x00093A50
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_SlotUsages;
			}
			set
			{
				this.m_SlotUsages = value;
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009585A File Offset: 0x00093A5A
		public TemplateAsset(string templateAlias, string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
			: base(fullTypeName, xmlNamespace)
		{
			Assert.IsFalse(string.IsNullOrEmpty(templateAlias), "Template alias must not be null or empty");
			this.m_TemplateAlias = templateAlias;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00095894 File Offset: 0x00093A94
		public void AddSlotUsage(string slotName, int resId)
		{
			bool flag = this.m_SlotUsages == null;
			if (flag)
			{
				this.m_SlotUsages = new List<VisualTreeAsset.SlotUsageEntry>();
			}
			this.m_SlotUsages.Add(new VisualTreeAsset.SlotUsageEntry(slotName, resId));
		}

		// Token: 0x04001281 RID: 4737
		[SerializeField]
		private string m_TemplateAlias;

		// Token: 0x04001282 RID: 4738
		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides = new List<TemplateAsset.AttributeOverride>();

		// Token: 0x04001283 RID: 4739
		[SerializeField]
		private List<TemplateAsset.UxmlSerializedDataOverride> m_SerializedDataOverride = new List<TemplateAsset.UxmlSerializedDataOverride>();

		// Token: 0x04001284 RID: 4740
		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		// Token: 0x02000542 RID: 1346
		[Serializable]
		public struct AttributeOverride
		{
			// Token: 0x06002944 RID: 10564 RVA: 0x000958D0 File Offset: 0x00093AD0
			public bool NamesPathMatchesElementNamesPath(IList<string> elementNamesPath)
			{
				bool flag = elementNamesPath == null || this.m_NamesPath == null || elementNamesPath.Count == 0 || this.m_NamesPath.Length == 0;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = this.m_NamesPath.Length == 1;
					if (flag3)
					{
						flag2 = this.m_NamesPath[0] == elementNamesPath[elementNamesPath.Count - 1];
					}
					else
					{
						bool flag4 = this.m_NamesPath.Length != elementNamesPath.Count;
						if (flag4)
						{
							flag2 = false;
						}
						else
						{
							for (int i = elementNamesPath.Count - 1; i >= 0; i--)
							{
								bool flag5 = elementNamesPath[i] != this.m_NamesPath[i];
								if (flag5)
								{
									return false;
								}
							}
							flag2 = true;
						}
					}
				}
				return flag2;
			}

			// Token: 0x04001285 RID: 4741
			public string m_ElementName;

			// Token: 0x04001286 RID: 4742
			public string[] m_NamesPath;

			// Token: 0x04001287 RID: 4743
			public string m_AttributeName;

			// Token: 0x04001288 RID: 4744
			public string m_Value;
		}

		// Token: 0x02000543 RID: 1347
		[Serializable]
		public struct UxmlSerializedDataOverride
		{
			// Token: 0x04001289 RID: 4745
			public int m_ElementId;

			// Token: 0x0400128A RID: 4746
			public List<int> m_ElementIdsPath;

			// Token: 0x0400128B RID: 4747
			[SerializeReference]
			public UxmlSerializedData m_SerializedData;
		}
	}
}
