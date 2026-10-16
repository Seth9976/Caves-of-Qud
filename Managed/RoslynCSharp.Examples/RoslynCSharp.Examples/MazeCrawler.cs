using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000015 RID: 21
	public abstract class MazeCrawler : MonoBehaviour
	{
		// Token: 0x06000025 RID: 37
		public abstract MazeDirection DecideDirection(Vector2Int position, bool canMoveLeft, bool canMoveRight, bool canMoveUp, bool canMoveDown);

		// Token: 0x06000026 RID: 38 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void Awake()
		{
			this.startPosition = base.transform.position;
			this.currentPosition = base.transform.position;
			this.targetPosition = base.transform.position;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A14 File Offset: 0x00000C14
		public void Update()
		{
			if (this.HasArrived(this.targetPosition))
			{
				this.currentPosition = this.targetPosition;
				bool flag = this.CanMoveInDirection(MazeDirection.Left);
				bool flag2 = this.CanMoveInDirection(MazeDirection.Right);
				bool flag3 = this.CanMoveInDirection(MazeDirection.Up);
				bool flag4 = this.CanMoveInDirection(MazeDirection.Down);
				Vector2Int vector2Int = new Vector2Int(Mathf.RoundToInt(this.currentPosition.x), Mathf.RoundToInt(this.currentPosition.y));
				if (!this.visited.Contains(vector2Int))
				{
					this.visited.Add(vector2Int);
					this.DropBreadcrumb();
				}
				try
				{
					switch (this.DecideDirection(vector2Int, flag, flag2, flag3, flag4))
					{
					case MazeDirection.Up:
						if (!flag3)
						{
							throw new InvalidOperationException("Invalid decision: Cannot move up. Game will restart");
						}
						this.targetPosition = this.currentPosition + new Vector2(0f, 1f);
						base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
						break;
					case MazeDirection.Down:
						if (!flag4)
						{
							throw new InvalidOperationException("Invalid decision: Cannot move down. Game will restart");
						}
						this.targetPosition = this.currentPosition + new Vector2(0f, -1f);
						base.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
						break;
					case MazeDirection.Left:
						if (!flag)
						{
							throw new InvalidOperationException("Invalid decision: Cannot move left. Game will restart");
						}
						this.targetPosition = this.currentPosition + new Vector2(-1f, 0f);
						base.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
						break;
					case MazeDirection.Right:
						if (!flag2)
						{
							throw new InvalidOperationException("Invalid decision: Cannot move right. Game will restart");
						}
						this.targetPosition = this.currentPosition + new Vector2(1f, 0f);
						base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
						break;
					}
					return;
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					this.Restart();
					return;
				}
			}
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.targetPosition, this.moveSpeed * Time.deltaTime);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C78 File Offset: 0x00000E78
		public void Restart()
		{
			foreach (GameObject gameObject in this.droppedBreadcrumbs)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.droppedBreadcrumbs.Clear();
			this.visited.Clear();
			base.transform.position = this.startPosition;
			this.currentPosition = base.transform.position;
			this.targetPosition = base.transform.position;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D20 File Offset: 0x00000F20
		private bool CanMoveInDirection(MazeDirection direction)
		{
			float num = 0f;
			float num2 = 0f;
			if (direction == MazeDirection.Left)
			{
				num -= 1f;
			}
			if (direction == MazeDirection.Right)
			{
				num += 1f;
			}
			if (direction == MazeDirection.Up)
			{
				num2 += 1f;
			}
			if (direction == MazeDirection.Down)
			{
				num2 -= 1f;
			}
			Vector2 vector = new Vector2(num, num2);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, vector, 0.75f);
			if (raycastHit2D.collider != null)
			{
				if (raycastHit2D.collider.gameObject.name == "MazeWallFinish")
				{
					base.enabled = false;
					Debug.Log("Congratulations! Your crawler successfully escaped the maze");
					return false;
				}
				if (raycastHit2D.collider.gameObject.name == "MazeWall")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DEC File Offset: 0x00000FEC
		private bool HasArrived(Vector2 targetPosition)
		{
			return Vector2.Distance(targetPosition, base.transform.position) < 0.05f;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E0C File Offset: 0x0000100C
		private void DropBreadcrumb()
		{
			if (this.breadcrumbPrefab != null)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.breadcrumbPrefab, base.transform.position, Quaternion.identity);
				this.droppedBreadcrumbs.Add(gameObject);
			}
		}

		// Token: 0x04000024 RID: 36
		private List<GameObject> droppedBreadcrumbs = new List<GameObject>();

		// Token: 0x04000025 RID: 37
		private HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

		// Token: 0x04000026 RID: 38
		private Vector2 startPosition;

		// Token: 0x04000027 RID: 39
		private Vector2 currentPosition;

		// Token: 0x04000028 RID: 40
		private Vector2 targetPosition;

		// Token: 0x04000029 RID: 41
		public GameObject breadcrumbPrefab;

		// Token: 0x0400002A RID: 42
		public float moveSpeed = 2f;
	}
}
