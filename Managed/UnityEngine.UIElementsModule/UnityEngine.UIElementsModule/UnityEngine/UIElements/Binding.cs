using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001D RID: 29
	[UxmlObject]
	public abstract class Binding
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000034C6 File Offset: 0x000016C6
		public static void SetGlobalLogLevel(BindingLogLevel logLevel)
		{
			DataBindingManager.globalLogLevel = logLevel;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034D0 File Offset: 0x000016D0
		public static void SetPanelLogLevel(IPanel panel, BindingLogLevel logLevel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel != null;
			if (flag)
			{
				baseVisualElementPanel.dataBindingManager.logLevel = logLevel;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000034FC File Offset: 0x000016FC
		public static void ResetPanelLogLevel(IPanel panel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel != null;
			if (flag)
			{
				baseVisualElementPanel.dataBindingManager.ResetLogLevel();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003527 File Offset: 0x00001727
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000352F File Offset: 0x0000172F
		internal string property
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get;
			set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003538 File Offset: 0x00001738
		public bool isDirty
		{
			get
			{
				return this.m_Dirty;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003540 File Offset: 0x00001740
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003548 File Offset: 0x00001748
		[CreateProperty]
		public BindingUpdateTrigger updateTrigger
		{
			get
			{
				return this.m_UpdateTrigger;
			}
			set
			{
				this.m_UpdateTrigger = value;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003551 File Offset: 0x00001751
		internal Binding()
		{
			this.m_Dirty = true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003562 File Offset: 0x00001762
		public void MarkDirty()
		{
			this.m_Dirty = true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000356C File Offset: 0x0000176C
		internal void ClearDirty()
		{
			this.m_Dirty = false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002152 File Offset: 0x00000352
		protected internal virtual void OnActivated(in BindingActivationContext context)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002152 File Offset: 0x00000352
		protected internal virtual void OnDeactivated(in BindingActivationContext context)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002152 File Offset: 0x00000352
		protected internal virtual void OnDataSourceChanged(in DataSourceContextChanged context)
		{
		}

		// Token: 0x04000037 RID: 55
		private bool m_Dirty;

		// Token: 0x04000038 RID: 56
		private BindingUpdateTrigger m_UpdateTrigger;

		// Token: 0x0400003A RID: 58
		internal const string k_UpdateTriggerTooltip = "This informs the binding system of whether the binding object should be updated on every frame, when a change occurs in the source or on every frame if change detection is impossible, and when explicitly marked as dirty.";

		// Token: 0x0200001E RID: 30
		[ExcludeFromDocs]
		[Serializable]
		public abstract class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x0600009E RID: 158 RVA: 0x00003578 File Offset: 0x00001778
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Binding.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("property", "property", null, Array.Empty<string>()),
					new UxmlAttributeNames("updateTrigger", "update-trigger", null, Array.Empty<string>())
				});
			}

			// Token: 0x0600009F RID: 159 RVA: 0x000035D4 File Offset: 0x000017D4
			public override void Deserialize(object obj)
			{
				Binding binding = (Binding)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.property_UxmlAttributeFlags);
				if (flag)
				{
					binding.property = this.property;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.updateTrigger_UxmlAttributeFlags);
				if (flag2)
				{
					binding.updateTrigger = this.updateTrigger;
				}
			}

			// Token: 0x0400003B RID: 59
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			[HideInInspector]
			[SerializeField]
			internal string property;

			// Token: 0x0400003C RID: 60
			[Tooltip("This informs the binding system of whether the binding object should be updated on every frame, when a change occurs in the source or on every frame if change detection is impossible, and when explicitly marked as dirty.")]
			[SerializeField]
			[HideInInspector]
			private BindingUpdateTrigger updateTrigger;

			// Token: 0x0400003D RID: 61
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags property_UxmlAttributeFlags;

			// Token: 0x0400003E RID: 62
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags updateTrigger_UxmlAttributeFlags;
		}
	}
}
