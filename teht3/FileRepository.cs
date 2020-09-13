using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class FileRepository : IRepository
{
    public Task<Player> Get(Guid id)
    {
        Player player = new Player();
        bool GuidFound = false, NameEmpty = true, ScoreEmpty = true, LevelEmpty = true, IsBannedEmpty = true, CreationTimeEmpty = true;

        string[] lines = File.ReadAllLines("game-dev.txt");
        foreach (string line in lines)
        {
            if (line.StartsWith("["))
            {
                if (line.Contains(id.ToString()))
                {
                    player.Id = id;
                    GuidFound = true;
                }
            }
            if (GuidFound && NameEmpty && line.StartsWith("Name"))
            {
                player.Name = line.Split("=")[1];
                NameEmpty = false;
            }

            if (GuidFound && ScoreEmpty && line.StartsWith("Score"))
            {
                player.Score = int.Parse(line.Split("=")[1]);
                ScoreEmpty = false;
            }

            if (GuidFound && LevelEmpty && line.StartsWith("Level"))
            {
                player.Level = int.Parse(line.Split("=")[1]);
                LevelEmpty = false;
            }

            if (GuidFound && IsBannedEmpty && line.StartsWith("IsBanned"))
            {
                player.IsBanned = bool.Parse(line.Split("=")[1]);
                IsBannedEmpty = false;
            }

            if (GuidFound && CreationTimeEmpty && line.StartsWith("CreationTime"))
            {
                player.CreationTime = DateTime.Parse(line.Split("=")[1]);
                CreationTimeEmpty = false;
            }
        }

        return Task.Run(() => { return player; });
    }

    public Task<Player[]> GetAll()
    {
        List<Player> players = new List<Player>();

        string[] lines = File.ReadAllLines("game-dev.txt");
        foreach (string line in lines)
        {
            if (line.StartsWith("["))
                players.Add(new Player() { Id = Guid.Parse(line.Replace("[", "").Replace("]", "")) });

            if (line.StartsWith("Name"))
                players.Last().Name = line.Split("=")[1];

            if (line.StartsWith("Score"))
                players.Last().Score = int.Parse(line.Split("=")[1]);

            if (line.StartsWith("Level"))
                players.Last().Level = int.Parse(line.Split("=")[1]);

            if (line.StartsWith("IsBanned"))
                players.Last().IsBanned = bool.Parse(line.Split("=")[1]);

            if (line.StartsWith("CreationTime"))
                players.Last().CreationTime = DateTime.Parse(line.Split("=")[1]);
        }

        return Task.Run(() => { return players.ToArray(); });
    }

    public Task<Player> Create(Player player)
    {
        if (!File.Exists("game-dev.txt")) return null;

        FileStream file = File.Open("game-dev.txt", FileMode.Append);

        file.Write(new String(("[" + player.Id.ToString() + "]\n")).Select(c => (byte)c).ToArray());
        file.Write(new String(("Name=" + player.Name + "\n")).Select(c => (byte)c).ToArray());
        file.Write(new String(("Score=" + player.Score.ToString() + "\n")).Select(c => (byte)c).ToArray());
        file.Write(new String(("Level=" + player.Level.ToString() + "\n")).Select(c => (byte)c).ToArray());
        file.Write(new String(("IsBanned=" + player.IsBanned.ToString() + "\n")).Select(c => (byte)c).ToArray());
        file.Write(new String(("CreationTime=" + player.CreationTime.ToString() + "\n")).Select(c => (byte)c).ToArray());
        file.Close();

        return Task.Run(() => { return player; });
    }

    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Player ret = new Player();

        Guid CurrentGuid = Guid.Empty;

        var lines = File.ReadAllLines("game-dev.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("["))
            {
                CurrentGuid = Guid.Parse(lines[i].Replace("[", "").Replace("]", ""));
            }
            if (lines[i].Contains(id.ToString()))
            {
                ret.Id = CurrentGuid;
            }

            if (CurrentGuid.Equals(id) && lines[i].StartsWith("Name"))
            {
                ret.Name = lines[i].Split("=")[1];
            }

            if (CurrentGuid.Equals(id) && lines[i].StartsWith("Score"))
            {
                lines[i] = "Score=" + player.Score.ToString();
                ret.Score = int.Parse(lines[i].Split("=")[1]);
            }

            if (CurrentGuid.Equals(id) && lines[i].StartsWith("Level"))
            {
                ret.Level = int.Parse(lines[i].Split("=")[1]);
            }

            if (CurrentGuid.Equals(id) && lines[i].StartsWith("IsBanned"))
            {
                ret.IsBanned = bool.Parse(lines[i].Split("=")[1]);
            }

            if (CurrentGuid.Equals(id) && lines[i].StartsWith("CreationTime"))
            {
                ret.CreationTime = DateTime.Parse(lines[i].Split("=")[1]);
            }

        }

        File.WriteAllLines("game-dev.txt", lines);
        return Task.Run(() => { return ret; });
    }

    public Task<Player> Delete(Guid id)
    {
        Player ret = new Player();

        Guid CurrentGuid = Guid.Empty;

        var lines = File.ReadAllLines("game-dev.txt").ToList<string>();
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith("["))
            {
                CurrentGuid = Guid.Parse(lines[i].Replace("[", "").Replace("]", ""));
                if (CurrentGuid == id)
                {
                    lines.RemoveAt(i);
                }
            }

            if (CurrentGuid == id && lines[i].StartsWith("Name"))
            {
                ret.Name = lines[i].Split("=")[1];
                lines.RemoveAt(i);
            }

            if (CurrentGuid == id && lines[i].StartsWith("Score"))
            {
                ret.Score = int.Parse(lines[i].Split("=")[1]);
                lines.RemoveAt(i);
            }

            if (CurrentGuid == id && lines[i].StartsWith("Level"))
            {
                ret.Level = int.Parse(lines[i].Split("=")[1]);
                lines.RemoveAt(i);
            }

            if (CurrentGuid == id && lines[i].StartsWith("IsBanned"))
            {
                ret.IsBanned = bool.Parse(lines[i].Split("=")[1]);
                lines.RemoveAt(i);
            }

            if (CurrentGuid == id && lines[i].StartsWith("CreationTime"))
            {
                ret.CreationTime = DateTime.Parse(lines[i].Split("=")[1]);
                lines.RemoveAt(i);
            }
        }

        File.WriteAllLines("game-dev.txt", lines);
        return Task.Run(() => { return ret; });
    }
}