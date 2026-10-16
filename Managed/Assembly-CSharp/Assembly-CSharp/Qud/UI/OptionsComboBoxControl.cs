using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000447 RID: 1095
	[ExecuteInEditMode]
	public class OptionsComboBoxControl : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x0600299A RID: 10650 RVA: 0x000D9BA0 File Offset: 0x000D7DA0
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (this.context != null && this.context.IsActive(true))
			{
				ScrollChildContext scrollChildContext = this.context.parentContext as ScrollChildContext;
				if (scrollChildContext != null && scrollChildContext.index != scontext.index)
				{
					this.context = new OptionsComboBoxControl.Context();
					if (this.optionsScroller.scrollContext.IsActive(true))
					{
						this.optionsScroller.scrollContext.parentContext.Activate(false);
						goto IL_00A6;
					}
					goto IL_00A6;
				}
			}
			OptionsComboBoxControl.Context context = NavigationController.instance.activeContext as OptionsComboBoxControl.Context;
			if (context != null)
			{
				ScrollChildContext scrollChildContext2 = context.parentContext as ScrollChildContext;
				if (scrollChildContext2 != null && scrollChildContext2.index == scontext.index)
				{
					this.context = context;
				}
			}
			IL_00A6:
			if (scontext != null)
			{
				OptionsComboBoxControl.Context context2;
				if ((context2 = this.context) == null)
				{
					context2 = (this.context = new OptionsComboBoxControl.Context());
				}
				scontext.proxyTo = context2;
				this.context.parentContext = scontext;
				this.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						this.optionsScroller.scrollContext.Activate(false);
					})
				} };
				this.optionsScroller.scrollContext.parentContext = this.context;
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000D9CC0 File Offset: 0x000D7EC0
		public void OnSelectOption(FrameworkDataElement element)
		{
			MenuOption menuOption = element as MenuOption;
			if (menuOption != null)
			{
				Options.SetOption(this.data.Id, menuOption.Id);
				this.data.Value = menuOption.Id;
				this.Render();
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000D9D04 File Offset: 0x000D7F04
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000D9D0C File Offset: 0x000D7F0C
		public void setData(FrameworkDataElement data)
		{
			OptionsComboBoxRow optionsComboBoxRow = data as OptionsComboBoxRow;
			if (optionsComboBoxRow != null)
			{
				this.data = optionsComboBoxRow;
				this.Render();
				return;
			}
			this.data = null;
			this.optionsScroller.BeforeShow(null, new FrameworkDataElement[0]);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000D9D4C File Offset: 0x000D7F4C
		public void Render()
		{
			this.text.SetText(this.data.Title);
			int selectedIndex = 0;
			List<MenuOption> renderOptions = this.RenderOptions;
			string[] options = this.data.Options;
			string[] displayOptions = this.data.DisplayOptions;
			string value = this.data.Value;
			renderOptions.Clear();
			int i = 0;
			int num = options.Length;
			while (i < num)
			{
				string text = options[i];
				bool flag = text == value;
				if (flag)
				{
					selectedIndex = i;
				}
				renderOptions.Add(new MenuOption
				{
					Description = (flag ? "{{W|" : "{{c|") + displayOptions[i] + "}}",
					Id = text
				});
				i++;
			}
			this.optionsScroller.BeforeShow(null, renderOptions);
			this.optionsScroller.scrollContext.selectedPosition = selectedIndex;
			ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = this.optionsScroller.scrollContext;
			if (scrollContext.buttonHandlers == null)
			{
				scrollContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			}
			this.optionsScroller.scrollContext.buttonHandlers[InputButtonTypes.CancelButton] = delegate
			{
				this.optionsScroller.scrollContext.selectedPosition = selectedIndex;
			};
			this.data.ValueChangedSinceLastObserved(this);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000D9E94 File Offset: 0x000D8094
		public void Awake()
		{
			this.optionsScroller.DisableFirstLateUpdate();
			this.optionsScroller.DisableForcedUpdatesOnLayout();
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000D9EAC File Offset: 0x000D80AC
		public void Update()
		{
			OptionsComboBoxRow optionsComboBoxRow = this.data;
			if (optionsComboBoxRow != null && optionsComboBoxRow.ValueChangedSinceLastObserved(this))
			{
				this.Render();
			}
		}

		// Token: 0x04001580 RID: 5504
		public NavigationContext editingContext;

		// Token: 0x04001581 RID: 5505
		public FrameworkContext frameworkContext;

		// Token: 0x04001582 RID: 5506
		public OptionsComboBoxRow data;

		// Token: 0x04001583 RID: 5507
		public UITextSkin text;

		// Token: 0x04001584 RID: 5508
		public FrameworkScroller optionsScroller;

		// Token: 0x04001585 RID: 5509
		private OptionsComboBoxControl.Context context;

		// Token: 0x04001586 RID: 5510
		private List<MenuOption> RenderOptions = new List<MenuOption>();

		// Token: 0x02001851 RID: 6225
		private class Context : NavigationContext
		{
		}
	}
}
