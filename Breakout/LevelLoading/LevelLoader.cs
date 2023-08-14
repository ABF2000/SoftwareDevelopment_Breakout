

namespace Breakout.LevelLoading{ 

    /// <summary>
    /// Class responsible for reading the level file and creating data that the rest of the program
    /// can interpet and use. 
    /// </summary>
    public class LevelLoader{
        private string[] rawData;
        private List<FormationData> map = new List<FormationData>();
        public List<FormationData> GetMap(){ return map; }

        private List<FormationData> meta = new List<FormationData>();
        public List<FormationData> GetMeta() { return meta; }

        private List<FormationData> legend = new List<FormationData>();
        public List<FormationData> GetLegend() { return legend; }

        public LevelLoader(){}

        /// <summary>
        /// Saves the data marked between "Map" in the level file
        /// </summary>
        private bool SaveMapData(string[] mapData, int i){
            int j;

            while(rawData[i] != "Map/"){
                j = -1;
                foreach(char c in mapData[i]){
                    j++;
                    if(c != '-'){  
                        map.Add(new FormationData{
                            GridPos = (11-j, i),
                            Symbol = c.ToString()
                        });
                    }
                }
                i++;
                if(i >= rawData.Length) {
                    InitializeEmptyGame();                                    
                    return false;
                } 
            }

            return true;
        }

        /// <summary>
        /// Saves the data marked between "Meta" in the level file
        /// </summary>
        private bool SaveMetaData(string[] metaData, int i){
            while(rawData[i] != "Meta/"){
                string str = "";
                List<string> temp = new List<string>();
                foreach(char c in metaData[i]){
                    if(c != ':' && c != ' '){
                        str += c;
                    }
                    if(c == ' '){
                        if(str == "LEVEL"){
                            str += c;
                        }else{
                            temp.Add(str);
                            str = "";
                        }
                    }
                }
                temp.Add(str);

                meta.Add(new FormationData{
                    Symbol = temp[1],
                    Attribute = temp[0]
                });
            
                
                i++;
                if(i >= rawData.Length){
                    InitializeEmptyGame();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Saves the data marked between "Legend" in the level file
        /// </summary>
        private bool SaveLegendData(string[] metaData, int i){
            while(rawData[i] != "Legend/"){
                string str = "";
                List<string> temp = new List<string>();
                foreach(char c in metaData[i]){
                    if(c != ')' && c != ' '){
                        str += c;
                    }
                    if(c == ' '){
                        temp.Add(str);
                        str = "";
                    }
                }
                temp.Add(str);

                legend.Add(new FormationData{
                    Symbol = temp[0],
                    Image = temp[1]
                });
                i++;
                if(i >= rawData.Length){
                    InitializeEmptyGame();
                    return false;
                }
            }
            return true;
        }


        public void InitializeEmptyGame(){
            map = meta = legend = new List<FormationData>();
        }


        public void ReadFile(string filepath){
            System.Console.WriteLine(filepath);     
            if(File.Exists(filepath)){    
                rawData = File.ReadAllLines(filepath);
                if(rawData.Length == 0) {
                    InitializeEmptyGame();
                    return;
                }
                for(int i = 0; i < rawData.Length; i++){
                    switch(rawData[i]){
                        case("Map:"):
                            i++;
                            if(!SaveMapData(rawData, i)) return;
                            break;
                        case("Meta:"):
                            i++;
                            if(!SaveMetaData(rawData, i)) return;
                            break;
                        case("Legend:"):
                            i++;
                            if(!SaveLegendData(rawData, i)) return;
                            break;
                        default:
                            break;
                    }
                }

                foreach(FormationData block in map){
                    foreach(FormationData type in legend){
                        if(block.Symbol == type.Symbol){
                            block.Image = type.Image;   // Sets the image
                        }
                    }
                    foreach(FormationData attribute in meta){
                        if(block.Symbol == attribute.Symbol){
                            block.Attribute = attribute.Attribute;  // Sets possible attribute
                        }
                    }
                }
            }

            else{
                InitializeEmptyGame(); // If something went wrong an empty game is initialized
            }

        }

    }

}