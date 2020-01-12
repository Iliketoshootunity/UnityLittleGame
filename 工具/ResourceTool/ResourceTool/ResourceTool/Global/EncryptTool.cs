using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckResourceTool
{
    /// <summary>
    /// 加密工具
    /// </summary>
    class EncryptTool
    {
        private static EncryptTool _instance;
        public static EncryptTool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EncryptTool();
                }
                return _instance;
            }
        }

        private Dictionary<char, string> encryptDic;
        private Dictionary<char, char> deEncryptDic;
        private Random rand;

        public EncryptTool()
        {
            EncryptToolInitialize();
        }

        /// <summary>
        /// 加密工具初始化
        /// </summary>
        /// <returns></returns>
        private void EncryptToolInitialize()
        {
            rand = new Random();

            #region 加密解密字典初始化
            encryptDic = new Dictionary<char, string>();
            deEncryptDic = new Dictionary<char, char>();

            deEncryptDic.Add('ㄋ', '"');
            deEncryptDic.Add('δ', '"');
            deEncryptDic.Add('ひ', '"');
            encryptDic.Add('"', "ㄋδひ");
            deEncryptDic.Add('Β', ',');
            deEncryptDic.Add('θ', ',');
            deEncryptDic.Add('ゃ', ',');
            encryptDic.Add(',', "Βθゃ");
            deEncryptDic.Add('ュ', '.');
            deEncryptDic.Add('Е', '.');
            deEncryptDic.Add('け', '.');
            encryptDic.Add('.', "ュЕけ");
            deEncryptDic.Add('Α', '/');
            deEncryptDic.Add('ㄍ', '/');
            deEncryptDic.Add('て', '/');
            encryptDic.Add('/', "Αㄍて");
            deEncryptDic.Add('С', '0');
            deEncryptDic.Add('Ι', '0');
            deEncryptDic.Add('Ψ', '0');
            encryptDic.Add('0', "СΙΨ");
            deEncryptDic.Add('は', '1');
            deEncryptDic.Add('ス', '1');
            deEncryptDic.Add('λ', '1');
            encryptDic.Add('1', "はスλ");
            deEncryptDic.Add('ト', '2');
            deEncryptDic.Add('ㄧ', '2');
            deEncryptDic.Add('η', '2');
            encryptDic.Add('2', "トㄧη");
            deEncryptDic.Add('せ', '3');
            deEncryptDic.Add('κ', '3');
            deEncryptDic.Add('Χ', '3');
            encryptDic.Add('3', "せκΧ");
            deEncryptDic.Add('﹄', '4');
            deEncryptDic.Add('И', '4');
            deEncryptDic.Add('ィ', '4');
            encryptDic.Add('4', "﹄Иィ");
            deEncryptDic.Add('く', '5');
            deEncryptDic.Add('ミ', '5');
            deEncryptDic.Add('П', '5');
            encryptDic.Add('5', "くミП");
            deEncryptDic.Add('Ζ', '6');
            deEncryptDic.Add('ι', '6');
            deEncryptDic.Add('ㄗ', '6');
            encryptDic.Add('6', "Ζιㄗ");
            deEncryptDic.Add('ㄝ', '7');
            deEncryptDic.Add('か', '7');
            deEncryptDic.Add('β', '7');
            encryptDic.Add('7', "ㄝかβ");
            deEncryptDic.Add('μ', '8');
            deEncryptDic.Add('З', '8');
            deEncryptDic.Add('ゑ', '8');
            encryptDic.Add('8', "μЗゑ");
            deEncryptDic.Add('α', '9');
            deEncryptDic.Add('Ν', '9');
            deEncryptDic.Add('Ш', '9');
            encryptDic.Add('9', "αΝШ");
            deEncryptDic.Add('У', ':');
            deEncryptDic.Add('ね', ':');
            deEncryptDic.Add('テ', ':');
            encryptDic.Add(':', "Уねテ");
            deEncryptDic.Add('し', 'A');
            deEncryptDic.Add('А', 'A');
            deEncryptDic.Add('Я', 'A');
            encryptDic.Add('A', "しАЯ");
            deEncryptDic.Add('Ф', 'B');
            deEncryptDic.Add('ゐ', 'B');
            deEncryptDic.Add('サ', 'B');
            encryptDic.Add('B', "Фゐサ");
            deEncryptDic.Add('︿', 'C');
            deEncryptDic.Add('ソ', 'C');
            deEncryptDic.Add('き', 'C');
            encryptDic.Add('C', "︿ソき");
            deEncryptDic.Add('ァ', 'D');
            deEncryptDic.Add('ξ', 'D');
            deEncryptDic.Add('Ε', 'D');
            encryptDic.Add('D', "ァξΕ");
            deEncryptDic.Add('Υ', 'E');
            deEncryptDic.Add('ㄔ', 'E');
            deEncryptDic.Add('Γ', 'E');
            encryptDic.Add('E', "ΥㄔΓ");
            deEncryptDic.Add('ャ', 'F');
            deEncryptDic.Add('Л', 'F');
            deEncryptDic.Add('∑', 'F');
            encryptDic.Add('F', "ャЛ∑");
            deEncryptDic.Add('ㄒ', 'G');
            deEncryptDic.Add('ㄈ', 'G');
            deEncryptDic.Add('ゎ', 'G');
            encryptDic.Add('G', "ㄒㄈゎ");
            deEncryptDic.Add('ο', 'H');
            deEncryptDic.Add('フ', 'H');
            deEncryptDic.Add('ㄤ', 'H');
            encryptDic.Add('H', "οフㄤ");
            deEncryptDic.Add('Μ', 'I');
            deEncryptDic.Add('γ', 'I');
            deEncryptDic.Add('カ', 'I');
            encryptDic.Add('I', "Μγカ");
            deEncryptDic.Add('υ', 'J');
            deEncryptDic.Add('Т', 'J');
            deEncryptDic.Add('ツ', 'J');
            encryptDic.Add('J', "υТツ");
            deEncryptDic.Add('ョ', 'K');
            deEncryptDic.Add('﹁', 'K');
            deEncryptDic.Add('χ', 'K');
            encryptDic.Add('K', "ョ﹁χ");
            deEncryptDic.Add('Б', 'L');
            deEncryptDic.Add('ρ', 'L');
            deEncryptDic.Add('Ч', 'L');
            encryptDic.Add('L', "БρЧ");
            deEncryptDic.Add('ぃ', 'M');
            deEncryptDic.Add('Д', 'M');
            deEncryptDic.Add('Г', 'M');
            encryptDic.Add('M', "ぃДГ");
            deEncryptDic.Add('σ', 'N');
            deEncryptDic.Add('ψ', 'N');
            deEncryptDic.Add('も', 'N');
            encryptDic.Add('N', "σψも");
            deEncryptDic.Add('ㄘ', 'O');
            deEncryptDic.Add('ケ', 'O');
            deEncryptDic.Add('Ρ', 'O');
            encryptDic.Add('O', "ㄘケΡ");
            deEncryptDic.Add('ㄛ', 'P');
            deEncryptDic.Add('︺', 'P');
            deEncryptDic.Add('ク', 'P');
            encryptDic.Add('P', "ㄛ︺ク");
            deEncryptDic.Add('ッ', 'Q');
            deEncryptDic.Add('ζ', 'Q');
            deEncryptDic.Add('Й', 'Q');
            encryptDic.Add('Q', "ッζЙ");
            deEncryptDic.Add('を', 'R');
            deEncryptDic.Add('ㄏ', 'R');
            deEncryptDic.Add('ナ', 'R');
            encryptDic.Add('R', "をㄏナ");
            deEncryptDic.Add('ω', 'S');
            deEncryptDic.Add('ㄨ', 'S');
            deEncryptDic.Add('ほ', 'S');
            encryptDic.Add('S', "ωㄨほ");
            deEncryptDic.Add('ヘ', 'T');
            deEncryptDic.Add('В', 'T');
            deEncryptDic.Add('シ', 'T');
            encryptDic.Add('T', "ヘВシ");
            deEncryptDic.Add('Ω', 'U');
            deEncryptDic.Add('ヮ', 'U');
            deEncryptDic.Add('Х', 'U');
            encryptDic.Add('U', "ΩヮХ");
            deEncryptDic.Add('ぅ', 'V');
            deEncryptDic.Add('の', 'V');
            deEncryptDic.Add('︶', 'V');
            encryptDic.Add('V', "ぅの︶");
            deEncryptDic.Add('Ж', 'W');
            deEncryptDic.Add('ハ', 'W');
            deEncryptDic.Add('ε', 'W');
            encryptDic.Add('W', "Жハε");
            deEncryptDic.Add('Ы', 'X');
            deEncryptDic.Add('Э', 'X');
            deEncryptDic.Add('Σ', 'X');
            encryptDic.Add('X', "ЫЭΣ");
            deEncryptDic.Add('ㄣ', 'Y');
            deEncryptDic.Add('Ξ', 'Y');
            deEncryptDic.Add('︾', 'Y');
            encryptDic.Add('Y', "ㄣΞ︾");
            deEncryptDic.Add('コ', 'Z');
            deEncryptDic.Add('ㄡ', 'Z');
            deEncryptDic.Add('︷', 'Z');
            encryptDic.Add('Z', "コㄡ︷");
            deEncryptDic.Add('Τ', '[');
            deEncryptDic.Add('︹', '[');
            deEncryptDic.Add('こ', '[');
            encryptDic.Add('[', "Τ︹こ");
            deEncryptDic.Add('セ', ']');
            deEncryptDic.Add('Ъ', ']');
            deEncryptDic.Add('そ', ']');
            encryptDic.Add(']', "セЪそ");
            deEncryptDic.Add('︸', '_');
            deEncryptDic.Add('М', '_');
            deEncryptDic.Add('ホ', '_');
            encryptDic.Add('_', "︸Мホ");
            deEncryptDic.Add('ㄌ', 'a');
            deEncryptDic.Add('ㄕ', 'a');
            deEncryptDic.Add('め', 'a');
            encryptDic.Add('a', "ㄌㄕめ");
            deEncryptDic.Add('ㄇ', 'b');
            deEncryptDic.Add('﹂', 'b');
            deEncryptDic.Add('ㄜ', 'b');
            encryptDic.Add('b', "ㄇ﹂ㄜ");
            deEncryptDic.Add('さ', 'c');
            deEncryptDic.Add('ち', 'c');
            deEncryptDic.Add('Ё', 'c');
            encryptDic.Add('c', "さちЁ");
            deEncryptDic.Add('∏', 'd');
            deEncryptDic.Add('ぁ', 'd');
            deEncryptDic.Add('ㄩ', 'd');
            encryptDic.Add('d', "∏ぁㄩ");
            deEncryptDic.Add('キ', 'e');
            deEncryptDic.Add('φ', 'e');
            deEncryptDic.Add('ㄞ', 'e');
            encryptDic.Add('e', "キφㄞ");
            deEncryptDic.Add('メ', 'f');
            deEncryptDic.Add('タ', 'f');
            deEncryptDic.Add('ヲ', 'f');
            encryptDic.Add('f', "メタヲ");
            deEncryptDic.Add('と', 'g');
            deEncryptDic.Add('ㄑ', 'g');
            deEncryptDic.Add('Κ', 'g');
            encryptDic.Add('g', "とㄑΚ");
            deEncryptDic.Add('ㄖ', 'h');
            deEncryptDic.Add('ヰ', 'h');
            deEncryptDic.Add('Ο', 'h');
            encryptDic.Add('h', "ㄖヰΟ");
            deEncryptDic.Add('︽', 'i');
            deEncryptDic.Add('へ', 'i');
            deEncryptDic.Add('К', 'i');
            encryptDic.Add('i', "︽へК");
            deEncryptDic.Add('Φ', 'j');
            deEncryptDic.Add('О', 'j');
            deEncryptDic.Add('ㄦ', 'j');
            encryptDic.Add('j', "ΦОㄦ");
            deEncryptDic.Add('Р', 'k');
            deEncryptDic.Add('ゅ', 'k');
            deEncryptDic.Add('Н', 'k');
            encryptDic.Add('k', "РゅН");
            deEncryptDic.Add('ゥ', 'l');
            deEncryptDic.Add('π', 'l');
            deEncryptDic.Add('ㄙ', 'l');
            encryptDic.Add('l', "ゥπㄙ");
            deEncryptDic.Add('Ь', 'm');
            deEncryptDic.Add('ヌ', 'm');
            deEncryptDic.Add('ν', 'm');
            encryptDic.Add('m', "Ьヌν");
            deEncryptDic.Add('ヴ', 'n');
            deEncryptDic.Add('ム', 'n');
            deEncryptDic.Add('た', 'n');
            encryptDic.Add('n', "ヴムた");
            deEncryptDic.Add('チ', 'o');
            deEncryptDic.Add('む', 'o');
            deEncryptDic.Add('Π', 'o');
            encryptDic.Add('o', "チむΠ");
            deEncryptDic.Add('ふ', 'p');
            deEncryptDic.Add('ヒ', 'p');
            deEncryptDic.Add('Δ', 'p');
            encryptDic.Add('p', "ふヒΔ");
            deEncryptDic.Add('ネ', 'q');
            deEncryptDic.Add('っ', 'q');
            deEncryptDic.Add('モ', 'q');
            encryptDic.Add('q', "ネっモ");
            deEncryptDic.Add('つ', 'r');
            deEncryptDic.Add('ニ', 'r');
            deEncryptDic.Add('ㄎ', 'r');
            encryptDic.Add('r', "つニㄎ");
            deEncryptDic.Add('Ц', 's');
            deEncryptDic.Add('Щ', 's');
            deEncryptDic.Add('す', 's');
            encryptDic.Add('s', "ЦЩす");
            deEncryptDic.Add('な', 't');
            deEncryptDic.Add('﹃', 't');
            deEncryptDic.Add('ん', 't');
            encryptDic.Add('t', "な﹃ん");
            deEncryptDic.Add('に', 'u');
            deEncryptDic.Add('︵', 'u');
            deEncryptDic.Add('Λ', 'u');
            encryptDic.Add('u', "に︵Λ");
            deEncryptDic.Add('ぉ', 'v');
            deEncryptDic.Add('ヶ', 'v');
            deEncryptDic.Add('Ю', 'v');
            encryptDic.Add('v', "ぉヶЮ");
            deEncryptDic.Add('ㄉ', 'w');
            deEncryptDic.Add('∧', 'w');
            deEncryptDic.Add('ヱ', 'w');
            encryptDic.Add('w', "ㄉ∧ヱ");
            deEncryptDic.Add('ま', 'x');
            deEncryptDic.Add('τ', 'x');
            deEncryptDic.Add('Η', 'x');
            encryptDic.Add('x', "まτΗ");
            deEncryptDic.Add('み', 'y');
            deEncryptDic.Add('ㄆ', 'y');
            deEncryptDic.Add('ォ', 'y');
            encryptDic.Add('y', "みㄆォ");
            deEncryptDic.Add('ㄢ', 'z');
            deEncryptDic.Add('ン', 'z');
            deEncryptDic.Add('ㄚ', 'z');
            encryptDic.Add('z', "ㄢンㄚ");
            deEncryptDic.Add('ぇ', '{');
            deEncryptDic.Add('﹀', '{');
            deEncryptDic.Add('Θ', '{');
            encryptDic.Add('{', "ぇ﹀Θ");
            deEncryptDic.Add('ㄠ', '}');
            deEncryptDic.Add('ㄊ', '}');
            deEncryptDic.Add('ょ', '}');
            encryptDic.Add('}', "ㄠㄊょ");
            #endregion
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptData(string data)
        {
            StringBuilder sb = new StringBuilder(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(EncryptSingleChar(data[i]));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DeEncryptData(string data)
        {
            StringBuilder sb = new StringBuilder(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(DeEncryptSingleChar(data[i]));
            }
            return sb.ToString();
        }

        private char EncryptSingleChar(char c)
        {
            if (encryptDic.ContainsKey(c))
            {
                return encryptDic[c][rand.Next(0, encryptDic[c].Length)];
            }
            return c;
        }

        private char DeEncryptSingleChar(char c)
        {
            if (deEncryptDic.ContainsKey(c))
            {
                return deEncryptDic[c];
            }
            return c;
        }
    }
}
