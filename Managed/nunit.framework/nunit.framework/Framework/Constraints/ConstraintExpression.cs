using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000FB RID: 251
	public class ConstraintExpression
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00012DB1 File Offset: 0x00010FB1
		public ConstraintExpression()
		{
			this.builder = new ConstraintBuilder();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00012DC4 File Offset: 0x00010FC4
		public ConstraintExpression(ConstraintBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00012DD3 File Offset: 0x00010FD3
		public override string ToString()
		{
			return this.builder.Resolve().ToString();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public ConstraintExpression Append(ConstraintOperator op)
		{
			this.builder.Append(op);
			return this;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public ResolvableConstraintExpression Append(SelfResolvingOperator op)
		{
			this.builder.Append(op);
			return new ResolvableConstraintExpression(this.builder);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00012E0D File Offset: 0x0001100D
		public Constraint Append(Constraint constraint)
		{
			this.builder.Append(constraint);
			return constraint;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00012E1C File Offset: 0x0001101C
		public ConstraintExpression Not
		{
			get
			{
				return this.Append(new NotOperator());
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00012E1C File Offset: 0x0001101C
		public ConstraintExpression No
		{
			get
			{
				return this.Append(new NotOperator());
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00012E29 File Offset: 0x00011029
		public ConstraintExpression All
		{
			get
			{
				return this.Append(new AllOperator());
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00012E36 File Offset: 0x00011036
		public ConstraintExpression Some
		{
			get
			{
				return this.Append(new SomeOperator());
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00012E43 File Offset: 0x00011043
		public ConstraintExpression None
		{
			get
			{
				return this.Append(new NoneOperator());
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00012E50 File Offset: 0x00011050
		public ConstraintExpression Exactly(int expectedCount)
		{
			return this.Append(new ExactCountOperator(expectedCount));
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012E5E File Offset: 0x0001105E
		public ResolvableConstraintExpression Property(string name)
		{
			return this.Append(new PropOperator(name));
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00012E6C File Offset: 0x0001106C
		public ResolvableConstraintExpression Length
		{
			get
			{
				return this.Property("Length");
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00012E79 File Offset: 0x00011079
		public ResolvableConstraintExpression Count
		{
			get
			{
				return this.Property("Count");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00012E86 File Offset: 0x00011086
		public ResolvableConstraintExpression Message
		{
			get
			{
				return this.Property("Message");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00012E93 File Offset: 0x00011093
		public ResolvableConstraintExpression InnerException
		{
			get
			{
				return this.Property("InnerException");
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00012EA0 File Offset: 0x000110A0
		public ResolvableConstraintExpression Attribute(Type expectedType)
		{
			return this.Append(new AttributeOperator(expectedType));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00012EAE File Offset: 0x000110AE
		public ResolvableConstraintExpression Attribute<TExpected>()
		{
			return this.Attribute(typeof(TExpected));
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00012EC0 File Offset: 0x000110C0
		public ConstraintExpression With
		{
			get
			{
				return this.Append(new WithOperator());
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00012ECD File Offset: 0x000110CD
		public Constraint Matches(IResolveConstraint constraint)
		{
			return this.Append((Constraint)constraint.Resolve());
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00012EE0 File Offset: 0x000110E0
		public Constraint Matches<TActual>(Predicate<TActual> predicate)
		{
			return this.Append(new PredicateConstraint<TActual>(predicate));
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00012EEE File Offset: 0x000110EE
		public NullConstraint Null
		{
			get
			{
				return (NullConstraint)this.Append(new NullConstraint());
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00012F00 File Offset: 0x00011100
		public TrueConstraint True
		{
			get
			{
				return (TrueConstraint)this.Append(new TrueConstraint());
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00012F12 File Offset: 0x00011112
		public FalseConstraint False
		{
			get
			{
				return (FalseConstraint)this.Append(new FalseConstraint());
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00012F24 File Offset: 0x00011124
		public GreaterThanConstraint Positive
		{
			get
			{
				return (GreaterThanConstraint)this.Append(new GreaterThanConstraint(0));
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00012F3C File Offset: 0x0001113C
		public LessThanConstraint Negative
		{
			get
			{
				return (LessThanConstraint)this.Append(new LessThanConstraint(0));
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00012F54 File Offset: 0x00011154
		public EqualConstraint Zero
		{
			get
			{
				return (EqualConstraint)this.Append(new EqualConstraint(0));
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00012F6C File Offset: 0x0001116C
		public NaNConstraint NaN
		{
			get
			{
				return (NaNConstraint)this.Append(new NaNConstraint());
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00012F7E File Offset: 0x0001117E
		public EmptyConstraint Empty
		{
			get
			{
				return (EmptyConstraint)this.Append(new EmptyConstraint());
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00012F90 File Offset: 0x00011190
		public UniqueItemsConstraint Unique
		{
			get
			{
				return (UniqueItemsConstraint)this.Append(new UniqueItemsConstraint());
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00012FA2 File Offset: 0x000111A2
		public BinarySerializableConstraint BinarySerializable
		{
			get
			{
				return (BinarySerializableConstraint)this.Append(new BinarySerializableConstraint());
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00012FB4 File Offset: 0x000111B4
		public XmlSerializableConstraint XmlSerializable
		{
			get
			{
				return (XmlSerializableConstraint)this.Append(new XmlSerializableConstraint());
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00012FC6 File Offset: 0x000111C6
		public EqualConstraint EqualTo(object expected)
		{
			return (EqualConstraint)this.Append(new EqualConstraint(expected));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00012FD9 File Offset: 0x000111D9
		public SameAsConstraint SameAs(object expected)
		{
			return (SameAsConstraint)this.Append(new SameAsConstraint(expected));
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00012FEC File Offset: 0x000111EC
		public GreaterThanConstraint GreaterThan(object expected)
		{
			return (GreaterThanConstraint)this.Append(new GreaterThanConstraint(expected));
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00012FFF File Offset: 0x000111FF
		public GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected)
		{
			return (GreaterThanOrEqualConstraint)this.Append(new GreaterThanOrEqualConstraint(expected));
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00012FFF File Offset: 0x000111FF
		public GreaterThanOrEqualConstraint AtLeast(object expected)
		{
			return (GreaterThanOrEqualConstraint)this.Append(new GreaterThanOrEqualConstraint(expected));
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00013012 File Offset: 0x00011212
		public LessThanConstraint LessThan(object expected)
		{
			return (LessThanConstraint)this.Append(new LessThanConstraint(expected));
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00013025 File Offset: 0x00011225
		public LessThanOrEqualConstraint LessThanOrEqualTo(object expected)
		{
			return (LessThanOrEqualConstraint)this.Append(new LessThanOrEqualConstraint(expected));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00013025 File Offset: 0x00011225
		public LessThanOrEqualConstraint AtMost(object expected)
		{
			return (LessThanOrEqualConstraint)this.Append(new LessThanOrEqualConstraint(expected));
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00013038 File Offset: 0x00011238
		public ExactTypeConstraint TypeOf(Type expectedType)
		{
			return (ExactTypeConstraint)this.Append(new ExactTypeConstraint(expectedType));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001304B File Offset: 0x0001124B
		public ExactTypeConstraint TypeOf<TExpected>()
		{
			return (ExactTypeConstraint)this.Append(new ExactTypeConstraint(typeof(TExpected)));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00013067 File Offset: 0x00011267
		public InstanceOfTypeConstraint InstanceOf(Type expectedType)
		{
			return (InstanceOfTypeConstraint)this.Append(new InstanceOfTypeConstraint(expectedType));
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001307A File Offset: 0x0001127A
		public InstanceOfTypeConstraint InstanceOf<TExpected>()
		{
			return (InstanceOfTypeConstraint)this.Append(new InstanceOfTypeConstraint(typeof(TExpected)));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00013096 File Offset: 0x00011296
		public AssignableFromConstraint AssignableFrom(Type expectedType)
		{
			return (AssignableFromConstraint)this.Append(new AssignableFromConstraint(expectedType));
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000130A9 File Offset: 0x000112A9
		public AssignableFromConstraint AssignableFrom<TExpected>()
		{
			return (AssignableFromConstraint)this.Append(new AssignableFromConstraint(typeof(TExpected)));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000130C5 File Offset: 0x000112C5
		public AssignableToConstraint AssignableTo(Type expectedType)
		{
			return (AssignableToConstraint)this.Append(new AssignableToConstraint(expectedType));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000130D8 File Offset: 0x000112D8
		public AssignableToConstraint AssignableTo<TExpected>()
		{
			return (AssignableToConstraint)this.Append(new AssignableToConstraint(typeof(TExpected)));
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000130F4 File Offset: 0x000112F4
		public CollectionEquivalentConstraint EquivalentTo(IEnumerable expected)
		{
			return (CollectionEquivalentConstraint)this.Append(new CollectionEquivalentConstraint(expected));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00013107 File Offset: 0x00011307
		public CollectionSubsetConstraint SubsetOf(IEnumerable expected)
		{
			return (CollectionSubsetConstraint)this.Append(new CollectionSubsetConstraint(expected));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001311A File Offset: 0x0001131A
		public CollectionSupersetConstraint SupersetOf(IEnumerable expected)
		{
			return (CollectionSupersetConstraint)this.Append(new CollectionSupersetConstraint(expected));
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001312D File Offset: 0x0001132D
		public CollectionOrderedConstraint Ordered
		{
			get
			{
				return (CollectionOrderedConstraint)this.Append(new CollectionOrderedConstraint());
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001313F File Offset: 0x0001133F
		public CollectionContainsConstraint Member(object expected)
		{
			return (CollectionContainsConstraint)this.Append(new CollectionContainsConstraint(expected));
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001313F File Offset: 0x0001133F
		public CollectionContainsConstraint Contains(object expected)
		{
			return (CollectionContainsConstraint)this.Append(new CollectionContainsConstraint(expected));
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00013152 File Offset: 0x00011352
		public ContainsConstraint Contains(string expected)
		{
			return (ContainsConstraint)this.Append(new ContainsConstraint(expected));
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00013152 File Offset: 0x00011352
		public ContainsConstraint Contain(string expected)
		{
			return (ContainsConstraint)this.Append(new ContainsConstraint(expected));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00013165 File Offset: 0x00011365
		public DictionaryContainsKeyConstraint ContainKey(object expected)
		{
			return (DictionaryContainsKeyConstraint)this.Append(new DictionaryContainsKeyConstraint(expected));
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00013178 File Offset: 0x00011378
		public DictionaryContainsValueConstraint ContainValue(object expected)
		{
			return (DictionaryContainsValueConstraint)this.Append(new DictionaryContainsValueConstraint(expected));
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001318B File Offset: 0x0001138B
		[Obsolete("Deprecated, use Contains")]
		public SubstringConstraint StringContaining(string expected)
		{
			return (SubstringConstraint)this.Append(new SubstringConstraint(expected));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001318B File Offset: 0x0001138B
		[Obsolete("Deprecated, use Contains")]
		public SubstringConstraint ContainsSubstring(string expected)
		{
			return (SubstringConstraint)this.Append(new SubstringConstraint(expected));
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001319E File Offset: 0x0001139E
		public StartsWithConstraint StartWith(string expected)
		{
			return (StartsWithConstraint)this.Append(new StartsWithConstraint(expected));
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001319E File Offset: 0x0001139E
		public StartsWithConstraint StartsWith(string expected)
		{
			return (StartsWithConstraint)this.Append(new StartsWithConstraint(expected));
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001319E File Offset: 0x0001139E
		[Obsolete("Deprecated, use Does.StartWith or StartsWith")]
		public StartsWithConstraint StringStarting(string expected)
		{
			return (StartsWithConstraint)this.Append(new StartsWithConstraint(expected));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000131B1 File Offset: 0x000113B1
		public EndsWithConstraint EndWith(string expected)
		{
			return (EndsWithConstraint)this.Append(new EndsWithConstraint(expected));
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000131B1 File Offset: 0x000113B1
		public EndsWithConstraint EndsWith(string expected)
		{
			return (EndsWithConstraint)this.Append(new EndsWithConstraint(expected));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000131B1 File Offset: 0x000113B1
		[Obsolete("Deprecated, use Does.EndWith or EndsWith")]
		public EndsWithConstraint StringEnding(string expected)
		{
			return (EndsWithConstraint)this.Append(new EndsWithConstraint(expected));
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000131C4 File Offset: 0x000113C4
		public RegexConstraint Match(string pattern)
		{
			return (RegexConstraint)this.Append(new RegexConstraint(pattern));
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000131C4 File Offset: 0x000113C4
		public RegexConstraint Matches(string pattern)
		{
			return (RegexConstraint)this.Append(new RegexConstraint(pattern));
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000131C4 File Offset: 0x000113C4
		[Obsolete("Deprecated, use Does.Match or Matches")]
		public RegexConstraint StringMatching(string pattern)
		{
			return (RegexConstraint)this.Append(new RegexConstraint(pattern));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000131D7 File Offset: 0x000113D7
		public SamePathConstraint SamePath(string expected)
		{
			return (SamePathConstraint)this.Append(new SamePathConstraint(expected));
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000131EA File Offset: 0x000113EA
		public SubPathConstraint SubPathOf(string expected)
		{
			return (SubPathConstraint)this.Append(new SubPathConstraint(expected));
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000131FD File Offset: 0x000113FD
		public SamePathOrUnderConstraint SamePathOrUnder(string expected)
		{
			return (SamePathOrUnderConstraint)this.Append(new SamePathOrUnderConstraint(expected));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00013210 File Offset: 0x00011410
		public RangeConstraint InRange(IComparable from, IComparable to)
		{
			return (RangeConstraint)this.Append(new RangeConstraint(from, to));
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00013224 File Offset: 0x00011424
		public Constraint Exist
		{
			get
			{
				return this.Append(new FileOrDirectoryExistsConstraint());
			}
		}

		// Token: 0x040001B0 RID: 432
		protected ConstraintBuilder builder;
	}
}
