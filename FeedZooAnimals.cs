using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FeedingZoo
{
    public class FeedZooAnimals
    {
        static void Main(string[] args)
        {
            FeedZooAnimals objFeedZooAnimals = new FeedZooAnimals();
            objFeedZooAnimals.ParseFiles();
        }

        public double GetWeight(string weightInStr)
        {
            double weight = 0;
            weightInStr = weightInStr.Replace("/>", "");
            weightInStr = weightInStr.Replace("'", "");
            double.TryParse(weightInStr, out weight);
            return weight;
        }

        public double ParseFiles()
        {
            double dblTotalLionWeight = 0;
            double dblTotalGiraffeWeight = 0;
            double dblTotalTigerWeight = 0;
            double dblTotalZebraWeight = 0;
            double dblTotalWolfWeight = 0;
            double dblTotalPiranhaWeight = 0;
            double dblTotal_Meat_EaterWeightage = 0;
            double dblTotal_Fruit_EaterWeightage = 0;
            double totalCost = 0;

            Dictionary<string, KeyValuePair<string, string>> dicAnimalRateType = new Dictionary<string, KeyValuePair<string, string>>();
            Dictionary<string, string> dicFoodTypeAndRate = new Dictionary<string, string>();

            // Get the current executable path and assuming the input files are placed there
            string strCsvPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "animals.csv");
            string strPricePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "prices.txt");
            string strXMLPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "zoo.xml");

            //System.Windows.Forms.MessageBox.Show(strCsvPath);

            //string strCsvPath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName, "animals.csv");
            //string strPricePath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName, "prices.txt");
            //string strXMLPath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName, "zoo.xml");

            try
            {
                if (File.Exists(strCsvPath) && File.Exists(strPricePath) && File.Exists(strXMLPath))
                {
                    // Parse animals.csv
                    using (StreamReader sr1 = new StreamReader(strCsvPath))
                    {
                        string strline = "";
                        while ((strline = sr1.ReadLine()) != null)
                        {
                            string[] strTemp = Regex.Split(strline, ";");
                            if (strTemp.Length > 3 && strTemp[strTemp.Length - 1] == "")
                            {
                                dicAnimalRateType.Add(strTemp[0], new KeyValuePair<string, string>(strTemp[1], strTemp[2]));
                            }
                            else if (strTemp.Length > 3)
                            {
                                dicAnimalRateType.Add(strTemp[0], new KeyValuePair<string, string>(strTemp[1], (strTemp[2] + "||" + strTemp[3])));
                            }
                        }
                    }

                    // Parse prices.txt
                    using (StreamReader sr2 = new StreamReader(strPricePath))
                    {
                        string strline = "";
                        while ((strline = sr2.ReadLine()) != null)
                        {
                            string[] strTemp = Regex.Split(strline, "=");
                            if (strTemp.Length == 2)
                            {
                                dicFoodTypeAndRate.Add(strTemp[0], strTemp[1]);
                            }
                        }
                    }

                    // Parse zoo.xml
                    using (StreamReader sr3 = new StreamReader(strXMLPath))
                    {
                        string strline = "";
                        string strTempVal = "";

                        while ((strline = sr3.ReadLine()) != null)
                        {
                            double weight = 0;
                            if (strline.Contains("Lion") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0)
                                {
                                    weight = GetWeight(strTmpInner[strTmpInner.Length - 1]);
                                    //dblTotalLionWeight += Int32.Parse(strTempVal/*strTmpInner[strTmpInner.Length-1]*/);
                                    dblTotalLionWeight += weight;
                                }
                            }
                            if (strline.Contains("Giraffe") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0)
                                {
                                    weight = GetWeight(strTmpInner[strTmpInner.Length - 1]);
                                    dblTotalGiraffeWeight += weight;
                                }
                            }
                            if (strline.Contains("Tiger") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0)
                                {
                                    weight = GetWeight(strTmpInner[strTmpInner.Length - 1]);
                                    dblTotalTigerWeight += weight;
                                }
                            }
                            if (strline.Contains("Zebra") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0)
                                {
                                    weight = GetWeight(strTmpInner[strTmpInner.Length - 1]);
                                    dblTotalZebraWeight += weight;
                                }
                            }
                            if (strline.Contains("Wolf") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0) { weight = GetWeight(strTmpInner[strTmpInner.Length - 1]); dblTotalWolfWeight += weight; }
                            }
                            if (strline.Contains("Piranha") && strline.Contains("kg="))
                            {
                                string[] strTmpInner = Regex.Split(strline, "kg=");
                                if (strTmpInner.Length > 0)
                                {
                                    weight = GetWeight(strTmpInner[strTmpInner.Length - 1]);
                                    dblTotalPiranhaWeight += weight;
                                }
                            }
                        }
                        double dblLionRate = 0;
                        double dblLionBothFruitRate = 0;
                        double dblTigerRate = 0;
                        double dblTigerBothFruitRate = 0;
                        double dblGiraffeRate = 0;
                        double dblGiraffeBothFruitRate = 0;
                        double dblZebraRate = 0;
                        double dblZebraBothFruitRate = 0;
                        double dblWolfRate = 0;
                        double dblWolfBothFruitRate = 0;
                        double dblPiranhaRate = 0;
                        double dblPiranhaBothFruitRate = 0;

                        foreach (KeyValuePair<string, KeyValuePair<string, string>> pair in dicAnimalRateType)
                        {
                            if (pair.Key.Equals("Lion"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblLionRate = GetMeatPercentage(pair.Value);
                                    dblLionBothFruitRate = double.Parse(pair.Value.Key) - dblLionRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalLionWeight * dblLionRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalLionWeight * dblLionBothFruitRate;
                                }
                                else
                                {
                                    dblLionRate = Double.Parse(pair.Value.Key);
                                    dblTotalLionWeight *= dblLionRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalLionWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalLionWeight; }
                                }
                            }
                            if (pair.Key.Equals("Tiger"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblTigerRate = GetMeatPercentage(pair.Value);
                                    dblTigerBothFruitRate = double.Parse(pair.Value.Key) - dblTigerRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalTigerWeight * dblTigerRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalTigerWeight * dblTigerBothFruitRate;
                                }
                                else
                                {
                                    dblTigerRate = double.Parse(pair.Value.Key);
                                    dblTotalTigerWeight *= dblTigerRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalTigerWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalTigerWeight; }
                                }
                            }
                            if (pair.Key.Equals("Giraffe"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblGiraffeRate = GetMeatPercentage(pair.Value);
                                    dblGiraffeBothFruitRate = double.Parse(pair.Value.Key) - dblGiraffeRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalGiraffeWeight * dblGiraffeRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalGiraffeWeight * dblGiraffeBothFruitRate;
                                }
                                else
                                {
                                    dblGiraffeRate = double.Parse(pair.Value.Key);
                                    dblTotalGiraffeWeight *= dblGiraffeRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalGiraffeWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalGiraffeWeight; }
                                }
                            }
                            if (pair.Key.Equals("Zebra"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblZebraRate = GetMeatPercentage(pair.Value);
                                    dblZebraBothFruitRate = double.Parse(pair.Value.Key) - dblZebraRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalZebraWeight * dblZebraRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalZebraWeight * dblZebraBothFruitRate;
                                }
                                else
                                {
                                    dblZebraRate = double.Parse(pair.Value.Key);
                                    dblTotalZebraWeight *= dblZebraRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalZebraWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalZebraWeight; }
                                }
                            }
                            if (pair.Key.Equals("Wolf"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblWolfRate = GetMeatPercentage(pair.Value);
                                    dblWolfBothFruitRate = double.Parse(pair.Value.Key) - dblWolfRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalWolfWeight * dblWolfRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalWolfWeight * dblWolfBothFruitRate;
                                }
                                else
                                {
                                    dblWolfRate = double.Parse(pair.Value.Key);
                                    dblTotalWolfWeight *= dblWolfRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalWolfWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalWolfWeight; }
                                }
                            }
                            if (pair.Key.Equals("Piranha"))
                            {
                                if (pair.Value.Value.Contains("||"))
                                {
                                    dblPiranhaRate = GetMeatPercentage(pair.Value);
                                    dblPiranhaBothFruitRate = double.Parse(pair.Value.Key) - dblPiranhaRate;
                                    dblTotal_Meat_EaterWeightage += dblTotalPiranhaWeight * dblPiranhaRate;
                                    dblTotal_Fruit_EaterWeightage += dblTotalPiranhaWeight * dblPiranhaBothFruitRate;
                                }
                                else
                                {
                                    dblPiranhaRate = double.Parse(pair.Value.Key);
                                    dblTotalPiranhaWeight *= dblPiranhaRate;
                                    if (pair.Value.Value.Contains("meat")) { dblTotal_Meat_EaterWeightage += dblTotalPiranhaWeight; }
                                    if (pair.Value.Value.Contains("fruit")) { dblTotal_Fruit_EaterWeightage += dblTotalPiranhaWeight; }
                                }
                            }
                        }

                        double dblTotalMeatExpenditure = double.Parse(dicFoodTypeAndRate["Meat"]) * dblTotal_Meat_EaterWeightage;
                        double dblTotalFruitExpenditure = double.Parse(dicFoodTypeAndRate["Fruit"]) * dblTotal_Fruit_EaterWeightage;
                        //listRate.Add(dblTotalMeatExpenditure);
                        //listRate.Add(dblTotalFruitExpenditure);
                       totalCost  = dblTotalFruitExpenditure + dblTotalMeatExpenditure;

                        //int dblTotalMeatExpenditure = Int32.Parse(dicFoodTypeAndRate["Meat"]) * dblTotal_Meat_EaterWeightage;
                        //int dblTotalFruitExpenditure = Int32.Parse(dicFoodTypeAndRate["Fruit"]) * dblTotal_Fruit_EaterWeightage;


                        //System.Windows.Forms.MessageBox.Show("Meat Cost = " + dblTotalMeatExpenditure.ToString() + "\n" + "Fruit Cost = " + dblTotalFruitExpenditure.ToString());
                        
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Inputs missing!!! Files should be in the same folder as the EXE.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return totalCost;
        }
        public static Double GetMeatPercentage(KeyValuePair<string, string> kvPair)
        {
            Double dblPercentageVal = 0;
            try
            {
                if (kvPair.Value.Contains("||"))
                {
                    string strIdx = kvPair.Value.Substring(kvPair.Value.IndexOf("||") + 2);
                    //string[] strTemp = Regex.Split(kvPair.Value, "||");
                    string strVal = strIdx.Replace("%", "");
                    if (strVal != "")
                    {
                        dblPercentageVal = Double.Parse(kvPair.Key) * Double.Parse(strVal) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return dblPercentageVal;
        }
    }
}
