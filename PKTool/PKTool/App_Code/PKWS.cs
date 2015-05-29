using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PKTool;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for PK
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PKWS : System.Web.Services.WebService {
    private static String[] arrItems = { "Animals", "Nature", "Building", "Ships", "Artifacts" };
    private const Int32 RANKPOINT_STEAL = 200;
    public PKWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="isAttackRandom"></param>
    /// <param name="isStealAuto"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PlayPKOne(string accountName, Boolean isAttackRandom, Boolean isStealAuto)
    {
        try
        {
            String urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE?{1}", getKey(accountName), DateTime.Now.ToOADate().ToString());
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            String retWheel = doGet(urlWheel);
            JToken data = JObject.Parse(retWheel);
            int wheelResult = Convert.ToInt16(data["WheelResult"]);
            int spins = Convert.ToInt16(data["PlayerState"]["Spins"]);
            String playerInfo = String.Format("Rank:{0} Shields:{1} Spins:{2} Cash:{3} NextSpin: {4}", data["PlayerState"]["RankPoints"], data["PlayerState"]["Shields"], data["PlayerState"]["Spins"], Convert.ToInt64(data["PlayerState"]["Cash"]).ToString("#,#", CultureInfo.InvariantCulture), getTimes(Convert.ToInt32(data["NextSpinClaimSeconds"])));
            String cashKingInfo = String.Format("Name:{1} Rank:{2} Cash:{3}", data["PlayerState"]["CashKing"]["FBID"], data["PlayerState"]["CashKing"]["Name"], data["PlayerState"]["CashKing"]["RankPoints"], Convert.ToInt64(data["PlayerState"]["CashKingCash"]).ToString("#,#", CultureInfo.InvariantCulture));
            String imgIsLand = "";
            String stealInfo = "";
            if (wheelResult == 6)
            {
                stealInfo = steal(accountName, data, isStealAuto);
                imgIsLand = getIsLandImgs(Convert.ToInt32(data["PlayerState"]["CashKing"]["RankPoints"]));
            }
            if (isAttackRandom && wheelResult == 7) attackRandom(accountName, data);
            dicResult.Add("wheelResult", wheelResult.ToString());
            dicResult.Add("playerInfo", playerInfo);
            dicResult.Add("cashKingInfo", cashKingInfo);
            dicResult.Add("imgIsLand", imgIsLand);
            dicResult.Add("stealInfo", stealInfo);
            return JsonConvert.SerializeObject(dicResult);
        }
        catch
        {
            return String.Empty;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="isAttackRandom"></param>
    /// <param name="isStealAuto"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PlayPK(string accountName, Boolean isAttackRandom, Boolean isStealAuto)
    {
        try
        {
            //http://prod.cashkinggame.com/CKService.svc/v3.0/spin/wheel/?42147.0388165131
            //{"ExpectedResult":"NONE","secretKey":"55412a8394c04106c03394a7","sessionToken":"555f6c4e94c03f0b349a5a3b","businessToken":"Abw_vAiiKYYw_uj6"}
            String urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE?{1}", getKey(accountName), DateTime.Now.ToOADate().ToString());
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            while (true)
            {
                String retWheel = doGet(urlWheel);
                JToken data = JObject.Parse(retWheel);
                int wheelResult = Convert.ToInt16(data["WheelResult"]);
                int spins = Convert.ToInt16(data["PlayerState"]["Spins"]);
                Console.WriteLine(data["PlayerState"].ToString());
                String playerInfo = String.Format("Rank:{0} Shields:{1} Spins:{2} Cash:{3} NextSpin: {4}", data["PlayerState"]["RankPoints"], data["PlayerState"]["Shields"], data["PlayerState"]["Spins"], Convert.ToInt64(data["PlayerState"]["Cash"]).ToString("#,#", CultureInfo.InvariantCulture), getTimes(Convert.ToInt32(data["NextSpinClaimSeconds"])));
                String cashKingInfo = String.Format("Name:{1} Rank:{2} Cash:{3}", data["PlayerState"]["CashKing"]["FBID"], data["PlayerState"]["CashKing"]["Name"], data["PlayerState"]["CashKing"]["RankPoints"], Convert.ToInt64(data["PlayerState"]["CashKingCash"]).ToString("#,#", CultureInfo.InvariantCulture));
                String imgIsLand = "";
                String stealInfo = "";
                if (wheelResult == 6)
                {
                    stealInfo = steal(accountName, data, isStealAuto);
                    imgIsLand = getIsLandImgs(Convert.ToInt32(data["PlayerState"]["CashKing"]["RankPoints"]));
                }
                if (isAttackRandom && wheelResult == 7) attackRandom(accountName, data);
                if (spins == 0 || (wheelResult == 6 && !isStealAuto) || (wheelResult == 7 && !isAttackRandom))
                {
                    dicResult.Add("wheelResult", wheelResult.ToString());
                    dicResult.Add("playerInfo", playerInfo);
                    dicResult.Add("cashKingInfo", cashKingInfo);
                    dicResult.Add("imgIsLand", imgIsLand);
                    dicResult.Add("stealInfo", stealInfo);
                    return JsonConvert.SerializeObject(dicResult);
                };
            }
        }
        catch
        {
            return String.Empty;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private static string doPost(string uri, string parameters)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            System.Net.ServicePointManager.Expect100Continue = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.UTF8.GetBytes(parameters);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    /// <summary>
    /// doGet
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string doGet(string url)
    {
        try
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string data = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static string getKey(string accountName)
    {
        return HttpUtility.UrlEncode(accountName).Replace("%e2", "").Replace("%80", "").Replace("%8b", "");
    }
    /// <summary>
    /// Steal
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private static string steal(string accountName, JToken data, Boolean isStealAuto)
    {
        int index = 0;
        List<Key> lstKeys = new List<Key>();
        foreach (JToken child in data["StealIslands"])
        {
            Key keyItem = new Key();
            keyItem.Index = index;
            keyItem.Level = Convert.ToInt32(child["Level"]);
            keyItem.Data = child.ToString();
            lstKeys.Add(keyItem);
            index = index + 1;
        }
        //
        Int32 rank = Convert.ToInt32(data["PlayerState"]["CashKing"]["RankPoints"]);
        //
        List<Key> lstKeysOrder = new List<Key>();
        if (rank >= RANKPOINT_STEAL)
        {
            //descending
            var query = from item in lstKeys
                        orderby item.Level descending
                        select item;
            lstKeysOrder = query.ToList();
        }
        else
        {
            //ascending
            var query = from item in lstKeys
                        orderby item.Level ascending
                        select item;
            lstKeysOrder = query.ToList();
        }
        //{"StealIndex":0,"FriendFBIDs":["10153223750579791","917613761592912"],"secretKey":"55412a8394c04106c03394a7","sessionToken":"555f70a594c03f0e3cb4d193","businessToken":"Abw_vAiiKYYw_uj6"}
        if (isStealAuto)
        {
            try
            {
                //http://prod.cashkinggame.com/CKService.svc/v3.0/attack/steal/?42147.0371009402
                //{"StealIndex":0,"FriendFBIDs":["10153223750579791","917613761592912"],"secretKey":"55412a8394c04106c03394a7","sessionToken":"555f6c4e94c03f0b349a5a3b","businessToken":"Abw_vAiiKYYw_uj6"}
                //{"StealIndex":2,"FriendFBIDs":["10153223750579791","978563412155163","981196078558590","995349240476870","1086644358016038","1088659451150514","917613761592912","1089997004360590","811271685595453","820464561352670","778260808955490","542868799184255","562560950552588","553930484749977","357830687757305","1652560578307068","1613600798926585"],"secretKey":"55596a2194c0430b70eec43f","sessionToken":"555f74a694c03f0b349b2e1b","businessToken":"AbwqDLM79jjg6yct"}
                Dictionary<string, object> dicResult = new Dictionary<string, object>();
                dicResult.Add("StealIndex", lstKeysOrder[0].Index);
                dicResult.Add("FriendFBIDs", "[]");
                dicResult.Add("secretKey", getKey(accountName));
                String urlSteal = "http://prod.cashkinggame.com/CKService.svc/v2/attack/steal/?" + DateTime.Now.ToOADate().ToString();
                doPost(urlSteal, JsonConvert.SerializeObject(dicResult));
            }
            catch
            {

            }
        }
        return String.Format("Steal: {0} No-Leve: 1-{1} 2-{2} 3-{3}", lstKeysOrder[0].Index + 1, lstKeys[0].Level, lstKeys[1].Level, lstKeys[2].Level);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private static string attackRandom(string accountName, JToken data)
    {
        List<Item> randomAttackIslandItems = getRandomAttackIslandItems(data);
        String itemAttackName = "";
        if (randomAttackIslandItems.Count > 0)
        {
            itemAttackName = randomAttackIslandItems[0].Name;
        }
        else
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int idxItem = rnd.Next(0, 4);
            itemAttackName = arrItems[idxItem];
        }
        String url = String.Format("http://prod.cashkinggame.com/CKService.svc/attack/random/{0}/{1}{2}", getKey(accountName), itemAttackName, "?" + DateTime.Now.ToOADate().ToString());
        return doGet(url);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private static List<Item> getRandomAttackIslandItems(JToken data)
    {
        List<Item> randomAttackIslandItems = new List<Item>();
        foreach (JToken child in data["RandomAttackIsland"])
        {
            var propertyChild = child as JProperty;
            if (arrItems.Contains(propertyChild.Name + "s"))
            {
                Item item = new Item();
                item.Name = propertyChild.Name + "s";
                foreach (JToken grandChild in propertyChild.Value)
                {
                    var property = grandChild as JProperty;
                    switch (property.Name.ToUpper())
                    {
                        case "ISDAMAGED":
                            item.Isdamaged = Convert.ToBoolean(property.Value);
                            break;
                        case "LEVEL":
                            item.Level = Convert.ToInt16(property.Value);
                            break;
                        default:
                            break;
                    }
                }
                randomAttackIslandItems.Add(item);
            }
        }
        var query = from item in randomAttackIslandItems
                    orderby item.Level descending, item.Isdamaged descending
                    select item;
        return query.ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    private static string getIsLandImgs(Int32 rank)
    {
        try
        {
            //String tmpImgs = "<img src='imgs/{0}' height='64' width='64' />&nbsp;<img src='imgs/{1}' height='64' width='64' />&nbsp;<img src='imgs/{2}' height='64' width='64' />";
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            int baseName = rank / 30 + 1;
            int baseNamePre = baseName > 1 ? baseName - 1 : baseName;
            int baseNameNext = baseName < 23 ? baseName + 1 : baseName;
            dicResult.Add("img0", baseNamePre.ToString());
            dicResult.Add("img1", baseName.ToString());
            dicResult.Add("img2", baseNameNext.ToString());
            return JsonConvert.SerializeObject(dicResult);
        }
        catch
        {
        }
        return "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private static string getTimes(Int32 time)
    {
        //00:00
        int min = (int)time / 60;
        int second = time - min * 60;
        return String.Format("{0}:{1}", min.ToString("00"), second.ToString("00"));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    private static string view(String fid)
    {
        String urlView = "http://prod.cashkinggame.com/CKService.svc/v3.0/island/view/friend/?" + DateTime.Now.ToOADate().ToString();
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        dicResult.Add("FriendScopedId", fid);
        String dataView = JsonConvert.SerializeObject(dicResult);
        JToken data = JObject.Parse(doPost(urlView, dataView));
        return collapseSpaces(data["WantedIsland"].ToString().Replace(System.Environment.NewLine, string.Empty));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string collapseSpaces(string value)
    {
        return Regex.Replace(value, @"\s+", " ");
    }
}
