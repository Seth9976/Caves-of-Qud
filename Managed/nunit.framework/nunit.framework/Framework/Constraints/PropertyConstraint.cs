using System;
using System.Reflection;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000135 RID: 309
	public class PropertyConstraint : PrefixConstraint
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x000169F1 File Offset: 0x00014BF1
		public PropertyConstraint(string name, IConstraint baseConstraint)
			: base(baseConstraint)
		{
			this.name = name;
			base.DescriptionPrefix = "property " + name;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00016A14 File Offset: 0x00014C14
		public override ConstraintResult ApplyTo(object actual)
		{
			Guard.ArgumentNotNull(actual, "actual");
			Type type = actual as Type;
			if (type == null)
			{
				type = actual.GetType();
			}
			PropertyInfo property = type.GetProperty(this.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
			{
				throw new ArgumentException(string.Format("Property {0} was not found", this.name), "name");
			}
			this.propValue = property.GetValue(actual, null);
			return new ConstraintResult(this, this.propValue, base.BaseConstraint.ApplyTo(this.propValue).IsSuccess);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00016AA6 File Offset: 0x00014CA6
		protected override string GetStringRepresentation()
		{
			return string.Format("<property {0} {1}>", this.name, base.BaseConstraint);
		}

		// Token: 0x040001E6 RID: 486
		private readonly string name;

		// Token: 0x040001E7 RID: 487
		private object propValue;
	}
}
