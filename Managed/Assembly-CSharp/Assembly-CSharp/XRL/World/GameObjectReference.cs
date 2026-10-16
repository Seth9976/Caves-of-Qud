using System;

namespace XRL.World
{
	// Token: 0x02000950 RID: 2384
	[Serializable]
	public class GameObjectReference : IComposite
	{
		// Token: 0x060061BE RID: 25022 RVA: 0x001E0273 File Offset: 0x001DE473
		public GameObjectReference()
		{
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x001E027B File Offset: 0x001DE47B
		public GameObjectReference(GameObject Object)
		{
			this.Set(Object);
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x001E028A File Offset: 0x001DE48A
		public void Set(GameObject Object)
		{
			this.ID = ((Object != null) ? Object.BaseID : 0);
			this.Object = Object;
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x001E02A5 File Offset: 0x001DE4A5
		public void Clear()
		{
			this.ID = 0;
			this.Object = null;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x001E02B5 File Offset: 0x001DE4B5
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x001E02B8 File Offset: 0x001DE4B8
		public void Read(SerializationReader Reader)
		{
			this.ID = Reader.ReadOptimizedInt32();
			this.Object = Reader.ReadGameObject(null);
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x001E02D3 File Offset: 0x001DE4D3
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.ID);
			Writer.WriteGameObject(this.Object, true);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x001E02EE File Offset: 0x001DE4EE
		public static void Free(ref GameObjectReference Reference)
		{
			if (Reference != null)
			{
				Reference.ID = 0;
				Reference.Object = null;
				Reference = null;
			}
		}

		// Token: 0x04002E00 RID: 11776
		public int ID;

		// Token: 0x04002E01 RID: 11777
		public GameObject Object;
	}
}
