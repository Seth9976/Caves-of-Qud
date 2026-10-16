using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Serialization;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E1 RID: 1249
	[UxmlElement("Instance")]
	[HideInInspector]
	public class TemplateContainer : BindableElement
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x0008A1BE File Offset: 0x000883BE
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x0008A1C6 File Offset: 0x000883C6
		[CreateProperty(ReadOnly = true)]
		public string templateId { get; private set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x0008A1CF File Offset: 0x000883CF
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x0008A1D7 File Offset: 0x000883D7
		[CreateProperty(ReadOnly = true)]
		public VisualTreeAsset templateSource
		{
			get
			{
				return this.m_TemplateSource;
			}
			internal set
			{
				this.m_TemplateSource = value;
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0008A1E0 File Offset: 0x000883E0
		public TemplateContainer()
			: this(null)
		{
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0008A1EB File Offset: 0x000883EB
		public TemplateContainer(string templateId)
			: this(templateId, null)
		{
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0008A1F7 File Offset: 0x000883F7
		internal TemplateContainer(string templateId, VisualTreeAsset templateSource)
		{
			this.templateId = templateId;
			this.templateSource = templateSource;
			this.m_ContentContainer = this;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x0008A218 File Offset: 0x00088418
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0008A230 File Offset: 0x00088430
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetContentContainer(VisualElement content)
		{
			this.m_ContentContainer = content;
		}

		// Token: 0x0400112E RID: 4398
		internal static readonly BindingId templateIdProperty = "templateId";

		// Token: 0x0400112F RID: 4399
		internal static readonly BindingId templateSourceProperty = "templateSource";

		// Token: 0x04001131 RID: 4401
		private VisualElement m_ContentContainer;

		// Token: 0x04001132 RID: 4402
		private VisualTreeAsset m_TemplateSource;

		// Token: 0x020004E2 RID: 1250
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06002618 RID: 9752 RVA: 0x0008A25C File Offset: 0x0008845C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TemplateContainer.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("template", "template", null, Array.Empty<string>()),
					new UxmlAttributeNames("templateId", "template", null, new string[] { "template" })
				});
			}

			// Token: 0x06002619 RID: 9753 RVA: 0x0008A2C1 File Offset: 0x000884C1
			public override object CreateInstance()
			{
				return new TemplateContainer();
			}

			// Token: 0x0600261A RID: 9754 RVA: 0x0008A2C8 File Offset: 0x000884C8
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				TemplateContainer templateContainer = (TemplateContainer)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.template_UxmlAttributeFlags);
				if (flag)
				{
					templateContainer.templateSource = this.template;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.templateId_UxmlAttributeFlags);
				if (flag2)
				{
					templateContainer.templateId = this.templateId;
				}
			}

			// Token: 0x04001133 RID: 4403
			[SerializeField]
			private VisualTreeAsset template;

			// Token: 0x04001134 RID: 4404
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags template_UxmlAttributeFlags;

			// Token: 0x04001135 RID: 4405
			[UxmlAttributeBindingPath("templateId")]
			[SerializeField]
			[FormerlySerializedAs("template")]
			[UxmlAttribute("template")]
			[HideInInspector]
			private string templateId;

			// Token: 0x04001136 RID: 4406
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags templateId_UxmlAttributeFlags;
		}

		// Token: 0x020004E3 RID: 1251
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TemplateContainer, TemplateContainer.UxmlTraits>
		{
			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x0600261C RID: 9756 RVA: 0x0008A31D File Offset: 0x0008851D
			public override string uxmlName
			{
				get
				{
					return "Instance";
				}
			}

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x0600261D RID: 9757 RVA: 0x0008A324 File Offset: 0x00088524
			public override string uxmlQualifiedName
			{
				get
				{
					return this.uxmlNamespace + "." + this.uxmlName;
				}
			}

			// Token: 0x04001137 RID: 4407
			internal const string k_ElementName = "Instance";
		}

		// Token: 0x020004E4 RID: 1252
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x0600261F RID: 9759 RVA: 0x0008A348 File Offset: 0x00088548
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06002620 RID: 9760 RVA: 0x0008A368 File Offset: 0x00088568
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TemplateContainer templateContainer = (TemplateContainer)ve;
				templateContainer.templateId = this.m_Template.GetValueFromBag(bag, cc);
				VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
				VisualTreeAsset visualTreeAsset2 = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(templateContainer.templateId) : null);
				bool flag = visualTreeAsset2 == null;
				if (flag)
				{
					templateContainer.Add(new Label(string.Format("Unknown Template: '{0}'", templateContainer.templateId)));
				}
				else
				{
					TemplateAsset templateAsset = bag as TemplateAsset;
					List<TemplateAsset.AttributeOverride> list = ((templateAsset != null) ? templateAsset.attributeOverrides : null);
					List<CreationContext.AttributeOverrideRange> list2 = cc.attributeOverrides;
					bool flag2 = list != null;
					if (flag2)
					{
						bool flag3 = list2 == null;
						if (flag3)
						{
							list2 = new List<CreationContext.AttributeOverrideRange>();
						}
						list2.Add(new CreationContext.AttributeOverrideRange(cc.visualTreeAsset, list));
					}
					templateContainer.templateSource = visualTreeAsset2;
					visualTreeAsset2.CloneTree(ve, new CreationContext(cc.slotInsertionPoints, list2));
				}
				bool flag4 = visualTreeAsset2 == null;
				if (flag4)
				{
					Debug.LogErrorFormat("Could not resolve template with name '{0}'", new object[] { templateContainer.templateId });
				}
			}

			// Token: 0x04001138 RID: 4408
			internal const string k_TemplateAttributeName = "template";

			// Token: 0x04001139 RID: 4409
			private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
			{
				name = "template",
				use = UxmlAttributeDescription.Use.Required
			};
		}
	}
}
