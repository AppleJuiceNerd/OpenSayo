using HidSharp;

namespace OpenSayo.Device;

class O3C
{
    // The HID device 
    private HidDevice device;
    private HidStream stream;


	// Tries to find and open a device
	// Might not work with multiple devices
	public O3C() 
	{
		try {
			IEnumerable<HidDevice> devices = DeviceList.Local.GetHidDevices(0x8089, 0x9);
			int index = -1;
			
			// Look for the correct device
			for(int i = 0; i < devices.Count(); i++)
			{
				if(devices.ElementAt(i).GetMaxOutputReportLength() == 1024)
				{
					index = i;
					break;
				}
			}
			
			device = devices.ElementAt(index);
			stream = device.Open();

		} catch { // If this fails, the device likely wasn't found
			Console.WriteLine("Device wasn't found.");
		}
	}


	public override string ToString()
	{
		return device.GetProductName();
	}
}
