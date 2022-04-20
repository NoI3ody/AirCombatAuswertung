using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace AirCombatAuswertung.Services
{
    public class SqliteDataService : IDataService
    {
        public const string DbName = "ACA.db";
        private IList<Class> _classes;
        public IList<Rule> _rules;
        private IList<Option> _options;

        public async Task InitializeDataAsync()
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await CreateClassTableAsync(db);
                await PopulateClassesAsync(db);

                await CreateRulesTableAsync(db);
                await PopulateRulesAsync(db);

                await CreateOptionTableAsync(db);
                await PopulateOptionsAsync(db);

                await CreatePilotTableAsync(db);

                await CreateStartlistTableAsync(db);

                await CreateScoresTableAsync(db);

                await CreateResultsTableAsync(db);

                await CreateResultsShowTableAsync(db);
            }

        }

        public async Task<IList<Class>> GetClassesAsync()
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetAllClassesAsync(db);
            }
        }
        public async Task<Class> GetClassByNameAsync(string name)
        {
            IList<Class> classes;
            using (var db = await GetOpenConnectionAsync())
            {
                classes = await GetAllClassesAsync(db);
            }
            return classes.FirstOrDefault(i => i.Name == name);
        }
        public async Task<int> GetPilotsPerClassAsync(Class c)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var value = await GetAllPilotsPerClass(db, c);
                return value;
            }
        }
        public async Task<Option> GetOptionByNameAsync(string name)
        {
            IList<Option> options;
            using (var db = await GetOpenConnectionAsync())
            {
                options = await GetAllOptionsAsync(db);
            }
            return options.FirstOrDefault(i => i.Name == name);
        }
        public async Task UpdateOptionByNameAsync(Option option)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await UpdateOptionsByNameAsync(db, option);
            }
        }
        public async Task<Pilot> AddPilotAsync(Pilot p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await InsertPilotAsync(db, p);
            }
        }
        public async Task<IList<Pilot>> GetPilotsAsync()
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetAllPilotsAsync(db);
            }
        }
        public async Task DeletePilotAsync(Pilot p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await RemovePilotAsync(db, (int)p.Startnr);
            }
        }
        public async Task UpdatePilotAsync(Pilot p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await UpdatePilotAsync(db, p);
            }
        }
        public async Task ClearStartlistAsync(Class c)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await ClearStartlistAsync(db, c);
            }
        }
        public async Task<Pilot> GetPilotAsync(int Startnr)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetPilotAsync(db, Startnr);
            }
        }
        public async Task CreateStartlistEntry(Startlist sl)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await InsertStartlistEntry(db, sl);
            }
        }
        public async Task<IList<int>> GetAllRoundsfromStartlist(Class c)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var list = await GetAllRoundsfromStartlist(db, c);
                return list;
            }
        }
        public async Task<IList<int>> GetAllHeatsfromStartlist(Class c, int round)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var list = await GetAllHeatsfromStartlist(db, c, round);
                return list;
            }
        }
        public async Task<IList<string>> GetAllPilotsfromStartlist(Class c, int round, int heat)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var list = await GetAllPilotsfromStartlist(db, c, round, heat);
                return list;
            }
        }
        public async Task<IList<string>> GetAllFrequencyfromStartlist(Class c, int round, int heat)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var list = await GetAllFrequencyfromStartlist(db, c, round, heat);
                return list;
            }
        }
        public async Task<IList<string>> GetAllJudgesfromStartlist(Class c, int round, int heat)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                var list = await GetAllJudgesfromStartlist(db, c, round, heat);
                return list;
            }
        }
        public async Task<Score> GetScoreAsync(Class c, int round, Pilot p, string category)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetScoreAsync(db, c, round, p, category);
            }
        }
        public async Task<Score> AddScoreAsync(Class c, int round, Pilot p, string category)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await AddScoreAsync(db, c, round, p, category);
            }
        }
        public async Task<Task> UpdateScoreAsync(Class c, int round, Pilot p, string category, int value)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await UpdateScoreAsync(db, c, round, p, category, value);
            }
            return Task.CompletedTask;
        }
        public async Task<IList<Score>> GetScoresforClassRoundPilot(int c, int round, int p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetScoresforClassRoundPilot(db, c, round, p);
            }
        }
        public async Task<Result> GetResultAsync(Class c, int round, Pilot p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await GetResultAsync(db, c, round, p);
            }
        }
        public async Task<Result> AddResultAsync(Class c, int round, Pilot p)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                return await AddResultAsync(db, c, round, p);
            }
        }
        public async Task<IList<Result>> GetAllResultsforClass(Class c)
        {
            IList<Result> results;
            using (var db = await GetOpenConnectionAsync())
            {
                results = await GetAllResultsforClass(db, c);
            }
            return results;
        }
        public async Task UpdateResultAsync(Result r)
        {
            using (var db = await GetOpenConnectionAsync())
            {
                await UpdateResultAsync(db, r);
            }
        }


        private async Task<SqliteConnection> GetOpenConnectionAsync()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(DbName, CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            var cn = new SqliteConnection($"Filename={dbPath}");
            cn.Open();
            return cn;
        }

        private async Task CreateClassTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Classes (Nr INTEGER NOT NULL UNIQUE, 
                                                                        Name TEXT NOT NULL, 
                                                                        Description TEXT NOT NULL, 
                                                                        Rules TEXT NOT NULL, 
                                                                        PRIMARY KEY(Nr))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateRulesTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Rules (Name TEXT NOT NULL,
                                                                      Value DOUBLE NOT NULL,
                                                                      Description TEXT NOT NULL)";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateOptionTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Options (Name NOT NULL UNIQUE, 
                                                                        Min INTEGER NOT NULL, 
                                                                        Max INTEGER NOT NULL, 
                                                                        Value TEXT NOT NULL, 
                                                                        PRIMARY KEY(Name))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreatePilotTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Pilots (Startnr INTEGER NOT NULL UNIQUE, 
                                                                       Firstname TEXT NOT NULL, 
                                                                       Lastname TEXT NOT NULL, 
                                                                       Nation TEXT NOT NULL, 
                                                                       Channel REAL NOT NULL, 
                                                                       FliesWW2 BOOL,
                                                                       FliesWW1 BOOL,
                                                                       FliesEPA BOOL,
                                                                       IsJudge BOOL, 
                                                                       PRIMARY KEY(Startnr AUTOINCREMENT))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateStartlistTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Startlist (Classnr INTEGER NOT NULL,
                                                                          Round INTEGER NOT NULL,
                                                                          Heat INTEGER NOT NULL,
                                                                          Pilotnr INTEGER NOT NULL,
                                                                          Judgenr INTEGER)";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateScoresTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Scores (Startnr INTEGER NOT NULL,
                                                                       Classnr INTEGER NOT NULL,
                                                                       Round INTEGER NOT NULL,
                                                                       Category TEXT NOT NULL,
                                                                       Value INTEGER,
                                                                       PRIMARY KEY(Startnr, Classnr, Round, Category))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateResultsTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS Results (Startnr INTEGER NOT NULL,
                                                                        Classnr INTEGER NOT NULL,
                                                                        Round INTEGER NOT NULL,
                                                                        SumModP INTEGER,
                                                                        Sum INTEGER,
                                                                        Total INTEGER,
                                                                        PRIMARY KEY(Startnr, Classnr, Round))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }
        private async Task CreateResultsShowTableAsync(SqliteConnection db)
        {
            string tableCommand = @"CREATE TABLE IF NOT EXISTS ResultsShow (Startnr INTEGER NOT NULL,
                                                                            Classnr INTEGER NOT NULL,
                                                                            Round1 INTEGER,
                                                                            Round2 INTEGER,
                                                                            Round3 INTEGER,
                                                                            Round4 INTEGER,
                                                                            Round5 INTEGER,
                                                                            Round6 INTEGER,
                                                                            Round7 INTEGER,
                                                                            Round8 INTEGER,
                                                                            Round9 INTEGER,
                                                                            Round10 INTEGER,
                                                                            Round11 INTEGER,
                                                                            Round12 INTEGER,
                                                                            Total INTEGER,
                                                                            PRIMARY KEY(Startnr, Classnr))";
            var createTable = new SqliteCommand(tableCommand, db);
            await createTable.ExecuteNonQueryAsync();
        }

        private async Task InsertClassAsync(SqliteConnection db, Class c)
        {
            var newClass = await db.QueryAsync<long>($@"INSERT INTO Classes({nameof(Class.Name)},
                                                                            {nameof(Class.Description)},
                                                                            {nameof(Class.Rules)}) 
                                                                            VALUES (@{nameof(Class.Name)},
                                                                            @{nameof(Class.Description)},
                                                                            @{nameof(Class.Rules)});
                                                                            SELECT last_insert_rowid()", c);
            c.Nr = (int)newClass.First();
        }
        private async Task InsertRuleAsync(SqliteConnection db, Rule r)
        {
            var newRule = await db.QueryAsync<Rule>($@"INSERT INTO Rules({nameof(Rule.Name)},
                                                                         {nameof(Rule.Value)},
                                                                         {nameof(Rule.Description)}) 
                                                                         VALUES (@{nameof(Rule.Name)},
                                                                         @{nameof(Rule.Value)},
                                                                         @{nameof(Rule.Description)});
                                                                         SELECT last_insert_rowid()", r);
        }
        private async Task InsertOptionAsync(SqliteConnection db, Option o)
        {
            var newOption = await db.QueryAsync<string>($@"INSERT INTO Options({nameof(Option.Name)},
                                                                               {nameof(Option.Min)},
                                                                               {nameof(Option.Max)},
                                                                               {nameof(Option.Value)}) 
                                                                               VALUES (@{nameof(Option.Name)},
                                                                               @{nameof(Option.Min)},
                                                                               @{nameof(Option.Max)},
                                                                               @{nameof(Option.Value)})", o);
        }
        private async Task<Pilot> InsertPilotAsync(SqliteConnection db, Pilot p)
        {
            var newPilot = await db.QueryAsync<Pilot>($@"INSERT INTO Pilots( {nameof(Pilot.Firstname)},
                                                                             {nameof(Pilot.Lastname)},
                                                                             {nameof(Pilot.Nation)},
                                                                             {nameof(Pilot.Channel)},
                                                                             {nameof(Pilot.FliesWW2)},
                                                                             {nameof(Pilot.FliesWW1)},
                                                                             {nameof(Pilot.FliesEPA)},
                                                                             {nameof(Pilot.IsJudge)}) 
                                                                             VALUES (@{nameof(Pilot.Firstname)},
                                                                             @{nameof(Pilot.Lastname)},
                                                                             @{nameof(Pilot.Nation)},
                                                                             @{nameof(Pilot.Channel)},
                                                                             @{nameof(Pilot.FliesWW2)},
                                                                             @{nameof(Pilot.FliesWW1)},
                                                                             @{nameof(Pilot.FliesEPA)},
                                                                             @{nameof(Pilot.IsJudge)})
                                                                             RETURNING *;", p);
            return (Pilot)newPilot.First();
        }
        private async Task RemovePilotAsync(SqliteConnection db, int startnr)
        {
            await db.DeleteAsync<Pilot>(new Pilot { Startnr = startnr });
        }
        private async Task UpdateOptionsByNameAsync(SqliteConnection db, Option o)
        {
            await db.QueryAsync(@"UPDATE Options SET Value = @Value WHERE Name = @Name", o);
        }
        private async Task UpdatePilotAsync(SqliteConnection db, Pilot p)
        {
            await db.QueryAsync(@"UPDATE Pilots SET Firstname = @Firstname,
                                                    Lastname = @Lastname,
                                                    Nation = @Nation,
                                                    Channel = @Channel,
                                                    FliesWW2 = @FliesWW2,
                                                    FliesWW1 = @FliesWW1,
                                                    FliesEPA = @FliesEPA,
                                                    IsJudge = @IsJudge
                                                    WHERE Startnr = @Startnr", p);
        }
        private async Task ClearStartlistAsync(SqliteConnection db, Class c)
        {
            await db.ExecuteAsync("DELETE FROM Startlist WHERE Classnr=@Nr", c);
        }
        private async Task<Pilot> GetPilotAsync(SqliteConnection db, int Startnr)
        {
            var pilot = await db.QuerySingleAsync<Pilot>(@"SELECT * FROM Pilots WHERE Startnr = @Startnr", new { Startnr });
            return pilot;
        }
        private async Task InsertStartlistEntry(SqliteConnection db, Startlist sl)
        {
            await db.QueryAsync($@"INSERT INTO Startlist({nameof(Startlist.Classnr)},
                                                         {nameof(Startlist.Round)},
                                                         {nameof(Startlist.Heat)},
                                                         {nameof(Startlist.Pilotnr)},
                                                         {nameof(Startlist.Judgenr)})
                                                         VALUES (@{nameof(Startlist.Classnr)},
                                                         @{nameof(Startlist.Round)},
                                                         @{nameof(Startlist.Heat)},
                                                         @{nameof(Startlist.Pilotnr)},
                                                         @{nameof(Startlist.Judgenr)})", sl);
        }
        private async Task<Score> GetScoreAsync(SqliteConnection db, Class c, int round, Pilot p, string category)
        {
            Score s = new Score();
            s.Startnr = p.Startnr;
            s.Classnr = c.Nr;
            s.Round = round;
            s.Category = category;
            var score = await db.QuerySingleOrDefaultAsync<Score>(@"SELECT * FROM Scores WHERE Startnr = @Startnr AND Classnr = @Classnr AND Round = @Round AND Category = @Category", s);
            if (score == null)
            {
                using (db)
                {
                    score = await AddScoreAsync(db, c, round, p, category);
                }
            }
            return score;
        }
        private async Task<Score> AddScoreAsync(SqliteConnection db, Class c, int round, Pilot p, string category)
        {
            Score s = new Score();
            s.Startnr = p.Startnr;
            s.Classnr = c.Nr;
            s.Round = round;
            s.Category = category;
            s.Value = 0;
            var newScore = await db.QueryAsync<Score>($@"INSERT INTO Scores( {nameof(Score.Startnr)},
                                                                             {nameof(Score.Classnr)},
                                                                             {nameof(Score.Round)},
                                                                             {nameof(Score.Category)},
                                                                             {nameof(Score.Value)}) 
                                                                             VALUES (@{nameof(Score.Startnr)},
                                                                             @{nameof(Score.Classnr)},
                                                                             @{nameof(Score.Round)},
                                                                             @{nameof(Score.Category)},
                                                                             @{nameof(Score.Value)})
                                                                             RETURNING *;", s);
            return (Score)newScore.First();
        }
        private async Task UpdateScoreAsync(SqliteConnection db, Class c, int round, Pilot p, string category, int value)
        {
            var score = new Score
            {
                Startnr = p.Startnr,
                Classnr = c.Nr,
                Round = round,
                Category = category,
                Value = value
            };
            await db.QueryAsync(@"UPDATE Scores SET Value = @Value
                                                    WHERE Startnr = @Startnr AND Classnr = @Classnr AND Round = @Round AND Category = @Category", score);
            await CalcScore(score);
        }
        private async Task<Result> GetResultAsync(SqliteConnection db, Class c, int round, Pilot p)
        {
            Result r = new Result();
            r.Startnr = p.Startnr;
            r.Classnr = c.Nr;
            r.Round = round;
            var result = await db.QuerySingleOrDefaultAsync<Result>(@"SELECT * FROM Results WHERE Startnr = @Startnr AND Classnr = @Classnr AND Round = @Round", r);
            if (result == null)
            {
                using (db)
                {
                    result = await AddResultAsync(db, c, round, p);
                }
            }
            return result;
        }
        private async Task<Result> AddResultAsync(SqliteConnection db, Class c, int round, Pilot p)
        {
            Result r = new Result();
            r.Startnr = p.Startnr;
            r.Classnr = c.Nr;
            r.Round = round;
            var newResult = await db.QueryAsync<Result>($@"INSERT INTO Results( {nameof(Result.Startnr)},
                                                                               {nameof(Result.Classnr)},
                                                                               {nameof(Result.Round)},
                                                                               {nameof(Result.SumModP)},
                                                                               {nameof(Result.Sum)},
                                                                               {nameof(Result.Total)}) 
                                                                               VALUES (@{nameof(Result.Startnr)},
                                                                               @{nameof(Result.Classnr)},
                                                                               @{nameof(Result.Round)},
                                                                               @{nameof(Result.SumModP)},
                                                                               @{nameof(Result.Sum)},
                                                                               @{nameof(Result.Total)})
                                                                               RETURNING *;", r);
            return (Result)newResult.First();
        }
        private async Task UpdateResultAsync(SqliteConnection db, Result r)
        {
            await db.QueryAsync(@"UPDATE Results SET SumModP = @SumModP, Sum = @Sum, Total = @Total
                                                    WHERE Startnr = @Startnr AND Classnr = @Classnr AND Round = @Round", r);
        }

        private async Task<IList<Class>> GetAllClassesAsync(SqliteConnection db)
        {
            var classes = await db.QueryAsync<Class>(@"SELECT Nr,Name,Description,Rules FROM Classes");
            return classes.ToList();
        }
        private async Task<IList<Rule>> GetAllRulesAsync(SqliteConnection db)
        {
            var rules = await db.QueryAsync<Rule>(@"SELECT Name,Value,Description FROM Rules");
            return rules.ToList();
        }
        private async Task<IList<Option>> GetAllOptionsAsync(SqliteConnection db)
        {
            var options = await db.QueryAsync<Option>(@"Select Name,Min,Max,Value FROM Options");
            return options.ToList();
        }
        private async Task<IList<Pilot>> GetAllPilotsAsync(SqliteConnection db)
        {
            var pilots = await db.QueryAsync<Pilot>(@"Select Startnr, Firstname, Lastname, Nation, Channel, FliesWW2, FliesWW1, FliesEPA, IsJudge FROM Pilots");
            return pilots.ToList();
        }
        private async Task<int> GetAllPilotsPerClass(SqliteConnection db, Class c)
        {
            var count = await db.ExecuteScalarAsync<int>("SELECT COUNT() FROM Pilots WHERE Flies" + c.Name + " ==1");
            return count;
        }
        private async Task<IList<int>> GetAllRoundsfromStartlist(SqliteConnection db, Class c)
        {
            Startlist sl = new Startlist();
            sl.Classnr = c.Nr;
            var list = await db.QueryAsync<int>(@"SELECT DISTINCT Round FROM Startlist WHERE Classnr = @Classnr", sl);
            return list.ToList();
        }
        private async Task<IList<int>> GetAllHeatsfromStartlist(SqliteConnection db, Class c, int round)
        {
            Startlist sl = new Startlist();
            sl.Classnr = c.Nr;
            sl.Round = round;
            var list = await db.QueryAsync<int>(@"SELECT DISTINCT Heat FROM Startlist WHERE Classnr = @Classnr AND Round = @Round", sl);
            return list.ToList();
        }
        private async Task<IList<string>> GetAllPilotsfromStartlist(SqliteConnection db, Class c, int round, int heat)
        {
            var list = new List<string>();
            Startlist sl = new Startlist();
            sl.Classnr = c.Nr;
            sl.Round = round;
            sl.Heat = heat;
            var listPnr = await db.QueryAsync<int>(@"SELECT Pilotnr FROM Startlist WHERE Classnr = @Classnr AND Round = @Round AND Heat = @Heat", sl);
            listPnr.ToList();
            foreach (int p in listPnr)
            {
                var pilot = await GetPilotAsync(db, p);
                string fullname = pilot.Firstname + " " + pilot.Lastname;
                list.Add(fullname);
            }
            return list.ToList();
        }
        private async Task<IList<string>> GetAllFrequencyfromStartlist(SqliteConnection db, Class c, int round, int heat)
        {
            var list = new List<string>();
            Startlist sl = new Startlist();
            sl.Classnr = c.Nr;
            sl.Round = round;
            sl.Heat = heat;
            var listPnr = await db.QueryAsync<int>(@"SELECT Pilotnr FROM Startlist WHERE Classnr = @Classnr AND Round = @Round AND Heat = @Heat", sl);
            listPnr.ToList();
            foreach (int p in listPnr)
            {
                var pilot = await GetPilotAsync(db, p);
                string freq = pilot.Channel.ToString();
                list.Add(freq);
            }
            return list.ToList();
        }
        private async Task<IList<string>> GetAllJudgesfromStartlist(SqliteConnection db, Class c, int round, int heat)
        {
            var list = new List<string>();
            Startlist sl = new Startlist();
            sl.Classnr = c.Nr;
            sl.Round = round;
            sl.Heat = heat;
            var listJnr = await db.QueryAsync<int>(@"SELECT Judgenr FROM Startlist WHERE Classnr = @Classnr AND Round = @Round AND Heat = @Heat", sl);
            listJnr.ToList();
            foreach (int j in listJnr)
            {
                var judge = await GetPilotAsync(db, j);
                string fullname = judge.Firstname + " " + judge.Lastname;
                list.Add(fullname);
            }
            return list.ToList();
        }
        private async Task<IList<Score>> GetScoresforClassRoundPilot(SqliteConnection db, int c, int round, int p)
        {
            var s = new Score();
            s.Startnr = p;
            s.Classnr = c;
            s.Round = round;
            var scores = await db.QueryAsync<Score>(@"SELECT * FROM Scores WHERE Startnr = @Startnr AND Classnr = @Classnr AND Round = @Round", s);
            return scores.ToList();
        }
        private async Task<IList<Result>> GetAllResultsforClass(SqliteConnection db, Class c)
        {
            var r = new Result();
            r.Classnr = c.Nr;
            var results = await db.QueryAsync<Result>(@"SELECT * FROM Results WHERE Classnr = @Classnr", r);
            return results.ToList();
        }

        private async Task PopulateClassesAsync(SqliteConnection db)
        {
            _classes = await GetAllClassesAsync(db);

            if (_classes.Count == 0)
            {
                var WW2 = new Class
                {
                    Nr = 1,
                    Name = "WW2",
                    Description = "Weltkrieg 2 (1939-1945)",
                    Rules = "2016 ACES int WWII Rules"
                };
                var WW1 = new Class
                {
                    Nr = 2,
                    Name = "WW1",
                    Description = "Weltkrieg 1 (1914-1918)",
                    Rules = "2016 ACES int WWI Rules"
                };
                var EPA = new Class
                {
                    Nr = 3,
                    Name = "EPA",
                    Description = "Electric \"Polysterene\" Aircombat",
                    Rules = "2006 CZE-ACES EPA Rules"
                };

                var classes = new List<Class>
                {
                    WW2,
                    WW1,
                    EPA
                };

                foreach (var c in classes)
                {
                    await InsertClassAsync(db, c);
                }

                _classes = await GetAllClassesAsync(db);
            }
        }
        private async Task PopulateRulesAsync(SqliteConnection db)
        {
            _rules = await GetAllRulesAsync(db);

            if (_rules.Count == 0)
            {
                double val1 = 1;
                double val2 = 3;
                var Flighttime = new Rule
                {
                    Name = "Time",
                    Value = val1 / val2,
                    Description = $"Flugzeit pro {val2}sec wird {val1} Punkt vergeben"
                };
                val1 = 138;
                var maxFlighttimepoints = new Rule
                {
                    Name = "maxTime",
                    Value = val1,
                    Description = $"max. Flugzeitpunkte auf {val1} begrenzt"
                };
                val1 = 100;
                var Cut = new Rule
                {
                    Name = "Cut",
                    Value = val1,
                    Description = $"pro Cut werden {val1} Punkte vergeben"
                };
                val1 = 50;
                var Stream = new Rule
                {
                    Name = "StreamerOK",
                    Value = val1,
                    Description = $"für StreamerOK werden {val1} Punkte vergeben"
                };
                val1 = 10;
                var minTimeStreamerOK = new Rule
                {
                    Name = "minTimeStreamerOK",
                    Value = val1,
                    Description = $"erst wenn die Flugzeit mindestens {val1}sec beträgt, wird StreamerOK vergeben"
                };
                val1 = -200;
                var SafetyCross = new Rule
                {
                    Name = "Safetyline Cross",
                    Value = val1,
                    Description = $"wird die Safetylineüberfolgen werden {val1} Punkte abgezogen"
                };
                val1 = -50;
                val2 = 30;
                var NonE = new Rule
                {
                    Name = "Non Engagement",
                    Value = val1,
                    Description = $"hält sich der Pilot für {val2}sec aus dem Kampf, werden {val1} Punkte abgezogen"
                };
                val1 = 0;
                var E25 = new Rule
                {
                    Name = "WW2 2,5ccm Engine",
                    Value = val1,
                    Description = $"Fliegt der Pilot eine 2,5ccm Maschine in WW2 werden {val1} Punkte vergeben"
                };
                val1 = 20;
                var FourStroke = new Rule
                {
                    Name = "WW1 Four-Stroke",
                    Value = val1,
                    Description = $"Fliegt der Pilot einen 4-Taktmotor in WW1 werden {val1} Punkte vergeben"
                };
                val1 = 50;
                var WW1Bipl = new Rule
                {
                    Name = "WW1 Biplane",
                    Value = val1,
                    Description = $"Fliegt der Pilot einen Mehrdecker in WW1 werden {val1} Punkte vergeben"
                };
                val1 = 30;
                var WingStruct = new Rule
                {
                    Name = "WW1 WingStructure",
                    Value = val1,
                    Description = $"Fliegt der Pilot einen Flügel mit Holzspanten in WW1 werden {val1} Punkte vergeben"
                };
                val1 = 10;
                var Wire = new Rule
                {
                    Name = "WW1 Wires and Struts",
                    Value = val1,
                    Description = $"Fliegt der Pilot ein Modell mit Verspannungen in WW1 werden {val1} Punkte vergeben"
                };
                val1 = 50;
                var GroundTargets = new Rule
                {
                    Name = "WW1 Ground Targets",
                    Value = val1,
                    Description = $"pro Bodenziel in WW1 werden {val1} Punkte vergeben"
                };
                val1 = 100;
                var maxModPoints = new Rule
                {
                    Name = "WW1 max. Modelpoints",
                    Value = val1,
                    Description = $"max. Modelpunkte in WW1 auf {val1} Punkte begrenzt"
                };
                val1 = 50;
                var ScStart = new Rule
                {
                    Name = "WW1 Scale Start",
                    Value = val1,
                    Description = $"Startet der Pilot in WW1 vom Boden werden {val1} Punkte vergeben"
                };
                val1 = 50;
                var ScLand = new Rule
                {
                    Name = "WW1 Scale Landing",
                    Value = val1,
                    Description = $"Landet der Pilot in WW1 im Landefeld ohne Zwischenlandungen werden {val1} Punkte vergeben"
                };
                val1 = 25;
                var DoublE = new Rule
                {
                    Name = "EPA Double Engine",
                    Value = val1,
                    Description = $"Fliegt der Pilot ein Modell mit mehreren Motoren in EPA werden {val1} Punkte vergeben"
                };
                val1 = 25;
                var EPABipl = new Rule
                {
                    Name = "EPA Biplane",
                    Value = val1,
                    Description = $"Fliegt der Pilot einen Mehrdecker in EPA werden {val1} Punkte vergeben"
                };
                val1 = -25;
                var FlatFuse = new Rule
                {
                    Name = "EPA Flat Fuselage",
                    Value = val1,
                    Description = $"Fliegt der Pilot einen Silouttenrumpf in EPA werden {val1} Punkte abgezogen"
                };

                var rules = new List<Rule>
                {
                    Flighttime,
                    maxFlighttimepoints,
                    Cut,
                    Stream,
                    minTimeStreamerOK,
                    SafetyCross,
                    NonE,
                    E25,
                    FourStroke,
                    WW1Bipl,
                    WingStruct,
                    Wire,
                    GroundTargets,
                    maxModPoints,
                    ScStart,
                    ScLand,
                    DoublE,
                    EPABipl,
                    FlatFuse
                };

                foreach (var r in rules)
                {
                    await InsertRuleAsync(db, r);
                }

                _rules = await GetAllRulesAsync(db);
            }
        }
        private async Task PopulateOptionsAsync(SqliteConnection db)
        {
            _options = await GetAllOptionsAsync(db);

            if (_options.Count == 0)
            {
                Option Location = new Option
                {
                    Name = "Location",
                    Min = 0,
                    Max = 50,
                    Value = ""
                };
                Option Date = new Option
                {
                    Name = "Date",
                    Min = (int)DateTimeOffset.MinValue.Ticks,
                    Max = (int)DateTimeOffset.MaxValue.Ticks,
                    Value = DateTimeOffset.Now.Date.ToShortDateString()
                };
                var options = new List<Option>
                {
                    Location,
                    Date
                };
                foreach (Class c in await GetAllClassesAsync(db))
                {
                    Option Rounds = new Option
                    {
                        Name = "AnzRnd" + c.Nr,
                        Min = 0,
                        Max = 10,
                        Value = "0"
                    };
                    options.Add(Rounds);
                    Option Final = new Option
                    {
                        Name = "Finale" + c.Nr,
                        Min = 0,
                        Max = 1,
                        Value = false.ToString()
                    };
                    options.Add(Final);
                    Option FinalPilots = new Option
                    {
                        Name = "AnzFin" + c.Nr,
                        Min = 2,
                        Max = 30,
                        Value = "7"
                    };
                    options.Add(FinalPilots);
                    Option SemiFinal = new Option
                    {
                        Name = "Semi" + c.Nr,
                        Min = 0,
                        Max = 1,
                        Value = false.ToString()
                    };
                    options.Add(SemiFinal);
                    Option SemiFinalPilots = new Option
                    {
                        Name = "AnzSemi" + c.Nr,
                        Min = 2,
                        Max = 45,
                        Value = "21"
                    };
                    options.Add(SemiFinalPilots);
                    Option Heats = new Option
                    {
                        Name = "AnzHeats" + c.Nr,
                        Min = 1,
                        Max = 50,
                        Value = ""
                    };
                    options.Add(Heats);
                    Option IgnoredoubleFrequency = new Option
                    {
                        Name = "IgnoredoubleFrequency" + c.Nr,
                        Min = 0,
                        Max = 1,
                        Value = false.ToString()
                    };
                    options.Add(IgnoredoubleFrequency);
                    Option GeneratewithoutJudges = new Option
                    {
                        Name = "GeneratewithoutJudges" + c.Nr,
                        Min = 0,
                        Max = 1,
                        Value = false.ToString()
                    };
                    options.Add(GeneratewithoutJudges);
                }
                foreach (var o in options)
                {
                    await InsertOptionAsync(db, o);
                }
                _options = await GetAllOptionsAsync(db);
            }
        }


        #region Calculations
        private async Task CalcScore(Score s)
        {
            Result r = new Result();
            r.Startnr = s.Startnr;
            r.Classnr = s.Classnr;
            r.Round = s.Round;
            r.SumModP = 0;
            r.Sum = 0;

            var scores = await GetScoresforClassRoundPilot(r.Classnr, r.Round, (int)r.Startnr);
            //Wenn noch nicht alle Klassen angelegt wurden berechnung verzögern!
            if (r.Classnr == 1 && scores.Count != 6) return;
            else if (r.Classnr == 2 && scores.Count != 12) return;
            else if (r.Classnr == 3 && scores.Count != 8) return;

            foreach (var score in scores)
            {
                // Wenn Flugzeit = 0, keine Punkte vergeben, außer Safetyline!.
                if (score.Category == "Time" && score.Value == 0)
                {
                    if (scores.Where(s => s.Category == "Safetyline Cross").FirstOrDefault().Value != 0)
                    {
                        r.Sum = (int)_rules.Where(sl => sl.Name == "Safetyline Cross").FirstOrDefault().Value;
                        break;
                    }
                    r.Sum = 0;
                    break;
                }
                var rule = _rules.Where(r => r.Name == score.Category).FirstOrDefault();
                int zwischenSumme = Convert.ToInt32(Math.Ceiling(score.Value * rule.Value));
                //Max. Time begrenzen
                if (score.Category == "Time")
                {
                    var rule2 = _rules.Where(r => r.Name == "maxTime").FirstOrDefault();
                    if (zwischenSumme > rule2.Value)
                    {
                        zwischenSumme = (int)rule2.Value;
                    }
                }
                //Streamer OK erst nach min. Flugzeit
                else if (score.Category == "StreamerOK")
                {
                    var rule3 = _rules.Where(r => r.Name == "minTimeStreamerOK").FirstOrDefault();
                    if (scores.Where(s => s.Category == "Time").FirstOrDefault().Value < rule3.Value)
                    {
                        zwischenSumme = 0;
                    }
                }
                //Modellpunkte berechnen WW1
                if (score.Category == "WW1 Four-Stroke" || score.Category == "WW1 Biplane" || score.Category == "WW1 WingStructure" || score.Category == "WW1 Wires and Struts")
                {
                    r.SumModP += zwischenSumme;
                }
                //Modellpunkte berechnen EPA
                if (score.Category == "EPA Double Engine" || score.Category == "EPA Biplane" || score.Category == "EPA Flat Fuselage")
                {
                    r.SumModP += zwischenSumme;
                }
                r.Sum += zwischenSumme;
            }
            //Limitiere Modellpunkte in WW1
            if (r.Classnr == 2)
            {
                if (r.SumModP > _rules.Where(r => r.Name == "WW1 max. Modelpoints").FirstOrDefault().Value)
                {
                    int diff = (int)_rules.Where(r => r.Name == "WW1 max. Modelpoints").FirstOrDefault().Value - r.SumModP;
                    r.SumModP = (int)_rules.Where(r => r.Name == "WW1 max. Modelpoints").FirstOrDefault().Value;
                    r.Sum -= Math.Abs(diff);
                }
            }
            if (r.Round == 1)
            {
                r.Total = r.Sum;
            }
            else if (r.Round > 1 && r.Round <= 10)
            {
                Class c = new Class { Nr = r.Classnr };
                Pilot p = new Pilot { Startnr = r.Startnr };
                var r2 = await GetResultAsync(c, r.Round - 1, p);
                r.Total = r.Sum + r2.Total;
            }
            else if (r.Round > 10)
            {
                Class c = new Class { Nr = r.Classnr };
                Pilot p = new Pilot { Startnr = r.Startnr };

                //Hole die max. Runde
                var round = await GetOptionByNameAsync("AnzRnd" + c.Nr);
                if (r.Round == 12 && bool.Parse(GetOptionByNameAsync("Semi" + c.Nr).Result.Value))
                {
                    var r3 = await GetResultAsync(c, 11, p);
                    r.Total = r.Sum + r3.Total;
                }
                else
                {
                    var r4 = await GetResultAsync(c, int.Parse(round.Value), p);
                    r.Total = r.Sum + r4.Total;
                }                
            }
            await UpdateResultAsync(r);
        }
        #endregion
    }
}