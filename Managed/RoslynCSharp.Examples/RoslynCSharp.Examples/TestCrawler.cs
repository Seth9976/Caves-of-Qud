using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000017 RID: 23
	internal class TestCrawler : MazeCrawler
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000032CC File Offset: 0x000014CC
		public override MazeDirection DecideDirection(Vector2Int position, bool canMoveLeft, bool canMoveRight, bool canMoveUp, bool canMoveDown)
		{
			this.available.Clear();
			if (canMoveLeft && !this.visited.Contains(position + new Vector2Int(-1, 0)))
			{
				this.available.Add(MazeDirection.Left);
			}
			if (canMoveRight && !this.visited.Contains(position + new Vector2Int(1, 0)))
			{
				this.available.Add(MazeDirection.Right);
			}
			if (canMoveUp && !this.visited.Contains(position + new Vector2Int(0, 1)))
			{
				this.available.Add(MazeDirection.Up);
			}
			if (canMoveDown && !this.visited.Contains(position + new Vector2Int(0, -1)))
			{
				this.available.Add(MazeDirection.Down);
			}
			bool flag = false;
			MazeDirection mazeDirection;
			if (this.available.Count == 0)
			{
				if (this.searchPath.Count <= 0)
				{
					base.enabled = false;
					throw new Exception("Mouse crawler could not find a solution to the maze. Giving up...");
				}
				mazeDirection = this.searchPath.Pop();
				if (mazeDirection == MazeDirection.Left)
				{
					mazeDirection = MazeDirection.Right;
				}
				else if (mazeDirection == MazeDirection.Right)
				{
					mazeDirection = MazeDirection.Left;
				}
				else if (mazeDirection == MazeDirection.Up)
				{
					mazeDirection = MazeDirection.Down;
				}
				else if (mazeDirection == MazeDirection.Down)
				{
					mazeDirection = MazeDirection.Up;
				}
				flag = true;
			}
			else
			{
				mazeDirection = this.available[global::UnityEngine.Random.Range(0, this.available.Count)];
			}
			if (!flag)
			{
				this.searchPath.Push(mazeDirection);
			}
			if (!this.visited.Contains(position))
			{
				this.visited.Add(position);
			}
			return mazeDirection;
		}

		// Token: 0x0400003D RID: 61
		private Stack<MazeDirection> searchPath = new Stack<MazeDirection>();

		// Token: 0x0400003E RID: 62
		private List<MazeDirection> available = new List<MazeDirection>();

		// Token: 0x0400003F RID: 63
		private HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
	}
}
