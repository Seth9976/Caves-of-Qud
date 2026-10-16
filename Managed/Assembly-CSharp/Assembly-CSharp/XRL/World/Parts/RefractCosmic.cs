using System;
using Genkit;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02001210 RID: 4624
	[Serializable]
	public class RefractCosmic : IPart
	{
		// Token: 0x0600B7D9 RID: 47065 RVA: 0x0042E494 File Offset: 0x0042C694
		public ReadOnlySpan<float> GetDirections(Location2D Source, int Amount = 3)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			uint num = Hash.FNV1A32(Source.X | (Source.Y << 16), 2166136261U);
			num = Hash.FNV1A32(currentCell.X | (currentCell.Y << 16), num);
			Random random = new Random((int)num);
			float[] array = this.Directions.GetArray(Amount);
			for (int i = 0; i < Amount; i++)
			{
				array[i] = (float)(random.NextDouble() * 360.0);
			}
			return array.AsSpan<float>(0, Amount);
		}

		// Token: 0x040049FD RID: 18941
		[NonSerialized]
		private Rack<float> Directions = new Rack<float>();
	}
}
