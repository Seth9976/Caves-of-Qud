using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000583 RID: 1411
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class UxmlObjectAsset : UxmlAsset
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x0009891C File Offset: 0x00096B1C
		public bool isField
		{
			get
			{
				return this.m_IsField;
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00098924 File Offset: 0x00096B24
		public UxmlObjectAsset(string fullTypeNameOrFieldName, bool isField, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
			: base(fullTypeNameOrFieldName, xmlNamespace)
		{
			this.m_IsField = isField;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00098937 File Offset: 0x00096B37
		public override string ToString()
		{
			return this.isField ? string.Format("Reference: {0} (id:{1} parent:{2})", base.fullTypeName, base.id, base.parentId) : base.ToString();
		}

		// Token: 0x04001303 RID: 4867
		[SerializeField]
		private bool m_IsField;
	}
}
