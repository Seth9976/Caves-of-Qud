using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.SchemeSettingElement" /> objects.</summary>
	// Token: 0x0200087C RID: 2172
	[ConfigurationCollection(typeof(SchemeSettingElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
	public sealed class SchemeSettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class. </summary>
		// Token: 0x060044C2 RID: 17602 RVA: 0x00013BAE File Offset: 0x00011DAE
		public SchemeSettingElementCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an item at the specified index in the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> collection.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.SchemeSettingElement" />.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.SchemeSettingElement" /> to return.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="index" /> parameter is less than zero.-or- The item specified by the parameter is null or has been removed.</exception>
		// Token: 0x17000F88 RID: 3976
		public SchemeSettingElement this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x00032868 File Offset: 0x00030A68
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x00032868 File Offset: 0x00030A68
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />; otherwise, -1.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.SchemeSettingElement" /> for the specified index location.</param>
		// Token: 0x060044C6 RID: 17606 RVA: 0x000ED21C File Offset: 0x000EB41C
		public int IndexOf(SchemeSettingElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
