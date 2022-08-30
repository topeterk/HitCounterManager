using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HCM.AutoSplitterCore
{
    public class SaveModule
    {
        /// <summary>
        /// Stores user data in new XML for AutoSplitter
        /// </summary>
        /// 
        public DataAutoSplitter dataAS = new DataAutoSplitter();
        private SekiroSplitter sekiroSplitter = new SekiroSplitter();
        private Ds1Splitter ds1Splitter = new Ds1Splitter();
        private Ds2Splitter ds2Splitter = new Ds2Splitter();
        private Ds3Splitter ds3Splitter = new Ds3Splitter();
        private EldenSplitter eldenSplitter = new EldenSplitter();
        private HollowSplitter hollowSplitter = new HollowSplitter();
        private CelesteSplitter celesteSplitter = new CelesteSplitter();
        private CupheadSplitter cupSplitter = new CupheadSplitter();
        private AslSplitter aslSplitter = new AslSplitter();
        private IGTModule IgtModule = new IGTModule();
        private void SaveAutoSplitterSettings()
        {
            bool newSave = false;
            string savePath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
            string saveBakPath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml.bak");
            if (!File.Exists(savePath))
            {
                newSave = true;
            }

            if (File.Exists(saveBakPath))
            {
                File.Delete(saveBakPath);
            }

            if (!newSave) { File.Move(savePath, saveBakPath); }
            File.Delete(savePath);
            Stream myStream = new FileStream("HitCounterManagerSaveAutoSplitter.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead) });
            dataAS.DataSekiro = sekiroSplitter.getDataSekiro();
            dataAS.DataHollow = hollowSplitter.getDataHollow();
            dataAS.DataElden = eldenSplitter.getDataElden();
            dataAS.DataDs3 = ds3Splitter.getDataDs3();
            dataAS.DataDs2 = ds2Splitter.getDataDs2();
            dataAS.DataDs1 = ds1Splitter.getDataDs1();
            dataAS.DataCeleste = celesteSplitter.getDataCeleste();
            dataAS.DataCuphead = cupSplitter.getDataCuphead();
            dataAS.ASLMethod = aslSplitter.enableSplitting;
            formatter.Serialize(myStream, dataAS);
            myStream.Close();
            XmlDocument Save = new XmlDocument();
            Save.Load(savePath);
            XmlNode Asl = Save.CreateElement("DataASL");
            XmlNode AslData = aslSplitter.getData(Save);
            Asl.AppendChild(AslData);
            Save.DocumentElement.AppendChild(Asl);
            Save.Save(savePath);
        }

        /// <summary>
        /// Load user data in XML for AutoSplitter
        /// </summary>
        private void LoadAutoSplitterSettings(ProfilesControl profiles)
        {
            DTSekiro dataSekiro = null;
            DTHollow dataHollow = null;
            DTElden dataElden = null;
            DTDs3 dataDs3 = null;
            DTDs2 dataDs2 = null;
            DTDs1 dataDs1 = null;
            DTCeleste dataCeleste = null;
            DTCuphead dataCuphead = null;

            try
            {
                Stream myStream = new FileStream("HitCounterManagerSaveAutoSplitter.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer formatter = new XmlSerializer(typeof(DataAutoSplitter), new Type[] { typeof(DTSekiro), typeof(DTHollow), typeof(DTElden), typeof(DTDs3), typeof(DTDs2), typeof(DTDs1), typeof(DTCeleste), typeof(DTCuphead) });
                dataAS = (DataAutoSplitter)formatter.Deserialize(myStream);
                dataSekiro = dataAS.DataSekiro;
                dataHollow = dataAS.DataHollow;
                dataElden = dataAS.DataElden;
                dataDs3 = dataAS.DataDs3;
                dataDs2 = dataAS.DataDs2;
                dataDs1 = dataAS.DataDs1;
                dataCeleste = dataAS.DataCeleste;
                dataCuphead = dataAS.DataCuphead;
                aslSplitter.enableSplitting = dataAS.ASLMethod;
                myStream.Close();
            }
            catch (Exception) { }

            //Case Old Savefile or New file;
            if (dataSekiro == null) { dataSekiro = new DTSekiro(); }
            if (dataHollow == null) { dataHollow = new DTHollow(); }
            if (dataElden == null) { dataElden = new DTElden(); }
            if (dataDs3 == null) { dataDs3 = new DTDs3(); }
            if (dataDs2 == null) { dataDs2 = new DTDs2(); }
            if (dataDs1 == null) { dataDs1 = new DTDs1(); }
            if (dataCeleste == null) { dataCeleste = new DTCeleste(); }
            if (dataCuphead == null) { dataCuphead = new DTCuphead(); }

            sekiroSplitter.setDataSekiro(dataSekiro, profiles);
            hollowSplitter.setDataHollow(dataHollow, profiles);
            eldenSplitter.setDataElden(dataElden, profiles);
            ds3Splitter.setDataDs3(dataDs3, profiles);
            ds2Splitter.setDataDs2(dataDs2, profiles);
            ds1Splitter.setDataDs1(dataDs1, profiles);
            celesteSplitter.setDataCeleste(dataCeleste, profiles);
            cupSplitter.setDataCuphead(dataCuphead, profiles);
            try
            {
                string savePath = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(savePath);

                XmlElement docElements = doc.DocumentElement;
                XmlNodeList nodeList = docElements.SelectNodes("//DataASL");

                foreach (XmlNode node in nodeList)
                {
                    aslSplitter.setData(node.FirstChild, profiles);
                }
            }
            catch (Exception) { aslSplitter.setData(null, profiles); }

            IgtModule.setSplitterPointers(getSekiroInstance(), getEldenInstance(), getDs3Instance(), getCelesteInstance(), getCupheadInstance(), getDs1Instance());
        }

        public SekiroSplitter getSekiroInstance()
        {
            return this.sekiroSplitter;
        }

        public HollowSplitter getHollowInstance()
        {
            return this.hollowSplitter;
        }

        public EldenSplitter getEldenInstance()
        {
            return this.eldenSplitter;
        }

        public Ds3Splitter getDs3Instance()
        {
            return this.ds3Splitter;
        }

        public Ds2Splitter getDs2Instance()
        {
            return this.ds2Splitter;
        }

        public Ds1Splitter getDs1Instance()
        {
            return this.ds1Splitter;
        }

        public CelesteSplitter getCelesteInstance()
        {
            return this.celesteSplitter;
        }

        public CupheadSplitter getCupheadInstance()
        {
            return this.cupSplitter;
        }

        public AslSplitter getAslInstance()
        {
            return this.aslSplitter;
        }
    }
}