using System;
using System.Collections.Generic;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000169 RID: 361
	public class PopupWindow : TextElement
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000332DC File Offset: 0x000314DC
		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0003333C File Offset: 0x0003153C
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x040006D3 RID: 1747
		private VisualElement m_ContentContainer;

		// Token: 0x040006D4 RID: 1748
		public new static readonly string ussClassName = "unity-popup-window";

		// Token: 0x040006D5 RID: 1749
		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		// Token: 0x0200016A RID: 362
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextElement.UxmlSerializedData
		{
			// Token: 0x06000BA7 RID: 2983 RVA: 0x00033374 File Offset: 0x00031574
			public override object CreateInstance()
			{
				return new PopupWindow();
			}
		}

		// Token: 0x0200016B RID: 363
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
		}

		// Token: 0x0200016C RID: 364
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00033384 File Offset: 0x00031584
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}
		}
	}
}
