using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000133 RID: 307
	public class PredicateConstraint<T> : Constraint
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x000168D0 File Offset: 0x00014AD0
		public PredicateConstraint(Predicate<T> predicate)
			: base(new object[0])
		{
			this.predicate = predicate;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000168E8 File Offset: 0x00014AE8
		public override string Description
		{
			get
			{
				string name = this.predicate.Method.Name;
				if (!name.StartsWith("<"))
				{
					return "value matching " + name;
				}
				return "value matching lambda expression";
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00016924 File Offset: 0x00014B24
		public override ConstraintResult ApplyTo(object actual)
		{
			if (!(actual is T))
			{
				throw new ArgumentException("The actual value is not of type " + typeof(T).Name, "actual");
			}
			return new ConstraintResult(this, actual, this.predicate((T)((object)actual)));
		}

		// Token: 0x040001E3 RID: 483
		private readonly Predicate<T> predicate;
	}
}
