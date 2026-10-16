using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public abstract class exPlane : MonoBehaviour
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0002E203 File Offset: 0x0002C403
	// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0002E20B File Offset: 0x0002C40B
	public virtual float width
	{
		get
		{
			return this.width_;
		}
		set
		{
			this.width_ = value;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060006AA RID: 1706 RVA: 0x0002E214 File Offset: 0x0002C414
	// (set) Token: 0x060006AB RID: 1707 RVA: 0x0002E21C File Offset: 0x0002C41C
	public virtual float height
	{
		get
		{
			return this.height_;
		}
		set
		{
			this.height_ = value;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060006AC RID: 1708 RVA: 0x0002E225 File Offset: 0x0002C425
	// (set) Token: 0x060006AD RID: 1709 RVA: 0x0002E22D File Offset: 0x0002C42D
	public virtual Anchor anchor
	{
		get
		{
			return this.anchor_;
		}
		set
		{
			this.anchor_ = value;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060006AE RID: 1710 RVA: 0x0002E236 File Offset: 0x0002C436
	// (set) Token: 0x060006AF RID: 1711 RVA: 0x0002E23E File Offset: 0x0002C43E
	public virtual Vector2 offset
	{
		get
		{
			return this.offset_;
		}
		set
		{
			this.offset_ = value;
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0002E248 File Offset: 0x0002C448
	public bool hasSprite
	{
		get
		{
			exSpriteBase component = base.GetComponent<exSpriteBase>();
			return component != null && component != this;
		}
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0002E271 File Offset: 0x0002C471
	public Rect GetWorldAABoundingRect()
	{
		return exGeometryUtility.GetAABoundingRect(this.GetWorldVertices());
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0002E27E File Offset: 0x0002C47E
	public Rect GetLocalAABoundingRect()
	{
		return exGeometryUtility.GetAABoundingRect(this.GetLocalVertices());
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0002E28C File Offset: 0x0002C48C
	public Vector3[] GetRectVertices(Space _space)
	{
		float num = this.height_ * 0.5f;
		float num2 = this.width_ * 0.5f;
		Vector2 vector;
		switch (this.anchor_)
		{
		case Anchor.TopLeft:
			vector.x = num2;
			vector.y = -num;
			break;
		case Anchor.TopCenter:
			vector.x = 0f;
			vector.y = -num;
			break;
		case Anchor.TopRight:
			vector.x = -num2;
			vector.y = -num;
			break;
		case Anchor.MidLeft:
			vector.x = num2;
			vector.y = 0f;
			break;
		case Anchor.MidCenter:
			vector.x = 0f;
			vector.y = 0f;
			break;
		case Anchor.MidRight:
			vector.x = -num2;
			vector.y = 0f;
			break;
		case Anchor.BotLeft:
			vector.x = num2;
			vector.y = num;
			break;
		case Anchor.BotCenter:
			vector.x = 0f;
			vector.y = num;
			break;
		case Anchor.BotRight:
			vector.x = -num2;
			vector.y = num;
			break;
		default:
			vector.x = 0f;
			vector.y = 0f;
			break;
		}
		vector.x += this.offset_.x;
		vector.y += this.offset_.y;
		Vector3 vector2 = new Vector3(-num2 + vector.x, -num + vector.y, 0f);
		Vector3 vector3 = new Vector3(-num2 + vector.x, num + vector.y, 0f);
		Vector3 vector4 = new Vector3(num2 + vector.x, num + vector.y, 0f);
		Vector3 vector5 = new Vector3(num2 + vector.x, -num + vector.y, 0f);
		if (_space == Space.World)
		{
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			vector2 = localToWorldMatrix.MultiplyPoint3x4(vector2);
			vector3 = localToWorldMatrix.MultiplyPoint3x4(vector3);
			vector4 = localToWorldMatrix.MultiplyPoint3x4(vector4);
			vector5 = localToWorldMatrix.MultiplyPoint3x4(vector5);
		}
		return new Vector3[] { vector2, vector3, vector4, vector5 };
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0002E4D2 File Offset: 0x0002C6D2
	protected virtual Vector3[] GetVertices(Space _space)
	{
		return this.GetRectVertices(_space);
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0002E4DB File Offset: 0x0002C6DB
	public virtual Vector3[] GetLocalVertices()
	{
		return this.GetVertices(Space.Self);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
	public virtual Vector3[] GetWorldVertices()
	{
		return this.GetVertices(Space.World);
	}

	// Token: 0x040004DF RID: 1247
	[SerializeField]
	protected float width_ = 1f;

	// Token: 0x040004E0 RID: 1248
	[SerializeField]
	protected float height_ = 1f;

	// Token: 0x040004E1 RID: 1249
	[SerializeField]
	protected Anchor anchor_ = Anchor.MidCenter;

	// Token: 0x040004E2 RID: 1250
	[SerializeField]
	protected Vector2 offset_ = Vector2.zero;
}
