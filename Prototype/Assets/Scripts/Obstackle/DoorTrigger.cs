using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	public Door door;
	public bool ignoreTrigger; //kalo ga mau pake trigger depan pintu

	void OnTriggerEnter2D(Collider2D target){
		if (ignoreTrigger) //skip function ini kalo mau pake switch SAJA
			return;

		if (target.gameObject.tag == "Player") {  //buka door kalo itu player
			door.Open();
		}
	}

	void OnTriggerExit2D(Collider2D target){
		if (ignoreTrigger) //skip function ini kalo mau pake switch SAJA
			return;

		if (target.gameObject.tag == "Player") {
			door.Close();
		}
	}

	public void Toggle(bool value){ //fungsi ini buat dijalanin sama switch
		if (value)
			door.Open ();
		else
			door.Close ();
	}

	void OnDrawGizmos(){ //penanda kalo doornya pake trigger atau pake switch
		Gizmos.color = ignoreTrigger ? Color.gray : Color.green; //pake trigger warna kotaknya hijau, pake switch doank warna kotaknya abu

        //proses mengambil kotak collider dan posisinya
		var bc2d = GetComponent<BoxCollider2D> ();
		var bc2dPos = bc2d.transform.position;
		var newPos = new Vector2(bc2dPos.x + bc2d.offset.x, bc2dPos.y + bc2d.offset.y);
		Gizmos.DrawWireCube (newPos, new Vector2 (bc2d.size.x, bc2d.size.y)); //menggambar garis sesuai kotak collider

	}
}
