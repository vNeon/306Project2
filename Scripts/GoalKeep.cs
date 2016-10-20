using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To control the movement of npc character. Npc would keep the goal and attempt to shoot the ball to the goal
//attach to goal keep npc
namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof (NavMeshAgent))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class GoalKeep : MonoBehaviour {

		public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; } // the character we are controlling


		public GameObject ball;
		public GameObject goal;

		private Vector3 originalPos;

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

			originalPos = this.gameObject.transform.position;
		}


		private void Update()
		{
			//set target
			if (Vector3.Distance (character.transform.position, ball.transform.position) < 10 && Vector3.Distance(character.transform.position,originalPos)<10) {
				agent.SetDestination (ball.transform.position);


			} else {
				agent.SetDestination (originalPos);
			}


			if (agent.remainingDistance > agent.stoppingDistance) {
				character.Move (agent.desiredVelocity, false, false);

			}

			//Kick the ball
			if (Vector3.Distance (character.transform.position, ball.transform.position) < 1f) {
				Vector3 direction = goal.transform.position - ball.transform.position;
				ball.GetComponent<Rigidbody> ().AddForce (direction * 2);
			}

		}


	}
}

