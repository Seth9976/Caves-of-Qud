using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data item in a data-bound list control. This class cannot be inherited.</summary>
	// Token: 0x020003BC RID: 956
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true, "Text")]
	[ControlBuilder(typeof(ListItemControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ListItem : IAttributeAccessor, IParserAccessor, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text, value, and enabled data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
		/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
		/// <param name="enabled">Indicates whether the <see cref="T:System.Web.UI.WebControls.ListItem" /> is enabled.</param>
		// Token: 0x06002796 RID: 10134 RVA: 0x00064FB5 File Offset: 0x000631B5
		public ListItem(string text, string value, bool enabled)
			: this(text, value)
		{
			this.enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text and value data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x06002797 RID: 10135 RVA: 0x00064FC6 File Offset: 0x000631C6
		public ListItem(string text, string value)
		{
			this.text = text;
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x06002798 RID: 10136 RVA: 0x00064FE3 File Offset: 0x000631E3
		public ListItem(string text)
			: this(text, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class.</summary>
		// Token: 0x06002799 RID: 10137 RVA: 0x00064FED File Offset: 0x000631ED
		public ListItem()
			: this(null, null)
		{
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.ListItem" /> from the specified text.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that represents the text specified by the <paramref name="s" /> parameter.</returns>
		/// <param name="s">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x0600279A RID: 10138 RVA: 0x00064FF7 File Offset: 0x000631F7
		public static ListItem FromString(string s)
		{
			return new ListItem(s);
		}

		/// <summary>Determines whether the specified object has the same value and text as the current list item.</summary>
		/// <returns>true if the specified object is equivalent to the current list item; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current list item.</param>
		// Token: 0x0600279B RID: 10139 RVA: 0x00065000 File Offset: 0x00063200
		public override bool Equals(object o)
		{
			ListItem listItem = o as ListItem;
			return listItem != null && listItem.Text == this.Text && listItem.Value == this.Value;
		}

		/// <summary>Serves as a hash function for a particular type, and is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		// Token: 0x0600279C RID: 10140 RVA: 0x0006503F File Offset: 0x0006323F
		public override int GetHashCode()
		{
			return this.Text.GetHashCode() ^ this.Value.GetHashCode();
		}

		/// <summary>Returns the attribute value of the list item control having the specified attribute name.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="name">The name component of an attribute's name/value pair. </param>
		// Token: 0x0600279D RID: 10141 RVA: 0x00065058 File Offset: 0x00063258
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this.attrs == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		/// <summary>Sets an attribute of the list item control with the specified name and value.</summary>
		/// <param name="name">The name component of the attribute's name/value pair. </param>
		/// <param name="value">The value component of the attribute's name/value pair. </param>
		// Token: 0x0600279E RID: 10142 RVA: 0x00065070 File Offset: 0x00063270
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		/// <summary>Allows the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property to be persisted as inner content.</summary>
		/// <param name="obj">The specified object that is parsed. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is a <see cref="T:System.Web.UI.DataBoundLiteralControl" />.- or -<paramref name="obj" /> is not a <see cref="T:System.Web.UI.LiteralControl" />. </exception>
		// Token: 0x0600279F RID: 10143 RVA: 0x00065080 File Offset: 0x00063280
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = "'ListItem' cannot have children of type ";
				Type type = obj.GetType();
				throw new HttpException(text + ((type != null) ? type.ToString() : null));
			}
			this.Text = literalControl.Text;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
		// Token: 0x060027A0 RID: 10144 RVA: 0x000650C5 File Offset: 0x000632C5
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000650D0 File Offset: 0x000632D0
		internal void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			if (array[0] != null)
			{
				this.sb = new StateBag(true);
				this.sb.LoadViewState(array[0]);
				this.sb.SetDirty(true);
			}
			if (array[1] != null)
			{
				this.text = (string)array[1];
			}
			if (array[2] != null)
			{
				this.value = (string)array[2];
			}
			if (array[3] != null)
			{
				this.selected = (bool)array[3];
			}
			if (array[4] != null)
			{
				this.enabled = (bool)array[4];
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060027A2 RID: 10146 RVA: 0x0006515F File Offset: 0x0006335F
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00065168 File Offset: 0x00063368
		internal object SaveViewState()
		{
			if (!this.dirty)
			{
				return null;
			}
			return new object[]
			{
				(this.sb != null) ? this.sb.SaveViewState() : null,
				this.text,
				this.value,
				this.selected,
				this.enabled
			};
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x060027A4 RID: 10148 RVA: 0x000651CC File Offset: 0x000633CC
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000651D4 File Offset: 0x000633D4
		internal void TrackViewState()
		{
			this.tracking = true;
			if (this.sb != null)
			{
				this.sb.TrackViewState();
				this.sb.SetDirty(true);
			}
		}

		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060027A6 RID: 10150 RVA: 0x000651FC File Offset: 0x000633FC
		public override string ToString()
		{
			return this.Text;
		}

		/// <summary>Gets a collection of attribute name and value pairs for the <see cref="T:System.Web.UI.WebControls.ListItem" /> that are not directly supported by the class.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> that contains a collection of name and value pairs.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00065204 File Offset: 0x00063404
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attrs != null)
				{
					return this.attrs;
				}
				if (this.sb == null)
				{
					this.sb = new StateBag(true);
					if (this.tracking)
					{
						this.sb.TrackViewState();
					}
				}
				return this.attrs = new AttributeCollection(this.sb);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true if view state is being tracked; otherwise false.  The default is true.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0006525B File Offset: 0x0006345B
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
		/// <returns>true if the item is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00065263 File Offset: 0x00063463
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x0006526B File Offset: 0x0006346B
		[DefaultValue(false)]
		[TypeConverter("System.Web.UI.MinimizableAttributeTypeConverter")]
		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		/// <summary>Gets or sets the text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
		/// <returns>The text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00065284 File Offset: 0x00063484
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x000652AC File Offset: 0x000634AC
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				string empty = this.text;
				if (empty == null)
				{
					empty = this.value;
				}
				if (empty == null)
				{
					empty = string.Empty;
				}
				return empty;
			}
			set
			{
				this.text = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x000652C4 File Offset: 0x000634C4
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x000652EC File Offset: 0x000634EC
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				string empty = this.value;
				if (empty == null)
				{
					empty = this.text;
				}
				if (empty == null)
				{
					empty = string.Empty;
				}
				return empty;
			}
			set
			{
				this.value = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00065303 File Offset: 0x00063503
		internal void SetDirty()
		{
			this.dirty = true;
		}

		/// <summary>Gets or sets a value indicating whether the list item is enabled.</summary>
		/// <returns>true if the list item is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x0006530C File Offset: 0x0006350C
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00065314 File Offset: 0x00063514
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x0006532B File Offset: 0x0006352B
		internal bool HasAttributes
		{
			get
			{
				return this.attrs != null && this.attrs.Count > 0;
			}
		}

		// Token: 0x04001A5C RID: 6748
		private string text;

		// Token: 0x04001A5D RID: 6749
		private string value;

		// Token: 0x04001A5E RID: 6750
		private bool selected;

		// Token: 0x04001A5F RID: 6751
		private bool dirty;

		// Token: 0x04001A60 RID: 6752
		private bool enabled = true;

		// Token: 0x04001A61 RID: 6753
		private bool tracking;

		// Token: 0x04001A62 RID: 6754
		private StateBag sb;

		// Token: 0x04001A63 RID: 6755
		private AttributeCollection attrs;
	}
}
