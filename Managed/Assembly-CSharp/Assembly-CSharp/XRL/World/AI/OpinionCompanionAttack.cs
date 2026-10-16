using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B42 RID: 2882
	[GenerateSerializationPartial]
	public class OpinionCompanionAttack : IOpinionCombat
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600745A RID: 29786 RVA: 0x002BE29C File Offset: 0x002BC49C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600745B RID: 29787 RVA: 0x002BE29F File Offset: 0x002BC49F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x002BE2B9 File Offset: 0x002BC4B9
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600745D RID: 29789 RVA: 0x002BE2D3 File Offset: 0x002BC4D3
		public override int BaseValue
		{
			get
			{
				return -75;
			}
		}

		// Token: 0x0600745E RID: 29790 RVA: 0x002BE2D7 File Offset: 0x002BC4D7
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		public override string GetText(GameObject Actor)
		{
			return "Companion attacked me.";
		}
	}
}
