using System;

namespace System.Reflection
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum MethodSemanticsAttributes
	{
		/// <summary>
		///   <para>Used to modify the value of the property.</para>
		///   <para>CLS-compliant setters are named with the <see langword="set_" /> prefix.</para>
		/// </summary>
		// Token: 0x04000010 RID: 16
		Setter = 1,
		/// <summary>
		///   <para>Reads the value of the property.</para>
		///   <para>CLS-compliant getters are named with get_ prefix.</para>
		/// </summary>
		// Token: 0x04000011 RID: 17
		Getter = 2,
		/// <summary>Other method for a property (not a getter or setter) or an event (not an adder, remover, or raiser).</summary>
		// Token: 0x04000012 RID: 18
		Other = 4,
		/// <summary>
		///   <para>Used to add a handler for an event. Corresponds to the <see langword="AddOn" /> flag in the Ecma 335 CLI specification.</para>
		///   <para>CLS-compliant adders are named the with <see langword="add_" /> prefix.</para>
		/// </summary>
		// Token: 0x04000013 RID: 19
		Adder = 8,
		/// <summary>
		///   <para>Used to remove a handler for an event. Corresponds to the <see langword="RemoveOn" /> flag in the Ecma 335 CLI specification.</para>
		///   <para>CLS-compliant removers are named with the <see langword="remove_" /> prefix.</para>
		/// </summary>
		// Token: 0x04000014 RID: 20
		Remover = 16,
		/// <summary>
		///   <para>Used to indicate that an event has occurred. Corresponds to the <see langword="Fire" /> flag in the Ecma 335 CLI specification.</para>
		///   <para> CLS-compliant raisers are named with the <see langword="raise_" /> prefix.</para>
		/// </summary>
		// Token: 0x04000015 RID: 21
		Raiser = 32
	}
}
