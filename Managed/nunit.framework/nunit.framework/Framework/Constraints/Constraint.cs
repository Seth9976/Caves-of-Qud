using System;
using System.Globalization;
using System.Text;
using NUnit.Compatibility;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F9 RID: 249
	public abstract class Constraint : IConstraint, IResolveConstraint
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x000128A0 File Offset: 0x00010AA0
		protected Constraint(params object[] args)
		{
			this.Arguments = args;
			this._displayName = new Lazy<string>(delegate
			{
				Type type = base.GetType();
				string text = type.Name;
				if (type.GetTypeInfo().IsGenericType)
				{
					text = text.Substring(0, text.Length - 2);
				}
				if (text.EndsWith("Constraint", StringComparison.Ordinal))
				{
					text = text.Substring(0, text.Length - 10);
				}
				return text;
			});
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000128C6 File Offset: 0x00010AC6
		public virtual string DisplayName
		{
			get
			{
				return this._displayName.Value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000128D3 File Offset: 0x00010AD3
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x000128DB File Offset: 0x00010ADB
		public virtual string Description { get; protected set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000128E4 File Offset: 0x00010AE4
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x000128EC File Offset: 0x00010AEC
		public object[] Arguments { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000128F5 File Offset: 0x00010AF5
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x000128FD File Offset: 0x00010AFD
		public ConstraintBuilder Builder { get; set; }

		// Token: 0x060007A3 RID: 1955
		public abstract ConstraintResult ApplyTo(object actual);

		// Token: 0x060007A4 RID: 1956 RVA: 0x00012908 File Offset: 0x00010B08
		public virtual ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
		{
			if (AsyncInvocationRegion.IsAsyncOperation(del))
			{
				using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(del))
				{
					return this.ApplyTo(asyncInvocationRegion.WaitForPendingOperationsToComplete(del()));
				}
			}
			return this.ApplyTo(this.GetTestObject<TActual>(del));
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00012968 File Offset: 0x00010B68
		public virtual ConstraintResult ApplyTo<TActual>(ref TActual actual)
		{
			return this.ApplyTo(actual);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001297B File Offset: 0x00010B7B
		protected virtual object GetTestObject<TActual>(ActualValueDelegate<TActual> del)
		{
			return del();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00012988 File Offset: 0x00010B88
		public override string ToString()
		{
			string stringRepresentation = this.GetStringRepresentation();
			if (this.Builder != null)
			{
				return string.Format("<unresolved {0}>", stringRepresentation);
			}
			return stringRepresentation;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000129B4 File Offset: 0x00010BB4
		protected virtual string GetStringRepresentation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<");
			stringBuilder.Append(this.DisplayName.ToLower());
			foreach (object obj in this.Arguments)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(Constraint._displayable(obj));
			}
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00012A2C File Offset: 0x00010C2C
		private static string _displayable(object o)
		{
			if (o == null)
			{
				return "null";
			}
			string text = ((o is string) ? "\"{0}\"" : "{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { o });
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00012A6C File Offset: 0x00010C6C
		public static Constraint operator &(Constraint left, Constraint right)
		{
			return new AndConstraint(((IResolveConstraint)left).Resolve(), ((IResolveConstraint)right).Resolve());
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00012A8C File Offset: 0x00010C8C
		public static Constraint operator |(Constraint left, Constraint right)
		{
			return new OrConstraint(((IResolveConstraint)left).Resolve(), ((IResolveConstraint)right).Resolve());
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00012AAC File Offset: 0x00010CAC
		public static Constraint operator !(Constraint constraint)
		{
			return new NotConstraint(((IResolveConstraint)constraint).Resolve());
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00012ABC File Offset: 0x00010CBC
		public ConstraintExpression And
		{
			get
			{
				ConstraintBuilder constraintBuilder = this.Builder;
				if (constraintBuilder == null)
				{
					constraintBuilder = new ConstraintBuilder();
					constraintBuilder.Append(this);
				}
				constraintBuilder.Append(new AndOperator());
				return new ConstraintExpression(constraintBuilder);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00012AF1 File Offset: 0x00010CF1
		public ConstraintExpression With
		{
			get
			{
				return this.And;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00012AFC File Offset: 0x00010CFC
		public ConstraintExpression Or
		{
			get
			{
				ConstraintBuilder constraintBuilder = this.Builder;
				if (constraintBuilder == null)
				{
					constraintBuilder = new ConstraintBuilder();
					constraintBuilder.Append(this);
				}
				constraintBuilder.Append(new OrOperator());
				return new ConstraintExpression(constraintBuilder);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00012B34 File Offset: 0x00010D34
		public DelayedConstraint After(int delayInMilliseconds)
		{
			return new DelayedConstraint((this.Builder == null) ? this : this.Builder.Resolve(), delayInMilliseconds);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00012B60 File Offset: 0x00010D60
		public DelayedConstraint After(int delayInMilliseconds, int pollingInterval)
		{
			return new DelayedConstraint((this.Builder == null) ? this : this.Builder.Resolve(), delayInMilliseconds, pollingInterval);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00012B8C File Offset: 0x00010D8C
		IConstraint IResolveConstraint.Resolve()
		{
			if (this.Builder != null)
			{
				return this.Builder.Resolve();
			}
			return this;
		}

		// Token: 0x040001A9 RID: 425
		private Lazy<string> _displayName;
	}
}
