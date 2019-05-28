using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconGroundJudge : MonoBehaviour
{
	private bool b_GroundFlag = false;

	public void DestroyFlag() { b_GroundFlag = false; }

	public bool IsGround() { return b_GroundFlag; }

	void OnTriggerEnter2D(Collider2D _collider)
	{
		if (_collider.gameObject.tag == "TileMap") {
			b_GroundFlag = true;
		}
	}
}
