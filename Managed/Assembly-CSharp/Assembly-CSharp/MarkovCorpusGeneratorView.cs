using System;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine.UI;
using XRL.UI;
using XRL.Wish;

// Token: 0x02000091 RID: 145
[UIView("MarkovCorpusGenerator", false, true, false, null, "MarkovCorpusGenerator", false, 0, false, UICanvasHost = 1)]
[HasWishCommand]
public class MarkovCorpusGeneratorView : SingletonWindowBase<MarkovCorpusGeneratorView>
{
	// Token: 0x06000509 RID: 1289 RVA: 0x00021340 File Offset: 0x0001F540
	public override void Show()
	{
		this.generator = base.gameObject.AddComponent<MarkovCorpusGenerator>();
		this.generator.rootObject = base.gameObject;
		base.Show();
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0002136C File Offset: 0x0001F56C
	public void OnCommand(string Command)
	{
		if (Command == "Back")
		{
			UIManager.getWindow("MarkovCorpusGenerator").Hide();
		}
		if (Command == "Generate")
		{
			base.gameObject.transform.Find("ProgressPanel").gameObject.SetActive(true);
			base.gameObject.transform.Find("ProgressPanel/ProgressLabel").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Hotloading game configuration...";
			this.generator.Generate();
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x000213F6 File Offset: 0x0001F5F6
	[WishCommand("corpusgenerator", null)]
	public static void WishDisplay()
	{
		GameManager.Instance.PushGameView("MarkovCorpusGenerator", true);
		Keyboard.getvk(false, false, true);
	}

	// Token: 0x040003E4 RID: 996
	private MarkovCorpusGenerator generator;
}
