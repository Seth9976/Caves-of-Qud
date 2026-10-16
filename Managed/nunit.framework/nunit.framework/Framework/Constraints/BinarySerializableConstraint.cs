using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000ED RID: 237
	public class BinarySerializableConstraint : Constraint
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00011CFD File Offset: 0x0000FEFD
		public override string Description
		{
			get
			{
				return "binary serializable";
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00011D04 File Offset: 0x0000FF04
		public override ConstraintResult ApplyTo(object actual)
		{
			if (actual == null)
			{
				throw new ArgumentNullException("actual");
			}
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			try
			{
				this.serializer.Serialize(memoryStream, actual);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				flag = this.serializer.Deserialize(memoryStream) != null;
			}
			catch (SerializationException)
			{
			}
			return new ConstraintResult(this, actual.GetType(), flag);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00011D70 File Offset: 0x0000FF70
		protected override string GetStringRepresentation()
		{
			return "<binaryserializable>";
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00011D77 File Offset: 0x0000FF77
		public BinarySerializableConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x0400019A RID: 410
		private readonly BinaryFormatter serializer = new BinaryFormatter();
	}
}
