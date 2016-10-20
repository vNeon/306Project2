using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To control the movement of npc character. Npc would follows the ball and attempt to shoot the ball to the goal
//attach to football player npc
namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof (NavMeshAgent))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class PlayFootball : MonoBehaviour {

		public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; } // the character we are controlling

		public float kickDistance = 1.5f;
		public GameObject ball;
		public GameObject[] goals;

		private Vector3 direction;
		private int random;
		private GameObject goal;

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

			if (Vector3.Distance (character.transform.position, ball.transform.position) < kickDistance) {
				random = UnityEngine.Random.Range(0,goals.Length);
				goal = goals [random];
				agent.SetDestination (goal.transform.position);
				direction = goal.transform.position - ball.transform.position;
				if (Vector3.Distance (ball.transform.position, goal.transform.position) < 30) {
					ball.GetComponent<Rigidbody> ().AddForce (direction * 8);
					//Debug.Log ("Smaller than 30 and "+Vector3.Distance (ball.transform.position, goal.transform.position));
				} else if(Vector3.Distance (ball.transform.position, goal.transform.position) >= 30){
					ball.GetComponent<Rigidbody> ().AddForce (direction * 1);
					//Debug.Log ("Bigger  than 30 and "+Vector3.Distance (ball.transform.position, goal.transform.position));

				}


			} else {
				agent.SetDestination (ball.transform.position);
			}


			if (agent.remainingDistance > agent.stoppingDistance) {
				character.Move (agent.desiredVelocity, false, false);

			} 

		}
			

	}
}
