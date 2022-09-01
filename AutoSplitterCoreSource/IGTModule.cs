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

namespace AutoSplitterCore
{
    public class IGTModule
    {
        public int gameSelect = 0;
        SekiroSplitter sekiroSplitter;
        EldenSplitter eldenSplitter;
        Ds3Splitter ds3Splitter;
        Ds1Splitter ds1Splitter;
        CelesteSplitter celesteSplitter;
        CupheadSplitter cupSplitter;
        public int ReturnCurrentIGT()
        {
            switch (gameSelect)
            {
                case 1:
                    return sekiroSplitter.getTimeInGame();
                case 2:
                    return ds1Splitter.getTimeInGame();
                case 4:
                    return ds3Splitter.getTimeInGame();
                case 5:
                    return eldenSplitter.getTimeInGame();
                case 7:
                    return celesteSplitter.getTimeInGame();
                case 8:
                    return cupSplitter.getTimeInGame();

                case 3:
                case 6:
                case 0:
                case 9:
                default:
                    return -1;
            }
        }

        public void setSplitterPointers(SekiroSplitter sekiroSplitter, EldenSplitter eldenSplitter, Ds3Splitter ds3Splitter, CelesteSplitter celesteSplitter, CupheadSplitter cupSplitter, Ds1Splitter ds1Splitter)
        {
            this.sekiroSplitter = sekiroSplitter;
            this.eldenSplitter = eldenSplitter;
            this.ds3Splitter = ds3Splitter;
            this.ds1Splitter = ds1Splitter;
            this.celesteSplitter = celesteSplitter;
            this.cupSplitter = cupSplitter;
        }
    }
}
