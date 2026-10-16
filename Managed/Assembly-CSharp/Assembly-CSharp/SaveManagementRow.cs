using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Kobold;
using Qud.API;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

// Token: 0x02000083 RID: 131
public class SaveManagementRow : MonoBehaviour, IFrameworkControl
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001FC64 File Offset: 0x0001DE64
	public FrameworkContext context
	{
		get
		{
			FrameworkContext frameworkContext;
			if ((frameworkContext = this._context) == null)
			{
				frameworkContext = (this._context = base.GetComponent<FrameworkContext>());
			}
			return frameworkContext;
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0001FC8C File Offset: 0x0001DE8C
	public void setData(FrameworkDataElement data)
	{
		FrameworkContext frameworkContext = this.deleteButton;
		if (frameworkContext.context == null)
		{
			frameworkContext.context = new NavigationContext();
		}
		this.deleteButton.context.parentContext = this.context.context;
		SaveInfoData saveInfoData = data as SaveInfoData;
		if (saveInfoData != null)
		{
			SaveGameInfo saveGame = saveInfoData.SaveGame;
			SaveGameJSON saveGameJSON = ((saveGame != null) ? saveGame.json : null);
			ImageTinyFrame imageTinyFrame = this.imageTinyFrame;
			if (saveGameJSON != null)
			{
				imageTinyFrame.sprite = SpriteManager.GetUnitySprite(saveGameJSON.CharIcon);
				imageTinyFrame.unselectedBorderColor = The.Color.Black;
				imageTinyFrame.selectedBorderColor = The.Color.Yellow;
				imageTinyFrame.unselectedForegroundColor = The.Color.Black;
				imageTinyFrame.unselectedDetailColor = The.Color.Black;
				Color color;
				imageTinyFrame.selectedForegroundColor = (ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(saveGameJSON.FColor, out color) ? color : The.Color.Gray);
				Color color2;
				imageTinyFrame.selectedDetailColor = (ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(saveGameJSON.DColor, out color2) ? color2 : The.Color.DarkBlack);
			}
			else
			{
				imageTinyFrame.sprite = SpriteManager.GetUnitySprite("Text/32.bmp");
				imageTinyFrame.unselectedBorderColor = The.Color.Black;
				imageTinyFrame.selectedBorderColor = The.Color.Yellow;
				imageTinyFrame.unselectedForegroundColor = Color.clear;
				imageTinyFrame.unselectedDetailColor = Color.clear;
				imageTinyFrame.selectedForegroundColor = Color.clear;
				imageTinyFrame.selectedDetailColor = Color.clear;
			}
			if (imageTinyFrame.ThreeColor)
			{
				imageTinyFrame.ThreeColor.SetHFlip(true);
			}
			imageTinyFrame.Sync(true);
			this.TextSkins[0].SetText(string.Concat(new string[]
			{
				"{{W|",
				saveInfoData.SaveGame.Name,
				" :: ",
				saveInfoData.SaveGame.Description,
				" }}"
			}));
			this.TextSkins[1].SetText("{{C|Location:}} " + saveInfoData.SaveGame.Info);
			this.TextSkins[2].SetText("{{C|Last saved:}} " + saveInfoData.SaveGame.SaveTime);
			this.TextSkins[3].SetText(string.Concat(new string[]
			{
				"{{K|",
				saveInfoData.SaveGame.Size,
				" {",
				saveInfoData.SaveGame.ID,
				"} }}"
			}));
			this.modsDiffer.SetActive(saveInfoData.SaveGame.DifferentMods());
			this.wasSelected = null;
			this.Update();
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0001FF48 File Offset: 0x0001E148
	public void Update()
	{
		FrameworkContext context = this.context;
		bool? flag;
		if (context == null)
		{
			flag = null;
		}
		else
		{
			NavigationContext context2 = context.context;
			flag = ((context2 != null) ? new bool?(context2.IsActive(true)) : null);
		}
		bool? flag2 = flag;
		bool valueOrDefault = flag2.GetValueOrDefault();
		bool flag3 = valueOrDefault;
		flag2 = this.wasSelected;
		if (!((flag3 == flag2.GetValueOrDefault()) & (flag2 != null)))
		{
			this.wasSelected = new bool?(valueOrDefault);
			this.deleteButton.gameObject.SetActive(valueOrDefault);
			Color darkCyan = The.Color.DarkCyan;
			darkCyan.a = (valueOrDefault ? 0.25f : 0f);
			this.background.color = darkCyan;
			bool flag4 = true;
			foreach (UITextSkin uitextSkin in this.TextSkins)
			{
				if (valueOrDefault)
				{
					uitextSkin.color = The.Color.Gray;
					uitextSkin.StripFormatting = false;
				}
				else
				{
					uitextSkin.color = (flag4 ? The.Color.DarkCyan : The.Color.Black);
					uitextSkin.StripFormatting = true;
				}
				uitextSkin.Apply();
				flag4 = false;
			}
		}
		if (valueOrDefault && ControlManager.GetButtonDown("CmdDelete", false))
		{
			FrameworkContext frameworkContext = this.deleteButton;
			if (frameworkContext == null)
			{
				return;
			}
			frameworkContext.context.commandHandlers["CmdDelete"]();
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x000200C0 File Offset: 0x0001E2C0
	public void handleDelete()
	{
		FrameworkContext frameworkContext = this.deleteButton;
		if (frameworkContext == null)
		{
			return;
		}
		frameworkContext.context.buttonHandlers[InputButtonTypes.AcceptButton]();
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x000200E2 File Offset: 0x0001E2E2
	public NavigationContext GetNavigationContext()
	{
		return null;
	}

	// Token: 0x040003AD RID: 941
	public ImageTinyFrame imageTinyFrame;

	// Token: 0x040003AE RID: 942
	public List<UITextSkin> TextSkins;

	// Token: 0x040003AF RID: 943
	public Image background;

	// Token: 0x040003B0 RID: 944
	public GameObject modsDiffer;

	// Token: 0x040003B1 RID: 945
	public FrameworkContext deleteButton;

	// Token: 0x040003B2 RID: 946
	private FrameworkContext _context;

	// Token: 0x040003B3 RID: 947
	private bool? wasSelected;
}
