using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000167 RID: 359
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class PopupField<T> : BasePopupField<T, T>
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00032DE4 File Offset: 0x00030FE4
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x00032DFC File Offset: 0x00030FFC
		public virtual Func<T, string> formatSelectedValueCallback
		{
			get
			{
				return this.m_FormatSelectedValueCallback;
			}
			set
			{
				this.m_FormatSelectedValueCallback = value;
				base.textElement.text = this.GetValueToDisplay();
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00032E18 File Offset: 0x00031018
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x00032E30 File Offset: 0x00031030
		public virtual Func<T, string> formatListItemCallback
		{
			get
			{
				return this.m_FormatListItemCallback;
			}
			set
			{
				this.m_FormatListItemCallback = value;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00032E3C File Offset: 0x0003103C
		internal override string GetValueToDisplay()
		{
			bool flag = this.m_FormatSelectedValueCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatSelectedValueCallback(this.value);
			}
			else
			{
				bool flag2 = this.value != null;
				if (flag2)
				{
					T value = this.value;
					text = UIElementsUtility.ParseMenuName(value.ToString());
				}
				else
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00032EA4 File Offset: 0x000310A4
		internal override string GetListItemToDisplay(T value)
		{
			bool flag = this.m_FormatListItemCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatListItemCallback(value);
			}
			else
			{
				text = ((value != null && this.m_Choices.Contains(value)) ? value.ToString() : string.Empty);
			}
			return text;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00032EFC File Offset: 0x000310FC
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x00032F14 File Offset: 0x00031114
		public override T value
		{
			get
			{
				return base.value;
			}
			set
			{
				int index = this.m_Index;
				List<T> choices = this.m_Choices;
				this.m_Index = ((choices != null) ? choices.IndexOf(value) : (-1));
				base.value = value;
				bool flag = this.m_Index != index;
				if (flag)
				{
					base.NotifyPropertyChanged(in PopupField<T>.indexProperty);
				}
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00032F66 File Offset: 0x00031166
		public override void SetValueWithoutNotify(T newValue)
		{
			List<T> choices = this.m_Choices;
			this.m_Index = ((choices != null) ? choices.IndexOf(newValue) : (-1));
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00032F8C File Offset: 0x0003118C
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00032FA4 File Offset: 0x000311A4
		[CreateProperty]
		public int index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				bool flag = value != this.m_Index;
				if (flag)
				{
					this.m_Index = value;
					bool flag2 = this.m_Index >= 0 && this.m_Index < this.m_Choices.Count;
					if (flag2)
					{
						this.value = this.m_Choices[this.m_Index];
					}
					else
					{
						this.value = default(T);
					}
					base.NotifyPropertyChanged(in PopupField<T>.indexProperty);
				}
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00033024 File Offset: 0x00031224
		public PopupField()
			: this(null)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003302F File Offset: 0x0003122F
		public PopupField(string label = null)
			: base(label)
		{
			base.AddToClassList(PopupField<T>.ussClassName);
			base.labelElement.AddToClassList(PopupField<T>.labelUssClassName);
			base.visualInput.AddToClassList(PopupField<T>.inputUssClassName);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003306F File Offset: 0x0003126F
		public PopupField(List<T> choices, T defaultValue, Func<T, string> formatSelectedValueCallback = null, Func<T, string> formatListItemCallback = null)
			: this(null, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00033080 File Offset: 0x00031280
		public PopupField(string label, List<T> choices, T defaultValue, Func<T, string> formatSelectedValueCallback = null, Func<T, string> formatListItemCallback = null)
			: this(label)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this.choices = choices;
			bool flag2 = !this.m_Choices.Contains(defaultValue);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Default value {0} is not present in the list of possible values", defaultValue));
			}
			this.SetValueWithoutNotify(defaultValue);
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000330F9 File Offset: 0x000312F9
		public PopupField(List<T> choices, int defaultIndex, Func<T, string> formatSelectedValueCallback = null, Func<T, string> formatListItemCallback = null)
			: this(null, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00033109 File Offset: 0x00031309
		public PopupField(string label, List<T> choices, int defaultIndex, Func<T, string> formatSelectedValueCallback = null, Func<T, string> formatListItemCallback = null)
			: this(label)
		{
			this.choices = choices;
			this.SetIndexWithoutNotify(defaultIndex);
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00033138 File Offset: 0x00031338
		internal override void AddMenuItems(IGenericMenu menu)
		{
			bool flag = menu == null;
			if (flag)
			{
				throw new ArgumentNullException("menu");
			}
			bool flag2 = this.m_Choices == null;
			if (!flag2)
			{
				using (List<T>.Enumerator enumerator = this.m_Choices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T item = enumerator.Current;
						bool flag3 = EqualityComparer<T>.Default.Equals(item, this.value) && !base.showMixedValue;
						menu.AddItem(this.GetListItemToDisplay(item), flag3, delegate
						{
							this.ChangeValueFromMenu(item);
						});
					}
				}
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00033208 File Offset: 0x00031408
		internal void SetIndexWithoutNotify(int index)
		{
			this.m_Index = index;
			bool flag = this.m_Index >= 0 && this.m_Index < this.m_Choices.Count;
			if (flag)
			{
				this.SetValueWithoutNotify(this.m_Choices[this.m_Index]);
			}
			else
			{
				this.SetValueWithoutNotify(default(T));
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003326A File Offset: 0x0003146A
		private void ChangeValueFromMenu(T menuItem)
		{
			this.value = menuItem;
		}

		// Token: 0x040006CB RID: 1739
		internal static readonly BindingId indexProperty = "index";

		// Token: 0x040006CC RID: 1740
		internal const int kPopupFieldDefaultIndex = -1;

		// Token: 0x040006CD RID: 1741
		private int m_Index = -1;

		// Token: 0x040006CE RID: 1742
		public new static readonly string ussClassName = "unity-popup-field";

		// Token: 0x040006CF RID: 1743
		public new static readonly string labelUssClassName = PopupField<T>.ussClassName + "__label";

		// Token: 0x040006D0 RID: 1744
		public new static readonly string inputUssClassName = PopupField<T>.ussClassName + "__input";
	}
}
