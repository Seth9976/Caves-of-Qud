using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World
{
	// Token: 0x02000854 RID: 2132
	[GenerateSerializationPartial]
	[Serializable]
	public class WorshipTracking : IComposite
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06005528 RID: 21800 RVA: 0x00197B4C File Offset: 0x00195D4C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x00197B50 File Offset: 0x00195D50
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.Faction);
			Writer.Write(this.Devoted);
			Writer.WriteOptimized(this.Times);
			Writer.WriteOptimized(this.First);
			Writer.WriteOptimized(this.Last);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00197BA8 File Offset: 0x00195DA8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Name = Reader.ReadOptimizedString();
			this.Faction = Reader.ReadOptimizedString();
			this.Devoted = Reader.ReadBoolean();
			this.Times = Reader.ReadOptimizedInt32();
			this.First = Reader.ReadOptimizedInt64();
			this.Last = Reader.ReadOptimizedInt64();
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00197C08 File Offset: 0x00195E08
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Name: ",
				this.Name,
				"\nFaction: ",
				this.Faction,
				"\nDevoted: ",
				this.Devoted.ToString(),
				"\nTimes: ",
				this.Times.ToString(),
				"\nFirst: ",
				this.First.ToString(),
				"\nLast: ",
				this.Last.ToString()
			});
		}

		// Token: 0x04002AD0 RID: 10960
		public string Name;

		// Token: 0x04002AD1 RID: 10961
		public string Faction;

		// Token: 0x04002AD2 RID: 10962
		public bool Devoted;

		// Token: 0x04002AD3 RID: 10963
		public int Times;

		// Token: 0x04002AD4 RID: 10964
		public long First;

		// Token: 0x04002AD5 RID: 10965
		public long Last;
	}
}
