namespace OpenSayo.Device
{
	class Util {
		// The algorithm that satisfies whatever checks bytes 0x02 and 0x03
		public static void Checksum(byte[] buffer)
		{
			ushort num = 0;
			for(int i = 0; i < buffer.Length; i += 2) {
				if(i == 2) { continue; } // Just in case something happens to be here
				
				num += BitConverter.ToUInt16(buffer, i);
			}

			buffer[2] = (byte) (num & 0xFF);
			buffer[3] = (byte) (num >> 8);
		}

		// Sets header parameters
		// Should be called after the packet is constructed
		public static void SetHeader(
			byte[] buffer,
			byte twelve, // unknown
			byte cmd,
			byte index,
			int length // Possibly just buffer.Length - 8
		)
		{
			ushort len = (ushort) (length + 4);
			buffer[0] = 0x22; // Probably sometimes 0x23
			buffer[1] = twelve; // Usually 12...
			buffer[2] = 0; // Sum b1
			buffer[3] = 0; // Sum b2
			buffer[4] = (byte) (len & 0xFF); // Length?
			buffer[5] = (byte) (len >> 8);
			buffer[6] = cmd;
			buffer[7] = index;

			Checksum(buffer);
		}
	}



	class Color {
		public byte r;
		public byte g;
		public byte b;
		
		public Color(byte red, byte blue, byte green) {
			r = red;
			g = green;
			b = blue;
		}

		override public string ToString()
		{
			return "(" + r + ", " + g + ", " + b + ")";
		}
	}
}