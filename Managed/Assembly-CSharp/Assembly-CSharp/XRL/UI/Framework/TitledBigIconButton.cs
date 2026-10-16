using System;
using Kobold;
using Qud.UI;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001503 RID: 5379
	public class TitledBigIconButton : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1D9 RID: 53721 RVA: 0x004DEEA4 File Offset: 0x004DD0A4
		public void setData(FrameworkDataElement data)
		{
			ChoiceWithColorIcon choiceWithColorIcon = data as ChoiceWithColorIcon;
			if (choiceWithColorIcon == null)
			{
				throw new ArgumentException("TitledBigIconButton expected ChoiceWithColorIcon data");
			}
			ImageTinyFrame imageTinyFrame = base.GetComponent<ImageTinyFrame>() ?? base.GetComponent<TitledIconButton>().ImageTinyFrame;
			base.GetComponent<TitledIconButton>().SetTitle(choiceWithColorIcon.Title + (string.IsNullOrEmpty(choiceWithColorIcon.Hotkey) ? "" : ("\n" + choiceWithColorIcon.Hotkey)));
			imageTinyFrame.sprite = SpriteManager.GetUnitySprite(choiceWithColorIcon.IconPath);
			if (imageTinyFrame.ThreeColor)
			{
				imageTinyFrame.ThreeColor.SetHFlip(choiceWithColorIcon.HFlip);
				imageTinyFrame.ThreeColor.SetVFlip(choiceWithColorIcon.VFlip);
			}
			imageTinyFrame.unselectedBorderColor = (choiceWithColorIcon.IsChosen() ? The.Color.Cyan : The.Color.Black);
			imageTinyFrame.selectedBorderColor = (choiceWithColorIcon.IsChosen() ? The.Color.Yellow : The.Color.Yellow);
			imageTinyFrame.selectedForegroundColor = choiceWithColorIcon.IconForegroundColor;
			imageTinyFrame.selectedDetailColor = choiceWithColorIcon.IconDetailColor;
			imageTinyFrame.Sync(true);
		}

		// Token: 0x0600D1DA RID: 53722 RVA: 0x004DEFBD File Offset: 0x004DD1BD
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
