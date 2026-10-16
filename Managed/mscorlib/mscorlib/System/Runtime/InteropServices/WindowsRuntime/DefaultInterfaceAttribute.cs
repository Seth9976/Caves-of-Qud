using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the default interface of a managed Windows Runtime class.</summary>
	// Token: 0x02000782 RID: 1922
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute" /> class. </summary>
		/// <param name="defaultInterface">The interface type that is specified as the default interface for the class the attribute is applied to. </param>
		// Token: 0x0600447E RID: 17534 RVA: 0x000E3B1A File Offset: 0x000E1D1A
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		/// <summary>Gets the type of the default interface. </summary>
		/// <returns>The type of the default interface. </returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x000E3B29 File Offset: 0x000E1D29
		public Type DefaultInterface
		{
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002C1D RID: 11293
		private Type m_defaultInterface;
	}
}
