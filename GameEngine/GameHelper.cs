using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class GameHelper
    {

        private static GameHelper _instance;
        public Keys ZoomIn = Keys.PageDown;
        public Keys ZoomOut = Keys.PageUp;
        public Keys RotateRight = Keys.Home;
        public Keys RotateLeft = Keys.End;
        public Keys CamMoveSpeedPlus = Keys.Add;
        public Keys CamMoveSpeedMinus = Keys.Subtract;
        public Keys PlayerMoveUp = Keys.W;
        public Keys PlayerMoveDown = Keys.S;
        public Keys PlayerMoveLeft = Keys.A;
        public Keys PlayerMoveRight = Keys.D;

        private GameHelper(){
            _random = new Random();
        }

        /// <summary>
        /// Staticka instance
        /// </summary>
        public static GameHelper Instance{
            get { return _instance ?? (_instance = new GameHelper()); }
        }

        private readonly Random _random;

        /// <summary>
        /// Generuje nahodne cele cislo od 0 do maxima
        /// </summary>
        /// <param name="i">urcuje maximalni hodnotu</param>
        /// <returns>nahodne cislo</returns>
        public int RandomNext(int i){
            return _random.Next(i);
        }

        /// <summary>
        /// Generace nahodneho celeho cisla
        /// </summary>
        /// <param name="min">minimalni hodnota</param>
        /// <param name="max">maximalni hodnota</param>
        /// <returns></returns>
        public int RandomNext(int min, int max){
            return _random.Next(min,max);
        }

        /// <summary>
        /// Genereju nahodne cislo
        /// </summary>
        /// <param name="min">minimalni hodnota</param>
        /// <param name="max">maximalni hodnota</param>
        /// <returns></returns>
        public float RandomNext(float min, float max){
            float nasobek = max - min;
            return ((float) _random.NextDouble()*nasobek)+min;
        }

        /// <summary>
        /// Vrati true/false
        /// </summary>
        /// <param name="sance">sance 0f-1f</param>
        /// <returns></returns>
        public bool RandomBool(float sance){
            float x = RandomNext(0f, 0.99f);
            return x<sance;
        }

        /// <summary>
        /// Vrati realnou pozici
        /// </summary>
        /// <param name="nonTransformed">Vektor pro transformaci</param>
        /// <param name="transformace">Matice, ktera je pouzita k vykresleni</param>
        /// <returns>Vektor - Realna pozice</returns>
        public Vector2 RealVector2(Vector2 nonTransformed, Matrix transformace){
            Matrix inversMatrix = Matrix.Invert(transformace);
            Vector2 real;
            Vector2.Transform(ref nonTransformed,ref inversMatrix, out real);
            return real;
        }

        /// <summary>
        /// Vrati Regex prednastaven pro IP
        /// </summary>
        /// <returns>Regex - IP</returns>
        public Regex RegIp(){
            return new Regex(@"^((25[0-5])|(2[0-4][0-9])|(0?[0-9][0-9])|((0{2})?[0-9])|(1[0-9][0-9]))\.((25[0-5])|(2[0-4][0-9])|(0?[0-9][0-9])|((0{2})?[0-9])|(1[0-9][0-9]))\.((25[0-5])|(2[0-4][0-9])|(0?[0-9][0-9])|((0{2})?[0-9])|(1[0-9][0-9]))\.((25[0-5])|(2[0-4][0-9])|(0?[0-9][0-9])|((0{2})?[0-9])|(1[0-9][0-9]))$");
        }

        /// <summary>
        /// Vrati Regex prednastaven pro e-mail
        /// </summary>
        /// <returns>Regex - e-mail</returns>
        public Regex RegEmail(){
            return new Regex(@"^[a-zA-Z.]+@[a-zA-Z.]+\.[a-zA-Z]{2,4}$");
        }

        /// <summary>
        /// Vrati znak klavesy
        /// </summary>
        /// <param name="key">Klavesa</param>
        /// <param name="shift">Je zmacknut shift</param>
        /// <returns>String - Znak klavesy</returns>
        public string TextFromKey(Keys key,bool shift = false){
            switch (key)
            {
                //Alphabet keys
                case Keys.A: return shift ? "A" : "a"; 
                case Keys.B: return shift ? "B" : "b"; 
                case Keys.C: return shift ? "C" : "c"; 
                case Keys.D: return shift ? "D" : "d"; 
                case Keys.E: return shift ? "E" : "e"; 
                case Keys.F: return shift ? "F" : "f"; 
                case Keys.G: return shift ? "G" : "g"; 
                case Keys.H: return shift ? "H" : "h"; 
                case Keys.I: return shift ? "I" : "i"; 
                case Keys.J: return shift ? "J" : "j"; 
                case Keys.K: return shift ? "K" : "k"; 
                case Keys.L: return shift ? "L" : "l"; 
                case Keys.M: return shift ? "M" : "m"; 
                case Keys.N: return shift ? "N" : "n"; 
                case Keys.O: return shift ? "O" : "o"; 
                case Keys.P: return shift ? "P" : "p"; 
                case Keys.Q: return shift ? "Q" : "q"; 
                case Keys.R: return shift ? "R" : "r"; 
                case Keys.S: return shift ? "S" : "s"; 
                case Keys.T: return shift ? "T" : "t"; 
                case Keys.U: return shift ? "U" : "u"; 
                case Keys.V: return shift ? "V" : "v"; 
                case Keys.W: return shift ? "W" : "w"; 
                case Keys.X: return shift ? "X" : "x"; 
                case Keys.Y: return shift ? "Y" : "y"; 
                case Keys.Z: return shift ? "Z" : "z"; 

                //Decimal keys
                case Keys.D0: return shift ? ")" : "0"; 
                case Keys.D1: return shift ? "!" : "1"; 
                case Keys.D2: return shift ? "@" : "2"; 
                case Keys.D3: return shift ? "#" : "3"; 
                case Keys.D4: return shift ? "$" : "4"; 
                case Keys.D5: return shift ? "%" : "5"; 
                case Keys.D6: return shift ? "^" : "6"; 
                case Keys.D7: return shift ? "&" : "7"; 
                case Keys.D8: return shift ? "*" : "8"; 
                case Keys.D9: return shift ? "(" : "9"; 

                //Decimal numpad keys
                case Keys.NumPad0: return "0"; 
                case Keys.NumPad1: return "1"; 
                case Keys.NumPad2: return "2"; 
                case Keys.NumPad3: return "3"; 
                case Keys.NumPad4: return "4"; 
                case Keys.NumPad5: return "5"; 
                case Keys.NumPad6: return "6"; 
                case Keys.NumPad7: return "7"; 
                case Keys.NumPad8: return "8"; 
                case Keys.NumPad9: return "9"; 

                //Special keys
                case Keys.OemTilde: return shift ? "~" : "`"; 
                case Keys.OemSemicolon: return shift ? ":" : ";"; 
                case Keys.OemQuotes: return shift ? "\"" : "\'"; 
                case Keys.OemQuestion: return shift ? "?" : "/"; 
                case Keys.OemPlus: return shift ? "+" : "="; 
                case Keys.OemPipe: return shift ? "|" : "\\"; 
                case Keys.OemPeriod: return shift ? ">" : "."; 
                case Keys.OemOpenBrackets: return shift ? "{" : "["; 
                case Keys.OemCloseBrackets: return shift ? "}" : "]"; 
                case Keys.OemMinus: return shift ? "_" : "-"; 
                case Keys.OemComma: return shift ? "<" : ","; 
                case Keys.Space: return " ";  
            }
            return null;
        }
    }
}
