using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MazeRunners
{
    public partial class Maze : Form
    {

        private static Dictionary<ObjectType, Dictionary<string, string>> typeChips;
        private ObjectType currentChip;
        Dictionary<string, string>[] StatusChips;
        int numberTurn = 0;
        int redTeamFlags = 0;
        int blueTeamFlags = 0;

        public Maze()
        {
            InitializeComponent();
        }


        private bool gameStarted = false;
        private ObjectType[,] maze = new ObjectType[20, 20];
        private List<(int, int)> posRedFlafs = new List<(int, int)>();
        private List<(int, int)> posBlueFlafs = new List<(int, int)>();
        private List<(int, int)> roads = new List<(int, int)>();
        static int[,] directions = { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
        int positionX = -1;
        int positionY = -1;

        public void StartGame()
        {
            turns.Text = "Blue Team";

            TypeChips();

            CreateFlags(maze, ObjectType.RedFlags, posRedFlafs);
            CreateFlags(maze, ObjectType.BlueFlags, posBlueFlafs);

            CreateTeam(maze, posRedFlafs, 5, roads);
            CreateTeam(maze, posBlueFlafs, 10, roads);

            CreateObs(maze, roads);

            CreateTraps(maze, ObjectType.TrapClosedDoor);
            CreateTraps(maze, ObjectType.TrapPitOfThorns);
            CreateTraps(maze, ObjectType.TrapPoisonousGas);
        }

        public enum ObjectType
        {
            RedChipWarrior = 1,
            RedChipKiller = 2,
            RedChipExplorer = 3,
            RedChipHealer = 4,
            RedChipWizard = 5,

            BlueChipWarrior = 6,
            BlueChipKiller = 7,
            BlueChipExplorer = 8,
            BlueChipHealer = 9,
            BlueChipWizard = 10,

            RedFlags = 11,
            BlueFlags = 12,
            TrapClosedDoor = 13,
            TrapPoisonousGas = 14,
            TrapPitOfThorns = 15,
            Obs = 16,
        }

        /* Dibujar tablero */
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

                    if (maze[x, y] == ObjectType.Obs)
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\obs.png");
                        g.DrawImage(image, r);
                    }
                    // Blue team
                    if (((int)maze[x, y]) <= 10 && ((int)maze[x, y]) >= 6)
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\blue.png");
                        g.DrawImage(image, r);
                    }
                    //Red Team
                    else if (((int)maze[x, y]) <= 5 && ((int)maze[x, y]) >= 1)
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\red.png");
                        g.DrawImage(image, r);
                    }
                    else if (maze[x, y] == ObjectType.BlueFlags)
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\blueflag.png");
                        g.DrawImage(image, r);
                    }
                    else if (maze[x, y] == ObjectType.RedFlags)
                    {
                        Image image = Image.FromFile(Application.StartupPath + @"\pictures\redflag.png");
                        g.DrawImage(image, r);
                    }
                }
            }
        }

        /* Crear las banderas de cada equipo */
        private static void CreateFlags(ObjectType[,] maze, ObjectType flag, List<(int, int)> pos)
        {
            int flags = 5;
            Random r = new();

            while (flags > 0)
            {
                int x = r.Next(0, maze.GetLength(0));
                int y = r.Next(0, maze.GetLength(1));

                if (maze[x, y] == 0)
                {
                    maze[x, y] = flag;
                    flags--;
                    pos.Add((x, y));
                }
            }
        }

        /* Crear las fichas de cada equipo */
        private void CreateTeam(ObjectType[,] maze, List<(int, int)> pos, int n, List<(int, int)> roads)
        {
            int players = n;
            Random r = new Random();

            while (players > 0 && n == 5 || players > 5 && n == 10)
            {
                int x = r.Next(0, maze.GetLength(0));
                int y = r.Next(0, maze.GetLength(1));

                bool find = true;

                if (maze[x, y] == 0)
                {
                    List<(int, int)> newRoad = new List<(int, int)>();
                    for (int i = 0; i < pos.Count; i++)
                    {
                        int endX = pos[i].Item1;
                        int endY = pos[i].Item2;
                        bool[,] mask = new bool[20, 20];
                        if (!CreatePaths(mask, maze, x, y, endX, endY, newRoad, (ObjectType)players)) find = false;
                    }

                    if (find)
                    {
                        foreach (var item in newRoad)
                        {
                            if (!roads.Contains(item)) roads.Add(item);
                        }
                        maze[x, y] = (ObjectType)players;
                        players--;
                    }
                }

            }
        }


        /* Guardar posibles movimientos realizados*/
        private bool CreatePaths(bool[,] mask, ObjectType[,] maze, int startX, int startY, int endX, int endY, List<(int, int)> roads, ObjectType chip)
        {
            if (!isValidMov(startX, startY, chip) || !isValidMov(endX, endY, chip)) return false;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            Dictionary<(int, int), (int, int)> parent = new Dictionary<(int, int), (int, int)>();
            queue.Enqueue((startX, startY));
            parent[(startX, startY)] = (-1, -1); 
            mask[startX, startY] = true; 

            bool found = false;

            while (queue.Count > 0 && !found)
            {
                var current = queue.Dequeue();
                if (current.Item1 == endX && current.Item2 == endY)
                {
                    found = true;
                    break;
                }

                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int newX = current.Item1 + directions[i, 0];
                    int newY = current.Item2 + directions[i, 1];

                    if (isValidMov(newX, newY, chip) && !mask[newX, newY])
                    {
                        mask[newX, newY] = true; 
                        queue.Enqueue((newX, newY));
                        parent[(newX, newY)] = current;
                    }
                }
            }

            if (!found) return false;
            ReconstructPath((endX, endY), parent, roads);
            return true;
        }

        /* Reconstruir los caminos*/
        private void ReconstructPath((int, int) end, Dictionary<(int, int), (int, int)> parent, List<(int, int)> roads)
        {
            var current = end;
            while (current != (-1, -1)) 
            {
                roads.Insert(0, current); 
                current = parent[current];
            }
        }


        /* Crear los obstaculos */
        private static void CreateObs(ObjectType[,] maze, List<(int, int)> roads)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == 0 && !roads.Contains((x, y)))
                    {
                        maze[x, y] = ObjectType.Obs;
                    }
                }
            }
        }


        /* Asignar los distintos tipos de fichas */
        private static void TypeChips()
        {
            typeChips = new Dictionary<ObjectType, Dictionary<string, string>>();
            typeChips.Add(ObjectType.RedChipWarrior, new Dictionary<string, string>
            {
                { "Name", "Red Warrior" },
                { "Skill", "Deactivate Trap" },
                { "Description", "Can deactivate a trap in your path" },
                { "Cooldown", "4" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.RedChipExplorer, new Dictionary<string, string>
            {
                { "Name", "Red Explorer" },
                { "Skill", "Agile Leap" },
                { "Description", "Jump the trap" },
                { "Cooldown", "4" },
                { "Turn", "0" },
            });


            typeChips.Add(ObjectType.RedChipKiller, new Dictionary<string, string>
            {
                { "Name", "Red Killer" },
                { "Skill", "Substitution" },
                { "Description", "Create a clone" },
                { "Cooldown", "5" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.RedChipHealer, new Dictionary<string, string>
            {
                { "Name", "Red Healer" },
                { "Skill", "Antidote" },
                { "Description", "Increase turn by 2" },
                { "Cooldown", "3" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.RedChipWizard, new Dictionary<string, string>
            {
                { "Name", "Red Wizard" },
                { "Skill", "Magic Lighting" },
                { "Description", "Swaps position with the enemy wizard" },
                { "Cooldown", "5" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.BlueChipWarrior, new Dictionary<string, string>
            {
                { "Name", "Blue Warrior" },
                { "Skill", "Deactivate Trap" },
                { "Description", "Can deactivate a trap in your path" },
                { "Cooldown", "4" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.BlueChipExplorer, new Dictionary<string, string>
            {
                { "Name", "Blue Explorer" },
                { "Skill", "Agile Leap" },
                { "Description", "Jump the trap" },
                { "Cooldown", "4" },
                { "Turn", "0" },
            });


            typeChips.Add(ObjectType.BlueChipKiller, new Dictionary<string, string>
            {
                { "Name", "Blue Killer" },
                { "Skill", "Substitution" },
                { "Description", "Create a clone" },
                { "Cooldown", "5" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.BlueChipHealer, new Dictionary<string, string>
            {
                { "Name", "Blue Healer" },
                { "Skill", "Antidote" },
                { "Description", "Increase turn by 2" },
                { "Cooldown", "3" },
                { "Turn", "0" },
            });

            typeChips.Add(ObjectType.BlueChipWizard, new Dictionary<string, string>
            {
                { "Name", "Blue Wizard" },
                { "Skill", "Magic Lighting" },
                { "Description", "Swaps position with the enemy wizard" },
                { "Cooldown", "5" },
                { "Turn", "0" },
            });
        }

        /* Crear las trampas */
        private static void CreateTraps(ObjectType[,] array, ObjectType trap)
        {
            int numTraps = 20;
            Random r = new();

            while (numTraps > 0)
            {
                int x = r.Next(0, array.GetLength(0));
                int y = r.Next(0, array.GetLength(1));

                if (array[x, y] == 0)
                {
                    array[x, y] = trap;
                    numTraps = numTraps - 1;
                }
            }
        }



        /* Efectos de las trampas */
        public void ApplyTrapEffect(ObjectType trap, ObjectType currentChip, int x, int y, int newX, int newY, bool power)
        {
            var info = typeChips[currentChip];
            string cooldown = info["Cooldown"];
            string turn = info["Turn"];

            switch (trap)
            {
                case ObjectType.TrapPitOfThorns:

                    if (int.Parse(turn) == int.Parse(cooldown) && power)
                    {
                        if (ObjectType.BlueChipWarrior == currentChip || ObjectType.RedChipWarrior == currentChip)
                        {
                            MessageBox.Show("The trap has been deactivated");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            maze[newX, newY] = 0;
                            maze[x, y] = currentChip;
                            Map.Refresh();
                            break;
                        }

                        else if (ObjectType.BlueChipWizard == currentChip || ObjectType.RedChipWizard == currentChip)
                        {
                            MessageBox.Show("Exchange");
                            info["Turn"] = "0";
                            Turn.Text = "0";

                            // Buscar la posición del Wizard enemigo
                            bool swapped = false;
                            for (int i = 0; i < maze.GetLength(0); i++)
                            {
                                for (int j = 0; j < maze.GetLength(1); j++)
                                {
                                    if (maze[i, j] == (currentChip == ObjectType.BlueChipWizard ? ObjectType.RedChipWizard : ObjectType.BlueChipWizard))
                                    {
                                        // Intercambiar posiciones
                                        maze[x, y] = maze[i, j]; // Mover el Wizard enemigo a la posición de la trampa
                                        maze[i, j] = currentChip; // Mover el Wizard actual a la posición del enemigo
                                        swapped = true;
                                        break;
                                    }
                                }
                                if (swapped) break; 
                            }

                            Map.Refresh();
                            break;
                        }

                        else if (ObjectType.BlueChipExplorer == currentChip || ObjectType.RedChipExplorer == currentChip)
                        {
                            MessageBox.Show("Time Jump");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            int nextX = newX + (newX - x); // Calcula la dirección del movimiento
                            int nextY = newY + (newY - y);

                            // Verificar si la siguiente posición es válida (dentro del laberinto y no bloqueada)
                            if (nextX >= 0 && nextX < maze.GetLength(0) && nextY >= 0 && nextY < maze.GetLength(1) && maze[nextX, nextY] == 0)
                            {
                                maze[x, y] = 0;
                                maze[newX, newY] = 0;
                                maze[nextX, nextY] = currentChip; 
                            }

                            else
                            {
                                maze[x, y] = 0; 
                                maze[newX, newY] = currentChip;
                            }

                            Map.Refresh();
                            break;
                        }

                        else if (ObjectType.BlueChipKiller == currentChip || ObjectType.RedChipKiller == currentChip)
                        {
                            MessageBox.Show("Kage Bunshin no Jutsu");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            maze[newX, newY] = currentChip;
                            maze[x, y] = currentChip;
                            Map.Refresh();
                            break;
                        }
                    }

                    Random r = new Random();
                    int n = r.Next(0, roads.Count);
                    var pos = roads[n];
                    maze[x, y] = 0;
                    maze[pos.Item1, pos.Item2] = currentChip;
                    Map.Refresh();
                    break;

                case ObjectType.TrapPoisonousGas:

                    if (int.Parse(turn) == int.Parse(cooldown) && power)
                    {
                        if (ObjectType.BlueChipWarrior == currentChip || ObjectType.RedChipWarrior == currentChip)
                        {
                            MessageBox.Show("The trap has been deactivated");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            maze[newX, newY] = 0;
                            maze[x, y] = currentChip;
                            Map.Refresh();
                            break;
                        }

                        else if (ObjectType.BlueChipExplorer == currentChip || ObjectType.RedChipExplorer == currentChip)
                        {
                            MessageBox.Show("Time Jump");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            int nextX = newX + (newX - x); 
                            int nextY = newY + (newY - y);

                            if (nextX >= 0 && nextX < maze.GetLength(0) && nextY >= 0 && nextY < maze.GetLength(1) && maze[nextX, nextY] == 0)
                            {
                                maze[x, y] = 0;
                                maze[newX, newY] = 0;
                                maze[nextX, nextY] = currentChip;
                            }

                            else
                            {
                                maze[x, y] = 0;
                                maze[newX, newY] = currentChip;
                            }

                            Map.Refresh();
                            break;


                        }

                        else if (ObjectType.BlueChipWizard == currentChip || ObjectType.RedChipWizard == currentChip)
                        {
                            MessageBox.Show("Exchange");
                            info["Turn"] = "0";
                            Turn.Text = "0";

                            bool swapped = false;
                            for (int i = 0; i < maze.GetLength(0); i++)
                            {
                                for (int j = 0; j < maze.GetLength(1); j++)
                                {
                                    if (maze[i, j] == (currentChip == ObjectType.BlueChipWizard ? ObjectType.RedChipWizard : ObjectType.BlueChipWizard))
                                    {
                                        maze[x, y] = maze[i, j]; 
                                        maze[i, j] = currentChip; 
                                        swapped = true;
                                        break;
                                    }
                                }
                                if (swapped) break; 
                            }

                            Map.Refresh();
                            break;
                        }

                        else if (ObjectType.BlueChipKiller == currentChip || ObjectType.RedChipKiller == currentChip)
                        {
                            MessageBox.Show("Kage Bunshin no Jutsu");
                            info["Turn"] = "0";
                            Turn.Text = "0";
                            maze[newX, newY] = currentChip;
                            maze[x, y] = currentChip;
                            Map.Refresh();
                            break;
                        }
                    }

                    int directionX = x - newX;
                    int directionY = y - newY;
                    int backX = x + (directionX * 3);
                    int backY = y + (directionY * 3);

                    if (backX >= 0 && backX < maze.GetLength(0) && backY >= 0 && backY < maze.GetLength(1))
                    {
                        if (maze[backX, backY] == 0)
                        {
                            maze[backX, backY] = currentChip;
                            maze[x, y] = 0;
                            positionX = backX;
                            positionY = backY;
                        }
                        else
                        {
                            backX = x + directionX;
                            backY = y + directionY;

                            if (maze[backX, backY] == 0)
                            {
                                maze[backX, backY] = currentChip;
                                maze[x, y] = 0;
                                positionX = backX;
                                positionY = backY;
                            }

                            else
                            {
                                MessageBox.Show("There is no room to turn back");
                            }
                        }
                    }

                    Map.Refresh();
                    break;

                case ObjectType.TrapClosedDoor:

                    if (int.Parse(turn) == int.Parse(cooldown) && power)
                    {
                        if (ObjectType.BlueChipExplorer == currentChip || ObjectType.RedChipExplorer == currentChip)
                        {
                            MessageBox.Show("Time Jump");
                            info["Turn"] = "2";
                            Turn.Text = "2";
                            Map.Refresh();
                            break;
                        }

                    }

                    info["Turn"] = "0";
                    Turn.Text = "0";
                    Map.Refresh();
                    break;
            }
        }


        /* Chequear si el movimiento es valido */
        private bool isValidMov(int positionX, int positionY, ObjectType chip)
        {
            if ((positionX >= 0 && positionX < maze.GetLength(0)) &&
                (positionY >= 0 && positionY < maze.GetLength(1)) &&
                (maze[positionX, positionY] == 0
                || maze[positionX, positionY] == ObjectType.TrapClosedDoor
                || maze[positionX, positionY] == ObjectType.TrapPitOfThorns
                || maze[positionX, positionY] == ObjectType.TrapPoisonousGas
                || (maze[positionX, positionY] == ObjectType.RedFlags && (int)chip <= 5 && (int)chip >= 1)
                || (maze[positionX, positionY] == ObjectType.BlueFlags && (int)chip <= 10 && (int)chip >= 6)))
            {
                return true;
            }
            return false;
        }

        /* Chequear que se seleccione una posicion valida */
        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            positionX = e.Y * maze.GetLength(1) / Map.Width;
            positionY = e.X * maze.GetLength(0) / Map.Height;

            var currentChipValue = (int)maze[positionX, positionY];
            bool isEvenTurn = (numberTurn % 2) == 0;
            bool isValidSelection = false;

            if (isEvenTurn)
            {
                isValidSelection = currentChipValue >= 6 && currentChipValue <= 10;
            }
            else
            {
                isValidSelection = currentChipValue >= 1 && currentChipValue <= 5;
            }
            
            if (!isValidSelection || (int)maze[positionX, positionY] > 10 || (int)maze[positionX, positionY] == 0)
            {
                positionX = -1;
                positionY = -1;
            }
            else
            {
                var currentChip = maze[positionX, positionY];
                var info = typeChips[currentChip]; 

                if (typeChips.ContainsKey(currentChip))
                {
                    string chip = info["Name"];
                    Chip.Text = $"{chip}";

                    string skill = info["Skill"];
                    _.Text = $"{skill}";

                    string description = info["Description"];
                    Description.Text = $"{description}";

                    string cooldown = info["Cooldown"];
                    Cooldown.Text = $"{cooldown}";

                    int currentTurn = int.Parse(info["Turn"]);
                    int currentCooldown = int.Parse(info["Cooldown"]);

                    if (currentTurn + 1 > currentCooldown)
                    {
                        info["Turn"] = (currentCooldown).ToString();
                    }
                    else
                    {
                        info["Turn"] = (currentTurn + 1).ToString();
                    }
                    Turn.Text = info["Turn"];
                }
            }

            Map.Refresh();
        }

        /* Asignar movimientos de las fichas */
        private void upButton_MouseClick(object sender, MouseEventArgs e) // Arriba
        {
            if (positionX != -1 && positionY != -1)
            {

                if (isValidMov(positionX - 1, positionY, maze[positionX, positionY]))
                {
                    var chip = maze[positionX, positionY];
                    var trap = maze[positionX - 1, positionY];
                   
                    maze[positionX - 1, positionY] = maze[positionX, positionY];
                    maze[positionX, positionY] = 0;
                    positionX = positionX - 1;
                    Map.Refresh();
                    CollectFlag(positionX, positionY, chip, posRedFlafs, posBlueFlafs);

                    if (trap == ObjectType.TrapClosedDoor || trap == ObjectType.TrapPitOfThorns
                    || trap == ObjectType.TrapPoisonousGas)
                    {
                        MessageBox.Show("Oh! You have fallen into a trap" + trap);
                        ApplyTrapEffect(trap, chip, positionX, positionY, positionX - 1, positionY, true);

                        var info = typeChips[chip];
                        if (typeChips.ContainsKey(chip))
                        {
                            string skill = info["Skill"];
                            _.Text = $"{skill}";

                            string description = info["Description"];
                            Description.Text = $"{description}";

                            string cooldown = info["Cooldown"];
                            Cooldown.Text = $"{cooldown}";

                            int currentTurn = int.Parse(info["Turn"]);
                            int currentCooldown = int.Parse(info["Cooldown"]);

                            if (currentTurn + 1 > currentCooldown)
                            {
                                info["Turn"] = (currentCooldown).ToString();
                            }
                            else
                            {
                                info["Turn"] = (currentTurn + 1).ToString();
                            }
                            Turn.Text = info["Turn"];

                        }
                    }
                }
                
                /* Finaliza el turno */
                MessageBox.Show("Shift ended");
                numberTurn++;
                if (numberTurn % 2 == 0)
                    turns.Text = "Blue Team";
                else turns.Text = "Red Team";
                positionX = -1;
                positionY = -1;
            }
        }

        private void downButton_MouseClick(object sender, MouseEventArgs e) // Abajo
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX + 1, positionY, maze[positionX, positionY]))
                {
                    var chip = maze[positionX, positionY];
                    var trap = maze[positionX + 1, positionY];

                    maze[positionX + 1, positionY] = maze[positionX, positionY];
                    maze[positionX, positionY] = 0;
                    positionX = positionX + 1;
                    Map.Refresh();
                    CollectFlag(positionX, positionY, chip, posRedFlafs, posBlueFlafs);

                    if (trap == ObjectType.TrapClosedDoor || trap == ObjectType.TrapPitOfThorns
                    || trap == ObjectType.TrapPoisonousGas)
                    {
                        MessageBox.Show("Oh! You have fallen into a trap" + trap);
                        ApplyTrapEffect(trap, chip, positionX - 1, positionY, positionX + 1, positionY, true);
                    }
                }

                /* Finaliza el turno */
                MessageBox.Show("Shift ended");
                numberTurn++;
                if (numberTurn % 2 == 0)
                    turns.Text = "Blue Team";
                else turns.Text = "Red Team";
                positionX = -1;
                positionY = -1;
            }
        }

        private void leftButton_MouseClick(object sender, MouseEventArgs e) // Izquierda
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX, positionY - 1, maze[positionX, positionY]))
                {
                    var chip = maze[positionX, positionY];
                    var trap = maze[positionX, positionY - 1];

                    maze[positionX, positionY - 1] = maze[positionX, positionY];
                    maze[positionX, positionY] = 0;
                    positionY = positionY - 1;
                    Map.Refresh();
                    CollectFlag(positionX, positionY, chip, posRedFlafs, posBlueFlafs);

                    if (trap == ObjectType.TrapClosedDoor || trap == ObjectType.TrapPitOfThorns
                    || trap == ObjectType.TrapPoisonousGas)
                    {
                        MessageBox.Show("Oh! You have fallen into a trap" + trap);
                        ApplyTrapEffect(trap, chip, positionX, positionY, positionX, positionY - 1, true);
                    }
                }

                /* Finaliza el turno */
                MessageBox.Show("Shift ended");
                numberTurn++;
                if (numberTurn % 2 == 0)
                    turns.Text = "Blue Team";
                else turns.Text = "Red Team";
                positionX = -1;
                positionY = -1;
            }
        }

        private void rightButton_MouseClick(object sender, MouseEventArgs e) // Derecha
        {
            if (positionX != -1 && positionY != -1)
            {
                if (isValidMov(positionX, positionY + 1, maze[positionX, positionY]))
                {
                    var chip = maze[positionX, positionY];
                    var trap = maze[positionX, positionY + 1];

                    maze[positionX, positionY + 1] = maze[positionX, positionY];
                    maze[positionX, positionY] = 0;
                    positionY = positionY + 1;
                    Map.Refresh();
                    CollectFlag(positionX, positionY, chip, posRedFlafs, posBlueFlafs);

                    if (trap == ObjectType.TrapClosedDoor || trap == ObjectType.TrapPitOfThorns
                    || trap == ObjectType.TrapPoisonousGas)
                    {
                        MessageBox.Show("Oh! You have fallen into a trap" + trap);
                        ApplyTrapEffect(trap, chip, positionX, positionY, positionX, positionY + 1, true);
                    }
                }

                /* Finaliza el turno */
                MessageBox.Show("Shift ended");
                numberTurn++;
                if (numberTurn % 2 == 0)
                    turns.Text = "Blue Team";
                else turns.Text = "Red Team";
                positionX = -1;
                positionY = -1;
            }
        }

        /* Recolección de las banderas */
        private void CollectFlag(int positionX, int positionY, ObjectType chip, List<(int, int)> posRedFlafs, List<(int, int)> posBlueFlafs)
        {
            if (posBlueFlafs.Contains((positionX, positionY)))
            {
                if ((int)chip >= 6 && (int)chip <= 10)
                {
                    posBlueFlafs.Remove((positionX, positionY));
                    Blue.Text = "Blue Flags: " + posBlueFlafs.Count;
                    Map.Refresh();
                    if (posBlueFlafs.Count == 0)
                    {
                        CheckVictory("Blue Team");
                    }
                    
                }
            }
            else if (posRedFlafs.Contains((positionX, positionY)))
            {
                if ((int)chip >= 1 && (int)chip <= 5)
                {
                    posRedFlafs.Remove((positionX, positionY));
                    Red.Text = "Red Flags: " + posRedFlafs.Count;
                    Map.Refresh();
                    if (posRedFlafs.Count == 0)
                    {
                        CheckVictory("Red Team");
                    }
                }
            }
        }

        /* Verificación de la victoria */
        private void CheckVictory(string team)
        {
            if (team == "Blue Team")
            {
                MessageBox.Show("The blue team has won!");
            }
            else if (team == "Red Team")
            {
                MessageBox.Show("The red team has won!");
            }
            Application.Exit();
        }
    }
}

