using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000434 RID: 1076
	[ExecuteInEditMode]
	public class CyberneticsTerminalRow : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x000D3978 File Offset: 0x000D1B78
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

		// Token: 0x060028D0 RID: 10448 RVA: 0x000D3A22 File Offset: 0x000D1C22
		public NavigationContext GetNavigationContext()
		{
			return this.frameworkContext.context;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000D3A30 File Offset: 0x000D1C30
		public void setData(FrameworkDataElement data)
		{
			CyberneticsTerminalLineData cyberneticsTerminalLineData = data as CyberneticsTerminalLineData;
			if (cyberneticsTerminalLineData != null)
			{
				cyberneticsTerminalLineData.row = this;
				this.screen = cyberneticsTerminalLineData.screen;
				this.data = cyberneticsTerminalLineData;
				this.description.SetText("");
				this.cursor = 0;
				this.cursorTimer = 0f;
				this.currentCursor = false;
				this.cursorDone = false;
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000D3A94 File Offset: 0x000D1C94
		public void Update()
		{
			if (!CyberneticsTerminalScreen.initReady)
			{
				return;
			}
			NavigationController instance = NavigationController.instance;
			NavigationController.SuspensionNavigationContext suspensionNavigationContext = ((instance != null) ? instance.activeContext : null);
			NavigationController instance2 = NavigationController.instance;
			if (suspensionNavigationContext == ((instance2 != null) ? instance2.suspensionContext : null))
			{
				return;
			}
			if (!this.cursorDone)
			{
				if (ControlManager.currentFrameCommands.Count > 0)
				{
					this.description.SetText(this.data.Text);
					this.currentCursor = false;
					this.cursorDone = true;
					if (this.data.nextCursorData == null)
					{
						CyberneticsTerminalScreen cyberneticsTerminalScreen = this.screen;
						if (cyberneticsTerminalScreen != null)
						{
							cyberneticsTerminalScreen.HandleTextComplete();
						}
					}
				}
				else if (this.currentCursor)
				{
					this.cursorTimer += Time.deltaTime;
					if (this.cursorTimer > 0.015f)
					{
						int num = (int)(this.cursorTimer / 0.015f);
						this.cursorTimer -= 0.015f * (float)num;
						this.cursor += num;
						if (this.cursor >= this.data.Text.Length)
						{
							this.description.SetText(this.data.Text);
							this.currentCursor = false;
							this.cursorDone = true;
							if (this.data.nextCursorData != null)
							{
								this.data.nextCursorData.row.currentCursor = true;
							}
							else if (this.data.nextCursorData == null)
							{
								CyberneticsTerminalScreen cyberneticsTerminalScreen2 = this.screen;
								if (cyberneticsTerminalScreen2 != null)
								{
									cyberneticsTerminalScreen2.HandleTextComplete();
								}
							}
						}
						else
						{
							this.description.SetText(this.data.Text.Substring(0, this.cursor) + "_");
						}
					}
				}
			}
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

		// Token: 0x0400147E RID: 5246
		public bool cursorDone;

		// Token: 0x0400147F RID: 5247
		public bool currentCursor;

		// Token: 0x04001480 RID: 5248
		public float cursorTimer;

		// Token: 0x04001481 RID: 5249
		public int cursor;

		// Token: 0x04001482 RID: 5250
		public Image background;

		// Token: 0x04001483 RID: 5251
		public CyberneticsTerminalLineData data;

		// Token: 0x04001484 RID: 5252
		public UITextSkin description;

		// Token: 0x04001485 RID: 5253
		public FrameworkContext frameworkContext;

		// Token: 0x04001486 RID: 5254
		public bool selectedMode;

		// Token: 0x04001487 RID: 5255
		private ScrollViewCalcs _calcs = new ScrollViewCalcs();

		// Token: 0x04001488 RID: 5256
		private CyberneticsTerminalScreen screen;

		// Token: 0x04001489 RID: 5257
		private bool? wasSelected;
	}
}
