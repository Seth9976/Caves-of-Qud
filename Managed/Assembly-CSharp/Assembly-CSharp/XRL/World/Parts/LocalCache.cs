using System;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02001301 RID: 4865
	[Serializable]
	public class LocalCache : IPart
	{
		// Token: 0x0600BEAA RID: 48810 RVA: 0x0045D070 File Offset: 0x0045B270
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Rack<LocalCache.Entry> values = this.Values;
			LocalCache.Entry[] array = ((values != null) ? values.GetArray() : null);
			Rack<LocalCache.Entry> values2 = this.Values;
			int num = ((values2 != null) ? values2.Count : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				array[i].Write(Writer);
			}
		}

		// Token: 0x0600BEAB RID: 48811 RVA: 0x0045D0C4 File Offset: 0x0045B2C4
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.Values = new Rack<LocalCache.Entry>(num);
				for (int i = 0; i < num; i++)
				{
					LocalCache.Entry entry = default(LocalCache.Entry);
					entry.Read(Reader);
					this.Values.Add(entry);
				}
			}
		}

		// Token: 0x04004CC0 RID: 19648
		[NonSerialized]
		private Rack<LocalCache.Entry> Values;

		// Token: 0x02001D2A RID: 7466
		private struct Entry
		{
			// Token: 0x0600F364 RID: 62308 RVA: 0x0054FE00 File Offset: 0x0054E000
			public void Write(SerializationWriter Writer)
			{
				Writer.WriteGameObject(this.Object);
				Writer.WriteOptimized(this.X);
				Writer.WriteOptimized(this.Y);
			}

			// Token: 0x0600F365 RID: 62309 RVA: 0x0054FE26 File Offset: 0x0054E026
			public void Read(SerializationReader Reader)
			{
				this.Object = Reader.ReadGameObject(null);
				this.X = Reader.ReadOptimizedInt32();
				this.Y = Reader.ReadOptimizedInt32();
			}

			// Token: 0x04007203 RID: 29187
			public GameObject Object;

			// Token: 0x04007204 RID: 29188
			public int X;

			// Token: 0x04007205 RID: 29189
			public int Y;
		}
	}
}
