using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI
{
	/// <summary>Contains the HTML cascading-style sheets (CSS) inline style attributes for a specified HTML server control. This class cannot be inherited.</summary>
	// Token: 0x020001B8 RID: 440
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class CssStyleCollection
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x0002F231 File Offset: 0x0002D431
		internal CssStyleCollection()
		{
			this.style = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0002F254 File Offset: 0x0002D454
		internal CssStyleCollection(StateBag bag)
			: this()
		{
			this.bag = bag;
			if (bag != null && bag["style"] != null)
			{
				this._value.Append(bag["style"]);
			}
			this.InitFromStyle();
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0002F290 File Offset: 0x0002D490
		private void InitFromStyle()
		{
			this.style.Clear();
			if (this._value.Length > 0)
			{
				for (int i = 0; i >= 0; i = this.ParseStyle(i))
				{
				}
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		private int ParseStyle(int startIndex)
		{
			int num = -1;
			for (int i = startIndex; i < this._value.Length; i++)
			{
				if (this._value[i] == ':')
				{
					num = i;
					break;
				}
			}
			if (num == -1 || num + 1 == this._value.Length)
			{
				return -1;
			}
			string text = this._value.ToString(startIndex, num - startIndex).Trim();
			int num2 = -1;
			for (int j = num + 1; j < this._value.Length; j++)
			{
				if (this._value[j] == ';')
				{
					num2 = j;
					break;
				}
			}
			string text2;
			if (num2 == -1)
			{
				text2 = this._value.ToString(num + 1, this._value.Length - num - 1).Trim();
			}
			else
			{
				text2 = this._value.ToString(num + 1, num2 - num - 1).Trim();
			}
			this.style.Add(text, text2);
			if (num2 == -1 || num2 + 1 == this._value.Length)
			{
				return -1;
			}
			return num2 + 1;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0002F3D0 File Offset: 0x0002D5D0
		private void BagToValue()
		{
			this._value.Length = 0;
			foreach (object obj in this.style.Keys)
			{
				string text = (string)obj;
				CssStyleCollection.AppendStyle(this._value, text, (string)this.style[text]);
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0002F450 File Offset: 0x0002D650
		private static void AppendStyle(StringBuilder sb, string key, string value)
		{
			if (string.Compare(key, "background-image", StringComparison.OrdinalIgnoreCase) == 0 && value.Length >= 3 && string.Compare("url", 0, value, 0, 3, StringComparison.OrdinalIgnoreCase) != 0)
			{
				sb.AppendFormat("{0}:url({1});", key, HttpUtility.UrlPathEncode(value));
				return;
			}
			sb.AppendFormat("{0}:{1};", key, value);
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0002F4A7 File Offset: 0x0002D6A7
		public int Count
		{
			get
			{
				return this.style.Count;
			}
		}

		/// <summary>Gets or sets the specified CSS value for the HTML server control.</summary>
		/// <returns>The value of <paramref name="key" />.</returns>
		/// <param name="key">The index to the CSS attribute. </param>
		// Token: 0x170005D3 RID: 1491
		public string this[string key]
		{
			get
			{
				return this.style[key] as string;
			}
			set
			{
				this.Add(key, value);
			}
		}

		/// <summary>Gets a collection of keys to all the styles in the <see cref="T:System.Web.UI.CssStyleCollection" /> object for a specific HTML server control.</summary>
		/// <returns>A collection of keys contained in the <see cref="T:System.Web.UI.CssStyleCollection" /> for the specified HTML server control.</returns>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0002F4D1 File Offset: 0x0002D6D1
		public ICollection Keys
		{
			get
			{
				return this.style.Keys;
			}
		}

		/// <summary>Adds a style item to the <see cref="T:System.Web.UI.CssStyleCollection" /> of a control using the specified name/value pair.</summary>
		/// <param name="key">The name of the new style attribute to add to the collection. </param>
		/// <param name="value">The value of the style attribute to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060011D3 RID: 4563 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		public void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				this.Remove(key);
				return;
			}
			string text = (string)this.style[key];
			if (text == null)
			{
				this.style[key] = value;
				CssStyleCollection.AppendStyle(this._value, key, value);
			}
			else
			{
				if (string.CompareOrdinal(text, value) == 0)
				{
					return;
				}
				this.style[key] = value;
				this.BagToValue();
			}
			if (this.bag != null)
			{
				this.bag["style"] = this._value.ToString();
			}
		}

		/// <summary>Adds a style item to the <see cref="T:System.Web.UI.CssStyleCollection" /> collection of a control using the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value and corresponding value.</summary>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value to add to the collection.</param>
		/// <param name="value">The value of the style attribute to add to the collection.</param>
		// Token: 0x060011D4 RID: 4564 RVA: 0x0002F577 File Offset: 0x0002D777
		public void Add(HtmlTextWriterStyle key, string value)
		{
			this.Add(HtmlTextWriter.StaticGetStyleName(key), value);
		}

		/// <summary>Removes all style items from the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
		// Token: 0x060011D5 RID: 4565 RVA: 0x0002F586 File Offset: 0x0002D786
		public void Clear()
		{
			this.style.Clear();
			this.SetValueInternal(null);
		}

		/// <summary>Removes a style item from the <see cref="T:System.Web.UI.CssStyleCollection" /> of a control using the specified style key.</summary>
		/// <param name="key">The string literal of the style item to remove. </param>
		// Token: 0x060011D6 RID: 4566 RVA: 0x0002F59A File Offset: 0x0002D79A
		public void Remove(string key)
		{
			if (this.style[key] == null)
			{
				return;
			}
			this.style.Remove(key);
			if (this.style.Count == 0)
			{
				this.SetValueInternal(null);
				return;
			}
			this.BagToValue();
		}

		/// <summary>Gets or sets the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> value for the HTML server control.</summary>
		/// <returns>The value <paramref name="key" />; otherwise, null, if <paramref name="key" /> is not in the server control's collection.</returns>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" />.</param>
		// Token: 0x170005D5 RID: 1493
		public string this[HtmlTextWriterStyle key]
		{
			get
			{
				return this.style[HtmlTextWriter.StaticGetStyleName(key)] as string;
			}
			set
			{
				this.Add(HtmlTextWriter.StaticGetStyleName(key), value);
			}
		}

		/// <summary>Removes a style item from the <see cref="T:System.Web.UI.CssStyleCollection" /> collection of a control using the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value.</summary>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value to remove.</param>
		// Token: 0x060011D9 RID: 4569 RVA: 0x0002F5EA File Offset: 0x0002D7EA
		public void Remove(HtmlTextWriterStyle key)
		{
			this.Remove(HtmlTextWriter.StaticGetStyleName(key));
		}

		/// <summary>Gets or sets the value of the style attribute of the HTML server control.</summary>
		/// <returns>The style string literal.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0002F5F8 File Offset: 0x0002D7F8
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0002F605 File Offset: 0x0002D805
		public string Value
		{
			get
			{
				return this._value.ToString();
			}
			set
			{
				this.SetValueInternal(value);
				this.InitFromStyle();
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0002F614 File Offset: 0x0002D814
		private void SetValueInternal(string value)
		{
			this._value.Length = 0;
			if (value != null)
			{
				this._value.Append(value);
			}
			if (this.bag != null)
			{
				if (value == null)
				{
					this.bag.Remove("style");
					return;
				}
				this.bag["style"] = value;
			}
		}

		// Token: 0x04001407 RID: 5127
		private StateBag bag;

		// Token: 0x04001408 RID: 5128
		private ListDictionary style;

		// Token: 0x04001409 RID: 5129
		private StringBuilder _value = new StringBuilder();
	}
}
