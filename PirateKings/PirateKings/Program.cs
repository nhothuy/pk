using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PirateKings
{
    class Program
    {
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
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //Application.EnableVisualStyles();
        //Application.SetCompatibleTextRenderingDefault(false);
        //Application.Run(new frmMain());
        //getTimes(100);
        //String retWheel = spinWheel();
        //JToken data = JObject.Parse("{'DidIncrementCacheIndex':false,'ErrorCode':100,'IncentivizedResults':null,'Island':null,'News':null,'NextSpinClaimSeconds':3280,'PlayerState':{'CacheIndex':2348,'Cash':568700,'CashKing':{'Avatar':0,'FBID':'372507646282658','Flag':12,'Name':'Khoa','RankPoints':295},'CashKingCash':823000,'RankPoints':8,'Shields':3,'Spins':2,'TotalSpins':2143},'DidReceiveFBBonus':null,'RandomAttackIsland':null,'RandomAttackTarget':null,'RevengeList':null,'StealIslands':[{'Animal':{'IsDamaged':false,'Level':5},'Artifact':{'IsDamaged':false,'Level':1},'Building':{'IsDamaged':false,'Level':4},'Level':12,'Nature':{'IsDamaged':false,'Level':3},'Ship':{'IsDamaged':false,'Level':2}},{'Animal':{'IsDamaged':false,'Level':3},'Artifact':{'IsDamaged':true,'Level':0},'Building':{'IsDamaged':false,'Level':2},'Level':22,'Nature':{'IsDamaged':false,'Level':3},'Ship':{'IsDamaged':false,'Level':2}},{'Animal':{'IsDamaged':false,'Level':3},'Artifact':{'IsDamaged':false,'Level':5},'Building':{'IsDamaged':false,'Level':3},'Level':19,'Nature':{'IsDamaged':false,'Level':5},'Ship':{'IsDamaged':false,'Level':4}}],'WheelResult':6}");
        //steal(data);
        //String test = view("100008656459413");
        //}

        static string key = "55596a2194c0430b70eec43f";
        static String urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE", key);
        static String urlAttack = String.Format("http://prod.cashkinggame.com/CKService.svc/attack/random/{0}/", key);
        static String urlSteal = "http://prod.cashkinggame.com/CKService.svc/v2/attack/steal/?42144.4352499857";
        static String[] arrItems = { "Animals", "Nature", "Building", "Ships", "Artifacts" };
        static void Main(string[] args)
        {
            while (true)
            {
                String retWheel = spinWheel();
                JToken data = JObject.Parse(retWheel);
                int wheelResult = Convert.ToInt16(data["WheelResult"]);
                int spins = Convert.ToInt16(data["PlayerState"]["Spins"]);
                Console.WriteLine(data["PlayerState"].ToString());
                //if (spins == 0) return;
                if (wheelResult == 7) attackRandom(data);
                if (wheelResult == 6) steal(data);
            }
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
        /// 
        /// </summary>
        /// <returns></returns>
        private static string spinWheel()
        {
            return doGet(urlWheel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// {"DidIncrementCacheIndex":true,"ErrorCode":100,"IncentivizedResults":null,"Island":null,"News":null,"NextSpinClaimSeconds":2763,
        /// "PlayerState":{"CacheIndex":3047,"Cash":1730200,"CashKing":{"Avatar":0,"FBID":"952560088095535","Flag":0,"Name":"Khanh Ars","RankPoints":251},"CashKingCash":770200,"RankPoints":8,"Shields":1,"Spins":39,"TotalSpins":2774},"DidReceiveFBBonus":null,
        /// "RandomAttackIsland":{"Animal":{"IsDamaged":false,"Level":1},"Artifact":{"IsDamaged":false,"Level":2},"Building":{"IsDamaged":false,"Level":5},"Level":7,"Nature":{"IsDamaged":false,"Level":0},"Ship":{"IsDamaged":true,"Level":5}},
        /// "RandomAttackTarget":{"Avatar":0,"FBID":"987855511225061","Flag":5,"Name":"Ezz E","RankPoints":168},
        /// "RevengeList":[{"Avatar":9,"ExtraData":"b","FBID":"473592552791564","Flag":9,"Name":"Bog Calm 7","RankPoints":116},{"Avatar":0,"ExtraData":"s","FBID":"771638366276372","Flag":7,"Name":"Faye Luke","RankPoints":52},{"Avatar":0,"ExtraData":"b","FBID":"100001014127256","Flag":0,"Name":"Michael W","RankPoints":316},{"Avatar":1,"ExtraData":"s","Flag":13,"Name":"BeardGinge","RankPoints":225},{"Avatar":8,"ExtraData":"s","FBID":"1114478168578935","Flag":0,"Name":"Play Fan 6","RankPoints":76},{"Avatar":0,"ExtraData":"s","FBID":"100005849432416","Flag":0,"Name":"Innushka","RankPoints":358},{"Avatar":9,"ExtraData":"s","FBID":"917613761592912","Flag":1,"Name":"NhoKhoa","RankPoints":226},{"Avatar":0,"ExtraData":"s","FBID":"10153223750579791","Flag":17,"Name":"Thuy Nho","RankPoints":277},{"Avatar":0,"ExtraData":"d","FBID":"832008230200678","Flag":0,"Name":"Rtrdetre G","RankPoints":8},{"Avatar":0,"ExtraData":"b","FBID":"1596498190567268","Flag":0,"Name":"BlueRoll 3","RankPoints":178}],
        /// "StealIslands":null,"WheelResult":7}
        /// {"DidIncrementCacheIndex":true,"ErrorCode":100,"IncentivizedResults":null,"Island":null,"News":null,"NextSpinClaimSeconds":1327,"PlayerState":{"CacheIndex":3083,"Cash":2827000,"CashKing":{"Avatar":6,"FBID":"609326162544369","Flag":10,"Name":"FogWin 329","RankPoints":123},"CashKingCash":815300,"RankPoints":8,"Shields":3,"Spins":10,"TotalSpins":2808},"DidReceiveFBBonus":null,"RandomAttackIsland":{"Animal":{"IsDamaged":false,"Level":2},"Artifact":{"IsDamaged":true,"Level":2},"Building":{"IsDamaged":false,"Level":3},"Level":16,"Nature":{"IsDamaged":false,"Level":4},"Ship":{"IsDamaged":false,"Level":5}},"RandomAttackTarget":{"Avatar":13,"Flag":0,"Name":"Zap Kid692","RankPoints":391},"RevengeList":[{"Avatar":9,"ExtraData":"b","FBID":"473592552791564","Flag":9,"Name":"Bog Calm 7","RankPoints":116},{"Avatar":0,"ExtraData":"s","FBID":"771638366276372","Flag":7,"Name":"Faye Luke","RankPoints":52},{"Avatar":0,"ExtraData":"b","FBID":"100001014127256","Flag":0,"Name":"Michael W","RankPoints":316},{"Avatar":1,"ExtraData":"s","Flag":13,"Name":"BeardGinge","RankPoints":225},{"Avatar":8,"ExtraData":"s","FBID":"1114478168578935","Flag":0,"Name":"Play Fan 6","RankPoints":76},{"Avatar":0,"ExtraData":"s","FBID":"100005849432416","Flag":0,"Name":"Innushka","RankPoints":358},{"Avatar":9,"ExtraData":"s","FBID":"917613761592912","Flag":1,"Name":"NhoKhoa","RankPoints":226},{"Avatar":0,"ExtraData":"s","FBID":"10153223750579791","Flag":17,"Name":"Thuy Nho","RankPoints":277},{"Avatar":0,"ExtraData":"d","FBID":"832008230200678","Flag":0,"Name":"Rtrdetre G","RankPoints":8},{"Avatar":0,"ExtraData":"b","FBID":"1596498190567268","Flag":0,"Name":"BlueRoll 3","RankPoints":178}],"StealIslands":null,"WheelResult":7}

        //GET /CKService.svc/attack/random/5541b1ac94c0430f980e0cc9/Artifacts?42144.4222831214 HTTP/1.1
        private static string attackRandom(JToken data)
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
            string url = urlAttack + itemAttackName + "?42144.4222831214";
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
        /// <returns></returns>
        /// http://prod.cashkinggame.com/CKService.svc/v2/attack/steal/?42144.4105775445
        /// {"DidIncrementCacheIndex":false,"ErrorCode":100,"IncentivizedResults":null,"Island":null,"News":null,"NextSpinClaimSeconds":2227,
        /// "PlayerState":{"CacheIndex":3053,"Cash":2037200,
        /// "CashKing":{"Avatar":6,"FBID":"609326162544369","Flag":10,"Name":"FogWin 329","RankPoints":123},
        /// "CashKingCash":815300,"RankPoints":8,"Shields":1,"Spins":40,"TotalSpins":2778},"DidReceiveFBBonus":null,"RandomAttackIsland":null,"RandomAttackTarget":null,"RevengeList":null,
        /// "StealIslands":[
        /// {"Animal":{"IsDamaged":false,"Level":1},"Artifact":{"IsDamaged":false,"Level":4},"Building":{"IsDamaged":false,"Level":4},"Level":5,"Nature":{"IsDamaged":false,"Level":4},"Ship":{"IsDamaged":false,"Level":5}},
        /// {"Animal":{"IsDamaged":false,"Level":1},"Artifact":{"IsDamaged":false,"Level":1},"Building":{"IsDamaged":false,"Level":3},"Level":22,"Nature":{"IsDamaged":false,"Level":1},"Ship":{"IsDamaged":false,"Level":3}},
        /// {"Animal":{"IsDamaged":false,"Level":2},"Artifact":{"IsDamaged":false,"Level":3},"Building":{"IsDamaged":false,"Level":2},"Level":18,"Nature":{"IsDamaged":true,"Level":2},"Ship":{"IsDamaged":false,"Level":5}}],
        /// "WheelResult":6}        
        /// {"StealIndex":2,"FriendFBIDs":["10153223750579791","917613761592912"],"secretKey":"5541b1ac94c0430f980e0cc9"}
        /// 
        /// 
        /// {"DidIncrementCacheIndex":true,"ErrorCode":100,"IncentivizedResults":null,"Island":null,"News":null,"NextSpinClaimSeconds":3599,"PlayerState":{"CacheIndex":2388,"Cash":183800,
        /// "CashKing":{"Avatar":7,"FBID":"100008656459413","Flag":15,"Name":"Marwa El M","RankPoints":442},
        /// "CashKingCash":829200,"RankPoints":8,"Shields":1,"Spins":47,"TotalSpins":2183},"DidReceiveFBBonus":null,"RandomAttackIsland":null,"RandomAttackTarget":null,"RevengeList":null,
        /// "StealIslands":[
        /// 1 {"Animal":{"IsDamaged":true,"Level":5},"Artifact":{"IsDamaged":false,"Level":1},"Building":{"IsDamaged":false,"Level":1},"Level":4,"Nature":{"IsDamaged":false,"Level":1},"Ship":{"IsDamaged":false,"Level":2}},
        /// 2 {"Animal":{"IsDamaged":false,"Level":4},"Artifact":{"IsDamaged":false,"Level":1},"Building":{"IsDamaged":true,"Level":4},"Level":18,"Nature":{"IsDamaged":false,"Level":1},"Ship":{"IsDamaged":false,"Level":2}},
        /// 3 {"Animal":{"IsDamaged":true,"Level":5},"Artifact":{"IsDamaged":false,"Level":2},"Building":{"IsDamaged":false,"Level":2},"Level":6,"Nature":{"IsDamaged":true,"Level":2},"Ship":{"IsDamaged":false,"Level":2}}],
        /// "WheelResult":6}
        /// 
        /// http://prod.cashkinggame.com/CKService.svc/v2/attack/steal/?42144.4352499857 
        /// {"StealIndex":1,"FriendFBIDs":["10153223750579791","917613761592912"],"secretKey":"5543d17a94c0420fd8b9d13f"}

        private static string steal(JToken data)
        {
            int index = 1;
            List<Key> lstKeys = new List<Key>();
            foreach (JToken child in data["StealIslands"])
            {
                Key keyItem = new Key();
                keyItem.Index = index;
                keyItem.Level = Convert.ToInt32(child["Level"]);
                lstKeys.Add(keyItem);
                index = index + 1;
            }
            var query = from item in lstKeys
                        orderby item.Level ascending
                        select item;
            lstKeys = query.ToList();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            dicResult.Add("StealIndex", lstKeys[0].Index);
            dicResult.Add("FriendFBIDs", "[]");
            dicResult.Add("secretKey", key);
            String urlSteal = "http://prod.cashkinggame.com/CKService.svc/v2/attack/steal/?42144.4352499857";
            return doPost(urlSteal, JsonConvert.SerializeObject(dicResult));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        private static string view(String fid)
        {
            String urlView = "http://prod.cashkinggame.com/CKService.svc/v3.0/island/view/friend/?42146.5503636935";
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            dicResult.Add("FriendScopedId", fid);
            String dataView = JsonConvert.SerializeObject(dicResult);
            JToken data = JObject.Parse(doPost(urlView, dataView));
            return data["WantedIsland"]["Level"].ToString();
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
        //POST /CKService.svc/v3.0/login/?42147.0475823242 HTTP/1.1
        //http://prod.cashkinggame.com/CKService.svc/v3.0/login/?42147.0475823242
        //{"CampaignReferral":"","DeviceToken":null,"Email":"thuyln@vnext.vn","FBID":"105922806406104","FBName":"Le Nho Khoa","FriendFBIDs":["10153223750579791","917613761592912"],"GCID":null,"GameVersion":215,"Platform":2,"UDID":"108a61cda531152f01e5436ba1a5b4fcf0acc23f","BusinessToken":"Abw_vAiiKYYw_uj6","AccessToken":"CAACUgz7qZAVYBAABg1tq2779080lZCHuX7QANkpoSxA3gkYr5TAsxZApekJ5aW3ngmiGWVq3owflZAWsRurVGb3d0TFOZBya1udbxdZBJQSUEzAkt2YaLTWnZCb7USenI3sciy0E8FFdzJuZAQqitTLXO2YAuHTZB2ybH91zecK5jcZA4H6bnWgVgZCO2o0bF29zm07l7w8TZAFt9QZDZD"}
        //

    }
}
