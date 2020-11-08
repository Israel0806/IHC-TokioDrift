using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class RotateOnAxisWheel : MonoBehaviour
{
	[Tooltip("Applies a rotation of eulerAngles.z degrees around the z-axis, eulerAngles.x degrees around the x-axis, and eulerAngles.y degrees around the y-axis (in that order).")]
	public Vector3 rotationSpeed;
	public bool rotate;
	//public GameObject state;
	Thread receiveThread; //1
	UdpClient client; //2
	int port; //3
	//public bool nextTutorial2;
	public double ejex;
	public bool StateDetect;
	void Start()
	{
		port = 5065; //1 
		ejex = 0.0;
		//nextTutorial2 = true;
		InitUDP(); //4
		StateDetect = false;
	}

	private void InitUDP()
	{
		print("UDP Initialized");

		receiveThread = new Thread(new ThreadStart(ReceiveData)); //1 
		receiveThread.IsBackground = true; //2
		receiveThread.Start(); //3
	}

	
	private void ReceiveData()
	{
        try
        {
			client = new UdpClient(port); //1
		}
        catch (Exception e)
        {
			client = new UdpClient(50003);
        }
        int angulo;
		while (true) //2
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
				byte[] data = client.Receive(ref anyIP); //4

				string text = Encoding.UTF8.GetString(data); //5
				//print(">> " + text);

				if(text == "not"){
					angulo = -1;					
				}else{
					angulo = int.Parse(text);
				}
				
				
				if(angulo != -1)
				{
					if(angulo > 85 && angulo < 95){
						ejex = 0.0;
						StateDetect = true;
					}else if (angulo > 60 && angulo <= 85 ){
						ejex = 0.125;
						StateDetect = true;
					}else if (angulo >= 95 && angulo <= 120){
						ejex = -0.125;
						StateDetect = true;
					}else if (angulo > 30 && angulo <= 60){
						ejex = 0.25;
						StateDetect = true;
					}else if (angulo > 120 && angulo <= 140){
						ejex = -0.25;
						StateDetect = true;
					}else if (angulo <= 30 ){
						ejex = 0.5;
						StateDetect = true;
					}else if (angulo > 140 ){
						ejex = -0.5;
						StateDetect = true;
					}
				
				}else{
					ejex = 0;
					StateDetect = false;
				}

			}
			catch (Exception e)
			{
				print(e.ToString()); //7
			}
		}
	}
	
	private void stopThread()
    {
        if (receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        client.Close();
    	print("cerrado client");
    }
    
	void Update()
	{
		transform.Rotate((float)ejex * rotationSpeed);
		/*if( transform.eulerAngles.y > -90 && transform.eulerAngles.y < 90 ){	
        			
        }else{
        	transform.eulerAngles = new Vector3(0, 0, 0);
        }
        */
		//print(transform.eulerAngles.y);
		/*if(state.GetComponent<KartGame.UI.LoadSceneButton>().nextTutorial1 && nextTutorial2)
		{
			print("hola");
			stopThread();
			nextTutorial2 = false;	
		} */
	}
	
	 void OnDestroy()
	{
	 	stopThread(); 	 
	}
   
}
