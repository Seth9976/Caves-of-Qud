using System;
using System.Collections.Generic;

namespace XRL.World.AI
{
	// Token: 0x02000B6D RID: 2925
	public sealed class PartyCollection : Dictionary<int, PartyMember>, IComposite
	{
		// Token: 0x06007544 RID: 30020 RVA: 0x002BF7CC File Offset: 0x002BD9CC
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			base.EnsureCapacity(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadOptimizedInt32();
				GameObjectReference gameObjectReference = Reader.ReadGameObjectReference();
				int num3 = Reader.ReadOptimizedInt32();
				base.Add(num2, new PartyMember(gameObjectReference, num3));
			}
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x002BF81C File Offset: 0x002BDA1C
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(base.Count);
			foreach (KeyValuePair<int, PartyMember> keyValuePair in this)
			{
				int num;
				PartyMember partyMember;
				keyValuePair.Deconstruct(out num, out partyMember);
				int num2 = num;
				PartyMember partyMember2 = partyMember;
				Writer.WriteOptimized(num2);
				Writer.Write(partyMember2.Reference);
				Writer.WriteOptimized(partyMember2.Flags);
			}
		}

		// Token: 0x17000973 RID: 2419
		public int this[GameObject Object]
		{
			get
			{
				return base[Object.BaseID].Flags;
			}
			set
			{
				PartyMember partyMember;
				if (base.TryGetValue(Object.BaseID, out partyMember))
				{
					partyMember.Flags = value;
					base[Object.BaseID] = partyMember;
					return;
				}
				this.Add(Object, value);
			}
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x002BF8EF File Offset: 0x002BDAEF
		public void Add(GameObject Object, int Flags)
		{
			base.Add(Object.BaseID, new PartyMember(Object.Reference(), Flags));
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x002BF909 File Offset: 0x002BDB09
		public bool TryAdd(GameObject Object)
		{
			return this.TryAdd(Object, 0);
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x002BF913 File Offset: 0x002BDB13
		public bool TryAdd(GameObject Object, int Flags)
		{
			if (base.ContainsKey(Object.BaseID))
			{
				return false;
			}
			this.Add(Object, Flags);
			return true;
		}
	}
}
