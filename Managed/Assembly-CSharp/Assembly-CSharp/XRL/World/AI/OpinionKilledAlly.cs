using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B49 RID: 2889
	[GenerateSerializationPartial]
	public class OpinionKilledAlly : IOpinionCombat
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06007486 RID: 29830 RVA: 0x002BE4FE File Offset: 0x002BC6FE
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x002BE501 File Offset: 0x002BC701
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Ally);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x002BE527 File Offset: 0x002BC727
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Ally = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06007489 RID: 29833 RVA: 0x002BE54D File Offset: 0x002BC74D
		public override int BaseValue
		{
			get
			{
				return -200;
			}
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x002BE554 File Offset: 0x002BC754
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			this.Ally = ((Object != null) ? Object.BaseDisplayNameStripped : null);
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x002BE568 File Offset: 0x002BC768
		public override string GetText(GameObject Actor)
		{
			return "Killed " + this.Ally + ".";
		}

		// Token: 0x04003589 RID: 13705
		public string Ally;
	}
}
