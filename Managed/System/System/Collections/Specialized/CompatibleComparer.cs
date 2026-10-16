using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06002757 RID: 10071 RVA: 0x0008882F File Offset: 0x00086A2F
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00088848 File Offset: 0x00086A48
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000888B8 File Offset: 0x00086AB8
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x000888E3 File Offset: 0x00086AE3
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000888EB File Offset: 0x00086AEB
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x000888F3 File Offset: 0x00086AF3
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x00088916 File Offset: 0x00086B16
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x0400155B RID: 5467
		private IComparer _comparer;

		// Token: 0x0400155C RID: 5468
		private static volatile IComparer defaultComparer;

		// Token: 0x0400155D RID: 5469
		private IHashCodeProvider _hcp;

		// Token: 0x0400155E RID: 5470
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
