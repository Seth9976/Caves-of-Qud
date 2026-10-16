using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F56 RID: 3926
	[Serializable]
	public class DisplayNameAdjectives : IPart
	{
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06009C9A RID: 40090 RVA: 0x0038C691 File Offset: 0x0038A891
		// (set) Token: 0x06009C99 RID: 40089 RVA: 0x0038C681 File Offset: 0x0038A881
		public string Adjectives
		{
			get
			{
				return this._Adjectives;
			}
			set
			{
				this._Adjectives = value;
				this._AdjectiveList = null;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06009C9B RID: 40091 RVA: 0x0038C69C File Offset: 0x0038A89C
		public List<string> AdjectiveList
		{
			get
			{
				if (this._AdjectiveList == null)
				{
					if (string.IsNullOrEmpty(this._Adjectives))
					{
						this._AdjectiveList = new List<string>();
					}
					else
					{
						this._AdjectiveList = new List<string>(this._Adjectives.Split(',', StringSplitOptions.None));
					}
				}
				return this._AdjectiveList;
			}
		}

		// Token: 0x06009C9C RID: 40092 RVA: 0x0038C6EA File Offset: 0x0038A8EA
		public void AddAdjective(string Adjective)
		{
			this.AdjectiveList.Add(Adjective);
			this._Adjectives = string.Join(",", this.AdjectiveList.ToArray());
		}

		// Token: 0x06009C9D RID: 40093 RVA: 0x0038C713 File Offset: 0x0038A913
		public void RequireAdjective(string Adjective)
		{
			if (!this.AdjectiveList.Contains(Adjective))
			{
				this.AddAdjective(Adjective);
			}
		}

		// Token: 0x06009C9E RID: 40094 RVA: 0x0038C72C File Offset: 0x0038A92C
		public void RemoveAdjective(string Adjective)
		{
			this.AdjectiveList.Remove(Adjective);
			if (this.AdjectiveList.Count == 0)
			{
				this.ParentObject.RemovePart(this);
				return;
			}
			this._Adjectives = string.Join(",", this.AdjectiveList.ToArray());
		}

		// Token: 0x06009C9F RID: 40095 RVA: 0x0038C77B File Offset: 0x0038A97B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009CA0 RID: 40096 RVA: 0x0038C794 File Offset: 0x0038A994
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			foreach (string text in this.AdjectiveList)
			{
				E.AddAdjective(text, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040C3 RID: 16579
		public string _Adjectives;

		// Token: 0x040040C4 RID: 16580
		[NonSerialized]
		private List<string> _AdjectiveList;
	}
}
