using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class CategoryAttribute : NUnitAttribute, IApplyToTest
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00003A07 File Offset: 0x00001C07
		public CategoryAttribute(string name)
		{
			this.categoryName = name.Trim();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003A1C File Offset: 0x00001C1C
		protected CategoryAttribute()
		{
			this.categoryName = base.GetType().Name;
			if (this.categoryName.EndsWith("Attribute"))
			{
				this.categoryName = this.categoryName.Substring(0, this.categoryName.Length - 9);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003A72 File Offset: 0x00001C72
		public string Name
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003A7C File Offset: 0x00001C7C
		public void ApplyToTest(Test test)
		{
			test.Properties.Add("Category", this.Name);
			if (this.Name.IndexOfAny(new char[] { ',', '!', '+', '-' }) >= 0)
			{
				test.RunState = RunState.NotRunnable;
				test.Properties.Set("_SKIPREASON", "Category name must not contain ',', '!', '+' or '-'");
			}
		}

		// Token: 0x04000016 RID: 22
		protected string categoryName;
	}
}
