using System;
using System.Collections.Generic;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.Core;
using XRL.UI;
using XRL.UI.Framework;

// Token: 0x0200007C RID: 124
public class HighScoresRow : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
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

	// Token: 0x06000497 RID: 1175 RVA: 0x0001E0FC File Offset: 0x0001C2FC
	public void SetupContexts(ScrollChildContext context)
	{
		this.context.context = context;
		NavigationContext context2 = this.revisitCodaButton.context;
		if (((context2 != null) ? context2.parentContext : null) != context)
		{
			FrameworkContext frameworkContext = this.revisitCodaButton;
			if (frameworkContext.context == null)
			{
				frameworkContext.context = new NavigationContext();
			}
			this.revisitCodaButton.context.parentContext = context;
		}
		NavigationContext context3 = this.deleteButton.context;
		if (((context3 != null) ? context3.parentContext : null) != context)
		{
			FrameworkContext frameworkContext = this.deleteButton;
			if (frameworkContext.context == null)
			{
				frameworkContext.context = new NavigationContext();
			}
			this.deleteButton.context.parentContext = context;
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0001E1A3 File Offset: 0x0001C3A3
	public void HandleDelete()
	{
		FrameworkContext frameworkContext = this.deleteButton;
		if (frameworkContext == null)
		{
			return;
		}
		frameworkContext.context.buttonHandlers[InputButtonTypes.AcceptButton]();
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001E1C5 File Offset: 0x0001C3C5
	public void HandleRevisitCoda()
	{
		if (this.hasCoda)
		{
			FrameworkContext frameworkContext = this.revisitCodaButton;
			if (frameworkContext == null)
			{
				return;
			}
			frameworkContext.context.buttonHandlers[InputButtonTypes.AcceptButton]();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001E1F0 File Offset: 0x0001C3F0
	public void setData(FrameworkDataElement data)
	{
		HighScoresDataElement highScoresDataElement = data as HighScoresDataElement;
		if (highScoresDataElement != null)
		{
			ScoreEntry2 entry = highScoresDataElement.entry;
			string[] array;
			if (entry == null)
			{
				array = null;
			}
			else
			{
				string details = entry.Details;
				array = ((details != null) ? details.Split(new char[] { '\n' }, 7, StringSplitOptions.RemoveEmptyEntries) : null);
			}
			string[] array2;
			if ((array2 = array) == null)
			{
				(array2 = new string[1])[0] = "";
			}
			string[] array3 = array2;
			this.hasCoda = highScoresDataElement.entry.HasCoda();
			this.TextSkins[0].SetText(string.Format("{{{{{0}|{1} :: Level {2} :: {3}}}}}", new object[]
			{
				this.hasCoda ? "W" : "C",
				highScoresDataElement.entry.Name,
				highScoresDataElement.entry.Level,
				highScoresDataElement.entry.GameMode
			}));
			int i = 1;
			int num = 0;
			while (i < this.TextSkins.Count)
			{
				this.TextSkins[i].SetText((array3.Length <= i) ? "" : array3[i + num]);
				if (i == 2)
				{
					num++;
				}
				i++;
			}
			this.wasSelected = null;
			this.Update();
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001E318 File Offset: 0x0001C518
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
			this.revisitCodaButton.gameObject.SetActive(valueOrDefault && this.hasCoda);
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
					uitextSkin.color = ((flag4 && this.hasCoda) ? The.Color.Yellow : (flag4 ? The.Color.DarkCyan : The.Color.Black));
					uitextSkin.StripFormatting = true;
				}
				uitextSkin.Apply();
				flag4 = false;
			}
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001E490 File Offset: 0x0001C690
	public NavigationContext GetNavigationContext()
	{
		return null;
	}

	// Token: 0x0400037F RID: 895
	public List<UITextSkin> TextSkins;

	// Token: 0x04000380 RID: 896
	public Image background;

	// Token: 0x04000381 RID: 897
	public FrameworkContext deleteButton;

	// Token: 0x04000382 RID: 898
	public FrameworkContext revisitCodaButton;

	// Token: 0x04000383 RID: 899
	public GameObject revisitCodaButtonObject;

	// Token: 0x04000384 RID: 900
	private FrameworkContext _context;

	// Token: 0x04000385 RID: 901
	private bool hasCoda;

	// Token: 0x04000386 RID: 902
	private bool? wasSelected;
}
