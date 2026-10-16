using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for design-time license context serialization.</summary>
	// Token: 0x02000452 RID: 1106
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x0000219B File Offset: 0x0000039B
		private DesigntimeLicenseContextSerializer()
		{
		}

		/// <summary>Serializes the licenses within the specified design-time license context using the specified key and output stream.</summary>
		/// <param name="o">The stream to output to. </param>
		/// <param name="cryptoKey">The key to use for encryption. </param>
		/// <param name="context">A <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> indicating the license context. </param>
		// Token: 0x060023EA RID: 9194 RVA: 0x00081703 File Offset: 0x0007F903
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			((IFormatter)new BinaryFormatter()).Serialize(o, new object[] { cryptoKey, context.savedLicenseKeys });
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00081724 File Offset: 0x0007F924
		internal static void Deserialize(Stream o, string cryptoKey, RuntimeLicenseContext context)
		{
			object obj = ((IFormatter)new BinaryFormatter()).Deserialize(o);
			if (obj is object[])
			{
				object[] array = (object[])obj;
				if (array[0] is string && (string)array[0] == cryptoKey)
				{
					context.savedLicenseKeys = (Hashtable)array[1];
				}
			}
		}
	}
}
