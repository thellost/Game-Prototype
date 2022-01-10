using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class Switch : MonoBehaviour {
	[SerializeField] DoorTrigger[] doorTriggers;
	public bool sticky; //var penentu switch hanya sekali tekan, tidak kembali naik lagi
	[SerializeField] Light2D greenLight;
	[SerializeField] Light2D redLight;
	private bool down;
	private Animator animator;

	
	void Start () {
		animator = GetComponent<Animator> ();
	}


    private void OnTriggerStay2D(Collider2D collision)
    {
		if (!down && collision.CompareTag("Player"))
		{
			if (Input.GetKey(KeyCode.E))
			{

				animator.SetInteger("AnimState", 1); //animasi switch ketekan
				down = true; //set boolean true

				greenLight.enabled = true;
				redLight.enabled = false;
				
				foreach (DoorTrigger trigger in doorTriggers)
				{ //cari semua doortrigger yg didaftarin pada array
					if (trigger != null) //kalo array tidak kosong
						trigger.Toggle(true); //jalankan fungsi toggle pada doortrigger
				}
			}
		}
	}


    void OnTriggerExit2D(Collider2D target){
		if (sticky && down) //kalau switch sudah ditekan DAN dia sticky
			return; //fungsi tidak dijalankan, alias switch tidak beranimasi naik lagi

		//dibawah ini artinya switch tidak sticky, alias beranimasi naik lagi
		animator.SetInteger ("AnimState", 2);
		down = false;

		foreach (DoorTrigger trigger in doorTriggers) { //sama kyk diatas, tapi ini difalse alias naik lagi
			if(trigger != null)
				trigger.Toggle(false);
		}
	}

	void OnDrawGizmos(){ //menggambar garis penanda doortrigger yg bersangkutan
		Gizmos.color = sticky ? Color.blue : Color.green; //kalo biru artinya switch tsb sticky, kalo hijau tidak sticky
		foreach (DoorTrigger trigger in doorTriggers) { //cari semua doortrigger pada array
			if(trigger != null)
				Gizmos.DrawLine(transform.position, trigger.door.transform.position); //gambar garisnya dari posisi si switch
		}
	}
}
