using System;
using XRL.Collections;

namespace XRL.World.AI
{
	// Token: 0x02000B6F RID: 2927
	public class OpinionList : Container<IOpinion>, IComposite
	{
		// Token: 0x17000976 RID: 2422
		public IOpinion this[int Index]
		{
			get
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Items[Index];
			}
			set
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Items[Index] = value;
				this.Variant++;
			}
		}

		// Token: 0x06007554 RID: 30036 RVA: 0x002BFB64 File Offset: 0x002BDD64
		public void RefreshTotal()
		{
			this.Total = 0;
			for (int i = 0; i < this.Length; i++)
			{
				this.Total += this.Items[i].Value;
			}
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x002BFBA4 File Offset: 0x002BDDA4
		public virtual void Add(IOpinion Item)
		{
			if (this.Length == this.Size)
			{
				base.Resize(this.Length * 2);
			}
			IOpinion[] items = this.Items;
			int length = this.Length;
			this.Length = length + 1;
			items[length] = Item;
			this.Total += Item.Value;
			this.Variant++;
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x002BFC08 File Offset: 0x002BDE08
		public bool Remove(IOpinion Item)
		{
			int num = Array.IndexOf<IOpinion>(this.Items, Item, 0, this.Length);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06007557 RID: 30039 RVA: 0x002BFC38 File Offset: 0x002BDE38
		public void RemoveAt(int Index)
		{
			if (Index >= this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Length--;
			this.Total -= this.Items[Index].Value;
			if (Index < this.Length)
			{
				Array.Copy(this.Items, Index + 1, this.Items, Index, this.Length - Index);
			}
			this.Items[this.Length] = null;
			this.Variant++;
		}

		// Token: 0x06007558 RID: 30040 RVA: 0x002BFCC0 File Offset: 0x002BDEC0
		public IOpinion Find(Predicate<IOpinion> Predicate)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (Predicate(this.Items[i]))
				{
					return this.Items[i];
				}
			}
			return null;
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x002BFCF8 File Offset: 0x002BDEF8
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				Writer.Write(this.Items[i]);
			}
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x002BFD30 File Offset: 0x002BDF30
		public override void Read(SerializationReader Reader)
		{
			this.Size = (this.Length = Reader.ReadOptimizedInt32());
			this.Items = new IOpinion[this.Size];
			this.Total = 0;
			for (int i = 0; i < this.Length; i++)
			{
				this.Items[i] = (IOpinion)Reader.ReadComposite();
				this.Total += this.Items[i].Value;
			}
		}

		// Token: 0x0400359C RID: 13724
		public int Total;
	}
}
