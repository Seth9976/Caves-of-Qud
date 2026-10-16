using System;
using QupKit;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.Core;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000490 RID: 1168
	[ExecuteAlways]
	[HasGameBasedStaticCache]
	[UIView("MessageLog", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "MessageLog", UICanvasHost = 1)]
	public class MessageLogWindow : MovableSceneFrameWindowBase<MessageLogWindow>
	{
		// Token: 0x06002CCF RID: 11471 RVA: 0x000F2090 File Offset: 0x000F0290
		public void LateUpdate()
		{
			if (MessageLogWindow.lastMessageLogSize != Options.MessageLogLineSizeAdjustment)
			{
				MessageLogWindow.lastMessageLogSize = Options.MessageLogLineSizeAdjustment;
				this.Reflow();
				MessageLogWindow._needsScrollToBottom = 2;
			}
			if (MessageLogWindow._needsScrollToBottom > 0)
			{
				MessageLogPooledScrollRect messageLogPooledScrollRect = this.messageLog;
				if (messageLogPooledScrollRect != null)
				{
					messageLogPooledScrollRect.ScrollToBot();
				}
				MessageLogWindow._needsScrollToBottom--;
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000F20E4 File Offset: 0x000F02E4
		public void ShowIfEnabled()
		{
			if (MessageLogWindow.Shown)
			{
				this.Show();
			}
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000F20F4 File Offset: 0x000F02F4
		public new void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (!base.canvas.enabled)
			{
				return;
			}
			if (ControlManager.isCommandDown("CmdShowSidebar", true, false, false))
			{
				MessageLogWindow.Shown = !MessageLogWindow.Shown;
			}
			if (GameManager.Instance.DockMovable <= 0)
			{
				this.toggle.SetActive(MessageLogWindow.Shown);
			}
			bool docked = this.Docked;
			if (GameManager.Instance.DockMovable > 0 && GameManager.MainCameraLetterbox)
			{
				this.Docked = true;
			}
			else
			{
				this.Docked = false;
			}
			if (this.Docked != docked)
			{
				this.messageLog.Reflow();
			}
			base.Update();
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000F219A File Offset: 0x000F039A
		[GameBasedCacheInit]
		public static void GameInit()
		{
			MessageLogWindow instance = SingletonWindowBase<MessageLogWindow>.instance;
			if (instance == null)
			{
				return;
			}
			instance.ClearMessageLog();
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000F21AB File Offset: 0x000F03AB
		public void Reflow()
		{
			MessageLogPooledScrollRect messageLogPooledScrollRect = this.messageLog;
			if (messageLogPooledScrollRect == null)
			{
				return;
			}
			messageLogPooledScrollRect.Reflow();
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000F21BD File Offset: 0x000F03BD
		public void ClearMessageLog()
		{
			MessageLogPooledScrollRect messageLogPooledScrollRect = this.messageLog;
			if (messageLogPooledScrollRect == null)
			{
				return;
			}
			messageLogPooledScrollRect.Clear();
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000F21CF File Offset: 0x000F03CF
		public override void Init()
		{
			base.Init();
			XRLCore.RegisterNewMessageLogEntryCallback(new Action<string>(this.AddMessage));
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000F21E8 File Offset: 0x000F03E8
		public void AddMessage(string log)
		{
			string l = ":: " + log;
			GameManager instance = GameManager.Instance;
			if (instance == null)
			{
				return;
			}
			ThreadTaskQueue uiQueue = instance.uiQueue;
			if (uiQueue == null)
			{
				return;
			}
			uiQueue.queueTask(delegate
			{
				this._AddMessage(l);
			}, 0);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000F223A File Offset: 0x000F043A
		private void _AddMessage(string log)
		{
			MessageLogPooledScrollRect messageLogPooledScrollRect = this.messageLog;
			if (messageLogPooledScrollRect == null)
			{
				return;
			}
			messageLogPooledScrollRect.Add(RTF.FormatToRTF(log, "FF", -1, false));
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000F2259 File Offset: 0x000F0459
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x040018FE RID: 6398
		public MessageLogPooledScrollRect messageLog;

		// Token: 0x040018FF RID: 6399
		public MinimapWindow Minimap;

		// Token: 0x04001900 RID: 6400
		public Scrollbar ScrollBar;

		// Token: 0x04001901 RID: 6401
		public static int _needsScrollToBottom = 0;

		// Token: 0x04001902 RID: 6402
		public static int lastMessageLogSize = -1;

		// Token: 0x04001903 RID: 6403
		public static bool Shown = true;
	}
}
