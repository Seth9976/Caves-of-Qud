using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Used by data source controls when implementing the members defined by the <see cref="T:System.ComponentModel.IListSource" /> interface. This class cannot be inherited.</summary>
	// Token: 0x020001DD RID: 477
	public static class ListSourceHelper
	{
		/// <summary>Indicates whether the specified data source control contains a collection of data source view objects.</summary>
		/// <returns>true if the data source control contains a collection of data source view objects; otherwise, false.</returns>
		/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that specifies the data source control to test for associated data source view objects.</param>
		// Token: 0x06001352 RID: 4946 RVA: 0x0003281F File Offset: 0x00030A1F
		public static bool ContainsListCollection(IDataSource dataSource)
		{
			return dataSource.GetViewNames().Count > 0;
		}

		/// <summary>Retrieves an <see cref="T:System.Collections.IList" /> collection of data source objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of one <see cref="T:System.Web.UI.IDataSource" />, if the <see cref="T:System.Web.UI.IDataSource" /> has one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects; otherwise, returns null. </returns>
		/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that contains one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects, which are retrieved by a call to <see cref="M:System.Web.UI.DataSourceControl.GetViewNames" />.</param>
		// Token: 0x06001353 RID: 4947 RVA: 0x0003282F File Offset: 0x00030A2F
		public static IList GetList(IDataSource dataSource)
		{
			if (dataSource.GetViewNames().Count == 0)
			{
				return null;
			}
			return new ListSourceHelper.ListSourceList { dataSource };
		}

		// Token: 0x020001DE RID: 478
		private sealed class ListSourceList : List<IDataSource>, ITypedList
		{
			// Token: 0x06001354 RID: 4948 RVA: 0x0003284C File Offset: 0x00030A4C
			PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
			{
				ICollection viewNames = base[0].GetViewNames();
				PropertyDescriptor[] array = new PropertyDescriptor[viewNames.Count];
				int num = 0;
				foreach (object obj in viewNames)
				{
					string text = (string)obj;
					array[num++] = new ListSourceHelper.ListSourcePropertyDescriptor(text, null);
				}
				return new PropertyDescriptorCollection(array);
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x0000EE63 File Offset: 0x0000D063
			string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
			{
				return string.Empty;
			}
		}

		// Token: 0x020001DF RID: 479
		private sealed class ListSourcePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06001357 RID: 4951 RVA: 0x000328D4 File Offset: 0x00030AD4
			public ListSourcePropertyDescriptor(MemberDescriptor descr)
				: base(descr)
			{
			}

			// Token: 0x06001358 RID: 4952 RVA: 0x000328DD File Offset: 0x00030ADD
			public ListSourcePropertyDescriptor(string name, Attribute[] attrs)
				: base(name, attrs)
			{
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x000328E7 File Offset: 0x00030AE7
			public ListSourcePropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
				: base(descr, attrs)
			{
			}

			// Token: 0x0600135A RID: 4954 RVA: 0x00003A2B File Offset: 0x00001C2B
			public override bool CanResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x0600135B RID: 4955 RVA: 0x00003A2B File Offset: 0x00001C2B
			public override Type ComponentType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x000328F4 File Offset: 0x00030AF4
			public override object GetValue(object component)
			{
				IDataSource dataSource = component as IDataSource;
				if (dataSource == null)
				{
					return null;
				}
				return dataSource.GetView(this.Name).ExecuteSelect(DataSourceSelectArguments.Empty);
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x0600135D RID: 4957 RVA: 0x00008B4A File Offset: 0x00006D4A
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x0600135E RID: 4958 RVA: 0x00032923 File Offset: 0x00030B23
			public override Type PropertyType
			{
				get
				{
					return typeof(IEnumerable);
				}
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x00003A2B File Offset: 0x00001C2B
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001360 RID: 4960 RVA: 0x00003A2B File Offset: 0x00001C2B
			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x00003A2B File Offset: 0x00001C2B
			public override bool ShouldSerializeValue(object component)
			{
				throw new NotImplementedException();
			}
		}
	}
}
