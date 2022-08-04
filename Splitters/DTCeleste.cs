using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Celeste;

namespace HitCounterManager
{
    public class DefinitionsCeleste
    {
        [Serializable]
        public class ElementToSplitCeleste
        {
            public string Title;
            public bool IsSplited = false;
        }

        public class InfoPlayerCeleste
        {
            public Area areaID;
            public string levelName;
            public double elapsed;
            public bool completed;        
        }
    }
    public class DTCeleste
    {
        //var Settings
        public bool enableSplitting = false;
        //Flags to Split
        public List<DefinitionsCeleste.ElementToSplitCeleste> chapterToSplit = new List<DefinitionsCeleste.ElementToSplitCeleste>();

        public List<DefinitionsCeleste.ElementToSplitCeleste> getChapterToSplit()
        {
            return this.chapterToSplit;
        }
    }
}
