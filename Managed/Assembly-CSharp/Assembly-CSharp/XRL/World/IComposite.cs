using System;

namespace XRL.World
{
	/// <summary>
	/// Contracts a type as composited entirely from public fields supported by <see cref="T:XRL.Serialization.FastSerialization" />
	/// and/or handling its own serialization.
	/// </summary>
	// Token: 0x02000916 RID: 2326
	public interface IComposite
	{
		/// <summary>
		/// If true, public instance field values will be serialized via reflection.
		/// </summary>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x001AD74D File Offset: 0x001AB94D
		bool WantFieldReflection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x001AD750 File Offset: 0x001AB950
		void Write(SerializationWriter Writer)
		{
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x001AD752 File Offset: 0x001AB952
		void Read(SerializationReader Reader)
		{
		}
	}
}
