using System;
using Unity.Properties;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D0 RID: 464
	internal class SetValueVisitor<TSrcValue> : PathVisitor
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00040556 File Offset: 0x0003E756
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0004055E File Offset: 0x0003E75E
		public ConverterGroup group { get; set; }

		// Token: 0x06000E4D RID: 3661 RVA: 0x00040567 File Offset: 0x0003E767
		public override void Reset()
		{
			base.Reset();
			this.Value = default(TSrcValue);
			this.group = null;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00040588 File Offset: 0x0003E788
		protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			bool isReadOnly = property.IsReadOnly;
			if (isReadOnly)
			{
				base.ReturnCode = VisitReturnCode.AccessViolation;
			}
			else
			{
				TValue tvalue;
				bool flag = this.group != null && this.group.TryConvert<TSrcValue, TValue>(ref this.Value, out tvalue);
				if (flag)
				{
					property.SetValue(ref container, tvalue);
				}
				else
				{
					TValue tvalue2;
					bool flag2 = ConverterGroups.TryConvert<TSrcValue, TValue>(ref this.Value, out tvalue2);
					if (flag2)
					{
						property.SetValue(ref container, tvalue2);
					}
					else
					{
						base.ReturnCode = VisitReturnCode.InvalidCast;
					}
				}
			}
		}

		// Token: 0x040008E4 RID: 2276
		public static readonly ObjectPool<SetValueVisitor<TSrcValue>> Pool = new ObjectPool<SetValueVisitor<TSrcValue>>(() => new SetValueVisitor<TSrcValue>(), delegate(SetValueVisitor<TSrcValue> v)
		{
			v.Reset();
		}, null, null, true, 10, 10000);

		// Token: 0x040008E5 RID: 2277
		public TSrcValue Value;
	}
}
