using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000438 RID: 1080
	[ExecuteInEditMode]
	public class HelpRow : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x06002900 RID: 10496 RVA: 0x000D4A74 File Offset: 0x000D2C74
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (scontext != null)
			{
				NavigationContext navigationContext;
				if ((navigationContext = this.categoryContext) == null)
				{
					navigationContext = (this.categoryContext = new NavigationContext());
				}
				scontext.proxyTo = navigationContext;
				this.categoryContext.parentContext = scontext;
				NavigationContext navigationContext2 = this.categoryContext;
				if (navigationContext2.axisHandlers == null)
				{
					navigationContext2.axisHandlers = new Dictionary<InputAxisTypes, Action>
					{
						{
							InputAxisTypes.NavigationPageYAxis,
							new Action(this.HandleUpDown)
						},
						{
							InputAxisTypes.NavigationYAxis,
							new Action(this.HandleUpDown)
						}
					};
				}
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000D4AF0 File Offset: 0x000D2CF0
		public void HandleUpDown()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			int? num = currentEvent.axisValue;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				if (ScrollViewCalcs.GetScrollViewCalcs(base.transform as RectTransform, this._calcs).isAnyBelowView)
				{
					this._calcs.ScrollPageDown();
					currentEvent.Handle();
					return;
				}
			}
			else
			{
				num = currentEvent.axisValue;
				num2 = 0;
				if (((num.GetValueOrDefault() < num2) & (num != null)) && ScrollViewCalcs.GetScrollViewCalcs(base.transform as RectTransform, this._calcs).isAnyAboveView)
				{
					this._calcs.ScrollPageUp();
					currentEvent.Handle();
				}
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000D4B9A File Offset: 0x000D2D9A
		public NavigationContext GetNavigationContext()
		{
			return this.frameworkContext.context;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000D4BA8 File Offset: 0x000D2DA8
		public void setData(FrameworkDataElement data)
		{
			HelpDataRow helpDataRow = data as HelpDataRow;
			if (helpDataRow != null)
			{
				this.categoryDescription.text = "{{C|" + helpDataRow.Description.ToUpper() + "}}";
				this.categoryDescription.Apply();
				this.description.text = helpDataRow.HelpText;
				if (HelpRow.keysByLength == null)
				{
					HelpRow.keysByLength = CommandBindingManager.CommandBindings.Keys.ToList<string>();
					HelpRow.keysByLength.Sort((string a, string b) => b.Length - a.Length);
				}
				if (this.description.text.Contains("~"))
				{
					if (this.description.text.Contains("~Highlight") && ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad)
					{
						this.description.text = this.description.text.Replace("~Highlight", "{{W|Alt}}");
					}
					for (int i = 0; i < HelpRow.keysByLength.Count; i++)
					{
						string text = HelpRow.keysByLength[i];
						if (this.description.text.Contains("~Highlight") || this.description.text.Contains("~" + text))
						{
							string text2 = ControlManager.getCommandInputFormatted(text, true, false);
							if (string.IsNullOrEmpty(text2))
							{
								GameCommand gameCommand;
								if (CommandBindingManager.CommandsByID.TryGetValue(text, out gameCommand))
								{
									text2 = "{{hotkey|" + gameCommand.DisplayText + " [Unbound]}}";
								}
								else
								{
									text2 = "{{hotkey|" + text + " [Unbound]}}";
								}
							}
							this.description.text = this.description.text.Replace("~" + text, text2);
							if (!this.description.text.Contains("~"))
							{
								break;
							}
						}
					}
				}
				this.description.Apply();
				this.description.gameObject.SetActive(!helpDataRow.Collapsed);
				if (helpDataRow.Collapsed)
				{
					this.categoryExpander.SetText("{{C|[+]}}");
					return;
				}
				this.categoryExpander.SetText("{{C|[-]}}");
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000D4DDC File Offset: 0x000D2FDC
		public void Update()
		{
			NavigationContext navigationContext = this.GetNavigationContext();
			bool? flag = ((navigationContext != null) ? new bool?(navigationContext.IsActive(true)) : null);
			bool? flag2 = this.wasSelected;
			bool? flag3 = flag;
			if (!((flag2.GetValueOrDefault() == flag3.GetValueOrDefault()) & (flag2 != null == (flag3 != null))))
			{
				this.wasSelected = flag;
				this.selectedMode = (this.background.enabled = flag.GetValueOrDefault());
			}
		}

		// Token: 0x040014B2 RID: 5298
		public Image background;

		// Token: 0x040014B3 RID: 5299
		public UITextSkin categoryDescription;

		// Token: 0x040014B4 RID: 5300
		public UITextSkin categoryExpander;

		// Token: 0x040014B5 RID: 5301
		public UITextSkin description;

		// Token: 0x040014B6 RID: 5302
		public NavigationContext categoryContext;

		// Token: 0x040014B7 RID: 5303
		public FrameworkContext frameworkContext;

		// Token: 0x040014B8 RID: 5304
		public bool selectedMode;

		// Token: 0x040014B9 RID: 5305
		private ScrollViewCalcs _calcs = new ScrollViewCalcs();

		// Token: 0x040014BA RID: 5306
		private static List<string> keysByLength;

		// Token: 0x040014BB RID: 5307
		private bool? wasSelected;
	}
}
