using UnityEngine;

public class BuildingTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Building; } }

	public override int RestAmount { get { return 30; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
		if (!MapManager.inst.tileInfos[pos].isVisited)
		{
			float rand = Random.Range(0, 100);
			if (rand < 10)
				UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound"));
		}

	}

	public override void OnExplored()
	{
		base.OnExplored();
		float rand = Random.Range(0, 100);
		if (rand < 5)
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound"));
	}
}
