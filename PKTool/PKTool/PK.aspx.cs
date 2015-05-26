using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using PKTool;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
public partial class PK : System.Web.UI.Page
{
    private static String[] arrItems = { "Animals", "Nature", "Building", "Ships", "Artifacts" };
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="isAttackRandom"></param>
    /// <param name="isStealAuto"></param>
    /// <returns></returns>
    [WebMethod]
    public static string PlayPKOne(string accountName, Boolean isAttackRandom, Boolean isStealAuto)
    {
        String urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE?{1}", getKey(accountName), DateTime.Now.ToOADate().ToString());
        Dictionary<string, string> dicResult = new Dictionary<string, string>();
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
        dicResult.Add("wheelResult", wheelResult.ToString());
        dicResult.Add("playerInfo", playerInfo);
        dicResult.Add("cashKingInfo", cashKingInfo);
        dicResult.Add("imgIsLand", imgIsLand);
        dicResult.Add("stealInfo", stealInfo);
        return JsonConvert.SerializeObject(dicResult);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="isAttackRandom"></param>
    /// <param name="isStealAuto"></param>
    /// <returns></returns>
    [WebMethod]
    public static string PlayPK(string accountName, Boolean isAttackRandom, Boolean isStealAuto)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static string doPost(string uri, string parameters)
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
        //switch (accountName)
        //{
        //    case "0969023566":
        //        return "5541b1ac94c0430f980e0cc9";
        //    case "0912901720":
        //        return "5543d17a94c0420fd8b9d13f";
        //    case "0974260220":
        //        return "5541320594c0430ff49f3672";
        //    case "0944220487":
        //        return "55412a8394c04106c03394a7";
        //    case "nhothuy48cb":
        //        return "5532757194c0430b8c2c9973";
        //    case "lethiminhht":
        //        return "553d2d6994c03f0ed4dd44d8";
        //    case "boot01":
        //        return "​555f931e94c03f0b349da61b​";
        //    case "boot02":
        //        return "555fa55794c03f0ddc9d5fdd​";
        //    case "boot03":
        //        return "55602d9494c0430ff82f1548​";
        //    case "boot04":
        //        return "55602e3294c03f07a8ce46bf​";
        //    case "boot05":
        //        return "55602ec494c042099431d635​";
        //    case "boot06":
        //        return "55602f3494c0430ff059edf0";
        //    case "boot07":
        //        return "55602fcb94c042099431fc60​";
        //    case "boot08":
        //        return "556030c094c042099432210c​";
        //    case "boot09":
        //        return "5560313c94c03f07a8cec8af​";
        //    case "boot10":
        //        return "5560320094c0430ff05a626a​";
        //    case "boot11":
        //        return "5560327494c0430fe4f45751​";
        //    case "boot12":
        //        return "5560337494c0430ff82ff9d4​";
        //    case "boot13":
        //        return "556033e094c03f07a8cf3493​";
        //    case "boot14":
        //        return "5560347194c0430ff830210d​​";
        //    default:
        //        return "";
        //}
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
        var query = from item in lstKeys
                    orderby item.Level ascending
                    select item;
        List<Key>  lstKeysOrder = query.ToList();
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
                String tmpImgs = "<img src='imgs/{0}' height='64' width='64' />&nbsp;<img src='imgs/{1}' height='64' width='64' />&nbsp;<img src='imgs/{2}' height='64' width='64' />";
                int baseName = rank / 30 + 1;
                int baseNamePre = baseName > 1 ? baseName - 1 : baseName;
                int baseNameNext = baseName < 23 ? baseName + 1 : baseName;
                return String.Format(tmpImgs, String.Format("{0}.png", baseNamePre), String.Format("{0}.png", baseName), String.Format("{0}.png", baseNameNext));
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