using UnityEngine;

public class SinusodialModify : MonoBehaviour
{
	public float speed;
	public float theta = 0.0f;
	public float topNumerical;
	public float bottomNumerical;

	public SinusodialModify(float _speed,float _topNumerical,float _bottomNumerical)
    {
		speed = _speed;
		topNumerical = _topNumerical;
		bottomNumerical = _bottomNumerical;
    }

	//Change numerical in sine wave shape
	public float ModifiedNumerical()
    {
		theta += Time.deltaTime * speed;
		if (theta >= 2 * Mathf.PI)
			theta = 0.0f;

		float usableNumerical = (Mathf.Sin(theta) + 1.0f) / 2.0f;
		usableNumerical= Mathf.Lerp(bottomNumerical, topNumerical, usableNumerical);

		return usableNumerical;
	}
}
