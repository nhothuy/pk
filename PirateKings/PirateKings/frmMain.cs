using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PirateKings
{
    public partial class frmMain : Form
    {
        private String[] arrItems = { "Animals", "Nature", "Building", "Ships", "Artifacts" };
        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbAccount.SelectedIndex = 0;           
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
        private string getKey()
        {
            String text = cmbAccount.Text;
            switch (text)
            { 
                case "0969023566":
                    return "5541b1ac94c0430f980e0cc9";
                case "0912901720":
                    return "5543d17a94c0420fd8b9d13f";
                case "0974260220":
                    return "5541320594c0430ff49f3672";
                case "0944220487":
                    return "55412a8394c04106c03394a7";
                case "nhothuy48cb@gmail.com":
                    return "5532757194c0430b8c2c9973";
                case "lethiminhht@gmail.com":
                    return "553d2d6994c03f0ed4dd44d8";
            }
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Enabled = false;            
            String urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE?{1}", getKey(), DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            lblPlayer.Text = String.Empty;
            lblInfo.Text = String.Empty;
            avatar.Image = null;
            while (true)
            {
                String retWheel = doGet(urlWheel);
                JToken data = JObject.Parse(retWheel);
                int wheelResult = Convert.ToInt16(data["WheelResult"]);
                int spins = Convert.ToInt16(data["PlayerState"]["Spins"]);
                Console.WriteLine(data["PlayerState"].ToString());
                lblPlayer.Text = String.Format("Rank:{0} Shields:{1} Spins:{2} Cash:{3}", data["PlayerState"]["RankPoints"], data["PlayerState"]["Shields"], data["PlayerState"]["Spins"], Convert.ToInt64(data["PlayerState"]["Cash"]).ToString("#,#", CultureInfo.InvariantCulture));
                lblInfo.Text = String.Format("Name:{1} Rank:{2} Cash:{3}", data["PlayerState"]["CashKing"]["FBID"], data["PlayerState"]["CashKing"]["Name"], data["PlayerState"]["CashKing"]["RankPoints"], Convert.ToInt64(data["PlayerState"]["CashKingCash"]).ToString("#,#", CultureInfo.InvariantCulture));
                displayAvatar(Convert.ToInt32(data["PlayerState"]["CashKing"]["RankPoints"]));
                if (spins == 0 || wheelResult == 6 || wheelResult == 7) {
                    switch (wheelResult)
                    {
                        case 6:
                            MessageBox.Show("Steal...!!!", "Steal", MessageBoxButtons.OK);
                            break;
                        case 7:
                            attackRandom(data);
                            //urlWheel = String.Format("http://prod.cashkinggame.com/CKService.svc/spin/wheel/{0}/NONE?{1}", getKey(), DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                            //doGet(urlWheel);
                            break;
                    }
                    btnOK.Enabled = true;
                    return;
                };                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string attackRandom(JToken data)
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
            String url = String.Format("http://prod.cashkinggame.com/CKService.svc/attack/random/{0}/{1}{2}", getKey(), itemAttackName, "?42144.4222831214");            
            return doGet(url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Item> getRandomAttackIslandItems(JToken data)
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
        private void displayAvatar(Int32 rank)
        {
            try
            {
                int baseName = rank / 30 + 1;
                int baseNamePre = baseName > 1 ? baseName - 1 : baseName;
                int baseNameNext = baseName < 23 ? baseName + 1 : baseName;
                avatarPre.Image = imageList.Images[String.Format("{0}.png", baseNamePre)];
                avatar.Image = imageList.Images[String.Format("{0}.png", baseName)];
                avatarNext.Image = imageList.Images[String.Format("{0}.png", baseNameNext)];
            }
            catch
            { 
            }
        }
    }
}
