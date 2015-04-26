using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using GameEngine.GameObjects;

namespace GameEngine.HelpObjects
{
    public class ScreenBuilder
    {
        private readonly GameScreen _screen;
        public ScreenBuilder(GameScreen screen)
        {
            _screen = screen;
        }

        public bool IsAbleToLoad()
        {
            return (File.Exists(_screen.Name + ".xml")) ;
        }

        public List<GameObjectPackage> LoadScreen()
        {
            if (IsAbleToLoad()){
                try{
                    XmlSerializer serializer = new XmlSerializer(typeof (List<GameObjectPackage>));
                    List<GameObjectPackage> packages;
                    using (StreamReader sr = new StreamReader(_screen.Name + ".xml"))
                    {
                        packages = (List<GameObjectPackage>) serializer.Deserialize(sr);
                    }
                    return packages;
                }
                catch (Exception e){
                    Console.WriteLine(e.ToString());
                }
            }
            return null;
        }

        public void SaveScreen()
        {
            try{
                XmlSerializer serializer = new XmlSerializer(typeof(List<GameObjectPackage>));
                using (StreamWriter sw = new StreamWriter(_screen.Name+".xml")){
                    List<GameObjectPackage> packages = new List<GameObjectPackage>();
                    foreach (Layer layer in _screen.Layers.Values){
                        if (layer.Name == "Gui") break;
                        foreach (GameObject obj in layer.Objekty)
                        {
                            packages.Add(new GameObjectPackage(obj));
                        }
                    }
                    serializer.Serialize(sw,packages);
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
