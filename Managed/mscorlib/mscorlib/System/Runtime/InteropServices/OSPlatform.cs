using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CC RID: 1740
	public readonly struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000E04EA File Offset: 0x000DE6EA
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x000E04F1 File Offset: 0x000DE6F1
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000E04F8 File Offset: 0x000DE6F8
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x06004005 RID: 16389 RVA: 0x000E04FF File Offset: 0x000DE6FF
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException("Value cannot be empty.", "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000E052E File Offset: 0x000DE72E
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000E0536 File Offset: 0x000DE736
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000E0544 File Offset: 0x000DE744
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000E0553 File Offset: 0x000DE753
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000E056B File Offset: 0x000DE76B
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x000E0582 File Offset: 0x000DE782
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x000E0593 File Offset: 0x000DE793
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000E059D File Offset: 0x000DE79D
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x04002A04 RID: 10756
		private readonly string _osPlatform;
	}
}
