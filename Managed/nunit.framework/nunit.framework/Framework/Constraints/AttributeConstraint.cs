using System;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000EA RID: 234
	public class AttributeConstraint : PrefixConstraint
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00011B24 File Offset: 0x0000FD24
		public AttributeConstraint(Type type, IConstraint baseConstraint)
			: base(baseConstraint)
		{
			this.expectedType = type;
			base.DescriptionPrefix = "attribute " + this.expectedType.FullName;
			if (!typeof(Attribute).GetTypeInfo().IsAssignableFrom(this.expectedType.GetTypeInfo()))
			{
				throw new ArgumentException(string.Format("Type {0} is not an attribute", this.expectedType), "type");
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00011B98 File Offset: 0x0000FD98
		public override ConstraintResult ApplyTo(object actual)
		{
			Guard.ArgumentNotNull(actual, "actual");
			Attribute[] customAttributes = AttributeHelper.GetCustomAttributes(actual, this.expectedType, true);
			if (customAttributes.Length == 0)
			{
				throw new ArgumentException(string.Format("Attribute {0} was not found", this.expectedType), "actual");
			}
			this.attrFound = customAttributes[0];
			return base.BaseConstraint.ApplyTo(this.attrFound);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00011BF7 File Offset: 0x0000FDF7
		protected override string GetStringRepresentation()
		{
			return string.Format("<attribute {0} {1}>", this.expectedType, base.BaseConstraint);
		}

		// Token: 0x04000195 RID: 405
		private readonly Type expectedType;

		// Token: 0x04000196 RID: 406
		private Attribute attrFound;
	}
}
