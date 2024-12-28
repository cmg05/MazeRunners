using System.Drawing;
using System.Drawing.Text;

namespace MazeRunners
{
    public partial class Maze : Form
    {
        public Maze()
        {
            InitializeComponent();
        }

        private bool gameStarted = false;
        string[,] maze = new string[20, 20];
        static int[,] direccion = { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
        
        int positionX = -1;
        int positionY = -1;

        public void StartGame()
        {
            CreateMaze(maze);
            CreateTeam(maze, "samurai");
            CreateTeam(maze, "ninja");
        }
        private void DrawMap(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int height = Map.Height;
            int width = Map.Width;
            int stripesH = height / maze.GetLength(0);
            int stripesV = width / maze.GetLength(1);
            Pen line = new Pen(Color.Gray, 1);
            SolidBrush stuffing = new SolidBrush(Color.White);



            /* Paint */
            if (positionX != -1 && positionY != -1)
            {
                Rectangle r = new Rectangle(positionY * (stripesV), positionX * (stripesH), (stripesV), (stripesH));
                g.DrawRectangle(line, r);
            }
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Rectangle r = new Rectangle(y * (stripesV), x * (stripesH), (stripesV), (stripesH));

                    if (maze[x, y] == "obs")
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\obs.jpg");
                        g.DrawImage(image, r);
                    }
                    if (maze[x, y] == "ninja")
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\ninja.jpg");
                        g.DrawImage(image, r);
                    }
                    else if (maze[x, y] == "samurai")
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\samurai.jpg");
                        g.DrawImage(image, r);
                    }
                }
            }
        }


        private static void CreateMaze(string[,] array)
        {
            int numObs = 150;
            Random r = new();

            while (numObs > 0)
            {
                int x = r.Next(0, array.GetLength(0));
                int y = r.Next(0, array.GetLength(1));

                if (array[x, y] == null)
                {
                    array[x, y] = "obs";
                    numObs = numObs - 1;
                }
            }
        }

        private static void CreateTeam(string[,] maze, string name)
        {
            int players = 5;
            Random r = new();

            while (players > 0)
            {
                int x = r.Next(0, maze.GetLength(0));
                int y = r.Next(0, maze.GetLength(1));

                if (maze[x, y] == null)
                {
                    maze[x, y] = name;
                    players = players - 1;
                }
            }
        }

        private bool isValidMov(int positionX, int positionY) 
        {
            if ((positionX >= 0 && positionX < maze.GetLength(0)) &&
                (positionY >= 0 && positionY < maze.GetLength(1)) &&
                (maze[positionX, positionY] == null || maze[positionX, positionY] == "traps"))
            {
                return true;   
            }
            return false;
        }

        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            positionX = e.Y * maze.GetLength(1) / Map.Width;
            positionY = e.X * maze.GetLength(0) / Map.Height;

            if (maze[positionX, positionY] != "samurai" && maze[positionX, positionY] != "ninja") 
            {
                positionX = -1;
                positionY = -1;
            }

            Map.Refresh();
        }

        private void upButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX - 1, positionY))
                {
                    maze[positionX - 1, positionY] = maze[positionX, positionY];
                    maze[positionX, positionY] = null;
                    positionX = positionX - 1;
                    Map.Refresh();
                }
            }
        }

        private void downButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX + 1, positionY))
                {
                    maze[positionX + 1, positionY] = maze[positionX, positionY];
                    maze[positionX, positionY] = null;
                    positionX = positionX + 1;
                    Map.Refresh();
                }
            }
        }

        private void leftButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX, positionY - 1))
                {
                    maze[positionX, positionY -1] = maze[positionX, positionY];
                    maze[positionX, positionY] = null;
                    positionY = positionY - 1;
                    Map.Refresh();
                }
            }
        }

        private void rightButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX, positionY + 1))
                {
                    maze[positionX, positionY + 1] = maze[positionX, positionY];
                    maze[positionX, positionY] = null;
                    positionY = positionY + 1;
                    Map.Refresh();
                }
            }
        }
    }
}