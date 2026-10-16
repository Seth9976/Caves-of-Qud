using System;
using System.IO;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000104 RID: 260
	public class EmptyConstraint : Constraint
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000139CE File Offset: 0x00011BCE
		public override string Description
		{
			get
			{
				if (this.realConstraint != null)
				{
					return this.realConstraint.Description;
				}
				return "<empty>";
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000139EC File Offset: 0x00011BEC
		public override ConstraintResult ApplyTo(object actual)
		{
			if (actual is string)
			{
				this.realConstraint = new EmptyStringConstraint();
			}
			else
			{
				if (actual == null)
				{
					throw new ArgumentException("The actual value must be a string or a non-null IEnumerable or DirectoryInfo", "actual");
				}
				if (actual is DirectoryInfo)
				{
					this.realConstraint = new EmptyDirectoryConstraint();
				}
				else
				{
					this.realConstraint = new EmptyCollectionConstraint();
				}
			}
			return this.realConstraint.ApplyTo(actual);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00011D90 File Offset: 0x0000FF90
		public EmptyConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x040001BE RID: 446
		private Constraint realConstraint;
	}
}
