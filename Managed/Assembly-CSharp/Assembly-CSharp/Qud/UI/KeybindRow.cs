using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200043E RID: 1086
	[ExecuteInEditMode]
	public class KeybindRow : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x06002939 RID: 10553 RVA: 0x000D6974 File Offset: 0x000D4B74
		public void SetupContexts(ScrollChildContext scontext)
		{
			NavigationContext navigationContext = this.categoryContext;
			if (((navigationContext != null) ? navigationContext.parentContext : null) == scontext)
			{
				ScrollContext<int, KeybindRow.KeybindRowSubContext> scrollContext = this.subscroller;
				if (((scrollContext != null) ? scrollContext.parentContext : null) == scontext)
				{
					goto IL_016C;
				}
			}
			this.categoryContext = null;
			this.subscroller = null;
			this.box1.context.context = null;
			this.box2.context.context = null;
			this.box3.context.context = null;
			this.box4.context.context = null;
			if (scontext.IsActive(true))
			{
				KeybindRow.KeybindRowSubContext keybindRowSubContext = NavigationController.instance.activeContext as KeybindRow.KeybindRowSubContext;
				if (keybindRowSubContext != null)
				{
					if (keybindRowSubContext.contextType == KeybindRow.SubContextTypes.box1 || keybindRowSubContext.contextType == KeybindRow.SubContextTypes.box2 || keybindRowSubContext.contextType == KeybindRow.SubContextTypes.box3 || keybindRowSubContext.contextType == KeybindRow.SubContextTypes.box4)
					{
						this.subscroller = keybindRowSubContext.parentContext as ScrollContext<int, KeybindRow.KeybindRowSubContext>;
						this.box1.context.context = this.subscroller.contexts[0];
						this.box2.context.context = this.subscroller.contexts[1];
						this.box3.context.context = this.subscroller.contexts[2];
						this.box4.context.context = this.subscroller.contexts[3];
					}
					if (keybindRowSubContext.contextType == KeybindRow.SubContextTypes.category)
					{
						this.categoryContext = keybindRowSubContext;
					}
				}
			}
			IL_016C:
			if (this.categoryContext == null)
			{
				this.categoryContext = new KeybindRow.KeybindRowSubContext(KeybindRow.SubContextTypes.category)
				{
					parentContext = scontext
				};
			}
			if (this.subscroller == null)
			{
				this.subscroller = new ScrollContext<int, KeybindRow.KeybindRowSubContext>
				{
					parentContext = scontext
				};
				this.box1.context.context = new KeybindRow.KeybindRowSubContext(KeybindRow.SubContextTypes.box1);
				this.box1.context.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						UnityEvent<KeybindDataRow, int, KeybindRow> unityEvent = this.onRebind;
						if (unityEvent == null)
						{
							return;
						}
						unityEvent.Invoke(this.dataRow, 0, this);
					})
				} };
				this.box2.context.context = new KeybindRow.KeybindRowSubContext(KeybindRow.SubContextTypes.box2);
				this.box2.context.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						UnityEvent<KeybindDataRow, int, KeybindRow> unityEvent2 = this.onRebind;
						if (unityEvent2 == null)
						{
							return;
						}
						unityEvent2.Invoke(this.dataRow, 1, this);
					})
				} };
				this.box3.context.context = new KeybindRow.KeybindRowSubContext(KeybindRow.SubContextTypes.box3);
				this.box3.context.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						UnityEvent<KeybindDataRow, int, KeybindRow> unityEvent3 = this.onRebind;
						if (unityEvent3 == null)
						{
							return;
						}
						unityEvent3.Invoke(this.dataRow, 2, this);
					})
				} };
				this.box4.context.context = new KeybindRow.KeybindRowSubContext(KeybindRow.SubContextTypes.box4);
				this.box4.context.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						UnityEvent<KeybindDataRow, int, KeybindRow> unityEvent4 = this.onRebind;
						if (unityEvent4 == null)
						{
							return;
						}
						unityEvent4.Invoke(this.dataRow, 3, this);
					})
				} };
				this.subscroller.SetAxis(InputAxisTypes.NavigationXAxis, false);
				this.subscroller.wraps = false;
				this.subscroller.data.Add(0);
				this.subscroller.data.Add(1);
				this.subscroller.data.Add(2);
				this.subscroller.data.Add(3);
				this.subscroller.contexts.Add(this.box1.context.context as KeybindRow.KeybindRowSubContext);
				this.subscroller.contexts.Add(this.box2.context.context as KeybindRow.KeybindRowSubContext);
				this.subscroller.contexts.Add(this.box3.context.context as KeybindRow.KeybindRowSubContext);
				this.subscroller.contexts.Add(this.box4.context.context as KeybindRow.KeybindRowSubContext);
			}
			if (this.categoryRow != null)
			{
				scontext.proxyTo = this.categoryContext;
			}
			else
			{
				scontext.proxyTo = this.subscroller;
				this.box1.context.context.disabled = !this.box1.gameObject.activeInHierarchy;
				this.box2.context.context.disabled = !this.box2.gameObject.activeInHierarchy;
				this.box3.context.context.disabled = !this.box3.gameObject.activeInHierarchy;
				this.box4.context.context.disabled = !this.box4.gameObject.activeInHierarchy;
			}
			scontext.Setup();
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000D6E01 File Offset: 0x000D5001
		public NavigationContext GetNavigationContext()
		{
			return this.frameworkContext.context;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000D6E10 File Offset: 0x000D5010
		public void setData(FrameworkDataElement data)
		{
			KeybindDataRow keybindDataRow = data as KeybindDataRow;
			if (keybindDataRow != null)
			{
				this.categoryDisplay.SetActive(false);
				this.bindingDisplay.SetActive(true);
				this.categoryRow = null;
				this.dataRow = keybindDataRow;
				this.description.text = "{{C|" + keybindDataRow.KeyDescription + "}}";
				this.description.Apply();
				this.layoutElement.preferredHeight = (float)(25 * (1 + keybindDataRow.KeyDescription.Length / 26));
				this.layoutElement.minHeight = (float)(25 * (1 + keybindDataRow.KeyDescription.Length / 26));
				if (string.IsNullOrEmpty(keybindDataRow.Bind1))
				{
					this.box1.boxText = "{{K|None}}";
					this.box2.boxText = "{{K|None}}";
					this.box3.boxText = "{{K|None}}";
					this.box4.boxText = "{{K|None}}";
				}
				else
				{
					this.box1.boxText = "{{w|" + keybindDataRow.Bind1 + "}}";
					this.box2.gameObject.SetActive(true);
					if (string.IsNullOrEmpty(keybindDataRow.Bind2))
					{
						this.box2.boxText = "{{K|None}}";
						this.box3.boxText = "{{K|None}}";
						this.box4.boxText = "{{K|None}}";
					}
					else
					{
						this.box2.boxText = "{{w|" + keybindDataRow.Bind2 + "}}";
						this.box3.gameObject.SetActive(true);
						if (string.IsNullOrEmpty(keybindDataRow.Bind3))
						{
							this.box3.boxText = "{{K|None}}";
							this.box4.boxText = "{{K|None}}";
						}
						else
						{
							this.box3.boxText = "{{w|" + keybindDataRow.Bind3 + "}}";
							this.box4.gameObject.SetActive(true);
							this.box4.boxText = (string.IsNullOrEmpty(keybindDataRow.Bind4) ? "{{K|None}}" : ("{{w|" + keybindDataRow.Bind4 + "}}"));
						}
					}
				}
				this.box1.forceUpdate = (this.box2.forceUpdate = (this.box3.forceUpdate = (this.box4.forceUpdate = true)));
				bool flag = SingletonWindowBase<KeybindsScreen>.instance.currentControllerType == ControlManager.InputDeviceType.Keyboard;
				if (flag)
				{
					(this.box1.transform as RectTransform).sizeDelta = new Vector2(-648f, 0f);
					(this.box1.transform as RectTransform).localPosition = new Vector2(-57f, 0f);
					(this.box1.transform as RectTransform).anchoredPosition = new Vector2(-57f, 0f);
					(this.box2.transform as RectTransform).sizeDelta = new Vector2(-648f, 0f);
					(this.box2.transform as RectTransform).localPosition = new Vector2(70f, 0f);
					(this.box2.transform as RectTransform).anchoredPosition = new Vector2(70f, 0f);
				}
				else
				{
					(this.box1.transform as RectTransform).sizeDelta = new Vector2(-534f, 0f);
					(this.box1.transform as RectTransform).localPosition = new Vector2(0f, 0f);
					(this.box1.transform as RectTransform).anchoredPosition = new Vector2(0f, 0f);
					(this.box2.transform as RectTransform).sizeDelta = new Vector2(-534f, 0f);
					(this.box2.transform as RectTransform).localPosition = new Vector2(230f, 0f);
					(this.box2.transform as RectTransform).anchoredPosition = new Vector2(230f, 0f);
				}
				this.box3.gameObject.SetActive(flag);
				this.box4.gameObject.SetActive(flag);
			}
			else
			{
				KeybindCategoryRow keybindCategoryRow = data as KeybindCategoryRow;
				if (keybindCategoryRow != null)
				{
					this.layoutElement.preferredHeight = 25f;
					this.layoutElement.minHeight = 25f;
					this.categoryDisplay.SetActive(true);
					this.bindingDisplay.SetActive(false);
					this.categoryRow = keybindCategoryRow;
					this.dataRow = null;
					this.categoryDescription.text = "{{C|" + this.categoryRow.CategoryDescription.ToUpper() + "}}";
					this.categoryDescription.Apply();
					if (keybindCategoryRow.Collapsed)
					{
						this.categoryExpander.SetText("{{C|[+]}}");
					}
					else
					{
						this.categoryExpander.SetText("{{C|[-]}}");
					}
				}
			}
			this.GetNavigationContext();
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000D7338 File Offset: 0x000D5538
		public void Update()
		{
			if (this.editorUpdateDataRow)
			{
				this.editorUpdateDataRow = false;
				if (this.dataRow != null && !string.IsNullOrEmpty(this.dataRow.KeyDescription))
				{
					this.setData(this.dataRow);
				}
				else if (this.categoryRow != null)
				{
					this.setData(this.categoryRow);
				}
			}
			NavigationContext navigationContext = this.GetNavigationContext();
			bool? flag = ((navigationContext != null) ? new bool?(navigationContext.IsActive(true)) : null);
			bool? flag2 = this.wasSelected;
			bool? flag3 = flag;
			if (!((flag2.GetValueOrDefault() == flag3.GetValueOrDefault()) & (flag2 != null == (flag3 != null))))
			{
				this.wasSelected = flag;
				this.selectedMode = flag.GetValueOrDefault();
				if (this.selectedMode)
				{
					if (this.background.color.a != 0.2470588f)
					{
						this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0.2470588f);
						return;
					}
				}
				else if (this.background.color.a != 0f)
				{
					this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0f);
				}
			}
		}

		// Token: 0x04001505 RID: 5381
		public Image background;

		// Token: 0x04001506 RID: 5382
		public GameObject bindingDisplay;

		// Token: 0x04001507 RID: 5383
		public GameObject categoryDisplay;

		// Token: 0x04001508 RID: 5384
		public UITextSkin categoryDescription;

		// Token: 0x04001509 RID: 5385
		public UITextSkin categoryExpander;

		// Token: 0x0400150A RID: 5386
		public LayoutElement layoutElement;

		// Token: 0x0400150B RID: 5387
		public UITextSkin description;

		// Token: 0x0400150C RID: 5388
		public KeybindBox box1;

		// Token: 0x0400150D RID: 5389
		public KeybindBox box2;

		// Token: 0x0400150E RID: 5390
		public KeybindBox box3;

		// Token: 0x0400150F RID: 5391
		public KeybindBox box4;

		// Token: 0x04001510 RID: 5392
		public ScrollContext<int, KeybindRow.KeybindRowSubContext> subscroller;

		// Token: 0x04001511 RID: 5393
		public NavigationContext categoryContext;

		// Token: 0x04001512 RID: 5394
		public FrameworkContext frameworkContext;

		// Token: 0x04001513 RID: 5395
		public bool selectedMode;

		// Token: 0x04001514 RID: 5396
		public KeybindDataRow dataRow = new KeybindDataRow
		{
			KeyDescription = "Interact Nearby",
			Bind1 = "Ctrl+Space",
			Bind2 = null
		};

		// Token: 0x04001515 RID: 5397
		public KeybindCategoryRow categoryRow;

		// Token: 0x04001516 RID: 5398
		public bool editorUpdateDataRow;

		// Token: 0x04001517 RID: 5399
		public UnityEvent<KeybindDataRow, int, KeybindRow> onRebind = new UnityEvent<KeybindDataRow, int, KeybindRow>();

		// Token: 0x04001518 RID: 5400
		private const string NO_BIND_DISPLAY = "{{K|None}}";

		// Token: 0x04001519 RID: 5401
		private bool? wasSelected;

		// Token: 0x0200182B RID: 6187
		public enum SubContextTypes
		{
			// Token: 0x0400615B RID: 24923
			box1,
			// Token: 0x0400615C RID: 24924
			box2,
			// Token: 0x0400615D RID: 24925
			box3,
			// Token: 0x0400615E RID: 24926
			box4,
			// Token: 0x0400615F RID: 24927
			category
		}

		// Token: 0x0200182C RID: 6188
		public class KeybindRowSubContext : ScrollChildContext
		{
			// Token: 0x0600DFDE RID: 57310 RVA: 0x00515F8A File Offset: 0x0051418A
			public KeybindRowSubContext(KeybindRow.SubContextTypes contextType)
			{
				this.contextType = contextType;
			}

			// Token: 0x04006160 RID: 24928
			public KeybindRow.SubContextTypes contextType;
		}
	}
}
