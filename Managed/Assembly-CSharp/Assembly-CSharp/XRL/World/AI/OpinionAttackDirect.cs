using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B3E RID: 2878
	[GenerateSerializationPartial]
	public class OpinionAttackDirect : IOpinionCombat
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600743F RID: 29759 RVA: 0x002BE11B File Offset: 0x002BC31B
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x002BE11E File Offset: 0x002BC31E
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x002BE138 File Offset: 0x002BC338
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06007442 RID: 29762 RVA: 0x002BE152 File Offset: 0x002BC352
		public override int BaseValue
		{
			get
			{
				return -75;
			}
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x002BE156 File Offset: 0x002BC356
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x002BE158 File Offset: 0x002BC358
		public override string GetText(GameObject Actor)
		{
			return "Leader directed me to attack.";
		}
	}
}
