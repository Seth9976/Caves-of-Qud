using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000054 RID: 84
	public class Is
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00006529 File Offset: 0x00004729
		public static ConstraintExpression Not
		{
			get
			{
				return new ConstraintExpression().Not;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000068C0 File Offset: 0x00004AC0
		public static ConstraintExpression All
		{
			get
			{
				return new ConstraintExpression().All;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000694C File Offset: 0x00004B4C
		public static NullConstraint Null
		{
			get
			{
				return new NullConstraint();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006953 File Offset: 0x00004B53
		public static TrueConstraint True
		{
			get
			{
				return new TrueConstraint();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000695A File Offset: 0x00004B5A
		public static FalseConstraint False
		{
			get
			{
				return new FalseConstraint();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00006961 File Offset: 0x00004B61
		public static GreaterThanConstraint Positive
		{
			get
			{
				return new GreaterThanConstraint(0);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000696E File Offset: 0x00004B6E
		public static LessThanConstraint Negative
		{
			get
			{
				return new LessThanConstraint(0);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000697B File Offset: 0x00004B7B
		public static EqualConstraint Zero
		{
			get
			{
				return new EqualConstraint(0);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00006988 File Offset: 0x00004B88
		public static NaNConstraint NaN
		{
			get
			{
				return new NaNConstraint();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000698F File Offset: 0x00004B8F
		public static EmptyConstraint Empty
		{
			get
			{
				return new EmptyConstraint();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00006996 File Offset: 0x00004B96
		public static UniqueItemsConstraint Unique
		{
			get
			{
				return new UniqueItemsConstraint();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000699D File Offset: 0x00004B9D
		public static BinarySerializableConstraint BinarySerializable
		{
			get
			{
				return new BinarySerializableConstraint();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000069A4 File Offset: 0x00004BA4
		public static XmlSerializableConstraint XmlSerializable
		{
			get
			{
				return new XmlSerializableConstraint();
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000069AB File Offset: 0x00004BAB
		public static EqualConstraint EqualTo(object expected)
		{
			return new EqualConstraint(expected);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000069B3 File Offset: 0x00004BB3
		public static SameAsConstraint SameAs(object expected)
		{
			return new SameAsConstraint(expected);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000069BB File Offset: 0x00004BBB
		public static GreaterThanConstraint GreaterThan(object expected)
		{
			return new GreaterThanConstraint(expected);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000069C3 File Offset: 0x00004BC3
		public static GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected)
		{
			return new GreaterThanOrEqualConstraint(expected);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000069C3 File Offset: 0x00004BC3
		public static GreaterThanOrEqualConstraint AtLeast(object expected)
		{
			return new GreaterThanOrEqualConstraint(expected);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000069CB File Offset: 0x00004BCB
		public static LessThanConstraint LessThan(object expected)
		{
			return new LessThanConstraint(expected);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000069D3 File Offset: 0x00004BD3
		public static LessThanOrEqualConstraint LessThanOrEqualTo(object expected)
		{
			return new LessThanOrEqualConstraint(expected);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000069D3 File Offset: 0x00004BD3
		public static LessThanOrEqualConstraint AtMost(object expected)
		{
			return new LessThanOrEqualConstraint(expected);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000069DB File Offset: 0x00004BDB
		public static ExactTypeConstraint TypeOf(Type expectedType)
		{
			return new ExactTypeConstraint(expectedType);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000069E3 File Offset: 0x00004BE3
		public static ExactTypeConstraint TypeOf<TExpected>()
		{
			return new ExactTypeConstraint(typeof(TExpected));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000069F4 File Offset: 0x00004BF4
		public static InstanceOfTypeConstraint InstanceOf(Type expectedType)
		{
			return new InstanceOfTypeConstraint(expectedType);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000069FC File Offset: 0x00004BFC
		public static InstanceOfTypeConstraint InstanceOf<TExpected>()
		{
			return new InstanceOfTypeConstraint(typeof(TExpected));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00006A0D File Offset: 0x00004C0D
		public static AssignableFromConstraint AssignableFrom(Type expectedType)
		{
			return new AssignableFromConstraint(expectedType);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00006A15 File Offset: 0x00004C15
		public static AssignableFromConstraint AssignableFrom<TExpected>()
		{
			return new AssignableFromConstraint(typeof(TExpected));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00006A26 File Offset: 0x00004C26
		public static AssignableToConstraint AssignableTo(Type expectedType)
		{
			return new AssignableToConstraint(expectedType);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00006A2E File Offset: 0x00004C2E
		public static AssignableToConstraint AssignableTo<TExpected>()
		{
			return new AssignableToConstraint(typeof(TExpected));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006A3F File Offset: 0x00004C3F
		public static CollectionEquivalentConstraint EquivalentTo(IEnumerable expected)
		{
			return new CollectionEquivalentConstraint(expected);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00006A47 File Offset: 0x00004C47
		public static CollectionSubsetConstraint SubsetOf(IEnumerable expected)
		{
			return new CollectionSubsetConstraint(expected);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006A4F File Offset: 0x00004C4F
		public static CollectionSupersetConstraint SupersetOf(IEnumerable expected)
		{
			return new CollectionSupersetConstraint(expected);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006A57 File Offset: 0x00004C57
		public static CollectionOrderedConstraint Ordered
		{
			get
			{
				return new CollectionOrderedConstraint();
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006443 File Offset: 0x00004643
		[Obsolete("Deprecated, use Does.Contain")]
		public static SubstringConstraint StringContaining(string expected)
		{
			return new SubstringConstraint(expected);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006554 File Offset: 0x00004754
		[Obsolete("Deprecated, use Does.StartWith")]
		public static StartsWithConstraint StringStarting(string expected)
		{
			return new StartsWithConstraint(expected);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000655C File Offset: 0x0000475C
		[Obsolete("Deprecated, use Does.EndWith")]
		public static EndsWithConstraint StringEnding(string expected)
		{
			return new EndsWithConstraint(expected);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006564 File Offset: 0x00004764
		[Obsolete("Deprecated, use Does.Match")]
		public static RegexConstraint StringMatching(string pattern)
		{
			return new RegexConstraint(pattern);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00006A5E File Offset: 0x00004C5E
		public static SamePathConstraint SamePath(string expected)
		{
			return new SamePathConstraint(expected);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006A66 File Offset: 0x00004C66
		public static SubPathConstraint SubPathOf(string expected)
		{
			return new SubPathConstraint(expected);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006A6E File Offset: 0x00004C6E
		public static SamePathOrUnderConstraint SamePathOrUnder(string expected)
		{
			return new SamePathOrUnderConstraint(expected);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006A76 File Offset: 0x00004C76
		public static RangeConstraint InRange(IComparable from, IComparable to)
		{
			return new RangeConstraint(from, to);
		}
	}
}
