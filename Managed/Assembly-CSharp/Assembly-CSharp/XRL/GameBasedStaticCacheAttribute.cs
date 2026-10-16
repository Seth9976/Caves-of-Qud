using System;

namespace XRL
{
	/// <summary>
	///     Flag a static field for "reset" when game starts.  The class should have <see cref="T:XRL.HasGameBasedStaticCacheAttribute" />.
	///     <para>
	///         The default mode is to set the value to an empty instance, but can be toggled to set to null instead with the
	///         <see cref="F:XRL.GameBasedStaticCacheAttribute.CreateInstance" /> field or constructor parameters.
	///     </para>
	///     <para>
	///         <see cref="F:XRL.GameBasedStaticCacheAttribute.ClearInstance" /> will not reset the value, instead calling Clear() method on it.
	///     </para>
	/// </summary>
	// Token: 0x020004BC RID: 1212
	[AttributeUsage(AttributeTargets.Field)]
	public class GameBasedStaticCacheAttribute : Attribute
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x000FFD5D File Offset: 0x000FDF5D
		public GameBasedStaticCacheAttribute(bool CreateInstance = true, bool ClearInstance = false)
		{
			this.CreateInstance = CreateInstance;
			this.ClearInstance = ClearInstance;
		}

		// Token: 0x04001ABF RID: 6847
		public bool CreateInstance = true;

		/// <summary>
		/// If cache implements <see cref="T:System.Collections.IDictionary" />, <see cref="T:System.Collections.IList" />
		/// or otherwise defines a parameterless Clear method, invoke it.
		/// </summary>
		// Token: 0x04001AC0 RID: 6848
		public bool ClearInstance;
	}
}
