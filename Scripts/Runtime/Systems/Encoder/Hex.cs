namespace SorryLab.Encoder {
    static public class Hex {
        static public string Encode(string n) {
            int p;
            if (int.TryParse(n, out p)) { return Encode(p); } else { return ""; }
        }
        static public string Encode(int n) { return n.ToString("X"); }
        static public int Decode(string n) {
            try { return int.Parse(n, System.Globalization.NumberStyles.HexNumber); } catch { return 0; }
        }
    }
}
