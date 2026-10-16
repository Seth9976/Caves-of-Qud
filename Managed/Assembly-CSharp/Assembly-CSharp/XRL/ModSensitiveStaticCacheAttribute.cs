using System;

namespace XRL
{
	/// <summary>
	///     Flag a static field for "reset" when mods reload.  The class should have <see cref="T:XRL.HasModSensitiveStaticCacheAttribute" />.
	///     <para>
	///         The default mode is to set the value to <see cref="!:null" />, but can be set to an empty instance with the
	///         <see cref="F:XRL.ModSensitiveStaticCacheAttribute.CreateEmptyInstance" /> field or constructor parameters.
	///     </para>
	/// </summary>
	// Token: 0x020004C0 RID: 1216
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class ModSensitiveStaticCacheAttribute : Attribute
	{
		// Token: 0x060034E2 RID: 13538 RVA: 0x000FFD92 File Offset: 0x000FDF92
		public ModSensitiveStaticCacheAttribute(bool createEmptyInstance = false)
		{
			this.CreateEmptyInstance = createEmptyInstance;
		}

		// Token: 0x04001AC1 RID: 6849
		public bool CreateEmptyInstance;
	}
}
