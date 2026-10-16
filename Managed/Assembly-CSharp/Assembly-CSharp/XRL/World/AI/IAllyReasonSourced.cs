using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B3B RID: 2875
	[GenerateSerializationPartial]
	public abstract class IAllyReasonSourced : IAllyReason
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600742C RID: 29740 RVA: 0x002BDED8 File Offset: 0x002BC0D8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x002BDEDB File Offset: 0x002BC0DB
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x002BDEF5 File Offset: 0x002BC0F5
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Name = Reader.ReadOptimizedString();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x002BDF10 File Offset: 0x002BC110
		public override void Initialize(GameObject Actor, GameObject Source, AllegianceSet Set)
		{
			this.Name = ((Source != null) ? Source.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, false, null, false, null, false) : null);
		}

		// Token: 0x04003583 RID: 13699
		public string Name;
	}
}
