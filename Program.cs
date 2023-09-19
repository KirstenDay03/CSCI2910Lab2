namespace CSCI2910Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            Dictionary<String, List<VideoGame>> games = new Dictionary<String, List<VideoGame>>(); //Creates a dictionary called games

            List<VideoGame> allVG = new List<VideoGame>(); //Creates a list called allVG
            allVG = p.CreateListOfGames(); //Fills allVG with all the video games in the data

            List<String> platforms = new List<string>(); //Creates a list of strings called platforms

            for(int x = 0; x < allVG.Count; x++) //For loop goes through all the games in allVG and stores each game's platform in platforms
            {
                string platform = allVG[x].GetPlatform();

                platforms.Add(platform);

            }

            IEnumerable<String> distinctPlatforms = platforms.Distinct().ToList(); //Takes platforms and removes all the duplicate values

            /**foreach (string plat in distinctPlatforms)
            {
                Console.WriteLine(plat);
            }

            Console.WriteLine();**/

            foreach(string plat in distinctPlatforms)                               //Creates all the lists and adds them to the dictionary using their platform as a key
            {
                List<VideoGame> gamesByPlatform = p.CreateList(plat);
                games.Add(plat, gamesByPlatform);
            }

            Func<Dictionary<string, List<VideoGame>>, string, List<VideoGame>> getTop5GamesByPlatform = (games, platform) =>        //Tells program what var to expect to send and receive
            {
                if (games.TryGetValue(platform, out var platformGames))                                                             //If the platform sent exists as a key, takeout the list associated with that key
                {
                    return platformGames.OrderByDescending(game => game.GetGlobalSales()).Take(5).ToList();                         //Return the list in decending order, by global sales, and only take top five
                }
                else
                {
                    return new List<VideoGame>();                                                                                   //Returns a list if the if fails so that there is no error
                }
            };


            foreach (string plat in distinctPlatforms)                                  //Displays each list for each platform in distinctPlatforms
            {
                Console.WriteLine($"The Top Five Globally Sold Games on {plat}\n" +
                                  $"------------------------------------------");
                int counter = 0;
                var top5GamesForPlatform = getTop5GamesByPlatform(games, plat);
                foreach (var game in top5GamesForPlatform)
                {
                    counter++;
                    Console.WriteLine($"{counter}: {game}");
                }
                Console.WriteLine($"------------------------------------------");
                Console.WriteLine();
            }

        }


        /// <summary>
        /// Returns a list that only contains games of a certain platform
        /// </summary>
        /// <param name="platform">string platform</param>
        /// <returns>List<VideoGame> gamesByPlatform</returns>
        public  List<VideoGame> CreateList(string platform)
        {
            List<VideoGame> gamesByPlatform = new List<VideoGame>();                          //creates a new list of VideoGames called games
            string fileName = $@"..\..\..\DataFile\videogames.csv";                 //saves the relative path to videogames.csv as a string

            try                                                                     //try to validate contained code
            {
                using (StreamReader rdr = new StreamReader(fileName))               //opens videogames.csv using StreamReader
                {
                    string nextLine = rdr.ReadLine();                               //reads the first line in videogames.csv and saves it in nextLine as a string
                    while (rdr.Peek() != -1)                                        //while loop to make sure there is another line to read
                    {
                        nextLine = rdr.ReadLine();                                  //reads the next line in videogames.csv

                        string[] fields = nextLine.Split(',');                      //creates a string array to hold each individual value in nextLine

                        VideoGame v = new VideoGame(fields[0], fields[1], int.Parse(fields[2]), fields[3], fields[4], double.Parse(fields[5]), double.Parse(fields[6]), double.Parse(fields[7]), double.Parse(fields[8]), double.Parse(fields[9]));
                        //creates a new VideoGame v and uses the values stored in fields as parameters

                        if (v.GetPlatform() == platform)
                        {
                            gamesByPlatform.Add(v);                                               //adds VideoGame v to games
                        }
                    }//End While

                    rdr.Close();                                                    //closes StreamReader rdr
                }//End using
            }//End Try
            catch (Exception e)                                                      //catches any exception that could be thrown
            {
                Console.WriteLine(e.Message);                                       //displays the error message
            }

            return gamesByPlatform;
        }

        /// <summary>
        /// Returns a list of all the games in the dataset
        /// </summary>
        /// <returns>List<VideoGame> games</returns>
        public List<VideoGame> CreateListOfGames()
        {
            List<VideoGame> games = new List<VideoGame>();                          //creates a new list of VideoGames called games
            string fileName = $@"..\..\..\DataFile\videogames.csv";                 //saves the relative path to videogames.csv as a string

            try                                                                     //try to validate contained code
            {
                using (StreamReader rdr = new StreamReader(fileName))               //opens videogames.csv using StreamReader
                {
                    string nextLine = rdr.ReadLine();                               //reads the first line in videogames.csv and saves it in nextLine as a string
                    while (rdr.Peek() != -1)                                        //while loop to make sure there is another line to read
                    {
                        nextLine = rdr.ReadLine();                                  //reads the next line in videogames.csv

                        string[] fields = nextLine.Split(',');                      //creates a string array to hold each individual value in nextLine

                        VideoGame v = new VideoGame(fields[0], fields[1], int.Parse(fields[2]), fields[3], fields[4], double.Parse(fields[5]), double.Parse(fields[6]), double.Parse(fields[7]), double.Parse(fields[8]), double.Parse(fields[9]));
                        //creates a new VideoGame v and uses the values stored in fields as parameters

                        
                            games.Add(v);                                               //adds VideoGame v to games
                        
                    }//End While

                    rdr.Close();                                                    //closes StreamReader rdr
                }//End using
            }//End Try
            catch (Exception e)                                                      //catches any exception that could be thrown
            {
                Console.WriteLine(e.Message);                                       //displays the error message
            }

            return games;
        }


    }
}