using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for building a set of related custom designers.</summary>
	// Token: 0x0200046E RID: 1134
	public interface ITreeDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets a collection of child designers.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" />, containing the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> child objects of the current designer. </returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002482 RID: 9346
		ICollection Children { get; }

		/// <summary>Gets the parent designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or null if there is no parent.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002483 RID: 9347
		IDesigner Parent { get; }
	}
}
