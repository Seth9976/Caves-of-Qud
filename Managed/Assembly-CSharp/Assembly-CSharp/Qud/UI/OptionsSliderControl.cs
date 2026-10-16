using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000452 RID: 1106
	[ExecuteInEditMode]
	public class OptionsSliderControl : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x060029D7 RID: 10711 RVA: 0x000DB6E0 File Offset: 0x000D98E0
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (this.context != null && this.context.IsActive(true))
			{
				ScrollChildContext scrollChildContext = this.context.parentContext as ScrollChildContext;
				if (scrollChildContext != null && scrollChildContext.index != scontext.index)
				{
					this.context = new OptionsSliderControl.Context();
					if (this.editingContext.IsActive(true))
					{
						this.editingContext.parentContext.Activate(false);
					}
					this.editingContext = null;
					goto IL_00A3;
				}
			}
			OptionsSliderControl.Context context = NavigationController.instance.activeContext as OptionsSliderControl.Context;
			if (context != null)
			{
				ScrollChildContext scrollChildContext2 = context.parentContext as ScrollChildContext;
				if (scrollChildContext2 != null && scrollChildContext2.index == scontext.index)
				{
					this.context = context;
				}
			}
			IL_00A3:
			if (scontext != null)
			{
				OptionsSliderControl.Context context2;
				if ((context2 = this.context) == null)
				{
					context2 = (this.context = new OptionsSliderControl.Context());
				}
				scontext.proxyTo = context2;
				this.context.parentContext = scontext;
				this.context.menuOptionDescriptions = new List<MenuOption> { OptionsSliderControl.CHANGE_VALUE };
				OptionsSliderControl.Context context3 = this.context;
				if (context3.buttonHandlers == null)
				{
					context3.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
					{
						InputButtonTypes.AcceptButton,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.valueCache = this.data.Value;
							this.editingContext.Activate(false);
						})
					} };
				}
			}
			if (this.editingContext == null)
			{
				this.editingContext = new NavigationContext
				{
					menuOptionDescriptions = new List<MenuOption>
					{
						OptionsSliderControl.SAVE_VALUE,
						OptionsSliderControl.CANCEL_VALUE,
						OptionsSliderControl.ARROWS_CHANGE_VALUE
					},
					parentContext = this.context,
					axisHandlers = new Dictionary<InputAxisTypes, Action>
					{
						{
							InputAxisTypes.NavigationXAxis,
							global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.Increase), new Action(this.Decrease), null))
						},
						{
							InputAxisTypes.NavigationYAxis,
							global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.Decrease5), new Action(this.Increase5), null))
						}
					},
					buttonHandlers = new Dictionary<InputButtonTypes, Action>
					{
						{
							InputButtonTypes.AcceptButton,
							global::XRL.UI.Framework.Event.Helpers.Handle(delegate
							{
								this.context.Activate(false);
							})
						},
						{
							InputButtonTypes.CancelButton,
							global::XRL.UI.Framework.Event.Helpers.Handle(delegate
							{
								this.SetValue(this.valueCache);
								this.context.Activate(false);
							})
						}
					},
					enterHandler = delegate
					{
						this.sliderHandleImage.color = The.Color.Yellow;
					},
					exitHandler = delegate
					{
						this.sliderHandleImage.color = ConsoleLib.Console.ColorUtility.FromWebColor("426470");
					}
				};
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000DB918 File Offset: 0x000D9B18
		public void SetValue(int value)
		{
			this.data.Value = Math.Min(this.data.Max, Math.Max(this.data.Min, value));
			this.slider.value = (float)this.data.Value;
			this.valueLabel.SetText(this.data.Value.ToString());
			Options.SetOption(this.data.Id, Convert.ToString(this.data.Value));
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000DB9A7 File Offset: 0x000D9BA7
		public void Increase5()
		{
			this.SetValue(this.data.Value + this.data.Increment * 5);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000DB9C8 File Offset: 0x000D9BC8
		public void Increase()
		{
			this.SetValue(this.data.Value + this.data.Increment);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000DB9E7 File Offset: 0x000D9BE7
		public void Decrease()
		{
			this.SetValue(this.data.Value - this.data.Increment);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000DBA06 File Offset: 0x000D9C06
		public void Decrease5()
		{
			this.SetValue(this.data.Value - this.data.Increment * 5);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000DBA28 File Offset: 0x000D9C28
		public void OnSliderChanged(float value)
		{
			int num = (Convert.ToInt32(value) - this.data.Min) / this.data.Increment * this.data.Increment + this.data.Min;
			if (!this.initializing)
			{
				Options.SetOption(this.data.Id, Convert.ToString(num));
				this.data.Value = num;
				this.valueLabel.SetText(this.data.Value.ToString());
			}
			this.slider.value = (float)num;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000DBAC2 File Offset: 0x000D9CC2
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000DBACC File Offset: 0x000D9CCC
		public void setData(FrameworkDataElement data)
		{
			this.initializing = true;
			try
			{
				OptionsSliderRow optionsSliderRow = data as OptionsSliderRow;
				if (optionsSliderRow != null)
				{
					this.data = optionsSliderRow;
					this.slider.minValue = (float)optionsSliderRow.Min;
					this.slider.maxValue = (float)optionsSliderRow.Max;
					this.slider.value = (float)optionsSliderRow.Value;
					this.slider.wholeNumbers = true;
					this.valueLabel.SetText(optionsSliderRow.Value.ToString());
					this.Render();
				}
				else
				{
					this.data = null;
				}
			}
			finally
			{
				this.initializing = false;
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000DBB78 File Offset: 0x000D9D78
		public void Render()
		{
			this.text.SetText(this.data.Title);
			this.data.ValueChangedSinceLastObserved(this);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000DBB9E File Offset: 0x000D9D9E
		public void Update()
		{
			OptionsSliderRow optionsSliderRow = this.data;
			if (optionsSliderRow != null && optionsSliderRow.ValueChangedSinceLastObserved(this))
			{
				this.Render();
			}
		}

		// Token: 0x040015BA RID: 5562
		public NavigationContext editingContext;

		// Token: 0x040015BB RID: 5563
		public FrameworkContext frameworkContext;

		// Token: 0x040015BC RID: 5564
		public OptionsSliderRow data;

		// Token: 0x040015BD RID: 5565
		public UITextSkin text;

		// Token: 0x040015BE RID: 5566
		public UITextSkin valueLabel;

		// Token: 0x040015BF RID: 5567
		public Slider slider;

		// Token: 0x040015C0 RID: 5568
		public Image sliderHandleImage;

		// Token: 0x040015C1 RID: 5569
		public int valueCache;

		// Token: 0x040015C2 RID: 5570
		private OptionsSliderControl.Context context;

		// Token: 0x040015C3 RID: 5571
		public static MenuOption CHANGE_VALUE = new MenuOption
		{
			InputCommand = "Accept",
			Description = "Change Value",
			disabled = true
		};

		// Token: 0x040015C4 RID: 5572
		public static MenuOption ARROWS_CHANGE_VALUE = new MenuOption
		{
			InputCommand = "NavigationXYAxis",
			Description = "Change Value",
			disabled = true
		};

		// Token: 0x040015C5 RID: 5573
		public static MenuOption SAVE_VALUE = new MenuOption
		{
			InputCommand = "Accept",
			Description = "Save",
			disabled = true
		};

		// Token: 0x040015C6 RID: 5574
		public static MenuOption CANCEL_VALUE = new MenuOption
		{
			InputCommand = "Cancel",
			Description = "Cancel",
			disabled = true
		};

		// Token: 0x040015C7 RID: 5575
		private bool initializing;

		// Token: 0x02001860 RID: 6240
		private class Context : NavigationContext
		{
		}
	}
}
