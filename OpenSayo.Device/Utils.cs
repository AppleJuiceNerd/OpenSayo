namespace OpenSayo.Device
{
	class Color {
		public byte g;
		public byte r;
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