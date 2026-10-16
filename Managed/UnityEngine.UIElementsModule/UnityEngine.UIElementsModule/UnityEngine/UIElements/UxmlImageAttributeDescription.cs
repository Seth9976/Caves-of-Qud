using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000580 RID: 1408
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UxmlImageAttributeDescription : UxmlAttributeDescription, IUxmlAssetAttributeDescription
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x0009837C File Offset: 0x0009657C
		public UxmlImageAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			this.defaultValue = default(Background);
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000983B9 File Offset: 0x000965B9
		// (set) Token: 0x06002A41 RID: 10817 RVA: 0x000983C1 File Offset: 0x000965C1
		public Background defaultValue { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000983CC File Offset: 0x000965CC
		public override string defaultValueAsString
		{
			get
			{
				return this.defaultValue.IsEmpty() ? "null" : this.defaultValue.ToString();
			}
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x00098404 File Offset: 0x00096604
		public Background GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			string text;
			VisualTreeAsset visualTreeAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out text, out visualTreeAsset) && visualTreeAsset != null;
			Background background;
			if (flag)
			{
				bool flag2 = text == null;
				if (flag2)
				{
					background = default(Background);
				}
				else
				{
					bool flag3 = this.m_AssetType == null;
					if (flag3)
					{
						this.m_AssetType = visualTreeAsset.GetAssetType(text);
					}
					background = Background.FromObject(visualTreeAsset.GetAsset(text, this.m_AssetType));
				}
			}
			else
			{
				background = default(Background);
			}
			return background;
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0009848A File Offset: 0x0009668A
		Type IUxmlAssetAttributeDescription.assetType
		{
			get
			{
				return this.m_AssetType ?? typeof(Texture);
			}
		}

		// Token: 0x040012F6 RID: 4854
		private Type m_AssetType;
	}
}
