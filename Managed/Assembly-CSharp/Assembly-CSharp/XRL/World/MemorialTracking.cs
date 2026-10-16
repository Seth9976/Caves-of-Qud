using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World
{
	// Token: 0x02000852 RID: 2130
	[GenerateSerializationPartial]
	[Serializable]
	public class MemorialTracking : IComposite
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x00195F96 File Offset: 0x00194196
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x00195F9C File Offset: 0x0019419C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.Faction);
			Writer.WriteOptimized(this.Eulogy);
			Writer.WriteOptimized(this.Reason);
			Writer.WriteOptimized(this.ThirdPersonReason);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.Blueprint);
			Writer.WriteOptimized(this.Location);
			Writer.WriteOptimized(this.Queued);
			Writer.WriteOptimized(this.Performed);
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00196024 File Offset: 0x00194224
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Name = Reader.ReadOptimizedString();
			this.Faction = Reader.ReadOptimizedString();
			this.Eulogy = Reader.ReadOptimizedString();
			this.Reason = Reader.ReadOptimizedString();
			this.ThirdPersonReason = Reader.ReadOptimizedString();
			this.ID = Reader.ReadOptimizedString();
			this.Blueprint = Reader.ReadOptimizedString();
			this.Location = Reader.ReadOptimizedString();
			this.Queued = Reader.ReadOptimizedInt64();
			this.Performed = Reader.ReadOptimizedInt64();
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x001960B4 File Offset: 0x001942B4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Name: ",
				this.Name,
				"\nFaction: ",
				this.Faction,
				"\nEulogy: ",
				this.Eulogy,
				"\nReason: ",
				this.Reason,
				"\nThirdPersonReason: ",
				this.ThirdPersonReason,
				"\nID: ",
				this.ID,
				"\nBlueprint: ",
				this.Blueprint,
				"\nLocation: ",
				this.Location,
				"\nQueued: ",
				this.Queued.ToString(),
				"\nPerformed: ",
				this.Performed.ToString()
			});
		}

		// Token: 0x04002ABB RID: 10939
		public string Name;

		// Token: 0x04002ABC RID: 10940
		public string Faction;

		// Token: 0x04002ABD RID: 10941
		public string Eulogy;

		// Token: 0x04002ABE RID: 10942
		public string Reason;

		// Token: 0x04002ABF RID: 10943
		public string ThirdPersonReason;

		// Token: 0x04002AC0 RID: 10944
		public string ID;

		// Token: 0x04002AC1 RID: 10945
		public string Blueprint;

		// Token: 0x04002AC2 RID: 10946
		public string Location;

		// Token: 0x04002AC3 RID: 10947
		public long Queued;

		// Token: 0x04002AC4 RID: 10948
		public long Performed;
	}
}
