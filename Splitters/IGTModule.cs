//MIT License

//Copyright (c) 2022 Ezequiel Medina

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace HitCounterManager
{
    public class IGTModule
    {
        System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1 };
        public int gameSelect = 0;
        private int IgtMs = 0;
        private string IgtFilePath = string.Empty;
        SekiroSplitter sekiroSplitter;
        HollowSplitter hollowSplitter;
        EldenSplitter eldenSplitter;
        Ds3Splitter ds3Splitter;
        Ds1Splitter ds1Splitter;
        CelesteSplitter celesteSplitter;
        CupheadSplitter cupSplitter;


        public IGTModule()
        {
            IgtFilePath = Path.GetFullPath("./Designs/IGT_Timer.xml");
            if (!File.Exists(IgtFilePath)) { File.Create(IgtFilePath); }
            _update_timer.Tick += (sender, args) => IGTText();
            _update_timer.Enabled = true;             
        }

        public int ReturnCurrentIGT()
        {
            return IgtMs;
        }

        public void setSplitterPointers(SekiroSplitter sekiroSplitter, HollowSplitter hollowSplitter, EldenSplitter eldenSplitter, Ds3Splitter ds3Splitter, CelesteSplitter celesteSplitter, CupheadSplitter cupSplitter, Ds1Splitter ds1Splitter)
        {
            this.sekiroSplitter = sekiroSplitter;
            this.hollowSplitter = hollowSplitter;
            this.eldenSplitter = eldenSplitter;
            this.ds3Splitter = ds3Splitter;
            this.ds1Splitter = ds1Splitter;
            this.celesteSplitter = celesteSplitter;
            this.cupSplitter = cupSplitter;
        }

        private void IGTText()
        {
            try
            {
                File.WriteAllText(IgtFilePath, string.Empty);
                using (StreamWriter writer = new StreamWriter(IgtFilePath))
                {
                    switch (gameSelect)
                    {
                        case 1:
                            IgtMs = sekiroSplitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;
                        case 2:
                            IgtMs = ds1Splitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;
                        case 4:
                            IgtMs = ds3Splitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;
                        case 5:
                            IgtMs = eldenSplitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;
                        case 7:
                            IgtMs = celesteSplitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;
                        case 8:
                            IgtMs = cupSplitter.getTimeInGame();
                            writer.Write(IgtMs.ToString());
                            break;

                        case 3:
                        case 6:
                        case 0:
                        case 9:
                        default:
                            writer.Write("-1"); break;
                    }
                }
            }catch (Exception) { }
        }
    }
}
