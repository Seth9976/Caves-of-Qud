using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000108 RID: 264
	public class EqualConstraint : Constraint
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00013B14 File Offset: 0x00011D14
		public EqualConstraint(object expected)
			: base(new object[] { expected })
		{
			this.AdjustArgumentIfNeeded<object>(ref expected);
			this._expected = expected;
			this.ClipStrings = true;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00013B52 File Offset: 0x00011D52
		public Tolerance Tolerance
		{
			get
			{
				return this._tolerance;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00013B5A File Offset: 0x00011D5A
		public bool CaseInsensitive
		{
			get
			{
				return this._comparer.IgnoreCase;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00013B67 File Offset: 0x00011D67
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00013B6F File Offset: 0x00011D6F
		public bool ClipStrings { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00013B78 File Offset: 0x00011D78
		public IList<NUnitEqualityComparer.FailurePoint> FailurePoints
		{
			get
			{
				return this._comparer.FailurePoints;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00013B85 File Offset: 0x00011D85
		public EqualConstraint IgnoreCase
		{
			get
			{
				this._comparer.IgnoreCase = true;
				return this;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00013B94 File Offset: 0x00011D94
		public EqualConstraint NoClip
		{
			get
			{
				this.ClipStrings = false;
				return this;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00013B9E File Offset: 0x00011D9E
		public EqualConstraint AsCollection
		{
			get
			{
				this._comparer.CompareAsCollection = true;
				return this;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00013BAD File Offset: 0x00011DAD
		public EqualConstraint Within(object amount)
		{
			if (!this._tolerance.IsUnsetOrDefault)
			{
				throw new InvalidOperationException("Within modifier may appear only once in a constraint expression");
			}
			this._tolerance = new Tolerance(amount);
			return this;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public EqualConstraint WithSameOffset
		{
			get
			{
				this._comparer.WithSameOffset = true;
				return this;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00013BE3 File Offset: 0x00011DE3
		public EqualConstraint Ulps
		{
			get
			{
				this._tolerance = this._tolerance.Ulps;
				return this;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00013BF7 File Offset: 0x00011DF7
		public EqualConstraint Percent
		{
			get
			{
				this._tolerance = this._tolerance.Percent;
				return this;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00013C0B File Offset: 0x00011E0B
		public EqualConstraint Days
		{
			get
			{
				this._tolerance = this._tolerance.Days;
				return this;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00013C1F File Offset: 0x00011E1F
		public EqualConstraint Hours
		{
			get
			{
				this._tolerance = this._tolerance.Hours;
				return this;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00013C33 File Offset: 0x00011E33
		public EqualConstraint Minutes
		{
			get
			{
				this._tolerance = this._tolerance.Minutes;
				return this;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00013C47 File Offset: 0x00011E47
		public EqualConstraint Seconds
		{
			get
			{
				this._tolerance = this._tolerance.Seconds;
				return this;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00013C5B File Offset: 0x00011E5B
		public EqualConstraint Milliseconds
		{
			get
			{
				this._tolerance = this._tolerance.Milliseconds;
				return this;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00013C6F File Offset: 0x00011E6F
		public EqualConstraint Ticks
		{
			get
			{
				this._tolerance = this._tolerance.Ticks;
				return this;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00013C83 File Offset: 0x00011E83
		public EqualConstraint Using(IComparer comparer)
		{
			this._comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
			return this;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00013C9C File Offset: 0x00011E9C
		public EqualConstraint Using<T>(IComparer<T> comparer)
		{
			this._comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00013CB5 File Offset: 0x00011EB5
		public EqualConstraint Using<T>(Comparison<T> comparer)
		{
			this._comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00013CCE File Offset: 0x00011ECE
		public EqualConstraint Using(IEqualityComparer comparer)
		{
			this._comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
			return this;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public EqualConstraint Using<T>(IEqualityComparer<T> comparer)
		{
			this._comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00013D00 File Offset: 0x00011F00
		public override ConstraintResult ApplyTo(object actual)
		{
			this.AdjustArgumentIfNeeded<object>(ref actual);
			return new EqualConstraintResult(this, actual, this._comparer.AreEqual(this._expected, actual, ref this._tolerance));
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00013D2C File Offset: 0x00011F2C
		public override string Description
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(MsgUtils.FormatValue(this._expected));
				if (this._tolerance != null && !this._tolerance.IsUnsetOrDefault)
				{
					stringBuilder.Append(" +/- ");
					stringBuilder.Append(MsgUtils.FormatValue(this._tolerance.Value));
					if (this._tolerance.Mode != ToleranceMode.Linear)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(this._tolerance.Mode.ToString());
					}
				}
				if (this._comparer.IgnoreCase)
				{
					stringBuilder.Append(", ignoring case");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00013DDC File Offset: 0x00011FDC
		private void AdjustArgumentIfNeeded<T>(ref T arg)
		{
			if (arg != null)
			{
				Type type = arg.GetType();
				if ((type.GetTypeInfo().IsGenericType ? type.GetGenericTypeDefinition() : null) == typeof(ArraySegment<>) && type.GetProperty("Array").GetValue(arg, null) == null)
				{
					Array array = Array.CreateInstance(type.GetGenericArguments()[0], 0);
					ConstructorInfo constructor = type.GetConstructor(new Type[] { array.GetType() });
					arg = (T)((object)constructor.Invoke(new object[] { array }));
				}
			}
		}

		// Token: 0x040001C1 RID: 449
		private readonly object _expected;

		// Token: 0x040001C2 RID: 450
		private Tolerance _tolerance = Tolerance.Default;

		// Token: 0x040001C3 RID: 451
		private NUnitEqualityComparer _comparer = new NUnitEqualityComparer();
	}
}
