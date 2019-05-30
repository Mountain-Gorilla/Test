using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationDirector : MonoBehaviour
{
	// 本体
	[SerializeField]
	GameObject g_Transformation = default;
	Animator an_Change;
	SpriteChange sc_Script;

	// Start is called before the first frame update
	void Start()
    {
		an_Change = g_Transformation.GetComponent<Animator>();
		sc_Script = g_Transformation.GetComponent<SpriteChange>();
		g_Transformation.SetActive(false);	
	}

	public void StartTransform(int _sprite_num)
	{
		g_Transformation.SetActive(true);
		an_Change.Play("Attack");
		sc_Script.SharingState(_sprite_num);
	}

	public bool IsEndAnimation(Vector3 _position)
	{
		transform.position = _position;

		if (an_Change.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
			g_Transformation.SetActive(false);
			return true;
		}
		return false;
	}
   
}
