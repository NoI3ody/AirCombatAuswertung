using AirCombatAuswertung.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirCombatAuswertung.Interfaces
{
    public interface IDataService
    {
        Task InitializeDataAsync();
        Task<IList<Class>> GetClassesAsync();
        Task<Class> GetClassByNameAsync(string name);
        Task<int> GetPilotsPerClassAsync(Class c);
        Task<IList<Pilot>> GetPilotsAsync();
        Task DeletePilotAsync(Pilot p);
        Task<Option> GetOptionByNameAsync(string name);
        Task UpdateOptionByNameAsync(Option option);
        Task<Pilot> AddPilotAsync(Pilot p);
        Task UpdatePilotAsync(Pilot p);
        Task ClearStartlistAsync(Class c);
        Task<Pilot> GetPilotAsync(int startnr);
        Task CreateStartlistEntry(Startlist sl);
        Task<IList<int>>GetAllRoundsfromStartlist(Class c);
        Task<IList<int>> GetAllHeatsfromStartlist(Class c, int round);
        Task<IList<string>> GetAllPilotsfromStartlist(Class c, int round, int heat);
        Task<IList<string>> GetAllFrequencyfromStartlist(Class c, int round, int heat);
        Task<IList<string>> GetAllJudgesfromStartlist(Class c, int round, int heat);
        Task<Score> GetScoreAsync(Class c, int round, Pilot p, string category);
        Task<Task> UpdateScoreAsync(Class c, int round, Pilot p, string category,int value);
        Task<IList<Score>> GetScoresforClassRoundPilot(int c, int round, int p);
        Task<Result> GetResultAsync(Class c,int round, Pilot p);
        Task<IList<Result>> GetAllResultsforClass(Class c);
    }
}
