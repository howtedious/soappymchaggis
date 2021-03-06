using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Linq;

namespace SoappyMcHaggis
{
    public class Level
    {
        #region Variables
        private String BGM;
        private List<Tile> m_Tiles;
        public List<Tile> Tiles
        {
            get
            {
                return m_Tiles;
            }
        }

        //private int mapWidth;
        //private int mapHeight;

        private List<Door> m_Doors;
        public List<Door> Doors
        {
            get
            {
                return m_Doors;
            }
        }

        public Cue BGMCue;

        public Vector2 StartPosition;
        public Rectangle MapBounds;
        private FabulousAdventure adv;
        #endregion

        public Level(int LevelIndex, ContentManager theContentManager, FabulousAdventure adv)
        {
            this.adv = adv;
            m_Tiles = new List<Tile>();
            XmlDocument currentLevel;
            currentLevel = new XmlDocument();
            XmlReader reader = XmlReader.Create("Levels/" + LevelIndex + ".xml");
            currentLevel.Load(reader);

            //Get background music for the level from the BackgroundMusic node
            XmlNodeList music = currentLevel.DocumentElement.GetElementsByTagName("BackgroundMusic");
            
            BGM = music[0].InnerText;

            //Get start position for the level from the StartTile node
            XmlNodeList startPosition = currentLevel.GetElementsByTagName("StartPosition");
            StartPosition = new Vector2(float.Parse(startPosition[0].Attributes["XPosition"].Value), float.Parse(startPosition[0].Attributes["YPosition"].Value));

            XmlNodeList mapSize = currentLevel.GetElementsByTagName("MapSize");
            MapBounds = new Rectangle(0, 0, int.Parse(mapSize[0].Attributes["MapWidth"].Value), int.Parse(mapSize[0].Attributes["MapHeight"].Value));

            XmlNodeList tileLayers = currentLevel.GetElementsByTagName("TileLayer");

            foreach (XmlNode tileLayer in tileLayers)
            {
                XmlNodeList tiles = tileLayer.ChildNodes;

                foreach (XmlNode tile in tiles)
                {

                    Tile currentTile = new Tile(int.Parse(tile["TileType"].InnerText),
                                      bool.Parse(tile["Visible"].InnerText),
                                      bool.Parse(tile["Walkable"].InnerText), new Vector2(
                                      int.Parse(tile["Column"].InnerText),
                                      int.Parse(tile["Row"].InnerText)));

                    currentTile.LoadContent(theContentManager, tile["TileTexture"].InnerText);

                    m_Tiles.Add(currentTile);
                }
            }

            XmlNodeList doors = currentLevel.GetElementsByTagName("Doors");

            m_Doors = new List<Door>();

            foreach (XmlNode doorData in doors)
            {
                XmlNodeList aDoor = doorData.ChildNodes;
                foreach (XmlNode data in aDoor)
                {
                    Door currentDoor = new Door(int.Parse(data["DoorNumber"].InnerText),
                        bool.Parse(data["Visible"].InnerText),
                        new Vector2(int.Parse(data["Column"].InnerText),
                        int.Parse(data["Row"].InnerText)),
                        int.Parse(data["LinkedDoor"].InnerText));

                    currentDoor.LoadContent(theContentManager, data["DoorTexture"].InnerText);

                    m_Doors.Add(currentDoor);
                }
            }

            XmlNodeList enemies = currentLevel.GetElementsByTagName("Enemies");

            foreach (XmlNode enemy in enemies)
            {
                XmlNodeList enemyData = enemy.ChildNodes;
                foreach (XmlNode newEnemyData in enemyData)
                {
                    if (newEnemyData["EnemyType"].InnerText == "Target")
                    {
                        Enemy currentEnemy = new Target(int.Parse(newEnemyData["Health"].InnerText), new Vector2(float.Parse(newEnemyData["Column"].InnerText),
                            float.Parse(newEnemyData["Row"].InnerText)));
                        currentEnemy.LoadContent(theContentManager, newEnemyData["EnemyTexture"].InnerText);
                        EnemyManager.addEnemy(currentEnemy);
                    }
                    if (newEnemyData["EnemyType"].InnerText == "HorizontalTarget")
                    {
                        Enemy currentEnemy = new HorizontalTarget(int.Parse(newEnemyData["Health"].InnerText), new Vector2(float.Parse(newEnemyData["Column"].InnerText),
                            float.Parse(newEnemyData["Row"].InnerText)));
                        currentEnemy.LoadContent(theContentManager, newEnemyData["EnemyTexture"].InnerText);
                        EnemyManager.addEnemy(currentEnemy);
                    }

                }
            }
            //BGMCue = Sound.Play(BGM);
        }

        public void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            foreach (Tile tile in m_Tiles)
                tile.Draw(theSpriteBatch, theCamera);
            foreach (Door door in m_Doors)
                door.Draw(theSpriteBatch, theCamera);
        }

        public void Update(float elapsedTime)
        {
            UpdateLossConditions();
        }

        public void UpdateLossConditions()
        {
            if(EnemyManager.AllDead()){
                adv.currentGameState = FabulousAdventure.GameState.GameOverWin;
                //Sound.Stop(BGMCue);
            }
        }

        public void EnterDoor(PlayerCharacter entering)
        {
            foreach (Door door in m_Doors)
            {
                door.Enter(entering, m_Doors);
            }
        }

        public bool Collide(Rectangle collideArea)
        {
            bool collided = false;
            foreach (Tile tile in m_Tiles)
            {
                if (tile.Collide(collideArea))
                    collided = true;
            }
            return collided;
        }
    }
}