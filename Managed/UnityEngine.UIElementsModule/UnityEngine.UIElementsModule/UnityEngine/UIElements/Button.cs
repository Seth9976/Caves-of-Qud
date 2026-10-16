using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B6 RID: 182
	public class Button : TextElement
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001FDD0 File Offset: 0x0001DFD0
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060006F2 RID: 1778 RVA: 0x0001FE45 File Offset: 0x0001E045
		// (remove) Token: 0x060006F3 RID: 1779 RVA: 0x0001FE50 File Offset: 0x0001E050
		[Obsolete("onClick is obsolete. Use clicked instead (UnityUpgradable) -> clicked", true)]
		public event Action onClick
		{
			add
			{
				this.clicked += value;
			}
			remove
			{
				this.clicked -= value;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060006F4 RID: 1780 RVA: 0x0001FE5C File Offset: 0x0001E05C
		// (remove) Token: 0x060006F5 RID: 1781 RVA: 0x0001FE98 File Offset: 0x0001E098
		public event Action clicked
		{
			add
			{
				bool flag = this.m_Clickable == null;
				if (flag)
				{
					this.clickable = new Clickable(value);
				}
				else
				{
					this.m_Clickable.clicked += value;
				}
			}
			remove
			{
				bool flag = this.m_Clickable != null;
				if (flag)
				{
					this.m_Clickable.clicked -= value;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001FEDF File Offset: 0x0001E0DF
		private Object iconImageReference
		{
			get
			{
				return this.iconImage.GetSelectedImage();
			}
			set
			{
				this.iconImage = Background.FromObject(value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001FEEE File Offset: 0x0001E0EE
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		[CreateProperty]
		public Background iconImage
		{
			get
			{
				return this.m_IconImage;
			}
			set
			{
				bool flag = (value.IsEmpty() && this.m_ImageElement == null) || value == this.m_IconImage;
				if (!flag)
				{
					bool flag2 = value.IsEmpty();
					if (flag2)
					{
						this.m_IconImage = value;
						this.ResetButtonHierarchy();
						base.NotifyPropertyChanged(in Button.iconImageProperty);
					}
					else
					{
						bool flag3 = this.m_ImageElement == null;
						if (flag3)
						{
							this.UpdateButtonHierarchy();
						}
						bool flag4 = value.texture;
						if (flag4)
						{
							this.m_ImageElement.image = value.texture;
						}
						else
						{
							bool flag5 = value.sprite;
							if (flag5)
							{
								this.m_ImageElement.sprite = value.sprite;
							}
							else
							{
								bool flag6 = value.renderTexture;
								if (flag6)
								{
									this.m_ImageElement.image = value.renderTexture;
								}
								else
								{
									this.m_ImageElement.vectorImage = value.vectorImage;
								}
							}
						}
						this.m_IconImage = value;
						base.EnableInClassList(Button.iconOnlyUssClassName, string.IsNullOrEmpty(this.text));
						base.NotifyPropertyChanged(in Button.iconImageProperty);
					}
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0002001E File Offset: 0x0001E21E
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00020030 File Offset: 0x0001E230
		public override string text
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				this.m_Text = value;
				base.EnableInClassList(Button.iconOnlyUssClassName, !this.m_IconImage.IsEmpty() && string.IsNullOrEmpty(this.text));
				bool flag = this.m_TextElement != null;
				if (flag)
				{
					base.text = string.Empty;
					bool flag2 = this.m_TextElement.text == this.m_Text;
					if (!flag2)
					{
						this.m_TextElement.text = this.m_Text;
					}
				}
				else
				{
					bool flag3 = base.text == this.m_Text;
					if (!flag3)
					{
						base.text = this.m_Text;
					}
				}
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000200DC File Offset: 0x0001E2DC
		public Button()
			: this(default(Background), null)
		{
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000200FB File Offset: 0x0001E2FB
		public Button(Background iconImage, Action clickEvent = null)
			: this(clickEvent)
		{
			this.iconImage = iconImage;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00020110 File Offset: 0x0001E310
		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new Clickable(clickEvent);
			this.focusable = true;
			base.tabIndex = 0;
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0002016D File Offset: 0x0001E36D
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			Clickable clickable = this.clickable;
			if (clickable != null)
			{
				clickable.SimulateSingleClick(evt, 100);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002018C File Offset: 0x0001E38C
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string text = this.text;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Button.NonEmptyString;
			}
			return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000201C4 File Offset: 0x0001E3C4
		private void UpdateButtonHierarchy()
		{
			bool flag = this.m_ImageElement == null;
			if (flag)
			{
				this.m_ImageElement = new Image
				{
					classList = { Button.imageUSSClassName }
				};
				base.Add(this.m_ImageElement);
				base.AddToClassList(Button.iconUssClassName);
			}
			bool flag2 = this.m_TextElement == null;
			if (flag2)
			{
				this.m_TextElement = new TextElement
				{
					text = this.text
				};
				this.m_Text = this.text;
				base.text = string.Empty;
				base.Add(this.m_TextElement);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020264 File Offset: 0x0001E464
		private void ResetButtonHierarchy()
		{
			bool flag = this.m_ImageElement != null;
			if (flag)
			{
				this.m_ImageElement.RemoveFromHierarchy();
				this.m_ImageElement = null;
				base.RemoveFromClassList(Button.iconUssClassName);
				base.RemoveFromClassList(Button.iconOnlyUssClassName);
			}
			bool flag2 = this.m_TextElement != null;
			if (flag2)
			{
				string text = this.m_TextElement.text;
				this.m_TextElement.RemoveFromHierarchy();
				this.m_TextElement = null;
				this.text = text;
			}
		}

		// Token: 0x040003D6 RID: 982
		internal static readonly BindingId iconImageProperty = "iconImage";

		// Token: 0x040003D7 RID: 983
		public new static readonly string ussClassName = "unity-button";

		// Token: 0x040003D8 RID: 984
		public static readonly string iconUssClassName = Button.ussClassName + "--with-icon";

		// Token: 0x040003D9 RID: 985
		public static readonly string iconOnlyUssClassName = Button.ussClassName + "--with-icon-only";

		// Token: 0x040003DA RID: 986
		public static readonly string imageUSSClassName = Button.ussClassName + "__image";

		// Token: 0x040003DB RID: 987
		private Clickable m_Clickable;

		// Token: 0x040003DC RID: 988
		private TextElement m_TextElement;

		// Token: 0x040003DD RID: 989
		private Image m_ImageElement;

		// Token: 0x040003DE RID: 990
		private Background m_IconImage;

		// Token: 0x040003DF RID: 991
		private string m_Text = string.Empty;

		// Token: 0x040003E0 RID: 992
		private static readonly string NonEmptyString = " ";

		// Token: 0x020000B7 RID: 183
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextElement.UxmlSerializedData
		{
			// Token: 0x06000704 RID: 1796 RVA: 0x00020350 File Offset: 0x0001E550
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Button.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("iconImageReference", "icon-image", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x00020385 File Offset: 0x0001E585
			public override object CreateInstance()
			{
				return new Button();
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x0002038C File Offset: 0x0001E58C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.iconImageReference_UxmlAttributeFlags);
				if (flag)
				{
					Button button = (Button)obj;
					button.iconImageReference = this.iconImageReference;
				}
			}

			// Token: 0x040003E1 RID: 993
			[ImageFieldValueDecorator]
			[SerializeField]
			[UxmlAttribute("icon-image")]
			[UxmlAttributeBindingPath("iconImage")]
			private Object iconImageReference;

			// Token: 0x040003E2 RID: 994
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags iconImageReference_UxmlAttributeFlags;
		}

		// Token: 0x020000B8 RID: 184
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
		}

		// Token: 0x020000B9 RID: 185
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000709 RID: 1801 RVA: 0x000203D9 File Offset: 0x0001E5D9
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x00020408 File Offset: 0x0001E608
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Button button = (Button)ve;
				button.iconImage = this.m_IconImage.GetValueFromBag(bag, cc);
			}

			// Token: 0x040003E3 RID: 995
			private readonly UxmlImageAttributeDescription m_IconImage = new UxmlImageAttributeDescription
			{
				name = "icon-image"
			};
		}
	}
}
