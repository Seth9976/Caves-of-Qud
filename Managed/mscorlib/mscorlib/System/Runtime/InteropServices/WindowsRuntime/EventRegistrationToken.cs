using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>A token that is returned when an event handler is added to a Windows Runtime event. The token is used to remove the event handler from the event at a later time. </summary>
	// Token: 0x02000788 RID: 1928
	public struct EventRegistrationToken
	{
		// Token: 0x0600448B RID: 17547 RVA: 0x000E3B9D File Offset: 0x000E1D9D
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x000E3BA6 File Offset: 0x000E1DA6
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are equal. </summary>
		/// <returns>true if the two objects are equal; otherwise, false. </returns>
		/// <param name="left">The first instance to compare. </param>
		/// <param name="right">The second instance to compare. </param>
		// Token: 0x0600448D RID: 17549 RVA: 0x000E3BAE File Offset: 0x000E1DAE
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are not equal.</summary>
		/// <returns>true if the two instances are not equal; otherwise, false. </returns>
		/// <param name="left">The first instance to compare. </param>
		/// <param name="right">The second instance to compare. </param>
		// Token: 0x0600448E RID: 17550 RVA: 0x000E3BC3 File Offset: 0x000E1DC3
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether the current object is equal to the specified object. </summary>
		/// <returns>true  if the current object is equal to <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x0600448F RID: 17551 RVA: 0x000E3BDC File Offset: 0x000E1DDC
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		/// <summary>Returns the hash code for this instance. </summary>
		/// <returns>The hash code for this instance. </returns>
		// Token: 0x06004490 RID: 17552 RVA: 0x000E3C09 File Offset: 0x000E1E09
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002C24 RID: 11300
		internal ulong m_value;
	}
}
