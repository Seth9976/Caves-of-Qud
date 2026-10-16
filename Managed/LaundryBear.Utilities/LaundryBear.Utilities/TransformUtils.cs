using System;
using LaundryBear.Math;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000009 RID: 9
	public static class TransformUtils
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000240A File Offset: 0x0000060A
		public static void TransferLocalTransform(Transform source, Transform target)
		{
			target.localPosition = source.localPosition;
			target.localRotation = source.localRotation;
			target.localScale = source.localScale;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002430 File Offset: 0x00000630
		public static void TransferWorldTransform(Transform source, Transform target)
		{
			target.position = source.position;
			target.rotation = source.rotation;
			if (target.parent != null)
			{
				target.localScale = VectorUtils.MemberwiseDivide(source.lossyScale, target.parent.lossyScale);
				return;
			}
			target.localScale = source.lossyScale;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000248C File Offset: 0x0000068C
		public static void CopyLocalValuesTo(this Transform source, Transform target)
		{
			TransformUtils.TransferLocalTransform(source, target);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002495 File Offset: 0x00000695
		public static void CopyLocalValuesFrom(this Transform target, Transform source)
		{
			TransformUtils.TransferLocalTransform(source, target);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000249E File Offset: 0x0000069E
		public static void CopyWorldValuesTo(this Transform source, Transform target)
		{
			TransformUtils.TransferWorldTransform(source, target);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024A7 File Offset: 0x000006A7
		public static void CopyWorldValuesFrom(this Transform target, Transform source)
		{
			TransformUtils.TransferWorldTransform(source, target);
		}
	}
}
