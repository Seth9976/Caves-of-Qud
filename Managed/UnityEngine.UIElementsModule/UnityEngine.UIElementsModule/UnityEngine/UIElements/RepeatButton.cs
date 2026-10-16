using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017D RID: 381
	public class RepeatButton : TextElement
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000349AF File Offset: 0x00032BAF
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x000349B8 File Offset: 0x00032BB8
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_AcceptClicksIfDisabled;
			}
			set
			{
				bool flag = this.m_AcceptClicksIfDisabled == value;
				if (!flag)
				{
					this.m_AcceptClicksIfDisabled = value;
					bool flag2 = this.m_Clickable != null;
					if (flag2)
					{
						this.m_Clickable.acceptClicksIfDisabled = value;
					}
				}
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000349F6 File Offset: 0x00032BF6
		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00034A0C File Offset: 0x00032C0C
		public RepeatButton(Action clickEvent, long delay, long interval)
			: this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00034A20 File Offset: 0x00032C20
		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new Clickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00034A4B File Offset: 0x00032C4B
		internal void AddAction(Action clickEvent)
		{
			this.m_Clickable.clicked += clickEvent;
		}

		// Token: 0x0400071A RID: 1818
		private Clickable m_Clickable;

		// Token: 0x0400071B RID: 1819
		private bool m_AcceptClicksIfDisabled;

		// Token: 0x0400071C RID: 1820
		public new static readonly string ussClassName = "unity-repeat-button";

		// Token: 0x0200017E RID: 382
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextElement.UxmlSerializedData
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x00034A68 File Offset: 0x00032C68
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(RepeatButton.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("delay", "delay", null, Array.Empty<string>()),
					new UxmlAttributeNames("interval", "interval", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x00034AC4 File Offset: 0x00032CC4
			public override object CreateInstance()
			{
				return new RepeatButton();
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x00034ACC File Offset: 0x00032CCC
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.delay_UxmlAttributeFlags) || UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.interval_UxmlAttributeFlags);
				if (flag)
				{
					RepeatButton repeatButton = (RepeatButton)obj;
					repeatButton.SetAction(null, this.delay, this.interval);
				}
			}

			// Token: 0x0400071D RID: 1821
			[SerializeField]
			private long delay;

			// Token: 0x0400071E RID: 1822
			[SerializeField]
			private long interval;

			// Token: 0x0400071F RID: 1823
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags delay_UxmlAttributeFlags;

			// Token: 0x04000720 RID: 1824
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags interval_UxmlAttributeFlags;
		}

		// Token: 0x0200017F RID: 383
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
		}

		// Token: 0x02000180 RID: 384
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000C11 RID: 3089 RVA: 0x00034B28 File Offset: 0x00032D28
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000721 RID: 1825
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			// Token: 0x04000722 RID: 1826
			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};
		}
	}
}
