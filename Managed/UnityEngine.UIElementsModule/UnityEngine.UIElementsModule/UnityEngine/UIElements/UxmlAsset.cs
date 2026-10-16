using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000582 RID: 1410
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class UxmlAsset : IUxmlAttributes
	{
		// Token: 0x06002A4D RID: 10829 RVA: 0x000985F0 File Offset: 0x000967F0
		public UxmlAsset(string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
		{
			this.m_FullTypeName = fullTypeName;
			this.m_XmlNamespace = xmlNamespace;
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x00098608 File Offset: 0x00096808
		// (set) Token: 0x06002A4F RID: 10831 RVA: 0x00098610 File Offset: 0x00096810
		public string fullTypeName
		{
			get
			{
				return this.m_FullTypeName;
			}
			set
			{
				this.m_FullTypeName = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x00098619 File Offset: 0x00096819
		// (set) Token: 0x06002A51 RID: 10833 RVA: 0x00098621 File Offset: 0x00096821
		public UxmlNamespaceDefinition xmlNamespace
		{
			get
			{
				return this.m_XmlNamespace;
			}
			set
			{
				this.m_XmlNamespace = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x0009862A File Offset: 0x0009682A
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x00098632 File Offset: 0x00096832
		public int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x0009863B File Offset: 0x0009683B
		public bool isNull
		{
			get
			{
				return this.fullTypeName == "null";
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x0009864D File Offset: 0x0009684D
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x00098655 File Offset: 0x00096855
		public int orderInDocument
		{
			get
			{
				return this.m_OrderInDocument;
			}
			set
			{
				this.m_OrderInDocument = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x0009865E File Offset: 0x0009685E
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x00098666 File Offset: 0x00096866
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00098670 File Offset: 0x00096870
		public List<UxmlNamespaceDefinition> namespaceDefinitions
		{
			get
			{
				List<UxmlNamespaceDefinition> list;
				if ((list = this.m_NamespaceDefinitions) == null)
				{
					list = (this.m_NamespaceDefinitions = new List<UxmlNamespaceDefinition>());
				}
				return list;
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00098695 File Offset: 0x00096895
		public List<string> GetProperties()
		{
			return this.m_Properties;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0009869D File Offset: 0x0009689D
		public bool HasParent()
		{
			return this.m_ParentId != 0;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000986A8 File Offset: 0x000968A8
		public bool HasAttribute(string attributeName)
		{
			bool flag = this.m_Properties == null || this.m_Properties.Count <= 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count; i += 2)
				{
					string text = this.m_Properties[i];
					bool flag3 = text == attributeName;
					if (flag3)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0009871C File Offset: 0x0009691C
		public string GetAttributeValue(string attributeName)
		{
			string text;
			this.TryGetAttributeValue(attributeName, out text);
			return text;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0009873C File Offset: 0x0009693C
		public bool TryGetAttributeValue(string propertyName, out string value)
		{
			bool flag = this.m_Properties == null;
			bool flag2;
			if (flag)
			{
				value = null;
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
				{
					bool flag3 = this.m_Properties[i] == propertyName;
					if (flag3)
					{
						value = this.m_Properties[i + 1];
						return true;
					}
				}
				value = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000987B4 File Offset: 0x000969B4
		public void AddUxmlNamespace(string prefix, string resolvedNamespace)
		{
			this.namespaceDefinitions.Add(new UxmlNamespaceDefinition
			{
				prefix = prefix,
				resolvedNamespace = resolvedNamespace
			});
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000987E7 File Offset: 0x000969E7
		public void SetAttribute(string name, string value)
		{
			this.SetOrAddProperty(name, value);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000987F4 File Offset: 0x000969F4
		public void RemoveAttribute(string attributeName)
		{
			bool flag = this.m_Properties == null || this.m_Properties.Count <= 0;
			if (!flag)
			{
				for (int i = 0; i < this.m_Properties.Count; i += 2)
				{
					string text = this.m_Properties[i];
					bool flag2 = text != attributeName;
					if (!flag2)
					{
						this.m_Properties.RemoveAt(i);
						this.m_Properties.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00098878 File Offset: 0x00096A78
		private void SetOrAddProperty(string propertyName, string propertyValue)
		{
			bool flag = this.m_Properties == null;
			if (flag)
			{
				this.m_Properties = new List<string>();
			}
			for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
			{
				bool flag2 = this.m_Properties[i] == propertyName;
				if (flag2)
				{
					this.m_Properties[i + 1] = propertyValue;
					return;
				}
			}
			this.m_Properties.Add(propertyName);
			this.m_Properties.Add(propertyValue);
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000988FF File Offset: 0x00096AFF
		public override string ToString()
		{
			return string.Format("{0}(id:{1})", this.fullTypeName, this.id);
		}

		// Token: 0x040012FB RID: 4859
		public const string NullNodeType = "null";

		// Token: 0x040012FC RID: 4860
		[SerializeField]
		private string m_FullTypeName;

		// Token: 0x040012FD RID: 4861
		[SerializeField]
		private UxmlNamespaceDefinition m_XmlNamespace;

		// Token: 0x040012FE RID: 4862
		[SerializeField]
		private int m_Id;

		// Token: 0x040012FF RID: 4863
		[SerializeField]
		private int m_OrderInDocument;

		// Token: 0x04001300 RID: 4864
		[SerializeField]
		private int m_ParentId;

		// Token: 0x04001301 RID: 4865
		[SerializeField]
		private List<UxmlNamespaceDefinition> m_NamespaceDefinitions;

		// Token: 0x04001302 RID: 4866
		[SerializeField]
		protected List<string> m_Properties;
	}
}
