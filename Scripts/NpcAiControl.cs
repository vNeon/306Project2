using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To control the movement of npc character. Npc would choose a random target and calculate the best path towards the target.
//If the player is within the range of npc, the npc stops and could be interact with player.
//attach to npc that is walking around i.e. teacher, hiker
namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof (NavMeshAgent))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class NpcAiControl : MonoBehaviour {

		public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; } // the character we are controlling
		// target to aim for
		public Transform[] targets;

		//target number
		private int counter;

		private GameObject player;
		private void Start()
		{
			// get the components on the object we need ( should not be null due to require component so no need to check )
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();
			player = GameObject.FindGameObjectWithTag ("Player");

			agent.updateRotation = false;
			agent.updatePosition = true;


			agent.autoBraking = true;
		}


		private void Update()
		{
			//set target
			if (targets [counter] != null) {
				agent.SetDestination(targets[counter].position);
			}

			//Check if player is within the range of npc
			if (Vector3.Distance (player.transform.position, character.transform.position) < 2) {
				agent.SetDestination (character.transform.position);
				character.Move (Vector3.zero, false, false);

				//The npc always face the character.
				Quaternion rotation = Quaternion.LookRotation (player.transform.position - character.transform.position, Vector3.up);
				character.transform.rotation = Quaternion.Slerp (character.transform.rotation, rotation, Time.deltaTime * 2);
				//this.GetComponent<TalkWithNPC> ().isStop = true;
			} else {
				//Npc finds the best path. If the target is reached, randomly choose another target.
				if (agent.remainingDistance > agent.stoppingDistance) {
					character.Move (agent.desiredVelocity, false, false);
				} else {
					GotoNextPoint ();

					character.Move (Vector3.zero, false, false);
					//character.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				}
				//this.GetComponent<TalkWithNPC> ().isStop = false;
			}   	

		}


		//Randomly choose a target point.
		void GotoNextPoint(){
			if (targets.Length == 0) {
				return;
			}
			agent.destination = targets [counter].position;
			counter = UnityEngine.Random.Range (0, targets.Length);

		}
	}
}