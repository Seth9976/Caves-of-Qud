using System;
using System.Collections.Generic;
using System.Threading;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

// Token: 0x0200007D RID: 125
public class SteamScoresRow : MonoBehaviour, IFrameworkControl
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001E49C File Offset: 0x0001C69C
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

	// Token: 0x0600049F RID: 1183 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
	public void setData(FrameworkDataElement data)
	{
		HighScoresDataElement highScoresDataElement = data as HighScoresDataElement;
		if (highScoresDataElement != null)
		{
			if (highScoresDataElement.message != null)
			{
				this.TextSkins[0].SetText(highScoresDataElement.message);
				this.TextSkins[1].SetText("");
			}
			else
			{
				this.cancelLast.Cancel();
				this.cancelLast = new CancellationTokenSource();
				this.showSteamRow(highScoresDataElement, this.cancelLast.Token);
			}
			this.wasSelected = null;
			this.Update();
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001E550 File Offset: 0x0001C750
	public async void showSteamRow(HighScoresDataElement scoreData, CancellationToken ct)
	{
		if (!ct.IsCancellationRequested)
		{
			LeaderboardEntry entry = scoreData.leaderboardEntry ?? LeaderboardEntry.GenerateEmpty();
			this.isSelf = entry.isCurrentUser;
			this.TextSkins[0].SetText(string.Format("{{{{Y|{0})}}}} {{{{{1}|{2}}}}}", entry.rank, this.isSelf ? 'W' : 'y', entry.displayName));
			this.TextSkins[1].SetText(string.Format("{0}", entry.score));
			string text = await LeaderboardManager.FriendNameAsync(scoreData.leaderboardEntry);
			if (!ct.IsCancellationRequested)
			{
				this.TextSkins[0].SetText(string.Format("{{{{Y|{0})}}}} {{{{{1}|{2}}}}}", entry.rank, this.isSelf ? 'W' : 'y', text));
			}
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0001E598 File Offset: 0x0001C798
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
			Color darkCyan = The.Color.DarkCyan;
			darkCyan.a = (valueOrDefault ? 0.25f : 0f);
			this.background.color = darkCyan;
			foreach (UITextSkin uitextSkin in this.TextSkins)
			{
				if (valueOrDefault)
				{
					uitextSkin.color = (this.isSelf ? The.Color.Yellow : The.Color.Gray);
					uitextSkin.StripFormatting = false;
				}
				else
				{
					uitextSkin.color = (this.isSelf ? The.Color.Yellow : The.Color.Cyan);
					uitextSkin.StripFormatting = true;
				}
				uitextSkin.Apply();
			}
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
	public NavigationContext GetNavigationContext()
	{
		return this.context.context;
	}

	// Token: 0x04000387 RID: 903
	public List<UITextSkin> TextSkins;

	// Token: 0x04000388 RID: 904
	public Image background;

	// Token: 0x04000389 RID: 905
	private FrameworkContext _context;

	// Token: 0x0400038A RID: 906
	public CancellationTokenSource cancelLast = new CancellationTokenSource();

	// Token: 0x0400038B RID: 907
	private bool? wasSelected;

	// Token: 0x0400038C RID: 908
	public bool isSelf;
}
