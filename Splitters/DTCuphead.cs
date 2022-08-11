using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Cuphead;

namespace HitCounterManager
{
    public class DefinitionCuphead
    {
        #region ElementToSplit.Cuphead
        [Serializable]
        public class ElementsToSplitCup
        {
            public string Title;
            public bool IsSplited = false;
        }
        #endregion

    }

    [Serializable]
    public class DTCuphead
    {
        //Settings Vars
        public bool enableSplitting = false;
        //Flags to Split
        public List<DefinitionCuphead.ElementsToSplitCup> elementToSplit = new List<DefinitionCuphead.ElementsToSplitCup>();

        public List<DefinitionCuphead.ElementsToSplitCup> getElementToSplit()
        {
            return this.elementToSplit;
        }
    }
}
