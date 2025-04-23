using OpenSayo.Device;

namespace OpenSayo.App;

class Program {
	public static void Main()
	{
		Console.WriteLine("yo");
		O3C device = new();

		
		Console.WriteLine(device);

		device.SetLight((Color) 0xABCDEF, 0, 0);
	}
}