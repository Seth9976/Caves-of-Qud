using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using ModelShark;
using TMPro;
using UnityEngine;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005BF RID: 1471
	[RequireComponent(typeof(FrameworkContext))]
	public class AttributeSelectionControl : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x06003EB2 RID: 16050 RVA: 0x00133D5C File Offset: 0x00131F5C
		public void raise()
		{
			this.data.raise();
			this.Updated();
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00133D6F File Offset: 0x00131F6F
		public void lower()
		{
			this.data.lower();
			this.Updated();
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00133D82 File Offset: 0x00131F82
		public void setAttributeText(string value)
		{
			this.attribute.text = value;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00133D90 File Offset: 0x00131F90
		public void setValueText(string value)
		{
			this.value.text = value;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x00133D9E File Offset: 0x00131F9E
		public void setModifierText(string value)
		{
			this.modifier.text = value;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00133DAC File Offset: 0x00131FAC
		public void Updated()
		{
			int num = this.data.Value;
			this.attribute.text = this.data.Attribute.Substring(0, 3).ToUpper();
			this.value.text = num.ToString();
			this.value.color = ConsoleLib.Console.ColorUtility._ColorMap[(this.data.Bonus > 0) ? 'G' : ((this.data.Bonus < 0) ? 'R' : 'c')];
			this.tooltip.SetText("BodyText", Sidebar.FormatToRTF(this.data.BonusSource, "FF"));
			this._wasActive = new bool?(false);
			this.modifier.text = "[" + Stat.GetScoreModifier(num).ToString() + "]";
			this.subtractButton.gameObject.SetActive(true);
			this.addButton.gameObject.SetActive(true);
			base.GetComponent<TitledIconButton>().SetTitle("[" + this.data.APToRaise.ToString() + "pts]");
			this.data.Updated();
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x00133EE7 File Offset: 0x001320E7
		public bool isActive
		{
			get
			{
				return this._wasActive.GetValueOrDefault();
			}
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x00133EF4 File Offset: 0x001320F4
		public void Update()
		{
			bool flag = this.navContext.IsActive(true);
			bool? wasActive = this._wasActive;
			bool flag2 = flag;
			if (!((wasActive.GetValueOrDefault() == flag2) & (wasActive != null)))
			{
				this._wasActive = new bool?(flag);
				if (!flag && this.tooltip.IsDisplayed())
				{
					Debug.Log("Hide");
					this.tooltip.HidePopup();
				}
			}
			if (flag)
			{
				AttributeDataElement attributeDataElement = this.data;
				if (!string.IsNullOrEmpty((attributeDataElement != null) ? attributeDataElement.BonusSource : null) && !this.tooltip.IsDisplayed())
				{
					Debug.Log("Show");
					this.tooltip.ShowManually(true);
				}
			}
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00133F9C File Offset: 0x0013219C
		public void setData(FrameworkDataElement d)
		{
			base.GetComponent<FrameworkContext>().context = this.navContext;
			this.data = d as AttributeDataElement;
			this.data.control = this;
			this.Updated();
			ScrollChildContext scrollChildContext = this.addButton.RequireContext<ScrollChildContext>();
			scrollChildContext.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
			{
				InputButtonTypes.AcceptButton,
				XRL.UI.Framework.Event.Helpers.Handle(new Action(this.raise))
			} };
			ScrollChildContext scrollChildContext2 = this.subtractButton.RequireContext<ScrollChildContext>();
			scrollChildContext2.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
			{
				InputButtonTypes.AcceptButton,
				XRL.UI.Framework.Event.Helpers.Handle(new Action(this.lower))
			} };
			this.navContext.contexts = new List<NavigationContext> { scrollChildContext, scrollChildContext2 };
			this.navContext.wraps = false;
			this.navContext.data = new List<int> { 1, -1 };
			this.navContext.SetAxis(InputAxisTypes.NavigationYAxis, false);
			Dictionary<InputAxisTypes, Action> axisHandlers = this.navContext.axisHandlers;
			InputAxisTypes inputAxisTypes = InputAxisTypes.NavigationPageYAxis;
			Action action = new Action(this.raise);
			axisHandlers.Add(inputAxisTypes, XRL.UI.Framework.Event.Helpers.Handle(XRL.UI.Framework.Event.Helpers.Axis(new Action(this.lower), action, null)));
			this.navContext.axisHandlers.Add(InputAxisTypes.NavigationVAxis, XRL.UI.Framework.Event.Helpers.Handle(XRL.UI.Framework.Event.Helpers.Axis(new Action(this.raise), new Action(this.lower), null)));
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x001340F5 File Offset: 0x001322F5
		public NavigationContext GetNavigationContext()
		{
			return this.navContext;
		}

		// Token: 0x04001EE2 RID: 7906
		public AttributeDataElement data;

		// Token: 0x04001EE3 RID: 7907
		public TextMeshProUGUI attribute;

		// Token: 0x04001EE4 RID: 7908
		public TextMeshProUGUI value;

		// Token: 0x04001EE5 RID: 7909
		public TextMeshProUGUI modifier;

		// Token: 0x04001EE6 RID: 7910
		public FrameworkContext addButton;

		// Token: 0x04001EE7 RID: 7911
		public FrameworkContext subtractButton;

		// Token: 0x04001EE8 RID: 7912
		public TooltipTrigger tooltip;

		// Token: 0x04001EE9 RID: 7913
		public ScrollContext<int, NavigationContext> navContext = new ScrollContext<int, NavigationContext>();

		// Token: 0x04001EEA RID: 7914
		private bool? _wasActive;
	}
}
