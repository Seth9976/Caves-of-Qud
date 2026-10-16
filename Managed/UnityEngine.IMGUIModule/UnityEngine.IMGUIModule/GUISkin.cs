using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[AssetFileNameExtension("guiskin", new string[] { })]
	[RequiredByNativeCode]
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000B462 File Offset: 0x00009662
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B48A File Offset: 0x0000968A
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B494 File Offset: 0x00009694
		internal static void CleanupRoots()
		{
			GUISkin.current = null;
			GUISkin.ms_Error = null;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B4A4 File Offset: 0x000096A4
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000B4BC File Offset: 0x000096BC
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				bool flag = GUISkin.current == this;
				if (flag)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B4F4 File Offset: 0x000096F4
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000B50C File Offset: 0x0000970C
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B520 File Offset: 0x00009720
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000B538 File Offset: 0x00009738
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B54C File Offset: 0x0000974C
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000B564 File Offset: 0x00009764
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B578 File Offset: 0x00009778
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000B590 File Offset: 0x00009790
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B5A4 File Offset: 0x000097A4
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000B5BC File Offset: 0x000097BC
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B5D0 File Offset: 0x000097D0
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B5FC File Offset: 0x000097FC
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000B614 File Offset: 0x00009814
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B628 File Offset: 0x00009828
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000B640 File Offset: 0x00009840
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B654 File Offset: 0x00009854
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000B66C File Offset: 0x0000986C
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B680 File Offset: 0x00009880
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000B698 File Offset: 0x00009898
		internal GUIStyle horizontalSliderThumbExtent
		{
			get
			{
				return this.m_horizontalSliderThumbExtent;
			}
			set
			{
				this.m_horizontalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B6AC File Offset: 0x000098AC
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000B6C4 File Offset: 0x000098C4
		internal GUIStyle sliderMixed
		{
			get
			{
				return this.m_SliderMixed;
			}
			set
			{
				this.m_SliderMixed = value;
				this.Apply();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B6D8 File Offset: 0x000098D8
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B704 File Offset: 0x00009904
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000B71C File Offset: 0x0000991C
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B730 File Offset: 0x00009930
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000B748 File Offset: 0x00009948
		internal GUIStyle verticalSliderThumbExtent
		{
			get
			{
				return this.m_verticalSliderThumbExtent;
			}
			set
			{
				this.m_verticalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B75C File Offset: 0x0000995C
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000B774 File Offset: 0x00009974
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B788 File Offset: 0x00009988
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B7B4 File Offset: 0x000099B4
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000B7CC File Offset: 0x000099CC
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B7E0 File Offset: 0x000099E0
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000B80C File Offset: 0x00009A0C
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000B824 File Offset: 0x00009A24
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B838 File Offset: 0x00009A38
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000B850 File Offset: 0x00009A50
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000B864 File Offset: 0x00009A64
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000B87C File Offset: 0x00009A7C
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B890 File Offset: 0x00009A90
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B8BC File Offset: 0x00009ABC
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B900 File Offset: 0x00009B00
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B914 File Offset: 0x00009B14
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B92C File Offset: 0x00009B2C
		internal static GUIStyle error
		{
			get
			{
				bool flag = GUISkin.ms_Error == null;
				if (flag)
				{
					GUISkin.ms_Error = new GUIStyle();
					GUISkin.ms_Error.name = "StyleNotFoundError";
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B96C File Offset: 0x00009B6C
		internal void Apply()
		{
			bool flag = this.m_CustomStyles == null;
			if (flag)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B99C File Offset: 0x00009B9C
		private void BuildStyleCache()
		{
			bool flag = this.m_box == null;
			if (flag)
			{
				this.m_box = new GUIStyle();
			}
			bool flag2 = this.m_button == null;
			if (flag2)
			{
				this.m_button = new GUIStyle();
			}
			bool flag3 = this.m_toggle == null;
			if (flag3)
			{
				this.m_toggle = new GUIStyle();
			}
			bool flag4 = this.m_label == null;
			if (flag4)
			{
				this.m_label = new GUIStyle();
			}
			bool flag5 = this.m_window == null;
			if (flag5)
			{
				this.m_window = new GUIStyle();
			}
			bool flag6 = this.m_textField == null;
			if (flag6)
			{
				this.m_textField = new GUIStyle();
			}
			bool flag7 = this.m_textArea == null;
			if (flag7)
			{
				this.m_textArea = new GUIStyle();
			}
			bool flag8 = this.m_horizontalSlider == null;
			if (flag8)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			bool flag9 = this.m_horizontalSliderThumb == null;
			if (flag9)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			bool flag10 = this.m_verticalSlider == null;
			if (flag10)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			bool flag11 = this.m_verticalSliderThumb == null;
			if (flag11)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			bool flag12 = this.m_horizontalScrollbar == null;
			if (flag12)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			bool flag13 = this.m_horizontalScrollbarThumb == null;
			if (flag13)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			bool flag14 = this.m_horizontalScrollbarLeftButton == null;
			if (flag14)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			bool flag15 = this.m_horizontalScrollbarRightButton == null;
			if (flag15)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			bool flag16 = this.m_verticalScrollbar == null;
			if (flag16)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			bool flag17 = this.m_verticalScrollbarThumb == null;
			if (flag17)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			bool flag18 = this.m_verticalScrollbarUpButton == null;
			if (flag18)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			bool flag19 = this.m_verticalScrollbarDownButton == null;
			if (flag19)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			bool flag20 = this.m_ScrollView == null;
			if (flag20)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			bool flag21 = this.m_CustomStyles != null;
			if (flag21)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					bool flag22 = this.m_CustomStyles[i] == null;
					if (!flag22)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			bool flag23 = !this.m_Styles.TryGetValue("HorizontalSliderThumbExtent", out this.m_horizontalSliderThumbExtent);
			if (flag23)
			{
				this.m_horizontalSliderThumbExtent = new GUIStyle();
				this.m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
				this.m_Styles["HorizontalSliderThumbExtent"] = this.m_horizontalSliderThumbExtent;
			}
			bool flag24 = !this.m_Styles.TryGetValue("SliderMixed", out this.m_SliderMixed);
			if (flag24)
			{
				this.m_SliderMixed = new GUIStyle();
				this.m_SliderMixed.name = "SliderMixed";
				this.m_Styles["SliderMixed"] = this.m_SliderMixed;
			}
			bool flag25 = !this.m_Styles.TryGetValue("VerticalSliderThumbExtent", out this.m_verticalSliderThumbExtent);
			if (flag25)
			{
				this.m_verticalSliderThumbExtent = new GUIStyle();
				this.m_Styles["VerticalSliderThumbExtent"] = this.m_verticalSliderThumbExtent;
				this.m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C060 File Offset: 0x0000A260
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			bool flag = guistyle != null;
			GUIStyle guistyle2;
			if (flag)
			{
				guistyle2 = guistyle;
			}
			else
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to find style '",
					styleName,
					"' in skin '",
					base.name,
					"' ",
					(Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"
				}));
				guistyle2 = GUISkin.error;
			}
			return guistyle2;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public GUIStyle FindStyle(string styleName)
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			GUIStyle guistyle;
			bool flag2 = this.m_Styles.TryGetValue(styleName, out guistyle);
			GUIStyle guistyle2;
			if (flag2)
			{
				guistyle2 = guistyle;
			}
			else
			{
				guistyle2 = null;
			}
			return guistyle2;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C12C File Offset: 0x0000A32C
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			bool flag = GUISkin.m_SkinChanged != null;
			if (flag)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C164 File Offset: 0x0000A364
		public IEnumerator GetEnumerator()
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}

		// Token: 0x040000AF RID: 175
		[SerializeField]
		private Font m_Font;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x040000B9 RID: 185
		[NonSerialized]
		private GUIStyle m_horizontalSliderThumbExtent;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x040000BC RID: 188
		[NonSerialized]
		private GUIStyle m_verticalSliderThumbExtent;

		// Token: 0x040000BD RID: 189
		[NonSerialized]
		private GUIStyle m_SliderMixed;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x040000C9 RID: 201
		internal static GUIStyle ms_Error;

		// Token: 0x040000CA RID: 202
		private Dictionary<string, GUIStyle> m_Styles = null;

		// Token: 0x040000CB RID: 203
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x040000CC RID: 204
		internal static GUISkin current;

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x0600030D RID: 781
		internal delegate void SkinChangedDelegate();
	}
}
