using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000018 RID: 24
	public class BindableElement : VisualElement, IBindable
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000339E File Offset: 0x0000159E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000033A6 File Offset: 0x000015A6
		public IBinding binding { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000033AF File Offset: 0x000015AF
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000033B7 File Offset: 0x000015B7
		public string bindingPath { get; set; }

		// Token: 0x0400002D RID: 45
		internal const string k_BindingPathTooltip = "Default method to define a path to a serialized property. Most often used for Editor extensions and inspectors.";

		// Token: 0x02000019 RID: 25
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000089 RID: 137 RVA: 0x000033C9 File Offset: 0x000015C9
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(BindableElement.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("bindingPath", "binding-path", null, Array.Empty<string>())
				});
			}

			// Token: 0x0600008A RID: 138 RVA: 0x000033FE File Offset: 0x000015FE
			public override object CreateInstance()
			{
				return new BindableElement();
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00003408 File Offset: 0x00001608
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.bindingPath_UxmlAttributeFlags);
				if (flag)
				{
					BindableElement bindableElement = (BindableElement)obj;
					bindableElement.bindingPath = this.bindingPath;
				}
			}

			// Token: 0x04000030 RID: 48
			[BindingPathDrawer]
			[Tooltip("Default method to define a path to a serialized property. Most often used for Editor extensions and inspectors.")]
			[SerializeField]
			private string bindingPath;

			// Token: 0x04000031 RID: 49
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags bindingPath_UxmlAttributeFlags;
		}

		// Token: 0x0200001A RID: 26
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BindableElement, BindableElement.UxmlTraits>
		{
		}

		// Token: 0x0200001B RID: 27
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x0600008E RID: 142 RVA: 0x00003455 File Offset: 0x00001655
			public UxmlTraits()
			{
				this.m_PropertyPath = new UxmlStringAttributeDescription
				{
					name = "binding-path"
				};
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003478 File Offset: 0x00001678
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				string valueFromBag = this.m_PropertyPath.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					IBindable bindable = ve as IBindable;
					bool flag2 = bindable != null;
					if (flag2)
					{
						bindable.bindingPath = valueFromBag;
					}
				}
			}

			// Token: 0x04000032 RID: 50
			private UxmlStringAttributeDescription m_PropertyPath;
		}
	}
}
