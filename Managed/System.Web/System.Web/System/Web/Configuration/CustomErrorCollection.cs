using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CustomError" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000592 RID: 1426
	[ConfigurationCollection(typeof(CustomError), AddItemName = "error", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CustomErrorCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.CustomError" /> object to the collection.</summary>
		/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> object to add already exists in the collection or the collection is read only.</param>
		// Token: 0x06003C5E RID: 15454 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(CustomError customError)
		{
			this.BaseAdd(customError);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.CustomError" /> objects from the collection.</summary>
		// Token: 0x06003C5F RID: 15455 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x0009EC48 File Offset: 0x0009CE48
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomError();
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x0009EC50 File Offset: 0x0009CE50
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomError)element).StatusCode.ToString();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</returns>
		/// <param name="index">The collection key's index. </param>
		// Token: 0x06003C62 RID: 15458 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</returns>
		/// <param name="statusCode">The HTTP status code associated with the custom error. </param>
		// Token: 0x06003C63 RID: 15459 RVA: 0x0009EC70 File Offset: 0x0009CE70
		public CustomError Get(string statusCode)
		{
			return (CustomError)base.BaseGet(statusCode);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object. </param>
		// Token: 0x06003C64 RID: 15460 RVA: 0x0009EC7E File Offset: 0x0009CE7E
		public CustomError Get(int index)
		{
			return (CustomError)base.BaseGet(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object from the collection.</summary>
		/// <param name="statusCode">The HTTP status code associated with the custom error.  </param>
		// Token: 0x06003C65 RID: 15461 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string statusCode)
		{
			base.BaseRemove(statusCode);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object to remove. </param>
		// Token: 0x06003C66 RID: 15462 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.CustomError" /> to the collection.</summary>
		/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> to add to the collection. </param>
		// Token: 0x06003C67 RID: 15463 RVA: 0x0009EC8C File Offset: 0x0009CE8C
		public void Set(CustomError customError)
		{
			CustomError customError2 = this.Get(customError.StatusCode.ToString());
			if (customError2 == null)
			{
				this.Add(customError);
				return;
			}
			int num = base.BaseIndexOf(customError2);
			this.RemoveAt(num);
			this.BaseAdd(num, customError);
		}

		/// <summary>Returns an array of the keys for all of the configuration elements contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
		/// <returns>An array containing the keys for all of the <see cref="T:System.Web.Configuration.CustomError" /> objects contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</returns>
		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x0009ECD0 File Offset: 0x0009CED0
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].StatusCode.ToString();
				}
				return array;
			}
		}

		/// <summary>The type of the <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x0009ED12 File Offset: 0x0009CF12
		protected override string ElementName
		{
			get
			{
				return "error";
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> at the specified index.</returns>
		/// <param name="index">The collection error's index. </param>
		// Token: 0x170012AC RID: 4780
		public CustomError this[int index]
		{
			get
			{
				return (CustomError)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					this.RemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</returns>
		/// <param name="statusCode">The HTTP status code. </param>
		// Token: 0x170012AD RID: 4781
		public CustomError this[string statusCode]
		{
			get
			{
				return (CustomError)base.BaseGet(statusCode);
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x0009ED33 File Offset: 0x0009CF33
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorCollection.properties;
			}
		}

		// Token: 0x040020CA RID: 8394
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
