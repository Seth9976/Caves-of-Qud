using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000FC RID: 252
	public class ConstraintFactory
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00013231 File Offset: 0x00011431
		public ConstraintExpression Not
		{
			get
			{
				return Is.Not;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00013238 File Offset: 0x00011438
		public ConstraintExpression No
		{
			get
			{
				return Has.No;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001323F File Offset: 0x0001143F
		public ConstraintExpression All
		{
			get
			{
				return Is.All;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00013246 File Offset: 0x00011446
		public ConstraintExpression Some
		{
			get
			{
				return Has.Some;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001324D File Offset: 0x0001144D
		public ConstraintExpression None
		{
			get
			{
				return Has.None;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00013254 File Offset: 0x00011454
		public static ConstraintExpression Exactly(int expectedCount)
		{
			return Has.Exactly(expectedCount);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001325C File Offset: 0x0001145C
		public ResolvableConstraintExpression Property(string name)
		{
			return Has.Property(name);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00013264 File Offset: 0x00011464
		public ResolvableConstraintExpression Length
		{
			get
			{
				return Has.Length;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001326B File Offset: 0x0001146B
		public ResolvableConstraintExpression Count
		{
			get
			{
				return Has.Count;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00013272 File Offset: 0x00011472
		public ResolvableConstraintExpression Message
		{
			get
			{
				return Has.Message;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00013279 File Offset: 0x00011479
		public ResolvableConstraintExpression InnerException
		{
			get
			{
				return Has.InnerException;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00013280 File Offset: 0x00011480
		public ResolvableConstraintExpression Attribute(Type expectedType)
		{
			return Has.Attribute(expectedType);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00013288 File Offset: 0x00011488
		public ResolvableConstraintExpression Attribute<TExpected>()
		{
			return this.Attribute(typeof(TExpected));
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0000694C File Offset: 0x00004B4C
		public NullConstraint Null
		{
			get
			{
				return new NullConstraint();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00006953 File Offset: 0x00004B53
		public TrueConstraint True
		{
			get
			{
				return new TrueConstraint();
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0000695A File Offset: 0x00004B5A
		public FalseConstraint False
		{
			get
			{
				return new FalseConstraint();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00006961 File Offset: 0x00004B61
		public GreaterThanConstraint Positive
		{
			get
			{
				return new GreaterThanConstraint(0);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0000696E File Offset: 0x00004B6E
		public LessThanConstraint Negative
		{
			get
			{
				return new LessThanConstraint(0);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0000697B File Offset: 0x00004B7B
		public EqualConstraint Zero
		{
			get
			{
				return new EqualConstraint(0);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00006988 File Offset: 0x00004B88
		public NaNConstraint NaN
		{
			get
			{
				return new NaNConstraint();
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0000698F File Offset: 0x00004B8F
		public EmptyConstraint Empty
		{
			get
			{
				return new EmptyConstraint();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00006996 File Offset: 0x00004B96
		public UniqueItemsConstraint Unique
		{
			get
			{
				return new UniqueItemsConstraint();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000699D File Offset: 0x00004B9D
		public BinarySerializableConstraint BinarySerializable
		{
			get
			{
				return new BinarySerializableConstraint();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000069A4 File Offset: 0x00004BA4
		public XmlSerializableConstraint XmlSerializable
		{
			get
			{
				return new XmlSerializableConstraint();
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001329A File Offset: 0x0001149A
		public EqualConstraint EqualTo(object expected)
		{
			return new EqualConstraint(expected);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000132A2 File Offset: 0x000114A2
		public SameAsConstraint SameAs(object expected)
		{
			return new SameAsConstraint(expected);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000132AA File Offset: 0x000114AA
		public GreaterThanConstraint GreaterThan(object expected)
		{
			return new GreaterThanConstraint(expected);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000132B2 File Offset: 0x000114B2
		public GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected)
		{
			return new GreaterThanOrEqualConstraint(expected);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000132B2 File Offset: 0x000114B2
		public GreaterThanOrEqualConstraint AtLeast(object expected)
		{
			return new GreaterThanOrEqualConstraint(expected);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000132BA File Offset: 0x000114BA
		public LessThanConstraint LessThan(object expected)
		{
			return new LessThanConstraint(expected);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000132C2 File Offset: 0x000114C2
		public LessThanOrEqualConstraint LessThanOrEqualTo(object expected)
		{
			return new LessThanOrEqualConstraint(expected);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000132C2 File Offset: 0x000114C2
		public LessThanOrEqualConstraint AtMost(object expected)
		{
			return new LessThanOrEqualConstraint(expected);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000132CA File Offset: 0x000114CA
		public ExactTypeConstraint TypeOf(Type expectedType)
		{
			return new ExactTypeConstraint(expectedType);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000132D2 File Offset: 0x000114D2
		public ExactTypeConstraint TypeOf<TExpected>()
		{
			return new ExactTypeConstraint(typeof(TExpected));
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000132E3 File Offset: 0x000114E3
		public InstanceOfTypeConstraint InstanceOf(Type expectedType)
		{
			return new InstanceOfTypeConstraint(expectedType);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000132EB File Offset: 0x000114EB
		public InstanceOfTypeConstraint InstanceOf<TExpected>()
		{
			return new InstanceOfTypeConstraint(typeof(TExpected));
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000132FC File Offset: 0x000114FC
		public AssignableFromConstraint AssignableFrom(Type expectedType)
		{
			return new AssignableFromConstraint(expectedType);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00013304 File Offset: 0x00011504
		public AssignableFromConstraint AssignableFrom<TExpected>()
		{
			return new AssignableFromConstraint(typeof(TExpected));
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00013315 File Offset: 0x00011515
		public AssignableToConstraint AssignableTo(Type expectedType)
		{
			return new AssignableToConstraint(expectedType);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001331D File Offset: 0x0001151D
		public AssignableToConstraint AssignableTo<TExpected>()
		{
			return new AssignableToConstraint(typeof(TExpected));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001332E File Offset: 0x0001152E
		public CollectionEquivalentConstraint EquivalentTo(IEnumerable expected)
		{
			return new CollectionEquivalentConstraint(expected);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00013336 File Offset: 0x00011536
		public CollectionSubsetConstraint SubsetOf(IEnumerable expected)
		{
			return new CollectionSubsetConstraint(expected);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001333E File Offset: 0x0001153E
		public CollectionSupersetConstraint SupersetOf(IEnumerable expected)
		{
			return new CollectionSupersetConstraint(expected);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00006A57 File Offset: 0x00004C57
		public CollectionOrderedConstraint Ordered
		{
			get
			{
				return new CollectionOrderedConstraint();
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00013346 File Offset: 0x00011546
		public CollectionContainsConstraint Member(object expected)
		{
			return new CollectionContainsConstraint(expected);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00013346 File Offset: 0x00011546
		public CollectionContainsConstraint Contains(object expected)
		{
			return new CollectionContainsConstraint(expected);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001334E File Offset: 0x0001154E
		public ContainsConstraint Contains(string expected)
		{
			return new ContainsConstraint(expected);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00013356 File Offset: 0x00011556
		[Obsolete("Deprecated, use Contains")]
		public SubstringConstraint StringContaining(string expected)
		{
			return new SubstringConstraint(expected);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00013356 File Offset: 0x00011556
		[Obsolete("Deprecated, use Contains")]
		public SubstringConstraint ContainsSubstring(string expected)
		{
			return new SubstringConstraint(expected);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001335E File Offset: 0x0001155E
		[Obsolete("Deprecated, use Does.Not.Contain")]
		public SubstringConstraint DoesNotContain(string expected)
		{
			return new ConstraintExpression().Not.ContainsSubstring(expected);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00013370 File Offset: 0x00011570
		public StartsWithConstraint StartWith(string expected)
		{
			return new StartsWithConstraint(expected);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00013370 File Offset: 0x00011570
		public StartsWithConstraint StartsWith(string expected)
		{
			return new StartsWithConstraint(expected);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00013370 File Offset: 0x00011570
		[Obsolete("Deprecated, use Does.StartWith or StartsWith")]
		public StartsWithConstraint StringStarting(string expected)
		{
			return new StartsWithConstraint(expected);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00013378 File Offset: 0x00011578
		[Obsolete("Deprecated, use Does.Not.StartWith")]
		public StartsWithConstraint DoesNotStartWith(string expected)
		{
			return new ConstraintExpression().Not.StartsWith(expected);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001338A File Offset: 0x0001158A
		public EndsWithConstraint EndWith(string expected)
		{
			return new EndsWithConstraint(expected);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001338A File Offset: 0x0001158A
		public EndsWithConstraint EndsWith(string expected)
		{
			return new EndsWithConstraint(expected);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001338A File Offset: 0x0001158A
		[Obsolete("Deprecated, use Does.EndWith or EndsWith")]
		public EndsWithConstraint StringEnding(string expected)
		{
			return new EndsWithConstraint(expected);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00013392 File Offset: 0x00011592
		[Obsolete("Deprecated, use Does.Not.EndWith")]
		public EndsWithConstraint DoesNotEndWith(string expected)
		{
			return new ConstraintExpression().Not.EndsWith(expected);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000133A4 File Offset: 0x000115A4
		public RegexConstraint Match(string pattern)
		{
			return new RegexConstraint(pattern);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000133A4 File Offset: 0x000115A4
		public RegexConstraint Matches(string pattern)
		{
			return new RegexConstraint(pattern);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000133A4 File Offset: 0x000115A4
		[Obsolete("Deprecated, use Does.Match or Matches")]
		public RegexConstraint StringMatching(string pattern)
		{
			return new RegexConstraint(pattern);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000133AC File Offset: 0x000115AC
		[Obsolete("Deprecated, use Does.Not.Match")]
		public RegexConstraint DoesNotMatch(string pattern)
		{
			return new ConstraintExpression().Not.Matches(pattern);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000133BE File Offset: 0x000115BE
		public SamePathConstraint SamePath(string expected)
		{
			return new SamePathConstraint(expected);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000133C6 File Offset: 0x000115C6
		public SubPathConstraint SubPathOf(string expected)
		{
			return new SubPathConstraint(expected);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000133CE File Offset: 0x000115CE
		public SamePathOrUnderConstraint SamePathOrUnder(string expected)
		{
			return new SamePathOrUnderConstraint(expected);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000133D6 File Offset: 0x000115D6
		public RangeConstraint InRange(IComparable from, IComparable to)
		{
			return new RangeConstraint(from, to);
		}
	}
}
