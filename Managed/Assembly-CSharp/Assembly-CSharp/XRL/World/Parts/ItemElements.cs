using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001057 RID: 4183
	[Serializable]
	public class ItemElements : IActivePart
	{
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600A68F RID: 42639 RVA: 0x003CECD3 File Offset: 0x003CCED3
		// (set) Token: 0x0600A68E RID: 42638 RVA: 0x003CECC3 File Offset: 0x003CCEC3
		public string Elements
		{
			get
			{
				return this._Elements;
			}
			set
			{
				this._ElementMap = null;
				this._Elements = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600A690 RID: 42640 RVA: 0x003CECDB File Offset: 0x003CCEDB
		public Dictionary<string, int> ElementMap
		{
			get
			{
				if (this._ElementMap == null && !this._Elements.IsNullOrEmpty())
				{
					this._ElementMap = this._Elements.CachedNumericDictionaryExpansion();
				}
				return this._ElementMap;
			}
		}

		// Token: 0x0600A691 RID: 42641 RVA: 0x003CED09 File Offset: 0x003CCF09
		public override bool SameAs(IPart Part)
		{
			return !((Part as ItemElements)._Elements != this._Elements) && base.SameAs(Part);
		}

		// Token: 0x0600A692 RID: 42642 RVA: 0x003CED2C File Offset: 0x003CCF2C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600A693 RID: 42643 RVA: 0x003CED44 File Offset: 0x003CCF44
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (this.ElementMap != null && E.IsRelevantObject(this.ParentObject))
			{
				foreach (KeyValuePair<string, int> keyValuePair in this.ElementMap)
				{
					E.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040044CB RID: 17611
		public string _Elements;

		// Token: 0x040044CC RID: 17612
		[NonSerialized]
		private Dictionary<string, int> _ElementMap;
	}
}
