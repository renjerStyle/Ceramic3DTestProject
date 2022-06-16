using UnityEngine;

public class Quad : MonoBehaviour
{
	public Controller contr;
	public Vector3 pos;
	public Vector3 rot;

    private void Awake()
	{
		pos = transform.localPosition;
		rot = transform.localEulerAngles;
		if (name.Contains("_"))
		{
			if (name.Contains("_neg"))
			{
				Destroy(gameObject);
			}
			else
			{
				contr = FindObjectOfType<Controller>();
				contr.filter = GetComponent<MeshFilter>();
				contr.pointStart = transform;
				name = name.Replace("_pos", "");
			}
		}
		else
		{
			if ((transform.localPosition != pos || rot != transform.localEulerAngles) && contr != null)
			{
				contr.enabled = true;
			}
		}
	}

    private void Update()
    {
		//if (name.Contains("_"))
		//{
			if (name.Contains("_neg"))
			{
				Destroy(gameObject);
			}
			else
			{
				contr = FindObjectOfType<Controller>();
				contr.filter = GetComponent<MeshFilter>();
				contr.pointStart = transform;
				name = name.Replace("_pos", "");
			}
		//}
		//else {
		if ((transform.localPosition != pos || rot != transform.localEulerAngles) && contr != null)
		{
			contr.enabled = true;
		}
		else
		{
			contr.area.text = (CreaterPlane.AreaOfMesh(contr.filter.sharedMesh)/100).ToString() + " м^2";
		}
		//}
	}
}
