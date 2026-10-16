using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the category in which to group the property or event when displayed in a <see cref="T:System.Windows.Forms.PropertyGrid" /> control set to Categorized mode.</summary>
	// Token: 0x0200040D RID: 1037
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Action category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the action category.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00072272 File Offset: 0x00070472
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.action == null)
				{
					CategoryAttribute.action = new CategoryAttribute("Action");
				}
				return CategoryAttribute.action;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Appearance category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the appearance category.</returns>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00072295 File Offset: 0x00070495
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.appearance == null)
				{
					CategoryAttribute.appearance = new CategoryAttribute("Appearance");
				}
				return CategoryAttribute.appearance;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Asynchronous category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the asynchronous category.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000722B8 File Offset: 0x000704B8
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.asynchronous == null)
				{
					CategoryAttribute.asynchronous = new CategoryAttribute("Asynchronous");
				}
				return CategoryAttribute.asynchronous;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Behavior category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the behavior category.</returns>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x000722DB File Offset: 0x000704DB
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.behavior == null)
				{
					CategoryAttribute.behavior = new CategoryAttribute("Behavior");
				}
				return CategoryAttribute.behavior;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Data category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the data category.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x000722FE File Offset: 0x000704FE
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.data == null)
				{
					CategoryAttribute.data = new CategoryAttribute("Data");
				}
				return CategoryAttribute.data;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Default category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the default category.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x00072321 File Offset: 0x00070521
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.defAttr == null)
				{
					CategoryAttribute.defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.defAttr;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Design category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the design category.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x0007233F File Offset: 0x0007053F
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.design == null)
				{
					CategoryAttribute.design = new CategoryAttribute("Design");
				}
				return CategoryAttribute.design;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the DragDrop category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the drag-and-drop category.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x00072362 File Offset: 0x00070562
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.dragDrop == null)
				{
					CategoryAttribute.dragDrop = new CategoryAttribute("DragDrop");
				}
				return CategoryAttribute.dragDrop;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Focus category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the focus category.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x00072385 File Offset: 0x00070585
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.focus == null)
				{
					CategoryAttribute.focus = new CategoryAttribute("Focus");
				}
				return CategoryAttribute.focus;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Format category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the format category.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000723A8 File Offset: 0x000705A8
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.format == null)
				{
					CategoryAttribute.format = new CategoryAttribute("Format");
				}
				return CategoryAttribute.format;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Key category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the key category.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000723CB File Offset: 0x000705CB
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.key == null)
				{
					CategoryAttribute.key = new CategoryAttribute("Key");
				}
				return CategoryAttribute.key;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Layout category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the layout category.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000723EE File Offset: 0x000705EE
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.layout == null)
				{
					CategoryAttribute.layout = new CategoryAttribute("Layout");
				}
				return CategoryAttribute.layout;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Mouse category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the mouse category.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x00072411 File Offset: 0x00070611
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.mouse == null)
				{
					CategoryAttribute.mouse = new CategoryAttribute("Mouse");
				}
				return CategoryAttribute.mouse;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the WindowStyle category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the window style category.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x00072434 File Offset: 0x00070634
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.windowStyle == null)
				{
					CategoryAttribute.windowStyle = new CategoryAttribute("WindowStyle");
				}
				return CategoryAttribute.windowStyle;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the category name Default.</summary>
		// Token: 0x06002179 RID: 8569 RVA: 0x00072457 File Offset: 0x00070657
		public CategoryAttribute()
			: this("Default")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the specified category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x0600217A RID: 8570 RVA: 0x00072464 File Offset: 0x00070664
		public CategoryAttribute(string category)
		{
			this.categoryValue = category;
			this.localized = false;
		}

		/// <summary>Gets the name of the category for the property or event that this attribute is applied to.</summary>
		/// <returns>The name of the category for the property or event that this attribute is applied to.</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0007247C File Offset: 0x0007067C
		public string Category
		{
			get
			{
				if (!this.localized)
				{
					this.localized = true;
					string localizedString = this.GetLocalizedString(this.categoryValue);
					if (localizedString != null)
					{
						this.categoryValue = localizedString;
					}
				}
				return this.categoryValue;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.CategoryAttribute" />..</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600217C RID: 8572 RVA: 0x000724B5 File Offset: 0x000706B5
		public override bool Equals(object obj)
		{
			return obj == this || (obj is CategoryAttribute && this.Category.Equals(((CategoryAttribute)obj).Category));
		}

		/// <summary>Returns the hash code for this attribute.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600217D RID: 8573 RVA: 0x000724DD File Offset: 0x000706DD
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Looks up the localized name of the specified category.</summary>
		/// <returns>The localized name of the category, or null if a localized name does not exist.</returns>
		/// <param name="value">The identifer for the category to look up. </param>
		// Token: 0x0600217E RID: 8574 RVA: 0x000724EC File Offset: 0x000706EC
		protected virtual string GetLocalizedString(string value)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 1062369733U)
			{
				if (num <= 630759034U)
				{
					if (num <= 433860734U)
					{
						if (num != 175614239U)
						{
							if (num == 433860734U)
							{
								if (value == "Default")
								{
									return "Misc";
								}
							}
						}
						else if (value == "Action")
						{
							return "Action";
						}
					}
					else if (num != 521774151U)
					{
						if (num == 630759034U)
						{
							if (value == "DragDrop")
							{
								return "Drag Drop";
							}
						}
					}
					else if (value == "Behavior")
					{
						return "Behavior";
					}
				}
				else if (num <= 723360612U)
				{
					if (num != 676498961U)
					{
						if (num == 723360612U)
						{
							if (value == "Mouse")
							{
								return "Mouse";
							}
						}
					}
					else if (value == "Scale")
					{
						return "Scale";
					}
				}
				else if (num != 822184863U)
				{
					if (num != 1041509726U)
					{
						if (num == 1062369733U)
						{
							if (value == "Data")
							{
								return "Data";
							}
						}
					}
					else if (value == "Text")
					{
						return "Text";
					}
				}
				else if (value == "Appearance")
				{
					return "Appearance";
				}
			}
			else if (num <= 2809814704U)
			{
				if (num <= 1779622119U)
				{
					if (num != 1762750224U)
					{
						if (num == 1779622119U)
						{
							if (value == "Config")
							{
								return "Configurations";
							}
						}
					}
					else if (value == "DDE")
					{
						return "DDE";
					}
				}
				else if (num != 2055433310U)
				{
					if (num != 2368288673U)
					{
						if (num == 2809814704U)
						{
							if (value == "Font")
							{
								return "Font";
							}
						}
					}
					else if (value == "List")
					{
						return "List";
					}
				}
				else if (value == "WindowStyle")
				{
					return "Window Style";
				}
			}
			else if (num <= 3441084684U)
			{
				if (num != 3159863731U)
				{
					if (num == 3441084684U)
					{
						if (value == "Key")
						{
							return "Key";
						}
					}
				}
				else if (value == "Focus")
				{
					return "Focus";
				}
			}
			else if (num != 3799987242U)
			{
				if (num != 3901555439U)
				{
					if (num == 4152902175U)
					{
						if (value == "Layout")
						{
							return "Layout";
						}
					}
				}
				else if (value == "Design")
				{
					return "Design";
				}
			}
			else if (value == "Position")
			{
				return "Position";
			}
			return value;
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600217F RID: 8575 RVA: 0x0007282A File Offset: 0x00070A2A
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(CategoryAttribute.Default.Category);
		}

		// Token: 0x04001026 RID: 4134
		private static volatile CategoryAttribute appearance;

		// Token: 0x04001027 RID: 4135
		private static volatile CategoryAttribute asynchronous;

		// Token: 0x04001028 RID: 4136
		private static volatile CategoryAttribute behavior;

		// Token: 0x04001029 RID: 4137
		private static volatile CategoryAttribute data;

		// Token: 0x0400102A RID: 4138
		private static volatile CategoryAttribute design;

		// Token: 0x0400102B RID: 4139
		private static volatile CategoryAttribute action;

		// Token: 0x0400102C RID: 4140
		private static volatile CategoryAttribute format;

		// Token: 0x0400102D RID: 4141
		private static volatile CategoryAttribute layout;

		// Token: 0x0400102E RID: 4142
		private static volatile CategoryAttribute mouse;

		// Token: 0x0400102F RID: 4143
		private static volatile CategoryAttribute key;

		// Token: 0x04001030 RID: 4144
		private static volatile CategoryAttribute focus;

		// Token: 0x04001031 RID: 4145
		private static volatile CategoryAttribute windowStyle;

		// Token: 0x04001032 RID: 4146
		private static volatile CategoryAttribute dragDrop;

		// Token: 0x04001033 RID: 4147
		private static volatile CategoryAttribute defAttr;

		// Token: 0x04001034 RID: 4148
		private bool localized;

		// Token: 0x04001035 RID: 4149
		private string categoryValue;
	}
}
