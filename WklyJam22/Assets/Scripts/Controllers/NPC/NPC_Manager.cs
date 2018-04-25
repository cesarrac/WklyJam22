using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCTYpe {Blacksmith, Trader, Recruiter, Salvager}
public class NPC_Manager : MonoBehaviour {

	public static NPC_Manager instance {get; protected set;}
	int[] spawnPositions;
	float y = 7;
	List<GameObject>active_npcs;
	ObjectPool pool;
	NPCPrototype[] npcPrototypes;
	//public NPCProfession[] npcProfessions;
	void Awake(){
		instance = this;
		active_npcs = new List<GameObject>();
	}
	public void SpawnNPCs(){
		pool = ObjectPool.instance;
		active_npcs = new List<GameObject>();
		spawnPositions = SpawnMap_Manager.instance.GetSpawnPositionsOf(SpawnType.NPC);
		npcPrototypes = Resources.LoadAll<NPCPrototype>("ScriptObjects/NPC Prototypes/");
		//npcProfessions = Resources.LoadAll<NPCProfession>("ScriptObjects/NPC Prototypes/");
	/* 	foreach(int x in spawnPositions){
			NPCPrototype prototype = npcPrototypes[1];
			GameObject npc = pool.GetObjectForType("NPC_"+ prototype.npcType.ToString(), true, new Vector2(x, y));
			npc.GetComponent<NPC_Controller>().Init(prototype);
			active_npcs.Add(npc);
		} */
	
		GameObject npc = pool.GetObjectForType("NPC", true, new Vector2(spawnPositions[0], y));
		npc.GetComponent<NPC_Controller>().Init(npcPrototypes[0], npcPrototypes[0].profession);
		active_npcs.Add(npc);

	
		GameObject npc2 = pool.GetObjectForType("NPC", true, new Vector2(spawnPositions[1], y));
		npc2.GetComponent<NPC_Controller>().Init(npcPrototypes[1], npcPrototypes[1].profession);
		active_npcs.Add(npc2);


		GameObject npc3 = pool.GetObjectForType("NPC", true, new Vector2(spawnPositions[2], y));
		npc3.GetComponent<NPC_Controller>().Init(npcPrototypes[2], npcPrototypes[2].profession);
		active_npcs.Add(npc3);
	}

}
