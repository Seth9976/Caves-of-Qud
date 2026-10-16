using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a service that can generate unique names for objects.</summary>
	// Token: 0x0200048E RID: 1166
	public interface INameCreationService
	{
		/// <summary>Creates a new name that is unique to all components in the specified container.</summary>
		/// <returns>A unique name for the data type.</returns>
		/// <param name="container">The container where the new object is added. </param>
		/// <param name="dataType">The data type of the object that receives the name. </param>
		// Token: 0x06002538 RID: 9528
		string CreateName(IContainer container, Type dataType);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <returns>true if the name is valid; otherwise, false.</returns>
		/// <param name="name">The name to validate. </param>
		// Token: 0x06002539 RID: 9529
		bool IsValidName(string name);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <param name="name">The name to validate. </param>
		// Token: 0x0600253A RID: 9530
		void ValidateName(string name);
	}
}
