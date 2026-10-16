using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000498 RID: 1176
	[RequireComponent(typeof(ScrollRect))]
	public class ModScrollerOne : QudBaseMenuController<ModInfo, ModMenuLine>
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000F3AEB File Offset: 0x000F1CEB
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000F3AF3 File Offset: 0x000F1CF3
		public List<ModInfo> mods
		{
			get
			{
				return this.menuData;
			}
			set
			{
				this.menuData = value;
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000F3AFC File Offset: 0x000F1CFC
		public void Awake()
		{
			this.activateHandlers.AddListener(new UnityAction<ModInfo>(this.OnActivate));
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000F3B18 File Offset: 0x000F1D18
		public override void Update()
		{
			if (this._scroller == null || this._scroller.gameObject != base.gameObject)
			{
				this._scroller = base.GetComponent<ScrollRect>();
			}
			if (this._scroller.content.sizeDelta.x != this._scroller.viewport.rect.width)
			{
				this._scroller.content.GetComponent<LayoutElement>().preferredHeight = (float)Math.Floor((double)this._scroller.viewport.rect.height);
				this._scroller.content.sizeDelta = new Vector2(this._scroller.viewport.rect.width, (float)Math.Ceiling((double)this._scroller.content.sizeDelta.y));
			}
			this.normalPos = this._scroller.normalizedPosition;
			base.Update();
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000F3C1C File Offset: 0x000F1E1C
		public void OnActivate(ModInfo modInfo)
		{
			if (modInfo.IsScripting && !Options.AllowCSMods)
			{
				Popup.WaitNewPopupMessage(modInfo.DisplayTitle + " contains scripts and has been permanently disabled in the options.\n{{K|(Options->Modding->Allow scripting mods)}}", PopupMessage.SingleButton, null, null, null, null, 0, null, null, null, true, true, null, null, false);
				return;
			}
			if (!modInfo.Settings.Enabled)
			{
				modInfo.Settings.Enabled = true;
				return;
			}
			if (modInfo.Settings.Failed)
			{
				modInfo.ConfirmFailure();
				return;
			}
			if (modInfo.IsMissingDependency)
			{
				modInfo.ConfirmDependencies();
				return;
			}
			if (modInfo.HasUpdate)
			{
				global::XRL.Version remoteVersion = modInfo.RemoteVersion;
				ModSettings settings = modInfo.Settings;
				if (remoteVersion > (((settings != null) ? settings.UpdateVersion : null) ?? global::XRL.Version.Zero))
				{
					modInfo.ConfirmUpdate();
					return;
				}
			}
			modInfo.Settings.Enabled = false;
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000F3CF7 File Offset: 0x000F1EF7
		public override void UpdateElements(bool evenIfNotCurrent = false)
		{
			base.UpdateElements(evenIfNotCurrent);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._scroller.content);
			this._scroller.verticalNormalizedPosition = 1f;
		}

		// Token: 0x04001943 RID: 6467
		private ScrollRect _scroller;

		// Token: 0x04001944 RID: 6468
		public bool forceEditorUpdate;

		// Token: 0x04001945 RID: 6469
		public Vector2 normalPos;
	}
}
