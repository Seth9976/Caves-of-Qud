using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a destination <see cref="T:System.Type" /> in another assembly. </summary>
	// Token: 0x0200080A RID: 2058
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> class specifying a destination <see cref="T:System.Type" />. </summary>
		/// <param name="destination">The destination <see cref="T:System.Type" /> in another assembly.</param>
		// Token: 0x0600462B RID: 17963 RVA: 0x000E59E5 File Offset: 0x000E3BE5
		public TypeForwardedToAttribute(Type destination)
		{
			this.Destination = destination;
		}

		/// <summary>Gets the destination <see cref="T:System.Type" /> in another assembly.</summary>
		/// <returns>The destination <see cref="T:System.Type" /> in another assembly.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x000E59F4 File Offset: 0x000E3BF4
		public Type Destination { get; }
	}
}
