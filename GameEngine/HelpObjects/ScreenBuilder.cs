using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GameEngine.GameObjects;

namespace GameEngine.HelpObjects
{
    class ScreenBuilder
    {
        private string _screenName;
        private GameScreen _screen;
        public ScreenBuilder(GameScreen screen)
        {
            _screenName = screen.Name;
            _screen = screen;
        }

        public bool IsAbleToLoad()
        {
            return false;
        }

        public void LoadScreen()
        {
            //TODO some load methods
        }

        public void SaveScreen()
        {
            //TODO some save methods
            try{
                XmlSerializer serializer = new XmlSerializer(typeof(SpriteObject));
                using (StreamWriter sw = new StreamWriter("test.xml")){
                    foreach (GameObject obj in _screen.Layers["Main"].Objekty){
                        if(obj is SpriteObject)
                            serializer.Serialize(sw,((SpriteObject)obj));
                    }
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
