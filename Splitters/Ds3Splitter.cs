using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SoulMemory.DarkSouls3;

namespace HitCounterManager
{
    public class Ds3Splitter
    {
        public static DarkSouls3 Ds3 = new DarkSouls3();
        public bool _StatusProcedure = true;
        public bool _StatusDs3 = false;
        public DTDs3 dataDs3;
        public DefinitionsDs3 defD3 = new DefinitionsDs3();
        public ProfilesControl _profile;
        private bool _writeMemory = false;


        public DTDs3 getDataDs3()
        {
            return this.dataDs3;
        }

        public void setDataDs3(DTDs3 data, ProfilesControl profile)
        {
            this.dataDs3 = data;
            this._profile = profile;
        }

        public void setStatusSplitting(bool status)
        {
            dataDs3.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); }
        }

        public bool getDs3StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusDs3 = Ds3.Refresh();
        }

        public int getTimeInGame()
        {
            return Ds3.GetInGameTimeMilliseconds();
        }

        public void clearData()
        {
            dataDs3.bossToSplit.Clear();
            dataDs3.bonfireToSplit.Clear();
            dataDs3.lvlToSplit.Clear();
            dataDs3.flagToSplit.Clear();
        }

        public void resetSplited()
        {
            if (dataDs3.getBossToSplit().Count != 0)
            {
                foreach (var b in dataDs3.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs3.getBonfireToSplit().Count != 0)
            {
                foreach (var bf in dataDs3.getBonfireToSplit())
                {
                    bf.IsSplited = false;
                }
            }

            if (dataDs3.getLvlToSplit().Count != 0)
            {
                foreach (var l in dataDs3.getLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }

            if (dataDs3.getFlagToSplit().Count != 0)
            {
                foreach (var cf in dataDs3.getFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }
        }

        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshDs3();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var task1 = new Task(() =>
            {
                bossToSplit();
            });
            var task2 = new Task(() =>
            {
                bonfireToSplit();
            });

            var task3 = new Task(() =>
            {
                lvlToSplit();
            });

            var task4 = new Task(() =>
            {
                customFlagToSplit();
            });

                taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
        }

        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs3.BossDs3 cBoss = defD3.stringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs3.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataDs3.bossToSplit[position].Id);
            dataDs3.bossToSplit.RemoveAt(position);
        }

        public void AddBonfire(string Bonfire, string mode)
        {
            DefinitionsDs3.BonfireDs3 cBonfire = defD3.stringToEnumBonfire(Bonfire);
            cBonfire.Mode = mode;
            dataDs3.bonfireToSplit.Add(cBonfire);
        }

        public void RemoveBonfire(int position)
        {
            listPendingBon.RemoveAll(iposition => iposition.Id == dataDs3.bonfireToSplit[position].Id);
            dataDs3.bonfireToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs3.LvlDs3 cLvl = new DefinitionsDs3.LvlDs3()
            {
                Attribute = defD3.stringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs3.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs3.lvlToSplit[position].Attribute && ilvl.Value == dataDs3.lvlToSplit[position].Value);
            dataDs3.lvlToSplit.RemoveAt(position);
        }

        public void AddCustomFlag(uint id, string mode)
        {
            DefinitionsDs3.CfDs3 cf = new DefinitionsDs3.CfDs3()
            { Id = id, Mode = mode };
            dataDs3.flagToSplit.Add(cf);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iflag => iflag.Id == dataDs3.flagToSplit[position].Id);
            dataDs3.flagToSplit.RemoveAt(position);
        }


        #region init()
        public void RefreshDs3()
        {
            _StatusDs3 = getDs3StatusProcess(0);
            while (!_StatusProcedure)
            {
                _StatusDs3 = getDs3StatusProcess(45000);
                if (!_StatusDs3)
                {
                    _writeMemory = false;
                }

                if (!_writeMemory)
                {
                    Ds3.WriteInGameTimeMilliseconds(0);
                    _writeMemory = true;
                }
            }
        }

        List<DefinitionsDs3.BossDs3> listPendingB = new List<DefinitionsDs3.BossDs3>();
        List<DefinitionsDs3.BonfireDs3> listPendingBon = new List<DefinitionsDs3.BonfireDs3>();
        List<DefinitionsDs3.LvlDs3> listPendingLvl = new List<DefinitionsDs3.LvlDs3>();
        List<DefinitionsDs3.CfDs3> listPendingCf = new List<DefinitionsDs3.CfDs3>();


        private void checkLoad()
        {
            while (dataDs3.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingB.Count > 0 || listPendingBon.Count > 0 || listPendingLvl.Count > 0)
                {
                    if (!Ds3.IsPlayerLoaded())
                    {
                        foreach (var boss in listPendingB)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var b = dataDs3.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataDs3.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var bone in listPendingBon)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var bo = dataDs3.bonfireToSplit.FindIndex(Ibone => Ibone.Id == bone.Id);
                            dataDs3.bonfireToSplit[bo].IsSplited = true;
                        }

                        foreach (var lvl in listPendingLvl)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var l = dataDs3.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                            dataDs3.lvlToSplit[l].IsSplited = true;
                        }

                        foreach (var cf in listPendingCf)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var c = dataDs3.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                            dataDs3.flagToSplit[c].IsSplited = true;
                        }

                        listPendingB.Clear();
                        listPendingBon.Clear();
                        listPendingLvl.Clear();
                        listPendingCf.Clear();


                    }
                }
            }
        }

        private void bossToSplit()
        {
            while (dataDs3.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var b in dataDs3.getBossToSplit())
                {
                    if (!b.IsSplited && Ds3.ReadEventFlag(b.Id))
                    {
                        if (b.Mode == "Loading game after")
                        {
                            if (!listPendingB.Contains(b))
                            {
                                listPendingB.Add(b);
                            }
                        }
                        else
                        {
                            b.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };

                        }
                    }

                }
            }
        }


        private void bonfireToSplit()
        {
            while (dataDs3.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var bonefire in dataDs3.getBonfireToSplit())
                {
                    if (!bonefire.IsSplited && Ds3.ReadEventFlag(bonefire.Id))
                    {
                        if (bonefire.Mode == "Loading game after")
                        {
                            if (!listPendingBon.Contains(bonefire))
                            {
                                listPendingBon.Add(bonefire);
                            }
                        }
                        else
                        {
                            bonefire.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };

                        }
                    }

                }
            }
        }

        private void lvlToSplit()
        {
            while (dataDs3.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var lvl in dataDs3.getLvlToSplit())
                {
                    if (!lvl.IsSplited && Ds3.ReadAttribute(lvl.Attribute) >= lvl.Value)
                    {
                        if (lvl.Mode == "Loading game after")
                        {
                            if (!listPendingLvl.Contains(lvl))
                            {
                                listPendingLvl.Add(lvl);
                            }
                        }
                        else
                        {
                            lvl.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                        }
                    }
                }
            }
        }

        private void customFlagToSplit()
        {
            while (dataDs3.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var cf in dataDs3.getFlagToSplit())
                {
                    if (!cf.IsSplited && Ds3.ReadEventFlag(cf.Id))
                    {
                        if (cf.Mode == "Loading game after")
                        {
                            if (!listPendingCf.Contains(cf))
                            {
                                listPendingCf.Add(cf);
                            }
                        }
                        else
                        {
                            cf.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };

                        }
                    }

                }
            }
        }
        #endregion
    }
}
