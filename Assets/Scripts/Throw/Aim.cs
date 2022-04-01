using UnityEngine;

public class Aim : MonoBehaviour
{
    //User Grades
	[SerializeField] private float speed;
	[SerializeField] private float minZ; 
	[SerializeField] private float maxZ;

    private bool canMove = true;

	private SinusodialModify sinusodialModify;

    private void OnEnable()
    {
        ThrowController.onThrowed += StopMoving;
        Bin.onFilledBin += SetClosing;
    }

    private void OnDisable()
    {
        ThrowController.onThrowed -= StopMoving;
        Bin.onFilledBin -= SetClosing;
    }

    private void Start()
    {
		sinusodialModify = new SinusodialModify(speed, maxZ, minZ);
    }

    private void Update()
    {
        if (canMove)
			Patrol();
	}

    //Modified coordinate on one axis in time changing
	private void Patrol()
    {
		transform.localPosition = new Vector3(0, 0.4f, sinusodialModify.ModifiedNumerical());
	}

    private void StopMoving()
    {
        canMove = false;
    }

    private void SetClosing() 
    {
        gameObject.SetActive(false);
    }
}
