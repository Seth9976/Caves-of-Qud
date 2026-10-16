using System;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x0200147C RID: 5244
	public class ObjectFinderLine : MonoBehaviour, IFrameworkControl, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x0600CE89 RID: 52873 RVA: 0x004B20EE File Offset: 0x004B02EE
		public NavigationContext GetNavigationContext()
		{
			return this.navContextHolder.context;
		}

		// Token: 0x0600CE8A RID: 52874 RVA: 0x004B20FB File Offset: 0x004B02FB
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.GetNavigationContext().IsActive(true) && !SingletonWindowBase<NearbyItemsWindow>.instance.KeyboardMode)
			{
				NavigationController.instance.activeContext = null;
			}
		}

		// Token: 0x0600CE8B RID: 52875 RVA: 0x004B2124 File Offset: 0x004B0324
		public void setData(FrameworkDataElement data)
		{
			this.lastData = null;
			ObjectFinderLine.Data data2 = data as ObjectFinderLine.Data;
			if (data2 != null)
			{
				this.lastData = data2;
				if (string.IsNullOrWhiteSpace(data2.RightText))
				{
					this.RightText.gameObject.SetActive(false);
				}
				else
				{
					this.RightText.gameObject.SetActive(true);
					this.RightText.SetText(data2.RightText);
				}
				if (string.IsNullOrWhiteSpace(data2.PrefixText))
				{
					this.PrefixText.gameObject.SetActive(false);
				}
				else
				{
					this.PrefixText.gameObject.SetActive(true);
					this.PrefixText.SetText(data2.PrefixText);
				}
				this.ObjectDescription.SetText(data2.Description);
				if (data2.Icon == null)
				{
					this.ObjectIcon.gameObject.SetActive(false);
					return;
				}
				this.ObjectIcon.gameObject.SetActive(true);
				this.ObjectIcon.FromRenderable(data2.Icon, true);
			}
		}

		// Token: 0x04005020 RID: 20512
		public UIThreeColorProperties ObjectIcon;

		// Token: 0x04005021 RID: 20513
		public UITextSkin PrefixText;

		// Token: 0x04005022 RID: 20514
		public UITextSkin ObjectDescription;

		// Token: 0x04005023 RID: 20515
		public UITextSkin RightText;

		// Token: 0x04005024 RID: 20516
		public FrameworkContext navContextHolder;

		// Token: 0x04005025 RID: 20517
		public ObjectFinderLine.Data lastData;

		// Token: 0x02001D96 RID: 7574
		public class Data : FrameworkDataElement
		{
			// Token: 0x04007364 RID: 29540
			public string RightText;

			// Token: 0x04007365 RID: 29541
			public string PrefixText;

			// Token: 0x04007366 RID: 29542
			public Renderable Icon;

			// Token: 0x04007367 RID: 29543
			public XRL.World.GameObject go;

			// Token: 0x04007368 RID: 29544
			public ObjectFinder.Context context;
		}
	}
}
