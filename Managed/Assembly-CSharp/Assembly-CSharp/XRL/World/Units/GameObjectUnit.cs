using System;

namespace XRL.World.Units
{
	// Token: 0x020009B7 RID: 2487
	[Serializable]
	public abstract class GameObjectUnit
	{
		// Token: 0x06006B0E RID: 27406 RVA: 0x00227A1E File Offset: 0x00225C1E
		public virtual void Initialize(GameObject Object)
		{
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x00227A20 File Offset: 0x00225C20
		public virtual void Apply(GameObject Object)
		{
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x00227A22 File Offset: 0x00225C22
		public virtual void Remove(GameObject Object)
		{
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x00227A24 File Offset: 0x00225C24
		public virtual void Reset()
		{
		}

		/// <summary>Whether the unit description should be written as rules text in the object description.</summary>
		// Token: 0x06006B12 RID: 27410 RVA: 0x00227A26 File Offset: 0x00225C26
		public virtual bool CanInscribe()
		{
			return true;
		}

		/// <summary>Get description of unit effects.</summary>
		/// <param name="Inscription">Returned text will be used as inscription.</param>
		// Token: 0x06006B13 RID: 27411 RVA: 0x00227A29 File Offset: 0x00225C29
		public virtual string GetDescription(bool Inscription = false)
		{
			return "";
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x00227A30 File Offset: 0x00225C30
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
