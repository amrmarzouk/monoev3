using System;
using MonoBrickFirmware;
using MonoBrickFirmware.IO;
using System.Threading;

namespace IRSensorExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IRChannel[] channels = {IRChannel.One, IRChannel.Two, IRChannel.Three, IRChannel.Four};
			
			int channelIdx = 0;
			ManualResetEvent terminateProgram = new ManualResetEvent(false);
			var sensor = new IRSensor(SensorPort.In1);
			ButtonEvents buts = new ButtonEvents ();
			
			buts.EscapePressed += () => { 
				terminateProgram.Set();
			};
			buts.UpPressed += () => { 
				Console.WriteLine("Distance " + sensor.ReadDistance() +  " cm");
			};
			buts.EnterPressed += () => { 
				Console.WriteLine("Remote command " + sensor.ReadRemoteCommand() + " on channel " + sensor.Channel);									
			};
			buts.DownPressed += () => { 
				BeaconLocation location  = sensor.ReadBeaconLocation();
				Console.WriteLine("Beacon location: " + location.Location + " Beacon distance: " + location.Distance + " cm"); 
				
			};
			buts.LeftPressed += () => { 
				channelIdx = (channelIdx+1)%channels.Length;
				sensor.Channel = channels[channelIdx];
				Console.WriteLine("Channel is set to: " + channels[channelIdx]);
			};
			buts.RightPressed += () => { 
				Console.WriteLine(sensor.ReadAsString());	
			};
			terminateProgram.WaitOne(); 
			
		}
	}
}