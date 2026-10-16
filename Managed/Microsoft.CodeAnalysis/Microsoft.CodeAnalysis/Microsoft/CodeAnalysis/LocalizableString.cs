using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200017B RID: 379
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LocalizableString : IFormattable, IEquatable<LocalizableString>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000FC4 RID: 4036 RVA: 0x0002E48C File Offset: 0x0002C68C
		// (remove) Token: 0x06000FC5 RID: 4037 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public event EventHandler<Exception> OnException;

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		public string ToString([Nullable(2)] IFormatProvider formatProvider)
		{
			string text;
			try
			{
				text = this.GetText(formatProvider);
			}
			catch (Exception ex)
			{
				this.RaiseOnException(ex);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002E534 File Offset: 0x0002C734
		[return: Nullable(2)]
		public static explicit operator string(LocalizableString localizableResource)
		{
			return localizableResource.ToString(null);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002E53D File Offset: 0x0002C73D
		public static implicit operator LocalizableString([Nullable(2)] string fixedResource)
		{
			return LocalizableString.FixedLocalizableString.Create(fixedResource);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002E545 File Offset: 0x0002C745
		public sealed override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002E54E File Offset: 0x0002C74E
		[NullableContext(2)]
		[return: Nullable(1)]
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0002E558 File Offset: 0x0002C758
		public sealed override int GetHashCode()
		{
			int num;
			try
			{
				num = this.GetHash();
			}
			catch (Exception ex)
			{
				this.RaiseOnException(ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0002E58C File Offset: 0x0002C78C
		[NullableContext(2)]
		public sealed override bool Equals(object other)
		{
			bool flag;
			try
			{
				flag = this.AreEqual(other);
			}
			catch (Exception ex)
			{
				this.RaiseOnException(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		[NullableContext(2)]
		public bool Equals(LocalizableString other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000FCE RID: 4046
		protected abstract string GetText([Nullable(2)] IFormatProvider formatProvider);

		// Token: 0x06000FCF RID: 4047
		protected abstract int GetHash();

		// Token: 0x06000FD0 RID: 4048
		[NullableContext(2)]
		protected abstract bool AreEqual(object other);

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		private void RaiseOnException(Exception ex)
		{
			if (ex is OperationCanceledException)
			{
				return;
			}
			try
			{
				EventHandler<Exception> onException = this.OnException;
				if (onException != null)
				{
					onException.Invoke(this, ex);
				}
			}
			catch
			{
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0002E60C File Offset: 0x0002C80C
		internal virtual bool CanThrowExceptions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x020005A9 RID: 1449
		[Nullable(0)]
		private sealed class FixedLocalizableString : LocalizableString
		{
			// Token: 0x060035AC RID: 13740 RVA: 0x00096E7B File Offset: 0x0009507B
			public static LocalizableString.FixedLocalizableString Create([Nullable(2)] string fixedResource)
			{
				if (RoslynString.IsNullOrEmpty(fixedResource))
				{
					return LocalizableString.FixedLocalizableString.s_empty;
				}
				return new LocalizableString.FixedLocalizableString(fixedResource);
			}

			// Token: 0x060035AD RID: 13741 RVA: 0x00096E91 File Offset: 0x00095091
			private FixedLocalizableString(string fixedResource)
			{
				this._fixedString = fixedResource;
			}

			// Token: 0x060035AE RID: 13742 RVA: 0x00096EA0 File Offset: 0x000950A0
			protected override string GetText([Nullable(2)] IFormatProvider formatProvider)
			{
				return this._fixedString;
			}

			// Token: 0x060035AF RID: 13743 RVA: 0x00096EA8 File Offset: 0x000950A8
			[NullableContext(2)]
			protected override bool AreEqual(object other)
			{
				LocalizableString.FixedLocalizableString fixedLocalizableString = other as LocalizableString.FixedLocalizableString;
				return fixedLocalizableString != null && string.Equals(this._fixedString, fixedLocalizableString._fixedString);
			}

			// Token: 0x060035B0 RID: 13744 RVA: 0x00096ED2 File Offset: 0x000950D2
			protected override int GetHash()
			{
				string fixedString = this._fixedString;
				if (fixedString == null)
				{
					return 0;
				}
				return fixedString.GetHashCode();
			}

			// Token: 0x17000F44 RID: 3908
			// (get) Token: 0x060035B1 RID: 13745 RVA: 0x00096EE5 File Offset: 0x000950E5
			internal override bool CanThrowExceptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04001654 RID: 5716
			private static readonly LocalizableString.FixedLocalizableString s_empty = new LocalizableString.FixedLocalizableString(string.Empty);

			// Token: 0x04001655 RID: 5717
			private readonly string _fixedString;
		}
	}
}
