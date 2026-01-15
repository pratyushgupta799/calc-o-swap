using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

[System.Serializable]
public class WsResponse
{
    public string lobbyCode;
    public string wsUrl;
}

[System.Serializable]
public class WsMessage
{
    public string type;
    public string from;
    public Data data;

    [System.Serializable]
    public class Data
    {
        public Position position;
        public int level;
        public string name;
    }

    [System.Serializable]
    public class Position
    {
        public float x, y, z;
    }
}

public static class RPCUtilities
{
    private static WebSocket ws;
    
    public static string Sanitize(string input) {
        if (string.IsNullOrEmpty(input)) return input;
        return input.Replace("\u200B", "").Trim();
    }

    public static async Task<string> GetWsUrl(string lobbyCode, bool isHost, string playerName)
    {
        lobbyCode = Sanitize(lobbyCode);
        playerName = Sanitize(playerName);
        
        string url = isHost
            ? $"https://calc-backend.pratyushgupta04.workers.dev/?action=host&name={playerName}"
            : $"https://calc-backend.pratyushgupta04.workers.dev/?action=join&code={lobbyCode}&name={playerName}";
        
        using var req = UnityWebRequest.Get(url);
        await req.SendWebRequest();
        
        var res = JsonUtility.FromJson<WsResponse>(req.downloadHandler.text);
        
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            return null;
        }
        
        NetworkData.SetLobbyId(res.lobbyCode);
        return res.wsUrl;
    }

    public static async Task Connect(string lobbyCode, bool isHost, string playerName)
    {
        string wsUrl = await GetWsUrl(lobbyCode, isHost, playerName);
        
        ws = new WebSocket(wsUrl);
        
        ws.OnOpen += (_, _) => Debug.Log("WebSocket Connected");
        ws.OnMessage += (_, e) =>
        {
            MultiplayerHandler.Instance.HandleMessage(e.Data);
        };
        ws.OnClose += (_, _) => Debug.Log("WebSocket Closed");
        ws.OnError += (_, e) => Debug.LogError("WebSocket Error: " + e.Exception.Message);

        Debug.Log("WS URL: " + wsUrl);
        ws.Connect();
    }

    public static void SendUpdate(Vector3 pos, int level)
    {
        if (ws == null || !ws.IsAlive) return;

        WsMessage msg = new WsMessage
        {
            type = "update",
            data = new WsMessage.Data
            {
                position = new WsMessage.Position
                {
                    x = pos.x,
                    y = pos.y,
                    z = pos.z
                },
                level = level
            }
        };

        string json = JsonUtility.ToJson(msg);
        
        ws.Send(json);
        Debug.Log("Sent message to DO");
    }

    public static void Close()
    {
        ws?.Close();
        ws = null;
    }

}
