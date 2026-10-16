using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class PropertyAttribute : NUnitAttribute, IApplyToTest
	{
		// Token: 0x0600015F RID: 351 RVA: 0x000040D6 File Offset: 0x000022D6
		public PropertyAttribute(string propertyName, string propertyValue)
		{
			this.properties.Add(propertyName, propertyValue);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000040F6 File Offset: 0x000022F6
		public PropertyAttribute(string propertyName, int propertyValue)
		{
			this.properties.Add(propertyName, propertyValue);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000411B File Offset: 0x0000231B
		public PropertyAttribute(string propertyName, double propertyValue)
		{
			this.properties.Add(propertyName, propertyValue);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004140 File Offset: 0x00002340
		protected PropertyAttribute()
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004154 File Offset: 0x00002354
		protected PropertyAttribute(object propertyValue)
		{
			string text = base.GetType().Name;
			if (text.EndsWith("Attribute"))
			{
				text = text.Substring(0, text.Length - 9);
			}
			this.properties.Add(text, propertyValue);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000041A9 File Offset: 0x000023A9
		public IPropertyBag Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000041B4 File Offset: 0x000023B4
		public virtual void ApplyToTest(Test test)
		{
			foreach (string text in this.Properties.Keys)
			{
				foreach (object obj in this.Properties[text])
				{
					test.Properties.Add(text, obj);
				}
			}
		}

		// Token: 0x0400002C RID: 44
		private PropertyBag properties = new PropertyBag();
	}
}
