using System;
using System.Collections.Generic;
using UnityEngine;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000497 RID: 1175
	public class ModMenuLine : ControlledSelectable
	{
		// Token: 0x06002D19 RID: 11545 RVA: 0x000F3410 File Offset: 0x000F1610
		public void SetTag(string Text, ref int Index, bool State = true)
		{
			if (!State)
			{
				return;
			}
			GameObject gameObject;
			if (Index < this._tagged.Count)
			{
				gameObject = this._tagged[Index];
			}
			else
			{
				gameObject = this.taggedPrefab.Instantiate<GameObject>();
				gameObject.transform.SetParent(this.taggedArea, false);
				this._tagged.Add(gameObject);
			}
			UITextSkin componentInChildren = gameObject.GetComponentInChildren<UITextSkin>();
			if (componentInChildren != null)
			{
				componentInChildren.SetText(Text);
			}
			gameObject.SetActive(true);
			Index++;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000F348C File Offset: 0x000F168C
		public override void Update()
		{
			if (this.modInfo != this.data)
			{
				this.modInfo = this.data as ModInfo;
			}
			base.Update();
			if (this.modInfo != null)
			{
				ModState? modState;
				ModState modState2;
				if (!(this.modInfo.DisplayTitle != this._lastTitle))
				{
					modState = this._lastState;
					modState2 = this.modInfo.State;
					if ((modState.GetValueOrDefault() == modState2) & (modState != null))
					{
						goto IL_015B;
					}
				}
				this._lastTitle = this.modInfo.DisplayTitle;
				if (this.modInfo.State == ModState.MissingDependency)
				{
					this.titleText.SetText("{{W|" + this.modInfo.DisplayTitle + "}}");
				}
				else if (this.modInfo.State == ModState.Failed)
				{
					this.titleText.SetText("{{R|" + this.modInfo.DisplayTitle + "}}");
				}
				else if (this.modInfo.State == ModState.Disabled)
				{
					this.titleText.SetText("{{K|" + this.modInfo.DisplayTitle + "}}");
				}
				else if (this.modInfo.State == ModState.Enabled)
				{
					this.titleText.SetText("{{Y|" + this.modInfo.DisplayTitle + "}}");
				}
				IL_015B:
				if (this.modInfo.Manifest.Author != this._lastAuthor)
				{
					this._lastAuthor = this.modInfo.Manifest.Author;
					if (string.IsNullOrEmpty(this.modInfo.Manifest.Author))
					{
						this.authorSpacer.SetActive(false);
						this.authorText.gameObject.SetActive(false);
					}
					else
					{
						this.authorSpacer.SetActive(true);
						this.authorText.gameObject.SetActive(true);
						this.authorText.SetText("{{y|by " + this._lastAuthor + "}}");
					}
				}
				if (this.version != null && this.modInfo.Manifest.Version != this._lastVersion)
				{
					this._lastVersion = this.modInfo.Manifest.Version;
					this.version.value = this.modInfo.Manifest.Version.ToString();
					this.version.gameObject.SetActive(!this._lastVersion.IsZero());
				}
				if (this.tags != null && this.modInfo.Manifest.Tags != this._lastTags)
				{
					this._lastTags = this.modInfo.Manifest.Tags;
					this.tags.value = (this._lastTags.IsNullOrEmpty<string>() ? "" : string.Join(", ", this._lastTags));
					this.tags.gameObject.SetActive(!this.tags.value.IsNullOrEmpty());
				}
				if (this.modInfo.Path != this._lastPath)
				{
					this._lastPath = this.modInfo.Path;
					this.location.value = DataManager.SanitizePathForDisplay(this.modInfo.Path);
				}
				if (this.modInfo.Size != this._lastSize)
				{
					this._lastSize = this.modInfo.Size;
					double num = (double)this._lastSize;
					if (num >= 1048576.0)
					{
						num /= 1048576.0;
						this.size.value = string.Format("{0:0.00} MB", num);
					}
					else if (this._lastSize >= 1024L)
					{
						num /= 1024.0;
						this.size.value = string.Format("{0:0} KB", num);
					}
					else
					{
						this.size.value = this._lastSize.ToString() + " bytes";
					}
				}
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
				{
					this.location.gameObject.SetActive(false);
					this.size.gameObject.SetActive(false);
				}
				modState = this._lastState;
				modState2 = this.modInfo.State;
				if (!((modState.GetValueOrDefault() == modState2) & (modState != null)) || this._lastHasUpdate != this.modInfo.HasUpdate)
				{
					int num2 = 0;
					this._lastState = new ModState?(this.modInfo.State);
					this._lastHasUpdate = this.modInfo.HasUpdate;
					switch (this.modInfo.State)
					{
					case ModState.Enabled:
						this.SetTag("{{green|ENABLED}}", ref num2, true);
						this.imageFrame.borderColor = The.Color.DarkGreen;
						break;
					case ModState.Disabled:
						this.SetTag("{{black|DISABLED}}", ref num2, true);
						this.imageFrame.borderColor = The.Color.Black;
						break;
					case ModState.MissingDependency:
						this.SetTag("{{yellow|MISSING DEPENDENCY}}", ref num2, true);
						this.imageFrame.borderColor = The.Color.Yellow;
						break;
					case ModState.Failed:
						this.SetTag("{{red|FAILED}}", ref num2, true);
						this.imageFrame.borderColor = The.Color.DarkRed;
						break;
					}
					if (this.modInfo.GetSprite() != null && this.imageFrame.sprite != this.modInfo.GetSprite())
					{
						this.imageFrame.sprite = this.modInfo.GetSprite();
					}
					this.SetTag("{{W|# UPDATE AVAILABLE}}", ref num2, this._lastHasUpdate);
					this.SetTag("{{w|# SCRIPTING}}", ref num2, this.modInfo.IsScripting);
					this.SetTag("{{W|# HARMONY PATCHES}}", ref num2, this.modInfo.Harmony != null);
					for (int i = this._tagged.Count - 1; i >= num2; i--)
					{
						this._tagged[i].SetActive(false);
					}
				}
			}
		}

		// Token: 0x0400192D RID: 6445
		public ModInfo modInfo;

		// Token: 0x0400192E RID: 6446
		public UITextSkin titleText;

		// Token: 0x0400192F RID: 6447
		public GameObject authorSpacer;

		// Token: 0x04001930 RID: 6448
		public UITextSkin authorText;

		// Token: 0x04001931 RID: 6449
		public InfoChip version;

		// Token: 0x04001932 RID: 6450
		public InfoChip size;

		// Token: 0x04001933 RID: 6451
		public InfoChip tags;

		// Token: 0x04001934 RID: 6452
		public InfoChip location;

		// Token: 0x04001935 RID: 6453
		public ImageTinyFrame imageFrame;

		// Token: 0x04001936 RID: 6454
		public RectTransform taggedArea;

		// Token: 0x04001937 RID: 6455
		public GameObject taggedPrefab;

		// Token: 0x04001938 RID: 6456
		private string _lastAuthor = "\0";

		// Token: 0x04001939 RID: 6457
		private string _lastTitle = "\0";

		// Token: 0x0400193A RID: 6458
		private string _lastPath = "\0";

		// Token: 0x0400193B RID: 6459
		private long _lastSize;

		// Token: 0x0400193C RID: 6460
		private global::XRL.Version _lastVersion;

		// Token: 0x0400193D RID: 6461
		private string[] _lastTags;

		// Token: 0x0400193E RID: 6462
		private List<GameObject> _tagged = new List<GameObject>();

		// Token: 0x0400193F RID: 6463
		private ModState? _lastState;

		// Token: 0x04001940 RID: 6464
		private bool _lastHasUpdate;

		// Token: 0x04001941 RID: 6465
		private Sprite _sprite;

		// Token: 0x04001942 RID: 6466
		private string _imgPath;
	}
}
