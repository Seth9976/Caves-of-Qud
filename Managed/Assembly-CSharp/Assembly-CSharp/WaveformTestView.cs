using System;
using Genkit;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL.Rules;
using XRL.UI;

// Token: 0x020000A5 RID: 165
[UIView("WaveformTest", false, false, false, null, null, false, 0, false, UICanvas = "WaveformTest", UICanvasHost = 1)]
public class WaveformTestView : SingletonWindowBase<WaveformTestView>
{
	// Token: 0x06000581 RID: 1409 RVA: 0x00023F46 File Offset: 0x00022146
	public override void Show()
	{
		base.Show();
		base.FindChild("Controls/Script").GetComponent<InputField>().text = "pass:temple,n=3,width=78,height=23";
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00023F68 File Offset: 0x00022168
	public void OnCommand(string Command)
	{
		if (Command == "Reseed To Static")
		{
			this.seed = 0;
			Stat.ReseedFrom(0, false);
		}
		if (Command == "Back")
		{
			UIManager.getWindow("WaveformTest").Hide();
			UIManager.showWindow("MainMenu", false);
		}
		if (Command == "TTest")
		{
			WaveCollapseTools.LoadTemplates(true);
			foreach (WaveTemplateEntry waveTemplateEntry in WaveCollapseTools.waveTemplates.Values)
			{
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(waveTemplateEntry, 3, 80, 25, true, false, 8, 0);
				waveCollapseFastModel.Run(this.seed, 0);
				this.seed++;
				if (waveCollapseFastModel.T > 350)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"template ",
						waveTemplateEntry.name,
						" T=",
						waveCollapseFastModel.T.ToString(),
						" exceeds 350"
					}));
				}
			}
		}
		if (Command == "Generate Fast")
		{
			WaveCollapseTools.LoadTemplates(true);
			Image component = base.FindChild("Controls/Image").GetComponent<Image>();
			WaveCollapseFastModel waveCollapseFastModel2 = null;
			string[] array = base.FindChild("Controls/Script").GetComponent<InputField>().text.Split('\n', StringSplitOptions.None);
			int num = 80;
			int num2 = 25;
			int num3 = 3;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':', StringSplitOptions.None);
				if (array2[0] == "pass")
				{
					string[] array3 = array2[1].Split(',', StringSplitOptions.None);
					for (int j = 0; j < array3.Length; j++)
					{
						if (array3[j].StartsWith("n="))
						{
							num3 = Convert.ToInt32(array3[j].Split('=', StringSplitOptions.None)[1]);
						}
						if (array3[j].StartsWith("width="))
						{
							num = Convert.ToInt32(array3[j].Split('=', StringSplitOptions.None)[1]);
						}
						if (array3[j].StartsWith("height="))
						{
							num2 = Convert.ToInt32(array3[j].Split('=', StringSplitOptions.None)[1]);
						}
					}
					if (waveCollapseFastModel2 == null)
					{
						waveCollapseFastModel2 = new WaveCollapseFastModel(array3[0], num3, num, num2, true, false, 8, 0);
					}
					else
					{
						waveCollapseFastModel2.UpdateSample(WaveCollapseTools.waveTemplates[array3[0]], num3, true, false, 8, 0);
					}
					waveCollapseFastModel2.Run(this.seed, 0);
					this.seed++;
				}
				else if (array2[0] == "clear")
				{
					waveCollapseFastModel2.ClearColors(array2[1], "all");
				}
				else if (array2[0].StartsWith("clear-"))
				{
					waveCollapseFastModel2.ClearColors(array2[1], array2[0].Split('-', StringSplitOptions.None)[1]);
				}
			}
			Debug.Log("model T=" + waveCollapseFastModel2.T.ToString());
			Texture2D texture2D = new Texture2D(waveCollapseFastModel2.FMX, waveCollapseFastModel2.FMY, TextureFormat.ARGB32, false);
			texture2D.filterMode = FilterMode.Point;
			texture2D.SetPixels32(waveCollapseFastModel2.GetResult());
			texture2D.Apply();
			Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
			component.sprite = sprite;
		}
		if (Command == "Generate")
		{
			WaveCollapseTools.LoadTemplates(false);
			Image component2 = base.FindChild("Controls/Image").GetComponent<Image>();
			MultipassModel multipassModel = null;
			string[] array4 = base.FindChild("Controls/Script").GetComponent<InputField>().text.Split('\n', StringSplitOptions.None);
			int num4 = 80;
			int num5 = 25;
			int num6 = 3;
			for (int k = 0; k < array4.Length; k++)
			{
				string[] array5 = array4[k].Split(':', StringSplitOptions.None);
				if (array5[0] == "pass")
				{
					string[] array6 = array5[1].Split(',', StringSplitOptions.None);
					for (int l = 0; l < array6.Length; l++)
					{
						if (array6[l].StartsWith("n="))
						{
							num6 = Convert.ToInt32(array6[l].Split('=', StringSplitOptions.None)[1]);
						}
						if (array6[l].StartsWith("width="))
						{
							num4 = Convert.ToInt32(array6[l].Split('=', StringSplitOptions.None)[1]);
						}
						if (array6[l].StartsWith("height="))
						{
							num5 = Convert.ToInt32(array6[l].Split('=', StringSplitOptions.None)[1]);
						}
					}
					if (multipassModel == null)
					{
						multipassModel = new MultipassModel(WaveCollapseTools.waveTemplates[array6[0]], num6, num4, num5, true, false, 8, 0);
					}
					else
					{
						multipassModel.UpdateSample(WaveCollapseTools.waveTemplates[array6[0]], num6, true, false, 8, 0);
					}
					multipassModel.Run(this.seed, 0);
					this.seed++;
				}
				else if (array5[0] == "clear")
				{
					multipassModel.ClearColors(array5[1], "all");
				}
				else if (array5[0].StartsWith("clear-"))
				{
					multipassModel.ClearColors(array5[1], array5[0].Split('-', StringSplitOptions.None)[1]);
				}
			}
			Texture2D texture2D2 = new Texture2D(multipassModel.FMX, multipassModel.FMY, TextureFormat.ARGB32, false);
			texture2D2.filterMode = FilterMode.Point;
			texture2D2.SetPixels32(multipassModel.GetResult());
			texture2D2.Apply();
			Sprite sprite2 = Sprite.Create(texture2D2, new Rect(0f, 0f, (float)texture2D2.width, (float)texture2D2.height), new Vector2(0f, 0f));
			component2.sprite = sprite2;
		}
	}

	// Token: 0x0400043B RID: 1083
	public int seed;
}
