using System;
using System.Reflection;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x0200005F RID: 95
	public class Throws
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000721F File Offset: 0x0000541F
		public static ResolvableConstraintExpression Exception
		{
			get
			{
				return new ConstraintExpression().Append(new ThrowsOperator());
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00007230 File Offset: 0x00005430
		public static ResolvableConstraintExpression InnerException
		{
			get
			{
				return Throws.Exception.InnerException;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000723C File Offset: 0x0000543C
		public static ExactTypeConstraint TargetInvocationException
		{
			get
			{
				return Throws.TypeOf(typeof(TargetInvocationException));
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000724D File Offset: 0x0000544D
		public static ExactTypeConstraint ArgumentException
		{
			get
			{
				return Throws.TypeOf(typeof(ArgumentException));
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000725E File Offset: 0x0000545E
		public static ExactTypeConstraint ArgumentNullException
		{
			get
			{
				return Throws.TypeOf(typeof(ArgumentNullException));
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000726F File Offset: 0x0000546F
		public static ExactTypeConstraint InvalidOperationException
		{
			get
			{
				return Throws.TypeOf(typeof(InvalidOperationException));
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00007280 File Offset: 0x00005480
		public static ThrowsNothingConstraint Nothing
		{
			get
			{
				return new ThrowsNothingConstraint();
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00007287 File Offset: 0x00005487
		public static ExactTypeConstraint TypeOf(Type expectedType)
		{
			return Throws.Exception.TypeOf(expectedType);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00007294 File Offset: 0x00005494
		public static ExactTypeConstraint TypeOf<TExpected>()
		{
			return Throws.TypeOf(typeof(TExpected));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000072A5 File Offset: 0x000054A5
		public static InstanceOfTypeConstraint InstanceOf(Type expectedType)
		{
			return Throws.Exception.InstanceOf(expectedType);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000072B2 File Offset: 0x000054B2
		public static InstanceOfTypeConstraint InstanceOf<TExpected>()
		{
			return Throws.InstanceOf(typeof(TExpected));
		}
	}
}
