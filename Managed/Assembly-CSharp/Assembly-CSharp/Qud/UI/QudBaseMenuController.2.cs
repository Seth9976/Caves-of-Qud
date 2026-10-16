using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000422 RID: 1058
	public abstract class QudBaseMenuController<DataT, UnityT> : QudBaseMenuController where UnityT : ControlledSelectable
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x000CF1D7 File Offset: 0x000CD3D7
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x000CF1E0 File Offset: 0x000CD3E0
		public int selectedOption
		{
			get
			{
				return this._selectedOption;
			}
			set
			{
				if (value == -1)
				{
					this._selectedOption = -1;
					return;
				}
				if (value == this._selectedOption)
				{
					return;
				}
				this._selectedOption = Math.Max(0, Math.Min(this.optionsCount - 1, value));
				int num = ((this.inputFields != null) ? this.inputFields.Count : 0);
				if (this._selectedOption < num)
				{
					this.PlayClick();
					this.inputFields[this._selectedOption].Init();
					this.inputFields[this._selectedOption].selected = true;
					return;
				}
				if (this._selectedOption < this.menuItems.Count + num)
				{
					if (this.menuData != null && this._selectedOption - num >= 0 && this.menuData.Count > this._selectedOption - num && this.menuItems.Count > this._selectedOption - num)
					{
						this.menuItems[this._selectedOption - num].selected = true;
						this.PlayClick();
						this.selectHandlers.Invoke(this.menuData[this._selectedOption - num]);
						return;
					}
				}
				else
				{
					int num2 = this._selectedOption - this.menuItems.Count - num;
					if (num2 < this.menuBottomContext.buttons.Count)
					{
						this.PlayClick();
						this.menuBottomContext.buttons[num2].selected = true;
					}
				}
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x000CF35A File Offset: 0x000CD55A
		public int optionsCount
		{
			get
			{
				return ((this.inputFields != null) ? this.inputFields.Count : 0) + ((this.menuData != null) ? this.menuData.Count : 0) + this.bottomContextOptions.Count;
			}
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000CF395 File Offset: 0x000CD595
		public IEnumerable<IControlledSelectable> GetOptions()
		{
			if (this.inputFields != null)
			{
				foreach (ControlledTMPInputField controlledTMPInputField in this.inputFields)
				{
					yield return controlledTMPInputField;
				}
				List<ControlledTMPInputField>.Enumerator enumerator = default(List<ControlledTMPInputField>.Enumerator);
			}
			foreach (UnityT unityT in this.menuItems)
			{
				yield return unityT;
			}
			List<UnityT>.Enumerator enumerator2 = default(List<UnityT>.Enumerator);
			foreach (SelectableTextMenuItem selectableTextMenuItem in this.menuBottomContext.buttons)
			{
				yield return selectableTextMenuItem;
			}
			List<SelectableTextMenuItem>.Enumerator enumerator3 = default(List<SelectableTextMenuItem>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x000CF3A5 File Offset: 0x000CD5A5
		[Obsolete]
		public bool hasCancel
		{
			get
			{
				return this.cancelButton != null;
			}
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000CF3B4 File Offset: 0x000CD5B4
		public override void SetSelected(ControlledSelectable item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
			this.lastEventSource = pipeline;
			if (this.isCurrentWindow())
			{
				List<ControlledTMPInputField> list = this.inputFields;
				int num = ((list != null) ? list.Count : 0);
				if (this.menuBottomContext.buttons.Contains(item as SelectableTextMenuItem))
				{
					this.PlayClick();
					this._selectedOption = num + this.menuItems.Count + this.menuBottomContext.buttons.IndexOf((SelectableTextMenuItem)item);
					return;
				}
				this._selectedOption = num + this.menuItems.IndexOf((UnityT)((object)item));
				if (this._selectedOption >= num && this._selectedOption < num + this.menuData.Count)
				{
					this.PlayClick();
					this.selectHandlers.Invoke(this.menuData[this._selectedOption]);
				}
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000CF48D File Offset: 0x000CD68D
		public virtual void Reselect(int selectedOption)
		{
			this._selectedOption = -1;
			this.selectedOption = selectedOption;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000CF4A0 File Offset: 0x000CD6A0
		public QudMenuItem? GetSelectedContextButton()
		{
			if (this.selectedOption >= this.menuItems.Count + this.inputFields.Count)
			{
				return new QudMenuItem?(this.menuBottomContext.items[this.selectedOption - this.menuItems.Count - this.inputFields.Count]);
			}
			return null;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000CF509 File Offset: 0x000CD709
		protected void PlayClick()
		{
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000CF50C File Offset: 0x000CD70C
		public virtual void UpdateButtonLayout()
		{
			if (this.menuItems.Count != this.menuData.Count || this.menuData.GetHashCode() != this.lastHash)
			{
				this.lastHash = this.menuData.GetHashCode();
				this.UpdateElements(false);
			}
			if (this.optionsCount > 0)
			{
				if (!this.GetOptions().Any<IControlledSelectable>((IControlledSelectable i) => i.IsSelected()))
				{
					this.selectedOption = Math.Min(this.selectedOption, this.optionsCount - 1);
				}
			}
			if (this.menuBottomContext != null)
			{
				this.menuBottomContext.items = this.bottomContextOptions;
				this.menuBottomContext.controller = this;
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000CF5D8 File Offset: 0x000CD7D8
		public virtual void Update()
		{
			if (this.menuItems == null)
			{
				this.menuItems = new List<UnityT>();
			}
			if (this.menuData == null)
			{
				this.menuData = new List<DataT>();
			}
			if ((this.forceEditorRebuild || Application.isPlaying) && this.isCurrentWindow())
			{
				if (this.forceEditorRebuild)
				{
					this.menuItems.Clear();
				}
				this.forceEditorRebuild = false;
				this.UpdateButtonLayout();
				if (!Application.isPlaying || !this.isCurrentWindow())
				{
					return;
				}
				if (ControlManager.isCommandDown("Navigate Up", true, false, false))
				{
					this.lastEventSource = QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick;
					int num = Math.Min(this.selectedOption - 1, this.menuData.Count - 1);
					if (num < 0)
					{
						this.selectedOption = Math.Max(0, this.menuData.Count - 1);
					}
					else
					{
						this.selectedOption = num;
					}
				}
				if (ControlManager.isCommandDown("Navigate Down", true, false, false))
				{
					this.lastEventSource = QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick;
					int num2 = this.selectedOption + 1;
					if (num2 >= this.optionsCount)
					{
						this.selectedOption = 0;
					}
					else
					{
						this.selectedOption = num2;
					}
				}
				if (ControlManager.isCommandDown("Page Up", true, false, false))
				{
					ScrollRect componentInChildrenFast = base.gameObject.GetComponentInChildrenFast<ScrollRect>();
					if (componentInChildrenFast != null)
					{
						componentInChildrenFast.verticalNormalizedPosition += componentInChildrenFast.viewport.rect.height / componentInChildrenFast.content.rect.height;
						UnityT unityT = this.menuItems.Find((UnityT i) => i.IsInView());
						if (unityT != null)
						{
							unityT.Select();
						}
					}
				}
				if (ControlManager.isCommandDown("Page Down", true, false, false))
				{
					ScrollRect componentInChildrenFast2 = base.gameObject.GetComponentInChildrenFast<ScrollRect>();
					if (componentInChildrenFast2 != null)
					{
						componentInChildrenFast2.verticalNormalizedPosition -= componentInChildrenFast2.viewport.rect.height / componentInChildrenFast2.content.rect.height;
						UnityT unityT2 = this.menuItems.FindLast((UnityT i) => i.IsInView());
						if (unityT2 != null)
						{
							unityT2.Select();
						}
					}
				}
				if (ControlManager.isCommandDown("Accept", true, false, false))
				{
					this.lastEventSource = QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick;
					QudMenuItem? selectedContextButton = this.GetSelectedContextButton();
					if (selectedContextButton != null)
					{
						QudMenuItem value = selectedContextButton.Value;
						this.ActivateContextButton(value, this.lastEventSource);
						return;
					}
					if (this.menuData.Count > this.selectedOption && this.selectedOption >= 0)
					{
						this.Activate(this.menuData[this.selectedOption], QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
					}
					return;
				}
				else
				{
					if (this.bottomContextOptions.Count > 0 && ControlManager.isCommandDown("Navigate Left", true, false, false))
					{
						this.lastEventSource = QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick;
						int num3 = this.menuItems.Count + this.inputFields.Count;
						if (this.selectedOption >= num3)
						{
							this.selectedOption = Math.Max(num3, this.selectedOption - 1);
						}
						else
						{
							this.selectedOption = num3;
						}
					}
					if (this.bottomContextOptions.Count > 0 && ControlManager.isCommandDown("Navigate Right", true, false, false))
					{
						this.lastEventSource = QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick;
						int num4 = this.menuItems.Count + this.inputFields.Count;
						if (this.selectedOption >= num4)
						{
							this.selectedOption = Math.Min(num4 + this.bottomContextOptions.Count - 1, this.selectedOption + 1);
						}
						else
						{
							this.selectedOption = num4 + this.bottomContextOptions.Count - 1;
						}
					}
					this.CheckKeyInteractions(this.bottomContextOptions, new Action<QudMenuItem, QudBaseMenuController.MenuEventPipeline>(this.ActivateContextButton));
				}
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000CF998 File Offset: 0x000CDB98
		public virtual void CheckKeyInteractions(IEnumerable<QudMenuItem> items, Action<QudMenuItem, QudBaseMenuController.MenuEventPipeline> activateHandler)
		{
			foreach (QudMenuItem qudMenuItem in items)
			{
				foreach (string text in (qudMenuItem.hotkey ?? "").CachedCommaExpansion())
				{
					if (ControlManager.isCommandDown(text, true, false, false))
					{
						activateHandler(qudMenuItem, QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
					}
					else if (text.StartsWith("char:"))
					{
						char c = ((text.Length > 5) ? text[5] : '\0');
						if (c != '\0' && c != ' ' && ControlManager.isCharDown(c))
						{
							activateHandler(qudMenuItem, QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
						}
					}
					else
					{
						try
						{
							if (ControlManager.isKeyDown(Keyboard.ParseUnityEngineKeyCode(text)))
							{
								this.ActivateContextButton(qudMenuItem, QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
							}
						}
						catch (ArgumentException)
						{
						}
					}
				}
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000CFAA4 File Offset: 0x000CDCA4
		public virtual void UpdateElements(bool evenIfNotCurrent = false)
		{
			if (!this.isCurrentWindow() && !evenIfNotCurrent)
			{
				return;
			}
			List<GameObject> list = new List<GameObject>();
			foreach (object obj in this.menuItemContainer.transform)
			{
				Transform transform = (Transform)obj;
				if (!this.menuItems.Contains(transform.gameObject.GetComponent<UnityT>()) && !this.spacers.Contains(transform.gameObject))
				{
					list.Add(transform.gameObject);
				}
			}
			list.ForEach(delegate(GameObject o)
			{
				o.DestroyImmediate();
			});
			if (this.menuItems.Count > this.menuData.Count)
			{
				this.menuItems.GetRange(this.menuData.Count, this.menuItems.Count - this.menuData.Count).ForEach(delegate(UnityT o)
				{
					o.gameObject.DestroyImmediate();
				});
				this.menuItems.RemoveRange(this.menuData.Count, this.menuItems.Count - this.menuData.Count);
			}
			if (this.spacers.Count > Math.Max(0, this.menuData.Count - 1))
			{
				this.spacers.GetRange(Math.Max(0, this.menuData.Count - 1), this.spacers.Count - Math.Max(0, this.menuData.Count - 1)).ForEach(delegate(GameObject o)
				{
					o.gameObject.DestroyImmediate();
				});
				this.spacers.RemoveRange(Math.Max(0, this.menuData.Count - 1), this.spacers.Count - Math.Max(0, this.menuData.Count - 1));
			}
			while (this.menuItems.Count < this.menuData.Count)
			{
				if (this.menuItems.Count > 0 && this.spacerPrefab != null)
				{
					GameObject gameObject = this.spacerPrefab.Instantiate<GameObject>();
					gameObject.transform.SetParent(this.menuItemContainer.transform, false);
					this.spacers.Add(gameObject);
				}
				GameObject gameObject2 = this.menuItemPrefab.Instantiate<GameObject>();
				gameObject2.transform.SetParent(this.menuItemContainer.transform, false);
				UnityT component = gameObject2.GetComponent<UnityT>();
				component.controller = this;
				this.menuItems.Add(component);
			}
			for (int i = 0; i < this.menuData.Count; i++)
			{
				this.menuItems[i].data = this.menuData[i];
				this.menuItems[i].UpdateData();
			}
			ControlManager.currentContext.isInInput = new Func<bool>(this.IsInInput);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000CFDE8 File Offset: 0x000CDFE8
		public bool IsInInput()
		{
			bool flag = false;
			if (this.inputFields != null)
			{
				for (int i = 0; i < this.inputFields.Count; i++)
				{
					if (this.inputFields[i].isFocused && this.inputFields[i].selected)
					{
						return true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000CFE3E File Offset: 0x000CE03E
		public void Cancel()
		{
			if (this.isCurrentWindow())
			{
				this.cancelHandlers.Invoke();
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000CFE58 File Offset: 0x000CE058
		public override void Activate(ControlledSelectable item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
			this.lastEventSource = pipeline;
			if (this.isCurrentWindow())
			{
				if (this.menuBottomContext.buttons.Contains(item as SelectableTextMenuItem))
				{
					QudMenuItem qudMenuItem = (QudMenuItem)item.data;
					this.ActivateContextButton(qudMenuItem, pipeline);
					return;
				}
				this.Activate(this.menuData[this.menuItems.IndexOf((UnityT)((object)item))], pipeline);
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000CFEC9 File Offset: 0x000CE0C9
		public virtual void Activate(DataT data, QudBaseMenuController.MenuEventPipeline pipeline)
		{
			this.lastEventSource = pipeline;
			this.activateHandlers.Invoke(data);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000CFEDE File Offset: 0x000CE0DE
		public virtual void ActivateContextButton(QudMenuItem data, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
			this.lastEventSource = pipeline;
			this.activateContextHandlers.Invoke(data);
			if (data.command == "Cancel")
			{
				this.Cancel();
			}
		}

		// Token: 0x040013E0 RID: 5088
		public AudioSource selectSound;

		// Token: 0x040013E1 RID: 5089
		private int _selectedOption;

		// Token: 0x040013E2 RID: 5090
		[NonSerialized]
		public Func<bool> isCurrentWindow = () => true;

		// Token: 0x040013E3 RID: 5091
		private List<UnityT> menuItems = new List<UnityT>();

		// Token: 0x040013E4 RID: 5092
		private List<GameObject> spacers = new List<GameObject>();

		// Token: 0x040013E5 RID: 5093
		public List<DataT> menuData;

		// Token: 0x040013E6 RID: 5094
		public List<ControlledTMPInputField> inputFields;

		// Token: 0x040013E7 RID: 5095
		[Obsolete]
		public SelectableTextMenuItem cancelButton;

		// Token: 0x040013E8 RID: 5096
		public GameObject menuItemPrefab;

		// Token: 0x040013E9 RID: 5097
		public GameObject spacerPrefab;

		// Token: 0x040013EA RID: 5098
		public GameObject menuItemContainer;

		// Token: 0x040013EB RID: 5099
		public QudMenuBottomContext menuBottomContext;

		// Token: 0x040013EC RID: 5100
		public List<QudMenuItem> bottomContextOptions = new List<QudMenuItem>();

		// Token: 0x040013ED RID: 5101
		public string cancelText = "cancel";

		// Token: 0x040013EE RID: 5102
		public bool forceEditorRebuild;

		// Token: 0x040013EF RID: 5103
		private int lastHash;

		// Token: 0x040013F0 RID: 5104
		public UnityEvent cancelHandlers = new UnityEvent();

		// Token: 0x040013F1 RID: 5105
		public UnityEvent<DataT> activateHandlers = new UnityEvent<DataT>();

		// Token: 0x040013F2 RID: 5106
		public UnityEvent<QudMenuItem> activateContextHandlers = new UnityEvent<QudMenuItem>();

		// Token: 0x040013F3 RID: 5107
		public UnityEvent<DataT> selectHandlers = new UnityEvent<DataT>();
	}
}
