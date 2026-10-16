using System;
using System.Reflection;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000136 RID: 310
	public class PropertyExistsConstraint : Constraint
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00016ABE File Offset: 0x00014CBE
		public PropertyExistsConstraint(string name)
			: base(new object[] { name })
		{
			this.name = name;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00016AD7 File Offset: 0x00014CD7
		public override string Description
		{
			get
			{
				return "property " + this.name;
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00016AEC File Offset: 0x00014CEC
		public override ConstraintResult ApplyTo(object actual)
		{
			Guard.ArgumentNotNull(actual, "actual");
			this.actualType = actual as Type;
			if (this.actualType == null)
			{
				this.actualType = actual.GetType();
			}
			PropertyInfo property = this.actualType.GetProperty(this.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return new ConstraintResult(this, this.actualType, property != null);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00016B51 File Offset: 0x00014D51
		protected override string GetStringRepresentation()
		{
			return string.Format("<propertyexists {0}>", this.name);
		}

		// Token: 0x040001E8 RID: 488
		private readonly string name;

		// Token: 0x040001E9 RID: 489
		private Type actualType;
	}
}
