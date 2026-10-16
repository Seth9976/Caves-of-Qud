using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides object-model access to all attributes declared in the opening tag of an ASP.NET server control element. This class cannot be inherited.</summary>
	// Token: 0x0200019D RID: 413
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AttributeCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.AttributeCollection" /> class.</summary>
		/// <param name="bag">An object that contains the attribute keys and their values from the opening tag of the server control. </param>
		// Token: 0x06000FAE RID: 4014 RVA: 0x000291D1 File Offset: 0x000273D1
		public AttributeCollection(StateBag bag)
		{
			this.bag = bag;
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.AttributeCollection" /> object is equal to the specified object.</summary>
		/// <returns>true if the object that is contained in the <paramref name="o" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.AttributeCollection" />; otherwise, false.</returns>
		/// <param name="o">The object instance to compare with this instance.</param>
		// Token: 0x06000FAF RID: 4015 RVA: 0x000291E0 File Offset: 0x000273E0
		public override bool Equals(object o)
		{
			AttributeCollection attributeCollection = o as AttributeCollection;
			if (attributeCollection == null)
			{
				return false;
			}
			if (this.Count != attributeCollection.Count)
			{
				return false;
			}
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				if (string.CompareOrdinal(text, "style") != 0 && string.CompareOrdinal(attributeCollection[text], this[text]) == 0)
				{
					return false;
				}
			}
			if ((this.styleCollection == null && attributeCollection.styleCollection != null) || (this.styleCollection != null && attributeCollection.styleCollection == null))
			{
				return false;
			}
			if (this.styleCollection != null)
			{
				if (this.styleCollection.Count != attributeCollection.styleCollection.Count)
				{
					return false;
				}
				foreach (object obj2 in this.styleCollection.Keys)
				{
					string text2 = (string)obj2;
					if (string.CompareOrdinal(this.styleCollection[text2], attributeCollection.styleCollection[text2]) == 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000FB0 RID: 4016 RVA: 0x00029330 File Offset: 0x00027530
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				if (!(text == "style"))
				{
					num ^= text.GetHashCode();
					string text2 = this[text];
					if (text2 != null)
					{
						num ^= text2.GetHashCode();
					}
				}
			}
			if (this.styleCollection != null)
			{
				foreach (object obj2 in this.styleCollection.Keys)
				{
					string text3 = (string)obj2;
					num ^= this.styleCollection[text3].GetHashCode();
					string text4 = this.styleCollection[text3];
					if (text4 != null)
					{
						num ^= text4.GetHashCode();
					}
				}
			}
			return num;
		}

		/// <summary>Gets the number of attributes in the <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00029438 File Offset: 0x00027638
		public int Count
		{
			get
			{
				return this.bag.Count;
			}
		}

		/// <summary>Gets a collection of styles for the ASP.NET server control to which the current <see cref="T:System.Web.UI.AttributeCollection" /> object belongs.</summary>
		/// <returns>A collection that contains the styles for the current server control.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00029445 File Offset: 0x00027645
		public CssStyleCollection CssStyle
		{
			get
			{
				if (this.styleCollection == null)
				{
					this.styleCollection = new CssStyleCollection(this.bag);
				}
				return this.styleCollection;
			}
		}

		/// <summary>Gets or sets a specified attribute value for a server control.</summary>
		/// <returns>The attribute value.</returns>
		/// <param name="key">The location of the attribute in the collection. </param>
		// Token: 0x17000535 RID: 1333
		public string this[string key]
		{
			get
			{
				return this.bag[key] as string;
			}
			set
			{
				this.Add(key, value);
			}
		}

		/// <summary>Gets a collection of keys to all attributes in the server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <returns>The collection of keys.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00029483 File Offset: 0x00027683
		public ICollection Keys
		{
			get
			{
				return this.bag.Keys;
			}
		}

		/// <summary>Adds an attribute to a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <param name="key">The attribute name. </param>
		/// <param name="value">The attribute value. </param>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00029490 File Offset: 0x00027690
		public void Add(string key, string value)
		{
			if (string.Compare(key, "style", true, Helpers.InvariantCulture) == 0)
			{
				this.CssStyle.Value = value;
				return;
			}
			this.bag.Add(key, value);
		}

		/// <summary>Adds attributes from the <see cref="T:System.Web.UI.AttributeCollection" /> class to the <see cref="T:System.Web.UI.HtmlTextWriter" /> object that is responsible for rendering the attributes as markup.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute to the opening tag of an ASP.NET server control. </param>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x000294C0 File Offset: 0x000276C0
		public void AddAttributes(HtmlTextWriter writer)
		{
			foreach (object obj in this.bag.Keys)
			{
				string text = (string)obj;
				string text2 = this.bag[text] as string;
				writer.AddAttribute(text, text2);
			}
		}

		/// <summary>Removes all attributes from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x00029534 File Offset: 0x00027734
		public void Clear()
		{
			this.CssStyle.Clear();
			this.bag.Clear();
		}

		/// <summary>Removes an attribute from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <param name="key">The name of the attribute to remove. </param>
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002954C File Offset: 0x0002774C
		public void Remove(string key)
		{
			if (string.Compare(key, "style", true, Helpers.InvariantCulture) == 0)
			{
				this.CssStyle.Clear();
				return;
			}
			this.bag.Remove(key);
		}

		/// <summary>Writes the collection of attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> output stream for the control to which the collection belongs.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute collection to the current output stream. </param>
		// Token: 0x06000FBA RID: 4026 RVA: 0x0002957C File Offset: 0x0002777C
		public void Render(HtmlTextWriter writer)
		{
			foreach (object obj in this.bag.Keys)
			{
				string text = (string)obj;
				string text2 = this.bag[text] as string;
				if (text2 != null)
				{
					writer.WriteAttribute(text, text2, true);
				}
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000295F4 File Offset: 0x000277F4
		internal void CopyFrom(AttributeCollection attributeCollection)
		{
			if (attributeCollection == null || attributeCollection.Count == 0)
			{
				return;
			}
			foreach (object obj in attributeCollection.bag.Keys)
			{
				string text = (string)obj;
				this.Add(text, attributeCollection[text]);
			}
		}

		// Token: 0x04001340 RID: 4928
		private StateBag bag;

		// Token: 0x04001341 RID: 4929
		private CssStyleCollection styleCollection;

		// Token: 0x04001342 RID: 4930
		internal const string StyleAttribute = "style";
	}
}
