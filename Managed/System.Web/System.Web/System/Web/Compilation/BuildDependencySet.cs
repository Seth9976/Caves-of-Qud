using System;
using System.Collections;

namespace System.Web.Compilation
{
	/// <summary>Represents dependencies returned by the build manager.</summary>
	// Token: 0x02000632 RID: 1586
	public sealed class BuildDependencySet
	{
		// Token: 0x06004424 RID: 17444 RVA: 0x00002050 File Offset: 0x00000250
		internal BuildDependencySet()
		{
		}

		/// <summary>Gets a string representing the hash code of the dependent virtual paths.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the hash code of the dependent virtual paths.</returns>
		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public string HashCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a list of virtual path dependencies.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> containing the virtual path dependencies.</returns>
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public IEnumerable VirtualPaths
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
