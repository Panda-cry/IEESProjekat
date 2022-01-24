using FTN.Common;
using FTN.Common.GDA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        TestGda test;
        ResourceDescription res = new ResourceDescription();
        private static ObservableCollection<DMSType> collection  = new ObservableCollection<DMSType>();
        private static ObservableCollection<ModelCode> properties  = new ObservableCollection<ModelCode>();
        private static ObservableCollection<ModelCode> related  = new ObservableCollection<ModelCode>();
        private static ObservableCollection<string> gids = new ObservableCollection<string>();
        public MainWindow()
        {
            
            InitializeComponent();
            try
            {
                test = new TestGda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void Load()
        {
            ModelResourcesDesc res = new ModelResourcesDesc();
            
            foreach (var item in res.AllDMSTypes)
            {
                if (item.Equals(DMSType.MASK_TYPE))
                    continue;
                collection.Add(item);
            }
           
            comboDMS.ItemsSource = collection;

            foreach (var item in res.AllModelCodes)
            {
              
                related.Add(item);
            }
            
            comboRelated.ItemsSource = related;

            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\TUF\\Desktop\\IEESProjekat\\ModelLabsProjekat\\ModelLabs\\Results\\Gids.txt");
            foreach (var item in lines)
            {
                StringBuilder sb = new StringBuilder();
                long parsed = long.Parse(item);
                sb.AppendFormat("0x{0:x16}", parsed);
                gids.Add(sb.ToString());
            }
            comboGids.ItemsSource = gids;
        }

        
        [STAThread]
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //prepravio sam metodu pa ne vraca resorucedesc nego string sa propertijima

                string strId = comboGids.SelectedItem.ToString().Remove(0, 2);
                string message = test.GetValue(Convert.ToInt64(Int64.Parse(strId, System.Globalization.NumberStyles.HexNumber)));
                textBoxFile.Text = message;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(1000);
            clearSelection();

        }

        private void LoadGids_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //prepravio sam metodu pa ne vraca resorucedesc nego string sa propertijima

                string strId = comboDMS.SelectedItem.ToString();
                ModelCode code = (ModelCode)Enum.Parse(typeof(ModelCode), strId);
                textBoxFile.Text = "";
                List<ModelCode> props = new List<ModelCode>();
                foreach (var item in  items.SelectedItems)
                {
                    props.Add((ModelCode)Enum.Parse(typeof(ModelCode),item.ToString()));
                }
                
                List<long> dataLongs = test.GetExtentValuesModified(code,props);
                foreach (var item in dataLongs)
                {
                    textBoxFile.AppendText(test.GetValue(item));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(1000);
            clearSelection();
        }
        private void LoadProperties()
        {
            if (comboDMS.SelectedItem == null)
                return;
            ModelResourcesDesc res = new ModelResourcesDesc();
            ModelCode strId = (ModelCode)Enum.Parse(typeof(ModelCode),comboDMS.SelectedItem.ToString());
            properties.Clear();
            List < ModelCode > data = res.GetAllPropertyIds(strId);
            foreach (var item in data)
            {
                properties.Add(item);
            }
            //comboProp.ItemsSource = properties;
            items.ItemsSource = properties;
        }
        private void comboDMS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProperties();
        }
        private void clearSelection()
        {
            Thread.Sleep(2000);
            comboDMS.SelectedItem = null;
            comboRelated.SelectedItem = null;
            comboGids.SelectedItem = null;
            items.SelectedItem = null;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {

                textBoxFile.Text = "";
                //source je konkretna klasa
                long modelCode = 0;
                string strId = comboGids.SelectedItem.ToString();
                if (strId.StartsWith("0x", StringComparison.Ordinal))
                {
                    strId = strId.Remove(0, 2);
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");

                    modelCode = Convert.ToInt64(Int64.Parse(strId, System.Globalization.NumberStyles.HexNumber));
                }
                else
                {
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");
                    modelCode =  Convert.ToInt64(strId);
                }
                //veza neka ref 


                Association association = new Association()
                {
                    PropertyId = (ModelCode)Enum.Parse(typeof(ModelCode), comboRelated.SelectedItem.ToString()),
                };
                ModelCode type;
                if (comboDMS.SelectedItem != null)
                {
                    string strtId = comboDMS.SelectedItem.ToString();
                    type = (ModelCode)Enum.Parse(typeof(ModelCode), strtId);
                    association.Type = type;
                }



                
                

                List<long> dataLongs = test.GetRelatedValuesModified(modelCode, association);
                foreach (var item in dataLongs)
                {
                    textBoxFile.AppendText(test.GetValue(item));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(1000);
            clearSelection();
        }
    }
}
