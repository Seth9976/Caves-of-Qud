using System;
using Kobold;
using Qud.UI;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014FB RID: 5371
	public class SmallIconBreadcrumb : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1BD RID: 53693 RVA: 0x004DE8AC File Offset: 0x004DCAAC
		public void setData(FrameworkDataElement data)
		{
			ChoiceWithColorIcon choiceWithColorIcon = data as ChoiceWithColorIcon;
			if (choiceWithColorIcon == null)
			{
				throw new ArgumentException("TitledBigIconButton expected ChoiceWithColorIcon data");
			}
			ImageTinyFrame imageTinyFrame = base.GetComponent<ImageTinyFrame>() ?? base.GetComponent<TitledIconButton>().ImageTinyFrame;
			base.GetComponent<TitledIconButton>().SetTitle(choiceWithColorIcon.Title);
			if (string.IsNullOrEmpty(choiceWithColorIcon.IconPath))
			{
				imageTinyFrame.sprite = null;
			}
			else
			{
				imageTinyFrame.sprite = SpriteManager.GetUnitySprite(choiceWithColorIcon.IconPath);
			}
			if (imageTinyFrame.ThreeColor)
			{
				imageTinyFrame.ThreeColor.SetHFlip(choiceWithColorIcon.HFlip);
				imageTinyFrame.ThreeColor.SetVFlip(choiceWithColorIcon.VFlip);
			}
			imageTinyFrame.selectedForegroundColor = choiceWithColorIcon.IconForegroundColor;
			imageTinyFrame.selectedDetailColor = choiceWithColorIcon.IconDetailColor;
			imageTinyFrame.Sync(false);
		}

		// Token: 0x0600D1BE RID: 53694 RVA: 0x004DE96A File Offset: 0x004DCB6A
		public NavigationContext GetNavigationContext()
		{
			FrameworkContext component = base.GetComponent<FrameworkContext>();
			if (component == null)
			{
				return null;
			}
			return component.context;
		}
	}
}
