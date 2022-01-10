using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	public Door door;
	public bool ignoreTrigger; //kalo ga mau pake trigger depan pintu


	public void Toggle(bool value){ //fungsi ini buat dijalanin sama switch
		if (value)
			door.Open ();
		else
			door.Close ();
	}

}
