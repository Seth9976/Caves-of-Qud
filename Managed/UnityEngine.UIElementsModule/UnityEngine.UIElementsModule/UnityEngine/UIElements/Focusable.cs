using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B7 RID: 695
	public abstract class Focusable : CallbackEventHandler
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x0004F740 File Offset: 0x0004D940
		protected Focusable()
		{
			UIElementsRuntimeUtilityNative.VisualElementCreation();
			this.focusable = true;
			this.tabIndex = 0;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060013CA RID: 5066
		public abstract FocusController focusController { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0004F767 File Offset: 0x0004D967
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x0004F770 File Offset: 0x0004D970
		[CreateProperty]
		public virtual bool focusable
		{
			get
			{
				return this.m_Focusable;
			}
			set
			{
				bool flag = this.m_Focusable == value;
				if (!flag)
				{
					this.m_Focusable = value;
					base.NotifyPropertyChanged(in Focusable.focusableProperty);
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0004F7A0 File Offset: 0x0004D9A0
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		[CreateProperty]
		public int tabIndex
		{
			get
			{
				return this.m_TabIndex;
			}
			set
			{
				bool flag = this.m_TabIndex == value;
				if (!flag)
				{
					this.m_TabIndex = value;
					base.NotifyPropertyChanged(in Focusable.tabIndexProperty);
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0004F7F0 File Offset: 0x0004D9F0
		[CreateProperty]
		public bool delegatesFocus
		{
			get
			{
				return this.m_DelegatesFocus;
			}
			set
			{
				bool flag = this.m_DelegatesFocus == value;
				if (!flag)
				{
					this.m_DelegatesFocus = value;
					base.NotifyPropertyChanged(in Focusable.delegatesFocusProperty);
				}
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0004F820 File Offset: 0x0004DA20
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0004F838 File Offset: 0x0004DA38
		internal bool excludeFromFocusRing
		{
			get
			{
				return this.m_ExcludeFromFocusRing;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("excludeFromFocusRing should only be set on composite roots.");
				}
				this.m_ExcludeFromFocusRing = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0004F86B File Offset: 0x0004DA6B
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0004F873 File Offset: 0x0004DA73
		internal bool isEligibleToReceiveFocusFromDisabledChild { get; set; } = true;

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004F87C File Offset: 0x0004DA7C
		[CreateProperty(ReadOnly = true)]
		public virtual bool canGrabFocus
		{
			get
			{
				return this.focusable;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004F884 File Offset: 0x0004DA84
		public virtual void Focus()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool canGrabFocus = this.canGrabFocus;
				if (canGrabFocus)
				{
					Focusable focusDelegate = this.GetFocusDelegate();
					this.focusController.SwitchFocus(focusDelegate, this != focusDelegate, DispatchMode.Default);
				}
				else
				{
					this.focusController.SwitchFocus(null, false, DispatchMode.Default);
				}
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004F8DC File Offset: 0x0004DADC
		public virtual void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Default);
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004F8F4 File Offset: 0x0004DAF4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void BlurImmediately()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Immediate);
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004F90C File Offset: 0x0004DB0C
		internal Focusable GetFocusDelegate()
		{
			Focusable focusable = this;
			while (focusable != null && focusable.delegatesFocus)
			{
				focusable = Focusable.GetFirstFocusableChild(focusable as VisualElement);
			}
			return focusable;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004F944 File Offset: 0x0004DB44
		private static Focusable GetFirstFocusableChild(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			int i = 0;
			while (i < childCount)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.canGrabFocus && visualElement.tabIndex >= 0;
				if (!flag)
				{
					bool flag2 = visualElement.hierarchy.parent != null && visualElement == visualElement.hierarchy.parent.contentContainer;
					bool flag3 = !visualElement.isCompositeRoot && !flag2;
					if (flag3)
					{
						Focusable firstFocusableChild = Focusable.GetFirstFocusableChild(visualElement);
						bool flag4 = firstFocusableChild != null;
						if (flag4)
						{
							return firstFocusableChild;
						}
					}
					i++;
					continue;
				}
				return visualElement;
			}
			return null;
		}

		// Token: 0x04000ABF RID: 2751
		internal static readonly BindingId focusableProperty = "focusable";

		// Token: 0x04000AC0 RID: 2752
		internal static readonly BindingId tabIndexProperty = "tabIndex";

		// Token: 0x04000AC1 RID: 2753
		internal static readonly BindingId delegatesFocusProperty = "delegatesFocus";

		// Token: 0x04000AC2 RID: 2754
		internal static readonly BindingId canGrabFocusProperty = "canGrabFocus";

		// Token: 0x04000AC3 RID: 2755
		private bool m_Focusable;

		// Token: 0x04000AC4 RID: 2756
		private int m_TabIndex;

		// Token: 0x04000AC5 RID: 2757
		private bool m_DelegatesFocus;

		// Token: 0x04000AC6 RID: 2758
		private bool m_ExcludeFromFocusRing;
	}
}
