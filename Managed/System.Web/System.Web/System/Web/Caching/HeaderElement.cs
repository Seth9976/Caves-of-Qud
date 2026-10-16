using System;

namespace System.Web.Caching
{
	/// <summary>Represents a single HTTP header that is included in a response from the output cache.</summary>
	// Token: 0x02000689 RID: 1673
	[Serializable]
	public sealed class HeaderElement
	{
		/// <summary>Gets the name of an HTTP header that is in the output cache.</summary>
		/// <returns>The name of the HTTP header. </returns>
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x000C66CC File Offset: 0x000C48CC
		// (set) Token: 0x06004745 RID: 18245 RVA: 0x000C66D4 File Offset: 0x000C48D4
		public string Name { get; private set; }

		/// <summary>Gets the value of an HTTP header that is in the output cache.</summary>
		/// <returns>The value of the HTTP header.</returns>
		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x000C66DD File Offset: 0x000C48DD
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x000C66E5 File Offset: 0x000C48E5
		public string Value { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.HeaderElement" /> class. </summary>
		/// <param name="name">The name of the HTTP header.</param>
		/// <param name="value">The value of the HTTP header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		// Token: 0x06004748 RID: 18248 RVA: 0x000C66EE File Offset: 0x000C48EE
		public HeaderElement(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Name = name;
			this.Value = value;
		}
	}
}
