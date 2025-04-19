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