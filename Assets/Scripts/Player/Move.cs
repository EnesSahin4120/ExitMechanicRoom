using UnityEngine;

public class Move : MonoBehaviour
{
	private bool canMove;
	private int currentTargetIndex = 0;

	//User Grades
	[SerializeField] private float moveSpeed;
	[SerializeField] private float accuracy;
	[SerializeField] private float rotSpeed;

	[SerializeField] private Waypoints waypoints;

	private void OnEnable()
    {
		DoorController.onOpenedDoor += StartMoving;
    }

	private void OnDisable()
    {
		DoorController.onOpenedDoor -= StartMoving;
	}

	void LateUpdate()
	{
        if (canMove){
			if (currentTargetIndex != waypoints.followPoints.Length - 1){

				//Calculate distance between player and point -> Plus current point index
				if (Vector3.Distance(transform.position, waypoints.followPoints[currentTargetIndex].position) < accuracy){
					currentTargetIndex++;
				}
			}
			MoveForward();
			Turn_to_Target();
		}
	}

	private void Turn_to_Target()
    {
		Vector3 lookAtGoal = new Vector3(waypoints.followPoints[currentTargetIndex].position.x,
									     transform.position.y,
									     waypoints.followPoints[currentTargetIndex].position.z);
		
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation,
												   Quaternion.LookRotation(direction),
												   Time.deltaTime * rotSpeed);
	}

	private void MoveForward() 
    {
		transform.Translate(0, 0, moveSpeed * Time.deltaTime);
	}

	private void StartMoving() 
    {
		canMove = true;
    }
}
