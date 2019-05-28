using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
	private enum Direct { Left = -1, Right = 1 };  // 向いている方向
	private int  n_LeftRightFlag = (int)Direct.Left;  // 方向の判定

	public int IsDirection() { return n_LeftRightFlag; }
	public void ChangeDirection() { n_LeftRightFlag *= -1; }

	// Start is called before the first frame update
	void Start()
    {
		n_LeftRightFlag = (int)Direct.Left;
	}

	public void ReSetDirection() { n_LeftRightFlag = (int)Direct.Right; }

   
}
