using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>Represents part of an output-cache entry that is stored in memory.</summary>
	// Token: 0x0200068B RID: 1675
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class MemoryResponseElement : ResponseElement
	{
		/// <summary>Gets an array that contains all or part of an output-cache response.</summary>
		/// <returns>An array of byte objects.</returns>
		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x000C67C8 File Offset: 0x000C49C8
		// (set) Token: 0x0600474F RID: 18255 RVA: 0x000C67D0 File Offset: 0x000C49D0
		public byte[] Buffer { get; private set; }

		/// <summary>Gets the size of the array that is referenced by the <see cref="P:System.Web.Caching.MemoryResponseElement.Buffer" /> property.</summary>
		/// <returns>The size of the array.</returns>
		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x000C67D9 File Offset: 0x000C49D9
		// (set) Token: 0x06004751 RID: 18257 RVA: 0x000C67E1 File Offset: 0x000C49E1
		public long Length { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.MemoryResponseElement" /> class.</summary>
		/// <param name="buffer">An array of bytes that contains a part of an output-cache response. </param>
		/// <param name="length">The size of the array in <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than the size of <paramref name="buffer" />.</exception>
		// Token: 0x06004752 RID: 18258 RVA: 0x000C67EA File Offset: 0x000C49EA
		public MemoryResponseElement(byte[] buffer, long length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length < 0L || length > (long)buffer.Length)
			{
				throw new ArgumentOutOfRangeException("length", "is less than zero or greater than the size of buffer.");
			}
			this.Buffer = buffer;
			this.Length = length;
		}
	}
}
