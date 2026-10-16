using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000053 RID: 83
	public class Has
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006529 File Offset: 0x00004729
		public static ConstraintExpression No
		{
			get
			{
				return new ConstraintExpression().Not;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000068C0 File Offset: 0x00004AC0
		public static ConstraintExpression All
		{
			get
			{
				return new ConstraintExpression().All;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000068CC File Offset: 0x00004ACC
		public static ConstraintExpression Some
		{
			get
			{
				return new ConstraintExpression().Some;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000068D8 File Offset: 0x00004AD8
		public static ConstraintExpression None
		{
			get
			{
				return new ConstraintExpression().None;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000068E4 File Offset: 0x00004AE4
		public static ConstraintExpression Exactly(int expectedCount)
		{
			return new ConstraintExpression().Exactly(expectedCount);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000068F1 File Offset: 0x00004AF1
		public static ResolvableConstraintExpression Property(string name)
		{
			return new ConstraintExpression().Property(name);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000068FE File Offset: 0x00004AFE
		public static ResolvableConstraintExpression Length
		{
			get
			{
				return Has.Property("Length");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000690A File Offset: 0x00004B0A
		public static ResolvableConstraintExpression Count
		{
			get
			{
				return Has.Property("Count");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00006916 File Offset: 0x00004B16
		public static ResolvableConstraintExpression Message
		{
			get
			{
				return Has.Property("Message");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00006922 File Offset: 0x00004B22
		public static ResolvableConstraintExpression InnerException
		{
			get
			{
				return Has.Property("InnerException");
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000692E File Offset: 0x00004B2E
		public static ResolvableConstraintExpression Attribute(Type expectedType)
		{
			return new ConstraintExpression().Attribute(expectedType);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000693B File Offset: 0x00004B3B
		public static ResolvableConstraintExpression Attribute<T>()
		{
			return Has.Attribute(typeof(T));
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000642B File Offset: 0x0000462B
		public static CollectionContainsConstraint Member(object expected)
		{
			return new CollectionContainsConstraint(expected);
		}
	}
}
