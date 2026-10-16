using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent an event.</summary>
	// Token: 0x02000923 RID: 2339
	[ComVisible(true)]
	[Serializable]
	public readonly struct EventToken : IEquatable<EventToken>
	{
		// Token: 0x0600500A RID: 20490 RVA: 0x000FAAAD File Offset: 0x000F8CAD
		internal EventToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of EventToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of EventToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with this instance. </param>
		// Token: 0x0600500B RID: 20491 RVA: 0x000FAAB8 File Offset: 0x000F8CB8
		public override bool Equals(object obj)
		{
			bool flag = obj is EventToken;
			if (flag)
			{
				EventToken eventToken = (EventToken)obj;
				flag = this.tokValue == eventToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.EventToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to the current instance.</param>
		// Token: 0x0600500C RID: 20492 RVA: 0x000FAAE9 File Offset: 0x000F8CE9
		public bool Equals(EventToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600500D RID: 20493 RVA: 0x000FAAF9 File Offset: 0x000F8CF9
		public static bool operator ==(EventToken a, EventToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600500E RID: 20494 RVA: 0x000FAB0C File Offset: 0x000F8D0C
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this event.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x0600500F RID: 20495 RVA: 0x000FAB22 File Offset: 0x000F8D22
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this event.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this event.</returns>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06005010 RID: 20496 RVA: 0x000FAB22 File Offset: 0x000F8D22
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x0400315B RID: 12635
		internal readonly int tokValue;

		/// <summary>The default EventToken with <see cref="P:System.Reflection.Emit.EventToken.Token" /> value 0.</summary>
		// Token: 0x0400315C RID: 12636
		public static readonly EventToken Empty;
	}
}
