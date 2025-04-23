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

			// This is to make sure a potential next Read command works... not sure why it wouldn't without it
			stream.Write([0x22]);

		} catch { // If this fails, the device likely wasn't found
			Console.WriteLine("Device wasn't found.");
		}
	}

	// Not Working
	public byte[] GetLights(byte key)
	{
		byte[] buffer = new byte[60];

		buffer[0] = 0x22;
		buffer[1] = 0x12;
		buffer[2] = 0x26;
		buffer[3] = 0x12;
		buffer[4] = 0x04;
		buffer[5] = 0x00;
		buffer[6] = 0x00;
		buffer[7] = key;
		
		stream.Read(buffer, 0, buffer.Length);
		return buffer;
	}
	
	// Sets the color of a key's light
	// WILL ERASE ALL KEY LIGHTS IN FUNCTION
	public bool SetLight(Color color, int key, int fn)
	{
		byte[] buffer = new byte[64];
		
		// Bytes with an unknown purpose
		buffer[0x08] = 0x01;
		buffer[0x0C] = 0xB8;
		buffer[0x0D] = 0x0B;
		buffer[0x0E] = 0xB8;
		buffer[0x0F] = 0x0B;
		buffer[0x10] = 0x08;
		buffer[0x11] = 0x07;
		buffer[0x12] = 0x08;
		buffer[0x13] = 0x07;
		buffer[0x14] = 0x64;

		ConstructLightSect(buffer, color, fn);

		Util.SetHeader(buffer, 0x12, 0x11, (byte) key, 56);
		stream.Write(buffer);
		return true;
	}
	
	// Constructs a light section of a light config packet
	// Does not yet write anything but color data
	private static void ConstructLightSect(byte[] buffer, Color color, int fn)
	{
		int offset = 0x18 + (8 * fn);
		buffer[offset + 4] = color.r;
		buffer[offset + 5] = color.g;
		buffer[offset + 6] = color.b;
	}

	// ToString override
	public override string ToString()
	{
		return device.GetProductName();
	}
}
