using System;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000EB RID: 235
	public class AttributeExistsConstraint : Constraint
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x00011C10 File Offset: 0x0000FE10
		public AttributeExistsConstraint(Type type)
			: base(new object[] { type })
		{
			this.expectedType = type;
			if (!typeof(Attribute).GetTypeInfo().IsAssignableFrom(this.expectedType.GetTypeInfo()))
			{
				throw new ArgumentException(string.Format("Type {0} is not an attribute", this.expectedType), "type");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00011C70 File Offset: 0x0000FE70
		public override string Description
		{
			get
			{
				return "type with attribute " + MsgUtils.FormatValue(this.expectedType);
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00011C88 File Offset: 0x0000FE88
		public override ConstraintResult ApplyTo(object actual)
		{
			Guard.ArgumentNotNull(actual, "actual");
			Attribute[] customAttributes = AttributeHelper.GetCustomAttributes(actual, this.expectedType, true);
			return new ConstraintResult(this, actual)
			{
				Status = ((customAttributes.Length != 0) ? ConstraintStatus.Success : ConstraintStatus.Failure)
			};
		}

		// Token: 0x04000197 RID: 407
		private Type expectedType;
	}
}
