using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace KartGame.KartSystems
{

	public class KeyboardInput : BaseInput
	{
		public Vector3 rotationSpeed;
		public string Horizontal = "Horizontal";
		public string Vertical = "Vertical";
		public bool StateDetect;
		private int stateWheel;
		
		Thread receiveThread; //1
		UdpClient client; //2
		int port; //3

		public double ejex;

		void Start()
		{
			port = 5065; //1 
			ejex = 0.0;
			InitUDP(); //4
			StateDetect = false;
			stateWheel = PlayerPrefs.GetInt("wheel");
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
				print("Default Port busy, created in another one");
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
					print(">> " + text);
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
							ejex = 0.25;
							StateDetect = true;
						}else if (angulo >= 95 && angulo <= 120){
							ejex = -0.25;
							StateDetect = true;
						}else if (angulo > 30 && angulo <= 60){
							ejex = 0.45;
							StateDetect = true;
						}else if (angulo > 120 && angulo <= 140){
							ejex = -0.45;
							StateDetect = true;
						}else if (angulo <= 30 ){
							ejex = 0.6;
							StateDetect = true;
						}else if (angulo > 140 ){
							ejex = -0.6;
							StateDetect = true;
						}
					
					}else{
						ejex = 0;
						StateDetect = false;
					}	
					
						
					/*if (String.Equals(text, "straight"))
					{
						ejex = 0;
						StateDetect = true;
					}
					else if (String.Equals(text, "left"))
					{
						ejex = -1;
						StateDetect = true;
					}
					else if (String.Equals(text, "right"))
					{
						ejex = 1;
						StateDetect = true;
					}
					else
					{
						ejex = 0;
						StateDetect = false;
					}*/

				}
				catch (Exception e)
				{
					print(e.ToString()); //7
				}
			}
		}

		void Update()
		{
		}

		public override Vector2 GenerateInput()
		{
			
			return new Vector2
			{
				
				//x = (float)ejex,
				//x = Input.GetAxis(Horizontal),
				x = calculateX(),
				y = Input.GetAxis(Vertical)
			};
		}
		
		public float calculateX(){
		
			if(stateWheel==1){
				return 	(float)ejex;
			}else{
				return Input.GetAxis(Horizontal);
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


		void OnDestroy()
		{
			stopThread();
		}

	}
}	
	
