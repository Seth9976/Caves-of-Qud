using System;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI.Framework;

// Token: 0x0200007E RID: 126
public class MainMenuRow : MonoBehaviour, IFrameworkControl
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001E700 File Offset: 0x0001C900
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

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001E728 File Offset: 0x0001C928
	public void setData(FrameworkDataElement data)
	{
		this.data = null;
		MainMenuOptionData mainMenuOptionData = data as MainMenuOptionData;
		if (mainMenuOptionData != null)
		{
			this.data = mainMenuOptionData;
			this.buttonenabled = mainMenuOptionData.Enabled;
			this.text.text = mainMenuOptionData.Text;
			this.alertMode = mainMenuOptionData.Alert;
			this.wasSelected = null;
			this.Update();
		}
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001E788 File Offset: 0x0001C988
	public void Update()
	{
		if (this.orig == Color.magenta)
		{
			this.orig = this.text.color;
		}
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
			float num = (this.data.Enabled ? 1f : 0.6f);
			if (valueOrDefault)
			{
				this.text.color = new Color(this.orig.r * num, this.orig.g * num, this.orig.b * num, 1f);
			}
			else
			{
				this.text.color = new Color(this.orig.r * num, this.orig.g * num, this.orig.b * num, 0.35f);
			}
		}
		if (this.alertMode == MainMenuOptionData.AlertMode.ModStatus && this.alert != null)
		{
			if (ModManager.IsAnyModFailed())
			{
				if (!this.alert.gameObject.activeInHierarchy)
				{
					this.alert.gameObject.SetActive(true);
					this.alert.color = Color.red;
					return;
				}
			}
			else if (ModManager.IsAnyModMissingDependency())
			{
				if (!this.alert.gameObject.activeInHierarchy)
				{
					this.alert.gameObject.SetActive(true);
					this.alert.color = Color.yellow;
					return;
				}
			}
			else if (ModManager.IsScriptingUndetermined())
			{
				if (!this.alert.gameObject.activeInHierarchy)
				{
					this.alert.gameObject.SetActive(true);
					this.alert.color = Color.white;
					return;
				}
			}
			else if (this.alert.gameObject.activeInHierarchy)
			{
				this.alert.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001E9AD File Offset: 0x0001CBAD
	public NavigationContext GetNavigationContext()
	{
		return null;
	}

	// Token: 0x0400038D RID: 909
	public Image alert;

	// Token: 0x0400038E RID: 910
	public Text text;

	// Token: 0x0400038F RID: 911
	private FrameworkContext _context;

	// Token: 0x04000390 RID: 912
	public MainMenuOptionData.AlertMode alertMode;

	// Token: 0x04000391 RID: 913
	public bool buttonenabled;

	// Token: 0x04000392 RID: 914
	public MainMenuOptionData data;

	// Token: 0x04000393 RID: 915
	private bool? wasSelected;

	// Token: 0x04000394 RID: 916
	private Color orig = Color.magenta;
}
